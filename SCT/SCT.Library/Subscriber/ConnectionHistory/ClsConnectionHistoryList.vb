Imports Csla
Imports SCT.DataAccess

Namespace Subscriber
    <Serializable()> Public Class ClsConnectionHistoryList
        Inherits BusinessListBase(Of ClsConnectionHistoryList, ClsConnectionHistory)

#Region " Business Methods "

        Public Function GetItem(ByVal connectionHistoryId As Long) As ClsConnectionHistory
            For Each connectionHistory As ClsConnectionHistory In Me
                If connectionHistory.ID = connectionHistoryId Then
                    Return connectionHistory
                End If
            Next
            Return Nothing
        End Function

        Public Function GetLastItem() As ClsConnectionHistory
            Try
                Return Me.Item(Me.Count - 1)
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Overloads Sub Add(ByVal hostIP As String, ByVal hostLocalIP As String)
            Add(ClsConnectionHistory.NewSubscriberConnectionHistory(hostIP, hostLocalIP))
        End Sub

        Public Overloads Sub Remove(ByVal connectionHistoryId As Long)
            For Each connectionHistory As ClsConnectionHistory In Me
                If connectionHistory.ID = connectionHistoryId Then
                    Remove(connectionHistory)
                    Exit For
                End If
            Next
        End Sub

        Public Overloads Function Contains(ByVal connectionHistoryId As Long) As Boolean
            For Each connectionHistory As ClsConnectionHistory In Me
                If connectionHistory.ID = connectionHistoryId Then
                    Return True
                End If
            Next
            Return False
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

        Public Shared Function NewConnectionHistoryList() As ClsConnectionHistoryList
            Return DataPortal.Create(Of ClsConnectionHistoryList)(New Criteria())
        End Function

        Public Shared Function GetConnectionHistoryList() As ClsConnectionHistoryList
            Return DataPortal.Fetch(Of ClsConnectionHistoryList)(New Criteria())
        End Function

        Public Shared Function GetConnectionHistoryList(ByVal subscriberID As Long, ByVal fromDate As Date, ByVal toDate As Date) As ClsConnectionHistoryList
            Return DataPortal.Fetch(Of ClsConnectionHistoryList)(New FilteredCriteria(subscriberID, fromDate, toDate))
        End Function

        Public Shared Function GetConnectionHistoryList(ByVal subscriberID() As Long, ByVal fromDate As Date, ByVal toDate As Date) As ClsConnectionHistoryList
            Return DataPortal.Fetch(Of ClsConnectionHistoryList)(New FilteredCriteriaList(subscriberID, fromDate, toDate))
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Child Methods "

        Friend Shared Function NewSubscriberConnectionHistories() As ClsConnectionHistoryList
            Dim ChildList As New ClsConnectionHistoryList
            ChildList.MarkAsChild()
            Return ChildList
        End Function

        Friend Shared Function GetSubscriberConnectionHistories(ByVal list As DAClsappSubscriberConnectionHistory.Struct()) As ClsConnectionHistoryList
            Return New ClsConnectionHistoryList(list)
        End Function

        Private Sub New(ByVal list As DAClsappSubscriberConnectionHistory.Struct())
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

            Public Sub New(ByVal subscriberId As Long, ByVal fromDate As Date, ByVal toDate As Date)
                Me.mIDSubscriber = subscriberId
                Me.mFromDate = fromDate
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

            Public Sub New(ByVal subscriberId() As Long, ByVal fromDate As Date, ByVal toDate As Date)
                Me.mIDSubscriber = subscriberId
                Me.mFromDate = fromDate
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
            Fetch(criteria.IDSubscriber, criteria.FromDate, criteria.ToDate)
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteriaList)
            Fetch(criteria.IDSubscriber, criteria.FromDate, criteria.ToDate)
        End Sub

        Protected Overrides Sub DataPortal_Update()
            RaiseListChangedEvents = False
            For Each item As ClsConnectionHistory In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsConnectionHistory In Me
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
            For Each item As ClsConnectionHistory In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsConnectionHistory In Me
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
            Fetch(DAClsappSubscriberConnectionHistory.Fetch())
        End Sub

        Private Sub Fetch(ByVal subscriberId As Long, ByVal fromDate As Date, ByVal toDate As Date)
            Fetch(DAClsappSubscriberConnectionHistory.Fetch(subscriberId, fromDate, toDate))
        End Sub

        Private Sub Fetch(ByVal subscriberId() As Long, ByVal fromDate As Date, ByVal toDate As Date)
            Fetch(DAClsappSubscriberConnectionHistory.Fetch(subscriberId, fromDate, toDate))
        End Sub

        Private Sub Fetch(ByVal list As DAClsappSubscriberConnectionHistory.Struct())
            RaiseListChangedEvents = False
            For Each struct As DAClsappSubscriberConnectionHistory.Struct In list
                Add(ClsConnectionHistory.GetSubscriberConnectionHistory(struct))
            Next
            RaiseListChangedEvents = True
        End Sub

#End Region

#End Region

    End Class
End Namespace