Imports Csla.Data
Imports DBConnection
Imports System.Data.OleDb

''' <summary>
''' Represents a controller of data for the appAdvertiserAdHistory table in the data base ServerComputerTracking
''' </summary>
''' <remarks></remarks>
<Serializable()> Public Class DAClsappAdvertiserAdHistory

    ''' <summary>
    ''' Represents one structures with the data of a registry of the table appAdvertiserAdHistory
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> Public Class Struct
        Public TableName As String = "appAdvertiserAdHistory"

        Public ID As StructField(Of Long) = New StructField(Of Long)("ID", True)
        Public IDProject As StructField(Of Long) = New StructField(Of Long)("IDProject", False)
        Public IDSubscriber As StructField(Of Long) = New StructField(Of Long)("IDSubscriber", False)
        Public DateAdDisplay As StructField(Of Date) = New StructField(Of Date)("DateAdDisplay", False)
        Public TimeAdDisplay As StructField(Of Date) = New StructField(Of Date)("TimeAdDisplay", False)
        Public DateAdClickThru As StructField(Of Date) = New StructField(Of Date)("DateAdClickThru", False)
        Public TimeAdClickThru As StructField(Of Date) = New StructField(Of Date)("TimeAdClickThru", False)
        Public URLAdDisplay As StructField(Of String) = New StructField(Of String)("URLAdDisplay", False, String.Empty)
        Public URLAdClickThru As StructField(Of String) = New StructField(Of String)("URLAdClickThru", False, String.Empty)

    End Class

    ''' <summary>
    ''' Reads the data of all the Advertiser AdHistory in the table appAdvertiserAdHistory and
    ''' return a list of DAClsAdvertiserAdHistory.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsAdvertiserAdHistory.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch() As Struct()
        Fetch = DoFetch(CreateSql(New String() {}))
    End Function

    ''' <summary>
    ''' Read the data of a certain Advertiser AdHistory in the table appAdvertiserAdHistory and
    ''' return a list of DAClsAdvertiserAdHistory.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pID">ID of Advertiser AdHistory to fetch</param>
    ''' <returns>List of DAClsAdvertiserAdHistory.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pID As Long) As Struct()
        Fetch = DoFetch(CreateSql("ID = " & pID.ToString))
    End Function

    ''' <summary>
    ''' Read the data of a certain Advertiser AdHistory in the table appAdvertiserAdHistory and
    ''' return a list of DAClsAdvertiserAdHistory.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdvertiserAdHistory.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDProject As Long, ByVal pIDSubscriber As Long, ByVal pFromDate As Date, ByVal pToDate As Date, ByVal pFromTime As Date, ByVal pToTime As Date) As Struct()
        Fetch = DoFetch(CreateSql(GetParamsList(pIDProject, pIDSubscriber, pFromDate, pToDate, pFromTime, pToTime)))
    End Function

    ''' <summary>
    ''' Read the data of a certain Advertiser AdHistory in the table appAdvertiserAdHistory and
    ''' return a list of DAClsAdvertiserAdHistory.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdvertiserAdHistory.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDProjects() As Long, ByVal pIDSubscriber() As Long, ByVal pFromDate As Date, ByVal pToDate As Date, ByVal pFromTime As Date, ByVal pToTime As Date) As Struct()
        Fetch = DoFetch(CreateSql(GetParamsList(pIDProjects, pIDSubscriber, pFromDate, pToDate, pFromTime, pToTime)))
    End Function

    ''' <summary>
    ''' Read the data of the Advertiser AdHistory in the table appAdvertiserAdHistory and
    ''' return a list of DAClsAdvertiserAdHistory.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pIDProject">Project ID of AdHistory to fetch</param>
    ''' <returns>List of DAClsAdvertiserAdHistory.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function FetchProjectAdHistory(ByVal pIDProject As Long) As Struct()
        FetchProjectAdHistory = DoFetch(CreateSql("IDProject = " & pIDProject.ToString))
    End Function

    ''' <summary>
    ''' Read the data of the subscriber AdHistory in the table appAdvertiserAdHistory and
    ''' return a list of DAClsAdvertiserAdHistory.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pIDSubscriber">Subscriber ID of AdHistory to fetch</param>
    ''' <returns>List of DAClsAdvertiserAdHistory.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function FetchSubscriberAdHistory(ByVal pIDSubscriber As Long) As Struct()
        FetchSubscriberAdHistory = DoFetch(CreateSql("IDSubscriber = " & pIDSubscriber.ToString))
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pIDProject"></param>
    ''' <param name="pIDSubscriber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParamsList(ByVal pIDProject As Long, ByVal pIDSubscriber As Long, ByVal pFromDate As Date, ByVal pToDate As Date, ByVal pFromTime As Date, ByVal pToTime As Date) As String()
        Dim params As New List(Of String)

        If pIDProject <> 0 Then
            params.Add("IDProject = " & pIDProject.ToString)
        End If

        If pIDSubscriber <> 0 Then
            params.Add("IDSubscriber = " & pIDSubscriber.ToString)
        End If

        If pFromDate.Date > New Date(1900, 1, 1).Date AndAlso pFromDate.Date <= Date.MaxValue.Date Then
            params.Add("DateAdDisplay >= '" & pFromDate.Date.ToString("yyyy-M-dd") & "'")
        End If

        If pToDate.Date >= New Date(1900, 1, 1).Date AndAlso pToDate.Date < Date.MaxValue.Date Then
            params.Add("DateAdDisplay <= '" & pToDate.Date.ToString("yyyy-M-dd") & "'")
        End If

        If pFromTime.TimeOfDay > Date.MinValue.TimeOfDay Then
            params.Add("TimeAdDisplay >= '" & pFromTime.ToString("HH:mm:ss") & "'")
        End If

        If pToTime.TimeOfDay < Date.MaxValue.TimeOfDay Then
            params.Add("TimeAdDisplay <= '" & pToTime.ToString("HH:mm:ss") & "'")
        End If

        Return params.ToArray
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pIDProjects"></param>
    ''' <param name="pIDSubscribers"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParamsList(ByVal pIDProjects() As Long, ByVal pIDSubscribers() As Long, ByVal pFromDate As Date, ByVal pToDate As Date, ByVal pFromTime As Date, ByVal pToTime As Date) As String()()
        Dim paramsList As New List(Of String())

        If pIDProjects.Length > 0 Then
            Dim paramList As New List(Of String)
            For Each idProject As Long In pIDProjects
                If idProject <> 0 Then
                    paramList.Add("IDProject = " & idProject.ToString)
                End If
            Next
            paramsList.Add(paramList.ToArray)
        End If

        If pIDSubscribers.Length > 0 Then
            Dim paramList As New List(Of String)
            For Each idSubscriber As Long In pIDSubscribers
                If idSubscriber <> 0 Then
                    paramList.Add("IDSubscriber = " & idSubscriber.ToString)
                End If
            Next
            paramsList.Add(paramList.ToArray)
        End If

        If pFromDate.Date > New Date(1900, 1, 1).Date AndAlso pFromDate.Date <= Date.MaxValue.Date Then
            paramsList.Add(New String() {"DateAdDisplay >= '" & pFromDate.Date.ToString("yyyy-M-dd") & "'"})
        End If

        If pToDate.Date >= New Date(1900, 1, 1).Date AndAlso pToDate.Date < Date.MaxValue.Date Then
            paramsList.Add(New String() {"DateAdDisplay <= '" & pToDate.Date.ToString("yyyy-M-dd") & "'"})
        End If

        If pFromTime.TimeOfDay > Date.MinValue.TimeOfDay Then
            paramsList.Add(New String() {"TimeAdDisplay >= '" & pFromTime.ToString("HH:mm:ss") & "'"})
        End If

        If pToTime.TimeOfDay < Date.MaxValue.TimeOfDay Then
            paramsList.Add(New String() {"TimeAdDisplay <= '" & pToTime.ToString("HH:mm:ss") & "'"})
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
        sql &= "  IDProject,"
        sql &= "  IDSubscriber,"
        sql &= "  DateAdDisplay,"
        sql &= "  TimeAdDisplay,"
        sql &= "  DateAdClickThru,"
        sql &= "  TimeAdClickThru,"
        sql &= "  URLAdDisplay,"
        sql &= "  URLAdClickThru"
        sql &= " From"
        sql &= "  appAdvertiserAdHistory"

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
        sql &= "  IDProject,"
        sql &= "  IDSubscriber,"
        sql &= "  DateAdDisplay,"
        sql &= "  TimeAdDisplay,"
        sql &= "  DateAdClickThru,"
        sql &= "  TimeAdClickThru,"
        sql &= "  URLAdDisplay,"
        sql &= "  URLAdClickThru"
        sql &= " From"
        sql &= "  appAdvertiserAdHistory"

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
    ''' Execute SQL statement to reads the data of the table appAdvertiserAdHistory and return
    ''' a list of DAClsAdvertiserAdHistory.Struct with each one of the read registries 
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
                    Struct.IDProject.SetValue(dr.GetInt64("IDProject"))
                    Struct.IDSubscriber.SetValue(dr.GetInt64("IDSubscriber"))
                    Struct.DateAdDisplay.SetValue(dr.GetDateTime("DateAdDisplay"))
                    Struct.TimeAdDisplay.SetValue(dr.GetDateTime("TimeAdDisplay"))
                    Struct.DateAdClickThru.SetValue(dr.GetDateTime("DateAdClickThru"))
                    Struct.TimeAdClickThru.SetValue(dr.GetDateTime("TimeAdClickThru"))
                    Struct.URLAdDisplay.SetValue(dr.GetString("URLAdDisplay"))
                    Struct.URLAdClickThru.SetValue(dr.GetString("URLAdClickThru"))
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

        pStruct.IDProject.OldValue = pStruct.IDProject.Value
        pStruct.IDProject.Value = pStruct.IDProject.NewValue

        pStruct.IDSubscriber.OldValue = pStruct.IDSubscriber.Value
        pStruct.IDSubscriber.Value = pStruct.IDSubscriber.NewValue

        pStruct.DateAdClickThru.OldValue = pStruct.DateAdClickThru.Value
        pStruct.DateAdClickThru.Value = pStruct.DateAdClickThru.NewValue

        pStruct.DateAdDisplay.OldValue = pStruct.DateAdDisplay.Value
        pStruct.DateAdDisplay.Value = pStruct.DateAdDisplay.NewValue

        pStruct.TimeAdClickThru.OldValue = pStruct.TimeAdClickThru.Value
        pStruct.TimeAdClickThru.Value = pStruct.TimeAdClickThru.NewValue

        pStruct.TimeAdDisplay.OldValue = pStruct.TimeAdDisplay.Value
        pStruct.TimeAdDisplay.Value = pStruct.TimeAdDisplay.NewValue

        pStruct.URLAdClickThru.OldValue = pStruct.URLAdClickThru.Value
        pStruct.URLAdClickThru.Value = pStruct.URLAdClickThru.NewValue

        pStruct.URLAdDisplay.OldValue = pStruct.URLAdDisplay.Value
        pStruct.URLAdDisplay.Value = pStruct.URLAdDisplay.NewValue

        Return pStruct
    End Function

    ''' <summary>
    ''' Insert a new registry in the table appAdvertiserAdHistory and return Advertiser AdHistory ID
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the Advertiser AdHistory to insert</param>
    ''' <returns>Advertiser AdHistory ID</returns>
    ''' <remarks></remarks>
    Public Shared Function Insert(ByVal pStruct As Struct) As Struct

        'Build SQL value for insert
        Dim sql As String = ""
        Sql &= " Insert Into appAdvertiserAdHistory"
        Sql &= "  ("
        Sql &= "    IDProject,"
        Sql &= "    IDSubscriber,"
        Sql &= "    DateAdDisplay,"
        Sql &= "    TimeAdDisplay,"
        Sql &= "    DateAdClickThru,"
        Sql &= "    TimeAdClickThru,"
        Sql &= "    URLAdDisplay,"
        Sql &= "    URLAdClickThru"
        Sql &= "  )"
        Sql &= " Values"
        Sql &= "  ("
        Sql &= "     " & pStruct.IDProject.NewValue.ToString & ","
        Sql &= "     " & pStruct.IDSubscriber.NewValue.ToString & ","
        Sql &= "    '" & pStruct.DateAdDisplay.NewValue.ToString("yyyy-M-dd") & "',"
        Sql &= "    '" & pStruct.TimeAdDisplay.NewValue.ToString("HH:mm:ss") & "',"
        Sql &= "    '" & pStruct.DateAdClickThru.NewValue.ToString("yyyy-M-dd") & "',"
        Sql &= "    '" & pStruct.TimeAdClickThru.NewValue.ToString("HH:mm:ss") & "',"
        Sql &= "    '" & pStruct.URLAdDisplay.NewValue & "',"
        Sql &= "    '" & pStruct.URLAdClickThru.NewValue & "'"
        Sql &= "  );"
        Sql &= " SELECT SCOPE_IDENTITY()"

        Try
            'Connection to the data base 
            Using DBConn As DBClsConnection = New DBClsConnection(DADataBase.SCTConnectionString)

                'Execute Query
                pStruct.ID.NewValue = DBConn.ExecuteScalar(Sql)
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
    ''' Update a registry in the table appAdvertiserAdHistory and return the number of rows affected
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the Advertiser AdHistory to Update</param>
    ''' <returns>Number of rows affected</returns>
    ''' <remarks></remarks>
    Public Shared Function Update(ByVal pStruct As Struct) As Struct

        'Build SQL value
        Dim sql As String = ""
        sql &= " UPDATE appAdvertiserAdHistory"
        sql &= " SET"
        sql &= "  IDProject = " & pStruct.IDProject.NewValue.ToString & ","
        sql &= "  IDSubscriber = " & pStruct.IDSubscriber.NewValue.ToString & ","
        sql &= "  DateAdDisplay = '" & pStruct.DateAdDisplay.NewValue.ToString("yyyy-M-dd") & "',"
        sql &= "  TimeAdDisplay = '" & pStruct.TimeAdDisplay.NewValue.ToString("HH:mm") & "',"
        sql &= "  DateAdClickThru = '" & pStruct.DateAdClickThru.NewValue.ToString("yyyy-M-dd") & "',"
        sql &= "  TimeAdClickThru = '" & pStruct.TimeAdClickThru.NewValue.ToString("HH:mm") & "',"
        sql &= "  URLAdDisplay = '" & pStruct.URLAdDisplay.NewValue & "',"
        sql &= "  URLAdClickThru = '" & pStruct.URLAdClickThru.NewValue & "'"
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
    ''' Delete a registry in the table appAdvertiserAdHistory and return the number of rows affected
    ''' </summary>
    ''' <param name="pID">ID of the Advertiser AdHistory to delete</param>
    ''' <remarks></remarks>
    Public Shared Sub Delete(ByVal pID As Long)

        'Build SQL value
        Dim sql As String = ""
        sql &= " Delete From appAdvertiserAdHistory"
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