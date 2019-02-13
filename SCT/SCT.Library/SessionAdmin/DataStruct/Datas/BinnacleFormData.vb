Option Strict On

<Serializable()> Public Class BinnacleFormData

    Private mID As Long
    Private mBinnacle As New BinnacleData
    Private mForm As New FormData
    Private mOperation As New OperationData
    Private mBHour As New Date
    Private mBinnacleFormFields As New Generic.List(Of BinnacleFormFieldData)

    Public Property ID() As Long
        Get
            Return Me.mID
        End Get
        Set(ByVal value As Long)
            Me.mID = value
        End Set
    End Property

    Public Property Binnacle() As BinnacleData
        Get
            Return Me.mBinnacle
        End Get
        Set(ByVal value As BinnacleData)
            Me.mBinnacle = value
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

    Public Property BHour() As Date
        Get
            Return Me.mBHour
        End Get
        Set(ByVal value As Date)
            Me.mBHour = value
        End Set
    End Property

    Public Sub AddBinnacleFormField(ByVal binnacleFormField As BinnacleFormFieldData)
        Me.mBinnacleFormFields.Add(binnacleFormField)
    End Sub

    Public Property BinnacleFormField() As BinnacleFormFieldData()
        Get
            Return Me.mBinnacleFormFields.ToArray
        End Get
        Set(ByVal value As BinnacleFormFieldData())
            Me.mBinnacleFormFields = New Generic.List(Of BinnacleFormFieldData)(value)
        End Set
    End Property

End Class
