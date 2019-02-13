Imports Csla.data
Imports DBConnection
Imports System.Data.OleDb

''' <summary>
''' Represents a controller of data for the appAdvertiserProjectInvoiceDetails table in the data base PCS 
''' </summary>
''' <remarks></remarks>
<Serializable()> Public Class DAClsappAdvertiserProjectInvoiceDetails

    ''' <summary>
    ''' Represents one structures with the data of a registry of the table appAdvertiserProjectInvoiceDetails 
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> Public Class Struct
        Public TableName As String = "appAdvertiserProjectInvoiceDetails"

        Public ID As StructField(Of Long) = New StructField(Of Long)("ID", True)
        Public IDAdvertiserInvoice As StructField(Of Long) = New StructField(Of Long)("IDAdvertiserInvoice", False)
        Public CurrentNumberOfClickThrough As StructField(Of Integer) = New StructField(Of Integer)("CurrentNumberOfClickThrough", False)
        Public CurrentNumberOfDisplay As StructField(Of Integer) = New StructField(Of Integer)("CurrentNumberOfDisplay", False)
        Public CostPerClickThrough As StructField(Of Double) = New StructField(Of Double)("CostPerClickThrough", False)
        Public CostPerDisplay As StructField(Of Double) = New StructField(Of Double)("CostPerDisplay", False)
        Public AmountDue As StructField(Of Double) = New StructField(Of Double)("AmountDue", False)

	End Class

    ''' <summary>
    ''' Reads the data of all the AdvertiserProjectInvoiceDetails in the table appAdvertiserProjectInvoiceDetails and return
    ''' a list of DAClsAdvertiserProjectInvoiceDetails.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsAdvertiserProjectInvoiceDetails.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch() As Struct()
        Fetch = DoFetch(CreateSql(New String() {}))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdvertiserProjectInvoiceDetail in the table appAdvertiserProjectInvoiceDetails and return
    ''' a list of DAClsAdvertiserProjectInvoiceDetails.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdvertiserProjectInvoiceDetails.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pID As Long) As Struct()
        Fetch = DoFetch(CreateSql("ID = " & pID.ToString))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdvertiserProjectInvoiceDetail in the table appAdvertiserProjectInvoiceDetails and return
    ''' a list of DAClsAdvertiserProjectInvoiceDetails.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdvertiserProjectInvoiceDetails.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function FetchInvoiceDetail(ByVal pIDAdvertiserInvoice As Long) As Struct()
        FetchInvoiceDetail = DoFetch(CreateSql("IDAdvertiserInvoice = " & pIDAdvertiserInvoice.ToString))
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
        sql &= "  IDAdvertiserInvoice,"
        sql &= "  CurrentNumberOfClickThrough,"
        sql &= "  CurrentNumberOfDisplay,"
        sql &= "  CostPerClickThrough,"
        sql &= "  CostPerDisplay,"
        sql &= "  AmountDue"
        sql &= " From"
        sql &= "  appAdvertiserProjectInvoiceDetails"

        If pParams.Length > 0 Then
            sql &= " Where " & Join(pParams, " And ")
        End If

        CreateSql = sql
    End Function

    ''' <summary>
    ''' Execute SQL statement to reads the data of the table appAdvertiserProjectInvoiceDetails and
    ''' return a list of DAClsAdvertiserProjectInvoiceDetails.Struct with each one of the read registries 
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
                    Struct.IDAdvertiserInvoice.SetValue(dr.GetInt64("IDAdvertiserInvoice"))
                    Struct.CurrentNumberOfClickThrough.SetValue(dr.GetInt32("CurrentNumberOfClickThrough"))
                    Struct.CurrentNumberOfDisplay.SetValue(dr.GetInt32("CurrentNumberOfDisplay"))
                    Struct.CostPerClickThrough.SetValue(dr.GetDouble("CostPerClickThrough"))
                    Struct.CostPerDisplay.SetValue(dr.GetDouble("CostPerDisplay"))
                    Struct.AmountDue.SetValue(dr.GetDouble("AmountDue"))
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

        pStruct.IDAdvertiserInvoice.OldValue = pStruct.IDAdvertiserInvoice.Value
        pStruct.IDAdvertiserInvoice.Value = pStruct.IDAdvertiserInvoice.NewValue

        pStruct.CurrentNumberOfClickThrough.OldValue = pStruct.CurrentNumberOfClickThrough.Value
        pStruct.CurrentNumberOfClickThrough.Value = pStruct.CurrentNumberOfClickThrough.NewValue

        pStruct.CurrentNumberOfDisplay.OldValue = pStruct.CurrentNumberOfDisplay.Value
        pStruct.CurrentNumberOfDisplay.Value = pStruct.CurrentNumberOfDisplay.NewValue

        pStruct.CostPerClickThrough.OldValue = pStruct.CostPerClickThrough.Value
        pStruct.CostPerClickThrough.Value = pStruct.CostPerClickThrough.NewValue

        pStruct.CostPerDisplay.OldValue = pStruct.CostPerDisplay.Value
        pStruct.CostPerDisplay.Value = pStruct.CostPerDisplay.NewValue

        pStruct.AmountDue.OldValue = pStruct.AmountDue.Value
        pStruct.AmountDue.Value = pStruct.AmountDue.NewValue

        Return pStruct
    End Function

    ''' <summary>
    ''' Insert a new registry in the table appAdvertiserProjectInvoiceDetails and return ID
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the AdvertiserProjectInvoiceDetail to insert</param>
    ''' <returns>ID</returns>
    ''' <remarks></remarks>
    Public Shared Function Insert(ByVal pStruct As Struct) As Struct

        'Build SQL value for insert
        Dim sql As String = ""
        sql &= " Insert Into appAdvertiserProjectInvoiceDetails"
        sql &= "  ("
        sql &= "    IDAdvertiserInvoice,"
        sql &= "    CurrentNumberOfClickThrough,"
        sql &= "    CurrentNumberOfDisplay,"
        sql &= "    CostPerClickThrough,"
        sql &= "    CostPerDisplay,"
        sql &= "    AmountDue"
        sql &= "  )"
        sql &= " Values"
        sql &= "  ("
        sql &= "    " & pStruct.IDAdvertiserInvoice.NewValue.ToString & ","
        sql &= "    " & pStruct.CurrentNumberOfClickThrough.NewValue.ToString & ","
        sql &= "    " & pStruct.CurrentNumberOfDisplay.NewValue.ToString & ","
        sql &= "    " & pStruct.CostPerClickThrough.NewValue.ToString & ","
        sql &= "    " & pStruct.CostPerDisplay.NewValue.ToString & ","
        sql &= "    " & pStruct.AmountDue.NewValue.ToString
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
    ''' Update a registry in the table appAdvertiserProjectInvoiceDetails and return the number of rows affected
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the AdvertiserProjectInvoiceDetail to update</param>
    ''' <returns>Number of rows affected</returns>
    ''' <remarks></remarks>
    Public Shared Function Update(ByVal pStruct As Struct) As Struct

        'Build SQL value
        Dim sql As String = ""
        sql &= " UPDATE appAdvertiserProjectInvoiceDetails"
        sql &= " SET"
        sql &= "  IDAdvertiserInvoice = " & pStruct.IDAdvertiserInvoice.NewValue.ToString & ","
        sql &= "  CurrentNumberOfClickThrough = " & pStruct.CurrentNumberOfClickThrough.NewValue.ToString & ","
        sql &= "  CurrentNumberOfDisplay = " & pStruct.CurrentNumberOfDisplay.NewValue.ToString & ","
        sql &= "  CostPerClickThrough = " & pStruct.CostPerClickThrough.NewValue.ToString & ","
        sql &= "  CostPerDisplay = " & pStruct.CostPerDisplay.NewValue.ToString & ","
        sql &= "  AmountDue = " & pStruct.AmountDue.NewValue.ToString
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
    ''' Delete a registry in the table appAdvertiserProjectInvoiceDetails and return the number of rows affected
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub Delete(ByVal pID As Long)

        'Build SQL value
        Dim sql As String = ""
        sql &= " Delete From appAdvertiserProjectInvoiceDetails"
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
