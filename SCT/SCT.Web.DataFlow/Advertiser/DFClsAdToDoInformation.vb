Imports SCT.Library

Public Class DFClsAdToDoInformation

#Region " Private Fields "

    Private mForm As Object
    Private mData As AdToDoData

#End Region

#Region " Properties And Methods "

    Public WriteOnly Property Form() As Object
        Set(ByVal value As Object)
            Me.mForm = value
        End Set
    End Property

    Public Sub New(ByRef form As Object)
        Me.mForm = form
        Me.mForm.Session("ValuePath") = "frmAdToDos"

        ApplyPageAuthorizationRules()

        If IsNumeric(Me.mForm.Session("todoID")) AndAlso Me.mForm.Session("todoID") <> 0 Then
            Me.mData = Me.GetData(Me.mForm.Session("todoID"))
            If Me.mData IsNot Nothing Then
                Me.PopulateData()
                Me.EnableMenuItem(True)
            End If
        End If
    End Sub

    Public Sub ApplyPageAuthorizationRules()
        If Not WebSite.ClsSessionAdmin.IsValidForm("frmAdToDoInformation", "txtAdToDoID", "txtAdToDoContact", "txtAdToDoDateEntered", "txtAdToDoDateDue", "txtAdToDoDateCompleted", "txtAdToDoTaskNote", "imgAdToDoCallBackRecord") Then Me.mForm.Response.Redirect("~/Forms/frmMain.aspx")
        Me.EnableMenuItem(False)
    End Sub

    Public Sub EnableMenuItem(ByVal value As Boolean)
        Me.mForm.FindControls("mnuContact").FindItem("Contact").Enabled = value

        Me.mForm.FindControls("mnuEditToDo").FindItem("Edit").Enabled = value
        Me.mForm.FindControls("mnuEditToDo").FindItem("Delete").Enabled = value
        Me.mForm.FindControls("mnuEditToDo").FindItem("Return").Enabled = True
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
        Me.mForm.Session("todoID") = Me.mData.ID
        Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdChangeToDoInformation.aspx")
    End Sub

    Public Sub OnDeleting()
        Me.mForm.FindControls("MsgBox").ShowConfirmation("Are you sure you want to delete this To Do?\n\nNote: there is no undo.", "", True, False)
    End Sub

    Public Sub OnOkDelete()
        If Me.DeleteData Then
            Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdToDos.aspx")
        End If
    End Sub

    Public Sub OnReturn()
        Me.mForm.Response.Redirect("~/frmsAdvertiser/frmAdToDos.aspx")
    End Sub

#End Region

#Region " Data Methods "

    Private Sub PopulateData()
        Me.mForm.FindControls("txtAdToDoID").Text = Me.mData.ID
        Me.mForm.FindControls("txtAdToDoContact").Text = Me.mData.Contact.FullName
        Me.mForm.FindControls("txtAdToDoDateEntered").Text = Me.DateFormat(Me.mData.DateEntered)
        Me.mForm.FindControls("txtAdToDoDateDue").Text = Me.DateFormat(Me.mData.DateDue)
        Me.mForm.FindControls("txtAdToDoDateCompleted").Text = Me.DateFormat(Me.mData.DateCompleted)
        Me.mForm.FindControls("txtAdToDoTaskNotes").Text = Me.mData.TaskNotes
        Me.mForm.FindControls("imgAdToDoCallBackRecord").ImageUrl = "~/Images/Checked_" & Me.mData.CallBackRecord.ToString & ".png"
    End Sub

    Private Function CollectDataID() As AdToDoNewData
        Dim formData As New AdToDoNewData
        formData.ID.SetValues("txtAdToDoID", True, Me.mData.ID, Me.mData.ID)
        Return formData
    End Function

    Private Function CollectDataID(ByVal todoID As String) As AdToDoNewData
        Dim formData As New AdToDoNewData
        formData.ID.SetValues("txtAdToDoID", True, 0, CLng(todoID))
        Return formData
    End Function

    Private Function GetData(ByVal todoID As String) As AdToDoData
        Try
            Return WebSite.ClsSessionAdmin.GetAdToDo(Me.CollectDataID(todoID))
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error reading: " & SysEx.Message)
            Return Nothing
        End Try
    End Function

    Private Function DeleteData() As Boolean
        Try
            WebSite.ClsSessionAdmin.DeleteAdToDo(Me.CollectDataID)
            Return True
        Catch SysEx As Exception
            Me.mForm.FindControls("MsgBox").ShowMessage("Error deleting: " & SysEx.Message)
            Return False
        End Try
    End Function

#End Region

End Class
