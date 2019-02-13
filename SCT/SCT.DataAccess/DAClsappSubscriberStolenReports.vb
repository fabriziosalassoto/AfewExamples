Imports Csla.Data
Imports DBConnection
Imports System.Data.OleDb

''' <summary>
''' Represents a controller of data for the DAClsappSubscriberStolenReports table in the data base ServerComputerTracking
''' </summary>
''' <remarks></remarks>
<Serializable()> Public Class DAClsappSubscriberStolenReports

    ''' <summary>
    ''' Represents one structures with the data of a registry of the table appSubscriberStolenReports
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> Public Class Struct
        Public TableName As String = "appSubscriberStolenReports"

        Public ID As StructField(Of Long) = New StructField(Of Long)("ID", True)
        Public IDSubscriber As StructField(Of Long) = New StructField(Of Long)("IDSubscriber", False)
        Public IDAsset As StructField(Of Long) = New StructField(Of Long)("IDAsset", False)
        Public DateReportMissing As StructField(Of Date) = New StructField(Of Date)("DateReportMissing", False)
        Public LastKnownLocationDescription As StructField(Of String) = New StructField(Of String)("LastKnownLocationDescription", False, String.Empty)
        Public DateReportFound As StructField(Of Date) = New StructField(Of Date)("DateReportFound", False)
        Public ActiveForAlerts As StructField(Of Boolean) = New StructField(Of Boolean)("ActiveForAlerts", False)
        Public ActionToTake As StructField(Of Integer) = New StructField(Of Integer)("ActionToTake", False)

    End Class

    ''' <summary>
    ''' Reads the data of all the Subscriber Stolen Reports in the table appSubscriberStolenReports
    ''' and return a list of DAClsSubscriberStolenReport.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsSubscriberStolenReport.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch() As Struct()
        Fetch = DoFetch(CreateSql(New String() {}))
    End Function

    ''' <summary>
    ''' Read the data of a certain Subscriber Stolen Report in the table appSubscriberStolenReports
    ''' and return a list of DAClsSubscriberStolenReport.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pID">ID of Subscriber Stolen Report to fetch</param>
    ''' <returns>List of DAClsSubscriberStolenReport.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pID As Long) As Struct()
        Fetch = DoFetch(CreateSql("ID = " & pID.ToString))
    End Function

    ''' <summary>
    ''' Reads the data of all the Subscriber Stolen Reports in the table appSubscriberStolenReports
    ''' and return a list of DAClsSubscriberStolenReport.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsSubscriberStolenReport.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDSubscriber As Long, ByVal pFromDate As Date, ByVal pToDate As Date) As Struct()
        Fetch = DoFetch(CreateSql(GetParamsList(pIDSubscriber, pFromDate, pToDate)))
    End Function

    ''' <summary>
    ''' Reads the data of all the Subscriber Stolen Reports in the table appSubscriberStolenReports
    ''' and return a list of DAClsSubscriberStolenReport.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsSubscriberStolenReport.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDSubscriber() As Long, ByVal pFromDate As Date, ByVal pToDate As Date) As Struct()
        Fetch = DoFetch(CreateSql(GetParamsList(pIDSubscriber, pFromDate, pToDate)))
    End Function

    ''' <summary>
    ''' Read the data of the Subscriber Stolen Reports in the table appSubscriberStolenReports and
    ''' return a list of DAClsSubscriberStolenReport.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pIDSubscriber">Subscriber ID of Stolen Reports to fetch</param>
    ''' <returns>List of DAClsSubscriberStolenReport.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function FetchSubscriberStolenReports(ByVal pIDSubscriber As Long) As Struct()
        FetchSubscriberStolenReports = DoFetch(CreateSql("IDSubscriber = " & pIDSubscriber.ToString))
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pIDSubscriber"></param>
    ''' <param name="pFromDate"></param>
    ''' <param name="pToDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParamsList(ByVal pIDSubscriber As Long, ByVal pFromDate As Date, ByVal pToDate As Date) As String()
        Dim params As New List(Of String)

        If pIDSubscriber <> 0 Then
            params.Add("IDSubscriber = " & pIDSubscriber.ToString)
        End If

        If pFromDate.Date > New Date(1900, 1, 1).Date AndAlso pFromDate.Date <= Date.MaxValue.Date Then
            params.Add("DateReportMissing >= '" & pFromDate.Date.ToString("yyyy-M-dd") & "'")
        End If

        If pToDate.Date >= New Date(1900, 1, 1).Date AndAlso pToDate.Date < Date.MaxValue.Date Then
            params.Add("DateReportMissing <= '" & pToDate.Date.ToString("yyyy-M-dd") & "'")
        End If

        Return params.ToArray
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pIDSubscribers"></param>
    ''' <param name="pFromDate"></param>
    ''' <param name="pToDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParamsList(ByVal pIDSubscribers() As Long, ByVal pFromDate As Date, ByVal pToDate As Date) As String()()
        Dim paramsList As New List(Of String())

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
            paramsList.Add(New String() {"DateReportMissing >= '" & pFromDate.Date.ToString("yyyy-M-dd") & "'"})
        End If

        If pToDate.Date >= New Date(1900, 1, 1).Date AndAlso pToDate.Date < Date.MaxValue.Date Then
            paramsList.Add(New String() {"DateReportMissing <= '" & pToDate.Date.ToString("yyyy-M-dd") & "'"})
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
        sql &= "  IDSubscriber,"
        sql &= "  IDAsset,"
        sql &= "  DateReportMissing,"
        sql &= "  LastKnownLocationDescription,"
        sql &= "  DateReportFound,"
        sql &= "  ActiveForAlerts,"
        sql &= "  ActionToTake"
        sql &= " From"
        sql &= "  appSubscriberStolenReports"

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
        sql &= "  IDSubscriber,"
        sql &= "  IDAsset,"
        sql &= "  DateReportMissing,"
        sql &= "  LastKnownLocationDescription,"
        sql &= "  DateReportFound,"
        sql &= "  ActiveForAlerts,"
        sql &= "  ActionToTake"
        sql &= " From"
        sql &= "  appSubscriberStolenReports"

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
    ''' Execute SQL statement to reads the data of the table appSubscriberStolenReports and
    ''' return a list of DAClsSubscriberStolenReport.Struct with each one of the read registries 
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
                    Struct.IDSubscriber.SetValue(dr.GetInt64("IDSubscriber"))
                    Struct.IDAsset.SetValue(dr.GetInt64("IDAsset"))
                    Struct.DateReportMissing.SetValue(dr.GetDateTime("DateReportMissing"))
                    Struct.LastKnownLocationDescription.SetValue(dr.GetString("LastKnownLocationDescription"))
                    Struct.DateReportFound.SetValue(dr.GetDateTime("DateReportFound"))
                    Struct.ActiveForAlerts.SetValue(dr.GetBoolean("ActiveForAlerts"))
                    Struct.ActionToTake.SetValue(dr.GetInt32("ActionToTake"))
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

        pStruct.IDSubscriber.OldValue = pStruct.IDSubscriber.Value
        pStruct.IDSubscriber.Value = pStruct.IDSubscriber.NewValue

        pStruct.IDAsset.OldValue = pStruct.IDAsset.Value
        pStruct.IDAsset.Value = pStruct.IDAsset.NewValue

        pStruct.ActionToTake.OldValue = pStruct.ActionToTake.Value
        pStruct.ActionToTake.Value = pStruct.ActionToTake.NewValue

        pStruct.ActiveForAlerts.OldValue = pStruct.ActiveForAlerts.Value
        pStruct.ActiveForAlerts.Value = pStruct.ActiveForAlerts.NewValue

        pStruct.DateReportFound.OldValue = pStruct.DateReportFound.Value
        pStruct.DateReportFound.Value = pStruct.DateReportFound.NewValue

        pStruct.DateReportMissing.OldValue = pStruct.DateReportMissing.Value
        pStruct.DateReportMissing.Value = pStruct.DateReportMissing.NewValue

        pStruct.LastKnownLocationDescription.OldValue = pStruct.LastKnownLocationDescription.Value
        pStruct.LastKnownLocationDescription.Value = pStruct.LastKnownLocationDescription.NewValue

        Return pStruct

    End Function

    ''' <summary>
    ''' Insert a new registry in the table appSubscriberStolenReports and return Subscriber Stolen Report ID
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the Subscriber Stolen Report to insert</param>
    ''' <returns>Subscriber Stolen Report ID</returns>
    ''' <remarks></remarks>
    Public Shared Function Insert(ByVal pStruct As Struct) As Struct

        'Build SQL value for insert
        Dim sql As String = ""
        sql &= " Insert Into appSubscriberStolenReports"
        sql &= "  ("
        sql &= "    IDSubscriber,"
        sql &= "    IDAsset,"
        sql &= "    DateReportMissing,"
        sql &= "    LastKnownLocationDescription,"
        sql &= "    DateReportFound,"
        sql &= "    ActiveForAlerts,"
        sql &= "    ActionToTake"
        sql &= "  )"
        sql &= " Values"
        sql &= "  ("
        sql &= "     " & pStruct.IDSubscriber.NewValue.ToString & ","
        sql &= "     " & pStruct.IDAsset.NewValue.ToString & ","
        sql &= "    '" & pStruct.DateReportMissing.NewValue.ToString("yyyy-M-dd") & "',"
        sql &= "    '" & pStruct.LastKnownLocationDescription.NewValue & "',"
        sql &= "    '" & pStruct.DateReportFound.NewValue.ToString("yyyy-M-dd") & "',"
        sql &= "     " & CByte(pStruct.ActiveForAlerts.NewValue).ToString & ","
        sql &= "     " & pStruct.ActionToTake.NewValue.ToString
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
    ''' Update a registry in the table appSubscriberStolenReports and return the number of rows affected
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the Subscriber Stolen Report to update</param>
    ''' <returns>Number of rows affected</returns>
    ''' <remarks></remarks>
    Public Shared Function Update(ByVal pStruct As Struct) As Struct

        'Build SQL value
        Dim sql As String = ""
        sql &= " UPDATE appSubscriberStolenReports"
        sql &= " SET"
        sql &= "  IDSubscriber = " & pStruct.IDSubscriber.NewValue.ToString & ","
        sql &= "  IDAsset = " & pStruct.IDAsset.NewValue.ToString & ","
        sql &= "  DateReportMissing = '" & pStruct.DateReportMissing.NewValue.ToString("yyyy-M-dd") & "',"
        sql &= "  LastKnownLocationDescription = '" & pStruct.LastKnownLocationDescription.NewValue & "',"
        sql &= "  DateReportFound = '" & pStruct.DateReportFound.NewValue.ToString("yyyy-M-dd") & "',"
        sql &= "  ActiveForAlerts = " & CByte(pStruct.ActiveForAlerts.NewValue).ToString & ","
        sql &= "  ActionToTake = " & pStruct.ActionToTake.NewValue.ToString
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
    ''' Delete a registry in the table appSubscriberStolenReports and return the number of rows affected
    ''' </summary>
    ''' <param name="pID">ID of the Subscriber Stolen Reports to delete</param>
    ''' <remarks></remarks>
    Public Shared Sub Delete(ByVal pID As Long)

        'Build SQL value
        Dim sql As String = ""
        sql &= " Delete From appSubscriberStolenReports"
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