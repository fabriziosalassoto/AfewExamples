Imports SCT.Library
Imports SCT.Library.AdminSite
Imports System.Web.UI.WebControls

Public Class DFClsFormProfiles

#Region " Private Fields "

    Private mForm As Object
    Private mData As FormProfileData
    Private mDataList As FormProfileData()

    Private mPageIndex As Integer
    Private mSortDirection As String
    Private mSortExpression As String

    Private mFormIDList() As Long = New Long() {}

    Private mEditMode As Boolean

#End Region

#Region " Authorization Rules "

    Public Shared Function CanSelect() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmFormProfiles") Then
            Return ClsSessionAdmin.CanSelectInForm("frmFormProfiles")
        Else
            Return ClsSessionAdmin.CanSelectInForm("AllForms")
        End If
    End Function

    Public Shared Function CanInsert() As Boolean
        Return ClsSessionAdmin.CanInsertInForm("frmFormProfiles")
    End Function

    Public Shared Function CanUpdate() As Boolean
        Return ClsSessionAdmin.CanUpdateInForm("frmFormProfiles")
    End Function

    Public Shared Function CanDelete() As Boolean
        Return ClsSessionAdmin.CanDeleteInForm("frmFormProfiles")
    End Function

    Public Shared Function CanSelectField(ByVal fieldName As String) As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmFormProfiles") Then
            Return ClsSessionAdmin.CanSelectFieldInForm("frmFormProfiles", fieldName)
        Else
            Return ClsSessionAdmin.CanSelectFieldInForm("AllForms", "AllFields")
        End If
    End Function

    Public Shared Function CanInsertField(ByVal fieldName As String) As Boolean
        Return ClsSessionAdmin.CanInsertFieldInForm("frmFormProfiles", fieldName)
    End Function

    Public Shared Function CanUpdateField(ByVal fieldName As String) As Boolean
        Return ClsSessionAdmin.CanUpdateFieldInForm("frmFormProfiles", fieldName)
    End Function

    Public Shared Function CanDeleteField(ByVal fieldName As String) As Boolean
        Return ClsSessionAdmin.CanDeleteFieldInForm("frmFormProfiles", fieldName)
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
        Me.mForm.Session("ValuePath") = "frmSecurity/frmForms"

        Me.ApplyPageAuthorizationRules()

        Me.LoadFilters(Me.mForm.Session("ID1"))
        Me.mDataList = Me.GetDataList()
        Me.PopulateDataList("Form", "ASC", 0)

    End Sub

    Public Sub ApplyPageAuthorizationRules()
        Dim canSelectField As Boolean
        Dim canSelectCol As Boolean

        If (Not ClsSessionAdmin.IsValidForm("frmFormProfiles", "colForm", "colProfile", "colFormProfileSelect", "colFormProfileInsert", "colFormProfileUpdate", "colFormProfileDelete", "ddlProfile", "ddlForm", "ckbSelect", "ckbInsert", "ckbUpdate", "ckbDelete")) OrElse (Not DFClsFormProfiles.CanSelect()) Then Me.mForm.Response.Redirect("~/Forms/frmMain.aspx")

        canSelectCol = DFClsFormProfiles.CanSelectField("colForm") AndAlso DFClsFormProfiles.CanSelectField("colProfile")

        Me.mForm.FindControls("GridView").Columns(0).Visible = canSelectCol
        Me.mForm.FindControls("lblForms").Visible = canSelectCol
        Me.mForm.FindControls("ddlForms").Visible = canSelectCol

        Me.mForm.FindControls("GridView").Columns(1).Visible = canSelectCol

        Me.mForm.FindControls("GridView").Columns(2).Visible = canSelectCol AndAlso DFClsFormProfiles.CanSelectField("colFormProfileSelect")
        Me.mForm.FindControls("GridView").Columns(3).Visible = canSelectCol AndAlso DFClsFormProfiles.CanSelectField("colFormProfileInsert")
        Me.mForm.FindControls("GridView").Columns(4).Visible = canSelectCol AndAlso DFClsFormProfiles.CanSelectField("colFormProfileUpdate")
        Me.mForm.FindControls("GridView").Columns(5).Visible = canSelectCol AndAlso DFClsFormProfiles.CanSelectField("colFormProfileDelete")

        Me.mForm.FindControls("GridView").Columns(6).Visible = canSelectCol AndAlso DFClsFormProfiles.CanUpdate
        Me.mForm.FindControls("GridView").Columns(7).Visible = canSelectCol AndAlso DFClsFormProfiles.CanDelete
        Me.mForm.FindControls("GridView").PagerSettings.Visible = canSelectCol

        Me.mForm.FindControls("mnuAddNew").FindItem("AddNew").Enabled = DFClsFormProfiles.CanInsert()

        canSelectField = DFClsFormProfiles.CanSelectField("ddlProfile")
        Me.mForm.FindControls("ddlProfile").Visible = canSelectField
        Me.mForm.FindControls("lblProfile").Visible = canSelectField

        canSelectField = DFClsFormProfiles.CanSelectField("ddlForm")
        Me.mForm.FindControls("ddlForm").Visible = canSelectField
        Me.mForm.FindControls("lblForm").Visible = canSelectField

        Me.mForm.FindControls("ckbSelect").Visible = DFClsFormProfiles.CanSelectField("ckbSelect")
        Me.mForm.FindControls("ckbInsert").Visible = DFClsFormProfiles.CanSelectField("ckbInsert")
        Me.mForm.FindControls("ckbUpdate").Visible = DFClsFormProfiles.CanSelectField("ckbUpdate")
        Me.mForm.FindControls("ckbDelete").Visible = DFClsFormProfiles.CanSelectField("ckbDelete")
    End Sub

    Public Shared Sub ApplyGridRowAuthorizationRules(ByVal gridView As GridView, ByVal row As System.Web.UI.WebControls.GridViewRow)
        If row.RowType = DataControlRowType.DataRow Then
            Dim canSelectForm As Boolean = DFClsForms.CanSelect

            row.Cells(0).Controls(1).Visible = canSelectForm
            row.Cells(0).Controls(2).Visible = Not canSelectForm

            Dim canSelectProfile As Boolean = DFClsProfiles.CanSelect

            row.Cells(1).Controls(1).Visible = canSelectProfile
            row.Cells(1).Controls(2).Visible = Not canSelectProfile
        End If
    End Sub

    Public Sub ApplyAddFormAuthorizationRules(ByVal editMode As Boolean)
        Dim canSave As Boolean

        Me.mForm.FindControls("ddlProfile").Enabled = (Not editMode AndAlso DFClsFormProfiles.CanInsertField("ddlProfile"))
        Me.mForm.FindControls("ddlForm").Enabled = (Not editMode AndAlso DFClsFormProfiles.CanInsertField("ddlForm"))
        Me.mForm.FindControls("ckbSelect").Enabled = (Not editMode AndAlso DFClsFormProfiles.CanInsertField("ckbSelect")) OrElse (editMode AndAlso DFClsFormProfiles.CanUpdateField("ckbSelect"))
        Me.mForm.FindControls("ckbInsert").Enabled = (Not editMode AndAlso DFClsFormProfiles.CanInsertField("ckbInsert")) OrElse (editMode AndAlso DFClsFormProfiles.CanUpdateField("ckbInsert"))
        Me.mForm.FindControls("ckbUpdate").Enabled = (Not editMode AndAlso DFClsFormProfiles.CanInsertField("ckbUpdate")) OrElse (editMode AndAlso DFClsFormProfiles.CanUpdateField("ckbUpdate"))
        Me.mForm.FindControls("ckbDelete").Enabled = (Not editMode AndAlso DFClsFormProfiles.CanInsertField("ckbDelete")) OrElse (editMode AndAlso DFClsFormProfiles.CanUpdateField("ckbDelete"))

        Me.ApplyMenuItemAuthorizationRules(editMode)

        canSave = (Not editMode AndAlso DFClsFormProfiles.CanInsert) OrElse (editMode AndAlso DFClsFormProfiles.CanUpdate)
        Me.mForm.FindControls("mnuSave").FindItem("Ok").Enabled = canSave
        Me.mForm.FindControls("mnuSave").FindItem("Save").Enabled = canSave

        Me.ShowAddForm(DFClsFormProfiles.CanSelect OrElse DFClsFormProfiles.CanInsert OrElse DFClsFormProfiles.CanUpdate)
    End Sub

    Private Sub ApplyMenuItemAuthorizationRules(ByVal editMode As Boolean)
        Me.mForm.FindControls("mnuItem").FindItem("Form").Enabled = editMode AndAlso DFClsForms.CanSelect
        Me.mForm.FindControls("mnuItem").FindItem("Profile").Enabled = editMode AndAlso DFClsProfiles.CanSelect
        Me.mForm.FindControls("mnuItem").FindItem("Delete").Enabled = editMode AndAlso DFClsFormProfiles.CanDelete
    End Sub

    Public Function ValidateAssign(ByVal ControlName As String) As Boolean
        Return Not (ClsFormProfile.Exists(Me.mForm.FindControls("ddlProfile").SelectedValue, Me.mForm.FindControls("ddlForm").SelectedValue) AndAlso Not Me.mEditMode)
    End Function

    Private Sub LoadFilters(ByVal formID As String)
        Me.LoadFormFilter(formID)
    End Sub

    Private Sub LoadFormFilter(ByVal formID As String)
        Me.LoadCombo(Me.mForm.FindControls("ddlForms"), Me.GetComboDataSources(Me.GetFormInfoList, String.Empty), formID)
    End Sub

    Private Sub LoadProfileCombo(ByVal profileID As String)
        Me.LoadCombo(Me.mForm.FindControls("ddlProfile"), Me.GetComboDataSources(Me.GetProfileInfoList, "[Select a Profile]"), profileID)
        Me.mForm.FindControls("ddlProfile").Enabled = Me.mForm.FindControls("ddlForm").SelectedValue <> 0
    End Sub

    Private Sub LoadProfileComboItem(ByVal profileID As Long, ByVal description As String)
        Dim profileData As New ProfileData
        profileData.ID = profileID
        profileData.Description = description
        Me.LoadCombo(Me.mForm.FindControls("ddlProfile"), Me.GetComboDataSources(New ProfileData() {profileData}, "[Select a Profile]"), profileID)
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
        table.Columns.Add("Select", GetType(String))
        table.Columns.Add("Insert", GetType(String))
        table.Columns.Add("Update", GetType(String))
        table.Columns.Add("Delete", GetType(String))

        table.DefaultView.Sort = Me.GetSortProperty

        For Each profile As FormProfileData In Me.mDataList
            table.Rows.Add(New Object() {profile.ID, profile.Description, profile.Form.ID, profile.Form.Description, "~/Images/Checked_" & profile.PSelect.ToString & ".png", "~/Images/Checked_" & profile.PInsert.ToString & ".png", "~/Images/Checked_" & profile.PUpdate.ToString & ".png", "~/Images/Checked_" & profile.PDelete.ToString & ".png"})
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
        Me.mForm.Session("ID1") = Me.mData.ID
        Me.mForm.Response.Redirect("~/Forms/frmForms.aspx")
    End Sub

    Public Sub OnEditing(ByVal formID As Long, ByVal profileID As Long)
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

    Public Sub OnDeleting(ByVal formID As Long, ByVal profileID As Long)
        Me.ShowAddForm(False)
        Me.LoadProfileComboItem(profileID, String.Empty)
        Me.LoadFormComboItem(formID, String.Empty)
        Me.mForm.FindControls("MsgBox").ShowConfirmation("Are you sure you want to delete this Profile?\n\nNote: there is no undo.", "", True, False)
    End Sub

    Public Sub OnDeleting()
        Me.mForm.FindControls("MsgBox").ShowConfirmation("Are you sure you want to delete this Profile?\n\nNote: there is no undo.", "", True, False)
    End Sub

    Public Sub OnMsgBoxOk()
        If Me.DeleteData Then
            Me.ShowAddForm(False)
            Me.LoadFilters(Me.mForm.FindControls("ddlForms").SelectedValue)
            Me.mDataList = Me.GetDataList()
            Me.PopulateDataList("Form", "ASC", 0)
        End If
    End Sub

    Public Sub OnSelectedFormChanged()
        Me.ShowAddForm(False)
        Me.mDataList = Me.GetDataList()
        Me.PopulateDataList(0)
    End Sub

    Public Sub OnAddNew()
        Me.mEditMode = False
        Me.mData = New FormProfileData
        Me.ApplyAddFormAuthorizationRules(False)
        Me.ClearAddForm()
    End Sub

    Public Sub OnReturn()
        Me.mForm.Session("ID1") = Me.mForm.FindControls("ddlForms").SelectedValue
        Me.mForm.Response.Redirect("~/Forms/frmForms.aspx")
    End Sub

    Public Sub OnSelectForm()
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
            Me.LoadFilters(Me.mForm.FindControls("ddlForm").SelectedValue)
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
            Me.LoadFilters(Me.mForm.FindControls("ddlForm").SelectedValue)
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
        Me.LoadFormComboItem(Me.mData.Form.ID, Me.mData.Form.Description)
        Me.LoadProfileComboItem(Me.mData.ID, Me.mData.Description)
        Me.CheckedChecks(Me.mData.PSelect, Me.mData.PInsert, Me.mData.PUpdate, Me.mData.PDelete)
        Me.EnabledChecks(True, Me.mData.PSelect)
    End Sub

    Private Sub ClearAddForm()
        Me.LoadFormCombo(Me.mForm.FindControls("ddlForms").SelectedValue)
        Me.LoadProfileCombo(0)
        Me.CheckedChecks(False)
        Me.EnabledChecks(False, False)
    End Sub

    Private Function CollectData() As FormProfileNewData
        Dim formData As New FormProfileNewData

        formData.ID.SetValues("ddlProfile", True, Me.mData.ID, CLng(Me.mForm.FindControls("ddlProfile").SelectedItem.Value))
        formData.Description.SetValues("ddlProfile", False, Me.mData.Description, Me.mForm.FindControls("ddlProfile").SelectedItem.Text)

        formData.Form.ID.SetValues("ddlForm", True, Me.mData.Form.ID, CLng(Me.mForm.FindControls("ddlForm").SelectedValue))
        formData.Form.Description.SetValues("ddlForm", False, Me.mData.Form.Description, Me.mForm.FindControls("ddlForm").SelectedItem.Text)

        formData.PSelect.SetValues("ckbSelect", False, Me.mData.PSelect, Me.mForm.FindControls("ckbSelect").Checked)
        formData.PInsert.SetValues("ckbInsert", False, Me.mData.PInsert, Me.mForm.FindControls("ckbInsert").Checked)
        formData.PUpdate.SetValues("ckbUpdate", False, Me.mData.PUpdate, Me.mForm.FindControls("ckbUpdate").Checked)
        formData.PDelete.SetValues("ckbDelete", False, Me.mData.PDelete, Me.mForm.FindControls("ckbDelete").Checked)

        Return formData
    End Function

    Private Function CollectIDData() As FormProfileNewData
        Dim formData As New FormProfileNewData
        formData.ID.SetValues("ddlProfile", True, 0, CLng(Me.mForm.FindControls("ddlProfile").SelectedValue))
        formData.Form.ID.SetValues("ddlForm", True, 0, CLng(Me.mForm.FindControls("ddlForm").SelectedValue))
        Return formData
    End Function

    Private Sub CollectFilters()
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

    Private Function GetDataList() As FormProfileData()
        Try
            Me.CollectFilters()
            Return ClsSessionAdmin.GetFormProfileList(Me.mFormIDList)
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return New FormProfileData() {}
        End Try
    End Function

    Private Function GetData() As FormProfileData
        Try
            Return ClsSessionAdmin.GetFormProfile(Me.CollectIDData)
            Return Nothing
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return Nothing
        End Try
    End Function

    Private Function SaveData() As Boolean
        Try
            If Me.mEditMode Then
                Me.mData = ClsSessionAdmin.EditFormProfile(Me.CollectData)
            Else
                Me.mData = ClsSessionAdmin.AddFormProfile(Me.CollectData)
            End If
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error saving: " & SysEx.Message)
            Return False
        End Try
    End Function

    Private Function DeleteData() As Boolean
        Try
            ClsSessionAdmin.DeleteFormProfile(Me.CollectIDData)
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error deleting: " & SysEx.Message)
            Return False
        End Try
    End Function

#End Region

End Class
