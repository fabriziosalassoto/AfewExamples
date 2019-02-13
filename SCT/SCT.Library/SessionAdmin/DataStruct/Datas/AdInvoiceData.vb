Option Strict On

<Serializable()> Public Class AdInvoiceData

    Private mID As Long
    Private mProject As New AdProjectData
    Private mInvoiceNumber As Decimal
    Private mInvoiceSequence As Decimal
    Private mInvoiceDate As Date
    Private mPaidToDate As Date
    Private mChargedToDate As Date
    Private mPreviousBalance As Double
    Private mPreviousNumberOfClickThrough As Integer
    Private mPreviousNumberOfDisplays As Integer
    Private mTotalAmountDue As Double
    Private mDetailList As New AdInvoiceDetailListData

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

    Public Property InvoiceNumber() As Decimal
        Get
            Return Me.mInvoiceNumber
        End Get
        Set(ByVal value As Decimal)
            Me.mInvoiceNumber = value
        End Set
    End Property

    Public Property InvoiceSequence() As Decimal
        Get
            Return Me.mInvoiceSequence
        End Get
        Set(ByVal value As Decimal)
            Me.mInvoiceSequence = value
        End Set
    End Property

    Public Property InvoiceDate() As DateTime
        Get
            Return Me.mInvoiceDate
        End Get
        Set(ByVal value As DateTime)
            Me.mInvoiceDate = value
        End Set
    End Property

    Public Property PaidToDate() As DateTime
        Get
            Return Me.mPaidToDate
        End Get
        Set(ByVal value As DateTime)
            Me.mPaidToDate = value
        End Set
    End Property

    Public Property ChargedToDate() As DateTime
        Get
            Return Me.mChargedToDate
        End Get
        Set(ByVal value As DateTime)
            Me.mChargedToDate = value
        End Set
    End Property

    Public Property PreviousBalance() As Double
        Get
            Return Me.mPreviousBalance
        End Get
        Set(ByVal value As Double)
            Me.mPreviousBalance = value
        End Set
    End Property

    Public Property PreviousNumberOfClickThrough() As Integer
        Get
            Return Me.mPreviousNumberOfClickThrough
        End Get
        Set(ByVal value As Integer)
            Me.mPreviousNumberOfClickThrough = value
        End Set
    End Property

    Public Property PreviousNumberOfDisplays() As Integer
        Get
            Return Me.mPreviousNumberOfDisplays
        End Get
        Set(ByVal value As Integer)
            Me.mPreviousNumberOfDisplays = value
        End Set
    End Property

    Public Property TotalAmountDue() As Double
        Get
            Return Me.mTotalAmountDue
        End Get
        Set(ByVal value As Double)
            Me.mTotalAmountDue = value
        End Set
    End Property

    Public Property DetailList() As AdInvoiceDetailListData
        Get
            Return Me.mDetailList
        End Get
        Set(ByVal value As AdInvoiceDetailListData)
            Me.mDetailList = value
        End Set
    End Property

End Class
