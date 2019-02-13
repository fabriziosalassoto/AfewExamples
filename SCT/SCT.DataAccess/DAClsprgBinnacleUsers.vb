Imports Csla.data
Imports DBConnection
Imports System.Data.OleDb

''' <summary>
''' Represents a controller of data for the prgAdministratorUsers table in the data base PCS 
''' </summary>
''' <remarks></remarks>
<Serializable()> Public Class DAClsprgBinnacleUsers

    ''' <summary>
    ''' Represents one structures with the data of a registry of the table prgAdministratorUsers 
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> Public Class Struct
        Public TableName As String = String.Empty

        Public ID As StructField(Of Long) = New StructField(Of Long)("ID", True)
        Public Name As StructField(Of String) = New StructField(Of String)("Name", False)
        Public Log As StructField(Of Logs) = New StructField(Of Logs)("Log", False)
    End Class

    ''' <summary>
    ''' Reads the data of all the AdministratorUsers in the table prgAdministratorUsers and return
    ''' a list of DAClsAdministratorUsers.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsAdministratorUsers.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch() As Struct()
        Dim Logs() As Logs = GetLogList(0)
        Fetch = DoFetch(Logs, CreateSql(Logs))
    End Function

    ''' <summary>
    ''' Reads the data of all the AdministratorUsers in the table prgAdministratorUsers and return
    ''' a list of DAClsAdministratorUsers.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsAdministratorUsers.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLog As Logs) As Struct()
        Dim Logs() As Logs = GetLogList(pLog)
        Fetch = DoFetch(Logs, CreateSql(Logs))
    End Function

    ''' <summary>
    ''' Reads the data of all the AdministratorUsers in the table prgAdministratorUsers and return
    ''' a list of DAClsAdministratorUsers.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsAdministratorUsers.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLogs() As Logs) As Struct()
        Dim Logs() As Logs = GetLogList(pLogs)
        Fetch = DoFetch(Logs, CreateSql(Logs))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdministratorUser in the table prgAdministratorUsers and return
    ''' a list of DAClsAdministratorUsers.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pID">ID of AdministratorUser to fetch</param>
    ''' <returns>List of DAClsAdministratorUsers.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLog As Logs, ByVal pID As Long) As Struct()
        Dim Logs() As Logs = GetLog(pLog)
        Fetch = DoFetch(Logs, CreateSql(Logs, "ID = " & pID.ToString))
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="plog"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetLog(ByVal plog As Logs) As Logs()
        Dim logs As New List(Of Logs)
        If [Enum].IsDefined(GetType(Logs), plog) Then
            logs.Add(plog)
        End If
        Return logs.ToArray
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="plog"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetLogList(ByVal plog As Logs) As Logs()
        Dim logs As New List(Of Logs)
        If plog = 0 Then
            logs = New List(Of Logs)([Enum].GetValues(GetType(Logs)))
        ElseIf [Enum].IsDefined(GetType(Logs), plog) Then
            logs.Add(plog)
        End If
        Return logs.ToArray
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="plog"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetLogList(ByVal plog() As Logs) As Logs()
        Dim logs As New List(Of Logs)
        For Each log As Logs In plog
            If log = 0 Then
                logs = New List(Of Logs)([Enum].GetValues(GetType(Logs)))
                Exit For
            ElseIf [Enum].IsDefined(GetType(Logs), log) Then
                logs.Add(log)
            End If
        Next
        Return logs.ToArray
    End Function

    ''' <summary>
    ''' Creates the Sql Statement and returns it
    ''' </summary>
    ''' <returns>Sql Statement</returns>
    ''' <remarks></remarks>
    Private Shared Function CreateSql(ByVal pLogs() As Logs, ByVal ParamArray pParametersList() As String) As String
        Dim sqls As New List(Of String)

        For Each log As Logs In pLogs

            'Build SQL value
            Dim sql As String = ""
            sql &= " Select"

            Select Case log
                Case Logs.Administrative
                    sql &= "  ID,"
                    sql &= "  " & log & " as Log,"
                    sql &= "  (LastName + ', ' + FirstName) as Name"
                    sql &= " from"
                    sql &= "  prgAdministrativeUsers"
                Case Logs.Advertiser
                    sql &= "  ID,"
                    sql &= "  " & log & " as Log,"
                    sql &= "  CompanyName as Name"
                    sql &= " from"
                    sql &= "  appAdvertiserAccount"
                Case Logs.Subscriber
                    sql &= "  ID,"
                    sql &= "  " & log & " as Log,"
                    sql &= "  ComputerSerialNumber as Name"
                    sql &= " from"
                    sql &= "  appSubscribersAccounts"
            End Select

            If pParametersList.Length > 0 Then
                sql &= " Where " & Join(pParametersList, " And ")
            End If

            sqls.Add(sql)
        Next

        Return Join(sqls.ToArray, " Union ")
    End Function

    ''' <summary>
    ''' Execute SQL statement to reads the data of the table prgAdministratorUsers and
    ''' return a list of DAClsAdministratorUsers.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="psql">Sql Statement to execute</param>
    ''' <returns>List of the read registries</returns>
    ''' <remarks></remarks>
    Private Shared Function DoFetch(ByVal pLogs() As Logs, ByVal psql As String) As Struct()
        Dim Struct As Struct
        Dim List As New List(Of Struct)

        If Len(psql) > 0 Then

            'Connection to the data base 
            Using DBConn As DBClsConnection = New DBClsConnection(DADataBase.SCTConnectionString)

                'Execute SQL statement
                Using dr As SafeDataReader = DBConn.ExecuteReader(psql)

                    'Read and add each registry to the list 
                    While dr.Read()
                        Struct = New Struct
                        Struct.TableName = dr.GetSchemaTable.TableName
                        Struct.ID.SetValue(dr.GetInt64("ID"))
                        Struct.Log.SetValue(dr.GetInt32("Log"))
                        Struct.Name.SetValue(dr.GetString("Name"))
                        List.Add(Struct)
                    End While
                End Using
            End Using
        End If
        DoFetch = List.ToArray
    End Function

End Class
