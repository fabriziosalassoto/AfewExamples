Option Strict On

<Serializable()> Public Class BinnacleTableFieldData

    Private mID As Long
    Private mBinnacleTable As New BinnacleTableData
    Private mFieldName As String = String.Empty
    Private mOldValue As String = String.Empty
    Private mNewValue As String = String.Empty

    Public Property ID() As Long
        Get
            Return Me.mID
        End Get
        Set(ByVal value As Long)
            Me.mID = value
        End Set
    End Property

    Public Property FieldName() As String
        Get
            Return Me.mFieldName
        End Get
        Set(ByVal value As String)
            Me.mFieldName = value
        End Set
    End Property

    Public Property OldValue() As String
        Get
            Return Me.mOldValue
        End Get
        Set(ByVal value As String)
            Me.mOldValue = value
        End Set
    End Property

    Public Property NewValue() As String
        Get
            Return Me.mNewValue
        End Get
        Set(ByVal value As String)
            Me.mNewValue = value
        End Set
    End Property

    Public Property BinnacleTable() As BinnacleTableData
        Get
            Return Me.mBinnacleTable
        End Get
        Set(ByVal value As BinnacleTableData)
            Me.mBinnacleTable = value
        End Set
    End Property

End Class
