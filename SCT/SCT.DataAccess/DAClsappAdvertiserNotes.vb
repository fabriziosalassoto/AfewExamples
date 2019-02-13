Imports Csla.Data
Imports DBConnection
Imports System.Data.OleDb

''' <summary>
''' Represents a controller of data for the appAdvertiserNotes table in the data base ServerComputerTracking
''' </summary>
''' <remarks></remarks>
<Serializable()> Public Class DAClsappAdvertiserNotes

    ''' <summary>
    ''' Represents one structures with the data of a registry of the table appAdvertiserNotes
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> Public Class Struct
        Public TableName As String = "appAdvertiserNotes"

        Public ID As StructField(Of Long) = New StructField(Of Long)("ID", True)
        Public IDAdvertiserContact As StructField(Of Long) = New StructField(Of Long)("IDAdvertiserContact", False)
        Public DateEntered As StructField(Of Date) = New StructField(Of Date)("DateEntered", False)
        Public DescriptionOfNotes As StructField(Of String) = New StructField(Of String)("DescriptionOfNotes", False, String.Empty)

    End Class

    ''' <summary>
    ''' Reads the data of all the Advertiser Notes in the table appAdvertiserNotes and return
    ''' a list of DAClsAdvertiserNotes.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsAdvertiserNotes.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch() As Struct()
        Fetch = DoFetch(CreateSql(New String() {}))
    End Function

    ''' <summary>
    ''' Read the data of a certain Advertiser Notes in the table appAdvertiserNotes and return
    ''' a list of DAClsAdvertiserNotes.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pID">ID of Advertiser Notes to fetch</param>
    ''' <returns>List of DAClsAdvertiserNotes.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pID As Long) As Struct()
        Fetch = DoFetch(CreateSql("ID = " & pID.ToString))
    End Function

    ''' <summary>
    ''' Reads the data of all the AdvertiserToDo in the table appAdvertiserToDo and return
    ''' a list of DAClsAdvertiserToDo.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsAdvertiserToDo.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDContact As Long, ByVal pFromDate As Date, ByVal pToDate As Date) As Struct()
        Fetch = DoFetch(CreateSql(GetParamsList(pIDContact, pFromDate, pToDate)))
    End Function

    ''' <summary>
    ''' Reads the data of all the AdvertiserToDo in the table appAdvertiserToDo and return
    ''' a list of DAClsAdvertiserToDo.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsAdvertiserToDo.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDContacts() As Long, ByVal pFromDate As Date, ByVal pToDate As Date) As Struct()
        Fetch = DoFetch(CreateSql(GetParamsList(pIDContacts, pFromDate, pToDate)))
    End Function

    ''' <summary>
    ''' Read the data of the Contact Advertiser Notes in the table appAdvertiserNotes and
    ''' return a list of DAClsAdvertiserNote.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pIDContact">Contact ID of Advertiser Notes to fetch</param>
    ''' <returns>List of DAClsAdvertiserNotes.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function FetchContactNotes(ByVal pIDContact As Long) As Struct()
        FetchContactNotes = DoFetch(CreateSql("IDAdvertiserContact = " & pIDContact.ToString))
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pIDContact"></param>
    ''' <param name="pFromDate"></param>
    ''' <param name="pToDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParamsList(ByVal pIDContact As Long, ByVal pFromDate As Date, ByVal pToDate As Date) As String()
        Dim params As New List(Of String)

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
    Private Shared Function GetParamsList(ByVal pIDContacts() As Long, ByVal pFromDate As Date, ByVal pToDate As Date) As String()()
        Dim paramsList As New List(Of String())

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
    '''<param name="pParams"></param> 
    ''' <returns>Sql Statement</returns>
    ''' <remarks></remarks>
    Private Shared Function CreateSql(ByVal ParamArray pParams() As String) As String

        'Build SQL value
        Dim sql As String = ""
        sql &= " Select"
        sql &= "  ID,"
        sql &= "  IDAdvertiserContact,"
        sql &= "  DateEntered,"
        sql &= "  DescriptionOfNotes"
        sql &= " From"
        sql &= "  appAdvertiserNotes"

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
        sql &= "  IDAdvertiserContact,"
        sql &= "  DateEntered,"
        sql &= "  DescriptionOfNotes"
        sql &= " From"
        sql &= "  appAdvertiserNotes"


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
    ''' Execute SQL statement to reads the data of the table appAdvertiserNotes and
    ''' return a list of DAClsAdvertiserNotes.Struct with each one of the read registries 
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
                    Struct.IDAdvertiserContact.SetValue(dr.GetInt64("IDAdvertiserContact"))
                    Struct.DateEntered.SetValue(dr.GetDateTime("DateEntered"))
                    Struct.DescriptionOfNotes.SetValue(dr.GetString("DescriptionOfNotes"))
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

        pStruct.IDAdvertiserContact.OldValue = pStruct.IDAdvertiserContact.Value
        pStruct.IDAdvertiserContact.Value = pStruct.IDAdvertiserContact.NewValue

        pStruct.DateEntered.OldValue = pStruct.DateEntered.Value
        pStruct.DateEntered.Value = pStruct.DateEntered.NewValue

        pStruct.DescriptionOfNotes.OldValue = pStruct.DescriptionOfNotes.Value
        pStruct.DescriptionOfNotes.Value = pStruct.DescriptionOfNotes.NewValue

        Return pStruct
    End Function

    ''' <summary>
    ''' Insert a new registry in the table appAdvertiserNotes and return Advertiser Notes ID
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the Advertiser Notes to insert</param>
    ''' <returns>Advertiser Notes ID</returns>
    ''' <remarks></remarks>
    Public Shared Function Insert(ByVal pStruct As Struct) As Struct

        'Build SQL value for insert
        Dim sql As String = ""
        sql &= " Insert Into appAdvertiserNotes"
        sql &= "  ("
        sql &= "    IDAdvertiserContact,"
        sql &= "    DateEntered,"
        sql &= "    DescriptionOfNotes"
        sql &= "  )"
        sql &= " Values"
        sql &= "  ("
        sql &= "     " & pStruct.IDAdvertiserContact.NewValue.ToString & ","
        sql &= "    '" & pStruct.DateEntered.NewValue.ToString("yyyy-M-dd") & "',"
        sql &= "    '" & pStruct.DescriptionOfNotes.NewValue & "'"
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
    ''' Update a registry in the table appAdvertiserNotes and return the number of rows affected
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the Advertiser Notes to update</param>
    ''' <returns>Number of rows affected</returns>
    ''' <remarks></remarks>
    Public Shared Function Update(ByVal pStruct As Struct) As Struct

        'Build SQL value
        Dim sql As String = ""
        sql &= " UPDATE appAdvertiserNotes"
        sql &= " SET"
        sql &= "  IDAdvertiserContact = " & pStruct.IDAdvertiserContact.NewValue.ToString & ","
        sql &= "  DateEntered = '" & pStruct.DateEntered.NewValue.ToString("yyyy-M-dd") & "',"
        sql &= "  DescriptionOfNotes = '" & pStruct.DescriptionOfNotes.NewValue & "'"
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
    ''' Delete a registry in the table appAdvertiserNotes  and return the number of rows affected
    ''' </summary>
    ''' <param name="pID">ID of the Advertiser Notes to delete</param>
    ''' <remarks></remarks>
    Public Shared Sub Delete(ByVal pID As Long)

        'Build SQL value
        Dim sql As String = ""
        sql &= " Delete From appAdvertiserNotes"
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