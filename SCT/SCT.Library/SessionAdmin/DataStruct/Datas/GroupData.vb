Option Strict On

<Serializable()> Public Class GroupData

    Private mID As Long
    Private mForm As New FormData
    Private mDescription As String = String.Empty
    Private mProfiles As New Generic.List(Of GroupProfileData)
    Private mFields As New Generic.List(Of FieldData)

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

    Public Property Form() As FormData
        Get
            Return Me.mForm
        End Get
        Set(ByVal value As FormData)
            Me.mForm = value
        End Set
    End Property

    Public Sub AddProfile(ByVal profile As GroupProfileData)
        Me.mProfiles.Add(profile)
    End Sub

    Public Property Profiles() As GroupProfileData()
        Get
            Return Me.mProfiles.ToArray
        End Get
        Set(ByVal value As GroupProfileData())
            Me.mProfiles = New Generic.List(Of GroupProfileData)(value)
        End Set
    End Property

    Public Sub AddField(ByVal field As FieldData)
        Me.mFields.Add(field)
    End Sub

    Public Property Fields() As FieldData()
        Get
            Return Me.mFields.ToArray
        End Get
        Set(ByVal value As FieldData())
            Me.mFields = New Generic.List(Of FieldData)(value)
        End Set
    End Property

    
End Class
