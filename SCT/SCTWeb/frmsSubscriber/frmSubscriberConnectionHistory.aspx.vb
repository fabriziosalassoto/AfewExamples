
Partial Class frmsSubscriber_frmSubscriberConnectionHistory
    Inherits System.Web.UI.Page

    Private wcfrmSubConnectionHistory As SCT.Web.DataFlow.DFClsSubConnectionHistory

    Public Function FindControls(ByVal Id As String) As System.Web.UI.Control
        Return Me.Controls.Item(0).FindControl("ContentPlaceHolder").FindControl(Id)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.wcfrmSubConnectionHistory = New SCT.Web.DataFlow.DFClsSubConnectionHistory(Me)
        Else
            Me.wcfrmSubConnectionHistory = Me.Session("wcFormSubConnectionHistory")
            If Me.wcfrmSubConnectionHistory Is Nothing Then
                Me.wcfrmSubConnectionHistory = New SCT.Web.DataFlow.DFClsSubConnectionHistory(Me)
            Else
                Me.wcfrmSubConnectionHistory.Form = Me
            End If
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        Me.Session("wcFormSubConnectionHistory") = Me.wcfrmSubConnectionHistory
    End Sub

    Protected Sub GridView_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView.PageIndexChanging
        Me.wcfrmSubConnectionHistory.OnPageIndexChanging(e.NewPageIndex)
    End Sub
End Class
