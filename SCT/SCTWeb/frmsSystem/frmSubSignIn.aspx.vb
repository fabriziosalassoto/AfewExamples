
Partial Class frmsSystem_frmSubSignIn
    Inherits System.Web.UI.Page

    Private wcform As SCT.Web.DataFlow.DFClsSubSignIn

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.wcform = New SCT.Web.DataFlow.DFClsSubSignIn(Me)
        Else
            If SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsForm Is Nothing OrElse SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsForm.GetType().Name <> "DFClsSubSignIn" Then
                Me.wcform = New SCT.Web.DataFlow.DFClsSubSignIn(Me)
            Else
                Me.wcform = SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsForm
                Me.wcform.Form = Me
            End If
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsForm = Me.wcform
    End Sub

    Protected Sub CustomValidator_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles CustomValidator.ServerValidate
        args.IsValid = Me.wcform.ValidateUser(Me.txtUserName.Text, Me.txtPassword.Text)
    End Sub

    Protected Sub cmdSignIn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSignIn.Click
        If Page.IsValid Then
            Me.wcform.OnSignIn()
        End If
    End Sub
End Class
