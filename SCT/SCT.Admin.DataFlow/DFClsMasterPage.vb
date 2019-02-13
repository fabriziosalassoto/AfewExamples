Public Class DFClsMasterPage

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
        If SCT.Library.AdminSite.ClsSessionAdmin.IsSessionUserAuthenticated Then
            Me.mMasterPage.FindControl("lblUserName").Text = "Welcome: " & SCT.Library.AdminSite.ClsSessionAdmin.GetSessionUserName

            Me.mMasterPage.FindControl("mnuMain").FindItem(Me.mMasterPage.Session("ValuePath")).Enabled = True
            Me.mMasterPage.FindControl("mnuMain").FindItem(Me.mMasterPage.Session("ValuePath")).Selected = True

            Me.mMasterPage.FindControl("mnuMain").FindItem("frmAdvertisers").Enabled = True
            Me.mMasterPage.FindControl("mnuMain").FindItem("frmAdvertisers/frmAdvertiserAccounts").Enabled = DFClsAdvertiserAccounts.CanSelect
            Me.mMasterPage.FindControl("mnuMain").FindItem("frmAdvertisers/frmAdvertiserContacts").Enabled = DFClsAdvertiserContacts.CanSelect
            Me.mMasterPage.FindControl("mnuMain").FindItem("frmAdvertisers/frmAdvertiserProjects").Enabled = DFClsAdvertiserProjects.CanSelect
            Me.mMasterPage.FindControl("mnuMain").FindItem("frmAdvertisers/frmAdvertiserToDos").Enabled = DFClsAdvertiserToDos.CanSelect
            Me.mMasterPage.FindControl("mnuMain").FindItem("frmAdvertisers/frmAdvertiserNotes").Enabled = DFClsAdvertiserNotes.CanSelect
            Me.mMasterPage.FindControl("mnuMain").FindItem("frmAdvertisers/frmAdvertiserProjectHistory").Enabled = DFClsAdvertiserProjectHistory.CanSelect
            Me.mMasterPage.FindControl("mnuMain").FindItem("frmAdvertisers/frmAdvertiserStateOfAccount").Enabled = DFClsAdvertiserStateOfAccount.CanSelect
            Me.mMasterPage.FindControl("mnuMain").FindItem("frmAdvertisers/frmAdvertiserTransactions").Enabled = DFClsAdvertiserTransactions.CanSelect

            Me.mMasterPage.FindControl("mnuMain").FindItem("frmSubscribers").Enabled = True

            Me.mMasterPage.FindControl("mnuMain").FindItem("frmSecurity").Enabled = True
            Me.mMasterPage.FindControl("mnuMain").FindItem("frmSecurity/frmProfiles").Enabled = DFClsProfiles.CanSelect
            Me.mMasterPage.FindControl("mnuMain").FindItem("frmSecurity/frmUsers").Enabled = DFClsUsers.CanSelect
            Me.mMasterPage.FindControl("mnuMain").FindItem("frmSecurity/frmForms").Enabled = DFClsForms.CanSelect
            Me.mMasterPage.FindControl("mnuMain").FindItem("frmSecurity/frmGroups").Enabled = DFClsGroups.CanSelect
            Me.mMasterPage.FindControl("mnuMain").FindItem("frmSecurity/frmFields").Enabled = DFClsFields.CanSelect
            Me.mMasterPage.FindControl("mnuMain").FindItem("frmSecurity/frmOperations").Enabled = DFClsOperations.CanSelect
            Me.mMasterPage.FindControl("mnuMain").FindItem("frmSecurity/frmBinnacleFormEntries").Enabled = DFClsBinnacleFormEntries.CanSelect
        Else
            SCT.Library.AdminSite.ClsSessionAdmin.Logout()
        End If
    End Sub

    Public Sub SignOut()
        SCT.Library.AdminSite.ClsSessionAdmin.Logout()
    End Sub

    Public Sub NavegateTo(ByVal formName As String)
        Me.mMasterPage.Session("ID1") = 0
        Me.mMasterPage.Session("ID2") = 0
        Me.mMasterPage.Response.Redirect("~/Forms/" & formName & ".aspx")
    End Sub

End Class
