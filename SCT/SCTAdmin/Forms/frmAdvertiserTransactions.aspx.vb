
Partial Class Forms_frmAdvertiserTransactions
    Inherits System.Web.UI.Page

    Private wcForm As SCT.Admin.DataFlow.DFClsAdvertiserTransactions

    Public Function FindControls(ByVal Id As String) As System.Web.UI.Control
        Return Me.Controls.Item(0).FindControl("ContentPlaceHolder").FindControl(Id)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.wcForm = New SCT.Admin.DataFlow.DFClsAdvertiserTransactions(Me)
        Else
            If SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsForm Is Nothing OrElse SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsForm.GetType.Name <> "DFClsAdvertiserTransactions" Then
                Me.wcForm = New SCT.Admin.DataFlow.DFClsAdvertiserTransactions(Me)
            Else
                Me.wcForm = SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsForm
                Me.wcForm.Form = Me
            End If
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsForm = Me.wcForm
    End Sub

    Protected Sub lstAdvertisers_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstAdvertisers.SelectedIndexChanged
        Me.wcForm.LoadProjectsList()
    End Sub

    Protected Sub vldStartDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles vldStartDate.ServerValidate
        args.IsValid = Me.wcForm.ValidateDate(Me.ddlStartYear.SelectedValue, Me.ddlStartMonth.SelectedValue, Me.ddlStartDay.SelectedValue)
    End Sub

    Protected Sub vldEndDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles vldEndDate.ServerValidate
        args.IsValid = Me.wcForm.ValidateDate(Me.ddlEndYear.SelectedValue, Me.ddlEndMonth.SelectedValue, Me.ddlEndDay.SelectedValue)
    End Sub

    Protected Sub grdAdvertiserTransactions_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdAdvertiserTransactions.RowDataBound
        SCT.Admin.DataFlow.DFClsAdvertiserTransactions.ApplyGridRowAuthorizationRules(CType(sender, GridView), e.Row)
    End Sub

    Protected Sub grdAdvertiserTransactions_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdAdvertiserTransactions.Sorting
        Me.wcForm.OnSorting(e.SortExpression, CType(sender, GridView).PageIndex)
    End Sub

    Protected Sub lnkAdvertiser_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.wcForm.GoAdvertiserAccount(CType(sender, LinkButton).CommandArgument)
    End Sub

    Protected Sub lnkProject_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.wcForm.GoAdvertiserProject(CType(sender, LinkButton).CommandArgument)
    End Sub

    Protected Sub lnkNumber_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.wcForm.GoTransactionDetail(CType(sender, LinkButton).CommandArgument, CType(sender, LinkButton).CommandName)
    End Sub

    Protected Sub grdAdvertiserTransactions_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdAdvertiserTransactions.PageIndexChanging
        Me.wcForm.OnPageIndexChanging(e.NewPageIndex)
    End Sub

    Protected Sub mnuSubmit_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles mnuSubmit.MenuItemClick
        Page.Validate()
        If Page.IsValid Then
            Me.wcForm.SubmitQuery()
        End If
    End Sub

End Class
