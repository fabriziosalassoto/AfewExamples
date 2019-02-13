Imports Csla
Imports SCT.DataAccess

Namespace Advertiser.Receipt
    <Serializable()> Public Class ClsProjectInfo
        Inherits ReadOnlyBase(Of ClsProjectInfo)

#Region " Business Methods "

        Private mID As Long
        Private mAdvertiserAccount As Advertiser.ClsAccountInfo = Advertiser.ClsAccountInfo.NewAccountInfo
        Private mContact As Advertiser.ClsContactInfo = Advertiser.ClsContactInfo.NewContactInfo
        Private mReceipt As ClsReceiptInfo = ClsReceiptInfo.NewReceiptInfo
        Private mProjectDescription As String = String.Empty
        Private mADUrl As String = String.Empty

        Public ReadOnly Property ID() As Long
            Get
                Return Me.mID
            End Get
        End Property

        Public ReadOnly Property AdvertiserAccount() As Advertiser.ClsAccountInfo
            Get
                Return Me.mAdvertiserAccount
            End Get
        End Property

        Public ReadOnly Property Contact() As Advertiser.ClsContactInfo
            Get
                Return Me.mContact
            End Get
        End Property

        Public ReadOnly Property Receipt() As ClsReceiptInfo
            Get
                Return Me.mReceipt
            End Get
        End Property

        Public ReadOnly Property ProjectDescription() As String
            Get
                Return Me.mProjectDescription
            End Get
        End Property

        Public ReadOnly Property ADUrl() As String
            Get
                Return Me.mADUrl
            End Get
        End Property

        Protected Overrides Function GetIdValue() As Object
            Return Me.mID
        End Function

        Public Overrides Function ToString() As String
            Return Me.mProjectDescription
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

        Public Shared Function NewReceiptProjectInfo() As ClsProjectInfo
            Return New ClsProjectInfo
        End Function

        Public Shared Function GetReceiptProjectInfo(ByVal IDProject As Long, ByVal IDReceipt As Long) As ClsProjectInfo
            Return DataPortal.Fetch(Of ClsProjectInfo)(New Criteria(IDProject, IDReceipt))
        End Function

        Public Shared Function GetReceiptProjectInfo(ByVal Struct As DAClsappAdvertiserProjectReceipts.Struct) As ClsProjectInfo
            Return DataPortal.Fetch(Of ClsProjectInfo)(Struct)
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
                Throw New System.Security.SecurityException("Project doesn't assign to Receipt")
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
            Dim project As DAClsappAdvertiserProjects.Struct() = DAClsappAdvertiserProjects.Fetch(projectReceipt.IDAdvertiserProject.Value)
            If project.Length > 0 Then
                Me.mID = project(0).ID.Value
                Me.mProjectDescription = project(0).ProjectDescription.Value
                Me.mADUrl = project(0).ADUrl.Value
                Me.mAdvertiserAccount = Advertiser.ClsAccountInfo.GetAccountInfo(project(0).IDAdvertiser.Value)
                Me.mContact = Advertiser.ClsContactInfo.GetContactInfo(project(0).IDAdvertiserContact.Value)
                Me.mReceipt = ClsReceiptInfo.GetReceiptInfo(projectReceipt.IDAdvertiserReceipt.Value)
            Else
                Throw New System.Security.SecurityException("Project doesn't assign to Receipt")
            End If
        End Sub

#End Region

    End Class
End Namespace