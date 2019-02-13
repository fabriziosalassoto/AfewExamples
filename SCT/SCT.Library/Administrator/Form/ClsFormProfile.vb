Imports Csla
Imports SCT.DataAccess
Imports System.ComponentModel

<Serializable()> Public Class ClsFormProfile
    Inherits BusinessBase(Of ClsFormProfile)

#Region " Business Methods "

    Private mID As Long
    Private mDescription As String = String.Empty
    Private mForm As ClsFormInfo = ClsFormInfo.NewFormInfo
    Private mPSelect As Boolean = False
    Private mPInsert As Boolean = False
    Private mPUpdate As Boolean = False
    Private mPDelete As Boolean = False
    Private mGroupProfiles As ClsGroupProfileList = ClsGroupProfileList.NewGroupProfileList

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

    Public ReadOnly Property Form() As ClsFormInfo
        Get
            CanReadProperty(True)
            Return Me.mForm
        End Get
    End Property

    Public ReadOnly Property GroupProfiles() As ClsGroupProfileList
        Get
            CanReadProperty(True)
            Return Me.mGroupProfiles
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
            Return MyBase.IsValid AndAlso Me.GroupProfiles.IsValid
        End Get
    End Property

    Public Overrides ReadOnly Property IsDirty() As Boolean
        Get
            Return MyBase.IsDirty OrElse Me.GroupProfiles.IsDirty
        End Get
    End Property

    Public Function ToListItem() As System.Web.UI.WebControls.ListItem
        Return New System.Web.UI.WebControls.ListItem(Me.mDescription, Me.mID)
    End Function

    Public Function CanExecute(ByVal pName As String) As Boolean
        If Len(pName) > 0 Then
            For Each prop As PropertyDescriptor In TypeDescriptor.GetProperties(Me)
                If prop.Name = pName Then
                    Return Me.mPSelect AndAlso prop.GetValue(Me)
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
        ValidationRules.AddRule(AddressOf IsSelectRequired, "PSelect")
    End Sub

    Private Function IsSelectRequired(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
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

    Friend Shared Function NewFormProfile(ByVal idProfile As Long, ByVal idForm As Long, ByVal pSelect As Boolean, ByVal pInsert As Boolean, ByVal pUpdate As Boolean, ByVal pDelete As Boolean) As ClsFormProfile
        Return New ClsFormProfile(idProfile, idForm, pSelect, pInsert, pUpdate, pDelete)
    End Function

    Friend Shared Function GetFormProfile(ByVal formPermissions As DAClsprgAdministrativeFormPermissions.Struct) As ClsFormProfile
        Return New ClsFormProfile(formPermissions)
    End Function

    Private Sub New(ByVal idProfile As Long, ByVal idForm As Long, ByVal pSelect As Boolean, ByVal pInsert As Boolean, ByVal pUpdate As Boolean, ByVal pDelete As Boolean)
        MarkAsChild()
        Fetch(idProfile, idForm, pSelect, pInsert, pUpdate, pDelete)
    End Sub

    Private Sub New(ByVal formPermissions As DAClsprgAdministrativeFormPermissions.Struct)
        MarkAsChild()
        Fetch(formPermissions)
    End Sub

#End Region

#Region " Data Access "

    Private Sub Fetch(ByVal idProfile As Long, ByVal idform As Long, ByVal pSelect As Boolean, ByVal pInsert As Boolean, ByVal pUpdate As Boolean, ByVal pDelete As Boolean)
        Dim list As DAClsprgAdministrativeProfiles.Struct() = DAClsprgAdministrativeProfiles.Fetch(idProfile)
        If list.Length > 0 Then
            Me.LoadObjectData(list(0), idform, pSelect, pInsert, pUpdate, pDelete)
            Me.ValidationRules.CheckRules("PSelect")
        Else
            Throw New System.Security.SecurityException("Profile doesn't assign to form")
        End If
    End Sub

    Private Sub Fetch(ByVal formPermissions As DAClsprgAdministrativeFormPermissions.Struct)
        Dim list As DAClsprgAdministrativeProfiles.Struct() = DAClsprgAdministrativeProfiles.Fetch(formPermissions.IDProfile.Value)
        If list.Length > 0 Then
            Me.mStruct = formPermissions
            Me.LoadObjectData(list(0))
            Me.MarkOld()
        Else
            Throw New System.Security.SecurityException("Profile doesn't assign to form")
        End If
    End Sub

    Friend Sub Insert(ByVal parent As Object)
        ' if we're not dirty then don't update the database
        If Not Me.IsDirty Then Exit Sub

        Me.LoadTableStruct(New Object() {parent})
        Me.mStruct = DAClsprgAdministrativeFormPermissions.Insert(Me.mStruct)
        Me.mGroupProfiles.Update(Me)
        MarkOld()
    End Sub

    Friend Sub Update(ByVal parent As Object)
        ' if we're not dirty then don't update the database
        If Not Me.IsDirty Then Exit Sub

        Me.LoadTableStruct(New Object() {parent})
        Me.mStruct = DAClsprgAdministrativeFormPermissions.Update(Me.mStruct)
        Me.mGroupProfiles.Update(Me)
        MarkOld()
    End Sub

    Friend Sub DeleteSelf(ByVal parent As Object)
        ' if we're not dirty then don't update the database
        If Not Me.IsDirty Then Exit Sub

        ' if we're new then don't update the database
        If Me.IsNew Then Exit Sub

        Me.mGroupProfiles.Clear()
        Me.mGroupProfiles.Update(Me)
        DAClsprgAdministrativeFormPermissions.Delete(Me.mID, parent.ID)
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
    Private Sub LoadObjectData(ByVal profile As DAClsprgAdministrativeProfiles.Struct)
        Me.mID = profile.ID.Value
        Me.mDescription = profile.Description.Value
        Me.mForm = ClsFormInfo.GetFormInfo(Me.mStruct.IDForm.Value)
        Me.mGroupProfiles = ClsGroupProfileList.GetGroupProfileList(DAClsprgAdministrativeGroupPermissions.FetchByFormPermissions(profile.ID.Value, Me.mStruct.IDForm.Value))
        Me.mPSelect = Me.mStruct.pSelect.Value
        Me.mPInsert = Me.mStruct.pInsert.Value
        Me.mPUpdate = Me.mStruct.pUpdate.Value
        Me.mPDelete = Me.mStruct.pDelete.Value
    End Sub

    ''' <summary>
    ''' Populate the data of the object with the fetched data
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadObjectData(ByVal profile As DAClsprgAdministrativeProfiles.Struct, ByVal idform As Long, ByVal pSelect As Boolean, ByVal pInsert As Boolean, ByVal pUpdate As Boolean, ByVal pDelete As Boolean)
        Me.mID = profile.ID.Value
        Me.mDescription = profile.Description.Value
        Me.mForm = ClsFormInfo.GetFormInfo(idform)
        Me.mGroupProfiles = ClsGroupProfileList.NewGroupProfileList
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
            .IDProfile.NewValue = Me.mID
            .IDForm.NewValue = parents(0).ID
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
