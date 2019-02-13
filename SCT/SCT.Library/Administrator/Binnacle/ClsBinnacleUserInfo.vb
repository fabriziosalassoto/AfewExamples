Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsBinnacleUserInfo
    Inherits ReadOnlyBase(Of ClsBinnacleUserInfo)

#Region " Business Methods "

    Private mID As Long
    Private mName As String = String.Empty

    Public ReadOnly Property ID() As Long
        Get
            Return Me.mID
        End Get
    End Property

    Public ReadOnly Property Name() As String
        Get
            Return Me.mName
        End Get
    End Property

    Protected Overrides Function GetIdValue() As Object
        Return Me.mID
    End Function

    Public Overrides Function ToString() As String
        Return Me.mName
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

    Public Shared Function NewBinnacleUserInfo() As ClsBinnacleUserInfo
        Return New ClsBinnacleUserInfo
    End Function

    Public Shared Function GetBinnacleUserInfo(ByVal Log As Logs, ByVal ID As Long) As ClsBinnacleUserInfo
        Return DataPortal.Fetch(Of ClsBinnacleUserInfo)(New IDCriteria(ID, Log))
    End Function

    Public Shared Function GetBinnacleUserInfo(ByVal Struct As DAClsprgBinnacleUsers.Struct) As ClsBinnacleUserInfo
        Return DataPortal.Fetch(Of ClsBinnacleUserInfo)(Struct)
    End Function

    Private Sub New()
        ' Require use of factory methods
    End Sub

#End Region

#Region " Data Access "

    <Serializable()> Private Class IDCriteria

        Private mID As Long
        Private mLog As Logs

        Public ReadOnly Property ID() As Long
            Get
                Return Me.mID
            End Get
        End Property

        Public ReadOnly Property Log() As Logs
            Get
                Return Me.mLog
            End Get
        End Property

        Public Sub New(ByVal ID As Long, ByVal Log As Logs)
            Me.mID = ID
            Me.mLog = Log
        End Sub

    End Class

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As IDCriteria)
        Dim List As DAClsprgBinnacleUsers.Struct() = DAClsprgBinnacleUsers.Fetch(criteria.Log, criteria.ID)
        If List.Length > 0 Then
            Me.LoadObjectData(List(0))
        Else
            Throw New System.Security.SecurityException("Binnacle User doesn't exist")
        End If
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal Struct As DAClsprgBinnacleUsers.Struct)
        Me.LoadObjectData(Struct)
    End Sub

    ''' <summary>
    ''' Load the data of the object with the fetched data
    ''' </summary>
    ''' <param name="Struct">Struct with the data</param>
    ''' <remarks></remarks>
    Private Sub LoadObjectData(ByVal Struct As DAClsprgBinnacleUsers.Struct)
        With Struct
            Me.mID = .ID.Value
            Me.mName = .Name.Value
        End With
    End Sub

#End Region

End Class