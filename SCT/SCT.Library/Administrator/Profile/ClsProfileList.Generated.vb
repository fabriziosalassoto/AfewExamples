Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsProfileList
    Inherits BusinessListBase(Of ClsProfileList, ClsProfile)

#Region " Business Methods "

    Public Function GetItem(ByVal Id As Long) As ClsProfile
        For Each item As ClsProfile In Me
            If item.ID = Id Then
                Return item
            End If
        Next
        Return Nothing
    End Function

    Public Overloads Sub Remove(ByVal Id As Long)
        For Each item As ClsProfile In Me
            If item.ID = Id Then
                Remove(item)
                Exit For
            End If
        Next
    End Sub

    Public Overloads Function Contains(ByVal Id As Long) As Boolean
        For Each item As ClsProfile In Me
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

    Public Shared Function NewProfileList() As ClsProfileList
        Return DataPortal.Create(Of ClsProfileList)(New Criteria())
    End Function

    Public Shared Function GetProfileList() As ClsProfileList
        Return DataPortal.Fetch(Of ClsProfileList)(New Criteria())
    End Function

    Public Shared Function GetProfileList(ByVal description As String) As ClsProfileList
        Return DataPortal.Fetch(Of ClsProfileList)(New FilteredCriteria(description))
    End Function

#End Region

#Region " Child Methods "

    Friend Shared Function NewChildProfileList() As ClsProfileList
        Return New ClsProfileList
    End Function

    Friend Shared Function GetChildProfileList(ByVal list As DAClsprgAdministrativeProfiles.Struct()) As ClsProfileList
        Return New ClsProfileList(list)
    End Function

    Private Sub New()
        MarkAsChild()
    End Sub

    Private Sub New(ByVal list As DAClsprgAdministrativeProfiles.Struct())
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

        Private mDescription As String

        Public ReadOnly Property Description() As String
            Get
                Return Me.mDescription
            End Get
        End Property

        Public Sub New(ByVal description As String)
            Me.mDescription = description
        End Sub

    End Class

    <RunLocal()> Private Overloads Sub DataPortal_Create(ByVal criteria As Criteria)
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
        ' fetch with no filter
        Fetch()
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteria)
        Fetch(criteria.Description)
    End Sub

    Protected Overrides Sub DataPortal_Update()
        RaiseListChangedEvents = False
        For Each item As ClsProfile In DeletedList
            item.DeleteSelf()
        Next
        DeletedList.Clear()
        For Each item As ClsProfile In Me
            If item.IsNew Then
                item.Insert(Nothing)
            Else
                item.Update(Nothing)
            End If
        Next
        RaiseListChangedEvents = True
    End Sub

#End Region

#Region " Child Area "

    Friend Sub Update(ByVal parent As Object)
        RaiseListChangedEvents = False
        For Each item As ClsProfile In DeletedList
            item.DeleteSelf()
        Next
        DeletedList.Clear()
        For Each item As ClsProfile In Me
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
        Fetch(DAClsprgAdministrativeProfiles.Fetch())
    End Sub

    Private Sub Fetch(ByVal description As String)
        Fetch(DAClsprgAdministrativeProfiles.Fetch(description))
    End Sub

    Private Sub Fetch(ByVal list As DAClsprgAdministrativeProfiles.Struct())
        Me.RaiseListChangedEvents = False
        For Each Struct As DAClsprgAdministrativeProfiles.Struct In list
            Me.Add(ClsProfile.GetChildProfile(Struct))
        Next
        Me.RaiseListChangedEvents = True
    End Sub

#End Region

#End Region

End Class
