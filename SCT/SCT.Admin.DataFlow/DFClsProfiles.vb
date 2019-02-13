Imports SCT.Library
Imports SCT.Library.AdminSite
Imports System.Web.UI.WebControls

Public Class DFClsProfiles

#Region " Private Fields "

    Private mForm As Object
    Private mData As ProfileData
    Private mDataList As ProfileData()

    Private mPageIndex As Integer
    Private mSortDirection As String
    Private mSortExpression As String

    Private mEditMode As Boolean

#End Region

#Region " Authorization Rules "

    Public Shared Function CanSelect() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmProfiles") Then
            Return ClsSessionAdmin.CanSelectInForm("frmProfiles")
        Else
            Return ClsSessionAdmin.CanSelectInForm("AllForms")
        End If
    End Function

    Public Shared Function CanInsert() As Boolean
        Return ClsSessionAdmin.CanInsertInForm("frmProfiles")
    End Function

    Public Shared Function CanUpdate() As Boolean
        Return ClsSessionAdmin.CanUpdateInForm("frmProfiles")
    End Function

    Public Shared Function CanDelete() As Boolean
        Return ClsSessionAdmin.CanDeleteInForm("frmProfiles")
    End Function

    Public Shared Function CanSelectField(ByVal fieldName As String) As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmProfiles") Then
            Return ClsSessionAdmin.CanSelectFieldInForm("frmProfiles", fieldName)
        Else
            Return ClsSessionAdmin.CanSelectFieldInForm("AllForms", "AllFields")
        End If

    End Function

    Public Shared Function CanInsertField(ByVal fieldName As String) As Boolean
        Return ClsSessionAdmin.CanInsertFieldInForm("frmProfiles", fieldName)
    End Function

    Public Shared Function CanUpdateField(ByVal fieldName As String) As Boolean
        Return ClsSessionAdmin.CanUpdateFieldInForm("frmProfiles", fieldName)
    End Function

    Public Shared Function CanDeleteField(ByVal fieldName As String) As Boolean
        Return ClsSessionAdmin.CanDeleteFieldInForm("frmProfiles", fieldName)
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
        Me.mForm.Session("ValuePath") = "frmSecurity/frmProfiles"

        Me.ApplyPageAuthorizationRules()

        Me.mDataList = Me.GetDataList
        Me.PopulateDataList("Description", "ASC", 0)

        If IsNumeric(Me.mForm.Session("ID1")) AndAlso (Not Me.mForm.Session("ID1") = 0) Then
            Me.mEditMode = True
            Me.mData = Me.GetData(Me.mForm.Session("ID1"))
            If Me.mData IsNot Nothing Then
                Me.ApplyAddFormAuthorizationRules(True)
                Me.PopulateData()
            End If
        End If
    End Sub

    Public Sub ApplyPageAuthorizationRules()
        Dim canSelectMinCol As Boolean
        Dim canSelectField As Boolean

        If (Not ClsSessionAdmin.IsValidForm("frmProfiles", "colProfileID", "colProfileDescription", "txtProfileID", "txtProfileDescription")) OrElse (Not DFClsProfiles.CanSelect()) Then Me.mForm.Response.Redirect("~/Forms/frmMain.aspx")

        Me.mForm.FindControls("GridView").Columns(0).Visible = DFClsProfiles.CanSelectField("colProfileID")
        Me.mForm.FindControls("GridView").Columns(1).Visible = DFClsProfiles.CanSelectField("colProfileDescription")

        canSelectMinCol = Me.mForm.FindControls("GridView").Columns(0).Visible OrElse Me.mForm.FindControls("GridView").Columns(1).Visible
        Me.mForm.FindControls("GridView").Columns(2).Visible = canSelectMinCol AndAlso DFClsUsers.CanSelect
        Me.mForm.FindControls("GridView").Columns(3).Visible = canSelectMinCol AndAlso DFClsProfileForms.CanSelect
        Me.mForm.FindControls("GridView").Columns(4).Visible = canSelectMinCol AndAlso DFClsProfiles.CanUpdate
        Me.mForm.FindControls("GridView").Columns(5).Visible = canSelectMinCol AndAlso DFClsProfiles.CanDelete
        Me.mForm.FindControls("GridView").PagerSettings.Visible = canSelectMinCol

        Me.mForm.FindControls("mnuAddNew").FindItem("AddNew").Enabled = DFClsProfiles.CanInsert()

        canSelectField = DFClsProfiles.CanSelectField("txtProfileID")
        Me.mForm.FindControls("txtProfileID").Visible = canSelectField
        Me.mForm.FindControls("lblProfileID").Visible = canSelectField

        canSelectField = DFClsProfiles.CanSelectField("txtProfileDescription")
        Me.mForm.FindControls("txtProfileDescription").Visible = canSelectField
        Me.mForm.FindControls("lblProfileDescription").Visible = canSelectField
    End Sub

    Public Shared Sub ApplyGridRowAuthorizationRules(ByVal gridView As GridView, ByVal row As System.Web.UI.WebControls.GridViewRow)
        If row.RowType = DataControlRowType.DataRow Then
            Dim canSelect As Boolean = DFClsProfiles.CanSelect

            row.Cells(0).Controls(1).Visible = canSelect
            row.Cells(0).Controls(2).Visible = Not canSelect
        End If
    End Sub

    Public Sub ApplyAddFormAuthorizationRules(ByVal editMode As Boolean)
        Dim canSave As Boolean

        Me.mForm.FindControls("txtProfileDescription").Enabled = (Not editMode AndAlso DFClsProfiles.CanInsertField("txtProfileDescription")) OrElse (editMode AndAlso DFClsProfiles.CanUpdateField("txtProfileDescription"))

        Me.ApplyMenuItemAuthorizationRules(editMode)

        canSave = (Not editMode AndAlso DFClsProfiles.CanInsert) OrElse (editMode AndAlso DFClsProfiles.CanUpdate)
        Me.mForm.FindControls("mnuSave").FindItem("Ok").Enabled = canSave
        Me.mForm.FindControls("mnuSave").FindItem("Save").Enabled = canSave

        Me.ShowAddForm(DFClsProfiles.CanSelect OrElse DFClsProfiles.CanInsert OrElse DFClsProfiles.CanUpdate)
    End Sub

    Private Sub ApplyMenuItemAuthorizationRules(ByVal editMode As Boolean)
        Me.mForm.FindControls("mnuItem").FindItem("Users").Enabled = editMode AndAlso DFClsUsers.CanSelect
        Me.mForm.FindControls("mnuItem").FindItem("Forms").Enabled = editMode AndAlso DFClsProfileForms.CanSelect
        Me.mForm.FindControls("mnuItem").FindItem("Delete").Enabled = editMode AndAlso DFClsProfiles.CanDelete
    End Sub

    Public Function ValidateExistsDescription(ByVal ControlName As String) As Boolean
        Return Not ClsProfile.Exists(Me.mData.ID, Me.mForm.FindControls(ControlName).Text)
    End Function

    Private Sub LoadGridView(ByVal gridview As GridView)
        Me.AddRowData(gridview)
        Me.AddSortImage(gridview)
    End Sub

    Private Sub AddRowData(ByVal GridView As System.Web.UI.WebControls.GridView)
        GridView.PageIndex = Me.mPageIndex
        GridView.DataSource = Me.GetDataSources
        GridView.DataBind()
    End Sub

    Private Function GetDataSources() As DataTable
        Dim table As New DataTable

        table.Columns.Add("ID", GetType(Long))
        table.Columns.Add("Description", GetType(String))

        table.DefaultView.Sort = Me.mSortExpression & " " & Me.mSortDirection

        For Each profile As ProfileData In Me.mDataList
            table.Rows.Add(New Object() {profile.ID, profile.Description})
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

    Private Sub ClearAddForm()
        Me.mForm.FindControls("txtProfileID").Text = String.Empty
        Me.mForm.FindControls("txtProfileDescription").Text = String.Empty
        Me.mForm.FindControls("txtProfileDescription").Focus()
    End Sub

    Private Sub ShowAddForm(ByVal value As Boolean)
        Me.mForm.FindControls("pnlAddForm").Visible = value
    End Sub

    Public Sub OnSorting(ByVal SortExpression As String, ByVal PageIndex As Integer)
        Me.ShowAddForm(False)
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
        Me.ShowAddForm(False)
        Me.PopulateDataList(NewPageIndex)
    End Sub

    Public Sub OnUsers(ByVal profileID As String)
        Me.mForm.Session("ID1") = profileID
        Me.mForm.Response.Redirect("~/Forms/frmUsers.aspx")
    End Sub

    Public Sub OnUsers()
        Me.mForm.Session("ID1") = Me.mData.ID.ToString
        Me.mForm.Response.Redirect("~/Forms/frmUsers.aspx")
    End Sub

    Public Sub OnForms(ByVal profileID As String)
        Me.mForm.Session("ID2") = 0
        Me.mForm.Session("ID1") = profileID
        Me.mForm.Response.Redirect("~/Forms/frmProfileForms.aspx")
    End Sub

    Public Sub OnForms()
        Me.mForm.Session("ID2") = 0
        Me.mForm.Session("ID1") = Me.mData.ID.ToString
        Me.mForm.Response.Redirect("~/Forms/frmProfileForms.aspx")
    End Sub

    Public Sub OnEditing(ByVal profileID As String)
        If IsNumeric(profileID) AndAlso (Not profileID = 0) Then
            Me.mEditMode = True
            Me.mData = Me.GetData(profileID)
            If Me.mData IsNot Nothing Then
                Me.ApplyAddFormAuthorizationRules(True)
                Me.PopulateData()
            End If
        End If
    End Sub

    Public Sub OnDeleting(ByVal profileID As String)
        Me.ShowAddForm(False)
        If IsNumeric(profileID) AndAlso (Not profileID = 0) Then
            Me.mData = New ProfileData
            Me.mForm.FindControls("txtProfileID").Text = profileID
            Me.mForm.FindControls("MsgBox").ShowConfirmation("Are you sure you want to delete this Profile?\n\nNote: there is no undo.", "", True, False)
        End If
    End Sub

    Public Sub OnDeleting()
        Me.mForm.FindControls("MsgBox").ShowConfirmation("Are you sure you want to delete this Profile?\n\nNote: there is no undo.", "", True, False)
    End Sub

    Public Sub OnMsgBoxOk()
        If Me.DeleteData Then
            Me.ShowAddForm(False)
            Me.mDataList = Me.GetDataList
            Me.PopulateDataList("Description", "ASC", 0)
        End If
    End Sub

    Public Sub OnAddNew()
        Me.mEditMode = False
        Me.mData = New ProfileData
        Me.ApplyAddFormAuthorizationRules(False)
        Me.ClearAddForm()
    End Sub

    Public Sub OnSave()
        If Me.SaveData Then
            Me.mDataList = Me.GetDataList
            Me.PopulateDataList("Description", "ASC", 0)
            Me.mEditMode = True
            Me.ApplyMenuItemAuthorizationRules(True)
            Me.PopulateData()
        End If
    End Sub

    Public Sub OnOk()
        If Me.SaveData Then
            Me.ShowAddForm(False)
            Me.mDataList = Me.GetDataList
            Me.PopulateDataList("Description", "ASC", 0)
        End If
    End Sub

    Public Sub OnCancel()
        Me.ShowAddForm(False)
    End Sub

#End Region

#Region " Data Methods "

    Private Sub PopulateDataList(ByVal SortExpression As String, ByVal SortDirection As String, ByVal PageIndex As Integer)
        Me.mPageIndex = PageIndex
        Me.mSortDirection = SortDirection
        Me.mSortExpression = SortExpression
        Me.LoadGridView(Me.mForm.FindControls("GridView"))
    End Sub

    Private Sub PopulateDataList(ByVal PageIndex As Integer)
        Me.mPageIndex = PageIndex
        Me.LoadGridView(Me.mForm.FindControls("GridView"))
    End Sub

    Private Sub PopulateData()
        Me.mForm.FindControls("txtProfileID").Text = Me.mData.ID
        Me.mForm.FindControls("txtProfileDescription").Text = Me.mData.Description
        Me.mForm.FindControls("txtProfileDescription").Focus()
    End Sub

    Private Function GetDataID(ByVal textBox As TextBox) As String
        If textBox.Text = String.Empty Then
            Return 0
        End If
        Return textBox.Text
    End Function

    Private Function CollectData() As ProfileNewData
        Dim formData As New ProfileNewData
        formData.ID.SetValues("txtProfileID", True, Me.mData.ID, CLng(Me.GetDataID(Me.mForm.FindControls("txtProfileID"))))
        formData.Description.SetValues("txtProfileDescription", False, Me.mData.Description, Me.mForm.FindControls("txtProfileDescription").Text)
        Return formData
    End Function

    Private Function CollectDataID() As ProfileNewData
        Dim formData As New ProfileNewData
        formData.ID.SetValues("txtProfileID", True, Me.mData.ID, CLng(Me.GetDataID(Me.mForm.FindControls("txtProfileID"))))
        Return formData
    End Function

    Private Function CollectDataID(ByVal profileID As String) As ProfileNewData
        Dim formData As New ProfileNewData
        formData.ID.SetValues("txtProfileID", True, 0, CLng(profileID))
        Return formData
    End Function

    Private Function GetDataList() As ProfileData()
        Try
            Return ClsSessionAdmin.GetProfileList()
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return New ProfileData() {}
        End Try
    End Function

    Private Function GetData(ByVal profileID As String) As ProfileData
        Try
            Return ClsSessionAdmin.GetProfile(Me.CollectDataID(profileID))
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return Nothing
        End Try
    End Function

    Private Function SaveData() As Boolean
        Try
            If Me.mEditMode Then
                Me.mData = ClsSessionAdmin.EditProfile(Me.CollectData)
            Else
                Me.mData = ClsSessionAdmin.AddProfile(Me.CollectData)
            End If
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error saving: " & SysEx.Message)
            Return False
        End Try
    End Function

    Private Function DeleteData() As Boolean
        Try
            ClsSessionAdmin.DeleteProfile(Me.CollectDataID)
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error deleting: " & SysEx.Message)
            Return False
        End Try
    End Function

#End Region

End Class
