Option Strict On

<Serializable()> Public Class AdNoteData

    Private mID As Long
    Private mContact As New AdContactData
    Private mDateEntered As New Date(1900, 1, 1)
    Private mDescription As String = String.Empty

    Public Property ID() As Long
        Get
            Return Me.mID
        End Get
        Set(ByVal value As Long)
            Me.mID = value
        End Set
    End Property

    Public Property Contact() As AdContactData
        Get
            Return Me.mContact
        End Get
        Set(ByVal value As AdContactData)
            Me.mContact = value
        End Set
    End Property

    Public Property DateEntered() As Date
        Get
            Return Me.mDateEntered
        End Get
        Set(ByVal value As DateTime)
            Me.mDateEntered = value
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

End Class
