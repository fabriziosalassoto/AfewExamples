
Partial Class frmsSubscriber_frmSubscriberStolenReportsHistory
    Inherits System.Web.UI.Page

    Private wcfrmSubStolenReportsHistory As SCT.Web.DataFlow.DFClsSubStolenReportsHistory

    Public Function FindControls(ByVal Id As String) As System.Web.UI.Control
        Return Me.Controls.Item(0).FindControl("ContentPlaceHolder").FindControl(Id)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.wcfrmSubStolenReportsHistory = New SCT.Web.DataFlow.DFClsSubStolenReportsHistory(Me)
        Else
            Me.wcfrmSubStolenReportsHistory = Me.Session("wcFormSubStolenReportsHistory")
            If Me.wcfrmSubStolenReportsHistory Is Nothing Then
                Me.wcfrmSubStolenReportsHistory = New SCT.Web.DataFlow.DFClsSubStolenReportsHistory(Me)
            Else
                Me.wcfrmSubStolenReportsHistory.Form = Me
            End If
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        Me.Session("wcFormSubStolenReportsHistory") = Me.wcfrmSubStolenReportsHistory
    End Sub

    Protected Sub GridView_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView.PageIndexChanging
        Me.wcfrmSubStolenReportsHistory.OnPageIndexChanging(e.NewPageIndex)
    End Sub

End Class
