Imports SCT.Library
Imports SCT.Library.AdminSite
Imports System.Web.UI.WebControls

Public Class DFClsGroups

#Region " Private Fields "

    Private mForm As Object
    Private mData As GroupData
    Private mDataList As GroupData()

    Private mPageIndex As Integer
    Private mSortDirection As String
    Private mSortExpression As String

    Private mFormIDList() As Long = New Long() {}

    Private mEditMode As Boolean

#End Region

#Region " Authorization Rules "

    Public Shared Function CanSelect() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmGroups") Then
            Return ClsSessionAdmin.CanSelectInForm("frmGroups")
        Else
            Return ClsSessionAdmin.CanSelectInForm("AllForms")
        End If
    End Function

    Public Shared Function CanInsert() As Boolean
        Return ClsSessionAdmin.CanInsertInForm("frmGroups")
    End Function

    Public Shared Function CanUpdate() As Boolean
        Return ClsSessionAdmin.CanUpdateInForm("frmGroups")
    End Function

    Public Shared Function CanDelete() As Boolean
        Return ClsSessionAdmin.CanDeleteInForm("frmGroups")
    End Function

    Public Shared Function CanSelectField(ByVal fieldName As String) As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmGroups") Then
            Return ClsSessionAdmin.CanSelectFieldInForm("frmGroups", fieldName)
        Else
            Return ClsSessionAdmin.CanSelectFieldInForm("AllForms", "AllFields")
        End If
    End Function

    Public Shared Function CanInsertField(ByVal fieldName As String) As Boolean
        Return ClsSessionAdmin.CanInsertFieldInForm("frmGroups", fieldName)
    End Function

    Public Shared Function CanUpdateField(ByVal fieldName As String) As Boolean
        Return ClsSessionAdmin.CanUpdateFieldInForm("frmGroups", fieldName)
    End Function

    Public Shared Function CanDeleteField(ByVal fieldName As String) As Boolean
        Return ClsSessionAdmin.CanDeleteFieldInForm("frmGroups", fieldName)
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

        Me.LoadFormFilter(Me.mForm.Session("ID1"))

        Me.mDataList = Me.GetDataList()
        Me.PopulateDataList("Description", "ASC", 0)

        If IsNumeric(Me.mForm.Session("ID2")) AndAlso Me.mForm.Session("ID2") <> 0 Then
            Me.OnEditing(Me.mForm.Session("ID2"))
        End If
    End Sub

    Public Sub ApplyPageAuthorizationRules()
        Dim canSelectMinCol As Boolean
        Dim canSelectCol As Boolean
        Dim canSelectField As Boolean

        If (Not ClsSessionAdmin.IsValidForm("frmGroups", "colGroupID", "colGroupDescription", "colGroupForm", "txtGroupID", "ddlGroupForm", "txtGroupDescription")) OrElse (Not DFClsGroups.CanSelect()) Then Me.mForm.Response.Redirect("~/Forms/frmMain.aspx")

        Me.mForm.FindControls("GridView").Columns(0).Visible = DFClsGroups.CanSelectField("colGroupID")
        Me.mForm.FindControls("GridView").Columns(1).Visible = DFClsGroups.CanSelectField("colGroupDescription")

        canSelectMinCol = Me.mForm.FindControls("GridView").Columns(0).Visible OrElse Me.mForm.FindControls("GridView").Columns(1).Visible

        canSelectCol = canSelectMinCol AndAlso DFClsGroups.CanSelectField("colGroupForm")
        Me.mForm.FindControls("GridView").Columns(2).Visible = canSelectCol
        Me.mForm.FindControls("lblForms").Visible = canSelectCol
        Me.mForm.FindControls("ddlForms").Visible = canSelectCol

        Me.mForm.FindControls("GridView").Columns(3).Visible = canSelectMinCol AndAlso DFClsGroupProfiles.CanSelect
        Me.mForm.FindControls("GridView").Columns(4).Visible = canSelectMinCol AndAlso DFClsFields.CanSelect
        Me.mForm.FindControls("GridView").Columns(5).Visible = canSelectMinCol AndAlso DFClsGroups.CanUpdate
        Me.mForm.FindControls("GridView").Columns(6).Visible = canSelectMinCol AndAlso DFClsGroups.CanDelete
        Me.mForm.FindControls("GridView").PagerSettings.Visible = canSelectMinCol

        Me.mForm.FindControls("mnuAddNew").FindItem("AddNew").Enabled = DFClsGroups.CanInsert()

        canSelectField = DFClsGroups.CanSelectField("txtGroupID")
        Me.mForm.FindControls("txtGroupID").Visible = canSelectField
        Me.mForm.FindControls("lblGroupID").Visible = canSelectField

        canSelectField = DFClsGroups.CanSelectField("txtGroupDescription")
        Me.mForm.FindControls("txtGroupDescription").Visible = canSelectField
        Me.mForm.FindControls("lblGroupDescription").Visible = canSelectField

        canSelectField = DFClsGroups.CanSelectField("ddlGroupForm")
        Me.mForm.FindControls("ddlGroupForm").Visible = canSelectField
        Me.mForm.FindControls("lblGroupForm").Visible = canSelectField
    End Sub

    Public Shared Sub ApplyGridRowAuthorizationRules(ByVal gridView As GridView, ByVal row As System.Web.UI.WebControls.GridViewRow)
        If row.RowType = DataControlRowType.DataRow Then
            Dim canSelect As Boolean = DFClsGroups.CanSelect

            row.Cells(0).Controls(1).Visible = canSelect
            row.Cells(0).Controls(2).Visible = Not canSelect

            Dim canSelectForm As Boolean = DFClsForms.CanSelect

            row.Cells(2).Controls(1).Visible = canSelectForm
            row.Cells(2).Controls(2).Visible = Not canSelectForm
        End If
    End Sub

    Public Sub ApplyAddFormAuthorizationRules(ByVal editMode As Boolean)
        Dim canSave As Boolean

        Me.mForm.FindControls("ddlGroupForm").Enabled = (Not editMode AndAlso DFClsGroups.CanInsertField("ddlGroupForm")) OrElse (editMode AndAlso DFClsGroups.CanUpdateField("ddlGroupForm"))
        Me.mForm.FindControls("txtGroupDescription").Enabled = (Not editMode AndAlso DFClsGroups.CanInsertField("txtGroupDescription")) OrElse (editMode AndAlso DFClsGroups.CanUpdateField("txtGroupDescription"))

        Me.ApplyMenuItemAuthorizationRules(editMode)

        canSave = (Not editMode AndAlso DFClsGroups.CanInsert) OrElse (editMode AndAlso DFClsGroups.CanUpdate)
        Me.mForm.FindControls("mnuSave").FindItem("Ok").Enabled = canSave
        Me.mForm.FindControls("mnuSave").FindItem("Save").Enabled = canSave

        Me.ShowAddForm(DFClsGroups.CanSelect OrElse DFClsGroups.CanInsert OrElse DFClsGroups.CanUpdate)
    End Sub

    Private Sub ApplyMenuItemAuthorizationRules(ByVal editMode As Boolean)
        Me.mForm.FindControls("mnuItem").FindItem("Permissions").Enabled = editMode AndAlso DFClsGroupProfiles.CanSelect
        Me.mForm.FindControls("mnuItem").FindItem("Fields").Enabled = editMode AndAlso DFClsFields.CanSelect
        Me.mForm.FindControls("mnuItem").FindItem("Delete").Enabled = editMode AndAlso DFClsGroups.CanDelete
    End Sub

    Public Function ValidateExistsDescription(ByVal ControlName As String) As Boolean
        Return Not ClsGroup.Exists(Me.mData.ID, Me.mForm.FindControls("ddlGroupForm").SelectedValue, Me.mForm.FindControls(ControlName).Text)
    End Function

    Public Sub LoadFormFilter(ByVal formID As Long)
        Me.LoadCombo(Me.mForm.FindControls("ddlForms"), Me.GetFormDataSources(Me.GetFormInfoList, String.Empty), formID)
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

    Private Function GetFormDataSources(ByVal formInfoList As FormData(), ByVal FirstItemText As String) As DataTable
        Dim table As New DataTable

        table.Columns.Add("Description", GetType(String))
        table.Columns.Add("ID", GetType(Long))

        table.DefaultView.Sort = "Description"

        table.Rows.Add(New String() {FirstItemText, 0})
        For Each formInfo As FormData In formInfoList
            table.Rows.Add(New Object() {formInfo.Description, formInfo.ID})
        Next
        Return table
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
        table.Columns.Add("IDForm", GetType(Long))
        table.Columns.Add("Form", GetType(String))
        table.Columns.Add("Description", GetType(String))

        table.DefaultView.Sort = Me.GetSortProperty

        For Each group As GroupData In Me.mDataList
            table.Rows.Add(New Object() {group.ID, group.Form.ID, group.Form.Description, group.Description})
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
                Return sortProperty & ", Form " & Me.mSortDirection
            Case "Form"
                Return sortProperty & ", Description " & Me.mSortDirection
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
        Me.mForm.Session("ID1") = Me.mData.Form.ID.ToString
        Me.mForm.Response.Redirect("~/Forms/frmForms.aspx")
    End Sub

    Public Sub OnPermissions(ByVal groupID As Long, ByVal formID As Long)
        Me.mForm.Session("ID2") = groupID
        Me.mForm.Session("ID1") = formID
        Me.mForm.Response.Redirect("~/Forms/frmGroupProfiles.aspx")
    End Sub

    Public Sub OnPermissions()
        Me.mForm.Session("ID2") = Me.mData.ID
        Me.mForm.Session("ID1") = Me.mData.Form.ID
        Me.mForm.Response.Redirect("~/Forms/frmGroupProfiles.aspx")
    End Sub

    Public Sub OnFields(ByVal groupID As Long, ByVal formID As Long)
        Me.mForm.Session("ID2") = groupID
        Me.mForm.Session("ID1") = formID
        Me.mForm.Response.Redirect("~/Forms/frmFields.aspx")
    End Sub

    Public Sub OnFields()
        Me.mForm.Session("ID2") = Me.mData.ID
        Me.mForm.Session("ID1") = Me.mData.Form.ID
        Me.mForm.Response.Redirect("~/Forms/frmFields.aspx")
    End Sub

    Public Sub OnEditing(ByVal groupID As Long)
        Me.mEditMode = True
        Me.mForm.FindControls("txtGroupID").Text = groupID
        Me.mData = Me.GetData()
        If Me.mData IsNot Nothing Then
            Me.ApplyAddFormAuthorizationRules(True)
            Me.PopulateData()
        End If
    End Sub

    Public Sub OnDeleting(ByVal groupID As Long)
        Me.ShowAddForm(False)
        Me.mForm.FindControls("txtGroupID").Text = groupID
        Me.mForm.FindControls("MsgBox").ShowConfirmation("Are you sure you want to delete this Form?\n\nNote: there is no undo.", "", True, False)
    End Sub

    Public Sub OnDeleting()
        Me.mForm.FindControls("MsgBox").ShowConfirmation("Are you sure you want to delete this Group?\n\nNote: there is no undo.", "", True, False)
    End Sub

    Public Sub OnMsgBoxOk()
        If Me.DeleteData Then
            Me.ShowAddForm(False)
            Me.LoadFormFilter(Me.mForm.FindControls("ddlForms").SelectedValue)
            Me.mDataList = Me.GetDataList()
            Me.PopulateDataList("Description", "ASC", 0)
        End If
    End Sub

    Public Sub OnSelectedFormChanged()
        Me.ShowAddForm(False)
        Me.mDataList = Me.GetDataList()
        Me.PopulateDataList(0)
    End Sub

    Public Sub OnAddNew()
        Me.mEditMode = False
        Me.mData = New GroupData
        Me.ApplyAddFormAuthorizationRules(False)
        Me.ClearAddForm()
    End Sub

    Public Sub OnReturn()
        Me.mForm.Session("ID1") = Me.mForm.FindControls("ddlForms").SelectedValue
        Me.mForm.Response.Redirect("~/Forms/frmForms.aspx")
    End Sub

    Public Sub OnSave()
        If Me.SaveData Then
            Me.LoadFormFilter(Me.mData.Form.ID)
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
            Me.LoadFormFilter(Me.mData.Form.ID)
            Me.mDataList = Me.GetDataList()
            Me.PopulateDataList("Description", "ASC", 0)
        End If
    End Sub

    Public Sub OnCancel()
        Me.ShowAddForm(False)
    End Sub

#End Region

#Region " Data Methods "

    Private Sub PopulateDataList(ByVal Sortexpression As String, ByVal SortDirection As String, ByVal PageIndex As Integer)
        Me.mPageIndex = PageIndex
        Me.mSortDirection = SortDirection
        Me.mSortExpression = Sortexpression
        Me.LoadGridView(Me.mForm.FindControls("GridView"))
    End Sub

    Private Sub PopulateDataList(ByVal PageIndex As Integer)
        Me.mPageIndex = PageIndex
        Me.LoadGridView(Me.mForm.FindControls("GridView"))
    End Sub

    Private Sub PopulateData()
        Me.mForm.FindControls("txtGroupID").Text = Me.mData.ID
        Me.LoadCombo(Me.mForm.FindControls("ddlGroupForm"), Me.GetFormDataSources(Me.GetFormInfoList, "[Select a Form]"), Me.mData.Form.ID.ToString)
        Me.mForm.FindControls("txtGroupDescription").Text = Me.mData.Description
        Me.mForm.FindControls("txtGroupDescription").Focus()
    End Sub

    Private Sub ClearAddForm()
        Me.mForm.FindControls("txtGroupID").Text = String.Empty
        Me.LoadCombo(Me.mForm.FindControls("ddlGroupForm"), Me.GetFormDataSources(Me.GetFormInfoList, "[Select a Form]"), Me.mForm.FindControls("ddlForms").SelectedValue)
        Me.mForm.FindControls("txtGroupDescription").Text = String.Empty
        Me.mForm.FindControls("txtGroupDescription").Focus()
    End Sub

    Private Function GetDataID(ByVal textBox As TextBox) As String
        If textBox.Text = String.Empty Then
            Return 0
        End If
        Return textBox.Text
    End Function

    Private Function CollectData() As GroupNewData
        Dim formData As New GroupNewData
        formData.ID.SetValues("txtGroupID", True, Me.mData.ID, CLng(Me.GetDataID(Me.mForm.FindControls("txtGroupID"))))
        formData.Form.ID.SetValues("ddlGroupForm", False, Me.mData.Form.ID, CLng(Me.mForm.FindControls("ddlGroupForm").SelectedItem.Value))
        formData.Form.Description.SetValues("ddlGroupForm", False, Me.mData.Form.Description, Me.mForm.FindControls("ddlGroupForm").SelectedItem.Text)
        formData.Description.SetValues("txtGroupDescription", False, Me.mData.Description, Me.mForm.FindControls("txtGroupDescription").Text)
        Return formData
    End Function

    Private Function CollectDataID() As GroupNewData
        Dim formData As New GroupNewData
        formData.ID.SetValues("txtGroupID", True, 0, CLng(Me.GetDataID(Me.mForm.FindControls("txtGroupID"))))
        Return formData
    End Function

    Private Sub CollectFilters()
        Me.mFormIDList = Me.CollectIDSelected(Me.mForm.FindControls("ddlForms"))
    End Sub

    Private Function CollectIDSelected(ByVal list As DropDownList) As Long()
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

    Private Function GetDataList() As GroupData()
        Try
            Me.CollectFilters()
            Return ClsSessionAdmin.GetGroupList(Me.mFormIDList)
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return New GroupData() {}
        End Try
    End Function

    Private Function GetData() As GroupData
        Try
            Return ClsSessionAdmin.GetGroup(Me.CollectDataID)
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return Nothing
        End Try
    End Function

    Private Function SaveData() As Boolean
        Try
            If Me.mEditMode Then
                Me.mData = ClsSessionAdmin.EditGroup(Me.CollectData)
            Else
                Me.mData = ClsSessionAdmin.AddGroup(Me.CollectData)
            End If
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error saving: " & SysEx.Message)
            Return False
        End Try
    End Function

    Private Function DeleteData() As Boolean
        Try
            ClsSessionAdmin.DeleteGroup(Me.CollectDataID)
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error deleting: " & SysEx.Message)
            Return False
        End Try
    End Function

#End Region

End Class
