Imports SCT.Library
Imports System.Web.UI.WebControls

Public Class DFClsAdToDos

#Region " Private Fields "

    Private mForm As Object
    Private mData As AdToDoData
    Private mDataList As AdToDoData()

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
        Me.mForm.Session("ValuePath") = "frmAdToDos"

        Me.ApplyPageAuthorizationRules()

        Me.LoadContactsFilter(WebSite.ClsSessionAdmin.GetSessionUserID, Me.mForm.Session("contactID"))
        Me.LoadStartDate()
        Me.LoadEndDate()

        Me.CollectFiltersValuesSelected()
        Me.mDataList = Me.GetDataList(WebSite.ClsSessionAdmin.GetSessionUserID, Me.mAdContactIDs, Me.mStartDate, Me.mEndDate)

        Me.PopulateDataList("DateEntered", "ASC", 0)
    End Sub

    Public Sub ApplyPageAuthorizationRules()
        If Not WebSite.ClsSessionAdmin.IsValidForm("frmAdToDos", "grdAdToDos.ID") Then Me.mForm.Response.Redirect("~/Forms/frmMain.aspx")
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
        For i As Integer = 2000 To Date.Now.Year
            table.Rows.Add(New Object() {i.ToString, i.ToString})
        Next
        Return table
    End Function

    Public Shared Sub HidenMinDate(ByVal gridView As GridView, ByVal row As System.Web.UI.WebControls.GridViewRow)
        If row.RowType = DataControlRowType.DataRow Then
            If row.Cells(3).Text = "01/01/1900" Then
                row.Cells(3).Text = "---"
            End If
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
        table.Columns.Add("ContactID", GetType(Long))
        table.Columns.Add("Contact", GetType(String))
        table.Columns.Add("DateEntered", GetType(Date))
        table.Columns.Add("DateCompleted", GetType(Date))
        table.Columns.Add("Notes", GetType(String))
        table.DefaultView.Sort = Me.GetSortProperty

        For Each todo As AdToDoData In Me.mDataList
            table.Rows.Add(New Object() {todo.ID, todo.Contact.ID, todo.Contact.FullName, todo.DateEntered, todo.DateCompleted, todo.TaskNotes})
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
                Return sortProperty & ", DateEntered " & Me.mSortDirection & ", DateCompleted " & Me.mSortDirection
            Case "DateEntered"
                Return sortProperty & ", DateCompleted " & Me.mSortDirection & ", Contact " & Me.mSortDirection
            Case "DateCompleted"
                Return sortProperty & ", DateEntered " & Me.mSortDirection & ", Contact " & Me.mSortDirection
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
        CollectFiltersValuesSelected()
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

    Public Sub OnView(ByVal todoId As Long)
        Me.mForm.Session("todoID") = todoId
        Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdToDoInformation.aspx")
    End Sub

    Public Sub OnContact(ByVal contactId As Long)
        Me.mForm.Session("cContactID") = contactId
        Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdContactInformation.aspx")
    End Sub

    Public Sub OnEditing(ByVal todoId As Long)
        Me.mForm.Session("toDoID") = todoId
        Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdChangeToDoInformation.aspx")
    End Sub

    Public Sub OnDeleting(ByVal todoId As Long)
        If IsNumeric(todoId) AndAlso (Not todoId = 0) Then
            Me.mData = New AdToDoData
            Me.mData.ID = todoId
            Me.mForm.FindControls("MsgBox").ShowConfirmation("Are you sure you want to delete this To Do?\n\nNote: there is no undo.", "", True, True)
        End If
    End Sub

    Public Sub OnOkDelete()
        If Me.DeleteData Then
            Me.mDataList = Me.GetDataList(WebSite.ClsSessionAdmin.GetSessionUserID, Me.mAdContactIDs, Me.mStartDate, Me.mEndDate)
            Me.PopulateDataList("DateEntered", "ASC", 0)
        End If
    End Sub

    Public Sub OnAddNew()
        Me.mForm.Session("toDoID") = 0
        Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdChangeToDoInformation.aspx")
    End Sub

#End Region

#Region " Data Methods "

    Private Sub PopulateDataList(ByVal SortExpression As String, ByVal SortDirection As String, ByVal PageIndex As Integer)
        Me.mPageIndex = PageIndex
        Me.mSortDirection = SortDirection
        Me.mSortExpression = SortExpression
        Me.LoadGridView(Me.mForm.FindControls("grdAdToDos"))
    End Sub

    Private Sub PopulateDataList(ByVal PageIndex As Integer)
        Me.mPageIndex = PageIndex
        Me.LoadGridView(Me.mForm.FindControls("grdAdToDos"))
    End Sub

    Private Function CollectDataID() As AdToDoNewData
        Dim formData As New AdToDoNewData
        formData.ID.SetValues("grdAdToDos.ID", True, 0, Me.mData.ID)
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

    Private Function GetDataList(ByVal idAdAccount As Long, ByVal idAdContacts() As Long, ByVal fromDate As Date, ByVal toDate As Date) As AdToDoData()
        Try
            Return WebSite.ClsSessionAdmin.GetAdToDoList(idAdAccount, idAdContacts, fromDate, toDate)
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return New AdToDoData() {}
        End Try
    End Function

    Private Function DeleteData() As Boolean
        Try
            WebSite.ClsSessionAdmin.DeleteAdToDo(Me.CollectDataID)
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error deleting: " & SysEx.Message)
            Return False
        End Try
    End Function

#End Region

End Class
