Imports Csla
Imports SCT.DataAccess

Namespace Advertiser
    <Serializable()> Public Class ClsTransactionInvoiceInfo
        Inherits ReadOnlyBase(Of ClsTransactionInvoiceInfo)

#Region " Business Methods "

        Private mID As Long
        Private mAdvertiser As ClsAccountInfo = ClsAccountInfo.NewAccountInfo
        Private mProject As ClsProjectInfo = ClsProjectInfo.NewProjectInfo
        Private mTransactionType As Transactions
        Private mTransactionDate As Date
        Private mTransactionNumber As Decimal
        Private mTransactionAmount As Double

        Public ReadOnly Property ID() As Long
            Get
                Return Me.mID
            End Get
        End Property

        Public ReadOnly Property Advertiser() As ClsAccountInfo
            Get
                Return Me.mAdvertiser
            End Get
        End Property

        Public ReadOnly Property Project() As ClsProjectInfo
            Get
                Return Me.mProject
            End Get
        End Property

        Public ReadOnly Property TransactionType() As Transactions
            Get
                Return Me.mTransactionType
            End Get
        End Property

        Public ReadOnly Property TransactionDate() As Date
            Get
                Return Me.mTransactionDate
            End Get
        End Property

        Public ReadOnly Property TransactionNumber() As Decimal
            Get
                Return Me.mTransactionNumber
            End Get
        End Property

        Public ReadOnly Property TransactionAmount() As Double
            Get
                Return Me.mTransactionAmount
            End Get
        End Property

        Protected Overrides Function GetIdValue() As Object
            Return Me.mID
        End Function

        Public Overrides Function ToString() As String
            Return Me.mTransactionNumber
        End Function

        Public Function ToListItem() As System.Web.UI.WebControls.ListItem
            Return New System.Web.UI.WebControls.ListItem(Me.ToString, Me.mID)
        End Function

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole(" ")
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function NewTransactionInvoice() As ClsTransactionInvoiceInfo
            Return New ClsTransactionInvoiceInfo
        End Function

        Public Shared Function GetTransactionInvoice(ByVal ID As Long) As ClsTransactionInvoiceInfo
            Return DataPortal.Fetch(Of ClsTransactionInvoiceInfo)(New IDCriteria(ID))
        End Function

        Public Shared Function GetTransactionInvoice(ByVal Struct As DAClsappAdvertiserTransactionInvoices.Struct) As ClsTransactionInvoiceInfo
            Return DataPortal.Fetch(Of ClsTransactionInvoiceInfo)(Struct)
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        Private mStruct As DAClsappAdvertiserTransactionInvoices.Struct = New DAClsappAdvertiserTransactionInvoices.Struct

        Public Function GetTableStruct() As DAClsappAdvertiserTransactionInvoices.Struct
            Return Me.mStruct
        End Function

        <Serializable()> Private Class IDCriteria

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

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As IDCriteria)
            Dim List As DAClsappAdvertiserTransactionInvoices.Struct() = DAClsappAdvertiserTransactionInvoices.Fetch(criteria.ID)
            If List.Length > 0 Then
                Me.mStruct = List(0)
                Me.LoadObjectData()
            Else
                Throw New System.Security.SecurityException("Invoice doesn't exists")
            End If
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal struct As DAClsappAdvertiserTransactionInvoices.Struct)
            Me.mStruct = struct
            Me.LoadObjectData()
        End Sub

        ''' <summary>
        ''' Load the data of the object with the fetched data
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadObjectData()
            With Me.mStruct
                Me.mID = .ID.Value
                Me.mTransactionType = .TransactionType.Value
                Me.mAdvertiser = ClsAccountInfo.GetAccountInfo(.IDAdvertiser.Value)
                Me.mProject = ClsProjectInfo.GetProjectInfo(.IDAdvertiserProject.Value)
                Me.mTransactionDate = .TransactionDate.Value
                Me.mTransactionNumber = .TransactionNumber.Value
                Me.mTransactionAmount = .TransactionAmount.Value
            End With
        End Sub

#End Region

    End Class
End Namespace
