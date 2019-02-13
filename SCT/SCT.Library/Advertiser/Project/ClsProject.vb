Imports Csla
Imports SCT.DataAccess
Imports SCT.Library.Advertiser.Project

Namespace Advertiser
    <Serializable()> Public Class ClsProject
        Inherits BusinessBase(Of ClsProject)

#Region " Business Methods "

        Private mID As Long
        Private mAdvertiserAccount As ClsAccountInfo = ClsAccountInfo.NewAccountInfo
        Private mAdvertiserContact As ClsContactInfo = ClsContactInfo.NewContactInfo
        Private mProjectDescription As String = String.Empty
        Private mADUrl As String = String.Empty
        Private mADHeight As Integer = 100
        Private mADWidth As Integer = 100
        Private mRunStartDate As Date = New Date(1900, 1, 1)
        Private mRunEndDate As Date = New Date(1900, 1, 1)
        Private mMinDisplays As Integer = 0
        Private mMaxDisplays As Integer = 0
        Private mMaxPerDay As Integer = 0
        Private mMinPerDay As Integer = 0
        Private mStartTimeBasedOnSubscribersTime As Date = New Date(1900, 1, 1, 0, 1, 0)
        Private mEndTimeBasedOnSubscribersTime As Date = New Date(1900, 1, 1, 23, 59, 0)
        Private mAdCountDisplayed As Integer = 0
        Private mAdVerifiedDate As Date = New Date(1900, 1, 1)
        Private mAdOnlineDate As Date = New Date(1900, 1, 1)
        Private mPromoCode As Integer = 0
        Private mComissionCode As Integer = 0
        Private mDemographics As ClsDemographicList = ClsDemographicList.NewProjectDemographics
        Private mAdHistories As ClsAdHistoryList = ClsAdHistoryList.NewProjectAdHistories
        Private mPrices As ClsPriceList = ClsPriceList.NewProjectPrices
        Private mInvoices As ClsInvoiceList = ClsInvoiceList.NewProjectInvoice
        Private mReceipts As Project.ClsReceiptList = Project.ClsReceiptList.NewProjectReceiptList

        Public ReadOnly Property ID() As Long
            Get
                CanReadProperty(True)
                Return Me.mID
            End Get
        End Property

        Public Property AdvertiserAccount() As ClsAccountInfo
            Get
                CanReadProperty(True)
                Return Me.mAdvertiserAccount
            End Get
            Set(ByVal value As ClsAccountInfo)
                CanWriteProperty(True)
                If value IsNot Nothing AndAlso value.ID <> 0 Then
                    If Me.mAdvertiserAccount.ID <> value.ID Then
                        Me.mAdvertiserAccount = value
                        ValidationRules.CheckRules("AdvertiserContact")
                        PropertyHasChanged()
                    End If
                Else
                    Throw New System.Security.SecurityException("Advertiser Account required.")
                End If
            End Set
        End Property

        Public Property AdvertiserContact() As ClsContactInfo
            Get
                CanReadProperty(True)
                Return Me.mAdvertiserContact
            End Get
            Set(ByVal value As ClsContactInfo)
                CanWriteProperty(True)
                If value IsNot Nothing AndAlso value.ID <> 0 Then
                    If Me.mAdvertiserContact.ID <> value.ID Then
                        Me.mAdvertiserContact = value
                        ValidationRules.CheckRules("AdvertiserAccount")
                        ValidationRules.CheckRules("AdvertiserContact")
                        PropertyHasChanged()
                    End If
                Else
                    Throw New System.Security.SecurityException("Contact required.")
                End If
            End Set
        End Property

        Public Property ProjectDescription() As String
            Get
                CanReadProperty(True)
                Return Me.mProjectDescription
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mProjectDescription <> value Then
                    Me.mProjectDescription = value
                    ValidationRules.CheckRules("ProjectDescription")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property ADUrl() As String
            Get
                CanReadProperty(True)
                Return Me.mADUrl
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mADUrl <> value Then
                    Me.mADUrl = value
                    ValidationRules.CheckRules("ADUrl")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property ADHeight() As Integer
            Get
                CanReadProperty(True)
                Return Me.mADHeight
            End Get
            Set(ByVal value As Integer)
                CanWriteProperty(True)
                If Me.mADHeight <> value Then
                    Me.mADHeight = value
                    ValidationRules.CheckRules("ADHeight")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property ADWidth() As Integer
            Get
                CanReadProperty(True)
                Return Me.mADWidth
            End Get
            Set(ByVal value As Integer)
                CanWriteProperty(True)
                If Me.mADWidth <> value Then
                    Me.mADWidth = value
                    ValidationRules.CheckRules("ADWidth")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property RunStartDate() As Date
            Get
                CanReadProperty(True)
                Return Me.mRunStartDate
            End Get
            Set(ByVal value As Date)
                CanWriteProperty(True)
                If Me.mRunStartDate <> value Then
                    Me.mRunStartDate = value
                    ValidationRules.CheckRules("RunEndDate")
                    ValidationRules.CheckRules("RunStartDate")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property RunEndDate() As Date
            Get
                CanReadProperty(True)
                Return Me.mRunEndDate
            End Get
            Set(ByVal value As Date)
                CanWriteProperty(True)
                If Me.mRunEndDate <> value Then
                    Me.mRunEndDate = value
                    ValidationRules.CheckRules("RunStartDate")
                    ValidationRules.CheckRules("RunEndDate")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property MinDisplays() As Integer
            Get
                CanReadProperty(True)
                Return Me.mMinDisplays
            End Get
            Set(ByVal value As Integer)
                CanWriteProperty(True)
                If Me.mMinDisplays <> value Then
                    Me.mMinDisplays = value
                    ValidationRules.CheckRules("MinDisplays")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property MaxDisplays() As Integer
            Get
                CanReadProperty(True)
                Return Me.mMaxDisplays
            End Get
            Set(ByVal value As Integer)
                CanWriteProperty(True)
                If Me.mMaxDisplays <> value Then
                    Me.mMaxDisplays = value
                    ValidationRules.CheckRules("MaxDisplays")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property MaxPerDay() As Integer
            Get
                CanReadProperty(True)
                Return Me.mMaxPerDay
            End Get
            Set(ByVal value As Integer)
                CanWriteProperty(True)
                If Me.mMaxPerDay <> value Then
                    Me.mMaxPerDay = value
                    ValidationRules.CheckRules("MaxPerDay")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property MinPerDay() As Integer
            Get
                CanReadProperty(True)
                Return Me.mMinPerDay
            End Get
            Set(ByVal value As Integer)
                CanWriteProperty(True)
                If Me.mMinPerDay <> value Then
                    Me.mMinPerDay = value
                    ValidationRules.CheckRules("MinPerDay")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property StartTimeBasedOnSubscribersTime() As Date
            Get
                CanReadProperty(True)
                Return Me.mStartTimeBasedOnSubscribersTime
            End Get
            Set(ByVal value As Date)
                CanWriteProperty(True)
                If Me.mStartTimeBasedOnSubscribersTime <> value Then
                    Me.mStartTimeBasedOnSubscribersTime = value
                    ValidationRules.CheckRules("EndTimeBasedOnSubscribersTime")
                    ValidationRules.CheckRules("StartTimeBasedOnSubscribersTime")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property EndTimeBasedOnSubscribersTime() As Date
            Get
                CanReadProperty(True)
                Return Me.mEndTimeBasedOnSubscribersTime
            End Get
            Set(ByVal value As Date)
                CanWriteProperty(True)
                If Me.mEndTimeBasedOnSubscribersTime <> value Then
                    Me.mEndTimeBasedOnSubscribersTime = value
                    ValidationRules.CheckRules("StartTimeBasedOnSubscribersTime")
                    ValidationRules.CheckRules("EndTimeBasedOnSubscribersTime")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property AdCountDisplayed() As Integer
            Get
                CanReadProperty(True)
                Return Me.mAdCountDisplayed
            End Get
            Set(ByVal value As Integer)
                CanWriteProperty(True)
                If Me.mAdCountDisplayed <> value Then
                    Me.mAdCountDisplayed = value
                    ValidationRules.CheckRules("AdCountDisplayed")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property AdVerifiedDate() As Date
            Get
                CanReadProperty(True)
                Return Me.mAdVerifiedDate
            End Get
            Set(ByVal value As Date)
                CanWriteProperty(True)
                If Me.mAdVerifiedDate <> value Then
                    Me.mAdVerifiedDate = value
                    ValidationRules.CheckRules("AdVerifiedDate")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property AdOnlineDate() As Date
            Get
                CanReadProperty(True)
                Return Me.mAdOnlineDate
            End Get
            Set(ByVal value As Date)
                CanWriteProperty(True)
                If Me.mAdOnlineDate <> value Then
                    Me.mAdOnlineDate = value
                    ValidationRules.CheckRules("AdOnlineDate")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property PromoCode() As Integer
            Get
                CanReadProperty(True)
                Return Me.mPromoCode
            End Get
            Set(ByVal value As Integer)
                CanWriteProperty(True)
                If Me.mPromoCode <> value Then
                    Me.mPromoCode = value
                    ValidationRules.CheckRules("PromoCode")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property ComissionCode() As Integer
            Get
                CanReadProperty(True)
                Return Me.mComissionCode
            End Get
            Set(ByVal value As Integer)
                CanWriteProperty(True)
                If Me.mComissionCode <> value Then
                    Me.mComissionCode = value
                    ValidationRules.CheckRules("ComissionCode")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public ReadOnly Property Prices() As ClsPriceList
            Get
                Return Me.mPrices
            End Get
        End Property

        Public ReadOnly Property AdHistories() As Advertiser.Project.ClsAdHistoryList
            Get
                Return Me.mAdHistories
            End Get
        End Property

        Public ReadOnly Property Demographics() As ClsDemographicList
            Get
                Return Me.mDemographics
            End Get
        End Property

        Public ReadOnly Property Invoices() As ClsInvoiceList
            Get
                Return Me.mInvoices
            End Get
        End Property

        Public ReadOnly Property Receipts() As Project.ClsReceiptList
            Get
                Return Me.mReceipts
            End Get
        End Property

        Public Overrides ReadOnly Property IsValid() As Boolean
            Get
                Return MyBase.IsValid AndAlso Me.mDemographics.IsValid AndAlso Me.mAdHistories.IsValid AndAlso Me.mPrices.IsValid AndAlso Me.mInvoices.IsValid AndAlso Me.mReceipts.IsValid
            End Get
        End Property

        Public Overrides ReadOnly Property IsDirty() As Boolean
            Get
                Return MyBase.IsDirty OrElse Me.mDemographics.IsDirty OrElse Me.mAdHistories.IsDirty OrElse Me.mPrices.IsDirty OrElse Me.mInvoices.IsDirty OrElse Me.mReceipts.IsDirty
            End Get
        End Property

        Public Sub AssignAdvertiserAccount(ByVal accountId As Long)
            If accountId <> 0 Then
                If Me.mAdvertiserAccount.ID <> accountId Then
                    Me.mAdvertiserAccount = ClsAccountInfo.GetAccountInfo(accountId)
                    ValidationRules.CheckRules("AdvertiserContact")
                    PropertyHasChanged("AdvertiserAccount")
                End If
            Else
                Throw New System.Security.SecurityException("Advertiser Account required.")
            End If
        End Sub

        Public Sub AssignContact(ByVal contactId As Long)
            If contactId <> 0 Then
                If Me.mAdvertiserContact.ID <> contactId Then
                    Me.mAdvertiserContact = ClsContactInfo.GetContactInfo(contactId)
                    ValidationRules.CheckRules("AdvertiserAccount")
                    ValidationRules.CheckRules("AdvertiserContact")
                    PropertyHasChanged("AdvertiserContact")
                End If
            Else
                Throw New System.Security.SecurityException("Contact required.")
            End If
        End Sub

        Protected Overrides Function GetIdValue() As Object
            Return Me.mID
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()
            ValidationRules.AddRule(AddressOf AdAccountRequired, "AdvertiserAccount")

            ValidationRules.AddRule(AddressOf AdContactRequired, "AdvertiserContact")
            ValidationRules.AddRule(AddressOf IsAdvertiserContact, "AdvertiserContact")

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, "ADUrl")
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("ADUrl", 200))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("ProjectDescription", 1000))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.IntegerMinValue, New Validation.CommonRules.IntegerMinValueRuleArgs("ADHeight", 100))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.IntegerMinValue, New Validation.CommonRules.IntegerMinValueRuleArgs("ADWidth", 100))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.MinValue(Of Date), New Validation.CommonRules.MinValueRuleArgs(Of Date)("RunStartDate", New Date(1900, 1, 1)))
            ValidationRules.AddRule(AddressOf RunStartDateGTRunEndDate, "RunStartDate")

            ValidationRules.AddRule(AddressOf Validation.CommonRules.MinValue(Of Date), New Validation.CommonRules.MinValueRuleArgs(Of Date)("RunEndDate", New Date(1900, 1, 1)))
            ValidationRules.AddRule(AddressOf RunStartDateGTRunEndDate, "RunEndDate")

            ValidationRules.AddRule(AddressOf StartTimeGTEndTime, "StartTimeBasedOnSubscribersTime")
            ValidationRules.AddRule(AddressOf StartTimeGTEndTime, "EndTimeBasedOnSubscribersTime")

            ValidationRules.AddRule(AddressOf Validation.CommonRules.IntegerMinValue, New Validation.CommonRules.IntegerMinValueRuleArgs("MinDisplays", 0))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.IntegerMinValue, New Validation.CommonRules.IntegerMinValueRuleArgs("MaxDisplays", 0))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.IntegerMinValue, New Validation.CommonRules.IntegerMinValueRuleArgs("MinPerDay", 0))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.IntegerMinValue, New Validation.CommonRules.IntegerMinValueRuleArgs("MaxPerDay", 0))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.IntegerMinValue, New Validation.CommonRules.IntegerMinValueRuleArgs("AdCountDisplayed", 0))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.IntegerMinValue, New Validation.CommonRules.IntegerMinValueRuleArgs("PromoCode", 0))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.IntegerMinValue, New Validation.CommonRules.IntegerMinValueRuleArgs("ComissionCode", 0))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.MinValue(Of Date), New Validation.CommonRules.MinValueRuleArgs(Of Date)("AdVerifiedDate", New Date(1900, 1, 1)))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.MinValue(Of Date), New Validation.CommonRules.MinValueRuleArgs(Of Date)("AdOnlineDate", New Date(1900, 1, 1)))
        End Sub

        Private Function AdAccountRequired(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
            If Me.mAdvertiserAccount.ID = 0 Then
                e.Description = "Advertiser Account required."
                Return False
            Else
                Return True
            End If
        End Function

        Private Function AdContactRequired(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
            If Me.mAdvertiserContact.ID = 0 Then
                e.Description = "Contact required."
                Return False
            Else
                Return True
            End If
        End Function

        Private Function IsAdvertiserContact(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
            If Me.mAdvertiserAccount.ID <> Me.mAdvertiserContact.Advertiser.ID Then
                e.Description = "InValid advertiser contact."
                Return False
            Else
                Return True
            End If
        End Function

        Private Function RunStartDateGTRunEndDate(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
            If Me.mRunEndDate.Date > New Date(1900, 1, 1) AndAlso Me.mRunStartDate.Date > Me.mRunEndDate.Date Then
                e.Description = "Start date can't be after end date."
                Return False
            Else
                Return True
            End If
        End Function

        Private Function StartTimeGTEndTime(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
            If Me.mStartTimeBasedOnSubscribersTime > Me.mEndTimeBasedOnSubscribersTime Then
                e.Description = "Start time can't be after end time."
                Return False
            Else
                Return True
            End If
        End Function

#End Region

#Region " Authorization Rules "

        'Protected Overrides Sub AddAuthorizationRules()
        '    AuthorizationRules.AllowRead("ID", "")
        '    AuthorizationRules.AllowRead("AdvertiserAccount", "")
        '    AuthorizationRules.AllowRead("Contact", "")
        '    AuthorizationRules.AllowRead("ProjectDescription", "")
        '    AuthorizationRules.AllowRead("ADUrl", "")
        '    AuthorizationRules.AllowRead("ADHeight", "")     
        '    AuthorizationRules.AllowRead("ADWidth", "")
        '    AuthorizationRules.AllowRead("RunStartDate", "")
        '    AuthorizationRules.AllowRead("RunEndDate", "")
        '    AuthorizationRules.AllowRead("MinDisplays", "")
        '    AuthorizationRules.AllowRead("MaxDisplays", "")
        '    AuthorizationRules.AllowRead("MaxPerDay", "")
        '    AuthorizationRules.AllowRead("MinPerDay", "")
        '    AuthorizationRules.AllowRead("StartTimeBasedOnSubscribersTime", "")
        '    AuthorizationRules.AllowRead("EndTimeBasedOnSubscribersTime", "")
        '    AuthorizationRules.AllowRead("AdCountDisplayed", "")
        '    AuthorizationRules.AllowRead("AdVerifiedDate", "")
        '    AuthorizationRules.AllowRead("AdOnlineDate", "")
        '    AuthorizationRules.AllowRead("Prices", "")
        '    AuthorizationRules.AllowRead("AdHistories", "")
        '    AuthorizationRules.AllowRead("Demographics", "")
        '    AuthorizationRules.AllowWrite("ProjectDescription", "")
        '    AuthorizationRules.AllowWrite("ADUrl", "")
        '    AuthorizationRules.AllowWrite("RunStartDate", "")
        '    AuthorizationRules.AllowWrite("RunEndDate", "")
        '    AuthorizationRules.AllowWrite("MinDisplays", "")
        '    AuthorizationRules.AllowWrite("MaxDisplays", "")
        '    AuthorizationRules.AllowWrite("MaxPerDay", "")
        '    AuthorizationRules.AllowWrite("MinPerDay", "")
        '    AuthorizationRules.AllowWrite("StartTimeBasedOnSubscribersTime", "")
        '    AuthorizationRules.AllowWrite("EndTimeBasedOnSubscribersTime", "")
        '    AuthorizationRules.AllowWrite("AdCountDisplayed", "")
        '    AuthorizationRules.AllowWrite("AdVerifiedDate", "")
        '    AuthorizationRules.AllowWrite("AdOnlineDate", "")
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

        Public Shared Function NewProject() As ClsProject
            If Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add a project")
            End If
            Return DataPortal.Create(Of ClsProject)(New Criteria(0))
        End Function

        Public Shared Function GetProject(ByVal id As Long) As ClsProject
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view a project")
            End If
            Return DataPortal.Fetch(Of ClsProject)(New Criteria(id))
        End Function

        Public Shared Sub DeleteProject(ByVal id As Long)
            If Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove a project")
            End If
            DataPortal.Delete(New Criteria(id))
        End Sub

        Public Overrides Function Save() As ClsProject
            If IsDeleted AndAlso Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove a project")

            ElseIf IsNew AndAlso Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add a project")

            ElseIf Not CanEditObject() Then
                Throw New System.Security.SecurityException("User not authorized to update a project")
            End If
            Return MyBase.Save
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

#End Region

#Region " Child Methods "

        Friend Shared Function NewChildProject() As ClsProject
            Dim Child As New ClsProject
            Child.ValidationRules.CheckRules("AdvertiserAccount")
            Child.ValidationRules.CheckRules("AdvertiserContact")
            Child.ValidationRules.CheckRules("ADUrl")
            Child.MarkAsChild()
            Return Child
        End Function

        Friend Shared Function GetChildProject(ByVal project As DAClsappAdvertiserProjects.Struct) As ClsProject
            Return New ClsProject(project)
        End Function

        Friend Shared Function NewAdvertiserProject() As ClsProject
            Dim Child As New ClsProject
            Child.ValidationRules.CheckRules("AdvertiserContact")
            Child.ValidationRules.CheckRules("ADUrl")
            Child.MarkAsChild()
            Return Child
        End Function

        Friend Shared Function GetAdvertiserProject(ByVal project As DAClsappAdvertiserProjects.Struct) As ClsProject
            Return New ClsProject(project)
        End Function

        Friend Shared Function NewContactProject() As ClsProject
            Dim Child As New ClsProject
            Child.ValidationRules.CheckRules("AdvertiserAccount")
            Child.ValidationRules.CheckRules("ADUrl")
            Child.MarkAsChild()
            Return Child
        End Function

        Friend Shared Function GetContactProject(ByVal project As DAClsappAdvertiserProjects.Struct) As ClsProject
            Return New ClsProject(project)
        End Function

        Private Sub New(ByVal project As DAClsappAdvertiserProjects.Struct)
            MarkAsChild()
            Fetch(project)
        End Sub

#End Region

#End Region

#Region " Data Access "

#Region " Root Methods"

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
            Me.ValidationRules.CheckRules("AdvertiserAccount")
            Me.ValidationRules.CheckRules("AdvertiserContact")
            Me.ValidationRules.CheckRules("ADUrl")
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            Dim List As DAClsappAdvertiserProjects.Struct() = DAClsappAdvertiserProjects.Fetch(criteria.ID)
            If List.Length = 0 Then Throw New System.Security.SecurityException("Project doesn't exist")

            Me.mStruct = List(0)
            Me.LoadObjectData()
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Insert()
            Me.LoadTableStruct(New Object() {Me.mAdvertiserAccount, Me.mAdvertiserContact})
            Me.mStruct = DAClsappAdvertiserProjects.Insert(Me.mStruct)
            Me.mID = Me.mStruct.ID.Value
            Me.mDemographics.Update(Me)
            Me.mPrices.Update(Me)
            Me.mAdHistories.Update(Me)
            Me.mReceipts.Update(Me)
            Me.mInvoices.Update(Me)
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Update()
            If MyBase.IsDirty Then
                Me.LoadTableStruct(New Object() {Me.mAdvertiserAccount, Me.mAdvertiserContact})
                Me.mStruct = DAClsappAdvertiserProjects.Update(Me.mStruct)
            End If
            Me.mDemographics.Update(Me)
            Me.mPrices.Update(Me)
            Me.mAdHistories.Update(Me)
            Me.mReceipts.Update(Me)
            Me.mInvoices.Update(Me)
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_DeleteSelf()
            DataPortal_Delete(New Criteria(Me.mID))
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)
            If Me.AdHistories.Count > 0 Then Throw New Exception("Project has a Displayed history, and can't be deleted.")
            If Me.Invoices.Count > 0 Then Throw New Exception("Project has a Invoices history, and can't be deleted.")
            If Me.Receipts.Count > 0 Then Throw New Exception("Project has a Receipts history, and can't be deleted.")

            Me.mDemographics.Clear()
            Me.mPrices.Clear()
            Me.mDemographics.Update(Me)
            Me.mPrices.Update(Me)
            DAClsappAdvertiserProjects.Delete(criteria.ID)

        End Sub

#End Region

#Region " Child Area "

        Private Sub Fetch(ByVal project As DAClsappAdvertiserProjects.Struct)
            Me.mStruct = project
            Me.LoadObjectData()
            MarkOld()
        End Sub

        Friend Sub Insert(ByVal parents() As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct(New Object() {parents(0), parents(1)})
            Me.mStruct = DAClsappAdvertiserProjects.Insert(Me.mStruct)
            Me.mID = Me.mStruct.ID.Value
            Me.mDemographics.Update(Me)
            Me.mPrices.Update(Me)
            Me.mAdHistories.Update(Me)
            Me.mReceipts.Update(Me)
            Me.mInvoices.Update(Me)
            MarkOld()
        End Sub

        Friend Sub Update(ByVal parents() As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct(New Object() {parents(0), parents(1)})
            Me.mStruct = DAClsappAdvertiserProjects.Update(Me.mStruct)
            Me.mDemographics.Update(Me)
            Me.mPrices.Update(Me)
            Me.mAdHistories.Update(Me)
            Me.mReceipts.Update(Me)
            Me.mInvoices.Update(Me)
            MarkOld()
        End Sub

        Friend Sub DeleteSelf()
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            ' if we're new then don't update the database
            If Me.IsNew Then Exit Sub

            If Me.AdHistories.Count > 0 Then Throw New Exception("Project has a Displayed history, and can't be deleted.")
            If Me.Invoices.Count > 0 Then Throw New Exception("Project has a Invoices history, and can't be deleted.")
            If Me.Receipts.Count > 0 Then Throw New Exception("Project has a Receipts history, and can't be deleted.")

            Me.mDemographics.Clear()
            Me.mPrices.Clear()
            Me.mDemographics.Update(Me)
            Me.mPrices.Update(Me)
            DAClsappAdvertiserProjects.Delete(Me.mID)
            MarkNew()

        End Sub

#End Region

#Region " Common Area "

        Private mStruct As DAClsappAdvertiserProjects.Struct = New DAClsappAdvertiserProjects.Struct

        Public Function GetTableStruct() As DAClsappAdvertiserProjects.Struct
            Return Me.mStruct
        End Function

        ''' <summary>
        ''' Load the data of the object with the fetched data
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadObjectData()
            With Me.mStruct
                Me.mID = .ID.Value
                Me.mAdvertiserAccount = ClsAccountInfo.GetAccountInfo(.IDAdvertiser.Value)
                Me.mAdvertiserContact = ClsContactInfo.GetContactInfo(.IDAdvertiserContact.Value)
                Me.mProjectDescription = .ProjectDescription.Value
                Me.mADUrl = .ADUrl.Value
                Me.mADHeight = .ADHeight.Value
                Me.mADWidth = .ADWidth.Value
                Me.mRunStartDate = .RunStartDate.Value
                Me.mRunEndDate = .RunEndDate.Value
                Me.mMinDisplays = .MinDisplays.Value
                Me.mMaxDisplays = .MaxDisplays.Value
                Me.mMaxPerDay = .MaxPerDay.Value
                Me.mMinPerDay = .MinPerDay.Value
                Me.mStartTimeBasedOnSubscribersTime = .StartTimeBasedOnSubscribersTime.Value
                Me.mEndTimeBasedOnSubscribersTime = .EndTimeBasedOnSubscribersTime.Value
                Me.mAdCountDisplayed = .AdCountDisplayed.Value
                Me.mAdVerifiedDate = .AdVerifiedDate.Value
                Me.mAdOnlineDate = .AdOnlineDate.Value
                Me.mComissionCode = .ComissionCode.Value
                Me.mPromoCode = .PromoCode.Value
                Me.mPrices = ClsPriceList.GetProjectPrices(DAClsappAdvertiserProjectPriceInfo.FetchProjectPriceInfo(.ID.Value))
                Me.mAdHistories = Advertiser.Project.ClsAdHistoryList.GetProjectAdHistories(DAClsappAdvertiserAdHistory.FetchProjectAdHistory(.ID.Value))
                Me.mDemographics = ClsDemographicList.GetProjectDemographics(DAClsappAdvertiserDemographics.FetchProjectDemographics(.ID.Value))
                Me.mInvoices = ClsInvoiceList.GetProjectInvoice(DAClsappAdvertiserProjectInvoices.FetchProjectInvoice(.ID.Value))
                Me.mReceipts = Project.ClsReceiptList.GetProjectReceiptList(DAClsappAdvertiserProjectReceipts.FetchProjectReceipt(.ID.Value))
            End With
        End Sub

        ''' <summary>
        ''' Collect the data of the object to fill to a structure of data and returns the structure 
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadTableStruct(ByVal parents() As Object)
            With Me.mStruct
                .ID.NewValue = Me.mID
                .IDAdvertiser.NewValue = parents(0).ID
                .IDAdvertiserContact.NewValue = parents(1).ID
                .ProjectDescription.NewValue = Me.mProjectDescription
                .ADUrl.NewValue = Me.mADUrl
                .ADHeight.NewValue = Me.mADHeight
                .ADWidth.NewValue = Me.mADWidth
                .RunStartDate.NewValue = Me.mRunStartDate
                .RunEndDate.NewValue = Me.mRunEndDate
                .MinDisplays.NewValue = Me.mMinDisplays
                .MaxDisplays.NewValue = Me.mMaxDisplays
                .MaxPerDay.NewValue = Me.mMaxPerDay
                .MinPerDay.NewValue = Me.mMinPerDay
                .StartTimeBasedOnSubscribersTime.NewValue = Me.mStartTimeBasedOnSubscribersTime
                .EndTimeBasedOnSubscribersTime.NewValue = Me.mEndTimeBasedOnSubscribersTime
                .AdCountDisplayed.NewValue = Me.mAdCountDisplayed
                .AdVerifiedDate.NewValue = Me.mAdVerifiedDate
                .AdOnlineDate.NewValue = Me.mAdOnlineDate
                .PromoCode.NewValue = Me.mPromoCode
                .ComissionCode.NewValue = Me.ComissionCode
            End With
        End Sub

#End Region

#End Region

    End Class
End Namespace