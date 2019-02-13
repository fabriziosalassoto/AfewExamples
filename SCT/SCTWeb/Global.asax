<%@ Application Language="VB" %>

<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
    End Sub
    
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub
        
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when an unhandled error occurs
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started
        SCT.Library.WebSite.ClsSessionAdmin.NewUserSessionValues()
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
    End Sub
    
    Protected Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        ' Code that runs when
    End Sub
  
    Protected Sub Application_AcquireRequestState(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim principal As System.Security.Principal.IPrincipal
        Try
            principal = SCT.Library.WebSite.ClsSessionAdmin.GetSessionCslaPrincipal
        Catch
            principal = Nothing
        End Try
    
        If principal Is Nothing Then
            ' didn't get a principal from Session, so set it to an unauthenticted PTPrincipal
            SCT.Library.Security.ClsSCTPrincipal.Logout()
        Else
            ' use the principal from Session
            Csla.ApplicationContext.User = principal
        End If
    End Sub
       
</script>