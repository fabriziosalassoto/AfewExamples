Imports SCT.Library
Imports SCT.Library.AdminSite
Imports System.Globalization
Imports System.Web.UI.WebControls

Public Class DFClsAdvertiserProject

#Region " Private Fields "

    Private mForm As Object
    Private mData As AdProjectData

    Private mEditMode As Boolean

#End Region

#Region " Authorization Rules "

    Public Shared Function CanSelect() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserProject") Then
            Return ClsSessionAdmin.CanSelectInForm("frmAdvertiserProject")
        Else
            Return ClsSessionAdmin.CanSelectInForm("AllForms")
        End If
    End Function

    Public Shared Function CanInsert() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserProject") Then
            Return ClsSessionAdmin.CanInsertInForm("frmAdvertiserProject")
        Else
            Return ClsSessionAdmin.CanInsertInForm("AllForms")
        End If
    End Function

    Public Shared Function CanUpdate() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserProject") Then
            Return ClsSessionAdmin.CanUpdateInForm("frmAdvertiserProject")
        Else
            Return ClsSessionAdmin.CanUpdateInForm("AllForms")
        End If
    End Function

    Public Shared Function CanDelete() As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserProject") Then
            Return ClsSessionAdmin.CanDeleteInForm("frmAdvertiserProject")
        Else
            Return ClsSessionAdmin.CanDeleteInForm("AllForms")
        End If
    End Function

    Public Shared Function CanSelectField(ByVal fieldName As String) As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserProject") Then
            Return ClsSessionAdmin.CanSelectFieldInForm("frmAdvertiserProject", fieldName)
        Else
            Return ClsSessionAdmin.CanSelectFieldInForm("AllForms", "AllFields")
        End If
    End Function

    Public Shared Function CanInsertField(ByVal fieldName As String) As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserProject") Then
            Return ClsSessionAdmin.CanInsertFieldInForm("frmAdvertiserProject", fieldName)
        Else
            Return ClsSessionAdmin.CanInsertFieldInForm("AllForms", "AllFields")
        End If
    End Function

    Public Shared Function CanUpdateField(ByVal fieldName As String) As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserProject") Then
            Return ClsSessionAdmin.CanUpdateFieldInForm("frmAdvertiserProject", fieldName)
        Else
            Return ClsSessionAdmin.CanUpdateFieldInForm("AllForms", "AllFields")
        End If
    End Function

    Public Shared Function CanDeleteField(ByVal fieldName As String) As Boolean
        If ClsSessionAdmin.ContainsUserProfileInForm("frmAdvertiserProject") Then
            Return ClsSessionAdmin.CanDeleteFieldInForm("frmAdvertiserProject", fieldName)
        Else
            Return ClsSessionAdmin.CanDeleteFieldInForm("AllForms", "AllFields")
        End If
    End Function

#End Region

#Region " Properties And Methods "

    Public WriteOnly Property Form() As Object
        Set(ByVal value As Object)
            Me.mForm = value
        End Set
    End Property

    Public Sub New(ByRef form As Object)
        Me.mForm = form
        Me.mForm.Session("ValuePath") = "frmAdvertisers/frmAdvertiserProjects"

        Me.ApplyPageAuthorizationRules()

        If (Not IsNumeric(Me.mForm.Session("ID1"))) OrElse Me.mForm.Session("ID1") = 0 Then
            Me.mData = New AdProjectData
            Me.ApplyAddFormAuthorizationRules(False)
            Me.ClearAddForm()
        Else
            Me.mData = Me.GetData(Me.mForm.Session("ID1"))
            If Me.mData IsNot Nothing Then
                Me.ApplyAddFormAuthorizationRules(True)
                Me.PopulateData()
            End If
        End If
    End Sub

    Public Sub ApplyPageAuthorizationRules()
        Dim SelectField As Boolean

        If (Not ClsSessionAdmin.IsValidForm("frmAdvertiserProject", "txtAdProjectID", "ddlAdProjectAdvertiser", "ddlAdProjectContact", "txtAdProjectADUrl", "txtAdProjectDescription", "txtAdProjectHeight", "txtAdProjectWidth", "ddlAdProjectRunStartDate", "ddlAdProjectRunEndDate", "ddlAdProjectStartTime", "ddlAdProjectEndTime", "txtAdProjectMinDisplay", "txtAdProjectMaxDisplay", "txtAdProjectMinPerDay", "txtAdProjectMaxPerDay", "txtAdProjectCountDisplayed", "ddlAdProjectVerifiedDate", "ddlAdProjectOnlineDate", "txtAdProjectPromoCode", "txtAdProjectComissionCode", "ddlAdProjectMinAge", "ddlAdProjectMaxAge", "ddlAdProjectSex", "ddlAdProjectOccupation", "ddlAdProjectCountry", "ddlAdProjectState")) OrElse (Not DFClsAdvertiserProject.CanSelect()) Then Me.mForm.Response.Redirect("~/Forms/frmMain.aspx")

        SelectField = DFClsAdvertiserProject.CanSelectField("txtAdProjectID")
        Me.mForm.FindControls("txtAdProjectID").Visible = SelectField
        Me.mForm.FindControls("lblAdProjectID").Visible = SelectField
        Me.mForm.FindControls("txtAdProjectID").Enabled = False

        SelectField = DFClsAdvertiserProject.CanSelectField("ddlAdProjectAdvertiser")
        Me.mForm.FindControls("ddlAdProjectAdvertiser").Visible = SelectField
        Me.mForm.FindControls("lblAdProjectAdvertiser").Visible = SelectField
        Me.mForm.FindControls("ddlAdProjectAdvertiser").Enabled = False

        SelectField = DFClsAdvertiserProject.CanSelectField("ddlAdProjectAdvertiser")
        Me.mForm.FindControls("ddlAdProjectContact").Visible = SelectField
        Me.mForm.FindControls("lblAdProjectContact").Visible = SelectField
        Me.mForm.FindControls("ddlAdProjectContact").Enabled = False

        SelectField = DFClsAdvertiserProject.CanSelectField("txtAdProjectADUrl")
        Me.mForm.FindControls("txtAdProjectADUrl").Visible = SelectField
        Me.mForm.FindControls("lblAdProjectADUrl").Visible = SelectField
        Me.mForm.FindControls("txtAdProjectADUrl").Enabled = False

        SelectField = DFClsAdvertiserProject.CanSelectField("txtAdProjectDescription")
        Me.mForm.FindControls("txtAdProjectDescription").Visible = SelectField
        Me.mForm.FindControls("lblAdProjectDescription").Visible = SelectField
        Me.mForm.FindControls("txtAdProjectDescription").Enabled = False

        SelectField = DFClsAdvertiserProject.CanSelectField("txtAdProjectHeight")
        Me.mForm.FindControls("txtAdProjectHeight").Visible = SelectField
        Me.mForm.FindControls("lblAdProjectHeight").Visible = SelectField
        Me.mForm.FindControls("txtAdProjectHeight").Enabled = False

        SelectField = DFClsAdvertiserProject.CanSelectField("txtAdProjectWidth")
        Me.mForm.FindControls("txtAdProjectWidth").Visible = SelectField
        Me.mForm.FindControls("lblAdProjectWidth").Visible = SelectField
        Me.mForm.FindControls("txtAdProjectWidth").Enabled = False

        SelectField = DFClsAdvertiserProject.CanSelectField("ddlAdProjectRunStartDate")
        Me.mForm.FindControls("ddlAdProjectStartMonth").Visible = SelectField
        Me.mForm.FindControls("ddlAdProjectStartDay").Visible = SelectField
        Me.mForm.FindControls("ddlAdProjectStartYear").Visible = SelectField
        Me.mForm.FindControls("lblAdProjectStartDate").Visible = SelectField
        Me.mForm.FindControls("ddlAdProjectStartMonth").Enabled = False
        Me.mForm.FindControls("ddlAdProjectStartDay").Enabled = False
        Me.mForm.FindControls("ddlAdProjectStartYear").Enabled = False

        SelectField = DFClsAdvertiserProject.CanSelectField("ddlAdProjectRunEndDate")
        Me.mForm.FindControls("ddlAdProjectEndMonth").Visible = SelectField
        Me.mForm.FindControls("ddlAdProjectEndDay").Visible = SelectField
        Me.mForm.FindControls("ddlAdProjectEndYear").Visible = SelectField
        Me.mForm.FindControls("lblAdProjectEndDate").Visible = SelectField
        Me.mForm.FindControls("ddlAdProjectEndMonth").Enabled = False
        Me.mForm.FindControls("ddlAdProjectEndDay").Enabled = False
        Me.mForm.FindControls("ddlAdProjectEndYear").Enabled = False

        SelectField = DFClsAdvertiserProject.CanSelectField("ddlAdProjectStartTime")
        Me.mForm.FindControls("ddlAdProjectStartHour").Visible = SelectField
        Me.mForm.FindControls("ddlAdProjectStartMinute").Visible = SelectField
        Me.mForm.FindControls("ddlAdProjectStartAMPM").Visible = SelectField
        Me.mForm.FindControls("lblAdProjectStartTime").Visible = SelectField
        Me.mForm.FindControls("ddlAdProjectStartHour").Enabled = False
        Me.mForm.FindControls("ddlAdProjectStartMinute").Enabled = False
        Me.mForm.FindControls("ddlAdProjectStartAMPM").Enabled = False

        SelectField = DFClsAdvertiserProject.CanSelectField("ddlAdProjectEndTime")
        Me.mForm.FindControls("ddlAdProjectEndHour").Visible = SelectField
        Me.mForm.FindControls("ddlAdProjectEndMinute").Visible = SelectField
        Me.mForm.FindControls("ddlAdProjectEndAMPM").Visible = SelectField
        Me.mForm.FindControls("lblAdProjectEndTime").Visible = SelectField
        Me.mForm.FindControls("ddlAdProjectEndHour").Enabled = False
        Me.mForm.FindControls("ddlAdProjectEndMinute").Enabled = False
        Me.mForm.FindControls("ddlAdProjectEndAMPM").Enabled = False

        SelectField = DFClsAdvertiserProject.CanSelectField("txtAdProjectMinDisplay")
        Me.mForm.FindControls("txtAdProjectMinDisplay").Visible = SelectField
        Me.mForm.FindControls("lblAdProjectMinDisplay").Visible = SelectField
        Me.mForm.FindControls("txtAdProjectMinDisplay").Enabled = False

        SelectField = DFClsAdvertiserProject.CanSelectField("txtAdProjectMaxDisplay")
        Me.mForm.FindControls("txtAdProjectMaxDisplay").Visible = SelectField
        Me.mForm.FindControls("lblAdProjectMaxDisplay").Visible = SelectField
        Me.mForm.FindControls("txtAdProjectMaxDisplay").Enabled = False

        SelectField = DFClsAdvertiserProject.CanSelectField("txtAdProjectMinPerDay")
        Me.mForm.FindControls("txtAdProjectMinPerDay").Visible = SelectField
        Me.mForm.FindControls("lblAdProjectMinPerDay").Visible = SelectField
        Me.mForm.FindControls("txtAdProjectMinPerDay").Enabled = False

        SelectField = DFClsAdvertiserProject.CanSelectField("txtAdProjectMaxPerDay")
        Me.mForm.FindControls("txtAdProjectMaxPerDay").Visible = SelectField
        Me.mForm.FindControls("lblAdProjectMaxPerDay").Visible = SelectField
        Me.mForm.FindControls("txtAdProjectMaxPerDay").Enabled = False

        SelectField = DFClsAdvertiserProject.CanSelectField("txtAdProjectCountDisplayed")
        Me.mForm.FindControls("txtAdProjectCountDisplayed").Visible = SelectField
        Me.mForm.FindControls("lblAdProjectCountDisplayed").Visible = SelectField
        Me.mForm.FindControls("txtAdProjectCountDisplayed").Enabled = False

        SelectField = DFClsAdvertiserProject.CanSelectField("ddlAdProjectVerifiedDate")
        Me.mForm.FindControls("ddlAdProjectVerifiedMonth").Visible = SelectField
        Me.mForm.FindControls("ddlAdProjectVerifiedDay").Visible = SelectField
        Me.mForm.FindControls("ddlAdProjectVerifiedYear").Visible = SelectField
        Me.mForm.FindControls("lblAdProjectVerifiedDate").Visible = SelectField
        Me.mForm.FindControls("ddlAdProjectVerifiedMonth").Enabled = False
        Me.mForm.FindControls("ddlAdProjectVerifiedDay").Enabled = False
        Me.mForm.FindControls("ddlAdProjectVerifiedYear").Enabled = False

        SelectField = DFClsAdvertiserProject.CanSelectField("ddlAdProjectOnlineDate")
        Me.mForm.FindControls("ddlAdProjectOnlineMonth").Visible = SelectField
        Me.mForm.FindControls("ddlAdProjectOnlineDay").Visible = SelectField
        Me.mForm.FindControls("ddlAdProjectOnlineYear").Visible = SelectField
        Me.mForm.FindControls("lblAdProjectOnlineDate").Visible = SelectField
        Me.mForm.FindControls("ddlAdProjectOnlineMonth").Enabled = False
        Me.mForm.FindControls("ddlAdProjectOnlineDay").Enabled = False
        Me.mForm.FindControls("ddlAdProjectOnlineYear").Enabled = False

        SelectField = DFClsAdvertiserProject.CanSelectField("txtAdProjectPromoCode")
        Me.mForm.FindControls("txtAdProjectPromoCode").Visible = SelectField
        Me.mForm.FindControls("lblAdProjectPromoCode").Visible = SelectField
        Me.mForm.FindControls("txtAdProjectPromoCode").Enabled = False

        SelectField = DFClsAdvertiserProject.CanSelectField("txtAdProjectComissionCode")
        Me.mForm.FindControls("txtAdProjectComissionCode").Visible = SelectField
        Me.mForm.FindControls("lblAdProjectComissionCode").Visible = SelectField
        Me.mForm.FindControls("txtAdProjectComissionCode").Enabled = False

        SelectField = DFClsAdvertiserProject.CanSelectField("ddlAdProjectMinAge")
        Me.mForm.FindControls("ddlAdProjectMinAge").Visible = SelectField
        Me.mForm.FindControls("lblAdProjectMinAge").Visible = SelectField
        Me.mForm.FindControls("ddlAdProjectMinAge").Enabled = False

        SelectField = DFClsAdvertiserProject.CanSelectField("ddlAdProjectMaxAge")
        Me.mForm.FindControls("ddlAdProjectMaxAge").Visible = SelectField
        Me.mForm.FindControls("lblAdProjectMaxAge").Visible = SelectField
        Me.mForm.FindControls("ddlAdProjectMaxAge").Enabled = False

        SelectField = DFClsAdvertiserProject.CanSelectField("ddlAdProjectSex")
        Me.mForm.FindControls("ddlAdProjectSex").Visible = SelectField
        Me.mForm.FindControls("lblAdProjectSex").Visible = SelectField
        Me.mForm.FindControls("ddlAdProjectSex").Enabled = False

        SelectField = DFClsAdvertiserProject.CanSelectField("ddlAdProjectOccupation")
        Me.mForm.FindControls("ddlAdProjectOccupation").Visible = SelectField
        Me.mForm.FindControls("lblAdProjectOccupation").Visible = SelectField
        Me.mForm.FindControls("ddlAdProjectOccupation").Enabled = False

        SelectField = DFClsAdvertiserProject.CanSelectField("ddlAdProjectCountry")
        Me.mForm.FindControls("ddlAdProjectCountry").Visible = SelectField
        Me.mForm.FindControls("lblAdProjectCountry").Visible = SelectField
        Me.mForm.FindControls("ddlAdProjectCountry").Enabled = False

        SelectField = DFClsAdvertiserProject.CanSelectField("ddlAdProjectState")
        Me.mForm.FindControls("ddlAdProjectState").Visible = SelectField
        Me.mForm.FindControls("lblAdProjectState").Visible = SelectField
        Me.mForm.FindControls("ddlAdProjectState").Enabled = False

        Me.mForm.FindControls("mnuItem").FindItem("Advertiser").Enabled = False
        Me.mForm.FindControls("mnuItem").FindItem("Contact").Enabled = False
        Me.mForm.FindControls("mnuItem").FindItem("Delete").Enabled = False

        Me.mForm.FindControls("mnuSave").FindItem("Ok").Enabled = False
        Me.mForm.FindControls("mnuSave").FindItem("Save").Enabled = False
    End Sub

    Public Sub ApplyAddFormAuthorizationRules(ByVal editMode As Boolean)
        Dim canSave As Boolean

        Me.mEditMode = editMode

        Me.mForm.FindControls("ddlAdProjectAdvertiser").Enabled = (Not editMode AndAlso DFClsAdvertiserProject.CanInsertField("ddlAdProjectAdvertiser")) OrElse (editMode AndAlso DFClsAdvertiserProject.CanUpdateField("ddlAdProjectAdvertiser"))
        Me.mForm.FindControls("ddlAdProjectContact").Enabled = (Not editMode AndAlso DFClsAdvertiserProject.CanInsertField("ddlAdProjectContact")) OrElse (editMode AndAlso DFClsAdvertiserProject.CanUpdateField("ddlAdProjectContact"))
        Me.mForm.FindControls("txtAdProjectADUrl").Enabled = (Not editMode AndAlso DFClsAdvertiserProject.CanInsertField("txtAdProjectADUrl")) OrElse (editMode AndAlso DFClsAdvertiserProject.CanUpdateField("txtAdProjectADUrl"))
        Me.mForm.FindControls("txtAdProjectDescription").Enabled = (Not editMode AndAlso DFClsAdvertiserProject.CanInsertField("txtAdProjectDescription")) OrElse (editMode AndAlso DFClsAdvertiserProject.CanUpdateField("txtAdProjectDescription"))
        Me.mForm.FindControls("txtAdProjectHeight").Enabled = (Not editMode AndAlso DFClsAdvertiserProject.CanInsertField("txtAdProjectHeight")) OrElse (editMode AndAlso DFClsAdvertiserProject.CanUpdateField("txtAdProjectHeight"))
        Me.mForm.FindControls("txtAdProjectWidth").Enabled = (Not editMode AndAlso DFClsAdvertiserProject.CanInsertField("txtAdProjectWidth")) OrElse (editMode AndAlso DFClsAdvertiserProject.CanUpdateField("txtAdProjectWidth"))
        Me.mForm.FindControls("ddlAdProjectStartMonth").Enabled = (Not editMode AndAlso DFClsAdvertiserProject.CanInsertField("ddlAdProjectRunStartDate")) OrElse (editMode AndAlso DFClsAdvertiserProject.CanUpdateField("ddlAdProjectRunStartDate"))
        Me.mForm.FindControls("ddlAdProjectStartDay").Enabled = (Not editMode AndAlso DFClsAdvertiserProject.CanInsertField("ddlAdProjectRunStartDate")) OrElse (editMode AndAlso DFClsAdvertiserProject.CanUpdateField("ddlAdProjectRunStartDate"))
        Me.mForm.FindControls("ddlAdProjectStartYear").Enabled = (Not editMode AndAlso DFClsAdvertiserProject.CanInsertField("ddlAdProjectRunStartDate")) OrElse (editMode AndAlso DFClsAdvertiserProject.CanUpdateField("ddlAdProjectRunStartDate"))
        Me.mForm.FindControls("ddlAdProjectEndMonth").Enabled = (Not editMode AndAlso DFClsAdvertiserProject.CanInsertField("ddlAdProjectRunEndDate")) OrElse (editMode AndAlso DFClsAdvertiserProject.CanUpdateField("ddlAdProjectRunEndDate"))
        Me.mForm.FindControls("ddlAdProjectEndDay").Enabled = (Not editMode AndAlso DFClsAdvertiserProject.CanInsertField("ddlAdProjectRunEndDate")) OrElse (editMode AndAlso DFClsAdvertiserProject.CanUpdateField("ddlAdProjectRunEndDate"))
        Me.mForm.FindControls("ddlAdProjectEndYear").Enabled = (Not editMode AndAlso DFClsAdvertiserProject.CanInsertField("ddlAdProjectRunEndDate")) OrElse (editMode AndAlso DFClsAdvertiserProject.CanUpdateField("ddlAdProjectRunEndDate"))
        Me.mForm.FindControls("ddlAdProjectStartHour").Enabled = (Not editMode AndAlso DFClsAdvertiserProject.CanInsertField("ddlAdProjectStartTime")) OrElse (editMode AndAlso DFClsAdvertiserProject.CanUpdateField("ddlAdProjectStartTime"))
        Me.mForm.FindControls("ddlAdProjectStartMinute").Enabled = (Not editMode AndAlso DFClsAdvertiserProject.CanInsertField("ddlAdProjectStartTime")) OrElse (editMode AndAlso DFClsAdvertiserProject.CanUpdateField("ddlAdProjectStartTime"))
        Me.mForm.FindControls("ddlAdProjectStartAMPM").Enabled = (Not editMode AndAlso DFClsAdvertiserProject.CanInsertField("ddlAdProjectStartTime")) OrElse (editMode AndAlso DFClsAdvertiserProject.CanUpdateField("ddlAdProjectStartTime"))
        Me.mForm.FindControls("ddlAdProjectEndHour").Enabled = (Not editMode AndAlso DFClsAdvertiserProject.CanInsertField("ddlAdProjectEndTime")) OrElse (editMode AndAlso DFClsAdvertiserProject.CanUpdateField("ddlAdProjectEndTime"))
        Me.mForm.FindControls("ddlAdProjectEndMinute").Enabled = (Not editMode AndAlso DFClsAdvertiserProject.CanInsertField("ddlAdProjectEndTime")) OrElse (editMode AndAlso DFClsAdvertiserProject.CanUpdateField("ddlAdProjectEndTime"))
        Me.mForm.FindControls("ddlAdProjectEndAMPM").Enabled = (Not editMode AndAlso DFClsAdvertiserProject.CanInsertField("ddlAdProjectEndTime")) OrElse (editMode AndAlso DFClsAdvertiserProject.CanUpdateField("ddlAdProjectEndTime"))
        Me.mForm.FindControls("txtAdProjectMinDisplay").Enabled = (Not editMode AndAlso DFClsAdvertiserProject.CanInsertField("txtAdProjectMinDisplay")) OrElse (editMode AndAlso DFClsAdvertiserProject.CanUpdateField("txtAdProjectMinDisplay"))
        Me.mForm.FindControls("txtAdProjectMaxDisplay").Enabled = (Not editMode AndAlso DFClsAdvertiserProject.CanInsertField("txtAdProjectMaxDisplay")) OrElse (editMode AndAlso DFClsAdvertiserProject.CanUpdateField("txtAdProjectMaxDisplay"))
        Me.mForm.FindControls("txtAdProjectMinPerDay").Enabled = (Not editMode AndAlso DFClsAdvertiserProject.CanInsertField("txtAdProjectMinPerDay")) OrElse (editMode AndAlso DFClsAdvertiserProject.CanUpdateField("txtAdProjectMinPerDay"))
        Me.mForm.FindControls("txtAdProjectMaxPerDay").Enabled = (Not editMode AndAlso DFClsAdvertiserProject.CanInsertField("txtAdProjectMaxPerDay")) OrElse (editMode AndAlso DFClsAdvertiserProject.CanUpdateField("txtAdProjectMaxPerDay"))
        Me.mForm.FindControls("txtAdProjectCountDisplayed").Enabled = (Not editMode AndAlso DFClsAdvertiserProject.CanInsertField("txtAdProjectCountDisplayed")) OrElse (editMode AndAlso DFClsAdvertiserProject.CanUpdateField("txtAdProjectCountDisplayed"))
        Me.mForm.FindControls("ddlAdProjectVerifiedMonth").Enabled = (Not editMode AndAlso DFClsAdvertiserProject.CanInsertField("ddlAdProjectVerifiedDate")) OrElse (editMode AndAlso DFClsAdvertiserProject.CanUpdateField("ddlAdProjectVerifiedDate"))
        Me.mForm.FindControls("ddlAdProjectVerifiedDay").Enabled = (Not editMode AndAlso DFClsAdvertiserProject.CanInsertField("ddlAdProjectVerifiedDate")) OrElse (editMode AndAlso DFClsAdvertiserProject.CanUpdateField("ddlAdProjectVerifiedDate"))
        Me.mForm.FindControls("ddlAdProjectVerifiedYear").Enabled = (Not editMode AndAlso DFClsAdvertiserProject.CanInsertField("ddlAdProjectVerifiedDate")) OrElse (editMode AndAlso DFClsAdvertiserProject.CanUpdateField("ddlAdProjectVerifiedDate"))
        Me.mForm.FindControls("ddlAdProjectOnlineMonth").Enabled = (Not editMode AndAlso DFClsAdvertiserProject.CanInsertField("ddlAdProjectOnlineDate")) OrElse (editMode AndAlso DFClsAdvertiserProject.CanUpdateField("ddlAdProjectOnlineDate"))
        Me.mForm.FindControls("ddlAdProjectOnlineDay").Enabled = (Not editMode AndAlso DFClsAdvertiserProject.CanInsertField("ddlAdProjectOnlineDate")) OrElse (editMode AndAlso DFClsAdvertiserProject.CanUpdateField("ddlAdProjectOnlineDate"))
        Me.mForm.FindControls("ddlAdProjectOnlineYear").Enabled = (Not editMode AndAlso DFClsAdvertiserProject.CanInsertField("ddlAdProjectOnlineDate")) OrElse (editMode AndAlso DFClsAdvertiserProject.CanUpdateField("ddlAdProjectOnlineDate"))
        Me.mForm.FindControls("txtAdProjectPromoCode").Enabled = (Not editMode AndAlso DFClsAdvertiserProject.CanInsertField("txtAdProjectPromoCode")) OrElse (editMode AndAlso DFClsAdvertiserProject.CanUpdateField("txtAdProjectPromoCode"))
        Me.mForm.FindControls("txtAdProjectComissionCode").Enabled = (Not editMode AndAlso DFClsAdvertiserProject.CanInsertField("txtAdProjectComissionCode")) OrElse (editMode AndAlso DFClsAdvertiserProject.CanUpdateField("txtAdProjectComissionCode"))
        Me.mForm.FindControls("ddlAdProjectMinAge").Enabled = (Not editMode AndAlso DFClsAdvertiserProject.CanInsertField("ddlAdProjectMinAge")) OrElse (editMode AndAlso DFClsAdvertiserProject.CanUpdateField("ddlAdProjectMinAge"))
        Me.mForm.FindControls("ddlAdProjectMaxAge").Enabled = (Not editMode AndAlso DFClsAdvertiserProject.CanInsertField("ddlAdProjectMaxAge")) OrElse (editMode AndAlso DFClsAdvertiserProject.CanUpdateField("ddlAdProjectMaxAge"))
        Me.mForm.FindControls("ddlAdProjectSex").Enabled = (Not editMode AndAlso DFClsAdvertiserProject.CanInsertField("ddlAdProjectSex")) OrElse (editMode AndAlso DFClsAdvertiserProject.CanUpdateField("ddlAdProjectSex"))
        Me.mForm.FindControls("ddlAdProjectOccupation").Enabled = (Not editMode AndAlso DFClsAdvertiserProject.CanInsertField("ddlAdProjectOccupation")) OrElse (editMode AndAlso DFClsAdvertiserProject.CanUpdateField("ddlAdProjectOccupation"))
        Me.mForm.FindControls("ddlAdProjectCountry").Enabled = (Not editMode AndAlso DFClsAdvertiserProject.CanInsertField("ddlAdProjectCountry")) OrElse (editMode AndAlso DFClsAdvertiserProject.CanUpdateField("ddlAdProjectCountry"))
        Me.mForm.FindControls("ddlAdProjectState").Enabled = (Not editMode AndAlso DFClsAdvertiserProject.CanInsertField("ddlAdProjectState")) OrElse (editMode AndAlso DFClsAdvertiserProject.CanUpdateField("ddlAdProjectState"))

        Me.mForm.FindControls("mnuItem").FindItem("Advertiser").Enabled = editMode AndAlso DFClsAdvertiserAccount.CanSelect
        Me.mForm.FindControls("mnuItem").FindItem("Contact").Enabled = editMode AndAlso DFClsAdvertiserContact.CanSelect
        Me.mForm.FindControls("mnuItem").FindItem("Delete").Enabled = editMode AndAlso DFClsAdvertiserProject.CanDelete

        canSave = (Not editMode AndAlso DFClsAdvertiserProject.CanInsert) OrElse (editMode AndAlso DFClsAdvertiserProject.CanUpdate)
        Me.mForm.FindControls("mnuSave").FindItem("Ok").Enabled = canSave
        Me.mForm.FindControls("mnuSave").FindItem("Save").Enabled = canSave
    End Sub

    Public Function ValidateStarDate() As Boolean
        Dim startDate As String = Me.mForm.FindControls("ddlAdProjectStartYear").SelectedValue & "-" & Me.mForm.FindControls("ddlAdProjectStartMonth").SelectedValue & "-" & Me.mForm.FindControls("ddlAdProjectStartDay").SelectedValue
        Return startDate = "Year-Month-Day" OrElse IsDate(startDate)
    End Function

    Public Function ValidateEndDate() As Boolean
        Dim endDate As String = Me.mForm.FindControls("ddlAdProjectEndYear").SelectedValue & "-" & Me.mForm.FindControls("ddlAdProjectEndMonth").SelectedValue & "-" & Me.mForm.FindControls("ddlAdProjectEndDay").SelectedValue
        Return endDate = "Year-Month-Day" OrElse IsDate(endDate)
    End Function

    Public Function ValidateStartDateGTEndDate() As Boolean
        Dim startDateText As String = Me.mForm.FindControls("ddlAdProjectStartYear").SelectedValue & "-" & Me.mForm.FindControls("ddlAdProjectStartMonth").SelectedValue & "-" & Me.mForm.FindControls("ddlAdProjectStartDay").SelectedValue
        Dim endDateText As String = Me.mForm.FindControls("ddlAdProjectEndYear").SelectedValue & "-" & Me.mForm.FindControls("ddlAdProjectEndMonth").SelectedValue & "-" & Me.mForm.FindControls("ddlAdProjectEndDay").SelectedValue
        If IsDate(startDateText) AndAlso IsDate(endDateText) Then
            Dim startDate As Date = startDateText
            Dim endDate As Date = endDateText

            If startDate > endDate Then
                Return False
            Else
                Return True
            End If
        Else
            Return True
        End If
    End Function

    Public Function ValidateStartTime() As Boolean
        Dim startTime As String = Me.mForm.FindControls("ddlAdProjectStartHour").SelectedValue & ":" & Me.mForm.FindControls("ddlAdProjectStartMinute").SelectedValue & " " & Me.mForm.FindControls("ddlAdProjectStartAMPM").SelectedValue
        Return startTime = "Hour:Minute a.m./p.m." OrElse IsDate(startTime)
    End Function

    Public Function ValidateEndTime() As Boolean
        Dim endTime As String = Me.mForm.FindControls("ddlAdProjectEndHour").SelectedValue & ":" & Me.mForm.FindControls("ddlAdProjectEndMinute").SelectedValue & " " & Me.mForm.FindControls("ddlAdProjectEndAMPM").SelectedValue
        Return endTime = "Hour:Minute a.m./p.m." OrElse IsDate(endTime)
    End Function

    Public Function ValidateStartTimeGTEndTime() As Boolean
        Dim startTimeText As String = Me.mForm.FindControls("ddlAdProjectStartHour").SelectedValue & ":" & Me.mForm.FindControls("ddlAdProjectStartMinute").SelectedValue & " " & Me.mForm.FindControls("ddlAdProjectStartAMPM").SelectedValue
        Dim endTimeText As String = Me.mForm.FindControls("ddlAdProjectEndHour").SelectedValue & ":" & Me.mForm.FindControls("ddlAdProjectEndMinute").SelectedValue & " " & Me.mForm.FindControls("ddlAdProjectEndAMPM").SelectedValue

        If IsDate(startTimeText) AndAlso IsDate(endTimeText) Then
            Dim startTime As Date = startTimeText
            Dim endTime As Date = endTimeText

            If startTime > endTime Then
                Return False
            Else
                Return True
            End If
        Else
            Return True
        End If
    End Function

    Public Function ValidateVerifiedDate() As Boolean
        Dim startDate As String = Me.mForm.FindControls("ddlAdProjectVerifiedYear").SelectedValue & "-" & Me.mForm.FindControls("ddlAdProjectVerifiedMonth").SelectedValue & "-" & Me.mForm.FindControls("ddlAdProjectVerifiedDay").SelectedValue
        Return startDate = "Year-Month-Day" OrElse IsDate(startDate)
    End Function

    Public Function ValidateOnlineDate() As Boolean
        Dim endDate As String = Me.mForm.FindControls("ddlAdProjectOnlineYear").SelectedValue & "-" & Me.mForm.FindControls("ddlAdProjectOnlineMonth").SelectedValue & "-" & Me.mForm.FindControls("ddlAdProjectOnlineDay").SelectedValue
        Return endDate = "Year-Month-Day" OrElse IsDate(endDate)
    End Function

    Private Sub LoadCombo(ByVal combo As DropDownList, ByVal data As DataTable, ByVal selectedValue As String)
        combo.DataSource = data
        combo.DataTextField = data.Columns(0).Caption
        combo.DataValueField = data.Columns(1).Caption
        combo.DataBind()
        If combo.Items.FindByValue(selectedValue) IsNot Nothing Then
            combo.SelectedValue = selectedValue
        End If
    End Sub

    Public Sub LoadAdvertiser(ByVal advertiser As DropDownList, ByVal value As Long)
        Me.LoadCombo(advertiser, Me.GetComboDataSources(Me.GetAdAccountInfoList, "[Select a Advertiser]"), Value)
    End Sub

    Public Sub LoadContact(ByVal advertiser As DropDownList, ByVal contact As DropDownList, ByVal value As String)
        If advertiser.SelectedItem.Value <> 0 Then
            Me.LoadCombo(contact, Me.GetComboDataSources(Me.GetAdContactInfoList(advertiser.SelectedItem.Value), "[Select a Contact]"), value)
        Else
            Me.LoadCombo(contact, Me.GetComboDataSources(New AdContactData() {}, "[Select a Contact]"), 0)
        End If
    End Sub

    Private Sub LoadDate(ByVal year As DropDownList, ByVal month As DropDownList, ByVal day As DropDownList, ByVal dateValue As Date)
        Me.LoadCombo(year, Me.GetYearData, "1900")
        If dateValue.ToString("yyyy-MM-dd") = "1900-01-01" Then
            year.SelectedIndex = 0
            month.SelectedIndex = 0
            day.SelectedIndex = 0
        Else
            year.SelectedValue = dateValue.Year
            month.SelectedIndex = dateValue.Month
            day.SelectedIndex = dateValue.Day
        End If
    End Sub

    Private Sub LoadTime(ByVal hour As DropDownList, ByVal minute As DropDownList, ByVal ampm As DropDownList, ByVal timeValue As Date)
        If timeValue.ToString("HH:mm") = "00:01" OrElse timeValue.ToString("HH:mm") = "23:59" Then
            hour.SelectedIndex = 0
            minute.SelectedIndex = 0
            ampm.SelectedIndex = 0
        Else
            If timeValue.Hour = 12 Or timeValue.Hour = 0 Then
                hour.SelectedIndex = 12
            Else
                hour.SelectedIndex = timeValue.Hour Mod 12
            End If
            minute.SelectedIndex = (timeValue.Minute / 5) + 1
            ampm.SelectedIndex = Fix(timeValue.Hour / 12) + 1
        End If
    End Sub

    Private Function GetComboDataSources(ByVal InfoList() As AdAccountData, ByVal FirstItemText As String) As DataTable
        Dim table As New DataTable

        table.Columns.Add("CompanyName", GetType(String))
        table.Columns.Add("ID", GetType(String))

        table.DefaultView.Sort = "CompanyName"

        table.Rows.Add(New String() {FirstItemText, "0"})
        For Each Info As AdAccountData In InfoList
            table.Rows.Add(New String() {Info.CompanyName, Info.ID.ToString})
        Next

        Return table
    End Function

    Private Function GetComboDataSources(ByVal InfoList() As AdContactData, ByVal FirstItemText As String) As DataTable
        Dim table As New DataTable

        table.Columns.Add("FullName", GetType(String))
        table.Columns.Add("ID", GetType(String))

        table.DefaultView.Sort = "FullName"

        table.Rows.Add(New String() {FirstItemText, "0"})
        For Each Info As AdContactData In InfoList
            table.Rows.Add(New String() {Info.FullName, Info.ID.ToString})
        Next

        Return table
    End Function

    Private Function GetYearData() As DataTable
        Dim table As New DataTable

        table.Columns.Add("Text", GetType(String))
        table.Columns.Add("Value", GetType(String))
        table.Rows.Add(New Object() {"Year", "Year"})

        For i As Integer = 2000 To Date.Today.Year + 10
            table.Rows.Add(New Object() {i, i})
        Next
        Return table
    End Function

    Private Function GetAgeData() As DataTable
        Dim table As New DataTable

        table.Columns.Add("Text", GetType(String))
        table.Columns.Add("Value", GetType(String))
        table.Rows.Add(New Object() {String.Empty, String.Empty})

        For i As Integer = 1 To 99
            table.Rows.Add(New Object() {i.ToString("00"), i})
        Next
        Return table
    End Function

    Private Function GetSexData() As DataTable
        Dim table As New DataTable
        table.ReadXml(Me.mForm.MapPath("~") & "/App_Data/Sex.xml")
        table.Rows.Add(New Object() {String.Empty, String.Empty})
        table.DefaultView.Sort = "Text"
        Return table
    End Function

    Private Function GetOccupationData() As DataTable
        Dim table As New DataTable
        table.ReadXml(Me.mForm.MapPath("~") & "/App_Data/Occupations.xml")
        table.Rows.Add(New Object() {String.Empty, String.Empty})
        table.DefaultView.Sort = "Description"
        Return table
    End Function

    Private Function GetCountryData() As DataTable
        Dim reginfo As RegionInfo
        Dim cultInfoList() As CultureInfo

        Dim table As New DataTable
        Dim row As DataRow

        table.Columns.Add("Text", GetType(String))
        table.Columns.Add("Value", GetType(String))

        table.Rows.Add(New Object() {String.Empty, String.Empty})

        table.DefaultView.Sort = "Text"
        table.PrimaryKey = New DataColumn() {table.Columns("Text")}

        cultInfoList = CultureInfo.GetCultures(CultureTypes.AllCultures)
        For Each cultInfo As CultureInfo In cultInfoList
            Try
                reginfo = New RegionInfo(cultInfo.LCID)
                row = table.NewRow
                row("Text") = reginfo.EnglishName
                row("Value") = reginfo.ThreeLetterISORegionName
                If Not table.Rows.Contains(row) Then
                    table.Rows.Add(row)
                End If
            Catch ex As Exception
            End Try
        Next
        Return table
    End Function

    Private Function GetStateData(ByVal filter As String) As DataTable
        Dim table As New DataTable
        table.ReadXml(Me.mForm.MapPath("~") & "/App_Data/States.xml")
        table.Rows.Add(New Object() {String.Empty, String.Empty, filter})
        table.DefaultView.Sort = "Name"
        table.DefaultView.RowFilter = "CodeCountry = '" & filter & "'"
        Return table
    End Function

    Public Sub OnSelectedAdvertiser(ByVal idAccountID As String)
        Me.LoadContact(Me.mForm.FindControls("ddlAdProjectAdvertiser"), Me.mForm.FindControls("ddlAdProjectContact"), 0)
        Me.mForm.FindControls("ddlAdProjectContact").Focus()
    End Sub

    Public Sub OnSelectedCountry(ByVal countryCode As String)
        Me.LoadCombo(Me.mForm.FindControls("ddlAdProjectState"), Me.GetStateData(countryCode), String.Empty)
        Me.mForm.FindControls("ddlAdProjectState").Focus()
    End Sub

    Public Sub OnAdvertiser()
        Me.mForm.Session("ID1") = Me.mData.Advertiser.ID
        Me.mForm.Response.Redirect("~/Forms/frmAdvertiserAccount.aspx")
    End Sub

    Public Sub OnContact()
        Me.mForm.Session("ID1") = Me.mData.Contact.ID
        Me.mForm.Response.Redirect("~/Forms/frmAdvertiserContact.aspx")
    End Sub

    Public Sub OnProjects()
        Me.mForm.Session("ID1") = Me.mForm.FindControls("ddlAdProjectAdvertiser").SelectedItem.Value
        Me.mForm.Session("ID2") = Me.mForm.FindControls("ddlAdProjectContact").SelectedItem.Value
        Me.mForm.Response.Redirect("~/Forms/frmAdvertiserProjects.aspx")
    End Sub

    Public Sub OnDeleting()
        Me.mForm.FindControls("MsgBox").ShowConfirmation("Are you sure you want to delete this Project?\n\nNote: there is no undo.", "", True, False)
    End Sub

    Public Sub OnMsgBoxOk()
        If Me.DeleteData Then
            Me.OnProjects()
        End If
    End Sub

    Public Sub OnSave()
        If Me.SaveData Then
            Me.ApplyAddFormAuthorizationRules(True)
            Me.PopulateData()
        End If
    End Sub

    Public Sub OnOk()
        If Me.SaveData Then
            Me.OnProjects()
        End If
    End Sub

    Public Sub OnCancel()
        Me.OnProjects()
    End Sub

#End Region

#Region " Data Methods "

    Private Sub ClearAddForm()
        Me.mForm.FindControls("txtAdProjectID").Text = String.Empty

        Me.LoadAdvertiser(Me.mForm.FindControls("ddlAdProjectAdvertiser"), 0)
        Me.LoadContact(Me.mForm.FindControls("ddlAdProjectAdvertiser"), Me.mForm.FindControls("ddlAdProjectContact"), 0)

        Me.mForm.FindControls("txtAdProjectADUrl").Text = String.Empty
        Me.mForm.FindControls("txtAdProjectDescription").Text = String.Empty
        Me.mForm.FindControls("txtAdProjectHeight").Text = String.Empty
        Me.mForm.FindControls("txtAdProjectWidth").Text = String.Empty

        Me.LoadDate(Me.mForm.FindControls("ddlAdProjectStartYear"), Me.mForm.FindControls("ddlAdProjectStartMonth"), Me.mForm.FindControls("ddlAdProjectStartDay"), New Date(1900, 1, 1, 0, 1, 0))
        Me.LoadDate(Me.mForm.FindControls("ddlAdProjectEndYear"), Me.mForm.FindControls("ddlAdProjectEndMonth"), Me.mForm.FindControls("ddlAdProjectEndDay"), New Date(1900, 1, 1, 0, 1, 0))

        Me.LoadTime(Me.mForm.FindControls("ddlAdProjectStartHour"), Me.mForm.FindControls("ddlAdProjectStartMinute"), Me.mForm.FindControls("ddlAdProjectStartAMPM"), New Date(1900, 1, 1, 0, 1, 0))
        Me.LoadTime(Me.mForm.FindControls("ddlAdProjectEndHour"), Me.mForm.FindControls("ddlAdProjectEndMinute"), Me.mForm.FindControls("ddlAdProjectEndAMPM"), New Date(1900, 1, 1, 0, 1, 0))

        Me.mForm.FindControls("txtAdProjectMinDisplay").Text = String.Empty
        Me.mForm.FindControls("txtAdProjectMaxDisplay").Text = String.Empty
        Me.mForm.FindControls("txtAdProjectMinPerDay").Text = String.Empty
        Me.mForm.FindControls("txtAdProjectMaxPerDay").Text = String.Empty
        Me.mForm.FindControls("txtAdProjectCountDisplayed").Text = String.Empty

        Me.LoadDate(Me.mForm.FindControls("ddlAdProjectVerifiedYear"), Me.mForm.FindControls("ddlAdProjectVerifiedMonth"), Me.mForm.FindControls("ddlAdProjectVerifiedDay"), New Date(1900, 1, 1, 0, 1, 0))
        Me.LoadDate(Me.mForm.FindControls("ddlAdProjectOnlineYear"), Me.mForm.FindControls("ddlAdProjectOnlineMonth"), Me.mForm.FindControls("ddlAdProjectOnlineDay"), New Date(1900, 1, 1, 0, 1, 0))

        Me.mForm.FindControls("txtAdProjectPromoCode").Text = String.Empty
        Me.mForm.FindControls("txtAdProjectComissionCode").Text = String.Empty

        Me.LoadCombo(Me.mForm.FindControls("ddlAdProjectMinAge"), Me.GetAgeData, String.Empty)
        Me.LoadCombo(Me.mForm.FindControls("ddlAdProjectMaxAge"), Me.GetAgeData, String.Empty)
        Me.LoadCombo(Me.mForm.FindControls("ddlAdProjectSex"), Me.GetSexData, String.Empty)
        Me.LoadCombo(Me.mForm.FindControls("ddlAdProjectOccupation"), Me.GetOccupationData, String.Empty)
        Me.LoadCombo(Me.mForm.FindControls("ddlAdProjectCountry"), Me.GetCountryData, String.Empty)
        Me.LoadCombo(Me.mForm.FindControls("ddlAdProjectState"), Me.GetStateData(String.Empty), String.Empty)

        Me.mForm.FindControls("txtAdProjectADUrl").Focus()
    End Sub

    Private Sub PopulateData()
        Me.mForm.FindControls("txtAdProjectID").Text = Me.mData.ID

        Me.LoadAdvertiser(Me.mForm.FindControls("ddlAdProjectAdvertiser"), Me.mData.Advertiser.ID)
        Me.LoadContact(Me.mForm.FindControls("ddlAdProjectAdvertiser"), Me.mForm.FindControls("ddlAdProjectContact"), Me.mData.Contact.ID)

        Me.mForm.FindControls("txtAdProjectADUrl").Text = Me.mData.ADUrl
        Me.mForm.FindControls("txtAdProjectDescription").Text = Me.mData.ProjectDescription
        Me.mForm.FindControls("txtAdProjectHeight").Text = Me.mData.ADHeight.ToString("###0")
        Me.mForm.FindControls("txtAdProjectWidth").Text = Me.mData.ADWidth.ToString("###0")

        Me.LoadDate(Me.mForm.FindControls("ddlAdProjectStartYear"), Me.mForm.FindControls("ddlAdProjectStartMonth"), Me.mForm.FindControls("ddlAdProjectStartDay"), Me.mData.RunStartDate)
        Me.LoadDate(Me.mForm.FindControls("ddlAdProjectEndYear"), Me.mForm.FindControls("ddlAdProjectEndMonth"), Me.mForm.FindControls("ddlAdProjectEndDay"), Me.mData.RunEndDate)

        Me.LoadTime(Me.mForm.FindControls("ddlAdProjectStartHour"), Me.mForm.FindControls("ddlAdProjectStartMinute"), Me.mForm.FindControls("ddlAdProjectStartAMPM"), Me.mData.StartTimeBasedOnSubscribersTime)
        Me.LoadTime(Me.mForm.FindControls("ddlAdProjectEndHour"), Me.mForm.FindControls("ddlAdProjectEndMinute"), Me.mForm.FindControls("ddlAdProjectEndAMPM"), Me.mData.EndTimeBasedOnSubscribersTime)

        Me.mForm.FindControls("txtAdProjectMinDisplay").Text = Me.mData.MinDisplays.ToString("#,###,###,###")
        Me.mForm.FindControls("txtAdProjectMaxDisplay").Text = Me.mData.MaxDisplays.ToString("#,###,###,###")
        Me.mForm.FindControls("txtAdProjectMinPerDay").Text = Me.mData.MinPerDay.ToString("#,###,###,###")
        Me.mForm.FindControls("txtAdProjectMaxPerDay").Text = Me.mData.MaxPerDay.ToString("#,###,###,###")
        Me.mForm.FindControls("txtAdProjectCountDisplayed").Text = Me.mData.AdCountDisplayed.ToString("#,###,###,##0")

        Me.LoadDate(Me.mForm.FindControls("ddlAdProjectVerifiedYear"), Me.mForm.FindControls("ddlAdProjectVerifiedMonth"), Me.mForm.FindControls("ddlAdProjectVerifiedDay"), Me.mData.AdVerifiedDate)
        Me.LoadDate(Me.mForm.FindControls("ddlAdProjectOnlineYear"), Me.mForm.FindControls("ddlAdProjectOnlineMonth"), Me.mForm.FindControls("ddlAdProjectOnlineDay"), Me.mData.AdOnlineDate)

        Me.mForm.FindControls("txtAdProjectPromoCode").Text = Me.mData.PromoCode.ToString("#,###,###,###")
        Me.mForm.FindControls("txtAdProjectComissionCode").Text = Me.mData.ComissionCode.ToString("#,###,###,###")

        Me.LoadCombo(Me.mForm.FindControls("ddlAdProjectMinAge"), Me.GetAgeData, Me.mData.MinAge)
        Me.LoadCombo(Me.mForm.FindControls("ddlAdProjectMaxAge"), Me.GetAgeData, Me.mData.MaxAge)
        Me.LoadCombo(Me.mForm.FindControls("ddlAdProjectSex"), Me.GetSexData, Me.mData.Sex)
        Me.LoadCombo(Me.mForm.FindControls("ddlAdProjectOccupation"), Me.GetOccupationData, Me.mData.Occupation)
        Me.LoadCombo(Me.mForm.FindControls("ddlAdProjectCountry"), Me.GetCountryData, Me.mData.Country)
        Me.LoadCombo(Me.mForm.FindControls("ddlAdProjectState"), Me.GetStateData(Me.mData.Country), Me.mData.State)

        Me.mForm.FindControls("txtAdProjectADUrl").Focus()
    End Sub

    Private Function GetDataID(ByVal textBox As TextBox) As String
        If textBox.Text = String.Empty Then
            Return 0
        End If
        Return textBox.Text
    End Function

    Private Function CollectData() As AdProjectNewData
        Dim formData As New AdProjectNewData

        formData.ID.SetValues("txtAdProjectID", True, Me.mData.ID, CLng(Me.CollectID(Me.mForm.FindControls("txtAdProjectID").Text)))

        formData.Advertiser.ID.SetValues("ddlAdProjectAdvertiser", False, Me.mData.Advertiser.ID, CLng(Me.mForm.FindControls("ddlAdProjectAdvertiser").SelectedItem.Value))
        formData.Advertiser.CompanyName.SetValues("ddlAdProjectAdvertiser", False, Me.mData.Advertiser.CompanyName, Me.mForm.FindControls("ddlAdProjectAdvertiser").SelectedItem.Text)

        formData.Contact.ID.SetValues("ddlAdProjectContact", False, Me.mData.Contact.ID, CLng(Me.mForm.FindControls("ddlAdProjectContact").SelectedItem.Value))
        formData.Contact.FullName.SetValues("ddlAdProjectContact", False, Me.mData.Contact.FullName, Me.mForm.FindControls("ddlAdProjectContact").SelectedItem.Text)

        formData.ADUrl.SetValues("txtAdProjectADUrl", False, Me.mData.ADUrl, Me.mForm.FindControls("txtAdProjectADUrl").Text)
        formData.ProjectDescription.SetValues("txtAdProjectDescription", False, Me.mData.ProjectDescription, Me.mForm.FindControls("txtAdProjectDescription").Text)
        formData.ADHeight.SetValues("txtAdProjectHeight", False, Me.mData.ADHeight, Me.CollectNumeric(100, Me.mForm.FindControls("txtAdProjectHeight").Text))
        formData.ADWidth.SetValues("txtAdProjectWidth", False, Me.mData.ADWidth, Me.CollectNumeric(100, Me.mForm.FindControls("txtAdProjectWidth").Text))

        formData.RunStartDate.SetValues("ddlAdProjectRunStartDate", False, Me.mData.RunStartDate, Me.CollectDate(Me.mForm.FindControls("ddlAdProjectStartYear").SelectedValue, Me.mForm.FindControls("ddlAdProjectStartMonth").SelectedValue, Me.mForm.FindControls("ddlAdProjectStartDay").SelectedValue))
        formData.RunEndDate.SetValues("ddlAdProjectRunEndDate", False, Me.mData.RunEndDate, Me.CollectDate(Me.mForm.FindControls("ddlAdProjectEndYear").SelectedValue, Me.mForm.FindControls("ddlAdProjectEndMonth").SelectedValue, Me.mForm.FindControls("ddlAdProjectEndDay").SelectedValue))

        formData.StartTimeBasedOnSubscribersTime.SetValues("ddlAdProjectStartTime", False, Me.mData.StartTimeBasedOnSubscribersTime, Me.CollectTime(True, Me.mForm.FindControls("ddlAdProjectStartHour").SelectedValue, Me.mForm.FindControls("ddlAdProjectStartMinute").SelectedValue, Me.mForm.FindControls("ddlAdProjectStartAMPM").SelectedValue))
        formData.EndTimeBasedOnSubscribersTime.SetValues("ddlAdProjectEndTime", False, Me.mData.EndTimeBasedOnSubscribersTime, Me.CollectTime(False, Me.mForm.FindControls("ddlAdProjectEndHour").SelectedValue, Me.mForm.FindControls("ddlAdProjectEndMinute").SelectedValue, Me.mForm.FindControls("ddlAdProjectEndAMPM").SelectedValue))

        formData.MinDisplays.SetValues("txtAdProjectMinDisplay", False, Me.mData.MinDisplays, Me.CollectNumeric(0, Me.mForm.FindControls("txtAdProjectMinDisplay").Text))
        formData.MaxDisplays.SetValues("txtAdProjectMaxDisplay", False, Me.mData.MaxDisplays, Me.CollectNumeric(0, Me.mForm.FindControls("txtAdProjectMaxDisplay").Text))
        formData.MinPerDay.SetValues("txtAdProjectMinPerDay", False, Me.mData.MinPerDay, Me.CollectNumeric(0, Me.mForm.FindControls("txtAdProjectMinPerDay").Text))
        formData.MaxPerDay.SetValues("txtAdProjectMaxPerDay", False, Me.mData.MaxPerDay, Me.CollectNumeric(0, Me.mForm.FindControls("txtAdProjectMaxPerDay").Text))
        formData.AdCountDisplayed.SetValues("txtAdProjectCountDisplayed", False, Me.mData.AdCountDisplayed, Me.CollectNumeric(0, Me.mForm.FindControls("txtAdProjectCountDisplayed").Text))

        formData.AdVerifiedDate.SetValues("ddlAdProjectVerifiedDate", False, Me.mData.AdVerifiedDate, Me.CollectDate(Me.mForm.FindControls("ddlAdProjectVerifiedYear").SelectedValue, Me.mForm.FindControls("ddlAdProjectVerifiedMonth").SelectedValue, Me.mForm.FindControls("ddlAdProjectVerifiedDay").SelectedValue))
        formData.AdOnlineDate.SetValues("ddlAdProjectOnlineDate", False, Me.mData.AdOnlineDate, Me.CollectDate(Me.mForm.FindControls("ddlAdProjectOnlineYear").SelectedValue, Me.mForm.FindControls("ddlAdProjectOnlineMonth").SelectedValue, Me.mForm.FindControls("ddlAdProjectOnlineDay").SelectedValue))

        formData.PromoCode.SetValues("txtAdProjectPromoCode", False, Me.mData.PromoCode, Me.CollectNumeric(0, Me.mForm.FindControls("txtAdProjectPromoCode").Text))
        formData.ComissionCode.SetValues("txtAdProjectComissionCode", False, Me.mData.ComissionCode, Me.CollectNumeric(0, Me.mForm.FindControls("txtAdProjectComissionCode").Text))

        formData.MinAge.SetValues("ddlAdProjectMinAge", False, Me.mData.MinAge, Me.mForm.FindControls("ddlAdProjectMinAge").SelectedItem.Value)
        formData.MaxAge.SetValues("ddlAdProjectMaxAge", False, Me.mData.MaxAge, Me.mForm.FindControls("ddlAdProjectMaxAge").SelectedItem.Value)
        formData.Sex.SetValues("ddlAdProjectSex", False, Me.mData.Sex, Me.mForm.FindControls("ddlAdProjectSex").SelectedItem.Value)
        formData.Occupation.SetValues("ddlAdProjectOccupation", False, Me.mData.Occupation, Me.mForm.FindControls("ddlAdProjectOccupation").SelectedItem.Value)
        formData.Country.SetValues("ddlAdProjectCountry", False, Me.mData.Country, Me.mForm.FindControls("ddlAdProjectCountry").SelectedItem.Value)
        formData.State.SetValues("ddlAdProjectState", False, Me.mData.State, Me.mForm.FindControls("ddlAdProjectState").SelectedItem.Value)

        Return formData
    End Function

    Private Function CollectID(ByVal text As String) As String
        If text = String.Empty Then
            Return "0"
        End If
        Return text
    End Function

    Private Function CollectNumeric(ByVal minValue As Integer, ByVal text As String) As Integer
        If text = String.Empty OrElse Not IsNumeric(text) Then
            Return minValue
        End If
        Return text
    End Function

    Private Function CollectDate(ByVal year As String, ByVal month As String, ByVal day As String) As Date
        If year = "Year" AndAlso month = "Month" AndAlso day = "Day" Then
            Return "1900-01-01"
        Else
            Return year & "-" & month & "-" & day
        End If
    End Function

    Private Function CollectTime(ByVal startTime As Boolean, ByVal hour As String, ByVal minute As String, ByVal ampm As String) As Date
        If hour = "Hour" AndAlso minute = "Minute" AndAlso ampm = "a.m./p.m." Then
            If startTime Then
                Return New Date(1900, 1, 1, 0, 1, 0)
            Else
                Return New Date(1900, 1, 1, 23, 59, 0)
            End If
        Else
            Return "1900-01-01 " & hour & ":" & minute & " " & ampm
        End If
    End Function

    Private Function CollectDataID() As AdProjectNewData
        Dim formData As New AdProjectNewData
        formData.ID.SetValues("txtAdProjectID", True, Me.mData.ID, CLng(Me.GetDataID(Me.mForm.FindControls("txtAdProjectID"))))
        Return formData
    End Function

    Private Function CollectDataID(ByVal adProjectID As String) As AdProjectNewData
        Dim formData As New AdProjectNewData
        formData.ID.SetValues("txtAdProjectID", True, 0, CLng(adProjectID))
        Return formData
    End Function

    Private Function GetAdAccountInfoList() As AdAccountData()
        Try
            Return ClsSessionAdmin.GetAdAccountInfoList()
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return New AdAccountData() {}
        End Try
    End Function

    Private Function GetAdContactInfoList(ByVal idAccountID As Long) As AdContactData()
        Try
            Return ClsSessionAdmin.GetAdContactInfoList(idAccountID)
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return New AdContactData() {}
        End Try
    End Function

    Private Function GetData(ByVal adProjectID As String) As AdProjectData
        Try
            Return ClsSessionAdmin.GetAdProject(Me.CollectDataID(adProjectID))
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return Nothing
        End Try
    End Function

    Private Function SaveData() As Boolean
        Try
            If Me.mEditMode Then
                Me.mData = ClsSessionAdmin.EditAdProject(Me.CollectData)
            Else
                Me.mData = ClsSessionAdmin.AddAdProject(Me.CollectData)
            End If
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error saving: " & SysEx.Message)
            Return False
        End Try
    End Function

    Private Function DeleteData() As Boolean
        Try
            ClsSessionAdmin.DeleteAdProject(Me.CollectDataID)
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error deleting: " & SysEx.Message)
            Return False
        End Try
    End Function

#End Region

End Class
