Imports SCT.Library
Imports SCT.Library.AdminSite
Imports System.Web.UI.WebControls

Public Class DFClsOperations

#Region " Private Fields "

    Private mForm As Object
    Private mDataList As OperationData()

    Private mPageIndex As Integer
    Private mSortDirection As String
    Private mSortExpression As String

#End Region

#Region " Authorization Rules "

    Public Shared Function CanSelect() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmOperations") Then
            Return ClsSessionAdmin.CanSelectInForm("frmOperations")
        Else
            Return ClsSessionAdmin.CanSelectInForm("AllForms")
        End If
    End Function

    Public Shared Function CanInsert() As Boolean
        Return ClsSessionAdmin.CanInsertInForm("frmOperations")
    End Function

    Public Shared Function CanUpdate() As Boolean
        Return ClsSessionAdmin.CanUpdateInForm("frmOperations")
    End Function

    Public Shared Function CanDelete() As Boolean
        Return ClsSessionAdmin.CanDeleteInForm("frmOperations")
    End Function

    Public Shared Function CanSelectField(ByVal fieldName As String) As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmOperations") Then
            Return ClsSessionAdmin.CanSelectFieldInForm("frmOperations", fieldName)
        Else
            Return ClsSessionAdmin.CanSelectFieldInForm("AllForms", "AllFields")
        End If
    End Function

    Public Shared Function CanInsertField(ByVal fieldName As String) As Boolean
        Return ClsSessionAdmin.CanInsertFieldInForm("frmOperations", fieldName)
    End Function

    Public Shared Function CanUpdateField(ByVal fieldName As String) As Boolean
        Return ClsSessionAdmin.CanUpdateFieldInForm("frmOperations", fieldName)
    End Function

    Public Shared Function CanDeleteField(ByVal fieldName As String) As Boolean
        Return ClsSessionAdmin.CanDeleteFieldInForm("frmOperations", fieldName)
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
        Me.mForm.Session("ValuePath") = "frmSecurity/frmOperations"

        Me.ApplyPageAuthorizationRules()

        'Me.mDataList = Me.GetDataList()

        'Me.LoadProfileFilter(Me.mForm.Session("ID1"))
        'Me.PopulateDataList("FullName", "ASC", 0)
    End Sub

    Public Sub ApplyPageAuthorizationRules()
        If (Not ClsSessionAdmin.IsValidForm("frmOperations")) OrElse (Not DFClsBinnacleFormEntries.CanSelect()) Then Me.mForm.Response.Redirect("~/Forms/frmMain.aspx")
    End Sub

    'Public Shared Sub ApplyGridRowAuthorizationRules(ByVal gridView As GridView, ByVal row As System.Web.UI.WebControls.GridViewRow)
    '    If row.RowType = DataControlRowType.DataRow Then
    '        Dim canSelectUser As Boolean = DFClsUser.CanSelect

    '        CType(row.Cells(5).Controls(1), LinkButton).CommandArgument = row.RowIndex
    '        row.Cells(5).Controls(1).Visible = canSelectUser
    '        row.Cells(5).Controls(2).Visible = Not canSelectUser

    '        Dim canSelectForm As Boolean = DFClsForms.CanSelect

    '        CType(row.Cells(7).Controls(1), LinkButton).CommandArgument = row.RowIndex
    '        row.Cells(7).Controls(1).Visible = canSelectForm
    '        row.Cells(7).Controls(2).Visible = Not canSelectForm

    '        Dim canSelectOperation As Boolean = True 'DFClsOperations.CanSelect

    '        CType(row.Cells(9).Controls(1), LinkButton).CommandArgument = row.RowIndex
    '        row.Cells(9).Controls(1).Visible = canSelectOperation
    '        row.Cells(9).Controls(2).Visible = Not canSelectOperation
    '    End If
    'End Sub

    'Public Sub LoadProfileFilter(ByVal profileID As String)
    '    If String.IsNullOrEmpty(profileID) OrElse Not IsNumeric(profileID) Then
    '        profileID = 0
    '    End If
    '    Me.LoadCombo(Me.mForm.FindControls("ddlProfiles"), Me.GetComboDataSources(Me.GetProfileInfoList, "Profile"), CLng(profileID))
    'End Sub

    'Private Sub LoadCombo(ByVal combo As DropDownList, ByVal data As DataTable, ByVal selectedValue As String)
    '    combo.DataSource = data
    '    combo.DataTextField = data.Columns(0).Caption
    '    combo.DataValueField = data.Columns(1).Caption
    '    combo.DataBind()
    '    If combo.Items.FindByValue(selectedValue) IsNot Nothing Then
    '        combo.SelectedValue = selectedValue
    '    End If
    'End Sub

    'Private Function GetComboDataSources(ByVal InfoList() As ProfileData, ByVal title As String) As DataTable
    '    Dim table As New DataTable

    '    table.Columns.Add("Description", GetType(String))
    '    table.Columns.Add("ID", GetType(String))
    '    table.DefaultView.Sort = "Description"

    '    table.Rows.Add(New String() {"", "0"})
    '    For Each Info As ProfileData In InfoList
    '        table.Rows.Add(New String() {Info.Description, Info.ID.ToString})
    '    Next
    '    Return table
    'End Function

    'Private Sub LoadGridView(ByVal gridview As GridView)
    '    Me.AddRowData(gridview)
    '    Me.AddSortImage(gridview)
    'End Sub

    'Private Sub AddRowData(ByVal GridView As System.Web.UI.WebControls.GridView)
    '    GridView.SelectedIndex = -1
    '    GridView.PageIndex = Me.mPageIndex
    '    GridView.DataSource = Me.GetDataSources()
    '    GridView.Columns(0).Visible = True
    '    GridView.Columns(1).Visible = True
    '    GridView.Columns(4).Visible = True
    '    GridView.Columns(6).Visible = True
    '    GridView.Columns(8).Visible = True
    '    GridView.DataBind()
    '    GridView.Columns(0).Visible = False
    '    GridView.Columns(1).Visible = False
    '    GridView.Columns(4).Visible = False
    '    GridView.Columns(6).Visible = False
    '    GridView.Columns(8).Visible = False
    'End Sub

    'Private Function GetDataSources() As DataTable
    '    Dim table As New DataTable

    '    table.Columns.Add("BinnacleID", GetType(Long))
    '    table.Columns.Add("BinnacleFormID", GetType(Long))
    '    table.Columns.Add("BDate", GetType(Date))
    '    table.Columns.Add("BHour", GetType(Date))
    '    table.Columns.Add("UserID", GetType(Long))
    '    table.Columns.Add("User", GetType(String))
    '    table.Columns.Add("FormID", GetType(Long))
    '    table.Columns.Add("Form", GetType(String))
    '    table.Columns.Add("OperationID", GetType(Long))
    '    table.Columns.Add("Operation", GetType(String))

    '    table.DefaultView.Sort = Me.GetSortProperty

    '    If Me.mForm.FindControls("ddlProfiles").SelectedValue <> "0" Then
    '        table.DefaultView.RowFilter = "ProfileID = '" & Me.mForm.FindControls("ddlProfiles").SelectedValue & "'"
    '    End If

    '    For Each binnacle As BinnacleData In Me.mDataList
    '        For Each binnacleForm As BinnacleFormData In binnacle.BinnacleForms
    '            table.Rows.Add(New Object() {binnacle.ID, binnacleForm.ID, binnacle.BDate, binnacleForm.BHour, binnacle.User.ID, binnacle.User.FullName, binnacleForm.Form.ID, binnacleForm.Form.Description, binnacleForm.Operation.ID, binnacleForm.Operation.Description})
    '        Next
    '    Next
    '    Return table
    'End Function

    'Private Sub AddSortImage(ByVal GridView As System.Web.UI.WebControls.GridView)
    '    If GridView.HeaderRow IsNot Nothing Then
    '        Dim Image As New Image()
    '        Dim ColumnIndex As Integer = GetSortColumnIndex(GridView)
    '        If ColumnIndex <> -1 Then
    '            Image.ImageUrl = "~/Images/" & Me.mSortDirection & ".GIF"
    '            GridView.HeaderRow.Cells(ColumnIndex).Controls.Add(Image)
    '        End If
    '    End If
    'End Sub

    'Private Function GetSortColumnIndex(ByVal GridView As System.Web.UI.WebControls.GridView) As Integer
    '    For Each field As DataControlField In GridView.Columns
    '        If field.SortExpression = Me.mSortExpression Then
    '            Return GridView.Columns.IndexOf(field)
    '        End If
    '    Next
    '    Return -1
    'End Function

    'Private Function GetSortProperty() As String
    '    Dim sortProperty As String = Me.mSortExpression & " " & Me.mSortDirection

    '    Select Case Me.mSortExpression
    '        Case "FullName"
    '            Return sortProperty & ", Profile " & Me.mSortDirection
    '        Case "Profile"
    '            Return sortProperty & ", FullName " & Me.mSortDirection
    '        Case Else
    '            Return sortProperty
    '    End Select
    'End Function

    'Public Sub OnSorting(ByVal SortExpression As String, ByVal PageIndex As Integer)
    '    If Me.mSortExpression <> SortExpression Then
    '        Me.PopulateDataList(SortExpression, "ASC", PageIndex)
    '    Else
    '        If Me.mSortDirection = "ASC" Then
    '            Me.PopulateDataList(SortExpression, "DESC", PageIndex)
    '        Else
    '            Me.PopulateDataList(SortExpression, "ASC", PageIndex)
    '        End If
    '    End If
    'End Sub

    'Public Sub OnPageIndexChanging(ByVal NewPageIndex As Integer)
    '    Me.PopulateDataList(NewPageIndex)
    'End Sub

    'Public Sub OnUser(ByVal userID As String)
    '    Me.mForm.Session("UserID") = userID
    '    Me.mForm.Response.Redirect("~/Forms/frmUser.aspx")
    'End Sub

    'Public Sub OnForm(ByVal formID As String)
    '    Me.mForm.Session("ID1") = formID
    '    Me.mForm.Response.Redirect("~/Forms/frmForms.aspx")
    'End Sub

    'Public Sub OnOperation(ByVal operationID As String)
    '    Me.mForm.Session("ID1") = operationID
    '    Me.mForm.Response.Redirect("~/Forms/frmOperations.aspx")
    'End Sub

#End Region

#Region " Data Methods "

    'Private Sub PopulateDataList(ByVal SortExpression As String, ByVal SortDirection As String, ByVal PageIndex As Integer)
    '    Me.mPageIndex = PageIndex
    '    Me.mSortDirection = SortDirection
    '    Me.mSortExpression = SortExpression
    '    Me.PopulateDataList()
    'End Sub

    'Private Sub PopulateDataList(ByVal PageIndex As Integer)
    '    Me.mPageIndex = PageIndex
    '    Me.PopulateDataList()
    'End Sub

    'Private Sub PopulateDataList()
    '    Me.LoadGridView(Me.mForm.FindControls("GridView"))
    'End Sub

    'Private Function GetDataList() As BinnacleData()
    '    Try
    '        Return CType(Me.mForm.Session("UserSessionAdmin"), ClsUserSessionAdmin).GetBinnacleList
    '    Catch DataEx As Csla.DataPortalException
    '        Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & DataEx.BusinessException.Message)
    '        Return New BinnacleData() {}
    '    Catch SysEx As Exception
    '        Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
    '        Return New BinnacleData() {}
    '    End Try
    'End Function

    'Private Function GetProfileInfoList() As ProfileData()
    '    Try
    '        Return CType(Me.mForm.Session("UserSessionAdmin"), ClsUserSessionAdmin).GetProfileInfoList()
    '    Catch DataEx As Csla.DataPortalException
    '        Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & DataEx.BusinessException.Message)
    '        Return New ProfileData() {}
    '    Catch SysEx As Exception
    '        Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
    '        Return New ProfileData() {}
    '    End Try
    'End Function

#End Region

End Class
