Imports SCT.Library
Imports System.Web.UI.WebControls

Public Class DFClsAdChangeToDoInformation

#Region " Private Fields "

    Private mForm As Object
    Private mData As AdToDoData

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
        Me.mForm.Session("ValuePath") = "frmAdToDos"

        Me.ApplyPageAuthorizationRules()

        If (Not IsNumeric(Me.mForm.Session("todoID"))) OrElse Me.mForm.Session("todoID") = 0 Then
            Me.mData = New AdToDoData
            Me.ApplyAddFormAuthorizationRules(False)
            Me.ClearAddForm()
        Else
            Me.mData = Me.GetData(Me.mForm.Session("todoID"))
            If Me.mData IsNot Nothing Then
                Me.ApplyAddFormAuthorizationRules(True)
                Me.PopulateData()
            End If
        End If
    End Sub

    Public Sub ApplyPageAuthorizationRules()
        If Not WebSite.ClsSessionAdmin.IsValidForm("frmAdChangeToDoInformation", "txtAdToDoID", "ddlAdToDoContact", "ddlAdToDoEnteredDate", "ddlAdToDoDueDate", "ddlAdToDoCompletedDate", "txtAdToDoTaskNotes", "ckbAdToDoCallBAckRecord") Then Me.mForm.Response.Redirect("~/Forms/frmMain.aspx")
        Me.mForm.FindControls("cmdOK").Enabled = False
    End Sub

    Public Sub ApplyAddFormAuthorizationRules(ByVal editMode As Boolean)
        Me.mEditMode = editMode
        Me.SetFormTitle()
        Me.mForm.FindControls("cmdOK").Enabled = True
    End Sub

    Public Function ValidateEnteredDateRequered() As Boolean
        Return Me.mForm.FindControls("ddlAdToDoEnteredYear").SelectedValue & "-" & Me.mForm.FindControls("ddlAdToDoEnteredMonth").SelectedValue & "-" & Me.mForm.FindControls("ddlAdToDoEnteredDay").SelectedValue <> "Year-Month-Day"
    End Function

    Public Function ValidateEnteredDateValid() As Boolean
        Return Me.ValidateDate(Me.mForm.FindControls("ddlAdToDoEnteredYear").SelectedValue & "-" & Me.mForm.FindControls("ddlAdToDoEnteredMonth").SelectedValue & "-" & Me.mForm.FindControls("ddlAdToDoEnteredDay").SelectedValue)
    End Function

    Public Function ValidateDueDateValid() As Boolean
        Return Me.ValidateDate(Me.mForm.FindControls("ddlAdToDoDueYear").SelectedValue & "-" & Me.mForm.FindControls("ddlAdToDoDueMonth").SelectedValue & "-" & Me.mForm.FindControls("ddlAdToDoDueDay").SelectedValue)
    End Function

    Public Function ValidateCompletedDateValid() As Boolean
        Return Me.ValidateDate(Me.mForm.FindControls("ddlAdToDoCompletedYear").SelectedValue & "-" & Me.mForm.FindControls("ddlAdToDoCompletedMonth").SelectedValue & "-" & Me.mForm.FindControls("ddlAdToDoCompletedDay").SelectedValue)
    End Function

    Private Function ValidateDate(ByVal dateText As String) As Boolean
        Return dateText = "Year-Month-Day" OrElse IsDate(dateText)
    End Function

    Public Function ValidateEnteredDateGTDueDate() As Boolean
        Return Me.ValidateMinDateGTMaxDate(Me.mForm.FindControls("ddlAdToDoEnteredYear").SelectedValue & "-" & Me.mForm.FindControls("ddlAdToDoEnteredMonth").SelectedValue & "-" & Me.mForm.FindControls("ddlAdToDoEnteredDay").SelectedValue, Me.mForm.FindControls("ddlAdToDoDueYear").SelectedValue & "-" & Me.mForm.FindControls("ddlAdToDoDueMonth").SelectedValue & "-" & Me.mForm.FindControls("ddlAdToDoDueDay").SelectedValue)
    End Function

    Public Function ValidateEnteredDateGTCompletedDate() As Boolean
        Return Me.ValidateMinDateGTMaxDate(Me.mForm.FindControls("ddlAdToDoEnteredYear").SelectedValue & "-" & Me.mForm.FindControls("ddlAdToDoEnteredMonth").SelectedValue & "-" & Me.mForm.FindControls("ddlAdToDoEnteredDay").SelectedValue, Me.mForm.FindControls("ddlAdToDoCompletedYear").SelectedValue & "-" & Me.mForm.FindControls("ddlAdToDoCompletedMonth").SelectedValue & "-" & Me.mForm.FindControls("ddlAdToDoCompletedDay").SelectedValue)
    End Function

    Private Function ValidateMinDateGTMaxDate(ByVal minDateText As String, ByVal maxDateText As String) As Boolean
        If IsDate(minDateText) AndAlso IsDate(maxDateText) Then
            Dim minDate As Date = minDateText
            Dim maxDate As Date = maxDateText

            If minDate > maxDate Then
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
            Me.mForm.FindControls("lblTitle").Text = "Edit ToDo Information"
        Else
            Me.mForm.FindControls("lblTitle").Text = "Add New ToDo Information"
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
            Me.mForm.FindControls("MsgBox").ShowMessage("Saved To Do Information.")
            Me.ApplyAddFormAuthorizationRules(True)
            Me.PopulateData()
        End If
    End Sub

    Public Sub OnReturn()
        Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdToDos.aspx")
    End Sub

#End Region

#Region " Data Methods "

    Private Sub ClearAddForm()

        Me.mForm.FindControls("txtAdToDoID").Text = String.Empty

        Me.LoadCombo(Me.mForm.FindControls("ddlAdToDoContact"), Me.GetComboDataSources(Me.GetContactInfoList(WebSite.ClsSessionAdmin.GetSessionUserID), "[Select a Contact]"), 0)

        Me.LoadDate(Me.mForm.FindControls("ddlAdToDoEnteredYear"), Me.mForm.FindControls("ddlAdToDoEnteredMonth"), Me.mForm.FindControls("ddlAdToDoEnteredDay"), New Date(1900, 1, 1, 0, 1, 0))
        Me.LoadDate(Me.mForm.FindControls("ddlAdToDoDueYear"), Me.mForm.FindControls("ddlAdToDoDueMonth"), Me.mForm.FindControls("ddlAdToDoDueDay"), New Date(1900, 1, 1, 0, 1, 0))
        Me.LoadDate(Me.mForm.FindControls("ddlAdToDoCompletedYear"), Me.mForm.FindControls("ddlAdToDoCompletedMonth"), Me.mForm.FindControls("ddlAdToDoCompletedDay"), New Date(1900, 1, 1, 0, 1, 0))

        Me.mForm.FindControls("txtAdToDoTaskNotes").Text = String.Empty
        Me.mForm.FindControls("ckbAdToDoCallBAckRecord").Checked = False

        Me.mForm.FindControls("txtAdToDoTaskNotes").Focus()
    End Sub

    Private Sub PopulateData()

        Me.mForm.FindControls("txtAdToDoID").Text = Me.mData.ID

        Me.LoadCombo(Me.mForm.FindControls("ddlAdToDoContact"), Me.GetComboDataSources(Me.GetContactInfoList(WebSite.ClsSessionAdmin.GetSessionUserID), "[Select a Contact]"), Me.mData.Contact.ID)

        Me.LoadDate(Me.mForm.FindControls("ddlAdToDoEnteredYear"), Me.mForm.FindControls("ddlAdToDoEnteredMonth"), Me.mForm.FindControls("ddlAdToDoEnteredDay"), Me.mData.DateEntered)
        Me.LoadDate(Me.mForm.FindControls("ddlAdToDoDueYear"), Me.mForm.FindControls("ddlAdToDoDueMonth"), Me.mForm.FindControls("ddlAdToDoDueDay"), Me.mData.DateDue)
        Me.LoadDate(Me.mForm.FindControls("ddlAdToDoCompletedYear"), Me.mForm.FindControls("ddlAdToDoCompletedMonth"), Me.mForm.FindControls("ddlAdToDoCompletedDay"), Me.mData.DateCompleted)

        Me.mForm.FindControls("txtAdToDoTaskNotes").Text = Me.mData.TaskNotes
        Me.mForm.FindControls("ckbAdToDoCallBAckRecord").Checked = Me.mData.CallBackRecord

        Me.mForm.FindControls("txtAdToDoTaskNotes").Focus()
    End Sub

    Private Function CollectData() As AdToDoNewData
        Dim formData As New AdToDoNewData

        formData.ID.SetValues("txtAdToDoID", True, Me.mData.ID, CLng(Me.CollectID(Me.mForm.FindControls("txtAdToDoID").Text)))

        formData.Contact.ID.SetValues("ddlAdToDoContact", False, Me.mData.Contact.ID, CLng(Me.mForm.FindControls("ddlAdToDoContact").SelectedItem.Value))
        formData.Contact.FullName.SetValues("ddlAdToDoContact", False, Me.mData.Contact.FullName, Me.mForm.FindControls("ddlAdToDoContact").SelectedItem.Text)

        formData.DateEntered.SetValues("ddlAdToDoEnteredDate", False, Me.mData.DateEntered, Me.CollectDate(Me.mForm.FindControls("ddlAdToDoEnteredYear").SelectedValue, Me.mForm.FindControls("ddlAdToDoEnteredMonth").SelectedValue, Me.mForm.FindControls("ddlAdToDoEnteredDay").SelectedValue))
        formData.DateDue.SetValues("ddlAdToDoDueDate", False, Me.mData.DateDue, Me.CollectDate(Me.mForm.FindControls("ddlAdToDoDueYear").SelectedValue, Me.mForm.FindControls("ddlAdToDoDueMonth").SelectedValue, Me.mForm.FindControls("ddlAdToDoDueDay").SelectedValue))
        formData.DateCompleted.SetValues("ddlAdToDoCompletedDate", False, Me.mData.DateCompleted, Me.CollectDate(Me.mForm.FindControls("ddlAdToDoCompletedYear").SelectedValue, Me.mForm.FindControls("ddlAdToDoCompletedMonth").SelectedValue, Me.mForm.FindControls("ddlAdToDoCompletedDay").SelectedValue))

        formData.TaskNotes.SetValues("txtAdToDoTaskNotes", False, Me.mData.TaskNotes, Me.mForm.FindControls("txtAdToDoTaskNotes").Text)
        formData.CallBackRecord.SetValues("ckbAdToDoCallBAckRecord", False, Me.mData.CallBackRecord, Me.mForm.FindControls("ckbAdToDoCallBAckRecord").Checked)

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

    Private Function CollectDataID(ByVal todoID As String) As AdToDoNewData
        Dim formData As New AdToDoNewData
        formData.ID.SetValues("txtAdToDoID", True, 0, CLng(todoID))
        Return formData
    End Function

    Private Function GetContactInfoList(ByVal accountID As Long) As AdContactData()
        Try
            Return WebSite.ClsSessionAdmin.GetAdContactInfoList(accountID)
        Catch SysEx As Exception
            Return New AdContactData() {}
        End Try
    End Function

    Private Function GetData(ByVal todoID As String) As AdToDoData
        Try
            Return WebSite.ClsSessionAdmin.GetAdToDo(Me.CollectDataID(todoID))
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return Nothing
        End Try
    End Function

    Private Function SaveData() As Boolean
        Try
            If Me.mEditMode Then
                Me.mData = WebSite.ClsSessionAdmin.EditAdToDo(Me.CollectData)
            Else
                Me.mData = WebSite.ClsSessionAdmin.AddAdToDo(Me.CollectData)
            End If
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error saving: " & SysEx.Message)
            Return False
        End Try
    End Function

#End Region

End Class
