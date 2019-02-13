
Partial Class Forms_frmAdvertiserToDos
    Inherits System.Web.UI.Page

    Private wcForm As SCT.Admin.DataFlow.DFClsAdvertiserToDos

    Public Function FindControls(ByVal Id As String) As System.Web.UI.Control
        Return Me.Controls.Item(0).FindControl("ContentPlaceHolder").FindControl(Id)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.wcForm = New SCT.Admin.DataFlow.DFClsAdvertiserToDos(Me)
        Else
            If SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsForm Is Nothing OrElse SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsForm.GetType().Name <> "DFClsAdvertiserToDos" Then
                Me.wcForm = New SCT.Admin.DataFlow.DFClsAdvertiserToDos(Me)
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
        Me.wcForm.LoadContactsList("0")
    End Sub

    Protected Sub csvStartDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvStartDate.ServerValidate
        args.IsValid = Me.wcForm.ValidateDate(Me.ddlStartYear.SelectedValue, Me.ddlStartMonth.SelectedValue, Me.ddlStartDay.SelectedValue)
    End Sub

    Protected Sub csvEndDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvEndDate.ServerValidate
        args.IsValid = Me.wcForm.ValidateDate(Me.ddlEndYear.SelectedValue, Me.ddlEndMonth.SelectedValue, Me.ddlEndDay.SelectedValue)
    End Sub

    Protected Sub mnuSubmit_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles mnuSubmit.MenuItemClick
        Page.Validate()
        If Page.IsValid Then
            Me.wcForm.OnSubmitQuery()
        End If
    End Sub

    Protected Sub grdAdToDos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdAdToDos.RowDataBound
        SCT.Admin.DataFlow.DFClsAdvertiserToDos.ApplyGridRowAuthorizationRules(CType(sender, GridView), e.Row)
        SCT.Admin.DataFlow.DFClsAdvertiserToDos.HidenMinDate(CType(sender, GridView), e.Row)
    End Sub

    Protected Sub grdAdToDos_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdAdToDos.Sorting
        Me.wcForm.OnSorting(e.SortExpression, CType(sender, GridView).PageIndex)
    End Sub

    Protected Sub lnkID_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.wcForm.OnEditing(CType(sender, LinkButton).CommandArgument)
    End Sub

    Protected Sub lnkAdvertiser_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.wcForm.OnAdAccount(CType(sender, LinkButton).CommandArgument)
    End Sub

    Protected Sub lnkContact_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.wcForm.OnAdContact(CType(sender, LinkButton).CommandArgument)
    End Sub

    Protected Sub cmdEdit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Me.wcForm.OnEditing(CType(sender, ImageButton).CommandArgument)
    End Sub

    Protected Sub cmdDelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Me.wcForm.OnDeleting(CType(sender, ImageButton).CommandArgument)
    End Sub

    Protected Sub grdAdToDos_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdAdToDos.PageIndexChanging
        Me.wcForm.OnPageIndexChanging(e.NewPageIndex)
    End Sub

    Protected Sub MsgBox_YesChoosed(ByVal sender As Object, ByVal Key As String) Handles MsgBox.YesChoosed
        Me.wcForm.OnMsgBoxOk()
    End Sub

    Protected Sub cmdAddNew_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles cmdAddNew.MenuItemClick
        Me.wcForm.OnAddNew()
    End Sub

End Class
