Imports SCT.Library
Imports SCT.Library.AdminSite
Imports System.Web.UI.WebControls

Public Class DFClsAdvertiserProjectHistory

#Region " Private Fields "

    Private mForm As Object
    Private mDataList As AdHistoryData()

    Private mPageIndex As Integer
    Private mSortDirection As String
    Private mSortExpression As String

    Private mAdAccountIDs() As Long
    Private mAdProjectIDs() As Long
    Private mStartDate As Date
    Private mEndDate As Date
    Private mStartTime As Date
    Private mEndTime As Date

#End Region

#Region " Authorization Rules "

    Public Shared Function CanSelect() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserProjectHistory") Then
            Return ClsSessionAdmin.CanSelectInForm("frmAdvertiserProjectHistory")
        Else
            Return ClsSessionAdmin.CanSelectInForm("AllForms")
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
        Me.mForm.Session("ValuePath") = "frmAdvertisers/frmAdvertiserProjectHistory"

        Me.ApplyPageAuthorizationRules()

        Me.LoadAdvertisersList(Me.mForm.Session("ID1"))
        Me.LoadProjectsList(Me.mForm.Session("ID2"))
        Me.LoadStartDate()
        Me.LoadEndDate()
        Me.LoadStartTime()
        Me.LoadEndTime()

        Me.CollectFiltersValuesSelected()
        Me.mDataList = Me.GetDataList(Me.mAdAccountIDs, Me.mAdProjectIDs, Me.mStartDate, Me.mEndDate, Me.mStartTime, Me.mEndTime)

        Me.PopulateDataList("Advertiser", "ASC", 0)
    End Sub

    Public Sub ApplyPageAuthorizationRules()
        If (Not ClsSessionAdmin.IsValidForm("frmAdvertiserProjectHistory")) OrElse (Not DFClsAdvertiserProjectHistory.CanSelect()) Then Me.mForm.Response.Redirect("~/Forms/frmMain.aspx")
    End Sub

    Public Shared Sub ApplyGridRowAuthorizationRules(ByVal gridView As GridView, ByVal row As System.Web.UI.WebControls.GridViewRow)
        If row.RowType = DataControlRowType.DataRow Then
            Dim canSelectAdvertiser As Boolean = DFClsAdvertiserAccount.CanSelect

            row.Cells(0).Controls(1).Visible = canSelectAdvertiser
            row.Cells(0).Controls(2).Visible = Not canSelectAdvertiser

            Dim canSelectProject As Boolean = DFClsAdvertiserProject.CanSelect

            row.Cells(1).Controls(1).Visible = canSelectProject
            row.Cells(1).Controls(2).Visible = Not canSelectProject
        End If
    End Sub

    Public Function ValidateStartDate() As Boolean
        Return Me.ValidateDate(Me.mForm.FindControls("ddlStartYear").SelectedValue, Me.mForm.FindControls("ddlStartMonth").SelectedValue, Me.mForm.FindControls("ddlStartDay").SelectedValue)
    End Function

    Public Function ValidateEndDate() As Boolean
        Return Me.ValidateDate(Me.mForm.FindControls("ddlEndYear").SelectedValue, Me.mForm.FindControls("ddlEndMonth").SelectedValue, Me.mForm.FindControls("ddlEndDay").SelectedValue)
    End Function

    Private Function ValidateDate(ByVal year As String, ByVal month As String, ByVal day As String) As Boolean
        Return (year = String.Empty AndAlso month = String.Empty AndAlso day = String.Empty) OrElse IsDate(year & "-" & month & "-" & day)
    End Function

    Public Function ValidateStartTime() As Boolean
        Return Me.ValidateTime(Me.mForm.FindControls("ddlStartHour").SelectedValue, Me.mForm.FindControls("ddlStartMinute").SelectedValue, Me.mForm.FindControls("ddlStartAMPM").SelectedValue)
    End Function

    Public Function ValidateEndTime() As Boolean
        Return Me.ValidateTime(Me.mForm.FindControls("ddlEndHour").SelectedValue, Me.mForm.FindControls("ddlEndMinute").SelectedValue, Me.mForm.FindControls("ddlEndAMPM").SelectedValue)
    End Function

    Private Function ValidateTime(ByVal hour As String, ByVal minute As String, ByVal ampm As String) As Boolean
        Return (hour = String.Empty AndAlso minute = String.Empty AndAlso ampm = String.Empty) OrElse (hour <> String.Empty AndAlso minute <> String.Empty AndAlso ampm <> String.Empty)
    End Function

    Public Sub LoadAdvertisersList(ByVal SelectedValue As String)
        Me.LoadList(Me.mForm.FindControls("lstAdAccounts"), Me.GetListDataSources(Me.GetAdAccountInfoList), SelectedValue)
    End Sub

    Public Sub LoadProjectsList(ByVal SelectedValue As String)
        Dim AdvertiserIDsSelected As Long() = Me.CollectIDsSelected(Me.mForm.FindControls("lstAdAccounts").Items)
        If AdvertiserIDsSelected.Length = 1 AndAlso AdvertiserIDsSelected(0) <> 0 Then
            Me.LoadList(Me.mForm.FindControls("lstAdProjects"), Me.GetListDataSources(Me.GetAdProjectInfoList(AdvertiserIDsSelected(0))), SelectedValue)
        Else
            Me.LoadList(Me.mForm.FindControls("lstAdProjects"), Me.GetListDataSources(New AdProjectData() {}), "0")
        End If
    End Sub

    Public Sub LoadStartDate()
        Me.mForm.FindControls("ddlStartMonth").SelectedValue = Date.Today.Month.ToString("00")
        Me.mForm.FindControls("ddlStartDay").SelectedValue = "01"
        Me.LoadList(Me.mForm.FindControls("ddlStartYear"), Me.GetListDataSources, Date.Today.Year.ToString("0000"))
    End Sub

    Public Sub LoadEndDate()
        Me.mForm.FindControls("ddlEndMonth").SelectedValue = Date.Today.Month.ToString("00")
        Me.mForm.FindControls("ddlEndDay").SelectedValue = Date.Today.Day.ToString("00")
        Me.LoadList(Me.mForm.FindControls("ddlEndYear"), Me.GetListDataSources, Date.Today.Year.ToString("0000"))
    End Sub

    Public Sub LoadStartTime()
        Me.mForm.FindControls("ddlStartHour").SelectedValue = String.Empty
        Me.mForm.FindControls("ddlStartMinute").SelectedValue = String.Empty
        Me.mForm.FindControls("ddlStartAMPM").SelectedValue = String.Empty
    End Sub

    Public Sub LoadEndTime()
        Me.mForm.FindControls("ddlEndHour").SelectedValue = String.Empty
        Me.mForm.FindControls("ddlEndMinute").SelectedValue = String.Empty
        Me.mForm.FindControls("ddlEndAMPM").SelectedValue = String.Empty
    End Sub

    Private Sub LoadList(ByVal list As Object, ByVal data As DataTable, ByVal SelectedValue As String)
        list.DataSource = data
        list.DataTextField = data.Columns(0).Caption
        list.DataValueField = data.Columns(1).Caption
        list.DataBind()
        If list.Items.FindByValue(SelectedValue) IsNot Nothing Then
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

    Private Function GetListDataSources(ByVal InfoList() As AdAccountData) As DataTable
        Dim table As DataTable = Me.NewListDataTable(Of Long)("(All)", 0)
        For Each Info As AdAccountData In InfoList
            table.Rows.Add(New Object() {Info.CompanyName, Info.ID})
        Next
        Return table
    End Function

    Private Function GetListDataSources(ByVal InfoList() As AdProjectData) As DataTable
        Dim table As DataTable = Me.NewListDataTable(Of Long)("(All)", 0)
        For Each Info As AdProjectData In InfoList
            table.Rows.Add(New Object() {Info.ADUrl, Info.ID})
        Next
        Return table
    End Function

    Private Function GetListDataSources() As DataTable
        Dim table As DataTable = Me.NewListDataTable(Of String)(String.Empty, String.Empty)
        For i As Integer = 2000 To Date.Now.Year
            table.Rows.Add(New Object() {i.ToString, i.ToString})
        Next
        Return table
    End Function

    Public Shared Sub HidenMinDate(ByVal gridView As GridView, ByVal row As System.Web.UI.WebControls.GridViewRow)
        If row.RowType = DataControlRowType.DataRow Then
            If row.Cells(2).Text(0) = "&" Then
                row.Cells(3).Text = "---"
                row.Cells(4).Text = "---"
            End If

            If row.Cells(5).Text(0) = "&" Then
                row.Cells(6).Text = "---"
                row.Cells(7).Text = "---"
            End If
        End If
    End Sub

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

        table.Columns.Add("AdvertiserID", GetType(Long))
        table.Columns.Add("Advertiser", GetType(String))
        table.Columns.Add("Project", GetType(Long))
        table.Columns.Add("URLDisplay", GetType(String))
        table.Columns.Add("DateDisplay", GetType(Date))
        table.Columns.Add("TimeDisplay", GetType(Date))
        table.Columns.Add("URLClickThru", GetType(String))
        table.Columns.Add("DateClickThru", GetType(Date))
        table.Columns.Add("TimeClickThru", GetType(Date))

        table.DefaultView.Sort = Me.GetSortProperty

        For Each adHistory As AdHistoryData In Me.mDataList
            With adHistory
                table.Rows.Add(New Object() {.Project.Advertiser.ID, .Project.Advertiser.CompanyName, .Project.ID, .URLAdDisplay, .DateAdDisplay, .TimeAdDisplay, .URLAdClickThru, .DateAdClickThru, .TimeAdClickThru})
            End With
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
            Case "Advertiser"
                Return sortProperty & ", Project " & Me.mSortDirection & ", URLDisplay " & Me.mSortDirection & ", DateDisplay " & Me.mSortDirection & ", TimeDisplay " & Me.mSortDirection
            Case "Project"
                Return sortProperty & ", Advertiser " & Me.mSortDirection & ", URLDisplay " & Me.mSortDirection & ", DateDisplay " & Me.mSortDirection & ", TimeDisplay " & Me.mSortDirection
            Case "URLDisplay"
                Return sortProperty & ", Advertiser " & Me.mSortDirection & ", Project " & Me.mSortDirection & ", DateDisplay " & Me.mSortDirection & ", TimeDisplay " & Me.mSortDirection
            Case "DateDisplay"
                Return sortProperty & ", TimeDisplay " & Me.mSortDirection & ", Advertiser " & Me.mSortDirection & ", Project " & Me.mSortDirection & ", URLDisplay " & Me.mSortDirection
            Case "TimeDisplay"
                Return sortProperty & ", DateDisplay " & Me.mSortDirection & ", Advertiser " & Me.mSortDirection & ", Project " & Me.mSortDirection & ", URLDisplay " & Me.mSortDirection
            Case "URLClickThru"
                Return sortProperty & ", Advertiser " & Me.mSortDirection & ", Project " & Me.mSortDirection & ", DateClickThru " & Me.mSortDirection & ", TimeClickThru " & Me.mSortDirection
            Case "DateClickThru"
                Return sortProperty & ", TimeClickThru " & Me.mSortDirection & ", Advertiser " & Me.mSortDirection & ", Project " & Me.mSortDirection & ", URLClickThru " & Me.mSortDirection
            Case "TimeClickThru"
                Return sortProperty & ", DateClickThru " & Me.mSortDirection & ", Advertiser " & Me.mSortDirection & ", Project " & Me.mSortDirection & ", URLClickThru " & Me.mSortDirection
            Case Else
                Return sortProperty
        End Select
    End Function

    Private Sub CollectFiltersValuesSelected()
        Me.mAdAccountIDs = Me.CollectIDsSelected(Me.mForm.FindControls("lstAdAccounts").Items)
        Me.mAdProjectIDs = Me.CollectIDsSelected(Me.mForm.FindControls("lstAdProjects").Items)
        Me.mStartDate = Me.CollectStartDate(Me.mForm.FindControls("ddlStartYear").Text, Me.mForm.FindControls("ddlStartMonth").SelectedValue, Me.mForm.FindControls("ddlStartDay").SelectedValue)
        Me.mEndDate = Me.CollectEndDate(Me.mForm.FindControls("ddlEndYear").Text, Me.mForm.FindControls("ddlEndMonth").SelectedValue, Me.mForm.FindControls("ddlEndDay").SelectedValue)
        Me.mStartTime = Me.CollectStartTime(Me.mForm.FindControls("ddlStartHour").Text, Me.mForm.FindControls("ddlStartMinute").SelectedValue, Me.mForm.FindControls("ddlStartAMPM").SelectedValue)
        Me.mEndTime = Me.CollectEndTime(Me.mForm.FindControls("ddlEndHour").Text, Me.mForm.FindControls("ddlEndMinute").SelectedValue, Me.mForm.FindControls("ddlEndAMPM").SelectedValue)
    End Sub

    Private Function CollectIDsSelected(ByVal filterList As ListItemCollection) As Long()
        If filterList(0).Selected Then
            Return New Long() {filterList(0).Value}
        Else
            Dim idsSelected As New List(Of Long)
            For Each item As ListItem In filterList
                If item.Selected Then
                    idsSelected.Add(item.Value)
                End If
            Next
            Return idsSelected.ToArray
        End If
    End Function

    Private Function CollectStartDate(ByVal year As String, ByVal month As String, ByVal day As String) As Date
        If year = String.Empty AndAlso month = String.Empty AndAlso day = String.Empty Then
            Return New Date(1900, 1, 1)
        Else
            Return New Date(year, month, day)
        End If
    End Function

    Private Function CollectEndDate(ByVal year As String, ByVal month As String, ByVal day As String) As Date
        If year = String.Empty AndAlso month = String.Empty AndAlso day = String.Empty Then
            Return Date.MaxValue.Date
        Else
            Return New Date(year, month, day)
        End If
    End Function

    Private Function CollectStartTime(ByVal hour As String, ByVal minute As String, ByVal ampm As String) As Date
        If hour = String.Empty AndAlso minute = String.Empty AndAlso ampm = String.Empty Then
            Return Date.MinValue
        Else
            Return "0001-01-01 " & hour & ":" & minute & " " & ampm
        End If
    End Function

    Private Function CollectEndTime(ByVal hour As String, ByVal minute As String, ByVal ampm As String) As Date
        If hour = String.Empty AndAlso minute = String.Empty AndAlso ampm = String.Empty Then
            Return Date.MaxValue
        Else
            Return "0001-01-01 " & hour & ":" & minute & " " & ampm
        End If
    End Function

    Public Sub OnSubmitQuery()
        Me.CollectFiltersValuesSelected()
        Me.mDataList = Me.GetDataList(Me.mAdAccountIDs, Me.mAdProjectIDs, Me.mStartDate, Me.mEndDate, Me.mStartTime, Me.mEndTime)
        Me.PopulateDataList("Advertiser", "ASC", 0)
    End Sub

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

    Public Sub OnAdAccount(ByVal adAccountID As String)
        Me.mForm.Session("ID1") = adAccountID
        Me.mForm.Response.Redirect("~/Forms/frmAdvertiserAccount.aspx")
    End Sub

    Public Sub OnProject(ByVal projectId As Long)
        Me.mForm.Session("ID1") = projectId
        Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdvertiserProject.aspx")
    End Sub

#End Region

#Region " Data Methods "

    Private Sub PopulateDataList(ByVal SortExpression As String, ByVal SortDirection As String, ByVal PageIndex As Integer)
        Me.mPageIndex = PageIndex
        Me.mSortDirection = SortDirection
        Me.mSortExpression = SortExpression
        Me.LoadGridView(Me.mForm.FindControls("grdProjectHistory"))
    End Sub

    Private Sub PopulateDataList(ByVal PageIndex As Integer)
        Me.mPageIndex = PageIndex
        Me.LoadGridView(Me.mForm.FindControls("grdProjectHistory"))
    End Sub

    Private Function GetAdAccountInfoList() As AdAccountData()
        Try
            Return ClsSessionAdmin.GetAdAccountInfoList()
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return New AdAccountData() {}
        End Try
    End Function

    Private Function GetAdProjectInfoList(ByVal idAdAccount As Long) As AdProjectData()
        Try
            Return ClsSessionAdmin.GetAdProjectInfoList(idAdAccount)
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return New AdProjectData() {}
        End Try
    End Function

    Private Function GetDataList(ByVal idAdAccounts() As Long, ByVal idAdProjects() As Long, ByVal fromDate As Date, ByVal toDate As Date, ByVal fromTime As Date, ByVal toTime As Date) As AdHistoryData()
        Try
            Return ClsSessionAdmin.GetAdHistoryList(idAdAccounts, idAdProjects, fromDate, toDate, fromTime, toTime)
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return New AdHistoryData() {}
        End Try
    End Function

#End Region

End Class
