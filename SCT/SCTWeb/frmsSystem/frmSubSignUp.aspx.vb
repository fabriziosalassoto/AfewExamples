Imports SCT.Web.DataFlow

Partial Class frmsSystem_frmSubSignUp
    Inherits System.Web.UI.Page

    Private wcform As DFClsSubSignUp

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.wcform = New DFClsSubSignUp(Me)
        Else
            Me.wcform = Me.Session("wcformSubSignUp")
            If Me.wcform Is Nothing Then
                Me.wcform = New DFClsSubSignUp(Me)
            Else
                Me.wcform.Form = Me
            End If
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        Me.Session("wcformSubSignUp") = Me.wcform
    End Sub

    Protected Sub ctvLogin_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles ctvLogin.ServerValidate
        args.IsValid = Me.wcform.ValidateLoginExists(CType(source, System.Web.UI.WebControls.CustomValidator).ControlToValidate)
    End Sub

    Protected Sub ctvSerialNbr_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles ctvSerialNbr.ServerValidate
        args.IsValid = Me.wcform.ValidateSerialNbrExists(CType(source, System.Web.UI.WebControls.CustomValidator).ControlToValidate)
    End Sub

    Protected Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.wcform.OnCancel()
    End Sub

    Protected Sub cmdOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        If Page.IsValid Then
            Me.wcform.OnOk()
        End If
    End Sub

    Protected Sub ddlCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCountry.SelectedIndexChanged
        Me.wcform.OnSelectedCountry()
    End Sub
End Class
