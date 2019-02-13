Imports Csla
Imports SCT.DataAccess
Imports Microsoft.VisualBasic

Namespace Advertiser
    <Serializable()> Public Class ClsProjectList
        Inherits BusinessListBase(Of ClsProjectList, ClsProject)

#Region " Business Methods "

        Public Function GetItem(ByVal projectId As Long) As ClsProject
            For Each project As ClsProject In Me
                If project.ID = projectId Then
                    Return project
                End If
            Next
            Return Nothing
        End Function

        Public Overloads Sub Remove(ByVal projectId As Long)
            For Each project As ClsProject In Me
                If project.ID = projectId Then
                    Remove(project)
                    Exit For
                End If
            Next
        End Sub

        Public Overloads Function Contains(ByVal projectId As Long) As Boolean
            For Each project As ClsProject In Me
                If project.ID = projectId Then
                    Return True
                End If
            Next
            Return False
        End Function

        Public Function FindAllAdvertiserProjects(ByVal contactId As Long, ByVal description As String, ByVal adUrl As String) As ClsProjectList
            Dim projectList As ClsProjectList = ClsProjectList.NewAdvertiserProjects
            For Each project As ClsProject In Me
                If (contactId = 0 OrElse project.AdvertiserContact.ID = contactId) AndAlso (Len(description) = 0 OrElse project.ProjectDescription.ToUpper.IndexOf(description.ToUpper) > 0) AndAlso (Len(adUrl) = 0 OrElse project.ADUrl.ToUpper.IndexOf(adUrl.ToUpper) > 0) Then
                    projectList.Add(project)
                End If
            Next
            Return projectList
        End Function

        Public Function FindAllSubscriberProjects(ByVal subDemographics As Subscriber.ClsDemographicList) As ClsProjectList
            Dim projectList As ClsProjectList = ClsProjectList.NewAdvertiserProjects
            For Each project As ClsProject In Me
                If Me.IsForSubscriber(project.Demographics, subDemographics) Then
                    projectList.Add(project)
                End If
            Next
            Return projectList
        End Function

        Public Function FindAllActiveProjects(ByVal subTime As Date) As ClsProjectList
            Dim projectList As ClsProjectList = ClsProjectList.NewAdvertiserProjects
            For Each project As ClsProject In Me
                With project
                    If (.RunStartDate.Date <> "1900-01-01" AndAlso Date.Today.Date >= .RunStartDate.Date) AndAlso _
                       (.RunEndDate.Date = "1900-01-01" OrElse Date.Today.Date <= .RunEndDate.Date) AndAlso _
                       (.MaxDisplays = 0 OrElse .AdHistories.FindAllAdHistory(.RunStartDate, Date.Today).Count < .MaxDisplays) AndAlso _
                       (.MaxPerDay = 0 OrElse .AdHistories.FindAllAdHistory(Date.Today, Date.Today).Count < .MaxPerDay) AndAlso _
                       (Format(.StartTimeBasedOnSubscribersTime, "HH:mm") = "00:01" OrElse subTime.TimeOfDay >= .StartTimeBasedOnSubscribersTime.TimeOfDay) AndAlso _
                       (Format(.EndTimeBasedOnSubscribersTime, "HH:mm") = "23:59" OrElse subTime.TimeOfDay <= .EndTimeBasedOnSubscribersTime.TimeOfDay) Then
                        projectList.Add(project)
                    End If
                End With
            Next
            Return projectList
        End Function

        Public Function GetNextProjectToShow(ByVal lastAdHistory As Project.ClsAdHistory) As ClsProject
            If Me.Count > 0 Then
                For Each project As ClsProject In Me
                    If lastAdHistory Is Nothing OrElse project.ID > lastAdHistory.Project.ID Then
                        Return project
                    End If
                Next
                Return Me.Item(0)
            Else
                Return Nothing
            End If
        End Function

        Private Function IsForSubscriber(ByVal projectDemographics As Project.ClsDemographicList, ByVal subscriberDemographics As Subscriber.ClsDemographicList) As Boolean
            For Each projectDemographic As Project.ClsDemographic In projectDemographics
                If (projectDemographic.Tag = "MinAge" AndAlso IsNumeric(projectDemographic.Requirement) AndAlso subscriberDemographics.Contains("Age") AndAlso IsNumeric(subscriberDemographics.GetItem("Age").Answer) AndAlso subscriberDemographics.GetItem("Age").Answer < projectDemographic.Requirement) OrElse _
                   (projectDemographic.Tag = "MaxAge" AndAlso IsNumeric(projectDemographic.Requirement) AndAlso subscriberDemographics.Contains("Age") AndAlso IsNumeric(subscriberDemographics.GetItem("Age").Answer) AndAlso subscriberDemographics.GetItem("Age").Answer > projectDemographic.Requirement) OrElse _
                   (projectDemographic.Requirement <> String.Empty AndAlso subscriberDemographics.Contains(projectDemographic.Tag) AndAlso subscriberDemographics.GetItem(projectDemographic.Tag).Answer <> String.Empty AndAlso subscriberDemographics.GetItem(projectDemographic.Tag).Answer <> projectDemographic.Requirement) Then
                    Return False
                End If
            Next
            Return True
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

        Public Shared Function NewProjectList() As ClsProjectList
            Return DataPortal.Create(Of ClsProjectList)(New Criteria())
        End Function

        Public Shared Function GetProjectList() As ClsProjectList
            Return DataPortal.Fetch(Of ClsProjectList)(New Criteria())
        End Function

        Public Shared Function GetProjectList(ByVal advertiserID As Long, ByVal contactId As Long) As ClsProjectList
            Return DataPortal.Fetch(Of ClsProjectList)(New FilteredCriteria(advertiserID, contactId))
        End Function

        Public Shared Function GetProjectList(ByVal advertiserID() As Long, ByVal contactId() As Long) As ClsProjectList
            Return DataPortal.Fetch(Of ClsProjectList)(New FilteredCriteriaList(advertiserID, contactId))
        End Function

#End Region

#Region " Child Methods "

        Friend Shared Function NewAdvertiserProjects() As ClsProjectList
            Return New ClsProjectList
        End Function

        Friend Shared Function GetAdvertiserProjects(ByVal list As DAClsappAdvertiserProjects.Struct()) As ClsProjectList
            Return New ClsProjectList(list)
        End Function

        Friend Shared Function NewContactProjects() As ClsProjectList
            Return New ClsProjectList
        End Function

        Friend Shared Function GetContactProjects(ByVal list As DAClsappAdvertiserProjects.Struct()) As ClsProjectList
            Return New ClsProjectList(list)
        End Function

        Private Sub New()
            MarkAsChild()
        End Sub

        Private Sub New(ByVal list As DAClsappAdvertiserProjects.Struct())
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

            Private mIDAdvertiser As Long
            Private mIDContact As Long

            Public ReadOnly Property IDAdvertiser() As Long
                Get
                    Return Me.mIDAdvertiser
                End Get
            End Property

            Public ReadOnly Property IDContact() As Long
                Get
                    Return Me.mIDContact
                End Get
            End Property

            Public Sub New(ByVal advertiserId As Long, ByVal contactId As Long)
                Me.mIDAdvertiser = advertiserId
                Me.mIDContact = contactId
            End Sub

        End Class

        <Serializable()> Private Class FilteredCriteriaList

            Private mIDAdvertiser() As Long
            Private mIDContact() As Long

            Public ReadOnly Property IDAdvertiser() As Long()
                Get
                    Return Me.mIDAdvertiser
                End Get
            End Property

            Public ReadOnly Property IDContact() As Long()
                Get
                    Return Me.mIDContact
                End Get
            End Property

            Public Sub New(ByVal advertiserId() As Long, ByVal contactId() As Long)
                Me.mIDAdvertiser = advertiserId
                Me.mIDContact = contactId
            End Sub

        End Class

        <RunLocal()> Private Overloads Sub DataPortal_Create(ByVal criteria As Criteria)
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            ' fetch with no filter
            Fetch()
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteria)
            Fetch(criteria.IDAdvertiser, criteria.IDContact)
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteriaList)
            Fetch(criteria.IDAdvertiser, criteria.IDContact)
        End Sub

        Protected Overrides Sub DataPortal_Update()
            RaiseListChangedEvents = False
            For Each item As ClsProject In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsProject In Me
                If item.IsNew Then
                    item.Insert(New Object() {item.AdvertiserAccount, item.AdvertiserContact})
                Else
                    item.Update(New Object() {item.AdvertiserAccount, item.AdvertiserContact})
                End If
            Next
            RaiseListChangedEvents = True
        End Sub

#End Region

#Region " Child Area "

        Friend Sub Update(ByVal parent As Object)
            RaiseListChangedEvents = False
            For Each item As ClsProject In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsProject In Me
                If item.IsNew Then
                    item.Insert(New Object() {parent, item.AdvertiserContact})
                Else
                    item.Update(New Object() {parent, item.AdvertiserContact})
                End If
            Next
            RaiseListChangedEvents = True
        End Sub

        Friend Sub Update(ByVal parents() As Object)
            RaiseListChangedEvents = False
            For Each item As ClsProject In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsProject In Me
                If item.IsNew Then
                    item.Insert(parents)
                Else
                    item.Update(parents)
                End If
            Next
            RaiseListChangedEvents = True
        End Sub

#End Region

#Region " Common Area "

        Private Sub Fetch()
            Fetch(DAClsappAdvertiserProjects.Fetch())
        End Sub

        Private Sub Fetch(ByVal advertiserId As Long, ByVal contactId As Long)
            Fetch(DAClsappAdvertiserProjects.Fetch(advertiserID, contactId))
        End Sub

        Private Sub Fetch(ByVal advertiserId() As Long, ByVal contactId() As Long)
            Fetch(DAClsappAdvertiserProjects.Fetch(advertiserID, contactId))
        End Sub

        Private Sub Fetch(ByVal list As DAClsappAdvertiserProjects.Struct())
            RaiseListChangedEvents = False
            For Each Struct As DAClsappAdvertiserProjects.Struct In list
                Add(ClsProject.GetChildProject(Struct))
            Next
            RaiseListChangedEvents = True
        End Sub

#End Region

#End Region

    End Class
End Namespace