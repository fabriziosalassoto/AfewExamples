Imports SCT.Library
Imports SCT.Library.AdminSite
Imports System.Web.UI.WebControls

Public Class DFClsProfileForms

#Region " Private Fields "

    Private mForm As Object
    Private mData As ProfileFormData
    Private mDataList As ProfileFormData()

    Private mPageIndex As Integer
    Private mSortDirection As String
    Private mSortExpression As String

    Private mProfileIDList() As Long = New Long() {}

    Private mEditMode As Boolean

#End Region

#Region " Authorization Rules "

    Public Shared Function CanSelect() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmProfileForms") Then
            Return ClsSessionAdmin.CanSelectInForm("frmProfileForms")
        Else
            Return ClsSessionAdmin.CanSelectInForm("AllForms")
        End If
    End Function

    Public Shared Function CanInsert() As Boolean
        Return ClsSessionAdmin.CanInsertInForm("frmProfileForms")
    End Function

    Public Shared Function CanUpdate() As Boolean
        Return ClsSessionAdmin.CanUpdateInForm("frmProfileForms")
    End Function

    Public Shared Function CanDelete() As Boolean
        Return ClsSessionAdmin.CanDeleteInForm("frmProfileForms")
    End Function

    Public Shared Function CanSelectField(ByVal fieldName As String) As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmProfileForms") Then
            Return ClsSessionAdmin.CanSelectFieldInForm("frmProfileForms", fieldName)
        Else
            Return ClsSessionAdmin.CanSelectFieldInForm("AllForms", "AllFields")
        End If
    End Function

    Public Shared Function CanInsertField(ByVal fieldName As String) As Boolean
        Return ClsSessionAdmin.CanInsertFieldInForm("frmProfileForms", fieldName)
    End Function

    Public Shared Function CanUpdateField(ByVal fieldName As String) As Boolean
        Return ClsSessionAdmin.CanUpdateFieldInForm("frmProfileForms", fieldName)
    End Function

    Public Shared Function CanDeleteField(ByVal fieldName As String) As Boolean
        Return ClsSessionAdmin.CanDeleteFieldInForm("frmProfileForms", fieldName)
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

        Me.LoadFilters(Me.mForm.Session("ID1"))
        Me.mDataList = Me.GetDataList()
        Me.PopulateDataList("Profile", "ASC", 0)

        If IsNumeric(Me.mForm.Session("ID1")) AndAlso IsNumeric(Me.mForm.Session("ID2")) AndAlso Me.mForm.Session("ID1") <> 0 AndAlso Me.mForm.Session("ID2") <> 0 Then
            Me.OnEditing(Me.mForm.Session("ID1"), Me.mForm.Session("ID2"))
        End If
    End Sub

    Public Sub ApplyPageAuthorizationRules()
        Dim canSelectField As Boolean
        Dim canSelectCol As Boolean

        If (Not ClsSessionAdmin.IsValidForm("frmProfileForms", "colForm", "colProfile", "colProfileFormSelect", "colProfileFormInsert", "colProfileFormUpdate", "colProfileFormDelete", "ddlProfile", "ddlForm", "ckbSelect", "ckbInsert", "ckbUpdate", "ckbDelete")) OrElse (Not DFClsProfileForms.CanSelect()) Then Me.mForm.Response.Redirect("~/Forms/frmMain.aspx")

        canSelectCol = DFClsProfileForms.CanSelectField("colForm") AndAlso DFClsProfileForms.CanSelectField("colProfile")

        Me.mForm.FindControls("GridView").Columns(0).Visible = canSelectCol
        Me.mForm.FindControls("lblProfiles").Visible = canSelectCol
        Me.mForm.FindControls("ddlProfiles").Visible = canSelectCol

        Me.mForm.FindControls("GridView").Columns(1).Visible = canSelectCol

        Me.mForm.FindControls("GridView").Columns(2).Visible = canSelectCol AndAlso DFClsProfileForms.CanSelectField("colProfileFormSelect")
        Me.mForm.FindControls("GridView").Columns(3).Visible = canSelectCol AndAlso DFClsProfileForms.CanSelectField("colProfileFormInsert")
        Me.mForm.FindControls("GridView").Columns(4).Visible = canSelectCol AndAlso DFClsProfileForms.CanSelectField("colProfileFormUpdate")
        Me.mForm.FindControls("GridView").Columns(5).Visible = canSelectCol AndAlso DFClsProfileForms.CanSelectField("colProfileFormDelete")

        Me.mForm.FindControls("GridView").Columns(6).Visible = canSelectCol AndAlso DFClsProfileGroups.CanSelect
        Me.mForm.FindControls("GridView").Columns(7).Visible = canSelectCol AndAlso DFClsProfileForms.CanUpdate
        Me.mForm.FindControls("GridView").Columns(8).Visible = canSelectCol AndAlso DFClsProfileForms.CanDelete
        Me.mForm.FindControls("GridView").PagerSettings.Visible = canSelectCol

        Me.mForm.FindControls("mnuAddNew").FindItem("AddNew").Enabled = DFClsProfileForms.CanInsert()

        canSelectField = DFClsProfileForms.CanSelectField("ddlProfile")
        Me.mForm.FindControls("ddlProfile").Visible = canSelectField
        Me.mForm.FindControls("lblProfile").Visible = canSelectField

        canSelectField = DFClsProfileForms.CanSelectField("ddlForm")
        Me.mForm.FindControls("ddlForm").Visible = canSelectField
        Me.mForm.FindControls("lblForm").Visible = canSelectField

        Me.mForm.FindControls("ckbSelect").Visible = DFClsProfileForms.CanSelectField("ckbSelect")
        Me.mForm.FindControls("ckbInsert").Visible = DFClsProfileForms.CanSelectField("ckbInsert")
        Me.mForm.FindControls("ckbUpdate").Visible = DFClsProfileForms.CanSelectField("ckbUpdate")
        Me.mForm.FindControls("ckbDelete").Visible = DFClsProfileForms.CanSelectField("ckbDelete")
    End Sub

    Public Shared Sub ApplyGridRowAuthorizationRules(ByVal gridView As GridView, ByVal row As System.Web.UI.WebControls.GridViewRow)
        If row.RowType = DataControlRowType.DataRow Then
            Dim canSelectProfile As Boolean = DFClsProfiles.CanSelect

            row.Cells(0).Controls(1).Visible = canSelectProfile
            row.Cells(0).Controls(2).Visible = Not canSelectProfile

            Dim canSelectForm As Boolean = DFClsForms.CanSelect

            row.Cells(1).Controls(1).Visible = canSelectForm
            row.Cells(1).Controls(2).Visible = Not canSelectForm
        End If
    End Sub

    Public Sub ApplyAddFormAuthorizationRules(ByVal editMode As Boolean)
        Dim canSave As Boolean

        Me.mForm.FindControls("ddlProfile").Enabled = (Not editMode AndAlso DFClsProfileForms.CanInsertField("ddlProfile"))
        Me.mForm.FindControls("ddlForm").Enabled = (Not editMode AndAlso DFClsProfileForms.CanInsertField("ddlForm"))
        Me.mForm.FindControls("ckbSelect").Enabled = (Not editMode AndAlso DFClsProfileForms.CanInsertField("ckbSelect")) OrElse (editMode AndAlso DFClsProfileForms.CanUpdateField("ckbSelect"))
        Me.mForm.FindControls("ckbInsert").Enabled = (Not editMode AndAlso DFClsProfileForms.CanInsertField("ckbInsert")) OrElse (editMode AndAlso DFClsProfileForms.CanUpdateField("ckbInsert"))
        Me.mForm.FindControls("ckbUpdate").Enabled = (Not editMode AndAlso DFClsProfileForms.CanInsertField("ckbUpdate")) OrElse (editMode AndAlso DFClsProfileForms.CanUpdateField("ckbUpdate"))
        Me.mForm.FindControls("ckbDelete").Enabled = (Not editMode AndAlso DFClsProfileForms.CanInsertField("ckbDelete")) OrElse (editMode AndAlso DFClsProfileForms.CanUpdateField("ckbDelete"))

        Me.ApplyMenuItemAuthorizationRules(editMode)

        canSave = (Not editMode AndAlso DFClsProfileForms.CanInsert) OrElse (editMode AndAlso DFClsProfileForms.CanUpdate)
        Me.mForm.FindControls("mnuSave").FindItem("Ok").Enabled = canSave
        Me.mForm.FindControls("mnuSave").FindItem("Save").Enabled = canSave

        Me.ShowAddForm(DFClsProfileForms.CanSelect OrElse DFClsProfileForms.CanInsert OrElse DFClsProfileForms.CanUpdate)
    End Sub

    Private Sub ApplyMenuItemAuthorizationRules(ByVal editMode As Boolean)
        Me.mForm.FindControls("mnuItem").FindItem("Form").Enabled = editMode AndAlso DFClsForms.CanSelect
        Me.mForm.FindControls("mnuItem").FindItem("Profile").Enabled = editMode AndAlso DFClsProfiles.CanSelect
        Me.mForm.FindControls("mnuItem").FindItem("Groups").Enabled = editMode AndAlso DFClsProfileGroups.CanSelect
        Me.mForm.FindControls("mnuItem").FindItem("Delete").Enabled = editMode AndAlso DFClsProfileForms.CanDelete
    End Sub

    Public Function ValidateAssign() As Boolean
        Return Not (ClsProfileForm.Exists(Me.mForm.FindControls("ddlProfile").SelectedValue, Me.mForm.FindControls("ddlForm").SelectedValue) AndAlso Not Me.mEditMode)
    End Function

    Private Sub LoadFilters(ByVal profileID As String)
        Me.LoadProfileFilter(profileID)
    End Sub

    Private Sub LoadProfileFilter(ByVal profileID As String)
        Me.LoadCombo(Me.mForm.FindControls("ddlProfiles"), Me.GetComboDataSources(Me.GetProfileInfoList, String.Empty), profileID)
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

    Private Sub LoadFormCombo(ByVal formID As String)
        Me.LoadCombo(Me.mForm.FindControls("ddlForm"), Me.GetComboDataSources(Me.GetFormInfoList, "[Select a Form]"), formID)
        Me.mForm.FindControls("ddlForm").Enabled = Me.mForm.FindControls("ddlProfile").SelectedValue <> 0
    End Sub

    Private Sub LoadFormComboItem(ByVal formID As Long, ByVal description As String)
        Dim formData As New FormData
        formData.ID = formID
        formData.Description = description
        Me.LoadCombo(Me.mForm.FindControls("ddlForm"), Me.GetComboDataSources(New FormData() {formData}, "[Select a Form]"), formID)
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
        GridView.DataSource = Me.GetFormDataSources
        GridView.DataBind()
    End Sub

    Private Function GetFormDataSources() As DataTable
        Dim table As New DataTable

        table.Columns.Add("IDForm", GetType(Long))
        table.Columns.Add("Form", GetType(String))
        table.Columns.Add("IDProfile", GetType(Long))
        table.Columns.Add("Profile", GetType(String))
        table.Columns.Add("Select", GetType(String))
        table.Columns.Add("Insert", GetType(String))
        table.Columns.Add("Update", GetType(String))
        table.Columns.Add("Delete", GetType(String))

        table.DefaultView.Sort = Me.GetSortProperty

        For Each form As ProfileFormData In Me.mDataList
            table.Rows.Add(New Object() {form.ID, form.Description, form.Profile.ID, form.Profile.Description, "~/Images/Checked_" & form.PSelect.ToString & ".png", "~/Images/Checked_" & form.PInsert.ToString & ".png", "~/Images/Checked_" & form.PUpdate.ToString & ".png", "~/Images/Checked_" & form.PDelete.ToString & ".png"})
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
            Case "Form"
                Return sortProperty & ", Profile " & Me.mSortDirection
            Case "Profile"
                Return sortProperty & ", Form " & Me.mSortDirection
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

    Public Sub OnGroups(ByVal profileID As Long, ByVal formID As Long)
        Me.mForm.Session("ID1") = profileID
        Me.mForm.Session("ID2") = formID
        Me.mForm.Response.Redirect("~/Forms/frmProfileGroups.aspx")
    End Sub

    Public Sub OnGroups()
        Me.mForm.Session("ID1") = Me.mData.Profile.ID.ToString
        Me.mForm.Session("ID2") = Me.mData.ID.ToString
        Me.mForm.Response.Redirect("~/Forms/frmProfileGroups.aspx")
    End Sub

    Public Sub OnProfile(ByVal profileID As Long)
        Me.mForm.Session("ID1") = profileID
        Me.mForm.Response.Redirect("~/Forms/frmProfiles.aspx")
    End Sub

    Public Sub OnProfile()
        Me.mForm.Session("ID1") = Me.mData.Profile.ID.ToString
        Me.mForm.Response.Redirect("~/Forms/frmProfiles.aspx")
    End Sub

    Public Sub OnForm(ByVal formID As Long)
        Me.mForm.Session("ID1") = formID
        Me.mForm.Response.Redirect("~/Forms/frmForms.aspx")
    End Sub

    Public Sub OnForm()
        Me.mForm.Session("ID1") = Me.mData.ID.ToString
        Me.mForm.Response.Redirect("~/Forms/frmForms.aspx")
    End Sub

    Public Sub OnEditing(ByVal profileID As Long, ByVal formID As Long)
        Me.mEditMode = True
        Me.LoadProfileComboItem(profileID, String.Empty)
        Me.LoadFormComboItem(formID, String.Empty)
        Me.ShowAddForm(True)
        Me.mData = Me.GetData()
        If Me.mData IsNot Nothing Then
            Me.ApplyAddFormAuthorizationRules(True)
            Me.PopulateData()
        End If
    End Sub

    Public Sub OnDeleting(ByVal profileID As Long, ByVal formID As Long)
        Me.ShowAddForm(False)
        Me.LoadProfileComboItem(profileID, String.Empty)
        Me.LoadFormComboItem(formID, String.Empty)
        Me.mForm.FindControls("MsgBox").ShowConfirmation("Are you sure you want to delete this Form?\n\nNote: there is no undo.", "", True, False)
    End Sub

    Public Sub OnDeleting()
        Me.mForm.FindControls("MsgBox").ShowConfirmation("Are you sure you want to delete this Form?\n\nNote: there is no undo.", "", True, False)
    End Sub

    Public Sub OnMsgBoxOk()
        If Me.DeleteData Then
            Me.ShowAddForm(False)
            Me.LoadFilters(Me.mForm.FindControls("ddlProfiles").SelectedValue)
            Me.mDataList = Me.GetDataList()
            Me.PopulateDataList("Profile", "ASC", 0)
        End If
    End Sub

    Public Sub OnSelectedProfileChanged()
        Me.ShowAddForm(False)
        Me.mDataList = Me.GetDataList()
        Me.PopulateDataList(0)
    End Sub

    Public Sub OnAddNew()
        Me.mEditMode = False
        Me.mData = New ProfileFormData
        Me.ApplyAddFormAuthorizationRules(False)
        Me.ClearAddForm()
    End Sub

    Public Sub OnReturn()
        Me.mForm.Session("ID1") = Me.mForm.FindControls("ddlProfiles").SelectedValue
        Me.mForm.Response.Redirect("~/Forms/frmProfiles.aspx")
    End Sub

    Public Sub OnSelectProfile()
        Me.LoadFormCombo(0)
        Me.OnSelectForm()
    End Sub

    Public Sub OnSelectForm()
        If Me.mForm.FindControls("ddlForm").SelectedValue <> 0 Then
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
            Me.LoadFilters(Me.mForm.FindControls("ddlProfile").SelectedValue)
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
            Me.LoadFilters(Me.mForm.FindControls("ddlProfile").SelectedValue)
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
        Me.LoadProfileComboItem(Me.mData.Profile.ID, Me.mData.Profile.Description)
        Me.LoadFormComboItem(Me.mData.ID, Me.mData.Description)
        Me.CheckedChecks(Me.mData.PSelect, Me.mData.PInsert, Me.mData.PUpdate, Me.mData.PDelete)
        Me.EnabledChecks(True, Me.mData.PSelect)
    End Sub

    Private Sub ClearAddForm()
        Me.LoadProfileCombo(Me.mForm.FindControls("ddlProfiles").SelectedValue)
        Me.LoadFormCombo(0)
        Me.CheckedChecks(False)
        Me.EnabledChecks(False, False)
    End Sub

    Private Function CollectData() As ProfileFormNewData
        Dim formData As New ProfileFormNewData
        formData.ID.SetValues("ddlForm", True, Me.mData.ID, CLng(Me.mForm.FindControls("ddlForm").SelectedValue))
        formData.Description.SetValues("ddlForm", False, Me.mData.Description, Me.mForm.FindControls("ddlForm").SelectedItem.Text)

        formData.Profile.ID.SetValues("ddlProfile", True, Me.mData.Profile.ID, CLng(Me.mForm.FindControls("ddlProfile").SelectedItem.Value))
        formData.Profile.Description.SetValues("ddlProfile", False, Me.mData.Profile.Description, Me.mForm.FindControls("ddlProfile").SelectedItem.Text)

        formData.PSelect.SetValues("ckbSelect", False, Me.mData.PSelect, Me.mForm.FindControls("ckbSelect").Checked)
        formData.PInsert.SetValues("ckbInsert", False, Me.mData.PInsert, Me.mForm.FindControls("ckbInsert").Checked)
        formData.PUpdate.SetValues("ckbUpdate", False, Me.mData.PUpdate, Me.mForm.FindControls("ckbUpdate").Checked)
        formData.PDelete.SetValues("ckbDelete", False, Me.mData.PDelete, Me.mForm.FindControls("ckbDelete").Checked)

        Return formData
    End Function

    Private Function CollectIDData() As ProfileFormNewData
        Dim formData As New ProfileFormNewData
        formData.Profile.ID.SetValues("ddlProfile", True, 0, CLng(Me.mForm.FindControls("ddlProfile").SelectedValue))
        formData.ID.SetValues("ddlForm", True, 0, CLng(Me.mForm.FindControls("ddlForm").SelectedValue))
        Return formData
    End Function

    Private Sub CollectFilters()
        Me.mProfileIDList = Me.CollectIDSelectedList(Me.mForm.FindControls("ddlProfiles"))
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

    Private Function GetDataList() As ProfileFormData()
        Try
            Me.CollectFilters()
            Return ClsSessionAdmin.GetProfileFormList(Me.mProfileIDList)
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return New ProfileFormData() {}
        End Try
    End Function

    Private Function GetData() As ProfileFormData
        Try
            Return ClsSessionAdmin.GetProfileForm(Me.CollectIDData)
            Return Nothing
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return Nothing
        End Try
    End Function

    Private Function SaveData() As Boolean
        Try
            If Me.mEditMode Then
                Me.mData = ClsSessionAdmin.EditProfileForm(Me.CollectData)
            Else
                Me.mData = ClsSessionAdmin.AddProfileForm(Me.CollectData)
            End If
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error saving: " & SysEx.Message)
            Return False
        End Try
    End Function

    Private Function DeleteData() As Boolean
        Try
            ClsSessionAdmin.DeleteProfileForm(Me.CollectIDData)
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error deleting: " & SysEx.Message)
            Return False
        End Try
    End Function

#End Region

End Class
