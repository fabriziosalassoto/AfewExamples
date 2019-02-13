Imports Csla
Imports SCT.DataAccess

Namespace Administrator
    <Serializable()> Public Class ClsBinnacleForm
        Inherits BusinessBase(Of ClsBinnacleForm)

#Region " Business Methods "

        Private mID As Long
        Private mBinnacle As ClsBinnacleInfo = ClsBinnacleInfo.NewBinnacleInfo
        Private mForm As ClsFormInfo = ClsFormInfo.NewFormInfo
        Private mOperation As ClsOperationInfo = ClsOperationInfo.NewOperationInfo
        Private mBHour As Date = Date.MinValue
        Private mBinnacleFormFields As ClsBinnacleFormFieldList = ClsBinnacleFormFieldList.NewChildBinnacleFormFieldList

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

        Public Property Form() As ClsFormInfo
            Get
                CanReadProperty(True)
                Return Me.mForm
            End Get
            Set(ByVal value As ClsFormInfo)
                CanWriteProperty(True)
                If value IsNot Nothing AndAlso value.ID <> 0 Then
                    If Me.mForm.ID <> value.ID Then
                        Me.mForm = value
                        ValidationRules.CheckRules("Form")
                        PropertyHasChanged()
                    End If
                Else
                    Throw New System.Security.SecurityException("Form required.")
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

        Public Property BHour() As Date
            Get
                CanReadProperty(True)
                Return Me.mBHour
            End Get
            Set(ByVal value As Date)
                CanWriteProperty(True)
                If Me.mBHour <> value Then
                    Me.mBHour = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public ReadOnly Property BinnacleFormFields() As ClsBinnacleFormFieldList
            Get
                CanReadProperty(True)
                Return Me.mBinnacleFormFields
            End Get
        End Property

        Public Overrides ReadOnly Property IsValid() As Boolean
            Get
                Return MyBase.IsValid AndAlso Me.mBinnacleFormFields.IsValid
            End Get
        End Property

        Public Overrides ReadOnly Property IsDirty() As Boolean
            Get
                Return MyBase.IsDirty OrElse Me.mBinnacleFormFields.IsDirty
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

        Public Sub AssignForm(ByVal FormId As Long)
            If FormId <> 0 Then
                If Me.mForm.ID <> FormId Then
                    Me.mForm = ClsFormInfo.GetFormInfo(FormId)
                    PropertyHasChanged("Form")
                End If
            Else
                Throw New System.Security.SecurityException("Form required.")
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
            ValidationRules.AddRule(AddressOf FormRequired, "Form")
            ValidationRules.AddRule(AddressOf OperationRequired, "Operation")
        End Sub

        Private Function BinnacleRequired(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
            If Me.mBinnacle.ID = 0 Then
                e.Description = "Binnacle required."
                Return False
            Else
                Return True
            End If
        End Function

        Private Function FormRequired(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
            If Me.mForm.ID = 0 Then
                e.Description = "Form required."
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
        '    AuthorizationRules.AllowRead("Form", " ")
        '    AuthorizationRules.AllowRead("Operation", " ")
        '    AuthorizationRules.AllowRead("BHour", " ")
        '    AuthorizationRules.AllowRead("BinnacleFormFields", "")
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

        Public Shared Function NewBinnacleForm() As ClsBinnacleForm
            If Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add BinnacleForm records.")
            End If
            Return DataPortal.Create(Of ClsBinnacleForm)(New Criteria(0))
        End Function

        Public Shared Function GetBinnacleForm(ByVal ID As Long) As ClsBinnacleForm
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view BinnacleForm records.")
            End If
            Return DataPortal.Fetch(Of ClsBinnacleForm)(New Criteria(ID))
        End Function

        Public Shared Sub DeleteBinnacleForm(ByVal ID As Long)
            If Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove BinnacleForm records.")
            End If
            DataPortal.Delete(New Criteria(ID))
        End Sub

        Public Overrides Function Save() As ClsBinnacleForm
            If IsDeleted AndAlso Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove BinnacleForm records.")

            ElseIf IsNew AndAlso Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add BinnacleForm records.")

            ElseIf Not CanEditObject() Then
                Throw New System.Security.SecurityException("User not authorized to update BinnacleForm records.")
            End If
            Return MyBase.Save
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

#End Region

#Region " Child Methods "

        Friend Shared Function NewChildBinnacleForm() As ClsBinnacleForm
            Dim Child As New ClsBinnacleForm
            Child.ValidationRules.CheckRules()
            Child.MarkAsChild()
            Return Child
        End Function

        Friend Shared Function NewChildBinnacleForm(ByVal form As ClsForm, ByVal operation As ClsOperation, ByVal hour As Date, ByVal ParamArray formFieldsData() As Object) As ClsBinnacleForm
            Dim Child As New ClsBinnacleForm
            Child.AssignOperation(operation.ID)
            Child.AssignForm(form.ID)
            Child.BHour = hour
            For Each formFieldData As Object In formFieldsData
                Child.BinnacleFormFields.AddNewItem(form.Groups.GetField(formFieldData.Name), operation, formFieldData)
            Next
            Child.MarkAsChild()
            Return Child
        End Function

        Friend Shared Function GetChildBinnacleForm(ByVal BinnacleForm As DAClsprgAdministrativeBinnacleForms.Struct) As ClsBinnacleForm
            Return New ClsBinnacleForm(BinnacleForm)
        End Function

        Private Sub New(ByVal BinnacleForm As DAClsprgAdministrativeBinnacleForms.Struct)
            MarkAsChild()
            Fetch(BinnacleForm)
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
            Dim List As DAClsprgAdministrativeBinnacleForms.Struct() = DAClsprgAdministrativeBinnacleForms.Fetch(New Parameter(Of Long)(criteria.ID, 0))
            If List.Length > 0 Then
                Me.mStruct = List(0)
                Me.LoadObjectData()
            Else
                Throw New System.Security.SecurityException("Log Entry Detail doesn't exists")
            End If
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Insert()
            Me.LoadTableStruct(New Object() {Me.mBinnacle, Me.mForm, Me.mOperation})
            Me.mStruct = DAClsprgAdministrativeBinnacleForms.Insert(Me.mStruct)
            Me.mID = Me.mStruct.ID.Value
            Me.mBinnacleFormFields.Update(Me)
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Update()
            If MyBase.IsDirty Then
                Me.LoadTableStruct(New Object() {Me.mBinnacle, Me.mForm, Me.mOperation})
                Me.mStruct = DAClsprgAdministrativeBinnacleForms.Update(Me.mStruct)
            End If
            Me.mBinnacleFormFields.Update(Me)
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_DeleteSelf()
            DataPortal_Delete(New Criteria(Me.mID))
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)
            DAClsprgAdministrativeBinnacleForms.Delete(criteria.ID)
        End Sub

#End Region

#Region " Child Area "

        Private Sub Fetch(ByVal BinnacleForm As DAClsprgAdministrativeBinnacleForms.Struct)
            Me.mStruct = BinnacleForm
            Me.LoadObjectData()
            MarkOld()
        End Sub

        Friend Sub Insert(ByVal parents() As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct(parents)
            Me.mStruct = DAClsprgAdministrativeBinnacleForms.Insert(Me.mStruct)
            Me.mID = Me.mStruct.ID.Value
            Me.mBinnacleFormFields.Update(Me)
            MarkOld()
        End Sub

        Friend Sub Update(ByVal parents() As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct(parents)
            Me.mStruct = DAClsprgAdministrativeBinnacleForms.Update(Me.mStruct)
            Me.mBinnacleFormFields.Update(Me)
            MarkOld()
        End Sub

        Friend Sub DeleteSelf()
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            ' if we're new then don't update the database
            If Me.IsNew Then Exit Sub

            DAClsprgAdministrativeBinnacleForms.Delete(Me.mID)
            MarkNew()
        End Sub

#End Region

#Region " Common Area "

        Private mStruct As DAClsprgAdministrativeBinnacleForms.Struct = New DAClsprgAdministrativeBinnacleForms.Struct

        Public Function GetTableStruct() As DAClsprgAdministrativeBinnacleForms.Struct
            Return Me.mStruct
        End Function

        ''' <summary>
        ''' Load the data of the object with the fetched data
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadObjectData()
            With Me.mStruct
                Me.mID = .ID.Value
                Me.mBHour = .BHour.Value
                Me.mBinnacle = ClsBinnacleInfo.GetBinnacleInfo(.IDBinnacle.Value)
                Me.mForm = ClsFormInfo.GetFormInfo(.IDForm.Value)
                Me.mOperation = ClsOperationInfo.GetOperationInfo(.IDOperation.Value)
                Me.mBinnacleFormFields = ClsBinnacleFormFieldList.GetChildBinnacleFormFieldList(DAClsprgAdministrativeBinnacleFormFields.FetchByBinnacleForm(New Parameter(Of Long)(.ID.Value, 0)))
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
                .IDForm.NewValue = parents(1).ID
                .IDOperation.NewValue = parents(2).ID
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
                Me.mExists = (DAClsprgAdministrativeBinnacleForms.Fetch(New Parameter(Of Long)(Me.mID, 0)).Length > 0)
            End Sub

        End Class

#End Region

    End Class
End Namespace

