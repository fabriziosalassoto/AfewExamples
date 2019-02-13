Imports SCT.Library
Imports SCT.Library.AdminSite
Imports System.Web.UI.WebControls

Public Class DFClsAdvertiserAccounts

#Region " Private Fields "

    Private mForm As Object
    Private mData As AdAccountData
    Private mDataList As AdAccountData()

    Private mPageIndex As Integer
    Private mSortDirection As String
    Private mSortExpression As String

#End Region

#Region " Authorization Rules "

    Public Shared Function CanSelect() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserAccounts") Then
            Return ClsSessionAdmin.CanSelectInForm("frmAdvertiserAccounts")
        Else
            Return ClsSessionAdmin.CanSelectInForm("AllForms")
        End If
    End Function

    Public Shared Function CanInsert() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserAccounts") Then
            Return ClsSessionAdmin.CanInsertInForm("frmAdvertiserAccounts")
        Else
            Return ClsSessionAdmin.CanInsertInForm("AllForms")
        End If
    End Function

    Public Shared Function CanUpdate() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserAccounts") Then
            Return ClsSessionAdmin.CanUpdateInForm("frmAdvertiserAccounts")
        Else
            Return ClsSessionAdmin.CanUpdateInForm("AllForms")
        End If
    End Function

    Public Shared Function CanDelete() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserAccounts") Then
            Return ClsSessionAdmin.CanDeleteInForm("frmAdvertiserAccounts")
        Else
            Return ClsSessionAdmin.CanDeleteInForm("AllForms")
        End If
    End Function

    Public Shared Function CanSelectField(ByVal fieldName As String) As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserAccounts") Then
            Return ClsSessionAdmin.CanSelectFieldInForm("frmAdvertiserAccounts", fieldName)
        Else
            Return ClsSessionAdmin.CanSelectFieldInForm("AllForms", "AllFields")
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
        Me.mForm.Session("ValuePath") = "frmAdvertisers/frmAdvertiserAccounts"

        Me.ApplyPageAuthorizationRules()

        Me.mDataList = Me.GetDataList()
        Me.PopulateDataList("CompanyName", "ASC", 0)
    End Sub

    Public Sub ApplyPageAuthorizationRules()
        If (Not ClsSessionAdmin.IsValidForm("frmAdvertiserAccounts", "grdAdAccounts.ID")) OrElse (Not DFClsAdvertiserAccounts.CanSelect()) Then Me.mForm.Response.Redirect("~/Forms/frmMain.aspx")

        Me.mForm.FindControls("grdAdAccounts").Columns(2).Visible = DFClsAdvertiserAccount.CanUpdate
        Me.mForm.FindControls("grdAdAccounts").Columns(3).Visible = DFClsAdvertiserAccounts.CanDelete

        Me.mForm.FindControls("mnuAddNew").FindItem("AddNew").Enabled = DFClsAdvertiserAccounts.CanInsert()
    End Sub

    Public Shared Sub ApplyGridRowAuthorizationRules(ByVal gridView As GridView, ByVal row As System.Web.UI.WebControls.GridViewRow)
        If row.RowType = DataControlRowType.DataRow Then
            Dim canSelectAdAccount As Boolean = DFClsAdvertiserAccount.CanSelect

            row.Cells(0).Controls(1).Visible = canSelectAdAccount
            row.Cells(0).Controls(2).Visible = Not canSelectAdAccount
        End If
    End Sub

    Private Sub LoadGridView(ByVal gridview As GridView)
        Me.AddRowData(gridview)
        Me.AddSortImage(gridview)
    End Sub

    Private Sub AddRowData(ByVal GridView As System.Web.UI.WebControls.GridView)
        GridView.PageIndex = Me.mPageIndex
        GridView.DataSource = Me.GetDataSources()
        GridView.DataBind()
    End Sub

    Private Function GetDataSources() As DataTable
        Dim table As New DataTable

        table.Columns.Add("ID", GetType(Long))
        table.Columns.Add("CompanyName", GetType(String))

        table.DefaultView.Sort = Me.GetSortProperty

        For Each adAccount As AdAccountData In Me.mDataList
            table.Rows.Add(New Object() {adAccount.ID, adAccount.CompanyName})
        Next
        Return table
    End Function

    Private Sub AddSortImage(ByVal GridView As System.Web.UI.WebControls.GridView)
        If GridView.HeaderRow IsNot Nothing Then
            Dim Image As New Image()
            Dim ColumnIndex As Integer = GetSortColumnIndex(GridView)
            If ColumnIndex <> -1 Then
                Image.ImageUrl = "~/Images/" & Me.mSortDirection & ".GIF"
                GridView.HeaderRow.Cells(ColumnIndex).Controls.Add(Image)
            End If
        End If
    End Sub

    Private Function GetSortColumnIndex(ByVal GridView As System.Web.UI.WebControls.GridView) As Integer
        For Each field As DataControlField In GridView.Columns
            If field.SortExpression = Me.mSortExpression Then
                Return GridView.Columns.IndexOf(field)
            End If
        Next
        Return -1
    End Function

    Private Function GetSortProperty() As String
        Return Me.mSortExpression & " " & Me.mSortDirection
    End Function

    Public Sub OnSorting(ByVal SortExpression As String, ByVal PageIndex As Integer)
        If Me.mSortExpression <> SortExpression Then
            Me.PopulateDataList(SortExpression, "ASC", PageIndex)
        Else
            If Me.mSortDirection = "ASC" Then
                Me.PopulateDataList(SortExpression, "DESC", PageIndex)
            Else
                Me.PopulateDataList(SortExpression, "ASC", PageIndex)
            End If
        End If
    End Sub

    Public Sub OnPageIndexChanging(ByVal NewPageIndex As Integer)
        Me.PopulateDataList(NewPageIndex)
    End Sub

    Public Sub OnEditing(ByVal adAccountID As String)
        Me.mForm.Session("ID1") = adAccountID
        Me.mForm.Response.Redirect("~/Forms/frmAdvertiserAccount.aspx")
    End Sub

    Public Sub OnDeleting(ByVal adAccountID As String)
        If IsNumeric(adAccountID) AndAlso (Not adAccountID = 0) Then
            Me.mData = New AdAccountData
            Me.mData.ID = adAccountID
            Me.mForm.FindControls("MsgBox").ShowConfirmation("Are you sure you want to delete this Account?\n\nNote: there is no undo.", "", True, False)
        End If
    End Sub

    Public Sub OnMsgBoxOk()
        If Me.DeleteData Then
            Me.mDataList = Me.GetDataList()
            Me.PopulateDataList("CompanyName", "ASC", 0)
        End If
    End Sub

    Public Sub OnAddNew()
        Me.mForm.Session("ID1") = 0
        Me.mForm.Response.Redirect("~/Forms/frmAdvertiserAccount.aspx")
    End Sub

#End Region

#Region " Data Methods "

    Private Sub PopulateDataList(ByVal SortExpression As String, ByVal SortDirection As String, ByVal PageIndex As Integer)
        Me.mPageIndex = PageIndex
        Me.mSortDirection = SortDirection
        Me.mSortExpression = SortExpression
        Me.LoadGridView(Me.mForm.FindControls("grdAdAccounts"))
    End Sub

    Private Sub PopulateDataList(ByVal PageIndex As Integer)
        Me.mPageIndex = PageIndex
        Me.LoadGridView(Me.mForm.FindControls("grdAdAccounts"))
    End Sub

    Private Function CollectDataID() As AdAccountNewData
        Dim formData As New AdAccountNewData
        formData.ID.SetValues("grdAdAccounts.ID", True, 0, Me.mData.ID)
        Return formData
    End Function

    Private Function GetDataList() As AdAccountData()
        Try
            Return ClsSessionAdmin.GetAdAccountList()
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return New AdAccountData() {}
        End Try
    End Function

    Private Function DeleteData() As Boolean
        Try
            ClsSessionAdmin.DeleteAdAccount(Me.CollectDataID)
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error deleting: " & SysEx.Message)
            Return False
        End Try
    End Function

#End Region

End Class
