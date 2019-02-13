Imports SCT.Library
Imports SCT.DataAccess

Public Class Form1

    Private mClient As Subscriber.ClsAccount

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Try
        '    Dim projectList As SCT.Library.Advertiser.ClsProjectList = SCT.Library.Advertiser.ClsProjectList.GetProjectList(1)

        '    For Each project As SCT.Library.Advertiser.ClsProject In projectList
        '        'Dim AdHistory As SCT.Library.Advertiser.ClsAdHistory = SCT.Library.Advertiser.ClsAdHistory.GetAdHistory(11)
        '        Dim AdHistory As SCT.Library.Advertiser.ClsAdHistory = SCT.Library.Advertiser.ClsAdHistory.NewAdHistory
        '        AdHistory.AssignAdvertiserAccount(project.AdvertiserAccount.ID)
        '        AdHistory.AssignSubscriberAccount(1)
        '        AdHistory.Save()

        '    Next

        'Catch ex As Csla.DataPortalException
        '    MessageBox.Show(ex.BusinessException.Message, "Error loading", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, "Error loading", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        'End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Dim data As Subscriber.ClsAccount = Subscriber.ClsAccount.NewAccount
        'data.Login = "jhj"
        'data.ContactEmail = "khjh"
        'data.WebPassword = "jhk"
        'data.ClientPassword = "sdff"
        'data.ComputerSerialNumber = "2432"
        'data.Save()
        Dim l As BinnacleFormEntryData() = AdminSite.ClsSessionAdmin.GetBinnacleFormEntryList(New Logs() {1}, New SearchCriteriaList(Of Long)(New Long() {0}, 1), New SearchCriteria(Of Date)("2008-1-1", 1), New SearchCriteria(Of Date)("2008-12-31", 1), New SearchCriteriaList(Of Long)(New Long() {0}, 1), New SearchCriteriaList(Of Long)(New Long() {0}, 1))
    End Sub
End Class
