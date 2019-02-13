Imports SCT.Library
Imports System.Web.UI.WebControls

Public Class DFClsAdChangeNoteInformation

#Region " Private Fields "

    Private mForm As Object
    Private mData As AdNoteData

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
        Me.mForm.Session("ValuePath") = "frmAdNotes"

        Me.ApplyPageAuthorizationRules()

        If (Not IsNumeric(Me.mForm.Session("NoteID"))) OrElse Me.mForm.Session("NoteID") = 0 Then
            Me.mData = New AdNoteData
            Me.ApplyAddFormAuthorizationRules(False)
            Me.ClearAddForm()
        Else
            Me.mData = Me.GetData(Me.mForm.Session("NoteID"))
            If Me.mData IsNot Nothing Then
                Me.ApplyAddFormAuthorizationRules(True)
                Me.PopulateData()
            End If
        End If
    End Sub

    Public Sub ApplyPageAuthorizationRules()
        If Not WebSite.ClsSessionAdmin.IsValidForm("frmAdChangeNoteInformation", "txtAdNoteID", "ddlAdNoteContact", "ddlAdNoteEnteredDate", "txtAdNoteDescription") Then Me.mForm.Response.Redirect("~/Forms/frmMain.aspx")
        Me.mForm.FindControls("cmdOK").Enabled = False
    End Sub

    Public Sub ApplyAddFormAuthorizationRules(ByVal editMode As Boolean)
        Me.mEditMode = editMode
        Me.SetFormTitle()
        Me.mForm.FindControls("cmdOK").Enabled = True
    End Sub

    Public Function ValidateEnteredDateRequered() As Boolean
        Return Me.mForm.FindControls("ddlAdNoteEnteredYear").SelectedValue & "-" & Me.mForm.FindControls("ddlAdNoteEnteredMonth").SelectedValue & "-" & Me.mForm.FindControls("ddlAdNoteEnteredDay").SelectedValue <> "Year-Month-Day"
    End Function

    Public Function ValidateEnteredDateValid() As Boolean
        Dim enteredDate As String = Me.mForm.FindControls("ddlAdNoteEnteredYear").SelectedValue & "-" & Me.mForm.FindControls("ddlAdNoteEnteredMonth").SelectedValue & "-" & Me.mForm.FindControls("ddlAdNoteEnteredDay").SelectedValue
        Return enteredDate = "Year-Month-Day" OrElse IsDate(enteredDate)
    End Function

    Private Sub SetFormTitle()
        If Me.mEditMode Then
            Me.mForm.FindControls("lblTitle").Text = "Edit Note Information"
        Else
            Me.mForm.FindControls("lblTitle").Text = "Add New Note Information"
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

    Public Sub OnOk()
        If Me.SaveData() Then
            Me.mForm.FindControls("MsgBox").ShowMessage("Saved Note Information.")
            Me.ApplyAddFormAuthorizationRules(True)
            Me.PopulateData()
        End If
    End Sub

    Public Sub OnReturn()
        Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdNotes.aspx")
    End Sub

#End Region

#Region " Data Methods "

    Private Sub ClearAddForm()

        Me.mForm.FindControls("txtAdNoteID").Text = String.Empty

        Me.LoadCombo(Me.mForm.FindControls("ddlAdNoteContact"), Me.GetComboDataSources(Me.GetContactInfoList(WebSite.ClsSessionAdmin.GetSessionUserID), "[Select a Contact]"), 0)

        Me.LoadDate(Me.mForm.FindControls("ddlAdNoteEnteredYear"), Me.mForm.FindControls("ddlAdNoteEnteredMonth"), Me.mForm.FindControls("ddlAdNoteEnteredDay"), New Date(1900, 1, 1, 0, 1, 0))
        Me.mForm.FindControls("txtAdNoteDescription").Text = String.Empty

        Me.mForm.FindControls("txtAdNoteDescription").Focus()
    End Sub

    Private Sub PopulateData()

        Me.mForm.FindControls("txtAdNoteID").Text = Me.mData.ID

        Me.LoadCombo(Me.mForm.FindControls("ddlAdNoteContact"), Me.GetComboDataSources(Me.GetContactInfoList(WebSite.ClsSessionAdmin.GetSessionUserID), "[Select a Contact]"), Me.mData.Contact.ID)

        Me.LoadDate(Me.mForm.FindControls("ddlAdNoteEnteredYear"), Me.mForm.FindControls("ddlAdNoteEnteredMonth"), Me.mForm.FindControls("ddlAdNoteEnteredDay"), Me.mData.DateEntered)
        Me.mForm.FindControls("txtAdNoteDescription").Text = Me.mData.Description

        Me.mForm.FindControls("txtAdNoteDescription").Focus()
    End Sub

    Private Function CollectData() As AdNoteNewData
        Dim formData As New AdNoteNewData

        formData.ID.SetValues("txtAdNoteID", True, Me.mData.ID, CLng(Me.CollectID(Me.mForm.FindControls("txtAdNoteID").Text)))

        formData.Contact.ID.SetValues("ddlAdNoteContact", False, Me.mData.Contact.ID, CLng(Me.mForm.FindControls("ddlAdNoteContact").SelectedItem.Value))
        formData.Contact.FullName.SetValues("ddlAdNoteContact", False, Me.mData.Contact.FullName, Me.mForm.FindControls("ddlAdNoteContact").SelectedItem.Text)

        formData.DateEntered.SetValues("ddlAdNoteEnteredDate", False, Me.mData.DateEntered, Me.CollectDate(Me.mForm.FindControls("ddlAdNoteEnteredYear").SelectedValue, Me.mForm.FindControls("ddlAdNoteEnteredMonth").SelectedValue, Me.mForm.FindControls("ddlAdNoteEnteredDay").SelectedValue))
        formData.Description.SetValues("txtAdNoteDescription", False, Me.mData.Description, Me.mForm.FindControls("txtAdNoteDescription").Text)

        Return formData
    End Function

    Private Function CollectID(ByVal text As String) As String
        If text = String.Empty Then
            Return "0"
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

    Private Function CollectDataID(ByVal noteID As String) As AdNoteNewData
        Dim formData As New AdNoteNewData
        formData.ID.SetValues("txtAdNoteID", True, 0, CLng(noteID))
        Return formData
    End Function

    Private Function GetContactInfoList(ByVal accountID As Long) As AdContactData()
        Try
            Return WebSite.ClsSessionAdmin.GetAdContactInfoList(accountID)
        Catch SysEx As Exception
            Return New AdContactData() {}
        End Try
    End Function

    Private Function GetData(ByVal noteID As String) As AdNoteData
        Try
            Return WebSite.ClsSessionAdmin.GetAdNote(Me.CollectDataID(noteID))
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return Nothing
        End Try
    End Function

    Private Function SaveData() As Boolean
        Try
            If Me.mEditMode Then
                Me.mData = WebSite.ClsSessionAdmin.EditAdNote(Me.CollectData)
            Else
                Me.mData = WebSite.ClsSessionAdmin.AddAdNote(Me.CollectData)
            End If
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error saving: " & SysEx.Message)
            Return False
        End Try
    End Function

#End Region

End Class
