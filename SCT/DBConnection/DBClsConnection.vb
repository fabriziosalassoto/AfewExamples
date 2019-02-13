Imports Csla.Data
Imports System.Data.OleDb

''' <summary>
''' Represents a connection the data base SQL Server and allows to make transactions with her directly
''' </summary>
''' <remarks></remarks>
<Serializable()> Public Class DBClsConnection
    Implements IDisposable

#Region " Properties "

    Private WithEvents cn As OleDbConnection
    Private WithEvents cm As OleDbCommand
    Private mConnectionString As String

    ''' <summary>
    ''' Get or set ConnectionString used to open the data base
    ''' </summary>
    ''' <value>ConnectionString</value>
    ''' <returns>ConnectionString</returns>
    ''' <remarks></remarks>
    Public Property ConnectionString() As String
        Get
            Return Me.mConnectionString
        End Get
        Set(ByVal value As String)
            Me.mConnectionString = value
        End Set
    End Property

#End Region

#Region " Factory Methods "

    ''' <summary>
    ''' Create a new DBClsConnection instance
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        Me.cn = Nothing
        Me.cm = Nothing
        Me.mConnectionString = ""
    End Sub

    ''' <summary>
    ''' Create a new DBClsConnection instance
    ''' </summary>
    ''' <param name="pConnectionString">ConnectionString</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal pConnectionString As String)
        Me.cn = Nothing
        Me.cm = Nothing
        Me.mConnectionString = pConnectionString
    End Sub

    ''' <summary>
    ''' Opens the connection to the data base and creates a SqlCommand 
    ''' associated to the connection with the SQL Statement to execute 
    ''' </summary>
    ''' <param name="pSql">Sql Statement to execute</param>
    ''' <remarks></remarks>
    Private Sub ConnectionToDataBase(ByVal pSql As String)
        Me.cn = New OleDbConnection(Me.mConnectionString)
        Me.cm = New OleDbCommand
        Me.cn.Open()
        Me.cm = cn.CreateCommand
        Me.cm.CommandText = pSql
    End Sub

    ''' <summary>
    ''' Executes select statement and return data reader with the data.
    ''' </summary>
    ''' <param name="pSql">Select statement.</param> 
    ''' <returns>Data reader with tha data.</returns>
    ''' <remarks></remarks>
    Public Function ExecuteReader(ByVal pSql As String) As SafeDataReader
        Try
            Me.ConnectionToDataBase(pSql)
            Return New SafeDataReader(cm.ExecuteReader)
        Catch SqlEx As OleDbException
            Me.Dispose()
            Throw Me.NewException(SqlEx)
        Catch SysEx As SystemException
            Me.Dispose()
            Throw SysEx
        End Try
    End Function

    ''' <summary>
    ''' Executes Transact-SQL statement and return the number of rows affected.
    ''' </summary>
    ''' <param name="pSql">Transact-SQL statement to execute.</param>
    ''' <returns>Number of rows affected.</returns>
    ''' <remarks></remarks>
    Public Function ExecuteNonQuery(ByVal pSql As String) As Integer
        Try
            Me.ConnectionToDataBase(pSql)
            Return cm.ExecuteNonQuery()
        Catch SqlEx As OleDbException
            Me.Dispose()
            Throw Me.NewException(SqlEx)
        Catch SysEx As SystemException
            Me.Dispose()
            Throw SysEx
        End Try
    End Function

    ''' <summary>
    ''' Executes the query and return sentence the result set returned by the query.
    ''' </summary>
    ''' <param name="pSql">Query to execute.</param>
    ''' <returns>Result of the query.</returns>
    ''' <remarks></remarks>
    Public Function ExecuteScalar(ByVal pSql As String) As String
        Try
            Me.ConnectionToDataBase(pSql)
            Return cm.ExecuteScalar()
        Catch SqlEx As OleDbException
            Me.Dispose()
            Throw Me.NewException(SqlEx)
        Catch SysEx As SystemException
            Me.Dispose()
            Throw SysEx
        End Try
    End Function

    Private Function NewException(ByVal SqlEx As OleDbException) As Exception
        Select Case SqlEx.ErrorCode
            Case -2147467259, -2147217843
                Return New DataException("Invalid database or doesn't connect")
            Case -2147217865, -2147217900
                Return New DataException("Internal database error")
            Case Else
                Return SqlEx
        End Select
    End Function

#End Region

#Region " IDisposable Support "

    Private disposedValue As Boolean = False  ' To detect redundant calls
    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free unmanaged resources when explicitly called
                Me.cn.Dispose()
                Me.cm.Dispose()
            End If

            ' TODO: free shared unmanaged resources
        End If
        Me.disposedValue = True
    End Sub


    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class