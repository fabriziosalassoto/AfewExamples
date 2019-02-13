Option Strict On

<Serializable()> Public Class AdContactData

    Private mID As Long
    Private mAdvertiser As New AdAccountData
    Private mFirstName As String = String.Empty
    Private mLastName As String = String.Empty
    Private mMainCompanyAddress As Boolean
    Private mPrimaryAddress As String = String.Empty
    Private mSecondaryAddress As String = String.Empty
    Private mCity As String = String.Empty
    Private mState As String = String.Empty
    Private mCountry As String = String.Empty
    Private mZipCode As String = String.Empty
    Private mProvidence As String = String.Empty
    Private mDepartment As String = String.Empty
    Private mResposibleForNotes As String = String.Empty
    Private mNotes As New Generic.List(Of AdNoteData)
    Private mToDos As New Generic.List(Of AdToDoData)
    Private mProjects As New Generic.List(Of AdProjectData)

    Public Property ID() As Long
        Get
            Return Me.mID
        End Get
        Set(ByVal value As Long)
            Me.mID = value
        End Set
    End Property

    Public Property Advertiser() As AdAccountData
        Get
            Return Me.mAdvertiser
        End Get
        Set(ByVal value As AdAccountData)
            Me.mAdvertiser = value
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

    Public Property MainCompanyAddress() As Boolean
        Get
            Return Me.mMainCompanyAddress
        End Get
        Set(ByVal value As Boolean)
            Me.mMainCompanyAddress = value
        End Set
    End Property

    Public Property PrimaryAddress() As String
        Get
            Return Me.mPrimaryAddress
        End Get
        Set(ByVal value As String)
            Me.mPrimaryAddress = value
        End Set
    End Property

    Public Property SecondaryAddress() As String
        Get
            Return Me.mSecondaryAddress
        End Get
        Set(ByVal value As String)
            Me.mSecondaryAddress = value
        End Set
    End Property

    Public Property City() As String
        Get
            Return Me.mCity
        End Get
        Set(ByVal value As String)
            Me.mCity = value
        End Set
    End Property

    Public Property State() As String
        Get
            Return Me.mState
        End Get
        Set(ByVal value As String)
            Me.mState = value
        End Set
    End Property

    Public Property Country() As String
        Get
            Return Me.mCountry
        End Get
        Set(ByVal value As String)
            Me.mCountry = value
        End Set
    End Property


    Public Property ZipCode() As String
        Get
            Return Me.mZipCode
        End Get
        Set(ByVal value As String)
            Me.mZipCode = value
        End Set
    End Property

    Public Property Providence() As String
        Get
            Return Me.mProvidence
        End Get
        Set(ByVal value As String)
            Me.mProvidence = value
        End Set
    End Property

    Public Property Department() As String
        Get
            Return Me.mDepartment
        End Get
        Set(ByVal value As String)
            Me.mDepartment = value
        End Set
    End Property

    Public Property ResposibleForNotes() As String
        Get
            Return Me.mResposibleForNotes
        End Get
        Set(ByVal value As String)
            Me.mResposibleForNotes = value
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

    Public Sub AddToDos(ByVal todo As AdToDoData)
        Me.mToDos.Add(todo)
    End Sub

    Public Property ToDos() As AdToDoData()
        Get
            Return Me.mToDos.ToArray
        End Get
        Set(ByVal value As AdToDoData())
            Me.mToDos = New Generic.List(Of AdToDoData)(value)
        End Set
    End Property

    Public Sub AddNotes(ByVal note As AdNoteData)
        Me.mNotes.Add(note)
    End Sub

    Public Property Notes() As AdNoteData()
        Get
            Return Me.mNotes.ToArray
        End Get
        Set(ByVal value As AdNoteData())
            Me.mNotes = New Generic.List(Of AdNoteData)(value)
        End Set
    End Property

End Class
