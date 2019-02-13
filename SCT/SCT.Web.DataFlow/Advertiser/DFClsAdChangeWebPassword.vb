Imports SCT.Library
Imports System.Web.UI.WebControls

Public Class DFClsAdChangeWebPassword

#Region " Private Fields "

    Private mForm As Object
    Private mData As AdAccountData

#End Region

#Region " Properties And Methods "

    Public WriteOnly Property Form() As Object
        Set(ByVal value As Object)
            Me.mForm = value
        End Set
    End Property

    Public Sub New(ByRef form As Object)
        Me.mForm = form
        Me.mForm.Session("ValuePath") = "frmAdAccount"

        ApplyPageAuthorizationRules()

        If IsNumeric(WebSite.ClsSessionAdmin.GetSessionUserID) AndAlso WebSite.ClsSessionAdmin.GetSessionUserID <> 0 Then
            Me.mData = Me.GetData(WebSite.ClsSessionAdmin.GetSessionUserID)
            If Me.mData IsNot Nothing Then
                Me.ApplyAddFormAuthorizationRules(True)
                Me.PopulateData()
            End If
        End If
    End Sub

    Public Sub ApplyPageAuthorizationRules()
        If Not WebSite.ClsSessionAdmin.IsValidForm("frmAdChangeWebPassword", "txtAdAccountID", "txtAdAccountLogin", "txtAdAccountCurrentPassword", "txtAdAccountNewWebPassword", "txtAdAccountWebConfirm") Then Me.mForm.Response.Redirect("~/Forms/frmMain.aspx")
        Me.mForm.FindControls("cmdOK").Enabled = False
    End Sub

    Public Sub ApplyAddFormAuthorizationRules(ByVal editMode As Boolean)
        Me.mForm.FindControls("cmdOK").Enabled = editMode
    End Sub

    Public Function ValidatePassword(ByVal ControlName As String) As Boolean
        Return Me.mForm.FindControls(ControlName).Text = Me.mData.WebPassword
    End Function

    Public Sub OnOk()
        If Me.SaveData Then
            Me.mForm.FindControls("MsgBox").ShowMessage("The password was changed successfully.")
        End If
    End Sub

    Public Sub OnReturn()
        Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdAccount.aspx")
    End Sub

#End Region

#Region " Data Methods "

    Private Sub PopulateData()
        Me.mForm.FindControls("txtAdAccountLogin").Text = Me.mData.Login
        Me.mForm.FindControls("txtAdAccountID").Text = Me.mData.ID
        Me.mForm.FindControls("txtAdAccountCurrentPassword").Focus()
    End Sub

    Private Function GetDataID(ByVal textBox As TextBox) As String
        If textBox.Text = String.Empty Then
            Return 0
        End If
        Return textBox.Text
    End Function

    Private Function CollectData()
        Dim formData As New AdAccountNewData()
        formData.ID.SetValues("txtAdAccountID", True, Me.mData.ID, CLng(Me.GetDataID(Me.mForm.FindControls("txtAdAccountID"))))
        formData.Login.SetValues("txtAdAccountLogin", False, Me.mData.Login, Me.mForm.FindControls("txtAdAccountLogin").Text)
        formData.WebPassword.SetValues("txtAdAccountNewWebPassword", False, Me.mData.WebPassword, Me.mForm.FindControls("txtAdAccountNewWebPassword").Text)
        formData.CompanyName.SetValues(String.Empty, False, Me.mData.CompanyName, Me.mData.CompanyName)
        formData.CompanyNotes.SetValues(String.Empty, False, Me.mData.CompanyNotes, Me.mData.CompanyName)
        Return formData
    End Function

    Private Function CollectDataID(ByVal accountID As String) As AdAccountNewData
        Dim formData As New AdAccountNewData
        formData.ID.SetValues("txtAdAccountID", True, 0, CLng(accountID))
        Return formData
    End Function

    Private Function GetData(ByVal accountID As String) As AdAccountData
        Try
            Return WebSite.ClsSessionAdmin.GetAdAccount(Me.CollectDataID(accountID))
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return Nothing
        End Try
    End Function

    Private Function SaveData() As Boolean
        Try
            WebSite.ClsSessionAdmin.EditAdAccount(Me.CollectData)
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error saving: " & SysEx.Message)
            Return False
        End Try
    End Function

#End Region

End Class
