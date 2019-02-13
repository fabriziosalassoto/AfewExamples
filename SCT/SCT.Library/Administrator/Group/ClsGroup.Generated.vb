Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsGroup
    Inherits BusinessBase(Of ClsGroup)

#Region " Business Methods "

    Private mID As Long
    Private mForm As ClsFormInfo = ClsFormInfo.NewFormInfo
    Private mDescription As String = String.Empty
    Private mProfiles As ClsGroupProfileList = ClsGroupProfileList.NewGroupProfileList
    Private mFields As ClsFieldList = ClsFieldList.NewGroupFieldList

    Public ReadOnly Property ID() As Long
        Get
            CanReadProperty(True)
            Return Me.mID
        End Get
    End Property

    Public Property Description() As String
        Get
            CanReadProperty(True)
            Return Me.mDescription
        End Get
        Set(ByVal value As String)
            CanWriteProperty(True)
            If Me.mDescription <> value Then
                Me.mDescription = value
                ValidationRules.CheckRules("Description")
                PropertyHasChanged()
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

    Public ReadOnly Property Fields() As ClsFieldList
        Get
            CanReadProperty(True)
            Return Me.mFields
        End Get
    End Property

    Public ReadOnly Property Profiles() As ClsGroupProfileList
        Get
            CanReadProperty(True)
            Return Me.mProfiles
        End Get
    End Property

    Public Overrides ReadOnly Property IsValid() As Boolean
        Get
            Return MyBase.IsValid AndAlso Me.mFields.IsValid AndAlso Me.mProfiles.IsValid
        End Get
    End Property

    Public Overrides ReadOnly Property IsDirty() As Boolean
        Get
            Return MyBase.IsDirty OrElse Me.mFields.IsDirty OrElse Me.mProfiles.IsDirty
        End Get
    End Property

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

    Protected Overrides Function GetIdValue() As Object
        Return Me.mID
    End Function

    Public Function CanExecuteInField(ByVal fieldDescription As String, ByVal pName As String) As Boolean
        Return Me.mFields.Contains(fieldDescription) AndAlso Me.mProfiles.CanExecute(pName)
    End Function

#End Region

#Region " Validation Rules "

    Protected Overrides Sub AddBusinessRules()
        ValidationRules.AddRule(AddressOf FormRequired, "Form")

        ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, "Description")
        ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("Description", 100))
        ValidationRules.AddRule(AddressOf ExistsDescriptionInForm, "Description")
    End Sub

    Private Function ExistsDescriptionInForm(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
        If ClsGroup.Exists(Me.mID, Me.Form.ID, Me.mDescription) Then
            e.Description = "Description already assigned to another Groups in Form."
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

#End Region

#Region " Authorization Rules "

    'Protected Overrides Sub AddAuthorizationRules()
    '    AuthorizationRules.AllowRead("ID", " ")
    '    AuthorizationRules.AllowRead("IDForm", " ")
    '    AuthorizationRules.AllowRead("Description", " ")
    '    AuthorizationRules.AllowRead("Fields", "")
    '    AuthorizationRules.AllowRead("Profiles", "")
    '    AuthorizationRules.AllowWrite("Description", New String() {" ", " "})
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

    Public Shared Function NewGroup() As ClsGroup
        If Not CanAddObject() Then
            Throw New System.Security.SecurityException("User not authorized to add Group records.")
        End If
        Return DataPortal.Create(Of ClsGroup)(New Criteria(0))
    End Function

    Public Shared Function GetGroup(ByVal ID As Long) As ClsGroup
        If Not CanGetObject() Then
            Throw New System.Security.SecurityException("User not authorized to view Group records.")
        End If
        Return DataPortal.Fetch(Of ClsGroup)(New Criteria(ID))
    End Function

    Public Shared Sub DeleteGroup(ByVal ID As Long)
        If Not CanDeleteObject() Then
            Throw New System.Security.SecurityException("User not authorized to remove Group records.")
        End If
        DataPortal.Delete(New Criteria(ID))
    End Sub

    Public Overrides Function Save() As ClsGroup
        If IsDeleted AndAlso Not CanDeleteObject() Then
            Throw New System.Security.SecurityException("User not authorized to remove Group records.")

        ElseIf IsNew AndAlso Not CanAddObject() Then
            Throw New System.Security.SecurityException("User not authorized to add Group records.")

        ElseIf Not CanEditObject() Then
            Throw New System.Security.SecurityException("User not authorized to update Group records.")
        End If
        Return MyBase.Save
    End Function

    Private Sub New()
        ' Require use of factory methods
    End Sub

#End Region

#Region " Child Methods "

    Friend Shared Function NewChildGroup() As ClsGroup
        Dim Child As New ClsGroup
        Child.ValidationRules.CheckRules()
        Child.MarkAsChild()
        Return Child
    End Function

    Friend Shared Function GetChildGroup(ByVal Group As DAClsprgGroups.Struct) As ClsGroup
        Return New ClsGroup(Group)
    End Function

    Friend Shared Function NewFormGroup() As ClsGroup
        Dim Child As New ClsGroup
        Child.ValidationRules.CheckRules()
        Child.MarkAsChild()
        Return Child
    End Function

    Friend Shared Function GetFormGroup(ByVal Group As DAClsprgGroups.Struct) As ClsGroup
        Return New ClsGroup(Group)
    End Function

    Private Sub New(ByVal Group As DAClsprgGroups.Struct)
        MarkAsChild()
        Fetch(Group)
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
        Dim List As DAClsprgGroups.Struct() = DAClsprgGroups.Fetch(criteria.ID)
        If List.Length > 0 Then
            Me.mStruct = List(0)
            Me.LoadObjectData()
        Else
            Throw New System.Security.SecurityException("Group doesn't exist")
        End If
    End Sub

    <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Insert()
        Me.LoadTableStruct(New Object() {Me.mForm})
        Me.mStruct = DAClsprgGroups.Insert(Me.mStruct)
        Me.mID = Me.mStruct.ID.Value
        Me.mProfiles.Update(Me)
        Me.mFields.Update(Me)
    End Sub

    <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Update()
        If MyBase.IsDirty Then
            Me.LoadTableStruct(New Object() {Me.mForm})
            Me.mStruct = DAClsprgGroups.Update(Me.mStruct)
        End If
        Me.mProfiles.Update(Me)
        Me.mFields.Update(Me)
    End Sub

    <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_DeleteSelf()
        DataPortal_Delete(New Criteria(Me.mID))
    End Sub

    <Transactional(TransactionalTypes.TransactionScope)> Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)
        Me.mFields.Clear()
        Me.mProfiles.Clear()
        Me.mFields.Update(Me)
        Me.mProfiles.Update(Me)
        DAClsprgGroups.Delete(criteria.ID)
    End Sub

#End Region

#Region " Child Area "

    Private Sub Fetch(ByVal Group As DAClsprgGroups.Struct)
        Me.mStruct = Group
        Me.LoadObjectData()
        MarkOld()
    End Sub

    Friend Sub Insert(ByVal parent As Object)
        ' if we're not dirty then don't update the database
        If Not Me.IsDirty Then Exit Sub

        Me.LoadTableStruct(New Object() {parent})
        Me.mStruct = DAClsprgGroups.Insert(Me.mStruct)
        Me.mID = Me.mStruct.ID.Value
        Me.mProfiles.Update(Me)
        Me.mFields.Update(Me)
        MarkOld()
    End Sub

    Friend Sub Update(ByVal parent As Object)
        ' if we're not dirty then don't update the database
        If Not Me.IsDirty Then Exit Sub

        Me.LoadTableStruct(New Object() {parent})
        Me.mStruct = DAClsprgGroups.Update(Me.mStruct)
        Me.mProfiles.Update(Me)
        Me.mFields.Update(Me)
        MarkOld()
    End Sub

    Friend Sub DeleteSelf()
        ' if we're not dirty then don't update the database
        If Not Me.IsDirty Then Exit Sub

        ' if we're new then don't update the database
        If Me.IsNew Then Exit Sub

        Me.mFields.Clear()
        Me.mProfiles.Clear()
        Me.mFields.Update(Me)
        Me.mProfiles.Update(Me)
        DAClsprgGroups.Delete(Me.mID)
        MarkNew()
    End Sub

#End Region

#Region " Common Area "

    Private mStruct As DAClsprgGroups.Struct = New DAClsprgGroups.Struct

    Public Function GetTableStruct() As DAClsprgGroups.Struct
        Return Me.mStruct
    End Function

    ''' <summary>
    ''' Load the data of the object with the fetched data
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadObjectData()
        With Me.mStruct
            Me.mID = .ID.Value
            Me.mDescription = .Description.Value
            Me.mForm = ClsFormInfo.GetFormInfo(.IDForm.Value)
            Me.mFields = ClsFieldList.GetGroupFieldList(DAClsprgFields.FetchByGroup(.ID.Value))
            Me.mProfiles = ClsGroupProfileList.GetGroupProfileList(DAClsprgAdministrativeGroupPermissions.FetchByGroup(.ID.Value))
        End With
    End Sub

    ''' <summary>
    ''' Collect the data of the object to fill to a structure of data and returns the structure 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadTableStruct(ByVal parents() As Object)
        With Me.mStruct
            .ID.NewValue = Me.mID
            .IDForm.NewValue = parents(0).ID
            .Description.NewValue = Me.mDescription
        End With
    End Sub

#End Region

#End Region

#Region " Exists "

    Public Shared Function Exists(ByVal id As Long, ByVal idForm As Long, ByVal description As String) As Boolean
        Return DataPortal.Execute(Of ExistsCommand)(New ExistsCommand(id, idForm, description)).Exists
    End Function

    <Serializable()> Private Class ExistsCommand
        Inherits CommandBase

        Private mID As Long
        Private mIDForm As Long
        Private mDescription As String
        Private mExists As Boolean

        Public ReadOnly Property Exists() As Boolean
            Get
                Return Me.mExists
            End Get
        End Property

        Public Sub New(ByVal id As Long, ByVal idForm As String, ByVal description As String)
            Me.mID = id
            Me.mIDForm = idForm
            Me.mDescription = description
        End Sub

        Protected Overrides Sub DataPortal_Execute()
            Dim groups As DAClsprgGroups.Struct() = DAClsprgGroups.FetchByForm(Me.mIDForm, Me.mDescription)
            Me.mExists = groups.Length > 0 AndAlso groups(0).ID.Value <> Me.mID
        End Sub

    End Class

#End Region

End Class
