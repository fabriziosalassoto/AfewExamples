Imports Csla
Imports SCT.DataAccess

Namespace Administrator
    <Serializable()> Public Class ClsBinnacleTableInfoList
        Inherits ReadOnlyListBase(Of ClsBinnacleTableInfoList, ClsBinnacleTableInfo)

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole(" ")
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function NewBinnacleTableInfoList() As ClsBinnacleTableInfoList
            Return New ClsBinnacleTableInfoList
        End Function

        Public Shared Function GetBinnacleTableInfoList() As ClsBinnacleTableInfoList
            Return DataPortal.Fetch(Of ClsBinnacleTableInfoList)(New Criteria)
        End Function

        Public Shared Function GetBinnacleTableInfoList(ByVal idBinnacle As SearchCriteria(Of Long), ByVal idOperation As SearchCriteria(Of Long), ByVal tableName As SearchCriteria(Of String), ByVal fromHour As SearchCriteria(Of Date), ByVal toHour As SearchCriteria(Of Date)) As ClsBinnacleTableInfoList
            Return DataPortal.Fetch(Of ClsBinnacleTableInfoList)(New FilteredCriteria(idBinnacle, idOperation, tableName, fromHour, toHour))
        End Function

        Public Shared Function GetBinnacleTableInfoList(ByVal idBinnacle As SearchCriteriaList(Of Long), ByVal idOperation As SearchCriteriaList(Of Long), ByVal tableName As SearchCriteriaList(Of String), ByVal fromHour As SearchCriteria(Of Date), ByVal toHour As SearchCriteria(Of Date)) As ClsBinnacleTableInfoList
            Return DataPortal.Fetch(Of ClsBinnacleTableInfoList)(New FilteredCriteriaList(idBinnacle, idOperation, tableName, fromHour, toHour))
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

        Private Sub Fetch()
            Fetch(DAClsprgAdministrativeBinnacleTables.Fetch())
        End Sub

        Private Sub Fetch(ByVal idBinnacle As SearchCriteria(Of Long), ByVal idOperation As SearchCriteria(Of Long), ByVal tableName As SearchCriteria(Of String), ByVal fromHour As SearchCriteria(Of Date), ByVal toHour As SearchCriteria(Of Date))
            Fetch(DAClsprgAdministrativeBinnacleTables.Fetch(New Parameter(Of Long)(idBinnacle.Value, idBinnacle.Priority), New Parameter(Of String)(tableName.Value, tableName.Priority), New Parameter(Of Long)(idOperation.Value, idOperation.Priority), New Parameter(Of Date)(fromHour.Value, fromHour.Priority), New Parameter(Of Date)(toHour.Value, toHour.Priority)))
        End Sub

        Private Sub Fetch(ByVal idBinnacle As SearchCriteriaList(Of Long), ByVal idOperation As SearchCriteriaList(Of Long), ByVal tableName As SearchCriteriaList(Of String), ByVal fromHour As SearchCriteria(Of Date), ByVal toHour As SearchCriteria(Of Date))
            Fetch(DAClsprgAdministrativeBinnacleTables.Fetch(New ParameterList(Of Long)(idBinnacle.Values, idBinnacle.Priority), New ParameterList(Of String)(tableName.Values, tableName.Priority), New ParameterList(Of Long)(idOperation.Values, idOperation.Priority), New Parameter(Of Date)(fromHour.Value, fromHour.Priority), New Parameter(Of Date)(toHour.Value, toHour.Priority)))
        End Sub

        Private Sub Fetch(ByVal list As DAClsprgAdministrativeBinnacleTables.Struct())
            RaiseListChangedEvents = False
            IsReadOnly = False
            For Each Struct As DAClsprgAdministrativeBinnacleTables.Struct In list
                Me.Add(ClsBinnacleTableInfo.GetBinnacleTableInfo(Struct))
            Next
            IsReadOnly = True
            RaiseListChangedEvents = True
        End Sub

#End Region

    End Class
End Namespace