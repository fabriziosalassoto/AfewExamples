Imports Csla
Imports SCT.DataAccess

Namespace Advertiser
    <Serializable()> Public Class ClsAccount
        Inherits BusinessBase(Of ClsAccount)

#Region " Business Methods "

        Private mID As Long
        Private mCompanyName As String = String.Empty
        Private mCompanyNotes As String = String.Empty
        Private mLogin As String = String.Empty
        Private mClientPassword As String = String.Empty
        Private mWebPassword As String = String.Empty
        Private mHintByPassOne As String = String.Empty
        Private mHintByPassTwo As String = String.Empty
        Private mContacts As ClsContactList = ClsContactList.NewAdvertiserContacts
        Private mProjects As ClsProjectList = ClsProjectList.NewAdvertiserProjects

        Public ReadOnly Property ID() As Long
            Get
                CanReadProperty(True)
                Return Me.mID
            End Get
        End Property

        Public Property CompanyName() As String
            Get
                CanReadProperty(True)
                Return Me.mCompanyName
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mCompanyName <> value Then
                    Me.mCompanyName = value
                    ValidationRules.CheckRules("CompanyName")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property CompanyNotes() As String
            Get
                CanReadProperty(True)
                Return Me.mCompanyNotes
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mCompanyNotes <> value Then
                    Me.mCompanyNotes = value
                    ValidationRules.CheckRules("CompanyNotes")
                    PropertyHasChanged()
                End If
            End Set
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

        Public Property ClientPassword() As String
            Get
                CanReadProperty(True)
                Return Me.mClientPassword
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mClientPassword <> value Then
                    Me.mClientPassword = value
                    ValidationRules.CheckRules("ClientPassword")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property WebPassword() As String
            Get
                CanReadProperty(True)
                Return Me.mWebPassword
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mWebPassword <> value Then
                    Me.mWebPassword = value
                    ValidationRules.CheckRules("WebPassword")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property HintByPassOne() As String
            Get
                CanReadProperty(True)
                Return Me.mHintByPassOne
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mHintByPassOne <> value Then
                    Me.mHintByPassOne = value
                    ValidationRules.CheckRules("HintByPassOne")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property HintByPassTwo() As String
            Get
                CanReadProperty(True)
                Return Me.mHintByPassTwo
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mHintByPassTwo <> value Then
                    Me.mHintByPassTwo = value
                    ValidationRules.CheckRules("HintByPassTwo")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public ReadOnly Property Contacts() As ClsContactList
            Get
                Return Me.mContacts
            End Get
        End Property

        Public ReadOnly Property Projects() As ClsProjectList
            Get
                Return Me.mProjects
            End Get
        End Property

        Public Overrides ReadOnly Property IsValid() As Boolean
            Get
                Return MyBase.IsValid AndAlso Me.mProjects.IsValid AndAlso Me.mContacts.IsValid
            End Get
        End Property

        Public Overrides ReadOnly Property IsDirty() As Boolean
            Get
                Return MyBase.IsDirty OrElse Me.mProjects.IsDirty OrElse Me.mContacts.IsDirty
            End Get
        End Property

        Protected Overrides Function GetIdValue() As Object
            Return Me.mID
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()
            ValidationRules.AddRule(AddressOf Validation.StringRequired, "CompanyName")
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("CompanyName", 100))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("CompanyNotes", 100))

            ValidationRules.AddRule(AddressOf Validation.StringRequired, "Login")
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("Login", 50))
            ValidationRules.AddRule(AddressOf IsValidLogin, "Login")
            ValidationRules.AddRule(AddressOf ExistsAccountLogin, "Login")

            ValidationRules.AddRule(AddressOf Validation.StringRequired, "WebPassword")
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("WebPassword", 12))
            ValidationRules.AddRule(AddressOf IsValidWebPassword, "WebPassword")

            'ValidationRules.AddRule(AddressOf Validation.StringRequired, "ClientPassword")
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("ClientPassword", 12))
            'ValidationRules.AddRule(AddressOf IsValidClientPassword, "ClientPassword")

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("HintByPassOne", 50))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("HintByPassTwo", 50))
        End Sub

        Private Function ExistsAccountLogin(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
            If ClsAccount.Exists(Me.mID, Me.mLogin) Then
                e.Description = "Login is assigned to another"
                Return False
            Else
                Return True
            End If
        End Function

        Private Function IsValidLogin(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
            If Text.RegularExpressions.Regex.IsMatch(Me.mLogin, "\w+") Then
                Return True
            Else
                e.Description = "Invalid Login Password Format"
                Return False
            End If
        End Function

        Private Function IsValidWebPassword(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
            If Text.RegularExpressions.Regex.IsMatch(Me.mWebPassword, "\w{6,}") Then
                Return True
            Else
                e.Description = "Invalid Web Password Format"
                Return False
            End If
        End Function

        'Private Function IsValidClientPassword(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
        '    If Text.RegularExpressions.Regex.IsMatch(Me.mClientPassword, "\w{6,}") Then
        '        Return True
        '    Else
        '        e.Description = "Invalid Computer Password Format"
        '        Return False
        '    End If
        'End Function

#End Region

#Region " Authorization Rules "

        'Protected Overrides Sub AddAuthorizationRules()
        '    AuthorizationRules.AllowRead("ID", "")
        '    AuthorizationRules.AllowRead("CompanyName", "")
        '    AuthorizationRules.AllowRead("CompanyNotes", "")
        '    AuthorizationRules.AllowRead("Login", "")
        '    AuthorizationRules.AllowRead("WebPassword", "")
        '    AuthorizationRules.AllowRead("ClientPassword", "")
        '    AuthorizationRules.AllowRead("HintByPassOne", "")
        '    AuthorizationRules.AllowRead("HintByPassTwo", "")
        '    AuthorizationRules.AllowRead("Contacts", "")
        '    AuthorizationRules.AllowRead("Projects", "")
        '    AuthorizationRules.AllowWrite("CompanyName", "")
        '    AuthorizationRules.AllowWrite("CompanyNotes", "")
        '    AuthorizationRules.AllowWrite("Login", "")
        '    AuthorizationRules.AllowWrite("WebPassword", "")
        '    AuthorizationRules.AllowWrite("ClientPassword", "")
        '    AuthorizationRules.AllowWrite("HintByPassOne", "")
        '    AuthorizationRules.AllowWrite("HintByPassTwo", "")

        'End Sub

        Public Shared Function CanAddObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole("")
        End Function

        Public Shared Function CanGetObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole("")
        End Function

        Public Shared Function CanDeleteObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole("")
        End Function

        Public Shared Function CanEditObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole("")
        End Function

#End Region

#Region " Factory Methods "

#Region " Root Methods "

        Public Shared Function NewAccount() As ClsAccount
            If Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add a advertiser account")
            End If
            Return DataPortal.Create(Of ClsAccount)(New Criteria(0))
        End Function

        Public Shared Function GetAccount(ByVal id As Long) As ClsAccount
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view a advertiser account")
            End If
            Return DataPortal.Fetch(Of ClsAccount)(New Criteria(id))
        End Function

        Public Shared Sub DeleteAccount(ByVal id As Long)
            If Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove a advertiser account")
            End If
            DataPortal.Delete(New Criteria(id))
        End Sub

        Public Overrides Function Save() As ClsAccount
            If IsDeleted AndAlso Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove a advertiser account")

            ElseIf IsNew AndAlso Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add project a advertiser account")

            ElseIf Not CanEditObject() Then
                Throw New System.Security.SecurityException("User not authorized to update a advertiser account")
            End If
            Return MyBase.Save
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

#End Region

#Region " Child Methods "

        Friend Shared Function NewChildAccount() As ClsAccount
            Dim Child As New ClsAccount
            Child.ValidationRules.CheckRules("CompanyName")
            Child.ValidationRules.CheckRules("Login")
            Child.ValidationRules.CheckRules("WebPassword")
            Child.MarkAsChild()
            Return Child
        End Function

        Friend Shared Function GetChildAccount(ByVal account As DAClsappAdvertiserAccount.Struct) As ClsAccount
            Return New ClsAccount(account)
        End Function

        Private Sub New(ByVal account As DAClsappAdvertiserAccount.Struct)
            MarkAsChild()
            Fetch(account)
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

            Public Sub New(ByVal pID As Long)
                Me.mID = pID
            End Sub

        End Class

        <RunLocal()> Private Overloads Sub DataPortal_Create(ByVal criteria As Criteria)
            Me.ValidationRules.CheckRules("CompanyName")
            Me.ValidationRules.CheckRules("Login")
            Me.ValidationRules.CheckRules("WebPassword")
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            Dim List As DAClsappAdvertiserAccount.Struct() = DAClsappAdvertiserAccount.Fetch(criteria.ID)
            If List.Length = 0 Then Throw New System.Security.SecurityException("Advertiser account doesn't exist")

            Me.mStruct = List(0)
            Me.LoadObjectData()
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Insert()
            Me.LoadTableStruct()
            Me.mStruct = DAClsappAdvertiserAccount.Insert(Me.mStruct)
            Me.mID = Me.mStruct.ID.Value
            Me.mContacts.Update(Me)
            Me.mProjects.Update(Me)
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Update()
            If MyBase.IsDirty Then
                Me.LoadTableStruct()
                Me.mStruct = DAClsappAdvertiserAccount.Update(Me.mStruct)
            End If
            Me.mContacts.Update(Me)
            Me.mProjects.Update(Me)
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_DeleteSelf()
            DataPortal_Delete(New Criteria(Me.mID))
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)
            Me.mProjects.Clear()
            Me.mProjects.Update(Me)
            Me.mContacts.Clear()
            Me.mContacts.Update(Me)
            DAClsappAdvertiserAccount.Delete(criteria.ID)
        End Sub

#End Region

#Region " Child Area "

        Private Sub Fetch(ByVal account As DAClsappAdvertiserAccount.Struct)
            Me.mStruct = account
            Me.LoadObjectData()
            MarkOld()
        End Sub

        Friend Sub Insert(ByVal parent As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct()
            Me.mStruct = DAClsappAdvertiserAccount.Insert(Me.mStruct)
            Me.mID = Me.mStruct.ID.Value
            Me.mContacts.Update(Me)
            Me.mProjects.Update(Me)
            MarkOld()
        End Sub

        Friend Sub Update(ByVal parent As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct()
            Me.mStruct = DAClsappAdvertiserAccount.Update(Me.mStruct)
            Me.mContacts.Update(Me)
            Me.mProjects.Update(Me)
            MarkOld()
        End Sub

        Friend Sub DeleteSelf()
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            ' if we're new then don't update the database
            If Me.IsNew Then Exit Sub

            Me.mProjects.Clear()
            Me.mProjects.Update(Me)
            Me.mContacts.Clear()
            Me.mContacts.Update(Me)
            DAClsappAdvertiserAccount.Delete(Me.mID)
            MarkNew()
        End Sub

#End Region

#Region " Common Area "

        Private mStruct As DAClsappAdvertiserAccount.Struct = New DAClsappAdvertiserAccount.Struct

        Public Function GetTableStruct() As DAClsappAdvertiserAccount.Struct
            Return Me.mStruct
        End Function

        ''' <summary>
        ''' Load the data of the object with the fetched data
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadObjectData()
            With Me.mStruct
                Me.mID = .ID.Value
                Me.mCompanyName = .CompanyName.Value
                Me.mCompanyNotes = .CompanyNotes.Value
                Me.mLogin = .Login.Value
                Me.mClientPassword = .ClientPassword.Value
                Me.mWebPassword = .WebPassword.Value
                Me.mHintByPassOne = .HintByPassOne.Value
                Me.mHintByPassTwo = .HintByPassTwo.Value
                Me.mContacts = ClsContactList.GetAdvertiserContacts(DAClsappAdvertiserContactInfo.FetchAdvertiserContactInfo(.ID.Value))
                Me.mProjects = ClsProjectList.GetAdvertiserProjects(DAClsappAdvertiserProjects.FetchAdvertiserProject(.ID.Value))
            End With
        End Sub

        ''' <summary>
        ''' Collect the data of the object to fill to a structure of data and returns the structure 
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadTableStruct()
            With Me.mStruct
                .ID.NewValue = Me.mID
                .CompanyName.NewValue = Me.mCompanyName
                .CompanyNotes.NewValue = Me.mCompanyNotes
                .Login.NewValue = Me.mLogin
                .ClientPassword.NewValue = Me.mClientPassword
                .WebPassword.NewValue = Me.mWebPassword
                .HintByPassOne.NewValue = Me.mHintByPassOne
                .HintByPassTwo.NewValue = Me.mHintByPassTwo
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
                Dim adAccount As DAClsappAdvertiserAccount.Struct() = DAClsappAdvertiserAccount.Fetch(Me.mLogin)
                Me.mExists = adAccount.Length > 0 AndAlso adAccount(0).ID.Value <> Me.mID
            End Sub

        End Class

#End Region

    End Class
End Namespace