Imports Csla
Imports SCT.DataAccess

Namespace Advertiser.Project.Invoice
    <Serializable()> Public Class ClsDetail
        Inherits BusinessBase(Of ClsDetail)

#Region " Business Methods "

        Private mID As Long
        Private mInvoice As ClsInvoiceInfo = ClsInvoiceInfo.NewInvoiceInfo
        Private mCurrentNumberOfClickThrough As Integer = 0
        Private mCurrentNumberOfDisplay As Integer = 0
        Private mCostPerClickThrough As Double = 0
        Private mCostPerDisplay As Double = 0
        Private mAmountDue As Double = 0

        Public ReadOnly Property ID() As Long
            Get
                CanReadProperty(True)
                Return Me.mID
            End Get
        End Property

        Public Property Invoice() As ClsInvoiceInfo
            Get
                CanReadProperty(True)
                Return Me.mInvoice
            End Get
            Set(ByVal value As ClsInvoiceInfo)
                CanWriteProperty(True)
                If value IsNot Nothing AndAlso value.ID <> 0 Then
                    If Me.mInvoice.ID <> value.ID Then
                        Me.mInvoice = value
                        PropertyHasChanged()
                    End If
                Else
                    Throw New System.Security.SecurityException("Invoice required.")
                End If
            End Set
        End Property

        Public Property CurrentNumberOfClickThrough() As Integer
            Get
                CanReadProperty(True)
                Return Me.mCurrentNumberOfClickThrough
            End Get
            Set(ByVal value As Integer)
                CanWriteProperty(True)
                If Me.mCurrentNumberOfClickThrough <> value Then
                    Me.mCurrentNumberOfClickThrough = value
                    ValidationRules.CheckRules("CurrentNumberOfClickThrough")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property CurrentNumberOfDisplay() As Integer
            Get
                CanReadProperty(True)
                Return Me.mCurrentNumberOfDisplay
            End Get
            Set(ByVal value As Integer)
                CanWriteProperty(True)
                If Me.mCurrentNumberOfDisplay <> value Then
                    Me.mCurrentNumberOfDisplay = value
                    ValidationRules.CheckRules("CurrentNumberOfDisplay")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property CostPerClickThrough() As Double
            Get
                CanReadProperty(True)
                Return Me.mCostPerClickThrough
            End Get
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If Me.mCostPerClickThrough <> value Then
                    Me.mCostPerClickThrough = value
                    ValidationRules.CheckRules("CostPerClickThrough")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property CostPerDisplay() As Double
            Get
                CanReadProperty(True)
                Return Me.mCostPerDisplay
            End Get
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If Me.mCostPerDisplay <> value Then
                    Me.mCostPerDisplay = value
                    ValidationRules.CheckRules("CostPerDisplay")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property AmountDue() As Double
            Get
                CanReadProperty(True)
                Return Me.mAmountDue
            End Get
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If Me.mAmountDue <> value Then
                    Me.mAmountDue = value
                    ValidationRules.CheckRules("AmountDue")
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

        Public Sub AssignInvoice(ByVal invoiceId As Long)
            If invoiceId <> 0 Then
                If Me.mInvoice.ID <> invoiceId Then
                    Me.mInvoice = ClsInvoiceInfo.GetInvoiceInfo(invoiceId)
                    PropertyHasChanged("Invoice")
                End If
            Else
                Throw New System.Security.SecurityException("Invoice required.")
            End If
        End Sub

        Protected Overrides Function GetIdValue() As Object
            Return Me.mID
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()
            ValidationRules.AddRule(AddressOf InvoiceRequired, "Invoice")

            ValidationRules.AddRule(AddressOf Validation.CommonRules.IntegerMinValue, New Validation.CommonRules.IntegerMinValueRuleArgs("CurrentNumberOfClickThrough", 0))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.IntegerMinValue, New Validation.CommonRules.IntegerMinValueRuleArgs("CurrentNumberOfDisplay", 0))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.MinValue(Of Double), New Validation.CommonRules.MinValueRuleArgs(Of Double)("CostPerClickThrough", 0))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.MinValue(Of Double), New Validation.CommonRules.MinValueRuleArgs(Of Double)("CostPerDisplay", 0))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.MinValue(Of Double), New Validation.CommonRules.MinValueRuleArgs(Of Double)("AmountDue", 0))
        End Sub

        Private Function InvoiceRequired(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
            If Me.mInvoice.ID = 0 Then
                e.Description = "Invoice required."
                Return False
            Else
                Return True
            End If
        End Function

#End Region

#Region " Authorization Rules "

        'Protected Overrides Sub AddAuthorizationRules()
        '    AuthorizationRules.AllowRead("ID", " ")
        '    AuthorizationRules.AllowRead("Invoice", " ")
        '    AuthorizationRules.AllowRead("CurrentNumberOfClickThrough", " ")
        '    AuthorizationRules.AllowRead("CurrentNumberOfDisplay", " ")
        '    AuthorizationRules.AllowRead("CostPerClickThrough", " ")
        '    AuthorizationRules.AllowRead("CostPerDisplay", " ")
        '    AuthorizationRules.AllowRead("AmountDue", " ")
        '    AuthorizationRules.AllowWrite("Invoice", New String() {" ", " "})
        '    AuthorizationRules.AllowWrite("CurrentNumberOfClickThrough", New String() {" ", " "})
        '    AuthorizationRules.AllowWrite("CurrentNumberOfDisplay", New String() {" ", " "})
        '    AuthorizationRules.AllowWrite("CostPerClickThrough", New String() {" ", " "})
        '    AuthorizationRules.AllowWrite("CostPerDisplay", New String() {" ", " "})
        '    AuthorizationRules.AllowWrite("AmountDue", New String() {" ", " "})
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

        Public Shared Function NewDetail() As ClsDetail
            If Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add Detail records.")
            End If
            Return DataPortal.Create(Of ClsDetail)(New Criteria(0))
        End Function

        Public Shared Function GetDetail(ByVal ID As Long) As ClsDetail
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view Detail records.")
            End If
            Return DataPortal.Fetch(Of ClsDetail)(New Criteria(ID))
        End Function

        Public Shared Sub DeleteDetail(ByVal ID As Long)
            If Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove Detail records.")
            End If
            DataPortal.Delete(New Criteria(ID))
        End Sub

        Public Overrides Function Save() As ClsDetail
            If IsDeleted AndAlso Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove Detail records.")

            ElseIf IsNew AndAlso Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add Detail records.")

            ElseIf Not CanEditObject() Then
                Throw New System.Security.SecurityException("User not authorized to update Detail records.")
            End If
            Return MyBase.Save
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

#End Region

#Region " Child Methods "

        Friend Shared Function NewChildDetail() As ClsDetail
            Dim Child As New ClsDetail
            Child.ValidationRules.CheckRules("Invoice")
            Child.MarkAsChild()
            Return Child
        End Function

        Friend Shared Function GetChildDetail(ByVal detail As DAClsappAdvertiserProjectInvoiceDetails.Struct) As ClsDetail
            Return New ClsDetail(detail)
        End Function

        Friend Shared Function NewInvoiceDetail() As ClsDetail
            Dim Child As New ClsDetail
            Child.MarkAsChild()
            Return Child
        End Function

        Friend Shared Function GetInvoiceDetail(ByVal detail As DAClsappAdvertiserProjectInvoiceDetails.Struct) As ClsDetail
            Return New ClsDetail(detail)
        End Function

        Private Sub New(ByVal detail As DAClsappAdvertiserProjectInvoiceDetails.Struct)
            MarkAsChild()
            Fetch(detail)
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
            Me.ValidationRules.CheckRules("Invoice")
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            Dim List As DAClsappAdvertiserProjectInvoiceDetails.Struct() = DAClsappAdvertiserProjectInvoiceDetails.Fetch(criteria.ID)
            If List.Length = 0 Then Throw New System.Security.SecurityException("Detail record doesn't exists")

            Me.mStruct = List(0)
            Me.LoadObjectData()
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Insert()
            Me.LoadTableStruct(Me.mInvoice)
            Me.mStruct = DAClsappAdvertiserProjectInvoiceDetails.Insert(Me.mStruct)
            Me.mID = Me.mStruct.ID.Value
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Update()
            If MyBase.IsDirty Then
                Me.LoadTableStruct(Me.mInvoice)
                Me.mStruct = DAClsappAdvertiserProjectInvoiceDetails.Update(Me.mStruct)
            End If
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_DeleteSelf()
            DataPortal_Delete(New Criteria(Me.mID))
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)
            DAClsappAdvertiserProjectInvoiceDetails.Delete(criteria.ID)
        End Sub

#End Region

#Region " Child Area "

        Private Sub Fetch(ByVal detail As DAClsappAdvertiserProjectInvoiceDetails.Struct)
            Me.mStruct = detail
            Me.LoadObjectData()
            MarkOld()
        End Sub

        Friend Sub Insert(ByVal parent As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct(parent)
            Me.mStruct = DAClsappAdvertiserProjectInvoiceDetails.Insert(Me.mStruct)
            Me.mID = Me.mStruct.ID.Value
            MarkOld()
        End Sub

        Friend Sub Update(ByVal parent As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct(parent)
            Me.mStruct = DAClsappAdvertiserProjectInvoiceDetails.Update(Me.mStruct)
            MarkOld()
        End Sub

        Friend Sub DeleteSelf()
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            ' if we're new then don't update the database
            If Me.IsNew Then Exit Sub

            DAClsappAdvertiserProjectInvoiceDetails.Delete(Me.mID)
            MarkNew()
        End Sub

#End Region

#Region " Common Area "

        Private mStruct As DAClsappAdvertiserProjectInvoiceDetails.Struct = New DAClsappAdvertiserProjectInvoiceDetails.Struct

        Public Function GetTableStruct() As DAClsappAdvertiserProjectInvoiceDetails.Struct
            Return Me.mStruct
        End Function

        ''' <summary>
        ''' Load the data of the object with the fetched data
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadObjectData()
            With Me.mStruct
                Me.mID = .ID.Value
                Me.mInvoice = ClsInvoiceInfo.GetInvoiceInfo(.IDAdvertiserInvoice.Value)
                Me.mCurrentNumberOfClickThrough = .CurrentNumberOfClickThrough.Value
                Me.mCurrentNumberOfDisplay = .CurrentNumberOfDisplay.Value
                Me.mCostPerClickThrough = .CostPerClickThrough.Value
                Me.mCostPerDisplay = .CostPerDisplay.Value
                Me.mAmountDue = .AmountDue.Value
            End With
        End Sub

        ''' <summary>
        ''' Collect the data of the object to fill to a structure of data and returns the structure 
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadTableStruct(ByVal parent As Object)
            With Me.mStruct
                .ID.NewValue = Me.mID
                .IDAdvertiserInvoice.NewValue = parent.ID
                .CurrentNumberOfClickThrough.NewValue = Me.mCurrentNumberOfClickThrough
                .CurrentNumberOfDisplay.NewValue = Me.mCurrentNumberOfDisplay
                .CostPerClickThrough.NewValue = Me.mCostPerClickThrough
                .CostPerDisplay.NewValue = Me.mCostPerDisplay
                .AmountDue.NewValue = Me.mAmountDue
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
                Me.mExists = (DAClsappAdvertiserProjectInvoiceDetails.Fetch(Me.mID).Length > 0)
            End Sub

        End Class

#End Region

    End Class
End Namespace

