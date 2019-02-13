Option Strict On

<Serializable()> Public Class FieldData

    Private mID As Long
    Private mGroup As New GroupData
    Private mDescription As String = String.Empty

    Public Property ID() As Long
        Get
            Return Me.mID
        End Get
        Set(ByVal value As Long)
            Me.mID = value
        End Set
    End Property

    Public Property Group() As GroupData
        Get
            Return Me.mGroup
        End Get
        Set(ByVal value As GroupData)
            Me.mGroup = value
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
