
Partial Class frmsAdvertiser_frmAdvertiserProjectStatistics
    Inherits System.Web.UI.Page

    Private wcfrmAdProjectStatistics As SCT.Web.DataFlow.DFClsAdProjectStatistics

    Public Function FindControls(ByVal Id As String) As System.Web.UI.Control
        Return Me.Controls.Item(0).FindControl("ContentPlaceHolder").FindControl(Id)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.wcfrmAdProjectStatistics = New SCT.Web.DataFlow.DFClsAdProjectStatistics(Me)
        Else
            Me.wcfrmAdProjectStatistics = Me.Session("wcFormAdProjectStatistics")
            If Me.wcfrmAdProjectStatistics Is Nothing Then
                Me.wcfrmAdProjectStatistics = New SCT.Web.DataFlow.DFClsAdProjectStatistics(Me)
            Else
                Me.wcfrmAdProjectStatistics.Form = Me
            End If
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        Me.Session("wcFormAdProjectStatistics") = Me.wcfrmAdProjectStatistics
    End Sub

    Protected Sub GridView_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView.PageIndexChanging
        Me.wcfrmAdProjectStatistics.OnPageIndexChanging(e.NewPageIndex)
    End Sub

    Protected Sub GridView_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView.RowCommand
        Select Case e.CommandName
            Case "Project"
                Me.wcfrmAdProjectStatistics.OnProject(CType(CType(CType(sender, GridView).Rows(e.CommandArgument).Cells(0).Controls(0), LinkButton).Text, Long))
            Case Else
        End Select
    End Sub

End Class
