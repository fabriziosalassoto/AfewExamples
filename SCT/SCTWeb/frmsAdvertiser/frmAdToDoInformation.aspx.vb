
Partial Class frmsAdvertiser_frmAdToDoInformation
    Inherits System.Web.UI.Page

    Private wcform As SCT.Web.DataFlow.DFClsAdToDoInformation

    Public Function FindControls(ByVal Id As String) As System.Web.UI.Control
        Return Me.Controls.Item(0).FindControl("ContentPlaceHolder").FindControl(Id)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.wcform = New SCT.Web.DataFlow.DFClsAdToDoInformation(Me)
        Else
            If SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsForm Is Nothing OrElse SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsForm.GetType().Name <> "DFClsAdToDoInformation" Then
                Me.wcform = New SCT.Web.DataFlow.DFClsAdToDoInformation(Me)
            Else
                Me.wcform = SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsForm
                Me.wcform.Form = Me
            End If
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsForm = Me.wcform
    End Sub

    Protected Sub mnuContact_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles mnuContact.MenuItemClick
        Me.wcform.OnContact()
    End Sub

    Protected Sub mnuEditContact_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles mnuEditToDo.MenuItemClick
        Select Case e.Item.Value
            Case "Edit"
                Me.wcform.OnEditing()
            Case "Delete"
                Me.wcform.OnDeleting()
            Case "Return"
                Me.wcform.OnReturn()
            Case Else
        End Select
    End Sub

    Protected Sub MsgBox_YesChoosed(ByVal sender As Object, ByVal Key As String) Handles MsgBox.YesChoosed
        Me.wcform.OnOkDelete()
    End Sub

End Class
