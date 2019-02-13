Public Class DFClsDefault

    Private mFrmDefault As Object

    Public WriteOnly Property FormDefault() As Object
        Set(ByVal value As Object)
            Me.mFrmDefault = value
        End Set
    End Property

    Public Sub New(ByRef pFrmDefault As Object)
        Me.mFrmDefault = pFrmDefault
        Me.GoToLoginPage()
    End Sub

    Public Sub GoToLoginPage()
        Select Case CType(Me.mFrmDefault.Session("UserType"), String)
            Case "Advertiser"
                Me.mFrmDefault.Response.Redirect("~/frmsSystem/frmAdvertiserSignIn.aspx")
            Case "Subscriber"
                Me.mFrmDefault.Response.Redirect("~/frmsSystem/frmSubscriberSignIn.aspx")
            Case Else
                '
        End Select
    End Sub

    Public Sub OnEnter(ByVal userType As String)
        Me.mFrmDefault.Session("UserType") = userType
        Me.GoToLoginPage()
    End Sub

End Class
