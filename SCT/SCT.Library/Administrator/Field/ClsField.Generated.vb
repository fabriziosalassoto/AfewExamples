Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsField
    Inherits BusinessBase(Of ClsField)

#Region " Business Methods "

    Private mID As Long
    Private mGroup As ClsGroupInfo = ClsGroupInfo.NewGroupInfo
    Private mDescription As String = String.Empty

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

    Public Property Group() As ClsGroupInfo
        Get
            CanReadProperty(True)
            Return Me.mGroup
        End Get
        Set(ByVal value As ClsGroupInfo)
            CanWriteProperty(True)
            If value IsNot Nothing AndAlso value.ID <> 0 Then
                If Me.mGroup.ID <> value.ID Then
                    Me.mGroup = value
                    ValidationRules.CheckRules("Group")
                    PropertyHasChanged()
                End If
            Else
                Throw New System.Security.SecurityException("Group required.")
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

    Public Sub AssignGroup(ByVal GroupId As Long)
        If GroupId <> 0 Then
            If Me.mGroup.ID <> GroupId Then
                Me.mGroup = ClsGroupInfo.GetGroupInfo(GroupId)
                PropertyHasChanged("Group")
            End If
        Else
            Throw New System.Security.SecurityException("Group required.")
        End If
    End Sub

    Protected Overrides Function GetIdValue() As Object
        Return Me.mID
    End Function

#End Region

#Region " Validation Rules "

    Protected Overrides Sub AddBusinessRules()
        ValidationRules.AddRule(AddressOf GroupRequired, "Group")

        ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, "Description")
        ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("Description", 100))
        ValidationRules.AddRule(AddressOf ExistsDescriptionInForm, "Description")
        ValidationRules.AddRule(AddressOf ExistsDescriptionInGroup, "Description")
    End Sub

    Private Function GroupRequired(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
        If Me.mGroup.ID = 0 Then
            e.Description = "Group required."
            Return False
        Else
            Return True
        End If
    End Function

    Private Function ExistsDescriptionInForm(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
        If ClsField.ExistsInForm(Me.mID, Me.Group.Form.ID, Me.mDescription) Then
            e.Description = "Description already assigned to another Field in Form."
            Return False
        Else
            Return True
        End If
    End Function

    Private Function ExistsDescriptionInGroup(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
        If ClsField.ExistsInGroup(Me.mID, Me.Group.ID, Me.mDescription) Then
            e.Description = "Description already assigned to another Field in Group."
            Return False
        Else
            Return True
        End If
    End Function

#End Region

#Region " Authorization Rules "

    'Protected Overrides Sub AddAuthorizationRules()
    '    AuthorizationRules.AllowRead("ID", " ")
    '    AuthorizationRules.AllowRead("IDGroup", " ")
    '    AuthorizationRules.AllowRead("Description", " ")
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

    Public Shared Function NewField() As ClsField
        If Not CanAddObject() Then
            Throw New System.Security.SecurityException("User not authorized to add Field records.")
        End If
        Return DataPortal.Create(Of ClsField)(New Criteria(0)) ', 0, 0))
    End Function

    Public Shared Function GetField(ByVal ID As Long) As ClsField ', ByVal IDGroup As Long, ByVal IDForm As Long) As ClsField
        If Not CanGetObject() Then
            Throw New System.Security.SecurityException("User not authorized to view Field records.")
        End If
        Return DataPortal.Fetch(Of ClsField)(New Criteria(ID)) ', IDGroup, IDForm))
    End Function

    Public Shared Sub DeleteField(ByVal ID As Long) ', ByVal IDGroup As Long, ByVal IDForm As Long)
        If Not CanDeleteObject() Then
            Throw New System.Security.SecurityException("User not authorized to remove Field records.")
        End If
        DataPortal.Delete(New Criteria(ID)) ', IDGroup, IDForm))
    End Sub

    Public Overrides Function Save() As ClsField
        If IsDeleted AndAlso Not CanDeleteObject() Then
            Throw New System.Security.SecurityException("User not authorized to remove Field records.")

        ElseIf IsNew AndAlso Not CanAddObject() Then
            Throw New System.Security.SecurityException("User not authorized to add Field records.")

        ElseIf Not CanEditObject() Then
            Throw New System.Security.SecurityException("User not authorized to update Field records.")
        End If
        Return MyBase.Save
    End Function

    Private Sub New()
        ' Require use of factory methods
    End Sub

#End Region

#Region " Child Methods "

    Friend Shared Function NewChildField() As ClsField
        Dim Child As New ClsField
        Child.ValidationRules.CheckRules()
        Child.MarkAsChild()
        Return Child
    End Function

    Friend Shared Function GetChildField(ByVal Field As DAClsprgFields.Struct) As ClsField
        Return New ClsField(Field)
    End Function

    Friend Shared Function NewGroupField() As ClsField
        Dim Child As New ClsField
        Child.ValidationRules.CheckRules()
        Child.MarkAsChild()
        Return Child
    End Function

    Friend Shared Function GetGroupField(ByVal Field As DAClsprgFields.Struct) As ClsField
        Return New ClsField(Field)
    End Function

    Private Sub New(ByVal Field As DAClsprgFields.Struct)
        MarkAsChild()
        Fetch(Field)
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

        Public Sub New(ByVal id As Long)
            Me.mID = id
        End Sub

    End Class

    <RunLocal()> Private Overloads Sub DataPortal_Create(ByVal criteria As Criteria)
        Me.ValidationRules.CheckRules()
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
        Dim List As DAClsprgFields.Struct() = DAClsprgFields.Fetch(criteria.ID)
        If List.Length > 0 Then
            Me.mStruct = List(0)
            Me.LoadObjectData()
        Else
            Throw New System.Security.SecurityException("Field doesn't exist")
        End If
    End Sub

    <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Insert()
        Me.LoadTableStruct(New Object() {Me.mGroup})
        Me.mStruct = DAClsprgFields.Insert(Me.mStruct)
        Me.mID = Me.mStruct.ID.Value
    End Sub

    <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Update()
        If MyBase.IsDirty Then
            Me.LoadTableStruct(New Object() {Me.mGroup})
            Me.mStruct = DAClsprgFields.Update(Me.mStruct)
        End If
    End Sub

    <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_DeleteSelf()
        DataPortal_Delete(New Criteria(Me.mID))
    End Sub

    <Transactional(TransactionalTypes.TransactionScope)> Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)
        DAClsprgFields.Delete(criteria.ID)
    End Sub

#End Region

#Region " Child Area "

    Private Sub Fetch(ByVal Field As DAClsprgFields.Struct)
        Me.mStruct = Field
        Me.LoadObjectData()
        MarkOld()
    End Sub

    Friend Sub Insert(ByVal parent As Object)
        ' if we're not dirty then don't update the database
        If Not Me.IsDirty Then Exit Sub

        Me.LoadTableStruct(New Object() {parent})
        Me.mStruct = DAClsprgFields.Insert(Me.mStruct)
        Me.mID = Me.mStruct.ID.Value
        MarkOld()
    End Sub

    Friend Sub Update(ByVal parent As Object)
        ' if we're not dirty then don't update the database
        If Not Me.IsDirty Then Exit Sub

        Me.LoadTableStruct(New Object() {parent})
        Me.mStruct = DAClsprgFields.Update(Me.mStruct)
        MarkOld()
    End Sub

    Friend Sub DeleteSelf()
        ' if we're not dirty then don't update the database
        If Not Me.IsDirty Then Exit Sub

        ' if we're new then don't update the database
        If Me.IsNew Then Exit Sub

        DAClsprgFields.Delete(Me.mID) ', Me.mGroup.ID, Me.mGroup.Form.ID)
        MarkNew()
    End Sub

#End Region

#Region " Common Area "

    Private mStruct As DAClsprgFields.Struct = New DAClsprgFields.Struct

    Public Function GetTableStruct() As DAClsprgFields.Struct
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
            Me.mGroup = ClsGroupInfo.GetGroupInfo(.IDGroup.Value)
        End With
    End Sub

    ''' <summary>
    ''' Collect the data of the object to fill to a structure of data and returns the structure 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadTableStruct(ByVal parents() As Object)
        With Me.mStruct
            .ID.NewValue = Me.mID
            .IDGroup.NewValue = parents(0).ID
            .IDForm.NewValue = parents(0).Form.ID
            .Description.NewValue = Me.mDescription
        End With
    End Sub

#End Region

#End Region

#Region " Exists "

    Public Shared Function ExistsInGroup(ByVal id As Long, ByVal idGroup As String, ByVal description As String) As Boolean
        Return DataPortal.Execute(Of ExistsInGroupCommand)(New ExistsInGroupCommand(id, idGroup, description)).Exists
    End Function

    <Serializable()> Private Class ExistsInGroupCommand
        Inherits CommandBase

        Private mID As Long
        Private mIDGroup As Long
        Private mDescription As String
        Private mExists As Boolean

        Public ReadOnly Property Exists() As Boolean
            Get
                Return Me.mExists
            End Get
        End Property

        Public Sub New(ByVal id As Long, ByVal idGroup As Long, ByVal description As String)
            Me.mID = id
            Me.mIDGroup = idGroup
            Me.mDescription = description
        End Sub

        Protected Overrides Sub DataPortal_Execute()
            Dim fields As DAClsprgFields.Struct() = DAClsprgFields.FetchByGroup(Me.mIDGroup, Me.mDescription)
            Me.mExists = fields.Length > 0 AndAlso fields(0).ID.Value <> Me.mID
        End Sub

    End Class

    Public Shared Function ExistsInForm(ByVal id As Long, ByVal idForm As Long, ByVal description As String) As Boolean
        Return DataPortal.Execute(Of ExistsInFormCommand)(New ExistsInFormCommand(id, idForm, description)).Exists
    End Function

    <Serializable()> Private Class ExistsInFormCommand
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
            Dim fields As DAClsprgFields.Struct() = DAClsprgFields.FetchByForm(Me.mIDForm, Me.mDescription)
            Me.mExists = fields.Length > 0 AndAlso fields(0).ID.Value <> Me.mID
        End Sub

    End Class

#End Region

End Class
