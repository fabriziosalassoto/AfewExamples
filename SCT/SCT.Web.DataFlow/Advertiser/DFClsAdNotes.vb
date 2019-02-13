Imports SCT.Library
Imports System.Web.UI.WebControls

Public Class DFClsAdNotes

#Region " Private Fields "

    Private mForm As Object
    Private mData As AdNoteData
    Private mDataList As AdNoteData()

    Private mPageIndex As Integer
    Private mSortDirection As String
    Private mSortExpression As String

    Private mAdContactIDs() As Long
    Private mStartDate As Date
    Private mEndDate As Date

#End Region

#Region " Properties And Methods "

    Public WriteOnly Property Form() As Object
        Set(ByVal value As Object)
            Me.mForm = value
        End Set
    End Property

    Public Sub New(ByRef form As Object)
        Me.mForm = form
        Me.mForm.Session("ValuePath") = "frmAdNotes"

        Me.ApplyPageAuthorizationRules()

        Me.LoadContactsFilter(WebSite.ClsSessionAdmin.GetSessionUserID, Me.mForm.Session("contactID"))
        Me.LoadStartDate()
        Me.LoadEndDate()

        Me.CollectFiltersValuesSelected()
        Me.mDataList = Me.GetDataList(WebSite.ClsSessionAdmin.GetSessionUserID, Me.mAdContactIDs, Me.mStartDate, Me.mEndDate)

        Me.PopulateDataList("DateEntered", "ASC", 0)
    End Sub

    Public Sub ApplyPageAuthorizationRules()
        If Not WebSite.ClsSessionAdmin.IsValidForm("frmAdNotes", "grdAdNotes.ID") Then Me.mForm.Response.Redirect("~/Forms/frmMain.aspx")
    End Sub

    Public Function ValidateStartDate() As Boolean
        Return Me.ValidateDate(Me.mForm.FindControls("ddlStartYear").SelectedValue, Me.mForm.FindControls("ddlStartMonth").SelectedValue, Me.mForm.FindControls("ddlStartDay").SelectedValue)
    End Function

    Public Function ValidateEndDate() As Boolean
        Return Me.ValidateDate(Me.mForm.FindControls("ddlEndYear").SelectedValue, Me.mForm.FindControls("ddlEndMonth").SelectedValue, Me.mForm.FindControls("ddlEndDay").SelectedValue)
    End Function

    Private Function ValidateDate(ByVal year As String, ByVal month As String, ByVal day As String) As Boolean
        Return (year = String.Empty AndAlso month = String.Empty AndAlso day = String.Empty) OrElse IsDate(year & "-" & month & "-" & day)
    End Function

    Public Sub LoadContactsFilter(ByVal idAdAccount As Long, ByVal SelectedValue As String)
        LoadList(Me.mForm.FindControls("lstAdContacts"), Me.GetListDataSource(Me.GetAdContactInfoList(idAdAccount)), SelectedValue)
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

    Private Function GetListDataSource(ByVal InfoList() As AdContactData) As DataTable
        Dim table As DataTable = Me.NewListDataTable(Of Long)("(All)", 0)
        For Each Info As AdContactData In InfoList
            table.Rows.Add(New Object() {Info.FullName, Info.ID})
        Next
        Return table
    End Function

    Private Function GetListDataSources() As DataTable
        Dim table As DataTable = Me.NewListDataTable(Of String)(String.Empty, String.Empty)
        For i As Integer = 2001 To Date.Now.Year
            table.Rows.Add(New Object() {i.ToString, i.ToString})
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
        table.Columns.Add("ContactID", GetType(Long))
        table.Columns.Add("Contact", GetType(String))
        table.Columns.Add("DateEntered", GetType(Date))
        table.Columns.Add("Description", GetType(String))
        table.DefaultView.Sort = Me.GetSortProperty

        For Each note As AdNoteData In Me.mDataList
            table.Rows.Add(New Object() {note.ID, note.Contact.ID, note.Contact.FullName, note.DateEntered, note.Description})
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
            Case "Contact"
                Return sortProperty & ", DateEntered " & Me.mSortDirection
            Case "DateEntered"
                Return sortProperty & ", Contact " & Me.mSortDirection
            Case Else
                Return sortProperty
        End Select
    End Function

    Private Sub CollectFiltersValuesSelected()
        Me.mAdContactIDs = Me.CollectIDsSelected(Me.mForm.FindControls("lstAdContacts").Items)
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

    Public Sub SubmitQuery()
        Me.CollectFiltersValuesSelected()
        Me.mDataList = Me.GetDataList(WebSite.ClsSessionAdmin.GetSessionUserID, Me.mAdContactIDs, Me.mStartDate, Me.mEndDate)
        Me.PopulateDataList("DateEntered", "ASC", 0)
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

    Public Sub OnPageIndexChanging(ByVal NewPageIndex As Integer)
        Me.PopulateDataList(NewPageIndex)
    End Sub

    Public Sub OnView(ByVal noteId As Long)
        Me.mForm.Session("noteID") = noteId
        Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdNoteInformation.aspx")
    End Sub

    Public Sub OnContact(ByVal contactId As Long)
        Me.mForm.Session("contactID") = contactId
        Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdContactInformation.aspx")
    End Sub

    Public Sub OnEditing(ByVal noteId As Long)
        Me.mForm.Session("noteID") = noteId
        Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdChangeNoteInformation.aspx")
    End Sub

    Public Sub OnDeleting(ByVal noteId As Long)
        If IsNumeric(noteId) AndAlso (Not noteId = 0) Then
            Me.mData = New AdNoteData
            Me.mData.ID = noteId
            Me.mForm.FindControls("MsgBox").ShowConfirmation("Are you sure you want to delete this Note?\n\nNote: there is no undo.", "", True, True)
        End If
    End Sub

    Public Sub OnOkDelete()
        If Me.DeleteData Then
            Me.mDataList = Me.GetDataList(WebSite.ClsSessionAdmin.GetSessionUserID, Me.mAdContactIDs, Me.mStartDate, Me.mEndDate)
            Me.PopulateDataList("DateEntered", "ASC", 0)
        End If
    End Sub

    Public Sub OnAddNew()
        Me.mForm.Session("noteID") = 0
        Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdChangeNoteInformation.aspx")
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

    Private Function CollectDataID() As AdNoteNewData
        Dim formData As New AdNoteNewData
        formData.ID.SetValues("grdAdNotes.ID", True, 0, Me.mData.ID)
        Return formData
    End Function

    Private Function GetAdContactInfoList(ByVal idAdAccount As Long) As AdContactData()
        Try
            Return WebSite.ClsSessionAdmin.GetAdContactInfoList(idAdAccount)
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return New AdContactData() {}
        End Try
    End Function

    Private Function GetDataList(ByVal idAdAccount As Long, ByVal idAdContacts() As Long, ByVal fromDate As Date, ByVal toDate As Date) As AdNoteData()
        Try
            Return WebSite.ClsSessionAdmin.GetAdNoteList(idAdAccount, idAdContacts, fromDate, toDate)
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return New AdNoteData() {}
        End Try
    End Function

    Private Function DeleteData() As Boolean
        Try
            WebSite.ClsSessionAdmin.DeleteAdNote(Me.CollectDataID)
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error deleting: " & SysEx.Message)
            Return False
        End Try
    End Function

#End Region

End Class
