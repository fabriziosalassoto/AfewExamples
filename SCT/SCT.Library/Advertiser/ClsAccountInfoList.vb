Imports Csla
Imports SCT.DataAccess

Namespace Advertiser
    <Serializable()> Public Class ClsAccountInfoList
        Inherits ReadOnlyListBase(Of ClsAccountInfoList, ClsAccountInfo)

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole("")
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function GetAdvertiserAccountInfoList() As ClsAccountInfoList
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

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            ' fetch with no filter
            Fetch()
        End Sub

        Private Sub Fetch()
            Fetch(DAClsappAdvertiserAccount.Fetch())
        End Sub

        Private Sub Fetch(ByVal Accounts As DAClsappAdvertiserAccount.Struct())
            RaiseListChangedEvents = False
            IsReadOnly = False
            For Each Struct As DAClsappAdvertiserAccount.Struct In Accounts
                Me.Add(ClsAccountInfo.GetAccountInfo(Struct))
            Next
            IsReadOnly = True
            RaiseListChangedEvents = True
        End Sub

#End Region

    End Class
End Namespace