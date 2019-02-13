Option Strict On

<Serializable()> Public Class BinnacleData

    Private mID As Long
    Private mUser As New UserData
    Private mBDate As New Date
    Private mBinnacleForms As New Generic.List(Of BinnacleFormData)
    Private mBinnacleTables As New Generic.List(Of BinnacleTableData)

    Public Property ID() As Long
        Get
            Return Me.mID
        End Get
        Set(ByVal value As Long)
            Me.mID = value
        End Set
    End Property

    Public Property BDate() As Date
        Get
            Return Me.mBDate
        End Get
        Set(ByVal value As DateTime)
            Me.mBDate = value
        End Set
    End Property

    Public Property User() As UserData
        Get
            Return Me.mUser
        End Get
        Set(ByVal value As UserData)
            Me.mUser = value
        End Set
    End Property

    Public Sub AddBinnacleForm(ByVal binnacleForm As BinnacleFormData)
        Me.mBinnacleForms.Add(binnacleForm)
    End Sub

    Public Property BinnacleForms() As BinnacleFormData()
        Get
            Return Me.mBinnacleForms.ToArray
        End Get
        Set(ByVal value As BinnacleFormData())
            Me.mBinnacleForms = New Generic.List(Of BinnacleFormData)(value)
        End Set
    End Property

    Public Sub AddBinnacleTable(ByVal binnacleTable As BinnacleTableData)
        Me.mBinnacleTables.Add(binnacleTable)
    End Sub

    Public Property BinnacleTableFields() As BinnacleTableData()
        Get
            Return Me.mBinnacleTables.ToArray
        End Get
        Set(ByVal value As BinnacleTableData())
            Me.mBinnacleTables = New Generic.List(Of BinnacleTableData)(value)
        End Set
    End Property

End Class
