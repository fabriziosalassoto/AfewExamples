Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsUser
    Inherits BusinessBase(Of ClsUser)

#Region " Business Methods "

    Private mID As Long
    Private mProfile As ClsProfileInfo = ClsProfileInfo.NewProfileInfo
    Private mFirstName As String = String.Empty
    Private mLastName As String = String.Empty
    Private mLogin As String = String.Empty
    Private mPassword As String = String.Empty

    Public ReadOnly Property ID() As Long
        Get
            CanReadProperty(True)
            Return Me.mID
        End Get
    End Property

    Public Property Profile() As ClsProfileInfo
        Get
            CanReadProperty(True)
            Return Me.mProfile
        End Get
        Set(ByVal value As ClsProfileInfo)
            CanWriteProperty(True)
            If value IsNot Nothing AndAlso value.ID <> 0 Then
                If Me.mProfile.ID <> value.ID Then
                    Me.mProfile = value
                    ValidationRules.CheckRules("Profile")
                    PropertyHasChanged()
                End If
            Else
                Throw New System.Security.SecurityException("Profile required.")
            End If
        End Set
    End Property

    Public Property FirstName() As String
        Get
            CanReadProperty(True)
            Return Me.mFirstName
        End Get
        Set(ByVal value As String)
            CanWriteProperty(True)
            If Me.mFirstName <> value Then
                Me.mFirstName = value
                ValidationRules.CheckRules("FirstName")
                PropertyHasChanged()
            End If
        End Set
    End Property

    Public Property LastName() As String
        Get
            CanReadProperty(True)
            Return Me.mLastName
        End Get
        Set(ByVal value As String)
            CanWriteProperty(True)
            If Me.mLastName <> value Then
                Me.mLastName = value
                ValidationRules.CheckRules("LastName")
                PropertyHasChanged()
            End If
        End Set
    End Property

    Public ReadOnly Property FullName() As String
        Get
            If CanReadProperty("FirstName") AndAlso CanReadProperty("LastName") Then
                Return Me.mLastName & ", " & Me.mFirstName
            Else
                Throw New System.Security.SecurityException("FullName read not allowed")
            End If
        End Get
    End Property

    Public Property Login() As String
        Get
            CanReadProperty(True)
            Return Me.mLogin
        End Get
        Set(ByVal value As String)
            CanWriteProperty(True)
            If Me.mLogin <> value Then
                Me.mLogin = value
                ValidationRules.CheckRules("Login")
                PropertyHasChanged()
            End If
        End Set
    End Property

    Public Property Password() As String
        Get
            CanReadProperty(True)
            Return Me.mPassword
        End Get
        Set(ByVal value As String)
            CanWriteProperty(True)
            If Me.mPassword <> value Then
                Me.mPassword = value
                ValidationRules.CheckRules("Password")
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

    Public Sub AssignProfile(ByVal ProfileId As Long)
        If ProfileId <> 0 Then
            If Me.mProfile.ID <> ProfileId Then
                Me.mProfile = ClsProfileInfo.GetProfileInfo(ProfileId)
                PropertyHasChanged("Profile")
            End If
        Else
            Throw New System.Security.SecurityException("Profile required.")
        End If
    End Sub

    Protected Overrides Function GetIdValue() As Object
        Return Me.mID
    End Function

#End Region

#Region " Validation Rules "

    Protected Overrides Sub AddBusinessRules()
        ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, "FirstName")
        ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("FirstName", 100))

        ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, "LastName")
        ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("LastName", 100))

        ValidationRules.AddRule(AddressOf ProfileRequired, "Profile")

        ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("Login", 50))
        ValidationRules.AddRule(AddressOf ExistsLogin, "Login")

        ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("Password", 12))
        ValidationRules.AddRule(AddressOf IsValidPasswordFormat, "Password")
    End Sub

    Private Function ProfileRequired(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
        If Me.mProfile.ID = 0 Then
            e.Description = "Profile required."
            Return False
        Else
            Return True
        End If
    End Function

    Private Function ExistsLogin(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
        If ClsUser.Exists(Me.mID, Me.mLogin) Then
            e.Description = "Login already assigned to another user."
            Return False
        Else
            Return True
        End If
    End Function

    Private Function IsValidPasswordFormat(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
        If Me.mPassword <> String.Empty AndAlso Not Text.RegularExpressions.Regex.IsMatch(Me.mPassword, "\w{6,}") Then
            e.Description = "Invalid Password Format"
            Return False
        Else
            Return True
        End If
    End Function

#End Region

#Region " Authorization Rules "

    'Protected Overrides Sub AddAuthorizationRules()
    '    AuthorizationRules.AllowRead("ID", " ")
    '    AuthorizationRules.AllowRead("Profile", " ")
    '    AuthorizationRules.AllowRead("FirstName", " ")
    '    AuthorizationRules.AllowRead("LastName", " ")
    '    AuthorizationRules.AllowRead("Login", " ")
    '    AuthorizationRules.AllowRead("Password", " ")
    '    AuthorizationRules.AllowWrite("FirstName", New String() {" ", " "})
    '    AuthorizationRules.AllowWrite("LastName", New String() {" ", " "})
    '    AuthorizationRules.AllowWrite("Login", New String() {" ", " "})
    '    AuthorizationRules.AllowWrite("Password", New String() {" ", " "})
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

    Public Shared Function NewUser() As ClsUser
        If Not CanAddObject() Then
            Throw New System.Security.SecurityException("User not authorized to add User records.")
        End If
        Return DataPortal.Create(Of ClsUser)(New Criteria(0))
    End Function

    Public Shared Function GetUser(ByVal ID As Long) As ClsUser
        If Not CanGetObject() Then
            Throw New System.Security.SecurityException("User not authorized to view User records.")
        End If
        Return DataPortal.Fetch(Of ClsUser)(New Criteria(ID))
    End Function

    Public Shared Sub DeleteUser(ByVal ID As Long)
        If Not CanDeleteObject() Then
            Throw New System.Security.SecurityException("User not authorized to remove User records.")
        End If
        DataPortal.Delete(New Criteria(ID))
    End Sub

    Public Overrides Function Save() As ClsUser
        If IsDeleted AndAlso Not CanDeleteObject() Then
            Throw New System.Security.SecurityException("User not authorized to remove User records.")

        ElseIf IsNew AndAlso Not CanAddObject() Then
            Throw New System.Security.SecurityException("User not authorized to add User records.")

        ElseIf Not CanEditObject() Then
            Throw New System.Security.SecurityException("User not authorized to update User records.")
        End If
        Return MyBase.Save
    End Function

    Private Sub New()
        ' Require use of factory methods
    End Sub

#End Region

#Region " Child Methods "

    Friend Shared Function NewChildUser() As ClsUser
        Dim Child As New ClsUser
        Child.ValidationRules.CheckRules()
        Child.MarkAsChild()
        Return Child
    End Function

    Friend Shared Function GetChildUser(ByVal User As DAClsprgAdministrativeUsers.Struct) As ClsUser
        Return New ClsUser(User)
    End Function

    Friend Shared Function NewProfileUser() As ClsUser
        Dim Child As New ClsUser
        Child.ValidationRules.CheckRules()
        Child.MarkAsChild()
        Return Child
    End Function

    Friend Shared Function GetProfileUser(ByVal User As DAClsprgAdministrativeUsers.Struct) As ClsUser
        Return New ClsUser(User)
    End Function

    Private Sub New(ByVal AdministratorUser As DAClsprgAdministrativeUsers.Struct)
        MarkAsChild()
        Fetch(AdministratorUser)
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
        Dim List As DAClsprgAdministrativeUsers.Struct() = DAClsprgAdministrativeUsers.Fetch(criteria.ID)
        If List.Length = 0 Then Throw New System.Security.SecurityException("User doesn't exist")

        Me.mStruct = List(0)
        Me.LoadObjectData()
    End Sub

    <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Insert()
        Me.LoadTableStruct(New Object() {Me.mProfile})
        Me.mStruct = DAClsprgAdministrativeUsers.Insert(Me.mStruct)
        Me.mID = Me.mStruct.ID.Value
    End Sub

    <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Update()
        If MyBase.IsDirty Then
            Me.LoadTableStruct(New Object() {Me.mProfile})
            Me.mStruct = DAClsprgAdministrativeUsers.Update(Me.mStruct)
        End If
    End Sub

    <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_DeleteSelf()
        DataPortal_Delete(New Criteria(Me.mID))
    End Sub

    <Transactional(TransactionalTypes.TransactionScope)> Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)
        If Administrator.ClsBinnacleList.Exists(criteria.ID) Then Throw New System.Security.SecurityException("User is reference in Log Entries.")

        DAClsprgAdministrativeUsers.Delete(criteria.ID)
    End Sub

#End Region

#Region " Child Area "

    Private Sub Fetch(ByVal AdministratorUser As DAClsprgAdministrativeUsers.Struct)
        Me.mStruct = AdministratorUser
        Me.LoadObjectData()
        MarkOld()
    End Sub

    Friend Sub Insert(ByVal parent As Object)
        ' if we're not dirty then don't update the database
        If Not Me.IsDirty Then Exit Sub

        Me.LoadTableStruct(New Object() {parent})
        Me.mStruct = DAClsprgAdministrativeUsers.Insert(Me.mStruct)
        Me.mID = Me.mStruct.ID.Value
        MarkOld()
    End Sub

    Friend Sub Update(ByVal parent As Object)
        ' if we're not dirty then don't update the database
        If Not Me.IsDirty Then Exit Sub

        Me.LoadTableStruct(New Object() {parent})
        Me.mStruct = DAClsprgAdministrativeUsers.Update(Me.mStruct)
        MarkOld()
    End Sub

    Friend Sub DeleteSelf()
        ' if we're not dirty then don't update the database
        If Not Me.IsDirty Then Exit Sub

        ' if we're new then don't update the database
        If Me.IsNew Then Exit Sub

        If Administrator.ClsBinnacleList.Exists(Me.mID) Then Throw New System.Security.SecurityException("User is reference in Log Entries.")

        DAClsprgAdministrativeUsers.Delete(Me.mID)
        MarkNew()
    End Sub

#End Region

#Region " Common Area "

    Private mStruct As DAClsprgAdministrativeUsers.Struct = New DAClsprgAdministrativeUsers.Struct

    Public Function GetTableStruct() As DAClsprgAdministrativeUsers.Struct
        Return Me.mStruct
    End Function

    ''' <summary>
    ''' Load the data of the object with the fetched data
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadObjectData()
        With Me.mStruct
            Me.mID = .ID.Value
            Me.mFirstName = .FirstName.Value
            Me.mLastName = .LastName.Value
            Me.mLogin = .Login.Value
            Me.mPassword = .Password.Value
            Me.mProfile = ClsProfileInfo.GetProfileInfo(.IDProfile.Value)
        End With
    End Sub

    ''' <summary>
    ''' Collect the data of the object to fill to a structure of data and returns the structure 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadTableStruct(ByVal parents() As Object)
        With Me.mStruct
            .ID.NewValue = Me.mID
            .IDProfile.NewValue = parents(0).ID
            .FirstName.NewValue = Me.mFirstName
            .LastName.NewValue = Me.mLastName
            .Login.NewValue = Me.mLogin
            .Password.NewValue = Me.mPassword
        End With
    End Sub

#End Region

#End Region

#Region " Exists "

    Public Shared Function Exists(ByVal id As Long, ByVal login As String) As Boolean
        Return DataPortal.Execute(Of ExistsCommand)(New ExistsCommand(id, login)).Exists
    End Function

    <Serializable()> Private Class ExistsCommand
        Inherits CommandBase

        Private mID As Long
        Private mLogin As String
        Private mExists As Boolean

        Public ReadOnly Property Exists() As Boolean
            Get
                Return Me.mExists
            End Get
        End Property

        Public Sub New(ByVal id As Long, ByVal login As String)
            Me.mID = id
            Me.mLogin = login
        End Sub

        Protected Overrides Sub DataPortal_Execute()
            Dim users As DAClsprgAdministrativeUsers.Struct() = DAClsprgAdministrativeUsers.Fetch(Me.mLogin)
            Me.mExists = Me.mLogin <> String.Empty AndAlso users.Length > 0 AndAlso users(0).ID.Value <> Me.mID
        End Sub

    End Class

#End Region

End Class
