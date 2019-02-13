Public Class ClsConnectedSubscribersList
    Inherits List(Of ClsConnectedSubscriber)

#Region " Business Methods "

    Public Function GetItem(ByVal subscriberId As Long) As ClsConnectedSubscriber
        For Each connectedSubscriber As ClsConnectedSubscriber In Me
            If connectedSubscriber.ID = subscriberId Then
                Return connectedSubscriber
            End If
        Next
        Throw New InvalidOperationException("Subscriber not is connected")
    End Function

    Public Overloads Sub Add(ByVal subscriberId As Long, ByVal hostIP As String)
        If Not Contains(subscriberId) Then
            Me.Add(ClsConnectedSubscriber.NewConnectedSubscriber(subscriberId, hostIP))
        Else
            Throw New InvalidOperationException("Subscriber already is connected")
        End If
    End Sub

    Public Overloads Sub Remove(ByVal subscriberId As Long)
        For Each connectedSubscriber As ClsConnectedSubscriber In Me
            If connectedSubscriber.ID = subscriberId Then
                Remove(connectedSubscriber)
                Exit For
            End If
        Next
    End Sub

    Public Overloads Function Contains(ByVal subscriberId As Long) As Boolean
        For Each connectedSubscriber As ClsConnectedSubscriber In Me
            If connectedSubscriber.ID = subscriberId Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Shared Function NewConnectedSubscribersList() As ClsConnectedSubscribersList
        Return New ClsConnectedSubscribersList
    End Function

    Private Sub New()
        ' require use of factory methods
    End Sub

#End Region

End Class
