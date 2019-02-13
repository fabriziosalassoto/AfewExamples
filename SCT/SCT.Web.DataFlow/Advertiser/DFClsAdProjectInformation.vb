Imports SCT.Library
Imports System.Globalization

Public Class DFClsAdProjectInformation

#Region " Private Fields "

    Private mForm As Object
    Private mData As AdProjectData

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

        ApplyPageAuthorizationRules()

        If IsNumeric(Me.mForm.Session("projectID")) AndAlso Me.mForm.Session("projectID") <> 0 Then
            Me.mData = Me.GetData(Me.mForm.Session("projectID"))
            If Me.mData IsNot Nothing Then
                Me.PopulateData()
                Me.EnableMenuItem(True)
            End If
        End If
    End Sub

    Public Sub ApplyPageAuthorizationRules()
        If Not WebSite.ClsSessionAdmin.IsValidForm("frmAdProjectInformation", "txtAdProjectID", "txtAdProjectContact", "txtAdProjectADUrl", "txtAdProjectDescription", "txtAdProjectHeight", "txtAdProjectWidth", "txtAdProjectRunStartDate", "txtAdProjectRunEndDate", "txtAdProjectStartTime", "txtAdProjectEndTime", "txtAdProjectMinDisplay", "txtAdProjectMaxDisplay", "txtAdProjectMinPerDay", "txtAdProjectMaxPerDay", "txtAdProjectCountDisplayed", "txtAdProjectVerifiedDate", "txtAdProjectOnlineDate", "txtAdProjectMinAge", "txtAdProjectMaxAge", "txtAdProjectSex", "txtAdProjectOccupation", "txtAdProjectCountry", "txtAdProjectState") Then Me.mForm.Response.Redirect("~/Forms/frmMain.aspx")
        Me.EnableMenuItem(False)
    End Sub

    Public Sub EnableMenuItem(ByVal value As Boolean)
        Me.mForm.FindControls("mnuProject").FindItem("ProjectHistory").Enabled = value

        Me.mForm.FindControls("mnuContact").FindItem("Contact").Enabled = value

        Me.mForm.FindControls("mnuEditProject").FindItem("Edit").Enabled = value
        Me.mForm.FindControls("mnuEditProject").FindItem("Delete").Enabled = value
        Me.mForm.FindControls("mnuEditProject").FindItem("Return").Enabled = True
    End Sub

    Private Function GetSexDescription(ByVal value As String) As String
        Dim table As New DataTable
        table.ReadXml(Me.mForm.MapPath("~") & "/App_Data/Sex.xml")
        table.Rows.Add(New Object() {String.Empty, String.Empty})
        table.DefaultView.RowFilter = "Value = '" & value & "'"
        Return table.DefaultView.Item(0).Item("Text")
    End Function

    Private Function GetOccupationDescription(ByVal code As String) As String
        Dim table As New DataTable
        table.ReadXml(Me.mForm.MapPath("~") & "/App_Data/Occupations.xml")
        table.Rows.Add(New Object() {String.Empty, String.Empty})
        table.DefaultView.RowFilter = "Code = '" & code & "'"
        Return table.DefaultView.Item(0).Item("Description")
    End Function

    Private Function GetCountryName(ByVal value As String) As String
        Dim reginfo As RegionInfo
        Dim cultInfoList() As CultureInfo

        Dim table As New DataTable
        Dim row As DataRow

        table.Columns.Add("Text", GetType(String))
        table.Columns.Add("Value", GetType(String))
        table.Rows.Add(New Object() {String.Empty, String.Empty})

        table.DefaultView.RowFilter = "Value = '" & value & "'"
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
        Return table.DefaultView.Item(0).Item("Text")
    End Function

    Private Function GetStateName(ByVal codeCountry As String, ByVal codeState As String) As String
        Dim table As New DataTable
        table.ReadXml(Me.mForm.MapPath("~") & "/App_Data/States.xml")
        table.Rows.Add(New Object() {String.Empty, String.Empty, codeCountry})
        table.DefaultView.RowFilter = "CodeCountry = '" & codeCountry & "' and CodeState = '" & codeState & "'"
        Return table.DefaultView.Item(0).Item("Name")
    End Function

    Private Function GetDateFormat(ByVal dateValue As Date) As String
        If Not Format(dateValue, "yyyy-MM-dd") = "1900-01-01" Then
            Return Format(dateValue, "MMMM dd, yyyy")
        Else
            Return String.Empty
        End If
    End Function

    Private Function GetTimeFormat(ByVal dateValue As Date) As String
        If Not (Format(dateValue, "HH:mm") = "00:01" OrElse Format(dateValue, "HH:mm") = "23:59") Then
            Return Format(dateValue, "hh:mm tt")
        Else
            Return String.Empty
        End If
    End Function

    Public Sub OnProjectHistory()
        Me.mForm.Session("projectID") = Me.mData.ID
        Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdProjectHistory.aspx")
    End Sub

    Public Sub OnContact()
        Me.mForm.Session("contactID") = Me.mData.Contact.ID
        Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdContactInformation.aspx")
    End Sub

    Public Sub OnEditing()
        Me.mForm.Session("projectID") = Me.mData.ID
        Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdChangeProjectInformation.aspx")
    End Sub

    Public Sub OnDeleting()
        Me.mForm.FindControls("MsgBox").ShowConfirmation("Are you sure you want to delete this Project?\n\nNote: there is no undo.", "", True, False)
    End Sub

    Public Sub OnOkDelete()
        If Me.DeleteData Then
            Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdProjects.aspx")
        End If
    End Sub

    Public Sub OnReturn()
        Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdProjects.aspx")
    End Sub

#End Region

#Region " Data Methods "

    Private Sub PopulateData()
        Me.mForm.FindControls("txtAdProjectID").Text = Me.mData.ID
        Me.mForm.FindControls("txtAdProjectContact").Text = Me.mData.Contact.FullName
        Me.mForm.FindControls("txtAdProjectADUrl").Text = Me.mData.ADUrl
        Me.mForm.FindControls("txtAdProjectDescription").Text = Me.mData.ProjectDescription
        Me.mForm.FindControls("txtAdProjectHeight").Text = Me.mData.ADHeight.ToString("##########0")
        Me.mForm.FindControls("txtAdProjectWidth").Text = Me.mData.ADWidth.ToString("#########0")
        Me.mForm.FindControls("txtAdProjectRunStartDate").Text = Me.GetDateFormat(Me.mData.RunStartDate)
        Me.mForm.FindControls("txtAdProjectRunEndDate").Text = Me.GetDateFormat(Me.mData.RunEndDate)
        Me.mForm.FindControls("txtAdProjectStartTime").Text = Me.GetTimeFormat(Me.mData.StartTimeBasedOnSubscribersTime)
        Me.mForm.FindControls("txtAdProjectEndTime").Text = Me.GetTimeFormat(Me.mData.EndTimeBasedOnSubscribersTime)
        Me.mForm.FindControls("txtAdProjectMinDisplay").Text = Me.mData.MinDisplays.ToString("#,###,###,###")
        Me.mForm.FindControls("txtAdProjectMaxDisplay").Text = Me.mData.MaxDisplays.ToString("#,###,###,###")
        Me.mForm.FindControls("txtAdProjectMinPerDay").Text = Me.mData.MinPerDay.ToString("#,###,###,###")
        Me.mForm.FindControls("txtAdProjectMaxPerDay").Text = Me.mData.MaxPerDay.ToString("#,###,###,###")
        Me.mForm.FindControls("txtAdProjectCountDisplayed").Text = Me.mData.AdCountDisplayed.ToString("#,###,###,###,###,###,##0")
        Me.mForm.FindControls("txtAdProjectVerifiedDate").Text = Me.GetDateFormat(Me.mData.AdVerifiedDate)
        Me.mForm.FindControls("txtAdProjectOnlineDate").Text = Me.GetDateFormat(Me.mData.AdOnlineDate)
        Me.mForm.FindControls("txtAdProjectMinAge").Text = Me.mData.MinAge
        Me.mForm.FindControls("txtAdProjectMaxAge").Text = Me.mData.MaxAge
        Me.mForm.FindControls("txtAdProjectSex").Text = Me.GetSexDescription(Me.mData.Sex)
        Me.mForm.FindControls("txtAdProjectOccupation").Text = Me.GetOccupationDescription(Me.mData.Occupation)
        Me.mForm.FindControls("txtAdProjectCountry").Text = Me.GetCountryName(Me.mData.Country)
        Me.mForm.FindControls("txtAdProjectState").Text = Me.GetStateName(Me.mData.Country, Me.mData.State)
    End Sub

    Private Function CollectDataID() As AdProjectNewData
        Dim formData As New AdProjectNewData
        formData.ID.SetValues("txtAdProjectID", True, Me.mData.ID, Me.mData.ID)
        Return formData
    End Function

    Private Function CollectDataID(ByVal projectID As String) As AdProjectNewData
        Dim formData As New AdProjectNewData
        formData.ID.SetValues("txtAdProjectID", True, 0, CLng(projectID))
        Return formData
    End Function

    Private Function GetData(ByVal projectID As String) As AdProjectData
        Try
            Return WebSite.ClsSessionAdmin.GetAdProject(Me.CollectDataID(projectID))
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return Nothing
        End Try
    End Function

    Private Function DeleteData() As Boolean
        Try
            WebSite.ClsSessionAdmin.DeleteAdProject(Me.CollectDataID)
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error deleting: " & SysEx.Message)
            Return False
        End Try
    End Function

#End Region

End Class
