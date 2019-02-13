Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsProfileFormList
    Inherits BusinessListBase(Of ClsProfileFormList, ClsProfileForm)

#Region " Business Methods "

    Public Function GetItem(ByVal idForm As Long) As ClsProfileForm
        For Each form As ClsProfileForm In Me
            If form.ID = idForm Then
                Return form
            End If
        Next
        Return Nothing
    End Function

    Public Function GetDeletedItem(ByVal idForm As Long) As ClsProfileForm
        For Each form As ClsProfileForm In DeletedList
            If form.ID = idForm Then
                Return form
            End If
        Next
        Return Nothing
    End Function

    Public Overloads Sub Add(ByVal idForm As Long, ByVal idProfile As Long, ByVal pSelect As Boolean, ByVal pInsert As Boolean, ByVal pUpdate As Boolean, ByVal pDelete As Boolean)
        If Not Contains(idForm) Then
            Me.Add(ClsProfileForm.NewProfileForm(idForm, idProfile, pSelect, pInsert, pUpdate, pDelete))
        Else
            Throw New InvalidOperationException("Form already assigned to Profile")
        End If
    End Sub

    Public Sub Edit(ByVal idForm As Long, ByVal pSelect As Boolean, ByVal pInsert As Boolean, ByVal pUpdate As Boolean, ByVal pDelete As Boolean)
        If Contains(idForm) Then
            For Each form As ClsProfileForm In Me
                If form.ID = idForm Then
                    form.PSelect = pSelect
                    form.PInsert = pInsert
                    form.PUpdate = pUpdate
                    form.PDelete = pDelete
                    Exit For
                End If
            Next
        Else
            Throw New InvalidOperationException("Form no assigned to Profile")
        End If
    End Sub

    Public Overloads Sub Remove(ByVal idForm As Long)
        If Contains(idForm) Then
            For Each form As ClsProfileForm In Me
                If form.ID = idForm Then
                    Remove(form)
                    Exit For
                End If
            Next
        Else
            Throw New InvalidOperationException("Form no assigned to Profile")
        End If
    End Sub

    Public Overloads Function Contains(ByVal idForm As Long) As Boolean
        For Each form As ClsProfileForm In Me
            If form.ID = idForm Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Overloads Function ContainsDeleted(ByVal idForm As Long) As Boolean
        For Each form As ClsProfileForm In DeletedList
            If form.ID = idForm Then
                Return True
            End If
        Next
        Return False
    End Function

#End Region

#Region " Factory Methods "

    Friend Shared Function NewProfileFormList() As ClsProfileFormList
        Return New ClsProfileFormList
    End Function

    Friend Shared Function GetProfileFormList(ByVal list As DAClsprgAdministrativeFormPermissions.Struct()) As ClsProfileFormList
        Return New ClsProfileFormList(list)
    End Function

    Private Sub New()
        MarkAsChild()
    End Sub

    Private Sub New(ByVal list As DAClsprgAdministrativeFormPermissions.Struct())
        MarkAsChild()
        Fetch(list)
    End Sub

#End Region

#Region " Data Access "

    Private Sub Fetch(ByVal list As DAClsprgAdministrativeFormPermissions.Struct())
        Me.RaiseListChangedEvents = False
        For Each Struct As DAClsprgAdministrativeFormPermissions.Struct In list
            Me.Add(ClsProfileForm.GetProfileForm(Struct))
        Next
        Me.RaiseListChangedEvents = True
    End Sub

    Friend Sub Update(ByVal parents() As Object)
        Me.RaiseListChangedEvents = False
        ' update (thus deleting) any deleted child objects
        For Each item As ClsProfileForm In DeletedList
            item.DeleteSelf(parents)
        Next
        ' now that they are deleted, remove them from memory too
        DeletedList.Clear()

        ' add/update any current child objects
        For Each item As ClsProfileForm In Me
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
