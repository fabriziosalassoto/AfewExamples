Imports SCT.Library
Imports System.Web.UI.WebControls

Public Class DFClsAdProjectStatistics

#Region " Private Fields "
    Private mForm As Object
    Private mData As Advertiser.ClsAccount
#End Region

#Region " Properties And Methods "

    Public WriteOnly Property Form() As Object
        Set(ByVal value As Object)
            Me.mForm = value
        End Set
    End Property

    Public Sub New(ByRef form As Object)
        Me.mForm = form
        Me.mForm.Session("ValuePath") = "frmAdvertiserProjectStatistics"
        Me.mData = Me.GetData(Mid(Csla.ApplicationContext.User.Identity.Name, 1, Csla.ApplicationContext.User.Identity.Name.IndexOf(";")))
        Me.PopulateData(0)
    End Sub

    Private Sub LoadGridView(ByVal gridview As GridView, ByVal PageIndex As Integer)
        gridview.PageIndex = PageIndex
        gridview.DataSource = GetDataSources()
        gridview.DataBind()
    End Sub

    Private Function GetDataSources() As DataTable
        Dim table As New DataTable
        Dim Displays As Long
        Dim ClickThru As Long

        table.Columns.Add("ID", GetType(String))
        table.Columns.Add("URL", GetType(String))
        table.Columns.Add("Description", GetType(String))
        table.Columns.Add("Displays", GetType(String))
        table.Columns.Add("ClickThru", GetType(String))

        For Each project As Advertiser.ClsProject In Me.mData.Projects
            Displays = 0
            ClickThru = 0
            For Each AdHistory As Advertiser.Project.ClsAdHistory In project.AdHistories
                If Len(AdHistory.URLAdDisplay) > 0 Then
                    Displays += 1
                    If Len(AdHistory.URLAdClickThru) > 0 Then
                        ClickThru += 1
                    End If
                End If
            Next
            table.Rows.Add(New Object() {project.ID, project.ADUrl, project.ProjectDescription, Displays.ToString, ClickThru.ToString})
        Next
        Return table
    End Function

    Public Sub OnPageIndexChanging(ByVal NewPageIndex As Integer)
        Me.PopulateData(NewPageIndex)
    End Sub

    Public Sub OnProject(ByVal projectId As Long)
        Me.mForm.Session("projectID") = projectId
        Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdProjectInformation.aspx")
    End Sub

#End Region

#Region " Data Methods "

    Private Sub PopulateData(ByVal PageIndex As Integer)
        Me.LoadGridView(Me.mForm.FindControls("GridView"), PageIndex)
    End Sub

    Private Function GetData(ByVal accountId) As Advertiser.ClsAccount
        Try
            Return Advertiser.ClsAccount.GetAccount(accountId)
        Catch DataEx As Csla.DataPortalException
            Me.mForm.FindControls("MsgBox").ShowMessage(DataEx.BusinessException.Message)
            Return Advertiser.ClsAccount.NewAccount
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage(SysEx.Message)
            Return Advertiser.ClsAccount.NewAccount
        End Try
    End Function

#End Region

End Class
