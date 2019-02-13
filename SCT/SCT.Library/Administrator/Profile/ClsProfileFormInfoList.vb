Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsProfileFormInfoList
    Inherits ReadOnlyListBase(Of ClsProfileFormInfoList, ClsProfileFormInfo)

#Region " Factory Methods "

    Public Shared Function NewProfileFormInfoList() As ClsProfileFormInfoList
        Return New ClsProfileFormInfoList
    End Function

    Public Shared Function GetProfileFormInfoList() As ClsProfileFormInfoList
        Return DataPortal.Fetch(Of ClsProfileFormInfoList)(New Criteria)
    End Function

    Public Shared Function GetProfileFormInfoList(ByVal IDProfile As Long) As ClsProfileFormInfoList
        Return DataPortal.Fetch(Of ClsProfileFormInfoList)(New FilteredCriteria(IDProfile))
    End Function

    Public Shared Function GetProfileFormInfoList(ByVal IDProfile() As Long) As ClsProfileFormInfoList
        Return DataPortal.Fetch(Of ClsProfileFormInfoList)(New FilteredCriteriaList(IDProfile))
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

        Public ReadOnly Property IDProfile() As Long
            Get
                Return Me.mIDProfile
            End Get
        End Property

        Public Sub New(ByVal IDProfile As Long)
            Me.mIDProfile = IDProfile
        End Sub

    End Class

    <Serializable()> Private Class FilteredCriteriaList

        Private mIDProfile() As Long

        Public ReadOnly Property IDProfile() As Long()
            Get
                Return Me.mIDProfile
            End Get
        End Property

        Public Sub New(ByVal IDProfile() As Long)
            Me.mIDProfile = IDProfile
        End Sub

    End Class

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
        ' fetch with no filter
        Fetch()
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteria)
        Fetch(criteria.IDProfile)
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteriaList)
        Fetch(criteria.IDProfile)
    End Sub

    Private Sub Fetch()
        Fetch(DAClsprgAdministrativeFormPermissions.Fetch())
    End Sub

    Private Sub Fetch(ByVal IDProfile As Long)
        Fetch(DAClsprgAdministrativeFormPermissions.Fetch(IDProfile, 0))
    End Sub

    Private Sub Fetch(ByVal IDProfile() As Long)
        Fetch(DAClsprgAdministrativeFormPermissions.Fetch(IDProfile, New Long() {}))
    End Sub

    Private Sub Fetch(ByVal list As DAClsprgAdministrativeFormPermissions.Struct())
        RaiseListChangedEvents = False
        IsReadOnly = False
        For Each Struct As DAClsprgAdministrativeFormPermissions.Struct In List
            Me.Add(ClsProfileFormInfo.GetProfileFormInfo(Struct))
        Next
        IsReadOnly = True
        RaiseListChangedEvents = True
    End Sub

#End Region

End Class
