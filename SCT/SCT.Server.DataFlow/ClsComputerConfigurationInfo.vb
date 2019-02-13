Option Strict On

Imports Microsoft.VisualBasic

Public Class ClsComputerConfigurationInfo

    Private mComputerHDSerialNumber As String = String.Empty
    Private mComputerMacAddress As String = String.Empty
    Private mComputerName As String = String.Empty

    Public Property ComputerHDSerialNumber() As String
        Get
            Return Me.mComputerHDSerialNumber
        End Get
        Set(ByVal value As String)
            Me.mComputerHDSerialNumber = value
        End Set
    End Property

    Public Property ComputerMacAddress() As String
        Get
            Return Me.mComputerMacAddress
        End Get
        Set(ByVal value As String)
            Me.mComputerMacAddress = value
        End Set
    End Property

    Public Property ComputerName() As String
        Get
            Return Me.mComputerName
        End Get
        Set(ByVal value As String)
            Me.mComputerName = value
        End Set
    End Property

    Public Shared Function NewComputerConfigurationInfo() As ClsComputerConfigurationInfo
        Return New ClsComputerConfigurationInfo
    End Function

    Private Sub New()
        ' require use of factory methods
    End Sub

End Class
