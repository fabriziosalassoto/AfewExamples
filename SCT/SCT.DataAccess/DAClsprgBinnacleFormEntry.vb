Imports Csla.data
Imports DBConnection
Imports System.Data.OleDb

''' <summary>
''' Represents a controller of data for the prgAdminBinnacleForms table in the data base PCS 
''' </summary>
''' <remarks></remarks>
<Serializable()> Public Class DAClsprgBinnacleFormEntry

    ''' <summary>
    ''' Represents one structures with the data of a registry of the table prgAdminBinnacleForms 
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> Public Class Struct
        Public TableName As String = String.Empty

        Public ID As StructField(Of Long) = New StructField(Of Long)("ID", True)
        Public IDUser As StructField(Of Long) = New StructField(Of Long)("IDUser", False)
        Public IDForm As StructField(Of Long) = New StructField(Of Long)("IDForm", False)
        Public IDOperation As StructField(Of Long) = New StructField(Of Long)("IDOperation", False)
        Public Log As StructField(Of Logs) = New StructField(Of Logs)("Log", False)
        Public BDate As StructField(Of Date) = New StructField(Of Date)("BDate", False)
        Public BHour As StructField(Of Date) = New StructField(Of Date)("BHour", False)

    End Class

    ''' <summary>
    ''' Reads the data of all the AdminBinnacleForms in the table prgAdminBinnacleForms and return
    ''' a list of DAClsAdminBinnacleForms.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleForms.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLog As Logs, ByVal pID As Parameter(Of Long)) As Struct()
        Dim Logs() As Logs = GetLog(pLog)
        Fetch = DoFetch(Logs, CreateSql(Logs, "prg" & [Enum].GetName(GetType(Logs), pLog) & "BinnacleForms.ID = " & pID.Value.ToString))
    End Function

    ''' <summary>
    ''' Reads the data of all the AdminBinnacleForms in the table prgAdminBinnacleForms and return
    ''' a list of DAClsAdminBinnacleForms.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleForms.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch() As Struct()
        Dim Logs() As Logs = GetLogList(0)
        Fetch = DoFetch(Logs, CreateSql(Logs))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacleForm in the table prgAdminBinnacleForms and return
    ''' a list of DAClsAdminBinnacleForms.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleForms.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDUser As Parameter(Of Long), ByVal pIDForm As Parameter(Of Long), ByVal pIDOperation As Parameter(Of Long), ByVal pFromDate As Parameter(Of Date), ByVal pToDate As Parameter(Of Date), ByVal pFromHour As Parameter(Of Date), ByVal pToHour As Parameter(Of Date)) As Struct()
        Dim Logs() As Logs = GetLogList(0)
        Fetch = DoFetch(Logs, CreateSql(Logs, GetParametersLists(pIDUser, pIDForm, pIDOperation, pFromDate, pToDate, pFromHour, pToHour)))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacleForm in the table prgAdminBinnacleForms and return
    ''' a list of DAClsAdminBinnacleForms.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleForms.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDUser As ParameterList(Of Long), ByVal pIDForm As ParameterList(Of Long), ByVal pIDOperation As ParameterList(Of Long), ByVal pFromDate As Parameter(Of Date), ByVal pToDate As Parameter(Of Date), ByVal pFromHour As Parameter(Of Date), ByVal pToHour As Parameter(Of Date)) As Struct()
        Dim Logs() As Logs = GetLogList(0)
        Fetch = DoFetch(Logs, CreateSql(Logs, GetParametersLists(pIDUser, pIDForm, pIDOperation, pFromDate, pToDate, pFromHour, pToHour)))
    End Function

    ''' <summary>
    ''' Reads the data of all the AdminBinnacleForms in the table prgAdminBinnacleForms and return
    ''' a list of DAClsAdminBinnacleForms.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleForms.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLog As Logs) As Struct()
        Dim Logs() As Logs = GetLogList(pLog)
        Fetch = DoFetch(Logs, CreateSql(Logs))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacleForm in the table prgAdminBinnacleForms and return
    ''' a list of DAClsAdminBinnacleForms.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleForms.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLog As Logs, ByVal pIDUser As Parameter(Of Long), ByVal pIDForm As Parameter(Of Long), ByVal pIDOperation As Parameter(Of Long), ByVal pFromDate As Parameter(Of Date), ByVal pToDate As Parameter(Of Date), ByVal pFromHour As Parameter(Of Date), ByVal pToHour As Parameter(Of Date)) As Struct()
        Dim Logs() As Logs = GetLogList(pLog)
        Fetch = DoFetch(Logs, CreateSql(Logs, GetParametersLists(pIDUser, pIDForm, pIDOperation, pFromDate, pToDate, pFromHour, pToHour)))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacleForm in the table prgAdminBinnacleForms and return
    ''' a list of DAClsAdminBinnacleForms.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleForms.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLog As Logs, ByVal pIDUser As ParameterList(Of Long), ByVal pIDForm As ParameterList(Of Long), ByVal pIDOperation As ParameterList(Of Long), ByVal pFromDate As Parameter(Of Date), ByVal pToDate As Parameter(Of Date), ByVal pFromHour As Parameter(Of Date), ByVal pToHour As Parameter(Of Date)) As Struct()
        Dim Logs() As Logs = GetLogList(pLog)
        Fetch = DoFetch(Logs, CreateSql(Logs, GetParametersLists(pIDUser, pIDForm, pIDOperation, pFromDate, pToDate, pFromHour, pToHour)))
    End Function

    ''' <summary>
    ''' Reads the data of all the AdminBinnacleForms in the table prgAdminBinnacleForms and return
    ''' a list of DAClsAdminBinnacleForms.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleForms.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLogs() As Logs) As Struct()
        Dim Logs() As Logs = GetLogList(pLogs)
        Fetch = DoFetch(Logs, CreateSql(Logs))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacleForm in the table prgAdminBinnacleForms and return
    ''' a list of DAClsAdminBinnacleForms.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleForms.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLogs() As Logs, ByVal pIDUser As Parameter(Of Long), ByVal pIDForm As Parameter(Of Long), ByVal pIDOperation As Parameter(Of Long), ByVal pFromDate As Parameter(Of Date), ByVal pToDate As Parameter(Of Date), ByVal pFromHour As Parameter(Of Date), ByVal pToHour As Parameter(Of Date)) As Struct()
        Dim Logs() As Logs = GetLogList(pLogs)
        Fetch = DoFetch(Logs, CreateSql(Logs, GetParametersLists(pIDUser, pIDForm, pIDOperation, pFromDate, pToDate, pFromHour, pToHour)))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacleForm in the table prgAdminBinnacleForms and return
    ''' a list of DAClsAdminBinnacleForms.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacleForms.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLogs() As Logs, ByVal pIDUser As ParameterList(Of Long), ByVal pIDForm As ParameterList(Of Long), ByVal pIDOperation As ParameterList(Of Long), ByVal pFromDate As Parameter(Of Date), ByVal pToDate As Parameter(Of Date), ByVal pFromHour As Parameter(Of Date), ByVal pToHour As Parameter(Of Date)) As Struct()
        Dim Logs() As Logs = GetLogList(pLogs)
        Fetch = DoFetch(Logs, CreateSql(Logs, GetParametersLists(pIDUser, pIDForm, pIDOperation, pFromDate, pToDate, pFromHour, pToHour)))
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
    ''' <param name="plogs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetLogList(ByVal plogs() As Logs) As Logs()
        Dim logs As New List(Of Logs)
        For Each log As Logs In plogs
            If log = 0 Then
                Return New List(Of Logs)([Enum].GetValues(GetType(Logs))).ToArray
            ElseIf [Enum].IsDefined(GetType(Logs), log) Then
                logs.Add(log)
            End If
        Next
        Return logs.ToArray
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pIDUser"></param>
    ''' <param name="pIDForm"></param>
    ''' <param name="pIDOperation"></param>
    ''' <param name="pFromDate"></param>
    ''' <param name="pToDate"></param>
    ''' <param name="pFromHour"></param>
    ''' <param name="pToHour"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParametersLists(ByVal pIDUser As Parameter(Of Long), ByVal pIDForm As Parameter(Of Long), ByVal pIDOperation As Parameter(Of Long), ByVal pFromDate As Parameter(Of Date), ByVal pToDate As Parameter(Of Date), ByVal pFromHour As Parameter(Of Date), ByVal pToHour As Parameter(Of Date)) As String()
        Dim parameters As New List(Of String)
        Dim priorities As New List(Of Integer)

        If pIDUser.Priority <> -1 AndAlso pIDUser.Value <> 0 Then
            parameters.Add("IDUser = " & pIDUser.Value.ToString)
            priorities.Add(pIDUser.Priority)
        End If

        If pIDForm.Priority <> -1 AndAlso pIDForm.Value <> 0 Then
            parameters.Add("IDForm = " & pIDForm.Value.ToString)
            priorities.Add(pIDForm.Priority)
        End If

        If pIDOperation.Priority <> -1 AndAlso pIDOperation.Value <> 0 Then
            parameters.Add("IDOperation = " & pIDOperation.Value.ToString)
            priorities.Add(pIDOperation.Priority)
        End If

        If pFromDate.Priority <> -1 AndAlso pFromDate.Value.Date > New Date(1900, 1, 1).Date AndAlso pFromDate.Value.Date <= Date.MaxValue.Date Then
            parameters.Add("BDate >= '" & pFromDate.Value.ToString("yyyy-M-dd") & "'")
            priorities.Add(pFromDate.Priority)
        End If

        If pToDate.Priority <> -1 AndAlso pToDate.Value.Date >= New Date(1900, 1, 1).Date AndAlso pToDate.Value.Date < Date.MaxValue.Date Then
            parameters.Add("BDate <= '" & pToDate.Value.ToString("yyyy-M-dd") & "'")
            priorities.Add(pToDate.Priority)
        End If

        If pFromHour.Priority <> -1 AndAlso pFromHour.Value.TimeOfDay > Date.MinValue.TimeOfDay Then
            parameters.Add("BHour >= '" & pFromHour.Value.ToString("HH:mm:ss") & "'")
            priorities.Add(pFromHour.Priority)
        End If

        If pToHour.Priority <> -1 AndAlso pToHour.Value.TimeOfDay < Date.MaxValue.TimeOfDay Then
            parameters.Add("BHour <= '" & pToHour.Value.ToString("HH:mm:ss") & "'")
            priorities.Add(pToHour.Priority)
        End If

        Return SortParametersLists(priorities.ToArray, parameters.ToArray)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pIDUser"></param>
    ''' <param name="pIDForm"></param>
    ''' <param name="pIDOperation"></param>
    ''' <param name="pFromDate"></param>
    ''' <param name="pToDate"></param>
    ''' <param name="pFromHour"></param>
    ''' <param name="pToHour"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParametersLists(ByVal pIDUser As ParameterList(Of Long), ByVal pIDForm As ParameterList(Of Long), ByVal pIDOperation As ParameterList(Of Long), ByVal pFromDate As Parameter(Of Date), ByVal pToDate As Parameter(Of Date), ByVal pFromHour As Parameter(Of Date), ByVal pToHour As Parameter(Of Date)) As String()()
        Dim parameters As New List(Of String())
        Dim priorities As New List(Of Integer)

        If pIDUser.Priority <> -1 AndAlso pIDUser.Values.Length > 0 Then
            parameters.Add(GetParametersList("IDUser", " = ", pIDUser.Values))
            priorities.Add(pIDUser.Priority)
        End If

        If pIDForm.Priority <> -1 AndAlso pIDForm.Values.Length > 0 Then
            parameters.Add(GetParametersList("IDForm", " = ", pIDForm.Values))
            priorities.Add(pIDForm.Priority)
        End If

        If pIDOperation.Priority <> -1 AndAlso pIDOperation.Values.Length > 0 Then
            parameters.Add(GetParametersList("IDOperation", " = ", pIDOperation.Values))
            priorities.Add(pIDOperation.Priority)
        End If

        If pFromDate.Priority <> -1 AndAlso pFromDate.Value.Date > New Date(1900, 1, 1).Date AndAlso pFromDate.Value.Date <= Date.MaxValue.Date Then
            parameters.Add(GetParametersList("BDate", " >= ", pFromDate.Value, "yyyy-M-dd"))
            priorities.Add(pFromDate.Priority)
        End If

        If pToDate.Priority <> -1 AndAlso pToDate.Value.Date >= New Date(1900, 1, 1).Date AndAlso pToDate.Value.Date < Date.MaxValue.Date Then
            parameters.Add(GetParametersList("BDate", " <= ", pToDate.Value, "yyyy-M-dd"))
            priorities.Add(pToDate.Priority)
        End If

        If pFromHour.Priority <> -1 AndAlso pFromHour.Value.TimeOfDay > Date.MinValue.TimeOfDay Then
            parameters.Add(GetParametersList("BHour", " >= ", pFromHour.Value, "HH:mm:ss"))
            priorities.Add(pFromHour.Priority)
        End If

        If pToHour.Priority <> -1 AndAlso pToHour.Value.TimeOfDay < Date.MaxValue.TimeOfDay Then
            parameters.Add(GetParametersList("BHour", " <= ", pToHour.Value, "HH:mm:ss"))
            priorities.Add(pToHour.Priority)
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
    Private Shared Function GetParametersList(ByVal fieldName As String, ByVal paramOperator As String, ByVal values() As Long) As String()
        Dim parameter As New List(Of String)
        For Each value As Long In values
            If value <> 0 Then
                parameter.Add(fieldName & paramOperator & value.ToString)
            End If
        Next
        Return parameter.ToArray
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="value"></param>
    ''' <param name="fieldName"></param>
    ''' <param name="paramOperator"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParametersList(ByVal fieldName As String, ByVal paramOperator As String, ByVal value As Date, ByVal format As String) As String()
        Dim parameter As New List(Of String)
        parameter.Add(fieldName & paramOperator & "'" & value.ToString(format) & "'")
        Return parameter.ToArray
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="keys"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function SortParametersLists(ByVal keys As Integer(), ByVal parameters As String()) As String()
        Array.Sort(keys, parameters)
        Return parameters
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="keys"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function SortParametersLists(ByVal keys As Integer(), ByVal parameters As String()()) As String()()
        Array.Sort(keys, parameters)
        Return parameters
    End Function

    ''' <summary>
    ''' Creates the Sql Statement and returns it
    ''' </summary>
    ''' <param name="pParametersList"></param>
    ''' <returns>Sql Statement</returns>
    ''' <remarks></remarks>
    Private Shared Function CreateSql(ByVal pLogs() As Logs, ByVal ParamArray pParametersList() As String) As String
        Dim sqls As New List(Of String)

        For Each log As Logs In pLogs

            'Build SQL value
            Dim sql As String = ""
            sql &= " Select"
            sql &= "  prg" & [Enum].GetName(GetType(Logs), log) & "BinnacleForms.ID,"
            sql &= "  " & log & " as Log,"
            sql &= "  IDUser,"
            sql &= "  IDForm,"
            sql &= "  IDOperation,"
            sql &= "  BDate,"
            sql &= "  BHour"
            sql &= " From"
            sql &= "  prg" & [Enum].GetName(GetType(Logs), log) & "Binnacle"
            sql &= " Inner Join"
            sql &= "  prg" & [Enum].GetName(GetType(Logs), log) & "BinnacleForms"
            sql &= " On"
            sql &= "  prg" & [Enum].GetName(GetType(Logs), log) & "Binnacle.ID = prg" & [Enum].GetName(GetType(Logs), log) & "BinnacleForms.IDBinnacle"

            If pParametersList.Length > 0 Then
                sql &= " Where " & Join(pParametersList, " And ")
            End If

            sqls.Add(sql)
        Next

        CreateSql = Join(sqls.ToArray, " Union ")
    End Function

    ''' <summary>
    ''' Creates the Sql Statement and returns it
    ''' </summary>
    ''' <returns>Sql Statement</returns>
    ''' <remarks></remarks>
    Private Shared Function CreateSql(ByVal pLogs() As Logs, ByVal pParams()() As String) As String
        Dim sqls As New List(Of String)

        For Each log As Logs In pLogs

            'Build SQL value
            Dim sql As String = ""
            sql &= " Select"
            sql &= "  prg" & [Enum].GetName(GetType(Logs), log) & "BinnacleForms.ID,"
            sql &= "  " & log & " as Log,"
            sql &= "  IDUser,"
            sql &= "  IDForm,"
            sql &= "  IDOperation,"
            sql &= "  BDate,"
            sql &= "  BHour"
            sql &= " From"
            sql &= "  prg" & [Enum].GetName(GetType(Logs), log) & "Binnacle"
            sql &= " Inner Join"
            sql &= "  prg" & [Enum].GetName(GetType(Logs), log) & "BinnacleForms"
            sql &= " On"
            sql &= "  prg" & [Enum].GetName(GetType(Logs), log) & "Binnacle.ID = prg" & [Enum].GetName(GetType(Logs), log) & "BinnacleForms.IDBinnacle"

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
    ''' Execute SQL statement to reads the data of the table prgAdminBinnacleForms and
    ''' return a list of DAClsAdminBinnacleForms.Struct with each one of the read registries 
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

                        For Each log As Logs In pLogs
                            Struct.TableName &= "prg" & [Enum].GetName(GetType(Logs), log) & "BinnacleForms"
                        Next

                        Struct.ID.SetValue(dr.GetInt64("ID"))
                        Struct.IDUser.SetValue(dr.GetInt64("IDUser"))
                        Struct.IDForm.SetValue(dr.GetInt64("IDForm"))
                        Struct.IDOperation.SetValue(dr.GetInt64("IDOperation"))
                        Struct.Log.SetValue(dr.GetInt32("Log"))
                        Struct.BDate.SetValue(dr.GetDateTime("BDate"))
                        Struct.BHour.SetValue(dr.GetDateTime("BHour"))
                        List.Add(Struct)
                    End While
                End Using
            End Using
        End If

        DoFetch = List.ToArray
    End Function

End Class