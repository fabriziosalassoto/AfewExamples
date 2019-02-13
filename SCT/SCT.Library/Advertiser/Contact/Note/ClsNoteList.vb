Imports Csla
Imports SCT.DataAccess

Namespace Advertiser.Contact
    <Serializable()> Public Class ClsNoteList
        Inherits BusinessListBase(Of ClsNoteList, ClsNote)

#Region " Business Methods "

        Public Function GetItem(ByVal noteId As Long) As ClsNote
            For Each note As ClsNote In Me
                If note.ID = noteId Then
                    Return note
                End If
            Next
            Return Nothing
        End Function

        Public Overloads Sub Remove(ByVal noteId As Long)
            For Each note As ClsNote In Me
                If note.ID = noteId Then
                    Remove(note)
                    Exit For
                End If
            Next
        End Sub

        Public Overloads Function Contains(ByVal noteId As Long) As Boolean
            For Each note As ClsNote In Me
                If note.ID = noteId Then
                    Return True
                End If
            Next
            Return False
        End Function

        Public Function FindAllAdvertiserNote(ByVal advertiserID As Long) As ClsNoteList
            Dim noteList As ClsNoteList = ClsNoteList.NewNoteList
            For Each note As ClsNote In Me
                If advertiserID = 0 OrElse note.Contact.Advertiser.ID = advertiserID Then
                    noteList.Add(note)
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

        Public Shared Function NewNoteList() As ClsNoteList
            Return DataPortal.Create(Of ClsNoteList)(New Criteria())
        End Function

        Public Shared Function GetNoteList() As ClsNoteList
            Return DataPortal.Fetch(Of ClsNoteList)(New Criteria())
        End Function

        Public Shared Function GetNoteList(ByVal contactID As Long, ByVal fromDate As Date, ByVal toDate As Date) As ClsNoteList
            Return DataPortal.Fetch(Of ClsNoteList)(New FilteredCriteria(contactID, fromDate, toDate))
        End Function

        Public Shared Function GetNoteList(ByVal contactID() As Long, ByVal fromDate As Date, ByVal toDate As Date) As ClsNoteList
            Return DataPortal.Fetch(Of ClsNoteList)(New FilteredCriteriaList(contactID, fromDate, toDate))
        End Function

#End Region

#Region " Child Methods "

        Friend Shared Function NewContactNotes() As ClsNoteList
            Return New ClsNoteList
        End Function

        Friend Shared Function GetContactNotes(ByVal list As DAClsappAdvertiserNotes.Struct()) As ClsNoteList
            Return New ClsNoteList(list)
        End Function

        Private Sub New()
            MarkAsChild()
        End Sub

        Private Sub New(ByVal list As DAClsappAdvertiserNotes.Struct())
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

            Private mID As Long
            Private mFromDate As New Date
            Private mToDate As New Date

            Public ReadOnly Property ID() As Long
                Get
                    Return Me.mID
                End Get
            End Property

            Public ReadOnly Property FromDate() As Date
                Get
                    Return Me.mFromDate
                End Get
            End Property

            Public ReadOnly Property ToDate() As Date
                Get
                    Return Me.mToDate
                End Get
            End Property

            Public Sub New(ByVal contactID As Long, ByVal fromDate As Date, ByVal toDate As Date)
                Me.mID = contactID
                Me.mFromDate = fromDate
                Me.mToDate = toDate
            End Sub

        End Class

        <Serializable()> Private Class FilteredCriteriaList

            Private mID() As Long
            Private mFromDate As New Date
            Private mToDate As New Date

            Public ReadOnly Property ID() As Long()
                Get
                    Return Me.mID
                End Get
            End Property

            Public ReadOnly Property FromDate() As Date
                Get
                    Return Me.mFromDate
                End Get
            End Property

            Public ReadOnly Property ToDate() As Date
                Get
                    Return Me.mToDate
                End Get
            End Property

            Public Sub New(ByVal contactID() As Long, ByVal fromDate As Date, ByVal toDate As Date)
                Me.mID = contactID
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
            Fetch(criteria.ID, criteria.fromDate, criteria.todate)
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteriaList)
            Fetch(criteria.ID, criteria.fromDate, criteria.todate)
        End Sub

        Protected Overrides Sub DataPortal_Update()
            RaiseListChangedEvents = False
            For Each item As ClsNote In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsNote In Me
                If item.IsNew Then
                    item.Insert(item.Contact)
                Else
                    item.Update(item.Contact)
                End If
            Next
            RaiseListChangedEvents = True
        End Sub

#End Region

#Region " Child Area "

        Friend Sub Update(ByVal parent As Object)
            RaiseListChangedEvents = False
            For Each item As ClsNote In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsNote In Me
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
            Fetch(DAClsappAdvertiserNotes.Fetch())
        End Sub

        Private Sub Fetch(ByVal contactId As Long, ByVal fromDate As Date, ByVal toDate As Date)
            Fetch(DAClsappAdvertiserNotes.Fetch(contactId, fromDate, toDate))
        End Sub

        Private Sub Fetch(ByVal contactId() As Long, ByVal fromDate As Date, ByVal toDate As Date)
            Fetch(DAClsappAdvertiserNotes.Fetch(contactId, fromDate, toDate))
        End Sub

        Private Sub Fetch(ByVal list As DAClsappAdvertiserNotes.Struct())
            RaiseListChangedEvents = False
            For Each Struct As DAClsappAdvertiserNotes.Struct In list
                Add(ClsNote.GetChildNote(Struct))
            Next
            RaiseListChangedEvents = True
        End Sub

#End Region

#End Region

    End Class
End Namespace