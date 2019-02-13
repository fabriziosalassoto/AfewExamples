Imports Csla
Imports SCT.DataAccess

Namespace Administrator
    <Serializable()> Public Class ClsBinnacleTable
        Inherits BusinessBase(Of ClsBinnacleTable)

#Region " Business Methods "

        Private mID As Long
        Private mBinnacle As ClsBinnacleInfo = ClsBinnacleInfo.NewBinnacleInfo
        Private mOperation As ClsOperationInfo = ClsOperationInfo.NewOperationInfo
        Private mTableName As String = String.Empty
        Private mBHour As Date = Date.MinValue
        Private mBinnacleTableFields As ClsBinnacleTableFieldList = ClsBinnacleTableFieldList.NewChildBinnacleTableFieldList

        Public ReadOnly Property ID() As Long
            Get
                CanReadProperty(True)
                Return Me.mID
            End Get
        End Property

        Public Property Binnacle() As ClsBinnacleInfo
            Get
                CanReadProperty(True)
                Return Me.mBinnacle
            End Get
            Set(ByVal value As ClsBinnacleInfo)
                CanWriteProperty(True)
                If value IsNot Nothing AndAlso value.ID <> 0 Then
                    If Me.mBinnacle.ID <> value.ID Then
                        Me.mBinnacle = value
                        ValidationRules.CheckRules("Binnacle")
                        PropertyHasChanged()
                    End If
                Else
                    Throw New System.Security.SecurityException("Binnacle required.")
                End If
            End Set
        End Property

        Public Property Operation() As ClsOperationInfo
            Get
                CanReadProperty(True)
                Return Me.mOperation
            End Get
            Set(ByVal value As ClsOperationInfo)
                CanWriteProperty(True)
                If value IsNot Nothing AndAlso value.ID <> 0 Then
                    If Me.mOperation.ID <> value.ID Then
                        Me.mOperation = value
                        ValidationRules.CheckRules("Operation")
                        PropertyHasChanged()
                    End If
                Else
                    Throw New System.Security.SecurityException("Operation required.")
                End If
            End Set
        End Property

        Public Property TableName() As String
            Get
                CanReadProperty(True)
                Return Me.mTableName
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mTableName <> value Then
                    Me.mTableName = value
                    ValidationRules.CheckRules("TableName")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property BHour() As Date
            Get
                CanReadProperty(True)
                Return Me.mBHour
            End Get
            Set(ByVal value As DateTime)
                CanWriteProperty(True)
                If Me.mBHour <> value Then
                    Me.mBHour = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public ReadOnly Property BinnacleTableFields() As ClsBinnacleTableFieldList
            Get
                CanReadProperty(True)
                Return Me.mBinnacleTableFields
            End Get
        End Property

        Public Overrides ReadOnly Property IsValid() As Boolean
            Get
                Return MyBase.IsValid AndAlso Me.mBinnacleTableFields.IsValid
            End Get
        End Property

        Public Overrides ReadOnly Property IsDirty() As Boolean
            Get
                Return MyBase.IsDirty OrElse Me.mBinnacleTableFields.IsDirty
            End Get
        End Property

        Public Sub AssignBinnacle(ByVal BinnacleId As Long)
            If BinnacleId <> 0 Then
                If Me.mBinnacle.ID <> BinnacleId Then
                    Me.mBinnacle = ClsBinnacleInfo.GetBinnacleInfo(BinnacleId)
                    PropertyHasChanged("Binnacle")
                End If
            Else
                Throw New System.Security.SecurityException("Binnacle required.")
            End If
        End Sub

        Public Sub AssignOperation(ByVal OperationId As Long)
            If OperationId <> 0 Then
                If Me.mOperation.ID <> OperationId Then
                    Me.mOperation = ClsOperationInfo.GetOperationInfo(OperationId)
                    PropertyHasChanged("Operation")
                End If
            Else
                Throw New System.Security.SecurityException("Operation required.")
            End If
        End Sub

        Protected Overrides Function GetIdValue() As Object
            Return Me.mID
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()
            ValidationRules.AddRule(AddressOf BinnacleRequired, "Binnacle")
            ValidationRules.AddRule(AddressOf OperationRequired, "Operation")

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, "TableName")
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("TableName", 100))
        End Sub

        Private Function BinnacleRequired(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
            If Me.mBinnacle.ID = 0 Then
                e.Description = "Binnacle required."
                Return False
            Else
                Return True
            End If
        End Function

        Private Function OperationRequired(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
            If Me.mOperation.ID = 0 Then
                e.Description = "Operation required."
                Return False
            Else
                Return True
            End If
        End Function

#End Region

#Region " Authorization Rules "

        'Protected Overrides Sub AddAuthorizationRules()
        '    AuthorizationRules.AllowRead("ID", " ")
        '    AuthorizationRules.AllowRead("Binnacle", " ")
        '    AuthorizationRules.AllowRead("Operation", " ")
        '    AuthorizationRules.AllowRead("TableName", " ")
        '    AuthorizationRules.AllowRead("BHour", " ")
        '    AuthorizationRules.AllowRead("BinnacleTableFields", "")
        '    AuthorizationRules.AllowWrite("TableName", New String() {" ", " "})
        '    AuthorizationRules.AllowWrite("BHour", New String() {" ", " "})
        'End Sub

        Public Shared Function CanAddObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole(" ")
        End Function

        Public Shared Function CanGetObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole(" ")
        End Function

        Public Shared Function CanDeleteObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole(" ")
        End Function

        Public Shared Function CanEditObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole(" ")
        End Function

#End Region

#Region " Factory Methods "

#Region " Root Methods "

        Public Shared Function NewBinnacleTable() As ClsBinnacleTable
            If Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add BinnacleTable records.")
            End If
            Return DataPortal.Create(Of ClsBinnacleTable)(New Criteria(0))
        End Function

        Public Shared Function GetBinnacleTable(ByVal ID As Long) As ClsBinnacleTable
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view BinnacleTable records.")
            End If
            Return DataPortal.Fetch(Of ClsBinnacleTable)(New Criteria(ID))
        End Function

        Public Shared Sub DeleteBinnacleTable(ByVal ID As Long)
            If Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove BinnacleTable records.")
            End If
            DataPortal.Delete(New Criteria(ID))
        End Sub

        Public Overrides Function Save() As ClsBinnacleTable
            If IsDeleted AndAlso Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove BinnacleTable records.")

            ElseIf IsNew AndAlso Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add BinnacleTable records.")

            ElseIf Not CanEditObject() Then
                Throw New System.Security.SecurityException("User not authorized to update BinnacleTable records.")
            End If
            Return MyBase.Save
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

#End Region

#Region " Child Methods "

        Friend Shared Function NewChildBinnacleTable() As ClsBinnacleTable
            Dim Child As New ClsBinnacleTable
            Child.ValidationRules.CheckRules()
            Child.MarkAsChild()
            Return Child
        End Function

        Friend Shared Function NewChildBinnacleTable(ByVal operation As ClsOperation, ByVal tableName As String, ByVal hour As Date, ByVal ParamArray tableFieldsData() As Object) As ClsBinnacleTable
            Dim Child As New ClsBinnacleTable
            Child.AssignOperation(operation.ID)
            Child.TableName = tableName
            Child.BHour = hour
            For Each tableFieldData As Object In tableFieldsData
                Child.BinnacleTableFields.AddNewItem(operation, tableFieldData)
            Next
            Child.MarkAsChild()
            Return Child
        End Function

        Friend Shared Function GetChildBinnacleTable(ByVal BinnacleTable As DAClsprgAdministrativeBinnacleTables.Struct) As ClsBinnacleTable
            Return New ClsBinnacleTable(BinnacleTable)
        End Function

        Private Sub New(ByVal BinnacleTable As DAClsprgAdministrativeBinnacleTables.Struct)
            MarkAsChild()
            Fetch(BinnacleTable)
        End Sub

#End Region

#End Region

#Region " Data Access "

#Region " Root Area "

        <Serializable()> Private Class Criteria

            Private mID As Long

            Public ReadOnly Property ID() As Long
                Get
                    Return Me.mID
                End Get
            End Property

            Public Sub New(ByVal ID As Long)
                Me.mID = ID
            End Sub

        End Class

        <RunLocal()> Private Overloads Sub DataPortal_Create(ByVal criteria As Criteria)
            Me.ValidationRules.CheckRules()
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            Dim List As DAClsprgAdministrativeBinnacleTables.Struct() = DAClsprgAdministrativeBinnacleTables.Fetch(New Parameter(Of Long)(criteria.ID, 0))
            If List.Length > 0 Then
                Me.mStruct = List(0)
                Me.LoadObjectData()
            Else
                Throw New System.Security.SecurityException("BinnacleTable record doesn't exists")
            End If
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Insert()
            Me.LoadTableStruct(New Object() {Me.mBinnacle, Me.mOperation})
            Me.mStruct = DAClsprgAdministrativeBinnacleTables.Insert(Me.mStruct)
            Me.mID = Me.mStruct.ID.Value
            Me.mBinnacleTableFields.Update(Me)
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Update()
            If MyBase.IsDirty Then
                Me.LoadTableStruct(New Object() {Me.mBinnacle, Me.mOperation})
                Me.mStruct = DAClsprgAdministrativeBinnacleTables.Update(Me.mStruct)
            End If
            Me.mBinnacleTableFields.Update(Me)
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_DeleteSelf()
            DataPortal_Delete(New Criteria(Me.mID))
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)
            Me.mBinnacleTableFields.Clear()
            Me.mBinnacleTableFields.Update(Me)
            DAClsprgAdministrativeBinnacleTables.Delete(criteria.ID)
        End Sub

#End Region

#Region " Child Area "

        Private Sub Fetch(ByVal BinnacleTable As DAClsprgAdministrativeBinnacleTables.Struct)
            Me.mStruct = BinnacleTable
            Me.LoadObjectData()
            MarkOld()
        End Sub

        Friend Sub Insert(ByVal parents() As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct(parents)
            Me.mStruct = DAClsprgAdministrativeBinnacleTables.Insert(Me.mStruct)
            Me.mID = Me.mStruct.ID.Value
            Me.mBinnacleTableFields.Update(Me)
            MarkOld()
        End Sub

        Friend Sub Update(ByVal parents() As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct(parents)
            Me.mStruct = DAClsprgAdministrativeBinnacleTables.Update(Me.mStruct)
            Me.mBinnacleTableFields.Update(Me)
            MarkOld()
        End Sub

        Friend Sub DeleteSelf()
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            ' if we're new then don't update the database
            If Me.IsNew Then Exit Sub

            Me.mBinnacleTableFields.Clear()
            Me.mBinnacleTableFields.Update(Me)
            DAClsprgAdministrativeBinnacleTables.Delete(Me.mID)
            MarkNew()
        End Sub

#End Region

#Region " Common Area "

        Private mStruct As DAClsprgAdministrativeBinnacleTables.Struct = New DAClsprgAdministrativeBinnacleTables.Struct

        Public Function GetTableStruct() As DAClsprgAdministrativeBinnacleTables.Struct
            Return Me.mStruct
        End Function

        ''' <summary>
        ''' Load the data of the object with the fetched data
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadObjectData()
            With Me.mStruct
                Me.mID = .ID.Value
                Me.mTableName = .BTableName.Value
                Me.mBHour = .BHour.Value
                Me.mBinnacle = ClsBinnacleInfo.GetBinnacleInfo(.IDBinnacle.Value)
                Me.mOperation = ClsOperationInfo.GetOperationInfo(.IDOperation.Value)
                Me.mBinnacleTableFields = ClsBinnacleTableFieldList.GetChildBinnacleTableFieldList(DAClsprgAdministrativeBinnacleTableFields.FetchByBinnacleTable(New Parameter(Of Long)(.ID.Value, 0)))
            End With
        End Sub

        ''' <summary>
        ''' Collect the data of the object to fill to a structure of data and returns the structure 
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadTableStruct(ByVal parents() As Object)
            With Me.mStruct
                .ID.NewValue = Me.mID
                .IDBinnacle.NewValue = parents(0).ID
                .IDOperation.NewValue = parents(1).ID
                .BTableName.NewValue = Me.mTableName
                .BHour.NewValue = Me.mBHour
            End With
        End Sub

#End Region

#End Region

#Region " Exists "

        Public Shared Function Exists(ByVal id As Long) As Boolean
            Return DataPortal.Execute(Of ExistsCommand)(New ExistsCommand(id)).Exists
        End Function

        <Serializable()> Private Class ExistsCommand
            Inherits CommandBase

            Private mID As Long
            Private mExists As Boolean

            Public ReadOnly Property Exists() As Boolean
                Get
                    Return Me.mExists
                End Get
            End Property

            Public Sub New(ByVal id As String)
                Me.mID = id
            End Sub

            Protected Overrides Sub DataPortal_Execute()
                Me.mExists = (DAClsprgAdministrativeBinnacleTables.Fetch(New Parameter(Of Long)(Me.mID, 0)).Length > 0)
            End Sub

        End Class

#End Region

    End Class
End Namespace