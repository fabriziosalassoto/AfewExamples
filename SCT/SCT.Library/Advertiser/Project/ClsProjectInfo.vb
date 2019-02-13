Imports Csla
Imports SCT.DataAccess

Namespace Advertiser
    <Serializable()> Public Class ClsProjectInfo
        Inherits ReadOnlyBase(Of ClsProjectInfo)

#Region " Business Methods "

        Private mID As Long
        Private mAdvertiser As ClsAccountInfo = ClsAccountInfo.NewAccountInfo
        Private mContact As ClsContactInfo = ClsContactInfo.NewContactInfo
        Private mProjectDescription As String = String.Empty
        Private mADUrl As String = String.Empty

        Public ReadOnly Property ID() As Long
            Get
                CanReadProperty(True)
                Return Me.mID
            End Get
        End Property

        Public ReadOnly Property Advertiser() As ClsAccountInfo
            Get
                CanReadProperty(True)
                Return Me.mAdvertiser
            End Get
        End Property

        Public ReadOnly Property Contact() As ClsContactInfo
            Get
                CanReadProperty(True)
                Return Me.mContact
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

        Protected Overrides Function GetIdValue() As Object
            Return Me.mID
        End Function

        Public Overrides Function ToString() As String
            Return Me.mProjectDescription
        End Function

        Public Function ToListItem() As System.Web.UI.WebControls.ListItem
            Return New System.Web.UI.WebControls.ListItem(Me.mProjectDescription, Me.mID)
        End Function

        Public Function GetProject() As ClsProject
            Return ClsProject.GetProject(Me.mID)
        End Function

#End Region

#Region " Authorization Rules "

        'Protected Overrides Sub AddAuthorizationRules()
        '    AuthorizationRules.AllowRead("ID", "")
        '    AuthorizationRules.AllowRead("ProjectDescription", "")
        '    AuthorizationRules.AllowRead("ADUrl", "")
        'End Sub

        Public Shared Function CanGetObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole("")
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function NewProjectInfo() As ClsProjectInfo
            Return New ClsProjectInfo()
        End Function

        Public Shared Function GetProjectInfo(ByVal id As Long) As ClsProjectInfo
            Return DataPortal.Fetch(Of ClsProjectInfo)(New Criteria(id))
        End Function

        Public Shared Function GetProjectInfo(ByVal Struct As DAClsappAdvertiserProjects.Struct) As ClsProjectInfo
            Return DataPortal.Fetch(Of ClsProjectInfo)(Struct)
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

            Public Sub New(ByVal pID As Long)
                Me.mID = pID
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            Dim List As DAClsappAdvertiserProjects.Struct() = DAClsappAdvertiserProjects.Fetch(criteria.ID)
            If List.Length > 0 Then
                Me.LoadObjectData(List(0))
            Else
                Throw New System.Security.SecurityException("Project doesn't exist")
            End If
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal Struct As DAClsappAdvertiserProjects.Struct)
            Me.LoadObjectData(Struct)
        End Sub

        ''' <summary>
        ''' Load the data of the object with the fetched data
        ''' </summary>
        ''' <param name="Struct">Struct with the data</param>
        ''' <remarks></remarks>
        Private Sub LoadObjectData(ByVal Struct As DAClsappAdvertiserProjects.Struct)
            With Struct
                Me.mID = .ID.Value
                Me.mAdvertiser = ClsAccountInfo.GetAccountInfo(.IDAdvertiser.Value)
                Me.mContact = ClsContactInfo.GetContactInfo(.IDAdvertiserContact.Value)
                Me.mProjectDescription = .ProjectDescription.Value
                Me.mADUrl = .ADUrl.Value
            End With
        End Sub

#End Region

    End Class
End Namespace