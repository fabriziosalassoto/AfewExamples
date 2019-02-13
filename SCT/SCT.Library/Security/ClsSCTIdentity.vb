Imports Csla
Imports System.Security.Principal

Namespace Security
    <Serializable()> Public Class ClsSCTIdentity
        Inherits ReadOnlyBase(Of ClsSCTIdentity)
        Implements IIdentity

#Region " Business Methods "

        Protected Overrides Function GetIdValue() As Object
            Return Me.mName
        End Function

#Region " IsInProfile "

        Protected mRoles As New List(Of String)

        Friend Function IsInRole(ByVal role As String) As Boolean
            Return Me.mRoles.Contains(role)
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

        Friend Shared Function UnauthenticatedIdentity() As ClsSCTIdentity
            Return New ClsSCTIdentity
        End Function

        Friend Overridable Function GetIdentity(ByVal userName As String, ByVal password As String) As ClsSCTIdentity
            Return New ClsSCTIdentity
        End Function

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

#End Region

    End Class
End Namespace
