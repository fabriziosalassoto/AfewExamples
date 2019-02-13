Imports SCT.Library
Imports SCT.Library.AdminSite
Imports System.Web.UI.WebControls

Public Class DFClsAdvertiserProjects

#Region " Private Fields "

    Private mForm As Object
    Private mData As AdProjectData
    Private mDataList As AdProjectData()

    Private mPageIndex As Integer
    Private mSortDirection As String
    Private mSortExpression As String

    Private mAdAccountIDs() As Long
    Private mAdContactIDs() As Long

#End Region

#Region " Authorization Rules "

    Public Shared Function CanSelect() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserProjects") Then
            Return ClsSessionAdmin.CanSelectInForm("frmAdvertiserProjects")
        Else
            Return ClsSessionAdmin.CanSelectInForm("AllForms")
        End If
    End Function

    Public Shared Function CanInsert() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserProjects") Then
            Return ClsSessionAdmin.CanInsertInForm("frmAdvertiserProjects")
        Else
            Return ClsSessionAdmin.CanInsertInForm("AllForms")
        End If
    End Function

    Public Shared Function CanUpdate() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserProjects") Then
            Return ClsSessionAdmin.CanUpdateInForm("frmAdvertiserProjects")
        Else
            Return ClsSessionAdmin.CanUpdateInForm("AllForms")
        End If
    End Function

    Public Shared Function CanDelete() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserProjects") Then
            Return ClsSessionAdmin.CanDeleteInForm("frmAdvertiserProjects")
        Else
            Return ClsSessionAdmin.CanDeleteInForm("AllForms")
        End If
    End Function

    Public Shared Function CanSelectField(ByVal fieldName As String) As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserProjects") Then
            Return ClsSessionAdmin.CanSelectFieldInForm("frmAdvertiserProjects", fieldName)
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
        Me.mForm.Session("ValuePath") = "frmAdvertisers/frmAdvertiserProjects"

        Me.ApplyPageAuthorizationRules()

        Me.LoadAdvertisersList(Me.mForm.Session("ID1"))
        Me.LoadContactsList(Me.mForm.Session("ID2"))

        Me.CollectFiltersValuesSelected()
        Me.mDataList = Me.GetDataList(Me.mAdAccountIDs, Me.mAdContactIDs)

        Me.PopulateDataList("ID", "ASC", 0)
    End Sub

    Public Sub ApplyPageAuthorizationRules()
        If (Not ClsSessionAdmin.IsValidForm("frmAdvertiserProjects", "grdAdProjects.ID")) OrElse (Not DFClsAdvertiserProjects.CanSelect()) Then Me.mForm.Response.Redirect("~/Forms/frmMain.aspx")

        Me.mForm.FindControls("grdAdProjects").Columns(4).Visible = DFClsAdvertiserProject.CanUpdate
        Me.mForm.FindControls("grdAdProjects").Columns(5).Visible = DFClsAdvertiserProjects.CanDelete

        Me.mForm.FindControls("mnuAddNew").FindItem("AddNew").Enabled = DFClsAdvertiserProjects.CanInsert()
    End Sub

    Public Shared Sub ApplyGridRowAuthorizationRules(ByVal gridView As GridView, ByVal row As System.Web.UI.WebControls.GridViewRow)
        If row.RowType = DataControlRowType.DataRow Then
            Dim canSelectProject As Boolean = DFClsAdvertiserProject.CanSelect

            row.Cells(0).Controls(1).Visible = canSelectProject
            row.Cells(0).Controls(2).Visible = Not canSelectProject

            Dim canSelectAdvertiser As Boolean = DFClsAdvertiserAccount.CanSelect

            row.Cells(2).Controls(1).Visible = canSelectAdvertiser
            row.Cells(2).Controls(2).Visible = Not canSelectAdvertiser

            Dim canSelectContact As Boolean = DFClsAdvertiserContact.CanSelect

            row.Cells(3).Controls(1).Visible = canSelectContact
            row.Cells(3).Controls(2).Visible = Not canSelectContact
        End If
    End Sub

    Public Sub LoadAdvertisersList(ByVal SelectedValue As String)
        Me.LoadList(Me.mForm.FindControls("lstAdvertisers"), Me.GetListDataSources(Me.GetAdAccountInfoList), SelectedValue)
    End Sub

    Public Sub LoadContactsList(ByVal SelectedValue As String)
        Dim AdvertiserIDsSelected As Long() = Me.CollectIDsSelected(Me.mForm.FindControls("lstAdvertisers").Items)
        If AdvertiserIDsSelected.Length = 1 AndAlso AdvertiserIDsSelected(0) <> 0 Then
            Me.LoadList(Me.mForm.FindControls("lstContacts"), Me.GetListDataSources(Me.GetAdContactInfoList(AdvertiserIDsSelected(0))), SelectedValue)
        Else
            Me.LoadList(Me.mForm.FindControls("lstContacts"), Me.GetListDataSources(New AdContactData() {}), "0")
        End If
    End Sub

    Private Sub LoadList(ByVal list As Object, ByVal data As DataTable, ByVal SelectedValue As String)
        list.DataSource = data
        list.DataTextField = data.Columns(0).Caption
        list.DataValueField = data.Columns(1).Caption
        list.DataBind()
        If list.Items.FindByValue(SelectedValue) IsNot Nothing Then
            list.SelectedValue = SelectedValue
        End If
    End Sub

    Private Function NewListDataTable(Of T)(ByVal text As String, ByVal value As T) As DataTable
        Dim table As New DataTable
        table.Columns.Add("Text", GetType(String))
        table.Columns.Add("Value", GetType(T))
        table.DefaultView.Sort = "Text"
        table.Rows.Add(New Object() {text, value})
        Return table
    End Function

    Private Function GetListDataSources(ByVal InfoList() As AdAccountData) As DataTable
        Dim table As DataTable = Me.NewListDataTable(Of Long)("(All)", 0)
        For Each Info As AdAccountData In InfoList
            table.Rows.Add(New Object() {Info.CompanyName, Info.ID})
        Next
        Return table
    End Function

    Private Function GetListDataSources(ByVal InfoList() As AdContactData) As DataTable
        Dim table As DataTable = Me.NewListDataTable(Of Long)("(All)", 0)
        For Each Info As AdContactData In InfoList
            table.Rows.Add(New Object() {Info.FullName, Info.ID})
        Next
        Return table
    End Function

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
        table.Columns.Add("URL", GetType(String))
        table.Columns.Add("AdvertiserID", GetType(Long))
        table.Columns.Add("Advertiser", GetType(String))
        table.Columns.Add("ContactID", GetType(Long))
        table.Columns.Add("Contact", GetType(String))

        table.DefaultView.Sort = Me.GetSortProperty

        For Each project As AdProjectData In Me.mDataList
            table.Rows.Add(New Object() {project.ID, project.ADUrl, project.Advertiser.ID, project.Advertiser.CompanyName, project.Contact.ID, project.Contact.FullName})
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
        Dim sortProperty As String = Me.mSortExpression & " " & Me.mSortDirection

        Select Case Me.mSortExpression
            Case "URL"
                Return sortProperty & ", Advertiser " & Me.mSortDirection & ", Contact " & Me.mSortDirection
            Case "Advertiser"
                Return sortProperty & ", Contact " & Me.mSortDirection & ", URL " & Me.mSortDirection
            Case "Contact"
                Return sortProperty & ", Advertiser " & Me.mSortDirection & ", URL " & Me.mSortDirection
            Case Else
                Return sortProperty
        End Select
    End Function

    Public Sub OnSubmitQuery()
        Me.CollectFiltersValuesSelected()
        Me.mDataList = Me.GetDataList(Me.mAdAccountIDs, Me.mAdContactIDs)
        Me.PopulateDataList("ID", "ASC", 0)
    End Sub

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

    Public Sub OnAdAccount(ByVal adAccountID As String)
        Me.mForm.Session("ID1") = adAccountID
        Me.mForm.Response.Redirect("~/Forms/frmAdvertiserAccount.aspx")
    End Sub

    Public Sub OnAdContact(ByVal adContactID As String)
        Me.mForm.Session("ID1") = adContactID
        Me.mForm.Response.Redirect("~/Forms/frmAdvertiserContact.aspx")
    End Sub

    Public Sub OnEditing(ByVal adProjectID As String)
        Me.mForm.Session("ID1") = adProjectID
        Me.mForm.Response.Redirect("~/Forms/frmAdvertiserProject.aspx")
    End Sub

    Public Sub OnDeleting(ByVal adProjectID As String)
        If IsNumeric(adProjectID) AndAlso (Not adProjectID = 0) Then
            Me.mData = New AdProjectData
            Me.mData.ID = adProjectID
            Me.mForm.FindControls("MsgBox").ShowConfirmation("Are you sure you want to delete this Project?\n\nNote: there is no undo.", "", True, False)
        End If
    End Sub

    Public Sub OnPageIndexChanging(ByVal NewPageIndex As Integer)
        Me.PopulateDataList(NewPageIndex)
    End Sub

    Public Sub OnMsgBoxOk()
        If Me.DeleteData Then
            Me.CollectFiltersValuesSelected()
            Me.mDataList = Me.GetDataList(Me.mAdAccountIDs, Me.mAdContactIDs)
            Me.PopulateDataList("ID", "ASC", 0)
        End If
    End Sub

    Public Sub OnAddNew()
        Me.mForm.Session("ID1") = 0
        Me.mForm.Response.Redirect("~/Forms/frmAdvertiserProject.aspx")
    End Sub

#End Region

#Region " Data Methods "

    Private Sub PopulateDataList(ByVal SortExpression As String, ByVal SortDirection As String, ByVal PageIndex As Integer)
        Me.mPageIndex = PageIndex
        Me.mSortDirection = SortDirection
        Me.mSortExpression = SortExpression
        Me.LoadGridView(Me.mForm.FindControls("grdAdProjects"))
    End Sub

    Private Sub PopulateDataList(ByVal PageIndex As Integer)
        Me.mPageIndex = PageIndex
        Me.LoadGridView(Me.mForm.FindControls("grdAdProjects"))
    End Sub

    Private Function CollectIDsSelected(ByVal filterList As ListItemCollection) As Long()
        If filterList(0).Selected Then
            Return New Long() {filterList(0).Value}
        Else
            Dim idsSelected As New List(Of Long)
            For Each item As ListItem In filterList
                If item.Selected Then
                    idsSelected.Add(item.Value)
                End If
            Next
            Return idsSelected.ToArray
        End If
    End Function

    Private Sub CollectFiltersValuesSelected()
        Me.mAdAccountIDs = Me.CollectIDsSelected(Me.mForm.FindControls("lstAdvertisers").Items)
        Me.mAdContactIDs = Me.CollectIDsSelected(Me.mForm.FindControls("lstContacts").Items)
    End Sub

    Private Function CollectDataID() As AdProjectNewData
        Dim formData As New AdProjectNewData
        formData.ID.SetValues("grdAdProjects.ID", True, 0, Me.mData.ID)
        Return formData
    End Function

    Private Function GetAdAccountInfoList() As AdAccountData()
        Try
            Return ClsSessionAdmin.GetAdAccountInfoList()
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return New AdAccountData() {}
        End Try
    End Function

    Private Function GetAdContactInfoList(ByVal idAccounts As Long) As AdContactData()
        Try
            Return ClsSessionAdmin.GetAdContactInfoList(idAccounts)
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return New AdContactData() {}
        End Try
    End Function

    Private Function GetDataList(ByVal idAdAccounts() As Long, ByVal idAdContacts() As Long) As AdProjectData()
        Try
            Return ClsSessionAdmin.GetAdProjectList(idAdAccounts, idAdContacts)
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return New AdProjectData() {}
        End Try
    End Function

    Private Function DeleteData() As Boolean
        Try
            ClsSessionAdmin.DeleteAdProject(Me.CollectDataID)
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error deleting: " & SysEx.Message)
            Return False
        End Try
    End Function

#End Region

End Class
