Imports Csla
Imports SCT.DataAccess

Namespace Advertiser
    <Serializable()> Public Class ClsBinnacleList
        Inherits BusinessListBase(Of ClsBinnacleList, ClsBinnacle)

#Region " Business Methods "

        Public Function GetItem(ByVal Id As Long) As ClsBinnacle
            For Each item As ClsBinnacle In Me
                If item.ID = Id Then
                    Return item
                End If
            Next
            Return Nothing
        End Function

        Public Overloads Sub Remove(ByVal Id As Long)
            For Each item As ClsBinnacle In Me
                If item.ID = Id Then
                    Remove(item)
                    Exit For
                End If
            Next
        End Sub

        Public Overloads Function Contains(ByVal Id As Long) As Boolean
            For Each item As ClsBinnacle In Me
                If item.ID = Id Then
                    Return True
                End If
            Next
            Return False
        End Function

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

#Region " Root Methods "

        Public Shared Function NewBinnacleList() As ClsBinnacleList
            Return DataPortal.Create(Of ClsBinnacleList)(New Criteria())
        End Function

        Public Shared Function GetBinnacleList() As ClsBinnacleList
            Return DataPortal.Fetch(Of ClsBinnacleList)(New Criteria())
        End Function

        Public Shared Function GetBinnacleList(ByVal idUser As SearchCriteria(Of Long), ByVal fromDate As SearchCriteria(Of Date), ByVal toDate As SearchCriteria(Of Date)) As ClsBinnacleList
            Return DataPortal.Fetch(Of ClsBinnacleList)(New FilteredCriteria(idUser, fromDate, toDate))
        End Function

        Public Shared Function GetBinnacleList(ByVal idUser As SearchCriteriaList(Of Long), ByVal fromDate As SearchCriteria(Of Date), ByVal toDate As SearchCriteria(Of Date)) As ClsBinnacleList
            Return DataPortal.Fetch(Of ClsBinnacleList)(New FilteredCriteriaList(idUser, fromDate, toDate))
        End Function

#End Region

#Region " Child Methods "

        Friend Shared Function NewChildBinnacleList() As ClsBinnacleList
            Return New ClsBinnacleList
        End Function

        Friend Shared Function GetChildBinnacleList(ByVal list As DAClsprgAdvertiserBinnacle.Struct()) As ClsBinnacleList
            Return New ClsBinnacleList(list)
        End Function

        Private Sub New()
            MarkAsChild()
        End Sub

        Private Sub New(ByVal list As DAClsprgAdvertiserBinnacle.Struct())
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

            Private mIDUser As SearchCriteria(Of Long)
            Private mFromDate As SearchCriteria(Of Date)
            Private mToDate As SearchCriteria(Of Date)

            Public ReadOnly Property IDUser() As SearchCriteria(Of Long)
                Get
                    Return Me.mIDUser
                End Get
            End Property

            Public ReadOnly Property FromDate() As SearchCriteria(Of Date)
                Get
                    Return Me.mFromDate
                End Get
            End Property

            Public ReadOnly Property ToDate() As SearchCriteria(Of Date)
                Get
                    Return Me.mToDate
                End Get
            End Property

            Public Sub New(ByVal idUser As SearchCriteria(Of Long), ByVal fromDate As SearchCriteria(Of Date), ByVal toDate As SearchCriteria(Of Date))
                Me.mIDUser = idUser
                Me.mFromDate = fromDate
                Me.mToDate = toDate
            End Sub

        End Class

        <Serializable()> Private Class FilteredCriteriaList

            Private mIDUser As SearchCriteriaList(Of Long)
            Private mFromDate As SearchCriteria(Of Date)
            Private mToDate As SearchCriteria(Of Date)

            Public ReadOnly Property IDUser() As SearchCriteriaList(Of Long)
                Get
                    Return Me.mIDUser
                End Get
            End Property

            Public ReadOnly Property FromDate() As SearchCriteria(Of Date)
                Get
                    Return Me.mFromDate
                End Get
            End Property

            Public ReadOnly Property ToDate() As SearchCriteria(Of Date)
                Get
                    Return Me.mToDate
                End Get
            End Property

            Public Sub New(ByVal idUser As SearchCriteriaList(Of Long), ByVal fromDate As SearchCriteria(Of Date), ByVal toDate As SearchCriteria(Of Date))
                Me.mIDUser = idUser
                Me.mFromDate = fromDate
                Me.mToDate = toDate
            End Sub

        End Class

        <RunLocal()> Private Overloads Sub DataPortal_Create(ByVal criteria As Criteria)
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            ' fetch with no filter
            Fetch()
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteria)
            Fetch(criteria.IDUser, criteria.FromDate, criteria.ToDate)
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteriaList)
            Fetch(criteria.IDUser, criteria.FromDate, criteria.ToDate)
        End Sub

        Protected Overrides Sub DataPortal_Update()
            RaiseListChangedEvents = False
            For Each item As ClsBinnacle In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsBinnacle In Me
                If item.IsNew Then
                    item.Insert(New Object() {item.User})
                Else
                    item.Update(New Object() {item.User})
                End If
            Next
            RaiseListChangedEvents = True
        End Sub

#End Region

#Region " Child Area "

        Friend Sub Update(ByVal parent As Object)
            RaiseListChangedEvents = False
            For Each item As ClsBinnacle In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsBinnacle In Me
                If item.IsNew Then
                    item.Insert(New Object() {parent})
                Else
                    item.Update(New Object() {parent})
                End If
            Next
            RaiseListChangedEvents = True
        End Sub

#End Region

#Region " Common Area "

        Private Sub Fetch()
            Fetch(DAClsprgAdvertiserBinnacle.Fetch())
        End Sub

        Private Sub Fetch(ByVal idUser As SearchCriteria(Of Long), ByVal fromDate As SearchCriteria(Of Date), ByVal toDate As SearchCriteria(Of Date))
            Fetch(DAClsprgAdvertiserBinnacle.Fetch())
        End Sub

        Private Sub Fetch(ByVal idUser As SearchCriteriaList(Of Long), ByVal fromDate As SearchCriteria(Of Date), ByVal toDate As SearchCriteria(Of Date))
            Fetch(DAClsprgAdvertiserBinnacle.Fetch())
        End Sub

        Private Sub Fetch(ByVal list As DAClsprgAdvertiserBinnacle.Struct())
            Me.RaiseListChangedEvents = False
            For Each Struct As DAClsprgAdvertiserBinnacle.Struct In list
                Me.Add(ClsBinnacle.GetChildBinnacle(Struct))
            Next
            Me.RaiseListChangedEvents = True
        End Sub

#End Region

#End Region

#Region " Exists "

        Public Shared Function Exists(ByVal idUser As Long) As Boolean
            Return DataPortal.Execute(Of ExistsCommand)(New ExistsCommand(idUser)).Exists
        End Function

        <Serializable()> Private Class ExistsCommand
            Inherits CommandBase

            Private mIDUser As Long
            Private mExists As Boolean

            Public ReadOnly Property Exists() As Boolean
                Get
                    Return Me.mExists
                End Get
            End Property

            Public Sub New(ByVal idUser As Long)
                Me.mIDUser = idUser
            End Sub

            Protected Overrides Sub DataPortal_Execute()
                Me.mExists = DAClsprgAdvertiserBinnacle.FetchByUser(New Parameter(Of Long)(Me.mIDUser, 0)).Length > 0
            End Sub

        End Class

#End Region

    End Class
End Namespace
