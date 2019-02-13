Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsGroupProfileInfoList
    Inherits ReadOnlyListBase(Of ClsGroupProfileInfoList, ClsGroupProfileInfo)

#Region " Factory Methods "

    Public Shared Function NewGroupProfileInfoList() As ClsGroupProfileInfoList
        Return New ClsGroupProfileInfoList
    End Function

    Public Shared Function GetGroupProfileInfoList() As ClsGroupProfileInfoList
        Return DataPortal.Fetch(Of ClsGroupProfileInfoList)(New Criteria)
    End Function

    Public Shared Function GetGroupProfileInfoList(ByVal IDGroup As Long, ByVal IDForm As Long) As ClsGroupProfileInfoList
        Return DataPortal.Fetch(Of ClsGroupProfileInfoList)(New FilteredCriteria(IDGroup, IDForm))
    End Function

    Public Shared Function GetGroupProfileInfoList(ByVal IDGroup() As Long, ByVal IDForm() As Long) As ClsGroupProfileInfoList
        Return DataPortal.Fetch(Of ClsGroupProfileInfoList)(New FilteredCriteriaList(IDGroup, IDForm))
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

        Private mIDGroup As Long
        Private mIDForm As Long

        Public ReadOnly Property IDGroup() As Long
            Get
                Return Me.mIDGroup
            End Get
        End Property

        Public ReadOnly Property IDForm() As Long
            Get
                Return Me.mIDForm
            End Get
        End Property

        Public Sub New(ByVal IDGroup As Long, ByVal IDForm As Long)
            Me.mIDGroup = IDGroup
            Me.mIDForm = IDForm
        End Sub

    End Class

    <Serializable()> Private Class FilteredCriteriaList

        Private mIDGroup() As Long
        Private mIDForm() As Long

        Public ReadOnly Property IDGroup() As Long()
            Get
                Return Me.mIDGroup
            End Get
        End Property

        Public ReadOnly Property IDForm() As Long()
            Get
                Return Me.mIDForm
            End Get
        End Property

        Public Sub New(ByVal IDGroup() As Long, ByVal IDForm() As Long)
            Me.mIDGroup = IDGroup
            Me.mIDForm = IDForm
        End Sub

    End Class

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
        ' fetch with no filter
        Fetch()
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteria)
        Fetch(criteria.IDGroup, criteria.IDForm)
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteriaList)
        Fetch(criteria.IDGroup, criteria.IDForm)
    End Sub

    Private Sub Fetch()
        Fetch(DAClsprgAdministrativeGroupPermissions.Fetch())
    End Sub

    Private Sub Fetch(ByVal IDGroup As Long, ByVal IDForm As Long)
        Fetch(DAClsprgAdministrativeGroupPermissions.Fetch(IDGroup, 0, IDForm))
    End Sub

    Private Sub Fetch(ByVal IDGroup() As Long, ByVal IDForm() As Long)
        Fetch(DAClsprgAdministrativeGroupPermissions.Fetch(IDGroup, New Long() {}, IDForm))
    End Sub

    Private Sub Fetch(ByVal list As DAClsprgAdministrativeGroupPermissions.Struct())
        RaiseListChangedEvents = False
        IsReadOnly = False
        For Each Struct As DAClsprgAdministrativeGroupPermissions.Struct In List
            Me.Add(ClsGroupProfileInfo.GetGroupProfileInfo(Struct))
        Next
        IsReadOnly = True
        RaiseListChangedEvents = True
    End Sub

#End Region

End Class
