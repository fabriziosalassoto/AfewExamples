Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsUserInfo
    Inherits ReadOnlyBase(Of ClsUserInfo)

#Region " Business Methods "

    Private mID As Long
    Private mProfile As ClsProfileInfo = ClsProfileInfo.NewProfileInfo
    Private mFirstName As String = String.Empty
    Private mLastName As String = String.Empty

    Public ReadOnly Property ID() As Long
        Get
            Return Me.mID
        End Get
    End Property

    Public ReadOnly Property Profile() As ClsProfileInfo
        Get
            Return Me.mProfile
        End Get
    End Property

    Public ReadOnly Property FirstName() As String
        Get
            Return Me.mFirstName
        End Get
    End Property

    Public ReadOnly Property LastName() As String
        Get
            Return Me.mLastName
        End Get
    End Property

    Public ReadOnly Property FullName() As String
        Get
            If CanReadProperty("FirstName") AndAlso CanReadProperty("LastName") Then
                Return Me.mLastName & ", " & Me.mFirstName
            Else
                Throw New System.Security.SecurityException("FullName read not allowed")
            End If
        End Get
    End Property

    Protected Overrides Function GetIdValue() As Object
        Return Me.mID
    End Function

    Public Overrides Function ToString() As String
        Return Me.mLastName & ", " & Me.mFirstName
    End Function

    Public Function ToListItem() As System.Web.UI.WebControls.ListItem
        Return New System.Web.UI.WebControls.ListItem(Me.ToString, Me.mID)
    End Function

    Public Function ToArray() As Object()
        Dim Array() As Object = {Me.mID, Me.mFirstName, Me.mLastName}
        Return Array
    End Function

    Public Function GetAdministratorUser() As ClsUser
        Return ClsUser.GetUser(Me.mID)
    End Function

#End Region

#Region " Authorization Rules "

    'Protected Overrides Sub AddAuthorizationRules()
    '    AuthorizationRules.AllowRead("ID", " ")
    '    AuthorizationRules.AllowRead("IDProfile", " ")
    '    AuthorizationRules.AllowRead("FirstName", " ")
    '    AuthorizationRules.AllowRead("LastName", " ")
    '    AuthorizationRules.AllowRead("Login", " ")
    '    AuthorizationRules.AllowRead("Password", " ")
    'End Sub

    Public Shared Function CanGetObject() As Boolean
        Return True 'ApplicationContext.User.IsInRole(" ")
    End Function

#End Region

#Region " Factory Methods "

    Public Shared Function NewUserInfo() As ClsUserInfo
        Return New ClsUserInfo
    End Function

    Public Shared Function GetUserInfo(ByVal ID As Long) As ClsUserInfo
        If Not CanGetObject() Then
            Throw New System.Security.SecurityException("User not authorized to view User info records.")
        End If
        Return DataPortal.Fetch(Of ClsUserInfo)(New Criteria(ID))
    End Function

    Public Shared Function GetUserInfo(ByVal Struct As DAClsprgAdministrativeUsers.Struct) As ClsUserInfo
        If Not CanGetObject() Then
            Throw New System.Security.SecurityException("User not authorized to view User info records.")
        End If
        Return DataPortal.Fetch(Of ClsUserInfo)(Struct)
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
                Return Me.mID
            End Get
        End Property

        Public Sub New(ByVal ID As Long)
            Me.mID = ID
        End Sub

    End Class

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
        Dim List As DAClsprgAdministrativeUsers.Struct() = DAClsprgAdministrativeUsers.Fetch(criteria.ID)
        If List.Length > 0 Then
            Me.LoadObjectData(List(0))
        Else
            Throw New System.Security.SecurityException("Administrative User doesn't exist")
        End If
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal Struct As DAClsprgAdministrativeUsers.Struct)
        Me.LoadObjectData(Struct)
    End Sub

    ''' <summary>
    ''' Load the data of the object with the fetched data
    ''' </summary>
    ''' <param name="Struct">Struct with the data</param>
    ''' <remarks></remarks>
    Private Sub LoadObjectData(ByVal Struct As DAClsprgAdministrativeUsers.Struct)
        With Struct
            Me.mID = .ID.Value
            Me.mProfile = ClsProfileInfo.GetProfileInfo(.IDProfile.Value)
            Me.mFirstName = .FirstName.Value
            Me.mLastName = .LastName.Value
        End With
    End Sub

#End Region

End Class
