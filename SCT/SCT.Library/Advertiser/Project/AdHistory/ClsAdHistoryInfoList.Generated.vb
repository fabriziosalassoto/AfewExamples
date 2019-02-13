Imports Csla
Imports SCT.DataAccess

Namespace Advertiser.Project
    <Serializable()> Public Class ClsAdHistoryInfoList
        Inherits ReadOnlyListBase(Of ClsAdHistoryInfoList, ClsAdHistoryInfo)

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole(" ")
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function NewAdHistoryInfoList() As ClsAdHistoryInfoList
            Return New ClsAdHistoryInfoList
        End Function

        Public Shared Function GetAdHistoryInfoList() As ClsAdHistoryInfoList
            Return DataPortal.Fetch(Of ClsAdHistoryInfoList)(New Criteria)
        End Function

        Public Shared Function GetAdHistoryInfoList(ByVal idProject As Long, ByVal idSubscriber As Long, ByVal fromDate As Date, ByVal toDate As Date, ByVal fromTime As Date, ByVal toTime As Date) As ClsAdHistoryInfoList
            Return DataPortal.Fetch(Of ClsAdHistoryInfoList)(New FilteredCriteria(idProject, idSubscriber, fromDate, toDate, fromTime, toTime))
        End Function

        Public Shared Function GetAdHistoryInfoList(ByVal idProject() As Long, ByVal idSubscriber() As Long, ByVal fromDate As Date, ByVal toDate As Date, ByVal fromTime As Date, ByVal toTime As Date) As ClsAdHistoryInfoList
            Return DataPortal.Fetch(Of ClsAdHistoryInfoList)(New FilteredCriteriaList(idProject, idSubscriber, fromDate, toDate, fromTime, toTime))
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> Private Class Criteria
            ' no criteria - retrieve all records
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

            Private mIDProjects() As Long
            Private mIDSubscribers() As Long
            Private mFromDate As New Date
            Private mToDate As New Date
            Private mFromTime As New Date
            Private mToTime As New Date

            Public ReadOnly Property IDProjects() As Long()
                Get
                    Return Me.mIDProjects
                End Get
            End Property

            Public ReadOnly Property IDSubscribers() As Long()
                Get
                    Return Me.mIDSubscribers
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

            Public Sub New(ByVal idProjects() As Long, ByVal idSubscribers() As Long, ByVal fromDate As Date, ByVal toDate As Date, ByVal fromTime As Date, ByVal toTime As Date)
                Me.mIDProjects = idProjects
                Me.mIDSubscribers = idSubscribers
                Me.mFromDate = fromDate
                Me.mToDate = toDate
                Me.mFromTime = fromTime
                Me.mToTime = toTime
            End Sub

        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            ' fetch with no filter
            Fetch()
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteria)
            Fetch(criteria.IDProject, criteria.IDSubscriber, criteria.FromDate, criteria.ToDate, criteria.FromTime, criteria.ToTime)
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteriaList)
            Fetch(criteria.IDProjects, criteria.IDSubscribers, criteria.FromDate, criteria.ToDate, criteria.FromTime, criteria.ToTime)
        End Sub

        Private Sub Fetch()
            Fetch(DAClsappAdvertiserAdHistory.Fetch())
        End Sub

        Private Sub Fetch(ByVal idProject As Long, ByVal idSubscriber As Long, ByVal fromDate As Date, ByVal toDate As Date, ByVal fromTime As Date, ByVal toTime As Date)
            Fetch(DAClsappAdvertiserAdHistory.Fetch(idProject, idSubscriber, fromDate, toDate, fromTime, toTime))
        End Sub

        Private Sub Fetch(ByVal idProjects() As Long, ByVal idSubscribers() As Long, ByVal fromDate As Date, ByVal toDate As Date, ByVal fromTime As Date, ByVal toTime As Date)
            Fetch(DAClsappAdvertiserAdHistory.Fetch(idProjects, idSubscribers, fromDate, toDate, fromTime, toTime))
        End Sub

        Private Sub Fetch(ByVal list As DAClsappAdvertiserAdHistory.Struct())
            RaiseListChangedEvents = False
            IsReadOnly = False
            For Each Struct As DAClsappAdvertiserAdHistory.Struct In list
                Me.Add(ClsAdHistoryInfo.GetAdHistoryInfo(Struct))
            Next
            IsReadOnly = True
            RaiseListChangedEvents = True
        End Sub

#End Region

    End Class
End Namespace

