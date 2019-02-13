Imports SCT.Library
Imports SCT.Library.AdminSite
Imports System.Web.UI.WebControls

Public Class DFClsAdvertiserNotes

#Region " Private Fields "

    Private mForm As Object
    Private mData As AdNoteData
    Private mDataList As AdNoteData()

    Private mPageIndex As Integer
    Private mSortDirection As String
    Private mSortExpression As String

    Private mAdAccountIDs() As Long
    Private mAdContactIDs() As Long
    Private mStartDate As Date
    Private mEndDate As Date

#End Region

#Region " Authorization Rules "

    Public Shared Function CanSelect() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserNotes") Then
            Return ClsSessionAdmin.CanSelectInForm("frmAdvertiserNotes")
        Else
            Return ClsSessionAdmin.CanSelectInForm("AllForms")
        End If
    End Function

    Public Shared Function CanInsert() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserNotes") Then
            Return ClsSessionAdmin.CanInsertInForm("frmAdvertiserNotes")
        Else
            Return ClsSessionAdmin.CanInsertInForm("AllForms")
        End If
    End Function

    Public Shared Function CanUpdate() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserNotes") Then
            Return ClsSessionAdmin.CanUpdateInForm("frmAdvertiserNotes")
        Else
            Return ClsSessionAdmin.CanUpdateInForm("AllForms")
        End If
    End Function

    Public Shared Function CanDelete() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserNotes") Then
            Return ClsSessionAdmin.CanDeleteInForm("frmAdvertiserNotes")
        Else
            Return ClsSessionAdmin.CanDeleteInForm("AllForms")
        End If
    End Function

    Public Shared Function CanSelectField(ByVal fieldName As String) As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserNotes") Then
            Return ClsSessionAdmin.CanSelectFieldInForm("frmAdvertiserNotes", fieldName)
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
        Me.mForm.Session("ValuePath") = "frmAdvertisers/frmAdvertiserNotes"

        Me.ApplyPageAuthorizationRules()

        Me.LoadAdvertisersList(Me.mForm.Session("ID1"))
        Me.LoadContactsList(Me.mForm.Session("ID2"))
        Me.LoadStartDate()
        Me.LoadEndDate()

        Me.CollectFiltersValuesSelected()
        Me.mDataList = Me.GetDataList(Me.mAdAccountIDs, Me.mAdContactIDs, Me.mStartDate, Me.mEndDate)

        Me.PopulateDataList("Advertiser", "ASC", 0)
    End Sub

    Public Sub ApplyPageAuthorizationRules()
        If (Not ClsSessionAdmin.IsValidForm("frmAdvertiserNotes", "grdAdNotes.ID")) OrElse (Not DFClsAdvertiserNotes.CanSelect()) Then Me.mForm.Response.Redirect("~/Forms/frmMain.aspx")

        Me.mForm.FindControls("grdAdNotes").Columns(5).Visible = DFClsAdvertiserNote.CanUpdate
        Me.mForm.FindControls("grdAdNotes").Columns(6).Visible = DFClsAdvertiserNotes.CanDelete

        Me.mForm.FindControls("cmdAddNew").FindItem("AddNew").Enabled = DFClsAdvertiserNotes.CanInsert()
    End Sub

    Public Shared Sub ApplyGridRowAuthorizationRules(ByVal gridView As GridView, ByVal row As System.Web.UI.WebControls.GridViewRow)
        If row.RowType = DataControlRowType.DataRow Then
            Dim canSelectNote As Boolean = DFClsAdvertiserNote.CanSelect

            row.Cells(0).Controls(1).Visible = canSelectNote
            row.Cells(0).Controls(2).Visible = Not canSelectNote

            Dim canSelectAdvertiser As Boolean = DFClsAdvertiserAccount.CanSelect

            row.Cells(1).Controls(1).Visible = canSelectAdvertiser
            row.Cells(1).Controls(2).Visible = Not canSelectAdvertiser

            Dim canSelectContact As Boolean = DFClsAdvertiserContact.CanSelect

            row.Cells(2).Controls(1).Visible = canSelectContact
            row.Cells(2).Controls(2).Visible = Not canSelectContact
        End If
    End Sub

    Public Function ValidateDate(ByVal year As String, ByVal month As String, ByVal day As String) As Boolean
        Return (year = String.Empty AndAlso month = String.Empty AndAlso day = String.Empty) OrElse IsDate(year & "-" & month & "-" & day)
    End Function

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

    Public Sub LoadStartDate()
        Me.mForm.FindControls("ddlStartMonth").SelectedValue = String.Empty
        Me.mForm.FindControls("ddlStartDay").SelectedValue = String.Empty
        Me.LoadList(Me.mForm.FindControls("ddlStartYear"), Me.GetListDataSources, String.Empty)
    End Sub

    Public Sub LoadEndDate()
        Me.mForm.FindControls("ddlEndMonth").SelectedValue = String.Empty
        Me.mForm.FindControls("ddlEndDay").SelectedValue = String.Empty
        Me.LoadList(Me.mForm.FindControls("ddlEndYear"), Me.GetListDataSources, String.Empty)
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

    Private Function GetListDataSources() As DataTable
        Dim table As DataTable = Me.NewListDataTable(Of String)(String.Empty, String.Empty)
        For i As Integer = 2000 To Date.Now.Year
            table.Rows.Add(New Object() {i.ToString, i})
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
        table.Columns.Add("DateEntered", GetType(Date))
        table.Columns.Add("Description", GetType(String))
        table.Columns.Add("AdvertiserID", GetType(Long))
        table.Columns.Add("Advertiser", GetType(String))
        table.Columns.Add("ContactID", GetType(Long))
        table.Columns.Add("Contact", GetType(String))

        table.DefaultView.Sort = Me.GetSortProperty

        For Each note As AdNoteData In Me.mDataList
            table.Rows.Add(New Object() {note.ID, note.DateEntered, note.Description, note.Contact.Advertiser.ID, note.Contact.Advertiser.CompanyName, note.Contact.ID, note.Contact.FullName})
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
            Case "DateEntered"
                Return sortProperty & ", Advertiser " & Me.mSortDirection & ", Contact " & Me.mSortDirection
            Case "Advertiser"
                Return sortProperty & ", Contact " & Me.mSortDirection & ", DateEntered " & Me.mSortDirection
            Case "Contact"
                Return sortProperty & ", Advertiser " & Me.mSortDirection & ", DateEntered " & Me.mSortDirection
            Case Else
                Return sortProperty
        End Select
    End Function

    Public Sub OnSubmitQuery()
        Me.CollectFiltersValuesSelected()
        Me.mDataList = Me.GetDataList(Me.mAdAccountIDs, Me.mAdContactIDs, Me.mStartDate, Me.mEndDate)

        Me.PopulateDataList("Advertiser", "ASC", 0)
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

    Public Sub OnEditing(ByVal adNoteID As String)
        Me.mForm.Session("ID1") = adNoteID
        Me.mForm.Response.Redirect("~/Forms/frmAdvertiserNote.aspx")
    End Sub

    Public Sub OnDeleting(ByVal adNoteID As String)
        If IsNumeric(adNoteID) AndAlso (Not adNoteID = 0) Then
            Me.mData = New AdNoteData
            Me.mData.ID = adNoteID
            Me.mForm.FindControls("MsgBox").ShowConfirmation("Are you sure you want to delete this Note?\n\nNote: there is no undo.", "", True, False)
        End If
    End Sub

    Public Sub OnPageIndexChanging(ByVal NewPageIndex As Integer)
        Me.PopulateDataList(NewPageIndex)
    End Sub

    Public Sub OnMsgBoxOk()
        If Me.DeleteData Then
            Me.CollectFiltersValuesSelected()
            Me.mDataList = Me.GetDataList(Me.mAdAccountIDs, Me.mAdContactIDs, Me.mStartDate, Me.mEndDate)

            Me.PopulateDataList("Advertiser", "ASC", 0)
        End If
    End Sub

    Public Sub OnAddNew()
        Me.mForm.Session("ID1") = 0
        Me.mForm.Response.Redirect("~/Forms/frmAdvertiserNote.aspx")
    End Sub

#End Region

#Region " Data Methods "

    Private Sub PopulateDataList(ByVal SortExpression As String, ByVal SortDirection As String, ByVal PageIndex As Integer)
        Me.mPageIndex = PageIndex
        Me.mSortDirection = SortDirection
        Me.mSortExpression = SortExpression
        Me.LoadGridView(Me.mForm.FindControls("grdAdNotes"))
    End Sub

    Private Sub PopulateDataList(ByVal PageIndex As Integer)
        Me.mPageIndex = PageIndex
        Me.LoadGridView(Me.mForm.FindControls("grdAdNotes"))
    End Sub

    Private Sub CollectFiltersValuesSelected()
        Me.mAdAccountIDs = Me.CollectIDsSelected(Me.mForm.FindControls("lstAdvertisers").Items)
        Me.mAdContactIDs = Me.CollectIDsSelected(Me.mForm.FindControls("lstContacts").Items)
        Me.mStartDate = Me.CollectStartDate(Me.mForm.FindControls("ddlStartYear").Text, Me.mForm.FindControls("ddlStartMonth").SelectedValue, Me.mForm.FindControls("ddlStartDay").SelectedValue)
        Me.mEndDate = Me.CollectEndDate(Me.mForm.FindControls("ddlEndYear").Text, Me.mForm.FindControls("ddlEndMonth").SelectedValue, Me.mForm.FindControls("ddlEndDay").SelectedValue)
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

    Private Function CollectStartDate(ByVal year As String, ByVal month As String, ByVal day As String) As Date
        If year = String.Empty AndAlso month = String.Empty AndAlso day = String.Empty Then
            Return New Date(1900, 1, 1)
        Else
            Return New Date(year, month, day)
        End If
    End Function

    Private Function CollectEndDate(ByVal year As String, ByVal month As String, ByVal day As String) As Date
        If year = String.Empty AndAlso month = String.Empty AndAlso day = String.Empty Then
            Return Date.MaxValue.Date
        Else
            Return New Date(year, month, day)
        End If
    End Function

    Private Function CollectDataID() As AdNoteNewData
        Dim formData As New AdNoteNewData
        formData.ID.SetValues("grdAdNotes.ID", True, 0, Me.mData.ID)
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

    Private Function GetDataList(ByVal idAdAccounts() As Long, ByVal idAdContacts() As Long, ByVal fromDate As Date, ByVal toDate As Date) As AdNoteData()
        Try
            Return ClsSessionAdmin.GetAdNoteList(idAdAccounts, idAdContacts, fromDate, toDate)
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return New AdNoteData() {}
        End Try
    End Function

    Private Function DeleteData() As Boolean
        Try
            ClsSessionAdmin.DeleteAdNote(Me.CollectDataID)
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error deleting: " & SysEx.Message)
            Return False
        End Try
    End Function

#End Region

End Class
