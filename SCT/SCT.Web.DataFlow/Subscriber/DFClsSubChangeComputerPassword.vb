Public Class DFClsSubChangeComputerPassword

#Region " Private Fields "
    Private mForm As Object
    Private mData As SCTServer.ClsSubscriberAccountData
#End Region

#Region " Properties And Methods "

    Public WriteOnly Property Form() As Object
        Set(ByVal value As Object)
            Me.mForm = value
        End Set
    End Property

    Public Sub New(ByRef form As Object)
        Me.mForm = form
        Me.mData = Me.GetData(Mid(Csla.ApplicationContext.User.Identity.Name, 1, Csla.ApplicationContext.User.Identity.Name.IndexOf(";")))
        Me.PopulateData()
    End Sub

    Public Function ValidatePassword(ByVal ControlName As String) As Boolean
        Return Me.mForm.FindControls(ControlName).Text = Me.mData.ClientPassword
    End Function

    Public Sub OnOk()
        If Me.EditData Then
            Me.mForm.FindControls("MsgBox").ShowMessage("The password was changed successfully.")
        End If
    End Sub

    Public Sub OnReturn()
        Me.mForm.Response.Redirect("~/frmsSubscriber/frmSubscriberAccount.aspx")
    End Sub

#End Region

#Region " Data Methods "

    Private Sub PopulateData()
        Me.mForm.FindControls("txtComputerSerialNbr").Text = Me.mData.ComputerSerialNumber
        Me.mForm.FindControls("txtCurrentPassword").Focus()
    End Sub

    Private Function EditData() As Boolean
        Using svc As New SCTServer.SCTServer
            Try
                svc.ChangeSubscriberComputerPassword(Me.mData.ID, Me.mForm.FindControls("txtNewWebPassword").Text)
                Return True
            Catch DataEx As Csla.DataPortalException
                Me.mForm.FindControls("MsgBox").ShowMessage(DataEx.BusinessException.Message)
                Return False
            Catch SysEx As Exception
                Me.mForm.FindControls("MsgBox").ShowMessage(SysEx.Message)
                Return False
            End Try
        End Using

    End Function

    Private Function GetData(ByVal accountId As Long) As SCTServer.ClsSubscriberAccountData
        Using server As New SCTServer.SCTServer
            Try
                Return server.GetSubscriberAccount(accountId)
            Catch SysEx As Exception
                Me.mForm.FindControls("MsgBox").ShowMessage(SysEx.Message)
                Return Nothing
            End Try
        End Using
    End Function

#End Region

End Class
