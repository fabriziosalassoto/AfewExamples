Imports SCT.Library
Imports SCT.Library.AdminSite
Imports System.Web.UI.WebControls

Public Class DFClsBinnacleFormEntry

#Region " Private Fields "

    Private mForm As Object
    Private mData As BinnacleFormEntryData

#End Region

#Region " Authorization Rules "

    Public Shared Function CanSelect() As Boolean
        Return ClsSessionAdmin.CanSelectInForm("frmBinnacleFormEntry")
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

        If IsNumeric(Me.mForm.Session("Log")) AndAlso IsNumeric(Me.mForm.Session("ID")) Then
            Me.mData = Me.GetData(Me.mForm.Session("Log"), Me.mForm.Session("ID"))
            If Me.mData IsNot Nothing Then
                Me.PopulateData()
                Me.PopulateDataFields()
            End If
        End If
    End Sub

    Public Sub ApplyPageAuthorizationRules()
        If (Not ClsSessionAdmin.IsValidForm("frmBinnacleFormEntry", "txtBinnacleFormEntryID", "txtBinnacleFormEntryLog", "txtBinnacleFormEntryUser", "txtBinnacleFormEntryOperation", "txtBinnacleFormEntryForm", "txtBinnacleFormEntryDate", "txtBinnacleFormEntryHour", "grdBinnacleFormEntryFields")) OrElse (Not DFClsBinnacleFormEntry.CanSelect()) Then Me.mForm.Response.Write("<script>window.close;</script>")
    End Sub

    Private Sub LoadGridView(ByVal gridview As GridView)
        Me.AddRowData(gridview)
    End Sub

    Private Sub AddRowData(ByVal GridView As System.Web.UI.WebControls.GridView)
        GridView.DataSource = Me.GetGridDataSources()
        GridView.DataBind()
    End Sub

    Private Function GetGridDataSources() As DataTable
        Dim table As New DataTable

        table.Columns.Add("Description", GetType(String))
        table.Columns.Add("OldValue", GetType(String))
        table.Columns.Add("NewValue", GetType(String))

        table.DefaultView.Sort = "Description"

        For Each binnacleFormFieldEntry As BinnacleFormFieldEntryData In Me.mData.BinnacleFormFields
            table.Rows.Add(New Object() {binnacleFormFieldEntry.Field.Description, binnacleFormFieldEntry.OldValue, binnacleFormFieldEntry.NewValue})
        Next
        Return table
    End Function

#End Region

#Region " Data Methods "

    Private Sub PopulateData()
        Me.mForm.FindControl("txtBinnacleFormEntryID").Text = Me.mData.ID
        Me.mForm.FindControl("txtBinnacleFormEntryLog").Text = Me.mData.Log.ToString
        Me.mForm.FindControl("txtBinnacleFormEntryDate").Text = Me.mData.BDate.ToString("MMMM dd, yyyy")
        Me.mForm.FindControl("txtBinnacleFormEntryHour").Text = Me.mData.BHour.ToString("hh:mm:ss tt")
        Me.mForm.FindControl("txtBinnacleFormEntryUser").Text = Me.mData.User.Name
        Me.mForm.FindControl("txtBinnacleFormEntryForm").Text = Me.mData.Form.Description
        Me.mForm.FindControl("txtBinnacleFormEntryOperation").Text = Me.mData.Operation.Description
    End Sub

    Private Sub PopulateDataFields()
        Me.LoadGridView(Me.mForm.FindControl("grdBinnacleFormEntryFields"))
    End Sub

    Private Function CollectDataID(ByVal log As String, ByVal binnacleFormEntryID As String) As BinnacleFormEntryNewData
        Dim formData As New BinnacleFormEntryNewData
        formData.ID.SetValues("txtBinnacleFormEntryID", True, 0, CLng(binnacleFormEntryID))
        formData.Log.SetValues("txtBinnacleFormEntryLog", True, 0, CType(log, DataAccess.Logs))
        Return formData
    End Function

    Private Function GetData(ByVal log As String, ByVal binnacleFormEntryID As String) As BinnacleFormEntryData
        Try
            Return ClsSessionAdmin.GetBinnacleFormEntry(Me.CollectDataID(log, binnacleFormEntryID))
        Catch SysEx As Exception
            Me.mForm.FindControl("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return Nothing
        End Try
    End Function

#End Region

End Class
