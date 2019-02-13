Option Strict On

<Serializable()> Public Class AdAccountData

    Private mID As Long
    Private mCompanyName As String = String.Empty
    Private mCompanyNotes As String = String.Empty
    Private mLogin As String = String.Empty
    Private mClientPassword As String = String.Empty
    Private mWebPassword As String = String.Empty
    Private mHintByPassOne As String = String.Empty
    Private mHintByPassTwo As String = String.Empty
    Private mContacts As New Generic.List(Of AdContactData)
    Private mProjects As New Generic.List(Of AdProjectData)

    Public Property ID() As Long
        Get
            Return Me.mID
        End Get
        Set(ByVal value As Long)
            Me.mID = value
        End Set
    End Property

    Public Property CompanyName() As String
        Get
            Return Me.mCompanyName
        End Get
        Set(ByVal value As String)
            Me.mCompanyName = value
        End Set
    End Property

    Public Property CompanyNotes() As String
        Get
            Return Me.mCompanyNotes
        End Get
        Set(ByVal value As String)
            Me.mCompanyNotes = value
        End Set
    End Property

    Public Property Login() As String
        Get
            Return Me.mLogin
        End Get
        Set(ByVal value As String)
            Me.mLogin = value
        End Set
    End Property

    Public Property ClientPassword() As String
        Get
            Return Me.mClientPassword
        End Get
        Set(ByVal value As String)
            Me.mClientPassword = value
        End Set
    End Property

    Public Property WebPassword() As String
        Get
            Return Me.mWebPassword
        End Get
        Set(ByVal value As String)
            Me.mWebPassword = value
        End Set
    End Property

    Public Property HintByPassOne() As String
        Get
            Return Me.mHintByPassOne
        End Get
        Set(ByVal value As String)
            Me.mHintByPassOne = value
        End Set
    End Property

    Public Property HintByPassTwo() As String
        Get
            Return Me.mHintByPassTwo
        End Get
        Set(ByVal value As String)
            Me.mHintByPassTwo = value
        End Set
    End Property

    Public Sub AddContact(ByVal contact As AdContactData)
        Me.mContacts.Add(contact)
    End Sub

    Public Property Contacts() As AdContactData()
        Get
            Return Me.mContacts.ToArray
        End Get
        Set(ByVal value As AdContactData())
            Me.mContacts = New Generic.List(Of AdContactData)(value)
        End Set
    End Property

    Public Sub AddProject(ByVal project As AdProjectData)
        Me.mProjects.Add(project)
    End Sub

    Public Property Projects() As AdProjectData()
        Get
            Return Me.mProjects.ToArray
        End Get
        Set(ByVal value As AdProjectData())
            Me.mProjects = New Generic.List(Of AdProjectData)(value)
        End Set
    End Property

End Class
