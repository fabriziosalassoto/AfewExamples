Imports Csla.data
Imports DBConnection
Imports System.Data.OleDb

''' <summary>
''' Represents a controller of data for the appAdvertiserReceipts table in the data base PCS 
''' </summary>
''' <remarks></remarks>
<Serializable()> Public Class DAClsappAdvertiserReceipts

    ''' <summary>
    ''' Represents one structures with the data of a registry of the table appAdvertiserReceipts 
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> Public Class Struct
        Public TableName As String = "appAdvertiserReceipts"

        Public ID As StructField(Of Long) = New StructField(Of Long)("ID", True)
        Public ReceiptNumber As StructField(Of Decimal) = New StructField(Of Decimal)("ReceiptNumber", False)
        Public PaymentNumber As StructField(Of Decimal) = New StructField(Of Decimal)("PaymentNumber", False)
        Public PaymentAmount As StructField(Of Double) = New StructField(Of Double)("PaymentAmount", False)
        Public PaymentType As StructField(Of Integer) = New StructField(Of Integer)("PaymentType", False)
        Public PaymentDate As StructField(Of Date) = New StructField(Of Date)("PaymentDate", False)
    End Class

    ''' <summary>
    ''' Reads the data of all the AdvertiserReceipts in the table appAdvertiserReceipts and return
    ''' a list of DAClsAdvertiserReceipts.Struct with each one of the read registries
    ''' </summary>
    ''' <returns>List of DAClsAdvertiserReceipts.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch() As Struct()
        Fetch = DoFetch(CreateSql(New String() {}))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdvertiserReceipt in the table appAdvertiserReceipts and return
    ''' a list of DAClsAdvertiserReceipts.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdvertiserReceipts.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pID As Long) As Struct()
        Fetch = DoFetch(CreateSql("ID = " & pID.ToString))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdvertiserReceipt in the table appAdvertiserReceipts and return
    ''' a list of DAClsAdvertiserReceipts.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdvertiserReceipts.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pPaymentType As Integer, ByVal pFromDate As Date, ByVal pToDate As Date) As Struct()
        Fetch = DoFetch(CreateSql(GetParamsList(pPaymentType, pFromDate, pToDate)))
    End Function

    ''' <summary>
    ''' Read the data of a certain AdvertiserReceipt in the table appAdvertiserReceipts and return
    ''' a list of DAClsAdvertiserReceipts.Struct with each one of the read registries 
    ''' </summary>
    ''' <returns>List of DAClsAdvertiserReceipts.Struct with the read registries</returns>
    ''' <remarks></remarks>
    Public Shared Function Fetch(ByVal pPaymentType() As Integer, ByVal pFromDate As Date, ByVal pToDate As Date) As Struct()
        Fetch = DoFetch(CreateSql(GetParamsList(pPaymentType, pFromDate, pToDate)))
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pPaymnetType"></param>
    ''' <param name="pFromDate"></param>
    ''' <param name="pToDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParamsList(ByVal pPaymnetType As Integer, ByVal pFromDate As Date, ByVal pToDate As Date) As String()
        Dim params As New List(Of String)

        If pPaymnetType <> 0 Then
            params.Add("PaymentType = " & pPaymnetType.ToString)
        End If

        If pFromDate.Date > New Date(1900, 1, 1).Date AndAlso pFromDate.Date <= Date.MaxValue.Date Then
            params.Add("PaymentDate >= '" & pFromDate.Date.ToString("yyyy-M-dd") & "'")
        End If

        If pToDate.Date >= New Date(1900, 1, 1).Date AndAlso pToDate.Date < Date.MaxValue.Date Then
            params.Add("PaymentDate <= '" & pToDate.Date.ToString("yyyy-M-dd") & "'")
        End If

        Return params.ToArray
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pPaymnetTypes"></param>
    ''' <param name="pFromDate"></param>
    ''' <param name="pToDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParamsList(ByVal pPaymnetTypes() As Integer, ByVal pFromDate As Date, ByVal pToDate As Date) As String()()
        Dim paramsList As New List(Of String())

        If pPaymnetTypes.Length > 0 Then
            Dim paramList As New List(Of String)
            For Each paymnetType As Long In pPaymnetTypes
                If paymnetType <> 0 Then
                    paramList.Add("PaymentType = " & paymnetType.ToString)
                End If
            Next
            paramsList.Add(paramList.ToArray)
        End If

        If pFromDate.Date > New Date(1900, 1, 1).Date AndAlso pFromDate.Date <= Date.MaxValue.Date Then
            paramsList.Add(New String() {"PaymentDate >= '" & pFromDate.Date.ToString("yyyy-M-dd") & "'"})
        End If

        If pToDate.Date >= New Date(1900, 1, 1).Date AndAlso pToDate.Date < Date.MaxValue.Date Then
            paramsList.Add(New String() {"PaymentDate <= '" & pToDate.Date.ToString("yyyy-M-dd") & "'"})
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
        sql &= "  ReceiptNumber,"
        sql &= "  PaymentNumber,"
        sql &= "  PaymentAmount,"
        sql &= "  PaymentType,"
        sql &= "  PaymentDate"
        sql &= " From"
        sql &= "  appAdvertiserReceipts"

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
        sql &= "  ReceiptNumber,"
        sql &= "  PaymentNumber,"
        sql &= "  PaymentAmount,"
        sql &= "  PaymentType,"
        sql &= "  PaymentDate"
        sql &= " From"
        sql &= "  appAdvertiserReceipts"

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
    ''' Execute SQL statement to reads the data of the table appAdvertiserReceipts and
    ''' return a list of DAClsAdvertiserReceipts.Struct with each one of the read registries 
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
                    Struct.ReceiptNumber.SetValue(dr.GetDecimal("ReceiptNumber"))
                    Struct.PaymentNumber.SetValue(dr.GetDecimal("PaymentNumber"))
                    Struct.PaymentAmount.SetValue(dr.GetDouble("PaymentAmount"))
                    Struct.PaymentType.SetValue(dr.GetInt32("PaymentType"))
                    Struct.PaymentDate.SetValue(dr.GetDateTime("PaymentDate"))
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

        pStruct.ReceiptNumber.OldValue = pStruct.ReceiptNumber.Value
        pStruct.ReceiptNumber.Value = pStruct.ReceiptNumber.NewValue

        pStruct.PaymentNumber.OldValue = pStruct.PaymentNumber.Value
        pStruct.PaymentNumber.Value = pStruct.PaymentNumber.NewValue

        pStruct.PaymentAmount.OldValue = pStruct.PaymentAmount.Value
        pStruct.PaymentAmount.Value = pStruct.PaymentAmount.NewValue

        pStruct.PaymentType.OldValue = pStruct.PaymentType.Value
        pStruct.PaymentType.Value = pStruct.PaymentType.NewValue

        pStruct.PaymentDate.OldValue = pStruct.PaymentDate.Value
        pStruct.PaymentDate.Value = pStruct.PaymentDate.NewValue

        Return pStruct
    End Function

    ''' <summary>
    ''' Insert a new registry in the table appAdvertiserReceipts and return ID
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the AdvertiserReceipt to insert</param>
    ''' <returns>ID</returns>
    ''' <remarks></remarks>
    Public Shared Function Insert(ByVal pStruct As Struct) As Struct

        'Build SQL value for insert
        Dim sql As String = ""
        sql &= " Insert Into appAdvertiserReceipts"
        sql &= "  ("
        sql &= "    ReceiptNumber,"
        sql &= "    PaymentNumber,"
        sql &= "    PaymentAmount,"
        sql &= "    PaymentType,"
        sql &= "    PaymentDate"
        sql &= "  )"
        sql &= " Values"
        sql &= "  ("
        sql &= "     " & pStruct.ReceiptNumber.NewValue.ToString & ","
        sql &= "     " & pStruct.PaymentNumber.NewValue.ToString & ","
        sql &= "     " & pStruct.PaymentAmount.NewValue.ToString & ","
        sql &= "     " & pStruct.PaymentType.NewValue.ToString & ","
        sql &= "    '" & pStruct.PaymentDate.NewValue.ToString("yyyy-M-dd") & "'"
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
    ''' Update a registry in the table appAdvertiserReceipts and return the number of rows affected
    ''' </summary>
    ''' <param name="pStruct">Struct with data of the AdvertiserReceipt to update</param>
    ''' <returns>Number of rows affected</returns>
    ''' <remarks></remarks>
    Public Shared Function Update(ByVal pStruct As Struct) As Struct
        'Build SQL value
        Dim sql As String = ""
        sql &= " UPDATE appAdvertiserReceipts"
        sql &= " SET"
        sql &= "  ReceiptNumber = " & pStruct.ReceiptNumber.NewValue.ToString & ","
        sql &= "  PaymentNumber = " & pStruct.PaymentNumber.NewValue.ToString & ","
        sql &= "  PaymentAmount = " & pStruct.PaymentAmount.NewValue.ToString & ","
        sql &= "  PaymentType = " & pStruct.PaymentType.NewValue.ToString & ","
        sql &= "  PaymentDate = '" & pStruct.PaymentDate.NewValue.ToString("yyyy-M-dd") & "'"
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
    ''' Delete a registry in the table appAdvertiserReceipts and return the number of rows affected
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub Delete(ByVal pID As Long)

        'Build SQL value
        Dim sql As String = ""
        sql &= " Delete From appAdvertiserReceipts"
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
