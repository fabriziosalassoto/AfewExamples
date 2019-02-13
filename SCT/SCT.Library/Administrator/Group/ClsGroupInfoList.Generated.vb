Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsGroupInfoList
    Inherits ReadOnlyListBase(Of ClsGroupInfoList, ClsGroupInfo)

#Region " Authorization Rules "

    Public Shared Function CanGetObject() As Boolean
        Return True 'ApplicationContext.User.IsInRole(" ")
    End Function

#End Region

#Region " Factory Methods "

    Public Shared Function NewGroupInfoList() As ClsGroupInfoList
        Return New ClsGroupInfoList
    End Function

    Public Shared Function GetGroupInfoList() As ClsGroupInfoList
        Return DataPortal.Fetch(Of ClsGroupInfoList)(New Criteria)
    End Function

    Public Shared Function GetGroupInfoList(ByVal idForm As Long, ByVal description As String) As ClsGroupInfoList
        Return DataPortal.Fetch(Of ClsGroupInfoList)(New FilteredCriteria(idForm, description))
    End Function

    Public Shared Function GetGroupInfoList(ByVal idForm() As Long, ByVal description As String) As ClsGroupInfoList
        Return DataPortal.Fetch(Of ClsGroupInfoList)(New FilteredCriteriaList(idForm, description))
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
        Private mDescription As String

        Public ReadOnly Property IDForm() As Long
            Get
                Return Me.mIDForm
            End Get
        End Property

        Public ReadOnly Property Description() As String
            Get
                Return Me.mDescription
            End Get
        End Property

        Public Sub New(ByVal idForm As Long, ByVal description As String)
            Me.mIDForm = idForm
            Me.mDescription = description
        End Sub

    End Class

    <Serializable()> Private Class FilteredCriteriaList

        Private mIDForm() As Long
        Private mDescription As String

        Public ReadOnly Property IDForm() As Long()
            Get
                Return Me.mIDForm
            End Get
        End Property

        Public ReadOnly Property Description() As String
            Get
                Return Me.mDescription
            End Get
        End Property

        Public Sub New(ByVal idForm() As Long, ByVal description As String)
            Me.mIDForm = idForm
            Me.mDescription = description
        End Sub

    End Class

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
        ' fetch with no filter
        Fetch()
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteria)
        Fetch(criteria.IDForm, criteria.Description)
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteriaList)
        Fetch(criteria.IDForm, criteria.Description)
    End Sub

    Private Sub Fetch()
        Fetch(DAClsprgGroups.Fetch())
    End Sub

    Private Sub Fetch(ByVal idForm As Long, ByVal description As String)
        Fetch(DAClsprgGroups.Fetch(idForm, description))
    End Sub

    Private Sub Fetch(ByVal idForm() As Long, ByVal description As String)
        Fetch(DAClsprgGroups.Fetch(idForm, description))
    End Sub

    Private Sub Fetch(ByVal list As DAClsprgGroups.Struct())
        RaiseListChangedEvents = False
        IsReadOnly = False
        For Each Struct As DAClsprgGroups.Struct In list
            Me.Add(ClsGroupInfo.GetGroupInfo(Struct))
        Next
        IsReadOnly = True
        RaiseListChangedEvents = True
    End Sub

#End Region

End Class
