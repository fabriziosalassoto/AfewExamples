Imports Csla.Data
Imports DBConnection
Imports System.Data.OleDb

''' <summary>
''' Represents a controller of data for the appSubscribersAccounts table in the data base ServerComputerTracking
''' </summary>
''' <remarks></remarks>
<Serializable()> Public Class DAClsappSubscribersAccounts

    ''' <summary>
    ''' Represents one structures with the data of a registry of the table appAdvertiserAccount 
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> Public Class Struct
        Public TableName As String = "appSubscribersAccounts"

        Public ID As StructField(Of Long) = New StructField(Of Long)("ID", True)
        Public IDUser As StructField(Of Long) = New StructField(Of Long)("IDUser", False)
        Public IDDemographics As StructField(Of Long) = New StructField(Of Long)("IDDemographics", False)
        Public Login As StructField(Of String) = New StructField(Of String)("Login", False, String.Empty)
        Public ComputerSerialNumber As StructField(Of String) = New StructField(Of String)("ComputerSerialNumber", False, String.Empty)
        Public ComputerMacAddress As StructField(Of String) = New StructField(Of String)("ComputerMacAddress", False, String.Empty)
        Public ComputerName As StructField(Of String) = New StructField(Of String)("ComputerName", False, String.Empty)
        Public ComputerHDSerialNumber As StructField(Of String) = New StructField(Of String)("ComputerHDSerialNumber", False, String.Empty)
        Public WebPassword As StructField(Of String) = New StructField(Of String)("WebPassword", False, String.Empty)
        Public ClientPassword As StructField(Of String) = New StructField(Of String)("ClientPassword", False, String.Empty)
        Public HintByPassOne As StructField(Of String) = New StructField(Of String)("HintByPassOne", False, String.Empty)
        Public HintByPassTwo As StructField(Of String) = New StructField(Of String)("HintByPassTwo", False, String.Empty)
        Public ContactEmail As StructField(Of String) = New StructField(Of String)("ContactEmail", False, String.Empty)
        Public InstalledClientProgram As StructField(Of Boolean) = New StructField(Of Boolean)("InstalledClientProgram", False)

    End Class

    ''' <summary>
    ''' Reads the data of all the Subscribers Accountst in the table appSubscribersAccounts and return
    ''' a list of DAClsSubscriberAccount.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch() As Struct()
        Fetch = DoFetch(CreateSql(New String() {}))
    End Function

    ''' <summary>
    ''' Read the data of a certain Subscriber Account in the table appSubscribersAccounts and return
    ''' a list of DAClsSubscriberAccount.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pID">ID of Subscriber Account to fetch</param>
    ''' <returns>List of the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pID As Long) As Struct()
        Fetch = DoFetch(CreateSql("ID = " & pID.ToString))
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="plogin"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function FetchByLogin(ByVal plogin As String) As Struct()
        FetchByLogin = DoFetch(CreateSql("Login = '" & plogin & "'"))
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pSerialNbr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function FetchBySerialNbr(ByVal pSerialNbr As String) As Struct()
        FetchBySerialNbr = DoFetch(CreateSql("ComputerSerialNumber = '" & pSerialNbr & "'"))
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    '''<param name="pMacAddress"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function FetchByMacAddress(ByVal pMacAddress As String) As Struct()
        FetchByMacAddress = DoFetch(CreateSql("ComputerMacAddress = '" & pMacAddress & "'"))
    End Function

    ''' <summary>
    ''' Read the data of a certain Advertiser Account in the table appAdvertiserAccount and return
    ''' a list of DAClsAdvertiserAccount.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pLogin"></param> 
    ''' <param name="pWebPassword"></param>
    ''' <returns>List of DAClsAdvertiserAccount.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function FetchWebSubscriber(ByVal pLogin As String, ByVal pWebPassword As String) As Struct()
        FetchWebSubscriber = DoFetch(CreateSql("Login = '" & pLogin & "'", "WebPassword = '" & pWebPassword & "'"))
    End Function

    ''' <summary>
    ''' Read the data of a certain Advertiser Account in the table appAdvertiserAccount and return
    ''' a list of DAClsAdvertiserAccount.Struct with each one of the read registries 
    ''' </summary>
    ''' <param name="pComputerSerialNumber"></param> 
    ''' <param name="pClientPassword"></param>
    ''' <returns>List of DAClsAdvertiserAccount.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function FetchClientSubscriber(ByVal pComputerSerialNumber As String, ByVal pClientPassword As String) As Struct()
        FetchClientSubscriber = DoFetch(CreateSql("ComputerSerialNumber ='" & pComputerSerialNumber & "'", "ClientPassword = '" & pClientPassword & "'"))
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
        sql &= "  IDUser,"
        sql &= "  IDDemographics,"
        sql &= "  Login,"
        sql &= "  ComputerSerialNumber,"
        sql &= "  ComputerMacAddress,"
        sql &= "  ComputerName,"
        sql &= "  ComputerHDSerialNumber,"
        sql &= "  WebPassword,"
        sql &= "  ClientPassword,"
        sql &= "  HintByPassOne,"
        sql &= "  HintByPassTwo,"
        sql &= "  ContactEmail,"
        sql &= "  InstalledClientProgram"
        sql &= " From"
        sql &= "  appSubscribersAccounts"

        If pParams.Length > 0 Then
            sql &= " Where " & Join(pParams, " And ")
        End If

        CreateSql = sql
    End Function

    ''' <summary>
    ''' Execute SQL statement to reads the data of the table appSubscribersAccounts and
    ''' return a list of DAClsSubscriberAccount.Struct with each one of the read registries 
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
                    Struct.IDUser.SetValue(dr.GetInt64("IDUser"))
                    Struct.IDDemographics.SetValue(dr.GetInt64("IDDemographics"))
                    Struct.Login.SetValue(dr.GetString("Login"))
                    Struct.ComputerSerialNumber.SetValue(dr.GetString("ComputerSerialNumber"))
                    Struct.ComputerMacAddress.SetValue(dr.GetString("ComputerMacAddress"))
                    Struct.ComputerName.SetValue(dr.GetString("ComputerName"))
                    Struct.ComputerHDSerialNumber.SetValue(dr.GetString("ComputerHDSerialNumber"))
                    Struct.WebPassword.SetValue(dr.GetString("WebPassword"))
                    Struct.ClientPassword.SetValue(dr.GetString("ClientPassword"))
                    Struct.HintByPassOne.SetValue(dr.GetString("HintByPassOne"))
                    Struct.HintByPassTwo.SetValue(dr.GetString("HintByPassTwo"))
                    Struct.ContactEmail.SetValue(dr.GetString("ContactEmail"))
                    Struct.InstalledClientProgram.SetValue(dr.GetBoolean("InstalledClientProgram"))
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

        pStruct.IDUser.OldValue = pStruct.IDUser.Value
        pStruct.IDUser.Value = pStruct.IDUser.NewValue

        pStruct.IDDemographics.OldValue = pStruct.IDDemographics.Value
        pStruct.IDDemographics.Value = pStruct.IDDemographics.NewValue

        pStruct.Login.OldValue = pStruct.Login.Value
        pStruct.Login.Value = pStruct.Login.NewValue

        pStruct.ComputerSerialNumber.OldValue = pStruct.ComputerSerialNumber.Value
        pStruct.ComputerSerialNumber.Value = pStruct.ComputerSerialNumber.NewValue

        pStruct.ComputerMacAddress.OldValue = pStruct.ComputerMacAddress.Value
        pStruct.ComputerMacAddress.Value = pStruct.ComputerMacAddress.NewValue

        pStruct.ComputerName.OldValue = pStruct.ComputerName.Value
        pStruct.ComputerName.Value = pStruct.ComputerName.NewValue

        pStruct.ComputerHDSerialNumber.OldValue = pStruct.ComputerHDSerialNumber.Value
        pStruct.ComputerHDSerialNumber.Value = pStruct.ComputerHDSerialNumber.NewValue

        pStruct.WebPassword.OldValue = pStruct.WebPassword.Value
        pStruct.WebPassword.Value = pStruct.WebPassword.NewValue

        pStruct.ClientPassword.OldValue = pStruct.ClientPassword.Value
        pStruct.ClientPassword.Value = pStruct.ClientPassword.NewValue

        pStruct.HintByPassOne.OldValue = pStruct.HintByPassOne.Value
        pStruct.HintByPassOne.Value = pStruct.HintByPassOne.NewValue

        pStruct.HintByPassTwo.OldValue = pStruct.HintByPassTwo.Value
        pStruct.HintByPassTwo.Value = pStruct.HintByPassTwo.NewValue

        pStruct.ContactEmail.OldValue = pStruct.ContactEmail.Value
        pStruct.ContactEmail.Value = pStruct.ContactEmail.NewValue

        pStruct.InstalledClientProgram.OldValue = pStruct.InstalledClientProgram.Value
        pStruct.InstalledClientProgram.Value = pStruct.InstalledClientProgram.NewValue

        Return pStruct
    End Function

    ''' <summary>
    ''' Insert a new registry in the table appSubscribersAccounts and return Subscriber Account ID
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the Subscriber Account to insert</param>
    ''' <returns>Subscriber Account ID</returns>
    ''' <remarks></remarks>
    Public Shared Function Insert(ByVal pStruct As Struct) As Struct

        'Build SQL value for insert
        Dim sql As String = ""
        sql &= " Insert Into appSubscribersAccounts"
        sql &= "  ("
        sql &= "    IDUser,"
        sql &= "    IDDemographics,"
        sql &= "    Login,"
        sql &= "    ComputerSerialNumber,"
        sql &= "    ComputerMacAddress,"
        sql &= "    ComputerName,"
        sql &= "    ComputerHDSerialNumber,"
        sql &= "    WebPassword,"
        sql &= "    ClientPassword,"
        sql &= "    HintByPassOne,"
        sql &= "    HintByPassTwo,"
        sql &= "    ContactEmail,"
        sql &= "    InstalledClientProgram"
        sql &= "  )"
        sql &= " Values"
        sql &= "  ("
        sql &= "     " & pStruct.IDUser.NewValue.ToString & ","
        sql &= "     " & pStruct.IDDemographics.NewValue.ToString & ","
        sql &= "    '" & pStruct.Login.NewValue & "',"
        sql &= "    '" & pStruct.ComputerSerialNumber.NewValue & "',"
        sql &= "    '" & pStruct.ComputerMacAddress.NewValue & "',"
        sql &= "    '" & pStruct.ComputerName.NewValue & "',"
        sql &= "    '" & pStruct.ComputerHDSerialNumber.NewValue & "',"
        sql &= "    '" & pStruct.WebPassword.NewValue & "',"
        sql &= "    '" & pStruct.ClientPassword.NewValue & "',"
        sql &= "    '" & pStruct.HintByPassOne.NewValue & "',"
        sql &= "    '" & pStruct.HintByPassTwo.NewValue & "',"
        sql &= "    '" & pStruct.ContactEmail.NewValue & "',"
        sql &= "     " & CByte(pStruct.InstalledClientProgram.NewValue).ToString & ","
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
    ''' Update a registry in the table appSubscribersAccounts and return the number of rows affected
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the Subscriber Account to update</param>
    ''' <returns>Number of rows affected</returns>
    ''' <remarks></remarks>
    Public Shared Function Update(ByVal pStruct As Struct) As Struct

        'Build SQL value
        Dim sql As String = ""
        sql &= " UPDATE appSubscribersAccounts"
        sql &= " SET"
        sql &= "  IDUser = " & pStruct.IDUser.NewValue.ToString & ","
        sql &= "  IDDemographics = " & pStruct.IDDemographics.NewValue.ToString & ","
        sql &= "  Login = '" & pStruct.Login.NewValue & "',"
        sql &= "  ComputerSerialNumber = '" & pStruct.ComputerSerialNumber.NewValue & "',"
        sql &= "  ComputerMacAddress = '" & pStruct.ComputerMacAddress.NewValue & "',"
        sql &= "  ComputerName = '" & pStruct.ComputerName.NewValue & "',"
        sql &= "  ComputerHDSerialNumber = '" & pStruct.ComputerHDSerialNumber.NewValue & "',"
        sql &= "  WebPassword = '" & pStruct.WebPassword.NewValue & "',"
        sql &= "  ClientPassword = '" & pStruct.ClientPassword.NewValue & "',"
        sql &= "  HintByPassOne = '" & pStruct.HintByPassOne.NewValue & "',"
        sql &= "  HintByPassTwo = '" & pStruct.HintByPassTwo.NewValue & "',"
        sql &= "  ContactEmail = '" & pStruct.ContactEmail.NewValue & "',"
        sql &= "  InstalledClientProgram = " & CByte(pStruct.InstalledClientProgram.NewValue).ToString
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
    ''' Delete a registry in the table appSubscribersAccounts and return the number of rows affected
    ''' </summary>
    ''' <param name="pID">ID of the Subscriber Account</param>
    ''' <remarks></remarks>
    Public Shared Sub Delete(ByVal pID As Long)

        'Build SQL value
        Dim sql As String = ""
        sql &= " Delete From appSubscribersAccounts"
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