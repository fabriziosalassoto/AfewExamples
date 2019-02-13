Imports SCT.Library
Imports System.Globalization
Imports System.Web.UI.WebControls

Public Class DFClsSubSignUp

#Region " Private Fields "
    Private mForm As Object
#End Region

#Region " Properties And Methods "

    Public WriteOnly Property Form() As Object
        Set(ByVal value As Object)
            Me.mForm = value
        End Set
    End Property

    Public Sub New(ByRef form As Object)
        Me.mForm = form
        Me.LoadAgeCombo()
        Me.LoadSexCombo()
        Me.LoadOccupationCombo()
        Me.LoadCountryCombo()
        Me.LoadStateCombo(String.Empty)
    End Sub

    Private Sub LoadCombo(ByVal combo As DropDownList, ByVal data As DataTable)
        combo.DataSource = data
        combo.DataTextField = data.Columns(0).Caption
        combo.DataValueField = data.Columns(1).Caption
        combo.DataBind()
    End Sub

    Public Sub LoadAgeCombo()
        LoadCombo(Me.mForm.FindControl("ddlAge"), Me.GetAgeData)
    End Sub

    Public Sub LoadSexCombo()
        LoadCombo(Me.mForm.FindControl("ddlSex"), Me.GetSexData)
    End Sub

    Public Sub LoadOccupationCombo()
        LoadCombo(Me.mForm.FindControl("ddlOccupation"), Me.GetOccupationData)
    End Sub

    Public Sub LoadCountryCombo()
        LoadCombo(Me.mForm.FindControl("ddlCountry"), Me.GetCountryData)
    End Sub

    Public Sub LoadStateCombo(ByVal filter As String)
        LoadCombo(Me.mForm.FindControl("ddlState"), Me.GetStateData(filter))
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

    Public Function ValidateLoginExists(ByVal ControlName As String) As Boolean
        Return Not Subscriber.ClsAccount.ExistsLogin(Me.mForm.FindControl(ControlName).Text)
    End Function

    Public Function ValidateSerialNbrExists(ByVal ControlName As String) As Boolean
        Return Not Subscriber.ClsAccount.ExistsSerialNbr(Me.mForm.FindControl(ControlName).Text)
    End Function

    Public Sub OnSelectedCountry()
        Me.LoadStateCombo(Me.mForm.FindControl("ddlCountry").SelectedValue)
        Me.mForm.FindControl("ddlState").Focus()
    End Sub

    Public Function ValidateUser(ByVal userName As String, ByVal password As String) As Boolean
        Try
            If SCT.Library.Security.ClsSCTSubscriberPrincipal.Login(userName, password) Then
                System.Web.HttpContext.Current.Session("CslaPrincipal") = Csla.ApplicationContext.User
                System.Web.Security.FormsAuthentication.SetAuthCookie(userName, True)
                Return True
            Else
                Return False
            End If
        Catch CslaEx As Csla.DataPortalException
            Me.mForm.FindControl("MsgBox").ShowMessage(CslaEx.BusinessException.Message)
            Return False
        Catch SysEx As Exception
            Me.mForm.FindControl("MsgBox").ShowMessage(SysEx.Message)
            Return False
        End Try
    End Function

    Public Sub OnOk()
        If Me.SaveData() Then
            If ValidateUser(Me.mForm.FindControl("txtLogin").Text, Me.mForm.FindControl("txtWebPassword").Text) Then
                Me.mForm.Response.Redirect("~/frmsSubscriber/frmSubscriberWelcome.aspx")
            End If
        End If
    End Sub

    Public Sub OnCancel()
        Me.mForm.Response.Redirect("~/frmsSystem/frmSubscriberSignIn.aspx")
    End Sub

#End Region

#Region " Data Methods "

    Private Function CollectAccountData() As SCTServer.ClsSubscriberAccountData
        Dim account As New SCTServer.ClsSubscriberAccountData
        account.Login = Me.mForm.FindControl("txtLogin").Text
        account.WebPassword = Me.mForm.FindControl("txtWebPassword").Text
        account.ComputerSerialNumber = Me.mForm.FindControl("txtSerialNbr").Text
        account.ClientPassword = Me.mForm.FindControl("txtComputerPassword").Text
        account.ContactEmail = Me.mForm.FindControl("txtEmail").Text
        account.Demographics = Me.CollectDemographicsData
        Return account
    End Function

    Private Function CollectDemographicsData() As SCTServer.ClsSubscriberDemographicData()
        Dim demographics As New Generic.List(Of SCTServer.ClsSubscriberDemographicData)
        If Not Me.mForm.FindControl("txtFirstName").Text = String.Empty Then
            demographics.Add(Me.CollectDemographicData("First Name", CType(Me.mForm.FindControl("txtFirstName"), TextBox)))
        End If
        If Not Me.mForm.FindControl("txtLastName").Text = String.Empty Then
            demographics.Add(Me.CollectDemographicData("Last Name", CType(Me.mForm.FindControl("txtLastName"), TextBox)))
        End If
        If Not Me.mForm.FindControl("ddlAge").SelectedValue = String.Empty Then
            demographics.Add(Me.CollectDemographicData("Age", CType(Me.mForm.FindControl("ddlAge"), DropDownList)))
        End If
        If Not Me.mForm.FindControl("ddlSex").SelectedValue = String.Empty Then
            demographics.Add(Me.CollectDemographicData("Sex", CType(Me.mForm.FindControl("ddlSex"), DropDownList)))
        End If
        If Not Me.mForm.FindControl("ddlOccupation").SelectedValue = String.Empty Then
            demographics.Add(Me.CollectDemographicData("Occupation", CType(Me.mForm.FindControl("ddlOccupation"), DropDownList)))
        End If
        If Not Me.mForm.FindControl("ddlCountry").SelectedValue = String.Empty Then
            demographics.Add(Me.CollectDemographicData("Country", CType(Me.mForm.FindControl("ddlCountry"), DropDownList)))
        End If
        If Not Me.mForm.FindControl("ddlState").SelectedValue = String.Empty Then
            demographics.Add(Me.CollectDemographicData("State", CType(Me.mForm.FindControl("ddlState"), DropDownList)))
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

    Private Function SaveData() As Boolean
        Using svc As New SCTServer.SCTServer
            Try
                svc.AddSubsciberAccount(Me.CollectAccountData)
                Return True
            Catch DataEx As Csla.DataPortalException
                Me.mForm.FindControl("MsgBox").ShowMessage(DataEx.BusinessException.Message)
                Return False
            Catch SysEx As Exception
                Me.mForm.FindControl("MsgBox").ShowMessage(SysEx.Message)
                Return False
            End Try
        End Using
    End Function

#End Region

End Class
