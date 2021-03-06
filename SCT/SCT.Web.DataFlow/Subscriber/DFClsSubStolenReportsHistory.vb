Imports SCT.Library

Public Class DFClsSubStolenReportsHistory

#Region " Private Fields "
    Private mForm As Object
    Private mDataList As Csla.SortedBindingList(Of Subscriber.ClsStolenReport)
#End Region

#Region " Properties And Methods "

    Public WriteOnly Property Form() As Object
        Set(ByVal value As Object)
            Me.mForm = value
        End Set
    End Property

    Public Sub New(ByRef form As Object)
        Me.mForm = form
        Me.mForm.Session("ValuePath") = "frmSubscriberStolenReportsHistory"
        Me.mDataList = Me.GetDataList(Mid(Csla.ApplicationContext.User.Identity.Name, 1, Csla.ApplicationContext.User.Identity.Name.IndexOf(";")))
        Me.PopulateDataList(0)
    End Sub

    Private Sub LoadGridView(ByVal GridView As System.Web.UI.WebControls.GridView, ByVal PageIndex As Integer)
        GridView.PageIndex = PageIndex
        GridView.DataSource = GetDataSources()
        GridView.DataBind()
    End Sub

    Private Function GetDataSources() As DataTable
        Dim table As New DataTable

        table.Columns.Add("ID", GetType(String))
        table.Columns.Add("DateMissing", GetType(String))
        table.Columns.Add("LastKnownLocation", GetType(String))
        table.Columns.Add("DateFound", GetType(String))
        table.Columns.Add("Action", GetType(String))
        table.Columns.Add("Active", GetType(String))

        For Each data As Subscriber.ClsStolenReport In Me.mDataList
            With data
                table.Rows.Add(New Object() {.ID.ToString, Me.GetDateFormat(.DateReportMissing), .LastKnownLocationDescription, Me.GetDateFormat(.DateReportFound), Me.GetActionDescription(.ActionToTake.ToString), Me.GetCheckedSymbol(.ActiveForAlerts)})
            End With
        Next
        Return table
    End Function

    Private Function GetDateFormat(ByVal dateValue As Date) As String
        If Not dateValue = "1900-01-01" Then
            Return Format(dateValue, "MMMM dd, yyyy")
        Else
            Return String.Empty
        End If
    End Function

    Private Function GetActionDescription(ByVal action As String) As String
        Dim table As New DataTable
        table.ReadXml(Me.mForm.MapPath("~") & "/App_Data/Actions.xml")
        table.DefaultView.RowFilter = "Value = '" & action & "'"
        For Each row As DataRowView In table.DefaultView
            Return row.Item("Text")
        Next
        Return String.Empty
    End Function

    Private Function GetCheckedSymbol(ByVal value As Boolean) As String
        If value Then
            Return "√"
        Else
            Return String.Empty
        End If
    End Function

    Public Sub OnPageIndexChanging(ByVal NewPageIndex As Integer)
        Me.PopulateDataList(NewPageIndex)
    End Sub

#End Region

#Region " Data Methods "

    Private Sub PopulateDataList(ByVal PageIndex As Integer)
        Me.LoadGridView(Me.mForm.FindControls("GridView"), PageIndex)
    End Sub

    Private Function GetDataList(ByVal subscriberId As Long) As Csla.SortedBindingList(Of Subscriber.ClsStolenReport)
        Try
            Return New Csla.SortedBindingList(Of Subscriber.ClsStolenReport)(Subscriber.ClsStolenReportList.GetStolenReportList(subscriberId, Date.MinValue, Date.MaxValue))
        Catch DataEx As Csla.DataPortalException
            Me.mForm.FindControls("MsgBox").ShowMessage(DataEx.BusinessException.Message)
            Return New Csla.SortedBindingList(Of Subscriber.ClsStolenReport)(Subscriber.ClsStolenReportList.NewStolenReportList)
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage(SysEx.Message)
            Return New Csla.SortedBindingList(Of Subscriber.ClsStolenReport)(Subscriber.ClsStolenReportList.NewStolenReportList)
        End Try
    End Function

#End Region

End Class
