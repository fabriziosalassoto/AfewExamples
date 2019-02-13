Imports Csla
Imports SCT.Library

Public Class DFClsWebService

    Private mWebServer As Object

    Public Sub New(ByVal webServer As Object)
        Me.mWebServer = webServer
    End Sub

    Public Sub UseAnonymous()
        SCT.Library.Security.ClsSCTClientPrincipal.Logout()
    End Sub

    Public Sub Login(ByVal credentials As CslaCredentials)
        If Len(credentials.ComputerSerialNumber) = 0 Then
            Throw New System.Security.SecurityException("Valid credentials not provided")
        End If

        ' set to unauthenticated principal
        SCT.Library.Security.ClsSCTClientPrincipal.Logout()
        SCT.Library.Security.ClsSCTClientPrincipal.Login(credentials.ComputerSerialNumber, credentials.ClientPassword)

        If Not Csla.ApplicationContext.User.Identity.IsAuthenticated Then
            ' the user is not valid, raise an error
            Throw New System.Security.SecurityException("Invalid user or password")
        End If
    End Sub

    Public Function GetAdInfo() As ClsAdvertisingInfo
        Try
            mWebServer.Application.Lock()
            GetAdInfo = GetAdvertisingInfo(mWebServer.Application("AccountList").GetItem(mWebServer.Session("AccountID")))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            mWebServer.Application.UnLock()
        End Try
    End Function

    Public Function OpenConnection(ByVal serverRequest As ClsServerRequest) As ClsServerResponse
        Try
            mWebServer.Application.Lock()

            Dim subAccount As SCT.Library.Subscriber.ClsAccount = mWebServer.Application("AccountList").Connect(Mid(Csla.ApplicationContext.User.Identity.Name, 1, Csla.ApplicationContext.User.Identity.Name.IndexOf(";")), serverRequest.HostIP)
            mWebServer.Session("AccountID") = subAccount.ID
            OpenConnection = ClsServerResponse.NewServerResponse(GetActionToTake(subAccount), GetAdvertisingInfo(subAccount))

        Catch ex As Csla.DataPortalException
            Throw ex.BusinessException
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            mWebServer.Application.UnLock()
        End Try
    End Function

    Public Function ReConnection(ByVal serverRequest As ClsServerRequest) As ClsServerResponse
        Try
            mWebServer.Application.Lock()

            Dim subAccount As SCT.Library.Subscriber.ClsAccount = CType(mWebServer.Application("AccountList"), SCT.Library.Subscriber.ClsAccountList).ReConnect(mWebServer.Session("AccountID"), serverRequest.HostIP)
            ReConnection = ClsServerResponse.NewServerResponse(GetActionToTake(subAccount), Nothing)

        Catch ex As Csla.DataPortalException
            Throw ex.BusinessException
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            mWebServer.Application.UnLock()
        End Try
    End Function

    Public Sub CloseConnection()
        mWebServer.Session.Abandon()
    End Sub

    Private Function GetActionToTake(ByVal SubAccount As SCT.Library.Subscriber.ClsAccount) As Integer
        Dim StolenReport As SCT.Library.Subscriber.ClsStolenReport = SubAccount.StolenReports.GetActiveStolenReport

        If StolenReport IsNot Nothing Then
            Return StolenReport.ActionToTake
        End If
        Return 0
    End Function

    Private Function GetAdvertisingInfo(ByVal SubAccount As SCT.Library.Subscriber.ClsAccount) As ClsAdvertisingInfo
        Dim Project As SCT.Library.Advertiser.ClsProject = Me.GetAdvertisingProject(SubAccount.ID, Me.GetLastProjectID(SubAccount))

        If Project IsNot Nothing Then
            Return ClsAdvertisingInfo.NewAdvertisingInfo(SubAccount.AddAdHistory(Project.ID), Project.ADUrl)
        End If
        Return Nothing
    End Function

    Private Function GetAdvertisingProject(ByVal subscriberID As Long, ByVal lastProjectID As Long) As SCT.Library.Advertiser.ClsProject
        Try
            Dim projectList As SCT.Library.Advertiser.ClsProjectList = SCT.Library.Advertiser.ClsProjectList.GetProjectList(subscriberID)

            For Each project As SCT.Library.Advertiser.ClsProject In projectList
                If project.ID > lastProjectID Then
                    Return project
                End If
            Next
            Return projectList.Item(0)
        Catch ex As Csla.DataPortalException
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Function GetLastProjectID(ByVal SubAccount As SCT.Library.Subscriber.ClsAccount) As Integer
        Dim AdHistory As SCT.Library.Advertiser.ClsAdHistory = SubAccount.AdHistories.GetLastItem

        If AdHistory IsNot Nothing Then
            Return AdHistory.Project.ID
        End If
        Return 0
    End Function

End Class
