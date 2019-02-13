Option Strict On

<Serializable()> Public Class BinnacleNewData

    Private mID As New NewFieldData(Of Long)
    Private mUser As New UserNewData
    Private mBDate As New NewFieldData(Of Date)

    Public Property ID() As NewFieldData(Of Long)
        Get
            Return Me.mID
        End Get
        Set(ByVal value As NewFieldData(Of Long))
            Me.mID = value
        End Set
    End Property

    Public Property User() As UserNewData
        Get
            Return Me.mUser
        End Get
        Set(ByVal value As UserNewData)
            Me.mUser = value
        End Set
    End Property

    Public Property BDate() As NewFieldData(Of Date)
        Get
            Return Me.mBDate
        End Get
        Set(ByVal value As NewFieldData(Of Date))
            Me.mBDate = value
        End Set
    End Property

End Class
