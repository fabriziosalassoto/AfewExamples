Imports Csla
Imports SCT.DataAccess

Namespace Subscriber
    <Serializable()> Public Class ClsDemographicList
        Inherits BusinessListBase(Of ClsDemographicList, ClsDemographic)

#Region " Business Methods "

        Public Function GetItem(ByVal demographicId As Long) As ClsDemographic
            For Each demographic As ClsDemographic In Me
                If demographic.ID = demographicId Then
                    Return demographic
                End If
            Next
            Return Nothing
        End Function

        Public Function GetItem(ByVal tag As String) As ClsDemographic
            For Each demographic As ClsDemographic In Me
                If demographic.Tag = tag Then
                    Return demographic
                End If
            Next
            Return Nothing
        End Function

        Public Overloads Sub Add(ByVal tag As String, ByVal answer As String)
            Add(ClsDemographic.NewSubscriberDemographic(tag, answer))
        End Sub

        Public Overloads Sub Remove(ByVal demographicId As Long)
            For Each demographic As ClsDemographic In Me
                If demographic.ID = demographicId Then
                    Remove(demographic)
                    Exit For
                End If
            Next
        End Sub

        Public Overloads Function Contains(ByVal demographicId As Long) As Boolean
            For Each demographic As ClsDemographic In Me
                If demographic.ID = demographicId Then
                    Return True
                End If
            Next
            Return False
        End Function

        Public Overloads Function Contains(ByVal tag As String) As Boolean
            For Each demographic As ClsDemographic In Me
                If demographic.Tag = tag Then
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

        Public Shared Function NewDemographicList() As ClsDemographicList
            Return DataPortal.Create(Of ClsDemographicList)(New Criteria())
        End Function

        Public Shared Function GetDemographicList() As ClsDemographicList
            Return DataPortal.Fetch(Of ClsDemographicList)(New Criteria())
        End Function

        Public Shared Function GetDemographicList(ByVal subscriberID As Long, ByVal tag As String) As ClsDemographicList
            Return DataPortal.Fetch(Of ClsDemographicList)(New FilteredCriteria(subscriberID, tag))
        End Function

        Public Shared Function GetDemographicList(ByVal subscriberID() As Long, ByVal tag() As String) As ClsDemographicList
            Return DataPortal.Fetch(Of ClsDemographicList)(New FilteredCriteriaList(subscriberID, tag))
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Child Methods "

        Friend Shared Function NewSubscriberDemographics() As ClsDemographicList
            Dim ChildList As New ClsDemographicList
            ChildList.MarkAsChild()
            Return ChildList
        End Function

        Friend Shared Function GetSubscriberDemographics(ByVal list As DAClsappSubscribersDemographics.Struct()) As ClsDemographicList
            Return New ClsDemographicList(list)
        End Function

        Private Sub New(ByVal list As DAClsappSubscribersDemographics.Struct())
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

            Private mIDSubscriber As Long
            Private mTag As String

            Public ReadOnly Property IDSubscriber() As Long
                Get
                    Return Me.mIDSubscriber
                End Get
            End Property

            Public ReadOnly Property Tag() As String
                Get
                    Return Me.mTag
                End Get
            End Property

            Public Sub New(ByVal subscriberId As Long, ByVal tag As String)
                Me.mIDSubscriber = subscriberId
                Me.mTag = tag
            End Sub

        End Class

        <Serializable()> Private Class FilteredCriteriaList

            Private mIDSubscriber() As Long
            Private mTag() As String

            Public ReadOnly Property IDSubscriber() As Long()
                Get
                    Return Me.mIDSubscriber
                End Get
            End Property

            Public ReadOnly Property Tag() As String()
                Get
                    Return Me.mTag
                End Get
            End Property

            Public Sub New(ByVal subscriberId() As Long, ByVal tag() As String)
                Me.mIDSubscriber = subscriberId
                Me.mTag = tag
            End Sub

        End Class

        <RunLocal()> Private Overloads Sub DataPortal_Create(ByVal criteria As Criteria)
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            ' fetch with no filter
            Fetch()
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteria)
            Fetch(criteria.IDSubscriber, criteria.Tag)
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteriaList)
            Fetch(criteria.IDSubscriber, criteria.Tag)
        End Sub

        Protected Overrides Sub DataPortal_Update()
            RaiseListChangedEvents = False
            For Each item As ClsDemographic In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsDemographic In Me
                If item.IsNew Then
                    item.Insert(item.SubscriberAccount)
                Else
                    item.Update(item.SubscriberAccount)
                End If
            Next
            RaiseListChangedEvents = True
        End Sub

#End Region

#Region " Child Area "

        Friend Sub Update(ByVal parent As Object)
            RaiseListChangedEvents = False
            For Each item As ClsDemographic In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsDemographic In Me
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
            Fetch(DAClsappSubscribersDemographics.Fetch())
        End Sub

        Private Sub Fetch(ByVal subscriberIdFilter As Long, ByVal tagFilter As String)
            Fetch(DAClsappSubscribersDemographics.Fetch(subscriberIdFilter, tagFilter))
        End Sub

        Private Sub Fetch(ByVal subscriberIdFilter() As Long, ByVal tagFilter() As String)
            Fetch(DAClsappSubscribersDemographics.Fetch(subscriberIdFilter, tagFilter))
        End Sub

        Private Sub Fetch(ByVal list As DAClsappSubscribersDemographics.Struct())
            RaiseListChangedEvents = False
            For Each struct As DAClsappSubscribersDemographics.Struct In list
                Add(ClsDemographic.GetSubscriberDemographic(struct))
            Next
            RaiseListChangedEvents = True
        End Sub

#End Region

#End Region

    End Class
End Namespace