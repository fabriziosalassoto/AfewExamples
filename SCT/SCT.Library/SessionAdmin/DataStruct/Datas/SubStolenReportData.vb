Option Strict On

Imports Microsoft.VisualBasic

Public Class SubStolenReportData

    Private mID As Long
    Private mDateReportMissing As Date
    Private mLastKnownLocationDescription As String
    Private mDateReportFound As Date
    Private mActiveForAlerts As Boolean
    Private mActionToTake As Integer

    Public Property ID() As Long
        Get
            Return Me.mID
        End Get
        Set(ByVal value As Long)
            Me.mID = value
        End Set
    End Property

    Public Property DateReportMissing() As Date
        Get
            Return Me.mDateReportMissing
        End Get
        Set(ByVal value As Date)
            Me.mDateReportMissing = value
        End Set
    End Property

    Public Property LastKnownLocationDescription() As String
        Get
            Return Me.mLastKnownLocationDescription
        End Get
        Set(ByVal value As String)
            Me.mLastKnownLocationDescription = value
        End Set
    End Property

    Public Property DateReportFound() As Date
        Get
            Return Me.mDateReportFound
        End Get
        Set(ByVal value As Date)
            Me.mDateReportFound = value
        End Set
    End Property

    Public Property ActiveForAlerts() As Boolean
        Get
            Return Me.mActiveForAlerts
        End Get
        Set(ByVal value As Boolean)
            Me.mActiveForAlerts = value
        End Set
    End Property

    Public Property ActionToTake() As Integer
        Get
            Return Me.mActionToTake
        End Get
        Set(ByVal value As Integer)
            Me.mActionToTake = value
        End Set
    End Property

End Class
