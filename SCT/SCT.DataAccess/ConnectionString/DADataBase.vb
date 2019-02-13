Imports System.Configuration.ConfigurationManager

Public Module DADataBase

    Public ReadOnly Property SCTConnectionString() As String
        Get
            Return ConnectionStrings("SCTConnStr").ConnectionString
        End Get
    End Property

End Module