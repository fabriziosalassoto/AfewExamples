Imports SCT.Library

Public Class DFClsChangePassword

#Region " Private Fields "
    Private mForm As Object
#End Region

#Region " Properties And Methods "

    Public WriteOnly Property Form() As Object
        Set(ByVal value As Object)
            Me.mForm = value
        End Set
    End Property

    Public Sub New(ByRef form As Object)
        Me.mForm = form
        Me.mForm.Session("ValuePath") = "frmMain"
    End Sub

#End Region

End Class
