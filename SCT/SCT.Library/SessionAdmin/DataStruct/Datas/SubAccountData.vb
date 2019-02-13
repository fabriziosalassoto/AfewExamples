Option Strict On

Imports Microsoft.VisualBasic

Public Class SubAccountData

    Private mID As Long
    Private mConnected As Boolean
    Private mInstallClientProgram As Boolean
    Private mConfigurationHasChanged As Boolean
    Private mLogin As String
    Private mComputerSerialNumber As String
    Private mWebPassword As String
    Private mClientPassword As String
    Private mHintByPassOne As String
    Private mHintByPassTwo As String
    Private mContactEmail As String
    Private mConnectionHistories As New Generic.List(Of SubConnectionHistoryData)
    Private mStolenReports As New Generic.List(Of SubStolenReportData)
    Private mDemographics As New Generic.List(Of SubDemographicData)

    Public Property ID() As Long
        Get
            Return Me.mID
        End Get
        Set(ByVal value As Long)
            Me.mID = value
        End Set
    End Property

    Public Property Connected() As Boolean
        Get
            Return Me.mConnected
        End Get
        Set(ByVal value As Boolean)
            Me.mConnected = value
        End Set
    End Property

    Public Property InstallClientProgram() As Boolean
        Get
            Return Me.mInstallClientProgram
        End Get
        Set(ByVal value As Boolean)
            Me.mInstallClientProgram = value
        End Set
    End Property

    Public Property ConfigurationHasChanged() As Boolean
        Get
            Return Me.mConfigurationHasChanged
        End Get
        Set(ByVal value As Boolean)
            Me.mConfigurationHasChanged = value
        End Set
    End Property

    Public Property Login() As String
        Get
            Return Me.mLogin
        End Get
        Set(ByVal value As String)
            Me.mLogin = value
        End Set
    End Property

    Public Property ComputerSerialNumber() As String
        Get
            Return Me.mComputerSerialNumber
        End Get
        Set(ByVal value As String)
            Me.mComputerSerialNumber = value
        End Set
    End Property

    Public Property WebPassword() As String
        Get
            Return Me.mWebPassword
        End Get
        Set(ByVal value As String)
            Me.mWebPassword = value
        End Set
    End Property

    Public Property ClientPassword() As String
        Get
            Return Me.mClientPassword
        End Get
        Set(ByVal value As String)
            Me.mClientPassword = value
        End Set
    End Property

    Public Property HintByPassOne() As String
        Get
            Return Me.mHintByPassOne
        End Get
        Set(ByVal value As String)
            Me.mHintByPassOne = value
        End Set
    End Property

    Public Property HintByPassTwo() As String
        Get
            Return Me.mHintByPassTwo
        End Get
        Set(ByVal value As String)
            Me.mHintByPassTwo = value
        End Set
    End Property

    Public Property ContactEmail() As String
        Get
            Return Me.mContactEmail
        End Get
        Set(ByVal value As String)
            Me.mContactEmail = value
        End Set
    End Property

    Public Property ConnectionHistories() As SubConnectionHistoryData()
        Get
            If Me.mConnectionHistories.Count > 0 Then
                Return Me.mConnectionHistories.ToArray
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As SubConnectionHistoryData())
            Me.mConnectionHistories = New Generic.List(Of SubConnectionHistoryData)(value)
        End Set
    End Property

    Public Sub AddConnectionHistory(ByVal connectionHistory As SubConnectionHistoryData)
        Me.mConnectionHistories.Add(connectionHistory)
    End Sub

    Public Property StolenReports() As SubStolenReportData()
        Get
            If Me.mStolenReports.Count > 0 Then
                Return Me.mStolenReports.ToArray
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As SubStolenReportData())
            Me.mStolenReports = New Generic.List(Of SubStolenReportData)(value)
        End Set
    End Property

    Public Sub AddStolenReport(ByVal stolenReport As SubStolenReportData)
        Me.mStolenReports.Add(stolenReport)
    End Sub

    Public Property Demographics() As SubDemographicData()
        Get
            If Me.mDemographics.Count > 0 Then
                Return Me.mDemographics.ToArray
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As SubDemographicData())
            Me.mDemographics = New Generic.List(Of SubDemographicData)(value)
        End Set
    End Property

    Public Sub AddDemographic(ByVal Demographic As SubDemographicData)
        Me.mDemographics.Add(Demographic)
    End Sub

End Class
