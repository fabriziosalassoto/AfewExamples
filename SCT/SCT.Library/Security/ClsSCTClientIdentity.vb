Imports Csla
Imports SCT.DataAccess

Namespace Security
    <Serializable()> Public Class ClsSCTClientIdentity
        Inherits ClsSCTIdentity

#Region " Factory Methods "

        Friend Overloads Shared Function GetIdentity(ByVal userName As String, ByVal password As String) As ClsSCTClientIdentity
            Return DataPortal.Fetch(Of ClsSCTClientIdentity)(New Criteria(userName, password))
        End Function

#End Region

#Region " Data Access "

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            Dim List As DAClsappSubscribersAccounts.Struct() = DAClsappSubscribersAccounts.FetchClientSubscriber(criteria.UserName, criteria.Password)
            If List.Length > 0 AndAlso criteria.UserName <> String.Empty AndAlso criteria.Password <> String.Empty AndAlso List(0).ClientPassword.Value = criteria.Password Then
                Me.mName = List(0).ID.Value.ToString & ";" & List(0).ComputerSerialNumber.Value
                Me.mIsAuthenticated = True
                Me.mRoles.Add("Client")
            Else
                Me.mName = String.Empty
                Me.mIsAuthenticated = False
                Me.mRoles.Clear()
            End If
        End Sub

#End Region

    End Class
End Namespace
