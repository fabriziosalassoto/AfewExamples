
Partial Class Forms_frmAdvertiserContact
    Inherits System.Web.UI.Page

    Private wcForm As SCT.Admin.DataFlow.DFClsAdvertiserContact

    Public Function FindControls(ByVal Id As String) As System.Web.UI.Control
        Return Me.Controls.Item(0).FindControl("ContentPlaceHolder").FindControl(Id)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.wcForm = New SCT.Admin.DataFlow.DFClsAdvertiserContact(Me)
        Else
            If SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsForm Is Nothing OrElse SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsForm.GetType().Name <> "DFClsAdvertiserContact" Then
                Me.wcForm = New SCT.Admin.DataFlow.DFClsAdvertiserContact(Me)
            Else
                Me.wcForm = SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsForm
                Me.wcForm.Form = Me
            End If
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsForm = Me.wcForm
    End Sub

    Protected Sub cvAdContactAdvertiser_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvAdContactAdvertiser.ServerValidate
        args.IsValid = Me.wcForm.ValidateExistsInProjectsAnotherAdvertiser()
    End Sub

    Protected Sub ctvMainCompanyAddres_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles ctvMainCompanyAddres.ServerValidate
        args.IsValid = Me.wcForm.ValidateExistsMainCompanyAddress()
    End Sub

    Protected Sub ddlAdContactCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAdContactCountry.SelectedIndexChanged
        Me.wcForm.OnSelectedCountry(CType(sender, DropDownList).SelectedValue)
    End Sub

    Protected Sub MsgBox_YesChoosed(ByVal sender As Object, ByVal Key As String) Handles MsgBox.YesChoosed
        Me.wcForm.OnMsgBoxOk()
    End Sub

    Protected Sub mnuItem_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles mnuItem.MenuItemClick
        Select Case e.Item.Value
            Case "Advertiser"
                Me.wcForm.OnAdvertiser()
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
