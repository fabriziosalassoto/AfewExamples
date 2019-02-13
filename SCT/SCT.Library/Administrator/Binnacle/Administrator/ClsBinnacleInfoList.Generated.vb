Imports Csla
Imports SCT.DataAccess

Namespace Administrator
    <Serializable()> Public Class ClsBinnacleInfoList
        Inherits ReadOnlyListBase(Of ClsBinnacleInfoList, ClsBinnacleInfo)

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole(" ")
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function NewBinnacleInfoList() As ClsBinnacleInfoList
            Return New ClsBinnacleInfoList
        End Function

        Public Shared Function GetBinnacleInfoList() As ClsBinnacleInfoList
            Return DataPortal.Fetch(Of ClsBinnacleInfoList)(New Criteria)
        End Function

        Public Shared Function GetBinnacleInfoList(ByVal idUser As SearchCriteria(Of Long), ByVal fromDate As SearchCriteria(Of Date), ByVal toDate As SearchCriteria(Of Date)) As ClsBinnacleInfoList
            Return DataPortal.Fetch(Of ClsBinnacleInfoList)(New FilteredCriteria(idUser, fromDate, toDate))
        End Function

        Public Shared Function GetBinnacleInfoList(ByVal idUser As SearchCriteriaList(Of Long), ByVal fromDate As SearchCriteria(Of Date), ByVal toDate As SearchCriteria(Of Date)) As ClsBinnacleInfoList
            Return DataPortal.Fetch(Of ClsBinnacleInfoList)(New FilteredCriteriaList(idUser, fromDate, toDate))
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

            Private mIDUser As SearchCriteria(Of Long)
            Private mFromDate As SearchCriteria(Of Date)
            Private mToDate As SearchCriteria(Of Date)

            Public ReadOnly Property IDUser() As SearchCriteria(Of Long)
                Get
                    Return Me.mIDUser
                End Get
            End Property

            Public ReadOnly Property FromDate() As SearchCriteria(Of Date)
                Get
                    Return Me.mFromDate
                End Get
            End Property

            Public ReadOnly Property ToDate() As SearchCriteria(Of Date)
                Get
                    Return Me.mToDate
                End Get
            End Property

            Public Sub New(ByVal idUser As SearchCriteria(Of Long), ByVal fromDate As SearchCriteria(Of Date), ByVal toDate As SearchCriteria(Of Date))
                Me.mIDUser = idUser
                Me.mFromDate = fromDate
                Me.mToDate = toDate
            End Sub

        End Class

        <Serializable()> Private Class FilteredCriteriaList

            Private mIDUser As SearchCriteriaList(Of Long)
            Private mFromDate As SearchCriteria(Of Date)
            Private mToDate As SearchCriteria(Of Date)

            Public ReadOnly Property IDUser() As SearchCriteriaList(Of Long)
                Get
                    Return Me.mIDUser
                End Get
            End Property

            Public ReadOnly Property FromDate() As SearchCriteria(Of Date)
                Get
                    Return Me.mFromDate
                End Get
            End Property

            Public ReadOnly Property ToDate() As SearchCriteria(Of Date)
                Get
                    Return Me.mToDate
                End Get
            End Property

            Public Sub New(ByVal idUser As SearchCriteriaList(Of Long), ByVal fromDate As SearchCriteria(Of Date), ByVal toDate As SearchCriteria(Of Date))
                Me.mIDUser = idUser
                Me.mFromDate = fromDate
                Me.mToDate = toDate
            End Sub

        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            ' fetch with no filter
            Fetch()
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteria)
            Fetch(criteria.IDUser, criteria.FromDate, criteria.ToDate)
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteriaList)
            Fetch(criteria.IDUser, criteria.FromDate, criteria.ToDate)
        End Sub

        Private Sub Fetch()
            Fetch(DAClsprgAdministrativeBinnacle.Fetch())
        End Sub

        Private Sub Fetch(ByVal idUser As SearchCriteria(Of Long), ByVal fromDate As SearchCriteria(Of Date), ByVal toDate As SearchCriteria(Of Date))
            Fetch(DAClsprgAdministrativeBinnacle.Fetch(New Parameter(Of Long)(idUser.Value, idUser.Priority), New Parameter(Of Date)(fromDate.Value, fromDate.Priority), New Parameter(Of Date)(toDate.Value, toDate.Priority)))
        End Sub

        Private Sub Fetch(ByVal idUser As SearchCriteriaList(Of Long), ByVal fromDate As SearchCriteria(Of Date), ByVal toDate As SearchCriteria(Of Date))
            Fetch(DAClsprgAdministrativeBinnacle.Fetch(New ParameterList(Of Long)(idUser.Values, idUser.Priority), New Parameter(Of Date)(fromDate.Value, fromDate.Priority), New Parameter(Of Date)(toDate.Value, toDate.Priority)))
        End Sub

        Private Sub Fetch(ByVal list As DAClsprgAdministrativeBinnacle.Struct())

            RaiseListChangedEvents = False
            IsReadOnly = False
            For Each Struct As DAClsprgAdministrativeBinnacle.Struct In list
                Me.Add(ClsBinnacleInfo.GetBinnacleInfo(Struct))
            Next
            IsReadOnly = True
            RaiseListChangedEvents = True
        End Sub

#End Region

    End Class
End Namespace