Imports System.Globalization
Imports System.Web.UI.WebControls

Public Class DFClsSubAccountInformation

#Region " Private Fields "
    Private mForm As Object
    Private mData As SCTServer.ClsSubscriberAccountData
#End Region

#Region " Properties And Methods "

    Public WriteOnly Property Form() As Object
        Set(ByVal value As Object)
            Me.mForm = value
        End Set
    End Property

    Public Sub New(ByRef form As Object)
        Me.mForm = form
        Me.mForm.Session("ValuePath") = "frmSubscriberAccount"
        Me.mData = Me.GetData(Mid(Csla.ApplicationContext.User.Identity.Name, 1, Csla.ApplicationContext.User.Identity.Name.IndexOf(";")))
        Me.PopulateAccountData()
    End Sub

    Private Function GetSexDescription(ByVal value As String) As DataTable
        Dim table As New DataTable
        table.ReadXml(Me.mForm.MapPath("~") & "/App_Data/Sex.xml")
        table.Rows.Add(New Object() {"", ""})
        table.DefaultView.Sort = "Text"
        table.DefaultView.RowFilter = "Value = '" & value & "'"
        Return table
    End Function

    Private Function GetOccupationDescription(ByVal code As String) As DataTable
        Dim table As New DataTable
        table.ReadXml(Me.mForm.MapPath("~") & "/App_Data/Occupations.xml")
        table.DefaultView.RowFilter = "Code = '" & code & "'"
        Return table
    End Function

    Private Function GetCountryName(ByVal value As String) As DataTable
        Dim reginfo As RegionInfo
        Dim cultInfoList() As CultureInfo

        Dim table As New DataTable
        Dim row As DataRow

        table.Columns.Add("Text", GetType(String))
        table.Columns.Add("Value", GetType(String))
        table.Rows.Add(New Object() {"", ""})

        table.DefaultView.Sort = "Text"
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
        Return table
    End Function

    Private Function GetStateName(ByVal codeCountry As String, ByVal codeState As String) As DataTable
        Dim table As New DataTable
        table.ReadXml(Me.mForm.MapPath("~") & "/App_Data/States.xml")
        table.Rows.Add(New Object() {"", "", codeCountry})
        table.DefaultView.Sort = "Name"
        table.DefaultView.RowFilter = "CodeCountry = '" & codeCountry & "' and CodeState = '" & codeState & "'"
        Return table
    End Function

#End Region

#Region " Data Methods "

    Private Sub PopulateAccountData()
        Me.mForm.FindControls("txtLogin").Text = Me.mData.Login
        Me.mForm.FindControls("txtSerialNbr").Text = Me.mData.ComputerSerialNumber
        Me.mForm.FindControls("txtEmail").Text = Me.mData.ContactEmail
        If Me.mData.Demographics IsNot Nothing Then
            Me.PopulateDemographicsData()
        End If
    End Sub

    Private Sub PopulateDemographicsData()
        Me.mForm.FindControls("txtFirstName").Text = Me.GetAnswer("First Name")
        Me.mForm.FindControls("txtLastName").Text = Me.GetAnswer("Last Name")
        Me.mForm.FindControls("txtAge").Text = Me.GetAnswer("Age")
        Me.mForm.FindControls("txtSex").Text = Me.GetSexDescription(Me.GetAnswer("Sex")).DefaultView.Item(0).Item("Text")
        Me.mForm.FindControls("txtOccupation").Text = Me.GetOccupationDescription(Me.GetAnswer("Occupation")).DefaultView.Item(0).Item("Description")
        Me.mForm.FindControls("txtCountry").Text = Me.GetCountryName(Me.GetAnswer("Country")).DefaultView.Item(0).Item("Text")
        Me.mForm.FindControls("txtState").Text = Me.GetStateName(Me.GetAnswer("Country"), Me.GetAnswer("State")).DefaultView.Item(0).Item("Name")
    End Sub

    Private Function GetAnswer(ByVal tag As String) As String
        For Each demographicData As SCTServer.ClsSubscriberDemographicData In Me.mData.Demographics
            If demographicData.Tag = tag Then
                Return demographicData.Answer
            End If
        Next
        Return String.Empty
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
