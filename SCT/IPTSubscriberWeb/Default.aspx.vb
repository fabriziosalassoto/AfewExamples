
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.txtUserName.Focus()
    End Sub

    Public Function ValidateUser(ByVal userName As String, ByVal password As String) As Boolean
        Try
            If SCT.Library.Security.ClsSCTSubscriberPrincipal.Login(userName, password) Then
                System.Web.HttpContext.Current.Session("CslaPrincipal") = Csla.ApplicationContext.User
                System.Web.Security.FormsAuthentication.SetAuthCookie(userName, True)
                Return True
            Else
                Me.txtUserName.Focus()
                Return False
            End If
        Catch CslaEx As Csla.DataPortalException
            Me.MsgBox.ShowMessage(CslaEx.BusinessException.Message)
            Return False
        Catch SysEx As Exception
            Me.MsgBox.ShowMessage(SysEx.Message)
            Return False
        End Try
    End Function

    Protected Sub CustomValidator_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles CustomValidator.ServerValidate
        args.IsValid = Me.ValidateUser(Me.txtUserName.Text, Me.txtPassword.Text)
    End Sub

    Protected Sub cmdSignIn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSignIn.Click
        If Page.IsValid Then
            Me.Response.Redirect("~/WebForms/frmLocation.aspx")
        End If
    End Sub

    Protected Sub cmdSignUp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSignUp.Click
        Response.Redirect("http://localhost:1000/IPTSystemWeb/frmSubscriberSignUp.aspx")
    End Sub
End Class
