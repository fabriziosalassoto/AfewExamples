<Serializable()> Public Class ParameterList(Of T)

    Private mValues() As T
    Private mPriority As Integer

    Public Sub New(ByVal values() As T, ByVal priority As Integer)
        Me.mValues = values
        Me.mPriority = priority
    End Sub

    Public Property Values() As T()
        Get
            Return Me.mValues
        End Get
        Set(ByVal value As T())
            Me.mValues = value
        End Set
    End Property

    Public Property Priority() As Integer
        Get
            Return Me.mPriority
        End Get
        Set(ByVal value As Integer)
            Me.mPriority = value
        End Set
    End Property

End Class
