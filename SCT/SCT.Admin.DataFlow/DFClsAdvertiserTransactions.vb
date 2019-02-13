Imports SCT.Library
Imports SCT.Library.AdminSite
Imports System.Web.UI.WebControls

Public Class DFClsAdvertiserTransactions

#Region " Private Fields "

    Private mForm As Object
    Private mDataList As AdTransactionListData

    Private mPageIndex As Integer
    Private mSortDirection As String
    Private mSortExpression As String

    Private mTransactions() As SCT.DataAccess.Transactions
    Private mIDsAdvertisers() As Long
    Private mIDsProjects() As Long
    Private mStartDate As Date
    Private mEndDate As Date

#End Region

#Region " Authorization Rules "

    Public Shared Function CanSelect() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserTransactions") Then
            Return ClsSessionAdmin.CanSelectInForm("frmAdvertiserTransactions")
        Else
            Return ClsSessionAdmin.CanSelectInForm("AllForms")
        End If
    End Function

#End Region

#Region " Properties And Methods "

    Public WriteOnly Property Form() As Object
        Set(ByVal value As Object)
            Me.mForm = value
        End Set
    End Property

    Public Sub New(ByRef form As Object)
        Me.mForm = form
        Me.mForm.Session("ValuePath") = "frmAdvertisers/frmAdvertiserTransactions"

        Me.ApplyPageAuthorizationRules()

        Me.LoadTransactionsList()
        Me.LoadAdvertisersList()
        Me.LoadProjectsList()
        Me.LoadStartDate()
        Me.LoadEndDate()

        Me.SubmitQuery()
    End Sub

    Public Sub ApplyPageAuthorizationRules()
        If (Not ClsSessionAdmin.IsValidForm("frmAdvertiserTransactions", "grdAdvertiserTransactions.ID")) OrElse (Not DFClsAdvertiserTransactions.CanSelect()) Then Me.mForm.Response.Redirect("~/Forms/frmMain.aspx")
    End Sub

    Public Shared Sub ApplyGridRowAuthorizationRules(ByVal gridView As GridView, ByVal row As System.Web.UI.WebControls.GridViewRow)
        If row.RowType = DataControlRowType.DataRow Then
            Dim canSelectAdvertiser As Boolean = DFClsAdvertiserAccount.CanSelect

            row.Cells(0).Controls(1).Visible = canSelectAdvertiser
            row.Cells(0).Controls(2).Visible = Not canSelectAdvertiser

            Dim canSelectProject As Boolean = DFClsAdvertiserProject.CanSelect

            row.Cells(1).Controls(1).Visible = canSelectProject
            row.Cells(1).Controls(2).Visible = Not canSelectProject

            Dim canSelectTransaction As Boolean = (CType(row.Cells(3).Controls(1), LinkButton).CommandName = SCT.DataAccess.Transactions.Invoice AndAlso DFClsAdvertiserTransactionInvoice.CanSelect) OrElse (CType(row.Cells(3).Controls(1), LinkButton).CommandName = SCT.DataAccess.Transactions.Receipt AndAlso DFClsAdvertiserTransactionReceipt.CanSelect)

            row.Cells(3).Controls(1).Visible = canSelectTransaction
            row.Cells(3).Controls(2).Visible = Not canSelectTransaction
        End If
    End Sub

    Public Function ValidateDate(ByVal year As String, ByVal month As String, ByVal day As String) As Boolean
        Return (year = String.Empty AndAlso month = String.Empty AndAlso day = String.Empty) OrElse IsDate(year & "-" & month & "-" & day)
    End Function

    Public Sub LoadTransactionsList()
        Me.LoadList(Me.mForm.FindControls("lstTransactions"), Me.GetListDataSources(New List(Of SCT.DataAccess.Transactions)([Enum].GetValues(GetType(SCT.DataAccess.Transactions))).ToArray), "0")
    End Sub

    Public Sub LoadAdvertisersList()
        Me.LoadList(Me.mForm.FindControls("lstAdvertisers"), Me.GetListDataSources(Me.GetAdvertiserInfoList), "0")
    End Sub

    Public Sub LoadProjectsList()
        Dim AdvertiserIDsSelected As Long() = Me.CollectIDsSelected(Me.mForm.FindControls("lstAdvertisers").Items)
        If AdvertiserIDsSelected.Length = 1 AndAlso AdvertiserIDsSelected(0) <> 0 Then
            Me.LoadList(Me.mForm.FindControls("lstProjects"), Me.GetListDataSources(Me.GetProjectInfoList(AdvertiserIDsSelected(0))), "0")
        Else
            Me.LoadList(Me.mForm.FindControls("lstProjects"), Me.GetListDataSources(New AdProjectData() {}), "0")
        End If
    End Sub

    Public Sub LoadStartDate()
        Me.mForm.FindControls("ddlStartMonth").SelectedValue = Date.Today.Month.ToString("00")
        Me.mForm.FindControls("ddlStartDay").SelectedValue = "01"
        Me.LoadList(Me.mForm.FindControls("ddlStartYear"), Me.GetListDataSources, Date.Today.Year.ToString("0000"))
    End Sub

    Public Sub LoadEndDate()
        Me.mForm.FindControls("ddlEndMonth").SelectedValue = Date.Today.Month.ToString("00")
        Me.mForm.FindControls("ddlEndDay").SelectedValue = Date.Today.Day.ToString("00")
        Me.LoadList(Me.mForm.FindControls("ddlEndYear"), Me.GetListDataSources, Date.Today.Year.ToString("0000"))
    End Sub

    Private Sub LoadList(ByVal list As Object, ByVal data As DataTable, ByVal SelectedValue As String)
        list.DataSource = data
        list.DataTextField = data.Columns(0).Caption
        list.DataValueField = data.Columns(1).Caption
        list.DataBind()
        If list.Items.FindByValue(SelectedValue) IsNot Nothing Then
            list.SelectedValue = SelectedValue
        End If
    End Sub

    Private Function NewListDataTable(Of T)(ByVal text As String, ByVal value As T) As DataTable
        Dim table As New DataTable
        table.Columns.Add("Text", GetType(String))
        table.Columns.Add("Value", GetType(T))
        table.DefaultView.Sort = "Text"
        table.Rows.Add(New Object() {text, value})
        Return table
    End Function

    Private Function GetListDataSources(ByVal InfoList() As AdAccountData) As DataTable
        Dim table As DataTable = Me.NewListDataTable(Of Long)("(All)", 0)
        For Each Info As AdAccountData In InfoList
            table.Rows.Add(New Object() {Info.CompanyName, Info.ID})
        Next
        Return table
    End Function

    Private Function GetListDataSources(ByVal InfoList() As AdProjectData) As DataTable
        Dim table As DataTable = Me.NewListDataTable(Of Long)("(All)", 0)
        For Each Info As AdProjectData In InfoList
            table.Rows.Add(New Object() {Info.ADUrl, Info.ID})
        Next
        Return table
    End Function

    Private Function GetListDataSources(ByVal transactions() As SCT.DataAccess.Transactions) As DataTable
        Dim table As DataTable = Me.NewListDataTable(Of Integer)("(All)", 0)
        For Each transaction As SCT.DataAccess.Transactions In transactions
            table.Rows.Add(New Object() {transaction, transaction})
        Next
        Return table
    End Function

    Private Function GetListDataSources() As DataTable
        Dim table As DataTable = Me.NewListDataTable(Of String)(String.Empty, String.Empty)
        For i As Integer = 2000 To Date.Now.Year
            table.Rows.Add(New Object() {i.ToString, i})
        Next
        Return table
    End Function

    Private Sub LoadGridView(ByVal gridview As GridView)
        Me.AddRowData(gridview)
        Me.AddSortImage(gridview)
        Me.AddFooterData(gridview)
    End Sub

    Private Sub AddRowData(ByVal GridView As System.Web.UI.WebControls.GridView)
        GridView.PageIndex = Me.mPageIndex
        GridView.DataSource = Me.GetDataSources()
        GridView.DataBind()
    End Sub

    Private Function GetDataSources() As DataTable
        Dim table As New DataTable

        table.Columns.Add("IDAdvertiser", GetType(Long))
        table.Columns.Add("Advertiser", GetType(String))
        table.Columns.Add("IDProject", GetType(Long))
        table.Columns.Add("Project", GetType(String))
        table.Columns.Add("IDTransaction", GetType(Long))
        table.Columns.Add("Transaction", GetType(Integer))
        table.Columns.Add("TransactionNumber", GetType(Decimal))
        table.Columns.Add("TransactionDate", GetType(Date))
        table.Columns.Add("InvoiceAmount", GetType(String))
        table.Columns.Add("ReceiptAmount", GetType(String))

        table.DefaultView.Sort = Me.GetSortProperty

        For Each transaction As AdTransactionData In Me.mDataList.TransactionInvoices.TransactionInvoices
            table.Rows.Add(New Object() {transaction.Advertiser.ID, transaction.Advertiser.CompanyName, transaction.Project.ID, transaction.Project.ADUrl, transaction.ID, transaction.TransactionType, transaction.TransactionNumber, transaction.TransactionDate, transaction.TransactionAmount.ToString("#,###,##0.00"), String.Empty})
        Next

        For Each transaction As AdTransactionData In Me.mDataList.TransactionReceipts.TransactionReceipts
            table.Rows.Add(New Object() {transaction.Advertiser.ID, transaction.Advertiser.CompanyName, transaction.Project.ID, transaction.Project.ADUrl, transaction.ID, transaction.TransactionType, transaction.TransactionNumber, transaction.TransactionDate, String.Empty, transaction.TransactionAmount.ToString("#,###,##0.00")})
        Next

        Return table
    End Function

    Private Sub AddFooterData(ByVal GridView As System.Web.UI.WebControls.GridView)
        If GridView.FooterRow IsNot Nothing Then
            GridView.FooterRow.Cells(4).Text = Me.mDataList.TransactionInvoices.TransactionInvoicesTotal.ToString("#,###,##0.00")
            GridView.FooterRow.Cells(5).Text = Me.mDataList.TransactionReceipts.TransactionReceiptsTotal.ToString("#,###,##0.00")
        End If
    End Sub

    Private Sub AddSortImage(ByVal GridView As System.Web.UI.WebControls.GridView)
        If GridView.HeaderRow IsNot Nothing Then
            Dim Image As New Image()
            Dim ColumnIndex As Integer = GetSortColumnIndex(GridView)
            If ColumnIndex <> -1 Then
                Image.ImageUrl = "~/Images/" & Me.mSortDirection & ".GIF"
                GridView.HeaderRow.Cells(ColumnIndex).Controls.Add(Image)
            End If
        End If
    End Sub

    Private Function GetSortColumnIndex(ByVal GridView As System.Web.UI.WebControls.GridView) As Integer
        For Each field As DataControlField In GridView.Columns
            If field.SortExpression = Me.mSortExpression Then
                Return GridView.Columns.IndexOf(field)
            End If
        Next
        Return -1
    End Function

    Private Function GetSortProperty() As String
        Dim sortProperty As String = Me.mSortExpression & " " & Me.mSortDirection

        Select Case Me.mSortExpression
            Case "Advertiser"
                Return sortProperty & ", Project " & Me.mSortDirection & ", TransactionDate " & Me.mSortDirection & ", TransactionNumber " & Me.mSortDirection
            Case "Project"
                Return sortProperty & ", Advertiser " & Me.mSortDirection & ", TransactionDate " & Me.mSortDirection & ", TransactionNumber " & Me.mSortDirection
            Case "TransactionDate"
                Return sortProperty & ", Advertiser " & Me.mSortDirection & ", Project " & Me.mSortDirection & ", TransactionNumber " & Me.mSortDirection
            Case "TransactionNumber"
                Return sortProperty & ", Advertiser " & Me.mSortDirection & ", Project " & Me.mSortDirection & ", TransactionDate " & Me.mSortDirection
            Case Else
                Return sortProperty
        End Select
    End Function

    Public Sub SubmitQuery()
        Me.CollectFiltersValuesSelected()
        Me.mDataList = Me.GetDataList(Me.mTransactions, Me.mIDsAdvertisers, Me.mIDsProjects, Me.mStartDate, Me.mEndDate)
        Me.PopulateDataList("Advertiser", "ASC", 0)
    End Sub

    Public Sub OnSorting(ByVal Sortexpression As String, ByVal PageIndex As Integer)
        If Me.mSortExpression <> Sortexpression Then
            Me.PopulateDataList(Sortexpression, "ASC", PageIndex)
        Else
            If Me.mSortDirection = "ASC" Then
                Me.PopulateDataList(Sortexpression, "DESC", PageIndex)
            Else
                Me.PopulateDataList(Sortexpression, "ASC", PageIndex)
            End If
        End If
    End Sub

    Public Sub GoAdvertiserAccount(ByVal advertiserID As Long)
        Me.mForm.Session("ID") = advertiserID
        Me.mForm.Response.Redirect("~/Forms/frmAdvertiserAccount.aspx")
    End Sub

    Public Sub GoAdvertiserProject(ByVal projectID As Long)
        Me.mForm.Session("ID") = projectID
        Me.mForm.Response.Redirect("~/Forms/frmAdvertiserProject.aspx")
    End Sub

    Public Sub GoTransactionDetail(ByVal transacionID As Long, ByVal transactionType As DataAccess.Transactions)
        Me.mForm.Session("ID") = transacionID
        If transactionType = SCT.DataAccess.Transactions.Invoice Then
            Me.mForm.Response.Write("<script>window.open('frmAdvertiserTransactionInvoice.aspx','_blank','width=675,height=575,scrollbars=YES,resizable=YES,status=YES');</script>")
        ElseIf transactionType = SCT.DataAccess.Transactions.Receipt Then
            Me.mForm.Response.Write("<script>window.open('frmAdvertiserTransactionReceipt.aspx','_blank','width=825,height=575,scrollbars=YES,resizable=YES,status=YES');</script>")
        End If
    End Sub

    Public Sub OnPageIndexChanging(ByVal NewPageIndex As Integer)
        Me.PopulateDataList(NewPageIndex)
    End Sub

    Private Sub CollectFiltersValuesSelected()
        Me.mTransactions = Me.CollectTransactionsSelected(Me.mForm.FindControls("lsttransactions").Items)
        Me.mIDsAdvertisers = Me.CollectIDsSelected(Me.mForm.FindControls("lstAdvertisers").Items)
        Me.mIDsProjects = Me.CollectIDsSelected(Me.mForm.FindControls("lstProjects").Items)
        Me.mStartDate = Me.CollectStartDate(Me.mForm.FindControls("ddlStartYear").Text, Me.mForm.FindControls("ddlStartMonth").SelectedValue, Me.mForm.FindControls("ddlStartDay").SelectedValue)
        Me.mEndDate = Me.CollectEndDate(Me.mForm.FindControls("ddlEndYear").Text, Me.mForm.FindControls("ddlEndMonth").SelectedValue, Me.mForm.FindControls("ddlEndDay").SelectedValue)
    End Sub

    Private Function CollectTransactionsSelected(ByVal filterList As ListItemCollection) As SCT.DataAccess.Transactions()
        If filterList(0).Selected Then
            Return New SCT.DataAccess.Transactions() {filterList(0).Value}
        Else
            Dim logsSelected As New List(Of SCT.DataAccess.Transactions)
            For Each item As ListItem In filterList
                If item.Selected Then
                    logsSelected.Add(item.Value)
                End If
            Next
            Return logsSelected.ToArray
        End If
    End Function

    Private Function CollectIDsSelected(ByVal filterList As ListItemCollection) As Long()
        If filterList(0).Selected Then
            Return New Long() {filterList(0).Value}
        Else
            Dim idsSelected As New List(Of Long)
            For Each item As ListItem In filterList
                If item.Selected Then
                    idsSelected.Add(item.Value)
                End If
            Next
            Return idsSelected.ToArray
        End If
    End Function

    Private Function CollectStartDate(ByVal year As String, ByVal month As String, ByVal day As String) As Date
        If year = String.Empty AndAlso month = String.Empty AndAlso day = String.Empty Then
            Return New Date(1900, 1, 1)
        Else
            Return New Date(year, month, day)
        End If
    End Function

    Private Function CollectEndDate(ByVal year As String, ByVal month As String, ByVal day As String) As Date
        If year = String.Empty AndAlso month = String.Empty AndAlso day = String.Empty Then
            Return Date.MaxValue.Date
        Else
            Return New Date(year, month, day)
        End If
    End Function

#End Region

#Region " Data Methods "

    Private Sub PopulateDataList(ByVal SortExpression As String, ByVal SortDirection As String, ByVal PageIndex As Integer)
        Me.mPageIndex = PageIndex
        Me.mSortDirection = SortDirection
        Me.mSortExpression = SortExpression
        Me.LoadGridView(Me.mForm.FindControls("grdAdvertiserTransactions"))
    End Sub

    Private Sub PopulateDataList(ByVal PageIndex As Integer)
        Me.mPageIndex = PageIndex
        Me.LoadGridView(Me.mForm.FindControls("grdAdvertiserTransactions"))
    End Sub

    Private Function GetDataList(ByVal transactions() As SCT.DataAccess.Transactions, ByVal idAdvertisers As Long(), ByVal idProjects As Long(), ByVal startDate As Date, ByVal endDate As Date) As AdTransactionListData
        Try
            Return ClsSessionAdmin.GetAdTransactionList(transactions, idAdvertisers, idProjects, startDate, endDate)
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return New AdTransactionListData
        End Try
    End Function

    Private Function GetAdvertiserInfoList() As AdAccountData()
        Try
            Return ClsSessionAdmin.GetAdAccountInfoList
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return New AdAccountData() {}
        End Try
    End Function

    Private Function GetProjectInfoList(ByVal idAdvertiser As Long) As AdProjectData()
        Try
            Return ClsSessionAdmin.GetAdProjectInfoList(idAdvertiser)
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return New AdProjectData() {}
        End Try
    End Function

#End Region

End Class
