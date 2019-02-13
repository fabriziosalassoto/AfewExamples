Option Strict On

<Serializable()> Public Class LogData

    Private mValue As DataAccess.Logs
    Private mDescription As String = String.Empty

    Public Property Value() As DataAccess.Logs
        Get
            Return Me.mValue
        End Get
        Set(ByVal value As DataAccess.Logs)
            Me.mValue = value
        End Set
    End Property

    Public Property Description() As String
        Get
            Return Me.mDescription
        End Get
        Set(ByVal value As String)
            Me.mDescription = value
        End Set
    End Property

End Class
