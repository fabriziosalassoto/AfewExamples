Option Strict On

<Serializable()> Public Class GroupProfileData

    Private mID As Long
    Private mDescription As String = String.Empty
    Private mGroup As New GroupData
    Private mFormProfile As New FormProfileData
    Private mPSelect As Boolean = False
    Private mPInsert As Boolean = False
    Private mPUpdate As Boolean = False
    Private mPDelete As Boolean = False

    Public Property ID() As Long
        Get
            Return Me.mID
        End Get
        Set(ByVal value As Long)
            Me.mID = value
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

    Public Property Group() As GroupData
        Get
            Return Me.mGroup
        End Get
        Set(ByVal value As GroupData)
            Me.mGroup = value
        End Set
    End Property

    Public Property FormProfile() As FormProfileData
        Get
            Return Me.mFormProfile
        End Get
        Set(ByVal value As FormProfileData)
            Me.mFormProfile = value
        End Set
    End Property

    Public Property PSelect() As Boolean
        Get
            Return Me.mPSelect
        End Get
        Set(ByVal value As Boolean)
            Me.mPSelect = value
        End Set
    End Property

    Public Property PInsert() As Boolean
        Get
            Return Me.mPInsert
        End Get
        Set(ByVal value As Boolean)
            Me.mPInsert = value
        End Set
    End Property

    Public Property PUpdate() As Boolean
        Get
            Return Me.mPUpdate
        End Get
        Set(ByVal value As Boolean)
            Me.mPUpdate = value
        End Set
    End Property

    Public Property PDelete() As Boolean
        Get
            Return Me.mPDelete
        End Get
        Set(ByVal value As Boolean)
            Me.mPDelete = value
        End Set
    End Property

End Class
