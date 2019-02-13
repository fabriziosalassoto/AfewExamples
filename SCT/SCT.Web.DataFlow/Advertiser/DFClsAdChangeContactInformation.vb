Imports SCT.Library
Imports System.Globalization
Imports System.Web.UI.WebControls

Public Class DFClsAdChangeContactInformation

#Region " Private Fields "

    Private mForm As Object
    Private mData As AdContactData

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
        Me.mForm.Session("ValuePath") = "frmAdContacts"

        Me.ApplyPageAuthorizationRules()

        If (Not IsNumeric(Me.mForm.Session("contactID"))) OrElse Me.mForm.Session("contactID") = 0 Then
            Me.mData = New AdContactData
            Me.ApplyAddFormAuthorizationRules(False)
            Me.ClearAddForm()
        Else
            Me.mData = Me.GetData(Me.mForm.Session("contactID"))
            If Me.mData IsNot Nothing Then
                Me.ApplyAddFormAuthorizationRules(True)
                Me.PopulateData()
            End If
        End If
    End Sub

    Public Sub ApplyPageAuthorizationRules()
        If Not WebSite.ClsSessionAdmin.IsValidForm("frmAdChangeContactInformation", "txtAdContactID", "txtAdContactFirstName", "txtAdContactLastName", "txtAdContactPrimaryAddress", "txtAdContactSecondaryAddress", "ckbAdContactMainCompanyAddress", "txtAdContactCity", "ddlAdContactCountry", "ddlAdContactState", "txtAdContactZipCode", "txtAdContactProvidence", "txtAdContactDepartment", "txtAdContactNotes") Then Me.mForm.Response.Redirect("~/Forms/frmMain.aspx")
        Me.mForm.FindControls("cmdOK").Enabled = False
    End Sub

    Public Sub ApplyAddFormAuthorizationRules(ByVal editMode As Boolean)
        Me.mEditMode = editMode
        Me.SetFormTitle()
        Me.mForm.FindControls("cmdOK").Enabled = True
    End Sub

    Public Function ValidateExistsMainCompanyAddress() As Boolean
        Return Not Advertiser.ClsContact.ExistsMainCompanyAddress(Me.mData.ID, WebSite.ClsSessionAdmin.GetSessionUserID, Me.mForm.FindControls("ckbAdContactMainCompanyAddress").Checked)
    End Function

    Private Sub SetFormTitle()
        If Me.mEditMode Then
            Me.mForm.FindControls("lblTitle").Text = "Edit Contact Information"
        Else
            Me.mForm.FindControls("lblTitle").Text = "Add New Contact Information"
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

    Public Sub OnOk()
        If Me.SaveData() Then
            Me.mForm.FindControls("MsgBox").ShowMessage("Saved Contact Information.")
            Me.ApplyAddFormAuthorizationRules(True)
            Me.PopulateData()
        End If
    End Sub

    Public Sub OnReturn()
        Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdContacts.aspx")
    End Sub

#End Region

#Region " Data Methods "

    Private Sub ClearAddForm()
        Me.mForm.FindControls("txtAdContactID").Text = String.Empty
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

    Private Function CollectData() As AdContactNewData
        Dim formData As New AdContactNewData
        formData.ID.SetValues("txtAdContactID", True, Me.mData.ID, CLng(Me.GetDataID(Me.mForm.FindControls("txtAdContactID"))))

        formData.Advertiser.ID.SetValues("", False, Me.mData.Advertiser.ID, CLng(WebSite.ClsSessionAdmin.GetSessionUserID))
        formData.Advertiser.CompanyName.SetValues("", False, Me.mData.Advertiser.CompanyName, WebSite.ClsSessionAdmin.GetSessionUserName)

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

    Private Function CollectDataID(ByVal contactID As String) As AdContactNewData
        Dim formData As New AdContactNewData
        formData.ID.SetValues("txtAdContactID", True, 0, CLng(contactID))
        Return formData
    End Function

    Private Function GetDataID(ByVal textBox As TextBox) As String
        If textBox.Text = String.Empty Then
            Return 0
        End If
        Return textBox.Text
    End Function

    Private Function GetData(ByVal contactID As String) As AdContactData
        Try
            Return WebSite.ClsSessionAdmin.GetAdContact(Me.CollectDataID(contactID))
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return Nothing
        End Try
    End Function

    Private Function SaveData() As Boolean
        Try
            If Me.mEditMode Then
                Me.mData = WebSite.ClsSessionAdmin.EditAdContact(Me.CollectData)
            Else
                Me.mData = WebSite.ClsSessionAdmin.AddAdContact(Me.CollectData)
            End If
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error saving: " & SysEx.Message)
            Return False
        End Try
    End Function

#End Region

End Class
