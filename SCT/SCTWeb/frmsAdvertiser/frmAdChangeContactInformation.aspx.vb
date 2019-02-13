
Partial Class frmsAdvertiser_frmAdChangeContactInformation
    Inherits System.Web.UI.Page

    Private wcform As SCT.Web.DataFlow.DFClsAdChangeContactInformation

    Public Function FindControls(ByVal Id As String) As System.Web.UI.Control
        Return Me.Controls.Item(0).FindControl("ContentPlaceHolder").FindControl(Id)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.wcform = New SCT.Web.DataFlow.DFClsAdChangeContactInformation(Me)
        Else
            If SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsForm Is Nothing OrElse SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsForm.GetType().Name <> "DFClsAdChangeContactInformation" Then
                Me.wcform = New SCT.Web.DataFlow.DFClsAdChangeContactInformation(Me)
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

    Protected Sub ddlCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAdContactCountry.SelectedIndexChanged
        Me.wcform.OnSelectedCountry(CType(sender, DropDownList).SelectedValue)
    End Sub

    Protected Sub ctvMainCompanyAddres_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles ctvMainCompanyAddres.ServerValidate
        args.IsValid = Me.wcform.ValidateExistsMainCompanyAddress()
    End Sub
End Class
