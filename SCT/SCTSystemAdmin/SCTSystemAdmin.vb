Imports System.EnterpriseServices
Imports System.Runtime.InteropServices

<EventTrackingEnabled(True)> <ComVisible(True)> Public Class SCTSystemAdmin
    Inherits ServicedComponent

    Private mPrueba As String = String.Empty

    Public Property Prueba() As String
        Get
            Return Me.mPrueba
        End Get
        Set(ByVal value As String)
            Me.mPrueba = value
        End Set
    End Property

End Class
