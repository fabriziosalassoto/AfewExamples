Imports Csla
Imports SCT.DataAccess

Namespace Advertiser.Project
    <Serializable()> Public Class ClsInvoiceInfoList
        Inherits ReadOnlyListBase(Of ClsInvoiceInfoList, ClsInvoiceInfo)

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole(" ")
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function NewInvoiceInfoList() As ClsInvoiceInfoList
            Return New ClsInvoiceInfoList
        End Function

        Public Shared Function GetInvoiceInfoList() As ClsInvoiceInfoList
            Return DataPortal.Fetch(Of ClsInvoiceInfoList)(New Criteria)
        End Function

        Public Shared Function GetInvoiceInfoList(ByVal idProject As Long, ByVal fromDate As Date, ByVal toDate As Date) As ClsInvoiceInfoList
            Return DataPortal.Fetch(Of ClsInvoiceInfoList)(New FilteredCriteria(idProject, fromDate, toDate))
        End Function

        Public Shared Function GetInvoiceInfoList(ByVal idProject() As Long, ByVal fromDate As Date, ByVal toDate As Date) As ClsInvoiceInfoList
            Return DataPortal.Fetch(Of ClsInvoiceInfoList)(New FilteredCriteriaList(idProject, fromDate, toDate))
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

            Private mIDProject As Long
            Private mFromDate As Date
            Private mToDate As Date

            Public ReadOnly Property IDProject() As Long
                Get
                    Return Me.mIDProject
                End Get
            End Property

            Public ReadOnly Property FromDate() As Date
                Get
                    Return Me.mFromDate
                End Get
            End Property

            Public ReadOnly Property ToDate() As Date
                Get
                    Return Me.mToDate
                End Get
            End Property

            Public Sub New(ByVal idProject As Long, ByVal fromDate As Date, ByVal toDate As Date)
                Me.mIDProject = idProject
                Me.mFromDate = fromDate
                Me.mToDate = toDate
            End Sub

        End Class

        <Serializable()> Private Class FilteredCriteriaList

            Private mIDProject() As Long
            Private mFromDate As Date
            Private mToDate As Date

            Public ReadOnly Property IDProject() As Long()
                Get
                    Return Me.mIDProject
                End Get
            End Property

            Public ReadOnly Property FromDate() As Date
                Get
                    Return Me.mFromDate
                End Get
            End Property

            Public ReadOnly Property ToDate() As Date
                Get
                    Return Me.mToDate
                End Get
            End Property

            Public Sub New(ByVal idProject() As Long, ByVal fromDate As Date, ByVal toDate As Date)
                Me.mIDProject = idProject
                Me.mFromDate = fromDate
                Me.mToDate = toDate
            End Sub

        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            ' fetch with no filter
            Fetch()
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteria)
            Fetch(criteria.IDProject, criteria.FromDate, criteria.ToDate)
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteriaList)
            Fetch(criteria.IDProject, criteria.FromDate, criteria.ToDate)
        End Sub

        Private Sub Fetch()
            Fetch(DAClsappAdvertiserProjectInvoices.Fetch())
        End Sub

        Private Sub Fetch(ByVal idProject As Long, ByVal fromDate As Date, ByVal toDate As Date)
            Fetch(DAClsappAdvertiserProjectInvoices.Fetch(idProject, fromDate, toDate))
        End Sub

        Private Sub Fetch(ByVal idProject() As Long, ByVal fromDate As Date, ByVal toDate As Date)
            Fetch(DAClsappAdvertiserProjectInvoices.Fetch(idProject, fromDate, toDate))
        End Sub

        Private Sub Fetch(ByVal list As DAClsappAdvertiserProjectInvoices.Struct())
            RaiseListChangedEvents = False
            IsReadOnly = False
            For Each Struct As DAClsappAdvertiserProjectInvoices.Struct In list
                Me.Add(ClsInvoiceInfo.GetInvoiceInfo(Struct))
            Next
            IsReadOnly = True
            RaiseListChangedEvents = True
        End Sub

#End Region

    End Class
End Namespace

