Imports Csla.data
Imports DBConnection
Imports System.Data.OleDb

''' <summary>
''' Represents a controller of data for the prgAdminBinnacle table in the data base PCS 
''' </summary>
''' <remarks></remarks>
<Serializable()> Public Class DAClsprgAdministrativeBinnacle

    ''' <summary>
    ''' Represents one structures with the data of a registry of the table prgAdminBinnacle 
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> Public Class Struct

        Public TableName As String = "prgAdministrativeBinnacle"

        Public ID As StructField(Of Long) = New StructField(Of Long)("ID", True)
        Public IDUser As StructField(Of Long) = New StructField(Of Long)("IDUser", False)
        Public BDate As StructField(Of Date) = New StructField(Of Date)("BDate", False)

    End Class

    ''' <summary>
    ''' Reads the data of all the AdminBinnacles in the table prgAdminBinnacle and return
    ''' a list of DAClsAdminBinnacles.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacles.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch() As Struct()
        Fetch = DoFetch(CreateSql())
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacle in the table prgAdminBinnacle and return
    ''' a list of DAClsAdminBinnacles.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacles.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pID As Parameter(Of Long)) As Struct()
        Fetch = DoFetch(CreateSql("ID = " & pID.Value.ToString))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacle in the table prgAdminBinnacle and return
    ''' a list of DAClsAdminBinnacles.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacles.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDUser As Parameter(Of Long), ByVal bDate As Parameter(Of Date)) As Struct()
        Fetch = DoFetch(CreateSql("IDUser = " & pIDUser.Value.ToString, "BDate = '" & bDate.Value.ToString("yyyy-M-dd") & "'"))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacle in the table prgAdminBinnacle and return
    ''' a list of DAClsAdminBinnacles.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacles.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDUser As Parameter(Of Long), ByVal pFromDate As Parameter(Of Date), ByVal pToDate As Parameter(Of Date)) As Struct()
        Fetch = DoFetch(CreateSql(GetParametersLists(pIDUser, pFromDate, pToDate)))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacle in the table prgAdminBinnacle and return
    ''' a list of DAClsAdminBinnacles.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacles.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDUser As ParameterList(Of Long), ByVal pFromDate As Parameter(Of Date), ByVal pToDate As Parameter(Of Date)) As Struct()
        Fetch = DoFetch(CreateSql(GetParametersLists(pIDUser, pFromDate, pToDate)))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdminBinnacle in the table prgAdminBinnacle and return
    ''' a list of DAClsAdminBinnacles.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdminBinnacles.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function FetchByUser(ByVal pIDUser As Parameter(Of Long)) As Struct()
        FetchByUser = DoFetch(CreateSql("IDUser = " & pIDUser.Value.ToString))
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
            parameters.Add("BDate >= '" & pFromDate.Value & "'")
            priorities.Add(pFromDate.Priority)
        End If

        If pToDate.Priority <> -1 AndAlso pToDate.Value.Date >= New Date(1900, 1, 1).Date AndAlso pToDate.Value.Date < Date.MaxValue.Date Then
            parameters.Add("BDate <= '" & pToDate.Value & "'")
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
    Private Shared Function CreateSql(ByVal ParamArray pParametersList() As String) As String

        'Build SQL value
        Dim sql As String = ""
        sql &= " Select"
        sql &= "  ID,"
        sql &= "  IDUser,"
        sql &= "  BDate"
        sql &= " From"
        sql &= "  prgAdministrativeBinnacle"

        If pParametersList.Length > 0 Then
            sql &= " Where " & Join(pParametersList, " And ")
        End If

        Return sql
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
        sql &= "  IDUser,"
        sql &= "  BDate"
        sql &= " From"
        sql &= "  prgAdministrativeBinnacle"

        Dim list As New List(Of String)
        For Each item As String() In pParams
            If item.Length > 0 Then
                list.Add("(" & Join(item, " Or ") & ")")
            End If
        Next
        If list.Count > 0 Then
            sql &= " Where " & Join(list.ToArray, " And ")
        End If

        Return sql
    End Function

    ''' <summary>
    ''' Execute SQL statement to reads the data of the table prgAdminBinnacle and
    ''' return a list of DAClsAdminBinnacles.Struct with each one of the read registries 
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
                    Struct.IDUser.SetValue(dr.GetInt64("IDUser"))
                    Struct.BDate.SetValue(dr.GetDateTime("BDate"))
                    List.Add(Struct)
                End While
                DoFetch = List.ToArray
            End Using
        End Using
    End Function

    Private Shared Function RefreshStruct(ByVal pStruct As Struct) As Struct
        pStruct.ID.OldValue = pStruct.ID.Value
        pStruct.ID.Value = pStruct.ID.NewValue

        pStruct.IDUser.OldValue = pStruct.IDUser.Value
        pStruct.IDUser.Value = pStruct.IDUser.NewValue

        pStruct.BDate.OldValue = pStruct.BDate.Value
        pStruct.BDate.Value = pStruct.BDate.NewValue

        Return pStruct
    End Function

    ''' <summary>
    ''' Insert a new registry in the table prgAdminBinnacle and return ID
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the AdminBinnacle to insert</param>
    ''' <returns>ID</returns>
    ''' <remarks></remarks>
    Public Shared Function Insert(ByVal pStruct As Struct) As Struct

        'Build SQL value for insert
        Dim sql As String = ""
        sql &= " Insert Into prgAdministrativeBinnacle"
        sql &= "  ("
        sql &= "    IDUser,"
        sql &= "    BDate"
        sql &= "  )"
        sql &= " Values"
        sql &= "  ("
        sql &= "    " & pStruct.IDUser.NewValue.ToString & ","
        sql &= "    '" & pStruct.BDate.NewValue.ToString("yyyy-M-dd") & "'"
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
    ''' Update a registry in the table prgAdminBinnacle and return the number of rows affected
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the AdminBinnacle to update</param>
    ''' <returns>Number of rows affected</returns>
    ''' <remarks></remarks>
    Public Shared Function Update(ByVal pStruct As Struct) As Struct

        'Build SQL value
        Dim sql As String = ""
        sql &= " UPDATE prgAdministrativeBinnacle"
        sql &= " SET"
        sql &= "  IDUser = " & pStruct.IDUser.NewValue.ToString & ","
        sql &= "  BDate = '" & pStruct.BDate.NewValue.ToString("yyyy-M-dd") & "'"
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
    ''' Delete a registry in the table prgAdminBinnacle and return the number of rows affected
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub Delete(ByVal pID As Long)

        'Build SQL value
        Dim sql As String = ""
        sql &= " Delete From prgAdministrativeBinnacle"
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
