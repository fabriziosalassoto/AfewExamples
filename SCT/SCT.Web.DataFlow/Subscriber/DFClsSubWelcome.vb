Public Class DFClsSubWelcome

#Region " Private Fields "
    Private mForm As Object
#End Region

#Region " Properties And Methods "

    Public WriteOnly Property Form() As Object
        Set(ByVal value As Object)
            Me.mForm = value
        End Set
    End Property

    Public Sub New(ByRef form As Object)
        Me.mForm = form
        Me.mForm.FindControls("lblWelcome").Text = "Welcome: " & Mid(Csla.ApplicationContext.User.Identity.Name, Csla.ApplicationContext.User.Identity.Name.IndexOf(";") + 2)
    End Sub

    Public Sub OnOk()
        Me.mForm.Response.Redirect("~/DownloadAplication/NBDtor-Install.exe")
    End Sub

    Public Sub OnCancel()
        Me.mForm.Response.Redirect("~/frmsSubscriber/frmSubscriberStatus.aspx")
    End Sub

#End Region

End Class
