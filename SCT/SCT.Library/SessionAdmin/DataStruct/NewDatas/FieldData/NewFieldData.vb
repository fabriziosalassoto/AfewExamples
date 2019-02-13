Public Class NewFieldData(Of T)

    Public Name As String = String.Empty
    Public IsPrimaryKey As Boolean
    Public OldValue As T
    Public NewValue As T

    Public Sub New()

    End Sub

    Public Sub New(ByVal name As String, ByVal isPrimaryKey As Boolean)
        Me.Name = name
        Me.IsPrimaryKey = isPrimaryKey
    End Sub

    Public Sub New(ByVal name As String, ByVal isPrimaryKey As Boolean, ByVal oldValue As T, ByVal newValue As T)
        Me.Name = name
        Me.IsPrimaryKey = isPrimaryKey
        Me.OldValue = oldValue
        Me.NewValue = newValue
    End Sub

    Public Sub SetValues(ByVal name As String, ByVal isPrimaryKey As Boolean, ByVal oldValue As T, ByVal newValue As T)
        Me.Name = name
        Me.IsPrimaryKey = isPrimaryKey
        Me.OldValue = oldValue
        Me.NewValue = newValue
    End Sub

    Public Sub SetValues(ByVal oldValue As T, ByVal newValue As T)
        Me.OldValue = oldValue
        Me.NewValue = newValue
    End Sub

End Class
