Option Strict On

<Serializable()> Public Class AdAccountNewData

    Private mID As New NewFieldData(Of Long)
    Private mCompanyName As New NewFieldData(Of String)
    Private mCompanyNotes As New NewFieldData(Of String)
    Private mLogin As New NewFieldData(Of String)
    Private mClientPassword As New NewFieldData(Of String)
    Private mWebPassword As New NewFieldData(Of String)
    Private mHintByPassOne As New NewFieldData(Of String)
    Private mHintByPassTwo As New NewFieldData(Of String)

    Public Property ID() As NewFieldData(Of Long)
        Get
            Return Me.mID
        End Get
        Set(ByVal value As NewFieldData(Of Long))
            Me.mID = value
        End Set
    End Property

    Public Property CompanyName() As NewFieldData(Of String)
        Get
            Return Me.mCompanyName
        End Get
        Set(ByVal value As NewFieldData(Of String))
            Me.mCompanyName = value
        End Set
    End Property

    Public Property CompanyNotes() As NewFieldData(Of String)
        Get
            Return Me.mCompanyNotes
        End Get
        Set(ByVal value As NewFieldData(Of String))
            Me.mCompanyNotes = value
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

    Public Property ClientPassword() As NewFieldData(Of String)
        Get
            Return Me.mClientPassword
        End Get
        Set(ByVal value As NewFieldData(Of String))
            Me.mClientPassword = value
        End Set
    End Property

    Public Property WebPassword() As NewFieldData(Of String)
        Get
            Return Me.mWebPassword
        End Get
        Set(ByVal value As NewFieldData(Of String))
            Me.mWebPassword = value
        End Set
    End Property

    Public Property HintByPassOne() As NewFieldData(Of String)
        Get
            Return Me.mHintByPassOne
        End Get
        Set(ByVal value As NewFieldData(Of String))
            Me.mHintByPassOne = value
        End Set
    End Property

    Public Property HintByPassTwo() As NewFieldData(Of String)
        Get
            Return Me.mHintByPassTwo
        End Get
        Set(ByVal value As NewFieldData(Of String))
            Me.mHintByPassTwo = value
        End Set
    End Property

End Class
