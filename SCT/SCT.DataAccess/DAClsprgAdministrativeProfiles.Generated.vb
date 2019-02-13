Imports Csla.data
Imports DBConnection
Imports System.Data.OleDb

''' <summary>
''' Represents a controller of data for the prgAdministratorProfiles table in the data base PCS 
''' </summary>
''' <remarks></remarks>
<Serializable()> Public Class DAClsprgAdministrativeProfiles

    ''' <summary>
    ''' Represents one structures with the data of a registry of the table prgAdministratorProfiles 
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> Public Class Struct
        Public TableName As String = "prgAdministratorProfiles"

        Public ID As StructField(Of Long) = New StructField(Of Long)("ID", True)
        Public Description As StructField(Of String) = New StructField(Of String)("Description", False, String.Empty)
    End Class

    ''' <summary>
    ''' Reads the data of all the AdministratorProfiles in the table prgAdministratorProfiles and return
    ''' a list of DAClsAdministratorProfiles.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsAdministratorProfiles.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch() As Struct()
        Fetch = DoFetch(CreateSql(New String() {}))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdministratorProfile in the table prgAdministratorProfiles and return
    ''' a list of DAClsAdministratorProfiles.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pID">ID of AdministratorProfile to fetch</param>
    ''' <returns>List of DAClsAdministratorProfiles.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pID As Long) As Struct()
        Fetch = DoFetch(CreateSql("ID = " & pID.ToString))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdministratorProfile in the table prgAdministratorProfiles and return
    ''' a list of DAClsAdministratorProfiles.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pDescription">Description of AdministratorProfile to fetch</param>
    ''' <returns>List of DAClsAdministratorProfiles.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pDescription As String) As Struct()
        Fetch = DoFetch(CreateSql(GetParamsList(pDescription)))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdministratorProfile in the table prgAdministratorProfiles and return
    ''' a list of DAClsAdministratorProfiles.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pDescription">Description of AdministratorProfile to fetch</param>
    ''' <returns>List of DAClsAdministratorProfiles.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function FetchByDescription(ByVal pDescription As String) As Struct()
        FetchByDescription = DoFetch(CreateSql("Description = '" & pDescription & "'"))
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pDescription "></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParamsList(ByVal pDescription As String) As String()
        Dim params As New List(Of String)

        If Len(pDescription) > 0 Then
            params.Add("Description Like '%" & pDescription & "%'")
        End If

        Return params.ToArray
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
        sql &= "  Description"
        sql &= " From"
        sql &= "  prgAdministrativeProfiles"

        If pParams.Length > 0 Then
            sql &= " Where " & Join(pParams, " And ")
        End If

        CreateSql = sql
    End Function

    ''' <summary>
    ''' Execute SQL statement to reads the data of the table prgAdministratorProfiles and
    ''' return a list of DAClsAdministratorProfiles.Struct with each one of the read registries 
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

        pStruct.Description.OldValue = pStruct.Description.Value
        pStruct.Description.Value = pStruct.Description.NewValue

        Return pStruct
    End Function

    ''' <summary>
    ''' Insert a new registry in the table prgAdministratorProfiles and return ID
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the AdministratorProfile to insert</param>
    ''' <returns>ID</returns>
    ''' <remarks></remarks>
    Public Shared Function Insert(ByVal pStruct As Struct) As Struct

        'Build SQL value for insert
        Dim sql As String = ""
        sql &= " Insert Into prgAdministrativeProfiles"
        sql &= "  ("
        sql &= "    Description"
        sql &= "  )"
        sql &= " Values"
        sql &= "  ("
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
    ''' Update a registry in the table prgAdministratorProfiles and return the number of rows affected
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the AdministratorProfile to update</param>
    ''' <returns>Number of rows affected</returns>
    ''' <remarks></remarks>
    Public Shared Function Update(ByVal pStruct As Struct) As Struct

        'Build SQL value
        Dim sql As String = ""
        sql &= " UPDATE prgAdministrativeProfiles"
        sql &= " SET"
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
    ''' Delete a registry in the table prgAdministratorProfiles and return the number of rows affected
    ''' </summary>
    ''' <param name="pID">ID of AdministratorProfile to delete</param>
    ''' <remarks></remarks>
    Public Shared Sub Delete(ByVal pID As Long)

        'Build SQL value
        Dim sql As String = ""
        sql &= " Delete From prgAdministrativeProfiles"
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
