Option Strict On

<Serializable()> Public Class AdTransactionInvoiceListData

    Private mTransactionInvoices As New Generic.List(Of AdTransactionData)
    Private mTransactionInvoicesTotal As Double

    Public Sub AddTransactionInvoices(ByVal transactionInvoice As AdTransactionData)
        Me.mTransactionInvoices.Add(transactionInvoice)
    End Sub

    Public Property TransactionInvoices() As AdTransactionData()
        Get
            Return Me.mTransactionInvoices.ToArray
        End Get
        Set(ByVal value As AdTransactionData())
            Me.mTransactionInvoices = New Generic.List(Of AdTransactionData)(value)
        End Set
    End Property

    Public Sub AddTransactionInvoicesTotal(ByVal amount As Double)
        Me.mTransactionInvoicesTotal += amount
    End Sub

    Public Property TransactionInvoicesTotal() As Double
        Get
            Return Me.mTransactionInvoicesTotal
        End Get
        Set(ByVal value As Double)
            Me.mTransactionInvoicesTotal = value
        End Set
    End Property

End Class
