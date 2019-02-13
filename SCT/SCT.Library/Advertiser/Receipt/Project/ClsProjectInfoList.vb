Imports Csla
Imports SCT.DataAccess

Namespace Advertiser.Receipt
    <Serializable()> Public Class ClsProjectInfoList
        Inherits ReadOnlyListBase(Of ClsProjectInfoList, ClsProjectInfo)

#Region " Factory Methods "

        Public Shared Function NewReceiptProjectInfoList() As ClsProjectInfoList
            Return New ClsProjectInfoList
        End Function

        Public Shared Function GetReceiptProjectInfoList() As ClsProjectInfoList
            Return DataPortal.Fetch(Of ClsProjectInfoList)(New Criteria)
        End Function

        Public Shared Function GetReceiptProjectInfoList(ByVal IDReceipt As Long) As ClsProjectInfoList
            Return DataPortal.Fetch(Of ClsProjectInfoList)(New FilteredCriteria(IDReceipt))
        End Function

        Public Shared Function GetReceiptProjectInfoList(ByVal IDReceipt() As Long) As ClsProjectInfoList
            Return DataPortal.Fetch(Of ClsProjectInfoList)(New FilteredCriteriaList(IDReceipt))
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

            Private mIDReceipt As Long

            Public ReadOnly Property IDReceipt() As Long
                Get
                    Return Me.mIDReceipt
                End Get
            End Property

            Public Sub New(ByVal IDReceipt As Long)
                Me.mIDReceipt = IDReceipt
            End Sub

        End Class

        <Serializable()> Private Class FilteredCriteriaList

            Private mIDReceipt() As Long

            Public ReadOnly Property IDReceipt() As Long()
                Get
                    Return Me.mIDReceipt
                End Get
            End Property

            Public Sub New(ByVal IDReceipt() As Long)
                Me.mIDReceipt = IDReceipt
            End Sub

        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            ' fetch with no filter
            Fetch()
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteria)
            Fetch(criteria.IDReceipt)
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteriaList)
            Fetch(criteria.IDReceipt)
        End Sub

        Private Sub Fetch()
            Fetch(DAClsappAdvertiserProjectReceipts.Fetch())
        End Sub

        Private Sub Fetch(ByVal IDReceipt As Long)
            Fetch(DAClsappAdvertiserProjectReceipts.Fetch(0, IDReceipt))
        End Sub

        Private Sub Fetch(ByVal IDReceipt() As Long)
            Fetch(DAClsappAdvertiserProjectReceipts.Fetch(New Long() {}, IDReceipt))
        End Sub

        Private Sub Fetch(ByVal list As DAClsappAdvertiserProjectReceipts.Struct())
            RaiseListChangedEvents = False
            IsReadOnly = False
            For Each Struct As DAClsappAdvertiserProjectReceipts.Struct In list
                Me.Add(ClsProjectInfo.GetReceiptProjectInfo(Struct))
            Next
            IsReadOnly = True
            RaiseListChangedEvents = True
        End Sub

#End Region

    End Class
End Namespace