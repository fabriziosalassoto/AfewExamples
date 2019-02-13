Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsBinnacleFormEntry
    Inherits ReadOnlyBase(Of ClsBinnacleFormEntry)

#Region " Business Methods "

    Private mID As Long
    Private mUser As ClsBinnacleUserInfo = ClsBinnacleUserInfo.NewBinnacleUserInfo
    Private mForm As ClsFormInfo = ClsFormInfo.NewFormInfo
    Private mOperation As ClsOperationInfo = ClsOperationInfo.NewOperationInfo
    Private mLog As Logs
    Private mBDate As Date
    Private mBHour As Date
    Private mBinnacleFormEntryFields As ClsBinnacleFormFieldEntryList = ClsBinnacleFormFieldEntryList.NewBinnacleFormFieldEntryList

    Public ReadOnly Property ID() As Long
        Get
            Return Me.mID
        End Get
    End Property

    Public ReadOnly Property User() As ClsBinnacleUserInfo
        Get
            Return Me.mUser
        End Get
    End Property

    Public ReadOnly Property Form() As ClsFormInfo
        Get
            Return Me.mForm
        End Get
    End Property

    Public ReadOnly Property Operation() As ClsOperationInfo
        Get
            Return Me.mOperation
        End Get
    End Property

    Public ReadOnly Property Log() As Logs
        Get
            Return Me.mLog
        End Get
    End Property

    Public ReadOnly Property BDate() As Date
        Get
            Return Me.mBDate
        End Get
    End Property

    Public ReadOnly Property BHour() As Date
        Get
            Return Me.mBHour
        End Get
    End Property

    Public ReadOnly Property BinnacleFormEntryFields() As ClsBinnacleFormFieldEntryList
        Get
            Return Me.mBinnacleFormEntryFields
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

    Public Shared Function NewBinnacleFormEntry() As ClsBinnacleFormEntry
        Return New ClsBinnacleFormEntry
    End Function

    Public Shared Function GetBinnacleFormEntry(ByVal Log As Logs, ByVal ID As Long) As ClsBinnacleFormEntry
        Return DataPortal.Fetch(Of ClsBinnacleFormEntry)(New IDCriteria(ID, Log))
    End Function

    Public Shared Function GetBinnacleFormEntry(ByVal Struct As DAClsprgBinnacleFormEntry.Struct) As ClsBinnacleFormEntry
        Return DataPortal.Fetch(Of ClsBinnacleFormEntry)(Struct)
    End Function

    Private Sub New()
        ' Require use of factory methods
    End Sub

#End Region

#Region " Data Access "

    Private mStruct As DAClsprgBinnacleFormEntry.Struct = New DAClsprgBinnacleFormEntry.Struct

    Public Function GetTableStruct() As DAClsprgBinnacleFormEntry.Struct
        Return Me.mStruct
    End Function

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
            Me.mStruct = List(0)
            Me.LoadObjectData()
            Me.LoadObjectChildData()
        Else
            Throw New System.Security.SecurityException("Log Entry doesn't exists")
        End If
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal struct As DAClsprgBinnacleFormEntry.Struct)
        Me.mStruct = struct
        Me.LoadObjectData()
    End Sub

    ''' <summary>
    ''' Load the data of the object with the fetched data
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadObjectChildData()
        Me.mBinnacleFormEntryFields = ClsBinnacleFormFieldEntryList.GetBinnacleFormFieldEntryList(DAClsprgBinnacleFormFieldEntry.FetchByBinnacleFormEntry(Me.mStruct.Log.Value, New Parameter(Of Long)(Me.mStruct.ID.Value, 0)))
    End Sub

    ''' <summary>
    ''' Load the data of the object with the fetched data
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadObjectData()
        With Me.mStruct
            Me.mID = .ID.Value
            Me.mLog = .Log.Value
            Me.mUser = ClsBinnacleUserInfo.GetBinnacleUserInfo(.Log.Value, .IDUser.Value)
            Me.mForm = ClsFormInfo.GetFormInfo(.IDForm.Value)
            Me.mOperation = ClsOperationInfo.GetOperationInfo(.IDOperation.Value)
            Me.mBDate = .BDate.Value
            Me.mBHour = .BHour.Value
        End With
    End Sub

#End Region

End Class