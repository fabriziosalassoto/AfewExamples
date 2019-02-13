Imports SCT.Library
Imports SCT.Library.AdminSite
Imports System.Web.UI.WebControls

Public Class DFClsBinnacleFormEntries

#Region " Private Fields "

    Private mForm As Object
    Private mDataList As BinnacleFormEntryData()

    Private mPageIndex As Integer

    Private mLog() As SCT.DataAccess.Logs = New SCT.DataAccess.Logs() {}
    Private mIDsUser As SearchCriteriaList(Of Long)
    Private mIDsForm As SearchCriteriaList(Of Long)
    Private mIDsOperation As SearchCriteriaList(Of Long)
    Private mStartDate As SearchCriteria(Of Date)
    Private mEndDate As SearchCriteria(Of Date)

#End Region

#Region " Authorization Rules "

    Public Shared Function CanSelect() As Boolean
        Return ClsSessionAdmin.CanSelectInForm("frmBinnacleFormEntries")
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
        Me.mForm.Session("ValuePath") = "frmSecurity/frmBinnacleFormEntries"

        Me.ApplyPageAuthorizationRules()

        Me.LoadLogsList(String.Empty)
        Me.LoadOperationsList(String.Empty)
        Me.LoadUsersList(String.Empty)
        Me.LoadFormsList(String.Empty)
        Me.LoadStartYear()
        Me.LoadEndYear()
    End Sub

    Public Sub ApplyPageAuthorizationRules()
        If (Not ClsSessionAdmin.IsValidForm("frmBinnacleFormEntries", "grdBinnacleFormEntries.ID")) OrElse (Not DFClsBinnacleFormEntries.CanSelect()) Then Me.mForm.Response.Redirect("~/Forms/frmMain.aspx")

        Me.mForm.FindControls("grdBinnacleFormEntries").Columns(5).Visible = DFClsBinnacleFormEntry.CanSelect
    End Sub

    Public Function ValidateDate(ByVal year As String, ByVal month As String, ByVal day As String) As Boolean
        Dim expression As String = year & "-" & month & "-" & day
        Return IsDate(expression)
    End Function

    Public Sub LoadLogsList(ByVal selectedValue As String)
        Me.LoadList(Me.mForm.FindControls("lstLogs"), Me.GetListDataSources(New List(Of SCT.DataAccess.Logs)([Enum].GetValues(GetType(SCT.DataAccess.Logs))).ToArray), selectedValue)
    End Sub

    Public Sub LoadOperationsList(ByVal selectedValue As String)
        Me.LoadList(Me.mForm.FindControls("lstOperations"), Me.GetListDataSources(Me.GetOperationInfoList), selectedValue)
    End Sub

    Public Sub LoadUsersList(ByVal selectedValue As String)
        Dim logsSelected() As SCT.DataAccess.Logs = Me.CollectLogsSelected(Me.mForm.FindControls("lstLogs").Items, True)
        If logsSelected.Length = 1 Then
            Me.LoadList(Me.mForm.FindControls("lstUsers"), Me.GetListDataSources(Me.GetBinnacleUserInfoList(logsSelected(0))), selectedValue)
        Else
            Me.LoadList(Me.mForm.FindControls("lstUsers"), Me.GetListDataSources(New BinnacleUserData() {}), String.Empty)
        End If
    End Sub

    Public Sub LoadFormsList(ByVal selectedValue As String)
        Dim logsSelected() As SCT.DataAccess.Logs = Me.CollectLogsSelected(Me.mForm.FindControls("lstLogs").Items, True)
        If logsSelected.Length = 1 Then
            Me.LoadList(Me.mForm.FindControls("lstForms"), Me.GetListDataSources(Me.GetFormInfoList(logsSelected(0))), selectedValue)
        Else
            Me.LoadList(Me.mForm.FindControls("lstForms"), Me.GetListDataSources(New FormData() {}), String.Empty)
        End If
    End Sub

    Public Sub LoadStartYear()
        Me.LoadList(Me.mForm.FindControls("ddlStartYear"), Me.GetListDataSources, String.Empty)
    End Sub

    Public Sub LoadEndYear()
        Me.LoadList(Me.mForm.FindControls("ddlEndYear"), Me.GetListDataSources, String.Empty)
    End Sub

    Public Sub LoadFilters()
        Me.LoadUsersList("0")
        Me.LoadFormsList("0")
    End Sub

    Private Sub LoadList(ByVal list As Object, ByVal data As DataTable, ByVal SelectedValue As String)
        list.DataSource = data
        list.DataTextField = data.Columns(0).Caption
        list.DataValueField = data.Columns(1).Caption
        list.DataBind()
        If list.Enabled AndAlso list.Items.FindByValue(SelectedValue) IsNot Nothing Then
            list.SelectedValue = SelectedValue
        End If
    End Sub

    Private Function NewListDataTable(Of T)(ByVal text As String, ByVal value As T) As DataTable
        Dim table As New DataTable
        table.Columns.Add("Text", GetType(String))
        table.Columns.Add("Value", GetType(T))
        table.DefaultView.Sort = "Text"
        table.Rows.Add(New Object() {text, value})
        Return table
    End Function

    Private Function GetListDataSources(ByVal InfoList() As OperationData) As DataTable
        Dim table As DataTable = Me.NewListDataTable(Of Long)("(All)", 0)
        For Each Info As OperationData In InfoList
            table.Rows.Add(New Object() {Info.Description, Info.ID})
        Next
        Return table
    End Function

    Private Function GetListDataSources(ByVal InfoList() As BinnacleUserData) As DataTable
        Dim table As DataTable = Me.NewListDataTable(Of Long)("(All)", 0)
        For Each Info As BinnacleUserData In InfoList
            table.Rows.Add(New Object() {Info.Name, Info.ID})
        Next
        Return table
    End Function

    Private Function GetListDataSources(ByVal InfoList() As FormData) As DataTable
        Dim table As DataTable = Me.NewListDataTable(Of Long)("(All)", 0)
        For Each Info As FormData In InfoList
            table.Rows.Add(New Object() {Info.Description, Info.ID})
        Next
        Return table
    End Function

    Private Function GetListDataSources(ByVal logs() As SCT.DataAccess.Logs) As DataTable
        Dim table As DataTable = Me.NewListDataTable(Of Integer)("(All)", 0)
        For Each log As SCT.DataAccess.Logs In logs
            table.Rows.Add(New Object() {log, log})
        Next
        Return table
    End Function

    Private Function GetListDataSources() As DataTable
        Dim table As DataTable = Me.NewListDataTable(Of String)(String.Empty, String.Empty)
        For i As Integer = 2000 To Date.Now.Year
            table.Rows.Add(New Object() {i.ToString, i})
        Next
        Return table
    End Function

    Private Sub LoadGridView(ByVal gridview As GridView, ByVal checkItemList As ListItemCollection, ByVal radioItem As ListItem)
        Me.SortColumns(gridview, checkItemList)
        Me.AddRowData(gridview, checkItemList, radioItem)
    End Sub

    Private Sub SortColumns(ByVal gridview As GridView, ByVal checkItemList As ListItemCollection)
        Dim index As Integer
        For Each item As ListItem In checkItemList
            CType(gridview.Columns(index), BoundField).HeaderText = item.Text
            CType(gridview.Columns(index), BoundField).DataField = item.Value
            If item.Value = "BDate" Then
                CType(gridview.Columns(index), BoundField).DataFormatString = "{0:MMMM dd, yyyy}"
            Else
                CType(gridview.Columns(index), BoundField).DataFormatString = ""
            End If
            index += 1
        Next
    End Sub

    Private Sub AddRowData(ByVal GridView As System.Web.UI.WebControls.GridView, ByVal checkItemList As ListItemCollection, ByVal radioItem As ListItem)
        GridView.PageIndex = Me.mPageIndex
        GridView.DataSource = Me.GetGridDataSources(checkItemList, radioItem)
        GridView.DataBind()
        If GridView.DataSource.Rows.Count > 0 Then
            GridView.Caption = "Results " & 1 + GridView.PageIndex * GridView.PageSize & " to " & (GridView.PageIndex * GridView.PageSize) + GridView.Rows.Count & " of " & GridView.DataSource.Rows.Count & " Log Entries"
        Else
            GridView.Caption = String.Empty
        End If
    End Sub

    Private Function GetGridDataSources(ByVal checkItemList As ListItemCollection, ByVal radioItem As ListItem) As DataTable
        Dim table As New DataTable

        table.Columns.Add("BinnacleFormID", GetType(Long))
        table.Columns.Add("LogValue", GetType(SCT.DataAccess.Logs))
        table.Columns.Add("Log", GetType(String))
        table.Columns.Add("BDate", GetType(Date))
        table.Columns.Add("BHour", GetType(Date))
        table.Columns.Add("User", GetType(String))
        table.Columns.Add("Form", GetType(String))
        table.Columns.Add("Operation", GetType(String))

        table.DefaultView.Sort = Me.GetSortProperty(checkItemList, radioItem)

        For Each binnacleFormEntry As BinnacleFormEntryData In Me.mDataList
            table.Rows.Add(New Object() {binnacleFormEntry.ID, binnacleFormEntry.Log, binnacleFormEntry.Log.ToString, binnacleFormEntry.BDate, binnacleFormEntry.BHour, binnacleFormEntry.User.Name, binnacleFormEntry.Form.Description, binnacleFormEntry.Operation.Description})
        Next
        Return table
    End Function

    Private Function GetSortProperty(ByVal checkItemList As ListItemCollection, ByVal radioItem As ListItem) As String
        Dim sortProperty As String = String.Empty
        For Each checkItem As ListItem In checkItemList
            sortProperty &= checkItem.Value & " " & radioItem.Value & ", "
        Next
        sortProperty &= "BHour " & " " & radioItem.Value
        Return sortProperty
    End Function

    Public Sub SelectFilter(ByVal checkList As CheckBoxList)
        Me.SortFilterChecks(checkList)
        Me.ShowFilters(checkList)
    End Sub

    Private Sub SortFilterChecks(ByVal checkList As CheckBoxList)
        Dim selectedItems As ListItem() = Me.GetSelectedItem(checkList)
        Dim noSelectedItems As ListItem() = Me.GetNoSelectedItem(checkList, "Log", "BDate", "User", "Form", "Operation")

        checkList.Items.Clear()
        checkList.Items.AddRange(selectedItems)
        checkList.Items.AddRange(noSelectedItems)
    End Sub

    Private Function GetSelectedItem(ByVal checkList As CheckBoxList) As ListItem()
        Dim items As New Generic.List(Of ListItem)
        For Each item As ListItem In checkList.Items
            If item.Selected Then
                items.Add(item)
            End If
        Next
        Return items.ToArray
    End Function

    Private Function GetNoSelectedItem(ByVal checkList As CheckBoxList, ByVal ParamArray filterName() As String) As ListItem()
        Dim items As New Generic.List(Of ListItem)
        For Each name As String In filterName
            Dim item = checkList.Items.FindByValue(name)
            If Not item.Selected Then
                items.Add(item)
            End If
        Next
        Return items.ToArray
    End Function

    Private Sub ShowFilters(ByVal checkList As CheckBoxList)
        Me.ShowLogFilter(Me.mForm.FindControls("lstLogs"), Me.mForm.FindControls("lstUsers"), Me.mForm.FindControls("lstForms"), Me.mForm.FindControls("rfvLogs"), checkList.Items.FindByValue("Log"))
        Me.ShowFilter(Me.mForm.FindControls("lstOperations"), Me.mForm.FindControls("rfvOperations"), checkList.Items.FindByValue("Operation"))
        Me.ShowFilter(Me.mForm.FindControls("lstUsers"), Me.mForm.FindControls("rfvUsers"), checkList.Items.FindByValue("User"))
        Me.ShowFilter(Me.mForm.FindControls("lstForms"), Me.mForm.FindControls("rfvForms"), checkList.Items.FindByValue("Form"))
        Me.ShowFilter(Me.mForm.FindControls("ddlStartMonth"), Me.mForm.FindControls("ddlStartDay"), Me.mForm.FindControls("ddlStartYear"), Me.mForm.FindControls("vldStartDate"), checkList.Items.FindByValue("BDate"))
        Me.ShowFilter(Me.mForm.FindControls("ddlEndMonth"), Me.mForm.FindControls("ddlEndDay"), Me.mForm.FindControls("ddlEndYear"), Me.mForm.FindControls("vldEndDate"), checkList.Items.FindByValue("BDate"))
    End Sub

    Private Sub ShowLogFilter(ByVal logList As ListBox, ByVal userList As ListBox, ByVal formList As ListBox, ByVal validator As RequiredFieldValidator, ByVal checkItem As ListItem)
        Me.ShowFilter(logList, validator, checkItem)
        If Not checkItem.Selected Then
            Me.LoadUsersList(String.Empty)
            Me.LoadFormsList(String.Empty)
        End If
    End Sub

    Private Sub ShowFilter(ByVal list As ListBox, ByVal validator As RequiredFieldValidator, ByVal checkItem As ListItem)
        list.Enabled = checkItem.Selected
        validator.Enabled = checkItem.Selected
        If checkItem.Selected Then
            If list.SelectedItem Is Nothing Then
                list.SelectedIndex = 0
            End If
        Else
            list.ClearSelection()
        End If
    End Sub

    Private Sub ShowFilter(ByVal month As DropDownList, ByVal day As DropDownList, ByVal year As DropDownList, ByVal validator As CustomValidator, ByVal checkItem As ListItem)
        month.Enabled = checkItem.Selected
        month.Items(0).Enabled = Not checkItem.Selected

        day.Enabled = checkItem.Selected
        day.Items(0).Enabled = Not checkItem.Selected

        year.Enabled = checkItem.Selected
        year.Items(0).Enabled = Not checkItem.Selected

        validator.Enabled = checkItem.Selected

        If checkItem.Selected Then
            If month.SelectedValue = String.Empty Then
                month.SelectedValue = Date.Now.Month.ToString("00")
            End If
            If day.SelectedValue = String.Empty Then
                day.SelectedValue = Date.Now.Day.ToString("00")
            End If
            If year.SelectedValue = String.Empty Then
                year.SelectedValue = Date.Now.Year.ToString("0000")
            End If
        Else
            month.SelectedValue = String.Empty
            day.SelectedValue = String.Empty
            year.SelectedValue = String.Empty
        End If
    End Sub

    Public Sub ShowNextGridPage(ByVal NewPageIndex As Integer)
        Me.PopulateDataList(NewPageIndex)
    End Sub

    Public Sub ShowBinnacleFormEntry(ByVal binnacleFormID As String, ByVal Log As String)
        Me.mForm.Session("Log") = Log
        Me.mForm.Session("ID") = binnacleFormID
        Me.mForm.Response.Write("<script>window.open('frmBinnacleFormEntry.aspx','_blank','width=500,height=575,scrollbars=YES,resizable=YES,status=YES');</script>")
    End Sub

    Public Sub ClearQuery()
        Me.mDataList = New BinnacleFormEntryData() {}
        Me.PopulateDataList(0)
    End Sub

    Public Sub SubmitQuery()
        Me.CollectFiltersValuesSelected(Me.mForm.FindControls("cklFilter"))
        Me.mDataList = Me.GetDataList(Me.mLog, Me.mIDsUser, Me.mIDsForm, Me.mIDsOperation, Me.mStartDate, Me.mEndDate)
        Me.PopulateDataList(0)
    End Sub

    Private Sub CollectFiltersValuesSelected(ByVal checkBoxList As CheckBoxList)
        For Each checkBoxItem As ListItem In checkBoxList.Items
            Select Case checkBoxItem.Value
                Case "Log"
                    Me.mLog = Me.CollectLogsSelected(Me.mForm.FindControls("lstLogs").Items, checkBoxItem.Selected)
                Case "User"
                    Me.mIDsUser = Me.CollectIDsSelected(Me.mForm.FindControls("lstUsers").Items, checkBoxList.Items.IndexOf(checkBoxItem) + 1, checkBoxItem.Selected)
                Case "Form"
                    Me.mIDsForm = Me.CollectIDsSelected(Me.mForm.FindControls("lstForms").Items, checkBoxList.Items.IndexOf(checkBoxItem) + 1, checkBoxItem.Selected)
                Case "Operation"
                    Me.mIDsOperation = Me.CollectIDsSelected(Me.mForm.FindControls("lstOperations").Items, checkBoxList.Items.IndexOf(checkBoxItem) + 1, checkBoxItem.Selected)
                Case "BDate"
                    Me.mStartDate = Me.CollectStartDate(checkBoxList.Items.IndexOf(checkBoxItem), checkBoxItem.Selected)
                    Me.mEndDate = Me.CollectEndDate(checkBoxList.Items.IndexOf(checkBoxItem), checkBoxItem.Selected)
                Case Else
            End Select
        Next
    End Sub

    Private Function CollectLogsSelected(ByVal filterList As ListItemCollection, ByVal filterSelected As Boolean) As SCT.DataAccess.Logs()
        Dim logsSelected As New List(Of SCT.DataAccess.Logs)
        If filterSelected Then
            For Each item As ListItem In filterList
                If item.Selected Then
                    If item.Value = 0 Then
                        Return New List(Of SCT.DataAccess.Logs)([Enum].GetValues(GetType(SCT.DataAccess.Logs))).ToArray
                    Else
                        logsSelected.Add(item.Value)
                    End If
                End If
            Next
        Else
            logsSelected = New List(Of SCT.DataAccess.Logs)([Enum].GetValues(GetType(SCT.DataAccess.Logs)))
        End If
        Return logsSelected.ToArray
    End Function

    Private Function CollectIDsSelected(ByVal filterList As ListItemCollection, ByVal filterIndex As Integer, ByVal filterSelected As Boolean) As SearchCriteriaList(Of Long)
        Dim idsSelected As New SearchCriteriaList(Of Long)(filterIndex)
        If filterSelected Then
            For Each item As ListItem In filterList
                If item.Selected Then
                    If item.Value = 0 Then
                        Return New SearchCriteriaList(Of Long)(New Long() {item.Value}, filterIndex)
                    Else
                        idsSelected.AddValue(item.Value)
                    End If
                End If
            Next
        End If
        Return idsSelected
    End Function

    Private Function CollectStartDate(ByVal filterIndex As Integer, ByVal filterSelected As Boolean) As SearchCriteria(Of Date)
        If filterSelected Then
            Return New SearchCriteria(Of Date)(New Date(Me.mForm.FindControls("ddlStartYear").Text, Me.mForm.FindControls("ddlStartMonth").SelectedValue, Me.mForm.FindControls("ddlStartDay").SelectedValue), filterIndex)
        End If
        Return New SearchCriteria(Of Date)(New Date(1900, 1, 1), filterIndex)
    End Function

    Private Function CollectEndDate(ByVal filterIndex As Integer, ByVal filterSelected As Boolean) As SearchCriteria(Of Date)
        If filterSelected Then
            Return New SearchCriteria(Of Date)(New Date(Me.mForm.FindControls("ddlEndYear").Text, Me.mForm.FindControls("ddlEndMonth").SelectedValue, Me.mForm.FindControls("ddlEndDay").SelectedValue), filterIndex)
        End If
        Return New SearchCriteria(Of Date)(Date.MaxValue, filterIndex)
    End Function

#End Region

#Region " Data Methods "

    Private Sub PopulateDataList(ByVal PageIndex As Integer)
        Me.mPageIndex = PageIndex
        Me.LoadGridView(Me.mForm.FindControls("grdBinnacleFormEntries"), Me.mForm.FindControls("cklFilter").Items, Me.mForm.FindControls("rdlSortDirection").SelectedItem)
    End Sub

    Private Function GetDataList(ByVal log() As SCT.DataAccess.Logs, ByVal idUser As SearchCriteriaList(Of Long), ByVal idForm As SearchCriteriaList(Of Long), ByVal idOperation As SearchCriteriaList(Of Long), ByVal startDate As SearchCriteria(Of Date), ByVal endDate As SearchCriteria(Of Date)) As BinnacleFormEntryData()
        Try
            Return ClsSessionAdmin.GetBinnacleFormEntryList(log, idUser, startDate, endDate, idForm, idOperation)
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return New BinnacleFormEntryData() {}
        End Try
    End Function

    Private Function GetOperationInfoList() As OperationData()
        Try
            Return ClsSessionAdmin.GetOpeationInfoList
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return New OperationData() {}
        End Try
    End Function

    Private Function GetBinnacleUserInfoList(ByVal log As SCT.DataAccess.Logs) As BinnacleUserData()
        Try
            Return ClsSessionAdmin.GetBinnacleUserInfoList(log)
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return New BinnacleUserData() {}
        End Try
    End Function

    Private Function GetFormInfoList(ByVal log As SCT.DataAccess.Logs) As FormData()
        Try
            Return ClsSessionAdmin.GetFormInfoList(log.ToString)
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return New FormData() {}
        End Try
    End Function

#End Region

End Class
