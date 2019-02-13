Option Strict On

Imports Microsoft.VisualBasic

Public Class ClsConnectionResponse

    Private mActionToTake As Integer
    Private mHostPublicIP As String = String.Empty

    Public Property ActionToTake() As Integer
        Get
            Return Me.mActionToTake
        End Get
        Set(ByVal value As Integer)
            Me.mActionToTake = value
        End Set
    End Property

    Public Property HostPublicIP() As String
        Get
            Return Me.mHostPublicIP
        End Get
        Set(ByVal value As String)
            Me.mHostPublicIP = value
        End Set
    End Property

    Public Shared Function NewConnectionResponse() As ClsConnectionResponse
        Return New ClsConnectionResponse()
    End Function

    Public Shared Function NewConnectionResponse(ByVal action As Integer, ByVal hostPublicIP As String) As ClsConnectionResponse
        Return New ClsConnectionResponse(action, hostPublicIP)
    End Function

    Private Sub New()
        ' require use of factory methods
    End Sub

    Private Sub New(ByVal actionToTake As Integer, ByVal hostPublicIP As String)
        Me.mActionToTake = actionToTake
        Me.mHostPublicIP = hostPublicIP
    End Sub

End Class