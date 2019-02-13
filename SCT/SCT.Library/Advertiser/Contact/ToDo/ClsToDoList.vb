Imports Csla
Imports SCT.DataAccess

Namespace Advertiser.Contact
    <Serializable()> Public Class ClsToDoList
        Inherits BusinessListBase(Of ClsToDoList, ClsToDo)

#Region " Business Methods "

        Public Function GetItem(ByVal toDoId As Long) As ClsToDo
            For Each toDo As ClsToDo In Me
                If toDo.ID = toDoId Then
                    Return toDo
                End If
            Next
            Return Nothing
        End Function

        Public Overloads Sub Remove(ByVal todoId As Long)
            For Each todo As ClsToDo In Me
                If todo.ID = todoId Then
                    Remove(todo)
                    Exit For
                End If
            Next
        End Sub

        Public Overloads Function Contains(ByVal todoId As Long) As Boolean
            For Each todo As ClsToDo In Me
                If todo.ID = todoId Then
                    Return True
                End If
            Next
            Return False
        End Function

        Public Function FindAllAdvertiserToDo(ByVal advertiserContactList As ClsContactList) As ClsToDoList
            Dim toDoList As ClsToDoList = ClsToDoList.NewToDoList
            For Each toDo As ClsToDo In Me
                If advertiserContactList.Contains(toDo.Contact.ID) Then
                    toDoList.Add(toDo)
                End If
            Next
            Return toDoList
        End Function

        Public Function FindAllAdvertiserToDo(ByVal advertiserID As Long) As ClsToDoList
            Dim toDoList As ClsToDoList = ClsToDoList.NewToDoList
            For Each toDo As ClsToDo In Me
                If advertiserID = 0 OrElse toDo.Contact.Advertiser.ID = advertiserID Then
                    toDoList.Add(toDo)
                End If
            Next
            Return toDoList
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

        Public Shared Function NewToDoList() As ClsToDoList
            Return DataPortal.Create(Of ClsToDoList)(New Criteria())
        End Function

        Public Shared Function GetToDoList() As ClsToDoList
            Return DataPortal.Fetch(Of ClsToDoList)(New Criteria())
        End Function

        Public Shared Function GetToDoList(ByVal contactID As Long, ByVal fromDate As Date, ByVal toDate As Date) As ClsToDoList
            Return DataPortal.Fetch(Of ClsToDoList)(New FilteredCriteria(contactID, fromDate, toDate))
        End Function

        Public Shared Function GetToDoList(ByVal contactID() As Long, ByVal fromDate As Date, ByVal toDate As Date) As ClsToDoList
            Return DataPortal.Fetch(Of ClsToDoList)(New FilteredCriteriaList(contactID, fromDate, toDate))
        End Function

#End Region

#Region " Child Methods "

        Friend Shared Function NewContactToDos() As ClsToDoList
            Return New ClsToDoList
        End Function

        Friend Shared Function GetContactToDos(ByVal list As DAClsappAdvertiserToDo.Struct()) As ClsToDoList
            Return New ClsToDoList(list)
        End Function

        Private Sub New()
            MarkAsChild()
        End Sub

        Private Sub New(ByVal list As DAClsappAdvertiserToDo.Struct())
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

            Private mContactID As Long
            Private mFromDate As New Date
            Private mToDate As New Date

            Public ReadOnly Property ContactID() As Long
                Get
                    Return Me.mContactID
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
                Me.mContactID = contactID
                Me.mFromDate = fromDate
                Me.mToDate = toDate
            End Sub

        End Class

        <Serializable()> Private Class FilteredCriteriaList

            Private mContactID() As Long
            Private mFromDate As New Date
            Private mToDate As New Date

            Public ReadOnly Property ContactID() As Long()
                Get
                    Return Me.mContactID
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
                Me.mContactID = contactID
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
            Fetch(criteria.ContactID, criteria.FromDate, criteria.ToDate)
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteriaList)
            Fetch(criteria.ContactID, criteria.FromDate, criteria.ToDate)
        End Sub

        Protected Overrides Sub DataPortal_Update()
            RaiseListChangedEvents = False
            For Each item As ClsToDo In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsToDo In Me
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
            For Each item As ClsToDo In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsToDo In Me
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
            Fetch(DAClsappAdvertiserToDo.Fetch())
        End Sub

        Private Sub Fetch(ByVal contactID As Long, ByVal fromDate As Date, ByVal toDate As Date)
            Fetch(DAClsappAdvertiserToDo.Fetch(contactID, fromDate, toDate))
        End Sub

        Private Sub Fetch(ByVal contactID() As Long, ByVal fromDate As Date, ByVal toDate As Date)
            Fetch(DAClsappAdvertiserToDo.Fetch(contactID, fromDate, toDate))
        End Sub

        Private Sub Fetch(ByVal list As DAClsappAdvertiserToDo.Struct())
            RaiseListChangedEvents = False
            For Each Struct As DAClsappAdvertiserToDo.Struct In list
                Add(ClsToDo.GetChildToDo(Struct))
            Next
            RaiseListChangedEvents = True
        End Sub

#End Region

#End Region

    End Class
End Namespace