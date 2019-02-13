
Partial Class frmsAdvertiser_frmAdProjectHistory
    Inherits System.Web.UI.Page

    Private wcform As SCT.Web.DataFlow.DFClsAdProjectHistory

    Public Function FindControls(ByVal Id As String) As System.Web.UI.Control
        Return Me.Controls.Item(0).FindControl("ContentPlaceHolder").FindControl(Id)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.wcform = New SCT.Web.DataFlow.DFClsAdProjectHistory(Me)
        Else
            If SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsForm Is Nothing OrElse SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsForm.GetType().Name <> "DFClsAdProjectHistory" Then
                Me.wcform = New SCT.Web.DataFlow.DFClsAdProjectHistory(Me)
            Else
                Me.wcform = SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsForm
                Me.wcform.Form = Me
            End If
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsForm = Me.wcform
    End Sub

    Protected Sub csvStartDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvStartDate.ServerValidate
        args.IsValid = Me.wcform.ValidateStartDate
    End Sub

    Protected Sub csvEndDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvEndDate.ServerValidate
        args.IsValid = Me.wcform.ValidateEndDate
    End Sub

    Protected Sub csvStartTime_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvStartTime.ServerValidate
        args.IsValid = Me.wcform.ValidateStartTime
    End Sub

    Protected Sub csvEndTime_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvEndTime.ServerValidate
        args.IsValid = Me.wcform.ValidateEndTime
    End Sub

    Protected Sub grdProjectHistory_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdProjectHistory.RowDataBound
        SCT.Web.DataFlow.DFClsAdProjectHistory.HidenMinDate(CType(sender, GridView), e.Row)
    End Sub

    Protected Sub grdProjectHistory_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdProjectHistory.Sorting
        Me.wcform.OnSorting(e.SortExpression, CType(sender, GridView).PageIndex)
    End Sub

    Protected Sub lnkID_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.wcform.OnProject(CType(sender, LinkButton).CommandArgument)
    End Sub

    Protected Sub grdProjectHistory_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdProjectHistory.PageIndexChanging
        Me.wcform.OnPageIndexChanging(e.NewPageIndex)
    End Sub

    Protected Sub mnuSubmit_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles mnuSubmit.MenuItemClick
        Page.Validate()
        If Page.IsValid Then
            Me.wcform.SubmitQuery()
        End If
    End Sub

End Class
