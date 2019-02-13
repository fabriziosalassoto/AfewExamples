
Partial Class Forms_frmAdvertiserProject
    Inherits System.Web.UI.Page

    Private wcForm As SCT.Admin.DataFlow.DFClsAdvertiserProject

    Public Function FindControls(ByVal Id As String) As System.Web.UI.Control
        Return Me.Controls.Item(0).FindControl("ContentPlaceHolder").FindControl(Id)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.wcForm = New SCT.Admin.DataFlow.DFClsAdvertiserProject(Me)
        Else
            If SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsForm Is Nothing OrElse SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsForm.GetType().Name <> "DFClsAdvertiserProject" Then
                Me.wcForm = New SCT.Admin.DataFlow.DFClsAdvertiserProject(Me)
            Else
                Me.wcForm = SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsForm
                Me.wcForm.Form = Me
            End If
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsForm = Me.wcForm
    End Sub

    Protected Sub csvAdProjectStartDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvAdProjectStartDate.ServerValidate
        args.IsValid = Me.wcForm.ValidateStarDate
    End Sub

    Protected Sub csvAdProjectEndDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvAdProjectEndDate.ServerValidate
        args.IsValid = Me.wcForm.ValidateEndDate
    End Sub

    Protected Sub csvAdProjectStartDateGTEndDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvAdProjectStartDateGTEndDate.ServerValidate
        args.IsValid = Me.wcForm.ValidateStartDateGTEndDate
    End Sub

    Protected Sub csvAdProjectStartTime_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvAdProjectStartTime.ServerValidate
        args.IsValid = Me.wcForm.ValidateStartTime
    End Sub

    Protected Sub csvAdProjectEndTime_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvAdProjectEndTime.ServerValidate
        args.IsValid = Me.wcForm.ValidateEndTime
    End Sub

    Protected Sub csvAdProjectStartTimeGTEndTime_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvAdProjectStartTimeGTEndTime.ServerValidate
        args.IsValid = Me.wcForm.ValidateStartTimeGTEndTime
    End Sub

    Protected Sub csvAdProjectVerifiedDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvAdProjectVerifiedDate.ServerValidate
        args.IsValid = Me.wcForm.ValidateVerifiedDate
    End Sub

    Protected Sub csvAdProjectOnlineDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvAdProjectOnlineDate.ServerValidate
        args.IsValid = Me.wcForm.ValidateOnlineDate
    End Sub

    Protected Sub ddlAdProjectAdvertiser_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAdProjectAdvertiser.SelectedIndexChanged
        Me.wcForm.OnSelectedAdvertiser(CType(sender, DropDownList).SelectedValue)
    End Sub

    Protected Sub ddlAdProjectCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAdProjectCountry.SelectedIndexChanged
        Me.wcForm.OnSelectedCountry(CType(sender, DropDownList).SelectedValue)
    End Sub

    Protected Sub mnuItem_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles mnuItem.MenuItemClick
        Select Case e.Item.Value
            Case "Advertiser"
                Me.wcForm.OnAdvertiser()
            Case "Contact"
                Me.wcForm.OnContact()
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

    Protected Sub MsgBox_YesChoosed(ByVal sender As Object, ByVal Key As String) Handles MsgBox.YesChoosed
        Me.wcForm.OnMsgBoxOk()
    End Sub

End Class
