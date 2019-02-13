Imports Csla
Imports SCT.DataAccess

Namespace Advertiser.Project
    <Serializable()> Public Class ClsInvoiceInfo
        Inherits ReadOnlyBase(Of ClsInvoiceInfo)

#Region " Business Methods "

        Private mID As Long
        Private mProject As ClsProjectInfo = ClsProjectInfo.NewProjectInfo
        Private mInvoiceNumber As Decimal
        Private mInvoiceSequence As Decimal
        Private mInvoiceDate As Date
        Private mTotalAmountDue As Double

        Public ReadOnly Property ID() As Long
            Get
                Return Me.mID
            End Get
        End Property

        Public ReadOnly Property Project() As ClsProjectInfo
            Get
                Return Me.mProject
            End Get
        End Property

        Public ReadOnly Property InvoiceNumber() As Decimal
            Get
                Return Me.mInvoiceNumber
            End Get
        End Property

        Public ReadOnly Property InvoiceSequence() As Decimal
            Get
                Return Me.mInvoiceSequence
            End Get
        End Property

        Public ReadOnly Property InvoiceDate() As DateTime
            Get
                Return Me.mInvoiceDate
            End Get
        End Property

        Public ReadOnly Property TotalAmountDue() As Double
            Get
                Return Me.mTotalAmountDue
            End Get
        End Property

        Protected Overrides Function GetIdValue() As Object
            Return Me.mID
        End Function

        Public Overrides Function ToString() As String
            Return Me.mInvoiceNumber
        End Function

        Public Function ToListItem() As System.Web.UI.WebControls.ListItem
            Return New System.Web.UI.WebControls.ListItem(Me.ToString, Me.mID)
        End Function

        Public Function GetInvoice() As ClsInvoice
            Return ClsInvoice.GetInvoice(Me.mID)
        End Function

#End Region

#Region " Authorization Rules "

        'Protected Overrides Sub AddAuthorizationRules()
        '    AuthorizationRules.AllowRead("ID", " ")
        '    AuthorizationRules.AllowRead("Project", " ")
        '    AuthorizationRules.AllowRead("InvoiceNumber", " ")
        '    AuthorizationRules.AllowRead("InvoiceSequence", " ")
        '    AuthorizationRules.AllowRead("InvoiceDate", " ")
        '    AuthorizationRules.AllowRead("TotalAmountDue", " ")
        'End Sub

        Public Shared Function CanGetObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole(" ")
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function NewInvoiceInfo() As ClsInvoiceInfo
            Return New ClsInvoiceInfo
        End Function

        Public Shared Function GetInvoiceInfo(ByVal ID As Long) As ClsInvoiceInfo
            Return DataPortal.Fetch(Of ClsInvoiceInfo)(New Criteria(ID))
        End Function

        Public Shared Function GetInvoiceInfo(ByVal Struct As DAClsappAdvertiserProjectInvoices.Struct) As ClsInvoiceInfo
            Return DataPortal.Fetch(Of ClsInvoiceInfo)(Struct)
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
            Dim List As DAClsappAdvertiserProjectInvoices.Struct() = DAClsappAdvertiserProjectInvoices.Fetch(criteria.ID)
            If List.Length > 0 Then
                Me.LoadObjectData(List(0))
            Else
                Throw New System.Security.SecurityException("Invoice doesn't exists")
            End If
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal Struct As DAClsappAdvertiserProjectInvoices.Struct)
            Me.LoadObjectData(Struct)
        End Sub

        ''' <summary>
        ''' Load the data of the object with the fetched data
        ''' </summary>
        ''' <param name="Struct">Struct with the data</param>
        ''' <remarks></remarks>
        Private Sub LoadObjectData(ByVal Struct As DAClsappAdvertiserProjectInvoices.Struct)
            With Struct
                Me.mID = .ID.Value
                Me.mProject = ClsProjectInfo.GetProjectInfo(.IDAdvertiserProject.Value)
                Me.mInvoiceNumber = .InvoiceNumber.Value
                Me.mInvoiceSequence = .InvoiceSequence.Value
                Me.mInvoiceDate = .InvoiceDate.Value
                Me.mTotalAmountDue = .TotalAmountDue.Value
            End With
        End Sub

#End Region

    End Class
End Namespace