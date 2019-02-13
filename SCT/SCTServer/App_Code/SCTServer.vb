Imports System.Web
Imports SCT.Server.DataFlow
Imports System.Web.Services
Imports System.Web.Services.Protocols

<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class SCTServer
    Inherits System.Web.Services.WebService

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private wcWebService As New DFClsWebServer(Me)

    ''' <summary>
    ''' Credentials for custom authentication.
    ''' </summary>
    Public Credentials As New CslaCredentials

    <WebMethod(EnableSession:=True)> <SoapHeader("Credentials")> _
    Public Function OpenConnection(ByVal connectionRequest As ClsConnectionRequest) As ClsConnectionResponse
        ' User credentials required
        Me.wcWebService.Login(Credentials)

        Return Me.wcWebService.OpenConnection(connectionRequest)
    End Function

    <WebMethod(EnableSession:=True)> <SoapHeader("Credentials")> _
       Public Sub CloseConnection()
        ' User credentials required
        Me.wcWebService.Login(Credentials)

        Me.wcWebService.CloseConnection()
    End Sub

    <WebMethod(EnableSession:=True)> <SoapHeader("Credentials")> _
       Public Function KeepConnection(ByVal hostIPInfo As ClsHostIPInfo) As ClsConnectionResponse
        ' User credentials required
        Me.wcWebService.Login(Credentials)

        Return Me.wcWebService.KeepConnection(hostIPInfo)
    End Function

    <WebMethod(EnableSession:=True)> <SoapHeader("Credentials")> _
      Public Function GetAdvertising(ByVal subscriberTime As Date) As ClsAdvertisingInfo
        ' User credentials required
        Me.wcWebService.Login(Credentials)

        Return Me.wcWebService.GetAdvertisingInfo(subscriberTime)
    End Function

    <WebMethod(EnableSession:=True)> <SoapHeader("Credentials")> _
     Public Sub SetSystemConfiguration(ByVal systemConfigurationInfo As ClsComputerConfigurationInfo)
        If systemConfigurationInfo Is Nothing Then
            Throw New SoapException("Aqui.", SoapException.ClientFaultCode)
        End If

        ' User credentials required
        Me.wcWebService.Login(Credentials)

        Me.wcWebService.SetSystemConfiguration(systemConfigurationInfo)
    End Sub

    <WebMethod(EnableSession:=True)> <SoapHeader("Credentials")> _
      Public Sub AdDisplay(ByVal AdHistoryInfo As ClsAdvertisingInfo)
        ' User credentials required
        Me.wcWebService.Login(Credentials)

        Me.wcWebService.SetAdDisplay(AdHistoryInfo)
    End Sub

    <WebMethod(EnableSession:=True)> <SoapHeader("Credentials")> _
      Public Sub AdClickThru(ByVal AdHistoryInfo As ClsAdvertisingInfo)
        ' User credentials required
        Me.wcWebService.Login(Credentials)

        Me.wcWebService.SetAdClickThru(AdHistoryInfo)
    End Sub

    <WebMethod()> <SoapHeader("Credentials")> _
    Public Function SubscriberCount() As Long
        ' Anonymous access allowed
        Me.wcWebService.UseAnonymous()

        Return Me.wcWebService.SubscriberCount
    End Function

    <WebMethod()> <SoapHeader("Credentials")> _
    Public Function ConnectedSubscriberCount() As Long
        ' Anonymous access allowed
        Me.wcWebService.UseAnonymous()

        Return Me.wcWebService.ConnectedSubscriberCount
    End Function

    <WebMethod()> <SoapHeader("Credentials")> _
    Public Function DisConnectedSubscriberCount() As Long
        ' Anonymous access allowed
        Me.wcWebService.UseAnonymous()

        Return Me.wcWebService.DisconnectedSubscriberCount
    End Function

    <WebMethod()> <SoapHeader("Credentials")> _
    Public Function IsConnectedSubscriber(ByVal SubscriberID As Long) As Boolean
        ' Anonymous access allowed
        Me.wcWebService.UseAnonymous()

        Return Me.wcWebService.IsSubscriberConnected(SubscriberID)
    End Function

    <WebMethod()> <SoapHeader("Credentials")> _
    Public Sub AddSubsciberAccount(ByVal AccountData As ClsSubscriberAccountData)
        ' Anonymous access allowed
        Me.wcWebService.UseAnonymous()

        Me.wcWebService.AddAccount(AccountData)
    End Sub

    <WebMethod()> <SoapHeader("Credentials")> _
    Public Sub EditSubsciberAccount(ByVal AccountData As ClsSubscriberAccountData)
        ' Anonymous access allowed
        Me.wcWebService.UseAnonymous()

        Me.wcWebService.EditAccount(AccountData)
    End Sub

    <WebMethod()> <SoapHeader("Credentials")> _
    Public Function GetSubscriberAccount(ByVal SubscriberID As Long) As ClsSubscriberAccountData
        ' Anonymous access allowed
        Me.wcWebService.UseAnonymous()

        Return Me.wcWebService.GetAccount(SubscriberID)
    End Function

    <WebMethod()> <SoapHeader("Credentials")> _
   Public Sub UninstallClientProgram(ByVal SubscriberID As Long)
        ' Anonymous access allowed
        Me.wcWebService.UseAnonymous()

        Me.wcWebService.UninstallClientProgram(SubscriberID)
    End Sub

    <WebMethod()> <SoapHeader("Credentials")> _
    Public Sub ChangeSubscriberWebPassword(ByVal SubscriberID As Long, ByVal newPassword As String)
        ' Anonymous access allowed
        Me.wcWebService.UseAnonymous()

        Me.wcWebService.ChangeWebPassword(SubscriberID, newPassword)
    End Sub

    <WebMethod()> <SoapHeader("Credentials")> _
    Public Sub ChangeSubscriberComputerPassword(ByVal SubscriberID As Long, ByVal newPassword As String)
        ' Anonymous access allowed
        Me.wcWebService.UseAnonymous()

        Me.wcWebService.ChangeClientPassword(SubscriberID, newPassword)
    End Sub

    <WebMethod()> <SoapHeader("Credentials")> _
    Public Function GetSubscriberStolenReport(ByVal SubscriberID As Long) As ClsSubscriberStolenReportData
        ' Anonymous access allowed
        Me.wcWebService.UseAnonymous()

        Return Me.wcWebService.GetStolenReport(SubscriberID)
    End Function

    <WebMethod()> <SoapHeader("Credentials")> _
    Public Sub AddSubsciberStolenReport(ByVal SubscriberID As Long, ByVal stolenReportData As ClsSubscriberStolenReportData)
        ' Anonymous access allowed
        Me.wcWebService.UseAnonymous()

        Me.wcWebService.AddStolenReport(SubscriberID, stolenReportData)
    End Sub

    <WebMethod()> <SoapHeader("Credentials")> _
    Public Sub EditSubsciberStolenReport(ByVal SubscriberID As Long, ByVal stolenReportData As ClsSubscriberStolenReportData)
        ' Anonymous access allowed
        Me.wcWebService.UseAnonymous()

        Me.wcWebService.EditStolenReport(SubscriberID, stolenReportData)
    End Sub

End Class
