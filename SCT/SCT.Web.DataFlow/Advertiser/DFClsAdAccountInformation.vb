Imports SCT.Library

Public Class DFClsAdAccountInformation

#Region " Private Fields "

    Private mForm As Object
    Private mData As AdAccountData

#End Region

#Region " Properties And Methods "

    Public WriteOnly Property Form() As Object
        Set(ByVal value As Object)
            Me.mForm = value
        End Set
    End Property

    Public Sub New(ByRef form As Object)
        Me.mForm = form
        Me.mForm.Session("ValuePath") = "frmAdAccount"

        ApplyPageAuthorizationRules()

        If IsNumeric(WebSite.ClsSessionAdmin.GetSessionUserID) AndAlso WebSite.ClsSessionAdmin.GetSessionUserID <> 0 Then
            Me.mData = Me.GetData(WebSite.ClsSessionAdmin.GetSessionUserID)
            If Me.mData IsNot Nothing Then
                Me.PopulateData()
                Me.EnableMenuItem(True)
            End If
        End If
    End Sub

    Public Sub ApplyPageAuthorizationRules()
        If Not WebSite.ClsSessionAdmin.IsValidForm("frmAdAccount", "txtAdAccountID", "txtAdAccountLogin", "txtAdAccountCompanyName", "txtAdAccountCompanyNote") Then Me.mForm.Response.Redirect("~/Forms/frmMain.aspx")
        Me.EnableMenuItem(False)
    End Sub

    Public Sub EnableMenuItem(ByVal value As Boolean)
        Me.mForm.FindControls("mnuAccount").Enabled = value
        Me.mForm.FindControls("mnuChangePassword").Enabled = value
        Me.mForm.FindControls("mnuChangeCompanyInformation").Enabled = value
    End Sub

    Public Sub OnContacts()
        Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdContacts.aspx")
    End Sub

    Public Sub OnProjects()
        Me.mForm.Session("contactID") = 0
        Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdProjects.aspx")
    End Sub

#End Region

#Region " Data Methods "

    Private Sub PopulateData()
        Me.mForm.FindControls("txtAdAccountLogin").Text = Me.mData.Login
        Me.mForm.FindControls("txtAdAccountID").Text = Me.mData.ID
        Me.mForm.FindControls("txtAdAccountCompanyName").Text = Me.mData.CompanyName
        Me.mForm.FindControls("txtAdAccountCompanyNote").Text = Me.mData.CompanyNotes
    End Sub

    Private Function CollectDataID(ByVal accountID As String) As AdAccountNewData
        Dim formData As New AdAccountNewData
        formData.ID.SetValues("txtAdAccountID", True, 0, CLng(accountID))
        Return formData
    End Function

    Private Function GetData(ByVal accountID As String) As AdAccountData
        Try
            Return WebSite.ClsSessionAdmin.GetAdAccount(Me.CollectDataID(accountID))
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return Nothing
        End Try
    End Function

#End Region

End Class
