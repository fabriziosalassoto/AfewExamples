Imports Microsoft.VisualBasic

Public Class ClsServerRequest

    Private mHostIP As String

    Public Property HostIP() As String
        Get
            Return Me.mHostIP
        End Get
        Set(ByVal value As String)
            Me.mHostIP = value
        End Set
    End Property

    Public Shared Function NewServerResponse() As ClsServerRequest
        Return New ClsServerRequest
    End Function

    Public Shared Function NewServerResponse(ByVal hostIP As String) As ClsServerRequest
        Return New ClsServerRequest(hostIP)
    End Function

    Private Sub New()
        ' require use of factory methods
    End Sub

    Private Sub New(ByVal hostIP As String)
        Me.mHostIP = hostIP
    End Sub

End Class
