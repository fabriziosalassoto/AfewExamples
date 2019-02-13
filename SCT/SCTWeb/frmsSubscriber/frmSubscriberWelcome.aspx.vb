
Partial Class frmsSubscriber_frmSubscriberWelcome
    Inherits System.Web.UI.Page

    Private wcfrmSubWelcome As SCT.Web.DataFlow.DFClsSubWelcome

    Public Function FindControls(ByVal Id As String) As System.Web.UI.Control
        Return Me.Controls.Item(0).FindControl("ContentPlaceHolder").FindControl(Id)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.wcfrmSubWelcome = New SCT.Web.DataFlow.DFClsSubWelcome(Me)
        Else
            Me.wcfrmSubWelcome = Me.Session("wcFormSubWelcome")
            If Me.wcfrmSubWelcome Is Nothing Then
                Me.wcfrmSubWelcome = New SCT.Web.DataFlow.DFClsSubWelcome(Me)
            Else
                Me.wcfrmSubWelcome.Form = Me
            End If
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        Me.Session("wcFormSubWelcome") = Me.wcfrmSubWelcome
    End Sub

    Protected Sub cmdOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        Me.wcfrmSubWelcome.OnOk()
    End Sub

    Protected Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.wcfrmSubWelcome.OnCancel()
    End Sub

End Class
