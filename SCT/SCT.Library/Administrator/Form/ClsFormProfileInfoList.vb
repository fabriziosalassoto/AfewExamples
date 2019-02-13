Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsFormProfileInfoList
    Inherits ReadOnlyListBase(Of ClsFormProfileInfoList, ClsFormProfileInfo)

#Region " Factory Methods "

    Public Shared Function NewFormProfileInfoList() As ClsFormProfileInfoList
        Return New ClsFormProfileInfoList
    End Function

    Public Shared Function GetFormProfileInfoList() As ClsFormProfileInfoList
        Return DataPortal.Fetch(Of ClsFormProfileInfoList)(New Criteria)
    End Function

    Public Shared Function GetFormProfileInfoList(ByVal IDForm As Long) As ClsFormProfileInfoList
        Return DataPortal.Fetch(Of ClsFormProfileInfoList)(New FilteredCriteria(IDForm))
    End Function

    Public Shared Function GetFormProfileInfoList(ByVal IDForm() As Long) As ClsFormProfileInfoList
        Return DataPortal.Fetch(Of ClsFormProfileInfoList)(New FilteredCriteriaList(IDForm))
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

        Private mIDForm As Long

        Public ReadOnly Property IDForm() As Long
            Get
                Return Me.mIDForm
            End Get
        End Property

        Public Sub New(ByVal IDForm As Long)
            Me.mIDForm = IDForm
        End Sub

    End Class

    <Serializable()> Private Class FilteredCriteriaList

        Private mIDForm() As Long

        Public ReadOnly Property IDForm() As Long()
            Get
                Return Me.mIDForm
            End Get
        End Property

        Public Sub New(ByVal IDForm() As Long)
            Me.mIDForm = IDForm
        End Sub

    End Class

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
        ' fetch with no filter
        Fetch()
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteria)
        Fetch(criteria.IDForm)
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteriaList)
        Fetch(criteria.IDForm)
    End Sub

    Private Sub Fetch()
        Fetch(DAClsprgAdministrativeFormPermissions.Fetch())
    End Sub

    Private Sub Fetch(ByVal IDForm As Long)
        Fetch(DAClsprgAdministrativeFormPermissions.Fetch(0, IDForm))
    End Sub

    Private Sub Fetch(ByVal IDForm() As Long)
        Fetch(DAClsprgAdministrativeFormPermissions.Fetch(New Long() {}, IDForm))
    End Sub

    Private Sub Fetch(ByVal list As DAClsprgAdministrativeFormPermissions.Struct())
        RaiseListChangedEvents = False
        IsReadOnly = False
        For Each Struct As DAClsprgAdministrativeFormPermissions.Struct In List
            Me.Add(ClsFormProfileInfo.GetFormProfileInfo(Struct))
        Next
        IsReadOnly = True
        RaiseListChangedEvents = True
    End Sub

#End Region

End Class
