Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsBinnacleFormFieldEntry
    Inherits ReadOnlyBase(Of ClsBinnacleFormFieldEntry)

#Region " Business Methods "

    Private mID As Long
    Private mBinnacleFormEntry As ClsBinnacleFormEntryInfo = ClsBinnacleFormEntryInfo.NewBinnacleFormEntryInfo
    Private mField As ClsFieldInfo = ClsFieldInfo.NewFieldInfo
    Private mLog As Logs
    Private mOldValue As String
    Private mNewValue As String

    Public ReadOnly Property ID() As Long
        Get
            Return Me.mID
        End Get
    End Property

    Public ReadOnly Property BinnacleFormEntry() As ClsBinnacleFormEntryInfo
        Get
            Return Me.mBinnacleFormEntry
        End Get
    End Property

    Public ReadOnly Property Field() As ClsFieldInfo
        Get
            Return Me.mField
        End Get
    End Property

    Public ReadOnly Property Log() As Logs
        Get
            Return Me.mLog
        End Get
    End Property

    Public ReadOnly Property OldValue() As String
        Get
            Return Me.mOldValue
        End Get
    End Property

    Public ReadOnly Property NewValue() As String
        Get
            Return Me.mNewValue
        End Get
    End Property

    Protected Overrides Function GetIdValue() As Object
        Return Me.mID
    End Function

    Public Overrides Function ToString() As String
        Return Me.mField.Description
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

    Public Shared Function NewBinnacleFormFieldEntry() As ClsBinnacleFormFieldEntry
        Return New ClsBinnacleFormFieldEntry
    End Function

    Public Shared Function GetBinnacleFormFieldEntry(ByVal Log As Logs, ByVal ID As Long) As ClsBinnacleFormFieldEntry
        Return DataPortal.Fetch(Of ClsBinnacleFormFieldEntry)(New IDCriteria(ID, Log))
    End Function

    Public Shared Function GetBinnacleFormFieldEntry(ByVal Struct As DAClsprgBinnacleFormFieldEntry.Struct) As ClsBinnacleFormFieldEntry
        Return DataPortal.Fetch(Of ClsBinnacleFormFieldEntry)(Struct)
    End Function

    Private Sub New()
        ' Require use of factory methods
    End Sub

#End Region

#Region " Data Access "

    Private mStruct As DAClsprgBinnacleFormFieldEntry.Struct = New DAClsprgBinnacleFormFieldEntry.Struct

    Public Function GetTableStruct() As DAClsprgBinnacleFormFieldEntry.Struct
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
        Dim List As DAClsprgBinnacleFormFieldEntry.Struct() = DAClsprgBinnacleFormFieldEntry.Fetch(criteria.Log, New Parameter(Of Long)(criteria.ID, 0))
        If List.Length > 0 Then
            Me.mStruct = List(0)
            Me.LoadObjectData()
        Else
            Throw New System.Security.SecurityException("Log Entry Field record doesn't exists")
        End If
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal struct As DAClsprgBinnacleFormFieldEntry.Struct)
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
            Me.mBinnacleFormEntry = ClsBinnacleFormEntryInfo.GetBinnacleFormEntryInfo(.Log.Value, .IDBinnacleForm.Value)
            Me.mField = ClsFieldInfo.GetFieldInfo(.IDField.Value)
            Me.mLog = .Log.Value
            Me.mOldValue = .OldValue.Value
            Me.mNewValue = .NewValue.Value
        End With
    End Sub

#End Region

End Class