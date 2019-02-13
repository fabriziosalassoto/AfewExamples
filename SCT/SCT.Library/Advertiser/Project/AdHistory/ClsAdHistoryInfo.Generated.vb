Imports Csla
Imports SCT.DataAccess

Namespace Advertiser.Project
    <Serializable()> Public Class ClsAdHistoryInfo
        Inherits ReadOnlyBase(Of ClsAdHistoryInfo)

#Region " Business Methods "

        Private mID As Long
        Private mProject As Advertiser.ClsProjectInfo = Advertiser.ClsProjectInfo.NewProjectInfo
        Private mSubscriberAccount As Subscriber.ClsAccountInfo = Subscriber.ClsAccountInfo.NewAccountInfo
        Private mDateAdDisplay As Date
        Private mTimeAdDisplay As Date
        Private mDateAdClickThru As Date
        Private mTimeAdClickThru As Date
        Private mURLAdDisplay As String = String.Empty
        Private mURLAdClickThru As String = String.Empty

        Public ReadOnly Property ID() As Long
            Get
                Return Me.mID
            End Get
        End Property

        Public ReadOnly Property Project() As Advertiser.ClsProjectInfo
            Get
                Return Me.mProject
            End Get
        End Property

        Public ReadOnly Property SubscriberAccount() As Subscriber.ClsAccountInfo
            Get
                Return Me.mSubscriberAccount
            End Get
        End Property

        Public ReadOnly Property DateAdDisplay() As DateTime
            Get
                Return Me.mDateAdDisplay
            End Get
        End Property

        Public ReadOnly Property TimeAdDisplay() As DateTime
            Get
                Return Me.mTimeAdDisplay
            End Get
        End Property

        Public ReadOnly Property DateAdClickThru() As DateTime
            Get
                Return Me.mDateAdClickThru
            End Get
        End Property

        Public ReadOnly Property TimeAdClickThru() As DateTime
            Get
                Return Me.mTimeAdClickThru
            End Get
        End Property

        Public ReadOnly Property URLAdDisplay() As String
            Get
                Return Me.mURLAdDisplay
            End Get
        End Property

        Public ReadOnly Property URLAdClickThru() As String
            Get
                Return Me.mURLAdClickThru
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

        Public Function GetAdHistory() As ClsAdHistory
            Return ClsAdHistory.GetAdHistory(Me.mID)
        End Function

#End Region

#Region " Authorization Rules "

        'Protected Overrides Sub AddAuthorizationRules()
        '    AuthorizationRules.AllowRead("ID", " ")
        '    AuthorizationRules.AllowRead("Project", " ")
        '    AuthorizationRules.AllowRead("DateAdDisplay", " ")
        '    AuthorizationRules.AllowRead("TimeAdDisplay", " ")
        '    AuthorizationRules.AllowRead("DateAdClickThru", " ")
        '    AuthorizationRules.AllowRead("TimeAdClickThru", " ")
        '    AuthorizationRules.AllowRead("URLAdDisplay", " ")
        '    AuthorizationRules.AllowRead("URLAdClickThru", " ")
        '    AuthorizationRules.AllowRead("SubscriberAccount", " ")
        'End Sub

        Public Shared Function CanGetObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole(" ")
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function NewAdHistoryInfo() As ClsAdHistoryInfo
            Return New ClsAdHistoryInfo
        End Function

        Public Shared Function GetAdHistoryInfo(ByVal ID As Long) As ClsAdHistoryInfo
            Return DataPortal.Fetch(Of ClsAdHistoryInfo)(New Criteria(ID))
        End Function

        Public Shared Function GetAdHistoryInfo(ByVal Struct As DAClsappAdvertiserAdHistory.Struct) As ClsAdHistoryInfo
            Return DataPortal.Fetch(Of ClsAdHistoryInfo)(Struct)
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
            Dim List As DAClsappAdvertiserAdHistory.Struct() = DAClsappAdvertiserAdHistory.Fetch(criteria.ID)
            If List.Length > 0 Then
                Me.LoadObjectData(List(0))
            Else
                Throw New System.Security.SecurityException("AdHistory Record doesn't exists")
            End If
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal Struct As DAClsappAdvertiserAdHistory.Struct)
            Me.LoadObjectData(Struct)
        End Sub

        ''' <summary>
        ''' Load the data of the object with the fetched data
        ''' </summary>
        ''' <param name="Struct">Struct with the data</param>
        ''' <remarks></remarks>
        Private Sub LoadObjectData(ByVal Struct As DAClsappAdvertiserAdHistory.Struct)
            With Struct
                Me.mID = .ID.Value
                Me.mProject = Advertiser.ClsProjectInfo.GetProjectInfo(.IDProject.Value)
                Me.mSubscriberAccount = Subscriber.ClsAccountInfo.GetAccountInfo(.IDSubscriber.Value)
                Me.mDateAdDisplay = .DateAdDisplay.Value
                Me.mTimeAdDisplay = .TimeAdDisplay.Value
                Me.mDateAdClickThru = .DateAdClickThru.Value
                Me.mTimeAdClickThru = .TimeAdClickThru.Value
                Me.mURLAdDisplay = .URLAdDisplay.Value
                Me.mURLAdClickThru = .URLAdClickThru.Value
            End With
        End Sub

#End Region

    End Class
End Namespace

