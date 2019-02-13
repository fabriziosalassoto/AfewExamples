Imports Csla
Imports SCT.DataAccess

Namespace Advertiser.Receipt
    <Serializable()> Public Class ClsProject
        Inherits BusinessBase(Of ClsProject)

#Region " Business Methods "

        Private mID As Long
        Private mAdvertiserAccount As Advertiser.ClsAccountInfo = Advertiser.ClsAccountInfo.NewAccountInfo
        Private mContact As Advertiser.ClsContactInfo = Advertiser.ClsContactInfo.NewContactInfo
        Private mReceipt As ClsReceiptInfo = ClsReceiptInfo.NewReceiptInfo
        Private mProjectDescription As String = String.Empty
        Private mADUrl As String = String.Empty
        Private mPaidByDisplay As Double = 0
        Private mPaidByClickThrough As Double = 0
        Private mTotalPaid As Double = 0

        Public ReadOnly Property ID() As Long
            Get
                CanReadProperty(True)
                Return Me.mID
            End Get
        End Property

        Public ReadOnly Property AdvertiserAccount() As Advertiser.ClsAccountInfo
            Get
                CanReadProperty(True)
                Return Me.mAdvertiserAccount
            End Get
        End Property

        Public ReadOnly Property Contact() As Advertiser.ClsContactInfo
            Get
                CanReadProperty(True)
                Return Me.mContact
            End Get
        End Property

        Public ReadOnly Property Receipt() As ClsReceiptInfo
            Get
                CanReadProperty(True)
                Return Me.mReceipt
            End Get
        End Property

        Public ReadOnly Property ProjectDescription() As String
            Get
                CanReadProperty(True)
                Return Me.mProjectDescription
            End Get
        End Property

        Public ReadOnly Property ADUrl() As String
            Get
                CanReadProperty(True)
                Return Me.mADUrl
            End Get
        End Property

        Public Property PaidByDisplay() As Double
            Get
                CanReadProperty(True)
                Return Me.mPaidByDisplay
            End Get
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If Me.mPaidByDisplay <> value Then
                    Me.mPaidByDisplay = value
                    ValidationRules.CheckRules("PaidByDisplay")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property PaidByClickThrough() As Double
            Get
                CanReadProperty(True)
                Return Me.mPaidByClickThrough
            End Get
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If Me.mPaidByClickThrough <> value Then
                    Me.mPaidByClickThrough = value
                    ValidationRules.CheckRules("PaidByClickThrough")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property TotalPaid() As Double
            Get
                CanReadProperty(True)
                Return Me.mTotalPaid
            End Get
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If Me.mTotalPaid <> value Then
                    Me.mTotalPaid = value
                    ValidationRules.CheckRules("TotalPaid")
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

        Protected Overrides Function GetIdValue() As Object
            Return Me.mID
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()
            ValidationRules.AddRule(AddressOf Validation.CommonRules.MinValue(Of Double), New Validation.CommonRules.MinValueRuleArgs(Of Double)("PaidByDisplay", 0))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.MinValue(Of Double), New Validation.CommonRules.MinValueRuleArgs(Of Double)("PaidByClickThrough", 0))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.MinValue(Of Double), New Validation.CommonRules.MinValueRuleArgs(Of Double)("TotalPaid", 0))
        End Sub

#End Region

#Region " Authorization Rules "

        'Protected Overrides Sub AddAuthorizationRules()
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

        Friend Shared Function NewReceiptProject(ByVal idProject As Long, ByVal idReceipt As Long, ByVal pPaidByDisplay As Double, ByVal pPaidByClickThrough As Double, ByVal pTotalPaid As Double) As ClsProject
            Return New ClsProject(idProject, idReceipt, pPaidByDisplay, pPaidByClickThrough, pTotalPaid)
        End Function

        Friend Shared Function GetReceiptProject(ByVal projectReceipt As DAClsappAdvertiserProjectReceipts.Struct) As ClsProject
            Return New ClsProject(projectReceipt)
        End Function

        Private Sub New(ByVal idProject As Long, ByVal idReceipt As Long, ByVal paidByDisplay As Double, ByVal paidByClickThrough As Double, ByVal totalPaid As Double)
            MarkAsChild()
            Fetch(idProject, idReceipt, paidByDisplay, paidByClickThrough, totalPaid)
        End Sub

        Private Sub New(ByVal projectReceipt As DAClsappAdvertiserProjectReceipts.Struct)
            MarkAsChild()
            Fetch(projectReceipt)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal idProject As Long, ByVal idReceipt As Long, ByVal paidByDisplay As Double, ByVal paidByClickThrough As Double, ByVal totalPaid As Double)
            Dim list As DAClsappAdvertiserProjects.Struct() = DAClsappAdvertiserProjects.Fetch(idProject)
            If list.Length > 0 Then
                Me.LoadObjectData(list(0), idReceipt, paidByDisplay, paidByClickThrough, totalPaid)
            Else
                Throw New System.Security.SecurityException("Project doesn't assign to Receipt")
            End If
        End Sub

        Private Sub Fetch(ByVal projectReceipt As DAClsappAdvertiserProjectReceipts.Struct)
            Dim list As DAClsappAdvertiserProjects.Struct() = DAClsappAdvertiserProjects.Fetch(projectReceipt.IDAdvertiserProject.Value)
            If list.Length > 0 Then
                Me.mStruct = projectReceipt
                Me.LoadObjectData(list(0))
                Me.MarkOld()
            Else
                Throw New System.Security.SecurityException("Project doesn't assign to Receipt")
            End If
        End Sub

        Friend Sub Insert(ByVal parent As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct(parent)
            Me.mStruct = DAClsappAdvertiserProjectReceipts.Insert(Me.mStruct)
            MarkOld()
        End Sub

        Friend Sub Update(ByVal parent As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct(parent)
            Me.mStruct = DAClsappAdvertiserProjectReceipts.Update(Me.mStruct)
            MarkOld()
        End Sub

        Friend Sub DeleteSelf(ByVal parent As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            ' if we're new then don't update the database
            If Me.IsNew Then Exit Sub

            DAClsappAdvertiserProjectReceipts.Delete(Me.mID, parent.ID)
            MarkNew()
        End Sub

        Private mStruct As DAClsappAdvertiserProjectReceipts.Struct = New DAClsappAdvertiserProjectReceipts.Struct

        Public Function GetTableStruct() As DAClsappAdvertiserProjectReceipts.Struct
            Return Me.mStruct
        End Function

        ''' <summary>
        ''' Populate the data of the object with the fetched data
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadObjectData(ByVal project As DAClsappAdvertiserProjects.Struct)
            Me.mID = project.ID.Value
            Me.mProjectDescription = project.ProjectDescription.Value
            Me.mADUrl = project.ADUrl.Value
            Me.mAdvertiserAccount = Advertiser.ClsAccountInfo.GetAccountInfo(project.IDAdvertiser.Value)
            Me.mContact = Advertiser.ClsContactInfo.GetContactInfo(project.IDAdvertiserContact.Value)
            Me.mReceipt = ClsReceiptInfo.GetReceiptInfo(Me.mStruct.IDAdvertiserReceipt.Value)
            Me.mPaidByClickThrough = Me.mStruct.PaidByClickThrough.Value
            Me.mPaidByDisplay = Me.mStruct.PaidByDisplay.Value
            Me.mTotalPaid = Me.mStruct.TotalPaid.Value
        End Sub

        ''' <summary>
        ''' Populate the data of the object with the fetched data
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadObjectData(ByVal project As DAClsappAdvertiserProjects.Struct, ByVal idReceipt As Long, ByVal PaidByDisplay As Double, ByVal PaidByClickThrough As Double, ByVal TotalPaid As Double)
            Me.mID = project.ID.Value
            Me.mProjectDescription = project.ProjectDescription.Value
            Me.mADUrl = project.ADUrl.Value
            Me.mAdvertiserAccount = Advertiser.ClsAccountInfo.GetAccountInfo(project.IDAdvertiser.Value)
            Me.mContact = Advertiser.ClsContactInfo.GetContactInfo(project.IDAdvertiserContact.Value)
            Me.mReceipt = ClsReceiptInfo.GetReceiptInfo(idReceipt)
            Me.mPaidByClickThrough = PaidByClickThrough
            Me.mPaidByDisplay = PaidByDisplay
            Me.mTotalPaid = TotalPaid
        End Sub

        ''' <summary>
        ''' Collect the data of the object to fill to a structure of data and returns the structure 
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadTableStruct(ByVal parent As Object)
            With Me.mStruct
                .IDAdvertiserProject.NewValue = Me.mID
                .IDAdvertiserReceipt.NewValue = parent.ID
                .PaidByClickThrough.NewValue = Me.mPaidByClickThrough
                .PaidByDisplay.NewValue = Me.mPaidByDisplay
                .TotalPaid.NewValue = Me.mTotalPaid
            End With
        End Sub

#End Region

    End Class
End Namespace