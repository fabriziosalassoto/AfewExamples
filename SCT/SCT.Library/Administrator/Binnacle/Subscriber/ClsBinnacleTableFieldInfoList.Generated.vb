Imports Csla
Imports SCT.DataAccess

Namespace Subscriber
    <Serializable()> Public Class ClsBinnacleTableFieldInfoList
        Inherits ReadOnlyListBase(Of ClsBinnacleTableFieldInfoList, ClsBinnacleTableFieldInfo)

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole(" ")
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function NewBinnacleTableFieldInfoList() As ClsBinnacleTableFieldInfoList
            Return New ClsBinnacleTableFieldInfoList
        End Function

        Public Shared Function GetBinnacleTableFieldInfoList() As ClsBinnacleTableFieldInfoList
            Return DataPortal.Fetch(Of ClsBinnacleTableFieldInfoList)(New Criteria)
        End Function

        Public Shared Function GetBinnacleTableFieldInfoList(ByVal IDBinnacleTable As SearchCriteria(Of Long), ByVal FieldName As SearchCriteria(Of String)) As ClsBinnacleTableFieldInfoList
            Return DataPortal.Fetch(Of ClsBinnacleTableFieldInfoList)(New FilteredCriteria(IDBinnacleTable, FieldName))
        End Function

        Public Shared Function GetBinnacleTableFieldInfoList(ByVal IDBinnacleTable As SearchCriteriaList(Of Long), ByVal FieldName As SearchCriteriaList(Of String)) As ClsBinnacleTableFieldInfoList
            Return DataPortal.Fetch(Of ClsBinnacleTableFieldInfoList)(New FilteredCriteriaList(IDBinnacleTable, FieldName))
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

            Private mIDBinnacleTable As SearchCriteria(Of Long)
            Private mFieldName As SearchCriteria(Of String)

            Public ReadOnly Property IDbinnacleTable() As SearchCriteria(Of Long)
                Get
                    Return Me.mIDBinnacleTable
                End Get
            End Property

            Public ReadOnly Property FieldName() As SearchCriteria(Of String)
                Get
                    Return Me.mFieldName
                End Get
            End Property

            Public Sub New(ByVal idBinnacleTable As SearchCriteria(Of Long), ByVal fieldName As SearchCriteria(Of String))
                Me.mIDBinnacleTable = idBinnacleTable
                Me.mFieldName = fieldName
            End Sub

        End Class

        <Serializable()> Private Class FilteredCriteriaList

            Private mIDBinnacleTable As SearchCriteriaList(Of Long)
            Private mFieldName As SearchCriteriaList(Of String)

            Public ReadOnly Property IDbinnacleTable() As SearchCriteriaList(Of Long)
                Get
                    Return Me.mIDBinnacleTable
                End Get
            End Property

            Public ReadOnly Property FieldName() As SearchCriteriaList(Of String)
                Get
                    Return Me.mFieldName
                End Get
            End Property

            Public Sub New(ByVal idBinnacleTable As SearchCriteriaList(Of Long), ByVal fieldName As SearchCriteriaList(Of String))
                Me.mIDBinnacleTable = idBinnacleTable
                Me.mFieldName = fieldName
            End Sub

        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            ' fetch with no filter
            Fetch()
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteria)
            Fetch(criteria.IDbinnacleTable, criteria.FieldName)
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteriaList)
            Fetch(criteria.IDbinnacleTable, criteria.FieldName)
        End Sub

        Private Sub Fetch()
            Dim list As DAClsprgSubscriberBinnacleTableFields.Struct() = DAClsprgSubscriberBinnacleTableFields.Fetch()

            RaiseListChangedEvents = False
            IsReadOnly = False
            For Each Struct As DAClsprgSubscriberBinnacleTableFields.Struct In list
                Me.Add(ClsBinnacleTableFieldInfo.GetBinnacleTableFieldInfo(Struct))
            Next
            IsReadOnly = True
            RaiseListChangedEvents = True
        End Sub

        Private Sub Fetch(ByVal idBinnacleTable As SearchCriteria(Of Long), ByVal fieldName As SearchCriteria(Of String))
            Dim list As DAClsprgSubscriberBinnacleTableFields.Struct() = DAClsprgSubscriberBinnacleTableFields.Fetch(New Parameter(Of Long)(idBinnacleTable.Value, idBinnacleTable.Priority), New Parameter(Of String)(fieldName.Value, fieldName.Priority))

            RaiseListChangedEvents = False
            IsReadOnly = False
            For Each Struct As DAClsprgSubscriberBinnacleTableFields.Struct In list
                Me.Add(ClsBinnacleTableFieldInfo.GetBinnacleTableFieldInfo(Struct))
            Next
            IsReadOnly = True
            RaiseListChangedEvents = True
        End Sub

        Private Sub Fetch(ByVal idBinnacleTable As SearchCriteriaList(Of Long), ByVal fieldName As SearchCriteriaList(Of String))
            Dim list As DAClsprgSubscriberBinnacleTableFields.Struct() = DAClsprgSubscriberBinnacleTableFields.Fetch(New ParameterList(Of Long)(idBinnacleTable.Values, idBinnacleTable.Priority), New ParameterList(Of String)(fieldName.Values, fieldName.Priority))

            RaiseListChangedEvents = False
            IsReadOnly = False
            For Each Struct As DAClsprgSubscriberBinnacleTableFields.Struct In list
                Me.Add(ClsBinnacleTableFieldInfo.GetBinnacleTableFieldInfo(Struct))
            Next
            IsReadOnly = True
            RaiseListChangedEvents = True
        End Sub

#End Region

    End Class
End Namespace