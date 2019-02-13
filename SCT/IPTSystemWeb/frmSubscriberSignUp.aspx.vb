Imports SCT.Library

Partial Class WebForms_frmSubscriberSignUp
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.txtWebLogin.Text = ""
            Me.txtWebPassword.Text = ""
            Me.txtWebConfirm.Text = ""
            Me.txtSerialNbr.Text = ""
            Me.txtComputerPassword.Text = ""
            Me.txtComputerConfirm.Text = ""
            Me.txtEmail.Text = ""
            Me.txtFirstName.Text = ""
            Me.txtLastName.Text = ""
            Me.txtAge.Text = ""
            Me.txtCity.Text = ""
            Me.txtState.Text = ""
            Me.txtCountry.Text = ""
            Me.rblSex.SelectedIndex = -1
        End If
    End Sub

    Protected Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Response.Redirect("http://localhost:1000/IPTSubscriberWeb/")
    End Sub

    Protected Sub cmdOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        Try
            Dim Data As Subscriber.ClsAccount = Subscriber.ClsAccount.NewAccount

            Data.Login = Me.txtWebLogin.Text
            Data.WebPassword = Me.txtWebPassword.Text
            Data.ComputerSerialNumber = Me.txtSerialNbr.Text
            Data.ClientPassword = Me.txtComputerPassword.Text
            Data.ContactEmail = Me.txtEmail.Text

            Data.Demographics.AddNew()
            Data.Demographics.Item(Data.Demographics.Count - 1).Tag = "First Name"
            Data.Demographics.Item(Data.Demographics.Count - 1).Answer = Me.txtFirstName.Text

            Data.Demographics.AddNew()
            Data.Demographics.Item(Data.Demographics.Count - 1).Tag = "Last Name"
            Data.Demographics.Item(Data.Demographics.Count - 1).Answer = Me.txtLastName.Text

            Data.Demographics.AddNew()
            Data.Demographics.Item(Data.Demographics.Count - 1).Tag = "Age"
            Data.Demographics.Item(Data.Demographics.Count - 1).Answer = Me.txtAge.Text

            Data.Demographics.AddNew()
            Data.Demographics.Item(Data.Demographics.Count - 1).Tag = "Sex"
            Data.Demographics.Item(Data.Demographics.Count - 1).Answer = Me.rblSex.SelectedValue

            Data.Demographics.AddNew()
            Data.Demographics.Item(Data.Demographics.Count - 1).Tag = "Occupation"
            Data.Demographics.Item(Data.Demographics.Count - 1).Answer = Me.txtOccupation.Text

            Data.Demographics.AddNew()
            Data.Demographics.Item(Data.Demographics.Count - 1).Tag = "City"
            Data.Demographics.Item(Data.Demographics.Count - 1).Answer = Me.txtCity.Text

            Data.Demographics.AddNew()
            Data.Demographics.Item(Data.Demographics.Count - 1).Tag = "State"
            Data.Demographics.Item(Data.Demographics.Count - 1).Answer = Me.txtState.Text

            Data.Demographics.AddNew()
            Data.Demographics.Item(Data.Demographics.Count - 1).Tag = "Country"
            Data.Demographics.Item(Data.Demographics.Count - 1).Answer = Me.txtCountry.Text

            Data.Save()
            Me.cmdDownload.Enabled = True
            Me.cmdDownload.Focus()
        Catch DataEx As Csla.DataPortalException
            Me.MsgBox.ShowMessage(DataEx.BusinessException.Message)
        Catch SysEx As Exception
            Me.MsgBox.ShowMessage(SysEx.Message)
        End Try

    End Sub

    Protected Sub cmdDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDownload.Click
        Response.Redirect("System/IPTrackingSystem.zip")
    End Sub

End Class
