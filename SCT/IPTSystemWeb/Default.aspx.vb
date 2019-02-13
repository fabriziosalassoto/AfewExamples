
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub cmdAdvertiser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEnter.Click
        If Page.IsValid Then
            Select Case Me.rblSignInAs.SelectedValue
                Case "Advertiser"
                    Response.Redirect("http://localhost:1000/IPTAdvertiserWeb/")
                Case "Subscriber"
                    Response.Redirect("http://localhost:1000/IPTSubscriberWeb/")
                Case Else
            End Select

        End If

    End Sub

End Class
