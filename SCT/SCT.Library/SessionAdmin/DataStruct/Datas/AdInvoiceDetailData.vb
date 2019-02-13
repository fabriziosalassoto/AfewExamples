Option Strict On

<Serializable()> Public Class AdInvoiceDetailData

    Private mID As Long
    Private mInvoice As New AdInvoiceData
    Private mCurrentNumberOfClickThrough As Integer
    Private mCurrentNumberOfDisplay As Integer
    Private mCostPerClickThrough As Double
    Private mCostPerDisplay As Double
    Private mAmountDue As Double

    Public Property ID() As Long
        Get
            Return Me.mID
        End Get
        Set(ByVal value As Long)
            Me.mID = value
        End Set
    End Property

    Public Property Invoice() As AdInvoiceData
        Get
            Return Me.mInvoice
        End Get
        Set(ByVal value As AdInvoiceData)
            Me.mInvoice = value
        End Set
    End Property

    Public Property CurrentNumberOfClickThrough() As Integer
        Get
            Return Me.mCurrentNumberOfClickThrough
        End Get
        Set(ByVal value As Integer)
            Me.mCurrentNumberOfClickThrough = value
        End Set
    End Property

    Public Property CurrentNumberOfDisplay() As Integer
        Get
            Return Me.mCurrentNumberOfDisplay
        End Get
        Set(ByVal value As Integer)
            Me.mCurrentNumberOfDisplay = value
        End Set
    End Property

    Public Property CostPerClickThrough() As Double
        Get
            Return Me.mCostPerClickThrough
        End Get
        Set(ByVal value As Double)
            Me.mCostPerClickThrough = value
        End Set
    End Property

    Public Property CostPerDisplay() As Double
        Get
            Return Me.mCostPerDisplay
        End Get
        Set(ByVal value As Double)
            Me.mCostPerDisplay = value
        End Set
    End Property

    Public Property AmountDue() As Double
        Get
            Return Me.mAmountDue
        End Get
        Set(ByVal value As Double)
            Me.mAmountDue = value
        End Set
    End Property

End Class
