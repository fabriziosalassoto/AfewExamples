Imports SCT.Library

Public Class DFClsAdNoteInformation

#Region " Private Fields "

    Private mForm As Object
    Private mData As AdNoteData

#End Region

#Region " Properties And Methods "

    Public WriteOnly Property Form() As Object
        Set(ByVal value As Object)
            Me.mForm = value
        End Set
    End Property

    Public Sub New(ByRef form As Object)
        Me.mForm = form
        Me.mForm.Session("ValuePath") = "frmAdNotes"

        ApplyPageAuthorizationRules()

        If IsNumeric(Me.mForm.Session("noteID")) AndAlso Me.mForm.Session("noteID") <> 0 Then
            Me.mData = Me.GetData(Me.mForm.Session("noteID"))
            If Me.mData IsNot Nothing Then
                Me.PopulateData()
                Me.EnableMenuItem(True)
            End If
        End If
    End Sub

    Public Sub ApplyPageAuthorizationRules()
        If Not WebSite.ClsSessionAdmin.IsValidForm("frmAdNoteInformation", "txtAdNoteID", "txtAdNoteContact", "txtAdNoteDateEntered", "txtAdNotedescription") Then Me.mForm.Response.Redirect("~/Forms/frmMain.aspx")
        Me.EnableMenuItem(False)
    End Sub

    Public Sub EnableMenuItem(ByVal value As Boolean)
        Me.mForm.FindControls("mnuContact").FindItem("Contact").Enabled = value

        Me.mForm.FindControls("mnuEditNote").FindItem("Edit").Enabled = value
        Me.mForm.FindControls("mnuEditNote").FindItem("Delete").Enabled = value
        Me.mForm.FindControls("mnuEditNote").FindItem("Return").Enabled = True
    End Sub

    Private Function DateFormat(ByVal dateValue As Date) As String
        If Not Format(dateValue, "yyyy-MM-dd") = "1900-01-01" Then
            Return Format(dateValue, "MMMM dd, yyyy")
        Else
            Return String.Empty
        End If
    End Function

    Public Sub OnContact()
        Me.mForm.Session("contactID") = Me.mData.Contact.ID
        Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdContactInformation.aspx")
    End Sub

    Public Sub OnEditing()
        Me.mForm.Session("noteID") = Me.mData.ID
        Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdChangeNoteInformation.aspx")
    End Sub

    Public Sub OnDeleting()
        Me.mForm.FindControls("MsgBox").ShowConfirmation("Are you sure you want to delete this Note?\n\nNote: there is no undo.", "", True, False)
    End Sub

    Public Sub OnOkDelete()
        If Me.DeleteData Then
            Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdNotes.aspx")
        End If
    End Sub

    Public Sub OnReturn()
        Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdNotes.aspx")
    End Sub

#End Region

#Region " Data Methods "

    Private Sub PopulateData()
        Me.mForm.FindControls("txtAdNoteID").Text = Me.mData.ID
        Me.mForm.FindControls("txtAdNoteContact").Text = Me.mData.Contact.FullName
        Me.mForm.FindControls("txtAdNoteDateEntered").Text = Me.DateFormat(Me.mData.DateEntered)
        Me.mForm.FindControls("txtAdNoteDescription").Text = Me.mData.Description
    End Sub

    Private Function CollectDataID() As AdNoteNewData
        Dim formData As New AdNoteNewData
        formData.ID.SetValues("txtAdNoteID", True, Me.mData.ID, Me.mData.ID)
        Return formData
    End Function

    Private Function CollectDataID(ByVal noteID As String) As AdNoteNewData
        Dim formData As New AdNoteNewData
        formData.ID.SetValues("txtAdNoteID", True, 0, CLng(noteID))
        Return formData
    End Function

    Private Function GetData(ByVal noteID As String) As AdNoteData
        Try
            Return WebSite.ClsSessionAdmin.GetAdNote(Me.CollectDataID(noteID))
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return Nothing
        End Try
    End Function

    Private Function DeleteData() As Boolean
        Try
            WebSite.ClsSessionAdmin.DeleteAdNote(Me.CollectDataID)
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error deleting: " & SysEx.Message)
            Return False
        End Try
    End Function

#End Region

End Class
