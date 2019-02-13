Option Strict On

<Serializable()> Public Class FormData

    Private mID As Long
    Private mDescription As String = String.Empty
    Private mLogDescription As String = String.Empty
    Private mProfiles As New Generic.List(Of FormProfileData)
    Private mGroups As New Generic.List(Of GroupData)

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

    Public Property LogDescription() As String
        Get
            Return Me.mLogDescription
        End Get
        Set(ByVal value As String)
            Me.mLogDescription = value
        End Set
    End Property

    Public Sub AddProfile(ByVal profile As FormProfileData)
        Me.mProfiles.Add(profile)
    End Sub

    Public Property Profiles() As FormProfileData()
        Get
            Return Me.mProfiles.ToArray
        End Get
        Set(ByVal value As FormProfileData())
            Me.mProfiles = New Generic.List(Of FormProfileData)(value)
        End Set
    End Property

    Public Sub AddGroup(ByVal group As GroupData)
        Me.mGroups.Add(group)
    End Sub

    Public Property Groups() As GroupData()
        Get
            Return Me.mGroups.ToArray
        End Get
        Set(ByVal value As GroupData())
            Me.mGroups = New Generic.List(Of GroupData)(value)
        End Set
    End Property

End Class
