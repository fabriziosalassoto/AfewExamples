Imports SCT.Library
Imports System.Data

Partial Class WebForms_frmStatistics
    Inherits System.Web.UI.Page

    Dim mData As Advertiser.ClsAccount

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Session("ValuePath") = "Statistics"
        Me.mData = Me.GetData(Mid(Csla.ApplicationContext.User.Identity.Name, 1, Csla.ApplicationContext.User.Identity.Name.IndexOf(";")))
        Me.LoadGridView()
    End Sub

    Private Function GetData(ByVal accountId As Long) As Advertiser.ClsAccount
        Try
            Return Advertiser.ClsAccount.GetAccount(accountId)
        Catch DataEx As Csla.DataPortalException
            Me.MsgBox.ShowMessage(DataEx.BusinessException.Message)
            Return Nothing
        Catch SysEx As Exception
            Me.MsgBox.ShowMessage(SysEx.Message)
            Return Nothing
        End Try
    End Function

    Private Sub LoadGridView()
        GridView.DataSource = GetDataSources()
        GridView.DataBind()
    End Sub

    Private Function GetDataSources() As DataTable
        Dim table As New DataTable
        Dim Displays As Long = 0
        Dim ClickThru As Long = 0

        table.Columns.Add("ProjectID", GetType(String))
        table.Columns.Add("URL", GetType(String))
        table.Columns.Add("Displays", GetType(String))
        table.Columns.Add("ClickThru", GetType(String))

        If Me.mData IsNot Nothing Then
            For Each project As Advertiser.ClsProject In Me.mData.Projects
                For Each AdHistory As Advertiser.ClsAdHistory In Me.mData.AdHistories
                    If AdHistory.URLAdDisplay = project.ADUrl Then
                        Displays += 1
                        If AdHistory.URLAdClickThru <> "" Then
                            ClickThru += 1
                        End If
                    End If
                Next
                table.Rows.Add(New Object() {project.ID, project.ADUrl, Displays.ToString, ClickThru.ToString})
            Next
        End If
        Return table
    End Function

End Class
