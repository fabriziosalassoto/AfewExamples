Imports SCT.Library
Imports SCT.Library.AdminSite
Imports System.Web.UI.WebControls

Public Class DFClsAdvertiserTransactionInvoice

#Region " Private Fields "

    Private mForm As Object
    Private mData As AdInvoiceData

#End Region

#Region " Authorization Rules "

    Public Shared Function CanSelect() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserTransactionInvoice") Then
            Return ClsSessionAdmin.CanSelectInForm("frmAdvertiserTransactionInvoice")
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
        If (Not ClsSessionAdmin.IsValidForm("frmAdvertiserTransactionInvoice", "txtInvoiceNumber", "txtInvoiceSequence", "txtInvoiceDate", "txtAdvertiser", "txtProject", "txtChargedToDate", "txtPaidToDate", "txtPreviousClicks", "txtPreviousDisplays", "txtPreviousBalance", "txtTotalAmountDue", "grdDetails")) OrElse (Not DFClsAdvertiserTransactionInvoice.CanSelect()) Then Me.mForm.Response.Write("<script>window.close;</script>")
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
        table.Columns.Add("Displays", GetType(Integer))
        table.Columns.Add("Clicks", GetType(Integer))
        table.Columns.Add("DisplayCost", GetType(Double))
        table.Columns.Add("ClickCost", GetType(Double))
        table.Columns.Add("AmountDue", GetType(Double))

        table.DefaultView.Sort = "ID"

        For Each invoiceDetail As AdInvoiceDetailData In Me.mData.DetailList.Details
            table.Rows.Add(New Object() {invoiceDetail.ID, invoiceDetail.CurrentNumberOfDisplay, invoiceDetail.CurrentNumberOfClickThrough, invoiceDetail.CostPerDisplay, invoiceDetail.CostPerClickThrough, invoiceDetail.AmountDue})
        Next
        Return table
    End Function

    Private Sub AddFooterData(ByVal GridView As System.Web.UI.WebControls.GridView)
        GridView.FooterRow.Cells(4).Text = Me.mData.DetailList.TotalAmountDue.ToString("#,###,##0.00")
    End Sub

#End Region

#Region " Data Methods "

    Private Sub PopulateData()
        Me.mForm.FindControl("txtInvoiceNumber").Text = Me.mData.InvoiceNumber
        Me.mForm.FindControl("txtInvoiceSequence").Text = Me.mData.InvoiceSequence
        Me.mForm.FindControl("txtInvoiceDate").Text = Me.mData.InvoiceDate.ToString("MMMM dd, yyyy")
        Me.mForm.FindControl("txtAdvertiser").Text = Me.mData.Project.Advertiser.CompanyName
        Me.mForm.FindControl("txtProject").Text = Me.mData.Project.ADUrl
        Me.mForm.FindControl("txtChargedToDate").Text = Me.mData.ChargedToDate.ToString("MMMM dd, yyyy")
        Me.mForm.FindControl("txtPaidToDate").Text = Me.mData.PaidToDate.ToString("MMMM dd, yyyy")
        Me.mForm.FindControl("txtPreviousClicks").Text = Me.mData.PreviousNumberOfClickThrough.ToString("#,###,##0")
        Me.mForm.FindControl("txtPreviousDisplays").Text = Me.mData.PreviousNumberOfDisplays.ToString("#,###,##0")
        Me.mForm.FindControl("txtPreviousBalance").Text = Me.mData.PreviousBalance.ToString("#,###,##0.00")
        Me.mForm.FindControl("txtTotalAmountDue").Text = Me.mData.TotalAmountDue.ToString("#,###,##0.00")
    End Sub

    Private Sub PopulateDataDetails()
        Me.LoadGridView(Me.mForm.FindControl("grdDetails"))
    End Sub

    Private Function CollectDataID(ByVal invoiceID As String) As AdInvoiceNewData
        Dim invoiceData As New AdInvoiceNewData
        invoiceData.ID.SetValues("txtInvoiceID", True, 0, CLng(invoiceID))
        Return invoiceData
    End Function

    Private Function GetData(ByVal invoiceID As String) As AdInvoiceData
        Try
            Return ClsSessionAdmin.GetAdInvoice(Me.CollectDataID(invoiceID))
        Catch SysEx As Exception
            Me.mForm.FindControl("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return Nothing
        End Try
    End Function

#End Region

End Class
