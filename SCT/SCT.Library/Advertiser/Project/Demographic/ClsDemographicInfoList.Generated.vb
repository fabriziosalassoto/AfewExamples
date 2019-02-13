Imports Csla
Imports SCT.DataAccess

Namespace Advertiser.Project
    <Serializable()> Public Class ClsDemographicInfoList
        Inherits ReadOnlyListBase(Of ClsDemographicInfoList, ClsDemographicInfo)

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole(" ")
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function NewDemographicInfoList() As ClsDemographicInfoList
            Return New ClsDemographicInfoList
        End Function

        Public Shared Function GetDemographicInfoList() As ClsDemographicInfoList
            Return DataPortal.Fetch(Of ClsDemographicInfoList)(New Criteria)
        End Function

        Public Shared Function GetDemographicInfoList(ByVal idProject As Long, ByVal demographicTag As String) As ClsDemographicInfoList
            Return DataPortal.Fetch(Of ClsDemographicInfoList)(New FilteredCriteria(idProject, demographicTag))
        End Function

        Public Shared Function GetDemographicInfoList(ByVal idProject() As Long, ByVal demographicTag() As String) As ClsDemographicInfoList
            Return DataPortal.Fetch(Of ClsDemographicInfoList)(New FilteredCriteriaList(idProject, demographicTag))
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> Private Class Criteria
            ' no criteria - retrieve all records
        End Class

        <Serializable()> Private Class FilteredCriteria

            Private mIDProject As Long
            Private mDemographicTag As String

            Public ReadOnly Property IDProject() As Long
                Get
                    Return Me.mIDProject
                End Get
            End Property

            Public ReadOnly Property DemographicTag() As String
                Get
                    Return Me.mDemographicTag
                End Get
            End Property

            Public Sub New(ByVal idProject As Long, ByVal demographicTag As String)
                Me.mIDProject = idProject
                Me.mDemographicTag = demographicTag
            End Sub

        End Class

        <Serializable()> Private Class FilteredCriteriaList

            Private mIDProject() As Long
            Private mDemographicTag() As String

            Public ReadOnly Property IDProject() As Long()
                Get
                    Return Me.mIDProject
                End Get
            End Property

            Public ReadOnly Property DemographicTag() As String()
                Get
                    Return Me.mDemographicTag
                End Get
            End Property

            Public Sub New(ByVal idProject() As Long, ByVal demographicTag() As String)
                Me.mIDProject = idProject
                Me.mDemographicTag = demographicTag
            End Sub

        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            ' fetch with no filter
            Fetch()
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteria)
            Fetch(criteria.IDProject, criteria.DemographicTag)
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteriaList)
            Fetch(criteria.IDProject, criteria.DemographicTag)
        End Sub

        Private Sub Fetch()
            Fetch(DAClsappAdvertiserDemographics.Fetch())
        End Sub

        Private Sub Fetch(ByVal idProject As Long, ByVal DemographicTag As String)
            Fetch(DAClsappAdvertiserDemographics.Fetch(idProject, DemographicTag))
        End Sub

        Private Sub Fetch(ByVal idProject() As Long, ByVal DemographicTag() As String)
            Fetch(DAClsappAdvertiserDemographics.Fetch(idProject, DemographicTag))
        End Sub

        Private Sub Fetch(ByVal list As DAClsappAdvertiserDemographics.Struct())
            RaiseListChangedEvents = False
            IsReadOnly = False
            For Each Struct As DAClsappAdvertiserDemographics.Struct In list
                Me.Add(ClsDemographicInfo.GetDemographicInfo(Struct))
            Next
            IsReadOnly = True
            RaiseListChangedEvents = True
        End Sub

#End Region

    End Class
End Namespace

