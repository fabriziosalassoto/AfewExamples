
Partial Class frmsSubscriber_frmSubscriberLocation
    Inherits System.Web.UI.Page

    Private wcfrmSubStatus As SCT.Web.DataFlow.DFClsSubStatus

    Public Function FindControls(ByVal Id As String) As System.Web.UI.Control
        Return Me.Controls.Item(0).FindControl("ContentPlaceHolder").FindControl(Id)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.wcfrmSubStatus = New SCT.Web.DataFlow.DFClsSubStatus(Me)
        Else
            Me.wcfrmSubStatus = Me.Session("wcFormSubStatus")
            If Me.wcfrmSubStatus Is Nothing Then
                Me.wcfrmSubStatus = New SCT.Web.DataFlow.DFClsSubStatus(Me)
            Else
                Me.wcfrmSubStatus.Form = Me
            End If
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        Me.Session("wcFormSubStatus") = Me.wcfrmSubStatus
    End Sub

    Protected Sub GridView_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView.RowCommand
        Select Case e.CommandName
            Case "StolenReport"
                Me.wcfrmSubStatus.GoToStolenReport()
            Case "Location"
                Me.wcfrmSubStatus.GoToComputerLocation()
            Case "Install"
                Me.wcfrmSubStatus.InstallClientProgram()
            Case "Uninstall"
                Me.wcfrmSubStatus.UninstallClientProgram()
            Case Else
        End Select
    End Sub
End Class
