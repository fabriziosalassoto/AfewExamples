
Partial Class frmsAdvertiser_frmAdNotes
    Inherits System.Web.UI.Page

    Private wcForm As SCT.Web.DataFlow.DFClsAdNotes

    Public Function FindControls(ByVal Id As String) As System.Web.UI.Control
        Return Me.Controls.Item(0).FindControl("ContentPlaceHolder").FindControl(Id)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.wcForm = New SCT.Web.DataFlow.DFClsAdNotes(Me)
        Else
            If SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsForm Is Nothing OrElse SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsForm.GetType().Name <> "DFClsAdNotes" Then
                Me.wcForm = New SCT.Web.DataFlow.DFClsAdNotes(Me)
            Else
                Me.wcForm = SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsForm
                Me.wcForm.Form = Me
            End If
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsForm = Me.wcForm
    End Sub

    Protected Sub csvStartDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvStartDate.ServerValidate
        args.IsValid = Me.wcForm.ValidateStartDate
    End Sub

    Protected Sub csvEndDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvEndDate.ServerValidate
        args.IsValid = Me.wcForm.ValidateEndDate
    End Sub

    Protected Sub mnuSubmit_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles mnuSubmit.MenuItemClick
        Page.Validate()
        If Page.IsValid Then
            Me.wcForm.SubmitQuery()
        End If
    End Sub

    Protected Sub grdAdToDos_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdAdNotes.Sorting
        Me.wcForm.OnSorting(e.SortExpression, CType(sender, GridView).PageIndex)
    End Sub

    Protected Sub lnkID_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.wcForm.OnView(CType(sender, LinkButton).CommandArgument)
    End Sub

    Protected Sub lnkContact_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.wcForm.OnContact(CType(sender, LinkButton).CommandArgument)
    End Sub

    Protected Sub cmdEdit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Me.wcForm.OnEditing(CType(sender, ImageButton).CommandArgument)
    End Sub

    Protected Sub cmdDelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Me.wcForm.OnDeleting(CType(sender, ImageButton).CommandArgument)
    End Sub

    Protected Sub grdAdToDos_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdAdNotes.PageIndexChanging
        Me.wcForm.OnPageIndexChanging(e.NewPageIndex)
    End Sub

    Protected Sub MsgBox_YesChoosed(ByVal sender As Object, ByVal Key As String) Handles MsgBox.YesChoosed
        Me.wcForm.OnOkDelete()
    End Sub

    Protected Sub cmdAddNew_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles cmdAddNew.MenuItemClick
        Me.wcForm.OnAddNew()
    End Sub

End Class
