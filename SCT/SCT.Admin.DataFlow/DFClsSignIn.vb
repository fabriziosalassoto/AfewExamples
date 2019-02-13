Public Class DFClsSignIn

    Private mForm As Object

    Public WriteOnly Property Form() As Object
        Set(ByVal value As Object)
            Me.mForm = value
        End Set
    End Property

    Public Sub New(ByRef form As Object)
        Me.mForm = form
        Me.mForm.FindControl("txtUserName").Focus()
    End Sub

    Public Function ValidateUser(ByVal userName As String, ByVal password As String) As Boolean
        Try
            If SCT.Library.AdminSite.ClsSessionAdmin.Login(userName, password) Then
                Return True
            Else
                Me.mForm.FindControl("txtUserName").Focus()
                Return False
            End If
        Catch SysEx As Exception
            Me.mForm.FindControl("MsgBox").ShowMessage(SysEx.Message)
            Return False
        End Try
    End Function

    Public Sub SignIn()
        Me.mForm.Response.Redirect("~/Forms/frmMain.aspx")
    End Sub

End Class
