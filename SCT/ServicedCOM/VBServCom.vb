Imports System.EnterpriseServices
Imports System.Data.SqlClient

<Assembly: ApplicationActivation(ActivationOption.Server)> 
<Assembly: ApplicationName("SimpleTrans")> 
<Assembly: Description("Simple Transactional application to show Enterprise Services")> 

<Transaction(TransactionOption.RequiresNew)> Public Class VBServCom
    Inherits ServicedComponent

    Private mPrueba As String

    Public Property Prueba() As String
        Get
            Return Me.mPrueba
        End Get
        Set(ByVal value As String)
            Me.mPrueba = value
        End Set
    End Property

End Class
