Imports SCT.Library
Imports SCT.Library.AdminSite
Imports System.Web.UI.WebControls

Public Class DFClsGroupProfiles

#Region " Private Fields "

    Private mForm As Object
    Private mData As GroupProfileData
    Private mDataList As GroupProfileData()

    Private mPageIndex As Integer
    Private mSortDirection As String
    Private mSortExpression As String

    Private mFormIDList() As Long = New Long() {}
    Private mGroupIDList() As Long = New Long() {}

    Private mEditMode As Boolean

#End Region

#Region " Authorization Rules "

    Public Shared Function CanSelect() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmGroupProfiles") Then
            Return ClsSessionAdmin.CanSelectInForm("frmGroupProfiles")
        Else
            Return ClsSessionAdmin.CanSelectInForm("AllForms")
        End If
    End Function

    Public Shared Function CanInsert() As Boolean
        Return ClsSessionAdmin.CanInsertInForm("frmGroupProfiles")
    End Function

    Public Shared Function CanUpdate() As Boolean
        Return ClsSessionAdmin.CanUpdateInForm("frmGroupProfiles")
    End Function

    Public Shared Function CanDelete() As Boolean
        Return ClsSessionAdmin.CanDeleteInForm("frmGroupProfiles")
    End Function

    Public Shared Function CanSelectField(ByVal fieldName As String) As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmGroupProfiles") Then
            Return ClsSessionAdmin.CanSelectFieldInForm("frmGroupProfiles", fieldName)
        Else
            Return ClsSessionAdmin.CanSelectFieldInForm("AllForms", "AllFields")
        End If
    End Function

    Public Shared Function CanInsertField(ByVal fieldName As String) As Boolean
        Return ClsSessionAdmin.CanInsertFieldInForm("frmGroupProfiles", fieldName)
    End Function

    Public Shared Function CanUpdateField(ByVal fieldName As String) As Boolean
        Return ClsSessionAdmin.CanUpdateFieldInForm("frmGroupProfiles", fieldName)
    End Function

    Public Shared Function CanDeleteField(ByVal fieldName As String) As Boolean
        Return ClsSessionAdmin.CanDeleteFieldInForm("frmGroupProfiles", fieldName)
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
        Me.mForm.Session("ValuePath") = "frmSecurity/frmGroups"

        Me.ApplyPageAuthorizationRules()

        Me.LoadFilters(Me.mForm.Session("ID2"), Me.mForm.Session("ID1"))
        Me.mDataList = Me.GetDataList()
        Me.PopulateDataList("Form", "ASC", 0)
    End Sub

    Public Sub ApplyPageAuthorizationRules()
        Dim canSelectField As Boolean
        Dim canSelectCol As Boolean

        If (Not ClsSessionAdmin.IsValidForm("frmGroupProfiles", "colGroup", "colForm", "colProfile", "colGroupProfileSelect", "colGroupProfileInsert", "colGroupProfileUpdate", "colGroupProfileDelete", "ddlProfile", "ddlForm", "ddlGroup", "ckbSelect", "ckbInsert", "ckbUpdate", "ckbDelete")) OrElse (Not DFClsGroupProfiles.CanSelect()) Then Me.mForm.Response.Redirect("~/Forms/frmMain.aspx")

        canSelectCol = DFClsGroupProfiles.CanSelectField("colGroup") AndAlso DFClsGroupProfiles.CanSelectField("colForm") AndAlso DFClsGroupProfiles.CanSelectField("colProfile")

        Me.mForm.FindControls("GridView").Columns(0).Visible = canSelectCol
        Me.mForm.FindControls("lblForms").Visible = canSelectCol
        Me.mForm.FindControls("ddlForms").Visible = canSelectCol

        Me.mForm.FindControls("GridView").Columns(1).Visible = canSelectCol
        Me.mForm.FindControls("lblGroups").Visible = canSelectCol
        Me.mForm.FindControls("ddlGroups").Visible = canSelectCol

        Me.mForm.FindControls("GridView").Columns(2).Visible = canSelectCol

        Me.mForm.FindControls("GridView").Columns(3).Visible = canSelectCol AndAlso DFClsGroupProfiles.CanSelectField("colGroupProfileSelect")
        Me.mForm.FindControls("GridView").Columns(4).Visible = canSelectCol AndAlso DFClsGroupProfiles.CanSelectField("colGroupProfileInsert")
        Me.mForm.FindControls("GridView").Columns(5).Visible = canSelectCol AndAlso DFClsGroupProfiles.CanSelectField("colGroupProfileUpdate")
        Me.mForm.FindControls("GridView").Columns(6).Visible = canSelectCol AndAlso DFClsGroupProfiles.CanSelectField("colGroupProfileDelete")

        Me.mForm.FindControls("GridView").Columns(7).Visible = canSelectCol AndAlso DFClsGroupProfiles.CanUpdate
        Me.mForm.FindControls("GridView").Columns(8).Visible = canSelectCol AndAlso DFClsGroupProfiles.CanDelete
        Me.mForm.FindControls("GridView").PagerSettings.Visible = canSelectCol

        Me.mForm.FindControls("mnuAddNew").FindItem("AddNew").Enabled = DFClsGroupProfiles.CanInsert()

        canSelectField = DFClsGroupProfiles.CanSelectField("ddlProfile")
        Me.mForm.FindControls("ddlProfile").Visible = canSelectField
        Me.mForm.FindControls("lblProfile").Visible = canSelectField

        canSelectField = DFClsGroupProfiles.CanSelectField("ddlForm")
        Me.mForm.FindControls("ddlForm").Visible = canSelectField
        Me.mForm.FindControls("lblForm").Visible = canSelectField

        canSelectField = DFClsGroupProfiles.CanSelectField("ddlGroup")
        Me.mForm.FindControls("ddlGroup").Visible = canSelectField
        Me.mForm.FindControls("lblGroup").Visible = canSelectField

        canSelectField = DFClsGroupProfiles.CanSelectField("ckbSelect")
        Me.mForm.FindControls("ckbSelect").Visible = canSelectField
        Me.mForm.FindControls("lblSelect").Visible = canSelectField

        canSelectField = DFClsGroupProfiles.CanSelectField("ckbInsert")
        Me.mForm.FindControls("ckbInsert").Visible = canSelectField
        Me.mForm.FindControls("lblInsert").Visible = canSelectField

        canSelectField = DFClsGroupProfiles.CanSelectField("ckbUpdate")
        Me.mForm.FindControls("ckbUpdate").Visible = canSelectField
        Me.mForm.FindControls("lblUpdate").Visible = canSelectField

        canSelectField = DFClsGroupProfiles.CanSelectField("ckbDelete")
        Me.mForm.FindControls("ckbDelete").Visible = canSelectField
        Me.mForm.FindControls("lblDelete").Visible = canSelectField
    End Sub

    Public Shared Sub ApplyGridRowAuthorizationRules(ByVal gridView As GridView, ByVal row As System.Web.UI.WebControls.GridViewRow)
        If row.RowType = DataControlRowType.DataRow Then
            Dim canSelectForm As Boolean = DFClsForms.CanSelect

            row.Cells(0).Controls(1).Visible = canSelectForm
            row.Cells(0).Controls(2).Visible = Not canSelectForm

            Dim canSelectGroup As Boolean = DFClsGroups.CanSelect

            row.Cells(1).Controls(1).Visible = canSelectGroup
            row.Cells(1).Controls(2).Visible = Not canSelectGroup

            Dim canSelectProfile As Boolean = DFClsProfiles.CanSelect

            row.Cells(2).Controls(1).Visible = canSelectProfile
            row.Cells(2).Controls(2).Visible = Not canSelectProfile
        End If
    End Sub

    Public Sub ApplyAddFormAuthorizationRules(ByVal editMode As Boolean)
        Dim canSave As Boolean

        Me.mForm.FindControls("ddlProfile").Enabled = (Not editMode AndAlso DFClsGroupProfiles.CanInsertField("ddlProfile"))
        Me.mForm.FindControls("ddlForm").Enabled = (Not editMode AndAlso DFClsGroupProfiles.CanInsertField("ddlForm"))
        Me.mForm.FindControls("ddlGroup").Enabled = (Not editMode AndAlso DFClsGroupProfiles.CanInsertField("ddlGroup"))
        Me.mForm.FindControls("ckbSelect").Enabled = (Not editMode AndAlso DFClsGroupProfiles.CanInsertField("ckbSelect")) OrElse (editMode AndAlso DFClsGroupProfiles.CanUpdateField("ckbSelect"))
        Me.mForm.FindControls("ckbInsert").Enabled = (Not editMode AndAlso DFClsGroupProfiles.CanInsertField("ckbInsert")) OrElse (editMode AndAlso DFClsGroupProfiles.CanUpdateField("ckbInsert"))
        Me.mForm.FindControls("ckbUpdate").Enabled = (Not editMode AndAlso DFClsGroupProfiles.CanInsertField("ckbUpdate")) OrElse (editMode AndAlso DFClsGroupProfiles.CanUpdateField("ckbUpdate"))
        Me.mForm.FindControls("ckbDelete").Enabled = (Not editMode AndAlso DFClsGroupProfiles.CanInsertField("ckbDelete")) OrElse (editMode AndAlso DFClsGroupProfiles.CanUpdateField("ckbDelete"))

        Me.ApplyMenuItemAuthorizationRules(editMode)

        canSave = (Not editMode AndAlso DFClsGroupProfiles.CanInsert) OrElse (editMode AndAlso DFClsGroupProfiles.CanUpdate)
        Me.mForm.FindControls("mnuSave").FindItem("Ok").Enabled = canSave
        Me.mForm.FindControls("mnuSave").FindItem("Save").Enabled = canSave

        Me.ShowAddForm(DFClsGroupProfiles.CanSelect OrElse DFClsGroupProfiles.CanInsert OrElse DFClsGroupProfiles.CanUpdate)
    End Sub

    Private Sub ApplyMenuItemAuthorizationRules(ByVal editMode As Boolean)
        Me.mForm.FindControls("mnuItem").FindItem("Group").Enabled = editMode AndAlso DFClsGroups.CanSelect
        Me.mForm.FindControls("mnuItem").FindItem("Form").Enabled = editMode AndAlso DFClsForms.CanSelect
        Me.mForm.FindControls("mnuItem").FindItem("Profile").Enabled = editMode AndAlso DFClsProfiles.CanSelect
        Me.mForm.FindControls("mnuItem").FindItem("Delete").Enabled = editMode AndAlso DFClsGroupProfiles.CanDelete
    End Sub

    Public Function ValidateFormAssign(ByVal ControlName As String) As Boolean
        Return ClsFormProfile.Exists(Me.mForm.FindControls("ddlProfile").SelectedValue, Me.mForm.FindControls("ddlForm").SelectedValue)
    End Function

    Public Function ValidateAssign(ByVal ControlName As String) As Boolean
        Return Not (ClsGroupProfile.Exists(Me.mForm.FindControls("ddlProfile").SelectedValue, Me.mForm.FindControls("ddlForm").SelectedValue, Me.mForm.FindControls("ddlGroup").SelectedValue) AndAlso Not Me.mEditMode)
    End Function

    Private Sub LoadFilters(ByVal groupID As String, ByVal formID As String)
        Me.LoadFormFilter(formID)
        Me.LoadGroupFilter(Me.mForm.FindControls("ddlForms").SelectedValue, groupID)
    End Sub

    Private Sub LoadFormFilter(ByVal formID As String)
        Me.LoadCombo(Me.mForm.FindControls("ddlForms"), Me.GetComboDataSources(Me.GetFormInfoList, String.Empty), formID)
    End Sub

    Private Sub LoadGroupFilter(ByVal formID As Long, ByVal groupID As String)
        Me.LoadCombo(Me.mForm.FindControls("ddlGroups"), Me.GetComboDataSources(Me.GetGroupInfoList(formID), String.Empty), groupID)
    End Sub

    Private Sub LoadFormCombo(ByVal formID As String)
        Me.LoadCombo(Me.mForm.FindControls("ddlForm"), Me.GetComboDataSources(Me.GetFormInfoList, "[Select a Form]"), formID)
    End Sub

    Private Sub LoadFormComboItem(ByVal formID As Long, ByVal description As String)
        Dim formData As New FormData
        formData.ID = formID
        formData.Description = description
        Me.LoadCombo(Me.mForm.FindControls("ddlForm"), Me.GetComboDataSources(New FormData() {formData}, "[Select a Form]"), formID)
    End Sub

    Private Sub LoadGroupCombo(ByVal formID As Long, ByVal groupID As String)
        Me.LoadCombo(Me.mForm.FindControls("ddlGroup"), Me.GetComboDataSources(Me.GetGroupInfoList(formID), "[Select a Group]"), groupID)
        Me.mForm.FindControls("ddlGroup").Enabled = Me.mForm.FindControls("ddlForm").SelectedValue <> 0
    End Sub

    Private Sub LoadGroupComboItem(ByVal groupID As Long, ByVal description As String)
        Dim groupData As New GroupData
        groupData.ID = groupID
        groupData.Description = description
        Me.LoadCombo(Me.mForm.FindControls("ddlGroup"), Me.GetComboDataSources(New GroupData() {groupData}, "[Select a Group]"), groupID)
    End Sub

    Private Sub LoadProfileCombo(ByVal profileID As String)
        Me.LoadCombo(Me.mForm.FindControls("ddlProfile"), Me.GetComboDataSources(Me.GetProfileInfoList(), "[Select a Profile]"), profileID)
        Me.mForm.FindControls("ddlProfile").Enabled = Me.mForm.FindControls("ddlGroup").SelectedValue <> 0
    End Sub

    Private Sub LoadProfileComboItem(ByVal profileID As Long, ByVal description As String)
        Dim profileData As New ProfileData
        profileData.ID = profileID
        profileData.Description = description
        Me.LoadCombo(Me.mForm.FindControls("ddlProfile"), Me.GetComboDataSources(New ProfileData() {profileData}, "[Select a Profile]"), profileID)
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

    Private Sub LoadCombo(ByVal combo As DropDownList, ByVal listItem As ListItem)
        combo.Items.Clear()
        combo.Items.Add(listItem)
    End Sub

    Private Function GetComboDataSources(ByVal InfoList As Object, ByVal FirstItemText As String) As DataTable
        Dim table As New DataTable

        table.Columns.Add("Description", GetType(String))
        table.Columns.Add("ID", GetType(String))

        table.DefaultView.Sort = "Description"

        table.Rows.Add(New String() {FirstItemText, 0})
        For Each Info As Object In InfoList
            table.Rows.Add(New Object() {Info.Description, Info.ID.ToString})
        Next
        Return table
    End Function

    Private Sub LoadGridView(ByVal gridview As GridView)
        Me.AddRowData(gridview)
        Me.AddSortImage(gridview)
    End Sub

    Private Sub AddRowData(ByVal GridView As System.Web.UI.WebControls.GridView)
        GridView.PageIndex = Me.mPageIndex
        GridView.DataSource = Me.GetProfileDataSources
        GridView.DataBind()
    End Sub

    Private Function GetProfileDataSources() As DataTable
        Dim table As New DataTable

        table.Columns.Add("IDForm", GetType(Long))
        table.Columns.Add("Form", GetType(String))
        table.Columns.Add("IDGroup", GetType(Long))
        table.Columns.Add("Group", GetType(String))
        table.Columns.Add("IDProfile", GetType(Long))
        table.Columns.Add("Profile", GetType(String))
        table.Columns.Add("Select", GetType(String))
        table.Columns.Add("Insert", GetType(String))
        table.Columns.Add("Update", GetType(String))
        table.Columns.Add("Delete", GetType(String))

        table.DefaultView.Sort = Me.GetSortProperty

        For Each profile As GroupProfileData In Me.mDataList
            table.Rows.Add(New Object() {profile.Group.Form.ID, profile.Group.Form.Description, profile.Group.ID, profile.Group.Description, profile.ID, profile.Description, "~/Images/Checked_" & profile.PSelect.ToString & ".png", "~/Images/Checked_" & profile.PInsert.ToString & ".png", "~/Images/Checked_" & profile.PUpdate.ToString & ".png", "~/Images/Checked_" & profile.PDelete.ToString & ".png"})
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
            Case "Group"
                Return sortProperty & ", Form " & Me.mSortDirection & ", Profile " & Me.mSortDirection
            Case "Form"
                Return sortProperty & ", Group " & Me.mSortDirection & ", Profile " & Me.mSortDirection
            Case "Profile"
                Return sortProperty & ", Form " & Me.mSortDirection & ", Group " & Me.mSortDirection
            Case Else
                Return sortProperty
        End Select
    End Function

    Private Sub CheckedChecks(ByVal checked As Boolean)
        Me.mForm.FindControls("ckbSelect").Checked = checked
        Me.mForm.FindControls("ckbInsert").Checked = checked
        Me.mForm.FindControls("ckbUpdate").Checked = checked
        Me.mForm.FindControls("ckbDelete").Checked = checked
    End Sub

    Private Sub CheckedChecks(ByVal pSelect As Boolean, ByVal pInsert As Boolean, ByVal pUpdate As Boolean, ByVal pDelete As Boolean)
        Me.mForm.FindControls("ckbSelect").Checked = pSelect
        Me.mForm.FindControls("ckbInsert").Checked = pInsert
        Me.mForm.FindControls("ckbUpdate").Checked = pUpdate
        Me.mForm.FindControls("ckbDelete").Checked = pDelete
    End Sub

    Private Sub EnabledChecks(ByVal enabled As Boolean, ByVal CheckedSelect As Boolean)
        Me.mForm.FindControls("ckbSelect").Enabled = (enabled OrElse CheckedSelect)
        Me.mForm.FindControls("ckbInsert").Enabled = enabled AndAlso CheckedSelect
        Me.mForm.FindControls("ckbUpdate").Enabled = enabled AndAlso CheckedSelect
        Me.mForm.FindControls("ckbDelete").Enabled = enabled AndAlso CheckedSelect
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

    Public Sub OnProfile(ByVal profileID As Long)
        Me.mForm.Session("ID1") = profileID
        Me.mForm.Response.Redirect("~/Forms/frmProfiles.aspx")
    End Sub

    Public Sub OnProfile()
        Me.mForm.Session("ID1") = Me.mData.ID
        Me.mForm.Response.Redirect("~/Forms/frmProfiles.aspx")
    End Sub

    Public Sub OnForm(ByVal formID As Long)
        Me.mForm.Session("ID1") = formID
        Me.mForm.Response.Redirect("~/Forms/frmForms.aspx")
    End Sub

    Public Sub OnForm()
        Me.mForm.Session("ID1") = Me.mData.Group.Form.ID
        Me.mForm.Response.Redirect("~/Forms/frmForms.aspx")
    End Sub

    Public Sub OnGroup(ByVal groupID As Long, ByVal formID As Long)
        Me.mForm.Session("ID1") = formID
        Me.mForm.Session("ID2") = groupID
        Me.mForm.Response.Redirect("~/Forms/frmGroups.aspx")
    End Sub

    Public Sub OnGroup()
        Me.mForm.Session("ID1") = Me.mData.Group.Form.ID
        Me.mForm.Session("ID2") = Me.mData.Group.ID
        Me.mForm.Response.Redirect("~/Forms/frmGroups.aspx")
    End Sub

    Public Sub OnEditing(ByVal groupID As Long, ByVal formID As Long, ByVal profileID As Long)
        Me.mEditMode = True
        Me.LoadFormComboItem(formID, String.Empty)
        Me.LoadGroupComboItem(groupID, String.Empty)
        Me.LoadProfileComboItem(profileID, String.Empty)
        Me.ShowAddForm(True)
        Me.mData = Me.GetData()
        If Me.mData IsNot Nothing Then
            Me.ApplyAddFormAuthorizationRules(True)
            Me.PopulateData()
        End If
    End Sub

    Public Sub OnDeleting(ByVal groupID As Long, ByVal formID As Long, ByVal profileID As Long)
        Me.ShowAddForm(False)
        Me.LoadFormComboItem(formID, String.Empty)
        Me.LoadGroupComboItem(groupID, String.Empty)
        Me.LoadProfileComboItem(profileID, String.Empty)
        Me.mForm.FindControls("MsgBox").ShowConfirmation("Are you sure you want to delete this Profile?\n\nNote: there is no undo.", "", True, False)
    End Sub

    Public Sub OnDeleting()
        Me.mForm.FindControls("MsgBox").ShowConfirmation("Are you sure you want to delete this Profile?\n\nNote: there is no undo.", "", True, False)
    End Sub

    Public Sub OnMsgBoxOk()
        If Me.DeleteData Then
            Me.ShowAddForm(False)
            Me.LoadFilters(Me.mForm.FindControls("ddlGroups").SelectedValue, Me.mForm.FindControls("ddlForms").SelectedValue)
            Me.mDataList = Me.GetDataList()
            Me.PopulateDataList("Form", "ASC", 0)
        End If
    End Sub

    Public Sub OnSelectedFormChanged()
        Me.ShowAddForm(False)
        Me.LoadGroupFilter(Me.mForm.FindControls("ddlForms").SelectedValue, 0)
        Me.mDataList = Me.GetDataList()
        Me.PopulateDataList(0)
    End Sub

    Public Sub OnSelectedGroupChanged()
        Me.ShowAddForm(False)
        Me.mDataList = Me.GetDataList()
        Me.PopulateDataList(0)
    End Sub

    Public Sub OnAddNew()
        Me.mEditMode = False
        Me.mData = New GroupProfileData
        Me.ApplyAddFormAuthorizationRules(False)
        Me.ClearAddForm()
    End Sub

    Public Sub OnReturn()
        Me.mForm.Session("ID2") = Me.mForm.FindControls("ddlGroups").SelectedValue
        Me.mForm.Session("ID1") = Me.mForm.FindControls("ddlForms").SelectedValue
        Me.mForm.Response.Redirect("~/Forms/frmGroups.aspx")
    End Sub

    Public Sub OnSelectForm()
        Me.LoadGroupCombo(Me.mForm.FindControls("ddlForm").SelectedValue, 0)
        Me.OnSelectGroup()
    End Sub

    Public Sub OnSelectGroup()
        Me.LoadProfileCombo(0)
        Me.OnSelectProfile()
    End Sub

    Public Sub OnSelectProfile()
        If Me.mForm.FindControls("ddlProfile").SelectedValue <> 0 Then
            Me.EnabledChecks(True, Me.mForm.FindControls("ckbSelect").Checked)
        Else
            Me.CheckedChecks(False)
            Me.EnabledChecks(False, False)
        End If
    End Sub

    Public Sub OnSelectChecked(ByVal checked As Boolean)
        If Me.mForm.FindControls("ckbSelect").Checked Then
            Me.EnabledChecks(True, Me.mForm.FindControls("ckbSelect").Checked)
        Else
            Me.CheckedChecks(False)
            Me.EnabledChecks(False, True)
        End If
    End Sub

    Public Sub OnSave()
        If Me.SaveData Then
            Me.LoadFilters(Me.mForm.FindControls("ddlGroup").SelectedValue, Me.mForm.FindControls("ddlForm").SelectedValue)
            Me.mDataList = Me.GetDataList()
            Me.PopulateDataList("Form", "ASC", 0)

            Me.mEditMode = True
            Me.ApplyMenuItemAuthorizationRules(True)
            Me.PopulateData()
        End If
    End Sub

    Public Sub OnOk()
        If Me.SaveData Then
            Me.ShowAddForm(False)
            Me.LoadFilters(Me.mForm.FindControls("ddlGroup").SelectedValue, Me.mForm.FindControls("ddlForm").SelectedValue)
            Me.mDataList = Me.GetDataList()
            Me.PopulateDataList("Form", "ASC", 0)
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
        Me.mEditMode = True
        Me.LoadFormComboItem(Me.mData.Group.Form.ID, Me.mData.Group.Form.Description)
        Me.LoadGroupComboItem(Me.mData.Group.ID, Me.mData.Group.Description)
        Me.LoadProfileComboItem(Me.mData.ID, Me.mData.Description)
        Me.CheckedChecks(Me.mData.PSelect, Me.mData.PInsert, Me.mData.PUpdate, Me.mData.PDelete)
        Me.EnabledChecks(True, Me.mData.PSelect)
    End Sub

    Private Sub ClearAddForm()
        Me.LoadFormCombo(Me.mForm.FindControls("ddlForms").SelectedValue)
        Me.LoadGroupCombo(Me.mForm.FindControls("ddlForm").SelectedValue, Me.mForm.FindControls("ddlGroups").SelectedValue)
        Me.LoadProfileCombo(0)
        Me.CheckedChecks(False)
        Me.EnabledChecks(False, False)
    End Sub

    Private Function CollectData() As GroupProfileNewData
        Dim formData As New GroupProfileNewData

        formData.Group.Form.ID.SetValues("ddlForm", True, Me.mData.Group.Form.ID, CLng(Me.mForm.FindControls("ddlForm").SelectedValue))
        formData.Group.Form.Description.SetValues("ddlForm", False, Me.mData.Group.Form.Description, Me.mForm.FindControls("ddlForm").SelectedItem.Text)

        formData.Group.ID.SetValues("ddlGroup", True, Me.mData.Group.ID, CLng(Me.mForm.FindControls("ddlGroup").SelectedValue))
        formData.Group.Description.SetValues("ddlGroup", False, Me.mData.Group.Description, Me.mForm.FindControls("ddlGroup").SelectedItem.Text)

        formData.ID.SetValues("ddlProfile", True, Me.mData.ID, CLng(Me.mForm.FindControls("ddlProfile").SelectedItem.Value))
        formData.Description.SetValues("ddlProfile", False, Me.mData.Description, Me.mForm.FindControls("ddlProfile").SelectedItem.Text)

        formData.PSelect.SetValues("ckbSelect", False, Me.mData.PSelect, Me.mForm.FindControls("ckbSelect").Checked)
        formData.PInsert.SetValues("ckbInsert", False, Me.mData.PInsert, Me.mForm.FindControls("ckbInsert").Checked)
        formData.PUpdate.SetValues("ckbUpdate", False, Me.mData.PUpdate, Me.mForm.FindControls("ckbUpdate").Checked)
        formData.PDelete.SetValues("ckbDelete", False, Me.mData.PDelete, Me.mForm.FindControls("ckbDelete").Checked)

        Return formData
    End Function

    Private Function CollectIDData() As GroupProfileNewData
        Dim formData As New GroupProfileNewData
        formData.ID.SetValues("ddlProfile", True, 0, CLng(Me.mForm.FindControls("ddlProfile").SelectedValue))
        formData.Group.Form.ID.SetValues("ddlForm", True, 0, CLng(Me.mForm.FindControls("ddlForm").SelectedValue))
        formData.Group.ID.SetValues("ddlGroup", True, 0, CLng(Me.mForm.FindControls("ddlGroup").SelectedValue))
        Return formData
    End Function

    Private Sub CollectFilters()
        Me.mGroupIDList = Me.CollectIDSelectedList(Me.mForm.FindControls("ddlGroups"))
        Me.mFormIDList = Me.CollectIDSelectedList(Me.mForm.FindControls("ddlForms"))
    End Sub

    Private Function CollectIDSelectedList(ByVal list As DropDownList) As Long()
        If list.SelectedValue = 0 Then
            Return New Long() {}
        End If
        Return New Long() {list.SelectedValue}
    End Function

    Private Function GetProfileInfoList() As ProfileData()
        Try
            Return ClsSessionAdmin.GetProfileInfoList()
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return New ProfileData() {}
        End Try
    End Function

    Private Function GetFormInfoList() As FormData()
        Try
            Return ClsSessionAdmin.GetFormInfoList()
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return New FormData() {}
        End Try
    End Function

    Private Function GetGroupInfoList(ByVal formID As Long) As GroupData()
        Try
            Return ClsSessionAdmin.GetGroupInfoList(formID)
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return New GroupData() {}
        End Try
    End Function

    Private Function GetDataList() As GroupProfileData()
        Try
            Me.CollectFilters()
            Return ClsSessionAdmin.GetGroupProfileList(Me.mGroupIDList, Me.mFormIDList)
        Catch SysEx As Exception
            Return New GroupProfileData() {}
        End Try
    End Function

    Private Function GetData() As GroupProfileData
        Try
            Return ClsSessionAdmin.GetGroupProfile(Me.CollectIDData)
            Return Nothing
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return Nothing
        End Try
    End Function

    Private Function SaveData() As Boolean
        Try
            If Me.mEditMode Then
                Me.mData = ClsSessionAdmin.EditGroupProfile(Me.CollectData)
            Else
                Me.mData = ClsSessionAdmin.AddGroupProfile(Me.CollectData)
            End If
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error saving: " & SysEx.Message)
            Return False
        End Try
    End Function

    Private Function DeleteData() As Boolean
        Try
            ClsSessionAdmin.DeleteGroupProfile(Me.CollectIDData)
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error deleting: " & SysEx.Message)
            Return False
        End Try
    End Function

#End Region

End Class
