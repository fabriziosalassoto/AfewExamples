Imports SCT.Library
Imports SCT.Library.AdminSite
Imports System.Web.UI.WebControls

Public Class DFClsUsers

#Region " Private Fields "

    Private mForm As Object
    Private mData As UserData
    Private mDataList As UserData()

    Private mPageIndex As Integer
    Private mSortDirection As String
    Private mSortExpression As String

    Private mIDsProfiles() As Long

#End Region

#Region " Authorization Rules "

    Public Shared Function CanSelect() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmUsers") Then
            Return ClsSessionAdmin.CanSelectInForm("frmUsers")
        Else
            Return ClsSessionAdmin.CanSelectInForm("AllForms")
        End If
    End Function

    Public Shared Function CanInsert() As Boolean
        Return ClsSessionAdmin.CanInsertInForm("frmUsers")
    End Function

    Public Shared Function CanUpdate() As Boolean
        Return ClsSessionAdmin.CanUpdateInForm("frmUsers")
    End Function

    Public Shared Function CanDelete() As Boolean
        Return ClsSessionAdmin.CanDeleteInForm("frmUsers")
    End Function

    Public Shared Function CanSelectField(ByVal fieldName As String) As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmUsers") Then
            Return ClsSessionAdmin.CanSelectFieldInForm("frmUsers", fieldName)
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
        Me.mForm.Session("ValuePath") = "frmSecurity/frmUsers"

        Me.ApplyPageAuthorizationRules()

        Me.LoadProfileFilter(Me.mForm.Session("ID1"))
        Me.CollectFiltersValuesSelected()
        Me.mDataList = Me.GetDataList(Me.mIDsProfiles)
        Me.PopulateDataList("FullName", "ASC", 0)
    End Sub

    Public Sub ApplyPageAuthorizationRules()
        Dim canSelectMinCol As Boolean
        Dim canSelectProfileCol As Boolean

        If (Not ClsSessionAdmin.IsValidForm("frmUsers", "colUserID", "colUserName", "colUserProfile")) OrElse (Not DFClsUsers.CanSelect()) Then Me.mForm.Response.Redirect("~/Forms/frmMain.aspx")

        Me.mForm.FindControls("GridView").Columns(0).Visible = DFClsUsers.CanSelectField("colUserID")
        Me.mForm.FindControls("GridView").Columns(1).Visible = DFClsUsers.CanSelectField("colUserName")

        canSelectMinCol = Me.mForm.FindControls("GridView").Columns(0).Visible OrElse Me.mForm.FindControls("GridView").Columns(1).Visible

        canSelectProfileCol = canSelectMinCol AndAlso DFClsUsers.CanSelectField("colUserProfile")
        Me.mForm.FindControls("GridView").Columns(2).Visible = canSelectProfileCol
        Me.mForm.FindControls("lblProfiles").Visible = canSelectProfileCol
        Me.mForm.FindControls("ddlProfiles").Visible = canSelectProfileCol

        Me.mForm.FindControls("GridView").Columns(3).Visible = canSelectMinCol AndAlso DFClsUser.CanUpdate
        Me.mForm.FindControls("GridView").Columns(4).Visible = canSelectMinCol AndAlso DFClsUsers.CanDelete
        Me.mForm.FindControls("GridView").PagerSettings.Visible = canSelectMinCol

        Me.mForm.FindControls("mnuAddNew").FindItem("AddNew").Enabled = DFClsUsers.CanInsert()
    End Sub

    Public Shared Sub ApplyGridRowAuthorizationRules(ByVal gridView As GridView, ByVal row As System.Web.UI.WebControls.GridViewRow)
        If row.RowType = DataControlRowType.DataRow Then
            Dim canSelectUser As Boolean = DFClsUser.CanSelect

            row.Cells(0).Controls(1).Visible = canSelectUser
            row.Cells(0).Controls(2).Visible = Not canSelectUser

            Dim canSelectProfile As Boolean = DFClsProfiles.CanSelect

            row.Cells(2).Controls(1).Visible = canSelectProfile
            row.Cells(2).Controls(2).Visible = Not canSelectProfile
        End If
    End Sub

    Public Sub LoadProfileFilter(ByVal profileID As String)
        If String.IsNullOrEmpty(profileID) OrElse Not IsNumeric(profileID) Then
            profileID = 0
        End If
        Me.LoadCombo(Me.mForm.FindControls("ddlProfiles"), Me.GetComboDataSources(Me.GetProfileInfoList, String.Empty), CLng(profileID))
    End Sub

    Private Sub LoadCombo(ByVal combo As DropDownList, ByVal data As DataTable, ByVal selectedValue As String)
        combo.DataSource = data
        combo.DataTextField = data.Columns(0).Caption
        combo.DataValueField = data.Columns(1).Caption
        combo.DataBind()
        If combo.Items.FindByValue(selectedValue) IsNot Nothing Then
            combo.SelectedValue = selectedValue
        End If
    End Sub

    Private Function GetComboDataSources(ByVal InfoList() As ProfileData, ByVal firstItemText As String) As DataTable
        Dim table As New DataTable

        table.Columns.Add("Description", GetType(String))
        table.Columns.Add("ID", GetType(String))

        table.DefaultView.Sort = "Description"

        table.Rows.Add(New String() {firstItemText, "0"})
        For Each Info As ProfileData In InfoList
            table.Rows.Add(New String() {Info.Description, Info.ID.ToString})
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
        table.Columns.Add("FullName", GetType(String))
        table.Columns.Add("ProfileID", GetType(Long))
        table.Columns.Add("Profile", GetType(String))

        table.DefaultView.Sort = Me.GetSortProperty

        For Each user As UserData In Me.mDataList
            table.Rows.Add(New Object() {user.ID, user.FullName, user.Profile.ID, user.Profile.Description})
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
            Case "FullName"
                Return sortProperty & ", Profile " & Me.mSortDirection
            Case "Profile"
                Return sortProperty & ", FullName " & Me.mSortDirection
            Case Else
                Return sortProperty
        End Select
    End Function

    Private Sub CollectFiltersValuesSelected()
        Me.mIDsProfiles = CollectProfileIDSelected(Me.mForm.FindControls("ddlProfiles"))
    End Sub

    Private Function CollectProfileIDSelected(ByVal profileIDsList As DropDownList) As Long()
        If profileIDsList.SelectedValue <> 0 Then
            Return New Long() {profileIDsList.SelectedValue}
        Else
            Return New Long() {}
        End If
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

    Public Sub OnProfile(ByVal profileID As String)
        Me.mForm.Session("ID1") = profileID
        Me.mForm.Response.Redirect("~/Forms/frmProfiles.aspx")
    End Sub

    Public Sub OnEditing(ByVal userID As String)
        Me.mForm.Session("ID1") = userID
        Me.mForm.Response.Redirect("~/Forms/frmUser.aspx")
    End Sub

    Public Sub OnDeleting(ByVal userID As String)
        If IsNumeric(userID) AndAlso (Not userID = 0) Then
            Me.mData = New UserData
            Me.mData.ID = userID
            Me.mForm.FindControls("MsgBox").ShowConfirmation("Are you sure you want to delete this User?\n\nNote: there is no undo.", "", True, False)
        End If
    End Sub

    Public Sub OnMsgBoxOk()
        If Me.DeleteData Then
            Me.CollectFiltersValuesSelected()
            Me.mDataList = Me.GetDataList(Me.mIDsProfiles)
            Me.PopulateDataList("FullName", "ASC", 0)
        End If
    End Sub

    Public Sub OnSelectedProfileChanged()
        Me.CollectFiltersValuesSelected()
        Me.mDataList = Me.GetDataList(Me.mIDsProfiles)
        Me.PopulateDataList(0)
    End Sub

    Public Sub OnAddNew()
        Me.mForm.Session("ID1") = 0
        Me.mForm.Response.Redirect("~/Forms/frmUser.aspx")
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

    'Private Function GetDataID(ByVal textBox As TextBox) As String
    '    If textBox.Text = String.Empty Then
    '        Return 0
    '    End If
    '    Return textBox.Text
    'End Function

    Private Function CollectDataID() As UserNewData
        Dim formData As New UserNewData
        formData.ID.SetValues("colUserID", True, 0, Me.mData.ID)
        Return formData
    End Function

    Private Function GetProfileInfoList() As ProfileData()
        Try
            Return ClsSessionAdmin.GetProfileInfoList()
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return New ProfileData() {}
        End Try
    End Function

    Private Function GetDataList(ByVal idProfile() As Long) As UserData()
        Try
            Return ClsSessionAdmin.GetUserList(idProfile)
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return New UserData() {}
        End Try
    End Function

    Private Function DeleteData() As Boolean
        Try
            ClsSessionAdmin.DeleteUser(Me.CollectDataID)
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error deleting: " & SysEx.Message)
            Return False
        End Try
    End Function

#End Region

End Class
