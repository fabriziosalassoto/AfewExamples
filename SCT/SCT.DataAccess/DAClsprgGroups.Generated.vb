Imports Csla.data
Imports DBConnection
Imports System.Data.OleDb

''' <summary>
''' Represents a controller of data for the prgGroups table in the data base PCS 
''' </summary>
''' <remarks></remarks>
<Serializable()> Public Class DAClsprgGroups

    ''' <summary>
    ''' Represents one structures with the data of a registry of the table prgGroups 
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> Public Class Struct

        Public TableName As String = "prgGroups"

        Public ID As StructField(Of Long) = New StructField(Of Long)("ID", True)
        Public IDForm As StructField(Of Long) = New StructField(Of Long)("IDForm", False)
        Public Description As StructField(Of String) = New StructField(Of String)("Description", False, String.Empty)

    End Class

    ''' <summary>
    ''' Reads the data of all the Groups in the table prgGroups and return
    ''' a list of DAClsGroups.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsGroups.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch() As Struct()
        Fetch = DoFetch(CreateSql(New String() {}))
    End Function

    ''' <summary>
    ''' Read the data of a certain Group in the table prgGroups and return
    ''' a list of DAClsGroups.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pID">Id of Group to fetch</param>
    ''' <returns>List of DAClsGroups.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pID As Long) As Struct()
        Fetch = DoFetch(CreateSql("ID = " & pID.ToString))
    End Function

    ''' <summary>
    ''' Read the data of a certain Group in the table prgGroups and return
    ''' a list of DAClsGroups.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pDescription"></param>
    ''' <returns>List of DAClsGroups.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pDescription As String) As Struct()
        Fetch = DoFetch(CreateSql("Description = '" & pDescription & "'"))
    End Function

    ''' <summary>
    ''' Read the data of a certain Group in the table prgGroups and return
    ''' a list of DAClsGroups.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pIDForm">Id of form to fetch</param>
    ''' <param name="pDescription"></param>
    ''' <returns>List of DAClsGroups.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDForm As Long, ByVal pDescription As String) As Struct()
        Fetch = DoFetch(CreateSql(GetParamsList(pIDForm, pDescription)))
    End Function

    ''' <summary>
    ''' Read the data of a certain Group in the table prgGroups and return
    ''' a list of DAClsGroups.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pIDForm">Id of form to fetch</param>
    ''' <param name="pDescription"></param>
    ''' <returns>List of DAClsGroups.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDForm() As Long, ByVal pDescription As String) As Struct()
        Fetch = DoFetch(CreateSql(GetParamsList(pIDForm, pDescription)))
    End Function

    ''' <summary>
    ''' Read the data of a certain Group in the table prgGroups and return
    ''' a list of DAClsGroups.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pIDForm">Id of Form to fetch</param>
    ''' <returns>List of DAClsGroups.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function FetchByForm(ByVal pIDForm As Long) As Struct()
        FetchByForm = DoFetch(CreateSql("IDForm = " & pIDForm.ToString))
    End Function

    ''' <summary>
    ''' Read the data of a certain Group in the table prgGroups and return
    ''' a list of DAClsGroups.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pIDForm">Id of Form to fetch</param>
    ''' <param name="pDescription"></param>
    ''' <returns>List of DAClsGroups.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function FetchByForm(ByVal pIDForm As Long, ByVal pDescription As String) As Struct()
        FetchByForm = DoFetch(CreateSql("IDForm = " & pIDForm.ToString, "Description = '" & pDescription & "'"))
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pIDForm"></param>
    ''' <param name="pDescription "></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParamsList(ByVal pIDForm As Long, ByVal pDescription As String) As String()
        Dim params As New List(Of String)

        If pIDForm <> 0 Then
            params.Add("IDForm = " & pIDForm.ToString)
        End If

        If Len(pDescription) > 0 Then
            params.Add("Description Like '%" & pDescription & "%'")
        End If

        Return params.ToArray
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pIDForm"></param>
    ''' <param name="pDescription "></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParamsList(ByVal pIDForm() As Long, ByVal pDescription As String) As String()()
        Dim paramsList As New List(Of String())

        If pIDForm.Length > 0 Then
            Dim paramList As New List(Of String)
            For Each idForm As Long In pIDForm
                If idForm <> 0 Then
                    paramList.Add("IDForm = " & idForm.ToString)
                End If
            Next
            paramsList.Add(paramList.ToArray)
        End If

        If Len(pDescription) > 0 Then
            paramsList.Add(New String() {"Description Like '%" & pDescription & "%'"})
        End If

        Return paramsList.ToArray
    End Function

    ''' <summary>
    ''' Creates the Sql Statement and returns it
    ''' </summary>
    ''' <param name="pParams"></param>
    ''' <returns>Sql Statement</returns>
    ''' <remarks></remarks>
    Private Shared Function CreateSql(ByVal ParamArray pParams() As String) As String

        'Build SQL value
        Dim sql As String = ""
        sql &= " Select"
        sql &= "  ID,"
        sql &= "  IDForm,"
        sql &= "  Description"
        sql &= " From"
        sql &= "  prgGroups"

        If pParams.Length > 0 Then
            sql &= " Where " & Join(pParams, " And ")
        End If

        CreateSql = sql
    End Function

    ''' <summary>
    ''' Creates the Sql Statement and returns it
    ''' </summary>
    ''' <param name="pParams"></param>
    ''' <returns>Sql Statement</returns>
    ''' <remarks></remarks>
    Private Shared Function CreateSql(ByVal ParamArray pParams()() As String) As String

        'Build SQL value
        Dim sql As String = ""
        sql &= " Select"
        sql &= "  ID,"
        sql &= "  IDForm,"
        sql &= "  Description"
        sql &= " From"
        sql &= "  prgGroups"

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
    ''' Execute SQL statement to reads the data of the table prgGroups and
    ''' return a list of DAClsGroups.Struct with each one of the read registries 
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
                    Struct.IDForm.SetValue(dr.GetInt64("IDForm"))
                    Struct.Description.SetValue(dr.GetString("Description"))
                    List.Add(Struct)
                End While
                DoFetch = List.ToArray
            End Using
        End Using
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pStruct"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function RefreshStruct(ByVal pStruct As Struct) As Struct
        pStruct.ID.OldValue = pStruct.ID.Value
        pStruct.ID.Value = pStruct.ID.NewValue

        pStruct.IDForm.OldValue = pStruct.IDForm.Value
        pStruct.IDForm.Value = pStruct.IDForm.NewValue

        pStruct.Description.OldValue = pStruct.Description.Value
        pStruct.Description.Value = pStruct.Description.NewValue

        Return pStruct
    End Function

    ''' <summary>
    ''' Insert a new registry in the table prgGroups and return ID
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the Group to insert</param>
    ''' <returns>ID</returns>
    ''' <remarks></remarks>
    Public Shared Function Insert(ByVal pStruct As Struct) As Struct

        'Build SQL value for insert
        Dim sql As String = ""
        sql &= " Insert Into prgGroups"
        sql &= "  ("
        sql &= "    IDForm,"
        sql &= "    Description"
        sql &= "  )"
        sql &= " Values"
        sql &= "  ("
        sql &= "    " & pStruct.IDForm.NewValue.ToString & ","
        sql &= "    '" & pStruct.Description.NewValue & "'"
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
    ''' Update a registry in the table prgGroups and return the number of rows affected
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the Group to update</param>
    ''' <returns>Number of rows affected</returns>
    ''' <remarks></remarks>
    Public Shared Function Update(ByVal pStruct As Struct) As Struct

        'Build SQL value
        Dim sql As String = ""
        sql &= " UPDATE prgGroups"
        sql &= " SET"
        sql &= "  IDForm = " & pStruct.IDForm.NewValue.ToString & ","
        sql &= "  Description = '" & pStruct.Description.NewValue & "'"
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
    ''' Delete a registry in the table prgGroups and return the number of rows affected
    ''' </summary>
    ''' <param name="ID">Id of Group to delete</param>
    ''' <remarks></remarks>
    Public Shared Sub Delete(ByVal ID As Long)

        'Build SQL value
        Dim sql As String = ""
        sql &= " Delete From prgGroups"
        sql &= " Where"
        sql &= "  ID = " & ID.ToString

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
