Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsUserList
    Inherits BusinessListBase(Of ClsUserList, ClsUser)

#Region " Business Methods "

    Public Function GetItem(ByVal Id As Long) As ClsUser
        For Each item As ClsUser In Me
            If item.ID = Id Then
                Return item
            End If
        Next
        Return Nothing
    End Function

    Public Overloads Sub Remove(ByVal Id As Long)
        For Each item As ClsUser In Me
            If item.ID = Id Then
                Remove(item)
                Exit For
            End If
        Next
    End Sub

    Public Overloads Function Contains(ByVal Id As Long) As Boolean
        For Each item As ClsUser In Me
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

    Public Shared Function NewUserList() As ClsUserList
        Return DataPortal.Create(Of ClsUserList)(New Criteria())
    End Function

    Public Shared Function GetUserList() As ClsUserList
        Return DataPortal.Fetch(Of ClsUserList)(New Criteria())
    End Function

    Public Shared Function GetUserList(ByVal IDProfile As Long, ByVal FullName As String) As ClsUserList
        Return DataPortal.Fetch(Of ClsUserList)(New FilteredCriteria(IDProfile, FullName))
    End Function

    Public Shared Function GetUserList(ByVal IDProfile() As Long, ByVal FullName As String) As ClsUserList
        Return DataPortal.Fetch(Of ClsUserList)(New FilteredCriteriaList(IDProfile, FullName))
    End Function

#End Region

#Region " Child Methods "

    Friend Shared Function NewProfileUserList() As ClsUserList
        Return New ClsUserList
    End Function

    Friend Shared Function GetProfileUserList(ByVal list As DAClsprgAdministrativeUsers.Struct()) As ClsUserList
        Return New ClsUserList(list)
    End Function

    Private Sub New()
        MarkAsChild()
    End Sub

    Private Sub New(ByVal list As DAClsprgAdministrativeUsers.Struct())
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

        Private mIDProfile As Long
        Private mFullName As String

        Public ReadOnly Property IDProfile() As Long
            Get
                Return Me.mIDProfile
            End Get
        End Property

        Public ReadOnly Property FullName() As String
            Get
                Return Me.mFullName
            End Get
        End Property

        Public Sub New(ByVal IDprofile As Long, ByVal FullName As String)
            Me.mIDProfile = IDprofile
            Me.mFullName = FullName
        End Sub

    End Class

    <Serializable()> Private Class FilteredCriteriaList

        Private mIDProfile() As Long
        Private mFullName As String

        Public ReadOnly Property IDProfile() As Long()
            Get
                Return Me.mIDProfile
            End Get
        End Property

        Public ReadOnly Property FullName() As String
            Get
                Return Me.mFullName
            End Get
        End Property

        Public Sub New(ByVal IDprofile() As Long, ByVal FullName As String)
            Me.mIDProfile = IDprofile
            Me.mFullName = FullName
        End Sub

    End Class

    <RunLocal()> Private Overloads Sub DataPortal_Create(ByVal criteria As Criteria)
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
        ' fetch with no filter
        Fetch()
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteria)
        Fetch(criteria.IDProfile, criteria.FullName)
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriterialist)
        Fetch(criteria.IDProfile, criteria.FullName)
    End Sub

    Protected Overrides Sub DataPortal_Update()
        RaiseListChangedEvents = False
        For Each item As ClsUser In DeletedList
            item.DeleteSelf()
        Next
        DeletedList.Clear()
        For Each item As ClsUser In Me
            If item.IsNew Then
                item.Insert(item.Profile)
            Else
                item.Update(item.Profile)
            End If
        Next
        RaiseListChangedEvents = True
    End Sub

#End Region

#Region " Child Area "

    Friend Sub Update(ByVal parent As Object)
        RaiseListChangedEvents = False
        For Each item As ClsUser In DeletedList
            item.DeleteSelf()
        Next
        DeletedList.Clear()
        For Each item As ClsUser In Me
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
        Fetch(DAClsprgAdministrativeUsers.Fetch())
    End Sub

    Private Sub Fetch(ByVal IDProfile As Long, ByVal FullName As String)
        Fetch(DAClsprgAdministrativeUsers.Fetch(IDProfile, FullName))
    End Sub

    Private Sub Fetch(ByVal IDProfile() As Long, ByVal FullName As String)
        Fetch(DAClsprgAdministrativeUsers.Fetch(IDProfile, FullName))
    End Sub

    Private Sub Fetch(ByVal list As DAClsprgAdministrativeUsers.Struct())
        Me.RaiseListChangedEvents = False
        For Each Struct As DAClsprgAdministrativeUsers.Struct In list
            Me.Add(ClsUser.GetChildUser(Struct))
        Next
        Me.RaiseListChangedEvents = True
    End Sub

#End Region

#End Region

#Region " Exists "

    Public Shared Function Exists(ByVal idProfile As Long) As Boolean
        Return DataPortal.Execute(Of ExistsCommand)(New ExistsCommand(idProfile)).Exists
    End Function

    <Serializable()> Private Class ExistsCommand
        Inherits CommandBase

        Private mIDProfile As Long
        Private mExists As Boolean

        Public ReadOnly Property Exists() As Boolean
            Get
                Return Me.mExists
            End Get
        End Property

        Public Sub New(ByVal idProfile As Long)
            Me.mIDProfile = idProfile
        End Sub

        Protected Overrides Sub DataPortal_Execute()
            Me.mExists = DAClsprgAdministrativeUsers.FetchByProfile(Me.mIDProfile).Length > 0
        End Sub

    End Class

#End Region

End Class
