
Partial Class Forms_frmBinnacleFormEntry
    Inherits System.Web.UI.Page

    Private wcForm As SCT.Admin.DataFlow.DFClsBinnacleFormEntry

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.wcForm = New SCT.Admin.DataFlow.DFClsBinnacleFormEntry(Me)
        End If
    End Sub

End Class
