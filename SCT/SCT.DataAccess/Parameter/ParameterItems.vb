<Serializable()> Public Class ParameterItems

    Private mFieldName As String = String.Empty
    Private mValue As String
    Private mParamOperator As String

    Public Sub New(ByVal pFieldName As String, ByVal pOperator As String)
        Me.mFieldName = pFieldName
        Me.mParamOperator = pOperator
    End Sub

    Public Sub New(ByVal pFieldName As String, ByVal pParamOperator As String, ByVal pValue As String)
        Me.mFieldName = pFieldName
        Me.mParamOperator = pParamOperator
        Me.mValue = pValue
    End Sub

    Public ReadOnly Property FieldName() As String
        Get
            Return Me.mFieldName
        End Get
    End Property

    Public ReadOnly Property ParamOperator() As String
        Get
            Return Me.mParamOperator
        End Get
    End Property

    Public Property Value() As String
        Get
            Return Me.mValue
        End Get
        Set(ByVal value As String)
            Me.mValue = value
        End Set
    End Property

    Public Function GetFilter() As String
        Return Me.mFieldName & Me.mParamOperator & Me.mValue.ToString
    End Function

End Class
