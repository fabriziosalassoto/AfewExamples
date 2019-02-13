Imports Csla.data
Imports DBConnection
Imports System.Data.OleDb

''' <summary>
''' Represents a controller of data for the appAdvertiserProjectInvoices tables in the data base PCS 
''' </summary>
''' <remarks></remarks>
<Serializable()> Public Class DAClsappAdvertiserTransactionInvoices

    ''' <summary>
    ''' Represents one structures with the data of a registry of the table
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> Public Class Struct
        Public TableName As String = "appAdvertiserProjectInvoices"

        Public ID As StructField(Of Long) = New StructField(Of Long)("ID", True)
        Public IDAdvertiser As StructField(Of Long) = New StructField(Of Long)("IDAdvertiser", False)
        Public IDAdvertiserProject As StructField(Of Long) = New StructField(Of Long)("IDAdvertiserProject", False)
        Public TransactionType As StructField(Of Transactions) = New StructField(Of Transactions)("TransactionType", False)
        Public TransactionDate As StructField(Of Date) = New StructField(Of Date)("TransactionDate", False)
        Public TransactionNumber As StructField(Of Decimal) = New StructField(Of Decimal)("TransactionNumber", False)
        Public TransactionAmount As StructField(Of Double) = New StructField(Of Double)("TransactionAmount", False)
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
        Fetch = DoFetch(CreateSql("appAdvertiserProjectInvoices.ID = " & pID.ToString))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdvertiserProjectInvoice in the table appAdvertiserProjectInvoices and return
    ''' a list of DAClsAdvertiserProjectInvoices.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdvertiserProjectInvoices.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDAdvertiser As Long, ByVal pIDAdvertiserProject As Long, ByVal pFromDate As Date, ByVal pToDate As Date) As Struct()
        Fetch = DoFetch(CreateSql(GetParamsList(pIDAdvertiser, pIDAdvertiserProject, pFromDate, pToDate)))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdvertiserProjectInvoice in the table appAdvertiserProjectInvoices and return
    ''' a list of DAClsAdvertiserProjectInvoices.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdvertiserProjectInvoices.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pIDAdvertisers() As Long, ByVal pIDAdvertiserProjects() As Long, ByVal pFromDate As Date, ByVal pToDate As Date) As Struct()
        Fetch = DoFetch(CreateSql(GetParamsList(pIDAdvertisers, pIDAdvertiserProjects, pFromDate, pToDate)))
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pIDAdvertiser"></param>
    ''' <param name="pIDAdvertiserProject"></param>
    ''' <param name="pFromDate"></param>
    ''' <param name="pToDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParamsList(ByVal pIDAdvertiser As Long, ByVal pIDAdvertiserProject As Long, ByVal pFromDate As Date, ByVal pToDate As Date) As String()
        Dim params As New List(Of String)

        If pIDAdvertiserProject <> 0 Then
            params.Add("IDAdvertiser = " & pIDAdvertiserProject.ToString)
        End If

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
    ''' <param name="pIDAdvertisers"></param>
    ''' <param name="pIDAdvertiserProjects"></param>
    ''' <param name="pFromDate"></param>
    ''' <param name="pToDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParamsList(ByVal pIDAdvertisers() As Long, ByVal pIDAdvertiserProjects() As Long, ByVal pFromDate As Date, ByVal pToDate As Date) As String()()
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

        If pToDate.Date >= New Date(1900, 1, 1).Date AndAlso pToDate.Date < Date.MaxValue.Date Then
            paramsList.Add(New String() {"InvoiceDate <= '" & pToDate.Date.ToString("yyyy-M-dd") & "'"})
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
        sql &= "  appAdvertiserProjectInvoices.ID,"
        sql &= "  IDAdvertiserProject,"
        sql &= "  IDAdvertiser,"
        sql &= "  " & Transactions.Invoice & " as TransactionType,"
        sql &= "  InvoiceDate as TransactionDate,"
        sql &= "  InvoiceNumber as TransactionNumber,"
        sql &= "  TotalAmountDue as TransactionAmount"
        sql &= " From"
        sql &= "  appAdvertiserProjects"
        sql &= " Inner Join"
        sql &= "  appAdvertiserProjectInvoices"
        sql &= " On"
        sql &= "  appAdvertiserProjects.ID = appAdvertiserProjectInvoices.IDAdvertiserProject"

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
        sql &= "  appAdvertiserProjectInvoices.ID,"
        sql &= "  IDAdvertiserProject,"
        sql &= "  IDAdvertiser,"
        sql &= "  " & Transactions.Invoice & " as TransactionType,"
        sql &= "  InvoiceDate as TransactionDate,"
        sql &= "  InvoiceNumber as TransactionNumber,"
        sql &= "  TotalAmountDue as TransactionAmount"
        sql &= " From"
        sql &= "  appAdvertiserProjects"
        sql &= " Inner Join"
        sql &= "  appAdvertiserProjectInvoices"
        sql &= " On"
        sql &= "  appAdvertiserProjects.ID = appAdvertiserProjectInvoices.IDAdvertiserProject"

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
                    Struct.IDAdvertiserProject.SetValue(dr.GetInt64("IDAdvertiserProject"))
                    Struct.TransactionType.SetValue(dr.GetInt32("TransactionType"))
                    Struct.TransactionDate.SetValue(dr.GetDateTime("TransactionDate"))
                    Struct.TransactionNumber.SetValue(dr.GetDecimal("TransactionNumber"))
                    Struct.TransactionAmount.SetValue(dr.GetDouble("TransactionAmount"))
                    List.Add(Struct)
                End While
                DoFetch = List.ToArray
            End Using
        End Using
    End Function

End Class
