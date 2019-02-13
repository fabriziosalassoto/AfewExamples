Imports System.Security.Principal

Namespace Security
    <Serializable()> Public Class ClsSCTAdvertiserPrincipal
        Inherits Csla.Security.BusinessPrincipalBase

        Private Sub New(ByVal identity As IIdentity)
            MyBase.New(identity)
        End Sub

        Public Shared Function Login(ByVal username As String, ByVal password As String) As Boolean
            Dim identity As ClsSCTAdvertiserIdentity = ClsSCTAdvertiserIdentity.GetIdentity(username, password)
            If identity.IsAuthenticated Then
                Csla.ApplicationContext.User = New ClsSCTAdvertiserPrincipal(identity)
            End If
            Return identity.IsAuthenticated
        End Function

        Public Shared Sub Logout()
            Csla.ApplicationContext.User = New ClsSCTAdvertiserPrincipal(ClsSCTAdvertiserIdentity.UnauthenticatedIdentity)
        End Sub

        Public Overrides Function IsInRole(ByVal role As String) As Boolean
            Return DirectCast(Me.Identity, ClsSCTAdvertiserIdentity).IsInRole(role)
        End Function
    End Class
End Namespace