Public Class DFClsAdSignIn

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
        Me.mForm.FindControl("txtUserName").Focus()
    End Sub

    Public Function ValidateUser(ByVal userName As String, ByVal password As String) As Boolean
        Try
            If SCT.Library.WebSite.ClsSessionAdmin.LoginAdvertiser(userName, password) Then
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

    Public Sub OnSignIn()
        Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdAccount.aspx")
    End Sub

#End Region

End Class
