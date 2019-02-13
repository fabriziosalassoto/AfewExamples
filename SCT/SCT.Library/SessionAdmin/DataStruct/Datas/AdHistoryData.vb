Option Strict On

<Serializable()> Public Class AdHistoryData

    Private mID As Long
    Private mProject As New AdProjectData
    Private mSubscriber As New SubAccountData
    Private mDateAdDisplay As Date = New Date(1900, 1, 1)
    Private mTimeAdDisplay As Date = New Date(1900, 1, 1, 0, 0, 0)
    Private mDateAdClickThru As Date = New Date(1900, 1, 1)
    Private mTimeAdClickThru As Date = New Date(1900, 1, 1, 0, 0, 0)
    Private mURLAdDisplay As String = String.Empty
    Private mURLAdClickThru As String = String.Empty

    Public Property ID() As Long
        Get
            Return Me.mID
        End Get
        Set(ByVal value As Long)
            Me.mID = value
        End Set
    End Property

    Public Property Project() As AdProjectData
        Get
            Return Me.mProject
        End Get
        Set(ByVal value As AdProjectData)
            Me.mProject = value
        End Set
    End Property

    Public Property SubscriberAccount() As SubAccountData
        Get
            Return Me.mSubscriber
        End Get
        Set(ByVal value As SubAccountData)
            Me.mSubscriber = value
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
