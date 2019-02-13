Imports Csla
Imports SCT.DataAccess
Imports SCT.Library.Advertiser.Project.Invoice

Namespace Advertiser.Project
    <Serializable()> Public Class ClsInvoice
        Inherits BusinessBase(Of ClsInvoice)

#Region " Business Methods "

        Private mID As Long
        Private mProject As ClsProjectInfo = ClsProjectInfo.NewProjectInfo
        Private mInvoiceNumber As Decimal = 0
        Private mInvoiceSequence As Decimal = 0
        Private mInvoiceDate As Date = New Date(1900, 1, 1)
        Private mPaidToDate As Date = New Date(1900, 1, 1)
        Private mChargedToDate As Date = New Date(1900, 1, 1)
        Private mPreviousBalance As Double = 0
        Private mPreviousNumberOfClickThrough As Integer = 0
        Private mPreviousNumberOfDisplays As Integer = 0
        Private mTotalAmountDue As Double = 0
        Private mDetails As ClsDetailList = ClsDetailList.NewInvoiceDetailList

        Public ReadOnly Property ID() As Long
            Get
                CanReadProperty(True)
                Return Me.mID
            End Get
        End Property

        Public Property Project() As ClsProjectInfo
            Get
                CanReadProperty(True)
                Return Me.mProject
            End Get
            Set(ByVal value As ClsProjectInfo)
                CanWriteProperty(True)
                If value IsNot Nothing AndAlso value.ID <> 0 Then
                    If Me.mProject.ID <> value.ID Then
                        Me.mProject = value
                        PropertyHasChanged()
                    End If
                Else
                    Throw New System.Security.SecurityException("Project required.")
                End If
            End Set
        End Property

        Public Property InvoiceNumber() As Decimal
            Get
                CanReadProperty(True)
                Return Me.mInvoiceNumber
            End Get
            Set(ByVal value As Decimal)
                CanWriteProperty(True)
                If Me.mInvoiceNumber <> value Then
                    Me.mInvoiceNumber = value
                    ValidationRules.CheckRules("InvoiceNumber")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property InvoiceSequence() As Decimal
            Get
                CanReadProperty(True)
                Return Me.mInvoiceSequence
            End Get
            Set(ByVal value As Decimal)
                CanWriteProperty(True)
                If Me.mInvoiceSequence <> value Then
                    Me.mInvoiceSequence = value
                    ValidationRules.CheckRules("InvoiceSequence")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property InvoiceDate() As DateTime
            Get
                CanReadProperty(True)
                Return Me.mInvoiceDate
            End Get
            Set(ByVal value As DateTime)
                CanWriteProperty(True)
                If Me.mInvoiceDate <> value Then
                    Me.mInvoiceDate = value
                    ValidationRules.CheckRules("InvoiceDate")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property PaidToDate() As DateTime
            Get
                CanReadProperty(True)
                Return Me.mPaidToDate
            End Get
            Set(ByVal value As DateTime)
                CanWriteProperty(True)
                If Me.mPaidToDate <> value Then
                    Me.mPaidToDate = value
                    ValidationRules.CheckRules("PaidToDate")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property ChargedToDate() As DateTime
            Get
                CanReadProperty(True)
                Return Me.mChargedToDate
            End Get
            Set(ByVal value As DateTime)
                CanWriteProperty(True)
                If Me.mChargedToDate <> value Then
                    Me.mChargedToDate = value
                    ValidationRules.CheckRules("ChargedToDate")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property PreviousBalance() As Double
            Get
                CanReadProperty(True)
                Return Me.mPreviousBalance
            End Get
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If Me.mPreviousBalance <> value Then
                    Me.mPreviousBalance = value
                    ValidationRules.CheckRules("PreviousBalance")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property PreviousNumberOfClickThrough() As Integer
            Get
                CanReadProperty(True)
                Return Me.mPreviousNumberOfClickThrough
            End Get
            Set(ByVal value As Integer)
                CanWriteProperty(True)
                If Me.mPreviousNumberOfClickThrough <> value Then
                    Me.mPreviousNumberOfClickThrough = value
                    ValidationRules.CheckRules("PreviousNumberOfClickThrough")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property PreviousNumberOfDisplays() As Integer
            Get
                CanReadProperty(True)
                Return Me.mPreviousNumberOfDisplays
            End Get
            Set(ByVal value As Integer)
                CanWriteProperty(True)
                If Me.mPreviousNumberOfDisplays <> value Then
                    Me.mPreviousNumberOfDisplays = value
                    ValidationRules.CheckRules("PreviousNumberOfDisplays")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property TotalAmountDue() As Double
            Get
                CanReadProperty(True)
                Return Me.mTotalAmountDue
            End Get
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If Me.mTotalAmountDue <> value Then
                    Me.mTotalAmountDue = value
                    ValidationRules.CheckRules("TotalAmountDue")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public ReadOnly Property Details() As ClsDetailList
            Get
                CanReadProperty(True)
                Return Me.mDetails
            End Get
        End Property

        Public Overrides ReadOnly Property IsValid() As Boolean
            Get
                Return MyBase.IsValid AndAlso Me.mDetails.IsValid
            End Get
        End Property

        Public Overrides ReadOnly Property IsDirty() As Boolean
            Get
                Return MyBase.IsDirty OrElse Me.mDetails.IsDirty
            End Get
        End Property

        Public Sub AssignProject(ByVal ProjectId As Long)
            If ProjectId <> 0 Then
                If Me.mProject.ID <> ProjectId Then
                    Me.mProject = ClsProjectInfo.GetProjectInfo(ProjectId)
                    PropertyHasChanged("Project")
                End If
            Else
                Throw New System.Security.SecurityException("Project required.")
            End If
        End Sub

        Protected Overrides Function GetIdValue() As Object
            Return Me.mID
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()
            ValidationRules.AddRule(AddressOf ProjectRequired, "Project")

            ValidationRules.AddRule(AddressOf Validation.CommonRules.IntegerMinValue, New Validation.CommonRules.IntegerMinValueRuleArgs("PreviousNumberOfClickThrough", 0))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.IntegerMinValue, New Validation.CommonRules.IntegerMinValueRuleArgs("PreviousNumberOfDisplays", 0))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.MinValue(Of Decimal), New Validation.CommonRules.MinValueRuleArgs(Of Decimal)("InvoiceNumber", 0))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.MinValue(Of Decimal), New Validation.CommonRules.MinValueRuleArgs(Of Decimal)("InvoiceSequence", 0))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.MinValue(Of Double), New Validation.CommonRules.MinValueRuleArgs(Of Double)("PreviousBalance", 0))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.MinValue(Of Double), New Validation.CommonRules.MinValueRuleArgs(Of Double)("TotalAmountDue", 0))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.MinValue(Of Date), New Validation.CommonRules.MinValueRuleArgs(Of Date)("InvoiceDate", New Date(1900, 1, 1)))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.MinValue(Of Date), New Validation.CommonRules.MinValueRuleArgs(Of Date)("PaidToDate", New Date(1900, 1, 1)))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.MinValue(Of Date), New Validation.CommonRules.MinValueRuleArgs(Of Date)("ChargedToDate", New Date(1900, 1, 1)))
        End Sub

        Private Function ProjectRequired(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
            If Me.mProject.ID = 0 Then
                e.Description = "Project required."
                Return False
            Else
                Return True
            End If
        End Function
#End Region

#Region " Authorization Rules "

        'Protected Overrides Sub AddAuthorizationRules()
        '    AuthorizationRules.AllowRead("ID", " ")
        '    AuthorizationRules.AllowRead("Project", " ")
        '    AuthorizationRules.AllowRead("InvoiceNumber", " ")
        '    AuthorizationRules.AllowRead("InvoiceSequence", " ")
        '    AuthorizationRules.AllowRead("InvoiceDate", " ")
        '    AuthorizationRules.AllowRead("PaidToDate", " ")
        '    AuthorizationRules.AllowRead("ChargedToDate", " ")
        '    AuthorizationRules.AllowRead("PreviousBalance", " ")
        '    AuthorizationRules.AllowRead("PreviousNumberOfClickThrough", " ")
        '    AuthorizationRules.AllowRead("PreviousNumberOfDisplays", " ")
        '    AuthorizationRules.AllowRead("TotalAmountDue", " ")
        '    AuthorizationRules.AllowRead("Details", "")
        '    AuthorizationRules.AllowWrite("Project", New String() {" ", " "})
        '    AuthorizationRules.AllowWrite("InvoiceNumber", New String() {" ", " "})
        '    AuthorizationRules.AllowWrite("InvoiceSequence", New String() {" ", " "})
        '    AuthorizationRules.AllowWrite("InvoiceDate", New String() {" ", " "})
        '    AuthorizationRules.AllowWrite("PaidToDate", New String() {" ", " "})
        '    AuthorizationRules.AllowWrite("ChargedToDate", New String() {" ", " "})
        '    AuthorizationRules.AllowWrite("PreviousBalance", New String() {" ", " "})
        '    AuthorizationRules.AllowWrite("PreviousNumberOfClickThrough", New String() {" ", " "})
        '    AuthorizationRules.AllowWrite("PreviousNumberOfDisplays", New String() {" ", " "})
        '    AuthorizationRules.AllowWrite("TotalAmountDue", New String() {" ", " "})
        '    AuthorizationRules.AllowWrite("Details", New String() {" ", " "})
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

        Public Shared Function NewInvoice() As ClsInvoice
            If Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add Invoice records.")
            End If
            Return DataPortal.Create(Of ClsInvoice)(New Criteria(0))
        End Function

        Public Shared Function GetInvoice(ByVal ID As Long) As ClsInvoice
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view Invoice records.")
            End If
            Return DataPortal.Fetch(Of ClsInvoice)(New Criteria(ID))
        End Function

        Public Shared Sub DeleteInvoice(ByVal ID As Long)
            If Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove Invoice records.")
            End If
            DataPortal.Delete(New Criteria(ID))
        End Sub

        Public Overrides Function Save() As ClsInvoice
            If IsDeleted AndAlso Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove Invoice records.")

            ElseIf IsNew AndAlso Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add Invoice records.")

            ElseIf Not CanEditObject() Then
                Throw New System.Security.SecurityException("User not authorized to update Invoice records.")
            End If
            Return MyBase.Save
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

#End Region

#Region " Child Methods "

        Friend Shared Function NewChildInvoice() As ClsInvoice
            Dim Child As New ClsInvoice
            Child.ValidationRules.CheckRules("Project")
            Child.MarkAsChild()
            Return Child
        End Function

        Friend Shared Function GetChildInvoice(ByVal Invoice As DAClsappAdvertiserProjectInvoices.Struct) As ClsInvoice
            Return New ClsInvoice(Invoice)
        End Function

        Friend Shared Function NewProjectInvoice() As ClsInvoice
            Dim Child As New ClsInvoice
            Child.MarkAsChild()
            Return Child
        End Function

        Friend Shared Function GetProjectInvoice(ByVal Invoice As DAClsappAdvertiserProjectInvoices.Struct) As ClsInvoice
            Return New ClsInvoice(Invoice)
        End Function

        Private Sub New(ByVal Invoice As DAClsappAdvertiserProjectInvoices.Struct)
            MarkAsChild()
            Fetch(Invoice)
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
            Me.ValidationRules.CheckRules("Project")
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            Dim List As DAClsappAdvertiserProjectInvoices.Struct() = DAClsappAdvertiserProjectInvoices.Fetch(criteria.ID)
            If List.Length = 0 Then Throw New System.Security.SecurityException("Invoice record doesn't exists")

            Me.mStruct = List(0)
            Me.LoadObjectData()
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Insert()
            Me.LoadTableStruct(Me.mProject)
            Me.mStruct = DAClsappAdvertiserProjectInvoices.Insert(Me.mStruct)
            Me.mID = Me.mStruct.ID.Value
            Me.mDetails.Update(Me)
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Update()
            If MyBase.IsDirty Then
                Me.LoadTableStruct(Me.mProject)
                Me.mStruct = DAClsappAdvertiserProjectInvoices.Update(Me.mStruct)
            End If
            Me.mDetails.Update(Me)
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_DeleteSelf()
            DataPortal_Delete(New Criteria(Me.mID))
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)
            Me.mDetails.Clear()
            Me.mDetails.Update(Me)
            DAClsappAdvertiserProjectInvoices.Delete(criteria.ID)
        End Sub

#End Region

#Region " Child Area "

        Private Sub Fetch(ByVal Invoice As DAClsappAdvertiserProjectInvoices.Struct)
            Me.mStruct = Invoice
            Me.LoadObjectData()
            MarkOld()
        End Sub

        Friend Sub Insert(ByVal parent As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct(parent)
            Me.mStruct = DAClsappAdvertiserProjectInvoices.Insert(Me.mStruct)
            Me.mID = Me.mStruct.ID.Value
            Me.mDetails.Update(Me)
            MarkOld()
        End Sub

        Friend Sub Update(ByVal parent As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct(parent)
            Me.mStruct = DAClsappAdvertiserProjectInvoices.Update(Me.mStruct)
            Me.mDetails.Update(Me)
            MarkOld()
        End Sub

        Friend Sub DeleteSelf()
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            ' if we're new then don't update the database
            If Me.IsNew Then Exit Sub

            Me.mDetails.Clear()
            Me.mDetails.Update(Me)
            DAClsappAdvertiserProjectInvoices.Delete(Me.mID)
            MarkNew()
        End Sub

#End Region

#Region " Common Area "

        Private mStruct As DAClsappAdvertiserProjectInvoices.Struct = New DAClsappAdvertiserProjectInvoices.Struct

        Public Function GetTableStruct() As DAClsappAdvertiserProjectInvoices.Struct
            Return Me.mStruct
        End Function

        ''' <summary>
        ''' Load the data of the object with the fetched data
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadObjectData()
            With Me.mStruct
                Me.mID = .ID.Value
                Me.mProject = ClsProjectInfo.GetProjectInfo(.IDAdvertiserProject.Value)
                Me.mInvoiceNumber = .InvoiceNumber.Value
                Me.mInvoiceSequence = .InvoiceSequence.Value
                Me.mInvoiceDate = .InvoiceDate.Value
                Me.mPaidToDate = .PaidToDate.Value
                Me.mChargedToDate = .ChargedToDate.Value
                Me.mPreviousBalance = .PreviousBalance.Value
                Me.mPreviousNumberOfClickThrough = .PreviousNumberOfClickThrough.Value
                Me.mPreviousNumberOfDisplays = .PreviousNumberOfDisplays.Value
                Me.mTotalAmountDue = .TotalAmountDue.Value
                Me.mDetails = ClsDetailList.GetInvoiceDetailList(DAClsappAdvertiserProjectInvoiceDetails.FetchInvoiceDetail(.ID.Value))
            End With
        End Sub

        ''' <summary>
        ''' Collect the data of the object to fill to a structure of data and returns the structure 
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadTableStruct(ByVal parent As Object)
            With Me.mStruct
                .ID.NewValue = Me.mID
                .IDAdvertiserProject.NewValue = parent.ID
                .InvoiceNumber.NewValue = Me.mInvoiceNumber
                .InvoiceSequence.NewValue = Me.mInvoiceSequence
                .InvoiceDate.NewValue = Me.mInvoiceDate
                .PaidToDate.NewValue = Me.mPaidToDate
                .ChargedToDate.NewValue = Me.mChargedToDate
                .PreviousBalance.NewValue = Me.mPreviousBalance
                .PreviousNumberOfClickThrough.NewValue = Me.mPreviousNumberOfClickThrough
                .PreviousNumberOfDisplays.NewValue = Me.mPreviousNumberOfDisplays
                .TotalAmountDue.NewValue = Me.mTotalAmountDue
            End With
        End Sub

#End Region

#End Region

#Region " Exists "

        Public Shared Function Exists(ByVal id As Long) As Boolean
            Return DataPortal.Execute(Of ExistsCommand)(New ExistsCommand(id)).Exists
        End Function

        <Serializable()> Private Class ExistsCommand
            Inherits CommandBase

            Private mID As Long
            Private mExists As Boolean

            Public ReadOnly Property Exists() As Boolean
                Get
                    Return Me.mExists
                End Get
            End Property

            Public Sub New(ByVal id As String)
                Me.mID = id
            End Sub

            Protected Overrides Sub DataPortal_Execute()
                Me.mExists = (DAClsappAdvertiserProjectInvoices.Fetch(Me.mID).Length > 0)
            End Sub

        End Class

#End Region

    End Class
End Namespace

