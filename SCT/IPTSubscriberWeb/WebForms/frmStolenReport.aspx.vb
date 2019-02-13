Imports SCT.Library

Partial Class WebForms_frmStolenReport
    Inherits System.Web.UI.Page

    Dim mData As Subscriber.ClsAccount
    Dim mStolenReport As Subscriber.ClsStolenReport

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.Session("ValuePath") = "StolenReports"
            Me.mData = Me.GetData(Mid(Csla.ApplicationContext.User.Identity.Name, 1, Csla.ApplicationContext.User.Identity.Name.IndexOf(";")))
            Me.PopulateData()
        Else
            mData = Session("Data")
            mStolenReport = Session("StolenReport")
        End If
    End Sub

    Private Function GetData(ByVal accountId As Long) As Subscriber.ClsAccount
        Try
            Return Subscriber.ClsAccount.GetAccount(accountId)
        Catch DataEx As Csla.DataPortalException
            Me.MsgBox.ShowMessage(DataEx.BusinessException.Message)
            Return Nothing
        Catch SysEx As Exception
            Me.MsgBox.ShowMessage(SysEx.Message)
            Return Nothing
        End Try
    End Function

    Private Sub PopulateData()
        If Me.mData Is Nothing Then
            Response.Redirect("frmLocation.aspx")
        Else
            Me.txtID.Text = Me.mData.ID
            Me.txtSerialNbr.Text = Me.mData.ComputerSerialNumber
            If Me.mData.GetStolenReport Is Nothing Then
                Me.mStolenReport = Subscriber.ClsStolenReport.NewStolenReport
                Me.txtDateMissing.Text = Format(Date.Today, "MM-dd-yyyy")
                Me.txtDateFound.Text = ""
                Me.txtLasKnownLocation.Text = ""
                Me.cmdFound.Visible = False
            Else
                Me.mStolenReport = Subscriber.ClsStolenReport.GetStolenReport(Me.mData.GetStolenReport.ID)
                Me.txtDateMissing.Text = Format(Me.mStolenReport.DateReportMissing, "MM-dd-yyyy")
                If Me.mStolenReport.DateReportFound = "1900-01-01" Then
                    Me.txtDateFound.Text = ""
                Else
                    Me.txtDateFound.Text = Format(Me.mStolenReport.DateReportFound, "MM-dd-yyyy")
                End If
                Me.txtLasKnownLocation.Text = Me.mStolenReport.LastKnownLocationDescription
                Me.ddlAction.SelectedValue = Me.mStolenReport.ActionToTake
                Me.cmdFound.Visible = True
            End If
        End If
    End Sub

    Protected Sub cmdOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        Try
            Me.mStolenReport.AssignSubscriberAccount(Me.mData.ID)
            Me.mStolenReport.DateReportMissing = Date.Today
            Me.mStolenReport.LastKnownLocationDescription = Me.txtLasKnownLocation.Text
            If Me.txtDateFound.Text <> "" Then
                Me.mStolenReport.DateReportFound = Date.Today
            Else
                Me.mStolenReport.DateReportFound = "1900-01-01"
            End If
            Me.mStolenReport.ActionToTake = Me.ddlAction.SelectedValue
            Me.mStolenReport.Save()
            Me.Response.Redirect("frmLocation.aspx")
        Catch DataEx As Csla.DataPortalException
            Me.MsgBox.ShowMessage(DataEx.BusinessException.Message)
        Catch SysEx As Exception
            Me.MsgBox.ShowMessage(SysEx.Message)
        End Try
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Response.Redirect("frmLocation.aspx")
    End Sub

    Protected Sub cmdFound_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFound.Click
        Me.txtDateFound.Text = Format(Date.Today, "MM-dd-yyyy")
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        Session("Data") = mData
        Session("StolenReport") = mStolenReport
    End Sub
End Class
