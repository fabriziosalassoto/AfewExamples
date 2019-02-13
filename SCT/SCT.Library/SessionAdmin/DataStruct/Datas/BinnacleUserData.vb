Option Strict On

<Serializable()> Public Class BinnacleUserData

    Private mID As Long
    Private mName As String = String.Empty

    Public Property ID() As Long
        Get
            Return Me.mID
        End Get
        Set(ByVal value As Long)
            Me.mID = value
        End Set
    End Property

    Public Property Name() As String
        Get
            Return Me.mName
        End Get
        Set(ByVal value As String)
            Me.mName = value
        End Set
    End Property

End Class
