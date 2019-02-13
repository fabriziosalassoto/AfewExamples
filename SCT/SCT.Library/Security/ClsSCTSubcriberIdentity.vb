Imports Csla
Imports SCT.DataAccess

Namespace Security
    <Serializable()> Public Class ClsSCTSubcriberIdentity
        Inherits ClsSCTIdentity

#Region " Factory Methods "

        Friend Overloads Shared Function GetIdentity(ByVal userName As String, ByVal password As String) As ClsSCTSubcriberIdentity
            Return DataPortal.Fetch(Of ClsSCTSubcriberIdentity)(New Criteria(userName, password))
        End Function

#End Region

#Region " Data Access "

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            Dim List As DAClsappSubscribersAccounts.Struct() = DAClsappSubscribersAccounts.FetchWebSubscriber(criteria.UserName, criteria.Password)
            If List.Length > 0 AndAlso criteria.UserName <> String.Empty AndAlso criteria.Password <> String.Empty AndAlso List(0).WebPassword.Value = criteria.Password Then
                Me.mName = List(0).ID.Value.ToString & ";" & List(0).Login.Value
                Me.mIsAuthenticated = True
                Me.mRoles.Add("Subscriber")
            Else
                Me.mName = String.Empty
                Me.mIsAuthenticated = False
                Me.mRoles.Clear()
            End If
        End Sub

#End Region

    End Class
End Namespace

