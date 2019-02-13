Imports Csla
Imports SCT.DataAccess

Namespace Advertiser
    <Serializable()> Public Class ClsProjectPrice
        Inherits BusinessBase(Of ClsProjectPrice)

#Region " Business Methods "

        Private mID As Long
        Private mCostPerDisplay As Single
        Private mTotalPaid As Single
        Private mDatePaid As New DateTime
        Private mCheckNumber As Long
        Private mInvoiceNumber As Long
        Private mPaidByClickThrough As Single
        Private mPaidByDisplay As Single

        Public ReadOnly Property ID() As Long
            Get
                CanReadProperty(True)
                Return Me.mID
            End Get
        End Property

        Public Property CostPerDisplay() As Single
            Get
                CanReadProperty(True)
                Return Me.mCostPerDisplay
            End Get
            Set(ByVal value As Single)
                CanWriteProperty(True)
                If Me.mCostPerDisplay <> value Then
                    Me.mCostPerDisplay = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property TotalPaid() As Single
            Get
                CanReadProperty(True)
                Return Me.mTotalPaid
            End Get
            Set(ByVal value As Single)
                CanWriteProperty(True)
                If Me.mTotalPaid <> value Then
                    Me.mTotalPaid = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property DatePaid() As DateTime
            Get
                CanReadProperty(True)
                Return Me.mDatePaid
            End Get
            Set(ByVal value As DateTime)
                CanWriteProperty(True)
                If Me.mDatePaid <> value Then
                    Me.mDatePaid = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property CheckNumber() As Long
            Get
                CanReadProperty(True)
                Return Me.mCheckNumber
            End Get
            Set(ByVal value As Long)
                CanWriteProperty(True)
                If Me.mCheckNumber <> value Then
                    Me.mCheckNumber = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property InvoiceNumber() As Long
            Get
                CanReadProperty(True)
                Return Me.mInvoiceNumber
            End Get
            Set(ByVal value As Long)
                CanWriteProperty(True)
                If Me.mInvoiceNumber <> value Then
                    Me.mInvoiceNumber = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property PaidByClickThrough() As Single
            Get
                CanReadProperty(True)
                Return Me.mPaidByClickThrough
            End Get
            Set(ByVal value As Single)
                CanWriteProperty(True)
                If Me.mPaidByClickThrough <> value Then
                    Me.mPaidByClickThrough = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property PaidByDisplay() As Single
            Get
                CanReadProperty(True)
                Return Me.mPaidByDisplay
            End Get
            Set(ByVal value As Single)
                CanWriteProperty(True)
                If Me.mPaidByDisplay <> value Then
                    Me.mPaidByDisplay = value
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

#End Region

#Region " Authorization Rules "

        'Protected Overrides Sub AddAuthorizationRules()
        '    AuthorizationRules.AllowRead("ID", "")
        '    AuthorizationRules.AllowRead("Project", "")
        '    AuthorizationRules.AllowRead("CostPerDisplay", "")
        '    AuthorizationRules.AllowRead("TotalPaid", "")
        '    AuthorizationRules.AllowRead("DatePaid", "")
        '    AuthorizationRules.AllowRead("CheckNumber", "")
        '    AuthorizationRules.AllowRead("InvoiceNumber", "")
        '    AuthorizationRules.AllowRead("PaidByClickThrough", "")
        '    AuthorizationRules.AllowRead("PaidByDisplay", "")
        '    AuthorizationRules.AllowWrite("CostPerDisplay", "")
        '    AuthorizationRules.AllowWrite("TotalPaid", "")
        '    AuthorizationRules.AllowWrite("DatePaid", "")
        '    AuthorizationRules.AllowWrite("CheckNumber", "")
        '    AuthorizationRules.AllowWrite("InvoiceNumber", "")
        '    AuthorizationRules.AllowWrite("PaidByClickThrough", "")
        '    AuthorizationRules.AllowWrite("PaidByDisplay", "")
        'End Sub

#End Region

#Region " Factory Methods "

        Friend Shared Function NewProjectPrice() As ClsProjectPrice
            Return New ClsProjectPrice()
        End Function

        Friend Shared Function GetProjectPrice(ByVal price As DAClsappAdvertiserProjectPriceInfo.Struct) As ClsProjectPrice
            Return New ClsProjectPrice(price)
        End Function

        Private Sub New()
            MarkAsChild()
        End Sub

        Private Sub New(ByVal price As DAClsappAdvertiserProjectPriceInfo.Struct)
            MarkAsChild()
            Fetch(price)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal price As DAClsappAdvertiserProjectPriceInfo.Struct)
            With price
                Me.mID = .ID
                Me.mCostPerDisplay = .CostPerDisplay
                Me.mTotalPaid = .TotalPaid
                Me.mDatePaid = .DatePaid
                Me.mCheckNumber = .CheckNumber
                Me.mInvoiceNumber = .InvoiceNumber
                Me.mPaidByClickThrough = .PaidByClickThrough
                Me.mPaidByDisplay = .PaidByDisplay
            End With
            MarkOld()
        End Sub

        Friend Sub Insert(ByVal project As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.mID = DAClsappAdvertiserProjectPriceInfo.Insert(Me.GetAdvertiserProjectPriceInfoStruct(project.ID))
            MarkOld()
        End Sub

        Friend Sub Update(ByVal project As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            DAClsappAdvertiserProjectPriceInfo.Update(Me.GetAdvertiserProjectPriceInfoStruct(project.ID))
            MarkOld()
        End Sub

        ''' <summary>
        ''' Collect the data of the object to fill to a structure of data and returns the structure 
        ''' </summary>
        ''' <returns>Structure with data</returns>
        ''' <remarks></remarks>
        Private Function GetAdvertiserProjectPriceInfoStruct(ByVal projectID As Long) As DAClsappAdvertiserProjectPriceInfo.Struct
            Dim Struct As New DAClsappAdvertiserProjectPriceInfo.Struct
            With Struct
                .ID = Me.mID
                .IDAdvertiserProject = projectID
                .CostPerDisplay = Me.mCostPerDisplay
                .TotalPaid = Me.mTotalPaid
                .DatePaid = Me.mDatePaid
                .CheckNumber = Me.mCheckNumber
                .InvoiceNumber = Me.mInvoiceNumber
                .PaidByClickThrough = Me.mPaidByClickThrough
                .PaidByDisplay = Me.mPaidByDisplay
            End With
            Return Struct
        End Function

        Friend Sub DeleteSelf()
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            ' if we're new then don't update the database
            If Me.IsNew Then Exit Sub

            DAClsappAdvertiserProjectPriceInfo.Delete(Me.mID)
            MarkNew()
        End Sub

#End Region

    End Class
End Namespace