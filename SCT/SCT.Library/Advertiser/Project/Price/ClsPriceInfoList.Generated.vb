Imports Csla
Imports SCT.DataAccess

Namespace Advertiser.Project
    <Serializable()> Public Class ClsPriceInfoList
        Inherits ReadOnlyListBase(Of ClsPriceInfoList, ClsPriceInfo)

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole(" ")
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function NewPriceInfoList() As ClsPriceInfoList
            Return New ClsPriceInfoList
        End Function

        Public Shared Function GetPriceInfoList() As ClsPriceInfoList
            Return DataPortal.Fetch(Of ClsPriceInfoList)(New Criteria)
        End Function

        Public Shared Function GetPriceInfoList(ByVal idProject As Long) As ClsPriceInfoList
            Return DataPortal.Fetch(Of ClsPriceInfoList)(New FilteredCriteria(idProject))
        End Function

        Public Shared Function GetPriceInfoList(ByVal idProject() As Long) As ClsPriceInfoList
            Return DataPortal.Fetch(Of ClsPriceInfoList)(New FilteredCriteriaList(idProject))
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

            Public ReadOnly Property IDProject() As Long
                Get
                    Return Me.mIDProject
                End Get
            End Property

            Public Sub New(ByVal idProject As Long)
                Me.mIDProject = idProject
            End Sub

        End Class

        <Serializable()> Private Class FilteredCriteriaList

            Private mIDProject() As Long

            Public ReadOnly Property IDProject() As Long()
                Get
                    Return Me.mIDProject
                End Get
            End Property

            Public Sub New(ByVal idProject() As Long)
                Me.mIDProject = idProject
            End Sub

        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            ' fetch with no filter
            Fetch()
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteria)
            Fetch(criteria.IDProject)
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteriaList)
            Fetch(criteria.IDProject)
        End Sub

        Private Sub Fetch()
            Fetch(DAClsappAdvertiserProjectPriceInfo.Fetch())
        End Sub

        Private Sub Fetch(ByVal idProject As Long)
            Fetch(DAClsappAdvertiserProjectPriceInfo.FetchByProject(idProject))
        End Sub

        Private Sub Fetch(ByVal idProject() As Long)
            Fetch(DAClsappAdvertiserProjectPriceInfo.FetchByProject(idProject))
        End Sub

        Private Sub Fetch(ByVal list As DAClsappAdvertiserProjectPriceInfo.Struct())
            RaiseListChangedEvents = False
            IsReadOnly = False
            For Each Struct As DAClsappAdvertiserProjectPriceInfo.Struct In list
                Me.Add(ClsPriceInfo.GetPriceInfo(Struct))
            Next
            IsReadOnly = True
            RaiseListChangedEvents = True
        End Sub

#End Region

    End Class
End Namespace

