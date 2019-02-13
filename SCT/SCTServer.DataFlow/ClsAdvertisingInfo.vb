Option Strict On

Imports Microsoft.VisualBasic

Public Class ClsAdvertisingInfo

    Private mAdHistoryID As Long
    Private mADUrl As String

    Public Property AdHistoryID() As Long
        Get
            Return Me.mAdHistoryID
        End Get
        Set(ByVal value As Long)
            Me.mAdHistoryID = value
        End Set
    End Property

    Public Property ADUrl() As String
        Get
            Return Me.mADUrl
        End Get
        Set(ByVal value As String)
            Me.mADUrl = value
        End Set
    End Property

    Public Shared Function NewAdvertisingInfo() As ClsAdvertisingInfo
        Return New ClsAdvertisingInfo
    End Function

    Public Shared Function NewAdvertisingInfo(ByVal adHistoryId As Long, ByVal ADUrl As String) As ClsAdvertisingInfo
        Return New ClsAdvertisingInfo(adHistoryId, ADUrl)
    End Function

    Private Sub New()
        ' require use of factory methods
    End Sub

    Private Sub New(ByVal adHistoryId As Long, ByVal ADUrl As String)
        Me.mAdHistoryID = adHistoryId
        Me.mADUrl = ADUrl
    End Sub

End Class
