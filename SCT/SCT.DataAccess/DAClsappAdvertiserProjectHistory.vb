Imports Csla.Data
Imports DBConnection
Imports System.Data.OleDb

''' <summary>
''' Represents a controller of data for the appAdvertiserAdHistory table in the data base ServerComputerTracking
''' </summary>
''' <remarks></remarks>
<Serializable()> Public Class DAClsappAdvertiserProjectHistory

    ''' <summary>
    ''' Represents one structures with the data of a registry of the table appAdvertiserAdHistory
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> Public Class Struct
        Public TableName As String = "appAdvertiserAdHistory"

        Public ID As StructField(Of Long) = New StructField(Of Long)("ID", True)
        Public IDAdvertiser As StructField(Of Long) = New StructField(Of Long)("IDAdvertiser", False)
        Public IDAdvertiserContact As StructField(Of Long) = New StructField(Of Long)("IDAdvertiserContact", False)
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
    Public Shared Function Fetch(ByVal pIDAdvertiser As Long, ByVal pIDContact As Long, ByVal pIDProject As Long, ByVal pIDSubscriber As Long, ByVal pFromDate As Date, ByVal pToDate As Date, ByVal pFromTime As Date, ByVal pToTime As Date) As Struct()
        Fetch = DoFetch(CreateSql(GetParamsList(pIDAdvertiser, pIDContact, pIDProject, pIDSubscriber, pFromDate, pToDate, pFromTime, pToTime)))
    End Function

    ''' <summary>
    ''' Read the data of a certain Advertiser AdHistory in the table appAdvertiserAdHistory and
    ''' return a list of DAClsAdvertiserAdHistory.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdvertiserAdHistory.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDAdvertisers() As Long, ByVal pIDContacts() As Long, ByVal pIDProjects() As Long, ByVal pIDSubscriber() As Long, ByVal pFromDate As Date, ByVal pToDate As Date, ByVal pFromTime As Date, ByVal pToTime As Date) As Struct()
        Fetch = DoFetch(CreateSql(GetParamsList(pIDAdvertisers, pIDContacts, pIDProjects, pIDSubscriber, pFromDate, pToDate, pFromTime, pToTime)))
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pIDProject"></param>
    ''' <param name="pIDSubscriber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParamsList(ByVal pIDAdvertiser As Long, ByVal pIDContact As Long, ByVal pIDProject As Long, ByVal pIDSubscriber As Long, ByVal pFromDate As Date, ByVal pToDate As Date, ByVal pFromTime As Date, ByVal pToTime As Date) As String()
        Dim params As New List(Of String)

        If pIDAdvertiser <> 0 Then
            params.Add("IDAdvertiser = " & pIDAdvertiser.ToString)
        End If

        If pIDContact <> 0 Then
            params.Add("IDAdvertiserContact = " & pIDContact.ToString)
        End If

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
    Private Shared Function GetParamsList(ByVal pIDAdvertisers() As Long, ByVal pIDContacts() As Long, ByVal pIDProjects() As Long, ByVal pIDSubscribers() As Long, ByVal pFromDate As Date, ByVal pToDate As Date, ByVal pFromTime As Date, ByVal pToTime As Date) As String()()
        Dim paramsList As New List(Of String())

        If pIDAdvertisers.Length > 0 Then
            Dim paramList As New List(Of String)
            For Each idAdvertiser As Long In pIDAdvertisers
                If idAdvertiser <> 0 Then
                    paramList.Add("IDAdvertiser = " & idAdvertiser.ToString)
                End If
            Next
            paramsList.Add(paramList.ToArray)
        End If

        If pIDContacts.Length > 0 Then
            Dim paramList As New List(Of String)
            For Each idContact As Long In pIDContacts
                If idContact <> 0 Then
                    paramList.Add("IDAdvertiserContact = " & idContact.ToString)
                End If
            Next
            paramsList.Add(paramList.ToArray)
        End If

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
        sql &= "  appAdvertiserAdHistory.ID,"
        sql &= "  IDAdvertiser,"
        sql &= "  IDAdvertiserContact,"
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
        sql &= " Inner Join"
        sql &= "  appAdvertiserProjects"
        sql &= " On"
        sql &= "  IDProject = appAdvertiserProjects.ID"

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
        sql &= "  appAdvertiserAdHistory.ID,"
        sql &= "  IDAdvertiser,"
        sql &= "  IDAdvertiserContact,"
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
        sql &= " Inner Join"
        sql &= "  appAdvertiserProjects"
        sql &= " On"
        sql &= "  IDProject = appAdvertiserProjects.ID"

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
                    Struct.IDAdvertiser.SetValue(dr.GetInt64("IDAdvertiser"))
                    Struct.IDAdvertiserContact.SetValue(dr.GetInt64("IDAdvertiserContact"))
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

End Class
