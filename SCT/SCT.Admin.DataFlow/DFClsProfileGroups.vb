Imports SCT.Library
Imports SCT.Library.AdminSite
Imports System.Web.UI.WebControls

Public Class DFClsProfileGroups

#Region " Private Fields "

    Private mForm As Object
    Private mData As ProfileGroupData
    Private mDataList As ProfileGroupData()

    Private mPageIndex As Integer
    Private mSortDirection As String
    Private mSortExpression As String

    Private mProfileIDList() As Long = New Long() {}
    Private mFormIDList() As Long = New Long() {}

    Private mEditMode As Boolean

#End Region

#Region " Authorization Rules "

    Public Shared Function CanSelect() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmProfileGroups") Then
            Return ClsSessionAdmin.CanSelectInForm("frmProfileGroups")
        Else
            Return ClsSessionAdmin.CanSelectInForm("AllForms")
        End If
    End Function

    Public Shared Function CanInsert() As Boolean
        Return ClsSessionAdmin.CanInsertInForm("frmProfileGroups")
    End Function

    Public Shared Function CanUpdate() As Boolean
        Return ClsSessionAdmin.CanUpdateInForm("frmProfileGroups")
    End Function

    Public Shared Function CanDelete() As Boolean
        Return ClsSessionAdmin.CanDeleteInForm("frmProfileGroups")
    End Function

    Public Shared Function CanSelectField(ByVal fieldName As String) As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmProfileGroups") Then
            Return ClsSessionAdmin.CanSelectFieldInForm("frmProfileGroups", fieldName)
        Else
            Return ClsSessionAdmin.CanSelectFieldInForm("AllForms", "AllFields")
        End If
    End Function

    Public Shared Function CanInsertField(ByVal fieldName As String) As Boolean
        Return ClsSessionAdmin.CanInsertFieldInForm("frmProfileGroups", fieldName)
    End Function

    Public Shared Function CanUpdateField(ByVal fieldName As String) As Boolean
        Return ClsSessionAdmin.CanUpdateFieldInForm("frmProfileGroups", fieldName)
    End Function

    Public Shared Function CanDeleteField(ByVal fieldName As String) As Boolean
        Return ClsSessionAdmin.CanDeleteFieldInForm("frmProfileGroups", fieldName)
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

        Me.LoadFilters(Me.mForm.Session("ID1"), Me.mForm.Session("ID2"))
        Me.mDataList = Me.GetDataList()
        Me.PopulateDataList("Profile", "ASC", 0)
    End Sub

    Public Sub ApplyPageAuthorizationRules()
        Dim canSelectField As Boolean
        Dim canSelectCol As Boolean

        If (Not ClsSessionAdmin.IsValidForm("frmProfileGroups", "colGroup", "colForm", "colProfile", "colProfileGroupSelect", "colProfileGroupInsert", "colProfileGroupUpdate", "colProfileGroupDelete", "ddlProfile", "ddlForm", "ddlGroup", "ckbSelect", "ckbInsert", "ckbUpdate", "ckbDelete")) OrElse (Not DFClsProfileGroups.CanSelect()) Then Me.mForm.Response.Redirect("~/Forms/frmMain.aspx")

        canSelectCol = DFClsProfileGroups.CanSelectField("colGroup") AndAlso DFClsProfileGroups.CanSelectField("colForm") AndAlso DFClsProfileGroups.CanSelectField("colProfile")

        Me.mForm.FindControls("GridView").Columns(0).Visible = canSelectCol
        Me.mForm.FindControls("lblProfiles").Visible = canSelectCol
        Me.mForm.FindControls("ddlProfiles").Visible = canSelectCol

        Me.mForm.FindControls("GridView").Columns(1).Visible = canSelectCol
        Me.mForm.FindControls("lblForms").Visible = canSelectCol
        Me.mForm.FindControls("ddlForms").Visible = canSelectCol

        Me.mForm.FindControls("GridView").Columns(2).Visible = canSelectCol

        Me.mForm.FindControls("GridView").Columns(3).Visible = canSelectCol AndAlso DFClsProfileGroups.CanSelectField("colProfileGroupSelect")
        Me.mForm.FindControls("GridView").Columns(4).Visible = canSelectCol AndAlso DFClsProfileGroups.CanSelectField("colProfileGroupInsert")
        Me.mForm.FindControls("GridView").Columns(5).Visible = canSelectCol AndAlso DFClsProfileGroups.CanSelectField("colProfileGroupUpdate")
        Me.mForm.FindControls("GridView").Columns(6).Visible = canSelectCol AndAlso DFClsProfileGroups.CanSelectField("colProfileGroupDelete")

        Me.mForm.FindControls("GridView").Columns(7).Visible = canSelectCol AndAlso DFClsProfileGroups.CanUpdate
        Me.mForm.FindControls("GridView").Columns(8).Visible = canSelectCol AndAlso DFClsProfileGroups.CanDelete
        Me.mForm.FindControls("GridView").PagerSettings.Visible = canSelectCol

        Me.mForm.FindControls("mnuAddNew").FindItem("AddNew").Enabled = DFClsProfileGroups.CanInsert()

        canSelectField = DFClsProfileGroups.CanSelectField("ddlProfile")
        Me.mForm.FindControls("ddlProfile").Visible = canSelectField
        Me.mForm.FindControls("lblProfile").Visible = canSelectField

        canSelectField = DFClsProfileGroups.CanSelectField("ddlForm")
        Me.mForm.FindControls("ddlForm").Visible = canSelectField
        Me.mForm.FindControls("lblForm").Visible = canSelectField

        canSelectField = DFClsProfileGroups.CanSelectField("ddlGroup")
        Me.mForm.FindControls("ddlGroup").Visible = canSelectField
        Me.mForm.FindControls("lblGroup").Visible = canSelectField

        canSelectField = DFClsProfileGroups.CanSelectField("ckbSelect")
        Me.mForm.FindControls("ckbSelect").Visible = canSelectField
        Me.mForm.FindControls("lblSelect").Visible = canSelectField

        canSelectField = DFClsProfileGroups.CanSelectField("ckbInsert")
        Me.mForm.FindControls("ckbInsert").Visible = canSelectField
        Me.mForm.FindControls("lblInsert").Visible = canSelectField

        canSelectField = DFClsProfileGroups.CanSelectField("ckbUpdate")
        Me.mForm.FindControls("ckbUpdate").Visible = canSelectField
        Me.mForm.FindControls("lblUpdate").Visible = canSelectField

        canSelectField = DFClsProfileGroups.CanSelectField("ckbDelete")
        Me.mForm.FindControls("ckbDelete").Visible = canSelectField
        Me.mForm.FindControls("lblDelete").Visible = canSelectField
    End Sub

    Public Shared Sub ApplyGridRowAuthorizationRules(ByVal gridView As GridView, ByVal row As System.Web.UI.WebControls.GridViewRow)
        If row.RowType = DataControlRowType.DataRow Then
            Dim canSelectProfile As Boolean = DFClsProfiles.CanSelect

            row.Cells(0).Controls(1).Visible = canSelectProfile
            row.Cells(0).Controls(2).Visible = Not canSelectProfile

            Dim canSelectForm As Boolean = DFClsForms.CanSelect

            row.Cells(1).Controls(1).Visible = canSelectForm
            row.Cells(1).Controls(2).Visible = Not canSelectForm

            Dim canSelectGroup As Boolean = DFClsGroups.CanSelect

            row.Cells(2).Controls(1).Visible = canSelectGroup
            row.Cells(2).Controls(2).Visible = Not canSelectGroup
        End If
    End Sub

    Public Sub ApplyAddFormAuthorizationRules(ByVal editMode As Boolean)
        Dim canSave As Boolean

        Me.mForm.FindControls("ddlProfile").Enabled = (Not editMode AndAlso DFClsProfileGroups.CanInsertField("ddlProfile"))
        Me.mForm.FindControls("ddlForm").Enabled = (Not editMode AndAlso DFClsProfileGroups.CanInsertField("ddlForm"))
        Me.mForm.FindControls("ddlGroup").Enabled = (Not editMode AndAlso DFClsProfileGroups.CanInsertField("ddlGroup"))
        Me.mForm.FindControls("ckbSelect").Enabled = (Not editMode AndAlso DFClsProfileGroups.CanInsertField("ckbSelect")) OrElse (editMode AndAlso DFClsProfileGroups.CanUpdateField("ckbSelect"))
        Me.mForm.FindControls("ckbInsert").Enabled = (Not editMode AndAlso DFClsProfileGroups.CanInsertField("ckbInsert")) OrElse (editMode AndAlso DFClsProfileGroups.CanUpdateField("ckbInsert"))
        Me.mForm.FindControls("ckbUpdate").Enabled = (Not editMode AndAlso DFClsProfileGroups.CanInsertField("ckbUpdate")) OrElse (editMode AndAlso DFClsProfileGroups.CanUpdateField("ckbUpdate"))
        Me.mForm.FindControls("ckbDelete").Enabled = (Not editMode AndAlso DFClsProfileGroups.CanInsertField("ckbDelete")) OrElse (editMode AndAlso DFClsProfileGroups.CanUpdateField("ckbDelete"))

        Me.ApplyMenuItemAuthorizationRules(editMode)

        canSave = (Not editMode AndAlso DFClsProfileGroups.CanInsert) OrElse (editMode AndAlso DFClsProfileGroups.CanUpdate)
        Me.mForm.FindControls("mnuSave").FindItem("Ok").Enabled = canSave
        Me.mForm.FindControls("mnuSave").FindItem("Save").Enabled = canSave

        Me.ShowAddForm(DFClsProfileGroups.CanSelect OrElse DFClsProfileGroups.CanInsert OrElse DFClsProfileGroups.CanUpdate)
    End Sub

    Private Sub ApplyMenuItemAuthorizationRules(ByVal editMode As Boolean)
        Me.mForm.FindControls("mnuItem").FindItem("Group").Enabled = editMode AndAlso DFClsGroups.CanSelect
        Me.mForm.FindControls("mnuItem").FindItem("Form").Enabled = editMode AndAlso DFClsForms.CanSelect
        Me.mForm.FindControls("mnuItem").FindItem("Profile").Enabled = editMode AndAlso DFClsProfiles.CanSelect
        Me.mForm.FindControls("mnuItem").FindItem("Delete").Enabled = editMode AndAlso DFClsProfileGroups.CanDelete
    End Sub

    Public Function ValidateAssign() As Boolean
        Return Not (ClsProfileGroup.Exists(Me.mForm.FindControls("ddlProfile").SelectedValue, Me.mForm.FindControls("ddlForm").SelectedValue, Me.mForm.FindControls("ddlGroup").SelectedValue) AndAlso Not Me.mEditMode)
    End Function

    Private Sub LoadFilters(ByVal profileID As String, ByVal formID As String)
        Me.LoadProfileFilter(profileID)
        Me.LoadProfileFormFilter(Me.mForm.FindControls("ddlProfiles").SelectedValue, formID)
    End Sub

    Private Sub LoadProfileFilter(ByVal profileID As String)
        Me.LoadCombo(Me.mForm.FindControls("ddlProfiles"), Me.GetComboDataSources(Me.GetProfileInfoList, String.Empty), profileID)
    End Sub

    Private Sub LoadProfileFormFilter(ByVal profileID As Long, ByVal formID As String)
        Me.LoadCombo(Me.mForm.FindControls("ddlForms"), Me.GetComboDataSources(Me.GetProfileFormInfoList(profileID), String.Empty), formID)
    End Sub

    Private Sub LoadProfileCombo(ByVal profileID As String)
        Me.LoadCombo(Me.mForm.FindControls("ddlProfile"), Me.GetComboDataSources(Me.GetProfileInfoList, "[Select a Profile]"), profileID)
    End Sub

    Private Sub LoadProfileComboItem(ByVal profileID As Long, ByVal description As String)
        Dim profileData As New ProfileData
        profileData.ID = profileID
        profileData.Description = description
        Me.LoadCombo(Me.mForm.FindControls("ddlProfile"), Me.GetComboDataSources(New ProfileData() {profileData}, "[Select a Profile]"), profileID)
    End Sub

    Private Sub LoadProfileFormCombo(ByVal profileID As Long, ByVal formID As String)
        Me.LoadCombo(Me.mForm.FindControls("ddlForm"), Me.GetComboDataSources(Me.GetProfileFormInfoList(profileID), "[Select a Form]"), formID)
        Me.mForm.FindControls("ddlForm").Enabled = Me.mForm.FindControls("ddlProfile").SelectedValue <> 0
    End Sub

    Private Sub LoadProfileFormComboItem(ByVal formID As Long, ByVal description As String)
        Dim profileFormData As New ProfileFormData
        profileFormData.ID = formID
        profileFormData.Description = description
        Me.LoadCombo(Me.mForm.FindControls("ddlForm"), Me.GetComboDataSources(New ProfileFormData() {profileFormData}, "[Select a Form]"), formID)
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

    Private Sub LoadCombo(ByVal combo As DropDownList, ByVal data As DataTable, ByVal selectedValue As String)
        combo.DataSource = data
        combo.DataTextField = data.Columns(0).Caption
        combo.DataValueField = data.Columns(1).Caption
        combo.DataBind()
        If combo.Items.FindByValue(selectedValue) IsNot Nothing Then
            combo.SelectedValue = selectedValue
        End If
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

        table.Columns.Add("IDProfile", GetType(Long))
        table.Columns.Add("Profile", GetType(String))
        table.Columns.Add("IDForm", GetType(Long))
        table.Columns.Add("Form", GetType(String))
        table.Columns.Add("IDGroup", GetType(Long))
        table.Columns.Add("Group", GetType(String))
        table.Columns.Add("Select", GetType(String))
        table.Columns.Add("Insert", GetType(String))
        table.Columns.Add("Update", GetType(String))
        table.Columns.Add("Delete", GetType(String))

        table.DefaultView.Sort = Me.GetSortProperty

        For Each group As ProfileGroupData In Me.mDataList
            table.Rows.Add(New Object() {group.Form.Profile.ID, group.Form.Profile.Description, group.Form.ID, group.Form.Description, group.ID, group.Description, "~/Images/Checked_" & group.PSelect.ToString & ".png", "~/Images/Checked_" & group.PInsert.ToString & ".png", "~/Images/Checked_" & group.PUpdate.ToString & ".png", "~/Images/Checked_" & group.PDelete.ToString & ".png"})
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
                Return sortProperty & ", Profile " & Me.mSortDirection & ", Group " & Me.mSortDirection
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
        Me.mForm.FindControls("ckbSelect").Enabled = (enabled OrElse CheckedSelect) 'AndAlso Me.mData.PSelect
        Me.mForm.FindControls("ckbInsert").Enabled = enabled AndAlso CheckedSelect 'AndAlso Me.mData.PInsert
        Me.mForm.FindControls("ckbUpdate").Enabled = enabled AndAlso CheckedSelect 'AndAlso Me.mData.PUpdate
        Me.mForm.FindControls("ckbDelete").Enabled = enabled AndAlso CheckedSelect 'AndAlso Me.mData.PDelete
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

    Public Sub OnProfile()
        Me.mForm.Session("ID1") = Me.mData.Form.Profile.ID.ToString
        Me.mForm.Response.Redirect("~/Forms/frmProfiles.aspx")
    End Sub

    Public Sub OnProfile(ByVal profileID As Long)
        Me.mForm.Session("ID1") = profileID
        Me.mForm.Response.Redirect("~/Forms/frmProfiles.aspx")
    End Sub

    Public Sub OnForm()
        Me.mForm.Session("ID1") = Me.mData.Form.ID.ToString
        Me.mForm.Response.Redirect("~/Forms/frmForms.aspx")
    End Sub

    Public Sub OnForm(ByVal formID As Long)
        Me.mForm.Session("ID1") = formID
        Me.mForm.Response.Redirect("~/Forms/frmForms.aspx")
    End Sub

    Public Sub OnGroup()
        Me.mForm.Session("ID1") = Me.mData.Form.ID.ToString
        Me.mForm.Session("ID2") = Me.mData.ID.ToString
        Me.mForm.Response.Redirect("~/Forms/frmGroups.aspx")
    End Sub

    Public Sub OnGroup(ByVal formID As Long, ByVal groupID As Long)
        Me.mForm.Session("ID1") = formID
        Me.mForm.Session("ID2") = groupID
        Me.mForm.Response.Redirect("~/Forms/frmGroups.aspx")
    End Sub

    Public Sub OnEditing(ByVal profileID As Long, ByVal formID As Long, ByVal groupID As Long)
        Me.mEditMode = True
        Me.LoadProfileComboItem(profileID, String.Empty)
        Me.LoadProfileFormComboItem(formID, String.Empty)
        Me.LoadGroupComboItem(groupID, String.Empty)
        Me.ShowAddForm(True)
        Me.mData = Me.GetData()
        If Me.mData IsNot Nothing Then
            Me.ApplyAddFormAuthorizationRules(True)
            Me.PopulateData()
        End If
    End Sub

    Public Sub OnDeleting(ByVal profileID As Long, ByVal formID As Long, ByVal groupID As String)
        Me.ShowAddForm(False)
        Me.LoadProfileComboItem(profileID, String.Empty)
        Me.LoadProfileFormComboItem(formID, String.Empty)
        Me.LoadGroupComboItem(groupID, String.Empty)
        Me.mForm.FindControls("MsgBox").ShowConfirmation("Are you sure you want to delete this Profile?\n\nNote: there is no undo.", "", True, False)
    End Sub

    Public Sub OnDeleting()
        Me.mForm.FindControls("MsgBox").ShowConfirmation("Are you sure you want to delete this Profile?\n\nNote: there is no undo.", "", True, False)
    End Sub

    Public Sub OnMsgBoxOk()
        If Me.DeleteData Then
            Me.ShowAddForm(False)
            Me.LoadFilters(Me.mForm.FindControls("ddlProfiles").SelectedValue, Me.mForm.FindControls("ddlForms").SelectedValue)
            Me.mDataList = Me.GetDataList()
            Me.PopulateDataList("Profile", "ASC", 0)
        End If
    End Sub

    Public Sub OnSelectedProfileChanged()
        Me.ShowAddForm(False)
        Me.LoadProfileFormFilter(Me.mForm.FindControls("ddlProfiles").SelectedValue, 0)
        Me.mDataList = Me.GetDataList()
        Me.PopulateDataList(0)
    End Sub

    Public Sub OnSelectedProfileFormChanged()
        Me.ShowAddForm(False)
        Me.mDataList = Me.GetDataList()
        Me.PopulateDataList(0)
    End Sub

    Public Sub OnAddNew()
        Me.mEditMode = False
        Me.mData = New ProfileGroupData
        Me.ApplyAddFormAuthorizationRules(False)
        Me.ClearAddForm()
    End Sub

    Public Sub OnReturn()
        Me.mForm.Session("ID1") = Me.mForm.FindControls("ddlProfiles").SelectedValue
        Me.mForm.Session("ID2") = Me.mForm.FindControls("ddlForms").SelectedValue
        Me.mForm.Response.Redirect("~/Forms/frmProfileForms.aspx")
    End Sub

    Public Sub OnSelectProfile()
        Me.LoadProfileFormCombo(Me.mForm.FindControls("ddlProfile").SelectedValue, 0)
        Me.OnSelectProfileForm()
    End Sub

    Public Sub OnSelectProfileForm()
        Me.LoadGroupCombo(Me.mForm.FindControls("ddlForm").SelectedValue, 0)
        Me.OnSelectGroup()
    End Sub

    Public Sub OnSelectGroup()
        If Me.mForm.FindControls("ddlGroup").SelectedValue <> 0 Then
            Me.EnabledChecks(True, Me.mForm.FindControls("ckbSelect").Checked)
        Else
            Me.CheckedChecks(False)
            Me.EnabledChecks(False, False)
        End If
    End Sub

    Public Sub OnSelectChecked()
        If Me.mForm.FindControls("ckbSelect").Checked Then
            Me.EnabledChecks(True, Me.mForm.FindControls("ckbSelect").Checked)
        Else
            Me.CheckedChecks(False)
            Me.EnabledChecks(False, True)
        End If
    End Sub

    Public Sub OnSave()
        If Me.SaveData Then
            Me.LoadFilters(Me.mForm.FindControls("ddlProfile").SelectedValue, Me.mForm.FindControls("ddlForm").SelectedValue)
            Me.mDataList = Me.GetDataList()
            Me.PopulateDataList("Profile", "ASC", 0)

            Me.mEditMode = True
            Me.ApplyMenuItemAuthorizationRules(True)
            Me.PopulateData()
        End If
    End Sub

    Public Sub OnOk()
        If Me.SaveData Then
            Me.ShowAddForm(False)
            Me.LoadFilters(Me.mForm.FindControls("ddlProfile").SelectedValue, Me.mForm.FindControls("ddlForm").SelectedValue)
            Me.mDataList = Me.GetDataList()
            Me.PopulateDataList("Profile", "ASC", 0)
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
        Me.LoadProfileComboItem(Me.mData.Form.Profile.ID, Me.mData.Form.Profile.Description)
        Me.LoadProfileFormComboItem(Me.mData.Form.ID, Me.mData.Form.Description)
        Me.LoadGroupComboItem(Me.mData.ID, Me.mData.Description)
        Me.CheckedChecks(Me.mData.PSelect, Me.mData.PInsert, Me.mData.PUpdate, Me.mData.PDelete)
        Me.EnabledChecks(True, Me.mData.PSelect)
    End Sub

    Private Sub ClearAddForm()
        Me.LoadProfileCombo(Me.mForm.FindControls("ddlProfiles").SelectedValue)
        Me.LoadProfileFormCombo(Me.mForm.FindControls("ddlProfile").SelectedValue, Me.mForm.FindControls("ddlForms").SelectedValue)
        Me.LoadGroupCombo(Me.mForm.FindControls("ddlForm").SelectedValue, 0)
        Me.CheckedChecks(False)
        Me.EnabledChecks(False, False)
    End Sub

    Private Function CollectData() As ProfileGroupNewData
        Dim formData As New ProfileGroupNewData

        formData.Form.Profile.ID.SetValues("ddlProfile", True, Me.mData.Form.Profile.ID, CLng(Me.mForm.FindControls("ddlProfile").SelectedItem.Value))
        formData.Form.Profile.Description.SetValues("ddlProfile", False, Me.mData.Form.Profile.Description, Me.mForm.FindControls("ddlProfile").SelectedItem.Text)

        formData.Form.ID.SetValues("ddlForm", True, Me.mData.Form.ID, CLng(Me.mForm.FindControls("ddlForm").SelectedValue))
        formData.Form.Description.SetValues("ddlForm", False, Me.mData.Form.Description, Me.mForm.FindControls("ddlForm").SelectedItem.Text)

        formData.ID.SetValues("ddlGroup", True, Me.mData.ID, CLng(Me.mForm.FindControls("ddlGroup").SelectedValue))
        formData.Description.SetValues("ddlGroup", False, Me.mData.Description, Me.mForm.FindControls("ddlGroup").SelectedItem.Text)

        formData.PSelect.SetValues("ckbSelect", False, Me.mData.PSelect, Me.mForm.FindControls("ckbSelect").Checked)
        formData.PInsert.SetValues("ckbInsert", False, Me.mData.PInsert, Me.mForm.FindControls("ckbInsert").Checked)
        formData.PUpdate.SetValues("ckbUpdate", False, Me.mData.PUpdate, Me.mForm.FindControls("ckbUpdate").Checked)
        formData.PDelete.SetValues("ckbDelete", False, Me.mData.PDelete, Me.mForm.FindControls("ckbDelete").Checked)

        Return formData
    End Function

    Private Function CollectIDData() As ProfileGroupNewData
        Dim formData As New ProfileGroupNewData
        formData.Form.Profile.ID.SetValues("ddlProfile", True, 0, CLng(Me.mForm.FindControls("ddlProfile").SelectedValue))
        formData.Form.ID.SetValues("ddlForm", True, 0, CLng(Me.mForm.FindControls("ddlForm").SelectedValue))
        formData.ID.SetValues("ddlGroup", True, 0, CLng(Me.mForm.FindControls("ddlGroup").SelectedValue))
        Return formData
    End Function

    Private Sub CollectFilters()
        Me.mProfileIDList = Me.CollectIDSelectedList(Me.mForm.FindControls("ddlProfiles"))
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

    Private Function GetProfileFormInfoList(ByVal profileID As Long) As ProfileFormData()
        Try
            Return ClsSessionAdmin.GetProfileFormInfoList(profileID)
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return New ProfileFormData() {}
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

    Private Function GetDataList() As ProfileGroupData()
        Try
            Me.CollectFilters()
            Return ClsSessionAdmin.GetProfileGroupList(Me.mProfileIDList, Me.mFormIDList)
        Catch SysEx As Exception
            Return New ProfileGroupData() {}
        End Try
    End Function

    Private Function GetData() As ProfileGroupData
        Try
            Return ClsSessionAdmin.GetProfileGroup(Me.CollectIDData)
            Return Nothing
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return Nothing
        End Try
    End Function

    Private Function SaveData() As Boolean
        Try
            If Me.mEditMode Then
                Me.mData = ClsSessionAdmin.EditProfileGroup(Me.CollectData)
            Else
                Me.mData = ClsSessionAdmin.AddProfileGroup(Me.CollectData)
            End If
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error saving: " & SysEx.Message)
            Return False
        End Try
    End Function

    Private Function DeleteData() As Boolean
        Try
            ClsSessionAdmin.DeleteProfileGroup(Me.CollectIDData)
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error deleting: " & SysEx.Message)
            Return False
        End Try
    End Function

#End Region

End Class
