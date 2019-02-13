Imports SCT.Library
Imports System.Globalization
Imports SCT.Library.AdminSite
Imports System.Web.UI.WebControls

Public Class DFClsAdvertiserContact

#Region " Private Fields "

    Private mForm As Object
    Private mData As AdContactData

    Private mEditMode As Boolean

#End Region

#Region " Authorization Rules "

    Public Shared Function CanSelect() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserContact") Then
            Return ClsSessionAdmin.CanSelectInForm("frmAdvertiserContact")
        Else
            Return ClsSessionAdmin.CanSelectInForm("AllForms")
        End If
    End Function

    Public Shared Function CanInsert() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserContact") Then
            Return ClsSessionAdmin.CanInsertInForm("frmAdvertiserContact")
        Else
            Return ClsSessionAdmin.CanInsertInForm("AllForms")
        End If
    End Function

    Public Shared Function CanUpdate() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserContact") Then
            Return ClsSessionAdmin.CanUpdateInForm("frmAdvertiserContact")
        Else
            Return ClsSessionAdmin.CanUpdateInForm("AllForms")
        End If
    End Function

    Public Shared Function CanDelete() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserContact") Then
            Return ClsSessionAdmin.CanDeleteInForm("frmAdvertiserContact")
        Else
            Return ClsSessionAdmin.CanDeleteInForm("AllForms")
        End If
    End Function

    Public Shared Function CanSelectField(ByVal fieldName As String) As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserContact") Then
            Return ClsSessionAdmin.CanSelectFieldInForm("frmAdvertiserContact", fieldName)
        Else
            Return ClsSessionAdmin.CanSelectFieldInForm("AllForms", "AllFields")
        End If
    End Function

    Public Shared Function CanInsertField(ByVal fieldName As String) As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserContact") Then
            Return ClsSessionAdmin.CanInsertFieldInForm("frmAdvertiserContact", fieldName)
        Else
            Return ClsSessionAdmin.CanInsertFieldInForm("AllForms", "AllFields")
        End If
    End Function

    Public Shared Function CanUpdateField(ByVal fieldName As String) As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserContact") Then
            Return ClsSessionAdmin.CanUpdateFieldInForm("frmAdvertiserContact", fieldName)
        Else
            Return ClsSessionAdmin.CanUpdateFieldInForm("AllForms", "AllFields")
        End If
    End Function

    Public Shared Function CanDeleteField(ByVal fieldName As String) As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserContact") Then
            Return ClsSessionAdmin.CanDeleteFieldInForm("frmAdvertiserContact", fieldName)
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
        Me.mForm.Session("ValuePath") = "frmAdvertisers/frmAdvertiserContacts"

        Me.ApplyPageAuthorizationRules()

        If (Not IsNumeric(Me.mForm.Session("ID1"))) OrElse Me.mForm.Session("ID1") = 0 Then
            Me.mData = New AdContactData
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

        If (Not ClsSessionAdmin.IsValidForm("frmAdvertiserContact", "txtAdContactID", "ddlAdContactAdvertiser", "txtAdContactFirstName", "txtAdContactLastName", "txtAdContactPrimaryAddress", "txtAdContactSecondaryAddress", "ckbAdContactMainCompanyAddress", "txtAdContactCity", "ddlAdContactCountry", "ddlAdContactState", "txtAdContactZipCode", "txtAdContactProvidence", "txtAdContactDepartment", "txtAdContactNotes")) OrElse (Not DFClsAdvertiserContact.CanSelect()) Then Me.mForm.Response.Redirect("~/Forms/frmMain.aspx")

        SelectField = DFClsAdvertiserContact.CanSelectField("txtAdContactID")
        Me.mForm.FindControls("txtAdContactID").Visible = SelectField
        Me.mForm.FindControls("lblAdContactID").Visible = SelectField
        Me.mForm.FindControls("txtAdContactID").Enabled = False

        SelectField = DFClsAdvertiserContact.CanSelectField("ddlAdContactAdvertiser")
        Me.mForm.FindControls("ddlAdContactAdvertiser").Visible = SelectField
        Me.mForm.FindControls("lblAdContactAdvertiser").Visible = SelectField
        Me.mForm.FindControls("ddlAdContactAdvertiser").Enabled = False

        SelectField = DFClsAdvertiserContact.CanSelectField("txtAdContactFirstName")
        Me.mForm.FindControls("txtAdContactFirstName").Visible = SelectField
        Me.mForm.FindControls("lblAdContactFirstName").Visible = SelectField
        Me.mForm.FindControls("txtAdContactFirstName").Enabled = False

        SelectField = DFClsAdvertiserContact.CanSelectField("txtAdContactLastName")
        Me.mForm.FindControls("txtAdContactLastName").Visible = SelectField
        Me.mForm.FindControls("lblAdContactLastName").Visible = SelectField
        Me.mForm.FindControls("txtAdContactLastName").Enabled = False

        SelectField = DFClsAdvertiserContact.CanSelectField("txtAdContactPrimaryAddress")
        Me.mForm.FindControls("txtAdContactPrimaryAddress").Visible = SelectField
        Me.mForm.FindControls("lblAdContactPrimaryAddress").Visible = SelectField
        Me.mForm.FindControls("txtAdContactPrimaryAddress").Enabled = False

        SelectField = DFClsAdvertiserContact.CanSelectField("txtAdContactSecondaryAddress")
        Me.mForm.FindControls("txtAdContactSecondaryAddress").Visible = SelectField
        Me.mForm.FindControls("lblAdContactSecondaryAddress").Visible = SelectField
        Me.mForm.FindControls("txtAdContactSecondaryAddress").Enabled = False

        SelectField = DFClsAdvertiserContact.CanSelectField("ckbAdContactMainCompanyAddress")
        Me.mForm.FindControls("ckbAdContactMainCompanyAddress").Visible = SelectField
        Me.mForm.FindControls("ckbAdContactMainCompanyAddress").Enabled = False

        SelectField = DFClsAdvertiserContact.CanSelectField("txtAdContactCity")
        Me.mForm.FindControls("txtAdContactCity").Visible = SelectField
        Me.mForm.FindControls("lblAdContactCity").Visible = SelectField
        Me.mForm.FindControls("txtAdContactCity").Enabled = False

        SelectField = DFClsAdvertiserContact.CanSelectField("ddlAdContactCountry")
        Me.mForm.FindControls("ddlAdContactCountry").Visible = SelectField
        Me.mForm.FindControls("lblAdContactCountry").Visible = SelectField
        Me.mForm.FindControls("ddlAdContactCountry").Enabled = False

        SelectField = DFClsAdvertiserContact.CanSelectField("ddlAdContactState")
        Me.mForm.FindControls("ddlAdContactState").Visible = SelectField
        Me.mForm.FindControls("lblAdContactState").Visible = SelectField
        Me.mForm.FindControls("ddlAdContactState").Enabled = False

        SelectField = DFClsAdvertiserContact.CanSelectField("txtAdContactZipCode")
        Me.mForm.FindControls("txtAdContactZipCode").Visible = SelectField
        Me.mForm.FindControls("lblAdContactZipCode").Visible = SelectField
        Me.mForm.FindControls("txtAdContactZipCode").Enabled = False

        SelectField = DFClsAdvertiserContact.CanSelectField("txtAdContactProvidence")
        Me.mForm.FindControls("txtAdContactProvidence").Visible = SelectField
        Me.mForm.FindControls("lblAdContactProvidence").Visible = SelectField
        Me.mForm.FindControls("txtAdContactProvidence").Enabled = False

        SelectField = DFClsAdvertiserContact.CanSelectField("txtAdContactDepartment")
        Me.mForm.FindControls("txtAdContactDepartment").Visible = SelectField
        Me.mForm.FindControls("lblAdContactDepartment").Visible = SelectField
        Me.mForm.FindControls("txtAdContactDepartment").Enabled = False

        SelectField = DFClsAdvertiserContact.CanSelectField("txtAdContactNotes")
        Me.mForm.FindControls("txtAdContactNotes").Visible = SelectField
        Me.mForm.FindControls("lblAdContactNotes").Visible = SelectField
        Me.mForm.FindControls("txtAdContactNotes").Enabled = False

        Me.mForm.FindControls("mnuItem").FindItem("Advertiser").Enabled = False
        Me.mForm.FindControls("mnuItem").FindItem("Delete").Enabled = False

        Me.mForm.FindControls("mnuSave").FindItem("Ok").Enabled = False
        Me.mForm.FindControls("mnuSave").FindItem("Save").Enabled = False
    End Sub

    Public Sub ApplyAddFormAuthorizationRules(ByVal editMode As Boolean)
        Dim canSave As Boolean

        Me.mEditMode = editMode

        Me.mForm.FindControls("ddlAdContactAdvertiser").Enabled = (Not editMode AndAlso DFClsAdvertiserContact.CanInsertField("ddlAdContactAdvertiser")) OrElse (editMode AndAlso DFClsAdvertiserContact.CanUpdateField("ddlAdContactAdvertiser"))
        Me.mForm.FindControls("txtAdContactFirstName").Enabled = (Not editMode AndAlso DFClsAdvertiserContact.CanInsertField("txtAdContactFirstName")) OrElse (editMode AndAlso DFClsAdvertiserContact.CanUpdateField("txtAdContactFirstName"))
        Me.mForm.FindControls("txtAdContactLastName").Enabled = (Not editMode AndAlso DFClsAdvertiserContact.CanInsertField("txtAdContactLastName")) OrElse (editMode AndAlso DFClsAdvertiserContact.CanUpdateField("txtAdContactLastName"))
        Me.mForm.FindControls("txtAdContactPrimaryAddress").Enabled = (Not editMode AndAlso DFClsAdvertiserContact.CanInsertField("txtAdContactPrimaryAddress")) OrElse (editMode AndAlso DFClsAdvertiserContact.CanUpdateField("txtAdContactPrimaryAddress"))
        Me.mForm.FindControls("txtAdContactSecondaryAddress").Enabled = (Not editMode AndAlso DFClsAdvertiserContact.CanInsertField("txtAdContactSecondaryAddress")) OrElse (editMode AndAlso DFClsAdvertiserContact.CanUpdateField("txtAdContactSecondaryAddress"))
        Me.mForm.FindControls("ckbAdContactMainCompanyAddress").Enabled = (Not editMode AndAlso DFClsAdvertiserContact.CanInsertField("ckbAdContactMainCompanyAddress")) OrElse (editMode AndAlso DFClsAdvertiserContact.CanUpdateField("ckbAdContactMainCompanyAddress"))
        Me.mForm.FindControls("txtAdContactCity").Enabled = (Not editMode AndAlso DFClsAdvertiserContact.CanInsertField("txtAdContactCity")) OrElse (editMode AndAlso DFClsAdvertiserContact.CanUpdateField("txtAdContactCity"))
        Me.mForm.FindControls("ddlAdContactCountry").Enabled = (Not editMode AndAlso DFClsAdvertiserContact.CanInsertField("ddlAdContactCountry")) OrElse (editMode AndAlso DFClsAdvertiserContact.CanUpdateField("ddlAdContactCountry"))
        Me.mForm.FindControls("ddlAdContactState").Enabled = (Not editMode AndAlso DFClsAdvertiserContact.CanInsertField("ddlAdContactState")) OrElse (editMode AndAlso DFClsAdvertiserContact.CanUpdateField("ddlAdContactState"))
        Me.mForm.FindControls("txtAdContactZipCode").Enabled = (Not editMode AndAlso DFClsAdvertiserContact.CanInsertField("txtAdContactZipCode")) OrElse (editMode AndAlso DFClsAdvertiserContact.CanUpdateField("txtAdContactZipCode"))
        Me.mForm.FindControls("txtAdContactProvidence").Enabled = (Not editMode AndAlso DFClsAdvertiserContact.CanInsertField("txtAdContactProvidence")) OrElse (editMode AndAlso DFClsAdvertiserContact.CanUpdateField("txtAdContactProvidence"))
        Me.mForm.FindControls("txtAdContactDepartment").Enabled = (Not editMode AndAlso DFClsAdvertiserContact.CanInsertField("txtAdContactDepartment")) OrElse (editMode AndAlso DFClsAdvertiserContact.CanUpdateField("txtAdContactDepartment"))
        Me.mForm.FindControls("txtAdContactNotes").Enabled = (Not editMode AndAlso DFClsAdvertiserContact.CanInsertField("txtAdContactNotes")) OrElse (editMode AndAlso DFClsAdvertiserContact.CanUpdateField("txtAdContactNotes"))

        Me.mForm.FindControls("mnuItem").FindItem("Advertiser").Enabled = editMode AndAlso DFClsAdvertiserAccount.CanSelect
        Me.mForm.FindControls("mnuItem").FindItem("Delete").Enabled = editMode AndAlso DFClsAdvertiserContact.CanDelete

        canSave = (Not editMode AndAlso DFClsAdvertiserContact.CanInsert) OrElse (editMode AndAlso DFClsAdvertiserContact.CanUpdate)
        Me.mForm.FindControls("mnuSave").FindItem("Ok").Enabled = canSave
        Me.mForm.FindControls("mnuSave").FindItem("Save").Enabled = canSave
    End Sub

    Public Function ValidateExistsMainCompanyAddress() As Boolean
        Return Not Advertiser.ClsContact.ExistsMainCompanyAddress(Me.GetDataID(Me.mForm.FindControls("txtAdContactID")), Me.mForm.FindControls("ddlAdContactAdvertiser").SelectedItem.Value, Me.mForm.FindControls("ckbAdContactMainCompanyAddress").Checked)
    End Function

    Public Function ValidateExistsInProjectsAnotherAdvertiser() As Boolean
        Return Not Advertiser.ClsContact.ExistsInProjectsAnotherAdvertiser(Me.GetDataID(Me.mForm.FindControls("txtAdContactID")), Me.mForm.FindControls("ddlAdContactAdvertiser").SelectedItem.Value)
    End Function

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

    Private Function GetCountryData(ByVal FirstItemText As String) As DataTable
        Dim reginfo As RegionInfo
        Dim cultInfoList() As CultureInfo

        Dim table As New DataTable
        Dim row As DataRow

        table.Columns.Add("Text", GetType(String))
        table.Columns.Add("Value", GetType(String))

        table.Rows.Add(New Object() {FirstItemText, ""})

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

    Private Function GetStateData(ByVal filter As String, ByVal FirstItemText As String) As DataTable
        Dim table As New DataTable
        table.ReadXml(Me.mForm.MapPath("~") & "/App_Data/States.xml")
        table.Rows.Add(New Object() {FirstItemText, "", filter})
        table.DefaultView.Sort = "Name"
        table.DefaultView.RowFilter = "CodeCountry = '" & filter & "'"
        Return table
    End Function

    Public Sub OnSelectedCountry(ByVal countryCode As String)
        Me.LoadCombo(Me.mForm.FindControls("ddlAdContactState"), Me.GetStateData(countryCode, "[Select a State]"), String.Empty)
        Me.mForm.FindControls("ddlAdContactState").Focus()
    End Sub

    Public Sub OnAdvertiser()
        Me.mForm.Session("ID1") = Me.mData.Advertiser.ID
        Me.mForm.Response.Redirect("~/Forms/frmAdvertiserAccount.aspx")
    End Sub

    Public Sub OnContacts()
        Me.mForm.Session("ID1") = Me.mForm.FindControls("ddlAdContactAdvertiser").SelectedItem.Value
        Me.mForm.Response.Redirect("~/Forms/frmAdvertiserContacts.aspx")
    End Sub

    Public Sub OnDeleting()
        Me.mForm.FindControls("MsgBox").ShowConfirmation("Are you sure you want to delete this Contact?\n\nNote: there is no undo.", "", True, False)
    End Sub

    Public Sub OnMsgBoxOk()
        If Me.DeleteData Then
            Me.OnContacts()
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
            Me.OnContacts()
        End If
    End Sub

    Public Sub OnCancel()
        Me.OnContacts()
    End Sub

#End Region

#Region " Data Methods "

    Private Sub ClearAddForm()
        Me.mForm.FindControls("txtAdContactID").Text = String.Empty

        Me.LoadCombo(Me.mForm.FindControls("ddlAdContactAdvertiser"), Me.GetComboDataSources(Me.GetAdAccountInfoList, "[Select a Advertiser]"), 0)

        Me.mForm.FindControls("txtAdContactFirstName").Text = String.Empty
        Me.mForm.FindControls("txtAdContactLastName").Text = String.Empty
        Me.mForm.FindControls("txtAdContactPrimaryAddress").Text = String.Empty
        Me.mForm.FindControls("txtAdContactSecondaryAddress").Text = String.Empty
        Me.mForm.FindControls("ckbAdContactMainCompanyAddress").Checked = False
        Me.mForm.FindControls("txtAdContactCity").Text = String.Empty

        Me.LoadCombo(Me.mForm.FindControls("ddlAdContactCountry"), Me.GetCountryData("[Select a Country]"), String.Empty)
        Me.LoadCombo(Me.mForm.FindControls("ddlAdContactState"), Me.GetStateData(Me.mForm.FindControls("ddlAdContactCountry").SelectedValue, "[Select a State]"), String.Empty)

        Me.mForm.FindControls("txtAdContactZipCode").Text = String.Empty
        Me.mForm.FindControls("txtAdContactProvidence").Text = String.Empty
        Me.mForm.FindControls("txtAdContactDepartment").Text = String.Empty
        Me.mForm.FindControls("txtAdContactNotes").Text = String.Empty
        Me.mForm.FindControls("txtAdContactFirstName").Focus()
    End Sub

    Private Sub PopulateData()
        Me.mForm.FindControls("txtAdContactID").Text = Me.mData.ID

        Me.LoadCombo(Me.mForm.FindControls("ddlAdContactAdvertiser"), Me.GetComboDataSources(Me.GetAdAccountInfoList, "[Select a Advertiser]"), Me.mData.Advertiser.ID)

        Me.mForm.FindControls("txtAdContactFirstName").Text = Me.mData.FirstName
        Me.mForm.FindControls("txtAdContactLastName").Text = Me.mData.LastName
        Me.mForm.FindControls("txtAdContactPrimaryAddress").Text = Me.mData.PrimaryAddress
        Me.mForm.FindControls("txtAdContactSecondaryAddress").Text = Me.mData.SecondaryAddress
        Me.mForm.FindControls("ckbAdContactMainCompanyAddress").Checked = Me.mData.MainCompanyAddress
        Me.mForm.FindControls("txtAdContactCity").Text = Me.mData.City

        Me.LoadCombo(Me.mForm.FindControls("ddlAdContactCountry"), Me.GetCountryData("[Select a Country]"), Me.mData.Country)
        Me.LoadCombo(Me.mForm.FindControls("ddlAdContactState"), Me.GetStateData(Me.mForm.FindControls("ddlAdContactCountry").SelectedValue, "[Select a State]"), Me.mData.State)

        Me.mForm.FindControls("txtAdContactZipCode").Text = Me.mData.ZipCode
        Me.mForm.FindControls("txtAdContactProvidence").Text = Me.mData.Providence
        Me.mForm.FindControls("txtAdContactDepartment").Text = Me.mData.Department
        Me.mForm.FindControls("txtAdContactNotes").Text = Me.mData.ResposibleForNotes
        Me.mForm.FindControls("txtAdContactFirstName").Focus()
    End Sub

    Private Function GetDataID(ByVal textBox As TextBox) As String
        If textBox.Text = String.Empty Then
            Return 0
        End If
        Return textBox.Text
    End Function

    Private Function CollectData() As AdContactNewData
        Dim formData As New AdContactNewData
        formData.ID.SetValues("txtAdContactID", True, Me.mData.ID, CLng(Me.GetDataID(Me.mForm.FindControls("txtAdContactID"))))

        formData.Advertiser.ID.SetValues("ddlAdContactAdvertiser", False, Me.mData.Advertiser.ID, CLng(Me.mForm.FindControls("ddlAdContactAdvertiser").SelectedItem.Value))
        formData.Advertiser.CompanyName.SetValues("ddlAdContactAdvertiser", False, Me.mData.Advertiser.CompanyName, Me.mForm.FindControls("ddlAdContactAdvertiser").SelectedItem.Text)

        formData.FirstName.SetValues("txtAdContactFirstName", False, Me.mData.FirstName, Me.mForm.FindControls("txtAdContactFirstName").Text)
        formData.LastName.SetValues("txtAdContactLastName", False, Me.mData.LastName, Me.mForm.FindControls("txtAdContactLastName").Text)
        formData.PrimaryAddress.SetValues("txtAdContactPrimaryAddress", False, Me.mData.PrimaryAddress, Me.mForm.FindControls("txtAdContactPrimaryAddress").Text)
        formData.SecondaryAddress.SetValues("txtAdContactSecondaryAddress", False, Me.mData.SecondaryAddress, Me.mForm.FindControls("txtAdContactSecondaryAddress").Text)
        formData.MainCompanyAddress.SetValues("ckbAdContactMainCompanyAddress", False, Me.mData.MainCompanyAddress, Me.mForm.FindControls("ckbAdContactMainCompanyAddress").Checked)
        formData.City.SetValues("txtAdContactCity", False, Me.mData.City, Me.mForm.FindControls("txtAdContactCity").Text)
        formData.Country.SetValues("ddlAdContactCountry", False, Me.mData.Country, Me.mForm.FindControls("ddlAdContactCountry").SelectedValue)
        formData.State.SetValues("ddlAdContactState", False, Me.mData.State, Me.mForm.FindControls("ddlAdContactState").SelectedValue)
        formData.ZipCode.SetValues("txtAdContactZipCode", False, Me.mData.ZipCode, Me.mForm.FindControls("txtAdContactZipCode").Text)
        formData.Providence.SetValues("txtAdContactProvidence", False, Me.mData.Providence, Me.mForm.FindControls("txtAdContactProvidence").Text)
        formData.Department.SetValues("txtAdContactDepartment", False, Me.mData.Department, Me.mForm.FindControls("txtAdContactDepartment").Text)
        formData.ResposibleForNotes.SetValues("txtAdContactNotes", False, Me.mData.ResposibleForNotes, Me.mForm.FindControls("txtAdContactNotes").Text)
        Return formData
    End Function

    Private Function CollectDataID() As AdContactNewData
        Dim formData As New AdContactNewData
        formData.ID.SetValues("txtAdContactID", True, Me.mData.ID, CLng(Me.GetDataID(Me.mForm.FindControls("txtAdContactID"))))
        Return formData
    End Function

    Private Function CollectDataID(ByVal adContactID As String) As AdContactNewData
        Dim formData As New AdContactNewData
        formData.ID.SetValues("txtAdContactID", True, 0, CLng(adContactID))
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

    Private Function GetData(ByVal adContactID As String) As AdContactData
        Try
            Return ClsSessionAdmin.GetAdContact(Me.CollectDataID(adContactID))
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return Nothing
        End Try
    End Function

    Private Function SaveData() As Boolean
        Try
            If Me.mEditMode Then
                Me.mData = ClsSessionAdmin.EditAdContact(Me.CollectData)
            Else
                Me.mData = ClsSessionAdmin.AddAdContact(Me.CollectData)
            End If
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error saving: " & SysEx.Message)
            Return False
        End Try
    End Function

    Private Function DeleteData() As Boolean
        Try
            ClsSessionAdmin.DeleteAdContact(Me.CollectDataID)
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error deleting: " & SysEx.Message)
            Return False
        End Try
    End Function

#End Region

End Class
