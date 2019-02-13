Imports Csla
Imports SCT.DataAccess

Namespace Subscriber
    <Serializable()> Public Class ClsBinnacleTableList
        Inherits BusinessListBase(Of ClsBinnacleTableList, ClsBinnacleTable)

#Region " Business Methods "

        Public Function GetItem(ByVal Id As Long) As ClsBinnacleTable
            For Each item As ClsBinnacleTable In Me
                If item.ID = Id Then
                    Return item
                End If
            Next
            Return Nothing
        End Function

        Public Sub AddNewItem(ByVal operation As ClsOperation, ByVal tableName As String, ByVal hour As Date, ByVal ParamArray tableFieldsData() As Object)
            Add(ClsBinnacleTable.NewChildBinnacleTable(operation, tableName, hour, tableFieldsData))
        End Sub

        Public Overloads Sub Remove(ByVal Id As Long)
            For Each item As ClsBinnacleTable In Me
                If item.ID = Id Then
                    Remove(item)
                    Exit For
                End If
            Next
        End Sub

        Public Overloads Function Contains(ByVal Id As Long) As Boolean
            For Each item As ClsBinnacleTable In Me
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

        Public Shared Function NewBinnacleTableList() As ClsBinnacleTableList
            Return DataPortal.Create(Of ClsBinnacleTableList)(New Criteria())
        End Function

        Public Shared Function GetBinnacleTableList() As ClsBinnacleTableList
            Return DataPortal.Fetch(Of ClsBinnacleTableList)(New Criteria())
        End Function

        Public Shared Function GetBinnacleTableList(ByVal idBinnacle As SearchCriteria(Of Long), ByVal idOperation As SearchCriteria(Of Long), ByVal tableName As SearchCriteria(Of String), ByVal fromHour As SearchCriteria(Of Date), ByVal toHour As SearchCriteria(Of Date)) As ClsBinnacleTableList
            Return DataPortal.Fetch(Of ClsBinnacleTableList)(New FilteredCriteria(idBinnacle, idOperation, tableName, fromHour, toHour))
        End Function

        Public Shared Function GetBinnacleTableList(ByVal idBinnacle As SearchCriteriaList(Of Long), ByVal idOperation As SearchCriteriaList(Of Long), ByVal tableName As SearchCriteriaList(Of String), ByVal fromHour As SearchCriteria(Of Date), ByVal toHour As SearchCriteria(Of Date)) As ClsBinnacleTableList
            Return DataPortal.Fetch(Of ClsBinnacleTableList)(New FilteredCriteriaList(idBinnacle, idOperation, tableName, fromHour, toHour))
        End Function

#End Region

#Region " Child Methods "

        Friend Shared Function NewChildBinnacleTableList() As ClsBinnacleTableList
            Return New ClsBinnacleTableList
        End Function

        Friend Shared Function GetChildBinnacleTableList(ByVal list As DAClsprgSubscriberBinnacleTables.Struct()) As ClsBinnacleTableList
            Return New ClsBinnacleTableList(list)
        End Function

        Private Sub New()
            MarkAsChild()
        End Sub

        Private Sub New(ByVal list As DAClsprgSubscriberBinnacleTables.Struct())
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
            Private mIDOperation As SearchCriteria(Of Long)
            Private mTableName As SearchCriteria(Of String)
            Private mFromHour As SearchCriteria(Of Date)
            Private mToHour As SearchCriteria(Of Date)

            Public ReadOnly Property IDBinnacle() As SearchCriteria(Of Long)
                Get
                    Return Me.mIDBinnacle
                End Get
            End Property

            Public ReadOnly Property IDOperation() As SearchCriteria(Of Long)
                Get
                    Return Me.mIDOperation
                End Get
            End Property

            Public ReadOnly Property TableName() As SearchCriteria(Of String)
                Get
                    Return Me.mTableName
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

            Public Sub New(ByVal idBinnacle As SearchCriteria(Of Long), ByVal idOperation As SearchCriteria(Of Long), ByVal tableName As SearchCriteria(Of String), ByVal fromHour As SearchCriteria(Of Date), ByVal toHour As SearchCriteria(Of Date))
                Me.mIDBinnacle = idBinnacle
                Me.mIDOperation = idOperation
                Me.mTableName = tableName
                Me.mFromHour = fromHour
                Me.mToHour = toHour
            End Sub

        End Class

        <Serializable()> Private Class FilteredCriteriaList

            Private mIDBinnacle As SearchCriteriaList(Of Long)
            Private mIDOperation As SearchCriteriaList(Of Long)
            Private mTableName As SearchCriteriaList(Of String)
            Private mFromHour As SearchCriteria(Of Date)
            Private mToHour As SearchCriteria(Of Date)

            Public ReadOnly Property IDBinnacle() As SearchCriteriaList(Of Long)
                Get
                    Return Me.mIDBinnacle
                End Get
            End Property

            Public ReadOnly Property IDOperation() As SearchCriteriaList(Of Long)
                Get
                    Return Me.mIDOperation
                End Get
            End Property

            Public ReadOnly Property TableName() As SearchCriteriaList(Of String)
                Get
                    Return Me.mTableName
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

            Public Sub New(ByVal idBinnacle As SearchCriteriaList(Of Long), ByVal idOperation As SearchCriteriaList(Of Long), ByVal tableName As SearchCriteriaList(Of String), ByVal fromHour As SearchCriteria(Of Date), ByVal toHour As SearchCriteria(Of Date))
                Me.mIDBinnacle = idBinnacle
                Me.mIDOperation = idOperation
                Me.mTableName = tableName
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
            Fetch(criteria.IDBinnacle, criteria.IDOperation, criteria.TableName, criteria.FromHour, criteria.ToHour)
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteriaList)
            Fetch(criteria.IDBinnacle, criteria.IDOperation, criteria.TableName, criteria.FromHour, criteria.ToHour)
        End Sub


        Protected Overrides Sub DataPortal_Update()
            RaiseListChangedEvents = False
            For Each item As ClsBinnacleTable In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsBinnacleTable In Me
                If item.IsNew Then
                    item.Insert(New Object() {item.Binnacle, item.Operation})
                Else
                    item.Update(New Object() {item.Binnacle, item.Operation})
                End If
            Next
            RaiseListChangedEvents = True
        End Sub

#End Region

#Region " Child Area "

        Friend Sub Update(ByVal parent As ClsBinnacle)
            RaiseListChangedEvents = False
            For Each item As ClsBinnacleTable In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsBinnacleTable In Me
                If item.IsNew Then
                    item.Insert(New Object() {parent, item.Operation})
                Else
                    item.Update(New Object() {parent, item.Operation})
                End If
            Next
            RaiseListChangedEvents = True
        End Sub

        Friend Sub Update(ByVal parent As ClsOperation)
            RaiseListChangedEvents = False
            For Each item As ClsBinnacleTable In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsBinnacleTable In Me
                If item.IsNew Then
                    item.Insert(New Object() {item.Binnacle, parent})
                Else
                    item.Update(New Object() {item.Binnacle, parent})
                End If
            Next
            RaiseListChangedEvents = True
        End Sub

#End Region

#Region " Common Area "

        Private Sub Fetch()
            Fetch(DAClsprgSubscriberBinnacleTables.Fetch())
        End Sub

        Private Sub Fetch(ByVal idBinnacle As SearchCriteria(Of Long), ByVal idOperation As SearchCriteria(Of Long), ByVal tableName As SearchCriteria(Of String), ByVal fromHour As SearchCriteria(Of Date), ByVal toHour As SearchCriteria(Of Date))
            Fetch(DAClsprgSubscriberBinnacleTables.Fetch(New Parameter(Of Long)(idBinnacle.Value, idBinnacle.Priority), New Parameter(Of String)(tableName.Value, tableName.Priority), New Parameter(Of Long)(idOperation.Value, idOperation.Priority), New Parameter(Of Date)(fromHour.Value, fromHour.Priority), New Parameter(Of Date)(toHour.Value, toHour.Priority)))
        End Sub

        Private Sub Fetch(ByVal idBinnacle As SearchCriteriaList(Of Long), ByVal idOperation As SearchCriteriaList(Of Long), ByVal tableName As SearchCriteriaList(Of String), ByVal fromHour As SearchCriteria(Of Date), ByVal toHour As SearchCriteria(Of Date))
            Fetch(DAClsprgSubscriberBinnacleTables.Fetch(New ParameterList(Of Long)(idBinnacle.Values, idBinnacle.Priority), New ParameterList(Of String)(tableName.Values, tableName.Priority), New ParameterList(Of Long)(idOperation.Values, idOperation.Priority), New Parameter(Of Date)(fromHour.Value, fromHour.Priority), New Parameter(Of Date)(toHour.Value, toHour.Priority)))
        End Sub

        Private Sub Fetch(ByVal list As DAClsprgSubscriberBinnacleTables.Struct())
            Me.RaiseListChangedEvents = False
            For Each Struct As DAClsprgSubscriberBinnacleTables.Struct In list
                Me.Add(ClsBinnacleTable.GetChildBinnacleTable(Struct))
            Next
            Me.RaiseListChangedEvents = True
        End Sub

#End Region

#End Region

    End Class
End Namespace
