Imports Csla
Imports SCT.Library
Imports System.Web.Services.Protocols

Public Class DFClsWebServer

#Region " Private Fields "

    Private mWebServer As Object
    Private mAccount As Subscriber.ClsAccount
    Private mAccountList As Subscriber.ClsAccountList

#End Region

#Region " Constructor "

    Public Sub New(ByVal webServer As Object)
        Me.mWebServer = webServer
    End Sub

#End Region

#Region " Security "

    Public Sub UseAnonymous()
        SCT.Library.Security.ClsSCTClientPrincipal.Logout()
    End Sub

    Public Sub Login(ByVal credentials As CslaCredentials)
        If Len(credentials.ComputerSerialNumber) = 0 OrElse Len(credentials.ClientPassword) = 0 Then
            Throw New SoapException("Valid credentials not provided.", SoapException.ClientFaultCode)
        End If

        ' set to unauthenticated principal
        SCT.Library.Security.ClsSCTClientPrincipal.Logout()
        SCT.Library.Security.ClsSCTClientPrincipal.Login(credentials.ComputerSerialNumber, credentials.ClientPassword)

        If Not Csla.ApplicationContext.User.Identity.IsAuthenticated Then
            ' the user is not valid, raise an error
            Throw New SoapException("Invalid Computer Serial Number or Password.", SoapException.ClientFaultCode)
        End If
    End Sub

#End Region

#Region " Server Methods "

    Public Sub SetAdDisplay(ByVal AdHistoryInfo As ClsAdvertisingInfo)
        Dim adHistory As Advertiser.Project.ClsAdHistory
        Dim account As Subscriber.ClsAccount
        Dim accountList As Subscriber.ClsAccountList

        Try
            Me.mWebServer.Application.Lock()

            accountList = Me.mWebServer.Application("AccountList").Clone
            account = accountList.GetItem(Me.mWebServer.Session("AccountID"))

            If account IsNot Nothing Then
                adHistory = account.AdHistories.GetItem(AdHistoryInfo.AdHistoryID)
                If adHistory IsNot Nothing Then
                    adHistory.DateAdDisplay = Date.Today
                    adHistory.TimeAdDisplay = Date.Now
                    adHistory.URLAdDisplay = AdHistoryInfo.ADUrl

                    Me.mWebServer.Application("AccountList") = accountList.Save()
                End If
            Else
                Throw New SoapException("Subscriber account doesn't exists.", SoapException.ClientFaultCode)
            End If
        Catch ex As SoapException
            Throw ex
        Catch ex As Csla.DataPortalException
            Throw New SoapException("Internal server error.", SoapException.ServerFaultCode)
        Catch ex As Exception
            Throw New SoapException("Internal server error.", SoapException.ServerFaultCode)
        Finally
            Me.mWebServer.Application.UnLock()
        End Try
    End Sub

    Public Sub SetAdClickThru(ByVal AdHistoryInfo As ClsAdvertisingInfo)
        Dim adHistory As Advertiser.Project.ClsAdHistory
        Dim account As Subscriber.ClsAccount
        Dim accountList As Subscriber.ClsAccountList

        Try
            Me.mWebServer.Application.Lock()

            accountList = Me.mWebServer.Application("AccountList").Clone
            account = accountList.GetItem(Me.mWebServer.Session("AccountID"))

            If account IsNot Nothing Then
                adHistory = account.AdHistories.GetItem(AdHistoryInfo.AdHistoryID)
                If adHistory IsNot Nothing Then
                    adHistory.DateAdClickThru = Date.Today
                    adHistory.TimeAdClickThru = Date.Now
                    adHistory.URLAdClickThru = AdHistoryInfo.ADUrl

                    Me.mWebServer.Application("AccountList") = accountList.Save()
                End If
            Else
                Throw New SoapException("Subscriber account doesn't exists.", SoapException.ClientFaultCode)
            End If
        Catch ex As SoapException
            Throw ex
        Catch ex As Csla.DataPortalException
            Throw New SoapException("Internal server error.", SoapException.ServerFaultCode)
        Catch ex As Exception
            Throw New SoapException("Internal server error.", SoapException.ServerFaultCode)
        Finally
            Me.mWebServer.Application.UnLock()
        End Try
    End Sub

    Public Function GetAdvertisingInfo(ByVal subscribertime As Date) As ClsAdvertisingInfo
        Dim project As Advertiser.ClsProject
        Dim account As Subscriber.ClsAccount
        Dim accountList As Subscriber.ClsAccountList
        
        Try
            Me.mWebServer.Application.Lock()

            accountList = Me.mWebServer.Application("AccountList").Clone
            account = accountList.GetItem(Me.mWebServer.Session("AccountID"))

            If account IsNot Nothing Then
                If account.Connected Then

                    project = SCT.Library.Advertiser.ClsProjectList.GetProjectList().FindAllSubscriberProjects(account.Demographics).FindAllActiveProjects(subscribertime).GetNextProjectToShow(account.AdHistories.GetLastItem)
                    If project IsNot Nothing Then
                        account.AdHistories.AddSubscriberItem(project.ID)

                        Me.mWebServer.Application("AccountList") = accountList.Save()
                        GetAdvertisingInfo = ClsAdvertisingInfo.NewAdvertisingInfo(account.GetLastAdHistoryId, project.ADUrl, project.ADHeight, project.ADWidth)
                    Else
                        GetAdvertisingInfo = Nothing
                    End If
                Else
                    Throw New SoapException("Subscriber is Disconnected.", SoapException.ClientFaultCode)
                End If
            Else
                Throw New SoapException("Subscriber account doesn't exists.", SoapException.ClientFaultCode)
            End If
        Catch ex As SoapException
            Throw ex
        Catch ex As Csla.DataPortalException
            Throw New SoapException("Internal server error.", SoapException.ServerFaultCode)
        Catch ex As Exception
            Throw New SoapException("Internal server error.", SoapException.ServerFaultCode)
        Finally
            Me.mWebServer.Application.UnLock()
        End Try
    End Function

    Public Sub SetSystemConfiguration(ByVal systemConfigurationInfo As ClsComputerConfigurationInfo)
        Dim account As Subscriber.ClsAccount = Nothing
        Dim accountList As Subscriber.ClsAccountList

        Try
            Me.mWebServer.Application.Lock()

            accountList = Me.mWebServer.Application("AccountList").Clone
            account = accountList.GetItem(Me.mWebServer.Session("AccountID"))

            If account IsNot Nothing Then

                If Len(systemConfigurationInfo.ComputerMacAddress) = 0 OrElse account.ComputerMacAddress = systemConfigurationInfo.ComputerMacAddress OrElse (Not accountList.Contains(systemConfigurationInfo.ComputerMacAddress)) Then
                    account.ComputerMacAddress = systemConfigurationInfo.ComputerMacAddress
                Else
                    Throw New SoapException("System configuration is assigned to another subscriber account.", SoapException.ClientFaultCode)
                End If

                account.ComputerName = systemConfigurationInfo.ComputerName
                account.ComputerHDSerialNumber = systemConfigurationInfo.ComputerHDSerialNumber
                account.ConfigurationHasChanged = False

                Me.mWebServer.Application("AccountList") = accountList.Save()
            Else
                Throw New SoapException("Subscriber account doesn't exists.", SoapException.ClientFaultCode)
            End If
        Catch ex As SoapException
            Throw ex
        Catch ex As Csla.DataPortalException
            Throw New SoapException("Internal server error.", SoapException.ServerFaultCode)
        Catch ex As Exception
            Throw New SoapException("Internal server error.", SoapException.ServerFaultCode)
        Finally
            Me.mWebServer.Application.UnLock()
        End Try
    End Sub

    Public Function OpenConnection(ByVal connectionRequest As ClsConnectionRequest) As ClsConnectionResponse
        Dim account As Subscriber.ClsAccount
        Dim accountList As Subscriber.ClsAccountList

        Try
            Me.mWebServer.Application.Lock()

            accountList = Me.mWebServer.Application("AccountList").Clone
            account = accountList.GetItem(Mid(Csla.ApplicationContext.User.Identity.Name, 1, Csla.ApplicationContext.User.Identity.Name.IndexOf(";")))

            If account IsNot Nothing Then
                If Not account.Connected Then

                    Me.CheckComputerConfiguration(accountList, account, connectionRequest.ComputerConfigurationInfo)

                    If connectionRequest.FirstConnection Then
                        If Not account.ConfigurationHasChanged Then
                            account.InstallClientProgram = True
                        Else
                            Throw New SoapException("Subscribers account has another system configuration.", SoapException.ClientFaultCode)
                        End If
                    End If

                    account.Connected = True
                    account.ConnectionHistories.Add(Me.mWebServer.Context.Request.ServerVariables("REMOTE_ADDR"), connectionRequest.HostIPInfo.HostLocalIP)

                    Me.mWebServer.Application("AccountList") = accountList.Save()
                    Me.mWebServer.Session("AccountID") = account.ID

                    OpenConnection = ClsConnectionResponse.NewConnectionResponse(account.GetActionToTake(True), Me.mWebServer.Context.Request.ServerVariables("REMOTE_ADDR"))
                Else
                    Throw New SoapException("Subscriber is connected.", SoapException.ClientFaultCode)
                End If
            Else
                Throw New SoapException("Subscriber account doesn't exists.", SoapException.ClientFaultCode)
            End If
        Catch ex As SoapException
            Throw ex
        Catch ex As Csla.DataPortalException
            Throw New SoapException("Internal server error.", SoapException.ServerFaultCode)
        Catch ex As Exception
            Throw New SoapException("Internal server error.", SoapException.ServerFaultCode)
        Finally
            Me.mWebServer.Application.UnLock()
        End Try
    End Function

    Public Function KeepConnection(ByVal hostIPInfo As ClsHostIPInfo) As ClsConnectionResponse
        Dim account As Subscriber.ClsAccount
        Dim accountList As Subscriber.ClsAccountList

        Try
            Me.mWebServer.Application.Lock()

            accountList = Me.mWebServer.Application("AccountList").Clone
            account = accountList.GetItem(Me.mWebServer.Session("AccountID"))

            If account IsNot Nothing Then
                If account.Connected Then
                    If account.ConnectionHistories.GetLastItem.HostIP <> Me.mWebServer.Context.Request.ServerVariables("REMOTE_ADDR") OrElse account.ConnectionHistories.GetLastItem.HostLocalIP <> hostIPInfo.HostLocalIP Then
                        account.ConnectionHistories.Add(Me.mWebServer.Context.Request.ServerVariables("REMOTE_ADDR"), hostIPInfo.HostLocalIP)

                        Me.mWebServer.Application("AccountList") = accountList.Save()
                    End If
                    KeepConnection = ClsConnectionResponse.NewConnectionResponse(account.GetActionToTake(False), Me.mWebServer.Context.Request.ServerVariables("REMOTE_ADDR"))
                Else
                    Throw New SoapException("Subscriber is Disconnected.", SoapException.ClientFaultCode)
                End If
            Else
                Throw New SoapException("Subscriber account doesn't exists.", SoapException.ClientFaultCode)
            End If
        Catch ex As SoapException
            Throw ex
        Catch ex As Csla.DataPortalException
            Throw New SoapException("Internal server error.", SoapException.ServerFaultCode)
        Catch ex As Exception
            Throw New SoapException("Internal server error.", SoapException.ServerFaultCode)
        Finally
            Me.mWebServer.Application.UnLock()
        End Try
    End Function

    Public Sub CloseConnection()
        Me.mWebServer.Session.Abandon()
    End Sub

#End Region

#Region " Server Tools "

    Public Function IsSubscriberConnected(ByVal subscriberID As Long) As Boolean
        Try
            Me.mWebServer.Application.Lock()

            Me.mAccount = Me.mWebServer.Application("AccountList").GetItem(subscriberID)
            If Me.mAccount IsNot Nothing Then
                IsSubscriberConnected = Me.mAccount.Connected
            Else
                Throw New Exception("Subscriber account doesn't exists")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            Me.mWebServer.Application.UnLock()
        End Try
    End Function

    Public Function SubscriberCount() As Long
        Me.mWebServer.Application.Lock()
        SubscriberCount = Me.mWebServer.Application("AccountList").Count
        Me.mWebServer.Application.UnLock()
    End Function

    Public Function ConnectedSubscriberCount() As Long
        Dim count As Long = 0
        Me.mWebServer.Application.Lock()
        For Each account As Subscriber.ClsAccount In Me.mWebServer.Application("AccountList")
            If account.Connected Then
                count += 1
            End If
        Next
        Me.mWebServer.Application.UnLock()
        Return count
    End Function

    Public Function DisconnectedSubscriberCount() As Long
        Dim count As Long = 0
        Me.mWebServer.Application.Lock()
        For Each account As Subscriber.ClsAccount In Me.mWebServer.Application("AccountList")
            If Not account.Connected Then
                count += 1
            End If
        Next
        Me.mWebServer.Application.UnLock()
        Return count
    End Function

#End Region

#Region " Subscriber Account "

    Public Sub AddAccount(ByVal accountData As ClsSubscriberAccountData)
        Try
            Me.mWebServer.Application.Lock()
            Me.mAccountList = Me.mWebServer.Application("AccountList").Clone

            Me.mAccount = Subscriber.ClsAccount.NewAccount
            Me.CollectAccountData(Me.mAccount, accountData)

            Me.mAccountList.AddItem(Me.mAccount)
            Me.mWebServer.Application("AccountList") = Me.mAccountList.Save()

        Catch ex As Csla.DataPortalException
            Throw ex.BusinessException
        Catch ex As Csla.Validation.ValidationException
            Dim newEx As New Exception(ex.Message)
            For Each BrokenRule As Csla.Validation.BrokenRule In Me.mAccount.BrokenRulesCollection
                newEx = New Exception(BrokenRule.Description)
                Exit For
            Next
            Throw newEx
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            Me.mWebServer.Application.UnLock()
        End Try
    End Sub

    Public Sub EditAccount(ByVal accountData As ClsSubscriberAccountData)
        Try
            Me.mWebServer.Application.Lock()

            Me.mAccountList = Me.mWebServer.Application("AccountList").Clone
            Me.mAccount = Me.mAccountList.GetItem(accountData.ID)
            If Me.mAccount IsNot Nothing Then

                Me.CollectAccountData(Me.mAccount, accountData)

                Me.mWebServer.Application("AccountList") = Me.mAccountList.Save()
            Else
                Throw New Exception("Subscriber account doesn't exists")
            End If
        Catch ex As Csla.DataPortalException
            Throw ex.BusinessException
        Catch ex As Csla.Validation.ValidationException
            Dim newEx As New Exception(ex.Message)
            For Each BrokenRule As Csla.Validation.BrokenRule In Me.mAccount.BrokenRulesCollection
                newEx = New Exception(BrokenRule.Description)
                Exit For
            Next
            Throw newEx
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            Me.mWebServer.Application.UnLock()
        End Try
    End Sub

    Public Function GetAccount(ByVal accountId As Long) As ClsSubscriberAccountData
        Try
            Me.mWebServer.Application.Lock()

            Me.mAccount = Me.mWebServer.Application("AccountList").GetItem(accountId)
            If Me.mAccount IsNot Nothing Then
                Return Me.PopulateAccountData(Me.mAccount)
            Else
                Throw New Exception("Subscriber account doesn't exists")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            Me.mWebServer.Application.UnLock()
        End Try
    End Function

    Public Sub UninstallClientProgram(ByVal accountID As Long)
        Try
            Me.mWebServer.Application.Lock()

            Me.mAccountList = Me.mWebServer.Application("AccountList").Clone
            Me.mAccount = Me.mAccountList.GetItem(accountID)
            If Me.mAccount IsNot Nothing Then
                Me.mAccount.InstallClientProgram = False
                Me.mWebServer.Application("AccountList") = Me.mAccountList.Save()
            Else
                Throw New Exception("Subscriber account doesn't exists")
            End If
        Catch ex As Csla.DataPortalException
            Throw ex.BusinessException
        Catch ex As Csla.Validation.ValidationException
            Dim newEx As New Exception(ex.Message)
            For Each BrokenRule As Csla.Validation.BrokenRule In Me.mAccount.BrokenRulesCollection
                newEx = New Exception(BrokenRule.Description)
                Exit For
            Next
            Throw newEx
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            Me.mWebServer.Application.UnLock()
        End Try
    End Sub

    Public Sub ChangeWebPassword(ByVal accountID As Long, ByVal newPassword As String)
        Try
            Me.mWebServer.Application.Lock()

            Me.mAccountList = Me.mWebServer.Application("AccountList").Clone
            Me.mAccount = Me.mAccountList.GetItem(accountID)
            If Me.mAccount IsNot Nothing Then
                Me.mAccount.WebPassword = newPassword
                Me.mWebServer.Application("AccountList") = Me.mAccountList.Save()
            Else
                Throw New Exception("Subscriber account doesn't exists")
            End If
        Catch ex As Csla.DataPortalException
            Throw ex.BusinessException
        Catch ex As Csla.Validation.ValidationException
            Dim newEx As New Exception(ex.Message)
            For Each BrokenRule As Csla.Validation.BrokenRule In Me.mAccount.BrokenRulesCollection
                newEx = New Exception(BrokenRule.Description)
                Exit For
            Next
            Throw newEx
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            Me.mWebServer.Application.UnLock()
        End Try
    End Sub

    Public Sub ChangeClientPassword(ByVal accountID As Long, ByVal newPassword As String)
        Try
            Me.mWebServer.Application.Lock()

            Me.mAccountList = Me.mWebServer.Application("AccountList").Clone
            Me.mAccount = Me.mAccountList.GetItem(accountID)
            If Me.mAccount IsNot Nothing Then
                Me.mAccount.Connected = False
                Me.mAccount.ClientPassword = newPassword
                Me.mWebServer.Application("AccountList") = Me.mAccountList.Save()
            Else
                Throw New Exception("Subscriber account doesn't exists")
            End If
        Catch ex As Csla.DataPortalException
            Throw ex.BusinessException
        Catch ex As Csla.Validation.ValidationException
            Dim newEx As New Exception(ex.Message)
            For Each BrokenRule As Csla.Validation.BrokenRule In Me.mAccount.BrokenRulesCollection
                newEx = New Exception(BrokenRule.Description)
                Exit For
            Next
            Throw newEx
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            Me.mWebServer.Application.UnLock()
        End Try
    End Sub

    Private Function PopulateAccountData(ByVal account As Subscriber.ClsAccount) As ClsSubscriberAccountData
        Dim accountData As ClsSubscriberAccountData = New ClsSubscriberAccountData

        accountData.ID = account.ID
        accountData.Connected = account.Connected
        accountData.InstallClientProgram = account.InstallClientProgram
        accountData.ConfigurationHasChanged = account.ConfigurationHasChanged
        accountData.Login = account.Login
        accountData.ComputerSerialNumber = account.ComputerSerialNumber
        accountData.WebPassword = account.WebPassword
        accountData.ClientPassword = account.ClientPassword
        accountData.HintByPassOne = account.HintByPassOne
        accountData.HintByPassTwo = account.HintByPassTwo
        accountData.ContactEmail = account.ContactEmail
        accountData.AddStolenReport(Me.PopulateStolenReportData(Me.mAccount.StolenReports.GetActiveStolenReport))

        If Me.mAccount.Connected Then accountData.AddConnectionHistory(Me.PopulateConnectionHistoryData(Me.mAccount.ConnectionHistories.GetLastItem))

        For Each demographic As Subscriber.ClsDemographic In Me.mAccount.Demographics
            accountData.AddDemographic(Me.PopulateDemographicData(demographic))
        Next
        Return accountData
    End Function

    Private Sub CollectAccountData(ByRef account As Subscriber.ClsAccount, ByVal accountData As ClsSubscriberAccountData)
        account.Login = accountData.Login
        account.ComputerSerialNumber = accountData.ComputerSerialNumber
        account.WebPassword = accountData.WebPassword
        account.ClientPassword = accountData.ClientPassword
        account.HintByPassOne = accountData.HintByPassOne
        account.HintByPassTwo = accountData.HintByPassTwo
        account.ContactEmail = accountData.ContactEmail
        account.Demographics.Clear()

        If accountData.Demographics IsNot Nothing Then
            For Each demographicData As ClsSubscriberDemographicData In accountData.Demographics
                account.Demographics.Add(demographicData.Tag, demographicData.Answer)
            Next
        End If
    End Sub

    Private Sub CheckComputerConfiguration(ByVal accountList As Subscriber.ClsAccountList, ByRef account As Subscriber.ClsAccount, ByVal ComputerConfigurationInfo As ClsComputerConfigurationInfo)
        If Len(account.ComputerMacAddress) = 0 Then
            If Len(ComputerConfigurationInfo.ComputerMacAddress) > 0 Then
                If Not accountList.Contains(ComputerConfigurationInfo.ComputerMacAddress) Then
                    account.ComputerMacAddress = ComputerConfigurationInfo.ComputerMacAddress
                Else
                    Throw New SoapException("System configuration is assigned to another subscriber account.", SoapException.ClientFaultCode)
                End If
            End If
        Else
            If account.ComputerMacAddress <> ComputerConfigurationInfo.ComputerMacAddress Then
                account.ConfigurationHasChanged = True
            End If
        End If

        If Len(account.ComputerName) = 0 Then
            account.ComputerName = ComputerConfigurationInfo.ComputerName
        Else
            If account.ComputerName <> ComputerConfigurationInfo.ComputerName Then
                account.ConfigurationHasChanged = True
            End If
        End If

        If Len(account.ComputerHDSerialNumber) = 0 Then
            account.ComputerHDSerialNumber = ComputerConfigurationInfo.ComputerHDSerialNumber
        Else
            If account.ComputerHDSerialNumber <> ComputerConfigurationInfo.ComputerHDSerialNumber Then
                account.ConfigurationHasChanged = True
            End If
        End If
    End Sub

#End Region

#Region " Connection History "

    Private Function PopulateConnectionHistoryData(ByVal ConnectionHistory As Subscriber.ClsConnectionHistory) As ClsSubscriberConnectionHistoryData
        Dim ConnectionHistoryData As ClsSubscriberConnectionHistoryData = Nothing
        If ConnectionHistory IsNot Nothing Then
            ConnectionHistoryData = New ClsSubscriberConnectionHistoryData
            ConnectionHistoryData.ID = ConnectionHistory.ID
            ConnectionHistoryData.HostIP = ConnectionHistory.HostIP
            ConnectionHistoryData.HostLocalIP = ConnectionHistory.HostLocalIP
            ConnectionHistoryData.ConnectionDate = ConnectionHistory.ConnectionDate
            ConnectionHistoryData.ConnectionTime = ConnectionHistory.ConnectionTime
            ConnectionHistoryData.DNSResolutionIP = ConnectionHistory.DNSResolutionIP
            ConnectionHistoryData.IPState = ConnectionHistory.IPState
            ConnectionHistoryData.IPCity = ConnectionHistory.IPCity
            ConnectionHistoryData.ActivityStatus = ConnectionHistory.ActivityStatus
        End If
        Return ConnectionHistoryData
    End Function

#End Region

#Region " Stolen Report "

    Public Sub AddStolenReport(ByVal accountId As Long, ByVal StolenReportData As ClsSubscriberStolenReportData)
        Try
            Me.mWebServer.Application.Lock()

            Me.mAccountList = Me.mWebServer.Application("AccountList").Clone
            Me.mAccount = Me.mAccountList.GetItem(accountId)
            If Me.mAccount IsNot Nothing Then

                Dim stolenReport As Subscriber.ClsStolenReport = Subscriber.ClsStolenReport.NewStolenReport
                Me.CollectStolenReportData(stolenReport, StolenReportData)

                Me.mAccount.StolenReports.AddItem(stolenReport)
                Me.mWebServer.Application("AccountList") = Me.mAccountList.Save()
            Else
                Throw New Exception("Subscriber account doesn't exists")
            End If
        Catch ex As Csla.DataPortalException
            Throw ex.BusinessException
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            Me.mWebServer.Application.UnLock()
        End Try
    End Sub

    Public Sub EditStolenReport(ByVal accountId As Long, ByVal stolenReportData As ClsSubscriberStolenReportData)
        Try
            Me.mWebServer.Application.Lock()

            Me.mAccountList = Me.mWebServer.Application("AccountList").Clone
            Me.mAccount = Me.mAccountList.GetItem(accountId)
            If Me.mAccount IsNot Nothing Then

                Dim stolenReport As Subscriber.ClsStolenReport = Me.mAccount.StolenReports.GetItem(stolenReportData.ID)
                If stolenReport IsNot Nothing Then

                    Me.CollectStolenReportData(stolenReport, stolenReportData)

                    Me.mWebServer.Application("AccountList") = Me.mAccountList.Save()
                Else
                    Throw New Exception("Subscriber Stolen Report doesn't exists")
                End If
            Else
                Throw New Exception("Subscriber account doesn't exists")
            End If
        Catch ex As Csla.DataPortalException
            Throw ex.BusinessException
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            Me.mWebServer.Application.UnLock()
        End Try
    End Sub

    Public Function GetStolenReport(ByVal accountId As Long) As ClsSubscriberStolenReportData
        Try
            Me.mWebServer.Application.Lock()

            Me.mAccount = Me.mWebServer.Application("AccountList").GetItem(accountId)
            If Me.mAccount IsNot Nothing Then
                Return Me.PopulateStolenReportData(Me.mAccount.StolenReports.GetActiveStolenReport)
            Else
                Throw New Exception("Subscriber account doesn't exists")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            Me.mWebServer.Application.UnLock()
        End Try
    End Function

    Private Sub CollectStolenReportData(ByRef stolenReport As Subscriber.ClsStolenReport, ByVal stolenReportData As ClsSubscriberStolenReportData)
        stolenReport.ActionToTake = stolenReportData.ActionToTake
        stolenReport.ActiveForAlerts = stolenReportData.ActiveForAlerts
        stolenReport.DateReportFound = stolenReportData.DateReportFound
        stolenReport.DateReportMissing = stolenReportData.DateReportMissing
        stolenReport.LastKnownLocationDescription = stolenReportData.LastKnownLocationDescription
    End Sub

    Private Function PopulateStolenReportData(ByRef stolenReport As Subscriber.ClsStolenReport) As ClsSubscriberStolenReportData
        Dim StolenReportData As ClsSubscriberStolenReportData = Nothing
        If stolenReport IsNot Nothing Then
            StolenReportData = New ClsSubscriberStolenReportData
            StolenReportData.ID = stolenReport.ID
            StolenReportData.DateReportMissing = stolenReport.DateReportMissing
            StolenReportData.DateReportFound = stolenReport.DateReportFound
            StolenReportData.LastKnownLocationDescription = stolenReport.LastKnownLocationDescription
            StolenReportData.ActionToTake = stolenReport.ActionToTake
            StolenReportData.ActiveForAlerts = stolenReport.ActiveForAlerts
        End If
        Return StolenReportData
    End Function

#End Region

#Region " Subscriber Demographic "

    Private Function PopulateDemographicData(ByVal demographic As Subscriber.ClsDemographic) As ClsSubscriberDemographicData
        Dim DemographicData As ClsSubscriberDemographicData = Nothing
        If demographic IsNot Nothing Then
            DemographicData = New ClsSubscriberDemographicData
            DemographicData.ID = demographic.ID
            DemographicData.Tag = demographic.Tag
            DemographicData.Answer = demographic.Answer
        End If
        Return DemographicData
    End Function

#End Region

#Region " Encrypt/Decrypt "

    'Private Function Encrypt(ByVal value As String, ByVal key As Integer) As String
    '    Dim Result As String = String.Empty
    '    Randomize(key)
    '    For Each c As String In value
    '        Result = Result & Chr(CByte(c) Xor CInt(Int(257 * Rnd())))
    '    Next
    '    Return Result
    'End Function

    'Private Function Decrypt(ByVal value As String, ByVal key As Integer) As String
    '    Dim Result As String = String.Empty
    '    Randomize(key)
    '    For Each c As String In value
    '        Result = Result & Chr(CByte(c) Xor CInt(Int(257 * Rnd())))
    '    Next
    '    Return Result
    'End Function

#End Region

End Class