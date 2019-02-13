
Partial Class MasterPages_MasterPage
    Inherits System.Web.UI.MasterPage

    Private wcMaster As SCT.Web.DataFlow.DFClsSubMasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.wcMaster = New SCT.Web.DataFlow.DFClsSubMasterPage(Me)
        Else
            If SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsMaster Is Nothing OrElse SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsMaster.GetType().Name <> "DFClsSubMasterPage" Then
                Me.wcMaster = New SCT.Web.DataFlow.DFClsSubMasterPage(Me)
            Else
                Me.wcMaster = SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsMaster
                Me.wcMaster.MasterPage = Me
            End If
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsMaster = Me.wcMaster
    End Sub

    Protected Sub mnuSignOut_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles mnuSignOut.MenuItemClick
        Me.wcMaster.OnSignOut()
    End Sub

    Protected Sub mnuMain_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles mnuMain.MenuItemClick
        Me.wcMaster.OnMenuItemClick(e.Item)
    End Sub

End Class