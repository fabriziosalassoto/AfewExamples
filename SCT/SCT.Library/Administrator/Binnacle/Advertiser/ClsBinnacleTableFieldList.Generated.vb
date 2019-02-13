Imports Csla
Imports SCT.DataAccess

Namespace Advertiser
    <Serializable()> Public Class ClsBinnacleTableFieldList
        Inherits BusinessListBase(Of ClsBinnacleTableFieldList, ClsBinnacleTableField)

#Region " Business Methods "

        Public Function GetItem(ByVal Id As Long) As ClsBinnacleTableField
            For Each item As ClsBinnacleTableField In Me
                If item.ID = Id Then
                    Return item
                End If
            Next
            Return Nothing
        End Function

        Public Sub AddNewItem(ByVal operation As ClsOperation, ByVal tableFieldData As Object)
            'If tableFieldData.IsPrimaryKey OrElse (operation.Description = "Insert" OrElse operation.Description = "Update" OrElse operation.Description = "SignUp") AndAlso (tableFieldData.OldValue <> tableFieldData.Value) Then
            If tableFieldData.IsPrimaryKey OrElse tableFieldData.OldValue <> tableFieldData.Value Then
                Add(ClsBinnacleTableField.NewChildBinnacleTableField(tableFieldData.FieldName, tableFieldData.OldValue.ToString, tableFieldData.Value.ToString))
            End If
        End Sub

        Public Overloads Sub Remove(ByVal Id As Long)
            For Each item As ClsBinnacleTableField In Me
                If item.ID = Id Then
                    Remove(item)
                    Exit For
                End If
            Next
        End Sub

        Public Overloads Function Contains(ByVal Id As Long) As Boolean
            For Each item As ClsBinnacleTableField In Me
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

        Public Shared Function NewBinnacleTableFieldList() As ClsBinnacleTableFieldList
            Return DataPortal.Create(Of ClsBinnacleTableFieldList)(New Criteria())
        End Function

        Public Shared Function GetBinnacleTableFieldList() As ClsBinnacleTableFieldList
            Return DataPortal.Fetch(Of ClsBinnacleTableFieldList)(New Criteria())
        End Function

        Public Shared Function GetBinnacleTableFieldList(ByVal idBinnacleTable As SearchCriteria(Of Long), ByVal fieldName As SearchCriteria(Of String)) As ClsBinnacleTableFieldList
            Return DataPortal.Fetch(Of ClsBinnacleTableFieldList)(New FilteredCriteria(idBinnacleTable, fieldName))
        End Function

        Public Shared Function GetBinnacleTableFieldList(ByVal idBinnacleTable As SearchCriteriaList(Of Long), ByVal fieldName As SearchCriteriaList(Of String)) As ClsBinnacleTableFieldList
            Return DataPortal.Fetch(Of ClsBinnacleTableFieldList)(New FilteredCriteriaList(idBinnacleTable, fieldName))
        End Function

#End Region

#Region " Child Methods "

        Friend Shared Function NewChildBinnacleTableFieldList() As ClsBinnacleTableFieldList
            Return New ClsBinnacleTableFieldList
        End Function

        Friend Shared Function GetChildBinnacleTableFieldList(ByVal list As DAClsprgAdvertiserBinnacleTableFields.Struct()) As ClsBinnacleTableFieldList
            Return New ClsBinnacleTableFieldList(list)
        End Function

        Private Sub New()
            MarkAsChild()
        End Sub

        Private Sub New(ByVal list As DAClsprgAdvertiserBinnacleTableFields.Struct())
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

            Private mIDBinnacleTable As SearchCriteria(Of Long)
            Private mFieldName As SearchCriteria(Of String)

            Public ReadOnly Property IDBinnacleTable() As SearchCriteria(Of Long)
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

            Public ReadOnly Property IDBinnacleTable() As SearchCriteriaList(Of Long)
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

        <RunLocal()> Private Overloads Sub DataPortal_Create(ByVal criteria As Criteria)
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            ' fetch with no filter
            Fetch()
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteria)
            Fetch(criteria.IDBinnacleTable, criteria.FieldName)
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteriaList)
            Fetch(criteria.IDBinnacleTable, criteria.FieldName)
        End Sub

        Protected Overrides Sub DataPortal_Update()
            RaiseListChangedEvents = False
            For Each item As ClsBinnacleTableField In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsBinnacleTableField In Me
                If item.IsNew Then
                    item.Insert(item.BinnacleTable)
                Else
                    item.Update(item.BinnacleTable)
                End If
            Next
            RaiseListChangedEvents = True
        End Sub

#End Region

#Region " Child Area "

        Friend Sub Update(ByVal parent As ClsBinnacleTable)
            RaiseListChangedEvents = False
            For Each item As ClsBinnacleTableField In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsBinnacleTableField In Me
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
            Fetch(DAClsprgAdvertiserBinnacleTableFields.Fetch())
        End Sub

        Private Sub Fetch(ByVal idBinnacleTable As SearchCriteria(Of Long), ByVal fieldName As SearchCriteria(Of String))
            Fetch(DAClsprgAdvertiserBinnacleTableFields.Fetch(New Parameter(Of Long)(idBinnacleTable.Value, idBinnacleTable.Priority), New Parameter(Of String)(fieldName.Value, fieldName.Priority)))
        End Sub

        Private Sub Fetch(ByVal idBinnacleTable As SearchCriteriaList(Of Long), ByVal fieldName As SearchCriteriaList(Of String))
            Fetch(DAClsprgAdvertiserBinnacleTableFields.Fetch(New ParameterList(Of Long)(idBinnacleTable.Values, idBinnacleTable.Priority), New ParameterList(Of String)(fieldName.Values, fieldName.Priority)))
        End Sub

        Private Sub Fetch(ByVal list As DAClsprgAdvertiserBinnacleTableFields.Struct())
            Me.RaiseListChangedEvents = False
            For Each Struct As DAClsprgAdvertiserBinnacleTableFields.Struct In list
                Me.Add(ClsBinnacleTableField.GetChildBinnacleTableField(Struct))
            Next
            Me.RaiseListChangedEvents = True
        End Sub

#End Region

#End Region

    End Class
End Namespace