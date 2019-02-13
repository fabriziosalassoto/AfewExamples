Option Strict On

<Serializable()> Public Class AdReceiptNewData

    Private mID As New NewFieldData(Of Long)
    Private mReceiptNumber As New NewFieldData(Of Decimal)
    Private mPaymentNumber As New NewFieldData(Of Decimal)
    Private mPaymentAmount As New NewFieldData(Of Decimal)
    Private mPaymentType As New NewFieldData(Of Integer)
    Private mPaymentDate As New NewFieldData(Of Date)

    Public Property ID() As NewFieldData(Of Long)
        Get
            Return Me.mID
        End Get
        Set(ByVal value As NewFieldData(Of Long))
            Me.mID = value
        End Set
    End Property

    Public Property ReceiptNumber() As NewFieldData(Of Decimal)
        Get
            Return Me.mReceiptNumber
        End Get
        Set(ByVal value As NewFieldData(Of Decimal))
            Me.mReceiptNumber = value
        End Set
    End Property

    Public Property PaymentNumber() As NewFieldData(Of Decimal)
        Get
            Return Me.mPaymentNumber
        End Get
        Set(ByVal value As NewFieldData(Of Decimal))
            Me.mPaymentNumber = value
        End Set
    End Property

    Public Property PaymentAmount() As NewFieldData(Of Decimal)
        Get
            Return Me.mPaymentAmount
        End Get
        Set(ByVal value As NewFieldData(Of Decimal))
            Me.mPaymentAmount = value
        End Set
    End Property

    Public Property PaymentType() As NewFieldData(Of Integer)
        Get
            Return Me.mPaymentType
        End Get
        Set(ByVal value As NewFieldData(Of Integer))
            Me.mPaymentType = value
        End Set
    End Property

    Public Property PaymentDate() As NewFieldData(Of DateTime)
        Get
            Return Me.mPaymentDate
        End Get
        Set(ByVal value As NewFieldData(Of DateTime))
            Me.mPaymentDate = value
        End Set
    End Property

End Class
