Imports Csla.data
Imports DBConnection
Imports System.Data.OleDb

''' <summary>
''' Represents a controller of data for the prgAdminBinnacle table in the data base PCS 
''' </summary>
''' <remarks></remarks>
<Serializable()> Public Class DAClsprgBinnacle

    ''' <summary>
    ''' Represents one structures with the data of a registry of the table prgAdminBinnacle 
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> Public Class Struct

        Public TableName As String = String.Empty

        Public ID As StructField(Of Long) = New StructField(Of Long)("ID", True)
        Public IDUser As StructField(Of Long) = New StructField(Of Long)("IDUser", False)
        Public Log As StructField(Of Logs) = New StructField(Of Logs)("Log", False)
        Public BDate As StructField(Of Date) = New StructField(Of Date)("BDate", False)

    End Class

    ''' <summary>
    ''' Read the data of a certain AdminBinnacle in the table prgAdminBinnacle and return
    ''' a list of DAClsAdminBinnacles.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacles.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLog As Logs, ByVal pID As Parameter(Of Long)) As Struct()
        Dim Logs() As Logs = GetLog(pLog)
        Fetch = DoFetch(Logs, CreateSql(Logs, "ID = " & pID.Value.ToString))
    End Function

    ''' <summary>
    ''' Reads the data of all the AdminBinnacles in the table prgAdminBinnacle and return
    ''' a list of DAClsAdminBinnacles.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacles.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch() As Struct()
        Dim Logs() As Logs = GetLogList(0)
        Fetch = DoFetch(Logs, CreateSql(Logs))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacle in the table prgAdminBinnacle and return
    ''' a list of DAClsAdminBinnacles.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacles.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDUser As Parameter(Of Long), ByVal pFromDate As Parameter(Of Date), ByVal pToDate As Parameter(Of Date)) As Struct()
        Dim Logs() As Logs = GetLogList(0)
        Fetch = DoFetch(Logs, CreateSql(Logs, GetParametersLists(pIDUser, pFromDate, pToDate)))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacle in the table prgAdminBinnacle and return
    ''' a list of DAClsAdminBinnacles.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacles.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDUser As ParameterList(Of Long), ByVal pFromDate As Parameter(Of Date), ByVal pToDate As Parameter(Of Date)) As Struct()
        Dim Logs() As Logs = GetLogList(0)
        Fetch = DoFetch(Logs, CreateSql(Logs, GetParametersLists(pIDUser, pFromDate, pToDate)))
    End Function

    ''' <summary>
    ''' Reads the data of all the AdminBinnacles in the table prgAdminBinnacle and return
    ''' a list of DAClsAdminBinnacles.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacles.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLog As Logs) As Struct()
        Dim Logs() As Logs = GetLogList(pLog)
        Fetch = DoFetch(Logs, CreateSql(Logs))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacle in the table prgAdminBinnacle and return
    ''' a list of DAClsAdminBinnacles.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacles.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLog As Logs, ByVal pIDUser As Parameter(Of Long), ByVal pFromDate As Parameter(Of Date), ByVal pToDate As Parameter(Of Date)) As Struct()
        Dim Logs() As Logs = GetLogList(pLog)
        Fetch = DoFetch(Logs, CreateSql(Logs, GetParametersLists(pIDUser, pFromDate, pToDate)))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacle in the table prgAdminBinnacle and return
    ''' a list of DAClsAdminBinnacles.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacles.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLog As Logs, ByVal pIDUser As ParameterList(Of Long), ByVal pFromDate As Parameter(Of Date), ByVal pToDate As Parameter(Of Date)) As Struct()
        Dim Logs() As Logs = GetLogList(pLog)
        Fetch = DoFetch(Logs, CreateSql(Logs, GetParametersLists(pIDUser, pFromDate, pToDate)))
    End Function

    ''' <summary>
    ''' Reads the data of all the AdminBinnacles in the table prgAdminBinnacle and return
    ''' a list of DAClsAdminBinnacles.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacles.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLog() As Logs) As Struct()
        Dim Logs() As Logs = GetLogList(pLog)
        Fetch = DoFetch(Logs, CreateSql(Logs))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacle in the table prgAdminBinnacle and return
    ''' a list of DAClsAdminBinnacles.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacles.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLog() As Logs, ByVal pIDUser As Parameter(Of Long), ByVal pFromDate As Parameter(Of Date), ByVal pToDate As Parameter(Of Date)) As Struct()
        Dim Logs() As Logs = GetLogList(pLog)
        Fetch = DoFetch(Logs, CreateSql(Logs, GetParametersLists(pIDUser, pFromDate, pToDate)))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacle in the table prgAdminBinnacle and return
    ''' a list of DAClsAdminBinnacles.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacles.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLog() As Logs, ByVal pIDUser As ParameterList(Of Long), ByVal pFromDate As Parameter(Of Date), ByVal pToDate As Parameter(Of Date)) As Struct()
        Dim Logs() As Logs = GetLogList(pLog)
        Fetch = DoFetch(Logs, CreateSql(Logs, GetParametersLists(pIDUser, pFromDate, pToDate)))
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
    ''' <param name="pIDUser"></param>
    ''' <param name="pFromDate"></param>
    ''' <param name="pToDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParametersLists(ByVal pIDUser As Parameter(Of Long), ByVal pFromDate As Parameter(Of Date), ByVal pToDate As Parameter(Of Date)) As String()
        Dim parameters As New List(Of String)
        Dim priorities As New List(Of Integer)

        If pIDUser.Priority <> -1 AndAlso pIDUser.Value <> 0 Then
            parameters.Add("IDUser = " & pIDUser.Value)
            priorities.Add(pIDUser.Priority)
        End If

        If pFromDate.Priority <> -1 AndAlso pFromDate.Value.Date > New Date(1900, 1, 1).Date AndAlso pFromDate.Value.Date <= Date.MaxValue.Date Then
            parameters.Add("BDate >= " & pFromDate.Value.ToString("yyyy-M-dd"))
            priorities.Add(pFromDate.Priority)
        End If

        If pToDate.Priority <> -1 AndAlso pToDate.Value.Date >= New Date(1900, 1, 1).Date AndAlso pToDate.Value.Date < Date.MaxValue.Date Then
            parameters.Add("BDate <= " & pToDate.Value.ToString("yyyy-M-dd"))
            priorities.Add(pToDate.Priority)
        End If

        Return SortParametersLists(priorities.ToArray, parameters.ToArray)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pIDUser"></param>
    ''' <param name="pFromDate"></param>
    ''' <param name="pToDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParametersLists(ByVal pIDUser As ParameterList(Of Long), ByVal pFromDate As Parameter(Of Date), ByVal pToDate As Parameter(Of Date)) As String()()
        Dim parameters As New List(Of String())
        Dim priorities As New List(Of Integer)

        If pIDUser.Priority <> -1 AndAlso pIDUser.Values.Length > 0 Then
            parameters.Add(GetParametersList("IDUser", " = ", pIDUser.Values))
            priorities.Add(pIDUser.Priority)
        End If

        If pFromDate.Priority <> -1 AndAlso pFromDate.Value.Date > New Date(1900, 1, 1).Date AndAlso pFromDate.Value.Date <= Date.MaxValue.Date Then
            parameters.Add(GetParametersList("BDate", " >= ", pFromDate.Value))
            priorities.Add(pFromDate.Priority)
        End If

        If pToDate.Priority <> -1 AndAlso pToDate.Value.Date >= New Date(1900, 1, 1).Date AndAlso pToDate.Value.Date < Date.MaxValue.Date Then
            parameters.Add(GetParametersList("BDate", " <= ", pToDate.Value))
            priorities.Add(pToDate.Priority)
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
        Dim param As New List(Of String)
        For Each value As Long In values
            param.Add(fieldName & paramOperator & value.ToString)
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
    Private Shared Function GetParametersList(ByVal fieldName As String, ByVal paramOperator As String, ByVal value As Date) As String()
        Dim param As New List(Of String)
        param.Add(fieldName & paramOperator & "'" & value.ToString("yyyy-M-dd") & "'")
        Return param.ToArray
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
            sql &= "  ID,"
            sql &= "  " & log & " as Log,"
            sql &= "  IDUser,"
            sql &= "  BDate"
            sql &= " From"
            sql &= "  prg" & [Enum].GetName(GetType(Logs), log) & "Binnacle"

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
            sql &= "  " & log & " as Log,"
            sql &= "  IDUser,"
            sql &= "  BDate"
            sql &= " From"
            sql &= "  prg" & [Enum].GetName(GetType(Logs), log) & "Binnacle"

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
    ''' Execute SQL statement to reads the data of the table prgAdminBinnacle and
    ''' return a list of DAClsAdminBinnacles.Struct with each one of the read registries 
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
                        Struct.Log.SetValue(dr.GetInt32("Log"))
                        Struct.BDate.SetValue(dr.GetDateTime("BDate"))
                        List.Add(Struct)
                    End While
                End Using
            End Using
        End If
        DoFetch = List.ToArray
    End Function

End Class