Option Strict On

<Serializable()> Public Class AdStateOfAccountData

    Private mTransactionInvoices As New AdTransactionInvoiceListData
    Private mTransactionReceipts As New AdTransactionReceiptListData
    Private mStartBalance As Double
    Private mEndBalance As Double

    Public Property TransactionInvoices() As AdTransactionInvoiceListData
        Get
            Return Me.mTransactionInvoices
        End Get
        Set(ByVal value As AdTransactionInvoiceListData)
            Me.mTransactionInvoices = value
        End Set
    End Property

    Public Property TransactionReceipts() As AdTransactionReceiptListData
        Get
            Return Me.mTransactionReceipts
        End Get
        Set(ByVal value As AdTransactionReceiptListData)
            Me.mTransactionReceipts = value
        End Set
    End Property

    Public Sub AddStartBalance(ByVal amount As Double)
        Me.mStartBalance += amount
    End Sub

    Public Sub SubtractStartBalance(ByVal amount As Double)
        Me.mStartBalance -= amount
    End Sub

    Public Property StartBalance() As Double
        Get
            Return Me.mStartBalance
        End Get
        Set(ByVal value As Double)
            Me.mStartBalance = value
        End Set
    End Property

    Public Sub AddEndBalance(ByVal amount As Double)
        Me.mEndBalance += amount
    End Sub

    Public Sub SubtractEndBalance(ByVal amount As Double)
        Me.mEndBalance -= amount
    End Sub

    Public Property EndBalance() As Double
        Get
            Return Me.mEndBalance
        End Get
        Set(ByVal value As Double)
            Me.mEndBalance = value
        End Set
    End Property

End Class
