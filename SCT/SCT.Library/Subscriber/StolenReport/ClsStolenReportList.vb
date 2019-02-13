Imports Csla
Imports SCT.DataAccess

Namespace Subscriber
    <Serializable()> Public Class ClsStolenReportList
        Inherits BusinessListBase(Of ClsStolenReportList, ClsStolenReport)

#Region " Business Methods "

        Public Function GetItem(ByVal stolenReportId As Long) As ClsStolenReport
            For Each stolenReport As ClsStolenReport In Me
                If stolenReport.ID = stolenReportId Then
                    Return stolenReport
                End If
            Next
            Return Nothing
        End Function

        Public Function GetActiveStolenReport() As ClsStolenReport
            For Each stolenReport As ClsStolenReport In Me
                If stolenReport.ActiveForAlerts Then
                    Return stolenReport
                End If
            Next
            Return Nothing
        End Function

        Public Overloads Sub Remove(ByVal stolenReportId As Long)
            For Each stolenReport As ClsStolenReport In Me
                If stolenReport.ID = stolenReportId Then
                    Remove(stolenReport)
                    Exit For
                End If
            Next
        End Sub

        Public Overloads Function Contains(ByVal stolenReportId As Long) As Boolean
            For Each stolenReport As ClsStolenReport In Me
                If stolenReport.ID = stolenReportId Then
                    Return True
                End If
            Next
            Return False
        End Function

        Public Sub AddItem(ByVal stolenReport As ClsStolenReport)
            Add(ClsStolenReport.NewSubscriberStolenReport(stolenReport))
        End Sub

#End Region

#Region " Authorization Rules "

        Public Shared Function CanAddObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole("")
        End Function

        Public Shared Function CanGetObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole("")
        End Function

        Public Shared Function CanEditObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole("")
        End Function

        Public Shared Function CanDeleteObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole("")
        End Function

#End Region

#Region " Factory Methods "

#Region " Root Methods "

        Public Shared Function NewStolenReportList() As ClsStolenReportList
            Return DataPortal.Create(Of ClsStolenReportList)(New Criteria())
        End Function

        Public Shared Function GetStolenReportList() As ClsStolenReportList
            Return DataPortal.Fetch(Of ClsStolenReportList)(New Criteria())
        End Function

        Public Shared Function GetStolenReportList(ByVal subscriberID As Long, ByVal fromDate As Date, ByVal toDate As Date) As ClsStolenReportList
            Return DataPortal.Fetch(Of ClsStolenReportList)(New FilteredCriteria(subscriberID, fromDate, toDate))
        End Function

        Public Shared Function GetStolenReportList(ByVal subscriberID() As Long, ByVal fromDate As Date, ByVal toDate As Date) As ClsStolenReportList
            Return DataPortal.Fetch(Of ClsStolenReportList)(New FilteredCriteriaList(subscriberID, fromDate, toDate))
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Child Methods "

        Friend Shared Function NewSubscriberStolenReports() As ClsStolenReportList
            Dim ChildList As New ClsStolenReportList
            ChildList.MarkAsChild()
            Return ChildList
        End Function

        Friend Shared Function GetSubscriberStolenReports(ByVal list As DAClsappSubscriberStolenReports.Struct()) As ClsStolenReportList
            Return New ClsStolenReportList(list)
        End Function

        Private Sub New(ByVal list As DAClsappSubscriberStolenReports.Struct())
            MarkAsChild()
            Fetch(list)
        End Sub

#End Region

#End Region

#Region " Data Access "

#Region " Root Area "

        <Serializable()> Private Class Criteria
            ' no criteria - retrieve all projects
        End Class

        <Serializable()> Private Class FilteredCriteria

            Private mIDSubscriber As Long
            Private mFromDate As New Date
            Private mToDate As New Date

            Public ReadOnly Property IDSubscriber() As Long
                Get
                    Return Me.mIDSubscriber
                End Get
            End Property

            Public ReadOnly Property FormDate() As Date
                Get
                    Return Me.mFromDate
                End Get
            End Property

            Public ReadOnly Property ToDate() As Date
                Get
                    Return Me.mToDate
                End Get
            End Property

            Public Sub New(ByVal subscriberId As Long, ByVal fromDate As Date, ByVal toDate As Date)
                Me.mIDSubscriber = subscriberId
                Me.mFromDate = FormDate
                Me.mToDate = toDate
            End Sub

        End Class

        <Serializable()> Private Class FilteredCriteriaList

            Private mIDSubscriber() As Long
            Private mFromDate As New Date
            Private mToDate As New Date

            Public ReadOnly Property IDSubscriber() As Long()
                Get
                    Return Me.mIDSubscriber
                End Get
            End Property

            Public ReadOnly Property FormDate() As Date
                Get
                    Return Me.mFromDate
                End Get
            End Property

            Public ReadOnly Property ToDate() As Date
                Get
                    Return Me.mToDate
                End Get
            End Property

            Public Sub New(ByVal subscriberId() As Long, ByVal fromDate As Date, ByVal toDate As Date)
                Me.mIDSubscriber = subscriberId
                Me.mFromDate = FormDate
                Me.mToDate = toDate
            End Sub

        End Class

        <RunLocal()> Private Overloads Sub DataPortal_Create(ByVal criteria As Criteria)
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            ' fetch with no filter
            Fetch()
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteria)
            Fetch(criteria.IDSubscriber, criteria.FormDate, criteria.ToDate)
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteriaList)
            Fetch(criteria.IDSubscriber, criteria.FormDate, criteria.ToDate)
        End Sub

        Protected Overrides Sub DataPortal_Update()
            RaiseListChangedEvents = False
            For Each item As ClsStolenReport In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsStolenReport In Me
                If item.IsNew Then
                    item.Insert(item.SubscriberAccount)
                Else
                    item.Update(item.SubscriberAccount)
                End If
            Next
            RaiseListChangedEvents = True
        End Sub

#End Region

#Region " Child Area "

        Friend Sub Update(ByVal parent As Object)
            RaiseListChangedEvents = False
            For Each item As ClsStolenReport In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsStolenReport In Me
                If item.IsNew Then
                    item.Insert(parent)
                Else
                    item.Update(parent)
                End If
            Next
            RaiseListChangedEvents = True
        End Sub

#End Region

#Region " Common Area "

        Private Sub Fetch()
            Fetch(DAClsappSubscriberStolenReports.Fetch())
        End Sub

        Private Sub Fetch(ByVal subscriberId As Long, ByVal fromDate As Date, ByVal toDate As Date)
            Fetch(DAClsappSubscriberStolenReports.Fetch(subscriberId, fromDate, toDate))
        End Sub

        Private Sub Fetch(ByVal subscriberId() As Long, ByVal fromDate As Date, ByVal toDate As Date)
            Fetch(DAClsappSubscriberStolenReports.Fetch(subscriberId, fromDate, toDate))
        End Sub

        Private Sub Fetch(ByVal list As DAClsappSubscriberStolenReports.Struct())
            RaiseListChangedEvents = False
            For Each struct As DAClsappSubscriberStolenReports.Struct In list
                Add(ClsStolenReport.GetSubscriberStolenReport(struct))
            Next
            RaiseListChangedEvents = True
        End Sub

#End Region

#End Region

    End Class
End Namespace