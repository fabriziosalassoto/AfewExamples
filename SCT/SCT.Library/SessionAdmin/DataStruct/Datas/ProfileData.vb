Option Strict On

<Serializable()> Public Class ProfileData

    Private mID As Long
    Private mDescription As String = String.Empty
    Private mForms As New Generic.List(Of ProfileFormData)

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

    Public Sub AddForm(ByVal form As ProfileFormData)
        Me.mForms.Add(form)
    End Sub

    Public Property Forms() As ProfileFormData()
        Get
            Return Me.mForms.ToArray
        End Get
        Set(ByVal value As ProfileFormData())
            Me.mForms = New Generic.List(Of ProfileFormData)(value)
        End Set
    End Property

End Class
