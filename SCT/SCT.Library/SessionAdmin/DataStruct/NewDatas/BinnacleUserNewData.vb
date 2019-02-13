Option Strict On

<Serializable()> Public Class BinnacleUserNewData

    Private mID As New NewFieldData(Of Long)
    Private mName As New NewFieldData(Of String)

    Public Property ID() As NewFieldData(Of Long)
        Get
            Return Me.mID
        End Get
        Set(ByVal value As NewFieldData(Of Long))
            Me.mID = value
        End Set
    End Property

    Public Property Name() As NewFieldData(Of String)
        Get
            Return Me.mName
        End Get
        Set(ByVal value As NewFieldData(Of String))
            Me.mName = value
        End Set
    End Property

End Class
