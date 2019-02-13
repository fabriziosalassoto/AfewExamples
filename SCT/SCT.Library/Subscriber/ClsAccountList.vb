Imports Csla
Imports SCT.DataAccess

Namespace Subscriber
    <Serializable()> Public Class ClsAccountList
        Inherits BusinessListBase(Of ClsAccountList, ClsAccount)

#Region " Business Methods "

        Public Function GetItem(ByVal AccountId As Long) As ClsAccount
            For Each account As ClsAccount In Me
                If account.ID = AccountId Then
                    Return account
                End If
            Next
            Return Nothing
        End Function

        Public Overloads Sub Remove(ByVal AccountId As Long)
            For Each account As ClsAccount In Me
                If account.ID = AccountId Then
                    Remove(account)
                    Exit For
                End If
            Next
        End Sub

        Public Overloads Function Contains(ByVal AccountId As Long) As Boolean
            For Each account As ClsAccount In Me
                If account.ID = AccountId Then
                    Return True
                End If
            Next
            Return False
        End Function

        Public Overloads Function Contains(ByVal computerMacAddress As String) As Boolean
            For Each account As ClsAccount In Me
                If account.ComputerMacAddress = computerMacAddress Then
                    Return True
                End If
            Next
            Return False
        End Function

        Public Sub AddItem(ByVal account As ClsAccount)
            Add(ClsAccount.NewChildAccount(account))
        End Sub

#End Region

#Region " Authorization Rules "

        Public Shared Function CanAddObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole("")
        End Function

        Public Shared Function CanGetObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole("")
        End Function

        Public Shared Function CanEditObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole("")
        End Function

        Public Shared Function CanDeleteObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole("")
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function NewAccountList() As ClsAccountList
            Return DataPortal.Create(Of ClsAccountList)(New Criteria())
        End Function

        Public Shared Function GetAccountList() As ClsAccountList
            Return DataPortal.Fetch(Of ClsAccountList)(New Criteria())
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> Private Class Criteria
            ' no criteria - retrieve all projects
        End Class

        <RunLocal()> Private Overloads Sub DataPortal_Create(ByVal criteria As Criteria)
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            ' fetch with no filter
            Fetch()
        End Sub

        Protected Overrides Sub DataPortal_Update()
            RaiseListChangedEvents = False
            For Each item As ClsAccount In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsAccount In Me
                If item.IsNew Then
                    item.Insert(Nothing)
                Else
                    item.Update(Nothing)
                End If
            Next
            RaiseListChangedEvents = True
        End Sub

        Private Sub Fetch()
            Fetch(DAClsappSubscribersAccounts.Fetch())
        End Sub

        Private Sub Fetch(ByVal list As DAClsappSubscribersAccounts.Struct())
            RaiseListChangedEvents = False
            For Each Struct As DAClsappSubscribersAccounts.Struct In list
                Add(ClsAccount.GetChildAccount(Struct))
            Next
            RaiseListChangedEvents = True
        End Sub

#End Region

    End Class
End Namespace