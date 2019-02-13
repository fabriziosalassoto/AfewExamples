Imports Csla
Imports SCT.DataAccess

Namespace Advertiser
    <Serializable()> Public Class ClsReceiptInfo
        Inherits ReadOnlyBase(Of ClsReceiptInfo)

#Region " Business Methods "

        Private mID As Long
        Private mReceiptNumber As Decimal
        Private mPaymentAmount As Decimal
        Private mPaymnetDate As Date

        Public ReadOnly Property ID() As Long
            Get
                Return Me.mID
            End Get
        End Property

        Public ReadOnly Property ReceiptNumber() As Decimal
            Get
                Return Me.mReceiptNumber
            End Get
        End Property

        Public ReadOnly Property PaymentAmount() As Decimal
            Get
                Return Me.mPaymentAmount
            End Get
        End Property

        Public ReadOnly Property PaymnetDate() As DateTime
            Get
                Return Me.mPaymnetDate
            End Get
        End Property

        Protected Overrides Function GetIdValue() As Object
            Return Me.mID
        End Function

        Public Overrides Function ToString() As String
            Return Me.mReceiptNumber
        End Function

        Public Function ToListItem() As System.Web.UI.WebControls.ListItem
            Return New System.Web.UI.WebControls.ListItem(Me.ToString, Me.mID)
        End Function

        Public Function GetReceipt() As ClsReceipt
            Return ClsReceipt.GetReceipt(Me.mID)
        End Function

#End Region

#Region " Authorization Rules "

        'Protected Overrides Sub AddAuthorizationRules()
        '    AuthorizationRules.AllowRead("ID", " ")
        '    AuthorizationRules.AllowRead("ReceiptNumber", " ")
        '    AuthorizationRules.AllowRead("PaymentAmount", " ")
        '    AuthorizationRules.AllowRead("PaymnetDate", " ")
        'End Sub

        Public Shared Function CanGetObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole(" ")
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function NewReceiptInfo() As ClsReceiptInfo
            Return New ClsReceiptInfo
        End Function

        Public Shared Function GetReceiptInfo(ByVal ID As Long) As ClsReceiptInfo
            Return DataPortal.Fetch(Of ClsReceiptInfo)(New Criteria(ID))
        End Function

        Public Shared Function GetReceiptInfo(ByVal Struct As DAClsappAdvertiserReceipts.Struct) As ClsReceiptInfo
            Return DataPortal.Fetch(Of ClsReceiptInfo)(Struct)
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

#End Region

#Region " Data Access "

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

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            Dim List As DAClsappAdvertiserReceipts.Struct() = DAClsappAdvertiserReceipts.Fetch(criteria.ID)
            If List.Length > 0 Then
                Me.LoadObjectData(List(0))
            Else
                Throw New System.Security.SecurityException("Receipt doesn't exists")
            End If
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal Struct As DAClsappAdvertiserReceipts.Struct)
            Me.LoadObjectData(Struct)
        End Sub

        ''' <summary>
        ''' Load the data of the object with the fetched data
        ''' </summary>
        ''' <param name="Struct">Struct with the data</param>
        ''' <remarks></remarks>
        Private Sub LoadObjectData(ByVal Struct As DAClsappAdvertiserReceipts.Struct)
            With Struct
                Me.mID = .ID.Value
                Me.mReceiptNumber = .ReceiptNumber.Value
                Me.mPaymentAmount = .PaymentAmount.Value
                Me.mPaymnetDate = .PaymentDate.Value
            End With
        End Sub

#End Region

    End Class
End Namespace