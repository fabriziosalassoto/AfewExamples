Imports Csla.data
Imports DBConnection
Imports System.Data.OleDb

''' <summary>
''' Represents a controller of data for the prgSubscriberBinnacleTableFields table in the data base PCS 
''' </summary>
''' <remarks></remarks>
<Serializable()> Public Class DAClsprgSubscriberBinnacleTableFields

    ''' <summary>
    ''' Represents one structures with the data of a registry of the table prgSubscriberBinnacleTableFields 
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> Public Class Struct
	
        Public TableName As String = "prgAdvertiserBinnacleTableFields"

        Public ID As StructField(Of Long) = New StructField(Of Long)("ID", True)
        Public IDBinnacleTable As StructField(Of Long) = New StructField(Of Long)("IDBinnacleTable", False)
        Public FieldName As StructField(Of String) = New StructField(Of String)("FieldName", False)
        Public OldValue As StructField(Of String) = New StructField(Of String)("OldValue", False)
        Public NewValue As StructField(Of String) = New StructField(Of String)("NewValue", False)

	End Class

    ''' <summary>
    ''' Reads the data of all the SubscriberBinnacleTableFields in the table prgSubscriberBinnacleTableFields and return
    ''' a list of DAClsSubscriberBinnacleTableFields.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsSubscriberBinnacleTableFields.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch() As Struct()
        Fetch = DoFetch(CreateSql())
    End Function

    ''' <summary>
    ''' Read the data of a certain SubscriberBinnacleTableField in the table prgSubscriberBinnacleTableFields and return
    ''' a list of DAClsSubscriberBinnacleTableFields.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsSubscriberBinnacleTableFields.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pID As Parameter(Of Long)) As Struct()
        Fetch = DoFetch(CreateSql("ID = " & pID.Value.ToString))
    End Function

    ''' <summary>
    ''' Read the data of a certain SubscriberBinnacleTableField in the table prgSubscriberBinnacleTableFields and return
    ''' a list of DAClsSubscriberBinnacleTableFields.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsSubscriberBinnacleTableFields.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDBinnacleTable As Parameter(Of Long), ByVal pFieldName As Parameter(Of String)) As Struct()
        Fetch = DoFetch(CreateSql(GetParametersLists(pIDBinnacleTable, pFieldName)))
    End Function

    ''' <summary>
    ''' Read the data of a certain SubscriberBinnacleTableField in the table prgSubscriberBinnacleTableFields and return
    ''' a list of DAClsSubscriberBinnacleTableFields.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsSubscriberBinnacleTableFields.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDBinnacleTable As ParameterList(Of Long), ByVal pFieldName As ParameterList(Of String)) As Struct()
        Fetch = DoFetch(CreateSql(GetParametersLists(pIDBinnacleTable, pFieldName)))
    End Function

    ''' <summary>
    ''' Read the data of a certain SubscriberBinnacleTableField in the table prgSubscriberBinnacleTableFields and return
    ''' a list of DAClsSubscriberBinnacleTableFields.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsSubscriberBinnacleTableFields.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function FetchByBinnacleTable(ByVal pIDBinnacleTable As Parameter(Of Long)) As Struct()
        FetchByBinnacleTable = DoFetch(CreateSql("IDBinnacleTable = " & pIDBinnacleTable.Value.ToString))
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
            parameters.Add(GetFilters("IDBinnacleTable", " = ", pIDBinnacleTable.Values))
            priorities.Add(pIDBinnacleTable.Priority)
        End If

        If pFieldName.Priority <> -1 AndAlso pFieldName.Values.Length > 0 Then
            parameters.Add(GetFilters("FieldName", " = ", pFieldName.Values))
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
    ''' <param name="values"></param>
    ''' <param name="fieldName"></param>
    ''' <param name="paramOperator"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetFilters(ByVal fieldName As String, ByVal paramOperator As String, ByVal values() As String) As String()
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
    Private Shared Function CreateSql(ByVal ParamArray pParametersList() As String) As String

        'Build SQL value
        Dim sql As String = ""
        sql &= " Select"
        sql &= "  ID,"
        sql &= "  IDBinnacleTable,"
        sql &= "  FieldName,"
        sql &= "  OldValue,"
        sql &= "  NewValue"
        sql &= " From"
        sql &= "  prgSubscriberBinnacleTableFields"

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
        sql &= "  IDBinnacleTable,"
        sql &= "  FieldName,"
        sql &= "  OldValue,"
        sql &= "  NewValue"
        sql &= " From"
        sql &= "  prgSubscriberBinnacleTableFields"

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
    ''' Execute SQL statement to reads the data of the table prgSubscriberBinnacleTableFields and
    ''' return a list of DAClsSubscriberBinnacleTableFields.Struct with each one of the read registries 
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
                    Struct.IDBinnacleTable.SetValue(dr.GetInt64("IDBinnacleTable"))
                    Struct.FieldName.SetValue(dr.GetString("FieldName"))
                    Struct.OldValue.SetValue(dr.GetString("OldValue"))
                    Struct.NewValue.SetValue(dr.GetString("NewValue"))
                    List.Add(Struct)
                End While
                DoFetch = List.ToArray
            End Using
        End Using
    End Function

    Private Shared Function RefreshStruct(ByVal pStruct As Struct) As Struct
        pStruct.ID.OldValue = pStruct.ID.Value
        pStruct.ID.Value = pStruct.ID.NewValue

        pStruct.IDBinnacleTable.OldValue = pStruct.IDBinnacleTable.Value
        pStruct.IDBinnacleTable.Value = pStruct.IDBinnacleTable.NewValue

        pStruct.FieldName.OldValue = pStruct.FieldName.Value
        pStruct.FieldName.Value = pStruct.FieldName.NewValue

        pStruct.OldValue.OldValue = pStruct.OldValue.Value
        pStruct.OldValue.Value = pStruct.OldValue.NewValue

        pStruct.NewValue.OldValue = pStruct.NewValue.Value
        pStruct.NewValue.Value = pStruct.NewValue.NewValue

        Return pStruct
    End Function

    ''' <summary>
    ''' Insert a new registry in the table prgSubscriberBinnacleTableFields and return ID
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the SubscriberBinnacleTableField to insert</param>
    ''' <returns>ID</returns>
    ''' <remarks></remarks>
    Public Shared Function Insert(ByVal pStruct As Struct) As Struct

        'Build SQL value for insert
        Dim sql As String = ""
        sql &= " Insert Into prgSubscriberBinnacleTableFields"
        sql &= "  ("
        sql &= "    IDBinnacleTable,"
        sql &= "    FieldName,"
        sql &= "    OldValue,"
        sql &= "    NewValue"
        sql &= "  )"
        sql &= " Values"
        sql &= "  ("
        sql &= "     " & pStruct.IDBinnacleTable.NewValue.ToString & ","
        sql &= "    '" & pStruct.FieldName.NewValue & "',"
        sql &= "    '" & pStruct.OldValue.NewValue & "',"
        sql &= "    '" & pStruct.NewValue.NewValue & "'"
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
    ''' Update a registry in the table prgSubscriberBinnacleTableFields and return the number of rows affected
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the SubscriberBinnacleTableField to update</param>
    ''' <returns>Number of rows affected</returns>
    ''' <remarks></remarks>
    Public Shared Function Update(ByVal pStruct As Struct) As Struct

        'Build SQL value
        Dim sql As String = ""
        sql &= " UPDATE prgSubscriberBinnacleTableFields"
        sql &= " SET"
        sql &= "  IDBinnacleTable = " & pStruct.IDBinnacleTable.NewValue.ToString & ","
        sql &= "  FieldName = '" & pStruct.FieldName.NewValue & "',"
        sql &= "  OldValue = '" & pStruct.OldValue.NewValue & "',"
        sql &= "  NewValue = '" & pStruct.NewValue.NewValue & "'"
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
    ''' Delete a registry in the table prgSubscriberBinnacleTableFields and return the number of rows affected
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub Delete(ByVal pID As Long)

        'Build SQL value
        Dim sql As String = ""
        sql &= " Delete From prgSubscriberBinnacleTableFields"
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
