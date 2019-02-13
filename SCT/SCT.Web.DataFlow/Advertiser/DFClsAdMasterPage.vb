Public Class DFClsAdMasterPage

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
            SCT.Library.WebSite.ClsSessionAdmin.LogoutAdvertiser()
        End If
    End Sub

    Public Sub OnSignOut()
        SCT.Library.WebSite.ClsSessionAdmin.LogoutAdvertiser()
    End Sub

    Public Sub Navegate(ByVal value As String)
        Me.mMasterPage.Session("contactID") = 0
        Me.mMasterPage.Response.Redirect("~/frmsAdvertiser/" & value & ".aspx")
    End Sub

End Class
