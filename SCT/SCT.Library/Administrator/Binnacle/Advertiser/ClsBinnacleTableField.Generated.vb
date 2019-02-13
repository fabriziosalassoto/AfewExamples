Imports Csla
Imports SCT.DataAccess

Namespace Advertiser
    <Serializable()> Public Class ClsBinnacleTableField
        Inherits BusinessBase(Of ClsBinnacleTableField)

#Region " Business Methods "

        Private mID As Long
        Private mBinnacleTable As ClsBinnacleTableInfo = ClsBinnacleTableInfo.NewBinnacleTableInfo
        Private mFieldName As String = String.Empty
        Private mOldValue As String = String.Empty
        Private mNewValue As String = String.Empty

        Public ReadOnly Property ID() As Long
            Get
                CanReadProperty(True)
                Return Me.mID
            End Get
        End Property

        Public Property BinnacleTable() As ClsBinnacleTableInfo
            Get
                CanReadProperty(True)
                Return Me.mBinnacleTable
            End Get
            Set(ByVal value As ClsBinnacleTableInfo)
                CanWriteProperty(True)
                If value IsNot Nothing AndAlso value.ID <> 0 Then
                    If Me.mBinnacleTable.ID <> value.ID Then
                        Me.mBinnacleTable = value
                        Me.ValidationRules.CheckRules("BinnacleForm")
                        PropertyHasChanged()
                    End If
                Else
                    Throw New System.Security.SecurityException("BinnacleTable required.")
                End If
            End Set
        End Property

        Public Property FieldName() As String
            Get
                CanReadProperty(True)
                Return Me.mFieldName
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mFieldName <> value Then
                    Me.mFieldName = value
                    ValidationRules.CheckRules("FieldName")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property OldValue() As String
            Get
                CanReadProperty(True)
                Return Me.mOldValue
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mOldValue <> value Then
                    Me.mOldValue = value
                    ValidationRules.CheckRules("OldValue")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property NewValue() As String
            Get
                CanReadProperty(True)
                Return Me.mNewValue
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mNewValue <> value Then
                    Me.mNewValue = value
                    ValidationRules.CheckRules("NewValue")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Overrides ReadOnly Property IsValid() As Boolean
            Get
                Return MyBase.IsValid
            End Get
        End Property

        Public Overrides ReadOnly Property IsDirty() As Boolean
            Get
                Return MyBase.IsDirty
            End Get
        End Property

        Public Sub AssignBinnacleTable(ByVal BinnacleTableId As Long)
            If BinnacleTableId <> 0 Then
                If Me.mBinnacleTable.ID <> BinnacleTableId Then
                    Me.mBinnacleTable = ClsBinnacleTableInfo.GetBinnacleTableInfo(BinnacleTableId)
                    PropertyHasChanged("BinnacleTable")
                End If
            Else
                Throw New System.Security.SecurityException("BinnacleTable required.")
            End If
        End Sub

        Protected Overrides Function GetIdValue() As Object
            Return Me.mID
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()
            ValidationRules.AddRule(AddressOf BinnacleTableRequired, "BinnacleTable")
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, "FieldName")
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("FieldName", 100))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("OldValue", 100))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("NewValue", 100))
        End Sub

        Private Function BinnacleTableRequired(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
            If Me.mBinnacleTable.ID = 0 Then
                e.Description = "BinnacleTable required."
                Return False
            Else
                Return True
            End If
        End Function

#End Region

#Region " Authorization Rules "

        'Protected Overrides Sub AddAuthorizationRules()
        '    AuthorizationRules.AllowRead("ID", " ")
        '    AuthorizationRules.AllowRead("BinnacleTable", " ")
        '    AuthorizationRules.AllowRead("FieldName", " ")
        '    AuthorizationRules.AllowRead("OldValue", " ")
        '    AuthorizationRules.AllowRead("NewValue", " ")
        '    AuthorizationRules.AllowWrite("BinnacleTable", New String() {" ", " "})
        '    AuthorizationRules.AllowWrite("FieldName", New String() {" ", " "})
        '    AuthorizationRules.AllowWrite("OldValue", New String() {" ", " "})
        '    AuthorizationRules.AllowWrite("NewValue", New String() {" ", " "})
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

        Public Shared Function NewBinnacleTableField() As ClsBinnacleTableField
            If Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add BinnacleTableField records.")
            End If
            Return DataPortal.Create(Of ClsBinnacleTableField)(New Criteria(0))
        End Function

        Public Shared Function GetBinnacleTableField(ByVal ID As Long) As ClsBinnacleTableField
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view BinnacleTableField records.")
            End If
            Return DataPortal.Fetch(Of ClsBinnacleTableField)(New Criteria(ID))
        End Function

        Public Shared Sub DeleteBinnacleTableField(ByVal ID As Long)
            If Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove BinnacleTableField records.")
            End If
            DataPortal.Delete(New Criteria(ID))
        End Sub

        Public Overrides Function Save() As ClsBinnacleTableField
            If IsDeleted AndAlso Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove BinnacleTableField records.")

            ElseIf IsNew AndAlso Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add BinnacleTableField records.")

            ElseIf Not CanEditObject() Then
                Throw New System.Security.SecurityException("User not authorized to update BinnacleTableField records.")
            End If
            Return MyBase.Save
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

#End Region

#Region " Child Methods "

        Friend Shared Function NewChildBinnacleTableField() As ClsBinnacleTableField
            Dim Child As New ClsBinnacleTableField
            Child.ValidationRules.CheckRules()
            Child.MarkAsChild()
            Return Child
        End Function

        Friend Shared Function NewChildBinnacleTableField(ByVal fieldName As String, ByVal oldValue As String, ByVal newValue As String) As ClsBinnacleTableField
            Dim Child As New ClsBinnacleTableField
            Child.FieldName = fieldName
            Child.OldValue = oldValue
            Child.NewValue = newValue
            Child.MarkAsChild()
            Return Child
        End Function

        Friend Shared Function GetChildBinnacleTableField(ByVal BinnacleTableField As DAClsprgAdvertiserBinnacleTableFields.Struct) As ClsBinnacleTableField
            Return New ClsBinnacleTableField(BinnacleTableField)
        End Function

        Private Sub New(ByVal BinnacleTableField As DAClsprgAdvertiserBinnacleTableFields.Struct)
            MarkAsChild()
            Fetch(BinnacleTableField)
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
            Dim List As DAClsprgAdvertiserBinnacleTableFields.Struct() = DAClsprgAdvertiserBinnacleTableFields.Fetch(New Parameter(Of Long)(criteria.ID, 0))
            If List.Length > 0 Then
                Me.mStruct = List(0)
                Me.LoadObjectData()
            Else
                Throw New System.Security.SecurityException("BinnacleTableField record doesn't exists")
            End If
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Insert()
            Me.LoadTableStruct(New Object() {Me.mBinnacleTable})
            Me.mStruct = DAClsprgAdvertiserBinnacleTableFields.Insert(Me.mStruct)
            Me.mID = Me.mStruct.ID.Value
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Update()
            If MyBase.IsDirty Then
                Me.LoadTableStruct(New Object() {Me.mBinnacleTable})
                Me.mStruct = DAClsprgAdvertiserBinnacleTableFields.Update(Me.mStruct)
            End If
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_DeleteSelf()
            DataPortal_Delete(New Criteria(Me.mID))
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)
            DAClsprgAdvertiserBinnacleTableFields.Delete(criteria.ID)
        End Sub

#End Region

#Region " Child Area "

        Private Sub Fetch(ByVal BinnacleTableField As DAClsprgAdvertiserBinnacleTableFields.Struct)
            Me.mStruct = BinnacleTableField
            Me.LoadObjectData()
            MarkOld()
        End Sub

        Friend Sub Insert(ByVal parent As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct(New Object() {parent})
            Me.mStruct = DAClsprgAdvertiserBinnacleTableFields.Insert(Me.mStruct)
            Me.mID = Me.mStruct.ID.Value
            MarkOld()
        End Sub

        Friend Sub Update(ByVal parent As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct(New Object() {parent})
            Me.mStruct = DAClsprgAdvertiserBinnacleTableFields.Update(Me.mStruct)
            MarkOld()
        End Sub

        Friend Sub DeleteSelf()
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            ' if we're new then don't update the database
            If Me.IsNew Then Exit Sub

            DAClsprgAdvertiserBinnacleTableFields.Delete(Me.mID)
            MarkNew()
        End Sub

#End Region

#Region " Common Area "

        Private mStruct As DAClsprgAdvertiserBinnacleTableFields.Struct = New DAClsprgAdvertiserBinnacleTableFields.Struct

        Public Function GetTableStruct() As DAClsprgAdvertiserBinnacleTableFields.Struct
            Return Me.mStruct
        End Function

        ''' <summary>
        ''' Load the data of the object with the fetched data
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadObjectData()
            With Me.mStruct
                Me.mID = .ID.Value
                Me.mFieldName = .FieldName.Value
                Me.mOldValue = .OldValue.Value
                Me.mNewValue = .NewValue.Value
                Me.mBinnacleTable = ClsBinnacleTableInfo.GetBinnacleTableInfo(.IDBinnacleTable.Value)
            End With
        End Sub

        ''' <summary>
        ''' Collect the data of the object to fill to a structure of data and returns the structure 
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadTableStruct(ByVal parents() As Object)
            With Me.mStruct
                .ID.NewValue = Me.mID
                .IDBinnacleTable.NewValue = parents(0).ID
                .FieldName.NewValue = Me.mFieldName
                .OldValue.NewValue = Me.mOldValue
                .NewValue.NewValue = Me.mNewValue
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
                Me.mExists = (DAClsprgAdvertiserBinnacleTableFields.Fetch(New Parameter(Of Long)(Me.mID, 0)).Length > 0)
            End Sub

        End Class

#End Region

    End Class
End Namespace