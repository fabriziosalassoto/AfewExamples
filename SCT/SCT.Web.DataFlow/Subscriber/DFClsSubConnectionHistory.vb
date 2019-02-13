Imports SCT.Library

Public Class DFClsSubConnectionHistory

#Region " Private Fields "
    Private mForm As Object
    Private mDataList As Csla.SortedBindingList(Of Subscriber.ClsConnectionHistory)
#End Region

#Region " Properties And Methods "

    Public WriteOnly Property Form() As Object
        Set(ByVal value As Object)
            Me.mForm = value
        End Set
    End Property

    Public Sub New(ByRef form As Object)
        Me.mForm = form
        Me.mForm.Session("ValuePath") = "frmSubscriberConnectionHistory"
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
        table.Columns.Add("IPAddress", GetType(String))
        table.Columns.Add("Date", GetType(String))
        table.Columns.Add("Time", GetType(String))
        table.Columns.Add("DNS", GetType(String))
        table.Columns.Add("State", GetType(String))
        table.Columns.Add("City", GetType(String))
        table.Columns.Add("Activity", GetType(String))

        For Each data As Subscriber.ClsConnectionHistory In Me.mDataList
            With data
                table.Rows.Add(New Object() {.ID.ToString, .HostIP, .ConnectionDate.ToString("MMMM dd, yyyy"), .ConnectionTime.ToString("hh:mm tt"), .DNSResolutionIP, .IPState, .IPCity, .ActivityStatus})
            End With
        Next
        Return table
    End Function

    Public Sub OnPageIndexChanging(ByVal NewPageIndex As Integer)
        Me.PopulateDataList(NewPageIndex)
    End Sub

#End Region

#Region " Data Methods "

    Private Sub PopulateDataList(ByVal PageIndex As Integer)
        Me.LoadGridView(Me.mForm.FindControls("GridView"), PageIndex)
    End Sub

    Private Function GetDataList(ByVal subscriberId As Long) As Csla.SortedBindingList(Of Subscriber.ClsConnectionHistory)
        Try
            Return New Csla.SortedBindingList(Of Subscriber.ClsConnectionHistory)(Subscriber.ClsConnectionHistoryList.GetConnectionHistoryList(subscriberId, Date.MinValue, Date.MaxValue))
        Catch DataEx As Csla.DataPortalException
            Me.mForm.FindControls("MsgBox").ShowMessage(DataEx.BusinessException.Message)
            Return New Csla.SortedBindingList(Of Subscriber.ClsConnectionHistory)(Subscriber.ClsConnectionHistoryList.NewConnectionHistoryList)
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage(SysEx.Message)
            Return New Csla.SortedBindingList(Of Subscriber.ClsConnectionHistory)(Subscriber.ClsConnectionHistoryList.NewConnectionHistoryList)
        End Try
    End Function

#End Region

End Class
