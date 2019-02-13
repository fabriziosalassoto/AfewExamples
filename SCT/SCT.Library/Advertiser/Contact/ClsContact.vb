Imports Csla
Imports SCT.DataAccess
Imports SCT.Library.Advertiser.Contact

Namespace Advertiser
    <Serializable()> Public Class ClsContact
        Inherits BusinessBase(Of ClsContact)

#Region " Business Methods "

        Private mID As Long
        Private mAdvertiser As ClsAccountInfo = ClsAccountInfo.NewAccountInfo
        Private mFirstName As String = String.Empty
        Private mLastName As String = String.Empty
        Private mMainCompanyAddress As Boolean = False
        Private mPrimaryAddress As String = String.Empty
        Private mSecondaryAddress As String = String.Empty
        Private mCity As String = String.Empty
        Private mState As String = String.Empty
        Private mCountry As String = String.Empty
        Private mZipCode As String = String.Empty
        Private mProvidence As String = String.Empty
        Private mDepartment As String = String.Empty
        Private mResposibleForNotes As String = String.Empty
        Private mNotes As ClsNoteList = ClsNoteList.NewContactNotes
        Private mToDos As ClsToDoList = ClsToDoList.NewContactToDos
        Private mProjects As ClsProjectList = ClsProjectList.NewContactProjects

        Public ReadOnly Property ID() As Long
            Get
                CanReadProperty(True)
                Return Me.mID
            End Get
        End Property

        Public Property Advertiser() As ClsAccountInfo
            Get
                CanReadProperty(True)
                Return Me.mAdvertiser
            End Get
            Set(ByVal value As ClsAccountInfo)
                CanWriteProperty(True)
                If value IsNot Nothing AndAlso value.ID <> 0 Then
                    If Me.mAdvertiser.ID <> value.ID Then
                        Me.mAdvertiser = value
                        ValidationRules.CheckRules("Advertiser")
                        PropertyHasChanged()
                    End If
                Else
                    Throw New System.Security.SecurityException("Advertiser Account required.")
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

        Public Property MainCompanyAddress() As Boolean
            Get
                CanReadProperty(True)
                Return Me.mMainCompanyAddress
            End Get
            Set(ByVal value As Boolean)
                CanWriteProperty(True)
                If Me.mMainCompanyAddress <> value Then
                    Me.mMainCompanyAddress = value
                    ValidationRules.CheckRules("MainCompanyAddress")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property PrimaryAddress() As String
            Get
                CanReadProperty(True)
                Return Me.mPrimaryAddress
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mPrimaryAddress <> value Then
                    Me.mPrimaryAddress = value
                    ValidationRules.CheckRules("PrimaryAddress")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property SecondaryAddress() As String
            Get
                CanReadProperty(True)
                Return Me.mSecondaryAddress
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mSecondaryAddress <> value Then
                    Me.mSecondaryAddress = value
                    ValidationRules.CheckRules("SecondaryAddress")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property City() As String
            Get
                CanReadProperty(True)
                Return Me.mCity
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mCity <> value Then
                    Me.mCity = value
                    ValidationRules.CheckRules("City")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property State() As String
            Get
                CanReadProperty(True)
                Return Me.mState
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mState <> value Then
                    Me.mState = value
                    ValidationRules.CheckRules("State")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property Country() As String
            Get
                CanReadProperty(True)
                Return Me.mCountry
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mCountry <> value Then
                    Me.mCountry = value
                    ValidationRules.CheckRules("Country")
                    PropertyHasChanged()
                End If
            End Set
        End Property


        Public Property ZipCode() As String
            Get
                CanReadProperty(True)
                Return Me.mZipCode
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mZipCode <> value Then
                    Me.mZipCode = value
                    ValidationRules.CheckRules("ZipCode")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property Providence() As String
            Get
                CanReadProperty(True)
                Return Me.mProvidence
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mProvidence <> value Then
                    Me.mProvidence = value
                    ValidationRules.CheckRules("Providence")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property Department() As String
            Get
                CanReadProperty(True)
                Return Me.mDepartment
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mDepartment <> value Then
                    Me.mDepartment = value
                    ValidationRules.CheckRules("Department")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property ResposibleForNotes() As String
            Get
                CanReadProperty(True)
                Return Me.mResposibleForNotes
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mResposibleForNotes <> value Then
                    Me.mResposibleForNotes = value
                    ValidationRules.CheckRules("ResposibleForNotes")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public ReadOnly Property Notes() As ClsNoteList
            Get
                Return Me.mNotes
            End Get
        End Property

        Public ReadOnly Property ToDos() As ClsToDoList
            Get
                Return Me.mToDos
            End Get
        End Property

        Public ReadOnly Property Projects() As ClsProjectList
            Get
                Return Me.mProjects
            End Get
        End Property

        Public Overrides ReadOnly Property IsValid() As Boolean
            Get
                Return MyBase.IsValid AndAlso Me.mProjects.IsValid AndAlso Me.mNotes.IsValid AndAlso Me.mToDos.IsValid
            End Get
        End Property

        Public Overrides ReadOnly Property IsDirty() As Boolean
            Get
                Return MyBase.IsDirty OrElse Me.mProjects.IsDirty OrElse Me.mNotes.IsDirty OrElse Me.mToDos.IsDirty
            End Get
        End Property

        Public Sub AssignAdvertiserAccount(ByVal accountId As Long)
            If accountId <> 0 Then
                If Me.mAdvertiser.ID <> accountId Then
                    Me.mAdvertiser = ClsAccountInfo.GetAccountInfo(accountId)
                    ValidationRules.CheckRules("Advertiser")
                    PropertyHasChanged("Advertiser")
                End If
            Else
                Throw New System.Security.SecurityException("Advertiser Account required.")
            End If
        End Sub

        Protected Overrides Function GetIdValue() As Object
            Return Me.mID
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()
            ValidationRules.AddRule(AddressOf AdvertiserAccountRequired, "Advertiser")
            ValidationRules.AddRule(AddressOf IsAdvertiserProjects, "Advertiser")

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, "FirstName")
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("FirstName", 100))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, "LastName")
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("LastName", 100))

            ValidationRules.AddRule(AddressOf ExistMainCompanyAddress, "MainCompanyAddress")

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, "PrimaryAddress")
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("PrimaryAddress", 100))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("SecondaryAddress", 100))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("City", 50))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("State", 5))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("Country", 5))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("ZipCode", 10))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("Providence", 50))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("Department", 50))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("ResposibleForNotes", 1000))
        End Sub

        Private Function AdvertiserAccountRequired(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
            If Me.mAdvertiser.ID = 0 Then
                e.Description = "Advertiser Account required."
                Return False
            Else
                Return True
            End If
        End Function


        Private Function IsAdvertiserProjects(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
            For Each project As ClsProject In Me.Projects
                If Me.mAdvertiser.ID <> project.AdvertiserAccount.ID Then
                    e.Description = "Contact assigned to another projects advertiser."
                    Return False
                End If
            Next
            Return True
        End Function

        Private Function ExistMainCompanyAddress(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
            If ClsContact.ExistsMainCompanyAddress(Me.mID, Me.mAdvertiser.ID, Me.mMainCompanyAddress) Then
                e.Description = "Main Company Address assigned to another contact"
                Return False
            Else
                Return True
            End If
        End Function

#End Region

#Region " Authorization Rules "

        'Protected Overrides Sub AddAuthorizationRules()
        '    AuthorizationRules.AllowRead("ID", "")
        '    AuthorizationRules.AllowRead("Advertiser", "")
        '    AuthorizationRules.AllowRead("FirstName", "")
        '    AuthorizationRules.AllowRead("LastName", "")
        '    AuthorizationRules.AllowRead("FullName", "")
        '    AuthorizationRules.AllowRead("MainCompanyAddress", "")
        '    AuthorizationRules.AllowRead("PrimaryAddress", "")
        '    AuthorizationRules.AllowRead("SecondaryAddress", "")
        '    AuthorizationRules.AllowRead("City", "")
        '    AuthorizationRules.AllowRead("State", "")
        '    AuthorizationRules.AllowRead("Country", "")
        '    AuthorizationRules.AllowRead("ZipCode", "")
        '    AuthorizationRules.AllowRead("Providence", "")
        '    AuthorizationRules.AllowRead("Department", "")
        '    AuthorizationRules.AllowRead("ResposibleForNotes", "")
        '    AuthorizationRules.AllowRead("Notes", "")
        '    AuthorizationRules.AllowRead("ToDos", "")
        '    AuthorizationRules.AllowWrite("FirstName", "")
        '    AuthorizationRules.AllowWrite("LastName", "")
        '    AuthorizationRules.AllowWrite("MainCompanyAddress", "")
        '    AuthorizationRules.AllowWrite("PrimaryAddress", "")
        '    AuthorizationRules.AllowWrite("SecondaryAddress", "")
        '    AuthorizationRules.AllowWrite("City", "")
        '    AuthorizationRules.AllowWrite("State", "")
        '    AuthorizationRules.AllowWrite("ZipCode", "")
        '    AuthorizationRules.AllowWrite("Providence", "")
        '    AuthorizationRules.AllowWrite("Department", "")
        '    AuthorizationRules.AllowWrite("ResposibleForNotes", "")
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

        Public Shared Function NewContact() As ClsContact
            If Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add a contact")
            End If
            Return DataPortal.Create(Of ClsContact)(New Criteria(0))
        End Function

        Public Shared Function GetContact(ByVal id As Long) As ClsContact
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view a contact")
            End If
            Return DataPortal.Fetch(Of ClsContact)(New Criteria(id))
        End Function

        Public Shared Sub DeleteContact(ByVal id As Long)
            If Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove a contact")
            End If
            DataPortal.Delete(New Criteria(id))
        End Sub

        Public Overrides Function Save() As ClsContact
            If IsDeleted AndAlso Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove a contact")

            ElseIf IsNew AndAlso Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add project a contact")

            ElseIf Not CanEditObject() Then
                Throw New System.Security.SecurityException("User not authorized to update a contact")
            End If
            Return MyBase.Save
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

#End Region

#Region " Child Methods "

        Friend Shared Function NewChildContact() As ClsContact
            Dim Child As New ClsContact
            Child.ValidationRules.CheckRules("Advertiser")
            Child.ValidationRules.CheckRules("FirstName")
            Child.ValidationRules.CheckRules("LastName")
            Child.ValidationRules.CheckRules("PrimaryAddress")
            Child.MarkAsChild()
            Return Child
        End Function

        Friend Shared Function GetChildContact(ByVal contact As DAClsappAdvertiserContactInfo.Struct) As ClsContact
            Return New ClsContact(contact)
        End Function

        Friend Shared Function NewAdvertiserContact() As ClsContact
            Dim Child As New ClsContact
            Child.ValidationRules.CheckRules("FirstName")
            Child.ValidationRules.CheckRules("LastName")
            Child.ValidationRules.CheckRules("PrimaryAddress")
            Child.MarkAsChild()
            Return Child
        End Function

        Friend Shared Function GetAdvertiserContact(ByVal contact As DAClsappAdvertiserContactInfo.Struct) As ClsContact
            Return New ClsContact(contact)
        End Function

        Private Sub New(ByVal contact As DAClsappAdvertiserContactInfo.Struct)
            MarkAsChild()
            Fetch(contact)
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
            Me.ValidationRules.CheckRules("Advertiser")
            Me.ValidationRules.CheckRules("FirstName")
            Me.ValidationRules.CheckRules("LastName")
            Me.ValidationRules.CheckRules("PrimaryAddress")
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            Dim List As DAClsappAdvertiserContactInfo.Struct() = DAClsappAdvertiserContactInfo.Fetch(criteria.ID)
            If List.Length = 0 Then Throw New System.Security.SecurityException("Contact doesn't exist")

            Me.mStruct = List(0)
            Me.LoadObjectData()
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Insert()
            Me.LoadTableStruct(Me.mAdvertiser)
            Me.mStruct = DAClsappAdvertiserContactInfo.Insert(Me.mStruct)
            Me.mID = Me.mStruct.ID.Value
            Me.mNotes.Update(Me)
            Me.mToDos.Update(Me)
            Me.mProjects.Update(New Object() {Me.mAdvertiser, Me})
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Update()
            If MyBase.IsDirty Then
                Me.LoadTableStruct(Me.mAdvertiser)
                Me.mStruct = DAClsappAdvertiserContactInfo.Update(Me.mStruct)
            End If
            Me.mNotes.Update(Me)
            Me.mToDos.Update(Me)
            Me.mProjects.Update(New Object() {Me.mAdvertiser, Me})
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_DeleteSelf()
            DataPortal_Delete(New Criteria(Me.mID))
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)
            If Me.Projects.Count > 0 Then Throw New Exception("Contact is related to one or more Projects and can not be deleted.")
            If Me.Notes.Count > 0 Then Throw New Exception("Contact is related to one or more Notes and can not be deleted.")
            If Me.mToDos.Count > 0 Then Throw New Exception("Contact is related to one or more ToDo and can not be deleted.")

            DAClsappAdvertiserContactInfo.Delete(criteria.ID)
        End Sub

#End Region

#Region " Child Area "

        Private Sub Fetch(ByVal contact As DAClsappAdvertiserContactInfo.Struct)
            Me.mStruct = contact
            Me.LoadObjectData()
            MarkOld()
        End Sub

        Friend Sub Insert(ByVal parent As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct(parent)
            Me.mStruct = DAClsappAdvertiserContactInfo.Insert(Me.mStruct)
            Me.mID = Me.mStruct.ID.Value
            Me.mNotes.Update(Me)
            Me.mToDos.Update(Me)
            Me.mProjects.Update(New Object() {Me.mAdvertiser, Me})
            MarkOld()
        End Sub

        Friend Sub Update(ByVal parent As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct(parent)
            Me.mStruct = DAClsappAdvertiserContactInfo.Update(Me.mStruct)
            Me.mNotes.Update(Me)
            Me.mToDos.Update(Me)
            Me.mProjects.Update(New Object() {Me.mAdvertiser, Me})
            MarkOld()
        End Sub

        Friend Sub DeleteSelf()
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            ' if we're new then don't update the database
            If Me.IsNew Then Exit Sub

            If Me.Projects.Count > 0 Then Throw New Exception("Contact is related to one or more Projects and can not be deleted.")
            If Me.Notes.Count > 0 Then Throw New Exception("Contact is related to one or more Notes and can not be deleted.")
            If Me.mToDos.Count > 0 Then Throw New Exception("Contact is related to one or more ToDo and can not be deleted.")

            DAClsappAdvertiserContactInfo.Delete(Me.mID)
            MarkNew()
        End Sub

#End Region

#Region " Common Area "

        Private mStruct As DAClsappAdvertiserContactInfo.Struct = New DAClsappAdvertiserContactInfo.Struct

        Public Function GetTableStruct() As DAClsappAdvertiserContactInfo.Struct
            Return Me.mStruct
        End Function

        ''' <summary>
        ''' Load the data of the object with the fetched data
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadObjectData()
            With Me.mStruct
                Me.mID = .ID.Value
                Me.mAdvertiser = ClsAccountInfo.GetAccountInfo(.IDAdvertiser.Value)
                Me.mFirstName = .FirstName.Value
                Me.mLastName = .LastName.Value
                Me.mMainCompanyAddress = .MainCompanyAddress.Value
                Me.mPrimaryAddress = .PrimaryAddress.Value
                Me.mSecondaryAddress = .SecondaryAddress.Value
                Me.mCity = .City.Value
                Me.mState = .State.Value
                Me.mCountry = .Country.Value
                Me.mZipCode = .ZipCode.Value
                Me.mProvidence = .Providence.Value
                Me.mDepartment = .Department.Value
                Me.mResposibleForNotes = .ResposibleForNotes.Value
                Me.mNotes = ClsNoteList.GetContactNotes(DAClsappAdvertiserNotes.FetchContactNotes(.ID.Value))
                Me.mToDos = ClsToDoList.GetContactToDos(DAClsappAdvertiserToDo.FetchContactToDo(.ID.Value))
                Me.mProjects = ClsProjectList.GetContactProjects(DAClsappAdvertiserProjects.FetchContactProject(.ID.Value))
            End With
        End Sub

        ''' <summary>
        ''' Collect the data of the object to fill to a structure of data and returns the structure 
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadTableStruct(ByVal parent As Object)
            With Me.mStruct
                .ID.NewValue = Me.mID
                .IDAdvertiser.NewValue = parent.ID
                .FirstName.NewValue = Me.mFirstName
                .LastName.NewValue = Me.mLastName
                .MainCompanyAddress.NewValue = Me.mMainCompanyAddress
                .PrimaryAddress.NewValue = Me.mPrimaryAddress
                .SecondaryAddress.NewValue = Me.mSecondaryAddress
                .City.NewValue = Me.mCity
                .State.NewValue = Me.mState
                .Country.NewValue = Me.mCountry
                .ZipCode.NewValue = Me.mZipCode
                .Providence.NewValue = Me.mProvidence
                .Department.NewValue = Me.mDepartment
                .ResposibleForNotes.NewValue = Me.mResposibleForNotes
            End With
        End Sub

#End Region

#End Region

#Region " Exists "

        Public Shared Function ExistsMainCompanyAddress(ByVal id As Long, ByVal advertiserId As Long, ByVal mainCompanyAddress As Boolean) As Boolean
            Return DataPortal.Execute(Of ExistsCommandMainCompanyAddress)(New ExistsCommandMainCompanyAddress(id, advertiserId, mainCompanyAddress)).Exists
        End Function

        <Serializable()> Private Class ExistsCommandMainCompanyAddress
            Inherits CommandBase

            Private mID As Long
            Private mAdvertiserID As Long
            Private mMainCompanyAddress As Boolean
            Private mExists As Boolean

            Public ReadOnly Property Exists() As Boolean
                Get
                    Return Me.mExists
                End Get
            End Property

            Public Sub New(ByVal id As Long, ByVal advertiserId As Long, ByVal mainCompanyAddress As Boolean)
                Me.mID = id
                Me.mAdvertiserID = advertiserId
                Me.mMainCompanyAddress = mainCompanyAddress
            End Sub

            Protected Overrides Sub DataPortal_Execute()
                Dim contact As DAClsappAdvertiserContactInfo.Struct() = DAClsappAdvertiserContactInfo.Fetch(Me.mAdvertiserID, True)
                Me.mExists = Me.mMainCompanyAddress AndAlso contact.Length > 0 AndAlso contact(0).ID.Value <> Me.mID
            End Sub

        End Class

        Public Shared Function ExistsInProjectsAnotherAdvertiser(ByVal id As Long, ByVal advertiserId As Long) As Boolean
            Return DataPortal.Execute(Of ExistsCommandInProjectsAnotherAdvertiser)(New ExistsCommandInProjectsAnotherAdvertiser(id, advertiserId)).Exists
        End Function

        <Serializable()> Private Class ExistsCommandInProjectsAnotherAdvertiser
            Inherits CommandBase

            Private mID As Long
            Private mAdvertiserID As Long
            Private mExists As Boolean

            Public ReadOnly Property Exists() As Boolean
                Get
                    Return Me.mExists
                End Get
            End Property

            Public Sub New(ByVal id As Long, ByVal advertiserId As Long)
                Me.mID = id
                Me.mAdvertiserID = advertiserId
                Me.mExists = False
            End Sub

            Protected Overrides Sub DataPortal_Execute()
                Dim projects As DAClsappAdvertiserProjects.Struct() = New DAClsappAdvertiserProjects.Struct() {}

                If Me.mID <> 0 Then projects = DAClsappAdvertiserProjects.Fetch(0, Me.mID)

                For Each project As DAClsappAdvertiserProjects.Struct In projects
                    If project.IDAdvertiser.Value <> Me.mAdvertiserID Then
                        Me.mExists = True
                        Exit For
                    End If
                Next
            End Sub

        End Class

#End Region

    End Class
End Namespace