Imports Csla
Imports SCT.DataAccess

Namespace Advertiser
    <Serializable()> Public Class ClsContactList
        Inherits BusinessListBase(Of ClsContactList, ClsContact)

#Region " Business Methods "

        Public Function GetItem(ByVal contactId As Long) As Clscontact
            For Each contact As ClsContact In Me
                If contact.ID = contactId Then
                    Return contact
                End If
            Next
            Return Nothing
        End Function

        Public Overloads Sub Remove(ByVal contactId As Long)
            For Each contact As ClsContact In Me
                If contact.ID = contactId Then
                    Remove(contact)
                    Exit For
                End If
            Next
        End Sub

        Public Overloads Function Contains(ByVal contactId As Long) As Boolean
            For Each contact As ClsContact In Me
                If contact.ID = contactId Then
                    Return True
                End If
            Next
            Return False
        End Function

        Public Function FindAllAdvertiserTodo(ByVal contactId As Long) As Contact.ClsToDoList
            Dim todoList As Contact.ClsToDoList = Contact.ClsToDoList.NewToDoList
            For Each contact As ClsContact In Me
                If contactId = 0 OrElse contact.ID = contactId Then
                    For Each todo As Contact.ClsToDo In contact.ToDos
                        todoList.Add(todo)
                    Next
                End If
            Next
            Return todoList
        End Function

        Public Function FindAllAdvertiserNotes(ByVal contactId As Long) As Contact.ClsNoteList
            Dim noteList As Contact.ClsNoteList = Contact.ClsNoteList.NewNoteList
            For Each contact As ClsContact In Me
                If contactId = 0 OrElse contact.ID = contactId Then
                    For Each note As Contact.ClsNote In contact.Notes
                        noteList.Add(note)
                    Next
                End If
            Next
            Return noteList
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

        Public Shared Function NewContactList() As ClsContactList
            Return DataPortal.Create(Of ClsContactList)(New Criteria())
        End Function

        Public Shared Function GetContactList() As ClsContactList
            Return DataPortal.Fetch(Of ClsContactList)(New Criteria())
        End Function

        Public Shared Function GetContactList(ByVal advertiserID As Long, ByVal mainCompanyAddress As Boolean) As ClsContactList
            Return DataPortal.Fetch(Of ClsContactList)(New FilteredCriteria(advertiserID, mainCompanyAddress))
        End Function

        Public Shared Function GetContactList(ByVal advertiserID() As Long, ByVal mainCompanyAddress As Boolean) As ClsContactList
            Return DataPortal.Fetch(Of ClsContactList)(New FilteredCriteriaList(advertiserID, mainCompanyAddress))
        End Function

#End Region

#Region " Child Methods "

        Friend Shared Function NewAdvertiserContacts() As ClsContactList
            Return New ClsContactList
        End Function

        Friend Shared Function GetAdvertiserContacts(ByVal list As DAClsappAdvertiserContactInfo.Struct()) As ClsContactList
            Return New ClsContactList(list)
        End Function

        Private Sub New()
            MarkAsChild()
        End Sub

        Private Sub New(ByVal list As DAClsappAdvertiserContactInfo.Struct())
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

        <RunLocal()> Private Overloads Sub DataPortal_Create(ByVal criteria As Criteria)
        End Sub

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

        Protected Overrides Sub DataPortal_Update()
            RaiseListChangedEvents = False
            For Each item As ClsContact In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsContact In Me
                If item.IsNew Then
                    item.Insert(item.Advertiser)
                Else
                    item.Update(item.Advertiser)
                End If
            Next
            RaiseListChangedEvents = True
        End Sub

#End Region

#Region " Child Area "

        Friend Sub Update(ByVal parent As Object)
            RaiseListChangedEvents = False
            For Each item As ClsContact In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsContact In Me
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
            Fetch(DAClsappAdvertiserContactInfo.Fetch())
        End Sub

        Private Sub Fetch(ByVal advertiserID As Long, ByVal mainCompanyAddress As Boolean)
            Fetch(DAClsappAdvertiserContactInfo.Fetch(advertiserID, mainCompanyAddress))
        End Sub

        Private Sub Fetch(ByVal advertiserID() As Long, ByVal mainCompanyAddress As Boolean)
            Fetch(DAClsappAdvertiserContactInfo.Fetch(advertiserID, mainCompanyAddress))
        End Sub

        Private Sub Fetch(ByVal list As DAClsappAdvertiserContactInfo.Struct())
            RaiseListChangedEvents = False
            For Each Struct As DAClsappAdvertiserContactInfo.Struct In list
                Add(ClsContact.GetChildContact(Struct))
            Next
            RaiseListChangedEvents = True
        End Sub

#End Region

#End Region

    End Class
End Namespace