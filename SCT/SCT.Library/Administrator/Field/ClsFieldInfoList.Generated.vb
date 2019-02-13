Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsFieldInfoList
    Inherits ReadOnlyListBase(Of ClsFieldInfoList, ClsFieldInfo)

#Region " Business Methods "

    Public Function GetItem(ByVal description As String) As ClsFieldInfo
        For Each field As ClsFieldInfo In Me
            If field.Description = description Then
                Return field
            End If
        Next
        Return Nothing
    End Function

    Public Overloads Function Contains(ByVal description As String) As Boolean
        For Each field As ClsFieldInfo In Me
            If field.Description = description Then
                Return True
            End If
        Next
        Return False
    End Function

#End Region

#Region " Authorization Rules "

    Public Shared Function CanGetObject() As Boolean
        Return True 'ApplicationContext.User.IsInRole(" ")
    End Function

#End Region

#Region " Factory Methods "

    Public Shared Function NewFieldInfoList() As ClsFieldInfoList
        Return New ClsFieldInfoList
    End Function

    Public Shared Function GetFieldInfoList() As ClsFieldInfoList
        Return DataPortal.Fetch(Of ClsFieldInfoList)(New Criteria)
    End Function

    Public Shared Function GetFieldInfoList(ByVal IDForm As Long, ByVal IDGroup As Long, ByVal description As String) As ClsFieldInfoList
        Return DataPortal.Fetch(Of ClsFieldInfoList)(New FilteredCriteria(IDForm, IDGroup, description))
    End Function

    Public Shared Function GetFieldInfoList(ByVal IDForm() As Long, ByVal IDGroup() As Long, ByVal description As String) As ClsFieldInfoList
        Return DataPortal.Fetch(Of ClsFieldInfoList)(New FilteredCriteriaList(IDForm, IDGroup, description))
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
        Private mIDGroup As Long
        Private mDescription As String

        Public ReadOnly Property IDForm() As Long
            Get
                Return Me.mIDForm
            End Get
        End Property

        Public ReadOnly Property IDGroup() As Long
            Get
                Return Me.mIDGroup
            End Get
        End Property

        Public ReadOnly Property Description() As String
            Get
                Return Me.mDescription
            End Get
        End Property

        Public Sub New(ByVal idForm As Long, ByVal idGroup As Long, ByVal description As String)
            Me.mIDForm = idForm
            Me.mIDGroup = idGroup
            Me.mDescription = description
        End Sub

    End Class

    <Serializable()> Private Class FilteredCriteriaList

        Private mIDForm() As Long
        Private mIDGroup() As Long
        Private mDescription As String

        Public ReadOnly Property IDForm() As Long()
            Get
                Return Me.mIDForm
            End Get
        End Property

        Public ReadOnly Property IDGroup() As Long()
            Get
                Return Me.mIDGroup
            End Get
        End Property

        Public ReadOnly Property Description() As String
            Get
                Return Me.mDescription
            End Get
        End Property

        Public Sub New(ByVal idForm() As Long, ByVal idGroup() As Long, ByVal description As String)
            Me.mIDForm = idForm
            Me.mIDGroup = idGroup
            Me.mDescription = description
        End Sub

    End Class

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
        ' fetch with no filter
        Fetch()
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteria)
        Fetch(criteria.IDForm, criteria.IDGroup, criteria.Description)
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteriaList)
        Fetch(criteria.IDForm, criteria.IDGroup, criteria.Description)
    End Sub

    Private Sub Fetch()
        Fetch(DAClsprgFields.Fetch())
    End Sub

    Private Sub Fetch(ByVal idForm As Long, ByVal idGroup As Long, ByVal description As String)
        Fetch(DAClsprgFields.Fetch(idGroup, idForm, description))
    End Sub

    Private Sub Fetch(ByVal idForm() As Long, ByVal idGroup() As Long, ByVal description As String)
        Fetch(DAClsprgFields.Fetch(idGroup, idForm, description))
    End Sub

    Private Sub Fetch(ByVal list As DAClsprgFields.Struct())
        RaiseListChangedEvents = False
        IsReadOnly = False
        For Each Struct As DAClsprgFields.Struct In list
            Me.Add(ClsFieldInfo.GetFieldInfo(Struct))
        Next
        IsReadOnly = True
        RaiseListChangedEvents = True
    End Sub

#End Region

End Class
