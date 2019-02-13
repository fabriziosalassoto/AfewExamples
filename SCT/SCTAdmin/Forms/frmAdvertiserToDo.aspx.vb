
Partial Class Forms_frmAdvertiserToDo
    Inherits System.Web.UI.Page

    Private wcForm As SCT.Admin.DataFlow.DFClsAdvertiserToDo

    Public Function FindControls(ByVal Id As String) As System.Web.UI.Control
        Return Me.Controls.Item(0).FindControl("ContentPlaceHolder").FindControl(Id)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.wcForm = New SCT.Admin.DataFlow.DFClsAdvertiserToDo(Me)
        Else
            If SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsForm Is Nothing OrElse SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsForm.GetType().Name <> "DFClsAdvertiserToDo" Then
                Me.wcForm = New SCT.Admin.DataFlow.DFClsAdvertiserToDo(Me)
            Else
                Me.wcForm = SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsForm
                Me.wcForm.Form = Me
            End If
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        SCT.Library.AdminSite.ClsSessionAdmin.SessionCurrentClsForm = Me.wcForm
    End Sub

    Protected Sub ddlAdToDoAdvertiser_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAdToDoAdvertiser.SelectedIndexChanged
        Me.wcForm.OnSelectedAdvertiser(CType(sender, DropDownList).SelectedValue)
    End Sub

    Protected Sub csvAdToDoEnteredDateValid_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvAdToDoEnteredDateValid.ServerValidate
        args.IsValid = Me.wcForm.ValidateEnteredDateValid()
    End Sub

    Protected Sub csvAdToDoDateEnteredRequired_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvAdToDoDateEnteredRequired.ServerValidate
        args.IsValid = Me.wcForm.ValidateEnteredDateRequered()
    End Sub

    Protected Sub csvAdToDoDueDateValid_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvAdToDoDueDateValid.ServerValidate
        args.IsValid = Me.wcForm.ValidateDueDateValid
    End Sub

    Protected Sub csvAdToDoEnteredDateGTDueDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvAdToDoEnteredDateGTDueDate.ServerValidate
        args.IsValid = Me.wcForm.ValidateEnteredDateGTDueDate
    End Sub

    Protected Sub csvAdToDoCompletedDateValid_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvAdToDoCompletedDateValid.ServerValidate
        args.IsValid = Me.wcForm.ValidateCompletedDateValid
    End Sub

    Protected Sub csvAdToDoEnteredDateGTCompletedDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvAdToDoEnteredDateGTCompletedDate.ServerValidate
        args.IsValid = Me.wcForm.ValidateEnteredDateGTCompletedDate
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
