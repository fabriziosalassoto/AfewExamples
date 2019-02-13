Option Strict On

<Serializable()> Public Class AdReceiptData

    Private mID As Long
    Private mReceiptNumber As Decimal
    Private mPaymentNumber As Decimal
    Private mPaymentAmount As Decimal
    Private mPaymentType As Integer
    Private mPaymentDate As Date
    Private mProjectList As New AdReceiptProjectListData

    Public Property ID() As Long
        Get
            Return Me.mID
        End Get
        Set(ByVal value As Long)
            Me.mID = value
        End Set
    End Property

    Public Property ReceiptNumber() As Decimal
        Get
            Return Me.mReceiptNumber
        End Get
        Set(ByVal value As Decimal)
            Me.mReceiptNumber = value
        End Set
    End Property

    Public Property PaymentNumber() As Decimal
        Get
            Return Me.mPaymentNumber
        End Get
        Set(ByVal value As Decimal)
            Me.mPaymentNumber = value
        End Set
    End Property

    Public Property PaymentAmount() As Decimal
        Get
            Return Me.mPaymentAmount
        End Get
        Set(ByVal value As Decimal)
            Me.mPaymentAmount = value
        End Set
    End Property

    Public Property PaymentType() As Integer
        Get
            Return Me.mPaymentType
        End Get
        Set(ByVal value As Integer)
            Me.mPaymentType = value
        End Set
    End Property

    Public Property PaymentDate() As DateTime
        Get
            Return Me.mPaymentDate
        End Get
        Set(ByVal value As DateTime)
            Me.mPaymentDate = value
        End Set
    End Property

    Public Property ProjectList() As AdReceiptProjectListData
        Get
            Return Me.mProjectList
        End Get
        Set(ByVal value As AdReceiptProjectListData)
            Me.mProjectList = value
        End Set
    End Property

End Class
