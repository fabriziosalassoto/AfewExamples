Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports SCT.DataFlow.WebService

<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class SCTService
    Inherits System.Web.Services.WebService

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private wcWebService As New DFClsWebService(Me)

    ''' <summary>
    ''' Credentials for custom authentication.
    ''' </summary>
    Public Credentials As New CslaCredentials

    <WebMethod(EnableSession:=True)> <SoapHeader("Credentials")> _
    Public Function OpenConnection(ByVal serverRequest As ClsServerRequest) As ClsServerResponse
        ' User credentials required
        Me.wcWebService.Login(Credentials)

        Return Me.wcWebService.OpenConnection(serverRequest)
    End Function

    <WebMethod(EnableSession:=True)> <SoapHeader("Credentials")> _
       Public Sub CloseConnection()
        ' User credentials required
        Me.wcWebService.Login(Credentials)

        Me.wcWebService.CloseConnection()
    End Sub

    <WebMethod(EnableSession:=True)> <SoapHeader("Credentials")> _
       Public Function ReConnection(ByVal serverRequest As ClsServerRequest) As ClsServerResponse
        ' User credentials required
        Me.wcWebService.Login(Credentials)

        Return Me.wcWebService.ReConnection(serverRequest)
    End Function

    <WebMethod(EnableSession:=True)> <SoapHeader("Credentials")> _
      Public Function GetAdvertising() As ClsAdvertisingInfo
        ' User credentials required
        Me.wcWebService.Login(Credentials)

        Return Me.wcWebService.GetAdInfo
    End Function

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
    Public Function ConnectedSubscriberCount() As Long
        ' Anonymous access allowed
        Me.wcWebService.UseAnonymous()

        Return Me.wcWebService.ConnectedSubscriberCount
    End Function

    <WebMethod()> <SoapHeader("Credentials")> _
   Public Function IsConnectedSubscriber(ByVal SubscriberID As Long) As Boolean
        ' Anonymous access allowed
        Me.wcWebService.UseAnonymous()

        Return Me.wcWebService.IsConnectedSubscriber(SubscriberID)
    End Function

    <WebMethod()> <SoapHeader("Credentials")> _
    Public Function GetConnectedSubscriberList() As ClsConnectedSubscribersList
        ' Anonymous access allowed
        Me.wcWebService.UseAnonymous()

        Return Me.wcWebService.GetConnectedSubcriberList
    End Function

    <WebMethod()> <SoapHeader("Credentials")> _
    Public Function GetConnectedSubscriber(ByVal SubscriberID As Long) As ClsConnectedSubscriber
        ' Anonymous access allowed
        Me.wcWebService.UseAnonymous()

        Return Me.wcWebService.GetConnectedSubcriber(SubscriberID)
    End Function

    <WebMethod()> <SoapHeader("Credentials")> _
    Public Function SesionesCount() As Integer
        ' Anonymous access allowed
        Me.wcWebService.UseAnonymous()

        Return Application("Sesiones")
    End Function

End Class
