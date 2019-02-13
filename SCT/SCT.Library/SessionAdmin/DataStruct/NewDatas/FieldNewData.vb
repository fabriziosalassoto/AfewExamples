Option Strict On

<Serializable()> Public Class FieldNewData

    Private mID As New NewFieldData(Of Long)
    Private mGroup As New GroupNewData
    Private mDescription As New NewFieldData(Of String)

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

    Public Property Group() As GroupNewData
        Get
            Return Me.mGroup
        End Get
        Set(ByVal value As GroupNewData)
            Me.mGroup = value
        End Set
    End Property

End Class
