Imports Csla.data
Imports DBConnection
Imports System.Data.OleDb

''' <summary>
''' Represents a controller of data for the prgAdminBinnacleFormFields table in the data base PCS 
''' </summary>
''' <remarks></remarks>
<Serializable()> Public Class DAClsprgBinnacleFormFields

    ''' <summary>
    ''' Represents one structures with the data of a registry of the table prgAdminBinnacleFormFields 
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> Public Class Struct

        Public TableName As String = String.Empty

        Public ID As StructField(Of Long) = New StructField(Of Long)("ID", True)
        Public IDBinnacleForm As StructField(Of Long) = New StructField(Of Long)("IDBinnacleForm", False)
        Public IDField As StructField(Of Long) = New StructField(Of Long)("IDField", False)
        Public Log As StructField(Of Logs) = New StructField(Of Logs)("Log", False)
        Public OldValue As StructField(Of String) = New StructField(Of String)("OldValue", False)
        Public NewValue As StructField(Of String) = New StructField(Of String)("NewValue", False)

    End Class

    ''' <summary>
    ''' Read the data of a certain AdminBinnacleFormField in the table prgAdminBinnacleFormFields and return
    ''' a list of DAClsAdminBinnacleFormFields.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleFormFields.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLog As Logs, ByVal pID As Parameter(Of Long)) As Struct()
        Dim Logs() As Logs = GetLog(pLog)
        Fetch = DoFetch(Logs, CreateSql(Logs, "ID = " & pID.Value.ToString))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacleFormField in the table prgAdminBinnacleFormFields and return
    ''' a list of DAClsAdminBinnacleFormFields.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleFormFields.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function FetchByBinnacleForm(ByVal pLog As Logs, ByVal pIDBinnacleForm As Parameter(Of Long)) As Struct()
        Dim Logs() As Logs = GetLog(pLog)
        FetchByBinnacleForm = DoFetch(Logs, CreateSql(Logs, "IDBinnacleForm = " & pIDBinnacleForm.Value.ToString))
    End Function

    ''' <summary>
    ''' Reads the data of all the AdminBinnacleFormFields in the table prgAdminBinnacleFormFields and return
    ''' a list of DAClsAdminBinnacleFormFields.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleFormFields.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch() As Struct()
        Dim Logs() As Logs = GetLogList(0)
        Fetch = DoFetch(Logs, CreateSql(Logs))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacleFormField in the table prgAdminBinnacleFormFields and return
    ''' a list of DAClsAdminBinnacleFormFields.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleFormFields.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDBinnacleForm As Parameter(Of Long), ByVal pIDField As Parameter(Of Long)) As Struct()
        Dim Logs() As Logs = GetLogList(0)
        Fetch = DoFetch(Logs, CreateSql(Logs, GetParametersLists(pIDBinnacleForm, pIDField)))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacleFormField in the table prgAdminBinnacleFormFields and return
    ''' a list of DAClsAdminBinnacleFormFields.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleFormFields.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDBinnacleForm As ParameterList(Of Long), ByVal pIDField As ParameterList(Of Long)) As Struct()
        Dim Logs() As Logs = GetLogList(0)
        Fetch = DoFetch(Logs, CreateSql(Logs, GetParametersLists(pIDBinnacleForm, pIDField)))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacleFormField in the table prgAdminBinnacleFormFields and return
    ''' a list of DAClsAdminBinnacleFormFields.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleFormFields.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLog As Logs) As Struct()
        Dim Logs() As Logs = GetLogList(pLog)
        Fetch = DoFetch(Logs, CreateSql(Logs))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacleFormField in the table prgAdminBinnacleFormFields and return
    ''' a list of DAClsAdminBinnacleFormFields.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleFormFields.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLog As Logs, ByVal pIDBinnacleForm As Parameter(Of Long), ByVal pIDField As Parameter(Of Long)) As Struct()
        Dim Logs() As Logs = GetLogList(pLog)
        Fetch = DoFetch(Logs, CreateSql(Logs, GetParametersLists(pIDBinnacleForm, pIDField)))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacleFormField in the table prgAdminBinnacleFormFields and return
    ''' a list of DAClsAdminBinnacleFormFields.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleFormFields.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLog As Logs, ByVal pIDBinnacleForm As ParameterList(Of Long), ByVal pIDField As ParameterList(Of Long)) As Struct()
        Dim Logs() As Logs = GetLogList(pLog)
        Fetch = DoFetch(Logs, CreateSql(Logs, GetParametersLists(pIDBinnacleForm, pIDField)))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacleFormField in the table prgAdminBinnacleFormFields and return
    ''' a list of DAClsAdminBinnacleFormFields.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleFormFields.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLogs() As Logs) As Struct()
        Dim Logs() As Logs = GetLogList(pLogs)
        Fetch = DoFetch(Logs, CreateSql(Logs))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacleFormField in the table prgAdminBinnacleFormFields and return
    ''' a list of DAClsAdminBinnacleFormFields.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleFormFields.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLogs() As Logs, ByVal pIDBinnacleForm As Parameter(Of Long), ByVal pIDField As Parameter(Of Long)) As Struct()
        Dim Logs() As Logs = GetLogList(pLogs)
        Fetch = DoFetch(Logs, CreateSql(Logs, GetParametersLists(pIDBinnacleForm, pIDField)))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacleFormField in the table prgAdminBinnacleFormFields and return
    ''' a list of DAClsAdminBinnacleFormFields.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleFormFields.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLogs() As Logs, ByVal pIDBinnacleForm As ParameterList(Of Long), ByVal pIDField As ParameterList(Of Long)) As Struct()
        Dim Logs() As Logs = GetLogList(pLogs)
        Fetch = DoFetch(Logs, CreateSql(Logs, GetParametersLists(pIDBinnacleForm, pIDField)))
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
    ''' <param name="pIDBinnacleForm"></param>
    ''' <param name="pIDField"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParametersLists(ByVal pIDBinnacleForm As Parameter(Of Long), ByVal pIDField As Parameter(Of Long)) As String()
        Dim parameters As New List(Of String)
        Dim priorities As New List(Of Integer)

        If pIDBinnacleForm.Priority <> -1 AndAlso pIDBinnacleForm.Value <> 0 Then
            parameters.Add("IDBinnacleForm = " & pIDBinnacleForm.Value.ToString)
            priorities.Add(pIDBinnacleForm.Priority)
        End If

        If pIDField.Priority <> -1 AndAlso pIDField.Value <> 0 Then
            parameters.Add("IDField = " & pIDField.Value.ToString)
            priorities.Add(pIDField.Priority)
        End If

        Return SortParametersLists(priorities.ToArray, parameters.ToArray)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pIDBinnacleForm"></param>
    ''' <param name="pIDField"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParametersLists(ByVal pIDBinnacleForm As ParameterList(Of Long), ByVal pIDField As ParameterList(Of Long)) As String()()
        Dim parameters As New List(Of String())
        Dim priorities As New List(Of Integer)

        If pIDBinnacleForm.Priority <> -1 AndAlso pIDBinnacleForm.Values.Length > 0 Then
            parameters.Add(GetFilters("IDBinnacleForm", " = ", pIDBinnacleForm.Values))
            priorities.Add(pIDBinnacleForm.Priority)
        End If

        If pIDField.Priority <> -1 AndAlso pIDField.Values.Length > 0 Then
            parameters.Add(GetFilters("IDField", " = ", pIDField.Values))
            priorities.Add(pIDField.Priority)
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
    Private Shared Function GetFilters(ByVal fieldName As String, ByVal paramOperator As String, ByVal values() As Long) As String()
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
            sql &= "  IDBinnacleForm,"
            sql &= "  IDField,"
            sql &= "  " & log & " as Log,"
            sql &= "  OldValue,"
            sql &= "  NewValue"
            sql &= " From"
            sql &= "  prg" & [Enum].GetName(GetType(Logs), log) & "BinnacleFormFields"

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
            sql &= "  IDBinnacleForm,"
            sql &= "  IDField,"
            sql &= "  " & log & " as Log,"
            sql &= "  OldValue,"
            sql &= "  NewValue"
            sql &= " From"
            sql &= "  prg" & [Enum].GetName(GetType(Logs), log) & "BinnacleFormFields"

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
    ''' Execute SQL statement to reads the data of the table prgAdminBinnacleFormFields and
    ''' return a list of DAClsAdminBinnacleFormFields.Struct with each one of the read registries 
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
                        Struct.IDBinnacleForm.SetValue(dr.GetInt64("IDBinnacleForm"))
                        Struct.IDField.SetValue(dr.GetInt64("IDField"))
                        Struct.Log.SetValue(dr.GetInt32("Log"))
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
