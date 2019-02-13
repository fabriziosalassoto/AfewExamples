Option Strict On

<Serializable()> Public Class AdTransactionData

    Private mID As Long
    Private mAdvertiser As New AdAccountData
    Private mProject As New AdProjectData
    Private mTransactionType As DataAccess.Transactions
    Private mTransactionDate As Date
    Private mTransactionNumber As Decimal
    Private mTransactionAmount As Double

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

    Public Property Project() As AdProjectData
        Get
            Return Me.mProject
        End Get
        Set(ByVal value As AdProjectData)
            Me.mProject = value
        End Set
    End Property

    Public Property TransactionType() As DataAccess.Transactions
        Get
            Return Me.mTransactionType
        End Get
        Set(ByVal value As DataAccess.Transactions)
            Me.mTransactionType = value
        End Set
    End Property

    Public Property TransactionDate() As Date
        Get
            Return Me.mTransactionDate
        End Get
        Set(ByVal value As Date)
            Me.mTransactionDate = value
        End Set
    End Property

    Public Property TransactionNumber() As Decimal
        Get
            Return Me.mTransactionNumber
        End Get
        Set(ByVal value As Decimal)
            Me.mTransactionNumber = value
        End Set
    End Property

    Public Property TransactionAmount() As Double
        Get
            Return Me.mTransactionAmount
        End Get
        Set(ByVal value As Double)
            Me.mTransactionAmount = value
        End Set
    End Property

End Class
