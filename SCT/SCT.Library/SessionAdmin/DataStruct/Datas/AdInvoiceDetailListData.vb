Option Strict On

<Serializable()> Public Class AdInvoiceDetailListData

    Private mDetails As New Generic.List(Of AdInvoiceDetailData)
    Private mTotalAmountDue As Double

    Public Sub AddDetails(ByVal detail As AdInvoiceDetailData)
        Me.mDetails.Add(detail)
    End Sub

    Public Property Details() As AdInvoiceDetailData()
        Get
            Return Me.mDetails.ToArray
        End Get
        Set(ByVal value As AdInvoiceDetailData())
            Me.mDetails = New Generic.List(Of AdInvoiceDetailData)(value)
        End Set
    End Property

    Public Sub AddTotalAmountDue(ByVal amount As Double)
        Me.mTotalAmountDue += amount
    End Sub

    Public Property TotalAmountDue() As Double
        Get
            Return Me.mTotalAmountDue
        End Get
        Set(ByVal value As Double)
            Me.mTotalAmountDue = value
        End Set
    End Property

End Class
