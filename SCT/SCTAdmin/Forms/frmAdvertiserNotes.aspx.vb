
Partial Class Forms_frmAdvertiserNotes
    Inherits System.Web.UI.Page

    Private wcForm As SCT.Admin.DataFlow.DFClsAdvertiserNotes

    Public Function FindControls(ByVal Id As String) As System.Web.UI.Control
        Return Me.Controls.Item(0).FindControl("ContentPlaceHolder").FindControl(Id)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.wcForm = New SCT.Admin.DataFlow.DFClsAdvertiserNotes(Me)
        Else
            If SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsForm Is Nothing OrElse SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsForm.GetType().Name <> "DFClsAdvertiserNotes" Then
                Me.wcForm = New SCT.Admin.DataFlow.DFClsAdvertiserNotes(Me)
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

    Protected Sub grdAdNotes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdAdNotes.RowDataBound
        SCT.Admin.DataFlow.DFClsAdvertiserNotes.ApplyGridRowAuthorizationRules(CType(sender, GridView), e.Row)
    End Sub

    Protected Sub grdAdNotes_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdAdNotes.Sorting
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

    Protected Sub grdAdNotes_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdAdNotes.PageIndexChanging
        Me.wcForm.OnPageIndexChanging(e.NewPageIndex)
    End Sub

    Protected Sub MsgBox_YesChoosed(ByVal sender As Object, ByVal Key As String) Handles MsgBox.YesChoosed
        Me.wcForm.OnMsgBoxOk()
    End Sub

    Protected Sub cmdAddNew_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles cmdAddNew.MenuItemClick
        Me.wcForm.OnAddNew()
    End Sub

End Class
