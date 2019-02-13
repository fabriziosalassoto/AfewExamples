Imports Csla
Imports SCT.DataAccess

Namespace Advertiser.Project
    <Serializable()> Public Class ClsPriceInfo
        Inherits ReadOnlyBase(Of ClsPriceInfo)

#Region " Business Methods "

        Private mID As Long
        Private mProject As ClsProjectInfo = ClsProjectInfo.NewProjectInfo
        Private mCostPerClickThrough As Double
        Private mCostPerDisplay As Double

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

        Public ReadOnly Property CostPerClickThrough() As Double
            Get
                Return Me.mCostPerClickThrough
            End Get
        End Property

        Public ReadOnly Property CostPerDisplay() As Double
            Get
                Return Me.mCostPerDisplay
            End Get
        End Property

        Protected Overrides Function GetIdValue() As Object
            Return Me.mID
        End Function

        Public Overrides Function ToString() As String
            Return Me.mProject.ProjectDescription
        End Function

        Public Function ToListItem() As System.Web.UI.WebControls.ListItem
            Return New System.Web.UI.WebControls.ListItem(Me.ToString, Me.mID)
        End Function

        Public Function GetPrice() As ClsPrice
            Return ClsPrice.GetPrice(Me.mID)
        End Function

#End Region

#Region " Authorization Rules "

        'Protected Overrides Sub AddAuthorizationRules()
        '    AuthorizationRules.AllowRead("ID", " ")
        '    AuthorizationRules.AllowRead("Project", " ")
        '    AuthorizationRules.AllowRead("CostPerClickThrough", " ")
        '    AuthorizationRules.AllowRead("CostPerDisplay", " ")
        'End Sub

        Public Shared Function CanGetObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole(" ")
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function NewPriceInfo() As ClsPriceInfo
            Return New ClsPriceInfo
        End Function

        Public Shared Function GetPriceInfo(ByVal ID As Long) As ClsPriceInfo
            Return DataPortal.Fetch(Of ClsPriceInfo)(New Criteria(ID))
        End Function

        Public Shared Function GetPriceInfo(ByVal Struct As DAClsappAdvertiserProjectPriceInfo.Struct) As ClsPriceInfo
            Return DataPortal.Fetch(Of ClsPriceInfo)(Struct)
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
            Dim List As DAClsappAdvertiserProjectPriceInfo.Struct() = DAClsappAdvertiserProjectPriceInfo.Fetch(criteria.ID)
            If List.Length > 0 Then
                Me.LoadObjectData(List(0))
            Else
                Throw New System.Security.SecurityException("Project price information doesn't exist")
            End If
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal Struct As DAClsappAdvertiserProjectPriceInfo.Struct)
            Me.LoadObjectData(Struct)
        End Sub

        ''' <summary>
        ''' Load the data of the object with the fetched data
        ''' </summary>
        ''' <param name="Struct">Struct with the data</param>
        ''' <remarks></remarks>
        Private Sub LoadObjectData(ByVal Struct As DAClsappAdvertiserProjectPriceInfo.Struct)
            With Struct
                Me.mID = .ID.Value
                Me.mProject = ClsProjectInfo.GetProjectInfo(.IDAdvertiserProject.Value)
                Me.mCostPerClickThrough = .CostPerClickThrough.Value
                Me.mCostPerDisplay = .CostPerDisplay.Value
            End With
        End Sub

#End Region

    End Class
End Namespace

