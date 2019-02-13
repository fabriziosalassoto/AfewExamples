Imports Csla
Imports SCT.DataAccess

Namespace Advertiser
    <Serializable()> Public Class ClsReceipt
        Inherits BusinessBase(Of ClsReceipt)

#Region " Business Methods "

        Private mID As Long
        Private mReceiptNumber As Decimal = 0
        Private mPaymentNumber As Decimal = 0
        Private mPaymentAmount As Decimal = 0
        Private mPaymentType As Integer = 0
        Private mPaymentDate As Date = New Date(1900, 1, 1)
        Private mProjects As Receipt.ClsProjectList = Receipt.ClsProjectList.NewReceiptProjectList

        Public ReadOnly Property ID() As Long
            Get
                CanReadProperty(True)
                Return Me.mID
            End Get
        End Property

        Public Property ReceiptNumber() As Decimal
            Get
                CanReadProperty(True)
                Return Me.mReceiptNumber
            End Get
            Set(ByVal value As Decimal)
                CanWriteProperty(True)
                If Me.mReceiptNumber <> value Then
                    Me.mReceiptNumber = value
                    ValidationRules.CheckRules("ReceiptNumber")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property PaymentNumber() As Decimal
            Get
                CanReadProperty(True)
                Return Me.mPaymentNumber
            End Get
            Set(ByVal value As Decimal)
                CanWriteProperty(True)
                If Me.mPaymentNumber <> value Then
                    Me.mPaymentNumber = value
                    ValidationRules.CheckRules("PaymentNumber")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property PaymentAmount() As Decimal
            Get
                CanReadProperty(True)
                Return Me.mPaymentAmount
            End Get
            Set(ByVal value As Decimal)
                CanWriteProperty(True)
                If Me.mPaymentAmount <> value Then
                    Me.mPaymentAmount = value
                    ValidationRules.CheckRules("PaymentAmount")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property PaymentType() As Integer
            Get
                CanReadProperty(True)
                Return Me.mPaymentType
            End Get
            Set(ByVal value As Integer)
                CanWriteProperty(True)
                If Me.mPaymentType <> value Then
                    Me.mPaymentType = value
                    ValidationRules.CheckRules("PaymentType")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property PaymentDate() As Date
            Get
                CanReadProperty(True)
                Return Me.mPaymentDate
            End Get
            Set(ByVal value As Date)
                CanWriteProperty(True)
                If Me.mPaymentDate <> value Then
                    Me.mPaymentDate = value
                    ValidationRules.CheckRules("PaymentDate")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public ReadOnly Property Projects() As Receipt.ClsProjectList
            Get
                CanReadProperty(True)
                Return Me.mProjects
            End Get
        End Property

        Public Overrides ReadOnly Property IsValid() As Boolean
            Get
                Return MyBase.IsValid AndAlso Me.mProjects.IsValid
            End Get
        End Property

        Public Overrides ReadOnly Property IsDirty() As Boolean
            Get
                Return MyBase.IsDirty OrElse Me.mProjects.IsDirty
            End Get
        End Property

        Protected Overrides Function GetIdValue() As Object
            Return Me.mID
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()
            ValidationRules.AddRule(AddressOf Validation.CommonRules.MinValue(Of Decimal), New Validation.CommonRules.MinValueRuleArgs(Of Decimal)("ReceiptNumber", 0))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.MinValue(Of Decimal), New Validation.CommonRules.MinValueRuleArgs(Of Decimal)("PaymentNumber", 0))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.MinValue(Of Decimal), New Validation.CommonRules.MinValueRuleArgs(Of Decimal)("PaymentAmount", 0))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.IntegerMinValue, New Validation.CommonRules.IntegerMinValueRuleArgs("PaymentType", 0))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.MinValue(Of Date), New Validation.CommonRules.MinValueRuleArgs(Of Date)("PaymentDate", New Date(1900, 1, 1)))
        End Sub

#End Region

#Region " Authorization Rules "

        'Protected Overrides Sub AddAuthorizationRules()
        '    AuthorizationRules.AllowRead("ID", " ")
        '    AuthorizationRules.AllowRead("ReceiptNumber", " ")
        '    AuthorizationRules.AllowRead("PaymentNumber", " ")
        '    AuthorizationRules.AllowRead("PaymentAmount", " ")
        '    AuthorizationRules.AllowRead("PaymentType", " ")
        '    AuthorizationRules.AllowRead("PaymentDate", " ")
        '    AuthorizationRules.AllowRead("Projects", "")
        '    AuthorizationRules.AllowWrite("ReceiptNumber", New String() {" ", " "})
        '    AuthorizationRules.AllowWrite("PaymentNumber", New String() {" ", " "})
        '    AuthorizationRules.AllowWrite("PaymentAmount", New String() {" ", " "})
        '    AuthorizationRules.AllowWrite("PaymentType", New String() {" ", " "})
        '    AuthorizationRules.AllowWrite("PaymentDate", New String() {" ", " "})
        '    AuthorizationRules.AllowWrite("Projects", New String() {" ", " "})
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

        Public Shared Function NewReceipt() As ClsReceipt
            If Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add Receipt records.")
            End If
            Return DataPortal.Create(Of ClsReceipt)(New Criteria(0))
        End Function

        Public Shared Function GetReceipt(ByVal ID As Long) As ClsReceipt
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view Receipt records.")
            End If
            Return DataPortal.Fetch(Of ClsReceipt)(New Criteria(ID))
        End Function

        Public Shared Sub DeleteReceipt(ByVal ID As Long)
            If Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove Receipt records.")
            End If
            DataPortal.Delete(New Criteria(ID))
        End Sub

        Public Overrides Function Save() As ClsReceipt
            If IsDeleted AndAlso Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove Receipt records.")

            ElseIf IsNew AndAlso Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add Receipt records.")

            ElseIf Not CanEditObject() Then
                Throw New System.Security.SecurityException("User not authorized to update Receipt records.")
            End If
            Return MyBase.Save
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

#End Region

#Region " Child Methods "

        Friend Shared Function NewChildReceipt() As ClsReceipt
            Dim Child As New ClsReceipt
            Child.MarkAsChild()
            Return Child
        End Function

        Friend Shared Function GetChildReceipt(ByVal Receipt As DAClsappAdvertiserReceipts.Struct) As ClsReceipt
            Return New ClsReceipt(Receipt)
        End Function

        Private Sub New(ByVal Receipt As DAClsappAdvertiserReceipts.Struct)
            MarkAsChild()
            Fetch(Receipt)
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
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            Dim List As DAClsappAdvertiserReceipts.Struct() = DAClsappAdvertiserReceipts.Fetch(criteria.ID)
            If List.Length = 0 Then Throw New System.Security.SecurityException("Receipt record doesn't exists")

            Me.mStruct = List(0)
            Me.LoadObjectData()
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Insert()
            Me.LoadTableStruct()
            Me.mStruct = DAClsappAdvertiserReceipts.Insert(Me.mStruct)
            Me.mID = Me.mStruct.ID.Value
            Me.mProjects.Update(Me)
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Update()
            If MyBase.IsDirty Then
                Me.LoadTableStruct()
                Me.mStruct = DAClsappAdvertiserReceipts.Update(Me.mStruct)
            End If
            Me.mProjects.Update(Me)
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_DeleteSelf()
            DataPortal_Delete(New Criteria(Me.mID))
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)
            Me.mProjects.Clear()
            Me.mProjects.Update(Me)
            DAClsappAdvertiserReceipts.Delete(criteria.ID)
        End Sub

#End Region

#Region " Child Area "

        Private Sub Fetch(ByVal Receipt As DAClsappAdvertiserReceipts.Struct)
            Me.mStruct = Receipt
            Me.LoadObjectData()
            MarkOld()
        End Sub

        Friend Sub Insert(ByVal parent As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct()
            Me.mStruct = DAClsappAdvertiserReceipts.Insert(Me.mStruct)
            Me.mID = Me.mStruct.ID.Value
            Me.mProjects.Update(Me)
            MarkOld()
        End Sub

        Friend Sub Update(ByVal parent As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct()
            Me.mStruct = DAClsappAdvertiserReceipts.Update(Me.mStruct)
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
            DAClsappAdvertiserReceipts.Delete(Me.mID)
            MarkNew()
        End Sub

#End Region

#Region " Common Area "

        Private mStruct As DAClsappAdvertiserReceipts.Struct = New DAClsappAdvertiserReceipts.Struct

        Public Function GetTableStruct() As DAClsappAdvertiserReceipts.Struct
            Return Me.mStruct
        End Function

        ''' <summary>
        ''' Load the data of the object with the fetched data
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadObjectData()
            With Me.mStruct
                Me.mID = .ID.Value
                Me.mReceiptNumber = .ReceiptNumber.Value
                Me.mPaymentNumber = .PaymentNumber.Value
                Me.mPaymentAmount = .PaymentAmount.Value
                Me.mPaymentType = .PaymentType.Value
                Me.mPaymentDate = .PaymentDate.Value
                Me.mProjects = Receipt.ClsProjectList.GetReceiptProjectList(DAClsappAdvertiserProjectReceipts.FetchReceiptProject(.ID.Value))
            End With
        End Sub

        ''' <summary>
        ''' Collect the data of the object to fill to a structure of data and returns the structure 
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadTableStruct()
            With Me.mStruct
                .ID.NewValue = Me.mID
                .ReceiptNumber.NewValue = Me.mReceiptNumber
                .PaymentNumber.NewValue = Me.mPaymentNumber
                .PaymentAmount.NewValue = Me.mPaymentAmount
                .PaymentType.NewValue = Me.mPaymentType
                .PaymentDate.NewValue = Me.mPaymentDate
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
                Me.mExists = (DAClsappAdvertiserReceipts.Fetch(Me.mID).Length > 0)
            End Sub

        End Class

#End Region

    End Class
End Namespace

