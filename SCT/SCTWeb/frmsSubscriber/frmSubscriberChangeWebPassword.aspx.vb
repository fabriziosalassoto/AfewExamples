
Partial Class frmsSubscriber_frmSubscriberChangeWebPassword
    Inherits System.Web.UI.Page

    Private wcfrmSubChangeWebPassword As SCT.Web.DataFlow.DFClsSubChangeWebPassword

    Public Function FindControls(ByVal Id As String) As System.Web.UI.Control
        Return Me.Controls.Item(0).FindControl("ContentPlaceHolder").FindControl(Id)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.wcfrmSubChangeWebPassword = New SCT.Web.DataFlow.DFClsSubChangeWebPassword(Me)
        Else
            Me.wcfrmSubChangeWebPassword = Me.Session("wcFormSubChangeWebPassword")
            If Me.wcfrmSubChangeWebPassword Is Nothing Then
                Me.wcfrmSubChangeWebPassword = New SCT.Web.DataFlow.DFClsSubChangeWebPassword(Me)
            Else
                Me.wcfrmSubChangeWebPassword.Form = Me
            End If
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        Me.Session("wcFormSubChangeWebPassword") = Me.wcfrmSubChangeWebPassword
    End Sub

    Protected Sub ctvCurrentPassword_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles ctvCurrentPassword.ServerValidate
        args.IsValid = Me.wcfrmSubChangeWebPassword.ValidatePassword(CType(source, System.Web.UI.WebControls.CustomValidator).ControlToValidate)
    End Sub

    Protected Sub cmdOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        If Page.IsValid Then
            Me.wcfrmSubChangeWebPassword.OnOk()
        End If
    End Sub

    Protected Sub cmdReturn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdReturn.Click
        Me.wcfrmSubChangeWebPassword.OnReturn()
    End Sub

End Class
