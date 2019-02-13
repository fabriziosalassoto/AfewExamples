Imports SCT.Library
Imports System.Globalization
Imports System.Web.UI.WebControls

Public Class DFClsAdChangeProjectInformation

#Region " Private Fields "

    Private mForm As Object
    Private mData As AdProjectData

    Private mEditMode As Boolean

#End Region

#Region " Properties And Methods "

    Public WriteOnly Property Form() As Object
        Set(ByVal value As Object)
            Me.mForm = value
        End Set
    End Property

    Public Sub New(ByRef form As Object)
        Me.mForm = form
        Me.mForm.Session("ValuePath") = "frmAdProjects"

        Me.ApplyPageAuthorizationRules()

        If (Not IsNumeric(Me.mForm.Session("projectID"))) OrElse Me.mForm.Session("projectID") = 0 Then
            Me.mData = New AdProjectData
            Me.ApplyAddFormAuthorizationRules(False)
            Me.ClearAddForm()
        Else
            Me.mData = Me.GetData(Me.mForm.Session("projectID"))
            If Me.mData IsNot Nothing Then
                Me.ApplyAddFormAuthorizationRules(True)
                Me.PopulateData()
            End If
        End If
    End Sub

    Public Sub ApplyPageAuthorizationRules()
        If Not WebSite.ClsSessionAdmin.IsValidForm("frmAdChangeProjectInformation", "txtAdProjectID", "ddlAdProjectContact", "txtAdProjectADUrl", "txtAdProjectDescription", "txtAdProjectHeight", "txtAdProjectWidth", "ddlAdProjectRunStartDate", "ddlAdProjectRunEndDate", "ddlAdProjectStartTime", "ddlAdProjectEndTime", "txtAdProjectMinDisplay", "txtAdProjectMaxDisplay", "txtAdProjectMinPerDay", "txtAdProjectMaxPerDay", "ddlAdProjectMinAge", "ddlAdProjectMaxAge", "ddlAdProjectSex", "ddlAdProjectOccupation", "ddlAdProjectCountry", "ddlAdProjectState") Then Me.mForm.Response.Redirect("~/Forms/frmMain.aspx")
        Me.mForm.FindControls("cmdOK").Enabled = False
    End Sub

    Public Sub ApplyAddFormAuthorizationRules(ByVal editMode As Boolean)
        Me.mEditMode = editMode
        Me.SetFormTitle()
        Me.mForm.FindControls("cmdOK").Enabled = True
    End Sub

    Public Function ValidateStarDate() As Boolean
        Dim startDate As String = Me.mForm.FindControls("ddlAdProjectStartYear").SelectedValue & "-" & Me.mForm.FindControls("ddlAdProjectStartMonth").SelectedValue & "-" & Me.mForm.FindControls("ddlAdProjectStartDay").SelectedValue
        Return startDate = "Year-Month-Day" OrElse IsDate(startDate)
    End Function

    Public Function ValidateEndDate() As Boolean
        Dim endDate As String = Me.mForm.FindControls("ddlAdProjectEndYear").SelectedValue & "-" & Me.mForm.FindControls("ddlAdProjectEndMonth").SelectedValue & "-" & Me.mForm.FindControls("ddlAdProjectEndDay").SelectedValue
        Return endDate = "Year-Month-Day" OrElse IsDate(endDate)
    End Function

    Public Function ValidateStartDateGTEndDate() As Boolean
        Dim startDateText As String = Me.mForm.FindControls("ddlAdProjectStartYear").SelectedValue & "-" & Me.mForm.FindControls("ddlAdProjectStartMonth").SelectedValue & "-" & Me.mForm.FindControls("ddlAdProjectStartDay").SelectedValue
        Dim endDateText As String = Me.mForm.FindControls("ddlAdProjectEndYear").SelectedValue & "-" & Me.mForm.FindControls("ddlAdProjectEndMonth").SelectedValue & "-" & Me.mForm.FindControls("ddlAdProjectEndDay").SelectedValue
        If IsDate(startDateText) AndAlso IsDate(endDateText) Then
            Dim startDate As Date = startDateText
            Dim endDate As Date = endDateText

            If startDate > endDate Then
                Return False
            Else
                Return True
            End If
        Else
            Return True
        End If
    End Function

    Public Function ValidateStartTime() As Boolean
        Dim startTime As String = Me.mForm.FindControls("ddlAdProjectStartHour").SelectedValue & ":" & Me.mForm.FindControls("ddlAdProjectStartMinute").SelectedValue & " " & Me.mForm.FindControls("ddlAdProjectStartAMPM").SelectedValue
        Return startTime = "Hour:Minute a.m./p.m." OrElse IsDate(startTime)
    End Function

    Public Function ValidateEndTime() As Boolean
        Dim endTime As String = Me.mForm.FindControls("ddlAdProjectEndHour").SelectedValue & ":" & Me.mForm.FindControls("ddlAdProjectEndMinute").SelectedValue & " " & Me.mForm.FindControls("ddlAdProjectEndAMPM").SelectedValue
        Return endTime = "Hour:Minute a.m./p.m." OrElse IsDate(endTime)
    End Function

    Public Function ValidateStartTimeGTEndTime() As Boolean
        Dim startTimeText As String = Me.mForm.FindControls("ddlAdProjectStartHour").SelectedValue & ":" & Me.mForm.FindControls("ddlAdProjectStartMinute").SelectedValue & " " & Me.mForm.FindControls("ddlAdProjectStartAMPM").SelectedValue
        Dim endTimeText As String = Me.mForm.FindControls("ddlAdProjectEndHour").SelectedValue & ":" & Me.mForm.FindControls("ddlAdProjectEndMinute").SelectedValue & " " & Me.mForm.FindControls("ddlAdProjectEndAMPM").SelectedValue

        If IsDate(startTimeText) AndAlso IsDate(endTimeText) Then
            Dim startTime As Date = startTimeText
            Dim endTime As Date = endTimeText

            If startTime > endTime Then
                Return False
            Else
                Return True
            End If
        Else
            Return True
        End If
    End Function

    Private Sub SetFormTitle()
        If Me.mEditMode Then
            Me.mForm.FindControls("lblTitle").Text = "Edit Project Information"
        Else
            Me.mForm.FindControls("lblTitle").Text = "Add New Project Information"
        End If
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

    Private Sub LoadDate(ByVal year As DropDownList, ByVal month As DropDownList, ByVal day As DropDownList, ByVal dateValue As Date)
        Me.LoadCombo(year, Me.GetYearData, "1900")
        If dateValue.ToString("yyyy-MM-dd") = "1900-01-01" Then
            year.SelectedIndex = 0
            month.SelectedIndex = 0
            day.SelectedIndex = 0
        Else
            year.SelectedValue = dateValue.Year
            month.SelectedIndex = dateValue.Month
            day.SelectedIndex = dateValue.Day
        End If
    End Sub

    Private Sub LoadTime(ByVal hour As DropDownList, ByVal minute As DropDownList, ByVal ampm As DropDownList, ByVal timeValue As Date)
        If timeValue.ToString("HH:mm") = "00:01" OrElse timeValue.ToString("HH:mm") = "23:59" Then
            hour.SelectedIndex = 0
            minute.SelectedIndex = 0
            ampm.SelectedIndex = 0
        Else
            If timeValue.Hour = 12 Or timeValue.Hour = 0 Then
                hour.SelectedIndex = 12
            Else
                hour.SelectedIndex = timeValue.Hour Mod 12
            End If
            minute.SelectedIndex = (timeValue.Minute / 5) + 1
            ampm.SelectedIndex = Fix(timeValue.Hour / 12) + 1
        End If
    End Sub

    Private Function GetComboDataSources(ByVal InfoList() As AdContactData, ByVal FirstItemText As String) As DataTable
        Dim table As New DataTable

        table.Columns.Add("FullName", GetType(String))
        table.Columns.Add("ID", GetType(String))

        table.DefaultView.Sort = "FullName"

        table.Rows.Add(New String() {FirstItemText, "0"})
        For Each Info As AdContactData In InfoList
            table.Rows.Add(New String() {Info.FullName, Info.ID.ToString})
        Next

        Return table
    End Function

    Private Function GetYearData() As DataTable
        Dim table As New DataTable

        table.Columns.Add("Text", GetType(String))
        table.Columns.Add("Value", GetType(String))
        table.Rows.Add(New Object() {"Year", "Year"})

        For i As Integer = 2000 To Date.Today.Year + 10
            table.Rows.Add(New Object() {i, i})
        Next
        Return table
    End Function

    Private Function GetAgeData() As DataTable
        Dim table As New DataTable

        table.Columns.Add("Text", GetType(String))
        table.Columns.Add("Value", GetType(String))
        table.Rows.Add(New Object() {String.Empty, String.Empty})

        For i As Integer = 1 To 99
            table.Rows.Add(New Object() {i.ToString("00"), i})
        Next
        Return table
    End Function

    Private Function GetSexData() As DataTable
        Dim table As New DataTable
        table.ReadXml(Me.mForm.MapPath("~") & "/App_Data/Sex.xml")
        table.Rows.Add(New Object() {String.Empty, String.Empty})
        table.DefaultView.Sort = "Text"
        Return table
    End Function

    Private Function GetOccupationData() As DataTable
        Dim table As New DataTable
        table.ReadXml(Me.mForm.MapPath("~") & "/App_Data/Occupations.xml")
        table.Rows.Add(New Object() {String.Empty, String.Empty})
        table.DefaultView.Sort = "Description"
        Return table
    End Function

    Private Function GetCountryData() As DataTable
        Dim reginfo As RegionInfo
        Dim cultInfoList() As CultureInfo

        Dim table As New DataTable
        Dim row As DataRow

        table.Columns.Add("Text", GetType(String))
        table.Columns.Add("Value", GetType(String))

        table.Rows.Add(New Object() {String.Empty, String.Empty})

        table.DefaultView.Sort = "Text"
        table.PrimaryKey = New DataColumn() {table.Columns("Text")}

        cultInfoList = CultureInfo.GetCultures(CultureTypes.AllCultures)
        For Each cultInfo As CultureInfo In cultInfoList
            Try
                reginfo = New RegionInfo(cultInfo.LCID)
                row = table.NewRow
                row("Text") = reginfo.EnglishName
                row("Value") = reginfo.ThreeLetterISORegionName
                If Not table.Rows.Contains(row) Then
                    table.Rows.Add(row)
                End If
            Catch ex As Exception
            End Try
        Next
        Return table
    End Function

    Private Function GetStateData(ByVal filter As String) As DataTable
        Dim table As New DataTable
        table.ReadXml(Me.mForm.MapPath("~") & "/App_Data/States.xml")
        table.Rows.Add(New Object() {String.Empty, String.Empty, filter})
        table.DefaultView.Sort = "Name"
        table.DefaultView.RowFilter = "CodeCountry = '" & filter & "'"
        Return table
    End Function

    Public Sub OnSelectedCountry(ByVal countryCode As String)
        Me.LoadCombo(Me.mForm.FindControls("ddlAdProjectState"), Me.GetStateData(countryCode), String.Empty)
        Me.mForm.FindControls("ddlAdProjectState").Focus()
    End Sub

    Public Sub OnOk()
        If Me.SaveData() Then
            Me.mForm.FindControls("MsgBox").ShowMessage("Saved Project Information.")
            Me.ApplyAddFormAuthorizationRules(True)
            Me.PopulateData()
        End If
    End Sub

    Public Sub OnReturn()
        Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdProjects.aspx")
    End Sub

#End Region

#Region " Data Methods "

    Private Sub ClearAddForm()
        Me.mForm.FindControls("txtAdProjectID").Text = String.Empty

        Me.LoadCombo(Me.mForm.FindControls("ddlAdProjectContact"), Me.GetComboDataSources(Me.GetContactInfoList(WebSite.ClsSessionAdmin.GetSessionUserID), "[Select a Contact]"), 0)

        Me.mForm.FindControls("txtAdProjectADUrl").Text = String.Empty
        Me.mForm.FindControls("txtAdProjectDescription").Text = String.Empty
        Me.mForm.FindControls("txtAdProjectHeight").Text = String.Empty
        Me.mForm.FindControls("txtAdProjectWidth").Text = String.Empty

        Me.LoadDate(Me.mForm.FindControls("ddlAdProjectStartYear"), Me.mForm.FindControls("ddlAdProjectStartMonth"), Me.mForm.FindControls("ddlAdProjectStartDay"), New Date(1900, 1, 1, 0, 1, 0))
        Me.LoadDate(Me.mForm.FindControls("ddlAdProjectEndYear"), Me.mForm.FindControls("ddlAdProjectEndMonth"), Me.mForm.FindControls("ddlAdProjectEndDay"), New Date(1900, 1, 1, 0, 1, 0))

        Me.LoadTime(Me.mForm.FindControls("ddlAdProjectStartHour"), Me.mForm.FindControls("ddlAdProjectStartMinute"), Me.mForm.FindControls("ddlAdProjectStartAMPM"), New Date(1900, 1, 1, 0, 1, 0))
        Me.LoadTime(Me.mForm.FindControls("ddlAdProjectEndHour"), Me.mForm.FindControls("ddlAdProjectEndMinute"), Me.mForm.FindControls("ddlAdProjectEndAMPM"),New Date(1900, 1, 1, 0, 1, 0))

        Me.mForm.FindControls("txtAdProjectMinDisplay").Text = String.Empty
        Me.mForm.FindControls("txtAdProjectMaxDisplay").Text = String.Empty
        Me.mForm.FindControls("txtAdProjectMinPerDay").Text = String.Empty
        Me.mForm.FindControls("txtAdProjectMaxPerDay").Text = String.Empty

        Me.LoadCombo(Me.mForm.FindControls("ddlAdProjectMinAge"), Me.GetAgeData, String.Empty)
        Me.LoadCombo(Me.mForm.FindControls("ddlAdProjectMaxAge"), Me.GetAgeData, String.Empty)
        Me.LoadCombo(Me.mForm.FindControls("ddlAdProjectSex"), Me.GetSexData, String.Empty)
        Me.LoadCombo(Me.mForm.FindControls("ddlAdProjectOccupation"), Me.GetOccupationData, String.Empty)
        Me.LoadCombo(Me.mForm.FindControls("ddlAdProjectCountry"), Me.GetCountryData, String.Empty)
        Me.LoadCombo(Me.mForm.FindControls("ddlAdProjectState"), Me.GetStateData(String.Empty), String.Empty)

        Me.mForm.FindControls("txtAdProjectADUrl").Focus()
    End Sub

    Private Sub PopulateData()
        Me.mForm.FindControls("txtAdProjectID").Text = Me.mData.ID

        Me.LoadCombo(Me.mForm.FindControls("ddlAdProjectContact"), Me.GetComboDataSources(Me.GetContactInfoList(WebSite.ClsSessionAdmin.GetSessionUserID), "[Select a Contact]"), Me.mData.Contact.ID)

        Me.mForm.FindControls("txtAdProjectADUrl").Text = Me.mData.ADUrl
        Me.mForm.FindControls("txtAdProjectDescription").Text = Me.mData.ProjectDescription
        Me.mForm.FindControls("txtAdProjectHeight").Text = Me.mData.ADHeight.ToString("###0")
        Me.mForm.FindControls("txtAdProjectWidth").Text = Me.mData.ADWidth.ToString("###0")

        Me.LoadDate(Me.mForm.FindControls("ddlAdProjectStartYear"), Me.mForm.FindControls("ddlAdProjectStartMonth"), Me.mForm.FindControls("ddlAdProjectStartDay"), Me.mData.RunStartDate)
        Me.LoadDate(Me.mForm.FindControls("ddlAdProjectEndYear"), Me.mForm.FindControls("ddlAdProjectEndMonth"), Me.mForm.FindControls("ddlAdProjectEndDay"), Me.mData.RunEndDate)

        Me.LoadTime(Me.mForm.FindControls("ddlAdProjectStartHour"), Me.mForm.FindControls("ddlAdProjectStartMinute"), Me.mForm.FindControls("ddlAdProjectStartAMPM"), Me.mData.StartTimeBasedOnSubscribersTime)
        Me.LoadTime(Me.mForm.FindControls("ddlAdProjectEndHour"), Me.mForm.FindControls("ddlAdProjectEndMinute"), Me.mForm.FindControls("ddlAdProjectEndAMPM"), Me.mData.EndTimeBasedOnSubscribersTime)

        Me.mForm.FindControls("txtAdProjectMinDisplay").Text = Me.mData.MinDisplays.ToString("#,###,###,###")
        Me.mForm.FindControls("txtAdProjectMaxDisplay").Text = Me.mData.MaxDisplays.ToString("#,###,###,###")
        Me.mForm.FindControls("txtAdProjectMinPerDay").Text = Me.mData.MinPerDay.ToString("#,###,###,###")
        Me.mForm.FindControls("txtAdProjectMaxPerDay").Text = Me.mData.MaxPerDay.ToString("#,###,###,###")

        Me.LoadCombo(Me.mForm.FindControls("ddlAdProjectMinAge"), Me.GetAgeData, Me.mData.MinAge)
        Me.LoadCombo(Me.mForm.FindControls("ddlAdProjectMaxAge"), Me.GetAgeData, Me.mData.MaxAge)
        Me.LoadCombo(Me.mForm.FindControls("ddlAdProjectSex"), Me.GetSexData, Me.mData.Sex)
        Me.LoadCombo(Me.mForm.FindControls("ddlAdProjectOccupation"), Me.GetOccupationData, Me.mData.Occupation)
        Me.LoadCombo(Me.mForm.FindControls("ddlAdProjectCountry"), Me.GetCountryData, Me.mData.Country)
        Me.LoadCombo(Me.mForm.FindControls("ddlAdProjectState"), Me.GetStateData(Me.mData.Country), Me.mData.State)

        Me.mForm.FindControls("txtAdProjectADUrl").Focus()
    End Sub

    Private Function CollectData() As AdProjectNewData
        Dim formData As New AdProjectNewData

        formData.ID.SetValues("txtAdProjectID", True, Me.mData.ID, CLng(Me.CollectID(Me.mForm.FindControls("txtAdProjectID").Text)))

        formData.Advertiser.ID.SetValues("", False, Me.mData.Advertiser.ID, CLng(WebSite.ClsSessionAdmin.GetSessionUserID))
        formData.Advertiser.CompanyName.SetValues("", False, Me.mData.Advertiser.CompanyName, WebSite.ClsSessionAdmin.GetSessionUserName)

        formData.Contact.ID.SetValues("ddlAdProjectContact", False, Me.mData.Contact.ID, CLng(Me.mForm.FindControls("ddlAdProjectContact").SelectedItem.Value))
        formData.Contact.FullName.SetValues("ddlAdProjectContact", False, Me.mData.Contact.FullName, Me.mForm.FindControls("ddlAdProjectContact").SelectedItem.Text)

        formData.ADUrl.SetValues("txtAdProjectADUrl", False, Me.mData.ADUrl, Me.mForm.FindControls("txtAdProjectADUrl").Text)
        formData.ProjectDescription.SetValues("txtAdProjectDescription", False, Me.mData.ProjectDescription, Me.mForm.FindControls("txtAdProjectDescription").Text)
        formData.ADHeight.SetValues("txtAdProjectHeight", False, Me.mData.ADHeight, Me.CollectNumeric(100, Me.mForm.FindControls("txtAdProjectHeight").Text))
        formData.ADWidth.SetValues("txtAdProjectWidth", False, Me.mData.ADWidth, Me.CollectNumeric(100, Me.mForm.FindControls("txtAdProjectWidth").Text))

        formData.RunStartDate.SetValues("ddlAdProjectRunStartDate", False, Me.mData.RunStartDate, Me.CollectDate(Me.mForm.FindControls("ddlAdProjectStartYear").SelectedValue, Me.mForm.FindControls("ddlAdProjectStartMonth").SelectedValue, Me.mForm.FindControls("ddlAdProjectStartDay").SelectedValue))
        formData.RunEndDate.SetValues("ddlAdProjectRunEndDate", False, Me.mData.RunEndDate, Me.CollectDate(Me.mForm.FindControls("ddlAdProjectEndYear").SelectedValue, Me.mForm.FindControls("ddlAdProjectEndMonth").SelectedValue, Me.mForm.FindControls("ddlAdProjectEndDay").SelectedValue))

        formData.StartTimeBasedOnSubscribersTime.SetValues("ddlAdProjectStartTime", False, Me.mData.StartTimeBasedOnSubscribersTime, Me.CollectTime(True, Me.mForm.FindControls("ddlAdProjectStartHour").SelectedValue, Me.mForm.FindControls("ddlAdProjectStartMinute").SelectedValue, Me.mForm.FindControls("ddlAdProjectStartAMPM").SelectedValue))
        formData.EndTimeBasedOnSubscribersTime.SetValues("ddlAdProjectEndTime", False, Me.mData.EndTimeBasedOnSubscribersTime, Me.CollectTime(False, Me.mForm.FindControls("ddlAdProjectEndHour").SelectedValue, Me.mForm.FindControls("ddlAdProjectEndMinute").SelectedValue, Me.mForm.FindControls("ddlAdProjectEndAMPM").SelectedValue))

        formData.MinDisplays.SetValues("txtAdProjectMinDisplay", False, Me.mData.MinDisplays, Me.CollectNumeric(0, Me.mForm.FindControls("txtAdProjectMinDisplay").Text))
        formData.MaxDisplays.SetValues("txtAdProjectMaxDisplay", False, Me.mData.MaxDisplays, Me.CollectNumeric(0, Me.mForm.FindControls("txtAdProjectMaxDisplay").Text))
        formData.MinPerDay.SetValues("txtAdProjectMinPerDay", False, Me.mData.MinPerDay, Me.CollectNumeric(0, Me.mForm.FindControls("txtAdProjectMinPerDay").Text))
        formData.MaxPerDay.SetValues("txtAdProjectMaxPerDay", False, Me.mData.MaxPerDay, Me.CollectNumeric(0, Me.mForm.FindControls("txtAdProjectMaxPerDay").Text))

        formData.MinAge.SetValues("ddlAdProjectMinAge", False, Me.mData.MinAge, Me.mForm.FindControls("ddlAdProjectMinAge").SelectedItem.Value)
        formData.MaxAge.SetValues("ddlAdProjectMaxAge", False, Me.mData.MaxAge, Me.mForm.FindControls("ddlAdProjectMaxAge").SelectedItem.Value)
        formData.Sex.SetValues("ddlAdProjectSex", False, Me.mData.Sex, Me.mForm.FindControls("ddlAdProjectSex").SelectedItem.Value)
        formData.Occupation.SetValues("ddlAdProjectOccupation", False, Me.mData.Occupation, Me.mForm.FindControls("ddlAdProjectOccupation").SelectedItem.Value)
        formData.Country.SetValues("ddlAdProjectCountry", False, Me.mData.Country, Me.mForm.FindControls("ddlAdProjectCountry").SelectedItem.Value)
        formData.State.SetValues("ddlAdProjectState", False, Me.mData.State, Me.mForm.FindControls("ddlAdProjectState").SelectedItem.Value)

        Return formData
    End Function

    Private Function CollectID(ByVal text As String) As String
        If text = String.Empty Then
            Return "0"
        End If
        Return text
    End Function

    Private Function CollectNumeric(ByVal minValue As Integer, ByVal text As String) As Integer
        If text = String.Empty OrElse Not IsNumeric(text) Then
            Return minValue
        End If
        Return text
    End Function

    Private Function CollectDate(ByVal year As String, ByVal month As String, ByVal day As String) As Date
        If year = "Year" AndAlso month = "Month" AndAlso day = "Day" Then
            Return "1900-01-01"
        Else
            Return year & "-" & month & "-" & day
        End If
    End Function

    Private Function CollectTime(ByVal startTime As Boolean, ByVal hour As String, ByVal minute As String, ByVal ampm As String) As Date
        If hour = "Hour" AndAlso minute = "Minute" AndAlso ampm = "a.m./p.m." Then
            If startTime Then
                Return New Date(1900, 1, 1, 0, 1, 0)
            Else
                Return New Date(1900, 1, 1, 23, 59, 0)
            End If
        Else
            Return "1900-01-01 " & hour & ":" & minute & " " & ampm
        End If
    End Function

    Private Function CollectDataID(ByVal projectID As String) As AdProjectNewData
        Dim formData As New AdProjectNewData
        formData.ID.SetValues("txtAdProjectID", True, 0, CLng(projectID))
        Return formData
    End Function

    Private Function GetContactInfoList(ByVal accountID As Long) As AdContactData()
        Try
            Return WebSite.ClsSessionAdmin.GetAdContactInfoList(accountID)
        Catch SysEx As Exception
            Return New AdContactData() {}
        End Try
    End Function

    Private Function GetData(ByVal projectID As String) As AdProjectData
        Try
            Return WebSite.ClsSessionAdmin.GetAdProject(Me.CollectDataID(projectID))
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return Nothing
        End Try
    End Function

    Private Function SaveData() As Boolean
        Try
            If Me.mEditMode Then
                Me.mData = WebSite.ClsSessionAdmin.EditAdProject(Me.CollectData)
            Else
                Me.mData = WebSite.ClsSessionAdmin.AddAdProject(Me.CollectData)
            End If
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error saving: " & SysEx.Message)
            Return False
        End Try
    End Function

#End Region

End Class
