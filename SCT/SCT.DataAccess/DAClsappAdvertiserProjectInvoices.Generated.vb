Imports Csla.data
Imports DBConnection
Imports System.Data.OleDb

''' <summary>
''' Represents a controller of data for the appAdvertiserProjectInvoices table in the data base PCS 
''' </summary>
''' <remarks></remarks>
<Serializable()> Public Class DAClsappAdvertiserProjectInvoices

    ''' <summary>
    ''' Represents one structures with the data of a registry of the table appAdvertiserProjectInvoices 
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> Public Class Struct
        Public TableName As String = "appAdvertiserProjectInvoices"

        Public ID As StructField(Of Long) = New StructField(Of Long)("ID", True)
        Public IDAdvertiserProject As StructField(Of Long) = New StructField(Of Long)("IDAdvertiserProject", False)
        Public InvoiceNumber As StructField(Of Decimal) = New StructField(Of Decimal)("InvoiceNumber", False)
        Public InvoiceSequence As StructField(Of Decimal) = New StructField(Of Decimal)("InvoiceSequence", False)
        Public InvoiceDate As StructField(Of Date) = New StructField(Of Date)("InvoiceDate", False)
        Public PaidToDate As StructField(Of Date) = New StructField(Of Date)("PaidToDate", False)
        Public ChargedToDate As StructField(Of Date) = New StructField(Of Date)("ChargedToDate", False)
        Public PreviousBalance As StructField(Of Double) = New StructField(Of Double)("PreviousBalance", False)
        Public PreviousNumberOfClickThrough As StructField(Of Integer) = New StructField(Of Integer)("PreviousNumberOfClickThrough", False)
        Public PreviousNumberOfDisplays As StructField(Of Integer) = New StructField(Of Integer)("PreviousNumberOfDisplays", False)
        Public TotalAmountDue As StructField(Of Double) = New StructField(Of Double)("TotalAmountDue", False)

    End Class

    ''' <summary>
    ''' Reads the data of all the AdvertiserProjectInvoices in the table appAdvertiserProjectInvoices and return
    ''' a list of DAClsAdvertiserProjectInvoices.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsAdvertiserProjectInvoices.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch() As Struct()
        Fetch = DoFetch(CreateSql(New String() {}))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdvertiserProjectInvoice in the table appAdvertiserProjectInvoices and return
    ''' a list of DAClsAdvertiserProjectInvoices.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdvertiserProjectInvoices.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pID As Long) As Struct()
        Fetch = DoFetch(CreateSql("ID = " & pID.ToString))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdvertiserProjectInvoice in the table appAdvertiserProjectInvoices and return
    ''' a list of DAClsAdvertiserProjectInvoices.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdvertiserProjectInvoices.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDAdvertiserProject As Long, ByVal pFromDate As Date, ByVal pToDate As Date) As Struct()
        Fetch = DoFetch(CreateSql(GetParamsList(pIDAdvertiserProject, pFromDate, pToDate)))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdvertiserProjectInvoice in the table appAdvertiserProjectInvoices and return
    ''' a list of DAClsAdvertiserProjectInvoices.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdvertiserProjectInvoices.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDAdvertiserProjects() As Long, ByVal pFromDate As Date, ByVal pToDate As Date) As Struct()
        Fetch = DoFetch(CreateSql(GetParamsList(pIDAdvertiserProjects, pFromDate, pToDate)))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdvertiserProjectInvoice in the table appAdvertiserProjectInvoices and return
    ''' a list of DAClsAdvertiserProjectInvoices.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdvertiserProjectInvoices.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function FetchProjectInvoice(ByVal pIDAdvertiserProject As Long) As Struct()
        FetchProjectInvoice = DoFetch(CreateSql("IDAdvertiserProject = " & pIDAdvertiserProject.ToString))
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pIDAdvertiserProject"></param>
    ''' <param name="pFromDate"></param>
    ''' <param name="pToDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParamsList(ByVal pIDAdvertiserProject As Long, ByVal pFromDate As Date, ByVal pToDate As Date) As String()
        Dim params As New List(Of String)

        If pIDAdvertiserProject <> 0 Then
            params.Add("IDAdvertiserProject = " & pIDAdvertiserProject.ToString)
        End If

        If pFromDate.Date > New Date(1900, 1, 1).Date AndAlso pFromDate.Date <= Date.MaxValue.Date Then
            params.Add("InvoiceDate >= '" & pFromDate.Date.ToString("yyyy-M-dd") & "'")
        End If

        If pToDate.Date >= New Date(1900, 1, 1).Date AndAlso pToDate.Date < Date.MaxValue.Date Then
            params.Add("InvoiceDate <= '" & pToDate.Date.ToString("yyyy-M-dd") & "'")
        End If

        Return params.ToArray
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pIDAdvertiserProjects"></param>
    ''' <param name="pFromDate"></param>
    ''' <param name="pToDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParamsList(ByVal pIDAdvertiserProjects() As Long, ByVal pFromDate As Date, ByVal pToDate As Date) As String()()
        Dim paramsList As New List(Of String())

        If pIDAdvertiserProjects.Length > 0 Then
            Dim paramList As New List(Of String)
            For Each idAdvertiserProject As Long In pIDAdvertiserProjects
                If idAdvertiserProject <> 0 Then
                    paramList.Add("IDAdvertiserProject = " & idAdvertiserProject.ToString)
                End If
            Next
            paramsList.Add(paramList.ToArray)
        End If

        If pFromDate.Date > New Date(1900, 1, 1).Date AndAlso pFromDate.Date <= Date.MaxValue.Date Then
            paramsList.Add(New String() {"InvoiceDate >= '" & pFromDate.Date.ToString("yyyy-M-dd") & "'"})
        End If

        If pToDate.Date => New Date(1900, 1, 1).Date AndAlso pToDate.Date < Date.MaxValue.Date Then
            paramsList.Add(New String() {"InvoiceDate <= '" & pToDate.Date.ToString("yyyy-M-dd") & "'"})
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
        sql &= "  IDAdvertiserProject,"
        sql &= "  InvoiceNumber,"
        sql &= "  InvoiceSequence,"
        sql &= "  InvoiceDate,"
        sql &= "  PaidToDate,"
        sql &= "  ChargedToDate,"
        sql &= "  PreviousBalance,"
        sql &= "  PreviousNumberOfClickThrough,"
        sql &= "  PreviousNumberOfDisplays,"
        sql &= "  TotalAmountDue"
        sql &= " From"
        sql &= "  appAdvertiserProjectInvoices"

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
        sql &= "  IDAdvertiserProject,"
        sql &= "  InvoiceNumber,"
        sql &= "  InvoiceSequence,"
        sql &= "  InvoiceDate,"
        sql &= "  PaidToDate,"
        sql &= "  ChargedToDate,"
        sql &= "  PreviousBalance,"
        sql &= "  PreviousNumberOfClickThrough,"
        sql &= "  PreviousNumberOfDisplays,"
        sql &= "  TotalAmountDue"
        sql &= " From"
        sql &= "  appAdvertiserProjectInvoices"

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
                    Struct.IDAdvertiserProject.SetValue(dr.GetInt64("IDAdvertiserProject"))
                    Struct.InvoiceNumber.SetValue(dr.GetDecimal("InvoiceNumber"))
                    Struct.InvoiceSequence.SetValue(dr.GetDecimal("InvoiceSequence"))
                    Struct.InvoiceDate.SetValue(dr.GetDateTime("InvoiceDate"))
                    Struct.PaidToDate.SetValue(dr.GetDateTime("PaidToDate"))
                    Struct.ChargedToDate.SetValue(dr.GetDateTime("ChargedToDate"))
                    Struct.PreviousBalance.SetValue(dr.GetDouble("PreviousBalance"))
                    Struct.PreviousNumberOfClickThrough.SetValue(dr.GetInt32("PreviousNumberOfClickThrough"))
                    Struct.PreviousNumberOfDisplays.SetValue(dr.GetInt32("PreviousNumberOfDisplays"))
                    Struct.TotalAmountDue.SetValue(dr.GetDouble("TotalAmountDue"))
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

        pStruct.IDAdvertiserProject.OldValue = pStruct.IDAdvertiserProject.Value
        pStruct.IDAdvertiserProject.Value = pStruct.IDAdvertiserProject.NewValue

        pStruct.InvoiceNumber.OldValue = pStruct.InvoiceNumber.Value
        pStruct.InvoiceNumber.Value = pStruct.InvoiceNumber.NewValue

        pStruct.InvoiceSequence.OldValue = pStruct.InvoiceSequence.Value
        pStruct.InvoiceSequence.Value = pStruct.InvoiceSequence.NewValue

        pStruct.InvoiceDate.OldValue = pStruct.InvoiceDate.Value
        pStruct.InvoiceDate.Value = pStruct.InvoiceDate.NewValue

        pStruct.PaidToDate.OldValue = pStruct.PaidToDate.Value
        pStruct.PaidToDate.Value = pStruct.PaidToDate.NewValue

        pStruct.ChargedToDate.OldValue = pStruct.ChargedToDate.Value
        pStruct.ChargedToDate.Value = pStruct.ChargedToDate.NewValue

        pStruct.PreviousBalance.OldValue = pStruct.PreviousBalance.Value
        pStruct.PreviousBalance.Value = pStruct.PreviousBalance.NewValue

        pStruct.PreviousNumberOfClickThrough.OldValue = pStruct.PreviousNumberOfClickThrough.Value
        pStruct.PreviousNumberOfClickThrough.Value = pStruct.PreviousNumberOfClickThrough.NewValue

        pStruct.PreviousNumberOfDisplays.OldValue = pStruct.PreviousNumberOfDisplays.Value
        pStruct.PreviousNumberOfDisplays.Value = pStruct.PreviousNumberOfDisplays.NewValue

        pStruct.TotalAmountDue.OldValue = pStruct.TotalAmountDue.Value()
        pStruct.TotalAmountDue.Value = pStruct.TotalAmountDue.NewValue

        Return pStruct
    End Function

    ''' <summary>
    ''' Insert a new registry in the table appAdvertiserProjectInvoices and return ID
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the AdvertiserProjectInvoice to insert</param>
    ''' <returns>ID</returns>
    ''' <remarks></remarks>
    Public Shared Function Insert(ByVal pStruct As Struct) As Struct

        'Build SQL value for insert
        Dim sql As String = ""
        sql &= " Insert Into appAdvertiserProjectInvoices"
        sql &= "  ("
        sql &= "    IDAdvertiserProject,"
        sql &= "    InvoiceNumber,"
        sql &= "    InvoiceSequence,"
        sql &= "    InvoiceDate,"
        sql &= "    PaidToDate,"
        sql &= "    ChargedToDate,"
        sql &= "    PreviousBalance,"
        sql &= "    PreviousNumberOfClickThrough,"
        sql &= "    PreviousNumberOfDisplays,"
        sql &= "    TotalAmountDue"
        sql &= "  )"
        sql &= " Values"
        sql &= "  ("
        sql &= "     " & pStruct.IDAdvertiserProject.NewValue.ToString & ","
        sql &= "     " & pStruct.InvoiceNumber.NewValue.ToString & ","
        sql &= "     " & pStruct.InvoiceSequence.NewValue.ToString & ","
        sql &= "    '" & pStruct.InvoiceDate.NewValue.ToString("yyyy-M-dd") & "',"
        sql &= "    '" & pStruct.PaidToDate.NewValue.ToString("yyyy-M-dd") & "',"
        sql &= "    '" & pStruct.ChargedToDate.NewValue.ToString("yyyy-M-dd") & "',"
        sql &= "     " & pStruct.PreviousBalance.NewValue.ToString & ","
        sql &= "     " & pStruct.PreviousNumberOfClickThrough.NewValue.ToString & ","
        sql &= "     " & pStruct.PreviousNumberOfDisplays.NewValue.ToString & ","
        sql &= "     " & pStruct.TotalAmountDue.NewValue.ToString
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
    ''' Update a registry in the table appAdvertiserProjectInvoices and return the number of rows affected
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the AdvertiserProjectInvoice to update</param>
    ''' <returns>Number of rows affected</returns>
    ''' <remarks></remarks>
    Public Shared Function Update(ByVal pStruct As Struct) As Struct

        'Build SQL value
        Dim sql As String = ""
        sql &= " UPDATE appAdvertiserProjectInvoices"
        sql &= " SET"
        sql &= "  IDAdvertiserProject = " & pStruct.IDAdvertiserProject.NewValue.ToString & ","
        sql &= "  InvoiceNumber = " & pStruct.InvoiceNumber.NewValue.ToString & ","
        sql &= "  InvoiceSequence = " & pStruct.InvoiceSequence.NewValue.ToString & ","
        sql &= "  InvoiceDate = '" & pStruct.InvoiceDate.NewValue.ToString("yyyy-M-dd") & "',"
        sql &= "  PaidToDate = '" & pStruct.PaidToDate.NewValue.ToString("yyyy-M-dd") & "',"
        sql &= "  ChargedToDate = '" & pStruct.ChargedToDate.NewValue.ToString("yyyy-M-dd") & "',"
        sql &= "  PreviousBalance = " & pStruct.PreviousBalance.NewValue.ToString & ","
        sql &= "  PreviousNumberOfClickThrough = " & pStruct.PreviousNumberOfClickThrough.NewValue.ToString & ","
        sql &= "  PreviousNumberOfDisplays = " & pStruct.PreviousNumberOfDisplays.NewValue.ToString & ","
        sql &= "  TotalAmountDue = " & pStruct.TotalAmountDue.NewValue.ToString
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
    ''' Delete a registry in the table appAdvertiserProjectInvoices and return the number of rows affected
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub Delete(ByVal pID As Long)

        'Build SQL value
        Dim sql As String = ""
        sql &= " Delete From appAdvertiserProjectInvoices"
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
