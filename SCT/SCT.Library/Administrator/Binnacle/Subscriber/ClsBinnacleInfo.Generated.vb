Imports Csla
Imports SCT.DataAccess

Namespace Subscriber
    <Serializable()> Public Class ClsBinnacleInfo
        Inherits ReadOnlyBase(Of ClsBinnacleInfo)

#Region " Business Methods "

        Private mID As Long
        Private mUser As ClsAccountInfo = ClsAccountInfo.NewAccountInfo
        Private mBDate As Date

        Public ReadOnly Property ID() As Long
            Get
                Return Me.mID
            End Get
        End Property

        Public ReadOnly Property User() As ClsAccountInfo
            Get
                Return Me.mUser
            End Get
        End Property

        Public ReadOnly Property BDate() As DateTime
            Get
                Return Me.mBDate
            End Get
        End Property

        Protected Overrides Function GetIdValue() As Object
            Return Me.mID
        End Function

        Public Overrides Function ToString() As String
            Return Me.mID
        End Function

        Public Function ToListItem() As System.Web.UI.WebControls.ListItem
            Return New System.Web.UI.WebControls.ListItem(Me.ToString, Me.mID)
        End Function

        Public Function ToArray() As Object()
            Dim Array() As Object = {Me.mID, Me.mUser, Me.mBDate}
            Return Array
        End Function

        Public Function GetBinnacle() As ClsBinnacle
            Return ClsBinnacle.GetBinnacle(Me.mID)
        End Function

#End Region

#Region " Authorization Rules "

        'Protected Overrides Sub AddAuthorizationRules()
        '    AuthorizationRules.AllowRead("ID", " ")
        '    AuthorizationRules.AllowRead("IDUser", " ")
        '    AuthorizationRules.AllowRead("BDate", " ")
        'End Sub

        Public Shared Function CanGetObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole(" ")
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function NewBinnacleInfo() As ClsBinnacleInfo
            Return New ClsBinnacleInfo
        End Function

        Public Shared Function GetBinnacleInfo(ByVal ID As Long) As ClsBinnacleInfo
            Return DataPortal.Fetch(Of ClsBinnacleInfo)(New IDCriteria(ID))
        End Function

        Public Shared Function GetBinnacleInfo(ByVal Struct As DAClsprgSubscriberBinnacle.Struct) As ClsBinnacleInfo
            Return DataPortal.Fetch(Of ClsBinnacleInfo)(Struct)
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

#End Region

#Region " Data Access "

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
            Dim List As DAClsprgSubscriberBinnacle.Struct() = DAClsprgSubscriberBinnacle.Fetch(New Parameter(Of Long)(criteria.ID, 0))
            If List.Length > 0 Then
                Me.LoadObjectData(List(0))
            Else
                Throw New System.Security.SecurityException("Binnacle record doesn't exists")
            End If
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal Struct As DAClsprgSubscriberBinnacle.Struct)
            Me.LoadObjectData(Struct)
        End Sub

        ''' <summary>
        ''' Load the data of the object with the fetched data
        ''' </summary>
        ''' <param name="Struct">Struct with the data</param>
        ''' <remarks></remarks>
        Private Sub LoadObjectData(ByVal Struct As DAClsprgSubscriberBinnacle.Struct)
            With Struct
                Me.mID = .ID.Value
                Me.mUser = ClsAccountInfo.GetAccountInfo(.IDUser.Value)
                Me.mBDate = .BDate.Value
            End With
        End Sub

#End Region

    End Class
End Namespace
