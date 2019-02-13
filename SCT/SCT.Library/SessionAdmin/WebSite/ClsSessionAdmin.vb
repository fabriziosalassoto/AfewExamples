Option Strict On

Imports SCT.DataAccess
Imports System.Transactions
Imports System.Web.HttpContext
Imports System.Web.Security

Namespace WebSite
    <Serializable()> Public Class ClsSessionAdmin

#Region " Business Methods "

        <Serializable()> Private Class SessionValues

            Private mForms As ClsFormList = ClsFormList.NewFormList

            Private mCurrentForm As ClsForm = Nothing
            Private mCurrentClsForm As Object = Nothing
            Private mCurrentClsMaster As Object = Nothing
            Private mCslaPrincipal As System.Security.Principal.IPrincipal = Nothing

            Public Property Forms() As ClsFormList
                Get
                    Return Me.mForms
                End Get
                Set(ByVal value As ClsFormList)
                    Me.mForms = value
                End Set
            End Property

            Public Property CurrentForm() As ClsForm
                Get
                    Return Me.mCurrentForm
                End Get
                Set(ByVal value As ClsForm)
                    Me.mCurrentForm = value
                End Set
            End Property

            Public Property CurrentClsForm() As Object
                Get
                    Return Me.mCurrentClsForm
                End Get
                Set(ByVal value As Object)
                    Me.mCurrentClsForm = value
                End Set
            End Property

            Public Property CurrentClsMaster() As Object
                Get
                    Return Me.mCurrentClsMaster
                End Get
                Set(ByVal value As Object)
                    Me.mCurrentClsMaster = value
                End Set
            End Property

            Public Property CslaPrincipal() As System.Security.Principal.IPrincipal
                Get
                    Return Me.mCslaPrincipal
                End Get
                Set(ByVal value As System.Security.Principal.IPrincipal)
                    Me.mCslaPrincipal = value
                End Set
            End Property

        End Class

        Public Shared Property SessionCurrentClsForm() As Object
            Get
                If Not CType(Current.Session("SessionValues"), SessionValues) Is Nothing Then
                    Return CType(Current.Session("SessionValues"), SessionValues).CurrentClsForm
                End If
                Return Nothing
            End Get
            Set(ByVal value As Object)
                CType(Current.Session("SessionValues"), SessionValues).CurrentClsForm = value
            End Set
        End Property

        Public Shared Property SessionCurrentClsMaster() As Object
            Get
                If Not CType(Current.Session("SessionValues"), SessionValues) Is Nothing Then
                    Return CType(Current.Session("SessionValues"), SessionValues).CurrentClsMaster
                End If
                Return Nothing
            End Get
            Set(ByVal value As Object)
                CType(Current.Session("SessionValues"), SessionValues).CurrentClsMaster = value
            End Set
        End Property

        Public Shared Sub SetUserSessionValues(ByVal cslaPrincipal As System.Security.Principal.IPrincipal, ByVal InicialFormDescription As String)
            CType(Current.Session("SessionValues"), SessionValues).CurrentClsMaster = Nothing
            CType(Current.Session("SessionValues"), SessionValues).CurrentClsForm = Nothing
            CType(Current.Session("SessionValues"), SessionValues).Forms = ClsFormList.GetFormList(String.Empty, New String() {Logs.Advertiser.ToString, Logs.Subscriber.ToString})
            CType(Current.Session("SessionValues"), SessionValues).CurrentForm = CType(Current.Session("SessionValues"), SessionValues).Forms.GetItem(InicialFormDescription)
            CType(Current.Session("SessionValues"), SessionValues).CslaPrincipal = cslaPrincipal
        End Sub

        Public Shared Sub ClearUserSessionValues(ByVal cslaPrincipal As System.Security.Principal.IPrincipal)
            CType(Current.Session("SessionValues"), SessionValues).Forms = Nothing
            CType(Current.Session("SessionValues"), SessionValues).CurrentClsMaster = Nothing
            CType(Current.Session("SessionValues"), SessionValues).CurrentClsForm = Nothing
            CType(Current.Session("SessionValues"), SessionValues).CurrentForm = Nothing
            CType(Current.Session("SessionValues"), SessionValues).CslaPrincipal = cslaPrincipal
        End Sub

        Public Shared Sub NewUserSessionValues()
            Current.Session("SessionValues") = New SessionValues
        End Sub

        Public Shared Function GetSessionCslaPrincipal() As System.Security.Principal.IPrincipal
            If CType(Current.Session("SessionValues"), SessionValues) IsNot Nothing Then
                Return CType(Current.Session("SessionValues"), SessionValues).CslaPrincipal
            End If
            Return Nothing
        End Function

        Public Shared Function GetSessionUserID() As String
            Dim User As System.Security.Principal.IPrincipal = Csla.ApplicationContext.User
            Return Mid(User.Identity.Name, 1, User.Identity.Name.IndexOf(";"))
        End Function

        Public Shared Function GetSessionUserName() As String
            Dim User As System.Security.Principal.IPrincipal = Csla.ApplicationContext.User
            Return Mid(User.Identity.Name, User.Identity.Name.IndexOf(";") + 2)
        End Function

        Public Shared Function IsSessionUserAuthenticated() As Boolean
            Return Csla.ApplicationContext.User.Identity.IsAuthenticated
        End Function

        Public Shared Function IsValidForm(ByVal description As String, ByVal ParamArray fields() As String) As Boolean
            If CType(Current.Session("SessionValues"), SessionValues).Forms.Contains(description) Then
                CType(Current.Session("SessionValues"), SessionValues).CurrentForm = CType(Current.Session("SessionValues"), SessionValues).Forms.GetItem(description)
                For Each field As String In fields
                    If Not CType(Current.Session("SessionValues"), SessionValues).CurrentForm.Groups.ContainsField(field) Then
                        CType(Current.Session("SessionValues"), SessionValues).CurrentForm = Nothing
                        Return False
                    End If
                Next
                Return True
            Else
                CType(Current.Session("SessionValues"), SessionValues).CurrentForm = Nothing
                Return False
            End If
        End Function

#End Region

#Region " Binnacle "

#Region " Advertiser "

        Private Shared Sub SaveBinnacle(ByRef binnacle As Advertiser.ClsBinnacle, ByVal operation As ClsOperation, ByVal bDate As Date, ByVal AdNoteNewData As AdNoteNewData, ByVal adNote As Advertiser.Contact.ClsNote)
            Try
                binnacle.BinnacleForms.AddNewItem(CType(Current.Session("SessionValues"), SessionValues).CurrentForm, operation, bDate, AdNoteNewData.ID, AdNoteNewData.Contact.FullName, AdNoteNewData.DateEntered, AdNoteNewData.Description)
                binnacle.BinnacleTables.AddNewItem(operation, adNote.GetTableStruct.TableName, bDate, adNote.GetTableStruct.ID, adNote.GetTableStruct.IDAdvertiserContact, adNote.GetTableStruct.DateEntered, adNote.GetTableStruct.DescriptionOfNotes)
                binnacle.Save()
            Catch ValEx As Csla.Validation.ValidationException
                For Each BrokenRule As Csla.Validation.BrokenRule In binnacle.BrokenRulesCollection
                    Throw New Exception(BrokenRule.Description)
                Next
                Throw New Exception(ValEx.Message)
            End Try
        End Sub

        Private Shared Sub SaveBinnacle(ByRef binnacle As Advertiser.ClsBinnacle, ByVal operation As ClsOperation, ByVal bDate As Date, ByVal AdToDoNewData As AdToDoNewData, ByVal adToDo As Advertiser.Contact.ClsToDo)
            Try
                binnacle.BinnacleForms.AddNewItem(CType(Current.Session("SessionValues"), SessionValues).CurrentForm, operation, bDate, AdToDoNewData.ID, AdToDoNewData.Contact.FullName, AdToDoNewData.DateCompleted, AdToDoNewData.DateDue, AdToDoNewData.DateEntered, AdToDoNewData.CallBackRecord, AdToDoNewData.TaskNotes)
                binnacle.BinnacleTables.AddNewItem(operation, adToDo.GetTableStruct.TableName, bDate, adToDo.GetTableStruct.ID, adToDo.GetTableStruct.IDAdvertiserContact, adToDo.GetTableStruct.DateCompleted, adToDo.GetTableStruct.DateDue, adToDo.GetTableStruct.DateEntered, adToDo.GetTableStruct.CallBackRecord, adToDo.GetTableStruct.TaskNotes)
                binnacle.Save()
            Catch ValEx As Csla.Validation.ValidationException
                For Each BrokenRule As Csla.Validation.BrokenRule In binnacle.BrokenRulesCollection
                    Throw New Exception(BrokenRule.Description)
                Next
                Throw New Exception(ValEx.Message)
            End Try
        End Sub

        Private Shared Sub SaveBinnacle(ByRef binnacle As Advertiser.ClsBinnacle, ByVal operation As ClsOperation, ByVal bDate As Date, ByVal AdProjectNewData As AdProjectNewData, ByVal adProject As Advertiser.ClsProject, ByVal adProjectDemographics() As Advertiser.Project.ClsDemographic)
            Try
                binnacle.BinnacleForms.AddNewItem(CType(Current.Session("SessionValues"), SessionValues).CurrentForm, operation, bDate, AdProjectNewData.ID, AdProjectNewData.Contact.FullName, AdProjectNewData.ADUrl, AdProjectNewData.ProjectDescription, AdProjectNewData.ADHeight, AdProjectNewData.ADWidth, AdProjectNewData.RunStartDate, AdProjectNewData.RunEndDate, AdProjectNewData.StartTimeBasedOnSubscribersTime, AdProjectNewData.EndTimeBasedOnSubscribersTime, AdProjectNewData.MinDisplays, AdProjectNewData.MaxDisplays, AdProjectNewData.MinPerDay, AdProjectNewData.MaxPerDay, AdProjectNewData.AdCountDisplayed, AdProjectNewData.AdVerifiedDate, AdProjectNewData.AdOnlineDate, AdProjectNewData.PromoCode, AdProjectNewData.ComissionCode, AdProjectNewData.Sex, AdProjectNewData.MinAge, AdProjectNewData.MaxAge, AdProjectNewData.Occupation, AdProjectNewData.Country, AdProjectNewData.State)
                binnacle.BinnacleTables.AddNewItem(operation, adProject.GetTableStruct.TableName, bDate, adProject.GetTableStruct.ID, adProject.GetTableStruct.IDAdvertiser, adProject.GetTableStruct.IDAdvertiserContact, adProject.GetTableStruct.ADUrl, adProject.GetTableStruct.ProjectDescription, adProject.GetTableStruct.ADHeight, adProject.GetTableStruct.ADWidth, adProject.GetTableStruct.RunStartDate, adProject.GetTableStruct.RunEndDate, adProject.GetTableStruct.StartTimeBasedOnSubscribersTime, adProject.GetTableStruct.EndTimeBasedOnSubscribersTime, adProject.GetTableStruct.MinDisplays, adProject.GetTableStruct.MaxDisplays, adProject.GetTableStruct.MinPerDay, adProject.GetTableStruct.MaxPerDay, adProject.GetTableStruct.AdCountDisplayed, adProject.GetTableStruct.AdVerifiedDate, adProject.GetTableStruct.AdOnlineDate, adProject.GetTableStruct.ComissionCode, adProject.GetTableStruct.PromoCode)
                For Each adProjectDemographic As Advertiser.Project.ClsDemographic In adProjectDemographics
                    binnacle.BinnacleTables.AddNewItem(operation, adProjectDemographic.GetTableStruct.TableName, bDate, adProjectDemographic.GetTableStruct.ID, adProjectDemographic.GetTableStruct.IDAdvertiserProject, adProjectDemographic.GetTableStruct.DemographicTag, adProjectDemographic.GetTableStruct.DemographicRequirement)
                Next
                binnacle.Save()
            Catch ValEx As Csla.Validation.ValidationException
                For Each BrokenRule As Csla.Validation.BrokenRule In binnacle.BrokenRulesCollection
                    Throw New Exception(BrokenRule.Description)
                Next
                Throw New Exception(ValEx.Message)
            End Try
        End Sub

        Private Shared Sub SaveBinnacle(ByRef binnacle As Advertiser.ClsBinnacle, ByVal operation As ClsOperation, ByVal bDate As Date, ByVal formData As AdContactNewData, ByVal objectData As Advertiser.ClsContact)
            Try
                binnacle.BinnacleForms.AddNewItem(CType(Current.Session("SessionValues"), SessionValues).CurrentForm, operation, bDate, formData.ID, formData.LastName, formData.FirstName, formData.MainCompanyAddress, formData.PrimaryAddress, formData.SecondaryAddress, formData.City, formData.State, formData.Country, formData.ZipCode, formData.Providence, formData.Department, formData.ResposibleForNotes)
                binnacle.BinnacleTables.AddNewItem(operation, objectData.GetTableStruct.TableName, bDate, objectData.GetTableStruct.ID, objectData.GetTableStruct.IDAdvertiser, objectData.GetTableStruct.LastName, objectData.GetTableStruct.FirstName, objectData.GetTableStruct.MainCompanyAddress, objectData.GetTableStruct.PrimaryAddress, objectData.GetTableStruct.SecondaryAddress, objectData.GetTableStruct.City, objectData.GetTableStruct.City, objectData.GetTableStruct.State, objectData.GetTableStruct.Country, objectData.GetTableStruct.ZipCode, objectData.GetTableStruct.Providence, objectData.GetTableStruct.Department, objectData.GetTableStruct.ResposibleForNotes)
                binnacle.Save()
            Catch ValEx As Csla.Validation.ValidationException
                For Each BrokenRule As Csla.Validation.BrokenRule In binnacle.BrokenRulesCollection
                    Throw New Exception(BrokenRule.Description)
                Next
                Throw New Exception(ValEx.Message)
            End Try
        End Sub

        Private Shared Sub SaveBinnacle(ByRef binnacle As Advertiser.ClsBinnacle, ByVal operation As ClsOperation, ByVal bDate As Date, ByVal formData As AdAccountNewData, ByVal objectData As Advertiser.ClsAccount)
            Try
                binnacle.BinnacleForms.AddNewItem(CType(Current.Session("SessionValues"), SessionValues).CurrentForm, operation, bDate, formData.ID, formData.Login, formData.WebPassword, formData.ClientPassword, formData.HintByPassOne, formData.HintByPassTwo, formData.CompanyName, formData.CompanyNotes)
                binnacle.BinnacleTables.AddNewItem(operation, objectData.GetTableStruct.TableName, bDate, objectData.GetTableStruct.ID, objectData.GetTableStruct.Login, objectData.GetTableStruct.WebPassword, objectData.GetTableStruct.ClientPassword, objectData.GetTableStruct.HintByPassOne, objectData.GetTableStruct.HintByPassTwo, objectData.GetTableStruct.CompanyName, objectData.GetTableStruct.CompanyNotes)
                binnacle.Save()
            Catch ValEx As Csla.Validation.ValidationException
                For Each BrokenRule As Csla.Validation.BrokenRule In binnacle.BrokenRulesCollection
                    Throw New Exception(BrokenRule.Description)
                Next
                Throw New Exception(ValEx.Message)
            End Try
        End Sub

        Private Shared Sub SaveBinnacle(ByRef binnacle As Advertiser.ClsBinnacle, ByVal operation As ClsOperation, ByVal bDate As Date)
            Try
                binnacle.BinnacleForms.AddNewItem(CType(Current.Session("SessionValues"), SessionValues).CurrentForm, operation, bDate)
                binnacle.Save()
            Catch ValEx As Csla.Validation.ValidationException
                For Each BrokenRule As Csla.Validation.BrokenRule In binnacle.BrokenRulesCollection
                    Throw New Exception(BrokenRule.Description)
                Next
                Throw New Exception(ValEx.Message)
            End Try
        End Sub

        Private Shared Function GetAdvertiserBinnacle(ByVal BDate As Date) As Advertiser.ClsBinnacle
            If Advertiser.ClsBinnacle.ExistsUserBinnacle(CLng(Mid(Csla.ApplicationContext.User.Identity.Name, 1, Csla.ApplicationContext.User.Identity.Name.IndexOf(";"))), BDate) Then
                Return Advertiser.ClsBinnacle.GetBinnacle(CLng(Mid(Csla.ApplicationContext.User.Identity.Name, 1, Csla.ApplicationContext.User.Identity.Name.IndexOf(";"))), BDate)
            Else
                Return Advertiser.ClsBinnacle.NewBinnacle(CLng(Mid(Csla.ApplicationContext.User.Identity.Name, 1, Csla.ApplicationContext.User.Identity.Name.IndexOf(";"))), BDate)
            End If
        End Function

#End Region

#Region " Subscriber "

        Private Shared Sub SaveBinnacle(ByRef binnacle As Subscriber.ClsBinnacle, ByVal operation As ClsOperation, ByVal bDate As Date)
            Try
                binnacle.BinnacleForms.AddNewItem(CType(Current.Session("SessionValues"), SessionValues).CurrentForm, operation, bDate)
                binnacle.Save()
            Catch ValEx As Csla.Validation.ValidationException
                For Each BrokenRule As Csla.Validation.BrokenRule In binnacle.BrokenRulesCollection
                    Throw New Exception(BrokenRule.Description)
                Next
                Throw New Exception(ValEx.Message)
            End Try
        End Sub

        Private Shared Function GetSubscriberBinnacle(ByVal BDate As Date) As Subscriber.ClsBinnacle
            If Subscriber.ClsBinnacle.ExistsUserBinnacle(CLng(Mid(Csla.ApplicationContext.User.Identity.Name, 1, Csla.ApplicationContext.User.Identity.Name.IndexOf(";"))), BDate) Then
                Return Subscriber.ClsBinnacle.GetBinnacle(CLng(Mid(Csla.ApplicationContext.User.Identity.Name, 1, Csla.ApplicationContext.User.Identity.Name.IndexOf(";"))), BDate)
            Else
                Return Subscriber.ClsBinnacle.NewBinnacle(CLng(Mid(Csla.ApplicationContext.User.Identity.Name, 1, Csla.ApplicationContext.User.Identity.Name.IndexOf(";"))), BDate)
            End If
        End Function

#End Region

#End Region

#Region " Advertisers "

#Region " Accounts "

        Public Shared Function GetAdAccount(ByVal adAccountNewData As AdAccountNewData) As AdAccountData
            Dim adAccountData As AdAccountData = New AdAccountData
            Dim adAccount As Advertiser.ClsAccount

            Dim bDate As Date = Date.Now

            Try
                adAccount = Advertiser.ClsAccount.GetAccount(adAccountNewData.ID.NewValue)
                SaveBinnacle(GetAdvertiserBinnacle(bDate.Date), ClsOperation.GetOperation("Select"), bDate, adAccountNewData, adAccount)

                Csla.Data.DataMapper.Map(adAccount, adAccountData, "Contacts", "Projects")

                Return adAccountData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function EditAdAccount(ByVal adAccountNewData As AdAccountNewData) As AdAccountData
            Dim adAccountData As AdAccountData = New AdAccountData
            Dim adAccount As Advertiser.ClsAccount

            Try
                adAccount = Advertiser.ClsAccount.GetAccount(adAccountNewData.ID.NewValue)
                CollectAdAccountData(adAccountNewData, adAccount)
                If adAccount.IsDirty Then
                    SaveAdAccount(ClsOperation.GetOperation("Update"), adAccountNewData, adAccount)
                End If

                Csla.Data.DataMapper.Map(adAccount, adAccountData, "Contacts", "Projects")

                Return adAccountData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Private Shared Sub CollectAdAccountData(ByVal adAccountNewData As AdAccountNewData, ByVal adAccount As Advertiser.ClsAccount)
            With adAccount
                .Login = adAccountNewData.Login.NewValue
                .WebPassword = adAccountNewData.WebPassword.NewValue
                .CompanyName = adAccountNewData.CompanyName.NewValue
                .CompanyNotes = adAccountNewData.CompanyNotes.NewValue
            End With
        End Sub

        Private Shared Sub SaveAdAccount(ByVal operation As ClsOperation, ByVal adAccountNewData As AdAccountNewData, ByRef adAccount As Advertiser.ClsAccount)
            Dim bDate As Date = Date.Now
            Try
                Using scope As New TransactionScope()
                    adAccountNewData.ID.NewValue = adAccount.Save.ID
                    SaveBinnacle(GetAdvertiserBinnacle(bDate.Date), operation, bDate, adAccountNewData, adAccount)
                    scope.Complete()
                End Using
            Catch ValEx As Csla.Validation.ValidationException
                For Each BrokenRule As Csla.Validation.BrokenRule In adAccount.BrokenRulesCollection
                    Throw New Exception(BrokenRule.Description)
                Next
                Throw New Exception(ValEx.Message)
            End Try
        End Sub

#End Region

#Region " AdHistories "

        Public Shared Function GetAdHistoryList(ByVal idAdvertiser As Long, ByVal idProjects() As Long, ByVal fromDate As Date, ByVal toDate As Date, ByVal fromTime As Date, ByVal toTime As Date) As AdHistoryData()
            Return GetAdHistoryList(New Long() {idAdvertiser}, New Long() {0}, idProjects, New Long() {0}, fromDate, toDate, fromTime, toTime)
        End Function

        Private Shared Function GetAdHistoryList(ByVal idAdvertisers() As Long, ByVal idContacts() As Long, ByVal idProjects() As Long, ByVal idSubscribers() As Long, ByVal fromDate As Date, ByVal toDate As Date, ByVal fromTime As Date, ByVal toTime As Date) As AdHistoryData()
            Dim adHistoriesData As New List(Of AdHistoryData)
            Dim adHistories As Advertiser.ClsAdHistoryInfoList

            Dim bDate As Date = Date.Now

            Try
                adHistories = Advertiser.ClsAdHistoryInfoList.GetAdHistoryInfoList(idAdvertisers, idContacts, idProjects, idSubscribers, fromDate, toDate, fromTime, toTime)

                SaveBinnacle(GetAdvertiserBinnacle(bDate.Date), ClsOperation.GetOperation("Select"), bDate)

                For Each adHistory As Advertiser.ClsAdHistoryInfo In adHistories
                    Dim adHistoryData As New AdHistoryData

                    Csla.Data.DataMapper.Map(adHistory, adHistoryData, "Project", "SubAccount")
                    Csla.Data.DataMapper.Map(adHistory.Project, adHistoryData.Project, "Advertiser", "Contact")

                    adHistoriesData.Add(adHistoryData)
                Next
                Return adHistoriesData.ToArray
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

#End Region

#Region " Contacts "

        Public Shared Function GetAdContactInfoList(ByVal idAdAccount As Long) As AdContactData()
            Dim adContactsData As New List(Of AdContactData)
            Dim adContacts As Advertiser.ClsContactInfoList

            Try
                adContacts = Advertiser.ClsContactInfoList.GetContactInfoList(idAdAccount, Nothing)

                For Each adContact As Advertiser.ClsContactInfo In adContacts
                    Dim adContactData As New AdContactData

                    Csla.Data.DataMapper.Map(adContact, adContactData, "FullName", "Advertiser")

                    adContactsData.Add(adContactData)
                Next
                Return AdContactsData.ToArray
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function GetAdContactList(ByVal idAdAccount As Long) As AdContactData()
            Dim adContactsData As New List(Of AdContactData)
            Dim adContacts As Advertiser.ClsContactInfoList

            Dim bDate As Date = Date.Now

            Try
                adContacts = Advertiser.ClsContactInfoList.GetContactInfoList(idAdAccount, Nothing)

                SaveBinnacle(GetAdvertiserBinnacle(bDate.Date), ClsOperation.GetOperation("Select"), bDate)

                For Each adContact As Advertiser.ClsContactInfo In adContacts
                    Dim adContactData As New AdContactData

                    Csla.Data.DataMapper.Map(adContact, adContactData, "FullName", "Advertiser")


                    adContactsData.Add(adContactData)
                Next
                Return adContactsData.ToArray
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function GetAdContact(ByVal adContactNewData As AdContactNewData) As AdContactData
            Dim adContactData As New AdContactData
            Dim adContact As Advertiser.ClsContact

            Dim bDate As Date = Date.Now

            Try
                adContact = Advertiser.ClsContact.GetContact(adContactNewData.ID.NewValue)
                SaveBinnacle(GetAdvertiserBinnacle(bDate.Date), ClsOperation.GetOperation("Select"), bDate, adContactNewData, adContact)

                Csla.Data.DataMapper.Map(adContact, adContactData, "FullName", "Advertiser", "Notes", "ToDos", "Projects")

                Return adContactData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function AddAdContact(ByVal adContactNewData As AdContactNewData) As AdContactData
            Dim adContactData As New AdContactData
            Dim adContact As Advertiser.ClsContact

            Try
                adContact = Advertiser.ClsContact.NewContact
                CollectAdContactData(adContactNewData, adContact)
                SaveAdContact(ClsOperation.GetOperation("Insert"), adContactNewData, adContact)

                Csla.Data.DataMapper.Map(adContact, adContactData, "FullName", "Advertiser", "Notes", "ToDos", "Projects")

                Return adContactData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function EditAdContact(ByVal adContactNewData As AdContactNewData) As AdContactData
            Dim adContactData As New AdContactData
            Dim adContact As Advertiser.ClsContact

            Try
                adContact = Advertiser.ClsContact.GetContact(adContactNewData.ID.NewValue)
                CollectAdContactData(adContactNewData, adContact)
                If adContact.IsDirty Then
                    SaveAdContact(ClsOperation.GetOperation("Update"), adContactNewData, adContact)
                End If

                Csla.Data.DataMapper.Map(adContact, adContactData, "FullName", "Advertiser", "Notes", "ToDos", "Projects")

                Return adContactData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Sub DeleteAdContact(ByVal adContactNewData As AdContactNewData)
            Dim adContact As Advertiser.ClsContact

            Try
                adContact = Advertiser.ClsContact.GetContact(adContactNewData.ID.NewValue)
                adContact.Delete()
                SaveAdContact(ClsOperation.GetOperation("Delete"), adContactNewData, adContact)
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Sub

        Private Shared Sub CollectAdContactData(ByVal adContactNewData As AdContactNewData, ByRef adContact As Advertiser.ClsContact)
            With adContact
                .AssignAdvertiserAccount(adContactNewData.Advertiser.ID.NewValue)
                .FirstName = adContactNewData.FirstName.NewValue
                .LastName = adContactNewData.LastName.NewValue
                .MainCompanyAddress = adContactNewData.MainCompanyAddress.NewValue
                .PrimaryAddress = adContactNewData.PrimaryAddress.NewValue
                .SecondaryAddress = adContactNewData.SecondaryAddress.NewValue
                .City = adContactNewData.City.NewValue
                .State = adContactNewData.State.NewValue
                .Country = adContactNewData.Country.NewValue
                .ZipCode = adContactNewData.ZipCode.NewValue
                .Providence = adContactNewData.Providence.NewValue
                .Department = adContactNewData.Department.NewValue
                .ResposibleForNotes = adContactNewData.ResposibleForNotes.NewValue
            End With
        End Sub

        Private Shared Sub SaveAdContact(ByVal operation As ClsOperation, ByVal adContactNewData As AdContactNewData, ByRef adContact As Advertiser.ClsContact)
            Dim bDate As Date = Date.Now
            Try
                Using scope As New TransactionScope()
                    adContactNewData.ID.NewValue = adContact.Save.ID
                    SaveBinnacle(GetAdvertiserBinnacle(bDate.Date), operation, bDate, adContactNewData, adContact)
                    scope.Complete()
                End Using
            Catch ValEx As Csla.Validation.ValidationException
                For Each BrokenRule As Csla.Validation.BrokenRule In adContact.BrokenRulesCollection
                    Throw New Exception(BrokenRule.Description)
                Next
                Throw New Exception(ValEx.Message)
            End Try
        End Sub

#End Region

#Region " Projects "

        Public Shared Function GetAdProjectInfoList(ByVal idAdvertiser As Long) As AdProjectData()
            Dim adProjectsInfoData As New List(Of AdProjectData)
            Dim adProjectsInfo As Advertiser.ClsProjectInfoList

            Try
                adProjectsInfo = Advertiser.ClsProjectInfoList.GetProjectInfoList(idAdvertiser, 0)

                For Each adProjectInfo As Advertiser.ClsProjectInfo In adProjectsInfo
                    Dim adprojectInfoData As New AdProjectData

                    Csla.Data.DataMapper.Map(adProjectInfo, adprojectInfoData, "Advertiser", "Contact")

                    adProjectsInfoData.Add(adprojectInfoData)
                Next
                Return adProjectsInfoData.ToArray
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function GetAdProjectList(ByVal idAdvertiser As Long, ByVal idContacts() As Long) As AdProjectData()
            Return GetAdProjectList(New Long() {idAdvertiser}, idContacts)
        End Function

        Private Shared Function GetAdProjectList(ByVal idAdvertisers() As Long, ByVal idContacts() As Long) As AdProjectData()
            Dim projectsInfoData As New List(Of AdProjectData)
            Dim projectsInfo As Advertiser.ClsProjectInfoList

            Dim bDate As Date = Date.Now

            Try
                projectsInfo = Advertiser.ClsProjectInfoList.GetProjectInfoList(idAdvertisers, idContacts)

                SaveBinnacle(GetAdvertiserBinnacle(bDate.Date), ClsOperation.GetOperation("Select"), bDate)

                For Each projectInfo As Advertiser.ClsProjectInfo In projectsInfo
                    Dim projectInfoData As New AdProjectData

                    Csla.Data.DataMapper.Map(projectInfo, projectInfoData, "Advertiser", "Contact")
                    Csla.Data.DataMapper.Map(projectInfo.Contact, projectInfoData.Contact, "FullName", "Advertiser")

                    projectsInfoData.Add(projectInfoData)
                Next
                Return projectsInfoData.ToArray
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function GetAdProject(ByVal adProjectNewData As AdProjectNewData) As AdProjectData
            Dim adProjectData As New AdProjectData
            Dim adProject As Advertiser.ClsProject

            Dim bDate As Date = Date.Now

            Try
                adProject = Advertiser.ClsProject.GetProject(adProjectNewData.ID.NewValue)
                SaveBinnacle(GetAdvertiserBinnacle(bDate.Date), ClsOperation.GetOperation("Select"), bDate, adProjectNewData, adProject, New Advertiser.Project.ClsDemographic() {})
                PopulateProjectData(adProject, adProjectData)

                Return adProjectData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function AddAdProject(ByVal adProjectNewData As AdProjectNewData) As AdProjectData
            Dim adProjectData As New AdProjectData
            Dim adProject As Advertiser.ClsProject

            Try
                adProject = Advertiser.ClsProject.NewProject
                CollectAdProjectData(adProjectNewData, adProject)
                SaveAdProject(ClsOperation.GetOperation("Insert"), adProjectNewData, adProject, New List(Of Advertiser.Project.ClsDemographic)(adProject.Demographics).ToArray)

                PopulateProjectData(adProject, adProjectData)

                Return adProjectData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function EditAdProject(ByVal adProjectNewData As AdProjectNewData) As AdProjectData
            Dim adProjectData As New AdProjectData
            Dim adProject As Advertiser.ClsProject

            Try
                adProject = Advertiser.ClsProject.GetProject(adProjectNewData.ID.NewValue)
                CollectAdProjectData(adProjectNewData, adProject)

                If adProject.IsDirty Then
                    Dim demographics As New List(Of Advertiser.Project.ClsDemographic)

                    For Each adDemographic As Advertiser.Project.ClsDemographic In adProject.Demographics
                        If adDemographic.IsDirty Then
                            demographics.Add(adDemographic)
                        End If
                    Next

                    SaveAdProject(ClsOperation.GetOperation("Update"), adProjectNewData, adProject, demographics.ToArray)
                End If

                PopulateProjectData(adProject, adProjectData)

                Return adProjectData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Sub DeleteAdProject(ByVal adProjectNewData As AdProjectNewData)
            Dim adProject As Advertiser.ClsProject

            Try
                adProject = Advertiser.ClsProject.GetProject(adProjectNewData.ID.NewValue)
                adProject.Delete()
                SaveAdProject(ClsOperation.GetOperation("Delete"), adProjectNewData, adProject, New List(Of Advertiser.Project.ClsDemographic)(adProject.Demographics).ToArray)
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Sub

        Private Shared Sub PopulateProjectData(ByVal adProject As Advertiser.ClsProject, ByRef adProjectData As AdProjectData)
            Csla.Data.DataMapper.Map(adProject, adProjectData, "AdvertiserAccount", "AdvertiserContact", "Prices", "AdHistories", "Demographics", "Invoices", "Receipts")
            Csla.Data.DataMapper.Map(adProject.AdvertiserContact, adProjectData.Contact, "FullName", "Advertiser")
            PopulateDemographicsData(adProject.Demographics, adProjectData)
        End Sub

        Private Shared Sub PopulateDemographicsData(ByVal adProjectDemographics As Advertiser.Project.ClsDemographicList, ByRef adProjectData As AdProjectData)
            For Each adProjectDemographic As Advertiser.Project.ClsDemographic In adProjectDemographics
                Select Case adProjectDemographic.Tag
                    Case "Sex"
                        adProjectData.Sex = adProjectDemographic.Requirement
                    Case "MinAge"
                        adProjectData.MinAge = adProjectDemographic.Requirement
                    Case "MaxAge"
                        adProjectData.MaxAge = adProjectDemographic.Requirement
                    Case "Occupation"
                        adProjectData.Occupation = adProjectDemographic.Requirement
                    Case "Country"
                        adProjectData.Country = adProjectDemographic.Requirement
                    Case "State"
                        adProjectData.State = adProjectDemographic.Requirement
                    Case Else
                End Select
            Next
        End Sub

        Private Shared Sub CollectAdProjectData(ByVal adProjectNewData As AdProjectNewData, ByRef adProject As Advertiser.ClsProject)
            With adProject
                .AssignAdvertiserAccount(adProjectNewData.Advertiser.ID.NewValue)
                .AssignContact(adProjectNewData.Contact.ID.NewValue)
                .ADUrl = adProjectNewData.ADUrl.NewValue
                .ProjectDescription = adProjectNewData.ProjectDescription.NewValue
                .ADHeight = adProjectNewData.ADHeight.NewValue
                .ADWidth = adProjectNewData.ADWidth.NewValue
                .RunStartDate = adProjectNewData.RunStartDate.NewValue
                .RunEndDate = adProjectNewData.RunEndDate.NewValue
                .StartTimeBasedOnSubscribersTime = adProjectNewData.StartTimeBasedOnSubscribersTime.NewValue
                .EndTimeBasedOnSubscribersTime = adProjectNewData.EndTimeBasedOnSubscribersTime.NewValue
                .MinDisplays = adProjectNewData.MinDisplays.NewValue
                .MaxDisplays = adProjectNewData.MaxDisplays.NewValue
                .MinPerDay = adProjectNewData.MinPerDay.NewValue
                .MaxPerDay = adProjectNewData.MaxPerDay.NewValue

                CollectAdProjectDemographicsData(adProjectNewData, adProject)
            End With
        End Sub

        Private Shared Sub CollectAdProjectDemographicsData(ByVal adProjectNewData As AdProjectNewData, ByRef adProject As Advertiser.ClsProject)
            CollectAdProjectDemographicData("MinAge", adProjectNewData.MinAge.NewValue, adProject)
            CollectAdProjectDemographicData("MaxAge", adProjectNewData.MaxAge.NewValue, adProject)
            CollectAdProjectDemographicData("Sex", adProjectNewData.Sex.NewValue, adProject)
            CollectAdProjectDemographicData("Occupation", adProjectNewData.Occupation.NewValue, adProject)
            CollectAdProjectDemographicData("Country", adProjectNewData.Country.NewValue, adProject)
            CollectAdProjectDemographicData("State", adProjectNewData.State.NewValue, adProject)
        End Sub

        Private Shared Sub CollectAdProjectDemographicData(ByVal tag As String, ByVal requirement As String, ByRef adProject As Advertiser.ClsProject)
            If adProject.Demographics.Contains(tag) Then
                adProject.Demographics.GetItem(tag).Requirement = requirement
            Else
                adProject.Demographics.AddProjectItem(tag, requirement)
            End If
        End Sub

        Private Shared Sub SaveAdProject(ByVal operation As ClsOperation, ByVal adProjectNewData As AdProjectNewData, ByRef adProject As Advertiser.ClsProject, ByVal adProjectDemographics As Advertiser.Project.ClsDemographic())
            Dim bDate As Date = Date.Now
            Try
                Using scope As New TransactionScope()
                    adProjectNewData.ID.NewValue = adProject.Save.ID
                    SaveBinnacle(GetAdvertiserBinnacle(bDate.Date), operation, bDate, adProjectNewData, adProject, adProjectDemographics)
                    scope.Complete()
                End Using
            Catch ValEx As Csla.Validation.ValidationException
                For Each BrokenRule As Csla.Validation.BrokenRule In adProject.BrokenRulesCollection
                    Throw New Exception(BrokenRule.Description)
                Next
                Throw New Exception(ValEx.Message)
            End Try
        End Sub

#End Region

#Region " Notes "

        Public Shared Function GetAdNoteList(ByVal idAdvertiser As Long, ByVal idContacts() As Long, ByVal fromDate As Date, ByVal toDate As Date) As AdNoteData()
            Return GetAdNoteList(New Long() {idAdvertiser}, idContacts, fromDate, toDate)
        End Function

        Private Shared Function GetAdNoteList(ByVal idAdvertisers() As Long, ByVal idContacts() As Long, ByVal fromDate As Date, ByVal toDate As Date) As AdNoteData()
            Dim adNotesData As New List(Of AdNoteData)
            Dim adNotes As Advertiser.ClsNoteInfoList

            Dim bDate As Date = Date.Now

            Try
                adNotes = Advertiser.ClsNoteInfoList.GetNoteInfoList(idAdvertisers, idContacts, fromDate, toDate)

                SaveBinnacle(GetAdvertiserBinnacle(bDate.Date), ClsOperation.GetOperation("Select"), bDate)

                For Each adNote As Advertiser.ClsNoteInfo In adNotes
                    Dim adNoteData As New AdNoteData

                    Csla.Data.DataMapper.Map(adNote, adNoteData, "Contact")
                    Csla.Data.DataMapper.Map(adNote.Contact, adNoteData.Contact, "Advertiser", "FullName")

                    adNotesData.Add(adNoteData)
                Next
                Return adNotesData.ToArray
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function GetAdNote(ByVal adNoteNewData As AdNoteNewData) As AdNoteData
            Dim adNoteData As New AdNoteData
            Dim adNote As Advertiser.Contact.ClsNote

            Dim bDate As Date = Date.Now

            Try
                adNote = Advertiser.Contact.ClsNote.GetNote(adNoteNewData.ID.NewValue)
                SaveBinnacle(GetAdvertiserBinnacle(bDate.Date), ClsOperation.GetOperation("Select"), bDate, adNoteNewData, adNote)

                Csla.Data.DataMapper.Map(adNote, adNoteData, "Contact")
                Csla.Data.DataMapper.Map(adNote.Contact, adNoteData.Contact, "Advertiser", "FullName")

                Return adNoteData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function AddAdNote(ByVal adNoteNewData As AdNoteNewData) As AdNoteData
            Dim adNoteData As New AdNoteData
            Dim adNote As Advertiser.Contact.ClsNote

            Try
                adNote = Advertiser.Contact.ClsNote.NewNote
                CollectAdNoteData(adNoteNewData, adNote)
                SaveAdNote(ClsOperation.GetOperation("Insert"), adNoteNewData, adNote)

                Csla.Data.DataMapper.Map(adNote, adNoteData, "Contact")
                Csla.Data.DataMapper.Map(adNote.Contact, adNoteData.Contact, "Advertiser", "FullName")

                Return adNoteData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function EditAdNote(ByVal adNoteNewData As AdNoteNewData) As AdNoteData
            Dim adNoteData As New AdNoteData
            Dim adNote As Advertiser.Contact.ClsNote

            Try
                adNote = Advertiser.Contact.ClsNote.GetNote(adNoteNewData.ID.NewValue)
                CollectAdNoteData(adNoteNewData, adNote)
                If adNote.IsDirty Then
                    SaveAdNote(ClsOperation.GetOperation("Update"), adNoteNewData, adNote)
                End If

                Csla.Data.DataMapper.Map(adNote, adNoteData, "Contact")
                Csla.Data.DataMapper.Map(adNote.Contact, adNoteData.Contact, "Advertiser", "FullName")

                Return adNoteData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Sub DeleteAdNote(ByVal adNoteNewData As AdNoteNewData)
            Dim adNote As Advertiser.Contact.ClsNote

            Try
                adNote = Advertiser.Contact.ClsNote.GetNote(adNoteNewData.ID.NewValue)
                adNote.Delete()
                SaveAdNote(ClsOperation.GetOperation("Delete"), adNoteNewData, adNote)
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Sub

        Private Shared Sub CollectAdNoteData(ByVal adNoteNewData As AdNoteNewData, ByRef adNote As Advertiser.Contact.ClsNote)
            With adNote
                .AssignContact(adNoteNewData.Contact.ID.NewValue)
                .DateEntered = adNoteNewData.DateEntered.NewValue
                .Description = adNoteNewData.Description.NewValue
            End With
        End Sub

        Private Shared Sub SaveAdNote(ByVal operation As ClsOperation, ByVal adNoteNewData As AdNoteNewData, ByRef adNote As Advertiser.Contact.ClsNote)
            Dim bDate As Date = Date.Now
            Try
                Using scope As New TransactionScope()
                    adNoteNewData.ID.NewValue = adNote.Save.ID
                    SaveBinnacle(GetAdvertiserBinnacle(bDate.Date), operation, bDate, adNoteNewData, adNote)
                    scope.Complete()
                End Using
            Catch ValEx As Csla.Validation.ValidationException
                For Each BrokenRule As Csla.Validation.BrokenRule In adNote.BrokenRulesCollection
                    Throw New Exception(BrokenRule.Description)
                Next
                Throw New Exception(ValEx.Message)
            End Try
        End Sub

#End Region

#Region " SignIn "

        Public Shared Function LoginAdvertiser(ByVal userName As String, ByVal password As String) As Boolean
            Dim bDate As Date = Date.Now
            Try
                If Security.ClsSCTAdvertiserPrincipal.Login(userName, password) Then
                    SetUserSessionValues(Csla.ApplicationContext.User, "frmAdSignIn")
                    SaveBinnacle(GetAdvertiserBinnacle(bDate.Date), ClsOperation.GetOperation("SignIn"), bDate)
                    FormsAuthentication.SetAuthCookie(userName, True)
                    Return True
                Else
                    Return False
                End If
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Sub LogoutAdvertiser()
            Security.ClsSCTAdvertiserPrincipal.Logout()
            ClearUserSessionValues(Csla.ApplicationContext.User)
            FormsAuthentication.SignOut()
            FormsAuthentication.RedirectToLoginPage()
        End Sub

#End Region

#Region " SignUp "

        Public Shared Sub SignUpAdAccount(ByVal adAccountNewData As AdAccountNewData)
            Dim adAccountData As AdAccountData = New AdAccountData
            Dim adAccount As Advertiser.ClsAccount

            Try
                adAccount = Advertiser.ClsAccount.NewAccount
                CollectSignUpAdAccountData(adAccountNewData, adAccount)
                SaveSignUpAdAccount(adAccountNewData, adAccount)

            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Sub

        Private Shared Sub CollectSignUpAdAccountData(ByVal adAccountNewData As AdAccountNewData, ByRef adAccount As Advertiser.ClsAccount)
            With adAccount
                .Login = adAccountNewData.Login.NewValue
                .WebPassword = adAccountNewData.WebPassword.NewValue
                .CompanyName = adAccountNewData.CompanyName.NewValue
                .CompanyNotes = adAccountNewData.CompanyNotes.NewValue
            End With
        End Sub

        Private Shared Sub SaveSignUpAdAccount(ByVal adAccountNewData As AdAccountNewData, ByRef adAccount As Advertiser.ClsAccount)
            Dim bDate As Date = Date.Now
            Try
                Using scope As New TransactionScope()
                    adAccountNewData.ID.NewValue = adAccount.Save.ID

                    Security.ClsSCTAdvertiserPrincipal.Login(adAccount.Login, adAccount.WebPassword)
                    SetUserSessionValues(Csla.ApplicationContext.User, "frmAdSignUp")
                    SaveBinnacle(GetAdvertiserBinnacle(bDate.Date), ClsOperation.GetOperation("SignUp"), bDate, adAccountNewData, adAccount)
                    SaveBinnacle(GetAdvertiserBinnacle(bDate.Date), ClsOperation.GetOperation("SignIn"), bDate)
                    FormsAuthentication.SetAuthCookie(adAccount.Login, True)

                    scope.Complete()
                End Using
            Catch ValEx As Csla.Validation.ValidationException
                For Each BrokenRule As Csla.Validation.BrokenRule In adAccount.BrokenRulesCollection
                    Throw New Exception(BrokenRule.Description)
                Next
                Throw New Exception(ValEx.Message)
            End Try
        End Sub

#End Region

#Region " To Dos "

        Public Shared Function GetAdToDoList(ByVal idAdvertiser As Long, ByVal idContacts() As Long, ByVal fromDate As Date, ByVal toDate As Date) As AdToDoData()
            Return GetAdToDoList(New Long() {idAdvertiser}, idContacts, fromDate, toDate)
        End Function

        Private Shared Function GetAdToDoList(ByVal idAdvertisers() As Long, ByVal idContacts() As Long, ByVal fromDate As Date, ByVal toDate As Date) As AdToDoData()
            Dim adToDosData As New List(Of AdToDoData)
            Dim adToDos As Advertiser.ClsToDoInfoList

            Dim bDate As Date = Date.Now

            Try
                adToDos = Advertiser.ClsToDoInfoList.GetToDoInfoList(idAdvertisers, idContacts, fromDate, toDate)

                SaveBinnacle(GetAdvertiserBinnacle(bDate.Date), ClsOperation.GetOperation("Select"), bDate)

                For Each adToDo As Advertiser.ClsToDoInfo In adToDos
                    Dim adToDoData As New AdToDoData

                    Csla.Data.DataMapper.Map(adToDo, adToDoData, "Contact")
                    Csla.Data.DataMapper.Map(adToDo.Contact, adToDoData.Contact, "Advertiser", "FullName")

                    adToDosData.Add(adToDoData)
                Next
                Return adToDosData.ToArray
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function GetAdToDo(ByVal adToDoNewData As AdToDoNewData) As AdToDoData
            Dim adToDoData As New AdToDoData
            Dim adToDo As Advertiser.Contact.ClsToDo

            Dim bDate As Date = Date.Now

            Try
                adToDo = Advertiser.Contact.ClsToDo.GetToDo(adToDoNewData.ID.NewValue)
                SaveBinnacle(GetAdvertiserBinnacle(bDate.Date), ClsOperation.GetOperation("Select"), bDate, adToDoNewData, adToDo)

                Csla.Data.DataMapper.Map(adToDo, adToDoData, "Contact")
                Csla.Data.DataMapper.Map(adToDo.Contact, adToDoData.Contact, "Advertiser", "FullName")

                Return adToDoData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function AddAdToDo(ByVal adToDoNewData As AdToDoNewData) As AdToDoData
            Dim adToDoData As New AdToDoData
            Dim adToDo As Advertiser.Contact.ClsToDo

            Try
                adToDo = Advertiser.Contact.ClsToDo.NewToDo
                CollectAdtodoData(adToDoNewData, adToDo)
                SaveAdToDo(ClsOperation.GetOperation("Insert"), adToDoNewData, adToDo)

                Csla.Data.DataMapper.Map(adToDo, adToDoData, "Contact")
                Csla.Data.DataMapper.Map(adToDo.Contact, adToDoData.Contact, "Advertiser", "FullName")

                Return adToDoData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function EditAdToDo(ByVal adToDoNewData As AdToDoNewData) As AdToDoData
            Dim adToDoData As New AdToDoData
            Dim adToDo As Advertiser.Contact.ClsToDo

            Try
                adToDo = Advertiser.Contact.ClsToDo.GetToDo(adToDoNewData.ID.NewValue)
                CollectAdtodoData(adToDoNewData, adToDo)
                If adToDo.IsDirty Then
                    SaveAdToDo(ClsOperation.GetOperation("Update"), adToDoNewData, adToDo)
                End If

                Csla.Data.DataMapper.Map(adToDo, adToDoData, "Contact")
                Csla.Data.DataMapper.Map(adToDo.Contact, adToDoData.Contact, "Advertiser", "FullName")

                Return adToDoData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Sub DeleteAdToDo(ByVal adToDoNewData As AdToDoNewData)
            Dim adToDo As Advertiser.Contact.ClsToDo

            Try
                adToDo = Advertiser.Contact.ClsToDo.GetToDo(adToDoNewData.ID.NewValue)
                adToDo.Delete()
                SaveAdToDo(ClsOperation.GetOperation("Delete"), adToDoNewData, adToDo)
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Sub

        Private Shared Sub CollectAdToDoData(ByVal adToDoNewData As AdToDoNewData, ByRef adToDo As Advertiser.Contact.ClsToDo)
            With adToDo
                .AssignContact(adToDoNewData.Contact.ID.NewValue)
                .CallBackRecord = adToDoNewData.CallBackRecord.NewValue
                .DateCompleted = adToDoNewData.DateCompleted.NewValue
                .DateDue = adToDoNewData.DateDue.NewValue
                .DateEntered = adToDoNewData.DateEntered.NewValue
                .TaskNotes = adToDoNewData.TaskNotes.NewValue
            End With
        End Sub

        Private Shared Sub SaveAdToDo(ByVal operation As ClsOperation, ByVal adToDoNewData As AdToDoNewData, ByRef adToDo As Advertiser.Contact.ClsToDo)
            Dim bDate As Date = Date.Now
            Try
                Using scope As New TransactionScope()
                    adToDoNewData.ID.NewValue = adToDo.Save.ID
                    SaveBinnacle(GetAdvertiserBinnacle(bDate.Date), operation, bDate, adToDoNewData, adToDo)
                    scope.Complete()
                End Using
            Catch ValEx As Csla.Validation.ValidationException
                For Each BrokenRule As Csla.Validation.BrokenRule In adToDo.BrokenRulesCollection
                    Throw New Exception(BrokenRule.Description)
                Next
                Throw New Exception(ValEx.Message)
            End Try
        End Sub

#End Region

#End Region

#Region " Subscribers "

#Region " SignIn "

        Public Shared Function LoginSubscriber(ByVal userName As String, ByVal password As String) As Boolean
            Dim bDate As Date = Date.Now
            Try
                If Security.ClsSCTSubscriberPrincipal.Login(userName, password) Then
                    SetUserSessionValues(Csla.ApplicationContext.User, "frmSubSignIn")
                    SaveBinnacle(GetSubscriberBinnacle(bDate.Date), ClsOperation.GetOperation("SignIn"), bDate)
                    FormsAuthentication.SetAuthCookie(userName, True)
                    Return True
                Else
                    Return False
                End If
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Sub LogoutSubscriber()
            Security.ClsSCTSubscriberPrincipal.Logout()
            ClearUserSessionValues(Csla.ApplicationContext.User)
            FormsAuthentication.SignOut()
            FormsAuthentication.RedirectToLoginPage()
        End Sub

#End Region

#End Region

    End Class
End Namespace

