Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsProfileInfoList
    Inherits ReadOnlyListBase(Of ClsProfileInfoList, ClsProfileInfo)

#Region " Authorization Rules "

    Public Shared Function CanGetObject() As Boolean
        Return True 'ApplicationContext.User.IsInRole(" ")
    End Function

#End Region

#Region " Factory Methods "

    Public Shared Function NewProfileInfoList() As ClsProfileInfoList
        Return New ClsProfileInfoList
    End Function

    Public Shared Function GetProfileInfoList() As ClsProfileInfoList
        Return DataPortal.Fetch(Of ClsProfileInfoList)(New Criteria)
    End Function

    Public Shared Function GetProfileInfoList(ByVal description As String) As ClsProfileInfoList
        Return DataPortal.Fetch(Of ClsProfileInfoList)(New FilteredCriteria(description))
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

        Public ReadOnly Property Description() As String
            Get
                Return Me.mDescription
            End Get
        End Property

        Public Sub New(ByVal description As String)
            Me.mDescription = description
        End Sub

    End Class

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
        ' fetch with no filter
        Fetch()
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteria)
        Fetch(criteria.Description)
    End Sub

    Private Sub Fetch()
        Fetch(DAClsprgAdministrativeProfiles.Fetch())
    End Sub

    Private Sub Fetch(ByVal description As String)
        Fetch(DAClsprgAdministrativeProfiles.Fetch(description))
    End Sub

    Private Sub Fetch(ByVal list As DAClsprgAdministrativeProfiles.Struct())
        RaiseListChangedEvents = False
        IsReadOnly = False
        For Each Struct As DAClsprgAdministrativeProfiles.Struct In List
            Me.Add(ClsProfileInfo.GetProfileInfo(Struct))
        Next
        IsReadOnly = True
        RaiseListChangedEvents = True
    End Sub

#End Region

End Class
