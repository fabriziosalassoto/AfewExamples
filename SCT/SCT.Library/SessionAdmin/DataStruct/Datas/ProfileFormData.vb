Option Strict On

<Serializable()> Public Class ProfileFormData

    Private mID As Long
    Private mDescription As String = String.Empty
    Private mProfile As New ProfileData
    Private mPSelect As Boolean
    Private mPInsert As Boolean
    Private mPUpdate As Boolean
    Private mPDelete As Boolean

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

    Public Property Profile() As ProfileData
        Get
            Return Me.mProfile
        End Get
        Set(ByVal value As ProfileData)
            Me.mProfile = value
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

