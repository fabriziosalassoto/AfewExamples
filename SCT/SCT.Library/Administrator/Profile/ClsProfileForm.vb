Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsProfileForm
    Inherits BusinessBase(Of ClsProfileForm)

#Region " Business Methods "

    Private mID As Long
    Private mDescription As String = String.Empty
    Private mProfile As ClsProfileInfo = ClsProfileInfo.NewProfileInfo
    Private mPSelect As Boolean = False
    Private mPInsert As Boolean = False
    Private mPUpdate As Boolean = False
    Private mPDelete As Boolean = False
    Private mGroups As ClsProfileGroupList = ClsProfileGroupList.NewProfileFormGroupList

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

    Public ReadOnly Property Profile() As ClsProfileInfo
        Get
            CanReadProperty(True)
            Return Me.mProfile
        End Get
    End Property

    Public ReadOnly Property Groups() As ClsProfileGroupList
        Get
            CanReadProperty(True)
            Return Me.mGroups
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
                PropertyHasChanged()
            End If
        End Set
    End Property

    Public Overrides ReadOnly Property IsValid() As Boolean
        Get
            Return MyBase.IsValid AndAlso Me.mGroups.IsValid
        End Get
    End Property

    Public Overrides ReadOnly Property IsDirty() As Boolean
        Get
            Return MyBase.IsDirty OrElse Me.mGroups.IsDirty
        End Get
    End Property

    Public Function ToListItem() As System.Web.UI.WebControls.ListItem
        Return New System.Web.UI.WebControls.ListItem(Me.mDescription, Me.mID)
    End Function

    Protected Overrides Function GetIdValue() As Object
        Return Me.mID
    End Function

#End Region

#Region " Validation Rules "

    Protected Overrides Sub AddBusinessRules()
        ValidationRules.AddRule(AddressOf SelectPermissionRequired, "PSelect")
    End Sub

    Private Function SelectPermissionRequired(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
        If Me.mPSelect = False AndAlso (Me.mPInsert = True OrElse Me.mPUpdate = True OrElse Me.mPDelete = True) Then
            e.Description = "Select permission is required for another permissions"
            Return False
        Else
            Return True
        End If
    End Function

#End Region

#Region " Authorization Rules "

    'Protected Overrides Sub AddAuthorizationRules()
    '    AuthorizationRules.AllowRead("IDForm", " ")
    '    AuthorizationRules.AllowRead("Description", " ")
    '    AuthorizationRules.AllowRead("Groups", " ")
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

    Friend Shared Function NewProfileForm(ByVal idForm As Long, ByVal idProfile As Long, ByVal pSelect As Boolean, ByVal pInsert As Boolean, ByVal pUpdate As Boolean, ByVal pDelete As Boolean) As ClsProfileForm
        Return New ClsProfileForm(idForm, idProfile, pSelect, pInsert, pUpdate, pDelete)
    End Function

    Friend Shared Function GetProfileForm(ByVal formPermissions As DAClsprgAdministrativeFormPermissions.Struct) As ClsProfileForm
        Return New ClsProfileForm(formPermissions)
    End Function

    Private Sub New(ByVal idForm As Long, ByVal idProfile As Long, ByVal pSelect As Boolean, ByVal pInsert As Boolean, ByVal pUpdate As Boolean, ByVal pDelete As Boolean)
        MarkAsChild()
        Fetch(idform, idProfile, pSelect, pInsert, pUpdate, pDelete)
    End Sub

    Private Sub New(ByVal formPermissions As DAClsprgAdministrativeFormPermissions.Struct)
        MarkAsChild()
        Fetch(formPermissions)
    End Sub

#End Region

#Region " Data Access "

    Private Sub Fetch(ByVal idForm As Long, ByVal idProfile As Long, ByVal pSelect As Boolean, ByVal pInsert As Boolean, ByVal pUpdate As Boolean, ByVal pDelete As Boolean)
        Dim forms As DAClsprgForms.Struct() = DAClsprgForms.Fetch(idForm)
        If forms.Length > 0 Then
            Me.LoadObjectData(forms(0), idProfile, pSelect, pInsert, pUpdate, pDelete)
            Me.ValidationRules.CheckRules("PSelect")
        Else
            Throw New System.Security.SecurityException("Form doesn't exist")
        End If
    End Sub

    Private Sub Fetch(ByVal formPermissions As DAClsprgAdministrativeFormPermissions.Struct)
        Dim forms As DAClsprgForms.Struct() = DAClsprgForms.Fetch(formPermissions.IDForm.Value)
        If forms.Length > 0 Then
            Me.mStruct = formPermissions
            Me.LoadObjectData(forms(0))
            Me.MarkOld()
        Else
            Throw New System.Security.SecurityException("Form doesn't exist")
        End If
    End Sub

    Friend Sub Insert(ByVal parents() As Object)
        ' if we're not dirty then don't update the database
        If Not Me.IsDirty Then Exit Sub

        Me.LoadTableStruct(parents)
        Me.mStruct = DAClsprgAdministrativeFormPermissions.Insert(Me.mStruct)
        Me.mGroups.Update(New Object() {parents(0), Me})
        MarkOld()
    End Sub

    Friend Sub Update(ByVal parents() As Object)
        ' if we're not dirty then don't update the database
        If Not Me.IsDirty Then Exit Sub

        Me.LoadTableStruct(parents)
        Me.mStruct = DAClsprgAdministrativeFormPermissions.Update(Me.mStruct)
        Me.mGroups.Update(New Object() {parents(0), Me})
        MarkOld()
    End Sub

    Friend Sub DeleteSelf(ByVal parents() As Object)
        ' if we're not dirty then don't update the database
        If Not Me.IsDirty Then Exit Sub

        ' if we're new then don't update the database
        If Me.IsNew Then Exit Sub

        Me.mGroups.Clear()
        Me.mGroups.Update(New Object() {parents(0), Me})
        DAClsprgAdministrativeFormPermissions.Delete(parents(0).ID, Me.mID)
        MarkNew()
    End Sub

    Private mStruct As DAClsprgAdministrativeFormPermissions.Struct = New DAClsprgAdministrativeFormPermissions.Struct

    Public Function GetTableStruct() As DAClsprgAdministrativeFormPermissions.Struct
        Return Me.mStruct
    End Function

    ''' <summary>
    ''' Populate the data of the object with the fetched data
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadObjectData(ByVal form As DAClsprgForms.Struct)
        Me.mID = form.ID.Value
        Me.mDescription = form.Description.Value
        Me.mProfile = ClsProfileInfo.GetProfileInfo(Me.mStruct.IDProfile.Value)
        Me.mGroups = ClsProfileGroupList.GetProfileFormGroupList(DAClsprgAdministrativeGroupPermissions.FetchByFormPermissions(Me.mStruct.IDProfile.Value, form.ID.Value))
        Me.mPSelect = Me.mStruct.pSelect.Value
        Me.mPInsert = Me.mStruct.pInsert.Value
        Me.mPUpdate = Me.mStruct.pUpdate.Value
        Me.mPDelete = Me.mStruct.pDelete.Value
    End Sub

    ''' <summary>
    ''' Populate the data of the object with the fetched data
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadObjectData(ByVal form As DAClsprgForms.Struct, ByVal idProfile As Long, ByVal pSelect As Boolean, ByVal pInsert As Boolean, ByVal pUpdate As Boolean, ByVal pDelete As Boolean)
        Me.mID = form.ID.Value
        Me.mDescription = form.Description.Value
        Me.mProfile = ClsProfileInfo.GetProfileInfo(idProfile)
        Me.mGroups = ClsProfileGroupList.NewProfileFormGroupList
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
            .IDForm.NewValue = Me.mID
            .IDProfile.NewValue = parents(0).ID
            .pSelect.NewValue = Me.mPSelect
            .pInsert.NewValue = Me.mPInsert
            .pUpdate.NewValue = Me.mPUpdate
            .pDelete.NewValue = Me.mPDelete
        End With
    End Sub

#End Region

#Region " Exists "

    Public Shared Function Exists(ByVal idProfile As Long, ByVal idForm As Long) As Boolean
        Return DataPortal.Execute(Of ExistsCommand)(New ExistsCommand(idProfile, idForm)).Exists
    End Function

    <Serializable()> Private Class ExistsCommand
        Inherits CommandBase

        Private mIDProfile As Long
        Private mIDForm As Long
        Private mExists As Boolean

        Public ReadOnly Property Exists() As Boolean
            Get
                Return Me.mExists
            End Get
        End Property

        Public Sub New(ByVal idProfile As Long, ByVal idForm As Long)
            Me.mIDProfile = idProfile
            Me.mIDForm = idForm
        End Sub

        Protected Overrides Sub DataPortal_Execute()
            Me.mExists = DAClsprgAdministrativeFormPermissions.Fetch(Me.mIDProfile, Me.mIDForm).Length > 0
        End Sub

    End Class

#End Region

End Class
