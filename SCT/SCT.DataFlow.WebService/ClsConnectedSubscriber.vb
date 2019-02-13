Option Strict On

Imports Microsoft.VisualBasic
Imports SCT.Library.Subscriber

Public Class ClsConnectedSubscriber

    Private mID As Long
    Private mHostIP As String = ""
    Private mLastShownProjectID As Long
    Private mConnectionHistoryID As Long

    Public ReadOnly Property ID() As Long
        Get
            Return Me.mID
        End Get
    End Property

    Public Property HostIP() As String
        Get
            Return Me.mHostIP
        End Get
        Set(ByVal value As String)
            If Me.mHostIP <> value Then
                Me.mHostIP = value
                Me.mConnectionHistoryID = Me.AddConnectionHistory(value)
            End If
        End Set
    End Property

    Public ReadOnly Property ConnectionHistoryID() As Long
        Get
            Return Me.mConnectionHistoryID
        End Get
    End Property

    Public ReadOnly Property LastShownProjectID() As Long
        Get
            Return Me.mLastShownProjectID
        End Get
    End Property

    Public Function GetStolenReport() As SCT.Library.Subscriber.ClsStolenReport
        Dim StolenReportList As SCT.Library.Subscriber.ClsStolenReportList = SCT.Library.Subscriber.ClsStolenReportList.GetStolenReportList(Me.mID)

        For Each stolenReport As SCT.Library.Subscriber.ClsStolenReport In StolenReportList
            If stolenReport.DateReportFound < stolenReport.DateReportMissing Then
                Return stolenReport
            End If
        Next
        Return Nothing
    End Function

    Public Function GetProject() As SCT.Library.Advertiser.ClsProject
        Dim projectList As SCT.Library.Advertiser.ClsProjectList = SCT.Library.Advertiser.ClsProjectList.GetProjectList(Me.mID)

        If projectList.Count > 0 Then
            For Each project As SCT.Library.Advertiser.ClsProject In projectList
                If project.ID > Me.mLastShownProjectID Then
                    Me.mLastShownProjectID = project.ID
                    Return project
                End If
            Next
            Me.mLastShownProjectID = projectList.Item(0).ID
            Return projectList.Item(0)
        End If
        Return Nothing
    End Function

    Public Shared Function NewConnectedSubscriber() As ClsConnectedSubscriber
        Return New ClsConnectedSubscriber()
    End Function

    Public Shared Function NewConnectedSubscriber(ByVal subscriberId As Long, ByVal hostIP As String) As ClsConnectedSubscriber
        Return New ClsConnectedSubscriber(subscriberId, hostIP)
    End Function

    Private Sub New()
        ' require use of factory methods
    End Sub

    Private Sub New(ByVal subscriberID As Long, ByVal hostIP As String)
        Me.mID = subscriberID
        Me.mHostIP = hostIP
        Me.mLastShownProjectID = 0
        Me.mConnectionHistoryID = Me.AddConnectionHistory(hostIP)
    End Sub

    Private Function AddConnectionHistory(ByVal hostIP As String) As Long
        Dim ConnectionHistory As SCT.Library.Subscriber.ClsConnectionHistory = SCT.Library.Subscriber.ClsConnectionHistory.NewConnectionHistory()
        ConnectionHistory.AssignSubscriberAccount(Me.mID)
        ConnectionHistory.HostIP = hostIP
        ConnectionHistory.ConnectionDate = Date.Today
        Return ConnectionHistory.Save().ID
    End Function

End Class