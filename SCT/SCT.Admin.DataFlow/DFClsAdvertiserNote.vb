Imports SCT.Library
Imports SCT.Library.AdminSite
Imports System.Globalization
Imports System.Web.UI.WebControls

Public Class DFClsAdvertiserNote

#Region " Private Fields "

    Private mForm As Object
    Private mData As AdNoteData

    Private mEditMode As Boolean

#End Region

#Region " Authorization Rules "

    Public Shared Function CanSelect() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserNote") Then
            Return ClsSessionAdmin.CanSelectInForm("frmAdvertiserNote")
        Else
            Return ClsSessionAdmin.CanSelectInForm("AllForms")
        End If
    End Function

    Public Shared Function CanInsert() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserNote") Then
            Return ClsSessionAdmin.CanInsertInForm("frmAdvertiserNote")
        Else
            Return ClsSessionAdmin.CanInsertInForm("AllForms")
        End If
    End Function

    Public Shared Function CanUpdate() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserNote") Then
            Return ClsSessionAdmin.CanUpdateInForm("frmAdvertiserNote")
        Else
            Return ClsSessionAdmin.CanUpdateInForm("AllForms")
        End If
    End Function

    Public Shared Function CanDelete() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserNote") Then
            Return ClsSessionAdmin.CanDeleteInForm("frmAdvertiserNote")
        Else
            Return ClsSessionAdmin.CanDeleteInForm("AllForms")
        End If
    End Function

    Public Shared Function CanSelectField(ByVal fieldName As String) As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserNote") Then
            Return ClsSessionAdmin.CanSelectFieldInForm("frmAdvertiserNote", fieldName)
        Else
            Return ClsSessionAdmin.CanSelectFieldInForm("AllForms", "AllFields")
        End If
    End Function

    Public Shared Function CanInsertField(ByVal fieldName As String) As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserNote") Then
            Return ClsSessionAdmin.CanInsertFieldInForm("frmAdvertiserNote", fieldName)
        Else
            Return ClsSessionAdmin.CanInsertFieldInForm("AllForms", "AllFields")
        End If
    End Function

    Public Shared Function CanUpdateField(ByVal fieldName As String) As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserNote") Then
            Return ClsSessionAdmin.CanUpdateFieldInForm("frmAdvertiserNote", fieldName)
        Else
            Return ClsSessionAdmin.CanUpdateFieldInForm("AllForms", "AllFields")
        End If
    End Function

    Public Shared Function CanDeleteField(ByVal fieldName As String) As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserNote") Then
            Return ClsSessionAdmin.CanDeleteFieldInForm("frmAdvertiserNote", fieldName)
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
        Me.mForm.Session("ValuePath") = "frmAdvertisers/frmAdvertiserNotes"

        Me.ApplyPageAuthorizationRules()

        If (Not IsNumeric(Me.mForm.Session("ID1"))) OrElse Me.mForm.Session("ID1") = 0 Then
            Me.mData = New AdNoteData
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

        If (Not ClsSessionAdmin.IsValidForm("frmAdvertiserNote", "txtAdNoteID", "ddlAdNoteAdvertiser", "ddlAdNoteContact", "txtAdNoteDescription", "ddlAdNoteEnteredDate")) OrElse (Not DFClsAdvertiserNote.CanSelect()) Then Me.mForm.Response.Redirect("~/Forms/frmMain.aspx")

        SelectField = DFClsAdvertiserNote.CanSelectField("txtAdNoteID")
        Me.mForm.FindControls("txtAdNoteID").Visible = SelectField
        Me.mForm.FindControls("lblAdNoteID").Visible = SelectField
        Me.mForm.FindControls("txtAdNoteID").Enabled = False

        SelectField = DFClsAdvertiserNote.CanSelectField("ddlAdNoteAdvertiser")
        Me.mForm.FindControls("ddlAdNoteAdvertiser").Visible = SelectField
        Me.mForm.FindControls("lblAdNoteAdvertiser").Visible = SelectField
        Me.mForm.FindControls("ddlAdNoteAdvertiser").Enabled = False

        SelectField = DFClsAdvertiserNote.CanSelectField("ddlAdNoteAdvertiser")
        Me.mForm.FindControls("ddlAdNoteContact").Visible = SelectField
        Me.mForm.FindControls("lblAdNoteContact").Visible = SelectField
        Me.mForm.FindControls("ddlAdNoteContact").Enabled = False

        SelectField = DFClsAdvertiserNote.CanSelectField("txtAdNoteDescription")
        Me.mForm.FindControls("txtAdNoteDescription").Visible = SelectField
        Me.mForm.FindControls("lblAdNoteDescription").Visible = SelectField
        Me.mForm.FindControls("txtAdNoteDescription").Enabled = False

        SelectField = DFClsAdvertiserNote.CanSelectField("ddlAdNoteEnteredDate")
        Me.mForm.FindControls("ddlAdNoteEnteredMonth").Visible = SelectField
        Me.mForm.FindControls("ddlAdNoteEnteredDay").Visible = SelectField
        Me.mForm.FindControls("ddlAdNoteEnteredYear").Visible = SelectField
        Me.mForm.FindControls("lblAdNoteEnteredDate").Visible = SelectField
        Me.mForm.FindControls("ddlAdNoteEnteredMonth").Enabled = False
        Me.mForm.FindControls("ddlAdNoteEnteredDay").Enabled = False
        Me.mForm.FindControls("ddlAdNoteEnteredYear").Enabled = False

        Me.mForm.FindControls("mnuItem").FindItem("Advertiser").Enabled = False
        Me.mForm.FindControls("mnuItem").FindItem("Contact").Enabled = False
        Me.mForm.FindControls("mnuItem").FindItem("Delete").Enabled = False

        Me.mForm.FindControls("mnuSave").FindItem("Ok").Enabled = False
        Me.mForm.FindControls("mnuSave").FindItem("Save").Enabled = False
    End Sub

    Public Sub ApplyAddFormAuthorizationRules(ByVal editMode As Boolean)
        Dim canSave As Boolean

        Me.mEditMode = editMode

        Me.mForm.FindControls("ddlAdNoteAdvertiser").Enabled = (Not editMode AndAlso DFClsAdvertiserNote.CanInsertField("ddlAdNoteAdvertiser")) OrElse (editMode AndAlso DFClsAdvertiserNote.CanUpdateField("ddlAdNoteAdvertiser"))
        Me.mForm.FindControls("ddlAdNoteContact").Enabled = (Not editMode AndAlso DFClsAdvertiserNote.CanInsertField("ddlAdNoteContact")) OrElse (editMode AndAlso DFClsAdvertiserNote.CanUpdateField("ddlAdNoteContact"))
        Me.mForm.FindControls("txtAdNoteDescription").Enabled = (Not editMode AndAlso DFClsAdvertiserNote.CanInsertField("txtAdNoteDescription")) OrElse (editMode AndAlso DFClsAdvertiserNote.CanUpdateField("txtAdNoteDescription"))

        Me.mForm.FindControls("ddlAdNoteEnteredMonth").Enabled = (Not editMode AndAlso DFClsAdvertiserNote.CanInsertField("ddlAdNoteEnteredDate")) OrElse (editMode AndAlso DFClsAdvertiserNote.CanUpdateField("ddlAdNoteEnteredDate"))
        Me.mForm.FindControls("ddlAdNoteEnteredDay").Enabled = (Not editMode AndAlso DFClsAdvertiserNote.CanInsertField("ddlAdNoteEnteredDate")) OrElse (editMode AndAlso DFClsAdvertiserNote.CanUpdateField("ddlAdNoteEnteredDate"))
        Me.mForm.FindControls("ddlAdNoteEnteredYear").Enabled = (Not editMode AndAlso DFClsAdvertiserNote.CanInsertField("ddlAdNoteEnteredDate")) OrElse (editMode AndAlso DFClsAdvertiserNote.CanUpdateField("ddlAdNoteEnteredDate"))

        Me.mForm.FindControls("mnuItem").FindItem("Advertiser").Enabled = editMode AndAlso DFClsAdvertiserAccount.CanSelect
        Me.mForm.FindControls("mnuItem").FindItem("Contact").Enabled = editMode AndAlso DFClsAdvertiserContact.CanSelect
        Me.mForm.FindControls("mnuItem").FindItem("Delete").Enabled = editMode AndAlso DFClsAdvertiserNote.CanDelete

        canSave = (Not editMode AndAlso DFClsAdvertiserNote.CanInsert) OrElse (editMode AndAlso DFClsAdvertiserNote.CanUpdate)
        Me.mForm.FindControls("mnuSave").FindItem("Ok").Enabled = canSave
        Me.mForm.FindControls("mnuSave").FindItem("Save").Enabled = canSave
    End Sub

    Public Function ValidateEnteredDateRequered() As Boolean
        Return Me.mForm.FindControls("ddlAdNoteEnteredYear").SelectedValue & "-" & Me.mForm.FindControls("ddlAdNoteEnteredMonth").SelectedValue & "-" & Me.mForm.FindControls("ddlAdNoteEnteredDay").SelectedValue <> "Year-Month-Day"
    End Function

    Public Function ValidateEnteredDateValid() As Boolean
        Dim startDate As String = Me.mForm.FindControls("ddlAdNoteEnteredYear").SelectedValue & "-" & Me.mForm.FindControls("ddlAdNoteEnteredMonth").SelectedValue & "-" & Me.mForm.FindControls("ddlAdNoteEnteredDay").SelectedValue
        Return startDate = "Year-Month-Day" OrElse IsDate(startDate)
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
        Me.LoadContact(Me.mForm.FindControls("ddlAdNoteAdvertiser"), Me.mForm.FindControls("ddlAdNoteContact"), 0)
        Me.mForm.FindControls("ddlAdNoteContact").Focus()
    End Sub

    Public Sub OnAdvertiser()
        Me.mForm.Session("ID1") = Me.mData.Contact.Advertiser.ID
        Me.mForm.Response.Redirect("~/Forms/frmAdvertiserAccount.aspx")
    End Sub

    Public Sub OnContact()
        Me.mForm.Session("ID1") = Me.mData.Contact.ID
        Me.mForm.Response.Redirect("~/Forms/frmAdvertiserContact.aspx")
    End Sub

    Public Sub OnNotes()
        Me.mForm.Session("ID1") = Me.mForm.FindControls("ddlAdNoteAdvertiser").SelectedItem.Value
        Me.mForm.Session("ID2") = Me.mForm.FindControls("ddlAdNoteContact").SelectedItem.Value
        Me.mForm.Response.Redirect("~/Forms/frmAdvertiserNotes.aspx")
    End Sub

    Public Sub OnDeleting()
        Me.mForm.FindControls("MsgBox").ShowConfirmation("Are you sure you want to delete this Note?\n\nNote: there is no undo.", "", True, False)
    End Sub

    Public Sub OnMsgBoxOk()
        If Me.DeleteData Then
            Me.OnNotes()
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
            Me.OnNotes()
        End If
    End Sub

    Public Sub OnCancel()
        Me.OnNotes()
    End Sub

#End Region

#Region " Data Methods "

    Private Sub ClearAddForm()
        Me.mForm.FindControls("txtAdNoteID").Text = String.Empty

        Me.LoadAdvertiser(Me.mForm.FindControls("ddlAdNoteAdvertiser"), 0)
        Me.LoadContact(Me.mForm.FindControls("ddlAdNoteAdvertiser"), Me.mForm.FindControls("ddlAdNoteContact"), 0)

        Me.mForm.FindControls("txtAdNoteDescription").Text = String.Empty

        Me.LoadDate(Me.mForm.FindControls("ddlAdNoteEnteredYear"), Me.mForm.FindControls("ddlAdNoteEnteredMonth"), Me.mForm.FindControls("ddlAdNoteEnteredDay"), New Date(1900, 1, 1, 0, 1, 0))

        Me.mForm.FindControls("txtAdNoteDescription").Focus()
    End Sub

    Private Sub PopulateData()
        Me.mForm.FindControls("txtAdNoteID").Text = Me.mData.ID

        Me.LoadAdvertiser(Me.mForm.FindControls("ddlAdNoteAdvertiser"), Me.mData.Contact.Advertiser.ID)
        Me.LoadContact(Me.mForm.FindControls("ddlAdNoteAdvertiser"), Me.mForm.FindControls("ddlAdNoteContact"), Me.mData.Contact.ID)

        Me.mForm.FindControls("txtAdNoteDescription").Text = Me.mData.Description

        Me.LoadDate(Me.mForm.FindControls("ddlAdNoteEnteredYear"), Me.mForm.FindControls("ddlAdNoteEnteredMonth"), Me.mForm.FindControls("ddlAdNoteEnteredDay"), Me.mData.DateEntered)

        Me.mForm.FindControls("txtAdNoteDescription").Focus()
    End Sub

    Private Function GetDataID(ByVal textBox As TextBox) As String
        If textBox.Text = String.Empty Then
            Return 0
        End If
        Return textBox.Text
    End Function

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

    Private Function CollectDataID() As AdNoteNewData
        Dim formData As New AdNoteNewData
        formData.ID.SetValues("txtAdNoteID", True, Me.mData.ID, CLng(Me.GetDataID(Me.mForm.FindControls("txtAdNoteID"))))
        Return formData
    End Function

    Private Function CollectDataID(ByVal adNoteID As String) As AdNoteNewData
        Dim formData As New AdNoteNewData
        formData.ID.SetValues("txtAdNoteID", True, 0, CLng(adNoteID))
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

    Private Function GetData(ByVal adNoteID As String) As AdNoteData
        Try
            Return ClsSessionAdmin.GetAdNote(Me.CollectDataID(adNoteID))
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return Nothing
        End Try
    End Function

    Private Function SaveData() As Boolean
        Try
            If Me.mEditMode Then
                Me.mData = ClsSessionAdmin.EditAdNote(Me.CollectData)
            Else
                Me.mData = ClsSessionAdmin.AddAdNote(Me.CollectData)
            End If
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error saving: " & SysEx.Message)
            Return False
        End Try
    End Function

    Private Function DeleteData() As Boolean
        Try
            ClsSessionAdmin.DeleteAdNote(Me.CollectDataID)
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error deleting: " & SysEx.Message)
            Return False
        End Try
    End Function

#End Region

End Class
