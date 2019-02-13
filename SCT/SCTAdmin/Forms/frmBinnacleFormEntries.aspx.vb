
Partial Class Forms_frmBinnacleFormEntries
    Inherits System.Web.UI.Page

    Private wcForm As SCT.Admin.DataFlow.DFClsBinnacleFormEntries

    Public Function FindControls(ByVal Id As String) As System.Web.UI.Control
        Return Me.Controls.Item(0).FindControl("ContentPlaceHolder").FindControl(Id)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.wcForm = New SCT.Admin.DataFlow.DFClsBinnacleFormEntries(Me)
        Else
            If SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsForm Is Nothing OrElse SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsForm.GetType.Name <> "DFClsBinnacleFormEntries" Then
                Me.wcForm = New SCT.Admin.DataFlow.DFClsBinnacleFormEntries(Me)
            Else
                Me.wcForm = SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsForm
                Me.wcForm.Form = Me
            End If
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsForm = Me.wcForm
    End Sub

    Protected Sub grdBinnacleFormEntries_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdBinnacleFormEntries.PageIndexChanging
        Me.wcForm.ShowNextGridPage(e.NewPageIndex)
    End Sub

    Protected Sub cmdDetail_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Me.wcForm.ShowBinnacleFormEntry(CType(sender, ImageButton).CommandArgument, CType(sender, ImageButton).CommandName)
    End Sub

    Protected Sub lstLogs_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstLogs.SelectedIndexChanged
        Me.wcForm.LoadFilters()
    End Sub

    Protected Sub cklFilter_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cklFilter.SelectedIndexChanged
        Me.wcForm.SelectFilter(sender)
    End Sub

    Protected Sub vldStartDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles vldStartDate.ServerValidate
        args.IsValid = Me.wcForm.ValidateDate(Me.ddlStartYear.SelectedValue, Me.ddlStartMonth.SelectedValue, Me.ddlStartDay.SelectedValue)
    End Sub

    Protected Sub vldEndDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles vldEndDate.ServerValidate
        args.IsValid = Me.wcForm.ValidateDate(Me.ddlEndYear.SelectedValue, Me.ddlEndMonth.SelectedValue, Me.ddlEndDay.SelectedValue)
    End Sub

    Protected Sub mnuSubmitQuery_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles mnuSubmitQuery.MenuItemClick
        Page.Validate()
        Me.wcForm.ClearQuery()
        If Page.IsValid Then
            Me.wcForm.SubmitQuery()
        End If
    End Sub

End Class
