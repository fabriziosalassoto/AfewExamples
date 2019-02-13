Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsBinnacleFormEntryList
    Inherits ReadOnlyListBase(Of ClsBinnacleFormEntryList, ClsBinnacleFormEntry)

#Region " Authorization Rules "

    Public Shared Function CanGetObject() As Boolean
        Return True 'ApplicationContext.User.IsInRole(" ")
    End Function

#End Region

#Region " Factory Methods "

    Public Shared Function NewBinnacleFormEntryList() As ClsBinnacleFormEntryList
        Return New ClsBinnacleFormEntryList
    End Function

    Public Shared Function GetBinnacleFormEntryList() As ClsBinnacleFormEntryList
        Return DataPortal.Fetch(Of ClsBinnacleFormEntryList)(New Criteria)
    End Function

    Public Shared Function GetBinnacleFormEntryList(ByVal log As Logs) As ClsBinnacleFormEntryList
        Return DataPortal.Fetch(Of ClsBinnacleFormEntryList)(New LogCriteria(log))
    End Function

    Public Shared Function GetBinnacleFormEntryList(ByVal log() As Logs) As ClsBinnacleFormEntryList
        Return DataPortal.Fetch(Of ClsBinnacleFormEntryList)(New LogCriteriaList(log))
    End Function

    Public Shared Function GetBinnacleFormEntryList(ByVal log As Logs, ByVal idUser As SearchCriteria(Of Long), ByVal idForm As SearchCriteria(Of Long), ByVal idOperation As SearchCriteria(Of Long), ByVal fromDate As SearchCriteria(Of Date), ByVal toDate As SearchCriteria(Of Date), ByVal fromHour As SearchCriteria(Of Date), ByVal toHour As SearchCriteria(Of Date)) As ClsBinnacleFormEntryList
        Return DataPortal.Fetch(Of ClsBinnacleFormEntryList)(New FilteredCriteria(idUser, idForm, idOperation, fromDate, toDate, fromHour, toHour, log))
    End Function

    Public Shared Function GetBinnacleFormEntryList(ByVal log() As Logs, ByVal idUser As SearchCriteriaList(Of Long), ByVal idForm As SearchCriteriaList(Of Long), ByVal idOperation As SearchCriteriaList(Of Long), ByVal fromDate As SearchCriteria(Of Date), ByVal toDate As SearchCriteria(Of Date), ByVal fromHour As SearchCriteria(Of Date), ByVal toHour As SearchCriteria(Of Date)) As ClsBinnacleFormEntryList
        Return DataPortal.Fetch(Of ClsBinnacleFormEntryList)(New FilteredCriteriaList(idUser, idForm, idOperation, fromDate, toDate, fromHour, toHour, log))
    End Function

    Public Shared Function GetBinnacleFormEntryList(ByVal list As DAClsprgBinnacleFormEntry.Struct()) As ClsBinnacleFormEntryList
        Return DataPortal.Fetch(Of ClsBinnacleFormEntryList)(List)
    End Function

    Private Sub New()
        ' Require use of factory methods
    End Sub

#End Region

#Region " Data Access "

    <Serializable()> Private Class Criteria
        ' no criteria - retrieve all records
    End Class

    <Serializable()> Private Class LogCriteria

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

    <Serializable()> Private Class LogCriteriaList

        Private mLog() As Logs

        Public ReadOnly Property Log() As Logs()
            Get
                Return Me.mLog
            End Get
        End Property

        Public Sub New(ByVal log() As Logs)
            Me.mLog = log
        End Sub

    End Class

    <Serializable()> Private Class FilteredCriteria

        Private mIDUser As SearchCriteria(Of Long)
        Private mIDForm As SearchCriteria(Of Long)
        Private mIDOperation As SearchCriteria(Of Long)
        Private mFromDate As SearchCriteria(Of Date)
        Private mToDate As SearchCriteria(Of Date)
        Private mFromHour As SearchCriteria(Of Date)
        Private mToHour As SearchCriteria(Of Date)
        Private mLog As Logs

        Public ReadOnly Property IDUser() As SearchCriteria(Of Long)
            Get
                Return Me.mIDUser
            End Get
        End Property

        Public ReadOnly Property IDForm() As SearchCriteria(Of Long)
            Get
                Return Me.mIDForm
            End Get
        End Property

        Public ReadOnly Property IDOperation() As SearchCriteria(Of Long)
            Get
                Return Me.mIDOperation
            End Get
        End Property

        Public ReadOnly Property FromDate() As SearchCriteria(Of Date)
            Get
                Return Me.mFromDate
            End Get
        End Property

        Public ReadOnly Property ToDate() As SearchCriteria(Of Date)
            Get
                Return Me.mToDate
            End Get
        End Property

        Public ReadOnly Property FromHour() As SearchCriteria(Of Date)
            Get
                Return Me.mFromHour
            End Get
        End Property

        Public ReadOnly Property ToHour() As SearchCriteria(Of Date)
            Get
                Return Me.mToHour
            End Get
        End Property

        Public ReadOnly Property Log() As Logs
            Get
                Return Me.mLog
            End Get
        End Property

        Public Sub New(ByVal idUser As SearchCriteria(Of Long), ByVal idForm As SearchCriteria(Of Long), ByVal idOperation As SearchCriteria(Of Long), ByVal fromDate As SearchCriteria(Of Date), ByVal toDate As SearchCriteria(Of Date), ByVal fromHour As SearchCriteria(Of Date), ByVal toHour As SearchCriteria(Of Date), ByVal log As Logs)
            Me.mIDUser = idUser
            Me.mIDForm = idForm
            Me.mIDOperation = idOperation
            Me.mFromDate = fromDate
            Me.mToDate = toDate
            Me.mFromHour = fromHour
            Me.mToHour = toHour
            Me.mLog = log
        End Sub

    End Class

    <Serializable()> Private Class FilteredCriteriaList

        Private mIDUser As SearchCriteriaList(Of Long)
        Private mIDForm As SearchCriteriaList(Of Long)
        Private mIDOperation As SearchCriteriaList(Of Long)
        Private mFromDate As SearchCriteria(Of Date)
        Private mToDate As SearchCriteria(Of Date)
        Private mFromHour As SearchCriteria(Of Date)
        Private mToHour As SearchCriteria(Of Date)
        Private mLog() As Logs

        Public ReadOnly Property IDUser() As SearchCriteriaList(Of Long)
            Get
                Return Me.mIDUser
            End Get
        End Property

        Public ReadOnly Property IDForm() As SearchCriteriaList(Of Long)
            Get
                Return Me.mIDForm
            End Get
        End Property

        Public ReadOnly Property IDOperation() As SearchCriteriaList(Of Long)
            Get
                Return Me.mIDOperation
            End Get
        End Property

        Public ReadOnly Property FromDate() As SearchCriteria(Of Date)
            Get
                Return Me.mFromDate
            End Get
        End Property

        Public ReadOnly Property ToDate() As SearchCriteria(Of Date)
            Get
                Return Me.mToDate
            End Get
        End Property

        Public ReadOnly Property FromHour() As SearchCriteria(Of Date)
            Get
                Return Me.mFromHour
            End Get
        End Property

        Public ReadOnly Property ToHour() As SearchCriteria(Of Date)
            Get
                Return Me.mToHour
            End Get
        End Property

        Public ReadOnly Property Log() As Logs()
            Get
                Return Me.mLog
            End Get
        End Property

        Public Sub New(ByVal idUser As SearchCriteriaList(Of Long), ByVal idForm As SearchCriteriaList(Of Long), ByVal idOperation As SearchCriteriaList(Of Long), ByVal fromDate As SearchCriteria(Of Date), ByVal toDate As SearchCriteria(Of Date), ByVal fromHour As SearchCriteria(Of Date), ByVal toHour As SearchCriteria(Of Date), ByVal log() As Logs)
            Me.mIDUser = idUser
            Me.mIDForm = idForm
            Me.mIDOperation = idOperation
            Me.mFromDate = fromDate
            Me.mToDate = toDate
            Me.mFromHour = fromHour
            Me.mToHour = toHour
            Me.mLog = log
        End Sub

    End Class

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
        ' fetch with no filter
        Fetch()
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As LogCriteria)
        Fetch(criteria.Log)
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As LogCriteriaList)
        Fetch(criteria.Log)
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteria)
        Fetch(criteria.Log, criteria.IDUser, criteria.IDForm, criteria.IDOperation, criteria.FromDate, criteria.ToDate, criteria.FromHour, criteria.ToHour)
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteriaList)
        Fetch(criteria.Log, criteria.IDUser, criteria.IDForm, criteria.IDOperation, criteria.FromDate, criteria.ToDate, criteria.FromHour, criteria.ToHour)
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal list As DAClsprgBinnacleFormEntry.Struct())
        Fetch(list)
    End Sub

    Private Sub Fetch()
        Fetch(DAClsprgBinnacleFormEntry.Fetch())
    End Sub

    Private Sub Fetch(ByVal Log As Logs)
        Fetch(DAClsprgBinnacleFormEntry.Fetch(Log))
    End Sub

    Private Sub Fetch(ByVal Log() As Logs)
        Fetch(DAClsprgBinnacleFormEntry.Fetch(Log))
    End Sub

    Private Sub Fetch(ByVal Log As Logs, ByVal idUser As SearchCriteria(Of Long), ByVal idForm As SearchCriteria(Of Long), ByVal idOperation As SearchCriteria(Of Long), ByVal fromdate As SearchCriteria(Of Date), ByVal todate As SearchCriteria(Of Date), ByVal fromHour As SearchCriteria(Of Date), ByVal toHour As SearchCriteria(Of Date))
        Fetch(DAClsprgBinnacleFormEntry.Fetch(Log, New Parameter(Of Long)(idUser.Value, idUser.Priority), New Parameter(Of Long)(idForm.Value, idForm.Priority), New Parameter(Of Long)(idOperation.Value, idOperation.Priority), New Parameter(Of Date)(fromdate.Value, fromdate.Priority), New Parameter(Of Date)(todate.Value, todate.Priority), New Parameter(Of Date)(fromHour.Value, fromHour.Priority), New Parameter(Of Date)(toHour.Value, toHour.Priority)))
    End Sub

    Private Sub Fetch(ByVal Log() As Logs, ByVal idUser As SearchCriteriaList(Of Long), ByVal idForm As SearchCriteriaList(Of Long), ByVal idOperation As SearchCriteriaList(Of Long), ByVal fromdate As SearchCriteria(Of Date), ByVal todate As SearchCriteria(Of Date), ByVal fromHour As SearchCriteria(Of Date), ByVal toHour As SearchCriteria(Of Date))
        Fetch(DAClsprgBinnacleFormEntry.Fetch(Log, New ParameterList(Of Long)(idUser.Values, idUser.Priority), New ParameterList(Of Long)(idForm.Values, idForm.Priority), New ParameterList(Of Long)(idOperation.Values, idOperation.Priority), New Parameter(Of Date)(fromdate.Value, fromdate.Priority), New Parameter(Of Date)(todate.Value, todate.Priority), New Parameter(Of Date)(fromHour.Value, fromHour.Priority), New Parameter(Of Date)(toHour.Value, toHour.Priority)))
    End Sub

    Private Sub Fetch(ByVal list As DAClsprgBinnacleFormEntry.Struct())

        RaiseListChangedEvents = False
        IsReadOnly = False
        For Each Struct As DAClsprgBinnacleFormEntry.Struct In List
            Me.Add(ClsBinnacleFormEntry.GetBinnacleFormEntry(Struct))
        Next
        IsReadOnly = True
        RaiseListChangedEvents = True
    End Sub

#End Region

End Class