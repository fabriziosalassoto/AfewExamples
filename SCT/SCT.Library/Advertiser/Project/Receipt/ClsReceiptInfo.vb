Imports Csla
Imports SCT.DataAccess

Namespace Advertiser.Project
    <Serializable()> Public Class ClsReceiptInfo
        Inherits ReadOnlyBase(Of ClsReceiptInfo)

#Region " Business Methods "

        Private mID As Long
        Private mProject As ClsProjectInfo = ClsProjectInfo.NewProjectInfo
        Private mReceiptNumber As Decimal
        Private mPaymentDate As Date = New Date(1900, 1, 1)
        Private mTotalPaid As Double

        Public ReadOnly Property ID() As Long
            Get
                CanReadProperty(True)
                Return Me.mID
            End Get
        End Property

        Public ReadOnly Property Project() As ClsProjectInfo
            Get
                CanReadProperty(True)
                Return Me.mProject
            End Get
        End Property

        Public ReadOnly Property ReceiptNumber() As Decimal
            Get
                CanReadProperty(True)
                Return Me.mReceiptNumber
            End Get
        End Property

        Public ReadOnly Property PaymentDate() As DateTime
            Get
                CanReadProperty(True)
                Return Me.mPaymentDate
            End Get
        End Property

        Public ReadOnly Property TotalPaid() As Double
            Get
                CanReadProperty(True)
                Return Me.mTotalPaid
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

#End Region

#Region " Authorization Rules "

        'Protected Overrides Sub AddAuthorizationRules()
        'End Sub

        Public Shared Function CanGetObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole(" ")
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function NewReceiptInfo() As ClsReceiptInfo
            Return New ClsReceiptInfo
        End Function

        Public Shared Function GetReceiptInfo(ByVal IDProject As Long, ByVal IDReceipt As Long) As ClsReceiptInfo
            Return DataPortal.Fetch(Of ClsReceiptInfo)(New Criteria(IDProject, IDReceipt))
        End Function

        Public Shared Function GetReceiptInfo(ByVal Struct As DAClsappAdvertiserProjectReceipts.Struct) As ClsReceiptInfo
            Return DataPortal.Fetch(Of ClsReceiptInfo)(Struct)
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> Private Class Criteria

            Private mIDProject As Long
            Private mIDReceipt As Long

            Public ReadOnly Property IDProject() As Long
                Get
                    Return Me.mIDProject
                End Get
            End Property

            Public ReadOnly Property IDReceipt() As Long
                Get
                    Return Me.mIDReceipt
                End Get
            End Property

            Public Sub New(ByVal IDProject As Long, ByVal IDReceipt As Long)
                Me.mIDProject = IDProject
                Me.mIDReceipt = IDReceipt
            End Sub

        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            Dim List As DAClsappAdvertiserProjectReceipts.Struct() = DAClsappAdvertiserProjectReceipts.Fetch(criteria.IDProject, criteria.IDReceipt)
            If List.Length > 0 Then
                Me.LoadObjectData(List(0))
            Else
                Throw New System.Security.SecurityException("Receipt doesn't assign to Project")
            End If
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal Struct As DAClsappAdvertiserProjectReceipts.Struct)
            Me.LoadObjectData(Struct)
        End Sub

        ''' <summary>
        ''' Load the data of the object with the fetched data
        ''' </summary>
        ''' <param name="projectReceipt">Struct with the data</param>
        ''' <remarks></remarks>
        Private Sub LoadObjectData(ByVal projectReceipt As DAClsappAdvertiserProjectReceipts.Struct)
            Dim receipt As DAClsappAdvertiserReceipts.Struct() = DAClsappAdvertiserReceipts.Fetch(projectReceipt.IDAdvertiserReceipt.Value)
            If receipt.Length > 0 Then
                Me.mID = receipt(0).ID.Value
                Me.mReceiptNumber = receipt(0).ReceiptNumber.Value
                Me.mPaymentDate = receipt(0).PaymentDate.Value
                Me.mTotalPaid = projectReceipt.TotalPaid.Value
                Me.mProject = ClsProjectInfo.GetProjectInfo(projectReceipt.IDAdvertiserProject.Value)
            Else
                Throw New System.Security.SecurityException("Receipt doesn't assign to Project")
            End If
        End Sub

#End Region

    End Class
End Namespace

