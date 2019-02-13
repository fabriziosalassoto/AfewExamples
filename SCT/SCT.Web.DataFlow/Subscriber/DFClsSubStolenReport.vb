Imports System.Web.UI.WebControls

Public Class DFClsSubStolenReport

#Region " Private Fields "
    Private mForm As Object
    Private mAccountData As SCTServer.ClsSubscriberAccountData
    Private mStolenReportData As SCTServer.ClsSubscriberStolenReportData
#End Region

#Region " Properties And Methods "

    Public WriteOnly Property Form() As Object
        Set(ByVal value As Object)
            Me.mForm = value

        End Set
    End Property

    Public Sub New(ByRef form As Object)
        Me.mForm = form
        Me.mForm.Session("ValuePath") = "frmSubscriberStolenReport"
        Me.LoadActionCombo()
        Me.mAccountData = Me.GetData(Mid(Csla.ApplicationContext.User.Identity.Name, 1, Csla.ApplicationContext.User.Identity.Name.IndexOf(";")))
        If Me.mAccountData.StolenReport IsNot Nothing Then
            Me.mStolenReportData = Me.mAccountData.StolenReport
            Me.SetAddMode(False)
        Else
            Me.mStolenReportData = Me.NewStolenReportData
            Me.SetAddMode(True)
        End If
        Me.PopulateData()
    End Sub

    Private Function NewStolenReportData() As SCTServer.ClsSubscriberStolenReportData
        Dim StolenReportData As New SCTServer.ClsSubscriberStolenReportData
        StolenReportData.DateReportMissing = Date.Today
        StolenReportData.DateReportFound = "1900-01-01"
        StolenReportData.LastKnownLocationDescription = ""
        StolenReportData.ActiveForAlerts = True
        StolenReportData.ActionToTake = 1
        Return StolenReportData
    End Function

    Private Sub SetDeactivatedMode()
        Me.mForm.FindControls("txtLasKnownLocation").ReadOnly = True
        Me.mForm.FindControls("txtLasKnownLocation").BackColor = Drawing.Color.LightYellow
        Me.mForm.FindControls("ddlAction").Enabled = False
        Me.mForm.FindControls("ddlAction").BackColor = Drawing.Color.LightYellow
        Me.mForm.FindControls("rblAction").Enabled = False
        Me.mForm.FindControls("cmdApply").Enabled = False
        Me.mForm.FindControls("cmdOk").Enabled = False
    End Sub

    Private Sub SetAddMode(ByVal value As Boolean)
        Me.mForm.FindControls("cmdOk").AddMode = value
        Me.mForm.FindControls("cmdapply").AddMode = value
        Me.mForm.FindControls("rblAction").Enabled = Not value
    End Sub

    Private Sub LoadCombo(ByVal combo As DropDownList, ByVal data As DataTable)
        combo.DataSource = data
        combo.DataTextField = data.Columns(0).Caption
        combo.DataValueField = data.Columns(1).Caption
        combo.DataBind()
    End Sub

    Public Sub LoadActionCombo()
        LoadCombo(Me.mForm.FindControls("ddlAction"), Me.GetActionsData)
    End Sub

    Private Function GetActionsData() As DataTable
        Dim table As New DataTable
        table.ReadXml(Me.mForm.MapPath("~") & "/App_Data/Actions.xml")
        table.DefaultView.Sort = "Text"
        Return table
    End Function

    Public Sub OnOk_Add()
        If Me.AddData() Then
            Me.mForm.Response.Redirect("~/frmsSubscriber/frmSubscriberStatus.aspx")
        End If
    End Sub

    Public Sub OnOk_Edit()
        If Me.EditData() Then
            Me.mForm.Response.Redirect("~/frmsSubscriber/frmSubscriberStatus.aspx")
        End If
    End Sub

    Public Sub OnReurn()
        Me.mForm.Response.Redirect("~/frmsSubscriber/frmSubscriberStatus.aspx")
    End Sub

    Public Sub OnApply_Add()
        If Me.AddData() Then
            Me.SetAddMode(False)
        End If
    End Sub

    Public Sub OnApply_Edit()
        If Me.EditData() Then
            If Not Me.mStolenReportData.ActiveForAlerts Then
                Me.SetDeactivatedMode()
            End If
        End If
    End Sub

#End Region

#Region " Data Methods "

    Private Sub CollectData()
        Me.mStolenReportData.LastKnownLocationDescription = Me.mForm.FindControls("txtLasKnownLocation").Text
        Me.mStolenReportData.ActionToTake = Me.mForm.FindControls("ddlAction").SelectedValue

        Select Case Me.mForm.FindControls("rblAction").SelectedValue
            Case "ComputerFound"
                Me.mStolenReportData.DateReportFound = Date.Today
                Me.mStolenReportData.ActiveForAlerts = False
            Case "DisactivateAlert"
                Me.mStolenReportData.ActiveForAlerts = False
            Case Else
        End Select
    End Sub

    Private Sub PopulateData()
        Me.mForm.FindControls("txtSerialNbr").Text = Me.mAccountData.ComputerSerialNumber
        Me.mForm.FindControls("txtDateMissing").Text = Format(Me.mStolenReportData.DateReportMissing, "MMMM dd, yyyy")
        Me.mForm.FindControls("txtLasKnownLocation").Text = Me.mStolenReportData.LastKnownLocationDescription
        If Me.mForm.FindControls("ddlAction").Items.FindByValue(Me.mStolenReportData.ActionToTake) IsNot Nothing Then
            Me.mForm.FindControls("ddlAction").SelectedValue = Me.mStolenReportData.ActionToTake
        End If
        Me.mForm.FindControls("rblAction").Items(0).Selected = False
        Me.mForm.FindControls("rblAction").Items(1).Selected = False
    End Sub

    Private Function AddData() As Boolean
        Using svc As New SCTServer.SCTServer
            Try
                Me.CollectData()
                svc.AddSubsciberStolenReport(Me.mAccountData.ID, Me.mStolenReportData)
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

    Private Function EditData() As Boolean
        Using svc As New SCTServer.SCTServer
            Try
                Me.CollectData()
                svc.EditSubsciberStolenReport(Me.mAccountData.ID, Me.mStolenReportData)
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
