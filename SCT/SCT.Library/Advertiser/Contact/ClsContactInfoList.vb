Imports Csla
Imports SCT.DataAccess

Namespace Advertiser
    <Serializable()> Public Class ClsContactInfoList
        Inherits ReadOnlyListBase(Of ClsContactInfoList, ClsContactInfo)

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole("")
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function GetContactInfoList() As ClsContactInfoList
            Return DataPortal.Fetch(Of ClsContactInfoList)(New Criteria)
        End Function

        Public Shared Function GetContactInfoList(ByVal adAccountId As Long, ByVal mainCompanyAddress As Boolean) As ClsContactInfoList
            Return DataPortal.Fetch(Of ClsContactInfoList)(New FilteredCriteria(adAccountId, mainCompanyAddress))
        End Function

        Public Shared Function GetContactInfoList(ByVal adAccountId() As Long, ByVal mainCompanyAddress As Boolean) As ClsContactInfoList
            Return DataPortal.Fetch(Of ClsContactInfoList)(New FilteredCriteriaList(adAccountId, mainCompanyAddress))
        End Function

#End Region

#Region " Data Access "

        <Serializable()> Private Class Criteria
            ' no criteria - retrieve all projects
        End Class

        <Serializable()> Private Class FilteredCriteria

            Private mAdAccountID As Long
            Private mMainCompanyAddress As Boolean

            Public ReadOnly Property AdAccountID() As Long
                Get
                    Return Me.mAdAccountID
                End Get
            End Property

            Public ReadOnly Property MainCompanyAddress() As Boolean
                Get
                    Return Me.mMainCompanyAddress
                End Get
            End Property

            Public Sub New(ByVal adAccountId As Long, ByVal mainCompanyAddress As Boolean)
                Me.mAdAccountID = adAccountId
                Me.mMainCompanyAddress = mainCompanyAddress
            End Sub

        End Class

        <Serializable()> Private Class FilteredCriteriaList

            Private mAdAccountID() As Long
            Private mMainCompanyAddress As Boolean

            Public ReadOnly Property AdAccountID() As Long()
                Get
                    Return Me.mAdAccountID
                End Get
            End Property

            Public ReadOnly Property MainCompanyAddress() As Boolean
                Get
                    Return Me.mMainCompanyAddress
                End Get
            End Property

            Public Sub New(ByVal adAccountId As Long(), ByVal mainCompanyAddress As Boolean)
                Me.mAdAccountID = adAccountId
                Me.mMainCompanyAddress = mainCompanyAddress
            End Sub

        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            ' fetch with no filter
            Fetch()
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteria)
            Fetch(criteria.AdAccountID, criteria.MainCompanyAddress)
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteriaList)
            Fetch(criteria.AdAccountID, criteria.MainCompanyAddress)
        End Sub

        Private Sub Fetch()
            Fetch(DAClsappAdvertiserContactInfo.Fetch())
        End Sub

        Private Sub Fetch(ByVal adAccountId As Long, ByVal mainCompanyAddress As Boolean)
            Fetch(DAClsappAdvertiserContactInfo.Fetch(adAccountId, mainCompanyAddress))
        End Sub

        Private Sub Fetch(ByVal adAccountId() As Long, ByVal mainCompanyAddress As Boolean)
            Fetch(DAClsappAdvertiserContactInfo.Fetch(adAccountId, mainCompanyAddress))
        End Sub

        Private Sub Fetch(ByVal contacts As DAClsappAdvertiserContactInfo.Struct())
            RaiseListChangedEvents = False
            IsReadOnly = False
            For Each Struct As DAClsappAdvertiserContactInfo.Struct In contacts
                Me.Add(ClsContactInfo.GetContactInfo(Struct))
            Next
            IsReadOnly = True
            RaiseListChangedEvents = True
        End Sub

#End Region

    End Class
End Namespace