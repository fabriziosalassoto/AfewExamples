Option Strict On

<Serializable()> Public Class FormNewData

    Private mID As New NewFieldData(Of Long)
    Private mDescription As New NewFieldData(Of String)
    Private mLogDescription As New NewFieldData(Of String)

    Public Property ID() As NewFieldData(Of Long)
        Get
            Return Me.mID
        End Get
        Set(ByVal value As NewFieldData(Of Long))
            Me.mID = value
        End Set
    End Property

    Public Property Description() As NewFieldData(Of String)
        Get
            Return Me.mDescription
        End Get
        Set(ByVal value As NewFieldData(Of String))
            Me.mDescription = value
        End Set
    End Property

    Public Property LogDescription() As NewFieldData(Of String)
        Get
            Return Me.mLogDescription
        End Get
        Set(ByVal value As NewFieldData(Of String))
            Me.mLogDescription = value
        End Set
    End Property

End Class
