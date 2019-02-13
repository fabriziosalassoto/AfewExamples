
Partial Class frmsAdvertiser_frmAdContactInformation
    Inherits System.Web.UI.Page

    Private wcform As SCT.Web.DataFlow.DFClsAdContactInformation

    Public Function FindControls(ByVal Id As String) As System.Web.UI.Control
        Return Me.Controls.Item(0).FindControl("ContentPlaceHolder").FindControl(Id)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.wcform = New SCT.Web.DataFlow.DFClsAdContactInformation(Me)
        Else
            If SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsForm Is Nothing OrElse SCT.Library.WebSite.ClsSessionAdmin.SessionCurrentClsForm.GetType().Name <> "DFClsAdContactInformation" Then
                Me.wcform = New SCT.Web.DataFlow.DFClsAdContactInformation(Me)
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
        Select Case e.Item.Value
            Case "Projects"
                Me.wcform.OnProjects()
            Case "Notes"
                Me.wcform.OnNotes()
            Case "ToDo"
                Me.wcform.OnToDo()
        End Select
    End Sub

    Protected Sub cmdEditContact_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles mnuEditContact.MenuItemClick
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