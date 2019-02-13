Imports Csla
Imports SCT.DataAccess

Namespace Advertiser
    <Serializable()> Public Class ClsBinnacleFormFieldList
        Inherits BusinessListBase(Of ClsBinnacleFormFieldList, ClsBinnacleFormField)

#Region " Business Methods "

        Public Function GetItem(ByVal Id As Long) As ClsBinnacleFormField
            For Each item As ClsBinnacleFormField In Me
                If item.ID = Id Then
                    Return item
                End If
            Next
            Return Nothing
        End Function

        Public Sub AddNewItem(ByVal formField As ClsField, ByVal operation As ClsOperation, ByVal formFieldData As Object)
            'If (formFieldData.IsPrimaryKey OrElse (operation.Description = "Insert" OrElse operation.Description = "Update" OrElse operation.Description = "SignUp") AndAlso (formFieldData.OldValue <> formFieldData.NewValue)) AndAlso formField IsNot Nothing Then
            If (formFieldData.IsPrimaryKey OrElse formFieldData.OldValue <> formFieldData.NewValue) AndAlso formField IsNot Nothing Then
                Add(ClsBinnacleFormField.NewChildBinnacleFormField(formField.ID, formFieldData.OldValue.ToString, formFieldData.NewValue.ToString))
            End If
        End Sub

        Public Overloads Sub Remove(ByVal Id As Long)
            For Each item As ClsBinnacleFormField In Me
                If item.ID = Id Then
                    Remove(item)
                    Exit For
                End If
            Next
        End Sub

        Public Overloads Function Contains(ByVal Id As Long) As Boolean
            For Each item As ClsBinnacleFormField In Me
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

        Public Shared Function NewBinnacleFormFieldList() As ClsBinnacleFormFieldList
            Return DataPortal.Create(Of ClsBinnacleFormFieldList)(New Criteria())
        End Function

        Public Shared Function GetBinnacleFormFieldList() As ClsBinnacleFormFieldList
            Return DataPortal.Fetch(Of ClsBinnacleFormFieldList)(New Criteria())
        End Function

        Public Shared Function GetBinnacleFormFieldList(ByVal idBinnacleForm As SearchCriteria(Of Long), ByVal idField As SearchCriteria(Of Long)) As ClsBinnacleFormFieldList
            Return DataPortal.Fetch(Of ClsBinnacleFormFieldList)(New FilteredCriteria(idBinnacleForm, idField))
        End Function

        Public Shared Function GetBinnacleFormFieldList(ByVal idBinnacleForm As SearchCriteriaList(Of Long), ByVal idField As SearchCriteriaList(Of Long)) As ClsBinnacleFormFieldList
            Return DataPortal.Fetch(Of ClsBinnacleFormFieldList)(New FilteredCriteriaList(idBinnacleForm, idField))
        End Function

#End Region

#Region " Child Methods "

        Friend Shared Function NewChildBinnacleFormFieldList() As ClsBinnacleFormFieldList
            Return New ClsBinnacleFormFieldList
        End Function

        Friend Shared Function GetChildBinnacleFormFieldList(ByVal list As DAClsprgAdvertiserBinnacleFormFields.Struct()) As ClsBinnacleFormFieldList
            Return New ClsBinnacleFormFieldList(list)
        End Function

        Private Sub New()
            MarkAsChild()
        End Sub

        Private Sub New(ByVal list As DAClsprgAdvertiserBinnacleFormFields.Struct())
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

        <RunLocal()> Private Overloads Sub DataPortal_Create(ByVal criteria As Criteria)
        End Sub

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

        Protected Overrides Sub DataPortal_Update()
            RaiseListChangedEvents = False
            For Each item As ClsBinnacleFormField In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsBinnacleFormField In Me
                If item.IsNew Then
                    item.Insert(New Object() {item.BinnacleForm, item.Field})
                Else
                    item.Update(New Object() {item.BinnacleForm, item.Field})
                End If
            Next
            RaiseListChangedEvents = True
        End Sub

#End Region

#Region " Child Area "

        Friend Sub Update(ByVal parent As ClsBinnacleForm)
            RaiseListChangedEvents = False
            For Each item As ClsBinnacleFormField In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsBinnacleFormField In Me
                If item.IsNew Then
                    item.Insert(New Object() {parent, item.Field})
                Else
                    item.Update(New Object() {parent, item.Field})
                End If
            Next
            RaiseListChangedEvents = True
        End Sub

        Friend Sub Update(ByVal parent As ClsField)
            RaiseListChangedEvents = False
            For Each item As ClsBinnacleFormField In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsBinnacleFormField In Me
                If item.IsNew Then
                    item.Insert(New Object() {item.BinnacleForm, parent})
                Else
                    item.Update(New Object() {item.BinnacleForm, parent})
                End If
            Next
            RaiseListChangedEvents = True
        End Sub

#End Region

#Region " Common Area "

        Private Sub Fetch()
            Fetch(DAClsprgAdvertiserBinnacleFormFields.Fetch())
        End Sub

        Private Sub Fetch(ByVal idBinnacleForm As SearchCriteria(Of Long), ByVal idField As SearchCriteria(Of Long))
            Fetch(DAClsprgAdvertiserBinnacleFormFields.Fetch(New Parameter(Of Long)(idBinnacleForm.Value, idBinnacleForm.Priority), New Parameter(Of Long)(idField.Value, idField.Priority)))
        End Sub

        Private Sub Fetch(ByVal idBinnacleForm As SearchCriteriaList(Of Long), ByVal idField As SearchCriteriaList(Of Long))
            Fetch(DAClsprgAdvertiserBinnacleFormFields.Fetch(New ParameterList(Of Long)(idBinnacleForm.Values, idBinnacleForm.Priority), New ParameterList(Of Long)(idField.Values, idField.Priority)))
        End Sub

        Private Sub Fetch(ByVal list As DAClsprgAdvertiserBinnacleFormFields.Struct())
            Me.RaiseListChangedEvents = False
            For Each Struct As DAClsprgAdvertiserBinnacleFormFields.Struct In list
                Me.Add(ClsBinnacleFormField.GetChildBinnacleFormField(Struct))
            Next
            Me.RaiseListChangedEvents = True
        End Sub

#End Region

#End Region

    End Class
End Namespace
