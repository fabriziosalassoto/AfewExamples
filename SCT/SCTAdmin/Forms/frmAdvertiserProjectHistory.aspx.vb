
Partial Class Forms_frmAdvertiserProjectHistory
    Inherits System.Web.UI.Page

    Private wcForm As SCT.Admin.DataFlow.DFClsAdvertiserProjectHistory

    Public Function FindControls(ByVal Id As String) As System.Web.UI.Control
        Return Me.Controls.Item(0).FindControl("ContentPlaceHolder").FindControl(Id)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.wcForm = New SCT.Admin.DataFlow.DFClsAdvertiserProjectHistory(Me)
        Else
            If SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsForm Is Nothing OrElse SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsForm.GetType().Name <> "DFClsAdvertiserProjectHistory" Then
                Me.wcForm = New SCT.Admin.DataFlow.DFClsAdvertiserProjectHistory(Me)
            Else
                Me.wcForm = SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsForm
                Me.wcForm.Form = Me
            End If
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsForm = Me.wcForm
    End Sub

    Protected Sub lstAdAccounts_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstAdAccounts.SelectedIndexChanged
        Me.wcForm.LoadProjectsList("0")
    End Sub

    Protected Sub csvStartDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvStartDate.ServerValidate
        args.IsValid = Me.wcForm.ValidateStartDate
    End Sub

    Protected Sub csvEndDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvEndDate.ServerValidate
        args.IsValid = Me.wcForm.ValidateEndDate
    End Sub

    Protected Sub csvStartTime_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvStartTime.ServerValidate
        args.IsValid = Me.wcForm.ValidateStartTime
    End Sub

    Protected Sub csvEndTime_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvEndTime.ServerValidate
        args.IsValid = Me.wcForm.ValidateEndTime
    End Sub

    Protected Sub mnuSubmit_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles mnuSubmit.MenuItemClick
        Page.Validate()
        If Page.IsValid Then
            Me.wcForm.OnSubmitQuery()
        End If
    End Sub

    Protected Sub grdProjectHistory_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdProjectHistory.RowDataBound
        SCT.Admin.DataFlow.DFClsAdvertiserProjectHistory.ApplyGridRowAuthorizationRules(CType(sender, GridView), e.Row)
        SCT.Admin.DataFlow.DFClsAdvertiserProjectHistory.HidenMinDate(CType(sender, GridView), e.Row)
    End Sub

    Protected Sub grdProjectHistory_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdProjectHistory.Sorting
        Me.wcForm.OnSorting(e.SortExpression, CType(sender, GridView).PageIndex)
    End Sub

    Protected Sub lnkAdvertiser_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.wcForm.OnAdAccount(CType(sender, LinkButton).CommandArgument)
    End Sub

    Protected Sub lnkProject_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.wcForm.OnProject(CType(sender, LinkButton).CommandArgument)
    End Sub

    Protected Sub grdProjectHistory_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdProjectHistory.PageIndexChanging
        Me.wcForm.OnPageIndexChanging(e.NewPageIndex)
    End Sub

End Class
