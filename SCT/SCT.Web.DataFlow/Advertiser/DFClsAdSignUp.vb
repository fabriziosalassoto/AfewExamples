Imports SCT.Library

Public Class DFClsAdSignUp

#Region " Private Fields "
    Private mForm As Object
#End Region

#Region " Properties And Methods "

    Public WriteOnly Property Form() As Object
        Set(ByVal value As Object)
            Me.mForm = value
        End Set
    End Property

    Public Sub New(ByRef form As Object)
        Me.mForm = form
    End Sub

    Public Function ValidateLoginExists(ByVal ControlName As String) As Boolean
        Return Not Advertiser.ClsAccount.Exists(0, Me.mForm.FindControl(ControlName).Text)
    End Function

    Public Sub OnOk()
        If Me.SaveData() Then
            Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdAccount.aspx")
        End If
    End Sub

    Public Sub OnCancel()
        Me.mForm.Response.Redirect("~/frmsSystem/frmAdvertiserSignIn.aspx")
    End Sub

#End Region

#Region " Data Methods "

    Private Function CollectData() As AdAccountNewData
        Dim formData As New AdAccountNewData
        formData.ID.SetValues("txtAdAccountID", True, 0, 0)
        formData.Login.SetValues("txtAdAccountLogin", False, String.Empty, Me.mForm.FindControl("txtAdAccountLogin").Text)
        formData.WebPassword.SetValues("txtAdAccountWebPassword", False, String.Empty, Me.mForm.FindControl("txtAdAccountWebPassword").Text)
        formData.CompanyName.SetValues("txtAdAccountCompanyName", False, String.Empty, Me.mForm.FindControl("txtAdAccountCompanyName").Text)
        formData.CompanyNotes.SetValues("txtAdAccountCompanyNote", False, String.Empty, Me.mForm.FindControl("txtAdAccountCompanyNote").Text)
        Return formData
    End Function

    Private Function SaveData() As Boolean
        Try
            WebSite.ClsSessionAdmin.SignUpAdAccount(Me.CollectData)
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControl("MsgBox").ShowMessage("Error saving: " & SysEx.Message)
            Return False
        End Try
    End Function

#End Region

End Class
