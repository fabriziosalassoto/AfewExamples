Option Strict On

<Serializable()> Public Class AdToDoData

    Private mID As Long
    Private mContact As New AdContactData
    Private mDateEntered As New Date(1900, 1, 1)
    Private mDateDue As New Date(1900, 1, 1)
    Private mTaskNotes As String = String.Empty
    Private mDateCompleted As New Date(1900, 1, 1)
    Private mCallBackRecord As Boolean

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

    Public Property DateDue() As Date
        Get
            Return Me.mDateDue
        End Get
        Set(ByVal value As Date)
            Me.mDateDue = value
        End Set
    End Property

    Public Property TaskNotes() As String
        Get
            Return Me.mTaskNotes
        End Get
        Set(ByVal value As String)
            Me.mTaskNotes = value
        End Set
    End Property

    Public Property DateCompleted() As Date
        Get
            Return Me.mDateCompleted
        End Get
        Set(ByVal value As Date)
            Me.mDateCompleted = value
        End Set
    End Property

    Public Property CallBackRecord() As Boolean
        Get
            Return Me.mCallBackRecord
        End Get
        Set(ByVal value As Boolean)
            Me.mCallBackRecord = value
        End Set
    End Property

End Class
