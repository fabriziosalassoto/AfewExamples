Option Strict On

<Serializable()> Public Class BinnacleTableData

    Private mID As Long
    Private mBinnacle As New BinnacleData
    Private mOperation As New OperationData
    Private mTableName As String = String.Empty
    Private mBHour As New Date
    Private mBinnacleTableFields As New Generic.List(Of BinnacleTableFieldData)

    Public Property ID() As Long
        Get
            Return Me.mID
        End Get
        Set(ByVal value As Long)
            Me.mID = value
        End Set
    End Property

    Public Property Binnacle() As BinnacleData
        Get
            Return Me.mBinnacle
        End Get
        Set(ByVal value As BinnacleData)
            Me.mBinnacle = value
        End Set
    End Property

    Public Property Operation() As OperationData
        Get
            Return Me.mOperation
        End Get
        Set(ByVal value As OperationData)
            Me.mOperation = value
        End Set
    End Property

    Public Property TableName() As String
        Get
            Return Me.mTableName
        End Get
        Set(ByVal value As String)
            Me.mTableName = value
        End Set
    End Property

    Public Property BHour() As Date
        Get
            Return Me.mBHour
        End Get
        Set(ByVal value As Date)
            Me.mBHour = value
        End Set
    End Property

    Public Sub AddBinnacleTableField(ByVal binnacleTableField As BinnacleTableFieldData)
        Me.mBinnacleTableFields.Add(binnacleTableField)
    End Sub

    Public Property BinnacleTableField() As BinnacleTableFieldData()
        Get
            Return Me.mBinnacleTableFields.ToArray
        End Get
        Set(ByVal value As BinnacleTableFieldData())
            Me.mBinnacleTableFields = New Generic.List(Of BinnacleTableFieldData)(value)
        End Set
    End Property

End Class
