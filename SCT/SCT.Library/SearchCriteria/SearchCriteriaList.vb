<Serializable()> Public Class SearchCriteriaList(Of T)

    Private mValues As New List(Of T)
    Private mPriority As Integer

    Public Shared Function Empty() As SearchCriteriaList(Of T)
        Return New SearchCriteriaList(Of T)(-1)
    End Function

    Public Sub New()

    End Sub

    Public Sub New(ByVal priority As Integer)
        Me.mPriority = priority
    End Sub

    Public Sub New(ByVal values() As T, ByVal priority As Integer)
        Me.mValues = New List(Of T)(values)
        Me.mPriority = priority
    End Sub

    Public Sub AddValue(ByVal value As T)
        Me.mValues.Add(value)
    End Sub

    Public Property Values() As T()
        Get
            Return Me.mValues.ToArray
        End Get
        Set(ByVal value As T())
            Me.mValues = New List(Of T)(value)
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
