
Partial Class frmsAdvertiser_frmAdChangeNoteInformation
    Inherits System.Web.UI.Page

    Private wcform As SCT.Web.DataFlow.DFClsAdChangeNoteInformation

    Public Function FindControls(ByVal Id As String) As System.Web.UI.Control
        Return Me.Controls.Item(0).FindControl("ContentPlaceHolder").FindControl(Id)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.wcform = New SCT.Web.DataFlow.DFClsAdChangeNoteInformation(Me)
        Else
            If SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsForm Is Nothing OrElse SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsForm.GetType().Name <> "DFClsAdChangeNoteInformation" Then
                Me.wcform = New SCT.Web.DataFlow.DFClsAdChangeNoteInformation(Me)
            Else
                Me.wcform = SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsForm
                Me.wcform.Form = Me
            End If
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsForm = Me.wcform
    End Sub

    Protected Sub csvAdNoteEnteredDateValid_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvAdNoteEnteredDateValid.ServerValidate
        args.IsValid = Me.wcform.ValidateEnteredDateValid
    End Sub

    Protected Sub csvAdNoteDateEnteredRequired_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvAdNoteDateEnteredRequired.ServerValidate
        args.IsValid = Me.wcform.ValidateEnteredDateRequered
    End Sub

    Protected Sub cmdOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        If Page.IsValid Then
            Me.wcform.OnOk()
        End If
    End Sub

    Protected Sub cmdReturn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdReturn.Click
        Me.wcform.OnReturn()
    End Sub
End Class
