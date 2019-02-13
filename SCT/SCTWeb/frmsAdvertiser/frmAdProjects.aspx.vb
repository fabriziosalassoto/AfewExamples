
Partial Class frmsAdvertiser_frmAdProjects
    Inherits System.Web.UI.Page

    Private wcform As SCT.Web.DataFlow.DFClsAdProjects

    Public Function FindControls(ByVal Id As String) As System.Web.UI.Control
        Return Me.Controls.Item(0).FindControl("ContentPlaceHolder").FindControl(Id)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.wcform = New SCT.Web.DataFlow.DFClsAdProjects(Me)
        Else
            If SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsForm Is Nothing OrElse SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsForm.GetType().Name <> "DFClsAdProjects" Then
                Me.wcform = New SCT.Web.DataFlow.DFClsAdProjects(Me)
            Else
                Me.wcform = SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsForm
                Me.wcform.Form = Me
            End If
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsForm = Me.wcform
    End Sub

    Protected Sub mnuSubmit_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles mnuSubmit.MenuItemClick
        Page.Validate()
        If Page.IsValid Then
            Me.wcform.SubmitQuery()
        End If
    End Sub

    Protected Sub grdAdProjects_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdAdProjects.Sorting
        Me.wcform.OnSorting(e.SortExpression, CType(sender, GridView).PageIndex)
    End Sub

    Protected Sub lnkID_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.wcform.OnView(CType(sender, LinkButton).CommandArgument)
    End Sub

    Protected Sub lnkContact_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.wcform.OnContact(CType(sender, LinkButton).CommandArgument)
    End Sub

    Protected Sub cmdHistory_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Me.wcform.OnProjectHistory(CType(sender, ImageButton).CommandArgument)
    End Sub

    Protected Sub cmdEdit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Me.wcform.OnEditing(CType(sender, ImageButton).CommandArgument)
    End Sub

    Protected Sub cmdDelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Me.wcform.OnDeleting(CType(sender, ImageButton).CommandArgument)
    End Sub

    Protected Sub MsgBox_YesChoosed(ByVal sender As Object, ByVal Key As String) Handles MsgBox.YesChoosed
        Me.wcform.OnMsgBoxOk()
    End Sub

    Protected Sub grdAdProjects_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdAdProjects.PageIndexChanging
        Me.wcform.OnPageIndexChanging(e.NewPageIndex)
    End Sub

    Protected Sub cmdAddNew_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles cmdAddNew.MenuItemClick
        Me.wcform.OnAddNew()
    End Sub

End Class
