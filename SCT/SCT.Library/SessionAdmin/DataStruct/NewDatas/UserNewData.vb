Option Strict On

<Serializable()> Public Class UserNewData

    Private mID As New NewFieldData(Of Long)
    Private mProfile As New ProfileNewData
    Private mFirstName As New NewFieldData(Of String)
    Private mLastName As New NewFieldData(Of String)
    Private mLogin As New NewFieldData(Of String)
    Private mPassword As New NewFieldData(Of String)

    Public Property ID() As NewFieldData(Of Long)
        Get
            Return Me.mID
        End Get
        Set(ByVal value As NewFieldData(Of Long))
            Me.mID = value
        End Set
    End Property

    Public Property Profile() As ProfileNewData
        Get
            Return Me.mProfile
        End Get
        Set(ByVal value As ProfileNewData)
            Me.mProfile = value
        End Set
    End Property

    Public Property FirstName() As NewFieldData(Of String)
        Get
            Return Me.mFirstName
        End Get
        Set(ByVal value As NewFieldData(Of String))
            Me.mFirstName = value
        End Set
    End Property

    Public Property LastName() As NewFieldData(Of String)
        Get
            Return Me.mLastName
        End Get
        Set(ByVal value As NewFieldData(Of String))
            Me.mLastName = value
        End Set
    End Property

    Public Property Login() As NewFieldData(Of String)
        Get
            Return Me.mLogin
        End Get
        Set(ByVal value As NewFieldData(Of String))
            Me.mLogin = value
        End Set
    End Property

    Public Property Password() As NewFieldData(Of String)
        Get
            Return Me.mPassword
        End Get
        Set(ByVal value As NewFieldData(Of String))
            Me.mPassword = value
        End Set
    End Property

End Class
