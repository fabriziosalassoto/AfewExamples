Option Strict On

Imports Microsoft.VisualBasic

Public Class ClsServerResponse

    Private mAction As Integer
    Private mAdInfo As ClsAdvertisingInfo

    Public Property Action() As Integer
        Get
            Return Me.mAction
        End Get
        Set(ByVal value As Integer)
            Me.mAction = value
        End Set
    End Property

    Public Property AdInfo() As ClsAdvertisingInfo
        Get
            Return Me.mAdInfo
        End Get
        Set(ByVal value As ClsAdvertisingInfo)
            Me.mAdInfo = value
        End Set
    End Property

    Public Shared Function NewServerResponse() As ClsServerResponse
        Return New ClsServerResponse()
    End Function

    Public Shared Function NewServerResponse(ByVal action As Integer, ByVal adInfo As ClsAdvertisingInfo) As ClsServerResponse
        Return New ClsServerResponse(action, adInfo)
    End Function

    Private Sub New()
        ' require use of factory methods
    End Sub

    Private Sub New(ByVal action As Integer, ByVal adInfo As ClsAdvertisingInfo)
        Me.mAction = action
        Me.mAdInfo = adInfo
    End Sub

End Class
