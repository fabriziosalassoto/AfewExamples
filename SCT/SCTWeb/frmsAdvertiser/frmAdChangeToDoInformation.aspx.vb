
Partial Class frmsAdvertiser_frmAdChangeToDoInformation
    Inherits System.Web.UI.Page

    Private wcform As SCT.Web.DataFlow.DFClsAdChangeToDoInformation

    Public Function FindControls(ByVal Id As String) As System.Web.UI.Control
        Return Me.Controls.Item(0).FindControl("ContentPlaceHolder").FindControl(Id)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.wcform = New SCT.Web.DataFlow.DFClsAdChangeToDoInformation(Me)
        Else
            If SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsForm Is Nothing OrElse SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsForm.GetType().Name <> "DFClsAdChangeToDoInformation" Then
                Me.wcform = New SCT.Web.DataFlow.DFClsAdChangeToDoInformation(Me)
            Else
                Me.wcform = SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsForm
                Me.wcform.Form = Me
            End If
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsForm = Me.wcform
    End Sub

    Protected Sub csvAdToDoDateEnteredRequired_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvAdToDoDateEnteredRequired.ServerValidate
        args.IsValid = Me.wcform.ValidateEnteredDateRequered
    End Sub

    Protected Sub cvEnteredDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvAdToDoEnteredDateValid.ServerValidate
        args.IsValid = Me.wcform.ValidateEnteredDateValid
    End Sub

    Protected Sub cvDueDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvAdToDoDueDateValid.ServerValidate
        args.IsValid = Me.wcform.ValidateDueDateValid
    End Sub

    Protected Sub cvCompletedDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvAdToDoCompletedDateValid.ServerValidate
        args.IsValid = Me.wcform.ValidateCompletedDateValid
    End Sub

    Protected Sub cvEnteredDateGTDueDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvAdToDoEnteredDateGTDueDate.ServerValidate
        args.IsValid = Me.wcform.ValidateEnteredDateGTDueDate()
    End Sub

    Protected Sub cvEnteredDateGTCompletedDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvAdToDoEnteredDateGTCompletedDate.ServerValidate
        args.IsValid = Me.wcform.ValidateEnteredDateGTCompletedDate()
    End Sub

    Protected Sub cmdReturn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdReturn.Click
        Me.wcform.OnReturn()
    End Sub

    Protected Sub cmdOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        If Page.IsValid Then
            Me.wcform.OnOk()
        End If
    End Sub

End Class
