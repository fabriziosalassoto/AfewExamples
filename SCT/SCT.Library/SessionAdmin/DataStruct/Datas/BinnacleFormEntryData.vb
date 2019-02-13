Option Strict On

<Serializable()> Public Class BinnacleFormEntryData

    Private mID As Long
    Private mUser As New BinnacleUserData
    Private mForm As New FormData
    Private mOperation As New OperationData
    Private mLog As DataAccess.Logs
    Private mBDate As New Date
    Private mBHour As New Date
    Private mBinnacleFormFields As New Generic.List(Of BinnacleFormFieldEntryData)

    Public Property ID() As Long
        Get
            Return Me.mID
        End Get
        Set(ByVal value As Long)
            Me.mID = value
        End Set
    End Property

    Public Property User() As BinnacleUserData
        Get
            Return Me.mUser
        End Get
        Set(ByVal value As BinnacleUserData)
            Me.mUser = value
        End Set
    End Property

    Public Property Form() As FormData
        Get
            Return Me.mForm
        End Get
        Set(ByVal value As FormData)
            Me.mForm = value
        End Set
    End Property

    Public Property Operation() As OperationData
        Get
            Return Me.mOperation
        End Get
        Set(ByVal value As OperationData)
            Me.mOperation = value
        End Set
    End Property

    Public Property Log() As DataAccess.Logs
        Get
            Return Me.mLog
        End Get
        Set(ByVal value As DataAccess.Logs)
            Me.mLog = value
        End Set
    End Property

    Public Property BDate() As Date
        Get
            Return Me.mBDate
        End Get
        Set(ByVal value As Date)
            Me.mBDate = value
        End Set
    End Property

    Public Property BHour() As Date
        Get
            Return Me.mBHour
        End Get
        Set(ByVal value As Date)
            Me.mBHour = value
        End Set
    End Property

    Public Sub AddBinnacleFormFields(ByVal binnacleFormField As BinnacleFormFieldEntryData)
        Me.mBinnacleFormFields.Add(BinnacleFormField)
    End Sub

    Public Property BinnacleFormFields() As BinnacleFormFieldEntryData()
        Get
            Return Me.mBinnacleFormFields.ToArray
        End Get
        Set(ByVal value As BinnacleFormFieldEntryData())
            Me.mBinnacleFormFields = New Generic.List(Of BinnacleFormFieldEntryData)(value)
        End Set
    End Property

End Class
