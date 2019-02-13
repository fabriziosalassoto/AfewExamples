Imports SCT.Library
Imports SCT.Library.AdminSite
Imports System.Web.UI.WebControls

Public Class DFClsAdvertiserAccount

#Region " Private Fields "

    Private mForm As Object
    Private mData As AdAccountData

    Private mEditMode As Boolean

#End Region

#Region " Authorization Rules "

    Public Shared Function CanSelect() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserAccount") Then
            Return ClsSessionAdmin.CanSelectInForm("frmAdvertiserAccount")
        Else
            Return ClsSessionAdmin.CanSelectInForm("AllForms")
        End If
    End Function

    Public Shared Function CanInsert() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserAccount") Then
            Return ClsSessionAdmin.CanInsertInForm("frmAdvertiserAccount")
        Else
            Return ClsSessionAdmin.CanInsertInForm("AllForms")
        End If
    End Function

    Public Shared Function CanUpdate() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserAccount") Then
            Return ClsSessionAdmin.CanUpdateInForm("frmAdvertiserAccount")
        Else
            Return ClsSessionAdmin.CanUpdateInForm("AllForms")
        End If
    End Function

    Public Shared Function CanDelete() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserAccount") Then
            Return ClsSessionAdmin.CanDeleteInForm("frmAdvertiserAccount")
        Else
            Return ClsSessionAdmin.CanDeleteInForm("AllForms")
        End If
    End Function

    Public Shared Function CanSelectField(ByVal fieldName As String) As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserAccount") Then
            Return ClsSessionAdmin.CanSelectFieldInForm("frmAdvertiserAccount", fieldName)
        Else
            Return ClsSessionAdmin.CanSelectFieldInForm("AllForms", "AllFields")
        End If
    End Function

    Public Shared Function CanInsertField(ByVal fieldName As String) As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserAccount") Then
            Return ClsSessionAdmin.CanInsertFieldInForm("frmAdvertiserAccount", fieldName)
        Else
            Return ClsSessionAdmin.CanInsertFieldInForm("AllForms", "AllFields")
        End If
    End Function

    Public Shared Function CanUpdateField(ByVal fieldName As String) As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserAccount") Then
            Return ClsSessionAdmin.CanUpdateFieldInForm("frmAdvertiserAccount", fieldName)
        Else
            Return ClsSessionAdmin.CanUpdateFieldInForm("AllForms", "AllFields")
        End If
    End Function

    Public Shared Function CanDeleteField(ByVal fieldName As String) As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserAccount") Then
            Return ClsSessionAdmin.CanDeleteFieldInForm("frmAdvertiserAccount", fieldName)
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
        Me.mForm.Session("ValuePath") = "frmAdvertisers/frmAdvertiserAccounts"

        Me.ApplyPageAuthorizationRules()

        If (Not IsNumeric(Me.mForm.Session("ID1"))) OrElse Me.mForm.Session("ID1") = 0 Then
            Me.mData = New AdAccountData
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

        If (Not ClsSessionAdmin.IsValidForm("frmAdvertiserAccount", "txtAdAccountID", "txtAdAccountCompanyName", "txtAdAccountCompanyNotes", "txtAdAccountLogin", "txtAdAccountPassword")) OrElse (Not DFClsAdvertiserAccount.CanSelect()) Then Me.mForm.Response.Redirect("~/Forms/frmMain.aspx")

        SelectField = DFClsAdvertiserAccount.CanSelectField("txtAdAccountID")
        Me.mForm.FindControls("txtAdAccountID").Visible = SelectField
        Me.mForm.FindControls("lblAdAccountID").Visible = SelectField
        Me.mForm.FindControls("txtAdAccountID").Enabled = False

        SelectField = DFClsAdvertiserAccount.CanSelectField("txtAdAccountCompanyName")
        Me.mForm.FindControls("txtAdAccountCompanyName").Visible = SelectField
        Me.mForm.FindControls("lblAdAccountCompanyName").Visible = SelectField
        Me.mForm.FindControls("txtAdAccountCompanyName").Enabled = False

        SelectField = DFClsAdvertiserAccount.CanSelectField("txtAdAccountCompanyNotes")
        Me.mForm.FindControls("txtAdAccountCompanyNotes").Visible = SelectField
        Me.mForm.FindControls("lblAdAccountCompanyNotes").Visible = SelectField
        Me.mForm.FindControls("txtAdAccountCompanyNotes").Enabled = False

        SelectField = DFClsAdvertiserAccount.CanSelectField("txtAdAccountLogin")
        Me.mForm.FindControls("txtAdAccountLogin").Visible = SelectField
        Me.mForm.FindControls("lblAdAccountLogin").Visible = SelectField
        Me.mForm.FindControls("txtAdAccountLogin").Enabled = False

        SelectField = DFClsAdvertiserAccount.CanSelectField("txtAdAccountPassword")
        Me.mForm.FindControls("txtAdAccountPassword").Visible = SelectField
        Me.mForm.FindControls("lblAdAccountPassword").Visible = SelectField
        Me.mForm.FindControls("txtAdAccountPassword").Enabled = False

        Me.mForm.FindControls("mnuItem").FindItem("Delete").Enabled = False

        Me.mForm.FindControls("mnuSave").FindItem("Ok").Enabled = False
        Me.mForm.FindControls("mnuSave").FindItem("Save").Enabled = False
    End Sub

    Public Sub ApplyAddFormAuthorizationRules(ByVal editMode As Boolean)
        Dim canSave As Boolean

        Me.mEditMode = editMode

        Me.mForm.FindControls("txtAdAccountCompanyName").Enabled = (Not editMode AndAlso DFClsAdvertiserAccount.CanInsertField("txtAdAccountCompanyName")) OrElse (editMode AndAlso DFClsAdvertiserAccount.CanUpdateField("txtAdAccountCompanyName"))
        Me.mForm.FindControls("txtAdAccountCompanyNotes").Enabled = (Not editMode AndAlso DFClsAdvertiserAccount.CanInsertField("txtAdAccountCompanyNotes")) OrElse (editMode AndAlso DFClsAdvertiserAccount.CanUpdateField("txtAdAccountCompanyNotes"))
        Me.mForm.FindControls("txtAdAccountLogin").Enabled = (Not editMode AndAlso DFClsAdvertiserAccount.CanInsertField("txtAdAccountLogin")) OrElse (editMode AndAlso DFClsAdvertiserAccount.CanUpdateField("txtAdAccountLogin"))
        Me.mForm.FindControls("txtAdAccountPassword").Enabled = (Not editMode AndAlso DFClsAdvertiserAccount.CanInsertField("txtAdAccountPassword")) OrElse (editMode AndAlso DFClsAdvertiserAccount.CanUpdateField("txtAdAccountPassword"))

        Me.mForm.FindControls("mnuItem").FindItem("Delete").Enabled = editMode AndAlso DFClsAdvertiserAccount.CanDelete

        canSave = (Not editMode AndAlso DFClsAdvertiserAccount.CanInsert) OrElse (editMode AndAlso DFClsAdvertiserAccount.CanUpdate)
        Me.mForm.FindControls("mnuSave").FindItem("Ok").Enabled = canSave
        Me.mForm.FindControls("mnuSave").FindItem("Save").Enabled = canSave
    End Sub

    Public Function ValidateExistsLogin() As Boolean
        Return Not Advertiser.ClsAccount.Exists(Me.mData.ID, Me.mForm.FindControls("txtAdAccountLogin").Text)
    End Function

    Public Function ValidateRequiredPassword() As Boolean
        Return Me.mForm.FindControls("txtAdAccountPassword").Text <> String.Empty OrElse Me.mData.WebPassword <> String.Empty
    End Function

    Public Sub OnDeleting()
        Me.mForm.FindControls("MsgBox").ShowConfirmation("Are you sure you want to delete this Account?\n\nNote: there is no undo.", "", True, False)
    End Sub

    Public Sub OnMsgBoxOk()
        If Me.DeleteData Then
            Me.mForm.Response.Redirect("~/Forms/frmAdvertiserAccounts.aspx")
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
            Me.mForm.Response.Redirect("~/Forms/frmAdvertiserAccounts.aspx")
        End If
    End Sub

    Public Sub OnCancel()
        Me.mForm.Response.Redirect("~/Forms/frmAdvertiserAccounts.aspx")
    End Sub

#End Region

#Region " Data Methods "

    Private Sub ClearAddForm()
        Me.mForm.FindControls("txtAdAccountID").Text = String.Empty
        Me.mForm.FindControls("txtAdAccountCompanyName").Text = String.Empty
        Me.mForm.FindControls("txtAdAccountCompanyNotes").Text = String.Empty
        Me.mForm.FindControls("txtAdAccountLogin").Text = String.Empty
        Me.mForm.FindControls("txtAdAccountPassword").Text = String.Empty
        Me.mForm.FindControls("txtAdAccountLogin").Focus()
    End Sub

    Private Sub PopulateData()
        Me.mForm.FindControls("txtAdAccountID").Text = Me.mData.ID
        Me.mForm.FindControls("txtAdAccountCompanyName").Text = Me.mData.CompanyName
        Me.mForm.FindControls("txtAdAccountCompanyNotes").Text = Me.mData.CompanyNotes
        Me.mForm.FindControls("txtAdAccountLogin").Text = Me.mData.Login
        Me.mForm.FindControls("txtAdAccountPassword").Text = Me.mData.WebPassword
        Me.mForm.FindControls("txtAdAccountLogin").Focus()
    End Sub

    Private Function GetDataID(ByVal textBox As TextBox) As String
        If textBox.Text = String.Empty Then
            Return 0
        End If
        Return textBox.Text
    End Function

    Private Function GetDataPassword(ByVal textBox As TextBox) As String
        If textBox.Text = String.Empty Then
            Return Me.mData.WebPassword
        End If
        Return textBox.Text
    End Function

    Private Function CollectData() As AdAccountNewData
        Dim formData As New AdAccountNewData
        formData.ID.SetValues("txtAdAccountID", True, Me.mData.ID, CLng(Me.GetDataID(Me.mForm.FindControls("txtAdAccountID"))))
        formData.CompanyName.SetValues("txtAdAccountCompanyName", False, Me.mData.CompanyName, Me.mForm.FindControls("txtAdAccountCompanyName").Text)
        formData.CompanyNotes.SetValues("txtAdAccountCompanyNotes", False, Me.mData.CompanyNotes, Me.mForm.FindControls("txtAdAccountCompanyNotes").Text)
        formData.Login.SetValues("txtAdAccountLogin", False, Me.mData.Login, Me.mForm.FindControls("txtAdAccountLogin").Text)
        formData.WebPassword.SetValues("txtAdAccountPassword", False, Me.mData.WebPassword, Me.GetDataPassword(Me.mForm.FindControls("txtAdAccountPassword")))
        Return formData
    End Function

    Private Function CollectDataID() As AdAccountNewData
        Dim formData As New AdAccountNewData
        formData.ID.SetValues("txtAdAccountID", True, Me.mData.ID, CLng(Me.GetDataID(Me.mForm.FindControls("txtAdAccountID"))))
        Return formData
    End Function

    Private Function CollectDataID(ByVal AdAccountID As String) As AdAccountNewData
        Dim formData As New AdAccountNewData
        formData.ID.SetValues("txtAdAccountID", True, 0, CLng(AdAccountID))
        Return formData
    End Function

    Private Function GetData(ByVal AdAccountID As String) As AdAccountData
        Try
            Return ClsSessionAdmin.GetAdAccount(Me.CollectDataID(AdAccountID))
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return Nothing
        End Try
    End Function

    Private Function SaveData() As Boolean
        Try
            If Me.mEditMode Then
                Me.mData = ClsSessionAdmin.EditAdAccount(Me.CollectData)
            Else
                Me.mData = ClsSessionAdmin.AddAdAccount(Me.CollectData)
            End If
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error saving: " & SysEx.Message)
            Return False
        End Try
    End Function

    Private Function DeleteData() As Boolean
        Try
            ClsSessionAdmin.DeleteAdAccount(Me.CollectDataID)
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error deleting: " & SysEx.Message)
            Return False
        End Try
    End Function

#End Region

End Class
