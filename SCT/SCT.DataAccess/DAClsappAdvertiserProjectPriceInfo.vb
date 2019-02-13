Imports Csla.Data
Imports DBConnection
Imports System.Data.OleDb

''' <summary>
''' Represents a controller of data for the appAdvertiserProjectPriceInfo table in the data base ServerComputerTracking
''' </summary>
''' <remarks></remarks>
<Serializable()> Public Class DAClsappAdvertiserProjectPriceInfo

    ''' <summary>
    ''' Represents one structures with the data of a registry of the table appAdvertiserProjectPriceInfo
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> Public Class Struct
        Public TableName As String = "appAdvertiserProjectPriceInfo"

        Public ID As StructField(Of Long) = New StructField(Of Long)("ID", True)
        Public IDAdvertiserProject As StructField(Of Long) = New StructField(Of Long)("IDAdvertiserProject", False)
        Public CostPerClickThrough As StructField(Of Double) = New StructField(Of Double)("CostPerClickThrough", False)
        Public CostPerDisplay As StructField(Of Double) = New StructField(Of Double)("CostPerDisplay", False)

    End Class

    ''' <summary>
    ''' Reads the data of all the Advertiser Project Price Info in the table appAdvertiserProjectPriceInfo
    ''' and return a list of DAClsAdvertiserProjectPriceInfo.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsAdvertiserProjectPriceInfo.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch() As Struct()
        Fetch = DoFetch(CreateSql(New String() {}))
    End Function

    ''' <summary>
    ''' Read the data of a certain Advertiser Project Price Info in the table appAdvertiserProjectPriceInfo
    ''' and return a list of DAClsAdvertiserProjectPriceInfo.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pID">ID of Advertiser Project Price Info to fetch</param>
    ''' <returns>List of DAClsAdvertiserProjectPriceInfo.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pID As Long) As Struct()
        Fetch = DoFetch(CreateSql("ID = " & pID.ToString))
    End Function

    ''' <summary>
    ''' Read the data of a certain Advertiser Project Price Info in the table appAdvertiserProjectPriceInfo
    ''' and return a list of DAClsAdvertiserProjectPriceInfo.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdvertiserProjectPriceInfo.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function FetchByProject(ByVal pIDAdvertiserProject As Long) As Struct()
        FetchByProject = DoFetch(CreateSql(GetParamsList(pIDAdvertiserProject)))
    End Function

    ''' <summary>
    ''' Read the data of a certain Advertiser Project Price Info in the table appAdvertiserProjectPriceInfo
    ''' and return a list of DAClsAdvertiserProjectPriceInfo.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdvertiserProjectPriceInfo.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function FetchByProject(ByVal pIDAdvertiserProject() As Long) As Struct()
        FetchByProject = DoFetch(CreateSql(GetParamsList(pIDAdvertiserProject)))
    End Function

    ''' <summary>
    ''' Read the data of the project price info in the table appAdvertiserProjectPriceInfo and
    ''' return a list of DAClsAdvertiserProjectPriceInfo.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pIDAdvertiserProject">Project ID of price info to fetch</param>
    ''' <returns>List of DAClsAdvertiserProjectPriceInfo.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function FetchProjectPriceInfo(ByVal pIDAdvertiserProject As Long) As Struct()
        FetchProjectPriceInfo = DoFetch(CreateSql("IDAdvertiserProject = " & pIDAdvertiserProject.ToString))
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pIDAdvertiserProject"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParamsList(ByVal pIDAdvertiserProject As Long) As String()
        Dim params As New List(Of String)

        If pIDAdvertiserProject <> 0 Then
            params.Add("IDAdvertiserProject = " & pIDAdvertiserProject.ToString)
        End If

        Return params.ToArray
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pIDAdvertiserProjects"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParamsList(ByVal pIDAdvertiserProjects() As Long) As String()()
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

        Return paramsList.ToArray
    End Function

    ''' <summary>
    ''' Creates the Sql Statement and returns it
    ''' </summary>
    '''<param name="pParams"></param>
    ''' <returns>Sql Statement</returns>
    ''' <remarks></remarks>
    Private Shared Function CreateSql(ByVal ParamArray pParams() As String) As String

        'Build SQL value
        Dim sql As String = ""
        sql &= " Select"
        sql &= "  ID,"
        sql &= "  IDAdvertiserProject,"
        sql &= "  CostPerDisplay,"
        sql &= "  CostPerClickThrough"
        sql &= " From"
        sql &= "  appAdvertiserProjectPriceInfo"

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
        sql &= "  IDAdvertiserProject,"
        sql &= "  CostPerDisplay,"
        sql &= "  CostPerClickThrough"
        sql &= " From"
        sql &= "  appAdvertiserProjectPriceInfo"

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
    ''' Execute SQL statement to reads the data of the table appAdvertiserProjectPriceInfo and
    ''' return a list of DAClsAdvertiserProjectPriceInfo.Struct with each one of the read registries 
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
                    Struct.IDAdvertiserProject.SetValue(dr.GetInt64("IDAdvertiserProject"))
                    Struct.CostPerDisplay.SetValue(dr.GetDouble("CostPerDisplay"))
                    Struct.CostPerClickThrough.SetValue(dr.GetDouble("CostPerClickThrough"))
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

        pStruct.IDAdvertiserProject.OldValue = pStruct.IDAdvertiserProject.Value
        pStruct.IDAdvertiserProject.Value = pStruct.IDAdvertiserProject.NewValue

        pStruct.CostPerDisplay.OldValue = pStruct.CostPerDisplay.Value
        pStruct.CostPerDisplay.Value = pStruct.CostPerDisplay.NewValue

        pStruct.CostPerClickThrough.OldValue = pStruct.CostPerClickThrough.Value
        pStruct.CostPerClickThrough.Value = pStruct.CostPerClickThrough.NewValue

        Return pStruct
    End Function

    ''' <summary>
    ''' Insert a new registry in the table appAdvertiserProjectPriceInfo
    ''' and return Advertiser Project Price Info ID
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the Advertiser Project Price Info to insert</param>
    ''' <returns>Advertiser Project Price Info ID</returns>
    ''' <remarks></remarks>
    Public Shared Function Insert(ByVal pStruct As Struct) As Struct

        'Build SQL value for insert
        Dim sql As String = ""
        sql &= " Insert Into appAdvertiserProjectPriceInfo"
        sql &= "  ("
        sql &= "    IDAdvertiserProject,"
        sql &= "    CostPerDisplay,"
        sql &= "    CostPerClickThrough"
        sql &= "  )"
        sql &= " Values"
        sql &= "  ("
        sql &= "     " & pStruct.IDAdvertiserProject.NewValue.ToString & ","
        sql &= "     " & pStruct.CostPerDisplay.NewValue.ToString & ","
        sql &= "     " & pStruct.CostPerClickThrough.NewValue.ToString
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
    ''' Update a registry in the table appAdvertiserProjectPriceInfo and return the number of rows affected
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the Advertiser Project Price Info to update</param>
    ''' <returns>Number of rows affected</returns>
    ''' <remarks></remarks>
    Public Shared Function Update(ByVal pStruct As Struct) As Struct

        'Build SQL value
        Dim sql As String = ""
        sql &= " UPDATE appAdvertiserProjectPriceInfo"
        sql &= " SET"
        sql &= "  IDAdvertiserProject = " & pStruct.IDAdvertiserProject.NewValue.ToString & ","
        sql &= "  CostPerDisplay = " & pStruct.CostPerDisplay.NewValue.ToString & ","
        sql &= "  PaidByClickThrough = " & pStruct.CostPerClickThrough.NewValue.ToString
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
    ''' Delete a registry in the table appAdvertiserProjectPriceInfo and return the number of rows affected
    ''' </summary>
    ''' <param name="pID">ID of the Advertiser Project Price Info to delete</param>
    ''' <remarks></remarks>
    Public Shared Sub Delete(ByVal pID As Long)

        'Build SQL value
        Dim sql As String = ""
        sql &= " Delete From appAdvertiserProjectPriceInfo"
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