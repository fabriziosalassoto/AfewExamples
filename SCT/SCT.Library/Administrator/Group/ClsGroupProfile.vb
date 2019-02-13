Imports Csla
Imports SCT.DataAccess
Imports System.ComponentModel

<Serializable()> Public Class ClsGroupProfile
    Inherits BusinessBase(Of ClsGroupProfile)

#Region " Business Methods "

    Private mID As Long
    Private mDescription As String = String.Empty
    Private mGroup As ClsGroupInfo = ClsGroupInfo.NewGroupInfo
    Private mFormProfile As ClsFormProfileInfo = ClsFormProfileInfo.NewFormProfileInfo
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

    Public ReadOnly Property Group() As ClsGroupInfo
        Get
            CanReadProperty(True)
            Return Me.mGroup
        End Get
    End Property

    Public ReadOnly Property FormProfile() As ClsFormProfileInfo
        Get
            CanReadProperty(True)
            Return Me.mFormProfile
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
        Me.mFormProfile.PSelect = value
        If Me.mFormProfile.PSelect = False Then
            Me.PSelect = False
        End If
    End Sub

    Public Sub SetFormPInsert(ByVal value As Boolean)
        Me.mFormProfile.PInsert = value
        If Me.mFormProfile.PInsert = False Then
            Me.PInsert = False
        End If
    End Sub

    Public Sub SetFormPUpdate(ByVal value As Boolean)
        Me.mFormProfile.PUpdate = value
        If Me.mFormProfile.PUpdate = False Then
            Me.PUpdate = False
        End If
    End Sub

    Public Sub SetFormPDelete(ByVal value As Boolean)
        Me.mFormProfile.PDelete = value
        If Me.mFormProfile.PDelete = False Then
            Me.PDelete = False
        End If
    End Sub

    Public Sub SetFormPermissions(ByVal Form As Object)
        Me.SetFormPSelect(Form.PSelect)
        Me.SetFormPInsert(Form.PInsert)
        Me.SetFormPUpdate(Form.PUpdate)
        Me.SetFormPDelete(Form.PDelete)
    End Sub

    Public Function CanExecute(ByVal pName As String) As Boolean
        If Len(pName) > 0 Then
            For Each formProp As PropertyDescriptor In TypeDescriptor.GetProperties(Me.mFormProfile.GetType)
                If formProp.Name = pName Then
                    For Each groupProp As PropertyDescriptor In TypeDescriptor.GetProperties(Me.GetType)
                        If groupProp.Name = pName Then
                            Return Me.mFormProfile.PSelect AndAlso formProp.GetValue(Me.mFormProfile) AndAlso Me.mPSelect AndAlso groupProp.GetValue(Me)
                        End If
                    Next
                End If
            Next
        End If
        Return False
    End Function

    Protected Overrides Function GetIdValue() As Object
        Return Me.mID
    End Function

#End Region

#Region " Validation Rules "

    Protected Overrides Sub AddBusinessRules()
        ValidationRules.AddRule(AddressOf IsSelectPermissionRequired, "PSelect")
        ValidationRules.AddRule(AddressOf IsSelectFormPermissionRequired, "PSelect")
        ValidationRules.AddRule(AddressOf IsInsertFormPermissionRequired, "PInsert")
        ValidationRules.AddRule(AddressOf IsUpdateFormPermissionRequired, "PUpdate")
        ValidationRules.AddRule(AddressOf IsDeleteFormPermissionRequired, "PDelete")
    End Sub

    Private Function IsSelectPermissionRequired(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
        If Me.mPSelect = False AndAlso (Me.mPInsert = True OrElse Me.mPUpdate = True OrElse Me.mPDelete = True) Then
            e.Description = "Select permission is required for another permissions"
            Return False
        Else
            Return True
        End If
    End Function

    Private Function IsSelectFormPermissionRequired(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
        If Me.mFormProfile Is Nothing OrElse Me.mFormProfile.PSelect = False AndAlso Me.mPSelect = True Then
            e.Description = "Form Select permission is required to Group Select permission"
            Return False
        Else
            Return True
        End If
    End Function

    Private Function IsInsertFormPermissionRequired(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
        If Me.mFormProfile Is Nothing OrElse Me.mFormProfile.PInsert = False AndAlso Me.mPInsert = True Then
            e.Description = "Form Insert permission is required to Group Insert permission"
            Return False
        Else
            Return True
        End If
    End Function

    Private Function IsUpdateFormPermissionRequired(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
        If Me.mFormProfile Is Nothing OrElse Me.mFormProfile.PUpdate = False AndAlso Me.mPUpdate = True Then
            e.Description = "Form Update permission is required to Group Update permission"
            Return False
        Else
            Return True
        End If
    End Function

    Private Function IsDeleteFormPermissionRequired(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
        If Me.mFormProfile Is Nothing OrElse Me.mFormProfile.PDelete = False AndAlso Me.mPDelete = True Then
            e.Description = "Form Delete permission is required to Group Delete permission"
            Return False
        Else
            Return True
        End If
    End Function

#End Region

#Region " Authorization Rules "

    'Protected Overrides Sub AddAuthorizationRules()
    '    AuthorizationRules.AllowRead("IDProfile", " ")
    '    AuthorizationRules.AllowRead("Description", " ")
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

    Friend Shared Function NewGroupProfile(ByVal idProfile As Long, ByVal idForm As Long, ByVal idGroup As Long, ByVal pSelect As Boolean, ByVal pInsert As Boolean, ByVal pUpdate As Boolean, ByVal pDelete As Boolean) As ClsGroupProfile
        Return New ClsGroupProfile(idProfile, idForm, idGroup, pSelect, pInsert, pUpdate, pDelete)
    End Function

    Friend Shared Function GetGroupProfile(ByVal groupPermissions As DAClsprgAdministrativeGroupPermissions.Struct) As ClsGroupProfile
        Return New ClsGroupProfile(groupPermissions)
    End Function

    Private Sub New(ByVal idProfile As Long, ByVal idForm As Long, ByVal idGroup As Long, ByVal pSelect As Boolean, ByVal pInsert As Boolean, ByVal pUpdate As Boolean, ByVal pDelete As Boolean)
        MarkAsChild()
        Fetch(idProfile, idForm, idGroup, pSelect, pInsert, pUpdate, pDelete)
    End Sub

    Private Sub New(ByVal groupPermissions As DAClsprgAdministrativeGroupPermissions.Struct)
        MarkAsChild()
        Fetch(groupPermissions)
    End Sub

#End Region

#Region " Data Access "

    Private Sub Fetch(ByVal idProfile As Long, ByVal idForm As Long, ByVal idGroup As Long, ByVal pSelect As Boolean, ByVal pInsert As Boolean, ByVal pUpdate As Boolean, ByVal pDelete As Boolean)
        Dim list As DAClsprgAdministrativeProfiles.Struct() = DAClsprgAdministrativeProfiles.Fetch(idProfile)
        If list.Length > 0 Then
            Me.LoadObjectData(list(0), idForm, idGroup, pSelect, pInsert, pUpdate, pDelete)
            Me.ValidationRules.CheckRules()
        Else
            Throw New System.Security.SecurityException("Profile doesn't assign to group")
        End If
    End Sub

    Private Sub Fetch(ByVal groupPermissions As DAClsprgAdministrativeGroupPermissions.Struct)
        Dim list As DAClsprgAdministrativeProfiles.Struct() = DAClsprgAdministrativeProfiles.Fetch(groupPermissions.IDProfile.Value)
        If list.Length > 0 Then
            Me.mStruct = groupPermissions
            Me.LoadObjectData(list(0))
            Me.MarkOld()
        Else
            Throw New System.Security.SecurityException("Profile doesn't assign to group")
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

        DAClsprgAdministrativeGroupPermissions.Delete(parents(0).ID, parents(1).ID, parents(1).Form.ID)
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
    Private Sub LoadObjectData(ByVal profile As DAClsprgAdministrativeProfiles.Struct)
        Me.mID = profile.ID.Value
        Me.mDescription = profile.Description.Value
        Me.mFormProfile = ClsFormProfileInfo.GetFormProfileInfo(profile.ID.Value, Me.mStruct.IDForm.Value)
        Me.mGroup = ClsGroupInfo.GetGroupInfo(Me.mStruct.IDGroup.Value)
        Me.mPSelect = Me.mStruct.pSelect.Value
        Me.mPInsert = Me.mStruct.pInsert.Value
        Me.mPUpdate = Me.mStruct.pUpdate.Value
        Me.mPDelete = Me.mStruct.pDelete.Value
    End Sub

    ''' <summary>
    ''' Populate the data of the object with the fetched data
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadObjectData(ByVal profile As DAClsprgAdministrativeProfiles.Struct, ByVal idForm As Long, ByVal idGroup As Long, ByVal pSelect As Boolean, ByVal pInsert As Boolean, ByVal pUpdate As Boolean, ByVal pDelete As Boolean)
        Me.mID = profile.ID.Value
        Me.mDescription = profile.Description.Value
        Me.mFormProfile = ClsFormProfileInfo.GetFormProfileInfo(profile.ID.Value, idForm)
        Me.mGroup = ClsGroupInfo.GetGroupInfo(idGroup)
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
            .IDGroup.NewValue = parents(0).ID
            .IDProfile.NewValue = parents(1).ID
            .IDForm.NewValue = parents(1).Form.ID
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
