Option Strict On

Imports SCT.DataAccess
Imports System.Transactions
Imports System.Web.HttpContext
Imports System.Web.Security

Namespace AdminSite
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
            CType(Current.Session("SessionValues"), SessionValues).Forms = ClsFormList.GetFormList(String.Empty, Logs.Administrative.ToString)
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

        Public Shared Function ContainsUserProfileInForm(ByVal formDescription As String) As Boolean
            For Each item As ClsForm In CType(Current.Session("SessionValues"), SessionValues).Forms
                If item.Description = formDescription Then
                    Return item.ContainsUserProfile
                End If
            Next
            Return False
        End Function

        Public Shared Function CanSelectInForm(ByVal formDescription As String) As Boolean
            For Each item As ClsForm In CType(Current.Session("SessionValues"), SessionValues).Forms
                If item.Description = formDescription Then
                    Return item.CanSelect()
                End If
            Next
            Return False
        End Function

        Public Shared Function CanInsertInForm(ByVal formDescription As String) As Boolean
            For Each item As ClsForm In CType(Current.Session("SessionValues"), SessionValues).Forms
                If item.Description = formDescription Then
                    Return item.CanInsert()
                End If
            Next
            Return False
        End Function

        Public Shared Function CanUpdateInForm(ByVal formDescription As String) As Boolean
            For Each item As ClsForm In CType(Current.Session("SessionValues"), SessionValues).Forms
                If item.Description = formDescription Then
                    Return item.CanUpdate()
                End If
            Next
            Return False
        End Function

        Public Shared Function CanDeleteInForm(ByVal formDescription As String) As Boolean
            For Each item As ClsForm In CType(Current.Session("SessionValues"), SessionValues).Forms
                If item.Description = formDescription Then
                    Return item.CanDelete()
                End If
            Next
            Return False
        End Function

        Public Shared Function CanSelectFieldInForm(ByVal formDescription As String, ByVal fieldDescription As String) As Boolean
            For Each item As ClsForm In CType(Current.Session("SessionValues"), SessionValues).Forms
                If item.Description = formDescription Then
                    Return item.CanSelectField(fieldDescription)
                End If
            Next
            Return False
        End Function

        Public Shared Function CanInsertFieldInForm(ByVal formDescription As String, ByVal fieldDescription As String) As Boolean
            For Each item As ClsForm In CType(Current.Session("SessionValues"), SessionValues).Forms
                If item.Description = formDescription Then
                    Return item.CanInsertField(fieldDescription)
                End If
            Next
            Return False
        End Function

        Public Shared Function CanUpdateFieldInForm(ByVal formDescription As String, ByVal fieldDescription As String) As Boolean
            For Each item As ClsForm In CType(Current.Session("SessionValues"), SessionValues).Forms
                If item.Description = formDescription Then
                    Return item.CanUpdateField(fieldDescription)
                End If
            Next
            Return False
        End Function

        Public Shared Function CanDeleteFieldInForm(ByVal formDescription As String, ByVal fieldDescription As String) As Boolean
            For Each item As ClsForm In CType(Current.Session("SessionValues"), SessionValues).Forms
                If item.Description = formDescription Then
                    Return item.CanDeleteField(fieldDescription)
                End If
            Next
            Return False
        End Function

        Private Shared Function GetUserBinnacle(ByVal BDate As Date) As Administrator.ClsBinnacle
            If Administrator.ClsBinnacle.ExistsUserBinnacle(CLng(Mid(Csla.ApplicationContext.User.Identity.Name, 1, Csla.ApplicationContext.User.Identity.Name.IndexOf(";"))), BDate) Then
                Return Administrator.ClsBinnacle.GetBinnacle(CLng(Mid(Csla.ApplicationContext.User.Identity.Name, 1, Csla.ApplicationContext.User.Identity.Name.IndexOf(";"))), BDate)
            Else
                Return Administrator.ClsBinnacle.NewBinnacle(CLng(Mid(Csla.ApplicationContext.User.Identity.Name, 1, Csla.ApplicationContext.User.Identity.Name.IndexOf(";"))), BDate)
            End If
        End Function

#End Region

#Region " Binnacle "

        Private Shared Sub SaveBinnacle(ByVal operation As ClsOperation, ByVal bDate As Date, ByVal adToDoNewData As AdToDoNewData, ByVal adToDo As Advertiser.Contact.ClsToDo)
            Dim binnacle As Administrator.ClsBinnacle = Nothing

            Try
                binnacle = GetUserBinnacle(bDate.Date)
                binnacle.BinnacleForms.AddNewItem(CType(Current.Session("SessionValues"), SessionValues).CurrentForm, operation, bDate, adToDoNewData.ID, adToDoNewData.Contact.FullName, adToDoNewData.DateEntered, adToDoNewData.DateDue, adToDoNewData.DateCompleted, adToDoNewData.CallBackRecord, adToDoNewData.TaskNotes)
                binnacle.BinnacleTables.AddNewItem(operation, adToDo.GetTableStruct.TableName, bDate, adToDo.GetTableStruct.ID, adToDo.GetTableStruct.IDAdvertiserContact, adToDo.GetTableStruct.DateEntered, adToDo.GetTableStruct.DateDue, adToDo.GetTableStruct.DateCompleted, adToDo.GetTableStruct.CallBackRecord, adToDo.GetTableStruct.TaskNotes)
                binnacle.Save()
            Catch ValEx As Csla.Validation.ValidationException
                For Each BrokenRule As Csla.Validation.BrokenRule In binnacle.BrokenRulesCollection
                    Throw New Exception(BrokenRule.Description)
                Next
                Throw New Exception(ValEx.Message)
            End Try
        End Sub

        Private Shared Sub SaveBinnacle(ByVal operation As ClsOperation, ByVal bDate As Date, ByVal adNoteNewData As AdNoteNewData, ByVal adNote As Advertiser.Contact.ClsNote)
            Dim binnacle As Administrator.ClsBinnacle = Nothing

            Try
                binnacle = GetUserBinnacle(bDate.Date)
                binnacle.BinnacleForms.AddNewItem(CType(Current.Session("SessionValues"), SessionValues).CurrentForm, operation, bDate, adNoteNewData.ID, adNoteNewData.Contact.FullName, adNoteNewData.DateEntered, adNoteNewData.Description)
                binnacle.BinnacleTables.AddNewItem(operation, adNote.GetTableStruct.TableName, bDate, adNote.GetTableStruct.ID, adNote.GetTableStruct.IDAdvertiserContact, adNote.GetTableStruct.DateEntered, adNote.GetTableStruct.DescriptionOfNotes)
                binnacle.Save()
            Catch ValEx As Csla.Validation.ValidationException
                For Each BrokenRule As Csla.Validation.BrokenRule In binnacle.BrokenRulesCollection
                    Throw New Exception(BrokenRule.Description)
                Next
                Throw New Exception(ValEx.Message)
            End Try
        End Sub

        Private Shared Sub SaveBinnacle(ByVal operation As ClsOperation, ByVal bDate As Date, ByVal AdProjectNewData As AdProjectNewData, ByVal adProject As Advertiser.ClsProject, ByVal adProjectDemographics() As Advertiser.Project.ClsDemographic)
            Dim binnacle As Administrator.ClsBinnacle = Nothing

            Try
                binnacle = GetUserBinnacle(bDate.Date)
                binnacle.BinnacleForms.AddNewItem(CType(Current.Session("SessionValues"), SessionValues).CurrentForm, operation, bDate, AdProjectNewData.ID, AdProjectNewData.Contact.FullName, AdProjectNewData.ADUrl, AdProjectNewData.ProjectDescription, AdProjectNewData.ADHeight, AdProjectNewData.ADWidth, AdProjectNewData.RunStartDate, AdProjectNewData.RunEndDate, AdProjectNewData.StartTimeBasedOnSubscribersTime, AdProjectNewData.EndTimeBasedOnSubscribersTime, AdProjectNewData.MinDisplays, AdProjectNewData.MaxDisplays, AdProjectNewData.MinPerDay, AdProjectNewData.MaxPerDay, AdProjectNewData.AdCountDisplayed, AdProjectNewData.AdVerifiedDate, AdProjectNewData.AdOnlineDate, AdProjectNewData.Sex, AdProjectNewData.MinAge, AdProjectNewData.MaxAge, AdProjectNewData.Occupation, AdProjectNewData.Country, AdProjectNewData.State)
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

        Private Shared Sub SaveBinnacle(ByVal operation As ClsOperation, ByVal bDate As Date, ByVal formData As AdContactNewData, ByVal adContact As Advertiser.ClsContact)
            Dim binnacle As Administrator.ClsBinnacle = Nothing

            Try
                binnacle = GetUserBinnacle(bDate.Date)
                binnacle.BinnacleForms.AddNewItem(CType(Current.Session("SessionValues"), SessionValues).CurrentForm, operation, bDate, formData.ID, formData.Advertiser.CompanyName, formData.FirstName, formData.LastName, formData.MainCompanyAddress, formData.PrimaryAddress, formData.SecondaryAddress, formData.City, formData.State, formData.Country, formData.ZipCode, formData.Providence, formData.Department, formData.ResposibleForNotes)
                binnacle.BinnacleTables.AddNewItem(operation, adContact.GetTableStruct.TableName, bDate, adContact.GetTableStruct.ID, adContact.GetTableStruct.IDAdvertiser, adContact.GetTableStruct.FirstName, adContact.GetTableStruct.LastName, adContact.GetTableStruct.PrimaryAddress, adContact.GetTableStruct.SecondaryAddress, adContact.GetTableStruct.MainCompanyAddress, adContact.GetTableStruct.City, adContact.GetTableStruct.State, adContact.GetTableStruct.Country, adContact.GetTableStruct.ZipCode, adContact.GetTableStruct.Department, adContact.GetTableStruct.Providence, adContact.GetTableStruct.ResposibleForNotes)
                binnacle.Save()
            Catch ValEx As Csla.Validation.ValidationException
                For Each BrokenRule As Csla.Validation.BrokenRule In binnacle.BrokenRulesCollection
                    Throw New Exception(BrokenRule.Description)
                Next
                Throw New Exception(ValEx.Message)
            End Try
        End Sub

        Private Shared Sub SaveBinnacle(ByVal operation As ClsOperation, ByVal bDate As Date, ByVal formData As AdAccountNewData, ByVal adAccount As Advertiser.ClsAccount)
            Dim binnacle As Administrator.ClsBinnacle = Nothing

            Try
                binnacle = GetUserBinnacle(bDate.Date)
                binnacle.BinnacleForms.AddNewItem(CType(Current.Session("SessionValues"), SessionValues).CurrentForm, operation, bDate, formData.ID, formData.Login, formData.WebPassword, formData.CompanyName, formData.CompanyNotes)
                binnacle.BinnacleTables.AddNewItem(operation, adAccount.GetTableStruct.TableName, bDate, adAccount.GetTableStruct.ID, adAccount.GetTableStruct.Login, adAccount.GetTableStruct.WebPassword, adAccount.GetTableStruct.ClientPassword, adAccount.GetTableStruct.HintByPassOne, adAccount.GetTableStruct.HintByPassTwo, adAccount.GetTableStruct.CompanyName, adAccount.GetTableStruct.CompanyNotes)
                binnacle.Save()
            Catch ValEx As Csla.Validation.ValidationException
                For Each BrokenRule As Csla.Validation.BrokenRule In binnacle.BrokenRulesCollection
                    Throw New Exception(BrokenRule.Description)
                Next
                Throw New Exception(ValEx.Message)
            End Try
        End Sub

        Private Shared Sub SaveBinnacle(ByVal operation As ClsOperation, ByVal bDate As Date, ByVal formData As AdReceiptNewData, ByVal receipt As Advertiser.ClsReceipt)
            Dim binnacle As Administrator.ClsBinnacle = Nothing

            Try
                binnacle = GetUserBinnacle(bDate.Date)
                binnacle.BinnacleForms.AddNewItem(CType(Current.Session("SessionValues"), SessionValues).CurrentForm, operation, bDate, formData.ID, formData.ReceiptNumber, formData.PaymentDate, formData.PaymentType, formData.PaymentNumber, formData.PaymentAmount)
                binnacle.BinnacleTables.AddNewItem(operation, receipt.GetTableStruct.TableName, bDate, receipt.GetTableStruct.ID, receipt.GetTableStruct.ReceiptNumber, receipt.GetTableStruct.PaymentNumber, receipt.GetTableStruct.PaymentDate, receipt.GetTableStruct.PaymentType, receipt.GetTableStruct.PaymentNumber, receipt.GetTableStruct.PaymentAmount)
                binnacle.Save()
            Catch ValEx As Csla.Validation.ValidationException
                For Each BrokenRule As Csla.Validation.BrokenRule In binnacle.BrokenRulesCollection
                    Throw New Exception(BrokenRule.Description)
                Next
                Throw New Exception(ValEx.Message)
            End Try
        End Sub

        Private Shared Sub SaveBinnacle(ByVal operation As ClsOperation, ByVal bDate As Date, ByVal formData As AdInvoiceNewData, ByVal invoice As Advertiser.Project.ClsInvoice)
            Dim binnacle As Administrator.ClsBinnacle = Nothing

            Try
                binnacle = GetUserBinnacle(bDate.Date)
                binnacle.BinnacleForms.AddNewItem(CType(Current.Session("SessionValues"), SessionValues).CurrentForm, operation, bDate, formData.ID, formData.InvoiceNumber, formData.InvoiceSequence, formData.InvoiceDate, formData.Project.Advertiser.CompanyName, formData.Project.ADUrl, formData.PaidToDate, formData.ChargedToDate, formData.PreviousNumberOfClickThrough, formData.PreviousNumberOfDisplays, formData.PreviousBalance, formData.TotalAmountDue)
                binnacle.BinnacleTables.AddNewItem(operation, invoice.GetTableStruct.TableName, bDate, invoice.GetTableStruct.ID, invoice.GetTableStruct.IDAdvertiserProject, invoice.GetTableStruct.InvoiceNumber, invoice.GetTableStruct.InvoiceSequence, invoice.GetTableStruct.InvoiceDate, invoice.GetTableStruct.PaidToDate, invoice.GetTableStruct.ChargedToDate, invoice.GetTableStruct.PreviousNumberOfClickThrough, invoice.GetTableStruct.PreviousNumberOfDisplays, invoice.GetTableStruct.PreviousBalance, invoice.GetTableStruct.TotalAmountDue)
                binnacle.Save()
            Catch ValEx As Csla.Validation.ValidationException
                For Each BrokenRule As Csla.Validation.BrokenRule In binnacle.BrokenRulesCollection
                    Throw New Exception(BrokenRule.Description)
                Next
                Throw New Exception(ValEx.Message)
            End Try
        End Sub

        Private Shared Sub SaveBinnacle(ByVal operation As ClsOperation, ByVal bDate As Date, ByVal formData As GroupProfileNewData, ByVal GroupProfile As ClsGroupProfile)
            Dim binnacle As Administrator.ClsBinnacle = Nothing

            Try
                binnacle = GetUserBinnacle(bDate.Date)
                binnacle.BinnacleForms.AddNewItem(CType(Current.Session("SessionValues"), SessionValues).CurrentForm, operation, bDate, formData.ID, formData.Description, formData.Group.Form.ID, formData.Group.Form.Description, formData.Group.ID, formData.Group.Description, formData.PSelect, formData.PInsert, formData.PUpdate, formData.PDelete)
                binnacle.BinnacleTables.AddNewItem(operation, GroupProfile.GetTableStruct.TableName, bDate, GroupProfile.GetTableStruct.IDGroup, GroupProfile.GetTableStruct.IDForm, GroupProfile.GetTableStruct.IDProfile, GroupProfile.GetTableStruct.pSelect, GroupProfile.GetTableStruct.pInsert, GroupProfile.GetTableStruct.pUpdate, GroupProfile.GetTableStruct.pDelete)
                binnacle.Save()
            Catch ValEx As Csla.Validation.ValidationException
                For Each BrokenRule As Csla.Validation.BrokenRule In binnacle.BrokenRulesCollection
                    Throw New Exception(BrokenRule.Description)
                Next
                Throw New Exception(ValEx.Message)
            End Try
        End Sub

        Private Shared Sub SaveBinnacle(ByVal operation As ClsOperation, ByVal bDate As Date, ByVal formData As FormProfileNewData, ByVal formProfile As ClsFormProfile)
            Dim binnacle As Administrator.ClsBinnacle = Nothing

            Try
                binnacle = GetUserBinnacle(bDate.Date)
                binnacle.BinnacleForms.AddNewItem(CType(Current.Session("SessionValues"), SessionValues).CurrentForm, operation, bDate, formData.ID, formData.Description, formData.Form.ID, formData.Form.Description, formData.PSelect, formData.PInsert, formData.PUpdate, formData.PDelete)
                binnacle.BinnacleTables.AddNewItem(operation, formProfile.GetTableStruct.TableName, bDate, formProfile.GetTableStruct.IDForm, formProfile.GetTableStruct.IDProfile, formProfile.GetTableStruct.pSelect, formProfile.GetTableStruct.pInsert, formProfile.GetTableStruct.pUpdate, formProfile.GetTableStruct.pDelete)
                binnacle.Save()
            Catch ValEx As Csla.Validation.ValidationException
                For Each BrokenRule As Csla.Validation.BrokenRule In binnacle.BrokenRulesCollection
                    Throw New Exception(BrokenRule.Description)
                Next
                Throw New Exception(ValEx.Message)
            End Try
        End Sub

        Private Shared Sub SaveBinnacle(ByVal operation As ClsOperation, ByVal bDate As Date, ByVal formData As ProfileGroupNewData, ByVal profileGroup As ClsProfileGroup)
            Dim binnacle As Administrator.ClsBinnacle = Nothing

            Try
                binnacle = GetUserBinnacle(bDate.Date)
                binnacle.BinnacleForms.AddNewItem(CType(Current.Session("SessionValues"), SessionValues).CurrentForm, operation, bDate, formData.ID, formData.Description, formData.Form.ID, formData.Form.Description, formData.Form.Profile.ID, formData.Form.Profile.Description, formData.PSelect, formData.PInsert, formData.PUpdate, formData.PDelete)
                binnacle.BinnacleTables.AddNewItem(operation, profileGroup.GetTableStruct.TableName, bDate, profileGroup.GetTableStruct.IDGroup, profileGroup.GetTableStruct.IDForm, profileGroup.GetTableStruct.IDProfile, profileGroup.GetTableStruct.pSelect, profileGroup.GetTableStruct.pInsert, profileGroup.GetTableStruct.pUpdate, profileGroup.GetTableStruct.pDelete)
                binnacle.Save()
            Catch ValEx As Csla.Validation.ValidationException
                For Each BrokenRule As Csla.Validation.BrokenRule In binnacle.BrokenRulesCollection
                    Throw New Exception(BrokenRule.Description)
                Next
                Throw New Exception(ValEx.Message)
            End Try
        End Sub

        Private Shared Sub SaveBinnacle(ByVal operation As ClsOperation, ByVal bDate As Date, ByVal formData As ProfileFormNewData, ByVal profileForm As ClsProfileForm)
            Dim binnacle As Administrator.ClsBinnacle = Nothing

            Try
                binnacle = GetUserBinnacle(bDate.Date)
                binnacle.BinnacleForms.AddNewItem(CType(Current.Session("SessionValues"), SessionValues).CurrentForm, operation, bDate, formData.ID, formData.Description, formData.Profile.ID, formData.Profile.Description, formData.PSelect, formData.PInsert, formData.PUpdate, formData.PDelete)
                binnacle.BinnacleTables.AddNewItem(operation, profileForm.GetTableStruct.TableName, bDate, profileForm.GetTableStruct.IDForm, profileForm.GetTableStruct.IDProfile, profileForm.GetTableStruct.pSelect, profileForm.GetTableStruct.pInsert, profileForm.GetTableStruct.pUpdate, profileForm.GetTableStruct.pDelete)
                binnacle.Save()
            Catch ValEx As Csla.Validation.ValidationException
                For Each BrokenRule As Csla.Validation.BrokenRule In binnacle.BrokenRulesCollection
                    Throw New Exception(BrokenRule.Description)
                Next
                Throw New Exception(ValEx.Message)
            End Try
        End Sub

        Private Shared Sub SaveBinnacle(ByVal operation As ClsOperation, ByVal bDate As Date, ByVal formData As FieldNewData, ByVal field As ClsField)
            Dim binnacle As Administrator.ClsBinnacle = Nothing

            Try
                binnacle = GetUserBinnacle(bDate.Date)
                binnacle.BinnacleForms.AddNewItem(CType(Current.Session("SessionValues"), SessionValues).CurrentForm, operation, bDate, formData.ID, formData.Group.Description, formData.Group.Form.Description, formData.Description)
                binnacle.BinnacleTables.AddNewItem(operation, field.GetTableStruct.TableName, bDate, field.GetTableStruct.ID, field.GetTableStruct.IDGroup, field.GetTableStruct.IDForm, field.GetTableStruct.Description)
                binnacle.Save()
            Catch ValEx As Csla.Validation.ValidationException
                For Each BrokenRule As Csla.Validation.BrokenRule In binnacle.BrokenRulesCollection
                    Throw New Exception(BrokenRule.Description)
                Next
                Throw New Exception(ValEx.Message)
            End Try
        End Sub

        Private Shared Sub SaveBinnacle(ByVal operation As ClsOperation, ByVal bDate As Date, ByVal formData As GroupNewData, ByVal group As ClsGroup)
            Dim binnacle As Administrator.ClsBinnacle = Nothing

            Try
                binnacle = GetUserBinnacle(bDate.Date)
                binnacle.BinnacleForms.AddNewItem(CType(Current.Session("SessionValues"), SessionValues).CurrentForm, operation, bDate, formData.ID, formData.Form.Description, formData.Description)
                binnacle.BinnacleTables.AddNewItem(operation, group.GetTableStruct.TableName, bDate, group.GetTableStruct.ID, group.GetTableStruct.IDForm, group.GetTableStruct.Description)
                binnacle.Save()
            Catch ValEx As Csla.Validation.ValidationException
                For Each BrokenRule As Csla.Validation.BrokenRule In binnacle.BrokenRulesCollection
                    Throw New Exception(BrokenRule.Description)
                Next
                Throw New Exception(ValEx.Message)
            End Try
        End Sub

        Private Shared Sub SaveBinnacle(ByVal operation As ClsOperation, ByVal bDate As Date, ByVal formData As FormNewData, ByVal form As ClsForm)
            Dim binnacle As Administrator.ClsBinnacle = Nothing

            Try
                binnacle = GetUserBinnacle(bDate.Date)
                binnacle.BinnacleForms.AddNewItem(CType(Current.Session("SessionValues"), SessionValues).CurrentForm, operation, bDate, formData.ID, formData.Description, formData.LogDescription)
                binnacle.BinnacleTables.AddNewItem(operation, form.GetTableStruct.TableName, bDate, form.GetTableStruct.ID, form.GetTableStruct.Description, form.GetTableStruct.LogDescription)
                binnacle.Save()
            Catch ValEx As Csla.Validation.ValidationException
                For Each BrokenRule As Csla.Validation.BrokenRule In binnacle.BrokenRulesCollection
                    Throw New Exception(BrokenRule.Description)
                Next
                Throw New Exception(ValEx.Message)
            End Try
        End Sub

        Private Shared Sub SaveBinnacle(ByVal operation As ClsOperation, ByVal bDate As Date, ByVal formData As ProfileNewData, ByVal profile As ClsProfile)
            Dim binnacle As Administrator.ClsBinnacle = Nothing

            Try
                binnacle = GetUserBinnacle(bDate.Date)
                binnacle.BinnacleForms.AddNewItem(CType(Current.Session("SessionValues"), SessionValues).CurrentForm, operation, bDate, formData.ID, formData.Description)
                binnacle.BinnacleTables.AddNewItem(operation, profile.GetTableStruct.TableName, bDate, profile.GetTableStruct.ID, profile.GetTableStruct.Description)
                binnacle.Save()
            Catch ValEx As Csla.Validation.ValidationException
                For Each BrokenRule As Csla.Validation.BrokenRule In binnacle.BrokenRulesCollection
                    Throw New Exception(BrokenRule.Description)
                Next
                Throw New Exception(ValEx.Message)
            End Try
        End Sub

        Private Shared Sub SaveBinnacle(ByVal operation As ClsOperation, ByVal bDate As Date, ByVal formData As BinnacleFormEntryNewData, ByVal binnacleFormEntry As ClsBinnacleFormEntry)
            Dim binnacle As Administrator.ClsBinnacle = Nothing

            Try
                binnacle = GetUserBinnacle(bDate.Date)
                binnacle.BinnacleForms.AddNewItem(CType(Current.Session("SessionValues"), SessionValues).CurrentForm, operation, bDate, formData.ID, formData.Log, formData.BDate, formData.BHour, formData.User.Name, formData.Form.Description, formData.Operation.Description)
                binnacle.BinnacleTables.AddNewItem(operation, binnacleFormEntry.GetTableStruct.TableName, bDate, binnacleFormEntry.GetTableStruct.ID, binnacleFormEntry.GetTableStruct.BDate, binnacleFormEntry.GetTableStruct.BHour, binnacleFormEntry.GetTableStruct.IDUser, binnacleFormEntry.GetTableStruct.IDForm, binnacleFormEntry.GetTableStruct.IDOperation)
                binnacle.Save()
            Catch ValEx As Csla.Validation.ValidationException
                For Each BrokenRule As Csla.Validation.BrokenRule In binnacle.BrokenRulesCollection
                    Throw New Exception(BrokenRule.Description)
                Next
                Throw New Exception(ValEx.Message)
            End Try
        End Sub

        Private Shared Sub SaveBinnacle(ByVal operation As ClsOperation, ByVal bDate As Date, ByVal formData As UserNewData, ByVal user As ClsUser)
            Dim binnacle As Administrator.ClsBinnacle = Nothing

            Try
                binnacle = GetUserBinnacle(bDate.Date)
                'binnacle.BinnacleForms.Add(Administrator.ClsBinnacleForm.NewChildBinnacleForm(CType(Current.Session("SessionValues"), SessionValues).CurrentForm, operation, bDate, formData.ID, formData.Profile.Description, formData.FirstName, formData.LastName, formData.Login, formData.Password))
                'binnacle.BinnacleTables.Add(Administrator.ClsBinnacleTable.NewChildBinnacleTable(operation, user.GetTableStruct.TableName, bDate, user.GetTableStruct.ID, user.GetTableStruct.IDProfile, user.GetTableStruct.FirstName, user.GetTableStruct.LastName, user.GetTableStruct.Login, user.GetTableStruct.Password))

                binnacle.BinnacleForms.AddNewItem(CType(Current.Session("SessionValues"), SessionValues).CurrentForm, operation, bDate, formData.ID, formData.Profile.Description, formData.FirstName, formData.LastName, formData.Login, formData.Password)
                binnacle.BinnacleTables.AddNewItem(operation, user.GetTableStruct.TableName, bDate, user.GetTableStruct.ID, user.GetTableStruct.IDProfile, user.GetTableStruct.FirstName, user.GetTableStruct.LastName, user.GetTableStruct.Login, user.GetTableStruct.Password)
                binnacle.Save()
            Catch ValEx As Csla.Validation.ValidationException
                For Each BrokenRule As Csla.Validation.BrokenRule In binnacle.BrokenRulesCollection
                    Throw New Exception(BrokenRule.Description)
                Next
                Throw New Exception(ValEx.Message)
            End Try
        End Sub

        Private Shared Sub SaveBinnacle(ByVal operation As ClsOperation, ByVal bDate As Date)
            Dim binnacle As Administrator.ClsBinnacle = Nothing

            Try
                binnacle = GetUserBinnacle(bDate.Date)
                'binnacle.BinnacleForms.Add(Administrator.ClsBinnacleForm.NewChildBinnacleForm(CType(Current.Session("SessionValues"), SessionValues).CurrentForm, operation, bDate))
                binnacle.BinnacleForms.AddNewItem(CType(Current.Session("SessionValues"), SessionValues).CurrentForm, operation, bDate)
                binnacle.Save()
            Catch ValEx As Csla.Validation.ValidationException
                For Each BrokenRule As Csla.Validation.BrokenRule In binnacle.BrokenRulesCollection
                    Throw New Exception(BrokenRule.Description)
                Next
                Throw New Exception(ValEx.Message)
            End Try
        End Sub

#End Region

#Region " ADHistories "

        Public Shared Function GetAdHistoryList(ByVal idAdvertisers() As Long, ByVal idProjects() As Long, ByVal fromDate As Date, ByVal toDate As Date, ByVal fromTime As Date, ByVal toTime As Date) As AdHistoryData()
            Return GetAdHistoryList(idAdvertisers, New Long() {}, idProjects, New Long() {}, fromDate, toDate, fromTime, toTime)
        End Function

        Private Shared Function GetAdHistoryList(ByVal idAdvertisers() As Long, ByVal idContacts() As Long, ByVal idProjects() As Long, ByVal idSubscribers() As Long, ByVal fromDate As Date, ByVal toDate As Date, ByVal fromTime As Date, ByVal toTime As Date) As AdHistoryData()
            Dim adHistoriesData As New List(Of AdHistoryData)
            Dim adHistories As Advertiser.ClsAdHistoryInfoList

            Try
                adHistories = Advertiser.ClsAdHistoryInfoList.GetAdHistoryInfoList(idAdvertisers, idContacts, idProjects, idSubscribers, fromDate, toDate, fromTime, toTime)

                SaveBinnacle(ClsOperation.GetOperation("Select"), Date.Now)

                For Each adHistory As Advertiser.ClsAdHistoryInfo In adHistories
                    Dim adHistoryData As New AdHistoryData

                    Csla.Data.DataMapper.Map(adHistory, adHistoryData, "Project", "SubAccount")
                    Csla.Data.DataMapper.Map(adHistory.Project, adHistoryData.Project, "Advertiser", "Contact")
                    Csla.Data.DataMapper.Map(adHistory.Project.Advertiser, adHistoryData.Project.Advertiser)

                    adHistoriesData.Add(adHistoryData)
                Next
                Return adHistoriesData.ToArray
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

#End Region

#Region " Advertisers Account "

        Public Shared Function GetAdAccountInfoList() As AdAccountData()
            Dim adAccountsInfoData As New List(Of AdAccountData)
            Dim adAccountsInfo As Advertiser.ClsAccountInfoList

            Try
                adAccountsInfo = Advertiser.ClsAccountInfoList.GetAdvertiserAccountInfoList

                For Each advertiserInfo As Advertiser.ClsAccountInfo In adAccountsInfo
                    Dim advertiserInfoData As New AdAccountData

                    Csla.Data.DataMapper.Map(advertiserInfo, advertiserInfoData)

                    adAccountsInfoData.Add(advertiserInfoData)
                Next
                Return adAccountsInfoData.ToArray
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function GetAdAccountList() As AdAccountData()
            Dim adAccountsData As New List(Of AdAccountData)
            Dim adAccounts As Advertiser.ClsAccountInfoList

            Try
                adAccounts = Advertiser.ClsAccountInfoList.GetAdvertiserAccountInfoList

                SaveBinnacle(ClsOperation.GetOperation("Select"), Date.Now)

                For Each adAccount As Advertiser.ClsAccountInfo In adAccounts
                    Dim adAccountData As New AdAccountData

                    Csla.Data.DataMapper.Map(adAccount, adAccountData)

                    adAccountsData.Add(adAccountData)
                Next
                Return adAccountsData.ToArray
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function GetAdAccount(ByVal adAccountNewData As AdAccountNewData) As AdAccountData
            Dim adAccountData As AdAccountData = New AdAccountData
            Dim adAccount As Advertiser.ClsAccount

            Try
                adAccount = Advertiser.ClsAccount.GetAccount(adAccountNewData.ID.NewValue)
                SaveBinnacle(ClsOperation.GetOperation("Select"), Date.Now, adAccountNewData, adAccount)

                Csla.Data.DataMapper.Map(adAccount, adAccountData, "Contacts", "Projects")

                Return adAccountData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function AddAdAccount(ByVal adAccountNewData As AdAccountNewData) As AdAccountData
            Dim adAccountData As AdAccountData = New AdAccountData
            Dim adAccount As Advertiser.ClsAccount

            Try
                adAccount = Advertiser.ClsAccount.NewAccount
                CollectAdAccountData(adAccountNewData, adAccount)
                SaveAdAccount(ClsOperation.GetOperation("Insert"), adAccountNewData, adAccount)

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

        Public Shared Sub DeleteAdAccount(ByVal adAccountNewData As AdAccountNewData)
            Dim adAccount As Advertiser.ClsAccount

            Try
                adAccount = Advertiser.ClsAccount.GetAccount(adAccountNewData.ID.NewValue)
                adAccount.Delete()
                SaveAdAccount(ClsOperation.GetOperation("Delete"), adAccountNewData, adAccount)
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Sub

        Private Shared Sub CollectAdAccountData(ByVal adAccountNewData As AdAccountNewData, ByRef adAccount As Advertiser.ClsAccount)
            With adAccount
                .Login = adAccountNewData.Login.NewValue
                .WebPassword = adAccountNewData.WebPassword.NewValue
                .CompanyName = adAccountNewData.CompanyName.NewValue
                .CompanyNotes = adAccountNewData.CompanyNotes.NewValue
            End With
        End Sub

        Private Shared Sub SaveAdAccount(ByVal operation As ClsOperation, ByVal adAccountNewData As AdAccountNewData, ByRef adAccount As Advertiser.ClsAccount)
            Try
                Using scope As New TransactionScope()
                    adAccountNewData.ID.NewValue = adAccount.Save.ID
                    SaveBinnacle(operation, Date.Now, adAccountNewData, adAccount)
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

#Region " Binnacle Form Entries "

        Public Shared Function GetBinnacleFormEntryList(ByVal log() As DataAccess.Logs, ByVal idUser As SearchCriteriaList(Of Long), ByVal fromDate As SearchCriteria(Of Date), ByVal toDate As SearchCriteria(Of Date), ByVal idForm As SearchCriteriaList(Of Long), ByVal idOperation As SearchCriteriaList(Of Long)) As BinnacleFormEntryData()
            Dim binnacleFormEntriesData As New List(Of BinnacleFormEntryData)
            Dim binnacleFormEntries As ClsBinnacleFormEntryList

            Try
                binnacleFormEntries = ClsBinnacleFormEntryList.GetBinnacleFormEntryList(log, idUser, idForm, idOperation, fromDate, toDate, SearchCriteria(Of Date).Empty, SearchCriteria(Of Date).Empty)

                SaveBinnacle(ClsOperation.GetOperation("Select"), Date.Now)

                For Each binnacleFormEntry As ClsBinnacleFormEntry In binnacleFormEntries
                    Dim binnacleFormEntryData As New BinnacleFormEntryData

                    Csla.Data.DataMapper.Map(binnacleFormEntry, binnacleFormEntryData, "User", "Form", "Operation", "BinnacleFormEntryFields")
                    Csla.Data.DataMapper.Map(binnacleFormEntry.User, binnacleFormEntryData.User)
                    Csla.Data.DataMapper.Map(binnacleFormEntry.Form, binnacleFormEntryData.Form, "Profiles")
                    Csla.Data.DataMapper.Map(binnacleFormEntry.Operation, binnacleFormEntryData.Operation)

                    binnacleFormEntriesData.Add(binnacleFormEntryData)
                Next

                Return binnacleFormEntriesData.ToArray
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function GetBinnacleFormEntry(ByVal formData As BinnacleFormEntryNewData) As BinnacleFormEntryData
            Dim binnacleFormEntryData As BinnacleFormEntryData = New BinnacleFormEntryData
            Dim binnacleFormEntry As ClsBinnacleFormEntry

            Try
                binnacleFormEntry = ClsBinnacleFormEntry.GetBinnacleFormEntry(formData.Log.NewValue, formData.ID.NewValue)

                SaveBinnacle(ClsOperation.GetOperation("Select"), Date.Now, formData, binnacleFormEntry)

                Csla.Data.DataMapper.Map(binnacleFormEntry, binnacleFormEntryData, "User", "Form", "Operation", "BinnacleFormEntryFields")
                Csla.Data.DataMapper.Map(binnacleFormEntry.User, binnacleFormEntryData.User)
                Csla.Data.DataMapper.Map(binnacleFormEntry.Form, binnacleFormEntryData.Form, "Profiles")
                Csla.Data.DataMapper.Map(binnacleFormEntry.Operation, binnacleFormEntryData.Operation)

                For Each binnacleFormFieldEntry As ClsBinnacleFormFieldEntry In binnacleFormEntry.BinnacleFormEntryFields
                    Dim binnacleFormFieldEntryData As New BinnacleFormFieldEntryData

                    Csla.Data.DataMapper.Map(binnacleFormFieldEntry, binnacleFormFieldEntryData, "BinnacleFormEntry", "Field")
                    Csla.Data.DataMapper.Map(binnacleFormFieldEntry.Field, binnacleFormFieldEntryData.Field, "Group")

                    binnacleFormEntryData.AddBinnacleFormFields(binnacleFormFieldEntryData)
                Next

                Return binnacleFormEntryData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try

        End Function

#End Region

#Region " Binnacle Users "

        Public Shared Function GetBinnacleUserInfoList(ByVal Log As Logs) As BinnacleUserData()
            Dim binnacleUsersInfoData As New List(Of BinnacleUserData)
            Dim binnacleUsersInfo As ClsBinnacleUserInfoList

            Try
                binnacleUsersInfo = ClsBinnacleUserInfoList.GetBinnacleUserInfoList(Log)

                For Each binnacleUserInfo As ClsBinnacleUserInfo In binnacleUsersInfo
                    Dim binnacleUserInfoData As New BinnacleUserData

                    Csla.Data.DataMapper.Map(binnacleUserInfo, binnacleUserInfoData)

                    binnacleUsersInfoData.Add(binnacleUserInfoData)
                Next

                Return binnacleUsersInfoData.ToArray
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

#End Region

#Region " Contacts "

        Public Shared Function GetAdContactInfoList(ByVal idAdvertiser As Long) As AdContactData()
            Dim adContactsInfoData As New List(Of AdContactData)
            Dim adContactsInfo As Advertiser.ClsContactInfoList

            Try
                adContactsInfo = Advertiser.ClsContactInfoList.GetContactInfoList(idAdvertiser, Nothing)

                For Each adContactInfo As Advertiser.ClsContactInfo In adContactsInfo
                    Dim adContactInfoData As New AdContactData

                    Csla.Data.DataMapper.Map(adContactInfo, adContactInfoData, "Advertiser", "FullName")

                    adContactsInfoData.Add(adContactInfoData)
                Next
                Return adContactsInfoData.ToArray
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function GetAdContactList() As AdContactData()
            Return GetAdContactList(New Long() {})
        End Function

        Public Shared Function GetAdContactList(ByVal idAdAccount() As Long) As AdContactData()
            Return GetAdContactList(idAdAccount, Nothing)
        End Function

        Public Shared Function GetAdContactList(ByVal mainCompanyAddress As Boolean) As AdContactData()
            Return GetAdContactList(New Long() {}, mainCompanyAddress)
        End Function

        Public Shared Function GetAdContactList(ByVal idAdAccount() As Long, ByVal mainCompanyAddress As Boolean) As AdContactData()
            Dim adContactsData As New List(Of AdContactData)
            Dim adContacts As Advertiser.ClsContactInfoList

            Try
                adContacts = Advertiser.ClsContactInfoList.GetContactInfoList(idAdAccount, mainCompanyAddress)

                SaveBinnacle(ClsOperation.GetOperation("Select"), Date.Now)

                For Each adContact As Advertiser.ClsContactInfo In adContacts
                    Dim adContactData As New AdContactData

                    Csla.Data.DataMapper.Map(adContact, adContactData, "FullName", "Advertiser")
                    Csla.Data.DataMapper.Map(adContact.Advertiser, adContactData.Advertiser)

                    adContactsData.Add(adContactData)
                Next
                Return adContactsData.ToArray
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function GetAdContact(ByVal AdContactNewData As AdContactNewData) As AdContactData
            Dim adContactData As AdContactData = New AdContactData
            Dim adContact As Advertiser.ClsContact

            Try
                adContact = Advertiser.ClsContact.GetContact(AdContactNewData.ID.NewValue)
                SaveBinnacle(ClsOperation.GetOperation("Select"), Date.Now, AdContactNewData, adContact)

                Csla.Data.DataMapper.Map(adContact, adContactData, "FullName", "Advertiser", "Notes", "ToDos", "Projects")
                Csla.Data.DataMapper.Map(adContact.Advertiser, adContactData.Advertiser)

                Return adContactData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function AddAdContact(ByVal AdContactNewData As AdContactNewData) As AdContactData
            Dim adContactData As AdContactData = New AdContactData
            Dim adContact As Advertiser.ClsContact

            Try
                adContact = Advertiser.ClsContact.NewContact
                CollectAdContactData(AdContactNewData, adContact)
                SaveAdContact(ClsOperation.GetOperation("Insert"), AdContactNewData, adContact)

                Csla.Data.DataMapper.Map(adContact, adContactData, "FullName", "Advertiser", "Notes", "ToDos", "Projects")
                Csla.Data.DataMapper.Map(adContact.Advertiser, adContactData.Advertiser)

                Return adContactData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function EditAdContact(ByVal AdContactNewData As AdContactNewData) As AdContactData
            Dim adContactData As AdContactData = New AdContactData
            Dim adContact As Advertiser.ClsContact

            Try
                adContact = Advertiser.ClsContact.GetContact(AdContactNewData.ID.NewValue)
                CollectAdContactData(AdContactNewData, adContact)
                If adContact.IsDirty Then
                    SaveAdContact(ClsOperation.GetOperation("Update"), AdContactNewData, adContact)
                End If

                Csla.Data.DataMapper.Map(adContact, adContactData, "FullName", "Advertiser", "Notes", "ToDos", "Projects")
                Csla.Data.DataMapper.Map(adContact.Advertiser, adContactData.Advertiser)

                Return adContactData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Sub DeleteAdContact(ByVal AdContactNewData As AdContactNewData)
            Dim adContact As Advertiser.ClsContact

            Try
                adContact = Advertiser.ClsContact.GetContact(AdContactNewData.ID.NewValue)
                adContact.Delete()
                SaveAdContact(ClsOperation.GetOperation("Delete"), AdContactNewData, adContact)
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Sub

        Private Shared Sub CollectAdContactData(ByVal AdContactNewData As AdContactNewData, ByRef adContact As Advertiser.ClsContact)
            With adContact
                .AssignAdvertiserAccount(AdContactNewData.Advertiser.ID.NewValue)
                .FirstName = AdContactNewData.FirstName.NewValue
                .LastName = AdContactNewData.LastName.NewValue
                .PrimaryAddress = AdContactNewData.PrimaryAddress.NewValue
                .SecondaryAddress = AdContactNewData.SecondaryAddress.NewValue
                .MainCompanyAddress = AdContactNewData.MainCompanyAddress.NewValue
                .City = AdContactNewData.City.NewValue
                .State = AdContactNewData.State.NewValue
                .Country = AdContactNewData.Country.NewValue
                .ZipCode = AdContactNewData.ZipCode.NewValue
                .Providence = AdContactNewData.Providence.NewValue
                .Department = AdContactNewData.Department.NewValue
                .ResposibleForNotes = AdContactNewData.ResposibleForNotes.NewValue
            End With
        End Sub

        Private Shared Sub SaveAdContact(ByVal operation As ClsOperation, ByVal AdContactNewData As AdContactNewData, ByRef adContact As Advertiser.ClsContact)
            Try
                Using scope As New TransactionScope()
                    AdContactNewData.ID.NewValue = adContact.Save.ID
                    SaveBinnacle(operation, Date.Now, AdContactNewData, adContact)
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

#Region " Fields "

        Public Shared Function GetFieldInfoList() As FieldData()
            Return GetFieldInfoList(0, 0)
        End Function

        Public Shared Function GetFieldInfoList(ByVal idForm As Long, ByVal idGroup As Long) As FieldData()
            Dim fieldsInfoData As New List(Of FieldData)
            Dim fieldsInfo As ClsFieldInfoList

            Try
                fieldsInfo = ClsFieldInfoList.GetFieldInfoList(idForm, idGroup, String.Empty)

                For Each fieldInfo As ClsFieldInfo In fieldsInfo
                    Dim fieldInfoData As New FieldData

                    Csla.Data.DataMapper.Map(fieldInfo, fieldInfoData, "Group")

                    fieldsInfoData.Add(fieldInfoData)
                Next
                Return fieldsInfoData.ToArray
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function GetFieldList() As FieldData()
            Return GetFieldList(New Long() {}, New Long() {}, String.Empty)
        End Function

        Public Shared Function GetFieldList(ByVal description As String) As FieldData()
            Return GetFieldList(New Long() {}, New Long() {}, description)
        End Function

        Public Shared Function GetFieldList(ByVal idForm() As Long, ByVal idGroup() As Long) As FieldData()
            Return GetFieldList(idForm, idGroup, String.Empty)
        End Function

        Public Shared Function GetFieldList(ByVal idForm() As Long, ByVal idGroup() As Long, ByVal description As String) As FieldData()
            Dim fieldsData As New List(Of FieldData)
            Dim fields As ClsFieldInfoList

            Try
                fields = ClsFieldInfoList.GetFieldInfoList(idForm, idGroup, description)

                SaveBinnacle(ClsOperation.GetOperation("Select"), Date.Now)

                For Each field As ClsFieldInfo In fields
                    Dim fieldData As New FieldData

                    Csla.Data.DataMapper.Map(field, fieldData, "Group")
                    Csla.Data.DataMapper.Map(field.Group, fieldData.Group, "Form")
                    Csla.Data.DataMapper.Map(field.Group.Form, fieldData.Group.Form, "Profiles")

                    fieldsData.Add(fieldData)
                Next
                Return fieldsData.ToArray
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function GetField(ByVal formData As FieldNewData) As FieldData
            Dim fieldData As FieldData = New FieldData
            Dim field As ClsField

            Try
                field = ClsField.GetField(formData.ID.NewValue)
                SaveField(ClsOperation.GetOperation("Select"), formData, field)

                Csla.Data.DataMapper.Map(field, fieldData, "Group")
                Csla.Data.DataMapper.Map(field.Group, fieldData.Group, "Form")
                Csla.Data.DataMapper.Map(field.Group.Form, fieldData.Group.Form, "Profiles")

                Return fieldData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function AddField(ByVal formData As FieldNewData) As FieldData
            Dim fieldData As FieldData = New FieldData
            Dim field As ClsField

            Try
                field = ClsField.NewField
                CollectFieldData(formData, field)
                SaveField(ClsOperation.GetOperation("Insert"), formData, field)

                Csla.Data.DataMapper.Map(field, fieldData, "Group")
                Csla.Data.DataMapper.Map(field.Group, fieldData.Group, "Form")
                Csla.Data.DataMapper.Map(field.Group.Form, fieldData.Group.Form, "Profiles")

                Return fieldData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function EditField(ByVal formData As FieldNewData) As FieldData
            Dim fieldData As FieldData = New FieldData
            Dim field As ClsField
            Try
                field = ClsField.GetField(formData.ID.NewValue)
                CollectFieldData(formData, field)
                If field.IsDirty Then
                    SaveField(ClsOperation.GetOperation("Update"), formData, field)
                End If

                Csla.Data.DataMapper.Map(field, fieldData, "Group")
                Csla.Data.DataMapper.Map(field.Group, fieldData.Group, "Form")
                Csla.Data.DataMapper.Map(field.Group.Form, fieldData.Group.Form, "Profiles")

                Return fieldData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Sub DeleteField(ByVal formData As FieldNewData)
            Dim field As ClsField

            Try
                field = ClsField.GetField(formData.ID.NewValue)
                field.Delete()
                SaveField(ClsOperation.GetOperation("Delete"), formData, field)
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Sub

        Private Shared Sub CollectFieldData(ByVal formData As FieldNewData, ByRef field As ClsField)
            With field
                .AssignGroup(formData.Group.ID.NewValue)
                .Description = formData.Description.NewValue
            End With
        End Sub

        Private Shared Sub SaveField(ByVal operation As ClsOperation, ByVal formData As FieldNewData, ByRef field As ClsField)
            Try
                Using scope As New TransactionScope()
                    formData.ID.NewValue = field.Save.ID
                    SaveBinnacle(operation, Date.Now, formData, field)
                    scope.Complete()
                End Using
            Catch ValEx As Csla.Validation.ValidationException
                For Each BrokenRule As Csla.Validation.BrokenRule In field.BrokenRulesCollection
                    Throw New Exception(BrokenRule.Description)
                Next
                Throw New Exception(ValEx.Message)
            End Try
        End Sub

#End Region

#Region " Forms "

        Public Shared Function GetFormInfoList() As FormData()
            Return GetFormInfoList(String.Empty)
        End Function

        Public Shared Function GetFormInfoList(ByVal logDescription As String) As FormData()
            Dim formsInfoData As New List(Of FormData)
            Dim formsInfo As ClsFormInfoList

            Try
                formsInfo = ClsFormInfoList.GetFormInfoList(String.Empty, logDescription)

                For Each formInfo As ClsFormInfo In formsInfo
                    Dim formInfoData As New FormData

                    Csla.Data.DataMapper.Map(formInfo, formInfoData, "Profiles")

                    formsInfoData.Add(formInfoData)
                Next
                Return formsInfoData.ToArray
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function GetFormList() As FormData()
            Return GetFormList(String.Empty, New String() {})
        End Function

        Public Shared Function GetFormList(ByVal description As String) As FormData()
            Return GetFormList(description, New String() {})
        End Function

        Public Shared Function GetFormList(ByVal logDescription() As String) As FormData()
            Return GetFormList(String.Empty, logDescription)
        End Function

        Public Shared Function GetFormList(ByVal description As String, ByVal logDescription() As String) As FormData()
            Dim formsData As New List(Of FormData)
            Dim forms As ClsFormInfoList

            Try
                forms = ClsFormInfoList.GetFormInfoList(description, logDescription)

                SaveBinnacle(ClsOperation.GetOperation("Select"), Date.Now)

                For Each form As ClsFormInfo In forms
                    Dim formData As New FormData

                    Csla.Data.DataMapper.Map(form, formData, "Profiles")

                    formsData.Add(formData)
                Next
                Return formsData.ToArray
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function Getform(ByVal newFormData As FormNewData) As FormData
            Dim formData As FormData = New FormData
            Dim form As ClsForm

            Try
                form = ClsForm.GetForm(newFormData.ID.NewValue)
                SaveForm(ClsOperation.GetOperation("Select"), newFormData, form)

                Csla.Data.DataMapper.Map(form, formData, "Profiles", "Groups")

                Return formData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function Addform(ByVal newFormData As FormNewData) As FormData
            Dim formData As FormData = New FormData
            Dim form As ClsForm

            Try
                form = ClsForm.NewForm
                CollectFormData(newFormData, form)
                SaveForm(ClsOperation.GetOperation("Insert"), newFormData, form)

                Csla.Data.DataMapper.Map(form, formData, "Profiles", "Groups")

                Return formData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function Editform(ByVal newFormData As FormNewData) As FormData
            Dim formData As FormData = New FormData
            Dim form As ClsForm
            Try
                form = ClsForm.GetForm(newFormData.ID.NewValue)
                CollectFormData(newFormData, form)
                If form.IsDirty Then
                    SaveForm(ClsOperation.GetOperation("Update"), newFormData, form)
                End If

                Csla.Data.DataMapper.Map(form, formData, "Profiles", "Groups")

                Return formData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Sub DeleteForm(ByVal newFormData As FormNewData)
            Dim form As ClsForm

            Try
                form = ClsForm.GetForm(newFormData.ID.NewValue)
                form.Delete()
                SaveForm(ClsOperation.GetOperation("Delete"), newFormData, form)
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Sub

        Private Shared Sub CollectFormData(ByVal formData As FormNewData, ByRef form As ClsForm)
            With form
                .Description = formData.Description.NewValue
                .LogDescription = formData.LogDescription.NewValue
            End With
        End Sub

        Private Shared Sub SaveForm(ByVal operation As ClsOperation, ByVal formData As FormNewData, ByRef form As ClsForm)
            Try
                Using scope As New TransactionScope()
                    formData.ID.NewValue = form.Save.ID
                    SaveBinnacle(operation, Date.Now, formData, form)
                    scope.Complete()
                End Using
            Catch ValEx As Csla.Validation.ValidationException
                For Each BrokenRule As Csla.Validation.BrokenRule In form.BrokenRulesCollection
                    Throw New Exception(BrokenRule.Description)
                Next
                Throw New Exception(ValEx.Message)
            End Try
        End Sub

#End Region

#Region " Form Profiles "

        Public Shared Function GetFormProfileInfoList(ByVal formID As Long) As FormProfileData()
            Dim formProfilesInfoData As New List(Of FormProfileData)
            Dim formProfilesInfo As ClsFormProfileInfoList

            Try
                formProfilesInfo = ClsFormProfileInfoList.GetFormProfileInfoList(formID)

                For Each formProfileInfo As ClsFormProfileInfo In formProfilesInfo
                    Dim formProfileInfoData As New FormProfileData

                    Csla.Data.DataMapper.Map(formProfileInfo, formProfileInfoData, "Form")
                    Csla.Data.DataMapper.Map(formProfileInfo.Form, formProfileInfoData.Form)

                    formProfilesInfoData.Add(formProfileInfoData)
                Next
                Return formProfilesInfoData.ToArray
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function GetFormProfileList(ByVal idForm() As Long) As FormProfileData()
            Dim formProfilesData As New List(Of FormProfileData)
            Dim formProfiles As ClsFormProfileInfoList

            Try
                formProfiles = ClsFormProfileInfoList.GetFormProfileInfoList(idForm)

                SaveBinnacle(ClsOperation.GetOperation("Select"), Date.Now)

                For Each formProfile As ClsFormProfileInfo In formProfiles
                    Dim formProfileData As New FormProfileData

                    Csla.Data.DataMapper.Map(formProfile, formProfileData, "Form")
                    Csla.Data.DataMapper.Map(formProfile.Form, formProfileData.Form)

                    formProfilesData.Add(formProfileData)
                Next
                Return formProfilesData.ToArray
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function GetFormProfile(ByVal formData As FormProfileNewData) As FormProfileData
            Dim formProfileData As FormProfileData = New FormProfileData
            Dim formProfile As ClsFormProfile
            Dim form As ClsForm

            Try
                form = ClsForm.GetForm(formData.Form.ID.NewValue)
                formProfile = form.Profiles.GetItem(formData.ID.NewValue)

                If formProfile Is Nothing Then Throw New Exception("Profile no assigned to form")

                SaveBinnacle(ClsOperation.GetOperation("Select"), Date.Now, formData, formProfile)

                Csla.Data.DataMapper.Map(formProfile, formProfileData, "Form", "GroupProfiles")
                Csla.Data.DataMapper.Map(formProfile.Form, formProfileData.Form)

                Return formProfileData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function AddFormProfile(ByVal formData As FormProfileNewData) As FormProfileData
            Dim formProfileData As FormProfileData = New FormProfileData
            Dim formProfile As ClsFormProfile
            Dim form As ClsForm

            Try
                form = ClsForm.GetForm(formData.Form.ID.NewValue)
                form.Profiles.Add(formData.ID.NewValue, formData.Form.ID.NewValue, formData.PSelect.NewValue, formData.PInsert.NewValue, formData.PUpdate.NewValue, formData.PDelete.NewValue)

                SaveFormProfile("Insert", formData, form)

                formProfile = form.Profiles.GetItem(formData.ID.NewValue)
                Csla.Data.DataMapper.Map(formProfile, formProfileData, "Form", "GroupProfiles")
                Csla.Data.DataMapper.Map(formProfile.Form, formProfileData.Form)

                Return formProfileData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function EditFormProfile(ByVal formData As FormProfileNewData) As FormProfileData
            Dim formProfileData As FormProfileData = New FormProfileData
            Dim formProfile As ClsFormProfile
            Dim form As ClsForm

            Try
                form = ClsForm.GetForm(formData.Form.ID.NewValue)
                form.Profiles.Edit(formData.ID.NewValue, formData.PSelect.NewValue, formData.PInsert.NewValue, formData.PUpdate.NewValue, formData.PDelete.NewValue)

                If form.IsDirty Then SaveFormProfile("Update", formData, form)

                formProfile = form.Profiles.GetItem(formData.ID.NewValue)
                Csla.Data.DataMapper.Map(formProfile, formProfileData, "Form", "GroupProfiles")
                Csla.Data.DataMapper.Map(formProfile.Form, formProfileData.Form)

                Return formProfileData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Sub DeleteFormProfile(ByVal formData As FormProfileNewData)
            Dim form As ClsForm

            Try
                form = ClsForm.GetForm(formData.Form.ID.NewValue)
                form.Profiles.Remove(formData.ID.NewValue)

                SaveFormProfile(formData, form)
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Sub

        Private Shared Sub SaveFormProfile(ByVal operation As String, ByVal formData As FormProfileNewData, ByRef form As ClsForm)
            Try
                Using scope As New TransactionScope()
                    Dim formProfile As ClsFormProfile = form.Profiles.GetItem(formData.ID.NewValue)
                    form.Save()
                    SaveBinnacle(ClsOperation.GetOperation(operation), Date.Now, formData, formProfile)
                    scope.Complete()
                End Using
            Catch ValEx As Csla.Validation.ValidationException
                For Each BrokenRule As Csla.Validation.BrokenRule In form.Profiles.GetItem(formData.ID.NewValue).BrokenRulesCollection
                    Throw New Exception(BrokenRule.Description)
                Next
                Throw New Exception(ValEx.Message)
            End Try
        End Sub

        Private Shared Sub SaveFormProfile(ByVal formData As FormProfileNewData, ByRef form As ClsForm)
            Using scope As New TransactionScope()
                Dim formProfile As ClsFormProfile = form.Profiles.GetDeletedItem(formData.ID.NewValue)
                form.Save()
                SaveBinnacle(ClsOperation.GetOperation("Delete"), Date.Now, formData, formProfile)
                scope.Complete()
            End Using
        End Sub

#End Region

#Region " Groups "

        Public Shared Function GetGroupInfoList() As GroupData()
            Return GetGroupInfoList(0)
        End Function

        Public Shared Function GetGroupInfoList(ByVal idForm As Long) As GroupData()
            Dim groupsInfoData As New List(Of GroupData)
            Dim groupsInfo As ClsGroupInfoList

            Try
                groupsInfo = ClsGroupInfoList.GetGroupInfoList(idForm, String.Empty)

                For Each groupInfo As ClsGroupInfo In groupsInfo
                    Dim groupInfoData As New GroupData

                    Csla.Data.DataMapper.Map(groupInfo, groupInfoData, "Form")

                    groupsInfoData.Add(groupInfoData)
                Next
                Return groupsInfoData.ToArray
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function GetGroupList() As GroupData()
            Return GetGroupList(New Long() {}, String.Empty)
        End Function

        Public Shared Function GetGroupList(ByVal description As String) As GroupData()
            Return GetGroupList(New Long() {}, description)
        End Function

        Public Shared Function GetGroupList(ByVal idForm() As Long) As GroupData()
            Return GetGroupList(idForm, String.Empty)
        End Function

        Public Shared Function GetGroupList(ByVal idForm() As Long, ByVal description As String) As GroupData()
            Dim groupsData As New List(Of GroupData)
            Dim groups As ClsGroupInfoList

            Try
                groups = ClsGroupInfoList.GetGroupInfoList(idForm, description)

                SaveBinnacle(ClsOperation.GetOperation("Select"), Date.Now)

                For Each group As ClsGroupInfo In groups
                    Dim groupData As New GroupData

                    Csla.Data.DataMapper.Map(group, groupData, "Form")
                    Csla.Data.DataMapper.Map(group.Form, groupData.Form, "Profiles")

                    groupsData.Add(groupData)
                Next
                Return groupsData.ToArray
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function GetGroup(ByVal formData As GroupNewData) As GroupData
            Dim groupData As GroupData = New GroupData
            Dim group As ClsGroup

            Try
                group = ClsGroup.GetGroup(formData.ID.NewValue)
                SaveGroup(ClsOperation.GetOperation("Select"), formData, group)

                Csla.Data.DataMapper.Map(group, groupData, "Form", "Profiles", "Fields")
                Csla.Data.DataMapper.Map(group.Form, groupData.Form, "Profiles")

                Return groupData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function AddGroup(ByVal formData As GroupNewData) As GroupData
            Dim groupData As GroupData = New GroupData
            Dim group As ClsGroup

            Try
                group = ClsGroup.NewGroup
                CollectGroupData(formData, group)
                SaveGroup(ClsOperation.GetOperation("Insert"), formData, group)

                Csla.Data.DataMapper.Map(group, groupData, "Form", "Profiles", "Fields")
                Csla.Data.DataMapper.Map(group.Form, groupData.Form, "Profiles")

                Return groupData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function EditGroup(ByVal formData As GroupNewData) As GroupData
            Dim GroupData As GroupData = New GroupData
            Dim Group As ClsGroup
            Try
                Group = ClsGroup.GetGroup(formData.ID.NewValue)
                CollectGroupData(formData, Group)
                If Group.IsDirty Then
                    SaveGroup(ClsOperation.GetOperation("Update"), formData, Group)
                End If

                Csla.Data.DataMapper.Map(Group, GroupData, "Form", "Profiles", "Fields")
                Csla.Data.DataMapper.Map(Group.Form, GroupData.Form, "Profiles")

                Return GroupData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Sub DeleteGroup(ByVal formData As GroupNewData)
            Dim group As ClsGroup

            Try
                group = ClsGroup.GetGroup(formData.ID.NewValue)
                group.Delete()
                SaveGroup(ClsOperation.GetOperation("Delete"), formData, group)
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Sub

        Private Shared Sub CollectGroupData(ByVal formData As GroupNewData, ByRef group As ClsGroup)
            With group
                .Description = formData.Description.NewValue
                .AssignForm(formData.Form.ID.NewValue)
            End With
        End Sub

        Private Shared Sub SaveGroup(ByVal operation As ClsOperation, ByVal formData As GroupNewData, ByRef group As ClsGroup)
            Try
                Using scope As New TransactionScope()
                    formData.ID.NewValue = group.Save.ID
                    SaveBinnacle(operation, Date.Now, formData, group)
                    scope.Complete()
                End Using
            Catch ValEx As Csla.Validation.ValidationException
                For Each BrokenRule As Csla.Validation.BrokenRule In group.BrokenRulesCollection
                    Throw New Exception(BrokenRule.Description)
                Next
                Throw New Exception(ValEx.Message)
            End Try
        End Sub

#End Region

#Region " Group Profiles "

        Public Shared Function GetGroupProfileList(ByVal groupID() As Long, ByVal formID() As Long) As GroupProfileData()
            Dim groupProfilesData As New List(Of GroupProfileData)
            Dim groupProfiles As ClsGroupProfileInfoList

            Try
                groupProfiles = ClsGroupProfileInfoList.GetGroupProfileInfoList(groupID, formID)

                SaveBinnacle(ClsOperation.GetOperation("Select"), Date.Now)

                For Each groupProfile As ClsGroupProfileInfo In groupProfiles
                    Dim groupProfileData As New GroupProfileData

                    Csla.Data.DataMapper.Map(groupProfile, groupProfileData, "Group", "FormProfile")
                    Csla.Data.DataMapper.Map(groupProfile.Group, groupProfileData.Group, "Form")
                    Csla.Data.DataMapper.Map(groupProfile.Group.Form, groupProfileData.Group.Form)
                    Csla.Data.DataMapper.Map(groupProfile.FormProfile, groupProfileData.FormProfile, "Form")

                    groupProfilesData.Add(groupProfileData)
                Next
                Return groupProfilesData.ToArray
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function GetGroupProfile(ByVal formData As GroupProfileNewData) As GroupProfileData
            Dim groupProfileData As GroupProfileData = New GroupProfileData
            Dim groupProfile As ClsGroupProfile
            Dim group As ClsGroup

            Try
                group = ClsGroup.GetGroup(formData.Group.ID.NewValue)
                groupProfile = group.Profiles.GetItem(formData.ID.NewValue)

                If groupProfile Is Nothing Then Throw New Exception("Profile no assigned to form")

                SaveBinnacle(ClsOperation.GetOperation("Select"), Date.Now, formData, groupProfile)

                Csla.Data.DataMapper.Map(groupProfile, groupProfileData, "Group", "FormProfile")
                Csla.Data.DataMapper.Map(groupProfile.Group, groupProfileData.Group, "Form")
                Csla.Data.DataMapper.Map(groupProfile.Group.Form, groupProfileData.Group.Form)
                Csla.Data.DataMapper.Map(groupProfile.FormProfile, groupProfileData.FormProfile, "Form")

                Return groupProfileData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function AddGroupProfile(ByVal formData As GroupProfileNewData) As GroupProfileData
            Dim groupProfileData As GroupProfileData = New GroupProfileData
            Dim groupProfile As ClsGroupProfile
            Dim group As ClsGroup

            Try
                group = ClsGroup.GetGroup(formData.Group.ID.NewValue)
                group.Profiles.Add(formData.ID.NewValue, formData.Group.Form.ID.NewValue, formData.Group.ID.NewValue, formData.PSelect.NewValue, formData.PInsert.NewValue, formData.PUpdate.NewValue, formData.PDelete.NewValue)

                SaveGroupProfile("Insert", formData, group)

                groupProfile = group.Profiles.GetItem(formData.ID.NewValue)
                Csla.Data.DataMapper.Map(groupProfile, groupProfileData, "Group", "FormProfile")
                Csla.Data.DataMapper.Map(groupProfile.Group, groupProfileData.Group, "Form")
                Csla.Data.DataMapper.Map(groupProfile.Group.Form, groupProfileData.Group.Form)
                Csla.Data.DataMapper.Map(groupProfile.FormProfile, groupProfileData.FormProfile, "Form")

                Return groupProfileData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function EditGroupProfile(ByVal formData As GroupProfileNewData) As GroupProfileData
            Dim groupProfileData As GroupProfileData = New GroupProfileData
            Dim groupProfile As ClsGroupProfile
            Dim group As ClsGroup

            Try
                group = ClsGroup.GetGroup(formData.Group.ID.NewValue)
                group.Profiles.Edit(formData.ID.NewValue, formData.PSelect.NewValue, formData.PInsert.NewValue, formData.PUpdate.NewValue, formData.PDelete.NewValue)

                If group.IsDirty Then SaveGroupProfile("Update", formData, group)

                groupProfile = group.Profiles.GetItem(formData.ID.NewValue)
                Csla.Data.DataMapper.Map(groupProfile, groupProfileData, "Group", "FormProfile")
                Csla.Data.DataMapper.Map(groupProfile.Group, groupProfileData.Group, "Form")
                Csla.Data.DataMapper.Map(groupProfile.Group.Form, groupProfileData.Group.Form)
                Csla.Data.DataMapper.Map(groupProfile.FormProfile, groupProfileData.FormProfile, "Form")

                Return groupProfileData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Sub DeleteGroupProfile(ByVal formData As GroupProfileNewData)
            Dim group As ClsGroup

            Try
                group = ClsGroup.GetGroup(formData.Group.ID.NewValue)
                group.Profiles.Remove(formData.ID.NewValue)

                SaveGroupProfile(formData, group)
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Sub

        Private Shared Sub SaveGroupProfile(ByVal operation As String, ByVal formData As GroupProfileNewData, ByRef group As ClsGroup)
            Try
                Using scope As New TransactionScope()
                    Dim groupProfile As ClsGroupProfile = group.Profiles.GetItem(formData.ID.NewValue)
                    group.Save()
                    SaveBinnacle(ClsOperation.GetOperation(operation), Date.Now, formData, groupProfile)
                    scope.Complete()
                End Using
            Catch ValEx As Csla.Validation.ValidationException
                For Each BrokenRule As Csla.Validation.BrokenRule In group.Profiles.GetItem(formData.ID.NewValue).BrokenRulesCollection
                    Throw New Exception(BrokenRule.Description)
                Next
                Throw New Exception(ValEx.Message)
            End Try
        End Sub

        Private Shared Sub SaveGroupProfile(ByVal formData As GroupProfileNewData, ByRef group As ClsGroup)
            Using scope As New TransactionScope()
                Dim groupProfile As ClsGroupProfile = group.Profiles.GetDeletedItem(formData.ID.NewValue)
                group.Save()
                SaveBinnacle(ClsOperation.GetOperation("Delete"), Date.Now, formData, groupProfile)
                scope.Complete()
            End Using
        End Sub

#End Region

#Region " Invoices "

        Public Shared Function GetAdInvoice(ByVal newInvoiceData As AdInvoiceNewData) As AdInvoiceData
            Dim invoiceData As AdInvoiceData = New AdInvoiceData
            Dim invoice As Advertiser.Project.ClsInvoice

            Try
                invoice = Advertiser.Project.ClsInvoice.GetInvoice(newInvoiceData.ID.NewValue)

                SaveBinnacle(ClsOperation.GetOperation("Select"), Date.Now, newInvoiceData, invoice)

                Csla.Data.DataMapper.Map(invoice, invoiceData, "Project", "Details")
                Csla.Data.DataMapper.Map(invoice.Project, invoiceData.Project, "Advertiser", "Contact")
                Csla.Data.DataMapper.Map(invoice.Project.Advertiser, invoiceData.Project.Advertiser)

                For Each invoiceDetail As Advertiser.Project.Invoice.ClsDetail In invoice.Details
                    Dim invoiceDetailData As New AdInvoiceDetailData

                    Csla.Data.DataMapper.Map(invoiceDetail, invoiceDetailData, "Invoice")

                    invoiceData.DetailList.AddDetails(invoiceDetailData)
                    invoiceData.DetailList.AddTotalAmountDue(invoiceDetail.AmountDue)
                Next

                Return invoiceData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

#End Region

#Region " Notes "

        Public Shared Function GetAdNoteList(ByVal idAdvertisers() As Long, ByVal idContacts() As Long, ByVal fromDate As Date, ByVal toDate As Date) As AdNoteData()
            Dim adNotesData As New List(Of AdNoteData)
            Dim adNotes As Advertiser.ClsNoteInfoList

            Dim bDate As Date = Date.Now

            Try
                adNotes = Advertiser.ClsNoteInfoList.GetNoteInfoList(idAdvertisers, idContacts, fromDate, toDate)

                SaveBinnacle(ClsOperation.GetOperation("Select"), Date.Now)

                For Each adNote As Advertiser.ClsNoteInfo In adNotes
                    Dim adNoteData As New AdNoteData

                    Csla.Data.DataMapper.Map(adNote, adNoteData, "Contact")
                    Csla.Data.DataMapper.Map(adNote.Contact, adNoteData.Contact, "Advertiser", "FullName")
                    Csla.Data.DataMapper.Map(adNote.Contact.Advertiser, adNoteData.Contact.Advertiser)

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

            Try
                adNote = Advertiser.Contact.ClsNote.GetNote(adNoteNewData.ID.NewValue)
                SaveBinnacle(ClsOperation.GetOperation("Select"), Date.Now, adNoteNewData, adNote)

                Csla.Data.DataMapper.Map(adNote, adNoteData, "Contact")
                Csla.Data.DataMapper.Map(adNote.Contact, adNoteData.Contact, "Advertiser", "FullName")
                Csla.Data.DataMapper.Map(adNote.Contact.Advertiser, adNoteData.Contact.Advertiser)

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
                Csla.Data.DataMapper.Map(adNote.Contact.Advertiser, adNoteData.Contact.Advertiser)

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
                Csla.Data.DataMapper.Map(adNote.Contact.Advertiser, adNoteData.Contact.Advertiser)

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
            Try
                Using scope As New TransactionScope()
                    adNoteNewData.ID.NewValue = adNote.Save.ID
                    SaveBinnacle(operation, Date.Now, adNoteNewData, adNote)
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

#Region " Operations "

        Public Shared Function GetOpeationInfoList() As OperationData()
            Dim operationsInfoData As New List(Of OperationData)
            Dim operationsInfo As ClsOperationInfoList

            Try
                operationsInfo = ClsOperationInfoList.GetOperationInfoList

                For Each operationInfo As ClsOperationInfo In operationsInfo
                    Dim operationInfoData As New OperationData

                    Csla.Data.DataMapper.Map(operationInfo, operationInfoData)

                    operationsInfoData.Add(operationInfoData)
                Next
                Return operationsInfoData.ToArray
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

#End Region

#Region " Profiles "

        Public Shared Function GetProfileInfoList() As ProfileData()
            Dim profilesInfoData As New List(Of ProfileData)
            Dim profilesInfo As ClsProfileInfoList

            Try
                profilesInfo = ClsProfileInfoList.GetProfileInfoList()

                For Each profileInfo As ClsProfileInfo In profilesInfo
                    Dim profileInfoData As New ProfileData

                    Csla.Data.DataMapper.Map(profileInfo, profileInfoData)

                    profilesInfoData.Add(profileInfoData)
                Next
                Return profilesInfoData.ToArray
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function GetProfileList() As ProfileData()
            Return GetProfileList(String.Empty)
        End Function

        Public Shared Function GetProfileList(ByVal description As String) As ProfileData()
            Dim profilesData As New List(Of ProfileData)
            Dim profiles As ClsProfileInfoList

            Try
                profiles = ClsProfileInfoList.GetProfileInfoList(description)

                SaveBinnacle(ClsOperation.GetOperation("Select"), Date.Now)

                For Each profile As ClsProfileInfo In profiles
                    Dim profileData As New ProfileData

                    Csla.Data.DataMapper.Map(profile, profileData)

                    profilesData.Add(profileData)
                Next
                Return profilesData.ToArray
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function GetProfile(ByVal formData As ProfileNewData) As ProfileData
            Dim profileData As ProfileData = New ProfileData
            Dim profile As ClsProfile

            Try
                profile = ClsProfile.GetProfile(formData.ID.NewValue)
                SaveProfile(ClsOperation.GetOperation("Select"), formData, profile)

                Csla.Data.DataMapper.Map(profile, profileData, "Forms")

                Return profileData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function AddProfile(ByVal formData As ProfileNewData) As ProfileData
            Dim profileData As ProfileData = New ProfileData
            Dim profile As ClsProfile

            Try
                profile = ClsProfile.NewProfile
                CollectProfileData(formData, profile)
                SaveProfile(ClsOperation.GetOperation("Insert"), formData, profile)

                Csla.Data.DataMapper.Map(profile, profileData, "Forms")

                Return profileData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function EditProfile(ByVal formData As ProfileNewData) As ProfileData
            Dim profileData As ProfileData = New ProfileData
            Dim profile As ClsProfile
            Try
                profile = ClsProfile.GetProfile(formData.ID.NewValue)
                CollectProfileData(formData, profile)
                If profile.IsDirty Then
                    SaveProfile(ClsOperation.GetOperation("Update"), formData, profile)
                End If

                Csla.Data.DataMapper.Map(profile, profileData, "Forms")

                Return profileData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Sub DeleteProfile(ByVal formData As ProfileNewData)
            Dim profile As ClsProfile

            Try
                profile = ClsProfile.GetProfile(formData.ID.NewValue)
                profile.Delete()
                SaveProfile(ClsOperation.GetOperation("Delete"), formData, profile)
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Sub

        Private Shared Sub CollectProfileData(ByVal formData As ProfileNewData, ByRef profile As ClsProfile)
            With profile
                .Description = formData.Description.NewValue
            End With
        End Sub

        Private Shared Sub SaveProfile(ByVal operation As ClsOperation, ByVal formData As ProfileNewData, ByRef profile As ClsProfile)
            Try
                Using scope As New TransactionScope()
                    formData.ID.NewValue = profile.Save.ID
                    SaveBinnacle(operation, Date.Now, formData, profile)
                    scope.Complete()
                End Using
            Catch ValEx As Csla.Validation.ValidationException
                For Each BrokenRule As Csla.Validation.BrokenRule In profile.BrokenRulesCollection
                    Throw New Exception(BrokenRule.Description)
                Next
                Throw New Exception(ValEx.Message)
            End Try
        End Sub

#End Region

#Region " Profile Forms "

        Public Shared Function GetProfileFormInfoList(ByVal profileID As Long) As ProfileFormData()
            Dim profileFormsInfoData As New List(Of ProfileFormData)
            Dim profileFormsInfo As ClsProfileFormInfoList

            Try
                profileFormsInfo = ClsProfileFormInfoList.GetProfileFormInfoList(profileID)

                For Each profileFormInfo As ClsProfileFormInfo In profileFormsInfo
                    Dim profileFormInfoData As New ProfileFormData

                    Csla.Data.DataMapper.Map(profileFormInfo, profileFormInfoData, "Profile")
                    Csla.Data.DataMapper.Map(profileFormInfo.Profile, profileFormInfoData.Profile)

                    profileFormsInfoData.Add(profileFormInfoData)
                Next
                Return profileFormsInfoData.ToArray
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function GetProfileFormList(ByVal idProfile() As Long) As ProfileFormData()
            Dim profileFormsData As New List(Of ProfileFormData)
            Dim profileForms As ClsProfileFormInfoList

            Try
                profileForms = ClsProfileFormInfoList.GetProfileFormInfoList(idProfile)

                SaveBinnacle(ClsOperation.GetOperation("Select"), Date.Now)

                For Each profileForm As ClsProfileFormInfo In profileForms
                    Dim profileFormData As New ProfileFormData

                    Csla.Data.DataMapper.Map(profileForm, profileFormData, "Profile")
                    Csla.Data.DataMapper.Map(profileForm.Profile, profileFormData.Profile)

                    profileFormsData.Add(profileFormData)
                Next
                Return profileFormsData.ToArray
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function GetProfileForm(ByVal formData As ProfileFormNewData) As ProfileFormData
            Dim profileFormData As ProfileFormData = New ProfileFormData
            Dim profileForm As ClsProfileForm
            Dim profile As ClsProfile

            Try
                profile = ClsProfile.GetProfile(formData.Profile.ID.NewValue)
                profileForm = profile.Forms.GetItem(formData.ID.NewValue)

                If profileForm Is Nothing Then Throw New Exception("Form no assigned to profile")

                SaveBinnacle(ClsOperation.GetOperation("Select"), Date.Now, formData, profileForm)

                Csla.Data.DataMapper.Map(profileForm, profileFormData, "Profile", "Groups")
                Csla.Data.DataMapper.Map(profileForm.Profile, profileFormData.Profile)

                Return profileFormData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function AddProfileForm(ByVal formData As ProfileFormNewData) As ProfileFormData
            Dim profileFormData As ProfileFormData = New ProfileFormData
            Dim profileForm As ClsProfileForm
            Dim profile As ClsProfile

            Try
                profile = ClsProfile.GetProfile(formData.Profile.ID.NewValue)
                profile.Forms.Add(formData.ID.NewValue, formData.Profile.ID.NewValue, formData.PSelect.NewValue, formData.PInsert.NewValue, formData.PUpdate.NewValue, formData.PDelete.NewValue)

                SaveProfileForm("Insert", formData, profile)

                profileForm = profile.Forms.GetItem(formData.ID.NewValue)
                Csla.Data.DataMapper.Map(profileForm, profileFormData, "Profile", "Groups")
                Csla.Data.DataMapper.Map(profileForm.Profile, profileFormData.Profile)

                Return profileFormData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function EditProfileForm(ByVal formData As ProfileFormNewData) As ProfileFormData
            Dim profileFormData As ProfileFormData = New ProfileFormData
            Dim profileForm As ClsProfileForm
            Dim profile As ClsProfile

            Try
                profile = ClsProfile.GetProfile(formData.Profile.ID.NewValue)
                profile.Forms.Edit(formData.ID.NewValue, formData.PSelect.NewValue, formData.PInsert.NewValue, formData.PUpdate.NewValue, formData.PDelete.NewValue)

                If profile.IsDirty Then SaveProfileForm("Update", formData, profile)

                profileForm = profile.Forms.GetItem(formData.ID.NewValue)
                Csla.Data.DataMapper.Map(profileForm, profileFormData, "Profile", "Groups")
                Csla.Data.DataMapper.Map(profileForm.Profile, profileFormData.Profile)

                Return profileFormData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Sub DeleteProfileForm(ByVal formData As ProfileFormNewData)
            Dim profile As ClsProfile

            Try
                profile = ClsProfile.GetProfile(formData.Profile.ID.NewValue)
                profile.Forms.Remove(formData.ID.NewValue)

                SaveProfileForm(formData, profile)
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Sub

        Private Shared Sub SaveProfileForm(ByVal operation As String, ByVal formData As ProfileFormNewData, ByRef profile As ClsProfile)
            Try
                Using scope As New TransactionScope()
                    Dim profileForm As ClsProfileForm = profile.Forms.GetItem(formData.ID.NewValue)
                    profile.Save()
                    SaveBinnacle(ClsOperation.GetOperation(operation), Date.Now, formData, profileForm)
                    scope.Complete()
                End Using
            Catch ValEx As Csla.Validation.ValidationException
                For Each BrokenRule As Csla.Validation.BrokenRule In profile.Forms.GetItem(formData.ID.NewValue).BrokenRulesCollection
                    Throw New Exception(BrokenRule.Description)
                Next
                Throw New Exception(ValEx.Message)
            End Try
        End Sub

        Private Shared Sub SaveProfileForm(ByVal formData As ProfileFormNewData, ByRef profile As ClsProfile)
            Using scope As New TransactionScope()
                Dim profileForm As ClsProfileForm = profile.Forms.GetDeletedItem(formData.ID.NewValue)
                profile.Save()
                SaveBinnacle(ClsOperation.GetOperation("Delete"), Date.Now, formData, profileForm)
                scope.Complete()
            End Using
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

        Public Shared Function GetAdProjectList(ByVal idAdvertisers() As Long, ByVal idContacts() As Long) As AdProjectData()
            Dim adProjectsInfoData As New List(Of AdProjectData)
            Dim adProjectsInfo As Advertiser.ClsProjectInfoList

            Try
                adProjectsInfo = Advertiser.ClsProjectInfoList.GetProjectInfoList(idAdvertisers, idContacts)

                SaveBinnacle(ClsOperation.GetOperation("Select"), Date.Now)

                For Each adProjectInfo As Advertiser.ClsProjectInfo In adProjectsInfo
                    Dim adProjectInfoData As New AdProjectData

                    Csla.Data.DataMapper.Map(adProjectInfo, adProjectInfoData, "Advertiser", "Contact")
                    Csla.Data.DataMapper.Map(adProjectInfo.Advertiser, adProjectInfoData.Advertiser)
                    Csla.Data.DataMapper.Map(adProjectInfo.Contact, adProjectInfoData.Contact, "Advertiser", "FullName")

                    adProjectsInfoData.Add(adProjectInfoData)
                Next
                Return adProjectsInfoData.ToArray
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function GetAdProject(ByVal AdProjectNewData As AdProjectNewData) As AdProjectData
            Dim adProjectData As AdProjectData = New AdProjectData
            Dim adProject As Advertiser.ClsProject

            Try
                adProject = Advertiser.ClsProject.GetProject(AdProjectNewData.ID.NewValue)
                SaveBinnacle(ClsOperation.GetOperation("Select"), Date.Now, AdProjectNewData, adProject, New Advertiser.Project.ClsDemographic() {})
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
                    Dim adDemographics As New List(Of Advertiser.Project.ClsDemographic)

                    For Each adDemographic As Advertiser.Project.ClsDemographic In adProject.Demographics
                        If adDemographic.IsDirty Then
                            adDemographics.Add(adDemographic)
                        End If
                    Next

                    SaveAdProject(ClsOperation.GetOperation("Update"), adProjectNewData, adProject, adDemographics.ToArray)
                End If

                PopulateProjectData(adProject, adProjectData)

                Return adProjectData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Sub DeleteAdProject(ByVal AdProjectNewData As AdProjectNewData)
            Dim adProject As Advertiser.ClsProject

            Try
                adProject = Advertiser.ClsProject.GetProject(AdProjectNewData.ID.NewValue)
                adProject.Delete()
                SaveAdProject(ClsOperation.GetOperation("Delete"), AdProjectNewData, adProject, New List(Of Advertiser.Project.ClsDemographic)(adProject.Demographics).ToArray)
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Sub

        Private Shared Sub PopulateProjectData(ByVal adProject As Advertiser.ClsProject, ByRef adProjectData As AdProjectData)
            Csla.Data.DataMapper.Map(adProject, adProjectData, "AdvertiserAccount", "AdvertiserContact", "Prices", "AdHistories", "Demographics", "Invoices", "Receipts")
            Csla.Data.DataMapper.Map(adProject.AdvertiserContact, adProjectData.Contact, "Advertiser", "FullName")
            Csla.Data.DataMapper.Map(adProject.AdvertiserAccount, adProjectData.Advertiser)
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
                .AdCountDisplayed = adProjectNewData.AdCountDisplayed.NewValue
                .AdVerifiedDate = adProjectNewData.AdVerifiedDate.NewValue
                .AdOnlineDate = adProjectNewData.AdOnlineDate.NewValue
                .PromoCode = adProjectNewData.PromoCode.NewValue
                .ComissionCode = adProjectNewData.ComissionCode.NewValue

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

        Private Shared Sub SaveAdProject(ByVal operation As ClsOperation, ByVal AdProjectNewData As AdProjectNewData, ByRef adProject As Advertiser.ClsProject, ByVal adProjectDemographics As Advertiser.Project.ClsDemographic())
            Try
                Using scope As New TransactionScope()
                    AdProjectNewData.ID.NewValue = adProject.Save.ID
                    SaveBinnacle(operation, Date.Now, AdProjectNewData, adProject, adProjectDemographics)
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

#Region " Profile Groups "

        Public Shared Function GetProfileGroupList(ByVal idProfile() As Long, ByVal idForm() As Long) As ProfileGroupData()
            Dim profileGroupsData As New List(Of ProfileGroupData)
            Dim profileGroups As ClsProfileGroupInfoList

            Try
                profileGroups = ClsProfileGroupInfoList.GetProfileFormGroupInfoList(idProfile, idForm)

                SaveBinnacle(ClsOperation.GetOperation("Select"), Date.Now)

                For Each profileGroup As ClsProfileGroupInfo In profileGroups
                    Dim profileGroupData As New ProfileGroupData

                    Csla.Data.DataMapper.Map(profileGroup, profileGroupData, "Form")
                    Csla.Data.DataMapper.Map(profileGroup.Form, profileGroupData.Form, "Profile")
                    Csla.Data.DataMapper.Map(profileGroup.Form.Profile, profileGroupData.Form.Profile)

                    profileGroupsData.Add(profileGroupData)
                Next
                Return profileGroupsData.ToArray
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function GetProfileGroup(ByVal formData As ProfileGroupNewData) As ProfileGroupData
            Dim profileGroupData As ProfileGroupData = New ProfileGroupData
            Dim profileGroup As ClsProfileGroup
            Dim profileForm As ClsProfileForm
            Dim profile As ClsProfile

            Try
                profile = ClsProfile.GetProfile(formData.Form.Profile.ID.NewValue)

                profileForm = profile.Forms.GetItem(formData.Form.ID.NewValue)
                If profileForm Is Nothing Then Throw New Exception("Form no assigned to profile")

                profileGroup = profile.Forms.GetItem(formData.Form.ID.NewValue).Groups.GetItem(formData.ID.NewValue)
                If profileGroup Is Nothing Then Throw New Exception("Group no assigned to profile")

                SaveBinnacle(ClsOperation.GetOperation("Select"), Date.Now, formData, profileGroup)

                Csla.Data.DataMapper.Map(profileGroup, profileGroupData, "Form")
                Csla.Data.DataMapper.Map(profileGroup.Form, profileGroupData.Form, "Profile")
                Csla.Data.DataMapper.Map(profileGroup.Form.Profile, profileGroupData.Form.Profile)

                Return profileGroupData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function AddProfileGroup(ByVal formData As ProfileGroupNewData) As ProfileGroupData
            Dim profileGroupData As ProfileGroupData = New ProfileGroupData
            Dim profileGroup As ClsProfileGroup
            Dim profileForm As ClsProfileForm
            Dim profile As ClsProfile

            Try
                profile = ClsProfile.GetProfile(formData.Form.Profile.ID.NewValue)

                profileForm = profile.Forms.GetItem(formData.Form.ID.NewValue)
                If profileForm Is Nothing Then Throw New Exception("Form no assigned to profile")

                profileForm.Groups.Add(formData.ID.NewValue, formData.Form.ID.NewValue, formData.Form.Profile.ID.NewValue, formData.PSelect.NewValue, formData.PInsert.NewValue, formData.PUpdate.NewValue, formData.PDelete.NewValue)

                SaveProfileGroup("Insert", formData, profile)

                profileGroup = profileForm.Groups.GetItem(formData.ID.NewValue)
                Csla.Data.DataMapper.Map(profileGroup, profileGroupData, "Form")
                Csla.Data.DataMapper.Map(profileGroup.Form, profileGroupData.Form, "Profile")
                Csla.Data.DataMapper.Map(profileGroup.Form.Profile, profileGroupData.Form.Profile)

                Return profileGroupData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function EditProfileGroup(ByVal formData As ProfileGroupNewData) As ProfileGroupData
            Dim profileGroupData As ProfileGroupData = New ProfileGroupData
            Dim profileGroup As ClsProfileGroup
            Dim profileForm As ClsProfileForm
            Dim profile As ClsProfile

            Try
                profile = ClsProfile.GetProfile(formData.Form.Profile.ID.NewValue)

                profileForm = profile.Forms.GetItem(formData.Form.ID.NewValue)
                If profileForm Is Nothing Then Throw New Exception("Form no assigned to profile")

                profileForm.Groups.Edit(formData.ID.NewValue, formData.PSelect.NewValue, formData.PInsert.NewValue, formData.PUpdate.NewValue, formData.PDelete.NewValue)

                If profile.IsDirty Then SaveProfileGroup("Update", formData, profile)

                profileGroup = profileForm.Groups.GetItem(formData.ID.NewValue)
                Csla.Data.DataMapper.Map(profileGroup, profileGroupData, "Form")
                Csla.Data.DataMapper.Map(profileGroup.Form, profileGroupData.Form, "Profile")
                Csla.Data.DataMapper.Map(profileGroup.Form.Profile, profileGroupData.Form.Profile)

                Return profileGroupData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Sub DeleteProfileGroup(ByVal formData As ProfileGroupNewData)
            Dim profile As ClsProfile
            Dim profileForm As ClsProfileForm

            Try
                profile = ClsProfile.GetProfile(formData.Form.Profile.ID.NewValue)

                profileForm = profile.Forms.GetItem(formData.Form.ID.NewValue)
                If profileForm Is Nothing Then Throw New Exception("Form no assigned to profile")

                profileForm.Groups.Remove(formData.ID.NewValue)

                SaveProfileGroup(formData, profile)
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Sub

        Private Shared Sub SaveProfileGroup(ByVal operation As String, ByVal formData As ProfileGroupNewData, ByRef profile As ClsProfile)
            Try
                Using scope As New TransactionScope()
                    Dim profileGroup As ClsProfileGroup = profile.Forms.GetItem(formData.Form.ID.NewValue).Groups.GetItem(formData.ID.NewValue)
                    profile.Save()
                    SaveBinnacle(ClsOperation.GetOperation(operation), Date.Now, formData, profileGroup)
                    scope.Complete()
                End Using
            Catch ValEx As Csla.Validation.ValidationException
                For Each BrokenRule As Csla.Validation.BrokenRule In profile.Forms.GetItem(formData.Form.ID.NewValue).Groups.GetItem(formData.ID.NewValue).BrokenRulesCollection
                    Throw New Exception(BrokenRule.Description)
                Next
                Throw New Exception(ValEx.Message)
            End Try
        End Sub

        Private Shared Sub SaveProfileGroup(ByVal formData As ProfileGroupNewData, ByRef profile As ClsProfile)
            Using scope As New TransactionScope()
                Dim profileGroup As ClsProfileGroup = profile.Forms.GetItem(formData.Form.ID.NewValue).Groups.GetDeletedItem(formData.ID.NewValue)
                profile.Save()
                SaveBinnacle(ClsOperation.GetOperation("Delete"), Date.Now, formData, profileGroup)
                scope.Complete()
            End Using
        End Sub

#End Region

#Region " Receipts "

        Public Shared Function GetAdReceipt(ByVal newReceiptData As AdReceiptNewData) As AdReceiptData
            Dim receiptData As AdReceiptData = New AdReceiptData
            Dim receipt As Advertiser.ClsReceipt

            Try
                receipt = Advertiser.ClsReceipt.GetReceipt(newReceiptData.ID.NewValue)

                SaveBinnacle(ClsOperation.GetOperation("Select"), Date.Now, newReceiptData, receipt)

                Csla.Data.DataMapper.Map(receipt, receiptData, "Projects")

                For Each project As Advertiser.Receipt.ClsProject In receipt.Projects
                    Dim projectData As New AdReceiptProjectData

                    Csla.Data.DataMapper.Map(project, projectData, "AdvertiserAccount", "Contact", "Receipt")
                    Csla.Data.DataMapper.Map(project.AdvertiserAccount, projectData.Advertiser)

                    receiptData.ProjectList.AddProjects(projectData)
                    receiptData.ProjectList.AddTotalPaidByDisplay(project.PaidByDisplay)
                    receiptData.ProjectList.AddTotalPaidByClickThrough(project.PaidByClickThrough)
                    receiptData.ProjectList.AddTotalPaid(project.TotalPaid)
                Next

                Return receiptData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

#End Region

#Region " SignIn "

        Public Shared Function Login(ByVal userName As String, ByVal password As String) As Boolean
            Try
                If Security.ClsSCTUserPrincipal.Login(userName, password) Then
                    SetUserSessionValues(Csla.ApplicationContext.User, "frmSignIn")
                    SaveBinnacle(ClsOperation.GetOperation("SignIn"), Date.Now)
                    FormsAuthentication.SetAuthCookie(userName, True)
                    Return True
                Else
                    Return False
                End If
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Sub Logout()
            Security.ClsSCTUserPrincipal.Logout()
            ClearUserSessionValues(Csla.ApplicationContext.User)
            FormsAuthentication.SignOut()
            FormsAuthentication.RedirectToLoginPage()
        End Sub

#End Region

#Region " State of Account "

        Public Shared Function GetAdStateOfAccount(ByVal idAdvertisers() As Long, ByVal idProjects() As Long, ByVal fromDate As Date, ByVal toDate As Date) As AdStateOfAccountData
            Dim transactionsData As New AdStateOfAccountData
            Dim transactionInvoices As Advertiser.ClsTransactionInvoiceInfoList = Advertiser.ClsTransactionInvoiceInfoList.NewTransactionInvoiceInfoList
            Dim transactionReceipts As Advertiser.ClsTransactionReceiptInfoList = Advertiser.ClsTransactionReceiptInfoList.NewTransactionReceiptInfoList

            Try
                transactionInvoices = Advertiser.ClsTransactionInvoiceInfoList.GetTransactionInvoiceInfoList(idAdvertisers, idProjects, Date.MinValue, toDate)
                transactionReceipts = Advertiser.ClsTransactionReceiptInfoList.GetTransactionReceiptInfoList(idAdvertisers, idProjects, Date.MinValue, toDate)

                SaveBinnacle(ClsOperation.GetOperation("Select"), Date.Now)

                For Each transactionInvoice As Advertiser.ClsTransactionInvoiceInfo In transactionInvoices
                    If transactionInvoice.TransactionDate < fromDate Then
                        transactionsData.AddStartBalance(transactionInvoice.TransactionAmount)
                    Else
                        Dim transactionData As New AdTransactionData

                        Csla.Data.DataMapper.Map(transactionInvoice, transactionData, "Advertiser", "Project")
                        Csla.Data.DataMapper.Map(transactionInvoice.Project, transactionData.Project, "Advertiser", "Contact")
                        Csla.Data.DataMapper.Map(transactionInvoice.Advertiser, transactionData.Advertiser)

                        transactionsData.TransactionInvoices.AddTransactionInvoices(transactionData)
                        transactionsData.TransactionInvoices.AddTransactionInvoicesTotal(transactionInvoice.TransactionAmount)
                    End If
                Next

                For Each transactionReceipt As Advertiser.ClsTransactionReceiptInfo In transactionReceipts
                    If transactionReceipt.TransactionDate < fromDate Then
                        transactionsData.SubtractStartBalance(transactionReceipt.TransactionAmount)
                    Else
                        Dim transactionData As New AdTransactionData

                        Csla.Data.DataMapper.Map(transactionReceipt, transactionData, "Advertiser", "Project")
                        Csla.Data.DataMapper.Map(transactionReceipt.Project, transactionData.Project, "Advertiser", "Contact")
                        Csla.Data.DataMapper.Map(transactionReceipt.Advertiser, transactionData.Advertiser)

                        transactionsData.TransactionReceipts.AddTransactionReceipts(transactionData)
                        transactionsData.TransactionReceipts.AddTransactionReceiptsTotal(transactionReceipt.TransactionAmount)
                    End If
                Next
                transactionsData.EndBalance = transactionsData.StartBalance + transactionsData.TransactionInvoices.TransactionInvoicesTotal - transactionsData.TransactionReceipts.TransactionReceiptsTotal
                Return transactionsData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

#End Region

#Region " To Dos "

        Public Shared Function GetAdToDoList(ByVal idAdvertisers() As Long, ByVal idContacts() As Long, ByVal fromDate As Date, ByVal toDate As Date) As AdToDoData()
            Dim adToDosData As New List(Of AdToDoData)
            Dim adToDos As Advertiser.ClsToDoInfoList

            Try
                adToDos = Advertiser.ClsToDoInfoList.GetToDoInfoList(idAdvertisers, idContacts, fromDate, toDate)

                SaveBinnacle(ClsOperation.GetOperation("Select"), Date.Now)

                For Each adToDo As Advertiser.ClsToDoInfo In adToDos
                    Dim adToDoData As New AdToDoData

                    Csla.Data.DataMapper.Map(adToDo, adToDoData, "Contact")
                    Csla.Data.DataMapper.Map(adToDo.Contact, adToDoData.Contact, "Advertiser", "FullName")
                    Csla.Data.DataMapper.Map(adToDo.Contact.Advertiser, adToDoData.Contact.Advertiser)

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

            Try
                adToDo = Advertiser.Contact.ClsToDo.GetToDo(adToDoNewData.ID.NewValue)
                SaveBinnacle(ClsOperation.GetOperation("Select"), Date.Now, adToDoNewData, adToDo)

                Csla.Data.DataMapper.Map(adToDo, adToDoData, "Contact")
                Csla.Data.DataMapper.Map(adToDo.Contact, adToDoData.Contact, "Advertiser", "FullName")
                Csla.Data.DataMapper.Map(adToDo.Contact.Advertiser, adToDoData.Contact.Advertiser)

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
                Csla.Data.DataMapper.Map(adToDo.Contact.Advertiser, adToDoData.Contact.Advertiser)

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
                Csla.Data.DataMapper.Map(adToDo.Contact.Advertiser, adToDoData.Contact.Advertiser)

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
            Try
                Using scope As New TransactionScope()
                    adToDoNewData.ID.NewValue = adToDo.Save.ID
                    SaveBinnacle(operation, Date.Now, adToDoNewData, adToDo)
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

#Region " Transactions "

        Public Shared Function GetAdTransactionList(ByVal transactions() As DataAccess.Transactions, ByVal idAdvertisers() As Long, ByVal idProjects() As Long, ByVal fromDate As Date, ByVal toDate As Date) As AdTransactionListData
            Dim transactionsData As New AdTransactionListData
            Dim transactionInvoices As Advertiser.ClsTransactionInvoiceInfoList = Advertiser.ClsTransactionInvoiceInfoList.NewTransactionInvoiceInfoList
            Dim transactionReceipts As Advertiser.ClsTransactionReceiptInfoList = Advertiser.ClsTransactionReceiptInfoList.NewTransactionReceiptInfoList

            Try
                If Array.Exists(transactions, AddressOf AllTransactionOption) Then
                    transactionInvoices = Advertiser.ClsTransactionInvoiceInfoList.GetTransactionInvoiceInfoList(idAdvertisers, idProjects, fromDate, toDate)
                    transactionReceipts = Advertiser.ClsTransactionReceiptInfoList.GetTransactionReceiptInfoList(idAdvertisers, idProjects, fromDate, toDate)
                Else
                    For Each transaction As DataAccess.Transactions In transactions
                        Select Case transaction
                            Case DataAccess.Transactions.Invoice
                                transactionInvoices = Advertiser.ClsTransactionInvoiceInfoList.GetTransactionInvoiceInfoList(idAdvertisers, idProjects, fromDate, toDate)
                            Case DataAccess.Transactions.Receipt
                                transactionReceipts = Advertiser.ClsTransactionReceiptInfoList.GetTransactionReceiptInfoList(idAdvertisers, idProjects, fromDate, toDate)
                            Case Else
                        End Select
                    Next
                End If

                SaveBinnacle(ClsOperation.GetOperation("Select"), Date.Now)

                For Each transactionInvoice As Advertiser.ClsTransactionInvoiceInfo In transactionInvoices
                    Dim transactionData As New AdTransactionData

                    Csla.Data.DataMapper.Map(transactionInvoice, transactionData, "Advertiser", "Project")
                    Csla.Data.DataMapper.Map(transactionInvoice.Project, transactionData.Project, "Advertiser", "Contact")
                    Csla.Data.DataMapper.Map(transactionInvoice.Advertiser, transactionData.Advertiser)

                    transactionsData.TransactionInvoices.AddTransactionInvoices(transactionData)
                    transactionsData.TransactionInvoices.AddTransactionInvoicesTotal(transactionInvoice.TransactionAmount)
                Next

                For Each transactionReceipt As Advertiser.ClsTransactionReceiptInfo In transactionReceipts
                    Dim transactionData As New AdTransactionData

                    Csla.Data.DataMapper.Map(transactionReceipt, transactionData, "Advertiser", "Project")
                    Csla.Data.DataMapper.Map(transactionReceipt.Project, transactionData.Project, "Advertiser", "Contact")
                    Csla.Data.DataMapper.Map(transactionReceipt.Advertiser, transactionData.Advertiser)

                    transactionsData.TransactionReceipts.AddTransactionReceipts(transactionData)
                    transactionsData.TransactionReceipts.AddTransactionReceiptsTotal(transactionReceipt.TransactionAmount)
                Next

                Return transactionsData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Private Shared Function AllTransactionOption(ByVal value As Transactions) As Boolean
            Return value = 0
        End Function


#End Region

#Region " Users "

        Public Shared Function GetUserInfoList() As UserData()
            Dim usersInfoData As New List(Of UserData)
            Dim usersInfo As ClsUserInfoList

            Try
                usersInfo = ClsUserInfoList.GetUserInfoList

                For Each userInfo As ClsUserInfo In usersInfo
                    Dim userInfoData As New UserData

                    Csla.Data.DataMapper.Map(userInfo, userInfoData, "FullName", "Profile")

                    usersInfoData.Add(userInfoData)
                Next
                Return usersInfoData.ToArray
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function GetUserList() As UserData()
            Return GetUserList(New Long() {}, String.Empty)
        End Function

        Public Shared Function GetUserList(ByVal idProfile() As Long) As UserData()
            Return GetUserList(idProfile, String.Empty)
        End Function

        Public Shared Function GetUserList(ByVal fullName As String) As UserData()
            Return GetUserList(New Long() {}, fullName)
        End Function

        Public Shared Function GetUserList(ByVal idProfile() As Long, ByVal fullName As String) As UserData()
            Dim usersData As New List(Of UserData)
            Dim users As ClsUserInfoList

            Try
                users = ClsUserInfoList.GetUserInfoList(idProfile, fullName)

                SaveBinnacle(ClsOperation.GetOperation("Select"), Date.Now)

                For Each user As ClsUserInfo In users
                    Dim userData As New UserData

                    Csla.Data.DataMapper.Map(user, userData, "FullName", "Profile")
                    Csla.Data.DataMapper.Map(user.Profile, userData.Profile)

                    usersData.Add(userData)
                Next
                Return usersData.ToArray
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function GetUser(ByVal formData As UserNewData) As UserData
            Dim userData As UserData = New UserData
            Dim user As ClsUser

            Try
                user = ClsUser.GetUser(formData.ID.NewValue)
                SaveUser(ClsOperation.GetOperation("Select"), formData, user)

                Csla.Data.DataMapper.Map(user, userData, "FullName", "Profile")
                Csla.Data.DataMapper.Map(user.Profile, userData.Profile)

                Return userData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function AddUser(ByVal formData As UserNewData) As UserData
            Dim userData As UserData = New UserData
            Dim user As ClsUser

            Try
                user = ClsUser.NewUser
                CollectUserData(formData, user)
                SaveUser(ClsOperation.GetOperation("Insert"), formData, user)

                Csla.Data.DataMapper.Map(user, userData, "FullName", "Profile")
                Csla.Data.DataMapper.Map(user.Profile, userData.Profile)

                Return userData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Function EditUser(ByVal formData As UserNewData) As UserData
            Dim userData As UserData = New UserData
            Dim user As ClsUser
            Try
                user = ClsUser.GetUser(formData.ID.NewValue)
                CollectUserData(formData, user)
                If user.IsDirty Then
                    SaveUser(ClsOperation.GetOperation("Update"), formData, user)
                End If

                Csla.Data.DataMapper.Map(user, userData, "FullName", "Profile")
                Csla.Data.DataMapper.Map(user.Profile, userData.Profile)

                Return userData
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Function

        Public Shared Sub DeleteUser(ByVal formData As UserNewData)
            Dim user As ClsUser

            Try
                user = ClsUser.GetUser(formData.ID.NewValue)
                user.Delete()
                SaveUser(ClsOperation.GetOperation("Delete"), formData, user)
            Catch CslaEx As Csla.DataPortalException
                Throw New Exception(CslaEx.BusinessException.Message)
            End Try
        End Sub

        Private Shared Sub CollectUserData(ByVal formData As UserNewData, ByRef user As ClsUser)
            With user
                .FirstName = formData.FirstName.NewValue
                .LastName = formData.LastName.NewValue
                .Login = formData.Login.NewValue
                .Password = formData.Password.NewValue
                .AssignProfile(formData.Profile.ID.NewValue)
            End With
        End Sub

        Private Shared Sub SaveUser(ByVal operation As ClsOperation, ByVal formData As UserNewData, ByRef user As ClsUser)
            Try
                Using scope As New TransactionScope()
                    formData.ID.NewValue = user.Save.ID
                    SaveBinnacle(operation, Date.Now, formData, user)
                    scope.Complete()
                End Using
            Catch ValEx As Csla.Validation.ValidationException
                For Each BrokenRule As Csla.Validation.BrokenRule In user.BrokenRulesCollection
                    Throw New Exception(BrokenRule.Description)
                Next
                Throw New Exception(ValEx.Message)
            End Try
        End Sub

#End Region

    End Class
End Namespace