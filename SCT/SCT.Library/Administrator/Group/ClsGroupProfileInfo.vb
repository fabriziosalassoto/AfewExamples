Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsGroupProfileInfo
    Inherits ReadOnlyBase(Of ClsGroupProfileInfo)

#Region " Business Methods "

    Private mID As Long
    Private mDescription As String = String.Empty
    Private mGroup As ClsGroupInfo = ClsGroupInfo.NewGroupInfo
    Private mFormProfile As ClsFormProfileInfo = ClsFormProfileInfo.NewFormProfileInfo
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

    Public ReadOnly Property Group() As ClsGroupInfo
        Get
            CanReadProperty(True)
            Return Me.mGroup
        End Get
    End Property

    Public ReadOnly Property FormProfile() As ClsFormProfileInfo
        Get
            CanReadProperty(True)
            Return Me.mFormProfile
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
    '    AuthorizationRules.AllowRead("IDProfile", " ")
    '    AuthorizationRules.AllowRead("Description", " ")
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

    Public Shared Function NewGroupProfileInfo() As ClsGroupProfileInfo
        Return New ClsGroupProfileInfo
    End Function

    Public Shared Function GetGroupProfileInfo(ByVal IDProfile As Long, ByVal IDForm As Long, ByVal IDGroup As Long) As ClsGroupProfileInfo
        If Not CanGetObject() Then
            Throw New System.Security.SecurityException("User not authorized to view Profile info records.")
        End If
        Return DataPortal.Fetch(Of ClsGroupProfileInfo)(New Criteria(IDProfile, IDForm, IDGroup))
    End Function

    Public Shared Function GetGroupProfileInfo(ByVal Struct As DAClsprgAdministrativeGroupPermissions.Struct) As ClsGroupProfileInfo
        If Not CanGetObject() Then
            Throw New System.Security.SecurityException("User not authorized to view Profile info records.")
        End If
        Return DataPortal.Fetch(Of ClsGroupProfileInfo)(Struct)
    End Function

    Private Sub New()
        ' Require use of factory methods
    End Sub

#End Region

#Region " Data Access "

    <Serializable()> Private Class Criteria

        Private mIDProfile As Long
        Private mIDForm As Long
        Private mIDGroup As Long

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

        Public ReadOnly Property IDGroup() As Long
            Get
                Return Me.mIDGroup
            End Get
        End Property

        Public Sub New(ByVal IDProfile As Long, ByVal IDForm As Long, ByVal IDGroup As Long)
            Me.mIDProfile = IDProfile
            Me.mIDForm = IDForm
            Me.mIDGroup = IDGroup
        End Sub

    End Class

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
        Dim List As DAClsprgAdministrativeGroupPermissions.Struct() = DAClsprgAdministrativeGroupPermissions.Fetch(criteria.IDGroup, criteria.IDProfile, criteria.IDForm)
        If List.Length > 0 Then
            Me.LoadObjectData(List(0))
        Else
            Throw New System.Security.SecurityException("Profile doesn't assign to group")
        End If
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal Struct As DAClsprgAdministrativeGroupPermissions.Struct)
        Me.LoadObjectData(Struct)
    End Sub

    ''' <summary>
    ''' Load the data of the object with the fetched data
    ''' </summary>
    ''' <param name="GroupPermissions">Struct with the data</param>
    ''' <remarks></remarks>
    Private Sub LoadObjectData(ByVal groupPermissions As DAClsprgAdministrativeGroupPermissions.Struct)
        Dim profile As DAClsprgAdministrativeProfiles.Struct() = DAClsprgAdministrativeProfiles.Fetch(groupPermissions.IDProfile.Value)
        If profile.Length > 0 Then
            Me.mID = profile(0).ID.Value
            Me.mDescription = profile(0).Description.Value
            Me.mFormProfile = ClsFormProfileInfo.GetFormProfileInfo(profile(0).ID.Value, groupPermissions.IDForm.Value)
            Me.mGroup = ClsGroupInfo.GetGroupInfo(groupPermissions.IDGroup.Value)
            Me.mPSelect = groupPermissions.pSelect.Value
            Me.mPInsert = groupPermissions.pInsert.Value
            Me.mPUpdate = groupPermissions.pUpdate.Value
            Me.mPDelete = groupPermissions.pDelete.Value
        Else
            Throw New System.Security.SecurityException("Profile doesn't assign to group")
        End If
    End Sub

#End Region

End Class
