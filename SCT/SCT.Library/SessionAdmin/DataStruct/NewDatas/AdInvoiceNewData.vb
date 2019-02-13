Option Strict On

<Serializable()> Public Class AdInvoiceNewData

    Private mID As New NewFieldData(Of Long)
    Private mProject As New AdProjectNewData
    Private mInvoiceNumber As New NewFieldData(Of Decimal)
    Private mInvoiceSequence As New NewFieldData(Of Decimal)
    Private mInvoiceDate As New NewFieldData(Of Date)
    Private mPaidToDate As New NewFieldData(Of Date)
    Private mChargedToDate As New NewFieldData(Of Date)
    Private mPreviousBalance As New NewFieldData(Of Double)
    Private mPreviousNumberOfClickThrough As New NewFieldData(Of Integer)
    Private mPreviousNumberOfDisplays As New NewFieldData(Of Integer)
    Private mTotalAmountDue As New NewFieldData(Of Double)

    Public Property ID() As NewFieldData(Of Long)
        Get
            Return Me.mID
        End Get
        Set(ByVal value As NewFieldData(Of Long))
            Me.mID = value
        End Set
    End Property

    Public Property Project() As AdProjectNewData
        Get
            Return Me.mProject
        End Get
        Set(ByVal value As AdProjectNewData)
            Me.mProject = value
        End Set
    End Property

    Public Property InvoiceNumber() As NewFieldData(Of Decimal)
        Get
            Return Me.mInvoiceNumber
        End Get
        Set(ByVal value As NewFieldData(Of Decimal))
            Me.mInvoiceNumber = value
        End Set
    End Property

    Public Property InvoiceSequence() As NewFieldData(Of Decimal)
        Get
            Return Me.mInvoiceSequence
        End Get
        Set(ByVal value As NewFieldData(Of Decimal))
            Me.mInvoiceSequence = value
        End Set
    End Property

    Public Property InvoiceDate() As NewFieldData(Of Date)
        Get
            Return Me.mInvoiceDate
        End Get
        Set(ByVal value As NewFieldData(Of Date))
            Me.mInvoiceDate = value
        End Set
    End Property

    Public Property PaidToDate() As NewFieldData(Of Date)
        Get
            Return Me.mPaidToDate
        End Get
        Set(ByVal value As NewFieldData(Of Date))
            Me.mPaidToDate = value
        End Set
    End Property

    Public Property ChargedToDate() As NewFieldData(Of Date)
        Get
            Return Me.mChargedToDate
        End Get
        Set(ByVal value As NewFieldData(Of Date))
            Me.mChargedToDate = value
        End Set
    End Property

    Public Property PreviousBalance() As NewFieldData(Of Double)
        Get
            Return Me.mPreviousBalance
        End Get
        Set(ByVal value As NewFieldData(Of Double))
            Me.mPreviousBalance = value
        End Set
    End Property

    Public Property PreviousNumberOfClickThrough() As NewFieldData(Of Integer)
        Get
            Return Me.mPreviousNumberOfClickThrough
        End Get
        Set(ByVal value As NewFieldData(Of Integer))
            Me.mPreviousNumberOfClickThrough = value
        End Set
    End Property

    Public Property PreviousNumberOfDisplays() As NewFieldData(Of Integer)
        Get
            Return Me.mPreviousNumberOfDisplays
        End Get
        Set(ByVal value As NewFieldData(Of Integer))
            Me.mPreviousNumberOfDisplays = value
        End Set
    End Property

    Public Property TotalAmountDue() As NewFieldData(Of Double)
        Get
            Return Me.mTotalAmountDue
        End Get
        Set(ByVal value As NewFieldData(Of Double))
            Me.mTotalAmountDue = value
        End Set
    End Property

End Class
