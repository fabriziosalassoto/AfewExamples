Imports Csla
Imports SCT.DataAccess

Namespace Advertiser
    <Serializable()> Public Class ClsTransactionReceiptInfoList
        Inherits ReadOnlyListBase(Of ClsTransactionReceiptInfoList, ClsTransactionReceiptInfo)

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole(" ")
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function NewTransactionReceiptInfoList() As ClsTransactionReceiptInfoList
            Return New ClsTransactionReceiptInfoList
        End Function

        Public Shared Function GetTransactionReceiptInfoList() As ClsTransactionReceiptInfoList
            Return DataPortal.Fetch(Of ClsTransactionReceiptInfoList)(New Criteria)
        End Function

        Public Shared Function GetTransactionReceiptInfoList(ByVal idAdvertiser As Long, ByVal idProject As Long, ByVal fromDate As Date, ByVal toDate As Date) As ClsTransactionReceiptInfoList
            Return DataPortal.Fetch(Of ClsTransactionReceiptInfoList)(New FilteredCriteria(idAdvertiser, idProject, fromDate, toDate))
        End Function

        Public Shared Function GetTransactionReceiptInfoList(ByVal idAdvertisers() As Long, ByVal idProjects() As Long, ByVal fromDate As Date, ByVal toDate As Date) As ClsTransactionReceiptInfoList
            Return DataPortal.Fetch(Of ClsTransactionReceiptInfoList)(New FilteredCriteriaList(idAdvertisers, idProjects, fromDate, toDate))
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

            Private mIDAdvertiser As Long
            Private mIDProject As Long
            Private mFromDate As Date
            Private mToDate As Date

            Public ReadOnly Property IDAdvertiser() As Long
                Get
                    Return Me.mIDAdvertiser
                End Get
            End Property

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

            Public Sub New(ByVal idAdvertiser As Long, ByVal idProject As Long, ByVal fromDate As Date, ByVal toDate As Date)
                Me.mIDAdvertiser = idAdvertiser
                Me.mIDProject = idProject
                Me.mFromDate = fromDate
                Me.mToDate = toDate
            End Sub

        End Class

        <Serializable()> Private Class FilteredCriteriaList

            Private mIDAdvertisers() As Long
            Private mIDProjects() As Long
            Private mFromDate As Date
            Private mToDate As Date

            Public ReadOnly Property IDAdvertisers() As Long()
                Get
                    Return Me.mIDAdvertisers
                End Get
            End Property

            Public ReadOnly Property IDProjects() As Long()
                Get
                    Return Me.mIDProjects
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

            Public Sub New(ByVal idAdvertisers() As Long, ByVal idProjects() As Long, ByVal fromDate As Date, ByVal toDate As Date)
                Me.mIDAdvertisers = idAdvertisers
                Me.mIDProjects = idProjects
                Me.mFromDate = fromDate
                Me.mToDate = toDate
            End Sub

        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            ' fetch with no filter
            Fetch()
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteria)
            Fetch(criteria.IDAdvertiser, criteria.IDProject, criteria.FromDate, criteria.ToDate)
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteriaList)
            Fetch(criteria.IDAdvertisers, criteria.IDProjects, criteria.FromDate, criteria.ToDate)
        End Sub

        Private Sub Fetch()
            Fetch(DAClsappAdvertiserTransactionReceipts.Fetch())
        End Sub

        Private Sub Fetch(ByVal idAdvertiser As Long, ByVal idProject As Long, ByVal fromdate As Date, ByVal todate As Date)
            Fetch(DAClsappAdvertiserTransactionReceipts.Fetch(idAdvertiser, idProject, fromdate, todate))
        End Sub

        Private Sub Fetch(ByVal idAdvertisers() As Long, ByVal idProjects() As Long, ByVal fromdate As Date, ByVal todate As Date)
            Fetch(DAClsappAdvertiserTransactionReceipts.Fetch(idAdvertisers, idProjects, fromdate, todate))
        End Sub

        Private Sub Fetch(ByVal list As DAClsappAdvertiserTransactionReceipts.Struct())
            RaiseListChangedEvents = False
            IsReadOnly = False
            For Each Struct As DAClsappAdvertiserTransactionReceipts.Struct In list
                Me.Add(ClsTransactionReceiptInfo.GetTransactionReceipt(Struct))
            Next
            IsReadOnly = True
            RaiseListChangedEvents = True
        End Sub

#End Region

    End Class
End Namespace
