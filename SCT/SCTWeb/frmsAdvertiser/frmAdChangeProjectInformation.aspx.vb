
Partial Class frmsAdvertiser_frmAdChangeProjectInformation
    Inherits System.Web.UI.Page

    Private wcform As SCT.Web.DataFlow.DFClsAdChangeProjectInformation

    Public Function FindControls(ByVal Id As String) As System.Web.UI.Control
        Return Me.Controls.Item(0).FindControl("ContentPlaceHolder").FindControl(Id)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.wcform = New SCT.Web.DataFlow.DFClsAdChangeProjectInformation(Me)
        Else
            If SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsForm Is Nothing OrElse SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsForm.GetType().Name <> "DFClsAdChangeProjectInformation" Then
                Me.wcform = New SCT.Web.DataFlow.DFClsAdChangeProjectInformation(Me)
            Else
                Me.wcform = SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsForm
                Me.wcform.Form = Me
            End If
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsForm = Me.wcform
    End Sub

    Protected Sub cmdReturn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdReturn.Click
        Me.wcform.OnReturn()
    End Sub

    Protected Sub cmdOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        If Page.IsValid Then
            Me.wcform.OnOk()
        End If
    End Sub

    Protected Sub ddlCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAdProjectCountry.SelectedIndexChanged
        Me.wcform.OnSelectedCountry(CType(sender, DropDownList).SelectedValue)
    End Sub

    Protected Sub cvStartDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvAdProjectStartDate.ServerValidate
        args.IsValid = Me.wcform.ValidateStarDate()
    End Sub

    Protected Sub cvEndDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvAdProjectEndDate.ServerValidate
        args.IsValid = Me.wcform.ValidateEndDate
    End Sub

    Protected Sub cvStartDateGTEndDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvAdProjectStartDateGTEndDate.ServerValidate
        args.IsValid = Me.wcform.ValidateStartDateGTEndDate
    End Sub

    Protected Sub cvStartTime_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvAdProjectStartTime.ServerValidate
        args.IsValid = Me.wcform.ValidateStartTime
    End Sub

    Protected Sub cvEndTime_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvAdProjectEndTime.ServerValidate
        args.IsValid = Me.wcform.ValidateEndTime
    End Sub

    Protected Sub cvStartTimeGTEndTime_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles csvAdProjectStartTimeGTEndTime.ServerValidate
        args.IsValid = Me.wcform.ValidateStartTimeGTEndTime
    End Sub

End Class
