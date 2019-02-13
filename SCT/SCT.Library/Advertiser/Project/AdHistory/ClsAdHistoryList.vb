Imports Csla
Imports SCT.DataAccess

Namespace Advertiser.Project
    <Serializable()> Public Class ClsAdHistoryList
        Inherits BusinessListBase(Of ClsAdHistoryList, ClsAdHistory)

#Region " Business Methods "

        Public Function GetItem(ByVal adHistoryId As Long) As ClsAdHistory
            For Each adHistory As ClsAdHistory In Me
                If adHistory.ID = adHistoryId Then
                    Return adHistory
                End If
            Next
            Return Nothing
        End Function

        Public Function GetLastItem() As ClsAdHistory
            Try
                Return Me.Item(Me.Count - 1)
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Overloads Sub AddItem(ByVal projectId As Long, ByVal subscriberId As Long)
            Add(ClsAdHistory.NewChildAdHistory(projectId, subscriberId))
        End Sub

        Public Overloads Sub AddProjectItem(ByVal subscriberId As Long)
            Add(ClsAdHistory.NewProjectAdHistory(subscriberId))
        End Sub

        Public Overloads Sub AddSubscriberItem(ByVal projectId As Long)
            Add(ClsAdHistory.NewSubscriberAdHistory(projectId))
        End Sub

        Public Overloads Sub Remove(ByVal adHistoryId As Long)
            For Each adHistory As ClsAdHistory In Me
                If adHistory.ID = adHistoryId Then
                    Remove(adHistory)
                    Exit For
                End If
            Next
        End Sub

        Public Overloads Function Contains(ByVal adHistoryId As Long) As Boolean
            For Each adHistory As ClsAdHistory In Me
                If adHistory.ID = adHistoryId Then
                    Return True
                End If
            Next
            Return False
        End Function

        Public Function FindAllAdHistory(ByVal startDate As Date, ByVal endDate As Date) As ClsAdHistoryList
            Dim AdHistoryList As ClsAdHistoryList = ClsAdHistoryList.NewAdHistoryList
            For Each adHistory As ClsAdHistory In Me
                If adHistory.DateAdDisplay.Date >= startDate.Date AndAlso adHistory.DateAdDisplay.Date <= endDate.Date Then
                    AdHistoryList.Add(adHistory)
                End If
            Next
            Return AdHistoryList
        End Function

        Public Function FindAllAdvertiserAdHistory(ByVal advertiserProjectList As ClsProjectList) As ClsAdHistoryList
            Dim adHistoryList As ClsAdHistoryList = ClsAdHistoryList.NewAdHistoryList
            For Each adHistory As ClsAdHistory In Me
                If advertiserProjectList.Contains(adHistory.Project.ID) Then
                    adHistoryList.Add(adHistory)
                End If
            Next
            Return adHistoryList
        End Function

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

        Public Shared Function NewAdHistoryList() As ClsAdHistoryList
            Return DataPortal.Create(Of ClsAdHistoryList)(New Criteria())
        End Function

        Public Shared Function GetAdHistoryList() As ClsAdHistoryList
            Return DataPortal.Fetch(Of ClsAdHistoryList)(New Criteria())
        End Function

        Public Shared Function GetAdHistoryList(ByVal projectID As Long, ByVal subscriberId As Long, ByVal fromDate As Date, ByVal toDate As Date, ByVal fromTime As Date, ByVal toTime As Date) As ClsAdHistoryList
            Return DataPortal.Fetch(Of ClsAdHistoryList)(New FilteredCriteria(projectID, subscriberId, fromDate, toDate, fromTime, toTime))
        End Function

        Public Shared Function GetAdHistoryList(ByVal projectID() As Long, ByVal subscriberId() As Long, ByVal fromDate As Date, ByVal toDate As Date, ByVal fromTime As Date, ByVal toTime As Date) As ClsAdHistoryList
            Return DataPortal.Fetch(Of ClsAdHistoryList)(New FilteredCriteriaList(projectID, subscriberId, fromDate, toDate, fromTime, toTime))
        End Function

#End Region

#Region " Child Methods "

        Friend Shared Function NewProjectAdHistories() As ClsAdHistoryList
            Return New ClsAdHistoryList
        End Function

        Friend Shared Function GetProjectAdHistories(ByVal list As DAClsappAdvertiserAdHistory.Struct()) As ClsAdHistoryList
            Return New ClsAdHistoryList(list)
        End Function

        Friend Shared Function NewSubscriberAdHistories() As ClsAdHistoryList
            Return New ClsAdHistoryList
        End Function

        Friend Shared Function GetSubscriberAdHistories(ByVal list As DAClsappAdvertiserAdHistory.Struct()) As ClsAdHistoryList
            Return New ClsAdHistoryList(list)
        End Function

        Private Sub New(ByVal list As DAClsappAdvertiserAdHistory.Struct())
            MarkAsChild()
            Fetch(list)
        End Sub

        Private Sub New()
            MarkAsChild()
        End Sub

#End Region

#End Region

#Region " Data Access "

#Region " Root Area "

        <Serializable()> Private Class Criteria
            ' no criteria - retrieve all projects
        End Class

        <Serializable()> Private Class FilteredCriteria

            Private mIDProject As Long
            Private mIDSubscriber As Long
            Private mFromDate As New Date
            Private mToDate As New Date
            Private mFromTime As New Date
            Private mToTime As New Date

            Public ReadOnly Property IDProject() As Long
                Get
                    Return Me.mIDProject
                End Get
            End Property

            Public ReadOnly Property IDSubscriber() As Long
                Get
                    Return Me.mIDSubscriber
                End Get
            End Property

            Public ReadOnly Property FromDate() As Date
                Get
                    Return Me.mFromDate
                End Get
            End Property

            Public ReadOnly Property ToDate() As Date
                Get
                    Return Me.mToDate
                End Get
            End Property

            Public ReadOnly Property FromTime() As Date
                Get
                    Return Me.mFromTime
                End Get
            End Property

            Public ReadOnly Property ToTime() As Date
                Get
                    Return Me.mToTime
                End Get
            End Property

            Public Sub New(ByVal idProject As Long, ByVal idSubscriber As Long, ByVal fromDate As Date, ByVal toDate As Date, ByVal fromTime As Date, ByVal toTime As Date)
                Me.mIDProject = idProject
                Me.mIDSubscriber = idSubscriber
                Me.mFromDate = fromDate
                Me.mToDate = toDate
                Me.mFromTime = fromTime
                Me.mToTime = toTime
            End Sub

        End Class

        <Serializable()> Private Class FilteredCriteriaList

            Private mIDProject() As Long
            Private mIDSubscriber() As Long
            Private mFromDate As New Date
            Private mToDate As New Date
            Private mFromTime As New Date
            Private mToTime As New Date

            Public ReadOnly Property IDProject() As Long()
                Get
                    Return Me.mIDProject
                End Get
            End Property

            Public ReadOnly Property IDSubscriber() As Long()
                Get
                    Return Me.mIDSubscriber
                End Get
            End Property

            Public ReadOnly Property FromDate() As Date
                Get
                    Return Me.mFromDate
                End Get
            End Property

            Public ReadOnly Property ToDate() As Date
                Get
                    Return Me.mToDate
                End Get
            End Property

            Public ReadOnly Property FromTime() As Date
                Get
                    Return Me.mFromTime
                End Get
            End Property

            Public ReadOnly Property ToTime() As Date
                Get
                    Return Me.mToTime
                End Get
            End Property

            Public Sub New(ByVal idProject() As Long, ByVal idSubscriber() As Long, ByVal fromDate As Date, ByVal toDate As Date, ByVal fromTime As Date, ByVal toTime As Date)
                Me.mIDProject = idProject
                Me.mIDSubscriber = idSubscriber
                Me.mFromDate = fromDate
                Me.mToDate = toDate
                Me.mFromTime = fromTime
                Me.mToTime = toTime
            End Sub

        End Class

        <RunLocal()> Private Overloads Sub DataPortal_Create(ByVal criteria As Criteria)
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            ' fetch with no filter
            Fetch()
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteria)
            Fetch(criteria.IDProject, criteria.IDSubscriber, criteria.FromDate, criteria.ToDate, criteria.FromTime, criteria.ToTime)
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteriaList)
            Fetch(criteria.IDProject, criteria.IDSubscriber, criteria.FromDate, criteria.ToDate, criteria.FromTime, criteria.ToTime)
        End Sub

        Protected Overrides Sub DataPortal_Update()
            RaiseListChangedEvents = False
            For Each item As ClsAdHistory In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsAdHistory In Me
                If item.IsNew Then
                    item.Insert(New Object() {item.Project, item.SubscriberAccount})
                Else
                    item.Update(New Object() {item.Project, item.SubscriberAccount})
                End If
            Next
            RaiseListChangedEvents = True
        End Sub

#End Region

#Region " Child Area "

        Friend Sub Update(ByVal parent As Advertiser.ClsProject)
            RaiseListChangedEvents = False
            For Each item As ClsAdHistory In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsAdHistory In Me
                If item.IsNew Then
                    item.Insert(New Object() {parent, item.SubscriberAccount})
                Else
                    item.Update(New Object() {parent, item.SubscriberAccount})
                End If
            Next
            RaiseListChangedEvents = True
        End Sub

        Friend Sub Update(ByVal parent As Subscriber.ClsAccount)
            RaiseListChangedEvents = False
            For Each item As ClsAdHistory In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsAdHistory In Me
                If item.IsNew Then
                    item.Insert(New Object() {item.Project, parent})
                Else
                    item.Update(New Object() {item.Project, parent})
                End If
            Next
            RaiseListChangedEvents = True
        End Sub

#End Region

#Region " Common Area "

        Private Sub Fetch()
            Fetch(DAClsappAdvertiserAdHistory.Fetch())
        End Sub

        Private Sub Fetch(ByVal projectId As Long, ByVal subscriberId As Long, ByVal fromDate As Date, ByVal toDate As Date, ByVal fromTime As Date, ByVal toTime As Date)
            Fetch(DAClsappAdvertiserAdHistory.Fetch(projectId, subscriberId, fromDate, toDate, fromTime, toTime))
        End Sub

        Private Sub Fetch(ByVal projectId() As Long, ByVal subscriberId() As Long, ByVal fromDate As Date, ByVal toDate As Date, ByVal fromTime As Date, ByVal toTime As Date)
            Fetch(DAClsappAdvertiserAdHistory.Fetch(projectId, subscriberId, fromDate, toDate, fromTime, toTime))
        End Sub

        Private Sub Fetch(ByVal list As DAClsappAdvertiserAdHistory.Struct())
            RaiseListChangedEvents = False
            For Each Struct As DAClsappAdvertiserAdHistory.Struct In list
                Add(ClsAdHistory.GetChildAdHistory(Struct))
            Next
            RaiseListChangedEvents = True
        End Sub

#End Region

#End Region

    End Class
End Namespace