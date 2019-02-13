Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsFormInfoList
    Inherits ReadOnlyListBase(Of ClsFormInfoList, ClsFormInfo)

#Region " Authorization Rules "

    Public Shared Function CanGetObject() As Boolean
        Return True 'ApplicationContext.User.IsInRole(" ")
    End Function

#End Region

#Region " Factory Methods "

    Public Shared Function NewFormInfoList() As ClsFormInfoList
        Return New ClsFormInfoList
    End Function

    Public Shared Function GetFormInfoList() As ClsFormInfoList
        Return DataPortal.Fetch(Of ClsFormInfoList)(New Criteria)
    End Function

    Public Shared Function GetFormInfoList(ByVal description As String, ByVal logDescription As String) As ClsFormInfoList
        Return DataPortal.Fetch(Of ClsFormInfoList)(New FilteredCriteria(description, logDescription))
    End Function

    Public Shared Function GetFormInfoList(ByVal description As String, ByVal logDescription() As String) As ClsFormInfoList
        Return DataPortal.Fetch(Of ClsFormInfoList)(New FilteredCriteriaList(description, logDescription))
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

        Private mDescription As String
        Private mLogDescription As String

        Public ReadOnly Property Description() As String
            Get
                Return Me.mDescription
            End Get
        End Property

        Public ReadOnly Property LogDescription() As String
            Get
                Return Me.mLogDescription
            End Get
        End Property

        Public Sub New(ByVal description As String, ByVal logDescription As String)
            Me.mDescription = description
            Me.mLogDescription = logDescription
        End Sub

    End Class

    <Serializable()> Private Class FilteredCriteriaList

        Private mDescription As String
        Private mLogDescription() As String

        Public ReadOnly Property Description() As String
            Get
                Return Me.mDescription
            End Get
        End Property

        Public ReadOnly Property LogDescription() As String()
            Get
                Return Me.mLogDescription
            End Get
        End Property

        Public Sub New(ByVal description As String, ByVal logDescription() As String)
            Me.mDescription = description
            Me.mLogDescription = logDescription
        End Sub

    End Class

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
        ' fetch with no filter
        Fetch()
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteria)
        Fetch(criteria.Description, criteria.LogDescription)
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteriaList)
        Fetch(criteria.Description, criteria.LogDescription)
    End Sub

    Private Sub Fetch()
        Fetch(DAClsprgForms.Fetch())
    End Sub

    Private Sub Fetch(ByVal description As String, ByVal logDescription As String)
        Fetch(DAClsprgForms.Fetch(description, logDescription))
    End Sub

    Private Sub Fetch(ByVal description As String, ByVal logDescription() As String)
        Fetch(DAClsprgForms.Fetch(description, logDescription))
    End Sub

    Private Sub Fetch(ByVal list As DAClsprgForms.Struct())
        RaiseListChangedEvents = False
        IsReadOnly = False
        For Each Struct As DAClsprgForms.Struct In List
            Me.Add(ClsFormInfo.GetFormInfo(Struct))
        Next
        IsReadOnly = True
        RaiseListChangedEvents = True
    End Sub

#End Region

End Class
