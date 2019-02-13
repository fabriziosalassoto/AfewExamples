Imports Microsoft.VisualBasic

Public Class ClsSubscriberAdHistoryData

    Private mID As Long
    Private mDateAdDisplay As Date
    Private mTimeAdDisplay As Date
    Private mDateAdClickThru As Date
    Private mTimeAdClickThru As Date
    Private mURLAdDisplay As String
    Private mURLAdClickThru As String

    Public Property ID() As Long
        Get
            Return Me.mID
        End Get
        Set(ByVal value As Long)
            Me.mID = value
        End Set
    End Property

    Public Property DateAdDisplay() As Date
        Get
            Return Me.mDateAdDisplay
        End Get
        Set(ByVal value As Date)
            Me.mDateAdDisplay = value
        End Set
    End Property

    Public Property TimeAdDisplay() As Date
        Get
            Return Me.mTimeAdDisplay
        End Get
        Set(ByVal value As Date)
            Me.mTimeAdDisplay = value
        End Set
    End Property

    Public Property DateAdClickThru() As Date
        Get
            Return Me.mDateAdClickThru
        End Get
        Set(ByVal value As Date)
            Me.mDateAdClickThru = value
        End Set
    End Property

    Public Property TimeAdClickThru() As Date
        Get
            Return Me.mTimeAdClickThru
        End Get
        Set(ByVal value As Date)
            Me.mTimeAdClickThru = value
        End Set
    End Property

    Public Property URLAdDisplay() As String
        Get
            Return Me.mURLAdDisplay
        End Get
        Set(ByVal value As String)
            Me.mURLAdDisplay = value
        End Set
    End Property

    Public Property URLAdClickThru() As String
        Get
            Return Me.mURLAdClickThru
        End Get
        Set(ByVal value As String)
            Me.mURLAdClickThru = value
        End Set
    End Property

End Class
