Imports Csla
Imports SCT.DataAccess

Namespace Subscriber
    <Serializable()> Public Class ClsAccountInfoList
        Inherits ReadOnlyListBase(Of ClsAccountInfoList, ClsAccountInfo)

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole("")
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function GetSubscriberAccountInfoList() As ClsAccountInfoList
            Return DataPortal.Fetch(Of ClsAccountInfoList)(New Criteria)
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> Private Class Criteria
            ' no criteria - retrieve all projects
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            ' fetch with no filter
            Fetch()
        End Sub

        Private Sub Fetch()
            Fetch(DAClsappSubscribersAccounts.Fetch())
        End Sub

        Private Sub Fetch(ByVal Accounts As DAClsappSubscribersAccounts.Struct())
            RaiseListChangedEvents = False
            IsReadOnly = False
            For Each Struct As DAClsappSubscribersAccounts.Struct In Accounts
                Me.Add(ClsAccountInfo.GetAccountInfo(Struct))
            Next
            IsReadOnly = True
            RaiseListChangedEvents = True
        End Sub

#End Region

    End Class
End Namespace