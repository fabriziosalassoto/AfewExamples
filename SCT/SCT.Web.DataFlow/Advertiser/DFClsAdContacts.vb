Imports SCT.Library
Imports System.Web.UI.WebControls

Public Class DFClsAdContacts

#Region " Private Fields "

    Private mForm As Object
    Private mData As AdContactData
    Private mDataList As AdContactData()

    Private mPageIndex As Integer
    Private mSortDirection As String
    Private mSortExpression As String

#End Region

#Region " Properties And Methods "

    Public WriteOnly Property Form() As Object
        Set(ByVal value As Object)
            Me.mForm = value
        End Set
    End Property

    Public Sub New(ByRef form As Object)
        Me.mForm = form
        Me.mForm.Session("ValuePath") = "frmAdContacts"

        Me.ApplyPageAuthorizationRules()

        Me.mDataList = Me.GetDataList(WebSite.ClsSessionAdmin.GetSessionUserID)
        Me.PopulateDataList("FullName", "ASC", 0)
    End Sub

    Public Sub ApplyPageAuthorizationRules()
        If Not WebSite.ClsSessionAdmin.IsValidForm("frmAdContacts", "grdAdContacts.ID") Then Me.mForm.Response.Redirect("~/Forms/frmMain.aspx")
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
        table.Columns.Add("FullName", GetType(String))
        table.Columns.Add("Address", GetType(String))
        table.Columns.Add("MCA", GetType(String))

        table.DefaultView.Sort = Me.GetSortProperty

        For Each contact As AdContactData In Me.mDataList
            table.Rows.Add(New Object() {contact.ID, contact.FullName, contact.PrimaryAddress, "~/Images/Checked_" & contact.MainCompanyAddress.ToString & ".png"})
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

    Public Sub OnView(ByVal contactId As Long)
        Me.mForm.Session("contactID") = contactId
        Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdContactInformation.aspx")
    End Sub

    Public Sub OnProjects(ByVal contactId)
        Me.mForm.Session("contactID") = contactId
        Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdProjects.aspx")
    End Sub

    Public Sub OnNotes(ByVal contactId)
        Me.mForm.Session("contactID") = contactId
        Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdNotes.aspx")
    End Sub

    Public Sub OnToDo(ByVal contactId)
        Me.mForm.Session("contactID") = contactId
        Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdToDos.aspx")
    End Sub

    Public Sub OnEditing(ByVal contactId As Long)
        Me.mForm.Session("contactID") = contactId
        Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdChangeContactInformation.aspx")
    End Sub

    Public Sub OnDeleting(ByVal contactId As Long)
        If IsNumeric(contactId) AndAlso (Not contactId = 0) Then
            Me.mData = New AdContactData
            Me.mData.ID = contactId
            Me.mForm.FindControls("MsgBox").ShowConfirmation("Are you sure you want to delete this Contact?\n\nNote: there is no undo.", "", True, False)
        End If
    End Sub

    Public Sub OnMsgBoxOk()
        If Me.DeleteData Then
            Me.mDataList = Me.GetDataList(WebSite.ClsSessionAdmin.GetSessionUserID)
            Me.PopulateDataList("FullName", "ASC", 0)
        End If
    End Sub

    Public Sub OnAddNew()
        Me.mForm.Session("contactID") = 0
        Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdChangeContactInformation.aspx")
    End Sub

#End Region

#Region " Data Methods "

    Private Sub PopulateDataList(ByVal SortExpression As String, ByVal SortDirection As String, ByVal PageIndex As Integer)
        Me.mPageIndex = PageIndex
        Me.mSortDirection = SortDirection
        Me.mSortExpression = SortExpression
        Me.LoadGridView(Me.mForm.FindControls("grdAdContacts"))
    End Sub

    Private Sub PopulateDataList(ByVal PageIndex As Integer)
        Me.mPageIndex = PageIndex
        Me.LoadGridView(Me.mForm.FindControls("grdAdContacts"))
    End Sub

    Private Function CollectDataID() As AdContactNewData
        Dim formData As New AdContactNewData
        formData.ID.SetValues("grdAdContacts.ID", True, 0, Me.mData.ID)
        Return formData
    End Function

    Private Function GetDataList(ByVal idAdAccount As Long) As AdContactData()
        Try
            Return WebSite.ClsSessionAdmin.GetAdContactList(idAdAccount)
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return New AdContactData() {}
        End Try
    End Function

    Private Function DeleteData() As Boolean
        Try
            WebSite.ClsSessionAdmin.DeleteAdContact(Me.CollectDataID)
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error deleting: " & SysEx.Message)
            Return False
        End Try
    End Function

#End Region

End Class
