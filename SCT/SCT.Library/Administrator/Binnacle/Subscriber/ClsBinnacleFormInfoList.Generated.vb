Imports Csla
Imports SCT.DataAccess

Namespace Subscriber
    <Serializable()> Public Class ClsBinnacleFormInfoList
        Inherits ReadOnlyListBase(Of ClsBinnacleFormInfoList, ClsBinnacleFormInfo)

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole(" ")
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function NewBinnacleFormInfoList() As ClsBinnacleFormInfoList
            Return New ClsBinnacleFormInfoList
        End Function

        Public Shared Function GetBinnacleFormInfoList() As ClsBinnacleFormInfoList
            Return DataPortal.Fetch(Of ClsBinnacleFormInfoList)(New Criteria)
        End Function

        Public Shared Function GetBinnacleFormInfoList(ByVal idBinnacle As SearchCriteria(Of Long), ByVal idForm As SearchCriteria(Of Long), ByVal idOperation As SearchCriteria(Of Long), ByVal fromHour As SearchCriteria(Of Date), ByVal toHour As SearchCriteria(Of Date)) As ClsBinnacleFormInfoList
            Return DataPortal.Fetch(Of ClsBinnacleFormInfoList)(New FilteredCriteria(idBinnacle, idForm, idOperation, fromHour, toHour))
        End Function

        Public Shared Function GetBinnacleFormInfoList(ByVal idBinnacle As SearchCriteriaList(Of Long), ByVal idForm As SearchCriteriaList(Of Long), ByVal idOperation As SearchCriteriaList(Of Long), ByVal fromHour As SearchCriteria(Of Date), ByVal toHour As SearchCriteria(Of Date)) As ClsBinnacleFormInfoList
            Return DataPortal.Fetch(Of ClsBinnacleFormInfoList)(New FilteredCriteriaList(idBinnacle, idForm, idOperation, fromHour, toHour))
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

            Private mIDBinnacle As SearchCriteria(Of Long)
            Private mIDForm As SearchCriteria(Of Long)
            Private mIDOperation As SearchCriteria(Of Long)
            Private mFromHour As SearchCriteria(Of Date)
            Private mToHour As SearchCriteria(Of Date)

            Public ReadOnly Property IDBinnacle() As SearchCriteria(Of Long)
                Get
                    Return Me.mIDBinnacle
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

            Public Sub New(ByVal idBinnacle As SearchCriteria(Of Long), ByVal idForm As SearchCriteria(Of Long), ByVal idOperation As SearchCriteria(Of Long), ByVal fromHour As SearchCriteria(Of Date), ByVal toHour As SearchCriteria(Of Date))
                Me.mIDBinnacle = idBinnacle
                Me.mIDForm = idForm
                Me.mIDOperation = idOperation
                Me.mFromHour = fromHour
                Me.mToHour = toHour
            End Sub

        End Class

        <Serializable()> Private Class FilteredCriteriaList

            Private mIDBinnacle As SearchCriteriaList(Of Long)
            Private mIDForm As SearchCriteriaList(Of Long)
            Private mIDOperation As SearchCriteriaList(Of Long)
            Private mFromHour As SearchCriteria(Of Date)
            Private mToHour As SearchCriteria(Of Date)

            Public ReadOnly Property IDBinnacle() As SearchCriteriaList(Of Long)
                Get
                    Return Me.mIDBinnacle
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

            Public Sub New(ByVal idBinnacle As SearchCriteriaList(Of Long), ByVal idForm As SearchCriteriaList(Of Long), ByVal idOperation As SearchCriteriaList(Of Long), ByVal fromHour As SearchCriteria(Of Date), ByVal toHour As SearchCriteria(Of Date))
                Me.mIDBinnacle = idBinnacle
                Me.mIDForm = idForm
                Me.mIDOperation = idOperation
                Me.mFromHour = fromHour
                Me.mToHour = toHour
            End Sub

        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            ' fetch with no filter
            Fetch()
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteria)
            Fetch(criteria.IDBinnacle, criteria.IDForm, criteria.IDOperation, criteria.FromHour, criteria.ToHour)
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteriaList)
            Fetch(criteria.IDBinnacle, criteria.IDForm, criteria.IDOperation, criteria.FromHour, criteria.ToHour)
        End Sub

        Private Sub Fetch()
            Dim list As DAClsprgSubscriberBinnacleForms.Struct() = DAClsprgSubscriberBinnacleForms.Fetch()

            RaiseListChangedEvents = False
            IsReadOnly = False
            For Each Struct As DAClsprgSubscriberBinnacleForms.Struct In list
                Me.Add(ClsBinnacleFormInfo.GetBinnacleFormInfo(Struct))
            Next
            IsReadOnly = True
            RaiseListChangedEvents = True
        End Sub

        Private Sub Fetch(ByVal idBinnacle As SearchCriteria(Of Long), ByVal idForm As SearchCriteria(Of Long), ByVal idOperation As SearchCriteria(Of Long), ByVal fromHour As SearchCriteria(Of Date), ByVal toHour As SearchCriteria(Of Date))
            Dim list As DAClsprgSubscriberBinnacleForms.Struct() = DAClsprgSubscriberBinnacleForms.Fetch(New Parameter(Of Long)(idBinnacle.Value, idBinnacle.Priority), New Parameter(Of Long)(idForm.Value, idForm.Priority), New Parameter(Of Long)(idOperation.Value, idOperation.Priority), New Parameter(Of Date)(fromHour.Value, fromHour.Priority), New Parameter(Of Date)(toHour.Value, toHour.Priority))

            RaiseListChangedEvents = False
            IsReadOnly = False
            For Each Struct As DAClsprgSubscriberBinnacleForms.Struct In list
                Me.Add(ClsBinnacleFormInfo.GetBinnacleFormInfo(Struct))
            Next
            IsReadOnly = True
            RaiseListChangedEvents = True
        End Sub

        Private Sub Fetch(ByVal idBinnacle As SearchCriteriaList(Of Long), ByVal idForm As SearchCriteriaList(Of Long), ByVal idOperation As SearchCriteriaList(Of Long), ByVal fromHour As SearchCriteria(Of Date), ByVal toHour As SearchCriteria(Of Date))
            Dim list As DAClsprgSubscriberBinnacleForms.Struct() = DAClsprgSubscriberBinnacleForms.Fetch(New ParameterList(Of Long)(idBinnacle.Values, idBinnacle.Priority), New ParameterList(Of Long)(idForm.Values, idForm.Priority), New ParameterList(Of Long)(idOperation.Values, idOperation.Priority), New Parameter(Of Date)(fromHour.Value, fromHour.Priority), New Parameter(Of Date)(toHour.Value, toHour.Priority))

            RaiseListChangedEvents = False
            IsReadOnly = False
            For Each Struct As DAClsprgSubscriberBinnacleForms.Struct In list
                Me.Add(ClsBinnacleFormInfo.GetBinnacleFormInfo(Struct))
            Next
            IsReadOnly = True
            RaiseListChangedEvents = True
        End Sub

#End Region

    End Class
End Namespace
