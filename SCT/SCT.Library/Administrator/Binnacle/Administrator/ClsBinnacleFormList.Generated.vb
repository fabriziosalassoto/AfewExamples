Imports Csla
Imports SCT.DataAccess

Namespace Administrator
    <Serializable()> Public Class ClsBinnacleFormList
        Inherits BusinessListBase(Of ClsBinnacleFormList, ClsBinnacleForm)

#Region " Business Methods "

        Public Function GetItem(ByVal Id As Long) As ClsBinnacleForm
            For Each item As ClsBinnacleForm In Me
                If item.ID = Id Then
                    Return item
                End If
            Next
            Return Nothing
        End Function

        Public Sub AddNewItem(ByVal form As ClsForm, ByVal operation As ClsOperation, ByVal hour As Date, ByVal ParamArray formFieldsData() As Object)
            Add(ClsBinnacleForm.NewChildBinnacleForm(form, operation, hour, formFieldsData))
        End Sub

        Public Overloads Sub Remove(ByVal Id As Long)
            For Each item As ClsBinnacleForm In Me
                If item.ID = Id Then
                    Remove(item)
                    Exit For
                End If
            Next
        End Sub

        Public Overloads Function Contains(ByVal Id As Long) As Boolean
            For Each item As ClsBinnacleForm In Me
                If item.ID = Id Then
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

        Public Shared Function NewBinnacleFormList() As ClsBinnacleFormList
            Return DataPortal.Create(Of ClsBinnacleFormList)(New Criteria())
        End Function

        Public Shared Function GetBinnacleFormList() As ClsBinnacleFormList
            Return DataPortal.Fetch(Of ClsBinnacleFormList)(New Criteria())
        End Function

        Public Shared Function GetBinnacleFormList(ByVal idBinnacle As SearchCriteria(Of Long), ByVal idForm As SearchCriteria(Of Long), ByVal idOperation As SearchCriteria(Of Long), ByVal fromHour As SearchCriteria(Of Date), ByVal toHour As SearchCriteria(Of Date)) As ClsBinnacleFormList
            Return DataPortal.Fetch(Of ClsBinnacleFormList)(New FilteredCriteria(idBinnacle, idForm, idOperation, fromHour, toHour))
        End Function

        Public Shared Function GetBinnacleFormList(ByVal idBinnacle As SearchCriteriaList(Of Long), ByVal idForm As SearchCriteriaList(Of Long), ByVal idOperation As SearchCriteriaList(Of Long), ByVal fromHour As SearchCriteria(Of Date), ByVal toHour As SearchCriteria(Of Date)) As ClsBinnacleFormList
            Return DataPortal.Fetch(Of ClsBinnacleFormList)(New FilteredCriteriaList(idBinnacle, idForm, idOperation, fromHour, toHour))
        End Function

#End Region

#Region " Child Methods "

        Friend Shared Function NewChildBinnacleFormList() As ClsBinnacleFormList
            Return New ClsBinnacleFormList
        End Function

        Friend Shared Function GetChildBinnacleFormList(ByVal list As DAClsprgAdministrativeBinnacleForms.Struct()) As ClsBinnacleFormList
            Return New ClsBinnacleFormList(list)
        End Function

        Private Sub New()
            MarkAsChild()
        End Sub

        Private Sub New(ByVal list As DAClsprgAdministrativeBinnacleForms.Struct())
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

        <RunLocal()> Private Overloads Sub DataPortal_Create(ByVal criteria As Criteria)
        End Sub

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

        Protected Overrides Sub DataPortal_Update()
            RaiseListChangedEvents = False
            For Each item As ClsBinnacleForm In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsBinnacleForm In Me
                If item.IsNew Then
                    item.Insert(New Object() {item.Binnacle, item.Form, item.Operation})
                Else
                    item.Update(New Object() {item.Binnacle, item.Form, item.Operation})
                End If
            Next
            RaiseListChangedEvents = True
        End Sub

#End Region

#Region " Child Area "

        Friend Sub Update(ByVal parent As ClsBinnacle)
            RaiseListChangedEvents = False
            For Each item As ClsBinnacleForm In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsBinnacleForm In Me
                If item.IsNew Then
                    item.Insert(New Object() {parent, item.Form, item.Operation})
                Else
                    item.Update(New Object() {parent, item.Form, item.Operation})
                End If
            Next
            RaiseListChangedEvents = True
        End Sub

        Friend Sub Update(ByVal parent As ClsForm)
            RaiseListChangedEvents = False
            For Each item As ClsBinnacleForm In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsBinnacleForm In Me
                If item.IsNew Then
                    item.Insert(New Object() {item.Binnacle, parent, item.Operation})
                Else
                    item.Update(New Object() {item.Binnacle, parent, item.Operation})
                End If
            Next
            RaiseListChangedEvents = True
        End Sub

        Friend Sub Update(ByVal parent As ClsOperation)
            RaiseListChangedEvents = False
            For Each item As ClsBinnacleForm In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsBinnacleForm In Me
                If item.IsNew Then
                    item.Insert(New Object() {item.Binnacle, item.Form, parent})
                Else
                    item.Update(New Object() {item.Binnacle, item.Form, parent})
                End If
            Next
            RaiseListChangedEvents = True
        End Sub

#End Region

#Region " Common Area "

        Private Sub Fetch()
            Fetch(DAClsprgAdministrativeBinnacleForms.Fetch())
        End Sub

        Private Sub Fetch(ByVal idBinnacle As SearchCriteria(Of Long), ByVal idForm As SearchCriteria(Of Long), ByVal idOperation As SearchCriteria(Of Long), ByVal fromHour As SearchCriteria(Of Date), ByVal toHour As SearchCriteria(Of Date))
            Fetch(DAClsprgAdministrativeBinnacleForms.Fetch(New Parameter(Of Long)(idBinnacle.Value, idBinnacle.Priority), New Parameter(Of Long)(idForm.Value, idForm.Priority), New Parameter(Of Long)(idOperation.Value, idOperation.Priority), New Parameter(Of Date)(fromHour.Value, fromHour.Priority), New Parameter(Of Date)(toHour.Value, toHour.Priority)))
        End Sub

        Private Sub Fetch(ByVal idBinnacle As SearchCriteriaList(Of Long), ByVal idForm As SearchCriteriaList(Of Long), ByVal idOperation As SearchCriteriaList(Of Long), ByVal fromHour As SearchCriteria(Of Date), ByVal toHour As SearchCriteria(Of Date))
            Fetch(DAClsprgAdministrativeBinnacleForms.Fetch(New ParameterList(Of Long)(idBinnacle.Values, idBinnacle.Priority), New ParameterList(Of Long)(idForm.Values, idForm.Priority), New ParameterList(Of Long)(idOperation.Values, idOperation.Priority), New Parameter(Of Date)(fromHour.Value, fromHour.Priority), New Parameter(Of Date)(toHour.Value, toHour.Priority)))
        End Sub

        Private Sub Fetch(ByVal list As DAClsprgAdministrativeBinnacleForms.Struct())
            Me.RaiseListChangedEvents = False
            For Each Struct As DAClsprgAdministrativeBinnacleForms.Struct In list
                Me.Add(ClsBinnacleForm.GetChildBinnacleForm(Struct))
            Next
            Me.RaiseListChangedEvents = True
        End Sub

#End Region

#End Region

    End Class
End Namespace