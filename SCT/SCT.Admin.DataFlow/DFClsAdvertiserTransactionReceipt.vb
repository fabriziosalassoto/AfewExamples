Imports SCT.Library
Imports SCT.Library.AdminSite
Imports System.Web.UI.WebControls

Public Class DFClsAdvertiserTransactionReceipt

#Region " Private Fields "

    Private mForm As Object
    Private mData As AdReceiptData

#End Region

#Region " Authorization Rules "

    Public Shared Function CanSelect() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserTransactionReceipt") Then
            Return ClsSessionAdmin.CanSelectInForm("frmAdvertiserTransactionReceipt")
        Else
            Return ClsSessionAdmin.CanSelectInForm("AllForms")
        End If
    End Function

#End Region

#Region " Properties And Methods "

    Public WriteOnly Property Form() As Object
        Set(ByVal value As Object)
            Me.mForm = value
        End Set
    End Property

    Public Sub New(ByRef form As Object)
        Me.mForm = form

        Me.ApplyPageAuthorizationRules()

        If IsNumeric(Me.mForm.Session("ID")) Then
            Me.mData = Me.GetData(Me.mForm.Session("ID"))
            If Me.mData IsNot Nothing Then
                Me.PopulateData()
                Me.PopulateDataDetails()
            End If
        End If
    End Sub

    Public Sub ApplyPageAuthorizationRules()
        If (Not ClsSessionAdmin.IsValidForm("frmAdvertiserTransactionReceipt", "txtReceiptNumber", "txtPaymentDate", "txtPaymentType", "txtPaymentNunber", "txtPaymentAmount", "grdDetails")) OrElse (Not DFClsAdvertiserTransactionInvoice.CanSelect()) Then Me.mForm.Response.Write("<script>window.close;</script>")
    End Sub

    Private Sub LoadGridView(ByVal gridview As GridView)
        Me.AddRowData(gridview)
        Me.AddFooterData(gridview)
    End Sub

    Private Sub AddRowData(ByVal GridView As System.Web.UI.WebControls.GridView)
        GridView.DataSource = Me.GetGridDataSources()
        GridView.DataBind()
    End Sub

    Private Function GetGridDataSources() As DataTable
        Dim table As New DataTable

        table.Columns.Add("ID", GetType(Long))
        table.Columns.Add("Advertiser", GetType(String))
        table.Columns.Add("Project", GetType(String))
        table.Columns.Add("PaidByDisplay", GetType(Double))
        table.Columns.Add("PaidByClick", GetType(Double))
        table.Columns.Add("TotalPaid", GetType(Double))

        table.DefaultView.Sort = "Advertiser, Project"

        For Each project As AdReceiptProjectData In Me.mData.ProjectList.Projects
            table.Rows.Add(New Object() {project.ID, project.Advertiser.CompanyName, project.ADUrl, project.PaidByDisplay, project.PaidByClickThrough, project.TotalPaid})
        Next
        Return table
    End Function

    Private Sub AddFooterData(ByVal GridView As System.Web.UI.WebControls.GridView)
        GridView.FooterRow.Cells(2).Text = Me.mData.ProjectList.TotalPaidByDisplay.ToString("#,###,##0.00")
        GridView.FooterRow.Cells(3).Text = Me.mData.ProjectList.TotalPaidByClickThrough.ToString("#,###,##0.00")
        GridView.FooterRow.Cells(4).Text = Me.mData.ProjectList.TotalPaid.ToString("#,###,##0.00")
    End Sub

#End Region

#Region " Data Methods "

    Private Sub PopulateData()
        Me.mForm.FindControl("txtReceiptNumber").Text = Me.mData.ReceiptNumber
        Me.mForm.FindControl("txtPaymentDate").Text = Me.mData.PaymentDate.ToString("MMMM dd, yyyy")
        Me.mForm.FindControl("txtPaymentType").Text = Me.mData.PaymentType
        Me.mForm.FindControl("txtPaymentNunber").Text = Me.mData.PaymentNumber
        Me.mForm.FindControl("txtPaymentAmount").Text = Me.mData.PaymentAmount.ToString("#,###,##0.00")
    End Sub

    Private Sub PopulateDataDetails()
        Me.LoadGridView(Me.mForm.FindControl("grdDetails"))
    End Sub

    Private Function CollectDataID(ByVal receiptID As String) As AdReceiptNewData
        Dim receiptData As New AdReceiptNewData
        receiptData.ID.SetValues("txtReceiptID", True, 0, CLng(receiptID))
        Return receiptData
    End Function

    Private Function GetData(ByVal receiptID As String) As AdReceiptData
        Try
            Return ClsSessionAdmin.GetAdReceipt(Me.CollectDataID(receiptID))
        Catch SysEx As Exception
            Me.mForm.FindControl("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return Nothing
        End Try
    End Function

#End Region

End Class
