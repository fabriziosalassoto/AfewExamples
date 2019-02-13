Imports Csla.Data
Imports DBConnection
Imports System.Data.OleDb

''' <summary>
''' Represents a controller of data for the appAdvertiserContactInfo table in the data base ServerComputerTracking
''' </summary>
''' <remarks></remarks>
<Serializable()> Public Class DAClsappAdvertiserContactInfo

    ''' <summary>
    ''' Represents one structures with the data of a registry of the table appAdvertiserContactInfo 
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> Public Class Struct
        Public TableName As String = "appAdvertiserContactInfo"

        Public ID As StructField(Of Long) = New StructField(Of Long)("ID", True)
        Public IDAdvertiser As StructField(Of Long) = New StructField(Of Long)("IDAdvertiser", False)
        Public FirstName As StructField(Of String) = New StructField(Of String)("FirstName", False, String.Empty)
        Public LastName As StructField(Of String) = New StructField(Of String)("LastName", False, String.Empty)
        Public MainCompanyAddress As StructField(Of Boolean) = New StructField(Of Boolean)("MainCompanyAddress", False)
        Public PrimaryAddress As StructField(Of String) = New StructField(Of String)("PrimaryAddress", False, String.Empty)
        Public SecondaryAddress As StructField(Of String) = New StructField(Of String)("SecondaryAddress", False, String.Empty)
        Public City As StructField(Of String) = New StructField(Of String)("City", False, String.Empty)
        Public State As StructField(Of String) = New StructField(Of String)("State", False, String.Empty)
        Public Country As StructField(Of String) = New StructField(Of String)("Country", False, String.Empty)
        Public ZipCode As StructField(Of String) = New StructField(Of String)("ZipCode", False, String.Empty)
        Public Providence As StructField(Of String) = New StructField(Of String)("Providence", False, String.Empty)
        Public Department As StructField(Of String) = New StructField(Of String)("Department", False, String.Empty)
        Public ResposibleForNotes As StructField(Of String) = New StructField(Of String)("ResposibleForNotes", False, String.Empty)

    End Class

    ''' <summary>
    ''' Reads the data of all the Advertiser Contact Info in the table appAdvertiserContactInfo and return
    ''' a list of DAClsAdvertiserContactInfo.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsAdvertiserContactInfo.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch() As Struct()
        Fetch = DoFetch(CreateSql(New String() {}))
    End Function

    ''' <summary>
    ''' Read the data of a certain Advertiser Contact Info in the table appAdvertiserContactInfo and return
    ''' a list of DAClsAdvertiserContactInfo.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pID">ID of Advertiser Contact Info to fetch</param>
    ''' <returns>List of DAClsAdvertiserContactInfo.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pID As Long) As Struct()
        Fetch = DoFetch(CreateSql("ID = " & pID.ToString))
    End Function

    ''' <summary>
    ''' Reads the data of all the Advertiser Contact Info in the table appAdvertiserContactInfo and return
    ''' a list of DAClsAdvertiserContactInfo.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsAdvertiserContactInfo.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDAdvertiser As Long, ByVal pMainCompanyAddress As Boolean) As Struct()
        Fetch = DoFetch(CreateSql(GetParamsList(pIDAdvertiser, pMainCompanyAddress)))
    End Function

    ''' <summary>
    ''' Reads the data of all the Advertiser Contact Info in the table appAdvertiserContactInfo and return
    ''' a list of DAClsAdvertiserContactInfo.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsAdvertiserContactInfo.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDAdvertisers() As Long, ByVal pMainCompanyAddress As Boolean) As Struct()
        Fetch = DoFetch(CreateSql(GetParamsList(pIDAdvertisers, pMainCompanyAddress)))
    End Function

    ''' <summary>
    ''' Read the data of the Advertiser Contacts info in the table appAdvertiserContactInfo and
    ''' return a list of DAClsAdvertiserContactInfo.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pIDAdvertiser">Advertiser ID of Contact info to fetch</param>
    ''' <returns>List of DAClsAdvertiserContactInfo.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function FetchAdvertiserContactInfo(ByVal pIDAdvertiser As Long) As Struct()
        FetchAdvertiserContactInfo = DoFetch(CreateSql("IDAdvertiser = " & pIDAdvertiser.ToString))
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pIDAdvertiser"></param>
    ''' <param name="pMainCompanyAddress"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParamsList(ByVal pIDAdvertiser As Long, ByVal pMainCompanyAddress As Boolean) As String()
        Dim params As New List(Of String)

        If pIDAdvertiser <> 0 Then
            params.Add("IDAdvertiser = " & pIDAdvertiser.ToString)
        End If

        If pMainCompanyAddress <> Nothing Then
            params.Add("MainCompanyAddress = " & (pMainCompanyAddress * -1).ToString)
        End If

        Return params.ToArray
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pIDAdvertisers"></param>
    ''' <param name="pMainCompanyAddress"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParamsList(ByVal pIDAdvertisers() As Long, ByVal pMainCompanyAddress As Boolean) As String()()
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

        If pMainCompanyAddress <> Nothing Then
            paramsList.Add(New String() {"MainCompanyAddress = " & (pMainCompanyAddress * -1).ToString})
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
        sql &= "  IDAdvertiser,"
        sql &= "  FirstName,"
        sql &= "  LastName,"
        sql &= "  MainCompanyAddress,"
        sql &= "  PrimaryAddress,"
        sql &= "  SecondaryAddress,"
        sql &= "  City,"
        sql &= "  State,"
        sql &= "  Country,"
        sql &= "  ZipCode,"
        sql &= "  Providence,"
        sql &= "  Department,"
        sql &= "  ResposibleForNotes"
        sql &= " From"
        sql &= "  appAdvertiserContactInfo"

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
        sql &= "  IDAdvertiser,"
        sql &= "  FirstName,"
        sql &= "  LastName,"
        sql &= "  MainCompanyAddress,"
        sql &= "  PrimaryAddress,"
        sql &= "  SecondaryAddress,"
        sql &= "  City,"
        sql &= "  State,"
        sql &= "  Country,"
        sql &= "  ZipCode,"
        sql &= "  Providence,"
        sql &= "  Department,"
        sql &= "  ResposibleForNotes"
        sql &= " From"
        sql &= "  appAdvertiserContactInfo"

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
    ''' Execute SQL statement to reads the data of the table appAdvertiserContactInfo and
    ''' return a list of DAClsAdvertiserContactInfo.Struct with each one of the read registries 
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
                    Struct.FirstName.SetValue(dr.GetString("FirstName"))
                    Struct.LastName.SetValue(dr.GetString("LastName"))
                    Struct.MainCompanyAddress.SetValue(dr.GetBoolean("MainCompanyAddress"))
                    Struct.PrimaryAddress.SetValue(dr.GetString("PrimaryAddress"))
                    Struct.SecondaryAddress.SetValue(dr.GetString("SecondaryAddress"))
                    Struct.City.SetValue(dr.GetString("City"))
                    Struct.State.SetValue(dr.GetString("State"))
                    Struct.Country.SetValue(dr.GetString("Country"))
                    Struct.ZipCode.SetValue(dr.GetString("ZipCode"))
                    Struct.Providence.SetValue(dr.GetString("Providence"))
                    Struct.Department.SetValue(dr.GetString("Department"))
                    Struct.ResposibleForNotes.SetValue(dr.GetString("ResposibleForNotes"))
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

        pStruct.IDAdvertiser.OldValue = pStruct.IDAdvertiser.Value
        pStruct.IDAdvertiser.Value = pStruct.IDAdvertiser.NewValue

        pStruct.FirstName.OldValue = pStruct.FirstName.Value
        pStruct.FirstName.Value = pStruct.FirstName.NewValue

        pStruct.LastName.OldValue = pStruct.LastName.Value
        pStruct.LastName.Value = pStruct.LastName.NewValue

        pStruct.MainCompanyAddress.OldValue = pStruct.MainCompanyAddress.Value
        pStruct.MainCompanyAddress.Value = pStruct.MainCompanyAddress.NewValue

        pStruct.PrimaryAddress.OldValue = pStruct.PrimaryAddress.Value
        pStruct.PrimaryAddress.Value = pStruct.PrimaryAddress.NewValue

        pStruct.SecondaryAddress.OldValue = pStruct.SecondaryAddress.Value
        pStruct.SecondaryAddress.Value = pStruct.SecondaryAddress.NewValue

        pStruct.City.OldValue = pStruct.City.Value
        pStruct.City.Value = pStruct.City.NewValue

        pStruct.State.OldValue = pStruct.State.Value
        pStruct.State.Value = pStruct.State.NewValue

        pStruct.Country.OldValue = pStruct.Country.Value
        pStruct.Country.Value = pStruct.Country.NewValue

        pStruct.ZipCode.OldValue = pStruct.ZipCode.Value
        pStruct.ZipCode.Value = pStruct.ZipCode.NewValue

        pStruct.Providence.OldValue = pStruct.Providence.Value
        pStruct.Providence.Value = pStruct.Providence.NewValue

        pStruct.Department.OldValue = pStruct.Department.Value
        pStruct.Department.Value = pStruct.Department.NewValue

        pStruct.ResposibleForNotes.OldValue = pStruct.ResposibleForNotes.Value
        pStruct.ResposibleForNotes.Value = pStruct.ResposibleForNotes.NewValue

        Return pStruct
    End Function

    ''' <summary>
    ''' Insert a new registry in the table appAdvertiserContactInfo and return Advertiser Contact Info ID
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the Advertiser Contact Info to insert</param>
    ''' <returns>Advertiser Contact Info ID</returns>
    ''' <remarks></remarks>
    Public Shared Function Insert(ByVal pStruct As Struct) As Struct

        'Build SQL value for insert
        Dim sql As String = ""
        sql &= " Insert Into appAdvertiserContactInfo"
        sql &= "  ("
        sql &= "    IDAdvertiser,"
        sql &= "    FirstName,"
        sql &= "    LastName,"
        sql &= "    MainCompanyAddress,"
        sql &= "    PrimaryAddress,"
        sql &= "    SecondaryAddress,"
        sql &= "    City,"
        sql &= "    State,"
        sql &= "    Country,"
        sql &= "    ZipCode,"
        sql &= "    Providence,"
        sql &= "    Department,"
        sql &= "    ResposibleForNotes"
        sql &= "  )"
        sql &= " Values"
        sql &= "  ("
        sql &= "     " & pStruct.IDAdvertiser.NewValue.ToString & ","
        sql &= "    '" & pStruct.FirstName.NewValue & "',"
        sql &= "    '" & pStruct.LastName.NewValue & "',"
        sql &= "     " & CByte(pStruct.MainCompanyAddress.NewValue).ToString & ","
        sql &= "    '" & pStruct.PrimaryAddress.NewValue & "',"
        sql &= "    '" & pStruct.SecondaryAddress.NewValue & "',"
        sql &= "    '" & pStruct.City.NewValue & "',"
        sql &= "    '" & pStruct.State.NewValue & "',"
        sql &= "    '" & pStruct.Country.NewValue & "',"
        sql &= "    '" & pStruct.ZipCode.NewValue & "',"
        sql &= "    '" & pStruct.Providence.NewValue & "',"
        sql &= "    '" & pStruct.Department.NewValue & "',"
        sql &= "    '" & pStruct.ResposibleForNotes.NewValue & "'"
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
    ''' Update a registry in the table appAdvertiserContactInfo and return the number of rows affected
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the Advertiser Contact Info to update</param>
    ''' <returns>Number of rows affected</returns>
    ''' <remarks></remarks>
    Public Shared Function Update(ByVal pStruct As Struct) As Struct

        'Build SQL value
        Dim sql As String = ""
        sql &= " UPDATE appAdvertiserContactInfo"
        sql &= " SET"
        sql &= "  IDAdvertiser = " & pStruct.IDAdvertiser.NewValue.ToString & ","
        sql &= "  FirstName = '" & pStruct.FirstName.NewValue & "',"
        sql &= "  LastName = '" & pStruct.LastName.NewValue & "',"
        sql &= "  MainCompanyAddress = " & CByte(pStruct.MainCompanyAddress.NewValue).ToString & ","
        sql &= "  PrimaryAddress = '" & pStruct.PrimaryAddress.NewValue & "',"
        sql &= "  SecondaryAddress = '" & pStruct.SecondaryAddress.NewValue & "',"
        sql &= "  City = '" & pStruct.City.NewValue & "',"
        sql &= "  State = '" & pStruct.State.NewValue & "',"
        sql &= "  Country = '" & pStruct.Country.NewValue & "',"
        sql &= "  ZipCode = '" & pStruct.ZipCode.NewValue & "',"
        sql &= "  Providence = '" & pStruct.Providence.NewValue & "',"
        sql &= "  Department = '" & pStruct.Department.NewValue & "',"
        sql &= "  ResposibleForNotes = '" & pStruct.ResposibleForNotes.NewValue & "'"
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
    ''' Delete a registry in the table appAdvertiserContactInfo and return the number of rows affected
    ''' </summary>
    ''' <param name="pID">ID of the Advertiser Contact Info to delete</param>
    ''' <remarks></remarks>
    Public Shared Sub Delete(ByVal pID As Long)

        'Build SQL value
        Dim sql As String = ""
        sql &= " Delete From appAdvertiserContactInfo"
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