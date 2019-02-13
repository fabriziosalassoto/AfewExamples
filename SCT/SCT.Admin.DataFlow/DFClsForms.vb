Imports SCT.Library
Imports SCT.Library.AdminSite
Imports System.Web.UI.WebControls

Public Class DFClsForms

#Region " Private Fields "

    Private mForm As Object
    Private mData As FormData
    Private mDataList As FormData()

    Private mPageIndex As Integer
    Private mSortDirection As String
    Private mSortExpression As String

    Private mLogsDescription() As String = New String() {}

    Private mEditMode As Boolean

#End Region

#Region " Authorization Rules "

    Public Shared Function CanSelect() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmForms") Then
            Return ClsSessionAdmin.CanSelectInForm("frmForms")
        Else
            Return ClsSessionAdmin.CanSelectInForm("AllForms")
        End If
    End Function

    Public Shared Function CanInsert() As Boolean
        Return ClsSessionAdmin.CanInsertInForm("frmForms")
    End Function

    Public Shared Function CanUpdate() As Boolean
        Return ClsSessionAdmin.CanUpdateInForm("frmForms")
    End Function

    Public Shared Function CanDelete() As Boolean
        Return ClsSessionAdmin.CanDeleteInForm("frmForms")
    End Function

    Public Shared Function CanSelectField(ByVal fieldName As String) As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmForms") Then
            Return ClsSessionAdmin.CanSelectFieldInForm("frmForms", fieldName)
        Else
            Return ClsSessionAdmin.CanSelectFieldInForm("AllForms", "AllFields")
        End If
    End Function

    Public Shared Function CanInsertField(ByVal fieldName As String) As Boolean
        Return ClsSessionAdmin.CanInsertFieldInForm("frmForms", fieldName)
    End Function

    Public Shared Function CanUpdateField(ByVal fieldName As String) As Boolean
        Return ClsSessionAdmin.CanUpdateFieldInForm("frmForms", fieldName)
    End Function

    Public Shared Function CanDeleteField(ByVal fieldName As String) As Boolean
        Return ClsSessionAdmin.CanDeleteFieldInForm("frmForms", fieldName)
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

        Me.LoadLogFilter(String.Empty)

        Me.mDataList = Me.GetDataList()
        Me.PopulateDataList("Description", "ASC", 0)

        If IsNumeric(Me.mForm.Session("ID1")) AndAlso Me.mForm.Session("ID1") <> 0 Then
            Me.OnEditing(Me.mForm.Session("ID1"))
        End If
    End Sub

    Public Sub ApplyPageAuthorizationRules()
        Dim canSelectMinCol As Boolean
        Dim canSelectLogCol As Boolean
        Dim canSelectField As Boolean

        If (Not ClsSessionAdmin.IsValidForm("frmForms", "colFormID", "colFormDescription", "colFormLog", "txtFormID", "txtFormDescription", "ddlFormLog")) OrElse (Not DFClsForms.CanSelect()) Then Me.mForm.Response.Redirect("~/Forms/frmMain.aspx")

        Me.mForm.FindControls("GridView").Columns(0).Visible = DFClsForms.CanSelectField("colFormID")
        Me.mForm.FindControls("GridView").Columns(1).Visible = DFClsForms.CanSelectField("colFormDescription")

        canSelectMinCol = Me.mForm.FindControls("GridView").Columns(0).Visible OrElse Me.mForm.FindControls("GridView").Columns(1).Visible

        canSelectLogCol = canSelectMinCol AndAlso DFClsForms.CanSelectField("colFormLog")
        Me.mForm.FindControls("GridView").Columns(2).Visible = canSelectLogCol
        Me.mForm.FindControls("lblLogs").Visible = canSelectLogCol
        Me.mForm.FindControls("ddlLogs").Visible = canSelectLogCol

        Me.mForm.FindControls("GridView").Columns(3).Visible = canSelectMinCol AndAlso DFClsFormProfiles.CanSelect
        Me.mForm.FindControls("GridView").Columns(4).Visible = canSelectMinCol AndAlso DFClsGroups.CanSelect
        Me.mForm.FindControls("GridView").Columns(5).Visible = canSelectMinCol AndAlso DFClsForms.CanUpdate
        Me.mForm.FindControls("GridView").Columns(6).Visible = canSelectMinCol AndAlso DFClsForms.CanDelete
        Me.mForm.FindControls("GridView").PagerSettings.Visible = canSelectMinCol

        Me.mForm.FindControls("mnuAddNew").FindItem("AddNew").Enabled = DFClsForms.CanInsert()

        canSelectField = DFClsForms.CanSelectField("txtFormID")
        Me.mForm.FindControls("txtFormID").Visible = canSelectField
        Me.mForm.FindControls("lblFormID").Visible = canSelectField

        canSelectField = DFClsForms.CanSelectField("txtFormDescription")
        Me.mForm.FindControls("txtFormDescription").Visible = canSelectField
        Me.mForm.FindControls("lblFormDescription").Visible = canSelectField

        canSelectField = DFClsForms.CanSelectField("ddlFormLog")
        Me.mForm.FindControls("ddlFormLog").Visible = canSelectField
        Me.mForm.FindControls("lblFormLog").Visible = canSelectField
    End Sub

    Public Shared Sub ApplyGridRowAuthorizationRules(ByVal gridView As GridView, ByVal row As System.Web.UI.WebControls.GridViewRow)
        If row.RowType = DataControlRowType.DataRow Then
            Dim canSelect As Boolean = DFClsForms.CanSelect

            row.Cells(0).Controls(1).Visible = canSelect
            row.Cells(0).Controls(2).Visible = Not canSelect
        End If
    End Sub

    Public Sub ApplyAddFormAuthorizationRules(ByVal editMode As Boolean)
        Dim canSave As Boolean

        Me.mForm.FindControls("txtFormDescription").Enabled = (Not editMode AndAlso DFClsForms.CanInsertField("txtFormDescription")) OrElse (editMode AndAlso DFClsForms.CanUpdateField("txtFormDescription"))

        Me.ApplyMenuItemAuthorizationRules(editMode)

        canSave = (Not editMode AndAlso DFClsForms.CanInsert) OrElse (editMode AndAlso DFClsForms.CanUpdate)
        Me.mForm.FindControls("mnuSave").FindItem("Ok").Enabled = canSave
        Me.mForm.FindControls("mnuSave").FindItem("Save").Enabled = canSave

        Me.ShowAddForm(DFClsForms.CanSelect OrElse DFClsForms.CanInsert OrElse DFClsForms.CanUpdate)
    End Sub

    Private Sub ApplyMenuItemAuthorizationRules(ByVal editMode As Boolean)
        Me.mForm.FindControls("mnuItem").FindItem("Permissions").Enabled = editMode AndAlso DFClsFormProfiles.CanSelect
        Me.mForm.FindControls("mnuItem").FindItem("Groups").Enabled = editMode AndAlso DFClsGroups.CanSelect
        Me.mForm.FindControls("mnuItem").FindItem("Delete").Enabled = editMode AndAlso DFClsForms.CanDelete
    End Sub

    Public Function ValidateExistsDescription(ByVal ControlName As String) As Boolean
        Return Not ClsForm.Exists(Me.mData.ID, Me.mForm.FindControls(ControlName).Text)
    End Function

    Public Sub LoadLogFilter(ByVal logDescription As String)
        Me.LoadCombo(Me.mForm.FindControls("ddlLogs"), Me.GetComboDataSources(New List(Of SCT.DataAccess.Logs)([Enum].GetValues(GetType(SCT.DataAccess.Logs))).ToArray, String.Empty), logDescription)
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

    Private Function GetComboDataSources(ByVal logs() As SCT.DataAccess.Logs, ByVal FirstItemText As String) As DataTable
        Dim table As New DataTable

        table.Columns.Add("Description", GetType(String))
        table.Columns.Add("ID", GetType(String))

        table.DefaultView.Sort = "Description"

        table.Rows.Add(New String() {FirstItemText, String.Empty})
        For Each log As SCT.DataAccess.Logs In logs
            table.Rows.Add(New Object() {log, log})
        Next
        Return table
    End Function

    Private Sub LoadGridView(ByVal gridview As GridView, ByVal PageIndex As Integer)
        Me.AddRowData(gridview, PageIndex)
        Me.AddSortImage(gridview)
    End Sub

    Private Sub AddRowData(ByVal GridView As System.Web.UI.WebControls.GridView, ByVal PageIndex As Integer)
        GridView.PageIndex = PageIndex
        GridView.DataSource = Me.GetDataSources
        GridView.DataBind()
    End Sub

    Private Function GetDataSources() As DataTable
        Dim table As New DataTable

        table.Columns.Add("ID", GetType(Long))
        table.Columns.Add("Description", GetType(String))
        table.Columns.Add("LogDescription", GetType(String))

        table.DefaultView.Sort = Me.GetSortProperty

        For Each form As FormData In Me.mDataList
            table.Rows.Add(New Object() {form.ID, form.Description, form.LogDescription})
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
                Return sortProperty & ", LogDescription " & Me.mSortDirection
            Case "LogDescription"
                Return sortProperty & ", Description " & Me.mSortDirection
            Case Else
                Return sortProperty
        End Select
    End Function

    Private Sub CollectFilters()
        Me.mLogsDescription = CollectLogDescriptionSelected(Me.mForm.FindControls("ddlLogs"))
    End Sub

    Private Function CollectLogDescriptionSelected(ByVal logDescriptionsList As DropDownList) As String()
        If Len(logDescriptionsList.SelectedValue) > 0 Then
            Return New String() {logDescriptionsList.SelectedValue}
        Else
            Return New String() {}
        End If
    End Function

    Private Sub ClearAddForm()
        Me.mForm.FindControls("txtFormID").Text = String.Empty
        Me.mForm.FindControls("txtFormDescription").Text = String.Empty
        Me.LoadCombo(Me.mForm.FindControls("ddlFormLog"), Me.GetComboDataSources(New List(Of SCT.DataAccess.Logs)([Enum].GetValues(GetType(SCT.DataAccess.Logs))).ToArray, "[Select a Log]"), String.Empty)
        Me.mForm.FindControls("txtFormDescription").Focus()
    End Sub

    Private Sub ShowAddForm(ByVal value As Boolean)
        Me.mForm.FindControls("pnlAddForm").Visible = value
    End Sub

    Public Sub OnSorting(ByVal Sortexpression As String, ByVal PageIndex As Integer)
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

    Public Sub OnPermissions(ByVal formID As Long)
        Me.mForm.Session("ID1") = formID
        Me.mForm.Response.Redirect("~/Forms/frmFormProfiles.aspx")
    End Sub

    Public Sub OnPermissions()
        Me.mForm.Session("ID1") = Me.mData.ID.ToString
        Me.mForm.Response.Redirect("~/Forms/frmFormProfiles.aspx")
    End Sub

    Public Sub OnGroups(ByVal formID As Long)
        Me.mForm.Session("ID2") = 0
        Me.mForm.Session("ID1") = formID
        Me.mForm.Response.Redirect("~/Forms/frmGroups.aspx")
    End Sub

    Public Sub OnGroups()
        Me.mForm.Session("ID2") = 0
        Me.mForm.Session("ID1") = Me.mData.ID.ToString
        Me.mForm.Response.Redirect("~/Forms/frmGroups.aspx")
    End Sub

    Public Sub OnEditing(ByVal formID As Long)
        Me.mEditMode = True
        Me.mForm.FindControls("txtFormID").Text = formID
        Me.mData = Me.GetData()
        If Me.mData IsNot Nothing Then
            Me.ApplyAddFormAuthorizationRules(True)
            Me.PopulateData()
        End If
    End Sub

    Public Sub OnDeleting(ByVal formID As Long)
        Me.ShowAddForm(False)
        Me.mData = New FormData
        Me.mForm.FindControls("txtFormID").Text = formID
        Me.mForm.FindControls("MsgBox").ShowConfirmation("Are you sure you want to delete this Form?\n\nNote: there is no undo.", "", True, False)
    End Sub

    Public Sub OnDeleting()
        Me.mForm.FindControls("MsgBox").ShowConfirmation("Are you sure you want to delete this Form?\n\nNote: there is no undo.", "", True, False)
    End Sub

    Public Sub OnMsgBoxOk()
        If Me.DeleteData Then
            Me.ShowAddForm(False)
            Me.LoadLogFilter(Me.mForm.FindControls("ddlLogs").SelectedValue)
            Me.mDataList = Me.GetDataList()
            Me.PopulateDataList("Description", "ASC", 0)
        End If
    End Sub

    Public Sub OnSelectedLogChanged()
        Me.ShowAddForm(False)
        Me.mDataList = Me.GetDataList()
        Me.PopulateDataList(0)
    End Sub

    Public Sub OnAddNew()
        Me.mEditMode = False
        Me.mData = New FormData
        Me.ApplyAddFormAuthorizationRules(False)
        Me.ClearAddForm()
    End Sub

    Public Sub OnSave()
        If Me.SaveData Then
            Me.LoadLogFilter(Me.mForm.FindControls("ddlFormLog").SelectedValue)
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
            Me.LoadLogFilter(Me.mForm.FindControls("ddlFormLog").SelectedValue)
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
        Me.LoadGridView(Me.mForm.FindControls("GridView"), PageIndex)
    End Sub

    Private Sub PopulateDataList(ByVal PageIndex As Integer)
        Me.LoadGridView(Me.mForm.FindControls("GridView"), PageIndex)
    End Sub

    Private Sub PopulateData()
        Me.mForm.FindControls("txtFormID").Text = Me.mData.ID
        Me.mForm.FindControls("txtFormDescription").Text = Me.mData.Description
        Me.LoadCombo(Me.mForm.FindControls("ddlFormLog"), Me.GetComboDataSources(New List(Of SCT.DataAccess.Logs)([Enum].GetValues(GetType(SCT.DataAccess.Logs))).ToArray, "[Select a Log]"), Me.mData.LogDescription)
        Me.mForm.FindControls("txtFormDescription").Focus()
    End Sub

    Private Function GetDataID(ByVal textBox As TextBox) As String
        If textBox.Text = String.Empty Then
            Return 0
        End If
        Return textBox.Text
    End Function

    Private Function CollectData() As FormNewData
        Dim formData As New FormNewData
        formData.ID.SetValues("txtFormID", True, Me.mData.ID, CLng(Me.GetDataID(Me.mForm.FindControls("txtFormID"))))
        formData.Description.SetValues("txtFormDescription", False, Me.mData.Description, Me.mForm.FindControls("txtFormDescription").Text)
        formData.LogDescription.SetValues("ddlFormLog", False, Me.mData.LogDescription, Me.mForm.FindControls("ddlFormLog").SelectedValue)
        Return formData
    End Function

    Private Function CollectDataID() As FormNewData
        Dim formData As New FormNewData
        formData.ID.SetValues("txtFormID", True, 0, CLng(Me.GetDataID(Me.mForm.FindControls("txtFormID"))))
        Return formData
    End Function

    Private Function GetDataList() As FormData()
        Try
            Me.CollectFilters()
            Return ClsSessionAdmin.GetFormList(Me.mLogsDescription)
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return New FormData() {}
        End Try
    End Function

    Private Function GetData() As FormData
        Try
            Return ClsSessionAdmin.Getform(Me.CollectDataID)
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return Nothing
        End Try
    End Function

    Private Function SaveData() As Boolean
        Try
            If Me.mEditMode Then
                Me.mData = ClsSessionAdmin.Editform(Me.CollectData)
            Else
                Me.mData = ClsSessionAdmin.Addform(Me.CollectData)
            End If
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error saving: " & SysEx.Message)
            Return False
        End Try
    End Function

    Private Function DeleteData() As Boolean
        Try
            ClsSessionAdmin.DeleteForm(Me.CollectDataID)
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error deleting: " & SysEx.Message)
            Return False
        End Try
    End Function

#End Region

End Class
