Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsProfile
    Inherits BusinessBase(Of ClsProfile)

#Region " Business Methods "

    Private mID As Long
    Private mDescription As String = String.Empty
    Private mForms As ClsProfileFormList = ClsProfileFormList.NewProfileFormList

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

    Public ReadOnly Property Forms() As ClsProfileFormList
        Get
            CanReadProperty(True)
            Return Me.mForms
        End Get
    End Property

    Public Overrides ReadOnly Property IsValid() As Boolean
        Get
            Return MyBase.IsValid AndAlso Me.mForms.IsValid
        End Get
    End Property

    Public Overrides ReadOnly Property IsDirty() As Boolean
        Get
            Return MyBase.IsDirty OrElse Me.mForms.IsDirty
        End Get
    End Property

    Protected Overrides Function GetIdValue() As Object
        Return Me.mID
    End Function

#End Region

#Region " Validation Rules "

    Protected Overrides Sub AddBusinessRules()
        ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, "Description")
        ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("Description", 50))
        ValidationRules.AddRule(AddressOf ExistsDescription, "Description")
    End Sub

    Private Function ExistsDescription(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
        If ClsProfile.Exists(Me.mID, Me.mDescription) Then
            e.Description = "Description already assigned to another Profile."
            Return False
        Else
            Return True
        End If
    End Function

#End Region

#Region " Authorization Rules "

    'Protected Overrides Sub AddAuthorizationRules()
    '    AuthorizationRules.AllowRead("ID", " ")
    '    AuthorizationRules.AllowRead("Description", " ")
    '    AuthorizationRules.AllowRead("Forms", "")
    '    AuthorizationRules.AllowRead("Users", "")
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

    Public Shared Function NewProfile() As ClsProfile
        If Not CanAddObject() Then
            Throw New System.Security.SecurityException("User not authorized to add Profile records.")
        End If
        Return DataPortal.Create(Of ClsProfile)(New Criteria(0))
    End Function

    Public Shared Function GetProfile(ByVal ID As Long) As ClsProfile
        If Not CanGetObject() Then
            Throw New System.Security.SecurityException("User not authorized to view Profile records.")
        End If
        Return DataPortal.Fetch(Of ClsProfile)(New Criteria(ID))
    End Function

    Public Shared Sub DeleteProfile(ByVal ID As Long)
        If Not CanDeleteObject() Then
            Throw New System.Security.SecurityException("User not authorized to remove Profile records.")
        End If
        DataPortal.Delete(New Criteria(ID))
    End Sub

    Public Overrides Function Save() As ClsProfile
        If IsDeleted AndAlso Not CanDeleteObject() Then
            Throw New System.Security.SecurityException("User not authorized to remove Profile records.")

        ElseIf IsNew AndAlso Not CanAddObject() Then
            Throw New System.Security.SecurityException("User not authorized to add Profile records.")

        ElseIf Not CanEditObject() Then
            Throw New System.Security.SecurityException("User not authorized to update Profile records.")
        End If
        Return MyBase.Save
    End Function

    Private Sub New()
        ' Require use of factory methods
    End Sub

#End Region

#Region " Child Methods "

    Friend Shared Function NewChildProfile() As ClsProfile
        Dim Child As New ClsProfile
        Child.ValidationRules.CheckRules()
        Child.MarkAsChild()
        Return Child
    End Function

    Friend Shared Function GetChildProfile(ByVal Profile As DAClsprgAdministrativeProfiles.Struct) As ClsProfile
        Return New ClsProfile(Profile)
    End Function

    Private Sub New(ByVal Profile As DAClsprgAdministrativeProfiles.Struct)
        MarkAsChild()
        Fetch(Profile)
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
        ValidationRules.CheckRules()
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
        Dim List As DAClsprgAdministrativeProfiles.Struct() = DAClsprgAdministrativeProfiles.Fetch(criteria.ID)
        If List.Length = 0 Then Throw New System.Security.SecurityException("Profile doesn't exist")

        Me.mStruct = List(0)
        Me.LoadObjectData()
    End Sub

    <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Insert()
        Me.LoadTableStruct(Nothing)
        Me.mStruct = DAClsprgAdministrativeProfiles.Insert(Me.mStruct)
        Me.mID = Me.mStruct.ID.Value
        Me.mForms.Update(New Object() {Me})
    End Sub

    <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Update()
        If MyBase.IsDirty Then
            Me.LoadTableStruct(Nothing)
            Me.mStruct = DAClsprgAdministrativeProfiles.Update(Me.mStruct)
        End If
        Me.mForms.Update(New Object() {Me})
    End Sub

    <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_DeleteSelf()
        DataPortal_Delete(New Criteria(Me.mID))
    End Sub

    <Transactional(TransactionalTypes.TransactionScope)> Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)
        If ClsUserList.Exists(criteria.ID) Then Throw New System.Security.SecurityException("Users assigned to profile")

        Me.mForms.Clear()
        Me.mForms.Update(New Object() {Me})
        DAClsprgAdministrativeProfiles.Delete(criteria.ID)
    End Sub

#End Region

#Region " Child Area "

    Private Sub Fetch(ByVal Profile As DAClsprgAdministrativeProfiles.Struct)
        Me.mStruct = Profile
        Me.LoadObjectData()
        MarkOld()
    End Sub

    Friend Sub Insert(ByVal parent As Object)
        ' if we're not dirty then don't update the database
        If Not Me.IsDirty Then Exit Sub

        Me.LoadTableStruct(New Object() {parent})
        Me.mStruct = DAClsprgAdministrativeProfiles.Insert(Me.mStruct)
        Me.mID = Me.mStruct.ID.Value
        Me.mForms.Update(New Object() {Me})
        MarkOld()
    End Sub

    Friend Sub Update(ByVal parent As Object)
        ' if we're not dirty then don't update the database
        If Not Me.IsDirty Then Exit Sub

        Me.LoadTableStruct(New Object() {parent})
        Me.mStruct = DAClsprgAdministrativeProfiles.Insert(Me.mStruct)
        Me.mForms.Update(New Object() {Me})
        MarkOld()
    End Sub

    Friend Sub DeleteSelf()
        ' if we're not dirty then don't update the database
        If Not Me.IsDirty Then Exit Sub

        ' if we're new then don't update the database
        If Me.IsNew Then Exit Sub

        If ClsUserList.Exists(Me.mID) Then Throw New System.Security.SecurityException("Profile assigned to users.")

        Me.mForms.Clear()
        Me.mForms.Update(New Object() {Me})
        DAClsprgAdministrativeProfiles.Delete(Me.mID)
        MarkNew()
    End Sub

#End Region

#Region " Common Area "

    Private mStruct As DAClsprgAdministrativeProfiles.Struct = New DAClsprgAdministrativeProfiles.Struct

    Public Function GetTableStruct() As DAClsprgAdministrativeProfiles.Struct
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
            Me.mForms = ClsProfileFormList.GetProfileFormList(DAClsprgAdministrativeFormPermissions.FetchByProfile(.ID.Value))
        End With
    End Sub

    ''' <summary>
    ''' Collect the data of the object to fill to a structure of data and returns the structure 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadTableStruct(ByVal parents() As Object)
        With Me.mStruct
            .ID.NewValue = Me.mID
            .Description.NewValue = Me.mDescription
        End With
    End Sub

#End Region

#End Region

#Region " Exists "

    Public Shared Function Exists(ByVal id As Long) As Boolean
        Return DataPortal.Execute(Of IDExistsCommand)(New IDExistsCommand(id)).Exists
    End Function

    <Serializable()> Private Class IDExistsCommand
        Inherits CommandBase

        Private mID As Long
        Private mExists As Boolean

        Public ReadOnly Property Exists() As Boolean
            Get
                Return Me.mExists
            End Get
        End Property

        Public Sub New(ByVal id As Long)
            Me.mID = id
        End Sub

        Protected Overrides Sub DataPortal_Execute()
            Me.mExists = DAClsprgAdministrativeProfiles.Fetch(Me.mID).Length > 0
        End Sub

    End Class

    Public Shared Function Exists(ByVal id As Long, ByVal description As String) As Boolean
        Return DataPortal.Execute(Of IDDescriptionExistsCommand)(New IDDescriptionExistsCommand(id, description)).Exists
    End Function

    <Serializable()> Private Class IDDescriptionExistsCommand
        Inherits CommandBase

        Private mID As Long
        Private mDescription As String
        Private mExists As Boolean

        Public ReadOnly Property Exists() As Boolean
            Get
                Return Me.mExists
            End Get
        End Property

        Public Sub New(ByVal id As Long, ByVal description As String)
            Me.mID = id
            Me.mDescription = description
        End Sub

        Protected Overrides Sub DataPortal_Execute()
            Dim profiles As DAClsprgAdministrativeProfiles.Struct() = DAClsprgAdministrativeProfiles.FetchByDescription(Me.mDescription)
            Me.mExists = profiles.Length > 0 AndAlso profiles(0).ID.Value <> Me.mID
        End Sub

    End Class

#End Region

End Class
