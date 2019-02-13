Imports Csla
Imports SCT.Library

Public Class DFClsWebService

    Private mWebService As Object

    Public Sub New(ByVal webService As Object)
        Me.mWebService = webService
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

    Public Sub SetAdDisplay(ByVal AdHistoryInfo As ClsAdvertisingInfo)
        Try
            Me.SaveAdDisplay(AdHistoryInfo)
        Catch ex As Csla.DataPortalException
            Throw ex.BusinessException
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub SetAdClickThru(ByVal AdHistoryInfo As ClsAdvertisingInfo)
        Try
            Me.SaveAdClickThru(AdHistoryInfo)
        Catch ex As Csla.DataPortalException
            Throw ex.BusinessException
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Function GetAdInfo() As ClsAdvertisingInfo
        Dim Subscriber As SCT.DataFlow.WebService.ClsConnectedSubscriber

        Try
            mWebService.Application.Lock()

            Subscriber = mWebService.Application("ConnectedSubscriberList").GetItem(mWebService.Session("ConnectedSubscriberID"))
            GetAdInfo = GetAdvertisingInfo(Subscriber)

        Catch ex As Csla.DataPortalException
            Throw ex.BusinessException
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            mWebService.Application.UnLock()
        End Try
    End Function

    Public Function OpenConnection(ByVal serverRequest As ClsServerRequest) As ClsServerResponse
        Dim Subscriber As SCT.DataFlow.WebService.ClsConnectedSubscriber

        Try
            mWebService.Application.Lock()

            Subscriber = SCT.DataFlow.WebService.ClsConnectedSubscriber.NewConnectedSubscriber(Mid(Csla.ApplicationContext.User.Identity.Name, 1, Csla.ApplicationContext.User.Identity.Name.IndexOf(";")), serverRequest.HostIP)
            mWebService.Application("ConnectedSubscriberList").Add(Subscriber)
            mWebService.Session("ConnectedSubscriberID") = Subscriber.ID
            OpenConnection = ClsServerResponse.NewServerResponse(GetActionToTake(Subscriber), GetAdvertisingInfo(Subscriber))

        Catch ex As Csla.DataPortalException
            Throw ex.BusinessException
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            mWebService.Application.UnLock()
        End Try
    End Function

    Public Function ReConnection(ByVal serverRequest As ClsServerRequest) As ClsServerResponse
        Dim Subscriber As SCT.DataFlow.WebService.ClsConnectedSubscriber

        Try
            mWebService.Application.Lock()

            Subscriber = mWebService.Application("ConnectedSubscriberList").GetItem(mWebService.Session("ConnectedSubscriberID"))
            Subscriber.HostIP = serverRequest.HostIP
            ReConnection = ClsServerResponse.NewServerResponse(GetActionToTake(Subscriber), Nothing)

        Catch ex As Csla.DataPortalException
            Throw ex.BusinessException
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            mWebService.Application.UnLock()
        End Try
    End Function

    Public Sub CloseConnection()
        mWebService.Session.Abandon()
    End Sub

    Public Function IsConnectedSubscriber(ByVal subscriberID As Long) As Boolean
        mWebService.Application.Lock()
        IsConnectedSubscriber = CType(mWebService.Application("ConnectedSubscriberList"), SCT.DataFlow.WebService.ClsConnectedSubscribersList).Contains(subscriberID)
        mWebService.Application.UnLock()
    End Function

    Public Function ConnectedSubscriberCount() As Long
        mWebService.Application.Lock()
        ConnectedSubscriberCount = CType(mWebService.Application("ConnectedSubscriberList"), SCT.DataFlow.WebService.ClsConnectedSubscribersList).Count
        mWebService.Application.UnLock()
    End Function

    Public Function GetConnectedSubcriber(ByVal subscriberID As Long) As SCT.DataFlow.WebService.ClsConnectedSubscriber
        mWebService.Application.Lock()
        GetConnectedSubcriber = CType(mWebService.Application("ConnectedSubscriberList"), SCT.DataFlow.WebService.ClsConnectedSubscribersList).GetItem(subscriberID)
        mWebService.Application.UnLock()
    End Function

    Public Function GetConnectedSubcriberList() As SCT.DataFlow.WebService.ClsConnectedSubscribersList
        mWebService.Application.Lock()
        GetConnectedSubcriberList = CType(mWebService.Application("ConnectedSubscriberList"), SCT.DataFlow.WebService.ClsConnectedSubscribersList)
        mWebService.Application.UnLock()
    End Function

    Private Function GetActionToTake(ByVal Subscriber As SCT.DataFlow.WebService.ClsConnectedSubscriber) As Integer
        Dim StolenReport As SCT.Library.Subscriber.ClsStolenReport = Subscriber.GetStolenReport

        If StolenReport IsNot Nothing Then
            Return StolenReport.ActionToTake
        End If
        Return 0
    End Function

    Private Function GetAdvertisingInfo(ByVal Subscriber As SCT.DataFlow.WebService.ClsConnectedSubscriber) As ClsAdvertisingInfo
        Dim Project As SCT.Library.Advertiser.ClsProject = Subscriber.GetProject

        If Project IsNot Nothing Then
            Return ClsAdvertisingInfo.NewAdvertisingInfo(Me.AddAdHistory(Subscriber.ID, Project), Project.ADUrl)
        End If
        Return Nothing
    End Function

    Private Function AddAdHistory(ByVal subscriberId As Long, ByVal Project As SCT.Library.Advertiser.ClsProject) As Long
        Dim adHistory As SCT.Library.Advertiser.Project.ClsAdHistory = SCT.Library.Advertiser.Project.ClsAdHistory.NewAdHistory
        adHistory.AssignProject(Project.ID)
        adHistory.AssignSubscriberAccount(subscriberId)
        Return adHistory.Save().ID
    End Function

    Private Sub SaveAdDisplay(ByVal AdHistoryInfo As ClsAdvertisingInfo)
        Dim adHistory As SCT.Library.Advertiser.Project.ClsAdHistory = SCT.Library.Advertiser.Project.ClsAdHistory.GetAdHistory(AdHistoryInfo.AdHistoryID)
        adHistory.DateAdDisplay = Date.Today
        adHistory.TimeAdDisplay = Date.Now
        adHistory.URLAdDisplay = AdHistoryInfo.ADUrl
        adHistory.Save()
    End Sub

    Private Sub SaveAdClickThru(ByVal AdHistoryInfo As ClsAdvertisingInfo)
        Dim adHistory As SCT.Library.Advertiser.Project.ClsAdHistory = SCT.Library.Advertiser.Project.ClsAdHistory.GetAdHistory(AdHistoryInfo.AdHistoryID)
        adHistory.DateAdClickThru = Date.Today
        adHistory.TimeAdClickThru = Date.Now
        adHistory.URLAdClickThru = AdHistoryInfo.ADUrl
        adHistory.Save()
    End Sub

    'Private Function GetNextProject(ByVal subscriberId As Long, ByVal LastShownProjectID As Long) As SCT.Library.Advertiser.ClsProject
    '    Dim projectList As SCT.Library.Advertiser.ClsProjectList = SCT.Library.Advertiser.ClsProjectList.GetProjectList(subscriberId)

    '    If projectList.Count > 0 Then
    '        For Each project As SCT.Library.Advertiser.ClsProject In projectList
    '            If project.ID > LastShownProjectID Then
    '                Return project
    '            End If
    '        Next
    '        Return projectList.Item(0)
    '    End If
    '    Return Nothing
    'End Function

    'Private Function AdAddNew(ByVal advertiserId As Long, ByVal subscriberId As Long) As Long
    '    Dim adHistory As SCT.Library.Advertiser.ClsAdHistory = SCT.Library.Advertiser.ClsAdHistory.NewAdHistory
    '    adHistory.AssignAdvertiserAccount(advertiserId)
    '    adHistory.AssignSubscriberAccount(subscriberId)
    '    Return adHistory.Save().ID
    'End Function

    'Private Function GetStolenReport(ByVal SubscriberID As Long) As SCT.Library.Subscriber.ClsStolenReport
    '    Dim StolenReportList As SCT.Library.Subscriber.ClsStolenReportList = SCT.Library.Subscriber.ClsStolenReportList.GetStolenReportList(SubscriberID)

    '    For Each stolenReport As SCT.Library.Subscriber.ClsStolenReport In StolenReportList
    '        If stolenReport.DateReportFound < stolenReport.DateReportMissing Then
    '            Return stolenReport
    '        End If
    '    Next
    '    Return Nothing
    'End Function

End Class
