Imports Csla.data
Imports DBConnection
Imports System.Data.OleDb

''' <summary>
''' Represents a controller of data for the prgAdministratorUsers table in the data base PCS 
''' </summary>
''' <remarks></remarks>
<Serializable()> Public Class DAClsprgAdministrativeUsers

    ''' <summary>
    ''' Represents one structures with the data of a registry of the table prgAdministratorUsers 
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> Public Class Struct
        Public TableName As String = "prgAdministrativeUsers"

        Public ID As StructField(Of Long) = New StructField(Of Long)("ID", True)
        Public IDProfile As StructField(Of Long) = New StructField(Of Long)("IDProfile", False)
        Public FirstName As StructField(Of String) = New StructField(Of String)("FirstName", False, String.Empty)
        Public LastName As StructField(Of String) = New StructField(Of String)("LastName", False, String.Empty)
        Public Login As StructField(Of String) = New StructField(Of String)("Login", False, String.Empty)
        Public Password As StructField(Of String) = New StructField(Of String)("Password", False, String.Empty)
    End Class

    ''' <summary>
    ''' Reads the data of all the AdministratorUsers in the table prgAdministratorUsers and return
    ''' a list of DAClsAdministratorUsers.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsAdministratorUsers.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch() As Struct()
        Fetch = DoFetch(CreateSql(New String() {}))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdministratorUser in the table prgAdministratorUsers and return
    ''' a list of DAClsAdministratorUsers.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pID">ID of AdministratorUser to fetch</param>
    ''' <returns>List of DAClsAdministratorUsers.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pID As Long) As Struct()
        Fetch = DoFetch(CreateSql("ID = " & pID.ToString))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdministratorUser in the table prgAdministratorUsers and return
    ''' a list of DAClsAdministratorUsers.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pLogin">Login of AdministratorUser to fetch</param>
    ''' <returns>List of DAClsAdministratorUsers.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLogin As String) As Struct()
        Fetch = DoFetch(CreateSql("Login = '" & pLogin & "'"))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdministratorUser in the table prgAdministratorUsers and return
    ''' a list of DAClsAdministratorUsers.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pLogin">Login of AdministratorUser to fetch</param>
    ''' <param name="pPassword"></param>
    ''' <returns>List of DAClsAdministratorUsers.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLogin As String, ByVal pPassword As String) As Struct()
        Fetch = DoFetch(CreateSql("Login = '" & pLogin & "'", "Password = '" & pPassword & "'"))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdministratorUser in the table prgAdministratorUsers and return
    ''' a list of DAClsAdministratorUsers.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pIDProfile">Id Profile of AdministratorUser to fetch</param>
    ''' <returns>List of DAClsAdministratorUsers.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDProfile As Long, ByVal pFullName As String) As Struct()
        Fetch = DoFetch(CreateSql(GetParamsList(pIDProfile, pFullName)))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdministratorUser in the table prgAdministratorUsers and return
    ''' a list of DAClsAdministratorUsers.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pIDProfile">Id Profile of AdministratorUser to fetch</param>
    ''' <returns>List of DAClsAdministratorUsers.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDProfile() As Long, ByVal pFullName As String) As Struct()
        Fetch = DoFetch(CreateSql(GetParamsList(pIDProfile, pFullName)))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdministratorUser in the table prgAdministratorUsers and return
    ''' a list of DAClsAdministratorUsers.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pIDProfile">Id Profile of AdministratorUser to fetch</param>
    ''' <returns>List of DAClsAdministratorUsers.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function FetchByProfile(ByVal pIDProfile As Long) As Struct()
        FetchByProfile = DoFetch(CreateSql("IDProfile = " & pIDProfile.ToString))
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pIDProfile"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParamsList(ByVal pIDProfile As Long, ByVal pFullName As String) As String()
        Dim params As New List(Of String)

        If pIDProfile <> 0 Then
            params.Add("IDProfile = " & pIDProfile.ToString)
        End If

        If Len(pFullName) > 0 Then
            params.Add("LastName + ', ' + FirstName Like '%" & pFullName & "%'")
        End If

        Return params.ToArray
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pIDProfiles"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParamsList(ByVal pIDProfiles() As Long, ByVal pFullName As String) As String()()
        Dim paramsList As New List(Of String())

        If pIDProfiles.Length > 0 Then
            Dim paramList As New List(Of String)
            For Each idProfile As Long In pIDProfiles
                If idProfile <> 0 Then
                    paramList.Add("IDProfile = " & idProfile.ToString)
                End If
            Next
            paramsList.Add(paramList.ToArray)
        End If

        If Len(pFullName) > 0 Then
            paramsList.Add(New String() {"LastName + ', ' + FirstName Like '%" & pFullName & "%'"})
        End If

        Return paramsList.ToArray
    End Function

    ''' <summary>
    ''' Creates the Sql Statement and returns it
    ''' </summary>
    ''' <returns>Sql Statement</returns>
    ''' <remarks></remarks>
    Private Shared Function CreateSql(ByVal ParamArray pParams() As String) As String

        'Build SQL value
        Dim sql As String = ""
        sql &= " Select"
        sql &= "  ID,"
        sql &= "  IDProfile,"
        sql &= "  FirstName,"
        sql &= "  LastName,"
        sql &= "  Login,"
        sql &= "  Password"
        sql &= " From"
        sql &= "  prgAdministrativeUsers"

        If pParams.Length > 0 Then
            sql &= " Where " & Join(pParams, " And ")
        End If

        CreateSql = sql
    End Function

    ''' <summary>
    ''' Creates the Sql Statement and returns it
    ''' </summary>
    ''' <returns>Sql Statement</returns>
    ''' <remarks></remarks>
    Private Shared Function CreateSql(ByVal ParamArray pParams()() As String) As String

        'Build SQL value
        Dim sql As String = ""
        sql &= " Select"
        sql &= "  ID,"
        sql &= "  IDProfile,"
        sql &= "  FirstName,"
        sql &= "  LastName,"
        sql &= "  Login,"
        sql &= "  Password"
        sql &= " From"
        sql &= "  prgAdministrativeUsers"

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
    ''' Execute SQL statement to reads the data of the table prgAdministratorUsers and
    ''' return a list of DAClsAdministratorUsers.Struct with each one of the read registries 
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
                    Struct.IDProfile.SetValue(dr.GetInt64("IDProfile"))
                    Struct.FirstName.SetValue(dr.GetString("FirstName"))
                    Struct.LastName.SetValue(dr.GetString("LastName"))
                    Struct.Login.SetValue(dr.GetString("Login"))
                    Struct.Password.SetValue(dr.GetString("Password"))
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

        pStruct.IDProfile.OldValue = pStruct.IDProfile.Value
        pStruct.IDProfile.Value = pStruct.IDProfile.NewValue

        pStruct.FirstName.OldValue = pStruct.FirstName.Value
        pStruct.FirstName.Value = pStruct.FirstName.NewValue

        pStruct.LastName.OldValue = pStruct.LastName.Value
        pStruct.LastName.Value = pStruct.LastName.NewValue

        pStruct.Login.OldValue = pStruct.Login.Value
        pStruct.Login.Value = pStruct.Login.NewValue

        pStruct.Password.OldValue = pStruct.Password.Value
        pStruct.Password.Value = pStruct.Password.NewValue

        Return pStruct
    End Function

    ''' <summary>
    ''' Insert a new registry in the table prgAdministratorUsers and return ID
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the AdministratorUser to insert</param>
    ''' <returns>ID</returns>
    ''' <remarks></remarks>
    Public Shared Function Insert(ByVal pStruct As Struct) As Struct

        'Build SQL value for insert
        Dim sql As String = ""
        sql &= " Insert Into prgAdministrativeUsers"
        sql &= "  ("
        sql &= "    IDProfile,"
        sql &= "    FirstName,"
        sql &= "    LastName,"
        sql &= "    Login,"
        sql &= "    Password"
        sql &= "  )"
        sql &= " Values"
        sql &= "  ("
        sql &= "     " & pStruct.IDProfile.NewValue.ToString & ","
        sql &= "    '" & pStruct.FirstName.NewValue & "',"
        sql &= "    '" & pStruct.LastName.NewValue & "',"
        sql &= "    '" & pStruct.Login.NewValue & "',"
        sql &= "    '" & pStruct.Password.NewValue & "'"
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
    ''' Update a registry in the table prgAdministratorUsers and return the number of rows affected
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the AdministratorUser to update</param>
    ''' <returns>Number of rows affected</returns>
    ''' <remarks></remarks>
    Public Shared Function Update(ByVal pStruct As Struct) As Struct

        'Build SQL value
        Dim sql As String = ""
        sql &= " UPDATE prgAdministrativeUsers"
        sql &= " SET"
        sql &= "  IDProfile = " & pStruct.IDProfile.NewValue.ToString & ","
        sql &= "  FirstName = '" & pStruct.FirstName.NewValue & "',"
        sql &= "  LastName = '" & pStruct.LastName.NewValue & "',"
        sql &= "  Login = '" & pStruct.Login.NewValue & "',"
        sql &= "  Password = '" & pStruct.Password.NewValue & "'"
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
    ''' Delete a registry in the table prgAdministratorUsers and return the number of rows affected
    ''' </summary>
    ''' <param name="pID">ID of AdministratorUser to delete</param>
    ''' <remarks></remarks>
    Public Shared Sub Delete(ByVal pID As Long)

        'Build SQL value
        Dim sql As String = ""
        sql &= " Delete From prgAdministrativeUsers"
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
