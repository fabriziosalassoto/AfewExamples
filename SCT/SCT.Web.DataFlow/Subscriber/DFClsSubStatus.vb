Public Class DFClsSubStatus

#Region " Private Fields "
    Private mForm As Object
    Private mData As SCTServer.ClsSubscriberAccountData
#End Region

#Region " Properties And Methods "

    Public WriteOnly Property Form() As Object
        Set(ByVal value As Object)
            Me.mForm = value
        End Set
    End Property

    Public Sub New(ByRef form As Object)
        Me.mForm = form
        Me.mForm.Session("ValuePath") = "frmSubscriberStatus"

        Me.mData = Me.GetData(Mid(Csla.ApplicationContext.User.Identity.Name, 1, Csla.ApplicationContext.User.Identity.Name.IndexOf(";")))

        Me.ApplyUserAuthenticationRules()
       
        Me.LoadGridView()
    End Sub

    Private Sub ApplyUserAuthenticationRules()
        Dim hostLocalIP As String = String.Empty
        Dim hostPublicIP As String = String.Empty

        If Me.mForm.Request.IsLocal Then
            hostLocalIP = Me.mForm.Request.ServerVariables("LOCAL_ADDR")
            hostPublicIP = Me.mForm.Request.ServerVariables("LOCAL_ADDR")
        Else
            Dim host As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(Me.mForm.Request.ServerVariables("REMOTE_ADDR"))
            If host.AddressList.Length = 1 Then
                hostLocalIP = host.AddressList(0).ToString
                hostPublicIP = host.AddressList(0).ToString
            ElseIf host.AddressList.Length = 2 Then
                hostLocalIP = host.AddressList(0).ToString
                hostPublicIP = host.AddressList(1).ToString
            Else
                hostLocalIP = String.Empty
                hostPublicIP = String.Empty
            End If
        End If

        Me.mForm.FindControls("GridView").Columns(4).Visible = Me.mData IsNot Nothing AndAlso Me.mData.Connected
        Me.mForm.FindControls("GridView").Columns(6).Visible = Me.mData IsNot Nothing AndAlso Me.mData.InstallClientProgram AndAlso Me.mData.Connected AndAlso (Not Me.mData.ConfigurationHasChanged) AndAlso Me.mData.ConnectionHistory IsNot Nothing AndAlso Me.mData.ConnectionHistory.HostIP = hostPublicIP AndAlso Me.mData.ConnectionHistory.HostLocalIP = hostLocalIP
        Me.mForm.FindControls("GridView").Columns(7).Visible = Me.mData IsNot Nothing AndAlso (Not Me.mData.InstallClientProgram)
    End Sub

    Private Sub LoadGridView()
        Me.mForm.FindControls("GridView").DataSource = GetDataSources()
        Me.mForm.FindControls("GridView").DataBind()
    End Sub

    Private Function GetDataSources() As DataTable
        Dim serialNbr As String = ""
        Dim status As String = "Disconnected"
        Dim hostIP As String = "..."
        Dim alert As String = "Deactivated"

        Dim table As New DataTable
        table.Columns.Add("SerialNbr", GetType(String))
        table.Columns.Add("Status", GetType(String))
        table.Columns.Add("IPAddress", GetType(String))
        table.Columns.Add("Alert", GetType(String))

        If Me.mData IsNot Nothing Then
            serialNbr = Me.mData.ComputerSerialNumber
            If Me.mData.Connected AndAlso Me.mData.ConnectionHistory IsNot Nothing Then
                status = "Connected"
                hostIP = Me.mData.ConnectionHistory.HostIP
            End If
            If Me.mData.StolenReport IsNot Nothing Then
                alert = "Activated"
            End If
            table.Rows.Add(New Object() {serialNbr, status, hostIP, alert})
        End If

        Return table
    End Function

    Public Sub InstallClientProgram()
        Me.mForm.Response.Redirect("~/DownloadAplication/NBDtor-Install.exe")
    End Sub

    Public Sub UninstallClientProgram()
        Using server As New SCTServer.SCTServer
            Try
                server.UninstallClientProgram(Me.mData.ID)
            Catch SysEx As Exception
                Me.mForm.FindControls("MsgBox").ShowMessage(SysEx.Message)
            End Try
        End Using
    End Sub

    Public Sub GoToStolenReport()
        Me.mForm.Response.Redirect("~/frmsSubscriber/frmSubscriberStolenReport.aspx")
    End Sub

    Public Sub GoToComputerLocation()
        Me.mForm.Response.Redirect("~/frmsSubscriber/frmSubscriberLocation.aspx")
    End Sub


#End Region

#Region " Data Methods "

    Private Function GetData(ByVal accountId As Long) As SCTServer.ClsSubscriberAccountData
        Using server As New SCTServer.SCTServer
            Try
                Return server.GetSubscriberAccount(accountId)
            Catch SysEx As Exception
                Me.mForm.FindControls("MsgBox").ShowMessage(SysEx.Message)
                Return Nothing
            End Try
        End Using
    End Function

#End Region

End Class
