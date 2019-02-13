Option Strict On

<Serializable()> Public Class AdReceiptProjectListData

    Private mProjects As New Generic.List(Of AdReceiptProjectData)
    Private mTotalPaidByDisplay As Double
    Private mTotalPaidByClickThrough As Double
    Private mTotalPaid As Double

    Public Sub AddProjects(ByVal receipt As AdReceiptProjectData)
        Me.mProjects.Add(receipt)
    End Sub

    Public Property Projects() As AdReceiptProjectData()
        Get
            Return Me.mProjects.ToArray
        End Get
        Set(ByVal value As AdReceiptProjectData())
            Me.mProjects = New Generic.List(Of AdReceiptProjectData)(value)
        End Set
    End Property

    Public Sub AddTotalPaidByDisplay(ByVal amount As Double)
        Me.mTotalPaidByDisplay += amount
    End Sub

    Public Property TotalPaidByDisplay() As Double
        Get
            Return Me.mTotalPaidByDisplay
        End Get
        Set(ByVal value As Double)
            Me.mTotalPaidByDisplay = value
        End Set
    End Property

    Public Sub AddTotalPaidByClickThrough(ByVal amount As Double)
        Me.mTotalPaidByClickThrough += amount
    End Sub

    Public Property TotalPaidByClickThrough() As Double
        Get
            Return Me.mTotalPaidByClickThrough
        End Get
        Set(ByVal value As Double)
            Me.mTotalPaidByClickThrough = value
        End Set
    End Property

    Public Sub AddTotalPaid(ByVal amount As Double)
        Me.mTotalPaid += amount
    End Sub

    Public Property TotalPaid() As Double
        Get
            Return Me.mTotalPaid
        End Get
        Set(ByVal value As Double)
            Me.mTotalPaid = value
        End Set
    End Property


End Class
