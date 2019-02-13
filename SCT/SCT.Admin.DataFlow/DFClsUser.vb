Imports SCT.Library
Imports SCT.Library.AdminSite
Imports System.Web.UI.WebControls

Public Class DFClsUser

#Region " Private Fields "

    Private mForm As Object
    Private mData As UserData

    Private mEditMode As Boolean

#End Region

#Region " Authorization Rules "

    Public Shared Function CanSelect() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmUser") Then
            Return ClsSessionAdmin.CanSelectInForm("frmUser")
        Else
            Return ClsSessionAdmin.CanSelectInForm("AllForms")
        End If
    End Function

    Public Shared Function CanInsert() As Boolean
        Return ClsSessionAdmin.CanInsertInForm("frmUser")
    End Function

    Public Shared Function CanUpdate() As Boolean
        Return ClsSessionAdmin.CanUpdateInForm("frmUser")
    End Function

    Public Shared Function CanDelete() As Boolean
        Return ClsSessionAdmin.CanDeleteInForm("frmUser")
    End Function

    Public Shared Function CanSelectField(ByVal fieldName As String) As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmUser") Then
            Return ClsSessionAdmin.CanSelectFieldInForm("frmUser", fieldName)
        Else
            Return ClsSessionAdmin.CanSelectFieldInForm("AllForms", "AllFields")
        End If
    End Function

    Public Shared Function CanInsertField(ByVal fieldName As String) As Boolean
        Return ClsSessionAdmin.CanInsertFieldInForm("frmUser", fieldName)
    End Function

    Public Shared Function CanUpdateField(ByVal fieldName As String) As Boolean
        Return ClsSessionAdmin.CanUpdateFieldInForm("frmUser", fieldName)
    End Function

    Public Shared Function CanDeleteField(ByVal fieldName As String) As Boolean
        Return ClsSessionAdmin.CanDeleteFieldInForm("frmUser", fieldName)
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
        Me.mForm.Session("ValuePath") = "frmSecurity/frmUsers"

        Me.ApplyPageAuthorizationRules()

        If (Not IsNumeric(Me.mForm.Session("ID1"))) OrElse Me.mForm.Session("ID1") = 0 Then
            Me.mEditMode = False
            Me.mData = New UserData
            Me.ApplyAddFormAuthorizationRules(False)
            Me.ClearAddForm()
        Else
            Me.mEditMode = True
            Me.mData = Me.GetData(Me.mForm.Session("ID1"))
            If Me.mData IsNot Nothing Then
                Me.ApplyAddFormAuthorizationRules(True)
                Me.PopulateData()
            End If
        End If
    End Sub

    Public Sub ApplyPageAuthorizationRules()
        Dim SelectField As Boolean

        If (Not ClsSessionAdmin.IsValidForm("frmUser", "txtUserID", "txtUserFirstName", "txtUserLastName", "ddlUserProfile", "txtUserLogin", "txtUserPassword")) OrElse (Not DFClsUser.CanSelect()) Then Me.mForm.Response.Redirect("~/Forms/frmMain.aspx")

        SelectField = DFClsUser.CanSelectField("txtUserID")
        Me.mForm.FindControls("txtUserID").Visible = SelectField
        Me.mForm.FindControls("lblUserID").Visible = SelectField
        Me.mForm.FindControls("txtUserID").Enabled = False

        SelectField = DFClsUser.CanSelectField("txtUserFirstName")
        Me.mForm.FindControls("txtUserFirstName").Visible = SelectField
        Me.mForm.FindControls("lblUserFirstName").Visible = SelectField
        Me.mForm.FindControls("txtUserFirstName").Enabled = False

        SelectField = DFClsUser.CanSelectField("txtUserLastName")
        Me.mForm.FindControls("txtUserLastName").Visible = SelectField
        Me.mForm.FindControls("lblUserLastName").Visible = SelectField
        Me.mForm.FindControls("txtUserLastName").Enabled = False

        SelectField = DFClsUser.CanSelectField("ddlUserProfile")
        Me.mForm.FindControls("ddlUserProfile").Visible = SelectField
        Me.mForm.FindControls("lblUserProfile").Visible = SelectField
        Me.mForm.FindControls("ddlUserProfile").Enabled = False

        SelectField = DFClsUser.CanSelectField("txtUserLogin")
        Me.mForm.FindControls("txtUserLogin").Visible = SelectField
        Me.mForm.FindControls("lblUserLogin").Visible = SelectField
        Me.mForm.FindControls("txtUserLogin").Enabled = False

        SelectField = DFClsUser.CanSelectField("txtUserPassword")
        Me.mForm.FindControls("txtUserPassword").Visible = SelectField
        Me.mForm.FindControls("lblUserPassword").Visible = SelectField
        Me.mForm.FindControls("txtUserPassword").Enabled = False

        Me.mForm.FindControls("mnuItem").FindItem("Profile").Enabled = False
        Me.mForm.FindControls("mnuItem").FindItem("Delete").Enabled = False

        Me.mForm.FindControls("mnuSave").FindItem("Ok").Enabled = False
        Me.mForm.FindControls("mnuSave").FindItem("Save").Enabled = False
    End Sub

    Public Sub ApplyAddFormAuthorizationRules(ByVal editMode As Boolean)
        Dim canSave As Boolean

        Me.mForm.FindControls("txtUserFirstName").Enabled = (Not editMode AndAlso DFClsUser.CanInsertField("txtUserFirstName")) OrElse (editMode AndAlso DFClsUser.CanUpdateField("txtUserFirstName"))
        Me.mForm.FindControls("txtUserLastName").Enabled = (Not editMode AndAlso DFClsUser.CanInsertField("txtUserLastName")) OrElse (editMode AndAlso DFClsUser.CanUpdateField("txtUserLastName"))
        Me.mForm.FindControls("ddlUserProfile").Enabled = (Not editMode AndAlso DFClsUser.CanInsertField("ddlUserProfile")) OrElse (editMode AndAlso DFClsUser.CanUpdateField("ddlUserProfile"))
        Me.mForm.FindControls("txtUserLogin").Enabled = (Not editMode AndAlso DFClsUser.CanInsertField("txtUserLogin")) OrElse (editMode AndAlso DFClsUser.CanUpdateField("txtUserLogin"))
        Me.mForm.FindControls("txtUserPassword").Enabled = (Not editMode AndAlso DFClsUser.CanInsertField("txtUserPassword")) OrElse (editMode AndAlso DFClsUser.CanUpdateField("txtUserPassword"))

        Me.ApplyMenuItemAuthorizationRules(editMode)

        canSave = (Not editMode AndAlso DFClsUser.CanInsert) OrElse (editMode AndAlso DFClsUser.CanUpdate)
        Me.mForm.FindControls("mnuSave").FindItem("Ok").Enabled = canSave
        Me.mForm.FindControls("mnuSave").FindItem("Save").Enabled = canSave
    End Sub

    Private Sub ApplyMenuItemAuthorizationRules(ByVal editMode As Boolean)
        Me.mForm.FindControls("mnuItem").FindItem("Profile").Enabled = editMode AndAlso DFClsProfiles.CanSelect
        Me.mForm.FindControls("mnuItem").FindItem("Delete").Enabled = editMode AndAlso DFClsUser.CanDelete
    End Sub

    Public Function ValidateExistsLogin(ByVal ControlName As String) As Boolean
        Return Not ClsUser.Exists(Me.mData.ID, Me.mForm.FindControls(ControlName).Text)
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

    Private Function GetComboDataSources(ByVal InfoList() As ProfileData, ByVal FirstItemText As String) As DataTable
        Dim table As New DataTable

        table.Columns.Add("Description", GetType(String))
        table.Columns.Add("ID", GetType(String))

        table.DefaultView.Sort = "Description"

        table.Rows.Add(New String() {FirstItemText, "0"})
        For Each Info As ProfileData In InfoList
            table.Rows.Add(New String() {Info.Description, Info.ID.ToString})
        Next

        Return table
    End Function

    Public Sub OnProfile()
        Me.mForm.Session("ID1") = Me.mData.Profile.ID
        Me.mForm.Response.Redirect("~/Forms/frmProfiles.aspx")
    End Sub

    Public Sub OnDeleting()
        Me.mForm.FindControls("MsgBox").ShowConfirmation("Are you sure you want to delete this Profile?\n\nNote: there is no undo.", "", True, False)
    End Sub

    Public Sub OnMsgBoxOk()
        If Me.DeleteData Then
            Me.mForm.Response.Redirect("~/Forms/frmUsers.aspx")
        End If
    End Sub

    Public Sub OnSave()
        If Me.SaveData Then
            Me.mEditMode = True
            Me.ApplyMenuItemAuthorizationRules(True)
            Me.PopulateData()
        End If
    End Sub

    Public Sub OnOk()
        If Me.SaveData Then
            Me.mForm.Response.Redirect("~/Forms/frmUsers.aspx")
        End If
    End Sub

    Public Sub OnCancel()
        Me.mForm.Response.Redirect("~/Forms/frmUsers.aspx")
    End Sub

#End Region

#Region " Data Methods "

    Private Sub ClearAddForm()
        Me.mForm.FindControls("txtUserID").Text = String.Empty
        Me.mForm.FindControls("txtUserFirstName").Text = String.Empty
        Me.mForm.FindControls("txtUserLastName").Text = String.Empty
        Me.LoadCombo(Me.mForm.FindControls("ddlUserProfile"), Me.GetComboDataSources(Me.GetProfileInfoList, "[Select a Profile]"), 0)
        Me.mForm.FindControls("txtUserLogin").Text = String.Empty
        Me.mForm.FindControls("txtUserPassword").Text = String.Empty
        Me.mForm.FindControls("txtUserFirstName").Focus()
    End Sub

    Private Sub PopulateData()
        Me.mForm.FindControls("txtUserID").Text = Me.mData.ID
        Me.mForm.FindControls("txtUserFirstName").Text = Me.mData.FirstName
        Me.mForm.FindControls("txtUserLastName").Text = Me.mData.LastName
        Me.LoadCombo(Me.mForm.FindControls("ddlUserProfile"), Me.GetComboDataSources(Me.GetProfileInfoList, "[Select a Profile]"), Me.mData.Profile.ID)
        Me.mForm.FindControls("txtUserLogin").Text = Me.mData.Login
        Me.mForm.FindControls("txtUserPassword").Text = Me.mData.Password
        Me.mForm.FindControls("txtUserFirstName").Focus()
    End Sub

    Private Function GetDataID(ByVal textBox As TextBox) As String
        If textBox.Text = String.Empty Then
            Return 0
        End If
        Return textBox.Text
    End Function

    Private Function GetDataPassword(ByVal textBox As TextBox) As String
        If textBox.Text = String.Empty Then
            Return Me.mData.Password
        End If
        Return textBox.Text
    End Function

    Private Function CollectData() As UserNewData
        Dim formData As New UserNewData
        formData.ID.SetValues("txtUserID", True, Me.mData.ID, CLng(Me.GetDataID(Me.mForm.FindControls("txtUserID"))))
        formData.FirstName.SetValues("txtUserFirstName", False, Me.mData.FirstName, Me.mForm.FindControls("txtUserFirstName").Text)
        formData.LastName.SetValues("txtUserLastName", False, Me.mData.LastName, Me.mForm.FindControls("txtUserLastName").Text)
        formData.Profile.ID.SetValues("ddlUserProfile", False, Me.mData.Profile.ID, CLng(Me.mForm.FindControls("ddlUserProfile").SelectedItem.Value))
        formData.Profile.Description.SetValues("ddlUserProfile", False, Me.mData.Profile.Description, Me.mForm.FindControls("ddlUserProfile").SelectedItem.Text)
        formData.Login.SetValues("txtUserLogin", False, Me.mData.Login, Me.mForm.FindControls("txtUserLogin").Text)
        formData.Password.SetValues("txtUserPassword", False, Me.mData.Password, Me.GetDataPassword(Me.mForm.FindControls("txtUserPassword")))
        Return formData
    End Function

    Private Function CollectDataID() As UserNewData
        Dim formData As New UserNewData
        formData.ID.SetValues("txtUserID", True, Me.mData.ID, CLng(Me.GetDataID(Me.mForm.FindControls("txtUserID"))))
        Return formData
    End Function

    Private Function CollectDataID(ByVal userID As String) As UserNewData
        Dim formData As New UserNewData
        formData.ID.SetValues("txtUserID", True, 0, CLng(userID))
        Return formData
    End Function

    Private Function GetProfileInfoList() As ProfileData()
        Try
            Return ClsSessionAdmin.GetProfileInfoList()
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return New ProfileData() {}
        End Try
    End Function

    Private Function GetData(ByVal userID As String) As UserData
        Try
            Return ClsSessionAdmin.GetUser(Me.CollectDataID(userID))
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return Nothing
        End Try
    End Function

    Private Function SaveData() As Boolean
        Try
            If Me.mEditMode Then
                Me.mData = ClsSessionAdmin.EditUser(Me.CollectData)
            Else
                Me.mData = ClsSessionAdmin.AddUser(Me.CollectData)
            End If
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error saving: " & SysEx.Message)
            Return False
        End Try
    End Function

    Private Function DeleteData() As Boolean
        Try
            ClsSessionAdmin.DeleteUser(Me.CollectDataID)
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error deleting: " & SysEx.Message)
            Return False
        End Try
    End Function

#End Region

End Class
