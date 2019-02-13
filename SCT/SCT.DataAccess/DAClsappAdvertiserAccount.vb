Imports Csla.Data
Imports DBConnection
Imports System.Data.OleDb

''' <summary>
''' Represents a controller of data for the appAdvertiserAccount table in the data base ServerComputerTracking
''' </summary>
''' <remarks></remarks>
<Serializable()> Public Class DAClsappAdvertiserAccount

    ''' <summary>
    ''' Represents one structures with the data of a registry of the table appAdvertiserAccount 
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> Public Class Struct
        Public TableName As String = "appAdvertiserAccount"

        Public ID As StructField(Of Long) = New StructField(Of Long)("ID", True)
        Public CompanyName As StructField(Of String) = New StructField(Of String)("CompanyName", False, String.Empty)
        Public CompanyNotes As StructField(Of String) = New StructField(Of String)("CompanyNotes", False, String.Empty)
        Public Login As StructField(Of String) = New StructField(Of String)("Login", False, String.Empty)
        Public WebPassword As StructField(Of String) = New StructField(Of String)("WebPassword", False, String.Empty)
        Public ClientPassword As StructField(Of String) = New StructField(Of String)("ClientPassword", False, String.Empty)
        Public HintByPassOne As StructField(Of String) = New StructField(Of String)("HintByPassOne", False, String.Empty)
        Public HintByPassTwo As StructField(Of String) = New StructField(Of String)("HintByPassTwo", False, String.Empty)
    End Class

    ''' <summary>
    ''' Reads the data of all the Advertiser Account in the table appAdvertiserAccount and return
    ''' a list of DAClsAdvertiserAccount.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsAdvertiserAccount.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch() As Struct()
        Fetch = DoFetch(CreateSql(New String() {}))
    End Function

    ''' <summary>
    ''' Read the data of a certain Advertiser Account in the table appAdvertiserAccount and return
    ''' a list of DAClsAdvertiserAccount.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pID">ID of Advertiser Account to fetch</param>
    ''' <returns>List of DAClsAdvertiserAccount.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pID As Long) As Struct()
        Fetch = DoFetch(CreateSql("ID = " & pID.ToString))
    End Function

    ''' <summary>
    ''' Reads the data of all the Advertiser Account in the table appAdvertiserAccount and return
    ''' a list of DAClsAdvertiserAccount.Struct with each one of the read registries
    ''' </summary>
    ''' <param name="pLogin"></param>
    ''' <returns>List of DAClsAdvertiserAccount.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLogin As String) As Struct()
        Fetch = DoFetch(CreateSql("Login = '" & pLogin & "'"))
    End Function

    ''' <summary>
    ''' Read the data of a certain Advertiser Account in the table appAdvertiserAccount and return
    ''' a list of DAClsAdvertiserAccount.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pLogin"></param> 
    ''' <param name="pPassword"></param>
    ''' <returns>List of DAClsAdvertiserAccount.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pLogin As String, ByVal pPassword As String) As Struct()
        Fetch = DoFetch(CreateSql("Login = '" & pLogin & "'", "WebPassword = '" & pPassword & "'"))
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
        sql &= "  CompanyName,"
        sql &= "  CompanyNotes,"
        sql &= "  Login,"
        sql &= "  WebPassword,"
        sql &= "  ClientPassword,"
        sql &= "  HintByPassOne,"
        sql &= "  HintByPassTwo"
        sql &= " From"
        sql &= "  appAdvertiserAccount"

        If pParams.Length > 0 Then
            sql &= " Where " & Join(pParams, " And ")
        End If

        CreateSql = sql
    End Function

    ''' <summary>
    ''' Execute SQL statement to reads the data of the table appAdvertiserAccount and
    ''' return a list of DAClsAdvertiserAccount.Struct with each one of the read registries 
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
                    Struct.CompanyName.SetValue(dr.GetString("CompanyName"))
                    Struct.CompanyNotes.SetValue(dr.GetString("CompanyNotes"))
                    Struct.Login.SetValue(dr.GetString("Login"))
                    Struct.WebPassword.SetValue(dr.GetString("WebPassword"))
                    Struct.ClientPassword.SetValue(dr.GetString("ClientPassword"))
                    Struct.HintByPassOne.SetValue(dr.GetString("HintByPassOne"))
                    Struct.HintByPassTwo.SetValue(dr.GetString("HintByPassTwo"))
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

        pStruct.CompanyName.OldValue = pStruct.CompanyName.Value
        pStruct.CompanyName.Value = pStruct.CompanyName.NewValue

        pStruct.CompanyNotes.OldValue = pStruct.CompanyNotes.Value
        pStruct.CompanyNotes.Value = pStruct.CompanyNotes.NewValue

        pStruct.Login.OldValue = pStruct.Login.Value
        pStruct.Login.Value = pStruct.Login.NewValue

        pStruct.WebPassword.OldValue = pStruct.WebPassword.Value
        pStruct.WebPassword.Value = pStruct.WebPassword.NewValue

        pStruct.ClientPassword.OldValue = pStruct.ClientPassword.Value
        pStruct.ClientPassword.Value = pStruct.ClientPassword.NewValue

        pStruct.HintByPassOne.OldValue = pStruct.HintByPassOne.Value
        pStruct.HintByPassOne.Value = pStruct.HintByPassOne.NewValue

        pStruct.HintByPassTwo.OldValue = pStruct.HintByPassTwo.Value
        pStruct.HintByPassTwo.Value = pStruct.HintByPassTwo.NewValue

        Return pStruct
    End Function

    ''' <summary>
    ''' Insert a new registry in the table appAdvertiserAccount and return Advertiser Account ID
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the Advertiser Account to insert</param>
    ''' <returns>Advertiser Account ID</returns>
    ''' <remarks></remarks>
    Public Shared Function Insert(ByVal pStruct As Struct) As Struct

        'Build SQL value for insert
        Dim sql As String = ""
        sql &= " Insert Into appAdvertiserAccount"
        sql &= "  ("
        sql &= "    CompanyName,"
        sql &= "    CompanyNotes,"
        sql &= "    Login,"
        sql &= "    WebPassword,"
        sql &= "    ClientPassword,"
        sql &= "    HintByPassOne,"
        sql &= "    HintByPassTwo"
        sql &= "  )"
        sql &= " Values"
        sql &= "  ("
        sql &= "    '" & pStruct.CompanyName.NewValue & "',"
        sql &= "    '" & pStruct.CompanyNotes.NewValue & "',"
        sql &= "    '" & pStruct.Login.NewValue & "',"
        sql &= "    '" & pStruct.WebPassword.NewValue & "',"
        sql &= "    '" & pStruct.ClientPassword.NewValue & "',"
        sql &= "    '" & pStruct.HintByPassOne.NewValue & "',"
        sql &= "    '" & pStruct.HintByPassTwo.NewValue & "'"
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
    ''' Update a registry in the table appAdvertiserAccount and return the number of rows affected
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the Advertiser Account to update</param>
    ''' <returns>Number of rows affected</returns>
    ''' <remarks></remarks>
    Public Shared Function Update(ByVal pStruct As Struct) As Struct

        'Build SQL value
        Dim sql As String = ""
        sql &= " UPDATE appAdvertiserAccount"
        sql &= " SET"
        sql &= "  CompanyName = '" & pStruct.CompanyName.NewValue & "',"
        sql &= "  CompanyNotes = '" & pStruct.CompanyNotes.NewValue & "',"
        sql &= "  Login = '" & pStruct.Login.NewValue & "',"
        sql &= "  WebPassword = '" & pStruct.WebPassword.NewValue & "',"
        sql &= "  ClientPassword = '" & pStruct.ClientPassword.NewValue & "',"
        sql &= "  HintByPassOne = '" & pStruct.HintByPassOne.NewValue & "',"
        sql &= "  HintByPassTwo = '" & pStruct.HintByPassTwo.NewValue & "'"
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
    ''' Delete a registry in the table appAdvertiserAccount and return the number of rows affected
    ''' </summary>
    ''' <param name="pID">ID of the Advertiser Account</param>
    ''' <remarks></remarks>
    Public Shared Sub Delete(ByVal pID As Long)

        'Build SQL value
        Dim sql As String = ""
        sql &= " Delete From appAdvertiserAccount"
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