
Partial Class Forms_MasterPage
    Inherits System.Web.UI.MasterPage

    Private wcMaster As SCT.Admin.DataFlow.DFClsMasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.wcMaster = New SCT.Admin.DataFlow.DFClsMasterPage(Me)
        Else
            If SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsMaster Is Nothing OrElse SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsMaster.GetType().Name <> "DFClsMasterPage" Then
                Me.wcMaster = New SCT.Admin.DataFlow.DFClsMasterPage(Me)
            Else
                Me.wcMaster = SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsMaster
                Me.wcMaster.MasterPage = Me
            End If
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsMaster = Me.wcMaster
    End Sub

    Protected Sub mnuUser_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles mnuUser.MenuItemClick
        Select Case e.Item.Value
            Case "SignOut"
                Me.wcMaster.SignOut()
            Case Else
                Me.wcMaster.NavegateTo(e.Item.Value)
        End Select
    End Sub

    Protected Sub mnuMain_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles mnuMain.MenuItemClick
        Me.wcMaster.NavegateTo(e.Item.Value)
    End Sub
End Class

