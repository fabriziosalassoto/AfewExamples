
Partial Class frmsSubscriber_frmSubscriberChangeAccountInformation
    Inherits System.Web.UI.Page

    Private wcfrmSubChangeAccountInformation As SCT.Web.DataFlow.DFClsSubChangeAccountInformation

    Public Function FindControls(ByVal Id As String) As System.Web.UI.Control
        Return Me.Controls.Item(0).FindControl("ContentPlaceHolder").FindControl(Id)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.wcfrmSubChangeAccountInformation = New SCT.Web.DataFlow.DFClsSubChangeAccountInformation(Me)
        Else
            Me.wcfrmSubChangeAccountInformation = Me.Session("wcFormSubChangeAccountInformation")
            If Me.wcfrmSubChangeAccountInformation Is Nothing Then
                Me.wcfrmSubChangeAccountInformation = New SCT.Web.DataFlow.DFClsSubChangeAccountInformation(Me)
            Else
                Me.wcfrmSubChangeAccountInformation.Form = Me
            End If
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        Me.Session("wcFormSubChangeAccountInformation") = Me.wcfrmSubChangeAccountInformation
    End Sub

    Protected Sub ddlCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCountry.SelectedIndexChanged
        Me.wcfrmSubChangeAccountInformation.OnSelectedCountry()
    End Sub

    Protected Sub cmdOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        Me.wcfrmSubChangeAccountInformation.OnOk()
    End Sub

    Protected Sub cmdReturn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdReturn.Click
        Me.wcfrmSubChangeAccountInformation.OnReturn()
    End Sub
End Class
