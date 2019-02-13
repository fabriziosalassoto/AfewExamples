Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsProfileFormInfo
    Inherits ReadOnlyBase(Of ClsProfileFormInfo)

#Region " Business Methods "

    Private mID As Long
    Private mDescription As String = String.Empty
    Private mProfile As ClsProfileInfo = ClsProfileInfo.NewProfileInfo
    Private mPSelect As Boolean = False
    Private mPInsert As Boolean = False
    Private mPUpdate As Boolean = False
    Private mPDelete As Boolean = False

    Public ReadOnly Property ID() As Long
        Get
            Return Me.mID
        End Get
    End Property

    Public ReadOnly Property Description() As String
        Get
            Return Me.mDescription
        End Get
    End Property

    Public ReadOnly Property Profile() As ClsProfileInfo
        Get
            Return Me.mProfile
        End Get
    End Property

    Public Property PSelect() As Boolean
        Get
            Return Me.mPSelect
        End Get
        Friend Set(ByVal value As Boolean)
            Me.mPSelect = value
        End Set
    End Property

    Public Property PInsert() As Boolean
        Get
            Return Me.mPInsert
        End Get
        Friend Set(ByVal value As Boolean)
            Me.mPInsert = value
        End Set
    End Property

    Public Property PUpdate() As Boolean
        Get
            Return Me.mPUpdate
        End Get
        Friend Set(ByVal value As Boolean)
            Me.mPUpdate = value
        End Set
    End Property

    Public Property PDelete() As Boolean
        Get
            Return Me.mPDelete
        End Get
        Friend Set(ByVal value As Boolean)
            Me.mPDelete = value
        End Set
    End Property

    Protected Overrides Function GetIdValue() As Object
        Return Me.mID
    End Function

    Public Overrides Function ToString() As String
        Return Me.mDescription
    End Function

    Public Function ToListItem() As System.Web.UI.WebControls.ListItem
        Return New System.Web.UI.WebControls.ListItem(Me.ToString, Me.mID)
    End Function

    Public Function ToArray() As Object()
        Dim Array() As Object = {Me.mID, Me.mDescription, Me.mPSelect, Me.mPInsert, Me.mPUpdate, Me.mPDelete}
        Return Array
    End Function

#End Region

#Region " Authorization Rules "

    'Protected Overrides Sub AddAuthorizationRules()
    '    AuthorizationRules.AllowRead("IDForm", " ")
    '    AuthorizationRules.AllowRead("Description", " ")
    '    AuthorizationRules.AllowRead("Groups", " ")
    '    AuthorizationRules.AllowRead("PSelect", " ")
    '    AuthorizationRules.AllowRead("PInsert", " ")
    '    AuthorizationRules.AllowRead("PUpdate", " ")
    '    AuthorizationRules.AllowRead("PDelete", " ")
    'End Sub

    Public Shared Function CanGetObject() As Boolean
        Return True 'ApplicationContext.User.IsInRole(" ")
    End Function

#End Region

#Region " Factory Methods "

    Public Shared Function NewProfileFormInfo() As ClsProfileFormInfo
        Return New ClsProfileFormInfo
    End Function

    Public Shared Function GetProfileFormInfo(ByVal IDProfile As Long, ByVal IDForm As Long) As ClsProfileFormInfo
        Return DataPortal.Fetch(Of ClsProfileFormInfo)(New Criteria(IDProfile, IDForm))
    End Function

    Public Shared Function GetProfileFormInfo(ByVal Struct As DAClsprgAdministrativeFormPermissions.Struct) As ClsProfileFormInfo
        Return DataPortal.Fetch(Of ClsProfileFormInfo)(Struct)
    End Function

    Private Sub New()
        ' Require use of factory methods
    End Sub

#End Region

#Region " Data Access "

    <Serializable()> Private Class Criteria

        Private mIDProfile As Long
        Private mIDForm As Long

        Public ReadOnly Property IDProfile() As Long
            Get
                Return Me.mIDProfile
            End Get
        End Property

        Public ReadOnly Property IDForm() As Long
            Get
                Return Me.mIDForm
            End Get
        End Property

        Public Sub New(ByVal IDProfile As Long, ByVal IDForm As Long)
            Me.mIDProfile = IDProfile
            Me.mIDForm = IDForm
        End Sub

    End Class

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
        Dim List As DAClsprgAdministrativeFormPermissions.Struct() = DAClsprgAdministrativeFormPermissions.Fetch(criteria.IDProfile, criteria.IDForm)
        If List.Length > 0 Then
            Me.LoadObjectData(List(0))
        Else
            Throw New System.Security.SecurityException("Form doesn't exist")
        End If
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal Struct As DAClsprgAdministrativeFormPermissions.Struct)
        Me.LoadObjectData(Struct)
    End Sub

    ''' <summary>
    ''' Load the data of the object with the fetched data
    ''' </summary>
    ''' <param name="FormPermissions">Struct with the data</param>
    ''' <remarks></remarks>
    Private Sub LoadObjectData(ByVal FormPermissions As DAClsprgAdministrativeFormPermissions.Struct)
        Dim forms As DAClsprgForms.Struct() = DAClsprgForms.Fetch(FormPermissions.IDForm.Value)
        If forms.Length > 0 Then
            Me.mID = forms(0).ID.Value
            Me.mDescription = forms(0).Description.Value
            Me.mProfile = ClsProfileInfo.GetProfileInfo(FormPermissions.IDProfile.Value)
            Me.mPSelect = FormPermissions.pSelect.Value
            Me.mPInsert = FormPermissions.pInsert.Value
            Me.mPUpdate = FormPermissions.pUpdate.Value
            Me.mPDelete = FormPermissions.pDelete.Value
        Else
            Throw New System.Security.SecurityException("Form doesn't exist")
        End If
    End Sub

#End Region

End Class
