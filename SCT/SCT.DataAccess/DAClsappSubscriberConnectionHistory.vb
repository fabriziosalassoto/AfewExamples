Imports Csla.Data
Imports DBConnection
Imports System.Data.OleDb

''' <summary>
''' Represents a controller of data for the appSubscriberConnectionHistory table in the data base ServerComputerTracking
''' </summary>
''' <remarks></remarks>
<Serializable()> Public Class DAClsappSubscriberConnectionHistory

    ''' <summary>
    ''' Represents one structures with the data of a registry of the table appSubscribersDemographics
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> Public Class Struct
        Public TableName As String = "appSubscribersAccounts"

        Public ID As StructField(Of Long) = New StructField(Of Long)("ID", True)
        Public IDSubscriber As StructField(Of Long) = New StructField(Of Long)("IDSubscriber", False)
        Public HostIP As StructField(Of String) = New StructField(Of String)("HostIP", False, String.Empty)
        Public HostLocalIP As StructField(Of String) = New StructField(Of String)("HostLocalIP", False, String.Empty)
        Public ConnectionDate As StructField(Of Date) = New StructField(Of Date)("ConnectionDate", False)
        Public ConnectionTime As StructField(Of Date) = New StructField(Of Date)("ConnectionTime", False)
        Public DNSResolutionIP As StructField(Of String) = New StructField(Of String)("DNSResolutionIP", False, String.Empty)
        Public IPState As StructField(Of String) = New StructField(Of String)("IPState", False, String.Empty)
        Public IPCity As StructField(Of String) = New StructField(Of String)("IPCity", False, String.Empty)
        Public ActivityStatus As StructField(Of String) = New StructField(Of String)("ActivityStatus", False, String.Empty)
    End Class

    ''' <summary>
    ''' Reads the data of all the Subscriber Connection History in the table appSubscriberConnectionHistory
    ''' and return a list of DAClsSubscriberConnectionHistory.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsSubscriberConnectionHistory.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch() As Struct()
        Fetch = DoFetch(CreateSql(New String() {}))
    End Function

    ''' <summary>
    ''' Read the data of a certain Subscriber Connection History in the table appSubscriberConnectionHistory
    ''' and return a list of DAClsSubscriberConnectionHistory.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pID">ID of Subscriber Connection History to fetch</param>
    ''' <returns>List of DAClsSubscriberConnectionHistory.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pID As Long) As Struct()
        Fetch = DoFetch(CreateSql("ID = " & pID.ToString))
    End Function

    ''' <summary>
    ''' Reads the data of all the Subscriber Connection History in the table appSubscriberConnectionHistory
    ''' and return a list of DAClsSubscriberConnectionHistory.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsSubscriberConnectionHistory.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDSubscriber As Long, ByVal pFromDate As Date, ByVal pToDate As Date) As Struct()
        Fetch = DoFetch(CreateSql(GetParamsList(pIDSubscriber, pFromDate, pToDate)))
    End Function

    ''' <summary>
    ''' Reads the data of all the Subscriber Connection History in the table appSubscriberConnectionHistory
    ''' and return a list of DAClsSubscriberConnectionHistory.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsSubscriberConnectionHistory.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDSubscribers() As Long, ByVal pFromDate As Date, ByVal pToDate As Date) As Struct()
        Fetch = DoFetch(CreateSql(GetParamsList(pIDSubscribers, pFromDate, pToDate)))
    End Function

    ''' <summary>
    ''' Read the data of the Subscriber Connections History in the table appSubscriberConnectionHistory and
    ''' return a list of DAClsSubscriberConnectionHistory.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pIDSubscriber">Subscriber ID of connection history to fetch</param>
    ''' <returns>List of DAClsSubscriberConnectionHistory.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function FetchSubscriberConnectionHistory(ByVal pIDSubscriber As Long) As Struct()
        FetchSubscriberConnectionHistory = DoFetch(CreateSql("IDSubscriber = " & pIDSubscriber.ToString))
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pIDSubscriber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParamsList(ByVal pIDSubscriber As Long, ByVal pFromDate As Date, ByVal pToDate As Date) As String()
        Dim params As New List(Of String)

        If pIDSubscriber <> 0 Then
            params.Add("IDSubscriber = " & pIDSubscriber.ToString)
        End If

        If pFromDate.Date > New Date(1900, 1, 1).Date AndAlso pFromDate.Date <= Date.MaxValue.Date Then
            params.Add("ConnectionDate >= '" & pFromDate.Date.ToString("yyyy-M-dd") & "'")
        End If

        If pToDate.Date >= New Date(1900, 1, 1).Date AndAlso pToDate.Date < Date.MaxValue.Date Then
            params.Add("ConnectionDate <= '" & pToDate.Date.ToString("yyyy-M-dd") & "'")
        End If

        Return params.ToArray
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pIDSubscribers"></param>
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
            paramsList.Add(New String() {"ConnectionDate >= '" & pFromDate.Date.ToString("yyyy-M-dd") & "'"})
        End If

        If pToDate.Date >= New Date(1900, 1, 1).Date AndAlso pToDate.Date < Date.MaxValue.Date Then
            paramsList.Add(New String() {"ConnectionDate <= '" & pToDate.Date.ToString("yyyy-M-dd") & "'"})
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
        sql &= "  IDSubscriber,"
        sql &= "  HostIP,"
        sql &= "  HostLocalIP,"
        sql &= "  ConnectionDate,"
        sql &= "  ConnectionTime,"
        sql &= "  DNSResolutionIP,"
        sql &= "  IPState,"
        sql &= "  IPCity,"
        sql &= "  ActivityStatus"
        sql &= " From"
        sql &= "  appSubscriberConnectionHistory"

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
        sql &= "  HostIP,"
        sql &= "  HostLocalIP,"
        sql &= "  ConnectionDate,"
        sql &= "  ConnectionTime,"
        sql &= "  DNSResolutionIP,"
        sql &= "  IPState,"
        sql &= "  IPCity,"
        sql &= "  ActivityStatus"
        sql &= " From"
        sql &= "  appSubscriberConnectionHistory"


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
    ''' Execute SQL statement to reads the data of the table appSubscriberConnectionHistory and
    ''' return a list of DAClsSubscriberConnectionHistory.Struct with each one of the read registries 
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
                    Struct.HostIP.SetValue(dr.GetString("HostIP"))
                    Struct.HostLocalIP.SetValue(dr.GetString("HostLocalIP"))
                    Struct.ConnectionDate.SetValue(dr.GetDateTime("ConnectionDate"))
                    Struct.ConnectionTime.SetValue(dr.GetDateTime("ConnectionTime"))
                    Struct.DNSResolutionIP.SetValue(dr.GetString("DNSResolutionIP"))
                    Struct.IPState.SetValue(dr.GetString("IPState"))
                    Struct.IPCity.SetValue(dr.GetString("IPCity"))
                    Struct.ActivityStatus.SetValue(dr.GetString("ActivityStatus"))
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

        pStruct.HostIP.OldValue = pStruct.HostIP.Value
        pStruct.HostIP.Value = pStruct.HostIP.NewValue

        pStruct.HostLocalIP.OldValue = pStruct.HostLocalIP.Value
        pStruct.HostLocalIP.Value = pStruct.HostLocalIP.NewValue

        pStruct.ConnectionDate.OldValue = pStruct.ConnectionDate.Value
        pStruct.ConnectionDate.Value = pStruct.ConnectionDate.NewValue

        pStruct.ConnectionTime.OldValue = pStruct.ConnectionTime.Value
        pStruct.ConnectionTime.Value = pStruct.ConnectionTime.NewValue

        pStruct.DNSResolutionIP.OldValue = pStruct.DNSResolutionIP.Value
        pStruct.DNSResolutionIP.Value = pStruct.DNSResolutionIP.NewValue

        pStruct.IPCity.OldValue = pStruct.IPCity.Value
        pStruct.IPCity.Value = pStruct.IPCity.NewValue

        pStruct.IPState.OldValue = pStruct.IPState.Value
        pStruct.IPState.Value = pStruct.IPState.NewValue

        pStruct.ActivityStatus.OldValue = pStruct.ActivityStatus.Value
        pStruct.ActivityStatus.Value = pStruct.ActivityStatus.NewValue

        Return pStruct
    End Function

    ''' <summary>
    ''' Insert a new registry in the table appSubscriberConnectionHistory and return Subscriber Connection History ID
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the Subscriber Connection History to insert</param>
    ''' <returns>Subscriber Connection History ID</returns>
    ''' <remarks></remarks>
    Public Shared Function Insert(ByVal pStruct As Struct) As Struct

        'Build SQL value for insert
        Dim sql As String = ""
        sql &= " Insert Into appSubscriberConnectionHistory"
        sql &= "  ("
        sql &= "    IDSubscriber,"
        sql &= "    HostIP,"
        sql &= "    HostLocalIP,"
        sql &= "    ConnectionDate,"
        sql &= "    ConnectionTime,"
        sql &= "    DNSResolutionIP,"
        sql &= "    IPState,"
        sql &= "    IPCity,"
        sql &= "    ActivityStatus"
        sql &= "  )"
        sql &= " Values"
        sql &= "  ("
        sql &= "     " & pStruct.IDSubscriber.NewValue.ToString & ","
        sql &= "    '" & pStruct.HostIP.NewValue & "',"
        sql &= "    '" & pStruct.HostLocalIP.NewValue & "',"
        sql &= "    '" & pStruct.ConnectionDate.NewValue.ToString("yyyy-M-dd") & "',"
        sql &= "    '" & pStruct.ConnectionTime.NewValue.ToString("HH:mm") & "',"
        sql &= "    '" & pStruct.DNSResolutionIP.NewValue & "',"
        sql &= "    '" & pStruct.IPState.NewValue & "',"
        sql &= "    '" & pStruct.IPCity.NewValue & "',"
        sql &= "    '" & pStruct.ActivityStatus.NewValue & "'"
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
    ''' Update a registry in the table appSubscriberConnectionHistory and return the number of rows affected
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the Subscriber Connection History to update</param>
    ''' <returns>Number of rows affected</returns>
    ''' <remarks></remarks>
    Public Shared Function Update(ByVal pStruct As Struct) As Struct

        'Build SQL value
        Dim sql As String = ""
        sql &= " UPDATE appSubscriberConnectionHistory"
        sql &= " SET"
        sql &= "  IDSubscriber = " & pStruct.IDSubscriber.NewValue.ToString & ","
        sql &= "  HostIP = '" & pStruct.HostIP.NewValue & "',"
        sql &= "  HostLocalIP = '" & pStruct.HostLocalIP.NewValue & "',"
        sql &= "  ConnectionDate = '" & pStruct.ConnectionDate.NewValue.ToString("yyyy-M-dd") & "',"
        sql &= "  ConnectionTime = '" & pStruct.ConnectionTime.NewValue.ToString("HH:mm") & "',"
        sql &= "  DNSResolutionIP = '" & pStruct.DNSResolutionIP.NewValue & "',"
        sql &= "  IPState = '" & pStruct.IPState.NewValue & "',"
        sql &= "  IPCity = '" & pStruct.IPCity.NewValue & "',"
        sql &= "  ActivityStatus = '" & pStruct.ActivityStatus.NewValue & "'"
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
    ''' Delete a registry in the table appSubscriberConnectionHistory and return the number of rows affected
    ''' </summary>
    ''' <param name="pID">ID of the Subscriber Connection History to delete</param>
    ''' <remarks></remarks>
    Public Shared Sub Delete(ByVal pID As Long)

        'Build SQL value
        Dim sql As String = ""
        sql &= " Delete From appSubscriberConnectionHistory"
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