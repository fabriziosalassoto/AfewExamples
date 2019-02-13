Option Strict On

Imports Microsoft.VisualBasic

Public Class ClsAdvertisingInfo

    Private mAdHistoryID As Long
    Private mADUrl As String
    Private mADHeight As Integer
    Private mADWidth As Integer

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

    Public Property ADHeight() As Integer
        Get
            Return Me.mADHeight
        End Get
        Set(ByVal value As Integer)
            Me.mADHeight = value
        End Set
    End Property

    Public Property ADWidth() As Integer
        Get
            Return Me.mADWidth
        End Get
        Set(ByVal value As Integer)
            Me.mADWidth = value
        End Set
    End Property

    Public Shared Function NewAdvertisingInfo() As ClsAdvertisingInfo
        Return New ClsAdvertisingInfo
    End Function

    Public Shared Function NewAdvertisingInfo(ByVal adHistoryId As Long, ByVal ADUrl As String, ByVal ADHeight As Integer, ByVal ADWidth As Integer) As ClsAdvertisingInfo
        Return New ClsAdvertisingInfo(adHistoryId, ADUrl, ADHeight, ADWidth)
    End Function

    Private Sub New()
        ' require use of factory methods
    End Sub

    Private Sub New(ByVal adHistoryId As Long, ByVal ADUrl As String, ByVal ADHeight As Integer, ByVal ADWidth As Integer)
        Me.mAdHistoryID = adHistoryId
        Me.mADUrl = ADUrl
        Me.mADHeight = ADHeight
        Me.mADWidth = ADWidth
    End Sub

End Class
