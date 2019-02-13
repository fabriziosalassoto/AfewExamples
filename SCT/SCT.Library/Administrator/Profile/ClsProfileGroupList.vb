Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsProfileGroupList
    Inherits BusinessListBase(Of ClsProfileGroupList, ClsProfileGroup)

#Region " Business Methods "

    Public Function GetItem(ByVal idGroup As Long) As ClsProfileGroup
        For Each group As ClsProfileGroup In Me
            If group.ID = idGroup Then
                Return group
            End If
        Next
        Return Nothing
    End Function

    Public Function GetDeletedItem(ByVal idGroup As Long) As ClsProfileGroup
        For Each group As ClsProfileGroup In DeletedList
            If group.ID = idGroup Then
                Return group
            End If
        Next
        Return Nothing
    End Function

    Public Overloads Sub Add(ByVal idGroup As Long, ByVal idForm As Long, ByVal idProfile As Long, ByVal pSelect As Boolean, ByVal pInsert As Boolean, ByVal pUpdate As Boolean, ByVal pDelete As Boolean)
        If Not Contains(idGroup) Then
            Me.Add(ClsProfileGroup.NewProfileFormGroup(idGroup, idForm, idProfile, pSelect, pInsert, pUpdate, pDelete))
        Else
            Throw New InvalidOperationException("Group already assigned to Profile")
        End If
    End Sub

    Public Sub Edit(ByVal idGroup As Long, ByVal pSelect As Boolean, ByVal pInsert As Boolean, ByVal pUpdate As Boolean, ByVal pDelete As Boolean)
        If Contains(idGroup) Then
            For Each group As ClsProfileGroup In Me
                If group.ID = idGroup Then
                    group.PSelect = pSelect
                    group.PInsert = pInsert
                    group.PUpdate = pUpdate
                    group.PDelete = pDelete
                    Exit For
                End If
            Next
        Else
            Throw New InvalidOperationException("Group no assigned to Profile")
        End If
    End Sub

    Public Overloads Sub Remove(ByVal idGroup As Long)
        If Contains(idGroup) Then
            For Each group As ClsProfileGroup In Me
                If group.ID = idGroup Then
                    Remove(group)
                    Exit For
                End If
            Next
        Else
            Throw New InvalidOperationException("Group no assigned to Profile")
        End If
    End Sub

    Public Overloads Function Contains(ByVal idGroup As Long) As Boolean
        For Each group As ClsProfileGroup In Me
            If group.ID = idGroup Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Overloads Function ContainsDeleted(ByVal idGroup As Long) As Boolean
        For Each group As ClsProfileGroup In DeletedList
            If group.ID = idGroup Then
                Return True
            End If
        Next
        Return False
    End Function

#End Region

#Region " Factory Methods "

    Friend Shared Function NewProfileFormGroupList() As ClsProfileGroupList
        Return New ClsProfileGroupList
    End Function

    Friend Shared Function GetProfileFormGroupList(ByVal list As DAClsprgAdministrativeGroupPermissions.Struct()) As ClsProfileGroupList
        Return New ClsProfileGroupList(list)
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
            Me.Add(ClsProfileGroup.GetProfileFormGroup(Struct))
        Next
        Me.RaiseListChangedEvents = True
    End Sub

    Friend Sub Update(ByVal parents() As Object)

        Me.RaiseListChangedEvents = False
        ' update (thus deleting) any deleted child objects
        For Each item As ClsProfileGroup In DeletedList
            item.DeleteSelf(parents)
        Next
        ' now that they are deleted, remove them from memory too
        DeletedList.Clear()

        ' add/update any current child objects
        For Each item As ClsProfileGroup In Me
            If item.IsNew Then
                item.Insert(parents)
            Else
                item.Update(parents)
            End If
        Next
        Me.RaiseListChangedEvents = True

    End Sub

#End Region

End Class
