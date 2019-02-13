Option Strict On

<Serializable()> Public Class GroupNewData

    Private mID As New NewFieldData(Of Long)
    Private mForm As New FormNewData
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

    Public Property Form() As FormNewData
        Get
            Return Me.mForm
        End Get
        Set(ByVal value As FormNewData)
            Me.mForm = value
        End Set
    End Property

End Class
