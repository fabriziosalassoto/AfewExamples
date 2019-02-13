Option Strict On

<Serializable()> Public Class AdTransactionListData

    Private mTransactionInvoices As New AdTransactionInvoiceListData
    Private mTransactionReceipts As New AdTransactionReceiptListData

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

End Class
