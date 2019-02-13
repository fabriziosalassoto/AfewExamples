Imports Csla.data
Imports DBConnection
Imports System.Data.OleDb

''' <summary>
''' Represents a controller of data for the prgAdminBinnacleTables table in the data base PCS 
''' </summary>
''' <remarks></remarks>
<Serializable()> Public Class DAClsprgBinnacleTables

    ''' <summary>
    ''' Represents one structures with the data of a registry of the table prgAdminBinnacleTables 
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> Public Class Struct
        Public TableName As String = String.Empty

        Public ID As StructField(Of Long) = New StructField(Of Long)("ID", True)
        Public IDBinnacle As StructField(Of Long) = New StructField(Of Long)("IDBinnacle", False)
        Public IDOperation As StructField(Of Long) = New StructField(Of Long)("IDOperation", False)
        Public Log As StructField(Of Logs) = New StructField(Of Logs)("Log", False)
        Public BTableName As StructField(Of String) = New StructField(Of String)("TableName", False)
        Public BHour As StructField(Of Date) = New StructField(Of Date)("BHour", False)
    End Class

    ''' <summary>
    ''' Read the data of a certain AdminBinnacleTable in the table prgAdminBinnacleTables and return
    ''' a list of DAClsAdminBinnacleTables.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleTables.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLog As Logs, ByVal pID As Parameter(Of Long)) As Struct()
        Dim Logs() As Logs = GetLog(pLog)
        Fetch = DoFetch(Logs, CreateSql(Logs, "ID = " & pID.Value.ToString))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacleTable in the table prgAdminBinnacleTables and return
    ''' a list of DAClsAdminBinnacleTables.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleTables.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function FetchByBinnacle(ByVal pLog As Logs, ByVal pIDBinnacle As Parameter(Of Long)) As Struct()
        Dim Logs() As Logs = GetLog(pLog)
        FetchByBinnacle = DoFetch(Logs, CreateSql(Logs, "IDBinnacle = " & pIDBinnacle.Value.ToString))
    End Function

    ''' <summary>
    ''' Reads the data of all the AdminBinnacleTables in the table prgAdminBinnacleTables and return
    ''' a list of DAClsAdminBinnacleTables.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleTables.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch() As Struct()
        Dim Logs() As Logs = GetLogList(0)
        Fetch = DoFetch(Logs, CreateSql(Logs))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacleTable in the table prgAdminBinnacleTables and return
    ''' a list of DAClsAdminBinnacleTables.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleTables.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDBinnacle As Parameter(Of Long), ByVal pTableName As Parameter(Of String), ByVal pIDOperation As Parameter(Of Long), ByVal pFromHour As Parameter(Of Date), ByVal pToHour As Parameter(Of Date)) As Struct()
        Dim Logs() As Logs = GetLogList(0)
        Fetch = DoFetch(Logs, CreateSql(Logs, GetParametersLists(pIDBinnacle, pTableName, pIDOperation, pFromHour, pToHour)))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacleTable in the table prgAdminBinnacleTables and return
    ''' a list of DAClsAdminBinnacleTables.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleTables.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDBinnacle As ParameterList(Of Long), ByVal pTableName As ParameterList(Of String), ByVal pIDOperation As ParameterList(Of Long), ByVal pFromHour As Parameter(Of Date), ByVal pToHour As Parameter(Of Date)) As Struct()
        Dim Logs() As Logs = GetLogList(0)
        Fetch = DoFetch(Logs, CreateSql(Logs, GetParametersLists(pIDBinnacle, pTableName, pIDOperation, pFromHour, pToHour)))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacleTable in the table prgAdminBinnacleTables and return
    ''' a list of DAClsAdminBinnacleTables.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleTables.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLog As Logs) As Struct()
        Dim Logs() As Logs = GetLogList(pLog)
        Fetch = DoFetch(Logs, CreateSql(Logs))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacleTable in the table prgAdminBinnacleTables and return
    ''' a list of DAClsAdminBinnacleTables.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleTables.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLog As Logs, ByVal pIDBinnacle As Parameter(Of Long), ByVal pTableName As Parameter(Of String), ByVal pIDOperation As Parameter(Of Long), ByVal pFromHour As Parameter(Of Date), ByVal pToHour As Parameter(Of Date)) As Struct()
        Dim Logs() As Logs = GetLogList(pLog)
        Fetch = DoFetch(Logs, CreateSql(Logs, GetParametersLists(pIDBinnacle, pTableName, pIDOperation, pFromHour, pToHour)))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacleTable in the table prgAdminBinnacleTables and return
    ''' a list of DAClsAdminBinnacleTables.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleTables.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLog As Logs, ByVal pIDBinnacle As ParameterList(Of Long), ByVal pTableName As ParameterList(Of String), ByVal pIDOperation As ParameterList(Of Long), ByVal pFromHour As Parameter(Of Date), ByVal pToHour As Parameter(Of Date)) As Struct()
        Dim Logs() As Logs = GetLogList(pLog)
        Fetch = DoFetch(Logs, CreateSql(Logs, GetParametersLists(pIDBinnacle, pTableName, pIDOperation, pFromHour, pToHour)))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacleTable in the table prgAdminBinnacleTables and return
    ''' a list of DAClsAdminBinnacleTables.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleTables.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLogs() As Logs) As Struct()
        Dim Logs() As Logs = GetLogList(pLogs)
        Fetch = DoFetch(Logs, CreateSql(Logs))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacleTable in the table prgAdminBinnacleTables and return
    ''' a list of DAClsAdminBinnacleTables.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleTables.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLogs() As Logs, ByVal pIDBinnacle As Parameter(Of Long), ByVal pTableName As Parameter(Of String), ByVal pIDOperation As Parameter(Of Long), ByVal pFromHour As Parameter(Of Date), ByVal pToHour As Parameter(Of Date)) As Struct()
        Dim Logs() As Logs = GetLogList(pLogs)
        Fetch = DoFetch(Logs, CreateSql(Logs, GetParametersLists(pIDBinnacle, pTableName, pIDOperation, pFromHour, pToHour)))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacleTable in the table prgAdminBinnacleTables and return
    ''' a list of DAClsAdminBinnacleTables.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleTables.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLogs() As Logs, ByVal pIDBinnacle As ParameterList(Of Long), ByVal pTableName As ParameterList(Of String), ByVal pIDOperation As ParameterList(Of Long), ByVal pFromHour As Parameter(Of Date), ByVal pToHour As Parameter(Of Date)) As Struct()
        Dim Logs() As Logs = GetLogList(pLogs)
        Fetch = DoFetch(Logs, CreateSql(Logs, GetParametersLists(pIDBinnacle, pTableName, pIDOperation, pFromHour, pToHour)))
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
    ''' 
    ''' </summary>
    ''' <param name="pIDBinnacle"></param>
    ''' <param name="pTableName"></param>
    ''' <param name="pIDOperation"></param>
    ''' <param name="pFromHour"></param>
    ''' <param name="pToHour"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParametersLists(ByVal pIDBinnacle As Parameter(Of Long), ByVal pTableName As Parameter(Of String), ByVal pIDOperation As Parameter(Of Long), ByVal pFromHour As Parameter(Of Date), ByVal pToHour As Parameter(Of Date)) As String()
        Dim params As New List(Of String)
        Dim priorities As New List(Of Integer)

        If pIDBinnacle.Priority <> -1 AndAlso pIDBinnacle.Value <> 0 Then
            params.Add("IDBinnacle = " & pIDBinnacle.Value.ToString)
            priorities.Add(pIDBinnacle.Priority)
        End If

        If pTableName.Priority <> -1 AndAlso pTableName.Value <> 0 Then
            params.Add("TableName = " & pTableName.Value.ToString)
            priorities.Add(pTableName.Priority)
        End If

        If pIDOperation.Priority <> -1 AndAlso pIDOperation.Value <> 0 Then
            params.Add("IDOperation = " & pIDOperation.Value.ToString)
            priorities.Add(pIDOperation.Priority)
        End If

        If pFromHour.Priority <> -1 AndAlso pFromHour.Value.TimeOfDay > Date.MinValue.TimeOfDay Then
            params.Add("BHour >= '" & pFromHour.Value.ToString("HH:mm:ss") & "'")
            priorities.Add(pFromHour.Priority)
        End If

        If pToHour.Priority <> -1 AndAlso pToHour.Value.TimeOfDay < Date.MaxValue.TimeOfDay Then
            params.Add("BHour <= '" & pToHour.Value.ToString("HH:mm:ss") & "'")
            priorities.Add(pToHour.Priority)
        End If

        Return SortParametersLists(priorities.ToArray, params.ToArray)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pIDBinnacle"></param>
    ''' <param name="pTableName"></param>
    ''' <param name="pIDOperation"></param>
    ''' <param name="pFromHour"></param>
    ''' <param name="pToHour"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParametersLists(ByVal pIDBinnacle As ParameterList(Of Long), ByVal pTableName As ParameterList(Of String), ByVal pIDOperation As ParameterList(Of Long), ByVal pFromHour As Parameter(Of Date), ByVal pToHour As Parameter(Of Date)) As String()()
        Dim params As New List(Of String())
        Dim priorities As New List(Of Integer)

        If pIDBinnacle.Priority <> -1 AndAlso pIDBinnacle.Values.Length > 0 Then
            params.Add(GetParametersLists("IDBinnacle", " = ", pIDBinnacle.Values))
            priorities.Add(pIDBinnacle.Priority)
        End If

        If pTableName.Priority <> -1 AndAlso pTableName.Values.Length > 0 Then
            params.Add(GetParametersLists("TableName", " = ", pTableName.Values))
            priorities.Add(pTableName.Priority)
        End If

        If pIDOperation.Priority <> -1 AndAlso pIDOperation.Values.Length > 0 Then
            params.Add(GetParametersLists("IDOperation", " = ", pIDOperation.Values))
            priorities.Add(pIDOperation.Priority)
        End If

        If pFromHour.Priority <> -1 AndAlso pFromHour.Value.TimeOfDay > Date.MinValue.TimeOfDay Then
            params.Add(GetFilters("BHour", " >= ", pFromHour.Value))
            priorities.Add(pFromHour.Priority)
        End If

        If pToHour.Priority <> -1 AndAlso pToHour.Value.TimeOfDay < Date.MaxValue.TimeOfDay Then
            params.Add(GetFilters("BHour", " <= ", pToHour.Value))
            priorities.Add(pToHour.Priority)
        End If

        Return SortParametersLists(priorities.ToArray, params.ToArray)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="values"></param>
    ''' <param name="fieldName"></param>
    ''' <param name="paramOperator"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParametersLists(ByVal fieldName As String, ByVal paramOperator As String, ByVal values() As Long) As String()
        Dim param As New List(Of String)
        For Each value As Long In values
            If value <> 0 Then
                param.Add(fieldName & paramOperator & value.ToString)
            End If
        Next
        Return param.ToArray
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="values"></param>
    ''' <param name="fieldName"></param>
    ''' <param name="paramOperator"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParametersLists(ByVal fieldName As String, ByVal paramOperator As String, ByVal values() As String) As String()
        Dim param As New List(Of String)
        For Each value As String In values
            If value <> 0 Then
                param.Add(fieldName & paramOperator & value)
            End If
        Next
        Return param.ToArray
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="value"></param>
    ''' <param name="fieldName"></param>
    ''' <param name="paramOperator"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetFilters(ByVal fieldName As String, ByVal paramOperator As String, ByVal value As Date) As String()
        Dim param As New List(Of String)
        param.Add(fieldName & paramOperator & "'" & value.ToString("HH:mm:ss") & "'")
        Return param.ToArray
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="keys"></param>
    ''' <param name="params"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function SortParametersLists(ByVal keys As Integer(), ByVal params As String()) As String()
        Array.Sort(keys, params)
        Return params
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="keys"></param>
    ''' <param name="params"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function SortParametersLists(ByVal keys As Integer(), ByVal params As String()()) As String()()
        Array.Sort(keys, params)
        Return params
    End Function

    ''' <summary>
    ''' Creates the Sql Statement and returns it
    ''' </summary>
    ''' <param name="pParametersList"></param>
    ''' <returns>Sql Statement</returns>
    ''' <remarks></remarks>
    Private Shared Function CreateSql(ByVal pLogs As Logs(), ByVal ParamArray pParametersList() As String) As String
        Dim sqls As New List(Of String)

        For Each log As Logs In pLogs

            'Build SQL value
            Dim sql As String = ""
            sql &= " Select"
            sql &= "  ID,"
            sql &= "  IDBinnacle,"
            sql &= "  IDOperation,"
            sql &= "  " & log & " as Log,"
            sql &= "  TableName,"
            sql &= "  BHour"
            sql &= " From"
            sql &= "  prg" & [Enum].GetName(GetType(Logs), log) & "BinnacleTables"

            If pParametersList.Length > 0 Then
                sql &= " Where " & Join(pParametersList, " And ")
            End If

            sqls.Add(sql)
        Next

        Return Join(sqls.ToArray, " Union ")
    End Function

    ''' <summary>
    ''' Creates the Sql Statement and returns it
    ''' </summary>
    ''' <returns>Sql Statement</returns>
    ''' <remarks></remarks>
    Private Shared Function CreateSql(ByVal pLogs As Logs(), ByVal pParams()() As String) As String
        Dim sqls As New List(Of String)

        For Each log As Logs In pLogs

            'Build SQL value
            Dim sql As String = ""
            sql &= " Select"
            sql &= "  ID,"
            sql &= "  IDBinnacle,"
            sql &= "  IDOperation,"
            sql &= "  " & log & " as Log,"
            sql &= "  TableName,"
            sql &= "  BHour"
            sql &= " From"
            sql &= "  prg" & [Enum].GetName(GetType(Logs), log) & "BinnacleTables"

            Dim list As New List(Of String)
            For Each item As String() In pParams
                If item.Length > 0 Then
                    list.Add("(" & Join(item, " Or ") & ")")
                End If
            Next
            If list.Count > 0 Then
                sql &= " Where " & Join(list.ToArray, " And ")
            End If

            sqls.Add(sql)
        Next

        Return Join(sqls.ToArray, " Union ")
    End Function

    ''' <summary>
    ''' Execute SQL statement to reads the data of the table prgAdminBinnacleTables and
    ''' return a list of DAClsAdminBinnacleTables.Struct with each one of the read registries 
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
                        Struct.ID.SetValue(dr.GetInt64("ID"))
                        Struct.IDBinnacle.SetValue(dr.GetInt64("IDBinnacle"))
                        Struct.IDOperation.SetValue(dr.GetInt64("IDOperation"))
                        Struct.Log.SetValue(dr.GetInt32("Log"))
                        Struct.BTableName.SetValue(dr.GetString("TableName"))
                        Struct.BHour.SetValue(dr.GetDateTime("BHour"))
                        List.Add(Struct)
                    End While
                End Using
            End Using
        End If
        DoFetch = List.ToArray
    End Function

End Class
