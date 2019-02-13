Imports Csla.data
Imports DBConnection
Imports System.Data.OleDb

''' <summary>
''' Represents a controller of data for the prgAdministratorFormPermissions table in the data base PCS 
''' </summary>
''' <remarks></remarks>
<Serializable()> Public Class DAClsprgAdministrativeFormPermissions

    ''' <summary>
    ''' Represents one structures with the data of a registry of the table prgAdministratorFormPermissions 
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> Public Class Struct
        Public TableName As String = "prgAdministratorFormPermissions"

        Public IDProfile As StructField(Of Long) = New StructField(Of Long)("IDProfile", True)
        Public IDForm As StructField(Of Long) = New StructField(Of Long)("IDForm", True)
        Public pSelect As StructField(Of Boolean) = New StructField(Of Boolean)("pSelect", False)
        Public pInsert As StructField(Of Boolean) = New StructField(Of Boolean)("pInsert", False)
        Public pUpdate As StructField(Of Boolean) = New StructField(Of Boolean)("pUpdate", False)
        Public pDelete As StructField(Of Boolean) = New StructField(Of Boolean)("pDelete", False)
    End Class

    ''' <summary>
    ''' Reads the data of all the AdministratorFormPermissions in the table prgAdministratorFormPermissions and return
    ''' a list of DAClsAdministratorFormPermissions.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsAdministratorFormPermissions.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch() As Struct()
        Fetch = DoFetch(CreateSelectSql(New String() {}))
    End Function

    ''' <summary>
    ''' Read the data of a AdministratorFormPermission in the table prgAdministratorFormPermissions and return
    ''' a list of DAClsAdministratorFormPermissions.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name=" pIDProfile ">IDProfile to use</param>
    ''' <param name=" pIDForm ">IDForm to use</param>
    ''' <returns>List of DAClsAdministratorFormPermissions.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDProfile As Long, ByVal pIDForm As Long) As Struct()
        Fetch = DoFetch(CreateSelectSql(GetParamsList(pIDProfile, pIDForm)))
    End Function

    ''' <summary>
    ''' Read the data of a AdministratorFormPermission in the table prgAdministratorFormPermissions and return
    ''' a list of DAClsAdministratorFormPermissions.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name=" pIDProfiles">IDProfile to use</param>
    ''' <param name=" pIDForms">IDForm to use</param>
    ''' <returns>List of DAClsAdministratorFormPermissions.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDProfiles() As Long, ByVal pIDForms() As Long) As Struct()
        Fetch = DoFetch(CreateSelectSql(GetParamsList(pIDProfiles, pIDForms)))
    End Function

    ''' <summary>
    ''' Read the data of the AdministratorFormPermission in the table prgAdministratorFormPermissions and return
    ''' a list of DAClsAdministratorFormPermissions.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name=" pIDProfile ">IDProfile to use</param>
    ''' <returns>List of DAClsAdministratorFormPermissions.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function FetchByProfile(ByVal pIDProfile As Long) As Struct()
        FetchByProfile = DoFetch(CreateSelectSql("IDProfile = " & pIDProfile.ToString))
    End Function

    ''' <summary>
    ''' Read the data of the AdministratorFormPermission in the table prgAdministratorFormPermissions and return
    ''' a list of DAClsAdministratorFormPermissions.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name=" pIDForm ">IDForm to use</param>
    ''' <returns>List of DAClsAdministratorFormPermissions.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function FetchByForm(ByVal pIDForm As Long) As Struct()
        FetchByForm = DoFetch(CreateSelectSql("IDForm = " & pIDForm.ToString))
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pIDProfile"></param>
    ''' <param name="pIDForm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParamsList(ByVal pIDProfile As Long, ByVal pIDForm As Long) As String()
        Dim params As New List(Of String)

        If pIDProfile <> 0 Then
            params.Add("IDProfile = " & pIDProfile.ToString)
        End If

        If pIDForm <> 0 Then
            params.Add("IDForm = " & pIDForm.ToString)
        End If

        Return params.ToArray
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pIDProfiles"></param>
    ''' <param name="pIDForms"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParamsList(ByVal pIDProfiles() As Long, ByVal pIDForms() As Long) As String()()
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

        If pIDForms.Length > 0 Then
            Dim paramList As New List(Of String)
            For Each idForm As Long In pIDForms
                If idForm <> 0 Then
                    paramList.Add("IDForm = " & idForm.ToString)
                End If
            Next
            paramsList.Add(paramList.ToArray)
        End If

        Return paramsList.ToArray
    End Function

    ''' <summary>
    ''' Creates the Sql Statement and returns it
    ''' </summary>
    ''' <param name="pParams"></param>
    ''' <returns>Sql Statement</returns>
    ''' <remarks></remarks>
    Private Shared Function CreateSelectSql(ByVal ParamArray pParams() As String) As String

        'Build SQL value
        Dim sql As String = ""
        sql &= " Select"
        sql &= "  IDProfile,"
        sql &= "  IDForm,"
        sql &= "  pSelect,"
        sql &= "  pInsert,"
        sql &= "  pUpdate,"
        sql &= "  pDelete"
        sql &= " From"
        sql &= "  prgAdministrativeFormPermissions"

        If pParams.Length > 0 Then
            sql &= " Where " & Join(pParams, " And ")
        End If

        CreateSelectSql = sql
    End Function

    ''' <summary>
    ''' Creates the Sql Statement and returns it
    ''' </summary>
    ''' <returns>Sql Statement</returns>
    ''' <remarks></remarks>
    Private Shared Function CreateSelectSql(ByVal ParamArray pParams()() As String) As String

        'Build SQL value
        Dim sql As String = ""
        sql &= " Select"
        sql &= "  IDProfile,"
        sql &= "  IDForm,"
        sql &= "  pSelect,"
        sql &= "  pInsert,"
        sql &= "  pUpdate,"
        sql &= "  pDelete"
        sql &= " From"
        sql &= "  prgAdministrativeFormPermissions"

        Dim list As New List(Of String)
        For Each item As String() In pParams
            If item.Length > 0 Then
                list.Add("(" & Join(item, " Or ") & ")")
            End If
        Next
        If list.Count > 0 Then
            sql &= " Where " & Join(list.ToArray, " And ")
        End If

        CreateSelectSql = sql
    End Function

    ''' <summary>
    ''' Execute SQL statement to reads the data of the table prgAdministratorFormPermissions and
    ''' return a list of DAClsAdministratorFormPermissions.Struct with each one of the read registries 
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
                    Struct.IDProfile.SetValue(dr.GetInt64("IDProfile"))
                    Struct.IDForm.SetValue(dr.GetInt64("IDForm"))
                    Struct.pSelect.SetValue(dr.GetBoolean("pSelect"))
                    Struct.pInsert.SetValue(dr.GetBoolean("pInsert"))
                    Struct.pUpdate.SetValue(dr.GetBoolean("pUpdate"))
                    Struct.pDelete.SetValue(dr.GetBoolean("pDelete"))
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
        pStruct.IDForm.OldValue = pStruct.IDForm.Value
        pStruct.IDForm.Value = pStruct.IDForm.NewValue

        pStruct.IDProfile.OldValue = pStruct.IDProfile.Value
        pStruct.IDProfile.Value = pStruct.IDProfile.NewValue

        pStruct.pSelect.OldValue = pStruct.pSelect.Value
        pStruct.pSelect.Value = pStruct.pSelect.NewValue

        pStruct.pInsert.OldValue = pStruct.pInsert.Value
        pStruct.pInsert.Value = pStruct.pInsert.NewValue

        pStruct.pUpdate.OldValue = pStruct.pUpdate.Value
        pStruct.pUpdate.Value = pStruct.pUpdate.NewValue

        pStruct.pDelete.OldValue = pStruct.pDelete.Value
        pStruct.pDelete.Value = pStruct.pDelete.NewValue

        Return pStruct
    End Function

    ''' <summary>
    ''' Insert a new registry in the table prgAdministratorFormPermissions and return the number of rows affected
    ''' </summary>
    ''' <param name=" pStruct "></param>
    ''' <returns>Number of rows affected</returns>
    ''' <remarks></remarks>
    Public Shared Function Insert(ByVal pStruct As Struct) As Struct

        'Build SQL value for insert
        Dim sql As String = ""
        sql &= " Insert Into prgAdministrativeFormPermissions"
        sql &= "  ("
        sql &= "    IDProfile,"
        sql &= "    IDForm,"
        sql &= "    pSelect,"
        sql &= "    pInsert,"
        sql &= "    pUpdate,"
        sql &= "    pDelete"
        sql &= "  )"
        sql &= " Values"
        sql &= "  ("
        sql &= "    " & pStruct.IDProfile.NewValue.ToString & ","
        sql &= "    " & pStruct.IDForm.NewValue.ToString & ","
        sql &= "    " & CByte(pStruct.pSelect.NewValue).ToString & ","
        sql &= "    " & CByte(pStruct.pInsert.NewValue).ToString & ","
        sql &= "    " & CByte(pStruct.pUpdate.NewValue).ToString & ","
        sql &= "    " & CByte(pStruct.pDelete.NewValue).ToString
        sql &= "  )"

        Try
            'Connection to the data base 
            Using DBConn As DBClsConnection = New DBClsConnection(DADataBase.SCTConnectionString)

                'Execute Query
                DBConn.ExecuteNonQuery(sql)
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
    ''' Update a registry in the table prgFields and return the number of rows affected
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the Field to update</param>
    ''' <returns>Number of rows affected</returns>
    ''' <remarks></remarks>
    Public Shared Function Update(ByVal pStruct As Struct) As Struct

        'Build SQL value
        Dim sql As String = ""
        sql &= " UPDATE prgAdministrativeFormPermissions"
        sql &= " SET"
        sql &= "  pSelect = " & CByte(pStruct.pSelect.NewValue).ToString & ","
        sql &= "  pInsert = " & CByte(pStruct.pInsert.NewValue).ToString & ","
        sql &= "  pUpdate = " & CByte(pStruct.pUpdate.NewValue).ToString & ","
        sql &= "  pDelete = " & CByte(pStruct.pDelete.NewValue).ToString
        sql &= " Where IDProfile = " & pStruct.IDProfile.NewValue.ToString
        sql &= "   And IDForm = " & pStruct.IDForm.NewValue.ToString

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
    ''' Delete a registry in the table prgAdministratorFormPermissions and return the number of rows affected
    ''' </summary>
    ''' <param name=" pIDProfile ">IDProfile to use</param>
    ''' <param name=" pIDForm ">IDForm to use</param>
    ''' <remarks></remarks>
    Public Shared Sub Delete(ByVal pIDProfile As Long, ByVal pIDForm As Long)
        DoDelete(CreateDeleteSql("IDProfile = " & pIDProfile.ToString, "IDForm = " & pIDForm.ToString))
    End Sub

    ''' <summary>
    ''' Delete a registry in the table prgAdministratorFormPermissions and return the number of rows affected
    ''' </summary>
    ''' <param name=" pIDProfile ">IDProfile to use to delete</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteByProfile(ByVal pIDProfile As Long)
        DoDelete(CreateDeleteSql("IDProfile = " & pIDProfile.ToString))
    End Sub

    ''' <summary>
    ''' Delete a registry in the table prgAdministratorFormPermissions and return the number of rows affected
    ''' </summary>
    ''' <param name=" pIDForm ">IDForm to use to delete</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteByForm(ByVal pIDForm As Long)
        DoDelete(CreateDeleteSql("IDForm = " & pIDForm.ToString))
    End Sub

    ''' <summary>
    ''' Creates the Sql Statement and returns it
    ''' </summary>
    ''' <param name="pParams"></param>
    ''' <returns>Sql Statement</returns>
    ''' <remarks></remarks>
    Private Shared Function CreateDeleteSql(ByVal ParamArray pParams() As String) As String

        'Build SQL value
        Dim sql As String = ""
        sql &= " Delete From"
        sql &= "  prgAdministrativeFormPermissions"

        If pParams.Length > 0 Then
            sql &= " Where " & Join(pParams, " And ")
        End If

        CreateDeleteSql = sql
    End Function

    ''' <summary>
    ''' Execute SQL statement to delete the data of the table tblTasksResources and return number of rows affected
    ''' </summary>
    ''' <param name="psql">Sql Statement to execute</param>
    ''' <remarks></remarks>
    Private Shared Sub DoDelete(ByVal psql As String)
        Try
            'Connection to the data base 
            Using DBConn As DBClsConnection = New DBClsConnection(DADataBase.SCTConnectionString)

                'Execute Transact-SQL statement
                DBConn.ExecuteNonQuery(psql)
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
