
Partial Class frmsSubscriber_frmSubscriberStolenReport
    Inherits System.Web.UI.Page

    Private wcfrmSubStolenReport As SCT.Web.DataFlow.DFClsSubStolenReport

    Public Function FindControls(ByVal Id As String) As System.Web.UI.Control
        Return Me.Controls.Item(0).FindControl("ContentPlaceHolder").FindControl(Id)
    End Function
   
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.wcfrmSubStolenReport = New SCT.Web.DataFlow.DFClsSubStolenReport(Me)
        Else
            Me.wcfrmSubStolenReport = Me.Session("wcFormSubStolenReport")
            If Me.wcfrmSubStolenReport Is Nothing Then
                Me.wcfrmSubStolenReport = New SCT.Web.DataFlow.DFClsSubStolenReport(Me)
            Else
                Me.wcfrmSubStolenReport.Form = Me
            End If
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        Me.Session("wcFormSubStolenReport") = Me.wcfrmSubStolenReport
    End Sub

    Protected Sub cmdReturn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdReturn.Click
        Me.wcfrmSubStolenReport.OnReurn()
    End Sub

    Protected Sub cmdOk_AddClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOk.AddClicked
        Me.wcfrmSubStolenReport.OnOk_Add()
    End Sub

    Protected Sub cmdOk_SaveClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOk.SaveClicked
        Me.wcfrmSubStolenReport.OnOk_Edit()
    End Sub

    Protected Sub cmdApply_AddClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdApply.AddClicked
        Me.wcfrmSubStolenReport.OnApply_Add()
    End Sub

    Protected Sub cmdApply_SaveClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdApply.SaveClicked
        Me.wcfrmSubStolenReport.OnApply_Edit()
    End Sub

End Class
