Imports Csla
Imports SCT.DataAccess

Namespace Advertiser.Project
    <Serializable()> Public Class ClsPriceList
        Inherits BusinessListBase(Of ClsPriceList, ClsPrice)

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

#Region " Root Methods "

        Public Shared Function NewPriceList() As ClsPriceList
            Return DataPortal.Create(Of ClsPriceList)(New Criteria())
        End Function

        Public Shared Function GetPriceList() As ClsPriceList
            Return DataPortal.Fetch(Of ClsPriceList)(New Criteria())
        End Function

        Public Shared Function GetPriceList(ByVal projectID As Long) As ClsPriceList
            Return DataPortal.Fetch(Of ClsPriceList)(New FilteredCriteria(projectID))
        End Function

        Public Shared Function GetPriceList(ByVal projectID() As Long) As ClsPriceList
            Return DataPortal.Fetch(Of ClsPriceList)(New FilteredCriteriaList(projectID))
        End Function

#End Region

#Region " Child Methods "

        Friend Shared Function NewProjectPrices() As ClsPriceList
            Return New ClsPriceList
        End Function

        Friend Shared Function GetProjectPrices(ByVal list As DAClsappAdvertiserProjectPriceInfo.Struct()) As ClsPriceList
            Return New ClsPriceList(list)
        End Function

        Private Sub New()
            MarkAsChild()
        End Sub

        Private Sub New(ByVal list As DAClsappAdvertiserProjectPriceInfo.Struct())
            MarkAsChild()
            Fetch(list)
        End Sub

#End Region

#End Region

#Region " Data Access "

#Region " Root Area "

        <Serializable()> Private Class Criteria
            ' no criteria - retrieve all projects
        End Class

        <Serializable()> Private Class FilteredCriteria

            Private mIDProject As Long

            Public ReadOnly Property IDProject() As Long
                Get
                    Return Me.mIDProject
                End Get
            End Property

            Public Sub New(ByVal projectId As Long)
                Me.mIDProject = projectId
            End Sub

        End Class

        <Serializable()> Private Class FilteredCriteriaList

            Private mIDProject() As Long

            Public ReadOnly Property IDProject() As Long()
                Get
                    Return Me.mIDProject
                End Get
            End Property

            Public Sub New(ByVal projectId() As Long)
                Me.mIDProject = projectId
            End Sub

        End Class

        <RunLocal()> Private Overloads Sub DataPortal_Create(ByVal criteria As Criteria)
        End Sub

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

        Protected Overrides Sub DataPortal_Update()
            RaiseListChangedEvents = False
            For Each item As ClsPrice In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsPrice In Me
                If item.IsNew Then
                    item.Insert(item.Project)
                Else
                    item.Update(item.Project)
                End If
            Next
            RaiseListChangedEvents = True
        End Sub

#End Region

#Region " Child Area "

        Friend Sub Update(ByVal parent As Object)
            RaiseListChangedEvents = False
            For Each item As ClsPrice In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsPrice In Me
                If item.IsNew Then
                    item.Insert(parent)
                Else
                    item.Update(parent)
                End If
            Next
            RaiseListChangedEvents = True
        End Sub

#End Region

#Region " Common Area "

        Private Sub Fetch()
            Fetch(DAClsappAdvertiserProjectPriceInfo.Fetch())
        End Sub

        Private Sub Fetch(ByVal projectID As Long)
            Fetch(DAClsappAdvertiserProjectPriceInfo.FetchByProject(projectID))
        End Sub

        Private Sub Fetch(ByVal projectID() As Long)
            Fetch(DAClsappAdvertiserProjectPriceInfo.FetchByProject(projectID))
        End Sub

        Private Sub Fetch(ByVal list As DAClsappAdvertiserProjectPriceInfo.Struct())
            RaiseListChangedEvents = False
            For Each Struct As DAClsappAdvertiserProjectPriceInfo.Struct In list
                Add(ClsPrice.GetChildPrice(Struct))
            Next
            RaiseListChangedEvents = True
        End Sub

#End Region

#End Region

    End Class
End Namespace