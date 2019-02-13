Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsBinnacleUserInfoList
    Inherits ReadOnlyListBase(Of ClsBinnacleUserInfoList, ClsBinnacleUserInfo)

#Region " Authorization Rules "

    Public Shared Function CanGetObject() As Boolean
        Return True 'ApplicationContext.User.IsInRole(" ")
    End Function

#End Region

#Region " Factory Methods "

    Public Shared Function NewBinnacleUserInfoList() As ClsBinnacleUserInfoList
        Return New ClsBinnacleUserInfoList
    End Function

    Public Shared Function GetBinnacleUserInfoList() As ClsBinnacleUserInfoList
        Return DataPortal.Fetch(Of ClsBinnacleUserInfoList)(New Criteria)
    End Function

    Public Shared Function GetBinnacleUserInfoList(ByVal Log As Logs) As ClsBinnacleUserInfoList
        Return DataPortal.Fetch(Of ClsBinnacleUserInfoList)(New FilteredCriteria(Log))
    End Function

    Public Shared Function GetBinnacleUserInfoList(ByVal Logs() As Logs) As ClsBinnacleUserInfoList
        Return DataPortal.Fetch(Of ClsBinnacleUserInfoList)(New FilteredCriteriaList(Logs))
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

        Private mLog As Logs

        Public ReadOnly Property Log() As Logs
            Get
                Return Me.mLog
            End Get
        End Property

        Public Sub New(ByVal log As Logs)
            Me.mLog = log
        End Sub

    End Class

    <Serializable()> Private Class FilteredCriteriaList

        Private mLogs() As Logs

        Public ReadOnly Property Logs() As Logs()
            Get
                Return Me.mLogs
            End Get
        End Property

        Public Sub New(ByVal logs() As Logs)
            Me.mLogs = logs
        End Sub

    End Class

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
        ' fetch with no filter
        Fetch()
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteria)
        Fetch(criteria.Log)
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteriaList)
        Fetch(criteria.Logs)
    End Sub

    Private Sub Fetch()
        Fetch(DAClsprgBinnacleUsers.Fetch())
    End Sub

    Private Sub Fetch(ByVal Log As Logs)
        Fetch(DAClsprgBinnacleUsers.Fetch(Log))
    End Sub

    Private Sub Fetch(ByVal Logs() As Logs)
        Fetch(DAClsprgBinnacleUsers.Fetch(Logs))
    End Sub

    Private Sub Fetch(ByVal list As DAClsprgBinnacleUsers.Struct())
        RaiseListChangedEvents = False
        IsReadOnly = False
        For Each Struct As DAClsprgBinnacleUsers.Struct In list
            Me.Add(ClsBinnacleUserInfo.GetBinnacleUserInfo(Struct))
        Next
        IsReadOnly = True
        RaiseListChangedEvents = True
    End Sub

#End Region

End Class
