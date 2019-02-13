Imports Csla
Imports SCT.DataAccess

Namespace Advertiser.Project
    <Serializable()> Public Class ClsDemographicInfo
        Inherits ReadOnlyBase(Of ClsDemographicInfo)

#Region " Business Methods "

        Private mID As Long
        Private mProject As ClsProjectInfo = ClsProjectInfo.NewProjectInfo
        Private mDemographicTag As String = String.Empty
        Private mDemographicRequirement As String = String.Empty

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

        Public ReadOnly Property DemographicTag() As String
            Get
                Return Me.mDemographicTag
            End Get
        End Property

        Public ReadOnly Property DemographicRequirement() As String
            Get
                Return Me.mDemographicRequirement
            End Get
        End Property

        Protected Overrides Function GetIdValue() As Object
            Return Me.mID
        End Function

        Public Overrides Function ToString() As String
            Return Me.mDemographicTag
        End Function

        Public Function ToListItem() As System.Web.UI.WebControls.ListItem
            Return New System.Web.UI.WebControls.ListItem(Me.ToString, Me.mID)
        End Function

        Public Function GetDemographic() As ClsDemographic
            Return ClsDemographic.GetDemographic(Me.mID)
        End Function

#End Region

#Region " Authorization Rules "

        'Protected Overrides Sub AddAuthorizationRules()
        '    AuthorizationRules.AllowRead("ID", " ")
        '    AuthorizationRules.AllowRead("Project", " ")
        '    AuthorizationRules.AllowRead("DemographicTag", " ")
        '    AuthorizationRules.AllowRead("DemographicRequirement", " ")
        'End Sub

        Public Shared Function CanGetObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole(" ")
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function NewDemographicInfo() As ClsDemographicInfo
            Return New ClsDemographicInfo
        End Function

        Public Shared Function GetDemographicInfo(ByVal ID As Long) As ClsDemographicInfo
            Return DataPortal.Fetch(Of ClsDemographicInfo)(New Criteria(ID))
        End Function

        Public Shared Function GetDemographicInfo(ByVal Struct As DAClsappAdvertiserDemographics.Struct) As ClsDemographicInfo
            Return DataPortal.Fetch(Of ClsDemographicInfo)(Struct)
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
            Dim List As DAClsappAdvertiserDemographics.Struct() = DAClsappAdvertiserDemographics.Fetch(criteria.ID)
            If List.Length > 0 Then
                Me.LoadObjectData(List(0))
            Else
                Throw New System.Security.SecurityException("Advertiser Demagraphic information doesn't exist")
            End If
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal Struct As DAClsappAdvertiserDemographics.Struct)
            Me.LoadObjectData(Struct)
        End Sub

        ''' <summary>
        ''' Load the data of the object with the fetched data
        ''' </summary>
        ''' <param name="Struct">Struct with the data</param>
        ''' <remarks></remarks>
        Private Sub LoadObjectData(ByVal Struct As DAClsappAdvertiserDemographics.Struct)
            With Struct
                Me.mID = .ID.Value
                Me.mProject = ClsProjectInfo.GetProjectInfo(.IDAdvertiserProject.Value)
                Me.mDemographicTag = .DemographicTag.Value
                Me.mDemographicRequirement = .DemographicRequirement.Value
            End With
        End Sub

#End Region

    End Class
End Namespace

