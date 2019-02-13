Option Strict On

<Serializable()> Public Class BinnacleFormFieldEntryData

    Private mID As Long
    Private mBinnacleForm As New BinnacleFormData
    Private mField As New FieldData
    Private mLog As DataAccess.Logs
    Private mOldValue As String = String.Empty
    Private mNewValue As String = String.Empty

    Public Property ID() As Long
        Get
            Return Me.mID
        End Get
        Set(ByVal value As Long)
            Me.mID = value
        End Set
    End Property

    Public Property BinnacleForm() As BinnacleFormData
        Get
            Return Me.mBinnacleForm
        End Get
        Set(ByVal value As BinnacleFormData)
            Me.mBinnacleForm = value
        End Set
    End Property

    Public Property Field() As FieldData
        Get
            Return Me.mField
        End Get
        Set(ByVal value As FieldData)
            Me.mField = value
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

    Public Property OldValue() As String
        Get
            Return Me.mOldValue
        End Get
        Set(ByVal value As String)
            Me.mOldValue = value
        End Set
    End Property

    Public Property NewValue() As String
        Get
            Return Me.mNewValue
        End Get
        Set(ByVal value As String)
            Me.mNewValue = value
        End Set
    End Property

End Class
