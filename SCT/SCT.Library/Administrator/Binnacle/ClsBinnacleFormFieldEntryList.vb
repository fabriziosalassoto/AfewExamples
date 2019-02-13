Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsBinnacleFormFieldEntryList
    Inherits ReadOnlyListBase(Of ClsBinnacleFormFieldEntryList, ClsBinnacleFormFieldEntry)

#Region " Authorization Rules "

    Public Shared Function CanGetObject() As Boolean
        Return True 'ApplicationContext.User.IsInRole(" ")
    End Function

#End Region

#Region " Factory Methods "

    Public Shared Function NewBinnacleFormFieldEntryList() As ClsBinnacleFormFieldEntryList
        Return New ClsBinnacleFormFieldEntryList
    End Function

    Public Shared Function GetBinnacleFormFieldEntryList() As ClsBinnacleFormFieldEntryList
        Return DataPortal.Fetch(Of ClsBinnacleFormFieldEntryList)(New Criteria)
    End Function

    Public Shared Function GetBinnacleFormFieldEntryList(ByVal log As Logs) As ClsBinnacleFormFieldEntryList
        Return DataPortal.Fetch(Of ClsBinnacleFormFieldEntryList)(New LogCriteria(log))
    End Function

    Public Shared Function GetBinnacleFormFieldEntryList(ByVal log() As Logs) As ClsBinnacleFormFieldEntryList
        Return DataPortal.Fetch(Of ClsBinnacleFormFieldEntryList)(New LogCriteriaList(log))
    End Function

    Public Shared Function GetBinnacleFormFieldEntryList(ByVal log As Logs, ByVal idBinnacleForm As SearchCriteria(Of Long), ByVal idField As SearchCriteria(Of Long)) As ClsBinnacleFormFieldEntryList
        Return DataPortal.Fetch(Of ClsBinnacleFormFieldEntryList)(New FilteredCriteria(idBinnacleForm, idField, log))
    End Function

    Public Shared Function GetBinnacleFormFieldEntryList(ByVal log() As Logs, ByVal idBinnacleForm As SearchCriteriaList(Of Long), ByVal idField As SearchCriteriaList(Of Long)) As ClsBinnacleFormFieldEntryList
        Return DataPortal.Fetch(Of ClsBinnacleFormFieldEntryList)(New FilteredCriteriaList(idBinnacleForm, idField, log))
    End Function

    Public Shared Function GetBinnacleFormFieldEntryList(ByVal list As DAClsprgBinnacleFormFieldEntry.Struct()) As ClsBinnacleFormFieldEntryList
        Return DataPortal.Fetch(Of ClsBinnacleFormFieldEntryList)(list)
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

        Private mIDBinnacleForm As SearchCriteria(Of Long)
        Private mIDField As SearchCriteria(Of Long)
        Private mLog As Logs

        Public ReadOnly Property IDBinnacleForm() As SearchCriteria(Of Long)
            Get
                Return Me.mIDBinnacleForm
            End Get
        End Property

        Public ReadOnly Property IDField() As SearchCriteria(Of Long)
            Get
                Return Me.mIDField
            End Get
        End Property

        Public ReadOnly Property Log() As Logs
            Get
                Return Me.mLog
            End Get
        End Property

        Public Sub New(ByVal idBinnacleForm As SearchCriteria(Of Long), ByVal idField As SearchCriteria(Of Long), ByVal log As Logs)
            Me.mIDBinnacleForm = idBinnacleForm
            Me.mIDField = idField
            Me.mLog = log
        End Sub

    End Class

    <Serializable()> Private Class FilteredCriteriaList

        Private mIDBinnacleForm As SearchCriteriaList(Of Long)
        Private mIDField As SearchCriteriaList(Of Long)
        Private mLog() As Logs

        Public ReadOnly Property IDBinnacleForm() As SearchCriteriaList(Of Long)
            Get
                Return Me.mIDBinnacleForm
            End Get
        End Property

        Public ReadOnly Property IDField() As SearchCriteriaList(Of Long)
            Get
                Return Me.mIDField
            End Get
        End Property

        Public ReadOnly Property Log() As Logs()
            Get
                Return Me.mLog
            End Get
        End Property

        Public Sub New(ByVal idBinnacleForm As SearchCriteriaList(Of Long), ByVal idField As SearchCriteriaList(Of Long), ByVal log() As Logs)
            Me.mIDBinnacleForm = idBinnacleForm
            Me.mIDField = idField
            Me.mLog = Log
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
        Fetch(criteria.Log, criteria.IDBinnacleForm, criteria.IDField)
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteriaList)
        Fetch(criteria.Log, criteria.IDBinnacleForm, criteria.IDField)
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal list As DAClsprgBinnacleFormFieldEntry.Struct())
        Fetch(list)
    End Sub

    Private Sub Fetch()
        Fetch(DAClsprgBinnacleFormFieldEntry.Fetch())
    End Sub

    Private Sub Fetch(ByVal Log As Logs)
        Fetch(DAClsprgBinnacleFormFieldEntry.Fetch(Log))
    End Sub

    Private Sub Fetch(ByVal Log() As Logs)
        Fetch(DAClsprgBinnacleFormFieldEntry.Fetch(Log))
    End Sub

    Private Sub Fetch(ByVal Log As Logs, ByVal IDBinnacleForm As SearchCriteria(Of Long), ByVal IDField As SearchCriteria(Of Long))
        Fetch(DAClsprgBinnacleFormFieldEntry.Fetch(Log, New Parameter(Of Long)(IDBinnacleForm.Value, IDBinnacleForm.Priority), New Parameter(Of Long)(IDField.Value, IDField.Priority)))
    End Sub

    Private Sub Fetch(ByVal Log() As Logs, ByVal idBinnacleForm As SearchCriteriaList(Of Long), ByVal idField As SearchCriteriaList(Of Long))
        Fetch(DAClsprgBinnacleFormFieldEntry.Fetch(Log, New ParameterList(Of Long)(idBinnacleForm.Values, idBinnacleForm.Priority), New ParameterList(Of Long)(idField.Values, idField.Priority)))
    End Sub

    Private Sub Fetch(ByVal list As DAClsprgBinnacleFormFieldEntry.Struct())

        RaiseListChangedEvents = False
        IsReadOnly = False
        For Each Struct As DAClsprgBinnacleFormFieldEntry.Struct In List
            Me.Add(ClsBinnacleFormFieldEntry.GetBinnacleFormFieldEntry(Struct))
        Next
        IsReadOnly = True
        RaiseListChangedEvents = True
    End Sub

#End Region

End Class
