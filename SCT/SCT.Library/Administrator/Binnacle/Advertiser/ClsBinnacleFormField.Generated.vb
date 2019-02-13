Imports Csla
Imports SCT.DataAccess

Namespace Advertiser
    <Serializable()> Public Class ClsBinnacleFormField
        Inherits BusinessBase(Of ClsBinnacleFormField)

#Region " Business Methods "

        Private mID As Long
        Private mBinnacleForm As ClsBinnacleFormInfo = ClsBinnacleFormInfo.NewBinnacleFormInfo
        Private mField As ClsFieldInfo = ClsFieldInfo.NewFieldInfo
        Private mOldValue As String = String.Empty
        Private mNewValue As String = String.Empty

        Public ReadOnly Property ID() As Long
            Get
                CanReadProperty(True)
                Return Me.mID
            End Get
        End Property

        Public Property BinnacleForm() As ClsBinnacleFormInfo
            Get
                CanReadProperty(True)
                Return Me.mBinnacleForm
            End Get
            Set(ByVal value As ClsBinnacleFormInfo)
                CanWriteProperty(True)
                If value IsNot Nothing AndAlso value.ID <> 0 Then
                    If Me.mBinnacleForm.ID <> value.ID Then
                        Me.mBinnacleForm = value
                        Me.ValidationRules.CheckRules("BinnacleForm")
                        PropertyHasChanged()
                    End If
                Else
                    Throw New System.Security.SecurityException("BinnacleForm required.")
                End If
            End Set
        End Property

        Public Property Field() As ClsFieldInfo
            Get
                CanReadProperty(True)
                Return Me.mField
            End Get
            Set(ByVal value As ClsFieldInfo)
                CanWriteProperty(True)
                If value IsNot Nothing AndAlso value.ID <> 0 Then
                    If Me.mField.ID <> value.ID Then
                        Me.mField = value
                        ValidationRules.CheckRules("Field")
                        PropertyHasChanged()
                    End If
                Else
                    Throw New System.Security.SecurityException("Field required.")
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

        Public Sub AssignBinnacleForm(ByVal binnacleFormId As Long)
            If binnacleFormId <> 0 Then
                If Me.mBinnacleForm.ID <> binnacleFormId Then
                    Me.mBinnacleForm = ClsBinnacleFormInfo.GetBinnacleFormInfo(binnacleFormId)
                    PropertyHasChanged("BinnacleForm")
                End If
            Else
                Throw New System.Security.SecurityException("BinnacleForm required.")
            End If
        End Sub

        Public Sub AssignField(ByVal fieldId As Long)
            If fieldId <> 0 Then
                If Me.mField.ID <> fieldId Then
                    Me.mField = ClsFieldInfo.GetFieldInfo(fieldId)
                    PropertyHasChanged("Field")
                End If
            Else
                Throw New System.Security.SecurityException("Field required.")
            End If
        End Sub

        Protected Overrides Function GetIdValue() As Object
            Return Me.mID
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()
            ValidationRules.AddRule(AddressOf BinnacleFormRequired, "BinnacleForm")
            ValidationRules.AddRule(AddressOf FieldRequired, "Field")
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("OldValue", 100))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("NewValue", 100))
        End Sub

        Private Function BinnacleFormRequired(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
            If Me.mBinnacleForm.ID = 0 Then
                e.Description = "BinnacleForm required."
                Return False
            Else
                Return True
            End If
        End Function

        Private Function FieldRequired(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
            If Me.mField.ID = 0 Then
                e.Description = "Form required."
                Return False
            Else
                Return True
            End If
        End Function

#End Region

#Region " Authorization Rules "

        'Protected Overrides Sub AddAuthorizationRules()
        '    AuthorizationRules.AllowRead("ID", " ")
        '    AuthorizationRules.AllowRead("BinnacleForm", " ")
        '    AuthorizationRules.AllowRead("Field", " ")
        '    AuthorizationRules.AllowRead("OldValue", " ")
        '    AuthorizationRules.AllowRead("NewValue", " ")
        '    AuthorizationRules.AllowWrite("BinnacleForm", New String() {" ", " "})
        '    AuthorizationRules.AllowWrite("Field", New String() {" ", " "})
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

        Public Shared Function NewBinnacleFormField() As ClsBinnacleFormField
            If Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add BinnacleFormField records.")
            End If
            Return DataPortal.Create(Of ClsBinnacleFormField)(New Criteria(0))
        End Function

        Public Shared Function GetBinnacleFormField(ByVal ID As Long) As ClsBinnacleFormField
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view BinnacleFormField records.")
            End If
            Return DataPortal.Fetch(Of ClsBinnacleFormField)(New Criteria(ID))
        End Function

        Public Shared Sub DeleteBinnacleFormField(ByVal ID As Long)
            If Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove BinnacleFormField records.")
            End If
            DataPortal.Delete(New Criteria(ID))
        End Sub

        Public Overrides Function Save() As ClsBinnacleFormField
            If IsDeleted AndAlso Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove BinnacleFormField records.")

            ElseIf IsNew AndAlso Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add BinnacleFormField records.")

            ElseIf Not CanEditObject() Then
                Throw New System.Security.SecurityException("User not authorized to update BinnacleFormField records.")
            End If
            Return MyBase.Save
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

#End Region

#Region " Child Methods "

        Friend Shared Function NewChildBinnacleFormField() As ClsBinnacleFormField
            Dim Child As New ClsBinnacleFormField
            Child.ValidationRules.CheckRules()
            Child.MarkAsChild()
            Return Child
        End Function

        Friend Shared Function NewChildBinnacleFormField(ByVal fieldID As Long, ByVal oldValue As String, ByVal newValue As String) As ClsBinnacleFormField
            Dim Child As New ClsBinnacleFormField
            Child.AssignField(fieldID)
            Child.OldValue = oldValue
            Child.NewValue = newValue
            Child.MarkAsChild()
            Return Child
        End Function

        Friend Shared Function GetChildBinnacleFormField(ByVal BinnacleFormField As DAClsprgAdvertiserBinnacleFormFields.Struct) As ClsBinnacleFormField
            Return New ClsBinnacleFormField(BinnacleFormField)
        End Function

        Private Sub New(ByVal BinnacleFormField As DAClsprgAdvertiserBinnacleFormFields.Struct)
            MarkAsChild()
            Fetch(BinnacleFormField)
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
            Dim List As DAClsprgAdvertiserBinnacleFormFields.Struct() = DAClsprgAdvertiserBinnacleFormFields.Fetch(New Parameter(Of Long)(criteria.ID, 0))
            If List.Length > 0 Then
                Me.mStruct = List(0)
                Me.LoadObjectData()
            Else
                Throw New System.Security.SecurityException("BinnacleFormField record doesn't exists")
            End If
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Insert()
            Me.LoadTableStruct(New Object() {Me.mBinnacleForm, Me.mField})
            Me.mStruct = DAClsprgAdvertiserBinnacleFormFields.Insert(Me.mStruct)
            Me.mID = Me.mStruct.ID.Value
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Update()
            If MyBase.IsDirty Then
                Me.LoadTableStruct(New Object() {Me.mBinnacleForm, Me.mField})
                Me.mStruct = DAClsprgAdvertiserBinnacleFormFields.Update(Me.mStruct)
            End If
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_DeleteSelf()
            DataPortal_Delete(New Criteria(Me.mID))
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)
            DAClsprgAdvertiserBinnacleFormFields.Delete(criteria.ID)
        End Sub

#End Region

#Region " Child Area "

        Private Sub Fetch(ByVal BinnacleFormField As DAClsprgAdvertiserBinnacleFormFields.Struct)
            Me.mStruct = BinnacleFormField
            Me.LoadObjectData()
            MarkOld()
        End Sub

        Friend Sub Insert(ByVal parents() As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct(parents)
            Me.mStruct = DAClsprgAdvertiserBinnacleFormFields.Insert(Me.mStruct)
            Me.mID = Me.mStruct.ID.Value
            MarkOld()
        End Sub

        Friend Sub Update(ByVal parents() As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct(parents)
            Me.mStruct = DAClsprgAdvertiserBinnacleFormFields.Update(Me.mStruct)
            MarkOld()
        End Sub

        Friend Sub DeleteSelf()
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            ' if we're new then don't update the database
            If Me.IsNew Then Exit Sub

            DAClsprgAdvertiserBinnacleFormFields.Delete(Me.mID)
            MarkNew()
        End Sub

#End Region

#Region " Common Area "

        Private mStruct As DAClsprgAdvertiserBinnacleFormFields.Struct = New DAClsprgAdvertiserBinnacleFormFields.Struct

        Public Function GetTableStruct() As DAClsprgAdvertiserBinnacleFormFields.Struct
            Return Me.mStruct
        End Function

        ''' <summary>
        ''' Load the data of the object with the fetched data
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadObjectData()
            With Me.mStruct
                Me.mID = .ID.Value
                Me.mBinnacleForm = ClsBinnacleFormInfo.GetBinnacleFormInfo(.IDBinnacleForm.Value)
                Me.mField = ClsFieldInfo.GetFieldInfo(.IDField.Value)
                Me.mOldValue = .OldValue.Value
                Me.mNewValue = .NewValue.Value
            End With
        End Sub

        ''' <summary>
        ''' Collect the data of the object to fill to a structure of data and returns the structure 
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadTableStruct(ByVal parents() As Object)
            With Me.mStruct
                .ID.NewValue = Me.mID
                .IDBinnacleForm.NewValue = parents(0).ID
                .IDField.NewValue = parents(1).ID
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
                Me.mExists = (DAClsprgAdvertiserBinnacleFormFields.Fetch(New Parameter(Of Long)(Me.mID, 0)).Length > 0)
            End Sub

        End Class

#End Region

    End Class
End Namespace
