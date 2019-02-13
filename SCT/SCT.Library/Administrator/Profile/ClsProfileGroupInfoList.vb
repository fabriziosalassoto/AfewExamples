Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsProfileGroupInfoList
    Inherits ReadOnlyListBase(Of ClsProfileGroupInfoList, ClsProfileGroupInfo)

#Region " Factory Methods "

    Public Shared Function NewProfileFormGroupInfoList() As ClsProfileGroupInfoList
        Return New ClsProfileGroupInfoList
    End Function

    Public Shared Function GetProfileFormGroupInfoList() As ClsProfileGroupInfoList
        Return DataPortal.Fetch(Of ClsProfileGroupInfoList)(New Criteria)
    End Function

    Public Shared Function GetProfileFormGroupInfoList(ByVal IDProfile As Long, ByVal IDForm As Long) As ClsProfileGroupInfoList
        Return DataPortal.Fetch(Of ClsProfileGroupInfoList)(New FilteredCriteria(IDProfile, IDForm))
    End Function

    Public Shared Function GetProfileFormGroupInfoList(ByVal IDProfile() As Long, ByVal IDForm() As Long) As ClsProfileGroupInfoList
        Return DataPortal.Fetch(Of ClsProfileGroupInfoList)(New FilteredCriteriaList(IDProfile, IDForm))
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
        Private mIDForm As Long

        Public ReadOnly Property IDProfile() As Long
            Get
                Return Me.mIDProfile
            End Get
        End Property

        Public ReadOnly Property IDForm() As Long
            Get
                Return Me.mIDForm
            End Get
        End Property

        Public Sub New(ByVal IDProfile As Long, ByVal IDForm As Long)
            Me.mIDProfile = IDProfile
            Me.mIDForm = IDForm
        End Sub

    End Class

    <Serializable()> Private Class FilteredCriteriaList

        Private mIDProfile() As Long
        Private mIDForm() As Long

        Public ReadOnly Property IDProfile() As Long()
            Get
                Return Me.mIDProfile
            End Get
        End Property

        Public ReadOnly Property IDForm() As Long()
            Get
                Return Me.mIDForm
            End Get
        End Property

        Public Sub New(ByVal IDProfile() As Long, ByVal IDForm() As Long)
            Me.mIDProfile = IDProfile
            Me.mIDForm = IDForm
        End Sub

    End Class

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
        ' fetch with no filter
        Fetch()
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteria)
        Fetch(criteria.IDProfile, criteria.IDForm)
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteriaList)
        Fetch(criteria.IDProfile, criteria.IDForm)
    End Sub

    Private Sub Fetch()
        Fetch(DAClsprgAdministrativeGroupPermissions.Fetch())
    End Sub

    Private Sub Fetch(ByVal IDProfile As Long, ByVal IDForm As Long)
        Fetch(DAClsprgAdministrativeGroupPermissions.Fetch(0, IDProfile, IDForm))
    End Sub

    Private Sub Fetch(ByVal IDProfile() As Long, ByVal IDForm() As Long)
        Fetch(DAClsprgAdministrativeGroupPermissions.Fetch(New Long() {}, IDProfile, IDForm))
    End Sub

    Private Sub Fetch(ByVal list As DAClsprgAdministrativeGroupPermissions.Struct())
        RaiseListChangedEvents = False
        IsReadOnly = False
        For Each Struct As DAClsprgAdministrativeGroupPermissions.Struct In List
            Me.Add(ClsProfileGroupInfo.GetProfileFormGroupInfo(Struct))
        Next
        IsReadOnly = True
        RaiseListChangedEvents = True
    End Sub

#End Region

End Class
