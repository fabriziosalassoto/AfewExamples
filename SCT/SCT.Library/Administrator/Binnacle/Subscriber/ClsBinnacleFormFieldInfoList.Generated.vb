Imports Csla
Imports SCT.DataAccess

Namespace Subscriber
    <Serializable()> Public Class ClsBinnacleFormFieldInfoList
        Inherits ReadOnlyListBase(Of ClsBinnacleFormFieldInfoList, ClsBinnacleFormFieldInfo)

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole(" ")
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function NewBinnacleFormFieldInfoList() As ClsBinnacleFormFieldInfoList
            Return New ClsBinnacleFormFieldInfoList
        End Function

        Public Shared Function GetBinnacleFormFieldInfoList() As ClsBinnacleFormFieldInfoList
            Return DataPortal.Fetch(Of ClsBinnacleFormFieldInfoList)(New Criteria)
        End Function

        Public Shared Function GetBinnacleFormFieldInfoList(ByVal idBinnacleForm As SearchCriteria(Of Long), ByVal idField As SearchCriteria(Of Long)) As ClsBinnacleFormFieldInfoList
            Return DataPortal.Fetch(Of ClsBinnacleFormFieldInfoList)(New FilteredCriteria(idBinnacleForm, idField))
        End Function

        Public Shared Function GetBinnacleFormFieldInfoList(ByVal idBinnacleForm As SearchCriteriaList(Of Long), ByVal idField As SearchCriteriaList(Of Long)) As ClsBinnacleFormFieldInfoList
            Return DataPortal.Fetch(Of ClsBinnacleFormFieldInfoList)(New FilteredCriteriaList(idBinnacleForm, idField))
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

            Private mIDBinnacleForm As SearchCriteria(Of Long)
            Private mIDField As SearchCriteria(Of Long)

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

            Public Sub New(ByVal idBinnacleForm As SearchCriteria(Of Long), ByVal idField As SearchCriteria(Of Long))
                Me.mIDBinnacleForm = idBinnacleForm
                Me.mIDField = idField
            End Sub

        End Class

        <Serializable()> Private Class FilteredCriteriaList

            Private mIDBinnacleForm As SearchCriteriaList(Of Long)
            Private mIDField As SearchCriteriaList(Of Long)

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

            Public Sub New(ByVal idBinnacleForm As SearchCriteriaList(Of Long), ByVal idField As SearchCriteriaList(Of Long))
                Me.mIDBinnacleForm = idBinnacleForm
                Me.mIDField = idField
            End Sub

        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            ' fetch with no filter
            Fetch()
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteria)
            Fetch(criteria.IDBinnacleForm, criteria.IDField)
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteriaList)
            Fetch(criteria.IDBinnacleForm, criteria.IDField)
        End Sub

        Private Sub Fetch()
            Dim list As DAClsprgSubscriberBinnacleFormFields.Struct() = DAClsprgSubscriberBinnacleFormFields.Fetch()

            RaiseListChangedEvents = False
            IsReadOnly = False
            For Each Struct As DAClsprgSubscriberBinnacleFormFields.Struct In list
                Me.Add(ClsBinnacleFormFieldInfo.GetBinnacleFormFieldInfo(Struct))
            Next
            IsReadOnly = True
            RaiseListChangedEvents = True
        End Sub

        Private Sub Fetch(ByVal IDBinnacleForm As SearchCriteria(Of Long), ByVal IDField As SearchCriteria(Of Long))
            Dim list As DAClsprgSubscriberBinnacleFormFields.Struct() = DAClsprgSubscriberBinnacleFormFields.Fetch(New Parameter(Of Long)(IDBinnacleForm.Value, IDBinnacleForm.Priority), New Parameter(Of Long)(IDField.Value, IDField.Priority))

            RaiseListChangedEvents = False
            IsReadOnly = False
            For Each Struct As DAClsprgSubscriberBinnacleFormFields.Struct In list
                Me.Add(ClsBinnacleFormFieldInfo.GetBinnacleFormFieldInfo(Struct))
            Next
            IsReadOnly = True
            RaiseListChangedEvents = True
        End Sub

        Private Sub Fetch(ByVal idBinnacleForm As SearchCriteriaList(Of Long), ByVal idField As SearchCriteriaList(Of Long))
            Dim list As DAClsprgSubscriberBinnacleFormFields.Struct() = DAClsprgSubscriberBinnacleFormFields.Fetch(New ParameterList(Of Long)(idBinnacleForm.Values, idBinnacleForm.Priority), New ParameterList(Of Long)(idField.Values, idField.Priority))

            RaiseListChangedEvents = False
            IsReadOnly = False
            For Each Struct As DAClsprgSubscriberBinnacleFormFields.Struct In list
                Me.Add(ClsBinnacleFormFieldInfo.GetBinnacleFormFieldInfo(Struct))
            Next
            IsReadOnly = True
            RaiseListChangedEvents = True
        End Sub

#End Region

    End Class
End Namespace