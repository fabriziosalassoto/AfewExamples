Imports Csla.data
Imports DBConnection
Imports System.Data.OleDb

''' <summary>
''' Represents a controller of data for the appAdvertiserToDo tables in the data base PCS 
''' </summary>
''' <remarks></remarks>
<Serializable()> Public Class DAClsappAdvertiserContactToDo

    ''' <summary>
    ''' Represents one structures with the data of a registry of the table appAdvertiserToDo
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> Public Class Struct
        Public TableName As String = "appAdvertiserToDo"

        Public ID As StructField(Of Long) = New StructField(Of Long)("ID", True)
        Public IDAdvertiser As StructField(Of Long) = New StructField(Of Long)("IDAdvertiser", False)
        Public IDAdvertiserContact As StructField(Of Long) = New StructField(Of Long)("IDAdvertiserContact", False)
        Public DateEntered As StructField(Of Date) = New StructField(Of Date)("DateEntered", False)
        Public TaskNotes As StructField(Of String) = New StructField(Of String)("TaskNotes", False, String.Empty)
        Public DateCompleted As StructField(Of Date) = New StructField(Of Date)("DateCompleted", False)

    End Class

    ''' <summary>
    ''' Reads the data of all the AdvertiserToDo in the table appAdvertiserToDo and return
    ''' a list of DAClsAdvertiserToDo.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsAdvertiserToDo.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch() As Struct()
        Fetch = DoFetch(CreateSql(New String() {}))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdvertiserToDo in the table appAdvertiserToDo and return
    ''' a list of DAClsAdvertiserToDo.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pID">ID of AdvertiserToDo to fetch</param>
    ''' <returns>List of DAClsAdvertiserToDo.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pID As Long) As Struct()
        Fetch = DoFetch(CreateSql("appAdvertiserToDo.ID = " & pID.ToString))
    End Function

    ''' <summary>
    ''' Reads the data of all the AdvertiserToDo in the table appAdvertiserToDo and return
    ''' a list of DAClsAdvertiserToDo.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsAdvertiserToDo.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDAdvertiser As Long, ByVal pIDContact As Long, ByVal pFromDate As Date, ByVal pToDate As Date) As Struct()
        Fetch = DoFetch(CreateSql(GetParamsList(pIDAdvertiser, pIDContact, pFromDate, pToDate)))
    End Function

    ''' <summary>
    ''' Reads the data of all the AdvertiserToDo in the table appAdvertiserToDo and return
    ''' a list of DAClsAdvertiserToDo.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsAdvertiserToDo.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDAdvertisers() As Long, ByVal pIDContacts() As Long, ByVal pFromDate As Date, ByVal pToDate As Date) As Struct()
        Fetch = DoFetch(CreateSql(GetParamsList(pIDAdvertisers, pIDContacts, pFromDate, pToDate)))
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pIDContact"></param>
    ''' <param name="pFromDate"></param>
    ''' <param name="pToDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParamsList(ByVal pIDAdvertiser As Long, ByVal pIDContact As Long, ByVal pFromDate As Date, ByVal pToDate As Date) As String()
        Dim params As New List(Of String)

        If pIDAdvertiser <> 0 Then
            params.Add("IDAdvertiser = " & pIDAdvertiser.ToString)
        End If

        If pIDContact <> 0 Then
            params.Add("IDAdvertiserContact = " & pIDContact.ToString)
        End If

        If pFromDate.Date > New Date(1900, 1, 1).Date AndAlso pFromDate.Date <= Date.MaxValue.Date Then
            params.Add("DateEntered >= '" & pFromDate.Date.ToString("yyyy-M-dd") & "'")
        End If

        If pToDate.Date >= New Date(1900, 1, 1).Date AndAlso pToDate.Date < Date.MaxValue.Date Then
            params.Add("DateEntered <= '" & pToDate.Date.ToString("yyyy-M-dd") & "'")
        End If

        Return params.ToArray
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pIDContacts"></param>
    ''' <param name="pFromDate"></param>
    ''' <param name="pToDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParamsList(ByVal pIDAdvertisers() As Long, ByVal pIDContacts() As Long, ByVal pFromDate As Date, ByVal pToDate As Date) As String()()
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

        If pFromDate.Date > New Date(1900, 1, 1).Date AndAlso pFromDate.Date <= Date.MaxValue.Date Then
            paramsList.Add(New String() {"DateEntered >= '" & pFromDate.Date.ToString("yyyy-M-dd") & "'"})
        End If

        If pToDate.Date >= New Date(1900, 1, 1).Date AndAlso pToDate.Date < Date.MaxValue.Date Then
            paramsList.Add(New String() {"DateEntered <= '" & pToDate.Date.ToString("yyyy-M-dd") & "'"})
        End If

        Return paramsList.ToArray
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
        sql &= "  appAdvertiserToDo.ID,"
        sql &= "  IDAdvertiser,"
        sql &= "  IDAdvertiserContact,"
        sql &= "  DateEntered,"
        sql &= "  DateCompleted,"
        sql &= "  TaskNotes"
        sql &= " From"
        sql &= "  appAdvertiserToDo"
        sql &= " Inner Join"
        sql &= "  appAdvertiserContactInfo"
        sql &= " On"
        sql &= "  IDAdvertiserContact = appAdvertiserContactInfo.ID"

        If pParametersList.Length > 0 Then
            sql &= " Where " & Join(pParametersList, " And ")
        End If

        CreateSql = sql
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
        sql &= "  appAdvertiserToDo.ID,"
        sql &= "  IDAdvertiser,"
        sql &= "  IDAdvertiserContact,"
        sql &= "  DateEntered,"
        sql &= "  DateCompleted,"
        sql &= "  TaskNotes"
        sql &= " From"
        sql &= "  appAdvertiserToDo"
        sql &= " Inner Join"
        sql &= "  appAdvertiserContactInfo"
        sql &= " On"
        sql &= "  IDAdvertiserContact = appAdvertiserContactInfo.ID"

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
    ''' Execute SQL statement to reads the data of the table appAdvertiserProjectInvoices and
    ''' return a list of DAClsAdvertiserProjectInvoices.Struct with each one of the read registries 
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
                    Struct.DateEntered.SetValue(dr.GetDateTime("DateEntered"))
                    Struct.DateCompleted.SetValue(dr.GetDateTime("DateCompleted"))
                    Struct.TaskNotes.SetValue(dr.GetString("TaskNotes"))
                    List.Add(Struct)
                End While
                DoFetch = List.ToArray
            End Using
        End Using
    End Function

End Class
