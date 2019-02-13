Option Strict On

<Serializable()> Public Class BinnacleFormEntryNewData

    Private mID As New NewFieldData(Of Long)
    Private mUser As New BinnacleUserNewData
    Private mForm As New FormNewData
    Private mOperation As New OperationNewData
    Private mLog As New NewFieldData(Of DataAccess.Logs)
    Private mBDate As New NewFieldData(Of Date)
    Private mBHour As New NewFieldData(Of Date)

    Public Property ID() As NewFieldData(Of Long)
        Get
            Return Me.mID
        End Get
        Set(ByVal value As NewFieldData(Of Long))
            Me.mID = value
        End Set
    End Property

    Public Property User() As BinnacleUserNewData
        Get
            Return Me.mUser
        End Get
        Set(ByVal value As BinnacleUserNewData)
            Me.mUser = value
        End Set
    End Property

    Public Property Form() As FormNewData
        Get
            Return Me.mForm
        End Get
        Set(ByVal value As FormNewData)
            Me.mForm = value
        End Set
    End Property

    Public Property Operation() As OperationNewData
        Get
            Return Me.mOperation
        End Get
        Set(ByVal value As OperationNewData)
            Me.mOperation = value
        End Set
    End Property

    Public Property Log() As NewFieldData(Of DataAccess.Logs)
        Get
            Return Me.mLog
        End Get
        Set(ByVal value As NewFieldData(Of DataAccess.Logs))
            Me.mLog = value
        End Set
    End Property

    Public Property BDate() As NewFieldData(Of Date)
        Get
            Return Me.mBDate
        End Get
        Set(ByVal value As NewFieldData(Of Date))
            Me.mBHour = value
        End Set
    End Property

    Public Property BHour() As NewFieldData(Of Date)
        Get
            Return Me.mBHour
        End Get
        Set(ByVal value As NewFieldData(Of Date))
            Me.mBHour = value
        End Set
    End Property

End Class

