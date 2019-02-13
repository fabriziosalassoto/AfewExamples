Option Strict On

Imports Microsoft.VisualBasic

Public Class ClsHostIPInfo

    Private mHostPublicIP As String = String.Empty
    Private mHostLocalIP As String = String.Empty

    Public Property HostPublicIP() As String
        Get
            Return Me.mHostPublicIP
        End Get
        Set(ByVal value As String)
            Me.mHostPublicIP = value
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

    Public Shared Function NewHostIPInfo() As ClsHostIPInfo
        Return New ClsHostIPInfo
    End Function

    Private Sub New()
        ' require use of factory methods
    End Sub

End Class
