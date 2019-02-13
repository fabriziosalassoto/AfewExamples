Imports Csla
Imports SCT.DataAccess
Imports System.Security.Principal

Namespace Security
    <Serializable()> Public Class ClsSCTUserIdentity
        Inherits ReadOnlyBase(Of ClsSCTIdentity)
        Implements IIdentity

#Region " Business Methods "

        Protected Overrides Function GetIdValue() As Object
            Return Me.mName
        End Function

#Region " IsInProfile "

        Protected mProfile As ClsProfileInfo

        Friend Function IsInRole(ByVal profileID As String) As Boolean
            Return Me.mProfile.ID.ToString = profileID
        End Function

#End Region

#Region " IIdentity "

        Protected mIsAuthenticated As Boolean
        Protected mName As String = ""

        Public ReadOnly Property AuthenticationType() As String Implements System.Security.Principal.IIdentity.AuthenticationType
            Get
                Return "Csla"
            End Get
        End Property

        Public ReadOnly Property IsAuthenticated() As Boolean Implements System.Security.Principal.IIdentity.IsAuthenticated
            Get
                Return Me.mIsAuthenticated
            End Get
        End Property

        Public ReadOnly Property Name() As String Implements System.Security.Principal.IIdentity.Name
            Get
                Return Me.mName
            End Get
        End Property

#End Region

#End Region

#Region " Factory Methods "

        Friend Shared Function UnauthenticatedIdentity() As ClsSCTUserIdentity
            Return New ClsSCTUserIdentity
        End Function

        Friend Shared Function GetIdentity(ByVal userName As String, ByVal password As String) As ClsSCTUserIdentity
            Return DataPortal.Fetch(Of ClsSCTUserIdentity)(New Criteria(userName, password))
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Protected Class Criteria

            Private mUserName As String
            Private mPassword As String

            Public ReadOnly Property UserName() As String
                Get
                    Return Me.mUserName
                End Get
            End Property

            Public ReadOnly Property Password() As String
                Get
                    Return Me.mPassword
                End Get
            End Property

            Public Sub New(ByVal userName As String, ByVal password As String)
                Me.mUserName = userName
                Me.mPassword = password
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            Dim List As DAClsprgAdministrativeUsers.Struct() = DAClsprgAdministrativeUsers.Fetch(criteria.UserName, criteria.Password)
            If List.Length > 0 AndAlso criteria.UserName <> String.Empty AndAlso criteria.Password <> String.Empty AndAlso List(0).Password.Value = criteria.Password Then
                Me.mName = List(0).ID.Value.ToString & ";" & List(0).LastName.Value & ", " & List(0).FirstName.Value
                Me.mIsAuthenticated = True
                Me.mProfile = ClsProfileInfo.GetProfileInfo(List(0).IDProfile.Value)
            Else
                Me.mName = String.Empty
                Me.mIsAuthenticated = False
                Me.mProfile = ClsProfileInfo.NewProfileInfo
            End If
        End Sub

#End Region

    End Class
End Namespace
