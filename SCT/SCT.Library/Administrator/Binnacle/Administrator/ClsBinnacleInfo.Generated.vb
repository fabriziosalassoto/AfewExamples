Imports Csla
Imports SCT.DataAccess

Namespace Administrator
    <Serializable()> Public Class ClsBinnacleInfo
        Inherits ReadOnlyBase(Of ClsBinnacleInfo)

#Region " Business Methods "

        Private mID As Long
        Private mUser As ClsUserInfo = ClsUserInfo.NewUserInfo
        Private mBDate As Date

        Public ReadOnly Property ID() As Long
            Get
                Return Me.mID
            End Get
        End Property

        Public ReadOnly Property User() As ClsUserInfo
            Get
                Return Me.mUser
            End Get
        End Property

        Public ReadOnly Property BDate() As Date
            Get
                Return Me.mBDate
            End Get
        End Property

        Protected Overrides Function GetIdValue() As Object
            Return Me.mID
        End Function

        Public Overrides Function ToString() As String
            Return Me.mUser.FullName
        End Function

        Public Function ToListItem() As System.Web.UI.WebControls.ListItem
            Return New System.Web.UI.WebControls.ListItem(Me.ToString, Me.mID)
        End Function

        Public Function ToArray() As Object()
            Dim Array() As Object = {Me.mID, Me.mUser}
            Return Array
        End Function

        Public Function GetBinnacle() As ClsBinnacle
            Return ClsBinnacle.GetBinnacle(Me.mID)
        End Function

#End Region

#Region " Authorization Rules "

        'Protected Overrides Sub AddAuthorizationRules()
        '    AuthorizationRules.AllowRead("ID", " ")
        '    AuthorizationRules.AllowRead("User", " ")
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

        Public Shared Function GetBinnacleInfo(ByVal Struct As DAClsprgAdministrativeBinnacle.Struct) As ClsBinnacleInfo
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
            Dim List As DAClsprgAdministrativeBinnacle.Struct() = DAClsprgAdministrativeBinnacle.Fetch(New Parameter(Of Long)(criteria.ID, 0))
            If List.Length > 0 Then
                Me.LoadObjectData(List(0))
            Else
                Throw New System.Security.SecurityException("Binnacle record doesn't exists")
            End If
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal Struct As DAClsprgAdministrativeBinnacle.Struct)
            Me.LoadObjectData(Struct)
        End Sub

        ''' <summary>
        ''' Load the data of the object with the fetched data
        ''' </summary>
        ''' <param name="Struct">Struct with the data</param>
        ''' <remarks></remarks>
        Private Sub LoadObjectData(ByVal Struct As DAClsprgAdministrativeBinnacle.Struct)
            With Struct
                Me.mID = .ID.Value
                Me.mUser = ClsUserInfo.GetUserInfo(.IDUser.Value)
                Me.mBDate = .BDate.Value
            End With
        End Sub

#End Region

    End Class
End Namespace