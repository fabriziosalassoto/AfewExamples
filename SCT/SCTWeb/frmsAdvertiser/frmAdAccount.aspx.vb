
Partial Class frmsAdvertiser_frmAdAccount
    Inherits System.Web.UI.Page

    Private wcform As SCT.Web.DataFlow.DFClsAdAccountInformation

    Public Function FindControls(ByVal Id As String) As System.Web.UI.Control
        Return Me.Controls.Item(0).FindControl("ContentPlaceHolder").FindControl(Id)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.wcform = New SCT.Web.DataFlow.DFClsAdAccountInformation(Me)
        Else
            If SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsForm Is Nothing OrElse SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsForm.GetType().Name <> "DFClsAdAccountInformation" Then
                Me.wcform = New SCT.Web.DataFlow.DFClsAdAccountInformation(Me)
            Else
                Me.wcform = SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsForm
                Me.wcform.Form = Me
            End If
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsForm = Me.wcform
    End Sub

    Protected Sub mnuAccount_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles mnuAccount.MenuItemClick
        Select Case e.Item.Value
            Case "Contacts"
                Me.wcform.OnContacts()
            Case "Projects"
                Me.wcform.OnProjects()
            Case Else
        End Select
    End Sub

End Class
