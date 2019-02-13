Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsBinnacleFormEntryInfo
    Inherits ReadOnlyBase(Of ClsBinnacleFormEntryInfo)

#Region " Business Methods "

    Private mID As Long
    Private mForm As ClsFormInfo = ClsFormInfo.NewFormInfo

    Public ReadOnly Property ID() As Long
        Get
            Return Me.mID
        End Get
    End Property

    Public ReadOnly Property Form() As ClsFormInfo
        Get
            Return Me.mForm
        End Get
    End Property

    Protected Overrides Function GetIdValue() As Object
        Return Me.mID
    End Function

    Public Overrides Function ToString() As String
        Return Me.mForm.Description
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

    Public Shared Function NewBinnacleFormEntryInfo() As ClsBinnacleFormEntryInfo
        Return New ClsBinnacleFormEntryInfo
    End Function

    Public Shared Function GetBinnacleFormEntryInfo(ByVal Log As Logs, ByVal ID As Long) As ClsBinnacleFormEntryInfo
        Return DataPortal.Fetch(Of ClsBinnacleFormEntryInfo)(New IDCriteria(ID, Log))
    End Function

    Public Shared Function GetBinnacleFormEntryInfo(ByVal Struct As DAClsprgBinnacleFormEntry.Struct) As ClsBinnacleFormEntryInfo
        Return DataPortal.Fetch(Of ClsBinnacleFormEntryInfo)(Struct)
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
        Dim List As DAClsprgBinnacleFormEntry.Struct() = DAClsprgBinnacleFormEntry.Fetch(criteria.Log, New Parameter(Of Long)(criteria.ID, 0))
        If List.Length > 0 Then
            Me.LoadObjectData(List(0))
        Else
            Throw New System.Security.SecurityException("Log Entry doesn't exists")
        End If
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal struct As DAClsprgBinnacleFormEntry.Struct)
        Me.LoadObjectData(struct)
    End Sub

    ''' <summary>
    ''' Load the data of the object with the fetched data
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadObjectData(ByVal struct As DAClsprgBinnacleFormEntry.Struct)
        With struct
            Me.mID = .ID.Value
            Me.mForm = ClsFormInfo.GetFormInfo(.IDForm.Value)
        End With
    End Sub

#End Region

End Class
