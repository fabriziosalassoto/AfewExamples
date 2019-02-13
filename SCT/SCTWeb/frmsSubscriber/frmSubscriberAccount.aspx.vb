
Partial Class frmsSubscriber_frmSubscriberAccount
    Inherits System.Web.UI.Page

    Private wcfrmSubAccountInformation As SCT.Web.DataFlow.DFClsSubAccountInformation

    Public Function FindControls(ByVal Id As String) As System.Web.UI.Control
        Return Me.Controls.Item(0).FindControl("ContentPlaceHolder").FindControl(Id)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.wcfrmSubAccountInformation = New SCT.Web.DataFlow.DFClsSubAccountInformation(Me)
        Else
            Me.wcfrmSubAccountInformation = Me.Session("wcFormSubAccountInformation")
            If Me.wcfrmSubAccountInformation Is Nothing Then
                Me.wcfrmSubAccountInformation = New SCT.Web.DataFlow.DFClsSubAccountInformation(Me)
            Else
                Me.wcfrmSubAccountInformation.Form = Me
            End If
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        Me.Session("wcFormSubAccountInformation") = Me.wcfrmSubAccountInformation
    End Sub

End Class
