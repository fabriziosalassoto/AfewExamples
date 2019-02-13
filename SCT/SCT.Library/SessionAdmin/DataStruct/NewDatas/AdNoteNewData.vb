Option Strict On

<Serializable()> Public Class AdNoteNewData

    Private mID As New NewFieldData(Of Long)
    Private mContact As New AdContactNewData
    Private mDateEntered As New NewFieldData(Of Date)
    Private mDescription As New NewFieldData(Of String)

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

    Public Property Description() As NewFieldData(Of String)
        Get
            Return Me.mDescription
        End Get
        Set(ByVal value As NewFieldData(Of String))
            Me.mDescription = value
        End Set
    End Property

End Class
