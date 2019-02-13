Imports Csla
Imports SCT.DataAccess

Namespace Advertiser
    <Serializable()> Public Class ClsAdHistoryInfo
        Inherits ReadOnlyBase(Of ClsAdHistoryInfo)

#Region " Business Methods "

        Private mID As Long
        Private mProject As Advertiser.ClsProjectInfo = Advertiser.ClsProjectInfo.NewProjectInfo
        Private mSubAccount As Subscriber.ClsAccountInfo = Subscriber.ClsAccountInfo.NewAccountInfo
        Private mDateAdDisplay As Date = New Date(1900, 1, 1)
        Private mTimeAdDisplay As Date = New Date(1900, 1, 1, 0, 0, 0)
        Private mDateAdClickThru As Date = New Date(1900, 1, 1)
        Private mTimeAdClickThru As Date = New Date(1900, 1, 1, 0, 0, 0)
        Private mURLAdDisplay As String = String.Empty
        Private mURLAdClickThru As String = String.Empty

        Public ReadOnly Property ID() As Long
            Get
                CanReadProperty(True)
                Return Me.mID
            End Get
        End Property

        Public ReadOnly Property Project() As Advertiser.ClsProjectInfo
            Get
                CanReadProperty(True)
                Return Me.mProject
            End Get
        End Property

        Public ReadOnly Property SubAccount() As Subscriber.ClsAccountInfo
            Get
                CanReadProperty(True)
                Return Me.mSubAccount
            End Get
        End Property

        Public ReadOnly Property DateAdDisplay() As Date
            Get
                CanReadProperty(True)
                Return Me.mDateAdDisplay
            End Get
        End Property

        Public ReadOnly Property TimeAdDisplay() As Date
            Get
                CanReadProperty(True)
                Return Me.mTimeAdDisplay
            End Get
        End Property

        Public ReadOnly Property DateAdClickThru() As Date
            Get
                CanReadProperty(True)
                Return Me.mDateAdClickThru
            End Get
        End Property

        Public ReadOnly Property TimeAdClickThru() As Date
            Get
                CanReadProperty(True)
                Return Me.mTimeAdClickThru
            End Get
        End Property

        Public ReadOnly Property URLAdDisplay() As String
            Get
                CanReadProperty(True)
                Return Me.mURLAdDisplay
            End Get
        End Property

        Public ReadOnly Property URLAdClickThru() As String
            Get
                CanReadProperty(True)
                Return Me.mURLAdClickThru
            End Get
        End Property

        Protected Overrides Function GetIdValue() As Object
            Return Me.mID
        End Function

        Public Overrides Function ToString() As String
            Return Me.mURLAdDisplay
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

        Public Shared Function NewAdHistoryInfo() As ClsAdHistoryInfo
            Return New ClsAdHistoryInfo
        End Function

        Public Shared Function GetAdHistoryInfo(ByVal ID As Long) As ClsAdHistoryInfo
            Return DataPortal.Fetch(Of ClsAdHistoryInfo)(New IDCriteria(ID))
        End Function

        Public Shared Function GetAdHistoryInfo(ByVal Struct As DAClsappAdvertiserProjectHistory.Struct) As ClsAdHistoryInfo
            Return DataPortal.Fetch(Of ClsAdHistoryInfo)(Struct)
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        Private mStruct As DAClsappAdvertiserProjectHistory.Struct = New DAClsappAdvertiserProjectHistory.Struct

        Public Function GetTableStruct() As DAClsappAdvertiserProjectHistory.Struct
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
            Dim List As DAClsappAdvertiserProjectHistory.Struct() = DAClsappAdvertiserProjectHistory.Fetch(criteria.ID)
            If List.Length > 0 Then
                Me.mStruct = List(0)
                Me.LoadObjectData()
            Else
                Throw New System.Security.SecurityException("AdHistory Record doesn't exists")
            End If
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal struct As DAClsappAdvertiserProjectHistory.Struct)
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
                Me.mProject = Advertiser.ClsProjectInfo.GetProjectInfo(.IDProject.Value)
                Me.mSubAccount = Subscriber.ClsAccountInfo.GetAccountInfo(.IDSubscriber.Value)
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
