Imports Csla
Imports SCT.DataAccess

Namespace Subscriber
    <Serializable()> Public Class ClsAccount
        Inherits BusinessBase(Of ClsAccount)

#Region " Business Methods "

        Private mConnected As Boolean = False
        Private mConfigurationHasChanged As Boolean = False

        Private mID As Long
        Private mIDDemographics As Long
        Private mIDUser As Long
        Private mInstallClientProgram As Boolean = False
        Private mLogin As String = String.Empty
        Private mComputerSerialNumber As String = String.Empty
        Private mComputerMacAddress As String = String.Empty
        Private mComputerName As String = String.Empty
        Private mComputerHDSerialNumber As String = String.Empty
        Private mWebPassword As String = String.Empty
        Private mClientPassword As String = String.Empty
        Private mHintByPassOne As String = String.Empty
        Private mHintByPassTwo As String = String.Empty
        Private mContactEmail As String = String.Empty
        Private mDemographics As ClsDemographicList = ClsDemographicList.NewSubscriberDemographics
        Private mConnectionHistories As ClsConnectionHistoryList = ClsConnectionHistoryList.NewSubscriberConnectionHistories
        Private mStolenReports As ClsStolenReportList = ClsStolenReportList.NewSubscriberStolenReports
        Private mAdHistories As Advertiser.Project.ClsAdHistoryList = Advertiser.Project.ClsAdHistoryList.NewSubscriberAdHistories

        Public Property Connected() As Boolean
            Get
                Return Me.mConnected
            End Get
            Set(ByVal value As Boolean)
                Me.mConnected = value
            End Set
        End Property

        Public Property ConfigurationHasChanged() As Boolean
            Get
                Return Me.mConfigurationHasChanged
            End Get
            Set(ByVal value As Boolean)
                Me.mConfigurationHasChanged = value
            End Set
        End Property

        Public ReadOnly Property ID() As Long
            Get
                CanReadProperty(True)
                Return Me.mID
            End Get
        End Property

        Public Property IDDemographics() As Long
            Get
                CanReadProperty(True)
                Return Me.mIDDemographics
            End Get
            Set(ByVal value As Long)
                CanWriteProperty(True)
                If Me.mIDDemographics <> value Then
                    Me.mIDDemographics = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property IDUser() As Long
            Get
                CanReadProperty(True)
                Return Me.mIDUser
            End Get
            Set(ByVal value As Long)
                CanWriteProperty(True)
                If Me.mIDUser <> value Then
                    Me.mIDUser = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property InstallClientProgram() As Boolean
            Get
                CanReadProperty(True)
                Return Me.mInstallClientProgram
            End Get
            Set(ByVal value As Boolean)
                CanWriteProperty(True)
                If Me.mInstallClientProgram <> value Then
                    Me.mInstallClientProgram = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property ComputerMacAddress() As String
            Get
                CanReadProperty(True)
                Return Me.mComputerMacAddress
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mComputerMacAddress <> value Then
                    Me.mComputerMacAddress = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property ComputerName() As String
            Get
                CanReadProperty(True)
                Return Me.mComputerName
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mComputerName <> value Then
                    Me.mComputerName = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property ComputerHDSerialNumber() As String
            Get
                CanReadProperty(True)
                Return Me.mComputerHDSerialNumber
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mComputerHDSerialNumber <> value Then
                    Me.mComputerHDSerialNumber = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property ComputerSerialNumber() As String
            Get
                CanReadProperty(True)
                Return Me.mComputerSerialNumber
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mComputerSerialNumber <> value Then
                    Me.mComputerSerialNumber = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property ContactEmail() As String
            Get
                CanReadProperty(True)
                Return Me.mContactEmail
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mContactEmail <> value Then
                    Me.mContactEmail = value
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
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public ReadOnly Property Demographics() As ClsDemographicList
            Get
                Return Me.mDemographics
            End Get
        End Property

        Public ReadOnly Property ConnectionHistories() As ClsConnectionHistoryList
            Get
                Return Me.mConnectionHistories
            End Get
        End Property

        Public ReadOnly Property StolenReports() As ClsStolenReportList
            Get
                Return Me.mStolenReports
            End Get
        End Property

        Public ReadOnly Property AdHistories() As Advertiser.Project.ClsAdHistoryList
            Get
                Return Me.mAdHistories
            End Get
        End Property

        Public Overrides ReadOnly Property IsValid() As Boolean
            Get
                Return MyBase.IsValid AndAlso Me.mAdHistories.IsValid AndAlso Me.mConnectionHistories.IsValid AndAlso Me.mStolenReports.IsValid AndAlso Me.mDemographics.IsValid
            End Get
        End Property

        Public Overrides ReadOnly Property IsDirty() As Boolean
            Get
                Return MyBase.IsDirty OrElse Me.mAdHistories.IsDirty OrElse Me.mConnectionHistories.IsDirty OrElse Me.mStolenReports.IsDirty OrElse Me.mDemographics.IsDirty
            End Get
        End Property

        Protected Overrides Function GetIdValue() As Object
            Return Me.mID
        End Function

        Public Function GetActionToTake(ByVal openConnection As Boolean) As Integer

            Dim StolenReport As SCT.Library.Subscriber.ClsStolenReport = Me.mStolenReports.GetActiveStolenReport

            If StolenReport IsNot Nothing Then Return StolenReport.ActionToTake
            If openConnection AndAlso Me.mConfigurationHasChanged Then Return 99
            If (Not Me.mInstallClientProgram) AndAlso (Not Me.mConfigurationHasChanged) Then Return 100

            Return 0
        End Function

        Public Function GetLastAdHistoryId() As Long
            Dim AdHistory As SCT.Library.Advertiser.Project.ClsAdHistory = Me.mAdHistories.GetLastItem

            If AdHistory IsNot Nothing Then
                Return AdHistory.ID
            End If
            Return 0
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()
            ValidationRules.AddRule(AddressOf Validation.StringRequired, "Login")
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("Login", 50))
            ValidationRules.AddRule(AddressOf IsValidLogin, "Login")
            ValidationRules.AddRule(AddressOf ExistsAccountLogin, "Login")

            ValidationRules.AddRule(AddressOf Validation.StringRequired, "WebPassword")
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("WebPassword", 12))
            ValidationRules.AddRule(AddressOf IsValidWebPassword, "WebPassword")

            ValidationRules.AddRule(AddressOf Validation.StringRequired, "ComputerSerialNumber")
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("ComputerSerialNumber", 50))
            ValidationRules.AddRule(AddressOf IsValidSerialNbr, "ComputerSerialNumber")
            ValidationRules.AddRule(AddressOf ExistsAccountSerianNbr, "ComputerSerialNumber")

            ValidationRules.AddRule(AddressOf Validation.StringRequired, "ClientPassword")
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("ClientPassword", 12))
            ValidationRules.AddRule(AddressOf IsValidClientPassword, "ClientPassword")

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("HintByPassOne", 50))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("HintByPassTwo", 50))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("ContactEmail", 100))
            ValidationRules.AddRule(AddressOf IsValidEmail, "ContactEmail")
        End Sub

        Private Function ExistsAccountLogin(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
            If DAClsappSubscribersAccounts.FetchByLogin(Me.mLogin).Length > 0 Then
                e.Description = "Login is assigned to another"
                Return False
            Else
                Return True
            End If
        End Function

        Private Function ExistsAccountSerianNbr(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
            If DAClsappSubscribersAccounts.FetchBySerialNbr(Me.mComputerSerialNumber).Length > 0 Then
                e.Description = "Computer Serial Number is assigned to another"
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

        Private Function IsValidSerialNbr(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
            If Text.RegularExpressions.Regex.IsMatch(Me.mComputerSerialNumber, "\w+") Then
                Return True
            Else
                e.Description = "Invalid Computer Serial Number Password Format"
                Return False
            End If
        End Function

        Private Function IsValidClientPassword(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
            If Text.RegularExpressions.Regex.IsMatch(Me.mClientPassword, "\w{6,}") Then
                Return True
            Else
                e.Description = "Invalid Computer Password Format"
                Return False
            End If
        End Function

        Private Function IsValidEmail(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
            If Text.RegularExpressions.Regex.IsMatch(Me.mContactEmail, "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*") Or Me.mContactEmail = "" Then
                Return True
            Else
                e.Description = "Invalid E-mail Format"
                Return False
            End If
        End Function

#End Region

#Region " Authorization Rules "

        'Protected Overrides Sub AddAuthorizationRules()
        '    AuthorizationRules.AllowRead("ID", "")
        '    AuthorizationRules.AllowRead("IDDemographics", "")
        '    AuthorizationRules.AllowRead("IDUser", "")
        '    AuthorizationRules.AllowRead("Login", "")
        '    AuthorizationRules.AllowRead("ComputerSerialNumber", "")
        '    AuthorizationRules.AllowRead("WebPassword", "")
        '    AuthorizationRules.AllowRead("ClientPassword", "")
        '    AuthorizationRules.AllowRead("HintByPassOne", "")
        '    AuthorizationRules.AllowRead("HintByPassTwo", "")
        '    AuthorizationRules.AllowRead("ContactEmail", "")
        '    AuthorizationRules.AllowRead("Demographics", "")
        '    AuthorizationRules.AllowRead("ConnectionHistories", "")
        '    AuthorizationRules.AllowRead("StolenReports", "")
        '    AuthorizationRules.AllowRead("AdHistories", "")
        '    AuthorizationRules.AllowWrite("IDDemographics", "")
        '    AuthorizationRules.AllowWrite("IDUser", "")
        '    AuthorizationRules.AllowWrite("Login", "")
        '    AuthorizationRules.AllowWrite("ComputerSerialNumber", "")
        '    AuthorizationRules.AllowWrite("WebPassword", "")
        '    AuthorizationRules.AllowWrite("ClientPassword", "")
        '    AuthorizationRules.AllowWrite("HintByPassOne", "")
        '    AuthorizationRules.AllowWrite("HintByPassTwo", "")
        '    AuthorizationRules.AllowWrite("ContactEmail", "")
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
                Throw New System.Security.SecurityException("User not authorized to add a subscriber account")
            End If
            Return DataPortal.Create(Of ClsAccount)(New Criteria(0))
        End Function

        Public Shared Function GetAccount(ByVal id As Long) As ClsAccount
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view a subscriber account")
            End If
            Return DataPortal.Fetch(Of ClsAccount)(New Criteria(id))
        End Function

        Public Shared Sub DeleteAccount(ByVal id As Long)
            If Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove a subscriber account")
            End If
            DataPortal.Delete(New Criteria(id))
        End Sub

        Public Overrides Function Save() As ClsAccount
            If IsDeleted AndAlso Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove a subscriber account")

            ElseIf IsNew AndAlso Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add project a subscriber account")

            ElseIf Not CanEditObject() Then
                Throw New System.Security.SecurityException("User not authorized to update a subscriber account")
            End If
            Return MyBase.Save
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Child Methods "

        Friend Shared Function NewChildAccount() As ClsAccount
            If Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add a subscriber account")
            End If
            Return DataPortal.Create(Of ClsAccount)(New ChildCriteria)
        End Function

        Friend Shared Function NewChildAccount(ByVal account As ClsAccount) As ClsAccount
            If Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add a subscriber account")
            End If
            account.MarkAsChild()
            Return account
        End Function

        Friend Shared Function GetChildAccount(ByVal account As DAClsappSubscribersAccounts.Struct) As ClsAccount
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view a subscriber account")
            End If
            Return New ClsAccount(account)
        End Function

        Private Sub New(ByVal account As DAClsappSubscribersAccounts.Struct)
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
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            Dim List As DAClsappSubscribersAccounts.Struct() = DAClsappSubscribersAccounts.Fetch(criteria.ID)
            If List.Length = 0 Then Throw New System.Security.SecurityException("Subscriber account doesn't exist")

            Me.mStruct = List(0)
            Me.LoadObjectData()
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Insert()
            Me.LoadTableStruct()
            Me.mStruct = DAClsappSubscribersAccounts.Insert(Me.mStruct)
            Me.mID = Me.mStruct.ID.Value
            Me.mDemographics.Update(Me)
            Me.mConnectionHistories.Update(Me)
            Me.mStolenReports.Update(Me)
            Me.mAdHistories.Update(Me)
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Update()
            If MyBase.IsDirty Then
                Me.LoadTableStruct()
                Me.mStruct = DAClsappSubscribersAccounts.Update(Me.mStruct)
            End If
            Me.mDemographics.Update(Me)
            Me.mConnectionHistories.Update(Me)
            Me.mStolenReports.Update(Me)
            Me.mAdHistories.Update(Me)
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_DeleteSelf()
            DataPortal_Delete(New Criteria(Me.mID))
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)
            If Me.AdHistories.Count > 0 Then Throw New Exception("Project has a Displayed history, and can't be deleted.")
            If Me.ConnectionHistories.Count > 0 Then Throw New Exception("Project has a Connection history, and can't be deleted.")
            If Me.StolenReports.Count > 0 Then Throw New Exception("Project has a Stolen Reports history, and can't be deleted.")

            Me.mDemographics.Clear()
            Me.mDemographics.Update(Me)
            DAClsappSubscribersAccounts.Delete(criteria.ID)
        End Sub

#End Region

#Region " Child Area "

        <Serializable()> Private Class ChildCriteria
        End Class

        <RunLocal()> Private Overloads Sub DataPortal_Create(ByVal criteria As ChildCriteria)
            MarkAsChild()
        End Sub

        Private Sub Fetch(ByVal account As DAClsappSubscribersAccounts.Struct)
            Me.mStruct = account
            Me.LoadObjectData()
            MarkOld()
        End Sub

        Friend Sub Insert(ByVal parent As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct()
            Me.mStruct = DAClsappSubscribersAccounts.Insert(Me.mStruct)
            Me.mID = Me.mStruct.ID.Value
            Me.mDemographics.Update(Me)
            Me.mConnectionHistories.Update(Me)
            Me.mStolenReports.Update(Me)
            Me.mAdHistories.Update(Me)
            MarkOld()
        End Sub

        Friend Sub Update(ByVal parent As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct()
            Me.mStruct = DAClsappSubscribersAccounts.Update(Me.mStruct)
            Me.mDemographics.Update(Me)
            Me.mConnectionHistories.Update(Me)
            Me.mStolenReports.Update(Me)
            Me.mAdHistories.Update(Me)
            MarkOld()
        End Sub

        Friend Sub DeleteSelf()
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            ' if we're new then don't update the database
            If Me.IsNew Then Exit Sub

            If Me.AdHistories.Count > 0 Then Throw New Exception("Project has a Displayed history, and can't be deleted.")
            If Me.ConnectionHistories.Count > 0 Then Throw New Exception("Project has a Connection history, and can't be deleted.")
            If Me.StolenReports.Count > 0 Then Throw New Exception("Project has a Stolen Reports history, and can't be deleted.")

            Me.mDemographics.Clear()
            Me.mDemographics.Update(Me)
            DAClsappSubscribersAccounts.Delete(Me.mID)
            MarkNew()
        End Sub

#End Region

#Region " Common Area "

        Private mStruct As DAClsappSubscribersAccounts.Struct = New DAClsappSubscribersAccounts.Struct

        Public Function GetTableStruct() As DAClsappSubscribersAccounts.Struct
            Return Me.mStruct
        End Function

        ''' <summary>
        ''' Load the data of the object with the fetched data
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadObjectData()
            With Me.mStruct
                Me.mID = .ID.Value
                Me.mIDDemographics = .IDDemographics.Value
                Me.mIDUser = .IDUser.Value
                Me.mComputerSerialNumber = .ComputerSerialNumber.Value
                Me.mComputerMacAddress = .ComputerMacAddress.Value
                Me.mComputerName = .ComputerName.Value
                Me.mComputerHDSerialNumber = .ComputerHDSerialNumber.Value
                Me.mContactEmail = .ContactEmail.Value
                Me.mLogin = .Login.Value
                Me.mClientPassword = .ClientPassword.Value
                Me.mWebPassword = .WebPassword.Value
                Me.mHintByPassOne = .HintByPassOne.Value
                Me.mHintByPassTwo = .HintByPassTwo.Value
                Me.InstallClientProgram = .InstalledClientProgram.Value
                Me.mDemographics = ClsDemographicList.GetSubscriberDemographics(DAClsappSubscribersDemographics.FetchSubscriberDemographics(.ID.Value))
                Me.mConnectionHistories = ClsConnectionHistoryList.GetSubscriberConnectionHistories(DAClsappSubscriberConnectionHistory.FetchSubscriberConnectionHistory(.ID.Value))
                Me.mStolenReports = ClsStolenReportList.GetSubscriberStolenReports(DAClsappSubscriberStolenReports.FetchSubscriberStolenReports(.ID.Value))
                Me.mAdHistories = Advertiser.Project.ClsAdHistoryList.GetSubscriberAdHistories(DAClsappAdvertiserAdHistory.FetchSubscriberAdHistory(.ID.Value))
            End With
        End Sub

        ''' <summary>
        ''' Collect the data of the object to fill to a structure of data and returns the structure 
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadTableStruct()
            With Me.mStruct
                .ID.NewValue = Me.mID
                .IDDemographics.NewValue = Me.mIDDemographics
                .IDUser.NewValue = Me.mIDUser
                .ComputerSerialNumber.NewValue = Me.mComputerSerialNumber
                .ComputerMacAddress.NewValue = Me.mComputerMacAddress
                .ComputerName.NewValue = Me.mComputerName
                .ComputerHDSerialNumber.NewValue = Me.mComputerHDSerialNumber
                .ContactEmail.NewValue = Me.mContactEmail
                .Login.NewValue = Me.mLogin
                .ClientPassword.NewValue = Me.mClientPassword
                .WebPassword.NewValue = Me.mWebPassword
                .HintByPassOne.NewValue = Me.mHintByPassOne
                .HintByPassTwo.NewValue = Me.mHintByPassTwo
                .InstalledClientProgram.NewValue = Me.InstallClientProgram
            End With
        End Sub

#End Region

#End Region

#Region " Exists "

        Public Shared Function ExistsLogin(ByVal login As String) As Boolean
            Return DataPortal.Execute(Of ExistsCommand)(New ExistsCommand(ExistsCommand.Command.Login, login)).Exists
        End Function

        Public Shared Function ExistsSerialNbr(ByVal serialNbr As String) As Boolean
            Return DataPortal.Execute(Of ExistsCommand)(New ExistsCommand(ExistsCommand.Command.SerialNbr, serialNbr)).Exists
        End Function

        Public Shared Function ExistsMacAddress(ByVal macAddress As String) As Boolean
            Return DataPortal.Execute(Of ExistsCommand)(New ExistsCommand(ExistsCommand.Command.MacAddress, macAddress)).Exists
        End Function

        <Serializable()> Private Class ExistsCommand
            Inherits CommandBase

            Private mCommand As Command
            Private mValue As String
            Private mExists As Boolean

            Public ReadOnly Property Exists() As Boolean
                Get
                    Return Me.mExists
                End Get
            End Property

            Public Sub New(ByVal command As Command, ByVal value As String)
                Me.mCommand = command
                Me.mValue = value
            End Sub

            Public Enum Command
                Login
                SerialNbr
                MacAddress
            End Enum

            Protected Overrides Sub DataPortal_Execute()
                Select Case Me.mCommand
                    Case Command.Login
                        Me.mExists = DAClsappSubscribersAccounts.FetchByLogin(Me.mValue).Length > 0
                    Case Command.SerialNbr
                        Me.mExists = DAClsappSubscribersAccounts.FetchBySerialNbr(Me.mValue).Length > 0
                    Case Command.MacAddress
                        Me.mExists = DAClsappSubscribersAccounts.FetchByMacAddress(Me.mValue).Length > 0
                    Case Else
                End Select
            End Sub

        End Class

#End Region

    End Class
End Namespace