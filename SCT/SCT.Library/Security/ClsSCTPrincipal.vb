Imports System.Security.Principal

Namespace Security
    <Serializable()> Public Class ClsSCTPrincipal
        Inherits Csla.Security.BusinessPrincipalBase

        Private Sub New(ByVal identity As IIdentity)
            MyBase.New(identity)
        End Sub

        Public Shared Function Login(ByVal username As String, ByVal password As String) As Boolean
            Dim identity As ClsSCTSubcriberIdentity = ClsSCTSubcriberIdentity.GetIdentity(username, password)
            If identity.IsAuthenticated Then
                Csla.ApplicationContext.User = New ClsSCTPrincipal(identity)
            End If
            Return identity.IsAuthenticated
        End Function

        Public Shared Sub Logout()
            Csla.ApplicationContext.User = New ClsSCTPrincipal(ClsSCTSubcriberIdentity.UnauthenticatedIdentity)
        End Sub

        Public Overrides Function IsInRole(ByVal role As String) As Boolean
            Return DirectCast(Me.Identity, ClsSCTSubcriberIdentity).IsInRole(role)
        End Function
    End Class
End Namespace
