Option Strict On

<Serializable()> Public Class GroupProfileNewData

    Private mID As New NewFieldData(Of Long)
    Private mDescription As New NewFieldData(Of String)
    Private mGroup As New GroupNewData
    Private mFormProfile As New FormProfileNewData
    Private mPSelect As New NewFieldData(Of Boolean)
    Private mPInsert As New NewFieldData(Of Boolean)
    Private mPUpdate As New NewFieldData(Of Boolean)
    Private mPDelete As New NewFieldData(Of Boolean)

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

    Public Property FormProfile() As FormProfileNewData
        Get
            Return Me.mFormProfile
        End Get
        Set(ByVal value As FormProfileNewData)
            Me.mFormProfile = value
        End Set
    End Property

    Public Property PSelect() As NewFieldData(Of Boolean)
        Get
            Return Me.mPSelect
        End Get
        Set(ByVal value As NewFieldData(Of Boolean))
            Me.mPSelect = value
        End Set
    End Property

    Public Property PInsert() As NewFieldData(Of Boolean)
        Get
            Return Me.mPInsert
        End Get
        Set(ByVal value As NewFieldData(Of Boolean))
            Me.mPInsert = value
        End Set
    End Property

    Public Property PUpdate() As NewFieldData(Of Boolean)
        Get
            Return Me.mPUpdate
        End Get
        Set(ByVal value As NewFieldData(Of Boolean))
            Me.mPUpdate = value
        End Set
    End Property

    Public Property PDelete() As NewFieldData(Of Boolean)
        Get
            Return Me.mPDelete
        End Get
        Set(ByVal value As NewFieldData(Of Boolean))
            Me.mPDelete = value
        End Set
    End Property

End Class
