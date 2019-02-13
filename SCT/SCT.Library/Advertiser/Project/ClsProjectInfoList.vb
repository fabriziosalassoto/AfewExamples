Imports Csla
Imports SCT.DataAccess

Namespace Advertiser
    <Serializable()> Public Class ClsProjectInfoList
        Inherits ReadOnlyListBase(Of ClsProjectInfoList, ClsProjectInfo)

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole("")
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function GetProjectInfoList() As ClsProjectInfoList
            Return DataPortal.Fetch(Of ClsProjectInfoList)(New Criteria)
        End Function

        Public Shared Function GetProjectInfoList(ByVal idAdvertiser As Long, ByVal idAdvertiserContact As Long) As ClsProjectInfoList
            Return DataPortal.Fetch(Of ClsProjectInfoList)(New FilteredCriteria(idAdvertiser, idAdvertiserContact))
        End Function

        Public Shared Function GetProjectInfoList(ByVal idAdvertiser() As Long, ByVal idAdvertiserContact() As Long) As ClsProjectInfoList
            Return DataPortal.Fetch(Of ClsProjectInfoList)(New FilteredCriteriaList(idAdvertiser, idAdvertiserContact))
        End Function

#End Region

#Region " Data Access "

        <Serializable()> Private Class Criteria
            ' no criteria - retrieve all projects
        End Class

        <Serializable()> Private Class FilteredCriteria

            Private mIDAdvertiser As Long
            Private mIDAdvertiserContact As Long

            Public ReadOnly Property IDAdvertiser() As Long
                Get
                    Return Me.mIDAdvertiser
                End Get
            End Property

            Public ReadOnly Property IDAdvertiserContact() As Long
                Get
                    Return Me.mIDAdvertiserContact
                End Get
            End Property

            Public Sub New(ByVal idAdvertiser As Long, ByVal idAdvertiserContact As Long)
                Me.mIDAdvertiser = idAdvertiser
                Me.mIDAdvertiserContact = idAdvertiserContact
            End Sub

        End Class

        <Serializable()> Private Class FilteredCriteriaList

            Private mIDAdvertiser() As Long
            Private mIDAdvertiserContact() As Long

            Public ReadOnly Property IDAdvertiser() As Long()
                Get
                    Return Me.mIDAdvertiser
                End Get
            End Property

            Public ReadOnly Property IDAdvertiserContact() As Long()
                Get
                    Return Me.mIDAdvertiserContact
                End Get
            End Property

            Public Sub New(ByVal idAdvertiser() As Long, ByVal idAdvertiserContact() As Long)
                Me.mIDAdvertiser = idAdvertiser
                Me.mIDAdvertiserContact = idAdvertiserContact
            End Sub

        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            ' fetch with no filter
            Fetch()
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteria)
            Fetch(criteria.IDAdvertiser, criteria.IDAdvertiserContact)
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteriaList)
            Fetch(criteria.IDAdvertiser, criteria.IDAdvertiserContact)
        End Sub

        Private Sub Fetch()
            Fetch(DAClsappAdvertiserProjects.Fetch())
        End Sub

        Private Sub Fetch(ByVal idAdvertiser As Long, ByVal idAdvertiserContact As Long)
            Fetch(DAClsappAdvertiserProjects.Fetch(idAdvertiser, idAdvertiserContact))
        End Sub

        Private Sub Fetch(ByVal idAdvertiser() As Long, ByVal idAdvertiserContact() As Long)
            Fetch(DAClsappAdvertiserProjects.Fetch(idAdvertiser, idAdvertiserContact))
        End Sub

        Private Sub Fetch(ByVal projects As DAClsappAdvertiserProjects.Struct())
            RaiseListChangedEvents = False
            IsReadOnly = False
            For Each Struct As DAClsappAdvertiserProjects.Struct In projects
                Me.Add(ClsProjectInfo.GetProjectInfo(Struct))
            Next
            IsReadOnly = True
            RaiseListChangedEvents = True
        End Sub

#End Region

    End Class
End Namespace