
Partial Class MasterPage_MasterPage
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim User As System.Security.Principal.IPrincipal = Csla.ApplicationContext.User
        Dim menuItem As MenuItem = Me.mnuMain.FindItem(Me.Session("ValuePath"))

        If User.Identity.IsAuthenticated Then Me.lblUserName.Text = "Welcome: " & Mid(User.Identity.Name, User.Identity.Name.IndexOf(";") + 2)
        menuItem.Selected = menuItem IsNot Nothing
    End Sub

    Protected Sub mnuSignOut_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles mnuSignOut.MenuItemClick
        SCT.Library.Security.ClsSCTAdvertiserPrincipal.Logout()
        System.Web.HttpContext.Current.Session("CslaPrincipal") = Csla.ApplicationContext.User
        System.Web.Security.FormsAuthentication.SignOut()
        System.Web.Security.FormsAuthentication.RedirectToLoginPage()
    End Sub

    Protected Sub mnuMain_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles mnuMain.MenuItemClick
        Select Case e.Item.Value
            Case "Statistics"
                Me.Response.Redirect("~/WebForms/frmStatistics.aspx")
            Case "YourAccount"
                Me.Response.Redirect("~/WebForms/frmAccount.aspx")
            Case "YourProjects"
                Me.Response.Redirect("~/WebForms/frmProjects.aspx")
            Case "YourContacts"
                Me.Response.Redirect("~/WebForms/frmContacts.aspx")
            Case Else
        End Select
    End Sub
End Class

