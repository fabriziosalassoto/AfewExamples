Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsGroupProfileList
    Inherits BusinessListBase(Of ClsGroupProfileList, ClsGroupProfile)

#Region " Business Methods "

    Public Function GetItem(ByVal idProfile As Long) As ClsGroupProfile
        For Each profile As ClsGroupProfile In Me
            If profile.ID = idProfile Then
                Return profile
            End If
        Next
        Return Nothing
    End Function

    Public Function GetDeletedItem(ByVal idProfile As Long) As ClsGroupProfile
        For Each profile As ClsGroupProfile In DeletedList
            If profile.ID = idProfile Then
                Return profile
            End If
        Next
        Return Nothing
    End Function

    Public Overloads Sub Add(ByVal idProfile As Long, ByVal IDForm As Long, ByVal idGroup As Long, ByVal pSelect As Boolean, ByVal pInsert As Boolean, ByVal pUpdate As Boolean, ByVal pDelete As Boolean)
        If Not Contains(idProfile) Then
            Me.Add(ClsGroupProfile.NewGroupProfile(idProfile, IDForm, idGroup, pSelect, pInsert, pUpdate, pDelete))
        Else
            Throw New InvalidOperationException("Profile already assigned to Group")
        End If
    End Sub

    Public Sub Edit(ByVal idProfile As Long, ByVal pSelect As Boolean, ByVal pInsert As Boolean, ByVal pUpdate As Boolean, ByVal pDelete As Boolean)
        If Contains(idProfile) Then
            For Each profile As ClsGroupProfile In Me
                If profile.ID = idProfile Then
                    profile.PSelect = pSelect
                    profile.PInsert = pInsert
                    profile.PUpdate = pUpdate
                    profile.PDelete = pDelete
                    Exit For
                End If
            Next
        Else
            Throw New InvalidOperationException("Profile no assigned to Group")
        End If
    End Sub

    Public Overloads Sub Remove(ByVal idProfile As Long)
        If Contains(idProfile) Then
            For Each profile As ClsGroupProfile In Me
                If profile.ID = idProfile Then
                    Remove(profile)
                    Exit For
                End If
            Next
        Else
            Throw New InvalidOperationException("Profile no assigned to Group")
        End If
    End Sub

    Public Overloads Function Contains(ByVal idProfile As Long) As Boolean
        For Each profile As ClsGroupProfile In Me
            If profile.ID = idProfile Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Overloads Function ContainsDeleted(ByVal idProfile As Long) As Boolean
        For Each profile As ClsGroupProfile In DeletedList
            If profile.ID = idProfile Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Function CanExecute(ByVal pName As String) As Boolean
        For Each profile As ClsGroupProfile In Me
            If Csla.ApplicationContext.User.IsInRole(profile.ID.ToString) Then
                Return profile.CanExecute(pName)
            End If
        Next
        Return False
    End Function

#End Region

#Region " Factory Methods "

    Friend Shared Function NewGroupProfileList() As ClsGroupProfileList
        Return New ClsGroupProfileList
    End Function

    Friend Shared Function GetGroupProfileList(ByVal list As DAClsprgAdministrativeGroupPermissions.Struct()) As ClsGroupProfileList
        Return New ClsGroupProfileList(list)
    End Function

    Private Sub New()
        MarkAsChild()
    End Sub

    Private Sub New(ByVal list As DAClsprgAdministrativeGroupPermissions.Struct())
        MarkAsChild()
        Fetch(list)
    End Sub

#End Region

#Region " Data Access "

    Private Sub Fetch(ByVal list As DAClsprgAdministrativeGroupPermissions.Struct())
        Me.RaiseListChangedEvents = False
        For Each Struct As DAClsprgAdministrativeGroupPermissions.Struct In list
            Me.Add(ClsGroupProfile.GetGroupProfile(Struct))
        Next
        Me.RaiseListChangedEvents = True
    End Sub

    Friend Sub Update(ByVal parent As ClsGroup)

        Me.RaiseListChangedEvents = False
        ' update (thus deleting) any deleted child objects
        For Each item As ClsGroupProfile In DeletedList
            item.DeleteSelf(New Object() {parent, item.FormProfile})
        Next
        ' now that they are deleted, remove them from memory too
        DeletedList.Clear()

        ' add/update any current child objects
        For Each item As ClsGroupProfile In Me
            If item.IsNew Then
                item.Insert(New Object() {parent, item.FormProfile})

            Else
                item.Update(New Object() {parent, item.FormProfile})
            End If
        Next
        Me.RaiseListChangedEvents = True

    End Sub

    Friend Sub Update(ByVal parent As ClsFormProfile)

        Me.RaiseListChangedEvents = False
        ' update (thus deleting) any deleted child objects
        For Each item As ClsGroupProfile In DeletedList
            item.DeleteSelf(New Object() {item.Group, parent})
        Next
        ' now that they are deleted, remove them from memory too
        DeletedList.Clear()

        ' add/update any current child objects
        For Each item As ClsGroupProfile In Me
            If item.IsNew Then
                item.Insert(New Object() {item.Group, parent})
            Else
                item.Update(New Object() {item.Group, parent})
            End If
        Next
        Me.RaiseListChangedEvents = True

    End Sub

#End Region

End Class
