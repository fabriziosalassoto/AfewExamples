<Serializable()> Public Class StructField(Of T)

    Private mFieldName As String = String.Empty
    Private mIsPrimaryKey As Boolean
    Private mValue As T
    Private mOldValue As T
    Private mNewValue As T

    Protected Friend Sub New(ByVal fieldName As String, ByVal isPrimaryKey As Boolean)
        Me.mFieldName = fieldName
        Me.mIsPrimaryKey = isPrimaryKey
    End Sub

    Protected Friend Sub New(ByVal fieldName As String, ByVal isPrimaryKey As Boolean, ByVal value As T)
        Me.mFieldName = fieldName
        Me.mIsPrimaryKey = isPrimaryKey
        Me.mValue = value
        Me.mOldValue = value
    End Sub

    Protected Friend Sub SetValue(ByVal value As T)
        Me.mValue = value
        Me.mOldValue = value
    End Sub

    Public ReadOnly Property FieldName() As String
        Get
            Return Me.mFieldName
        End Get
    End Property

    Public ReadOnly Property IsPrimaryKey() As Boolean
        Get
            Return Me.mIsPrimaryKey
        End Get
    End Property

    Public Property Value() As T
        Get
            Return Me.mValue
        End Get
        Protected Friend Set(ByVal value As T)
            Me.mValue = value
        End Set
    End Property

    Public Property OldValue() As T
        Get
            Return Me.mOldValue
        End Get
        Protected Friend Set(ByVal value As T)
            Me.mOldValue = value
        End Set
    End Property

    Public Property NewValue() As T
        Protected Friend Get
            Return Me.mNewValue
        End Get
        Set(ByVal value As T)
            Me.mNewValue = value
        End Set
    End Property

End Class
