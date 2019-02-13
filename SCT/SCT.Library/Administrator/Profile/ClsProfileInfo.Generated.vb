Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsProfileInfo
    Inherits ReadOnlyBase(Of ClsProfileInfo)

#Region " Business Methods "

    Private mID As Long
    Private mDescription As String = String.Empty

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
        Dim Array() As Object = {Me.mID, Me.mDescription}
        Return Array
    End Function

    Public Function GetProfile() As ClsProfile
        Return ClsProfile.GetProfile(Me.mID)
    End Function

#End Region

#Region " Authorization Rules "

    'Protected Overrides Sub AddAuthorizationRules()
    '    AuthorizationRules.AllowRead("ID", " ")
    '    AuthorizationRules.AllowRead("Description", " ")
    '    AuthorizationRules.AllowRead("Forms", " ")
    'End Sub

    Public Shared Function CanGetObject() As Boolean
        Return True 'ApplicationContext.User.IsInRole(" ")
    End Function

#End Region

#Region " Factory Methods "

    Public Shared Function NewProfileInfo() As ClsProfileInfo
        Return New ClsProfileInfo
    End Function

    Public Shared Function GetProfileInfo(ByVal ID As Long) As ClsProfileInfo
        Return DataPortal.Fetch(Of ClsProfileInfo)(New Criteria(ID))
    End Function

    Public Shared Function GetProfileInfo(ByVal Struct As DAClsprgAdministrativeProfiles.Struct) As ClsProfileInfo
        Return DataPortal.Fetch(Of ClsProfileInfo)(Struct)
    End Function

    Private Sub New()
        ' Require use of factory methods
    End Sub

#End Region

#Region " Data Access "

    <Serializable()> Private Class Criteria

        Private mID As Long

        Public ReadOnly Property ID() As Long
            Get
                Return mID
            End Get
        End Property

        Public Sub New(ByVal ID As Long)
            mID = ID
        End Sub

    End Class

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
        Dim List As DAClsprgAdministrativeProfiles.Struct() = DAClsprgAdministrativeProfiles.Fetch(criteria.ID)
        If List.Length > 0 Then
            Me.LoadObjectData(List(0))
        Else
            Throw New System.Security.SecurityException("Profile doesn't exist")
        End If
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal Struct As DAClsprgAdministrativeProfiles.Struct)
        Me.LoadObjectData(Struct)
    End Sub

    ''' <summary>
    ''' Load the data of the object with the fetched data
    ''' </summary>
    ''' <param name="Struct">Struct with the data</param>
    ''' <remarks></remarks>
    Private Sub LoadObjectData(ByVal Struct As DAClsprgAdministrativeProfiles.Struct)
        With Struct
            Me.mID = .ID.Value
            Me.mDescription = .Description.Value
        End With
    End Sub

#End Region

End Class
