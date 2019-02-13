Imports Csla
Imports SCT.DataAccess

Namespace Advertiser
    <Serializable()> Public Class ClsNoteInfoList
        Inherits ReadOnlyListBase(Of ClsNoteInfoList, ClsNoteInfo)

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole(" ")
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function NewNoteInfoList() As ClsNoteInfoList
            Return New ClsNoteInfoList
        End Function

        Public Shared Function GetNoteInfoList() As ClsNoteInfoList
            Return DataPortal.Fetch(Of ClsNoteInfoList)(New Criteria)
        End Function

        Public Shared Function GetNoteInfoList(ByVal idAdvertiser As Long, ByVal idContact As Long, ByVal fromDate As Date, ByVal toDate As Date) As ClsNoteInfoList
            Return DataPortal.Fetch(Of ClsNoteInfoList)(New FilteredCriteria(idAdvertiser, idContact, fromDate, toDate))
        End Function

        Public Shared Function GetNoteInfoList(ByVal idAdvertisers() As Long, ByVal idContacts() As Long, ByVal fromDate As Date, ByVal toDate As Date) As ClsNoteInfoList
            Return DataPortal.Fetch(Of ClsNoteInfoList)(New FilteredCriteriaList(idAdvertisers, idContacts, fromDate, toDate))
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
            Private mIDContact As Long
            Private mFromDate As Date
            Private mToDate As Date

            Public ReadOnly Property IDAdvertiser() As Long
                Get
                    Return Me.mIDAdvertiser
                End Get
            End Property

            Public ReadOnly Property IDContact() As Long
                Get
                    Return Me.mIDContact
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

            Public Sub New(ByVal idAdvertiser As Long, ByVal idContact As Long, ByVal fromDate As Date, ByVal toDate As Date)
                Me.mIDAdvertiser = idAdvertiser
                Me.mIDContact = idContact
                Me.mFromDate = fromDate
                Me.mToDate = toDate
            End Sub

        End Class

        <Serializable()> Private Class FilteredCriteriaList

            Private mIDAdvertisers() As Long
            Private mIDContacts() As Long
            Private mFromDate As Date
            Private mToDate As Date

            Public ReadOnly Property IDAdvertisers() As Long()
                Get
                    Return Me.mIDAdvertisers
                End Get
            End Property

            Public ReadOnly Property IDContacts() As Long()
                Get
                    Return Me.mIDContacts
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

            Public Sub New(ByVal idAdvertisers() As Long, ByVal idContacts() As Long, ByVal fromDate As Date, ByVal toDate As Date)
                Me.mIDAdvertisers = idAdvertisers
                Me.mIDContacts = idContacts
                Me.mFromDate = fromDate
                Me.mToDate = toDate
            End Sub

        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            ' fetch with no filter
            Fetch()
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteria)
            Fetch(criteria.IDAdvertiser, criteria.IDContact, criteria.FromDate, criteria.ToDate)
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteriaList)
            Fetch(criteria.IDAdvertisers, criteria.IDContacts, criteria.FromDate, criteria.ToDate)
        End Sub

        Private Sub Fetch()
            Fetch(DAClsappAdvertiserContactNotes.Fetch())
        End Sub

        Private Sub Fetch(ByVal idAdvertiser As Long, ByVal idContact As Long, ByVal fromdate As Date, ByVal todate As Date)
            Fetch(DAClsappAdvertiserContactNotes.Fetch(idAdvertiser, idContact, fromdate, todate))
        End Sub

        Private Sub Fetch(ByVal idAdvertisers() As Long, ByVal idContacts() As Long, ByVal fromdate As Date, ByVal todate As Date)
            Fetch(DAClsappAdvertiserContactNotes.Fetch(idAdvertisers, idContacts, fromdate, todate))
        End Sub

        Private Sub Fetch(ByVal list As DAClsappAdvertiserContactNotes.Struct())
            RaiseListChangedEvents = False
            IsReadOnly = False
            For Each Struct As DAClsappAdvertiserContactNotes.Struct In list
                Me.Add(ClsNoteInfo.GetNoteInfo(Struct))
            Next
            IsReadOnly = True
            RaiseListChangedEvents = True
        End Sub

#End Region

    End Class
End Namespace