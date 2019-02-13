Imports SCT.Library
Imports SCT.Library.AdminSite
Imports System.Globalization
Imports System.Web.UI.WebControls

Public Class DFClsAdvertiserToDo

#Region " Private Fields "

    Private mForm As Object
    Private mData As AdToDoData

    Private mEditMode As Boolean

#End Region

#Region " Authorization Rules "

    Public Shared Function CanSelect() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserToDo") Then
            Return ClsSessionAdmin.CanSelectInForm("frmAdvertiserToDo")
        Else
            Return ClsSessionAdmin.CanSelectInForm("AllForms")
        End If
    End Function

    Public Shared Function CanInsert() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserToDo") Then
            Return ClsSessionAdmin.CanInsertInForm("frmAdvertiserToDo")
        Else
            Return ClsSessionAdmin.CanInsertInForm("AllForms")
        End If
    End Function

    Public Shared Function CanUpdate() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserToDo") Then
            Return ClsSessionAdmin.CanUpdateInForm("frmAdvertiserToDo")
        Else
            Return ClsSessionAdmin.CanUpdateInForm("AllForms")
        End If
    End Function

    Public Shared Function CanDelete() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserToDo") Then
            Return ClsSessionAdmin.CanDeleteInForm("frmAdvertiserToDo")
        Else
            Return ClsSessionAdmin.CanDeleteInForm("AllForms")
        End If
    End Function

    Public Shared Function CanSelectField(ByVal fieldName As String) As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserToDo") Then
            Return ClsSessionAdmin.CanSelectFieldInForm("frmAdvertiserToDo", fieldName)
        Else
            Return ClsSessionAdmin.CanSelectFieldInForm("AllForms", "AllFields")
        End If
    End Function

    Public Shared Function CanInsertField(ByVal fieldName As String) As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserToDo") Then
            Return ClsSessionAdmin.CanInsertFieldInForm("frmAdvertiserToDo", fieldName)
        Else
            Return ClsSessionAdmin.CanInsertFieldInForm("AllForms", "AllFields")
        End If
    End Function

    Public Shared Function CanUpdateField(ByVal fieldName As String) As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserToDo") Then
            Return ClsSessionAdmin.CanUpdateFieldInForm("frmAdvertiserToDo", fieldName)
        Else
            Return ClsSessionAdmin.CanUpdateFieldInForm("AllForms", "AllFields")
        End If
    End Function

    Public Shared Function CanDeleteField(ByVal fieldName As String) As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserToDo") Then
            Return ClsSessionAdmin.CanDeleteFieldInForm("frmAdvertiserToDo", fieldName)
        Else
            Return ClsSessionAdmin.CanDeleteFieldInForm("AllForms", "AllFields")
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
        Me.mForm.Session("ValuePath") = "frmAdvertisers/frmAdvertiserToDos"

        Me.ApplyPageAuthorizationRules()

        If (Not IsNumeric(Me.mForm.Session("ID1"))) OrElse Me.mForm.Session("ID1") = 0 Then
            Me.mData = New AdToDoData
            Me.ApplyAddFormAuthorizationRules(False)
            Me.ClearAddForm()
        Else
            Me.mData = Me.GetData(Me.mForm.Session("ID1"))
            If Me.mData IsNot Nothing Then
                Me.ApplyAddFormAuthorizationRules(True)
                Me.PopulateData()
            End If
        End If
    End Sub

    Public Sub ApplyPageAuthorizationRules()
        Dim SelectField As Boolean

        If (Not ClsSessionAdmin.IsValidForm("frmAdvertiserToDo", "txtAdToDoID", "ddlAdToDoAdvertiser", "ddlAdToDoContact", "txtAdToDoDescription", "ddlAdToDoEnteredDate", "ddlAdToDoDueDate", "ddlAdToDoCompletedDate", "ckbAdToDoCallBackRecord")) OrElse (Not DFClsAdvertiserToDo.CanSelect()) Then Me.mForm.Response.Redirect("~/Forms/frmMain.aspx")

        SelectField = DFClsAdvertiserToDo.CanSelectField("txtAdToDoID")
        Me.mForm.FindControls("txtAdToDoID").Visible = SelectField
        Me.mForm.FindControls("lblAdToDoID").Visible = SelectField
        Me.mForm.FindControls("txtAdToDoID").Enabled = False

        SelectField = DFClsAdvertiserToDo.CanSelectField("ddlAdToDoAdvertiser")
        Me.mForm.FindControls("ddlAdToDoAdvertiser").Visible = SelectField
        Me.mForm.FindControls("lblAdToDoAdvertiser").Visible = SelectField
        Me.mForm.FindControls("ddlAdToDoAdvertiser").Enabled = False

        SelectField = DFClsAdvertiserToDo.CanSelectField("ddlAdToDoAdvertiser")
        Me.mForm.FindControls("ddlAdToDoContact").Visible = SelectField
        Me.mForm.FindControls("lblAdToDoContact").Visible = SelectField
        Me.mForm.FindControls("ddlAdToDoContact").Enabled = False

        SelectField = DFClsAdvertiserToDo.CanSelectField("txtAdToDoTaskNotes")
        Me.mForm.FindControls("txtAdToDoTaskNotes").Visible = SelectField
        Me.mForm.FindControls("lblAdToDoTaskNotes").Visible = SelectField
        Me.mForm.FindControls("txtAdToDoTaskNotes").Enabled = False

        SelectField = DFClsAdvertiserToDo.CanSelectField("ckbAdToDoCallBackRecord")
        Me.mForm.FindControls("ckbAdToDoCallBackRecord").Visible = SelectField
        Me.mForm.FindControls("ckbAdToDoCallBackRecord").Enabled = False

        SelectField = DFClsAdvertiserToDo.CanSelectField("ddlAdToDoEnteredDate")
        Me.mForm.FindControls("ddlAdToDoEnteredMonth").Visible = SelectField
        Me.mForm.FindControls("ddlAdToDoEnteredDay").Visible = SelectField
        Me.mForm.FindControls("ddlAdToDoEnteredYear").Visible = SelectField
        Me.mForm.FindControls("lblAdToDoEnteredDate").Visible = SelectField
        Me.mForm.FindControls("ddlAdToDoEnteredMonth").Enabled = False
        Me.mForm.FindControls("ddlAdToDoEnteredDay").Enabled = False
        Me.mForm.FindControls("ddlAdToDoEnteredYear").Enabled = False

        SelectField = DFClsAdvertiserToDo.CanSelectField("ddlAdToDoDueDate")
        Me.mForm.FindControls("ddlAdToDoDueMonth").Visible = SelectField
        Me.mForm.FindControls("ddlAdToDoDueDay").Visible = SelectField
        Me.mForm.FindControls("ddlAdToDoDueYear").Visible = SelectField
        Me.mForm.FindControls("lblAdToDoDueDate").Visible = SelectField
        Me.mForm.FindControls("ddlAdToDoDueMonth").Enabled = False
        Me.mForm.FindControls("ddlAdToDoDueDay").Enabled = False
        Me.mForm.FindControls("ddlAdToDoDueYear").Enabled = False

        SelectField = DFClsAdvertiserToDo.CanSelectField("ddlAdToDoCompletedDate")
        Me.mForm.FindControls("ddlAdToDoCompletedMonth").Visible = SelectField
        Me.mForm.FindControls("ddlAdToDoCompletedDay").Visible = SelectField
        Me.mForm.FindControls("ddlAdToDoCompletedYear").Visible = SelectField
        Me.mForm.FindControls("lblAdToDoCompletedDate").Visible = SelectField
        Me.mForm.FindControls("ddlAdToDoCompletedMonth").Enabled = False
        Me.mForm.FindControls("ddlAdToDoCompletedDay").Enabled = False
        Me.mForm.FindControls("ddlAdToDoCompletedYear").Enabled = False

        Me.mForm.FindControls("mnuItem").FindItem("Advertiser").Enabled = False
        Me.mForm.FindControls("mnuItem").FindItem("Contact").Enabled = False
        Me.mForm.FindControls("mnuItem").FindItem("Delete").Enabled = False

        Me.mForm.FindControls("mnuSave").FindItem("Ok").Enabled = False
        Me.mForm.FindControls("mnuSave").FindItem("Save").Enabled = False
    End Sub

    Public Sub ApplyAddFormAuthorizationRules(ByVal editMode As Boolean)
        Dim canSave As Boolean

        Me.mEditMode = editMode

        Me.mForm.FindControls("ddlAdToDoAdvertiser").Enabled = (Not editMode AndAlso DFClsAdvertiserNote.CanInsertField("ddlAdToDoAdvertiser")) OrElse (editMode AndAlso DFClsAdvertiserNote.CanUpdateField("ddlAdToDoAdvertiser"))
        Me.mForm.FindControls("ddlAdToDoContact").Enabled = (Not editMode AndAlso DFClsAdvertiserNote.CanInsertField("ddlAdToDoContact")) OrElse (editMode AndAlso DFClsAdvertiserNote.CanUpdateField("ddlAdToDoContact"))
        Me.mForm.FindControls("txtAdToDoTaskNotes").Enabled = (Not editMode AndAlso DFClsAdvertiserNote.CanInsertField("txtAdToDoDescription")) OrElse (editMode AndAlso DFClsAdvertiserNote.CanUpdateField("txtAdToDoDescription"))
        Me.mForm.FindControls("ckbAdToDoCallBackRecord").Enabled = (Not editMode AndAlso DFClsAdvertiserNote.CanInsertField("ckbAdToDoCallBackRecord")) OrElse (editMode AndAlso DFClsAdvertiserNote.CanUpdateField("ckbAdToDoCallBackRecord"))

        Me.mForm.FindControls("ddlAdToDoEnteredMonth").Enabled = (Not editMode AndAlso DFClsAdvertiserNote.CanInsertField("ddlAdToDoEnteredDate")) OrElse (editMode AndAlso DFClsAdvertiserNote.CanUpdateField("ddlAdToDoEnteredDate"))
        Me.mForm.FindControls("ddlAdToDoEnteredDay").Enabled = (Not editMode AndAlso DFClsAdvertiserNote.CanInsertField("ddlAdToDoEnteredDate")) OrElse (editMode AndAlso DFClsAdvertiserNote.CanUpdateField("ddlAdToDoEnteredDate"))
        Me.mForm.FindControls("ddlAdToDoEnteredYear").Enabled = (Not editMode AndAlso DFClsAdvertiserNote.CanInsertField("ddlAdToDoEnteredDate")) OrElse (editMode AndAlso DFClsAdvertiserNote.CanUpdateField("ddlAdToDoEnteredDate"))

        Me.mForm.FindControls("ddlAdToDoDueMonth").Enabled = (Not editMode AndAlso DFClsAdvertiserNote.CanInsertField("ddlAdToDoDueDate")) OrElse (editMode AndAlso DFClsAdvertiserNote.CanUpdateField("ddlAdToDoDueDate"))
        Me.mForm.FindControls("ddlAdToDoDueDay").Enabled = (Not editMode AndAlso DFClsAdvertiserNote.CanInsertField("ddlAdToDoDueDate")) OrElse (editMode AndAlso DFClsAdvertiserNote.CanUpdateField("ddlAdToDoDueDate"))
        Me.mForm.FindControls("ddlAdToDoDueYear").Enabled = (Not editMode AndAlso DFClsAdvertiserNote.CanInsertField("ddlAdToDoDueDate")) OrElse (editMode AndAlso DFClsAdvertiserNote.CanUpdateField("ddlAdToDoDueDate"))

        Me.mForm.FindControls("ddlAdToDoCompletedMonth").Enabled = (Not editMode AndAlso DFClsAdvertiserNote.CanInsertField("ddlAdToDoCompletedDate")) OrElse (editMode AndAlso DFClsAdvertiserNote.CanUpdateField("ddlAdToDoCompletedDate"))
        Me.mForm.FindControls("ddlAdToDoCompletedDay").Enabled = (Not editMode AndAlso DFClsAdvertiserNote.CanInsertField("ddlAdToDoCompletedDate")) OrElse (editMode AndAlso DFClsAdvertiserNote.CanUpdateField("ddlAdToDoCompletedDate"))
        Me.mForm.FindControls("ddlAdToDoCompletedYear").Enabled = (Not editMode AndAlso DFClsAdvertiserNote.CanInsertField("ddlAdToDoCompletedDate")) OrElse (editMode AndAlso DFClsAdvertiserNote.CanUpdateField("ddlAdToDoCompletedDate"))

        Me.mForm.FindControls("mnuItem").FindItem("Advertiser").Enabled = editMode AndAlso DFClsAdvertiserAccount.CanSelect
        Me.mForm.FindControls("mnuItem").FindItem("Contact").Enabled = editMode AndAlso DFClsAdvertiserContact.CanSelect
        Me.mForm.FindControls("mnuItem").FindItem("Delete").Enabled = editMode AndAlso DFClsAdvertiserNote.CanDelete

        canSave = (Not editMode AndAlso DFClsAdvertiserNote.CanInsert) OrElse (editMode AndAlso DFClsAdvertiserNote.CanUpdate)
        Me.mForm.FindControls("mnuSave").FindItem("Ok").Enabled = canSave
        Me.mForm.FindControls("mnuSave").FindItem("Save").Enabled = canSave
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

    Private Sub LoadAdvertiser(ByVal advertiser As DropDownList, ByVal value As Long)
        Me.LoadCombo(advertiser, Me.GetComboDataSources(Me.GetAdAccountInfoList, "[Select a Advertiser]"), value)
    End Sub

    Private Sub LoadContact(ByVal advertiser As DropDownList, ByVal contact As DropDownList, ByVal value As String)
        If advertiser.SelectedItem.Value <> 0 Then
            Me.LoadCombo(contact, Me.GetComboDataSources(Me.GetAdContactInfoList(advertiser.SelectedItem.Value), "[Select a Contact]"), value)
        Else
            Me.LoadCombo(contact, Me.GetComboDataSources(New AdContactData() {}, "[Select a Contact]"), 0)
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

    Private Sub LoadCombo(ByVal combo As DropDownList, ByVal data As DataTable, ByVal selectedValue As String)
        combo.DataSource = data
        combo.DataTextField = data.Columns(0).Caption
        combo.DataValueField = data.Columns(1).Caption
        combo.DataBind()
        If combo.Items.FindByValue(selectedValue) IsNot Nothing Then
            combo.SelectedValue = selectedValue
        End If
    End Sub

    Private Function GetComboDataSources(ByVal InfoList() As AdAccountData, ByVal FirstItemText As String) As DataTable
        Dim table As New DataTable

        table.Columns.Add("CompanyName", GetType(String))
        table.Columns.Add("ID", GetType(String))

        table.DefaultView.Sort = "CompanyName"

        table.Rows.Add(New String() {FirstItemText, "0"})
        For Each Info As AdAccountData In InfoList
            table.Rows.Add(New String() {Info.CompanyName, Info.ID.ToString})
        Next

        Return table
    End Function

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

    Public Sub OnSelectedAdvertiser(ByVal idAccountID As String)
        Me.LoadContact(Me.mForm.FindControls("ddlAdToDoAdvertiser"), Me.mForm.FindControls("ddlAdToDoContact"), 0)
        Me.mForm.FindControls("ddlAdToDoContact").Focus()
    End Sub

    Public Sub OnAdvertiser()
        Me.mForm.Session("ID1") = Me.mData.Contact.Advertiser.ID
        Me.mForm.Response.Redirect("~/Forms/frmAdvertiserAccount.aspx")
    End Sub

    Public Sub OnContact()
        Me.mForm.Session("ID1") = Me.mData.Contact.ID
        Me.mForm.Response.Redirect("~/Forms/frmAdvertiserContact.aspx")
    End Sub

    Public Sub OnToDos()
        Me.mForm.Session("ID1") = Me.mForm.FindControls("ddlAdToDoAdvertiser").SelectedItem.Value
        Me.mForm.Session("ID2") = Me.mForm.FindControls("ddlAdToDoContact").SelectedItem.Value
        Me.mForm.Response.Redirect("~/Forms/frmAdvertiserToDos.aspx")
    End Sub

    Public Sub OnDeleting()
        Me.mForm.FindControls("MsgBox").ShowConfirmation("Are you sure you want to delete this To Do?\n\nNote: there is no undo.", "", True, False)
    End Sub

    Public Sub OnMsgBoxOk()
        If Me.DeleteData Then
            Me.OnToDos()
        End If
    End Sub

    Public Sub OnSave()
        If Me.SaveData Then
            Me.ApplyAddFormAuthorizationRules(True)
            Me.PopulateData()
        End If
    End Sub

    Public Sub OnOk()
        If Me.SaveData Then
            Me.OnToDos()
        End If
    End Sub

    Public Sub OnCancel()
        Me.OnToDos()
    End Sub

#End Region

#Region " Data Methods "

    Private Sub ClearAddForm()
        Me.mForm.FindControls("txtAdToDoID").Text = String.Empty

        Me.LoadAdvertiser(Me.mForm.FindControls("ddlAdToDoAdvertiser"), 0)
        Me.LoadContact(Me.mForm.FindControls("ddlAdToDoAdvertiser"), Me.mForm.FindControls("ddlAdToDoContact"), 0)

        Me.LoadDate(Me.mForm.FindControls("ddlAdToDoEnteredYear"), Me.mForm.FindControls("ddlAdToDoEnteredMonth"), Me.mForm.FindControls("ddlAdToDoEnteredDay"), New Date(1900, 1, 1, 0, 1, 0))
        Me.LoadDate(Me.mForm.FindControls("ddlAdToDoDueYear"), Me.mForm.FindControls("ddlAdToDoDueMonth"), Me.mForm.FindControls("ddlAdToDoDueDay"), New Date(1900, 1, 1, 0, 1, 0))
        Me.LoadDate(Me.mForm.FindControls("ddlAdToDoCompletedYear"), Me.mForm.FindControls("ddlAdToDoCompletedMonth"), Me.mForm.FindControls("ddlAdToDoCompletedDay"), New Date(1900, 1, 1, 0, 1, 0))

        Me.mForm.FindControls("txtAdToDoTaskNotes").Text = String.Empty
        Me.mForm.FindControls("ckbAdToDoCallBackRecord").Checked = False

        Me.mForm.FindControls("txtAdToDoTaskNotes").Focus()
    End Sub

    Private Sub PopulateData()
        Me.mForm.FindControls("txtAdToDoID").Text = Me.mData.ID

        Me.LoadAdvertiser(Me.mForm.FindControls("ddlAdToDoAdvertiser"), Me.mData.Contact.Advertiser.ID)
        Me.LoadContact(Me.mForm.FindControls("ddlAdToDoAdvertiser"), Me.mForm.FindControls("ddlAdToDoContact"), Me.mData.Contact.ID)

        Me.LoadDate(Me.mForm.FindControls("ddlAdToDoEnteredYear"), Me.mForm.FindControls("ddlAdToDoEnteredMonth"), Me.mForm.FindControls("ddlAdToDoEnteredDay"), Me.mData.DateEntered)
        Me.LoadDate(Me.mForm.FindControls("ddlAdToDoDueYear"), Me.mForm.FindControls("ddlAdToDoDueMonth"), Me.mForm.FindControls("ddlAdToDoDueDay"), Me.mData.DateDue)
        Me.LoadDate(Me.mForm.FindControls("ddlAdToDoCompletedYear"), Me.mForm.FindControls("ddlAdToDoCompletedMonth"), Me.mForm.FindControls("ddlAdToDoCompletedDay"), Me.mData.DateCompleted)

        Me.mForm.FindControls("txtAdToDoTaskNotes").Text = Me.mData.TaskNotes
        Me.mForm.FindControls("ckbAdToDoCallBackRecord").Checked = Me.mData.CallBackRecord

        Me.mForm.FindControls("txtAdToDoTaskNotes").Focus()
    End Sub

    Private Function GetDataID(ByVal textBox As TextBox) As String
        If textBox.Text = String.Empty Then
            Return 0
        End If
        Return textBox.Text
    End Function

    Private Function CollectData() As AdToDoNewData
        Dim formData As New AdToDoNewData

        formData.ID.SetValues("txtAdToDoID", True, Me.mData.ID, CLng(Me.CollectID(Me.mForm.FindControls("txtAdToDoID").Text)))

        formData.Contact.ID.SetValues("ddlAdToDoContact", False, Me.mData.Contact.ID, CLng(Me.mForm.FindControls("ddlAdToDoContact").SelectedItem.Value))
        formData.Contact.FullName.SetValues("ddlAdToDoContact", False, Me.mData.Contact.FullName, Me.mForm.FindControls("ddlAdToDoContact").SelectedItem.Text)

        formData.DateEntered.SetValues("ddlAdToDoEnteredDate", False, Me.mData.DateEntered, Me.CollectDate(Me.mForm.FindControls("ddlAdToDoEnteredYear").SelectedValue, Me.mForm.FindControls("ddlAdToDoEnteredMonth").SelectedValue, Me.mForm.FindControls("ddlAdToDoEnteredDay").SelectedValue))
        formData.DateDue.SetValues("ddlAdToDoDueDate", False, Me.mData.DateDue, Me.CollectDate(Me.mForm.FindControls("ddlAdToDoDueYear").SelectedValue, Me.mForm.FindControls("ddlAdToDoDueMonth").SelectedValue, Me.mForm.FindControls("ddlAdToDoDueDay").SelectedValue))
        formData.DateCompleted.SetValues("ddlAdToDoCompletedDate", False, Me.mData.DateCompleted, Me.CollectDate(Me.mForm.FindControls("ddlAdToDoCompletedYear").SelectedValue, Me.mForm.FindControls("ddlAdToDoCompletedMonth").SelectedValue, Me.mForm.FindControls("ddlAdToDoCompletedDay").SelectedValue))

        formData.TaskNotes.SetValues("txtAdToDoTaskNotes", False, Me.mData.TaskNotes, Me.mForm.FindControls("txtAdToDoTaskNotes").Text)
        formData.CallBackRecord.SetValues("ckbAdToDoCallBackRecord", False, Me.mData.CallBackRecord, Me.mForm.FindControls("ckbAdToDoCallBackRecord").Checked)

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

    Private Function CollectDataID() As AdToDoNewData
        Dim formData As New AdToDoNewData
        formData.ID.SetValues("txtAdToDoID", True, Me.mData.ID, CLng(Me.GetDataID(Me.mForm.FindControls("txtAdToDoID"))))
        Return formData
    End Function

    Private Function CollectDataID(ByVal adToDoID As String) As AdToDoNewData
        Dim formData As New AdToDoNewData
        formData.ID.SetValues("txtAdToDoID", True, 0, CLng(adToDoID))
        Return formData
    End Function

    Private Function GetAdAccountInfoList() As AdAccountData()
        Try
            Return ClsSessionAdmin.GetAdAccountInfoList()
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return New AdAccountData() {}
        End Try
    End Function

    Private Function GetAdContactInfoList(ByVal idAccountID As Long) As AdContactData()
        Try
            Return ClsSessionAdmin.GetAdContactInfoList(idAccountID)
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return New AdContactData() {}
        End Try
    End Function

    Private Function GetData(ByVal adToDoID As String) As AdToDoData
        Try
            Return ClsSessionAdmin.GetAdToDo(Me.CollectDataID(adToDoID))
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return Nothing
        End Try
    End Function

    Private Function SaveData() As Boolean
        Try
            If Me.mEditMode Then
                Me.mData = ClsSessionAdmin.EditAdToDo(Me.CollectData)
            Else
                Me.mData = ClsSessionAdmin.AddAdToDo(Me.CollectData)
            End If
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error saving: " & SysEx.Message)
            Return False
        End Try
    End Function

    Private Function DeleteData() As Boolean
        Try
            ClsSessionAdmin.DeleteAdToDo(Me.CollectDataID)
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error deleting: " & SysEx.Message)
            Return False
        End Try
    End Function

#End Region

End Class
