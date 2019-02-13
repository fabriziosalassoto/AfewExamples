Imports Csla.data
Imports DBConnection
Imports System.Data.OleDb

''' <summary>
''' Represents a controller of data for the prgSubscriberBinnacleFormFields table in the data base PCS 
''' </summary>
''' <remarks></remarks>
<Serializable()> Public Class DAClsprgSubscriberBinnacleFormFields

    ''' <summary>
    ''' Represents one structures with the data of a registry of the table prgSubscriberBinnacleFormFields 
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> Public Class Struct

        Public TableName As String = "prgSubscriberBinnacleFormFields"

        Public ID As StructField(Of Long) = New StructField(Of Long)("ID", True)
        Public IDBinnacleForm As StructField(Of Long) = New StructField(Of Long)("IDBinnacleForm", False)
        Public IDField As StructField(Of Long) = New StructField(Of Long)("IDField", False)
        Public OldValue As StructField(Of String) = New StructField(Of String)("OldValue", False)
        Public NewValue As StructField(Of String) = New StructField(Of String)("NewValue", False)

	End Class

    ''' <summary>
    ''' Reads the data of all the SubscriberBinnacleFormFields in the table prgSubscriberBinnacleFormFields and return
    ''' a list of DAClsSubscriberBinnacleFormFields.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsSubscriberBinnacleFormFields.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch() As Struct()
        Fetch = DoFetch(CreateSql())
    End Function

    ''' <summary>
    ''' Read the data of a certain SubscriberBinnacleFormField in the table prgSubscriberBinnacleFormFields and return
    ''' a list of DAClsSubscriberBinnacleFormFields.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsSubscriberBinnacleFormFields.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pID As Parameter(Of Long)) As Struct()
        Fetch = DoFetch(CreateSql("ID = " & pID.Value.ToString))
    End Function

    ''' <summary>
    ''' Read the data of a certain SubscriberBinnacleFormField in the table prgSubscriberBinnacleFormFields and return
    ''' a list of DAClsSubscriberBinnacleFormFields.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsSubscriberBinnacleFormFields.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDBinnacleForm As Parameter(Of Long), ByVal pIDField As Parameter(Of Long)) As Struct()
        Fetch = DoFetch(CreateSql(GetParametersLists(pIDBinnacleForm, pIDField)))
    End Function

    ''' <summary>
    ''' Read the data of a certain SubscriberBinnacleFormField in the table prgSubscriberBinnacleFormFields and return
    ''' a list of DAClsSubscriberBinnacleFormFields.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsSubscriberBinnacleFormFields.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDBinnacleForm As ParameterList(Of Long), ByVal pIDField As ParameterList(Of Long)) As Struct()
        Fetch = DoFetch(CreateSql(GetParametersLists(pIDBinnacleForm, pIDField)))
    End Function

    ''' <summary>
    ''' Read the data of a certain SubscriberBinnacleFormField in the table prgSubscriberBinnacleFormFields and return
    ''' a list of DAClsSubscriberBinnacleFormFields.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsSubscriberBinnacleFormFields.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function FetchByBinnacleForm(ByVal pIDBinnacleForm As Parameter(Of Long)) As Struct()
        FetchByBinnacleForm = DoFetch(CreateSql("IDBinnacleForm = " & pIDBinnacleForm.Value.ToString))
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
    Private Shared Function CreateSql(ByVal ParamArray pParametersList() As String) As String

        'Build SQL value
        Dim sql As String = ""
        sql &= " Select"
        sql &= "  ID,"
        sql &= "  IDBinnacleForm,"
        sql &= "  IDField,"
        sql &= "  OldValue,"
        sql &= "  NewValue"
        sql &= " From"
        sql &= "  prgSubscriberBinnacleFormFields"

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
        sql &= "  IDBinnacleForm,"
        sql &= "  IDField,"
        sql &= "  OldValue,"
        sql &= "  NewValue"
        sql &= " From"
        sql &= "  prgSubscriberBinnacleFormFields"

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
    ''' Execute SQL statement to reads the data of the table prgSubscriberBinnacleFormFields and
    ''' return a list of DAClsSubscriberBinnacleFormFields.Struct with each one of the read registries 
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
                    Struct.IDBinnacleForm.SetValue(dr.GetInt64("IDBinnacleForm"))
                    Struct.IDField.SetValue(dr.GetInt64("IDField"))
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

        pStruct.IDBinnacleForm.OldValue = pStruct.IDBinnacleForm.Value
        pStruct.IDBinnacleForm.Value = pStruct.IDBinnacleForm.NewValue

        pStruct.IDField.OldValue = pStruct.IDField.Value
        pStruct.IDField.Value = pStruct.IDField.NewValue

        pStruct.OldValue.OldValue = pStruct.OldValue.Value
        pStruct.OldValue.Value = pStruct.OldValue.NewValue

        pStruct.NewValue.OldValue = pStruct.NewValue.Value
        pStruct.NewValue.Value = pStruct.NewValue.NewValue

        Return pStruct
    End Function

    ''' <summary>
    ''' Insert a new registry in the table prgSubscriberBinnacleFormFields and return ID
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the SubscriberBinnacleFormField to insert</param>
    ''' <returns>ID</returns>
    ''' <remarks></remarks>
    Public Shared Function Insert(ByVal pStruct As Struct) As Struct

        'Build SQL value for insert
        Dim sql As String = ""
        sql &= " Insert Into prgSubscriberBinnacleFormFields"
        sql &= "  ("
        sql &= "    IDBinnacleForm,"
        sql &= "    IDField,"
        sql &= "    OldValue,"
        sql &= "    NewValue"
        sql &= "  )"
        sql &= " Values"
        sql &= "  ("
        sql &= "     " & pStruct.IDBinnacleForm.NewValue.ToString & ","
        sql &= "     " & pStruct.IDField.NewValue.ToString & ","
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
    ''' Update a registry in the table prgSubscriberBinnacleFormFields and return the number of rows affected
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the SubscriberBinnacleFormField to update</param>
    ''' <returns>Number of rows affected</returns>
    ''' <remarks></remarks>
    Public Shared Function Update(ByVal pStruct As Struct) As Struct
        'Build SQL value
        Dim sql As String = ""
        sql &= " UPDATE prgSubscriberBinnacleFormFields"
        sql &= " SET"
        sql &= "  IDBinnacleForm = " & pStruct.IDBinnacleForm.NewValue.ToString & ","
        sql &= "  IDField = " & pStruct.IDField.NewValue.ToString & ","
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
    ''' Delete a registry in the table prgSubscriberBinnacleFormFields and return the number of rows affected
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub Delete(ByVal pID As Long)

        'Build SQL value
        Dim sql As String = ""
        sql &= " Delete From prgSubscriberBinnacleFormFields"
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
