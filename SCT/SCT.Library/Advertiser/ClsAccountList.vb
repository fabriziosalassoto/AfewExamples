Imports Csla
Imports SCT.DataAccess

Namespace Advertiser
    <Serializable()> Public Class ClsAccountList
        Inherits BusinessListBase(Of ClsAccountList, ClsAccount)

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
            ' Require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> Private Class Criteria
            ' no criteria - retrieve all projects
        End Class

        <Serializable()> Private Class FilteredCriteria

            Private mCompanyName As String

            Public ReadOnly Property CompanyName() As String
                Get
                    Return Me.mCompanyName
                End Get
            End Property

            Public Sub New(ByVal companyName As String)
                Me.mCompanyName = companyName
            End Sub

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
            Fetch(DAClsappAdvertiserAccount.Fetch())
        End Sub

        Private Sub Fetch(ByVal list As DAClsappAdvertiserAccount.Struct())
            RaiseListChangedEvents = False
            For Each Struct As DAClsappAdvertiserAccount.Struct In list
                Add(ClsAccount.GetChildAccount(Struct))
            Next
            RaiseListChangedEvents = True
        End Sub

#End Region

    End Class
End Namespace