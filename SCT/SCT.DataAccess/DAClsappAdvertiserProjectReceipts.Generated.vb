Imports Csla.data
Imports DBConnection
Imports System.Data.OleDb

''' <summary>
''' Represents a controller of data for the appAdvertiserProjectReceipts table in the data base PCS 
''' </summary>
''' <remarks></remarks>
<Serializable()> Public Class DAClsappAdvertiserProjectReceipts

    ''' <summary>
    ''' Represents one structures with the data of a registry of the table appAdvertiserProjectReceipts 
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> Public Class Struct
        Public TableName As String = "appAdvertiserProjectReceipts"

        Public IDAdvertiserProject As StructField(Of Long) = New StructField(Of Long)("IDAdvertiserProject", True)
        Public IDAdvertiserReceipt As StructField(Of Long) = New StructField(Of Long)("IDAdvertiserReceipt", True)
        Public PaidByDisplay As StructField(Of Double) = New StructField(Of Double)("PaidByDisplay", False)
        Public PaidByClickThrough As StructField(Of Double) = New StructField(Of Double)("PaidByClickThrough", False)
        Public TotalPaid As StructField(Of Double) = New StructField(Of Double)("TotalPaid", False)
    End Class

    ''' <summary>
    ''' Reads the data of all the AdvertiserProjectReceipts in the table appAdvertiserProjectReceipts and return
    ''' a list of DAClsAdvertiserProjectReceipts.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsAdvertiserProjectReceipts.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch() As Struct()
        Fetch = DoFetch(CreateSelectSql(New String() {}))
    End Function

    ''' <summary>
    ''' Reads the data of all the AdvertiserProjectReceipts in the table appAdvertiserProjectReceipts and return
    ''' a list of DAClsAdvertiserProjectReceipts.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsAdvertiserProjectReceipts.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDAdvertiserProject As Long, ByVal pIDAdvertiserReceipt As Long) As Struct()
        Fetch = DoFetch(CreateSelectSql(GetParamsList(pIDAdvertiserProject, pIDAdvertiserReceipt)))
    End Function

    ''' <summary>
    ''' Reads the data of all the AdvertiserProjectReceipts in the table appAdvertiserProjectReceipts and return
    ''' a list of DAClsAdvertiserProjectReceipts.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsAdvertiserProjectReceipts.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDAdvertiserProjects() As Long, ByVal pIDAdvertiserReceipts() As Long) As Struct()
        Fetch = DoFetch(CreateSelectSql(GetParamsList(pIDAdvertiserProjects, pIDAdvertiserReceipts)))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdvertiserProjectReceipt in the table appAdvertiserProjectReceipts and return
    ''' a list of DAClsAdvertiserProjectReceipts.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdvertiserProjectReceipts.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function FetchProjectReceipt(ByVal pIDAdvertiserProject As Long) As Struct()
        FetchProjectReceipt = DoFetch(CreateSelectSql("IDAdvertiserProject = " & pIDAdvertiserProject.ToString))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdvertiserProjectReceipt in the table appAdvertiserProjectReceipts and return
    ''' a list of DAClsAdvertiserProjectReceipts.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdvertiserProjectReceipts.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function FetchReceiptProject(ByVal pIDAdvertiserReceipt As Long) As Struct()
        FetchReceiptProject = DoFetch(CreateSelectSql("IDAdvertiserReceipt = " & pIDAdvertiserReceipt.ToString))
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pIDAdvertiserProject"></param>
    ''' <param name="pIDAdvertiserReceipt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParamsList(ByVal pIDAdvertiserProject As Long, ByVal pIDAdvertiserReceipt As Long) As String()
        Dim params As New List(Of String)

        If pIDAdvertiserProject <> 0 Then
            params.Add("IDAdvertiserProject = " & pIDAdvertiserProject.ToString)
        End If

        If pIDAdvertiserReceipt <> 0 Then
            params.Add("IDAdvertiserReceipt = " & pIDAdvertiserReceipt.ToString)
        End If

        Return params.ToArray
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pIDAdvertiserProjects"></param>
    ''' <param name="pIDAdvertiserReceipts"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParamsList(ByVal pIDAdvertiserProjects() As Long, ByVal pIDAdvertiserReceipts() As Long) As String()()
        Dim paramsList As New List(Of String())

        If pIDAdvertiserProjects.Length > 0 Then
            Dim paramList As New List(Of String)
            For Each idAdvertiserProject As Long In pIDAdvertiserProjects
                If idAdvertiserProject <> 0 Then
                    paramList.Add("IDAdvertiserProject = " & idAdvertiserProject.ToString)
                End If
            Next
            paramsList.Add(paramList.ToArray)
        End If

        If pIDAdvertiserReceipts.Length > 0 Then
            Dim paramList As New List(Of String)
            For Each idAdvertiserReceipt As Long In pIDAdvertiserReceipts
                If idAdvertiserReceipt <> 0 Then
                    paramList.Add("IDAdvertiserReceipt = " & idAdvertiserReceipt.ToString)
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
        sql &= "  IDAdvertiserProject,"
        sql &= "  IDAdvertiserReceipt,"
        sql &= "  PaidByDisplay,"
        sql &= "  PaidByClickThrough,"
        sql &= "  TotalPaid"
        sql &= " From"
        sql &= "  appAdvertiserProjectReceipts"

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
        sql &= "  IDAdvertiserProject,"
        sql &= "  IDAdvertiserReceipt,"
        sql &= "  PaidByDisplay,"
        sql &= "  PaidByClickThrough,"
        sql &= "  TotalPaid"
        sql &= " From"
        sql &= "  appAdvertiserProjectReceipts"

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
    ''' Execute SQL statement to reads the data of the table appAdvertiserProjectReceipts and
    ''' return a list of DAClsAdvertiserProjectReceipts.Struct with each one of the read registries 
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
                    Struct.IDAdvertiserProject.SetValue(dr.GetInt64("IDAdvertiserProject"))
                    Struct.IDAdvertiserReceipt.SetValue(dr.GetInt64("IDAdvertiserReceipt"))
                    Struct.PaidByDisplay.SetValue(dr.GetDouble("PaidByDisplay"))
                    Struct.PaidByClickThrough.SetValue(dr.GetDouble("PaidByClickThrough"))
                    Struct.TotalPaid.SetValue(dr.GetDouble("TotalPaid"))
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
        pStruct.IDAdvertiserProject.OldValue = pStruct.IDAdvertiserProject.Value
        pStruct.IDAdvertiserProject.Value = pStruct.IDAdvertiserProject.NewValue

        pStruct.IDAdvertiserReceipt.OldValue = pStruct.IDAdvertiserReceipt.Value
        pStruct.IDAdvertiserReceipt.Value = pStruct.IDAdvertiserReceipt.NewValue

        pStruct.PaidByClickThrough.OldValue = pStruct.PaidByClickThrough.Value
        pStruct.PaidByClickThrough.Value = pStruct.PaidByClickThrough.NewValue

        pStruct.PaidByDisplay.OldValue = pStruct.PaidByDisplay.Value
        pStruct.PaidByDisplay.Value = pStruct.PaidByDisplay.NewValue

        pStruct.TotalPaid.OldValue = pStruct.TotalPaid.Value
        pStruct.TotalPaid.Value = pStruct.TotalPaid.NewValue

        Return pStruct
    End Function

    ''' <summary>
    ''' Insert a new registry in the table appAdvertiserProjectReceipts and return IDAdvertiserProject
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the AdvertiserProjectReceipt to insert</param>
    ''' <returns>IDAdvertiserProject</returns>
    ''' <remarks></remarks>
    Public Shared Function Insert(ByVal pStruct As Struct) As Struct

        'Build SQL value for insert
        Dim sql As String = ""
        sql &= " Insert Into appAdvertiserProjectReceipts"
        sql &= "  ("
        sql &= "    IDAdvertiserProject,"
        sql &= "    IDAdvertiserReceipt,"
        sql &= "    PaidByDisplay,"
        sql &= "    PaidByClickThrough,"
        sql &= "    TotalPaid"
        sql &= "  )"
        sql &= " Values"
        sql &= "  ("
        sql &= "    " & pStruct.IDAdvertiserProject.NewValue.ToString & ","
        sql &= "    " & pStruct.IDAdvertiserReceipt.NewValue.ToString & ","
        sql &= "    " & pStruct.PaidByDisplay.NewValue.ToString & ","
        sql &= "    " & pStruct.PaidByClickThrough.NewValue.ToString & ","
        sql &= "    " & pStruct.TotalPaid.NewValue.ToString
        sql &= "  );"
        sql &= " SELECT SCOPE_IDENTITY()"

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
    ''' Update a registry in the table appAdvertiserProjectReceipts and return the number of rows affected
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the AdvertiserProjectReceipt to update</param>
    ''' <returns>Number of rows affected</returns>
    ''' <remarks></remarks>
    Public Shared Function Update(ByVal pStruct As Struct) As Struct

        'Build SQL value
        Dim sql As String = ""
        sql &= " UPDATE appAdvertiserProjectReceipts"
        sql &= " SET"
        sql &= "  PaidByDisplay = " & pStruct.PaidByDisplay.NewValue.ToString & ","
        sql &= "  PaidByClickThrough = " & pStruct.PaidByClickThrough.NewValue.ToString & ","
        sql &= "  TotalPaid = " & pStruct.TotalPaid.NewValue.ToString & ""
        sql &= " Where IDAdvertiserProject = " & pStruct.IDAdvertiserProject.NewValue.ToString
        sql &= "   And IDAdvertiserReceipt = " & pStruct.IDAdvertiserReceipt.NewValue.ToString

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
    ''' Delete a registry in the table appAdvertiserProjectReceipts and return the number of rows affected
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub Delete(ByVal pIDAdvertiserProject As Long, ByVal pIDAdvertiserReceipt As Long)
        DoDelete(CreateDeleteSql("IDAdvertiserProject = " & pIDAdvertiserProject.ToString, "IDAdvertiserReceipt = " & pIDAdvertiserReceipt.ToString))
    End Sub

    ''' <summary>
    ''' Delete a registry in the table appAdvertiserProjectReceipts and return the number of rows affected
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub DeleteByProject(ByVal pIDAdvertiserProject As Long)
        DoDelete(CreateDeleteSql("IDAdvertiserProject = " & pIDAdvertiserProject.ToString))
    End Sub

    ''' <summary>
    ''' Delete a registry in the table appAdvertiserProjectReceipts and return the number of rows affected
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub DeleteByReceipt(ByVal pIDAdvertiserReceipt As Long)
        DoDelete(CreateDeleteSql("IDAdvertiserReceipt = " & pIDAdvertiserReceipt.ToString))
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
        sql &= "  appAdvertiserProjectReceipts"

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
