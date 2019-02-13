Imports SCT.Library
Imports System.Globalization

Public Class DFClsAdContactInformation

#Region " Private Fields "

    Private mForm As Object
    Private mData As AdContactData

#End Region

#Region " Properties And Methods "

    Public WriteOnly Property Form() As Object
        Set(ByVal value As Object)
            Me.mForm = value
        End Set
    End Property

    Public Sub New(ByRef form As Object)
        Me.mForm = form
        Me.mForm.Session("ValuePath") = "frmAdContacts"

        ApplyPageAuthorizationRules()

        If IsNumeric(Me.mForm.Session("contactID")) AndAlso Me.mForm.Session("contactID") <> 0 Then
            Me.mData = Me.GetData(Me.mForm.Session("contactID"))
            If Me.mData IsNot Nothing Then
                Me.PopulateData()
                Me.EnableMenuItem(True)
            End If
        End If
    End Sub

    Public Sub ApplyPageAuthorizationRules()
        If Not WebSite.ClsSessionAdmin.IsValidForm("frmAdContactInformation", "txtAdContactID", "txtAdContactFirstName", "txtAdContactLastName", "txtAdContactPrimaryAddress", "txtAdContactSecondaryAddress", "imgAdContactMainCompanyAddress", "txtAdContactCity", "txtAdContactCountry", "txtAdContactState", "txtAdContactZipCode", "txtAdContactProvidence", "txtAdContactDepartment", "txtAdContactNotes") Then Me.mForm.Response.Redirect("~/Forms/frmMain.aspx")
        Me.EnableMenuItem(False)
    End Sub

    Public Sub EnableMenuItem(ByVal value As Boolean)
        Me.mForm.FindControls("mnuContact").FindItem("Projects").Enabled = value
        Me.mForm.FindControls("mnuContact").FindItem("Notes").Enabled = value
        Me.mForm.FindControls("mnuContact").FindItem("ToDo").Enabled = value

        Me.mForm.FindControls("mnuEditContact").FindItem("Edit").Enabled = value
        Me.mForm.FindControls("mnuEditContact").FindItem("Delete").Enabled = value
        Me.mForm.FindControls("mnuEditContact").FindItem("Return").Enabled = True
    End Sub

    Private Function GetCountryName(ByVal value As String) As String
        Dim reginfo As RegionInfo
        Dim cultInfoList() As CultureInfo

        Dim table As New DataTable
        Dim row As DataRow

        table.Columns.Add("Text", GetType(String))
        table.Columns.Add("Value", GetType(String))
        table.Rows.Add(New Object() {String.Empty, String.Empty})

        table.DefaultView.RowFilter = "Value = '" & value & "'"
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
        Return table.DefaultView.Item(0).Item("Text")
    End Function

    Private Function GetStateName(ByVal codeCountry As String, ByVal codeState As String) As String
        Dim table As New DataTable
        table.ReadXml(Me.mForm.MapPath("~") & "/App_Data/States.xml")
        table.Rows.Add(New Object() {String.Empty, String.Empty, codeCountry})
        table.DefaultView.RowFilter = "CodeCountry = '" & codeCountry & "' and CodeState = '" & codeState & "'"
        Return table.DefaultView.Item(0).Item("Name")
    End Function

    Public Sub OnProjects()
        Me.mForm.Session("contactID") = Me.mData.ID
        Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdProjects.aspx")
    End Sub

    Public Sub OnNotes()
        Me.mForm.Session("contactID") = Me.mData.ID
        Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdNotes.aspx")
    End Sub

    Public Sub OnToDo()
        Me.mForm.Session("contactID") = Me.mData.ID
        Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdToDos.aspx")
    End Sub

    Public Sub OnEditing()
        Me.mForm.Session("contact") = Me.mData
        Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdChangeContactInformation.aspx")
    End Sub

    Public Sub OnDeleting()
        Me.mForm.FindControls("MsgBox").ShowConfirmation("Are you sure you want to delete this Contact?\n\nNote: there is no undo.", "", True, False)
    End Sub

    Public Sub OnOkDelete()
        If Me.DeleteData Then
            Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdContacts.aspx")
        End If
    End Sub

    Public Sub OnReturn()
        Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdContacts.aspx")
    End Sub

#End Region

#Region " Data Methods "

    Private Sub PopulateData()
        Me.mForm.FindControls("txtAdContactID").Text = Me.mData.ID
        Me.mForm.FindControls("txtAdContactFirstName").Text = Me.mData.FirstName
        Me.mForm.FindControls("txtAdContactLastName").Text = Me.mData.LastName
        Me.mForm.FindControls("txtAdContactPrimaryAddress").Text = Me.mData.PrimaryAddress
        Me.mForm.FindControls("txtAdContactSecondaryAddress").Text = Me.mData.SecondaryAddress
        Me.mForm.FindControls("imgAdContactMainCompanyAddress").ImageUrl = "~/Images/Checked_" & Me.mData.MainCompanyAddress.ToString & ".png"
        Me.mForm.FindControls("txtAdContactCity").Text = Me.mData.City
        Me.mForm.FindControls("txtAdContactCountry").Text = Me.GetCountryName(Me.mData.Country)
        Me.mForm.FindControls("txtAdContactState").Text = Me.GetStateName(Me.mData.Country, Me.mData.State)
        Me.mForm.FindControls("txtAdContactZipCode").Text = Me.mData.ZipCode
        Me.mForm.FindControls("txtAdContactProvidence").Text = Me.mData.Providence
        Me.mForm.FindControls("txtAdContactDepartment").Text = Me.mData.Department
        Me.mForm.FindControls("txtAdContactNotes").Text = Me.mData.ResposibleForNotes
    End Sub

    Private Function CollectDataID() As AdContactNewData
        Dim formData As New AdContactNewData
        formData.ID.SetValues("txtAdContactID", True, Me.mData.ID, Me.mData.ID)
        Return formData
    End Function

    Private Function CollectDataID(ByVal contactID As String) As AdContactNewData
        Dim formData As New AdContactNewData
        formData.ID.SetValues("txtAdContactID", True, 0, CLng(contactID))
        Return formData
    End Function

    Private Function GetData(ByVal contactID As String) As AdContactData
        Try
            Return WebSite.ClsSessionAdmin.GetAdContact(Me.CollectDataID(contactID))
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return Nothing
        End Try
    End Function

    Private Function DeleteData() As Boolean
        Try
            WebSite.ClsSessionAdmin.DeleteAdContact(Me.CollectDataID)
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error deleting: " & SysEx.Message)
            Return False
        End Try
    End Function

#End Region

End Class
