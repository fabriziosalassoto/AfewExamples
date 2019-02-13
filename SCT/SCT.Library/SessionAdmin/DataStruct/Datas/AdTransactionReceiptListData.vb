Option Strict On

<Serializable()> Public Class AdTransactionReceiptListData

    Private mTransactionReceipts As New Generic.List(Of AdTransactionData)
    Private mTransactionReceiptsTotal As Double

    Public Sub AddTransactionReceipts(ByVal transactionReceipt As AdTransactionData)
        Me.mTransactionReceipts.Add(transactionReceipt)
    End Sub

    Public Property TransactionReceipts() As AdTransactionData()
        Get
            Return Me.mTransactionReceipts.ToArray
        End Get
        Set(ByVal value As AdTransactionData())
            Me.mTransactionReceipts = New Generic.List(Of AdTransactionData)(value)
        End Set
    End Property

    Public Sub AddTransactionReceiptsTotal(ByVal amount As Double)
        Me.mTransactionReceiptsTotal += amount
    End Sub

    Public Property TransactionReceiptsTotal() As Double
        Get
            Return Me.mTransactionReceiptsTotal
        End Get
        Set(ByVal value As Double)
            Me.mTransactionReceiptsTotal = value
        End Set
    End Property

End Class
