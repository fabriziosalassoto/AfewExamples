Imports SCT.Library
Imports SCT.Library.AdminSite
Imports System.Web.UI.WebControls

Public Class DFClsFields

#Region " Private Fields "

    Private mForm As Object
    Private mData As FieldData
    Private mDataList As FieldData()

    Private mPageIndex As Integer
    Private mSortDirection As String
    Private mSortExpression As String

    Private mFormIDList() As Long = New Long() {}
    Private mGroupIDList() As Long = New Long() {}

    Private mEditMode As Boolean

#End Region

#Region " Authorization Rules "

    Public Shared Function CanSelect() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmFields") Then
            Return ClsSessionAdmin.CanSelectInForm("frmFields")
        Else
            Return ClsSessionAdmin.CanSelectInForm("AllForms")
        End If
    End Function

    Public Shared Function CanInsert() As Boolean
        Return ClsSessionAdmin.CanInsertInForm("frmFields")
    End Function

    Public Shared Function CanUpdate() As Boolean
        Return ClsSessionAdmin.CanUpdateInForm("frmFields")
    End Function

    Public Shared Function CanDelete() As Boolean
        Return ClsSessionAdmin.CanDeleteInForm("frmFields")
    End Function

    Public Shared Function CanSelectField(ByVal fieldName As String) As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmFields") Then
            Return ClsSessionAdmin.CanSelectFieldInForm("frmFields", fieldName)
        Else
            Return ClsSessionAdmin.CanSelectFieldInForm("AllForms", "AllFields")
        End If
    End Function

    Public Shared Function CanInsertField(ByVal fieldName As String) As Boolean
        Return ClsSessionAdmin.CanInsertFieldInForm("frmFields", fieldName)
    End Function

    Public Shared Function CanUpdateField(ByVal fieldName As String) As Boolean
        Return ClsSessionAdmin.CanUpdateFieldInForm("frmFields", fieldName)
    End Function

    Public Shared Function CanDeleteField(ByVal fieldName As String) As Boolean
        Return ClsSessionAdmin.CanDeleteFieldInForm("frmFields", fieldName)
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
        Me.mForm.Session("ValuePath") = "frmSecurity/frmFields"

        Me.ApplyPageAuthorizationRules()

        Me.LoadFilters(Me.mForm.Session("ID1"), Me.mForm.Session("ID2"))

        Me.mDataList = Me.GetDataList()
        Me.PopulateDataList("Description", "ASC", 0)
    End Sub

    Public Sub ApplyPageAuthorizationRules()
        Dim canSelectMinCol As Boolean
        Dim canSelectCol As Boolean
        Dim canSelectField As Boolean

        If (Not ClsSessionAdmin.IsValidForm("frmFields", "colFieldID", "colFieldDescription", "colFieldForm", "colFieldGroup", "txtFieldID", "ddlFieldForm", "ddlFieldGroup", "txtFieldDescription")) OrElse (Not DFClsFields.CanSelect()) Then Me.mForm.Response.Redirect("~/Forms/frmMain.aspx")

        Me.mForm.FindControls("GridView").Columns(0).Visible = DFClsFields.CanSelectField("colFieldID")
        Me.mForm.FindControls("GridView").Columns(1).Visible = DFClsFields.CanSelectField("colFieldDescription")

        canSelectMinCol = Me.mForm.FindControls("GridView").Columns(0).Visible OrElse Me.mForm.FindControls("GridView").Columns(1).Visible

        canSelectCol = canSelectMinCol AndAlso DFClsFields.CanSelectField("colFieldForm")
        Me.mForm.FindControls("GridView").Columns(2).Visible = canSelectCol
        Me.mForm.FindControls("lblForms").Visible = canSelectCol
        Me.mForm.FindControls("ddlForms").Visible = canSelectCol

        canSelectCol = canSelectMinCol AndAlso DFClsFields.CanSelectField("colFieldGroup")
        Me.mForm.FindControls("GridView").Columns(3).Visible = canSelectCol
        Me.mForm.FindControls("lblGroups").Visible = canSelectCol
        Me.mForm.FindControls("ddlGroups").Visible = canSelectCol

        Me.mForm.FindControls("GridView").Columns(4).Visible = canSelectMinCol AndAlso DFClsFields.CanUpdate
        Me.mForm.FindControls("GridView").Columns(5).Visible = canSelectMinCol AndAlso DFClsFields.CanDelete
        Me.mForm.FindControls("GridView").PagerSettings.Visible = canSelectMinCol

        Me.mForm.FindControls("mnuAddNew").FindItem("AddNew").Enabled = DFClsFields.CanInsert()

        canSelectField = DFClsFields.CanSelectField("txtFieldID")
        Me.mForm.FindControls("txtFieldID").Visible = canSelectField
        Me.mForm.FindControls("lblFieldID").Visible = canSelectField

        canSelectField = DFClsFields.CanSelectField("txtFieldDescription")
        Me.mForm.FindControls("txtFieldDescription").Visible = canSelectField
        Me.mForm.FindControls("lblFieldDescription").Visible = canSelectField

        canSelectField = DFClsFields.CanSelectField("ddlFieldForm")
        Me.mForm.FindControls("ddlFieldForm").Visible = canSelectField
        Me.mForm.FindControls("lblFieldForm").Visible = canSelectField

        canSelectField = DFClsFields.CanSelectField("ddlFieldGroup")
        Me.mForm.FindControls("ddlFieldGroup").Visible = canSelectField
        Me.mForm.FindControls("lblFieldGroup").Visible = canSelectField
    End Sub

    Public Shared Sub ApplyGridRowAuthorizationRules(ByVal gridView As GridView, ByVal row As System.Web.UI.WebControls.GridViewRow)
        If row.RowType = DataControlRowType.DataRow Then
            Dim canSelect As Boolean = DFClsFields.CanSelect

            row.Cells(0).Controls(1).Visible = canSelect
            row.Cells(0).Controls(2).Visible = Not canSelect

            Dim canSelectForm As Boolean = DFClsForms.CanSelect

            row.Cells(2).Controls(1).Visible = canSelectForm
            row.Cells(2).Controls(2).Visible = Not canSelectForm

            Dim canSelectGroup As Boolean = DFClsGroups.CanSelect

            row.Cells(3).Controls(1).Visible = canSelectGroup
            row.Cells(3).Controls(2).Visible = Not canSelectGroup
        End If
    End Sub

    Public Sub ApplyAddFormAuthorizationRules(ByVal editMode As Boolean)
        Dim canSave As Boolean

        Me.mForm.FindControls("ddlFieldForm").Enabled = (Not editMode AndAlso DFClsFields.CanInsertField("ddlFieldForm")) OrElse (editMode AndAlso DFClsFields.CanUpdateField("ddlFieldForm"))
        Me.mForm.FindControls("ddlFieldGroup").Enabled = (Not editMode AndAlso DFClsFields.CanInsertField("ddlFieldGroup")) OrElse (editMode AndAlso DFClsFields.CanUpdateField("ddlFieldGroup"))
        Me.mForm.FindControls("txtFieldDescription").Enabled = (Not editMode AndAlso DFClsFields.CanInsertField("txtFieldDescription")) OrElse (editMode AndAlso DFClsFields.CanUpdateField("txtFieldDescription"))

        Me.ApplyMenuItemAuthorizationRules(editMode)

        canSave = (Not editMode AndAlso DFClsFields.CanInsert) OrElse (editMode AndAlso DFClsFields.CanUpdate)
        Me.mForm.FindControls("mnuSave").FindItem("Ok").Enabled = canSave
        Me.mForm.FindControls("mnuSave").FindItem("Save").Enabled = canSave

        Me.ShowAddForm(DFClsFields.CanSelect OrElse DFClsFields.CanInsert OrElse DFClsFields.CanUpdate)
    End Sub

    Private Sub ApplyMenuItemAuthorizationRules(ByVal editMode As Boolean)
        Me.mForm.FindControls("mnuItem").FindItem("Form").Enabled = editMode AndAlso DFClsForms.CanSelect
        Me.mForm.FindControls("mnuItem").FindItem("Group").Enabled = editMode AndAlso DFClsGroups.CanSelect
        Me.mForm.FindControls("mnuItem").FindItem("Delete").Enabled = editMode AndAlso DFClsFields.CanDelete
    End Sub

    Public Function ValidateExistsDescriptionInForm(ByVal ControlName As String) As Boolean
        Return Not ClsField.ExistsInForm(Me.mData.ID, Me.mForm.FindControls("ddlFieldForm").SelectedValue, Me.mForm.FindControls(ControlName).Text)
    End Function

    Public Function ValidateExistsDescriptionInGroup(ByVal ControlName As String) As Boolean
        Return Not ClsField.ExistsInGroup(Me.mData.ID, Me.mForm.FindControls("ddlFieldGroup").SelectedValue, Me.mForm.FindControls(ControlName).Text)
    End Function

    Private Sub LoadFilters(ByVal formID As String, ByVal groupID As String)
        Me.LoadFormFilter(formID)
        Me.LoadGroupFilter(Me.mForm.FindControls("ddlForms").SelectedValue, groupID)
    End Sub

    Private Sub LoadFormFilter(ByVal formID As String)
        Me.LoadCombo(Me.mForm.FindControls("ddlForms"), Me.GetComboDataSources(Me.GetFormInfoList, String.Empty), formID)
    End Sub

    Private Sub LoadGroupFilter(ByVal formID As String, ByVal groupID As String)
        Me.LoadCombo(Me.mForm.FindControls("ddlGroups"), Me.GetComboDataSources(Me.GetGroupInfoList(formID), String.Empty), groupID)
        Me.mForm.FindControls("ddlGroups").Enabled = Me.mForm.FindControls("ddlForms").SelectedValue <> 0
    End Sub

    Private Sub LoadFormCombo(ByVal formID As String)
        Me.LoadCombo(Me.mForm.FindControls("ddlFieldForm"), Me.GetComboDataSources(Me.GetFormInfoList, "[Select a Form]"), formID)
    End Sub

    Private Sub LoadGroupCombo(ByVal formID As String, ByVal groupID As String)
        Me.LoadCombo(Me.mForm.FindControls("ddlFieldGroup"), Me.GetComboDataSources(Me.GetGroupInfoList(formID), "[Select a Group]"), groupID)
        Me.mForm.FindControls("ddlFieldGroup").Enabled = Me.mForm.FindControls("ddlFieldForm").SelectedValue <> 0
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
        table.Columns.Add("ID", GetType(Long))

        table.DefaultView.Sort = "Description"

        table.Rows.Add(New String() {FirstItemText, 0})
        For Each Info As Object In InfoList
            table.Rows.Add(New Object() {Info.Description, Info.ID})
        Next
        Return table
    End Function

    Private Sub LoadGridView(ByVal gridview As GridView)
        Me.AddRowData(gridview)
        Me.AddSortImage(gridview)
    End Sub

    Private Sub AddRowData(ByVal GridView As System.Web.UI.WebControls.GridView)
        GridView.PageIndex = Me.mPageIndex
        GridView.DataSource = Me.GetGridDataSources
        GridView.DataBind()
    End Sub

    Private Function GetGridDataSources() As DataTable
        Dim table As New DataTable

        table.Columns.Add("ID", GetType(Long))
        table.Columns.Add("IDForm", GetType(Long))
        table.Columns.Add("Form", GetType(String))
        table.Columns.Add("IDGroup", GetType(Long))
        table.Columns.Add("Group", GetType(String))
        table.Columns.Add("Description", GetType(String))

        table.DefaultView.Sort = Me.GetSortProperty

        For Each field As FieldData In Me.mDataList
            table.Rows.Add(New Object() {field.ID, field.Group.Form.ID, field.Group.Form.Description, field.Group.ID, field.Group.Description, field.Description})
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
            Case "Description"
                Return sortProperty & ", Form " & Me.mSortDirection & ", Group " & Me.mSortDirection
            Case "Form"
                Return sortProperty & ", Group " & Me.mSortDirection & ", Description " & Me.mSortDirection
            Case "Group"
                Return sortProperty & ", Form " & Me.mSortDirection & ", Description " & Me.mSortDirection
            Case Else
                Return sortProperty
        End Select
    End Function

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

    Public Sub OnForm(ByVal formID As Long)
        Me.mForm.Session("ID1") = formID
        Me.mForm.Response.Redirect("~/Forms/frmForms.aspx")
    End Sub

    Public Sub OnForm()
        Me.mForm.Session("ID1") = Me.mData.Group.Form.ID
        Me.mForm.Response.Redirect("~/Forms/frmForms.aspx")
    End Sub

    Public Sub OnGroup(ByVal groupID As Long, ByVal formID As Long)
        Me.mForm.Session("ID2") = groupID
        Me.mForm.Session("ID1") = formID
        Me.mForm.Response.Redirect("~/Forms/frmGroups.aspx")
    End Sub

    Public Sub OnGroup()
        Me.mForm.Session("ID2") = Me.mData.Group.ID
        Me.mForm.Session("ID1") = Me.mData.Group.Form.ID
        Me.mForm.Response.Redirect("~/Forms/frmGroups.aspx")
    End Sub

    Public Sub OnEditing(ByVal fieldID As Long)
        Me.mEditMode = True
        Me.mForm.FindControls("txtFieldID").Text = fieldID
        Me.mData = Me.GetData()
        If Me.mData IsNot Nothing Then
            Me.ApplyAddFormAuthorizationRules(True)
            Me.PopulateData()
        End If
    End Sub

    Public Sub OnDeleting(ByVal fieldID As Long)
        Me.ShowAddForm(False)
        Me.mForm.FindControls("txtFieldID").Text = fieldID
        Me.mForm.FindControls("MsgBox").ShowConfirmation("Are you sure you want to delete this Form?\n\nNote: there is no undo.", "", True, False)
    End Sub

    Public Sub OnDeleting()
        Me.mForm.FindControls("MsgBox").ShowConfirmation("Are you sure you want to delete this Field?\n\nNote: there is no undo.", "", True, False)
    End Sub

    Public Sub OnMsgBoxOk()
        If Me.DeleteData Then
            Me.ShowAddForm(False)
            Me.LoadFilters(Me.mForm.FindControls("ddlForms").SelectedValue, Me.mForm.FindControls("ddlGroups").SelectedValue)
            Me.mDataList = Me.GetDataList()
            Me.PopulateDataList("Description", "ASC", 0)
        End If
    End Sub

    Public Sub OnSelectedFormsChanged()
        Me.ShowAddForm(False)
        Me.LoadGroupFilter(Me.mForm.FindControls("ddlForms").SelectedValue, 0)
        Me.mDataList = Me.GetDataList()
        Me.PopulateDataList(0)
    End Sub

    Public Sub OnSelectedGroupsChanged()
        Me.ShowAddForm(False)
        Me.mDataList = Me.GetDataList()
        Me.PopulateDataList(0)
    End Sub

    Public Sub OnSelectedFieldFormChanged()
        Me.LoadGroupCombo(Me.mForm.FindControls("ddlFieldForm").SelectedValue, 0)
    End Sub

    Public Sub OnAddNew()
        Me.mEditMode = False
        Me.mData = New FieldData
        Me.ApplyAddFormAuthorizationRules(False)
        Me.ClearAddForm()
    End Sub

    Public Sub OnReturn()
        Me.mForm.Session("ID1") = Me.mForm.FindControls("ddlForms").SelectedValue
        Me.mForm.Session("ID2") = Me.mForm.FindControls("ddlGroups").SelectedValue
        Me.mForm.Response.Redirect("~/Forms/frmGroups.aspx")
    End Sub

    Public Sub OnSave()
        If Me.SaveData Then
            Me.LoadFilters(Me.mData.Group.Form.ID, Me.mData.Group.ID)
            Me.mDataList = Me.GetDataList()
            Me.PopulateDataList("Description", "ASC", 0)

            Me.mEditMode = True
            Me.ApplyMenuItemAuthorizationRules(True)
            Me.PopulateData()
        End If
    End Sub

    Public Sub OnOk()
        If Me.SaveData Then
            Me.ShowAddForm(False)

            Me.LoadFilters(Me.mData.Group.Form.ID, Me.mData.Group.ID)
            Me.mDataList = Me.GetDataList()
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
        Me.mForm.FindControls("txtFieldID").Text = Me.mData.ID
        Me.LoadFormCombo(Me.mData.Group.Form.ID.ToString)
        Me.LoadGroupCombo(Me.mForm.FindControls("ddlFieldForm").SelectedValue, Me.mData.Group.ID.ToString)
        Me.mForm.FindControls("txtFieldDescription").Text = Me.mData.Description
        Me.mForm.FindControls("txtFieldDescription").Focus()
    End Sub

    Private Sub ClearAddForm()
        Me.mForm.FindControls("txtFieldID").Text = String.Empty
        Me.LoadFormCombo(Me.mForm.FindControls("ddlForms").SelectedValue)
        Me.LoadGroupCombo(Me.mForm.FindControls("ddlForms").SelectedValue, Me.mForm.FindControls("ddlGroups").SelectedValue)
        Me.mForm.FindControls("txtFieldDescription").Text = String.Empty
        Me.mForm.FindControls("txtFieldDescription").Focus()
    End Sub

    Private Function GetDataID(ByVal textBox As TextBox) As String
        If textBox.Text = String.Empty Then
            Return 0
        End If
        Return textBox.Text
    End Function

    Private Function CollectData() As FieldNewData
        Dim formData As New FieldNewData
        formData.ID.SetValues("txtFieldID", True, Me.mData.ID, CLng(Me.GetDataID(Me.mForm.FindControls("txtFieldID"))))

        formData.Group.ID.SetValues("ddlFieldGroup", False, Me.mData.Group.ID, CLng(Me.mForm.FindControls("ddlFieldGroup").SelectedItem.Value))
        formData.Group.Description.SetValues("ddlFieldGroup", False, Me.mData.Group.Description, Me.mForm.FindControls("ddlFieldGroup").SelectedItem.Text)

        formData.Group.Form.ID.SetValues("ddlFieldForm", False, Me.mData.Group.Form.ID, CLng(Me.mForm.FindControls("ddlFieldForm").SelectedItem.Value))
        formData.Group.Form.Description.SetValues("ddlFieldForm", False, Me.mData.Group.Form.Description, Me.mForm.FindControls("ddlFieldForm").SelectedItem.Text)

        formData.Description.SetValues("txtFieldDescription", False, Me.mData.Description, Me.mForm.FindControls("txtFieldDescription").Text)
        Return formData
    End Function

    Private Function CollectIDData() As FieldNewData
        Dim formData As New FieldNewData
        formData.ID.SetValues("txtFieldID", True, 0, CLng(Me.GetDataID(Me.mForm.FindControls("txtFieldID"))))
        Return formData
    End Function

    Private Sub CollectFilters()
        Me.mFormIDList = Me.CollectIDSelectedList(Me.mForm.FindControls("ddlForms"))
        Me.mGroupIDList = Me.CollectIDSelectedList(Me.mForm.FindControls("ddlGroups"))
    End Sub

    Private Function CollectIDSelectedList(ByVal list As DropDownList) As Long()
        If list.SelectedValue = 0 Then
            Return New Long() {}
        End If
        Return New Long() {list.SelectedValue}
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

    Private Function GetDataList() As FieldData()
        Try
            Me.CollectFilters()
            Return ClsSessionAdmin.GetFieldList(Me.mFormIDList, Me.mGroupIDList)
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return New FieldData() {}
        End Try
    End Function

    Private Function GetData() As FieldData
        Try
            Return ClsSessionAdmin.GetField(Me.CollectIDData)
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return Nothing
        End Try
    End Function

    Private Function SaveData() As Boolean
        Try
            If Me.mEditMode Then
                Me.mData = ClsSessionAdmin.EditField(Me.CollectData)
            Else
                Me.mData = ClsSessionAdmin.AddField(Me.CollectData)
            End If
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error saving: " & SysEx.Message)
            Return False
        End Try
    End Function

    Private Function DeleteData() As Boolean
        Try
            ClsSessionAdmin.DeleteField(Me.CollectIDData)
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error deleting: " & SysEx.Message)
            Return False
        End Try
    End Function

#End Region

End Class
