
Partial Class Forms_frmAdvertiserNote
    Inherits System.Web.UI.Page

    Private wcForm As SCT.Admin.DataFlow.DFClsAdvertiserNote

    Public Function FindControls(ByVal Id As String) As System.Web.UI.Control
        Return Me.Controls.Item(0).FindControl("ContentPlaceHolder").FindControl(Id)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.wcForm = New SCT.Admin.DataFlow.DFClsAdvertiserNote(Me)
        Else
            If SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsForm Is Nothing OrElse SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsForm.GetType().Name <> "DFClsAdvertiserNote" Then
                Me.wcForm = New SCT.Admin.DataFlow.DFClsAdvertiserNote(Me)
            Else
                Me.wcForm = SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsForm
                Me.wcForm.Form = Me
            End If
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsForm = Me.wcForm
    End Sub

    Protected Sub ddlAdNoteAdvertiser_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAdNoteAdvertiser.SelectedIndexChanged
        Me.wcForm.OnSelectedAdvertiser(CType(sender, DropDownList).SelectedValue)
    End Sub

    Protected Sub csvAdNoteEnteredDateValid_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvAdNoteEnteredDateValid.ServerValidate
        args.IsValid = Me.wcForm.ValidateEnteredDateValid()
    End Sub

    Protected Sub csvAdNoteDateEnteredRequired_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvAdNoteDateEnteredRequired.ServerValidate
        args.IsValid = Me.wcForm.ValidateEnteredDateRequered()
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
