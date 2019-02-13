Imports Csla.Data
Imports DBConnection
Imports System.Data.OleDb

''' <summary>
''' Represents a controller of data for the appSubscribersDemographics table in the data base ServerComputerTracking
''' </summary>
''' <remarks></remarks>
<Serializable()> Public Class DAClsappSubscribersDemographics

    ''' <summary>
    ''' Represents one structures with the data of a registry of the table appSubscribersDemographics
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> Public Class Struct
        Public TableName As String = "appSubscribersDemographics"

        Public ID As StructField(Of Long) = New StructField(Of Long)("ID", True)
        Public IDSubscriber As StructField(Of Long) = New StructField(Of Long)("IDSubscriber", False)
        Public DemographicTag As StructField(Of String) = New StructField(Of String)("DemographicTag", False, String.Empty)
        Public DemographicAnswer As StructField(Of String) = New StructField(Of String)("DemographicAnswer", False, String.Empty)

    End Class

    ''' <summary>
    ''' Reads the data of all the Subscribers Demographics in the table appSubscribersDemographics and return
    ''' a list of DAClsSubscriberDemographic.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of  DAClsSubscribersDemographic.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch() As Struct()
        Fetch = DoFetch(CreateSql(New String() {}))
    End Function

    ''' <summary>
    ''' Read the data of a certain Subscriber Demographic in the table appSubscribersDemographics and return
    ''' a list of DAClsappSubscriberDemographic.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pID">ID of Subscriber Demographic to fetch</param>
    ''' <returns>List of  DAClsSubscribersDemographic.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pID As Long) As Struct()
        Fetch = DoFetch(CreateSql("ID = " & pID.ToString))
    End Function

    ''' <summary>
    ''' Reads the data of all the Subscribers Demographics in the table appSubscribersDemographics and return
    ''' a list of DAClsSubscriberDemographic.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of  DAClsSubscribersDemographic.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDSubscriber As Long, ByVal pDemographicTag As String) As Struct()
        Fetch = DoFetch(CreateSql(GetParamsList(pIDSubscriber, pDemographicTag)))
    End Function

    ''' <summary>
    ''' Reads the data of all the Subscribers Demographics in the table appSubscribersDemographics and return
    ''' a list of DAClsSubscriberDemographic.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of  DAClsSubscribersDemographic.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDSubscribers() As Long, ByVal pDemographicTags() As String) As Struct()
        Fetch = DoFetch(CreateSql(GetParamsList(pIDSubscribers, pDemographicTags)))
    End Function

    ''' <summary>
    ''' Read the data of the Subscriber demographic info in the table appSubscribersDemographics and
    ''' return a list of DAClsSubscriberDemographics.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pIDSubscriber">Subcriber ID of demographic info to fetch</param>
    ''' <returns>List of DAClsSubscribersDemographic.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function FetchSubscriberDemographics(ByVal pIDSubscriber As Long) As Struct()
        FetchSubscriberDemographics = DoFetch(CreateSql("IDSubscriber = " & pIDSubscriber.ToString))
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pIDSubscriber"></param>
    ''' <param name="pDemographicTag"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParamsList(ByVal pIDSubscriber As Long, ByVal pDemographicTag As String) As String()
        Dim params As New List(Of String)

        If pIDSubscriber <> 0 Then
            params.Add("IDSubscriber = " & pIDSubscriber.ToString)
        End If

        If Len(pDemographicTag) > 0 Then
            params.Add("DemographicTag = " & pDemographicTag)
        End If

        Return params.ToArray
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pIDSubscribers"></param>
    ''' <param name="pDemographicTags"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParamsList(ByVal pIDSubscribers() As Long, ByVal pDemographicTags() As String) As String()()
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

        If pDemographicTags.Length > 0 Then
            Dim paramList As New List(Of String)
            For Each DemographicTag As String In pDemographicTags
                If Len(DemographicTag) > 0 Then
                    paramList.Add("DemographicTag = " & DemographicTag)
                End If
            Next
            paramsList.Add(paramList.ToArray)
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
        sql &= "  DemographicTag,"
        sql &= "  DemographicAnswer"
        sql &= " From"
        sql &= "  appSubscribersDemographics"

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
        sql &= "  DemographicTag,"
        sql &= "  DemographicAnswer"
        sql &= " From"
        sql &= "  appSubscribersDemographics"

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
    ''' Execute SQL statement to reads the data of the table appSubscribersDemographics and
    ''' return a list of DAClsSubscriberDemographic.Struct with each one of the read registries 
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
                    Struct.DemographicTag.SetValue(dr.GetString("DemographicTag"))
                    Struct.DemographicAnswer.SetValue(dr.GetString("DemographicAnswer"))
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

        pStruct.DemographicTag.OldValue = pStruct.DemographicTag.Value
        pStruct.DemographicTag.Value = pStruct.DemographicTag.NewValue

        pStruct.DemographicAnswer.OldValue = pStruct.DemographicAnswer.Value
        pStruct.DemographicAnswer.Value = pStruct.DemographicAnswer.NewValue

        Return pStruct

    End Function

    ''' <summary>
    ''' Insert a new registry in the table appSubscribersDemographics and return Subscriber Demographic ID
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the Subscriber Demographic to insert</param>
    ''' <returns>Subscriber Demographic ID</returns>
    ''' <remarks></remarks>
    Public Shared Function Insert(ByVal pStruct As Struct) As Struct

        'Build SQL value for insert
        Dim sql As String = ""
        sql &= " Insert Into appSubscribersDemographics"
        sql &= "  ("
        sql &= "    IDSubscriber,"
        sql &= "    DemographicTag,"
        sql &= "    DemographicAnswer"
        sql &= "  )"
        sql &= " Values"
        sql &= "  ("
        sql &= "     " & pStruct.IDSubscriber.NewValue.ToString & ","
        sql &= "    '" & pStruct.DemographicTag.NewValue & "',"
        sql &= "    '" & pStruct.DemographicAnswer.NewValue & "'"
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
    ''' Update a registry in the table appSubscribersDemographics and return the number of rows affected
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the Subscriber Demographic to update</param>
    ''' <returns>Number of rows affected</returns>
    ''' <remarks></remarks>
    Public Shared Function Update(ByVal pStruct As Struct) As Struct

        'Build SQL value
        Dim sql As String = ""
        sql &= " UPDATE appSubscribersDemographics"
        sql &= " SET"
        sql &= "  IDSubscriber = " & pStruct.IDSubscriber.NewValue.ToString & ","
        sql &= "  DemographicTag = '" & pStruct.DemographicTag.NewValue & "',"
        sql &= "  DemographicAnswer = '" & pStruct.DemographicAnswer.NewValue & "'"
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
    ''' Delete a registry in the table appSubscribersDemographics and return the number of rows affected
    ''' </summary>
    ''' <param name="pID">ID of the Subscriber Demographic</param>
    ''' <remarks></remarks>
    Public Shared Sub Delete(ByVal pID As Long)

        'Build SQL value
        Dim sql As String = ""
        sql &= " Delete From appSubscribersDemographics"
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