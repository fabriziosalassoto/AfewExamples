<Serializable()> Public Class Parameter(Of T)

    Private mValue As T
    Private mPriority As Integer

    Public Shared Function Empty() As Parameter(Of T)
        Return New Parameter(Of T)(-1)
    End Function

    Public Sub New(ByVal priority As Integer)
        Me.mPriority = priority
    End Sub

    Public Sub New(ByVal value As T)
        Me.mValue = value
        Me.mPriority = 0
    End Sub

    Public Sub New(ByVal value As T, ByVal priority As Integer)
        Me.mValue = value
        Me.mPriority = priority
    End Sub

    Public Property Value() As T
        Get
            Return Me.mValue
        End Get
        Set(ByVal value As T)
            Me.mValue = value
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
