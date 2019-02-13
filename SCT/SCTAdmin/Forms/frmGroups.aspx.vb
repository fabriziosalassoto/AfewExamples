
Partial Class Forms_frmGroups
    Inherits System.Web.UI.Page

    Private wcForm As SCT.Admin.DataFlow.DFClsGroups

    Public Function FindControls(ByVal Id As String) As System.Web.UI.Control
        Return Me.Controls.Item(0).FindControl("ContentPlaceHolder").FindControl(Id)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.wcForm = New SCT.Admin.DataFlow.DFClsGroups(Me)
        Else
            If SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsForm Is Nothing OrElse SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsForm.GetType().Name <> "DFClsGroups" Then
                Me.wcForm = New SCT.Admin.DataFlow.DFClsGroups(Me)
            Else
                Me.wcForm = SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsForm
                Me.wcForm.Form = Me
            End If
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsForm = Me.wcForm
    End Sub

    Protected Sub GridView_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView.RowDataBound
        SCT.Admin.DataFlow.DFClsGroups.ApplyGridRowAuthorizationRules(CType(sender, GridView), e.Row)
    End Sub

    Protected Sub GridView_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridView.Sorting
        Me.wcForm.OnSorting(e.SortExpression, CType(sender, GridView).PageIndex)
    End Sub

    Protected Sub lnkGroupID_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.wcForm.OnEditing(CType(sender, LinkButton).CommandArgument)
    End Sub

    Protected Sub lnkGroupForm_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.wcForm.OnForm(CType(sender, LinkButton).CommandArgument)
    End Sub

    Protected Sub cmdPermissions_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Me.wcForm.OnPermissions(CType(sender, ImageButton).CommandArgument, CType(sender, ImageButton).CommandName)
    End Sub

    Protected Sub cmdFields_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Me.wcForm.OnFields(CType(sender, ImageButton).CommandArgument, CType(sender, ImageButton).CommandName)
    End Sub

    Protected Sub cmdEdit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Me.wcForm.OnEditing(CType(sender, ImageButton).CommandArgument)
    End Sub

    Protected Sub cmdDelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Me.wcForm.OnDeleting(CType(sender, ImageButton).CommandArgument)
    End Sub

    Protected Sub GridView_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView.PageIndexChanging
        Me.wcForm.OnPageIndexChanging(e.NewPageIndex)
    End Sub

    Protected Sub MsgBox_YesChoosed(ByVal sender As Object, ByVal Key As String) Handles MsgBox.YesChoosed
        Me.wcForm.OnMsgBoxOk()
    End Sub

    Protected Sub mnuAddNew_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles mnuAddNew.MenuItemClick
        Select Case e.Item.Value
            Case "AddNew"
                Me.wcForm.OnAddNew()
            Case "Return"
                Me.wcForm.OnReturn()
            Case Else
        End Select
    End Sub

    Protected Sub ddlForms_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlForms.SelectedIndexChanged
        Me.wcForm.OnSelectedFormChanged()
    End Sub

    Protected Sub cvExistsDescription_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvExistsDescription.ServerValidate
        args.IsValid = Me.wcForm.ValidateExistsDescription(CType(source, System.Web.UI.WebControls.CustomValidator).ControlToValidate)
    End Sub

    Protected Sub mnuItem_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles mnuItem.MenuItemClick
        Select Case e.Item.Value
            Case "Form"
                Me.wcForm.OnForm()
            Case "Permissions"
                Me.wcForm.OnPermissions()
            Case "Fields"
                Me.wcForm.OnFields()
            Case "Delete"
                Me.wcForm.OnDeleting()
            Case Else
        End Select
    End Sub

    Protected Sub mnuSave_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles mnuSave.MenuItemClick
        Select Case e.Item.Value
            Case "Ok"
                Page.Validate()
                If Page.IsValid Then
                    Me.wcForm.OnOk()
                End If
            Case "Cancel"
                Me.wcForm.OnCancel()
            Case "Save"
                Page.Validate()
                If Page.IsValid Then
                    Me.wcForm.OnSave()
                End If
            Case Else
        End Select
    End Sub

End Class
