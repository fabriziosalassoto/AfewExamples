Imports Csla.Data
Imports DBConnection
Imports System.Data.OleDb

''' <summary>
''' Represents a controller of data for the appAdvertiserProjects table in the data base ServerComputerTracking
''' </summary>
''' <remarks></remarks>
<Serializable()> Public Class DAClsappAdvertiserProjects

    ''' <summary>
    ''' Represents one structures with the data of a registry of the table appAdvertiserProjects
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> Public Class Struct
        Public TableName As String = "appAdvertiserProjects"

        Public ID As StructField(Of Long) = New StructField(Of Long)("ID", True)
        Public IDAdvertiser As StructField(Of Long) = New StructField(Of Long)("IDAdvertiser", False)
        Public IDAdvertiserContact As StructField(Of Long) = New StructField(Of Long)("IDAdvertiserContact", False)
        Public ProjectDescription As StructField(Of String) = New StructField(Of String)("ProjectDescription", False, String.Empty)
        Public ADUrl As StructField(Of String) = New StructField(Of String)("ADUrl", False, String.Empty)
        Public ADHeight As StructField(Of Integer) = New StructField(Of Integer)("ADHeight", False)
        Public ADWidth As StructField(Of Integer) = New StructField(Of Integer)("ADWidth", False)
        Public RunStartDate As StructField(Of Date) = New StructField(Of Date)("RunStartDate", False, New Date(1900, 1, 1))
        Public RunEndDate As StructField(Of Date) = New StructField(Of Date)("RunEndDate", False, New Date(1900, 1, 1))
        Public MinDisplays As StructField(Of Integer) = New StructField(Of Integer)("MinDisplays", False)
        Public MaxDisplays As StructField(Of Integer) = New StructField(Of Integer)("MaxDisplays", False)
        Public MaxPerDay As StructField(Of Integer) = New StructField(Of Integer)("MaxPerDay", False)
        Public MinPerDay As StructField(Of Integer) = New StructField(Of Integer)("MinPerDay", False)
        Public StartTimeBasedOnSubscribersTime As StructField(Of Date) = New StructField(Of Date)("StartTimeBasedOnSubscribersTime", False, New Date(1900, 1, 1, 0, 1, 0))
        Public EndTimeBasedOnSubscribersTime As StructField(Of Date) = New StructField(Of Date)("EndTimeBasedOnSubscribersTime", False, New Date(1900, 1, 1, 23, 59, 0))
        Public AdCountDisplayed As StructField(Of Integer) = New StructField(Of Integer)("AdCountDisplayed", False)
        Public AdVerifiedDate As StructField(Of Date) = New StructField(Of Date)("AdVerifiedDate", False, New Date(1900, 1, 1))
        Public AdOnlineDate As StructField(Of Date) = New StructField(Of Date)("AdOnlineDate", False, New Date(1900, 1, 1))
        Public PromoCode As StructField(Of Integer) = New StructField(Of Integer)("PromoCode", False)
        Public ComissionCode As StructField(Of Integer) = New StructField(Of Integer)("ComissionCode", False)
    End Class

    ''' <summary>
    ''' Reads the data of all the Advertiser Projects in the table appAdvertiserProjects and return
    ''' a list of DAClsAdvertiserProject.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsAdvertiserProject.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch() As Struct()
        Fetch = DoFetch(CreateSql(New String() {}))
    End Function

    ''' <summary>
    ''' Read the data of a certain Advertiser Project in the table appAdvertiserProjects and return
    ''' a list of DAClsAdvertiserProject.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pID">ID of Advertiser Project to fetch</param>
    ''' <returns>List of DAClsAdvertiserProject.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pID As Long) As Struct()
        Fetch = DoFetch(CreateSql("ID = " & pID.ToString))
    End Function

    ''' <summary>
    ''' Read the data of the Advertiser projects in the table appAdvertiserProjects and
    ''' return a list of DAClsappAdvertiserProject.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pIDAdvertiser">Advertiser ID of project to fetch</param>
    ''' <returns>List of DAClsAdvertiserProject.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDAdvertiser As Long, ByVal pIDAdvertiserContact As Long) As Struct()
        Fetch = DoFetch(CreateSql(GetParamsList(pIDAdvertiser, pIDAdvertiserContact)))
    End Function

    ''' <summary>
    ''' Read the data of the Advertiser projects in the table appAdvertiserProjects and
    ''' return a list of DAClsappAdvertiserProject.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pIDAdvertiser">Advertiser ID of project to fetch</param>
    ''' <returns>List of DAClsAdvertiserProject.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDAdvertiser() As Long, ByVal pIDAdvertiserContact() As Long) As Struct()
        Fetch = DoFetch(CreateSql(GetParamsList(pIDAdvertiser, pIDAdvertiserContact)))
    End Function

    ''' <summary>
    ''' Read the data of the Advertiser projects in the table appAdvertiserProjects and
    ''' return a list of DAClsappAdvertiserProject.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pIDAdvertiser">Advertiser ID of project to fetch</param>
    ''' <returns>List of DAClsAdvertiserProject.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function FetchAdvertiserProject(ByVal pIDAdvertiser As Long) As Struct()
        FetchAdvertiserProject = DoFetch(CreateSql("IDAdvertiser = " & pIDAdvertiser.ToString))
    End Function

    ''' <summary>
    ''' Read the data of the contact projects in the table appAdvertiserProjects and
    ''' return a list of DAClsAdvertiserProject.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pIDContact">Contact ID of project to fetch</param>
    ''' <returns>List of DAClsAdvertiserProject.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function FetchContactProject(ByVal pIDContact As Long) As Struct()
        FetchContactProject = DoFetch(CreateSql("IDAdvertiserContact = " & pIDContact.ToString))
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pIDAdvertiser"></param>
    ''' <param name="pIDAdvertiserContact"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParamsList(ByVal pIDAdvertiser As Long, ByVal pIDAdvertiserContact As Long) As String()
        Dim params As New List(Of String)

        If pIDAdvertiser <> 0 Then
            params.Add("IDAdvertiser = " & pIDAdvertiser.ToString)
        End If

        If pIDAdvertiserContact <> 0 Then
            params.Add("IDadvertiserContact = " & pIDAdvertiserContact.ToString)
        End If

        Return params.ToArray
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pIDAdvertisers"></param>
    ''' <param name="pIDAdvertiserContacts"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParamsList(ByVal pIDAdvertisers() As Long, ByVal pIDAdvertiserContacts() As Long) As String()()
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

        If pIDAdvertiserContacts.Length > 0 Then
            Dim paramList As New List(Of String)
            For Each idAdvertiserContact As Long In pIDAdvertiserContacts
                If idAdvertiserContact <> 0 Then
                    paramList.Add("IDAdvertiserContact = " & idAdvertiserContact.ToString)
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
    Private Shared Function CreateSql(ByVal ParamArray pParams() As String) As String

        'Build SQL value
        Dim sql As String = ""
        sql &= " Select"
        sql &= "  ID,"
        sql &= "  IDAdvertiser,"
        sql &= "  IDAdvertiserContact,"
        sql &= "  ProjectDescription,"
        sql &= "  ADUrl,"
        sql &= "  ADHeight,"
        sql &= "  ADWidth,"
        sql &= "  RunStartDate,"
        sql &= "  RunEndDate,"
        sql &= "  MinDisplays,"
        sql &= "  MaxDisplays,"
        sql &= "  MaxPerDay,"
        sql &= "  MinPerDay,"
        sql &= "  StartTimeBasedOnSubscribersTime,"
        sql &= "  EndTimeBasedOnSubscribersTime,"
        sql &= "  AdCountDisplayed,"
        sql &= "  AdVerifiedDate,"
        sql &= "  AdOnlineDate,"
        sql &= "  PromoCode,"
        sql &= "  ComissionCode"
        sql &= " From"
        sql &= "  appAdvertiserProjects"

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
        sql &= "  IDAdvertiserContact,"
        sql &= "  ProjectDescription,"
        sql &= "  ADUrl,"
        sql &= "  ADHeight,"
        sql &= "  ADWidth,"
        sql &= "  RunStartDate,"
        sql &= "  RunEndDate,"
        sql &= "  MinDisplays,"
        sql &= "  MaxDisplays,"
        sql &= "  MaxPerDay,"
        sql &= "  MinPerDay,"
        sql &= "  StartTimeBasedOnSubscribersTime,"
        sql &= "  EndTimeBasedOnSubscribersTime,"
        sql &= "  AdCountDisplayed,"
        sql &= "  AdVerifiedDate,"
        sql &= "  AdOnlineDate,"
        sql &= "  PromoCode,"
        sql &= "  ComissionCode"
        sql &= " From"
        sql &= "  appAdvertiserProjects"

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
    ''' Execute SQL statement to reads the data of the table appAdvertiserProjects and
    ''' return a list of DAClsAdvertiserProject.Struct with each one of the read registries 
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
                    Struct.ProjectDescription.SetValue(dr.GetString("ProjectDescription"))
                    Struct.ADUrl.SetValue(dr.GetString("ADUrl"))
                    Struct.ADHeight.SetValue(dr.GetInt32("ADHeight"))
                    Struct.ADWidth.SetValue(dr.GetInt32("ADWidth"))
                    Struct.RunStartDate.SetValue(dr.GetDateTime("RunStartDate"))
                    Struct.RunEndDate.SetValue(dr.GetDateTime("RunEndDate"))
                    Struct.MinDisplays.SetValue(dr.GetInt32("MinDisplays"))
                    Struct.MaxDisplays.SetValue(dr.GetInt32("MaxDisplays"))
                    Struct.MaxPerDay.SetValue(dr.GetInt32("MaxPerDay"))
                    Struct.MinPerDay.SetValue(dr.GetInt32("MinPerDay"))
                    Struct.StartTimeBasedOnSubscribersTime.SetValue(dr.GetDateTime("StartTimeBasedOnSubscribersTime"))
                    Struct.EndTimeBasedOnSubscribersTime.SetValue(dr.GetDateTime("EndTimeBasedOnSubscribersTime"))
                    Struct.AdCountDisplayed.SetValue(dr.GetInt32("AdCountDisplayed"))
                    Struct.AdVerifiedDate.SetValue(dr.GetDateTime("AdVerifiedDate"))
                    Struct.AdOnlineDate.SetValue(dr.GetDateTime("AdOnlineDate"))
                    Struct.PromoCode.SetValue(dr.GetInt32("PromoCode"))
                    Struct.ComissionCode.SetValue(dr.GetInt32("ComissionCode"))
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

        pStruct.IDAdvertiserContact.OldValue = pStruct.IDAdvertiserContact.Value
        pStruct.IDAdvertiserContact.Value = pStruct.IDAdvertiserContact.NewValue

        pStruct.ProjectDescription.OldValue = pStruct.ProjectDescription.Value
        pStruct.ProjectDescription.Value = pStruct.ProjectDescription.NewValue

        pStruct.ADUrl.OldValue = pStruct.ADUrl.Value
        pStruct.ADUrl.Value = pStruct.ADUrl.NewValue

        pStruct.ADHeight.OldValue = pStruct.ADHeight.Value
        pStruct.ADHeight.Value = pStruct.ADHeight.NewValue

        pStruct.ADWidth.OldValue = pStruct.ADWidth.Value
        pStruct.ADWidth.Value = pStruct.ADWidth.NewValue

        pStruct.RunStartDate.OldValue = pStruct.RunStartDate.Value
        pStruct.RunStartDate.Value = pStruct.RunStartDate.NewValue

        pStruct.RunEndDate.OldValue = pStruct.RunEndDate.Value
        pStruct.RunEndDate.Value = pStruct.RunEndDate.NewValue

        pStruct.MinDisplays.OldValue = pStruct.MinDisplays.Value
        pStruct.MinDisplays.Value = pStruct.MinDisplays.NewValue

        pStruct.MaxDisplays.OldValue = pStruct.MaxDisplays.Value
        pStruct.MaxDisplays.Value = pStruct.MaxDisplays.NewValue

        pStruct.MaxPerDay.OldValue = pStruct.MaxPerDay.Value
        pStruct.MaxPerDay.Value = pStruct.MaxPerDay.NewValue

        pStruct.MinPerDay.OldValue = pStruct.MinPerDay.Value
        pStruct.MinPerDay.Value = pStruct.MinPerDay.NewValue

        pStruct.StartTimeBasedOnSubscribersTime.OldValue = pStruct.StartTimeBasedOnSubscribersTime.Value
        pStruct.StartTimeBasedOnSubscribersTime.Value = pStruct.StartTimeBasedOnSubscribersTime.NewValue

        pStruct.EndTimeBasedOnSubscribersTime.OldValue = pStruct.EndTimeBasedOnSubscribersTime.Value
        pStruct.EndTimeBasedOnSubscribersTime.Value = pStruct.EndTimeBasedOnSubscribersTime.NewValue

        pStruct.AdCountDisplayed.OldValue = pStruct.AdCountDisplayed.Value
        pStruct.AdCountDisplayed.Value = pStruct.AdCountDisplayed.NewValue

        pStruct.AdVerifiedDate.OldValue = pStruct.AdVerifiedDate.Value
        pStruct.AdVerifiedDate.Value = pStruct.AdVerifiedDate.NewValue

        pStruct.AdOnlineDate.OldValue = pStruct.AdOnlineDate.Value
        pStruct.AdOnlineDate.Value = pStruct.AdOnlineDate.NewValue

        pStruct.PromoCode.OldValue = pStruct.PromoCode.Value
        pStruct.PromoCode.Value = pStruct.PromoCode.NewValue

        pStruct.ComissionCode.OldValue = pStruct.ComissionCode.Value
        pStruct.ComissionCode.Value = pStruct.ComissionCode.NewValue

        Return pStruct
    End Function

    ''' <summary>
    ''' Insert a new registry in the table appAdvertiserProjects and return Advertiser Project ID
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the Advertiser Project to insert</param>
    ''' <returns>Advertiser Project ID</returns>
    ''' <remarks></remarks>
    Public Shared Function Insert(ByVal pStruct As Struct) As Struct

        'Build SQL value for insert
        Dim sql As String = ""
        sql &= " Insert Into appAdvertiserProjects"
        sql &= "  ("
        sql &= "    IDAdvertiser,"
        sql &= "    IDAdvertiserContact,"
        sql &= "    ProjectDescription,"
        sql &= "    ADUrl,"
        sql &= "    ADHeight,"
        sql &= "    ADWidth,"
        sql &= "    RunStartDate,"
        sql &= "    RunEndDate,"
        sql &= "    MinDisplays,"
        sql &= "    MaxDisplays,"
        sql &= "    MaxPerDay,"
        sql &= "    MinPerDay,"
        sql &= "    StartTimeBasedOnSubscribersTime,"
        sql &= "    EndTimeBasedOnSubscribersTime,"
        sql &= "    AdCountDisplayed,"
        sql &= "    AdVerifiedDate,"
        sql &= "    AdOnlineDate,"
        sql &= "    PromoCode,"
        sql &= "    ComissionCode"
        sql &= "  )"
        sql &= " Values"
        sql &= "  ("
        sql &= "     " & pStruct.IDAdvertiser.NewValue.ToString & ","
        sql &= "     " & pStruct.IDAdvertiserContact.NewValue.ToString & ","
        sql &= "    '" & pStruct.ProjectDescription.NewValue & "',"
        sql &= "    '" & pStruct.ADUrl.NewValue & "',"
        sql &= "     " & pStruct.ADHeight.NewValue.ToString & ","
        sql &= "     " & pStruct.ADWidth.NewValue.ToString & ","
        sql &= "    '" & pStruct.RunStartDate.NewValue.ToString("yyyy-M-dd") & "',"
        sql &= "    '" & pStruct.RunEndDate.NewValue.ToString("yyyy-M-dd") & "',"
        sql &= "     " & pStruct.MinDisplays.NewValue.ToString & ","
        sql &= "     " & pStruct.MaxDisplays.NewValue.ToString & ","
        sql &= "     " & pStruct.MaxPerDay.NewValue.ToString & ","
        sql &= "     " & pStruct.MinPerDay.NewValue.ToString & ","
        sql &= "    '" & pStruct.StartTimeBasedOnSubscribersTime.NewValue.ToString("HH:mm") & "',"
        sql &= "    '" & pStruct.EndTimeBasedOnSubscribersTime.NewValue.ToString("HH:mm") & "',"
        sql &= "     " & pStruct.AdCountDisplayed.NewValue.ToString & ","
        sql &= "    '" & pStruct.AdVerifiedDate.NewValue.ToString("yyyy-M-dd") & "',"
        sql &= "    '" & pStruct.AdOnlineDate.NewValue.ToString("yyyy-M-dd") & "',"
        sql &= "     " & pStruct.PromoCode.NewValue.ToString & ","
        sql &= "     " & pStruct.ComissionCode.NewValue.ToString
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
    ''' Update a registry in the table appAdvertiserProjects and return the number of rows affected
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the Advertiser Project to update</param>
    ''' <returns>Number of rows affected</returns>
    ''' <remarks></remarks>
    Public Shared Function Update(ByVal pStruct As Struct) As Struct

        'Build SQL value
        Dim sql As String = ""
        sql &= " UPDATE appAdvertiserProjects"
        sql &= " SET"
        sql &= "  IDAdvertiser = " & pStruct.IDAdvertiser.NewValue.ToString & ","
        sql &= "  IDAdvertiserContact = " & pStruct.IDAdvertiserContact.NewValue.ToString & ","
        sql &= "  ProjectDescription = '" & pStruct.ProjectDescription.NewValue & "',"
        sql &= "  ADUrl = '" & pStruct.ADUrl.NewValue & "',"
        sql &= "  ADHeight = " & pStruct.ADHeight.NewValue.ToString & ","
        sql &= "  ADWidth = " & pStruct.ADWidth.NewValue.ToString & ","
        sql &= "  RunStartDate = '" & pStruct.RunStartDate.NewValue.ToString("yyyy-M-dd") & "',"
        sql &= "  RunEndDate = '" & pStruct.RunEndDate.NewValue.ToString("yyyy-M-dd") & "',"
        sql &= "  MinDisplays = " & pStruct.MinDisplays.NewValue.ToString & ","
        sql &= "  MaxDisplays = " & pStruct.MaxDisplays.NewValue.ToString & ","
        sql &= "  MaxPerDay = " & pStruct.MaxPerDay.NewValue.ToString & ","
        sql &= "  MinPerDay = " & pStruct.MinPerDay.NewValue.ToString & ","
        sql &= "  StartTimeBasedOnSubscribersTime = '" & pStruct.StartTimeBasedOnSubscribersTime.NewValue.ToString("HH:mm") & "',"
        sql &= "  EndTimeBasedOnSubscribersTime = '" & pStruct.EndTimeBasedOnSubscribersTime.NewValue.ToString("HH:mm") & "',"
        sql &= "  AdCountDisplayed = " & pStruct.AdCountDisplayed.NewValue.ToString & ","
        sql &= "  AdVerifiedDate = '" & pStruct.AdVerifiedDate.NewValue.ToString("yyyy-M-dd") & "',"
        sql &= "  AdOnlineDate = '" & pStruct.AdOnlineDate.NewValue.ToString("yyyy-M-dd") & "',"
        sql &= "  PromoCode = " & pStruct.PromoCode.NewValue.ToString & ","
        sql &= "  ComissionCode = " & pStruct.ComissionCode.NewValue.ToString
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
    ''' Delete a registry in the table appAdvertiserProjects and return the number of rows affected
    ''' </summary>
    ''' <param name="pID">Id of the Advertiser Project to delete</param>
    ''' <remarks></remarks>
    Public Shared Sub Delete(ByVal pID As Long)

        'Build SQL value
        Dim sql As String = ""
        sql &= " Delete From appAdvertiserProjects"
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