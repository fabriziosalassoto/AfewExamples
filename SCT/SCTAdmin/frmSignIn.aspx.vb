
Partial Class Forms_frmSignIn
    Inherits System.Web.UI.Page

    Private wcForm As SCT.Admin.DataFlow.DFClsSignIn

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.wcForm = New SCT.Admin.DataFlow.DFClsSignIn(Me)
        Else
            If SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsForm Is Nothing OrElse SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsForm.GetType().Name <> "DFClsSignIn" Then
                Me.wcForm = New SCT.Admin.DataFlow.DFClsSignIn(Me)
            Else
                Me.wcForm = SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsForm
                Me.wcForm.Form = Me
            End If
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsForm = Me.wcForm
    End Sub

    Protected Sub cvUser_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvUser.ServerValidate
        args.IsValid = Me.wcForm.ValidateUser(Me.txtUserName.Text, Me.txtPassword.Text)
    End Sub

    Protected Sub cmdSignIn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSignIn.Click
        If Page.IsValid Then
            Me.wcForm.SignIn()
        End If
    End Sub

End Class
