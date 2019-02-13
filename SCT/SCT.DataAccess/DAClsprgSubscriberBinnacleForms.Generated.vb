Imports Csla.data
Imports DBConnection
Imports System.Data.OleDb

''' <summary>
''' Represents a controller of data for the prgSubscriberBinnacleForms table in the data base PCS 
''' </summary>
''' <remarks></remarks>
<Serializable()> Public Class DAClsprgSubscriberBinnacleForms

    ''' <summary>
    ''' Represents one structures with the data of a registry of the table prgSubscriberBinnacleForms 
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> Public Class Struct
	
        Public TableName As String = "prgSubscriberBinnacleForms"

        Public ID As StructField(Of Long) = New StructField(Of Long)("ID", True)
        Public IDBinnacle As StructField(Of Long) = New StructField(Of Long)("IDBinnacle", False)
        Public IDForm As StructField(Of Long) = New StructField(Of Long)("IDForm", False)
        Public IDOperation As StructField(Of Long) = New StructField(Of Long)("IDOperation", False)
        Public BHour As StructField(Of Date) = New StructField(Of Date)("BHour", False)

	End Class

    ''' <summary>
    ''' Reads the data of all the SubscriberBinnacleForms in the table prgSubscriberBinnacleForms and return
    ''' a list of DAClsSubscriberBinnacleForms.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsSubscriberBinnacleForms.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch() As Struct()
        Fetch = DoFetch(CreateSql())
    End Function

    ''' <summary>
    ''' Read the data of a certain SubscriberBinnacleForm in the table prgSubscriberBinnacleForms and return
    ''' a list of DAClsSubscriberBinnacleForms.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsSubscriberBinnacleForms.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pID As Parameter(Of Long)) As Struct()
        Fetch = DoFetch(CreateSql("ID = " & pID.Value.ToString))
    End Function

    ''' <summary>
    ''' Reads the data of all the SubscriberBinnacleForms in the table prgSubscriberBinnacleForms and return
    ''' a list of DAClsSubscriberBinnacleForms.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsSubscriberBinnacleForms.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDBinnacle As Parameter(Of Long), ByVal pIDForm As Parameter(Of Long), ByVal pIDOperation As Parameter(Of Long), ByVal pFromHour As Parameter(Of Date), ByVal pToHour As Parameter(Of Date)) As Struct()
        Fetch = DoFetch(CreateSql(GetParametersLists(pIDBinnacle, pIDForm, pIDOperation, pFromHour, pToHour)))
    End Function

    ''' <summary>
    ''' Reads the data of all the SubscriberBinnacleForms in the table prgSubscriberBinnacleForms and return
    ''' a list of DAClsSubscriberBinnacleForms.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsSubscriberBinnacleForms.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDBinnacle As ParameterList(Of Long), ByVal pIDForm As ParameterList(Of Long), ByVal pIDOperation As ParameterList(Of Long), ByVal pFromHour As Parameter(Of Date), ByVal pToHour As Parameter(Of Date)) As Struct()
        Fetch = DoFetch(CreateSql(GetParametersLists(pIDBinnacle, pIDForm, pIDOperation, pFromHour, pToHour)))
    End Function

    ''' <summary>
    ''' Reads the data of all the SubscriberBinnacleForms in the table prgSubscriberBinnacleForms and return
    ''' a list of DAClsSubscriberBinnacleForms.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsSubscriberBinnacleForms.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function FetchByBinnacle(ByVal pIDBinnacle As Parameter(Of Long)) As Struct()
        FetchByBinnacle = DoFetch(CreateSql("IDBinnacle = " & pIDBinnacle.Value.ToString))
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pIDBinnacle"></param>
    ''' <param name="pIDForm"></param>
    ''' <param name="pIDOperation"></param>
    ''' <param name="pFromHour"></param>
    ''' <param name="pToHour"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParametersLists(ByVal pIDBinnacle As Parameter(Of Long), ByVal pIDForm As Parameter(Of Long), ByVal pIDOperation As Parameter(Of Long), ByVal pFromHour As Parameter(Of Date), ByVal pToHour As Parameter(Of Date)) As String()
        Dim params As New List(Of String)
        Dim priorities As New List(Of Integer)

        If pIDBinnacle.Priority <> -1 AndAlso pIDBinnacle.Value <> 0 Then
            params.Add("IDBinnacle = " & pIDBinnacle.Value.ToString)
            priorities.Add(pIDBinnacle.Priority)
        End If

        If pIDForm.Priority <> -1 AndAlso pIDForm.Value <> 0 Then
            params.Add("IDForm = " & pIDForm.Value.ToString)
            priorities.Add(pIDForm.Priority)
        End If

        If pIDOperation.Priority <> -1 AndAlso pIDOperation.Value <> 0 Then
            params.Add("IDOperation = " & pIDOperation.Value.ToString)
            priorities.Add(pIDOperation.Priority)
        End If

        If pFromHour.Priority <> -1 AndAlso pFromHour.Value.TimeOfDay <> Date.MinValue.TimeOfDay Then
            params.Add("BHour >= '" & pFromHour.Value.ToString("HH:mm:ss") & "'")
            priorities.Add(pFromHour.Priority)
        End If

        If pToHour.Priority <> -1 AndAlso pToHour.Value.TimeOfDay <> Date.MaxValue.TimeOfDay Then
            params.Add("BHour <= '" & pToHour.Value.ToString("HH:mm:ss") & "'")
            priorities.Add(pToHour.Priority)
        End If

        Return SortParametersLists(priorities.ToArray, params.ToArray)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pIDBinnacle"></param>
    ''' <param name="pIDForm"></param>
    ''' <param name="pIDOperation"></param>
    ''' <param name="pFromHour"></param>
    ''' <param name="pToHour"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParametersLists(ByVal pIDBinnacle As ParameterList(Of Long), ByVal pIDForm As ParameterList(Of Long), ByVal pIDOperation As ParameterList(Of Long), ByVal pFromHour As Parameter(Of Date), ByVal pToHour As Parameter(Of Date)) As String()()
        Dim params As New List(Of String())
        Dim priorities As New List(Of Integer)

        If pIDBinnacle.Priority <> -1 AndAlso pIDBinnacle.Values.Length > 0 Then
            params.Add(GetFilters("IDBinnacle", " = ", pIDBinnacle.Values))
            priorities.Add(pIDBinnacle.Priority)
        End If

        If pIDForm.Priority <> -1 AndAlso pIDForm.Values.Length > 0 Then
            params.Add(GetFilters("IDForm", " = ", pIDForm.Values))
            priorities.Add(pIDForm.Priority)
        End If

        If pIDOperation.Priority <> -1 AndAlso pIDOperation.Values.Length > 0 Then
            params.Add(GetFilters("IDOperation", " = ", pIDOperation.Values))
            priorities.Add(pIDOperation.Priority)
        End If

        If pFromHour.Priority <> -1 AndAlso pFromHour.Value.TimeOfDay <> Date.MinValue.TimeOfDay Then
            params.Add(GetFilters("BHour", " >= ", pFromHour.Value))
            priorities.Add(pFromHour.Priority)
        End If

        If pToHour.Priority <> -1 AndAlso pToHour.Value.TimeOfDay <> Date.MaxValue.TimeOfDay Then
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
    Private Shared Function CreateSql(ByVal ParamArray pParametersList() As String) As String

        'Build SQL value
        Dim sql As String = ""
        sql &= " Select"
        sql &= "  ID,"
        sql &= "  IDBinnacle,"
        sql &= "  IDForm,"
        sql &= "  IDOperation,"
        sql &= "  BHour"
        sql &= " From"
        sql &= "  prgSubscriberBinnacleForms"

        If pParametersList.Length > 0 Then
            sql &= " Where " & Join(pParametersList, " And ")
        End If

        CreateSql = sql
    End Function

    ''' <summary>
    ''' Creates the Sql Statement and returns it
    ''' </summary>
    ''' <returns>Sql Statement</returns>
    ''' <remarks></remarks>
    Private Shared Function CreateSql(ByVal pParams()() As String) As String

        'Build SQL value
        Dim sql As String = ""
        sql &= " Select"
        sql &= "  ID,"
        sql &= "  IDBinnacle,"
        sql &= "  IDForm,"
        sql &= "  IDOperation,"
        sql &= "  BHour"
        sql &= " From"
        sql &= "  prgSubscriberBinnacleForms"

        Dim list As New List(Of String)
        For Each item As String() In pParams
            If item.Length > 0 Then
                list.Add("(" & Join(item, " Or ") & ")")
            End If
        Next
        If list.Count > 0 Then
            sql &= " Where " & Join(list.ToArray, " And ")
        End If

        CreateSql = sql
    End Function

    ''' <summary>
    ''' Execute SQL statement to reads the data of the table prgSubscriberBinnacleForms and
    ''' return a list of DAClsSubscriberBinnacleForms.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="psql">Sql Statement to execute</param>
    ''' <returns>List of the read registries</returns>
    ''' <remarks></remarks>
    Private Shared Function DoFetch(ByVal psql As String) As Struct()
        Dim Struct As Struct
        Dim List As New List(Of Struct)

        'Connection to the data base 
        Using DBConn As DBClsConnection = New DBClsConnection(DADataBase.SCTConnectionString)

            'Execute SQL statement
            Using dr As SafeDataReader = DBConn.ExecuteReader(psql)

                'Read and add each registry to the list 
                While dr.Read()
                    Struct = New Struct
                    Struct.ID.SetValue(dr.GetInt64("ID"))
                    Struct.IDBinnacle.SetValue(dr.GetInt64("IDBinnacle"))
                    Struct.IDForm.SetValue(dr.GetInt64("IDForm"))
                    Struct.IDOperation.SetValue(dr.GetInt64("IDOperation"))
                    Struct.BHour.SetValue(dr.GetDateTime("BHour"))
                    List.Add(Struct)
                End While
                DoFetch = List.ToArray
            End Using
        End Using
    End Function

    Private Shared Function RefreshStruct(ByVal pStruct As Struct) As Struct
        pStruct.ID.OldValue = pStruct.ID.Value
        pStruct.ID.Value = pStruct.ID.NewValue

        pStruct.IDBinnacle.OldValue = pStruct.IDBinnacle.Value
        pStruct.IDBinnacle.Value = pStruct.IDBinnacle.NewValue

        pStruct.IDForm.OldValue = pStruct.IDForm.Value
        pStruct.IDForm.Value = pStruct.IDForm.NewValue

        pStruct.IDOperation.OldValue = pStruct.IDOperation.Value
        pStruct.IDOperation.Value = pStruct.IDOperation.NewValue

        pStruct.BHour.OldValue = pStruct.BHour.Value
        pStruct.BHour.Value = pStruct.BHour.NewValue

        Return pStruct
    End Function

    ''' <summary>
    ''' Insert a new registry in the table prgSubscriberBinnacleForms and return ID
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the SubscriberBinnacleForm to insert</param>
    ''' <returns>ID</returns>
    ''' <remarks></remarks>
    Public Shared Function Insert(ByVal pStruct As Struct) As Struct

        'Build SQL value for insert
        Dim sql As String = ""
        sql &= " Insert Into prgSubscriberBinnacleForms"
        sql &= "  ("
        sql &= "    IDBinnacle,"
        sql &= "    IDForm,"
        sql &= "    IDOperation,"
        sql &= "    BHour"
        sql &= "  )"
        sql &= " Values"
        sql &= "  ("
        sql &= "     " & pStruct.IDBinnacle.NewValue.ToString & ","
        sql &= "     " & pStruct.IDForm.NewValue.ToString & ","
        sql &= "     " & pStruct.IDOperation.NewValue.ToString & ","
        sql &= "    '" & pStruct.BHour.NewValue.ToString("HH:mm:ss") & "'"
        sql &= "  );"
        sql &= " SELECT SCOPE_IDENTITY()"

        Try
            'Connection to the data base 
            Using DBConn As DBClsConnection = New DBClsConnection(DADataBase.SCTConnectionString)

                'Execute Query
                pStruct.ID.NewValue = DBConn.ExecuteScalar(sql)
                Insert = RefreshStruct(pStruct)
            End Using
        Catch SqlEx As OleDbException
            Select Case SqlEx.ErrorCode
                Case -2147217873
                    Throw New DataException("Duplicate keys or invalid data.")
                Case Else
                    Throw New DataException(SqlEx.ErrorCode & ": " & SqlEx.Message)
            End Select
        End Try
    End Function

    ''' <summary>
    ''' Update a registry in the table prgSubscriberBinnacleForms and return the number of rows affected
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the SubscriberBinnacleForm to update</param>
    ''' <returns>Number of rows affected</returns>
    ''' <remarks></remarks>
    Public Shared Function Update(ByVal pStruct As Struct) As Struct

        'Build SQL value
        Dim sql As String = ""
        sql &= " UPDATE prgSubscriberBinnacleForms"
        sql &= " SET"
        sql &= "  IDBinnacle = " & pStruct.IDBinnacle.NewValue.ToString & ","
        sql &= "  IDForm = " & pStruct.IDForm.NewValue.ToString & ","
        sql &= "  IDOperation = " & pStruct.IDOperation.NewValue.ToString & ","
        sql &= "  BHour = '" & pStruct.BHour.NewValue.ToString("HH:mm:ss") & "'"
        sql &= " Where"
        sql &= "  ID = " & pStruct.ID.NewValue.ToString

        Try
            'Connection to the data base 
            Using DBConn As DBClsConnection = New DBClsConnection(DADataBase.SCTConnectionString)

                'Execute Transact-SQL statement
                DBConn.ExecuteNonQuery(sql)
                Update = RefreshStruct(pStruct)
            End Using
        Catch SqlEx As OleDbException
            Select Case SqlEx.ErrorCode
                Case -2147217873
                    Throw New DataException("Duplicate keys or invalid data.")
                Case Else
                    Throw New DataException(SqlEx.ErrorCode & ": " & SqlEx.Message)
            End Select
        End Try
    End Function

    ''' <summary>
    ''' Delete a registry in the table prgSubscriberBinnacleForms and return the number of rows affected
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub Delete(ByVal pID As Long)

        'Build SQL value
        Dim sql As String = ""
        sql &= " Delete From prgSubscriberBinnacleForms"
        sql &= " Where"
        sql &= "  ID = " & pID.ToString

        Try
            'Connection to the data base 
            Using DBConn As DBClsConnection = New DBClsConnection(DADataBase.SCTConnectionString)

                'Execute Transact-SQL statement
                DBConn.ExecuteNonQuery(sql)
            End Using
        Catch SqlEx As OleDbException
            Select Case SqlEx.ErrorCode
                Case -2147217873
                    Throw New DataException("Record is reference in another record.")
                Case Else
                    Throw New DataException()
            End Select
        End Try
    End Sub

End Class
