Option Strict On

Imports Microsoft.VisualBasic

Public Class ClsConnectionRequest

    Private mFirstConnection As Boolean
    Private mComputerConfigurationInfo As ClsComputerConfigurationInfo = ClsComputerConfigurationInfo.NewComputerConfigurationInfo
    Private mHostIPInfo As ClsHostIPInfo = ClsHostIPInfo.NewHostIPInfo

    'Private mHostIP As String = String.Empty
    'Private mHostLocalIP As String = String.Empty
    'Private mComputerHDSerialNumber As String = String.Empty
    'Private mComputerMacAddress As String = String.Empty
    'Private mComputerName As String = String.Empty

    'Public Property ComputerHDSerialNumber() As String
    '    Get
    '        Return Me.mComputerHDSerialNumber
    '    End Get
    '    Set(ByVal value As String)
    '        Me.mComputerHDSerialNumber = value
    '    End Set
    'End Property

    'Public Property ComputerMacAddress() As String
    '    Get
    '        Return Me.mComputerMacAddress
    '    End Get
    '    Set(ByVal value As String)
    '        Me.mComputerMacAddress = value
    '    End Set
    'End Property

    'Public Property ComputerName() As String
    '    Get
    '        Return Me.mComputerName
    '    End Get
    '    Set(ByVal value As String)
    '        Me.mComputerName = value
    '    End Set
    'End Property

    Public Property FirstConnection() As Boolean
        Get
            Return Me.mFirstConnection
        End Get
        Set(ByVal value As Boolean)
            Me.mFirstConnection = value
        End Set
    End Property

    'Public Property HostIP() As String
    '    Get
    '        Return Me.mHostIP
    '    End Get
    '    Set(ByVal value As String)
    '        Me.mHostIP = value
    '    End Set
    'End Property

    'Public Property HostLocalIP() As String
    '    Get
    '        Return Me.mHostLocalIP
    '    End Get
    '    Set(ByVal value As String)
    '        Me.mHostLocalIP = value
    '    End Set
    'End Property

    Public Property ComputerConfigurationInfo() As ClsComputerConfigurationInfo
        Get
            Return Me.mComputerConfigurationInfo
        End Get
        Set(ByVal value As ClsComputerConfigurationInfo)
            Me.mComputerConfigurationInfo = value
        End Set
    End Property

    Public Property HostIPInfo() As ClsHostIPInfo
        Get
            Return Me.mHostIPInfo
        End Get
        Set(ByVal value As ClsHostIPInfo)
            Me.mHostIPInfo = value
        End Set
    End Property

    Public Shared Function NewConnectionRequest() As ClsConnectionRequest
        Return New ClsConnectionRequest
    End Function

    Private Sub New()
        ' require use of factory methods
    End Sub

End Class