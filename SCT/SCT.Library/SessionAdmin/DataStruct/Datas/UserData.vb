Option Strict On

<Serializable()> Public Class UserData

    Private mID As Long 
    Private mProfile As New ProfileData
    Private mFirstName As String = String.Empty
    Private mLastName As String = String.Empty
    Private mLogin As String = String.Empty
    Private mPassword As String = String.Empty

    Public Property ID() As Long
        Get
            Return Me.mID
        End Get
        Set(ByVal value As Long)
            Me.mID = value
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

    Public Property FirstName() As String
        Get
            Return Me.mFirstName
        End Get
        Set(ByVal value As String)
            Me.mFirstName = value
        End Set
    End Property

    Public Property LastName() As String
        Get
            Return Me.mLastName
        End Get
        Set(ByVal value As String)
            Me.mLastName = value
        End Set
    End Property

    Public ReadOnly Property FullName() As String
        Get
            Return Me.mLastName & ", " & Me.mFirstName
        End Get
    End Property

    Public Property Login() As String
        Get
            Return Me.mLogin
        End Get
        Set(ByVal value As String)
            Me.mLogin = value
        End Set
    End Property

    Public Property Password() As String
        Get
            Return Me.mPassword
        End Get
        Set(ByVal value As String)
            Me.mPassword = value
        End Set
    End Property

End Class
