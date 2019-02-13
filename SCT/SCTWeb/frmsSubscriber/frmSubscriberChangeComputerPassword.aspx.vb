
Partial Class frmsSubscriber_frmSubscriberChangeComputerPassword
    Inherits System.Web.UI.Page

    Private wcfrmSubChangeComputerPassword As SCT.Web.DataFlow.DFClsSubChangeComputerPassword

    Public Function FindControls(ByVal Id As String) As System.Web.UI.Control
        Return Me.Controls.Item(0).FindControl("ContentPlaceHolder").FindControl(Id)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.wcfrmSubChangeComputerPassword = New SCT.Web.DataFlow.DFClsSubChangeComputerPassword(Me)
        Else
            Me.wcfrmSubChangeComputerPassword = Me.Session("wcFormSubChangeComputerPassword")
            If Me.wcfrmSubChangeComputerPassword Is Nothing Then
                Me.wcfrmSubChangeComputerPassword = New SCT.Web.DataFlow.DFClsSubChangeComputerPassword(Me)
            Else
                Me.wcfrmSubChangeComputerPassword.Form = Me
            End If
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        Me.Session("wcFormSubChangeComputerPassword") = Me.wcfrmSubChangeComputerPassword
    End Sub

    Protected Sub ctvCurrentPassword_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles ctvCurrentPassword.ServerValidate
        args.IsValid = Me.wcfrmSubChangeComputerPassword.ValidatePassword(CType(source, System.Web.UI.WebControls.CustomValidator).ControlToValidate)
    End Sub

    Protected Sub cmdOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        If Page.IsValid Then
            Me.wcfrmSubChangeComputerPassword.OnOk()
        End If
    End Sub

    Protected Sub cmdReturn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdReturn.Click
        Me.wcfrmSubChangeComputerPassword.OnReturn()
    End Sub

End Class
