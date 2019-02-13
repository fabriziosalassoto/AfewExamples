Imports Csla.data
Imports DBConnection
Imports System.Data.OleDb

''' <summary>
''' Represents a controller of data for the prgAdminBinnacleTableFields table in the data base PCS 
''' </summary>
''' <remarks></remarks>
<Serializable()> Public Class DAClsprgBinnacleTableFields

    ''' <summary>
    ''' Represents one structures with the data of a registry of the table prgAdminBinnacleTableFields 
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> Public Class Struct

        Public TableName As String = String.Empty

        Public ID As StructField(Of Long) = New StructField(Of Long)("ID", True)
        Public IDBinnacleTable As StructField(Of Long) = New StructField(Of Long)("IDBinnacleTable", False)
        Public Log As StructField(Of Logs) = New StructField(Of Logs)("Log", False)
        Public FieldName As StructField(Of String) = New StructField(Of String)("FieldName", False)
        Public OldValue As StructField(Of String) = New StructField(Of String)("OldValue", False)
        Public NewValue As StructField(Of String) = New StructField(Of String)("NewValue", False)

    End Class

    ''' <summary>
    ''' Read the data of a certain AdminBinnacleTableField in the table prgAdminBinnacleTableFields and return
    ''' a list of DAClsAdminBinnacleTableFields.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleTableFields.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLog As Logs, ByVal pID As Parameter(Of Long)) As Struct()
        Dim Logs() As Logs = GetLog(pLog)
        Fetch = DoFetch(Logs, CreateSql(Logs, "ID = " & pID.Value.ToString))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacleTableField in the table prgAdminBinnacleTableFields and return
    ''' a list of DAClsAdminBinnacleTableFields.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleTableFields.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function FetchByBinnacleTable(ByVal pLog As Logs, ByVal pIDBinnacleTable As Parameter(Of Long)) As Struct()
        Dim Logs() As Logs = GetLog(pLog)
        FetchByBinnacleTable = DoFetch(Logs, CreateSql(Logs, "IDBinnacleTable = " & pIDBinnacleTable.Value.ToString))
    End Function

    ''' <summary>
    ''' Reads the data of all the AdminBinnacleTableFields in the table prgAdminBinnacleTableFields and return
    ''' a list of DAClsAdminBinnacleTableFields.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleTableFields.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch() As Struct()
        Dim Logs() As Logs = GetLogList(0)
        Fetch = DoFetch(Logs, CreateSql(Logs))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacleTableField in the table prgAdminBinnacleTableFields and return
    ''' a list of DAClsAdminBinnacleTableFields.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleTableFields.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDBinnacleTable As Parameter(Of Long), ByVal pFieldName As Parameter(Of String)) As Struct()
        Dim Logs() As Logs = GetLogList(0)
        Fetch = DoFetch(Logs, CreateSql(Logs, GetParametersLists(pIDBinnacleTable, pFieldName)))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacleTableField in the table prgAdminBinnacleTableFields and return
    ''' a list of DAClsAdminBinnacleTableFields.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleTableFields.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDBinnacleTable As ParameterList(Of Long), ByVal pFieldName As ParameterList(Of String)) As Struct()
        Dim Logs() As Logs = GetLogList(0)
        Fetch = DoFetch(Logs, CreateSql(Logs, GetParametersLists(pIDBinnacleTable, pFieldName)))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacleTableField in the table prgAdminBinnacleTableFields and return
    ''' a list of DAClsAdminBinnacleTableFields.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleTableFields.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLog As Logs) As Struct()
        Dim Logs() As Logs = GetLogList(pLog)
        Fetch = DoFetch(Logs, CreateSql(Logs))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacleTableField in the table prgAdminBinnacleTableFields and return
    ''' a list of DAClsAdminBinnacleTableFields.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleTableFields.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLog As Logs, ByVal pIDBinnacleTable As Parameter(Of Long), ByVal pFieldName As Parameter(Of String)) As Struct()
        Dim Logs() As Logs = GetLogList(pLog)
        Fetch = DoFetch(Logs, CreateSql(Logs, GetParametersLists(pIDBinnacleTable, pFieldName)))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacleTableField in the table prgAdminBinnacleTableFields and return
    ''' a list of DAClsAdminBinnacleTableFields.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleTableFields.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLog As Logs, ByVal pIDBinnacleTable As ParameterList(Of Long), ByVal pFieldName As ParameterList(Of String)) As Struct()
        Dim Logs() As Logs = GetLogList(pLog)
        Fetch = DoFetch(Logs, CreateSql(Logs, GetParametersLists(pIDBinnacleTable, pFieldName)))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacleTableField in the table prgAdminBinnacleTableFields and return
    ''' a list of DAClsAdminBinnacleTableFields.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleTableFields.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLogs() As Logs) As Struct()
        Dim Logs() As Logs = GetLogList(pLogs)
        Fetch = DoFetch(Logs, CreateSql(Logs))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacleTableField in the table prgAdminBinnacleTableFields and return
    ''' a list of DAClsAdminBinnacleTableFields.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleTableFields.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLogs() As Logs, ByVal pIDBinnacleTable As Parameter(Of Long), ByVal pFieldName As Parameter(Of String)) As Struct()
        Dim Logs() As Logs = GetLogList(pLogs)
        Fetch = DoFetch(Logs, CreateSql(Logs, GetParametersLists(pIDBinnacleTable, pFieldName)))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacleTableField in the table prgAdminBinnacleTableFields and return
    ''' a list of DAClsAdminBinnacleTableFields.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleTableFields.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLogs() As Logs, ByVal pIDBinnacleTable As ParameterList(Of Long), ByVal pFieldName As ParameterList(Of String)) As Struct()
        Dim Logs() As Logs = GetLogList(pLogs)
        Fetch = DoFetch(Logs, CreateSql(Logs, GetParametersLists(pIDBinnacleTable, pFieldName)))
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
    ''' <param name="pIDBinnacleTable"></param>
    ''' <param name="pFieldName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParametersLists(ByVal pIDBinnacleTable As Parameter(Of Long), ByVal pFieldName As Parameter(Of String)) As String()
        Dim parameters As New List(Of String)
        Dim priorities As New List(Of Integer)

        If pIDBinnacleTable.Priority <> -1 AndAlso pIDBinnacleTable.Value <> 0 Then
            parameters.Add("IDBinnacleTable = " & pIDBinnacleTable.Value.ToString)
            priorities.Add(pIDBinnacleTable.Priority)
        End If

        If pFieldName.Priority <> -1 AndAlso pFieldName.Value <> 0 Then
            parameters.Add("FieldName = " & pFieldName.Value)
            priorities.Add(pFieldName.Priority)
        End If

        Return SortParametersLists(priorities.ToArray, parameters.ToArray)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pIDBinnacleTable"></param>
    ''' <param name="pFieldName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParametersLists(ByVal pIDBinnacleTable As ParameterList(Of Long), ByVal pFieldName As ParameterList(Of String)) As String()()
        Dim parameters As New List(Of String())
        Dim priorities As New List(Of Integer)

        If pIDBinnacleTable.Priority <> -1 AndAlso pIDBinnacleTable.Values.Length > 0 Then
            parameters.Add(GetParametersLists("IDBinnacleTable", " = ", pIDBinnacleTable.Values))
            priorities.Add(pIDBinnacleTable.Priority)
        End If

        If pFieldName.Priority <> -1 AndAlso pFieldName.Values.Length > 0 Then
            parameters.Add(GetParametersLists("FieldName", " = ", pFieldName.Values))
            priorities.Add(pFieldName.Priority)
        End If

        Return SortParametersLists(priorities.ToArray, parameters.ToArray)
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

        If pLogs.Length > 0 Then
            For Each log As Logs In pLogs

                'Build SQL value
                Dim sql As String = ""
                sql &= " Select"
                sql &= "  ID,"
                sql &= "  IDBinnacleTable,"
                sql &= "  " & log & " as Log,"
                sql &= "  FieldName,"
                sql &= "  OldValue,"
                sql &= "  NewValue"
                sql &= " From"
                sql &= "  prg" & [Enum].GetName(GetType(Logs), log) & "BinnacleTableFields"

                If pParametersList.Length > 0 Then
                    sql &= " Where " & Join(pParametersList, " And ")
                End If

                sqls.Add(sql)
            Next
        End If

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
            sql &= "  IDBinnacleTable,"
            sql &= "  " & log & " as Log,"
            sql &= "  FieldName,"
            sql &= "  OldValue,"
            sql &= "  NewValue"
            sql &= " From"
            sql &= "  prg" & [Enum].GetName(GetType(Logs), log) & "BinnacleTableFields"

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

        CreateSql = Join(sqls.ToArray, " Union ")
    End Function

    ''' <summary>
    ''' Execute SQL statement to reads the data of the table prgAdminBinnacleTableFields and
    ''' return a list of DAClsAdminBinnacleTableFields.Struct with each one of the read registries 
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
                        Struct.IDBinnacleTable.SetValue(dr.GetInt64("IDBinnacleTable"))
                        Struct.Log.SetValue(dr.GetInt32("Log"))
                        Struct.FieldName.SetValue(dr.GetString("FieldName"))
                        Struct.OldValue.SetValue(dr.GetString("OldValue"))
                        Struct.NewValue.SetValue(dr.GetString("NewValue"))
                        List.Add(Struct)
                    End While
                End Using
            End Using
        End If
        DoFetch = List.ToArray
    End Function

End Class
