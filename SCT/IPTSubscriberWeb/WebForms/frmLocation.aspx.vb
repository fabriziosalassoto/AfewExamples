Imports SCT.Library
Imports System.Data

Partial Class WebForms_frmLocation
    Inherits System.Web.UI.Page

    Dim mData As Subscriber.ClsAccount
    Dim mConnectionData As Server.ClsConnectedSubscriber

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Session("ValuePath") = "Location"
        Me.mData = Me.GetData(Mid(Csla.ApplicationContext.User.Identity.Name, 1, Csla.ApplicationContext.User.Identity.Name.IndexOf(";")))
        Me.mConnectionData = Me.GetConnectionData(Mid(Csla.ApplicationContext.User.Identity.Name, 1, Csla.ApplicationContext.User.Identity.Name.IndexOf(";")))
        Me.LoadGridView()
    End Sub

    Private Function GetData(ByVal accountId As Long) As Subscriber.ClsAccount
        Try
            Return Subscriber.ClsAccount.GetAccount(accountId)
        Catch DataEx As Csla.DataPortalException
            Me.MsgBox.ShowMessage(DataEx.BusinessException.Message)
            Return Nothing
        Catch SysEx As Exception
            Me.MsgBox.ShowMessage(SysEx.Message)
            Return Nothing
        End Try
    End Function

    Private Function GetConnectionData(ByVal accountId As Long) As Server.ClsConnectedSubscriber
        Using svc As New Server.SCTService
            Try
                Return svc.GetConnectedSubscriber(accountId)
            Catch DataEx As Csla.DataPortalException
                'Me.MsgBox.ShowMessage(DataEx.BusinessException.Message)
                Return Nothing
            Catch SysEx As Exception
                'Me.MsgBox.ShowMessage(SysEx.Message)
                Return Nothing
            End Try
        End Using

    End Function

    Private Sub LoadGridView()
        GridView.DataSource = GetDataSources()
        GridView.DataBind()
    End Sub

    Private Function GetDataSources() As DataTable
        Dim table As New DataTable
        Dim dataRow() As Object

        table.Columns.Add("ID", GetType(String))
        table.Columns.Add("SerialNbr", GetType(String))
        table.Columns.Add("Status", GetType(String))
        table.Columns.Add("IPAddress", GetType(String))
        table.Columns.Add("StolenReport", GetType(String))

        If Me.mData IsNot Nothing Then
            If Me.mConnectionData IsNot Nothing Then
                dataRow = New Object() {Me.mData.ID, Me.mData.ComputerSerialNumber, "Connected", Me.mConnectionData.HostIP, ""}
            Else
                dataRow = New Object() {Me.mData.ID, Me.mData.ComputerSerialNumber, "Disconnected", "...", ""}
            End If
            If Me.mData.GetStolenReport IsNot Nothing Then
                dataRow(4) = "YES"
            Else
                dataRow(4) = "NO"
            End If
            table.Rows.Add(dataRow)
        End If
        Return table
    End Function

End Class
