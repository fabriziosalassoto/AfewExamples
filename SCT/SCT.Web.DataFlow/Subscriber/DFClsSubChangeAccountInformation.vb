Imports System.Globalization
Imports System.Web.UI.WebControls

Public Class DFClsSubChangeAccountInformation

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
        Me.mData = Me.GetData(Mid(Csla.ApplicationContext.User.Identity.Name, 1, Csla.ApplicationContext.User.Identity.Name.IndexOf(";")))
        Me.LoadAgeCombo()
        Me.LoadSexCombo()
        Me.LoadOccupationCombo()
        Me.LoadCountryCombo()
        Me.LoadStateCombo(String.Empty)
        Me.PopulateAccountData()
    End Sub

    Private Sub LoadCombo(ByVal combo As DropDownList, ByVal data As DataTable)
        combo.DataSource = data
        combo.DataTextField = data.Columns(0).Caption
        combo.DataValueField = data.Columns(1).Caption
        combo.DataBind()
    End Sub

    Public Sub LoadAgeCombo()
        LoadCombo(Me.mForm.FindControls("ddlAge"), Me.GetAgeData)
    End Sub

    Public Sub LoadSexCombo()
        LoadCombo(Me.mForm.FindControls("ddlSex"), Me.GetSexData)
    End Sub

    Public Sub LoadOccupationCombo()
        LoadCombo(Me.mForm.FindControls("ddlOccupation"), Me.GetOccupationData)
    End Sub

    Public Sub LoadCountryCombo()
        LoadCombo(Me.mForm.FindControls("ddlCountry"), Me.GetCountryData)
    End Sub

    Public Sub LoadStateCombo(ByVal filter As String)
        LoadCombo(Me.mForm.FindControls("ddlState"), Me.GetStateData(filter))
    End Sub

    Private Function GetAgeData() As DataTable
        Dim table As New DataTable

        table.Columns.Add("Text", GetType(String))
        table.Columns.Add("Value", GetType(String))
        table.Rows.Add(New Object() {"", ""})

        For i As Integer = 1 To 99
            table.Rows.Add(New Object() {Format(i, "00"), i})
        Next
        Return table
    End Function

    Private Function GetSexData() As DataTable
        Dim table As New DataTable
        table.ReadXml(Me.mForm.MapPath("~") & "/App_Data/Sex.xml")
        table.Rows.Add(New Object() {"", ""})
        table.DefaultView.Sort = "Text"
        Return table
    End Function

    Private Function GetOccupationData() As DataTable
        Dim table As New DataTable
        table.ReadXml(Me.mForm.MapPath("~") & "/App_Data/Occupations.xml")
        Return table
    End Function

    Private Function GetCountryData() As DataTable
        Dim reginfo As RegionInfo
        Dim cultInfoList() As CultureInfo

        Dim table As New DataTable
        Dim row As DataRow

        table.Columns.Add("Text", GetType(String))
        table.Columns.Add("Value", GetType(String))
        table.Rows.Add(New Object() {"", ""})

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
        table.Rows.Add(New Object() {"", "", filter})
        table.DefaultView.Sort = "Name"
        table.DefaultView.RowFilter = "CodeCountry = '" & filter & "'"
        Return table
    End Function

    Public Sub OnSelectedCountry()
        Me.LoadStateCombo(Me.mForm.FindControls("ddlCountry").SelectedValue)
        Me.mForm.FindControls("ddlState").Focus()
    End Sub

    Public Sub OnOk()
        If Me.EditData() Then
            Me.mForm.FindControls("MsgBox").ShowMessage("The changes were made correctly.")
        End If
    End Sub

    Public Sub OnReturn()
        Me.mForm.Response.Redirect("~/frmsSubscriber/frmSubscriberAccount.aspx")
    End Sub

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
        Me.mForm.FindControls("ddlAge").SelectedValue = Me.GetAnswer("Age")
        Me.mForm.FindControls("ddlSex").SelectedValue = Me.GetAnswer("Sex")
        Me.mForm.FindControls("ddlOccupation").SelectedValue = Me.GetAnswer("Occupation")
        Me.mForm.FindControls("ddlCountry").SelectedValue = Me.GetAnswer("Country")
        Me.LoadStateCombo(Me.mForm.FindControls("ddlCountry").SelectedValue)
        Me.mForm.FindControls("ddlState").SelectedValue = Me.GetAnswer("State")
    End Sub

    Private Function GetAnswer(ByVal tag As String) As String
        For Each demographicData As SCTServer.ClsSubscriberDemographicData In Me.mData.Demographics
            If demographicData.Tag = tag Then
                Return demographicData.Answer
            End If
        Next
        Return String.Empty
    End Function

    Private Sub CollectAccountData()
        Me.mData.ContactEmail = Me.mForm.FindControls("txtEmail").Text
        Me.mData.Demographics = Me.CollectDemographicsData
    End Sub

    Private Function CollectDemographicsData() As SCTServer.ClsSubscriberDemographicData()
        Dim demographics As New Generic.List(Of SCTServer.ClsSubscriberDemographicData)
        If Not Me.mForm.FindControls("txtFirstName").Text = String.Empty Then
            demographics.Add(Me.CollectDemographicData("First Name", CType(Me.mForm.FindControls("txtFirstName"), TextBox)))
        End If
        If Not Me.mForm.FindControls("txtLastName").Text = String.Empty Then
            demographics.Add(Me.CollectDemographicData("Last Name", CType(Me.mForm.FindControls("txtLastName"), TextBox)))
        End If
        If Not Me.mForm.FindControls("ddlAge").SelectedValue = String.Empty Then
            demographics.Add(Me.CollectDemographicData("Age", CType(Me.mForm.FindControls("ddlAge"), DropDownList)))
        End If
        If Not Me.mForm.FindControls("ddlSex").SelectedValue = String.Empty Then
            demographics.Add(Me.CollectDemographicData("Sex", CType(Me.mForm.FindControls("ddlSex"), DropDownList)))
        End If
        If Not Me.mForm.FindControls("ddlOccupation").SelectedValue = String.Empty Then
            demographics.Add(Me.CollectDemographicData("Occupation", CType(Me.mForm.FindControls("ddlOccupation"), DropDownList)))
        End If
        If Not Me.mForm.FindControls("ddlCountry").SelectedValue = String.Empty Then
            demographics.Add(Me.CollectDemographicData("Country", CType(Me.mForm.FindControls("ddlCountry"), DropDownList)))
        End If
        If Not Me.mForm.FindControls("ddlState").SelectedValue = String.Empty Then
            demographics.Add(Me.CollectDemographicData("State", CType(Me.mForm.FindControls("ddlState"), DropDownList)))
        End If
        Return demographics.ToArray
    End Function

    Private Function CollectDemographicData(ByVal tag As String, ByRef textBox As TextBox) As SCTServer.ClsSubscriberDemographicData
        Dim demographic As New SCTServer.ClsSubscriberDemographicData
        demographic.Tag = tag
        demographic.Answer = textBox.Text
        Return demographic
    End Function

    Private Function CollectDemographicData(ByVal tag As String, ByRef dropDownList As DropDownList) As SCTServer.ClsSubscriberDemographicData
        Dim demographic As New SCTServer.ClsSubscriberDemographicData
        demographic.Tag = tag
        demographic.Answer = dropDownList.SelectedValue
        Return demographic
    End Function

    Private Function EditData() As Boolean
        Using svc As New SCTServer.SCTServer
            Try
                Me.CollectAccountData()
                svc.EditSubsciberAccount(Me.mData)
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
