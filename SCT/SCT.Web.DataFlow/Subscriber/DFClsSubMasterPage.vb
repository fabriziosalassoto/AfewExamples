Public Class DFClsSubMasterPage

    Private mMasterPage As Object

    Public WriteOnly Property MasterPage() As Object
        Set(ByVal value As Object)
            Me.mMasterPage = value
        End Set
    End Property

    Public Sub New(ByRef masterPage As Object)
        Me.mMasterPage = masterPage
        Me.ApplyAuthorizationRules()
    End Sub

    Public Sub ApplyAuthorizationRules()
        If SCT.Library.WebSite.ClsSessionAdmin.IsSessionUserAuthenticated Then
            Me.mMasterPage.FindControl("lblUserName").Text = "Welcome: " & SCT.Library.WebSite.ClsSessionAdmin.GetSessionUserName

            Me.mMasterPage.FindControl("mnuMain").FindItem(Me.mMasterPage.Session("ValuePath")).Enabled = True
            Me.mMasterPage.FindControl("mnuMain").FindItem(Me.mMasterPage.Session("ValuePath")).Selected = True
        Else
            SCT.Library.WebSite.ClsSessionAdmin.LogoutSubscriber()
        End If
    End Sub

    Public Sub OnSignOut()
        SCT.Library.WebSite.ClsSessionAdmin.LogoutSubscriber()
    End Sub

    Public Sub OnMenuItemClick(ByVal item As System.Web.UI.WebControls.MenuItem)
        Me.mMasterPage.Response.Redirect("~/frmsSubscriber/" & item.Value & ".aspx")
    End Sub

End Class
