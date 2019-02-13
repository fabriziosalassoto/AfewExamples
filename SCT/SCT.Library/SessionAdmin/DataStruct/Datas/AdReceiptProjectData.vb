Option Strict On

<Serializable()> Public Class AdReceiptProjectData

    Private mID As Long
    Private mAdvertiser As New AdAccountData
    Private mContact As New AdContactData
    Private mReceipt As New AdReceiptData
    Private mProjectDescription As String = String.Empty
    Private mADUrl As String = String.Empty
    Private mPaidByDisplay As Double
    Private mPaidByClickThrough As Double
    Private mTotalPaid As Double

    Public Property ID() As Long
        Get
            Return Me.mID
        End Get
        Set(ByVal value As Long)
            Me.mID = value
        End Set
    End Property

    Public Property Advertiser() As AdAccountData
        Get
            Return Me.mAdvertiser
        End Get
        Set(ByVal value As AdAccountData)
            Me.mAdvertiser = value
        End Set
    End Property

    Public Property Contact() As AdContactData
        Get
            Return Me.mContact
        End Get
        Set(ByVal value As AdContactData)
            Me.mContact = value
        End Set
    End Property

    Public Property Receipt() As AdReceiptData
        Get
            Return Me.mReceipt
        End Get
        Set(ByVal value As AdReceiptData)
            Me.mReceipt = value
        End Set
    End Property

    Public Property ProjectDescription() As String
        Get
            Return Me.mProjectDescription
        End Get
        Set(ByVal value As String)
            Me.mProjectDescription = value
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

    Public Property PaidByDisplay() As Double
        Get
            Return Me.mPaidByDisplay
        End Get
        Set(ByVal value As Double)
            Me.mPaidByDisplay = value
        End Set
    End Property

    Public Property PaidByClickThrough() As Double
        Get
            Return Me.mPaidByClickThrough
        End Get
        Set(ByVal value As Double)
            Me.mPaidByClickThrough = value
        End Set
    End Property

    Public Property TotalPaid() As Double
        Get
            Return Me.mTotalPaid
        End Get
        Set(ByVal value As Double)
            Me.mTotalPaid = value
        End Set
    End Property

End Class
