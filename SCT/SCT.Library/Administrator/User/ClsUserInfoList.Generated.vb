Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsUserInfoList
    Inherits ReadOnlyListBase(Of ClsUserInfoList, ClsUserInfo)

#Region " Authorization Rules "

    Public Shared Function CanGetObject() As Boolean
        Return True 'ApplicationContext.User.IsInRole(" ")
    End Function

#End Region

#Region " Factory Methods "

    Public Shared Function NewUserInfoList() As ClsUserInfoList
        Return New ClsUserInfoList
    End Function

    Public Shared Function GetUserInfoList() As ClsUserInfoList
        Return DataPortal.Fetch(Of ClsUserInfoList)(New Criteria)
    End Function

    Public Shared Function GetUserInfoList(ByVal IDProfile As Long, ByVal FullName As String) As ClsUserInfoList
        Return DataPortal.Fetch(Of ClsUserInfoList)(New FilteredCriteria(IDProfile, FullName))
    End Function

    Public Shared Function GetUserInfoList(ByVal IDProfile() As Long, ByVal FullName As String) As ClsUserInfoList
        Return DataPortal.Fetch(Of ClsUserInfoList)(New FilteredCriteriaList(IDProfile, FullName))
    End Function

    Private Sub New()
        ' Require use of factory methods
    End Sub

#End Region

#Region " Data Access "

    <Serializable()> Private Class Criteria
        ' no criteria - retrieve all records
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

        Public Sub New(ByVal IDProfile As Long, ByVal FullName As String)
            Me.mIDProfile = IDProfile
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

        Public Sub New(ByVal IDProfile() As Long, ByVal FullName As String)
            Me.mIDProfile = IDProfile
            Me.mFullName = FullName
        End Sub

    End Class

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
        ' fetch with no filter
        Fetch()
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteria)
        Fetch(criteria.IDProfile, criteria.FullName)
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteriaList)
        Fetch(criteria.IDProfile, criteria.FullName)
    End Sub

    Private Sub Fetch()
        Fetch(DAClsprgAdministrativeUsers.Fetch())
    End Sub

    Private Sub Fetch(ByVal IDProfile As Long, ByVal fullName As String)
        Fetch(DAClsprgAdministrativeUsers.Fetch(IDProfile, fullName))
    End Sub

    Private Sub Fetch(ByVal idProfile() As Long, ByVal fullName As String)
        Fetch(DAClsprgAdministrativeUsers.Fetch(idProfile, fullName))
    End Sub

    Private Sub Fetch(ByVal list As DAClsprgAdministrativeUsers.Struct())
        RaiseListChangedEvents = False
        IsReadOnly = False
        For Each Struct As DAClsprgAdministrativeUsers.Struct In List
            Me.Add(ClsUserInfo.GetUserInfo(Struct))
        Next
        IsReadOnly = True
        RaiseListChangedEvents = True
    End Sub

#End Region

End Class
