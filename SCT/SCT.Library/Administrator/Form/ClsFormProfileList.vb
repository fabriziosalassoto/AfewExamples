Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsFormProfileList
    Inherits BusinessListBase(Of ClsFormProfileList, ClsFormProfile)

#Region " Business Methods "

    Public Function GetItem(ByVal idProfile As Long) As ClsFormProfile
        For Each profile As ClsFormProfile In Me
            If profile.ID = idProfile Then
                Return profile
            End If
        Next
        Return Nothing
    End Function

    Public Function GetDeletedItem(ByVal idProfile As Long) As ClsFormProfile
        For Each profile As ClsFormProfile In DeletedList
            If profile.ID = idProfile Then
                Return profile
            End If
        Next
        Return Nothing
    End Function

    Public Overloads Sub Add(ByVal idProfile As Long, ByVal idForm As Long, ByVal pSelect As Boolean, ByVal pInsert As Boolean, ByVal pUpdate As Boolean, ByVal pDelete As Boolean)
        If Not Contains(idProfile) Then
            Me.Add(ClsFormProfile.NewFormProfile(idProfile, idForm, pSelect, pInsert, pUpdate, pDelete))
        Else
            Throw New InvalidOperationException("Profile already assigned to form")
        End If
    End Sub

    Public Sub Edit(ByVal idProfile As Long, ByVal pSelect As Boolean, ByVal pInsert As Boolean, ByVal pUpdate As Boolean, ByVal pDelete As Boolean)
        If Contains(idProfile) Then
            For Each profile As ClsFormProfile In Me
                If profile.ID = idProfile Then
                    profile.PSelect = pSelect
                    profile.PInsert = pInsert
                    profile.PUpdate = pUpdate
                    profile.PDelete = pDelete
                    Exit For
                End If
            Next
        Else
            Throw New InvalidOperationException("Profile no assigned to form")
        End If
    End Sub

    Public Overloads Sub Remove(ByVal idProfile As Long)
        If Contains(idProfile) Then
            For Each profile As ClsFormProfile In Me
                If profile.ID = idProfile Then
                    Remove(profile)
                    Exit For
                End If
            Next
        Else
            Throw New InvalidOperationException("Profile no assigned to Form")
        End If
    End Sub

    Public Overloads Function Contains(ByVal idProfile As Long) As Boolean
        For Each profile As ClsFormProfile In Me
            If profile.ID = idProfile Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Overloads Function ContainsUserProfile() As Boolean
        For Each profile As ClsFormProfile In Me
            If Csla.ApplicationContext.User.IsInRole(profile.ID.ToString) Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Overloads Function ContainsDeleted(ByVal idProfile As Long) As Boolean
        For Each profile As ClsFormProfile In DeletedList
            If profile.ID = idProfile Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Function CanExecute(ByVal pName As String) As Boolean
        For Each profile As ClsFormProfile In Me
            If Csla.ApplicationContext.User.IsInRole(profile.ID.ToString) Then
                Return profile.CanExecute(pName)
            End If
        Next
        Return False
    End Function

#End Region

#Region " Factory Methods "

    Friend Shared Function NewFormProfileList() As ClsFormProfileList
        Return New ClsFormProfileList
    End Function

    Friend Shared Function GetFormProfileList(ByVal list As DAClsprgAdministrativeFormPermissions.Struct()) As ClsFormProfileList
        Return New ClsFormProfileList(list)
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
            Me.Add(ClsFormProfile.GetFormProfile(Struct))
        Next
        Me.RaiseListChangedEvents = True
    End Sub

    Friend Sub Update(ByVal parent As Object)
        Me.RaiseListChangedEvents = False
        ' update (thus deleting) any deleted child objects
        For Each item As ClsFormProfile In DeletedList
            item.DeleteSelf(parent)
        Next
        ' now that they are deleted, remove them from memory too
        DeletedList.Clear()

        ' add/update any current child objects
        For Each item As ClsFormProfile In Me
            If item.IsNew Then
                item.Insert(parent)
            Else
                item.Update(parent)
            End If
        Next
        Me.RaiseListChangedEvents = True
    End Sub

#End Region

End Class
