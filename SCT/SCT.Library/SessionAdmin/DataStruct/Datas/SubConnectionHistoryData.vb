Option Strict On

Imports Microsoft.VisualBasic

Public Class SubConnectionHistoryData

    Private mID As Long
    Private mHostIP As String
    Private mHostLocalIP As String
    Private mConnectionDate As Date
    Private mConnectionTime As Date
    Private mDNSResolutionIP As String
    Private mIPState As String
    Private mIPCity As String
    Private mActivityStatus As String

    Public Property ID() As Long
        Get
            Return Me.mID
        End Get
        Set(ByVal value As Long)
            Me.mID = value
        End Set
    End Property

    Public Property HostIP() As String
        Get
            Return Me.mHostIP
        End Get
        Set(ByVal value As String)
            Me.mHostIP = value
        End Set
    End Property

    Public Property HostLocalIP() As String
        Get
            Return Me.mHostLocalIP
        End Get
        Set(ByVal value As String)
            Me.mHostLocalIP = value
        End Set
    End Property

    Public Property ConnectionDate() As Date
        Get
            Return Me.mConnectionDate
        End Get
        Set(ByVal value As Date)
            Me.mConnectionDate = value
        End Set
    End Property

    Public Property ConnectionTime() As Date
        Get
            Return Me.mConnectionTime
        End Get
        Set(ByVal value As Date)
            Me.mConnectionTime = value
        End Set
    End Property

    Public Property DNSResolutionIP() As String
        Get
            Return Me.mDNSResolutionIP
        End Get
        Set(ByVal value As String)
            Me.mDNSResolutionIP = value
        End Set
    End Property

    Public Property IPState() As String
        Get
            Return Me.mIPState
        End Get
        Set(ByVal value As String)
            Me.mIPState = value
        End Set
    End Property

    Public Property IPCity() As String
        Get
            Return Me.mIPCity
        End Get
        Set(ByVal value As String)
            Me.mIPCity = value
        End Set
    End Property

    Public Property ActivityStatus() As String
        Get
            Return Me.mActivityStatus
        End Get
        Set(ByVal value As String)
            Me.mActivityStatus = value
        End Set
    End Property

End Class
