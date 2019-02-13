Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsProfileGroup
    Inherits BusinessBase(Of ClsProfileGroup)

#Region " Business Methods "

    Private mID As Long
    Private mDescription As String = String.Empty
    Private mForm As ClsProfileFormInfo = ClsProfileFormInfo.NewProfileFormInfo
    Private mPSelect As Boolean = False
    Private mPInsert As Boolean = False
    Private mPUpdate As Boolean = False
    Private mPDelete As Boolean = False

    Public ReadOnly Property ID() As Long
        Get
            CanReadProperty(True)
            Return Me.mID
        End Get
    End Property

    Public ReadOnly Property Description() As String
        Get
            CanReadProperty(True)
            Return Me.mDescription
        End Get
    End Property

    Public ReadOnly Property Form() As ClsProfileFormInfo
        Get
            CanReadProperty(True)
            Return Me.mForm
        End Get
    End Property

    Public Property PSelect() As Boolean
        Get
            CanReadProperty(True)
            Return Me.mPSelect
        End Get
        Set(ByVal value As Boolean)
            CanWriteProperty(True)
            If Me.mPSelect <> value Then
                Me.mPSelect = value
                ValidationRules.CheckRules("PSelect")
                PropertyHasChanged()
            End If
        End Set
    End Property

    Public Property PInsert() As Boolean
        Get
            CanReadProperty(True)
            Return Me.mPInsert
        End Get
        Set(ByVal value As Boolean)
            CanWriteProperty(True)
            If Me.mPInsert <> value Then
                Me.mPInsert = value
                ValidationRules.CheckRules("PSelect")
                ValidationRules.CheckRules("PInsert")
                PropertyHasChanged()
            End If
        End Set
    End Property

    Public Property PUpdate() As Boolean
        Get
            CanReadProperty(True)
            Return Me.mPUpdate
        End Get
        Set(ByVal value As Boolean)
            CanWriteProperty(True)
            If Me.mPUpdate <> value Then
                Me.mPUpdate = value
                ValidationRules.CheckRules("PSelect")
                ValidationRules.CheckRules("PUpdate")
                PropertyHasChanged()
            End If
        End Set
    End Property

    Public Property PDelete() As Boolean
        Get
            CanReadProperty(True)
            Return Me.mPDelete
        End Get
        Set(ByVal value As Boolean)
            CanWriteProperty(True)
            If Me.mPDelete <> value Then
                Me.mPDelete = value
                ValidationRules.CheckRules("PSelect")
                ValidationRules.CheckRules("PDelete")
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

    Public Function ToListItem() As System.Web.UI.WebControls.ListItem
        Return New System.Web.UI.WebControls.ListItem(Me.mDescription, Me.mID)
    End Function

    Public Sub SetFormPSelect(ByVal value As Boolean)
        Me.mForm.PSelect = value
        If Me.mForm.PSelect = False Then
            Me.PSelect = False
        End If
    End Sub

    Public Sub SetFormPInsert(ByVal value As Boolean)
        Me.mForm.PInsert = value
        If Me.mForm.PInsert = False Then
            Me.PInsert = False
        End If
    End Sub

    Public Sub SetFormPUpdate(ByVal value As Boolean)
        Me.mForm.PUpdate = value
        If Me.mForm.PUpdate = False Then
            Me.PUpdate = False
        End If
    End Sub

    Public Sub SetFormPDelete(ByVal value As Boolean)
        Me.mForm.PDelete = value
        If Me.mForm.PDelete = False Then
            Me.PDelete = False
        End If
    End Sub

    Public Sub SetFormPermissions(ByVal Form As ClsProfileForm)
        Me.SetFormPSelect(Form.PSelect)
        Me.SetFormPInsert(Form.PInsert)
        Me.SetFormPUpdate(Form.PUpdate)
        Me.SetFormPDelete(Form.PDelete)
    End Sub

    Protected Overrides Function GetIdValue() As Object
        Return Me.mID
    End Function

#End Region

#Region " Validation Rules "

    Protected Overrides Sub AddBusinessRules()
        ValidationRules.AddRule(AddressOf SelectPermissionRequired, "PSelect")
        ValidationRules.AddRule(AddressOf SelectFormPermissionRequired, "PSelect")
        ValidationRules.AddRule(AddressOf InsertFormPermissionRequired, "PInsert")
        ValidationRules.AddRule(AddressOf UpdateFormPermissionRequired, "PUpdate")
        ValidationRules.AddRule(AddressOf DeleteFormPermissionRequired, "PDelete")
    End Sub

    Private Function SelectPermissionRequired(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
        If Me.mPSelect = False AndAlso (Me.mPInsert = True OrElse Me.mPUpdate = True OrElse Me.mPDelete = True) Then
            e.Description = "Select permission is required for another permissions"
            Return False
        Else
            Return True
        End If
    End Function

    Private Function SelectFormPermissionRequired(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
        If Me.mForm.PSelect = False AndAlso Me.mPSelect = True Then
            e.Description = "Form Select permission is required to Group Select permission"
            Return False
        Else
            Return True
        End If
    End Function

    Private Function InsertFormPermissionRequired(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
        If Me.mForm.PInsert = False AndAlso Me.mPInsert = True Then
            e.Description = "Form Insert permission is required to Group Insert permission"
            Return False
        Else
            Return True
        End If
    End Function

    Private Function UpdateFormPermissionRequired(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
        If Me.mForm.PUpdate = False AndAlso Me.mPUpdate = True Then
            e.Description = "Form Update permission is required to Group Update permission"
            Return False
        Else
            Return True
        End If
    End Function

    Private Function DeleteFormPermissionRequired(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
        If Me.mForm.PDelete = False AndAlso Me.mPDelete = True Then
            e.Description = "Form Delete permission is required to Group Delete permission"
            Return False
        Else
            Return True
        End If
    End Function

#End Region

#Region " Authorization Rules "

    'Protected Overrides Sub AddAuthorizationRules()
    '    AuthorizationRules.AllowRead("IDGroup", " ")
    '    AuthorizationRules.AllowRead("Description", " ")
    '    AuthorizationRules.AllowRead("Fields", " ")
    '    AuthorizationRules.AllowRead("PSelect", " ")
    '    AuthorizationRules.AllowRead("PInsert", " ")
    '    AuthorizationRules.AllowRead("PUpdate", " ")
    '    AuthorizationRules.AllowRead("PDelete", " ")
    '    AuthorizationRules.AllowWrite("PSelect", New String() {" ", " "})
    '    AuthorizationRules.AllowWrite("PInsert", New String() {" ", " "})
    '    AuthorizationRules.AllowWrite("PUpdate", New String() {" ", " "})
    '    AuthorizationRules.AllowWrite("PDelete", New String() {" ", " "})
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

    Friend Shared Function NewProfileFormGroup(ByVal idGroup As Long, ByVal idForm As Long, ByVal idProfile As Long, ByVal pSelect As Boolean, ByVal pInsert As Boolean, ByVal pUpdate As Boolean, ByVal pDelete As Boolean) As ClsProfileGroup
        Return New ClsProfileGroup(idGroup, idForm, idProfile, pSelect, pInsert, pUpdate, pDelete)
    End Function

    Friend Shared Function GetProfileFormGroup(ByVal groupPermissions As DAClsprgAdministrativeGroupPermissions.Struct) As ClsProfileGroup
        Return New ClsProfileGroup(groupPermissions)
    End Function

    Private Sub New(ByVal idGroup As Long, ByVal idForm As Long, ByVal idProfile As Long, ByVal pSelect As Boolean, ByVal pInsert As Boolean, ByVal pUpdate As Boolean, ByVal pDelete As Boolean)
        MarkAsChild()
        Fetch(idGroup, idForm, idProfile, pSelect, pInsert, pUpdate, pDelete)
    End Sub

    Private Sub New(ByVal GroupPermissions As DAClsprgAdministrativeGroupPermissions.Struct)
        MarkAsChild()
        Fetch(GroupPermissions)
    End Sub

#End Region

#Region " Data Access "

    Private Sub Fetch(ByVal idGroup As Long, ByVal idForm As Long, ByVal idProfile As Long, ByVal pSelect As Boolean, ByVal pInsert As Boolean, ByVal pUpdate As Boolean, ByVal pDelete As Boolean)
        Dim list As DAClsprgGroups.Struct() = DAClsprgGroups.Fetch(idGroup)
        If list.Length > 0 Then
            Me.LoadObjectData(list(0), idProfile, idForm, pSelect, pInsert, pUpdate, pDelete)
            Me.ValidationRules.CheckRules()
        Else
            Throw New System.Security.SecurityException("Group doesn't exist")
        End If
    End Sub

    Private Sub Fetch(ByVal groupPermissions As DAClsprgAdministrativeGroupPermissions.Struct)
        Dim list As DAClsprgGroups.Struct() = DAClsprgGroups.Fetch(groupPermissions.IDGroup.Value)
        If list.Length > 0 Then
            Me.mStruct = groupPermissions
            Me.LoadObjectData(list(0))
            Me.MarkOld()
        Else
            Throw New System.Security.SecurityException("Group doesn't exist")
        End If
    End Sub

    Friend Sub Insert(ByVal parents() As Object)
        Me.SetFormPermissions(parents(1))

        ' if we're not dirty then don't update the database
        If Not Me.IsDirty Then Exit Sub

        Me.LoadTableStruct(parents)
        Me.mStruct = DAClsprgAdministrativeGroupPermissions.Insert(Me.mStruct)
        MarkOld()
    End Sub

    Friend Sub Update(ByVal parents() As Object)
        Me.SetFormPermissions(parents(1))

        ' if we're not dirty then don't update the database
        If Not Me.IsDirty Then Exit Sub

        Me.LoadTableStruct(parents)
        Me.mStruct = DAClsprgAdministrativeGroupPermissions.Update(Me.mStruct)
        MarkOld()
    End Sub

    Friend Sub DeleteSelf(ByVal parents() As Object)
        ' if we're not dirty then don't update the database
        If Not Me.IsDirty Then Exit Sub

        ' if we're new then don't update the database
        If Me.IsNew Then Exit Sub

        DAClsprgAdministrativeGroupPermissions.Delete(Me.mID, parents(0).ID, parents(1).ID)
        MarkNew()
    End Sub

    Private mStruct As DAClsprgAdministrativeGroupPermissions.Struct = New DAClsprgAdministrativeGroupPermissions.Struct

    Public Function GetTableStruct() As DAClsprgAdministrativeGroupPermissions.Struct
        Return Me.mStruct
    End Function

    ''' <summary>
    ''' Populate the data of the object with the fetched data
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadObjectData(ByVal group As DAClsprgGroups.Struct)
        Me.mID = group.ID.Value
        Me.mDescription = group.Description.Value
        Me.mForm = ClsProfileFormInfo.GetProfileFormInfo(Me.mStruct.IDProfile.Value, Me.mStruct.IDForm.Value)
        Me.mPSelect = Me.mStruct.pSelect.Value
        Me.mPInsert = Me.mStruct.pInsert.Value
        Me.mPUpdate = Me.mStruct.pUpdate.Value
        Me.mPDelete = Me.mStruct.pDelete.Value
    End Sub

    ''' <summary>
    ''' Populate the data of the object with the fetched data
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadObjectData(ByVal group As DAClsprgGroups.Struct, ByVal idProfile As Long, ByVal idForm As Long, ByVal pSelect As Boolean, ByVal pInsert As Boolean, ByVal pUpdate As Boolean, ByVal pDelete As Boolean)
        Me.mID = group.ID.Value
        Me.mDescription = group.Description.Value
        Me.mForm = ClsProfileFormInfo.GetProfileFormInfo(idProfile, idForm)
        Me.mPSelect = pSelect
        Me.mPInsert = pInsert
        Me.mPUpdate = pUpdate
        Me.mPDelete = pDelete
    End Sub

    ''' <summary>
    ''' Collect the data of the object to fill to a structure of data and returns the structure 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadTableStruct(ByVal parents() As Object)
        With Me.mStruct
            .IDGroup.NewValue = Me.mID
            .IDProfile.NewValue = parents(0).ID
            .IDForm.NewValue = parents(1).ID
            .pSelect.NewValue = Me.mPSelect
            .pInsert.NewValue = Me.mPInsert
            .pUpdate.NewValue = Me.mPUpdate
            .pDelete.NewValue = Me.mPDelete
        End With
    End Sub

#End Region

#Region " Exists "

    Public Shared Function Exists(ByVal idProfile As Long, ByVal idForm As Long, ByVal idGroup As Long) As Boolean
        Return DataPortal.Execute(Of ExistsCommand)(New ExistsCommand(idProfile, idForm, idGroup)).Exists
    End Function

    <Serializable()> Private Class ExistsCommand
        Inherits CommandBase

        Private mIDProfile As Long
        Private mIDForm As Long
        Private mIDGroup As Long
        Private mExists As Boolean

        Public ReadOnly Property Exists() As Boolean
            Get
                Return Me.mExists
            End Get
        End Property

        Public Sub New(ByVal idProfile As Long, ByVal idForm As Long, ByVal idGroup As Long)
            Me.mIDProfile = idProfile
            Me.mIDForm = idForm
            Me.mIDGroup = idGroup
        End Sub

        Protected Overrides Sub DataPortal_Execute()
            Me.mExists = DAClsprgAdministrativeGroupPermissions.Fetch(Me.mIDGroup, Me.mIDProfile, Me.mIDForm).Length > 0
        End Sub

    End Class

#End Region

End Class
