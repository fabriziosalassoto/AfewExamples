Option Strict On

<Serializable()> Public Class BinnacleFormNewData

    Private mID As New NewFieldData(Of Long)
    Private mBinnacle As New BinnacleNewData
    Private mForm As New FormNewData
    Private mOperation As New OperationNewData
    Private mBHour As New NewFieldData(Of Date)

    Public Property ID() As NewFieldData(Of Long)
        Get
            Return Me.mID
        End Get
        Set(ByVal value As NewFieldData(Of Long))
            Me.mID = value
        End Set
    End Property

    Public Property Binnacle() As BinnacleNewData
        Get
            Return Me.mBinnacle
        End Get
        Set(ByVal value As BinnacleNewData)
            Me.mBinnacle = value
        End Set
    End Property

    Public Property Form() As FormNewData
        Get
            Return Me.mForm
        End Get
        Set(ByVal value As FormNewData)
            Me.mForm = value
        End Set
    End Property

    Public Property Operation() As OperationNewData
        Get
            Return Me.mOperation
        End Get
        Set(ByVal value As OperationNewData)
            Me.mOperation = value
        End Set
    End Property

    Public Property BHour() As NewFieldData(Of Date)
        Get
            Return Me.mBHour
        End Get
        Set(ByVal value As NewFieldData(Of Date))
            Me.mBHour = value
        End Set
    End Property

End Class
