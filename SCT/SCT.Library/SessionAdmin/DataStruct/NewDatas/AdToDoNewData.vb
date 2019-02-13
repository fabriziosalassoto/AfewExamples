Option Strict On

<Serializable()> Public Class AdToDoNewData

    Private mID As New NewFieldData(Of Long)
    Private mContact As New AdContactNewData
    Private mDateEntered As New NewFieldData(Of Date)
    Private mDateDue As New NewFieldData(Of Date)
    Private mTaskNotes As New NewFieldData(Of String)
    Private mDateCompleted As New NewFieldData(Of Date)
    Private mCallBackRecord As New NewFieldData(Of Boolean)

    Public Property ID() As NewFieldData(Of Long)
        Get
            Return Me.mID
        End Get
        Set(ByVal value As NewFieldData(Of Long))
            Me.mID = value
        End Set
    End Property

    Public Property Contact() As AdContactNewData
        Get
            Return Me.mContact
        End Get
        Set(ByVal value As AdContactNewData)
            Me.mContact = value
        End Set
    End Property

    Public Property DateEntered() As NewFieldData(Of Date)
        Get
            Return Me.mDateEntered
        End Get
        Set(ByVal value As NewFieldData(Of Date))
            Me.mDateEntered = value
        End Set
    End Property

    Public Property DateDue() As NewFieldData(Of Date)
        Get
            Return Me.mDateDue
        End Get
        Set(ByVal value As NewFieldData(Of Date))
            Me.mDateDue = value
        End Set
    End Property

    Public Property TaskNotes() As NewFieldData(Of String)
        Get
            Return Me.mTaskNotes
        End Get
        Set(ByVal value As NewFieldData(Of String))
            Me.mTaskNotes = value
        End Set
    End Property

    Public Property DateCompleted() As NewFieldData(Of Date)
        Get
            Return Me.mDateCompleted
        End Get
        Set(ByVal value As NewFieldData(Of Date))
            Me.mDateCompleted = value
        End Set
    End Property

    Public Property CallBackRecord() As NewFieldData(Of Boolean)
        Get
            Return Me.mCallBackRecord
        End Get
        Set(ByVal value As NewFieldData(Of Boolean))
            Me.mCallBackRecord = value
        End Set
    End Property

End Class
