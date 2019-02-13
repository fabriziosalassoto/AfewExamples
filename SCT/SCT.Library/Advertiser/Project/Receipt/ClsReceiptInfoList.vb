Imports Csla
Imports SCT.DataAccess

Namespace Advertiser.Project
    <Serializable()> Public Class ClsReceiptInfoList
        Inherits ReadOnlyListBase(Of ClsReceiptInfoList, ClsReceiptInfo)

#Region " Factory Methods "

        Public Shared Function NewReceiptInfoList() As ClsReceiptInfoList
            Return New ClsReceiptInfoList
        End Function

        Public Shared Function GetReceiptInfoList() As ClsReceiptInfoList
            Return DataPortal.Fetch(Of ClsReceiptInfoList)(New Criteria)
        End Function

        Public Shared Function GetReceiptInfoList(ByVal IDProject As Long) As ClsReceiptInfoList
            Return DataPortal.Fetch(Of ClsReceiptInfoList)(New FilteredCriteria(IDProject))
        End Function

        Public Shared Function GetReceiptInfoList(ByVal IDProject() As Long) As ClsReceiptInfoList
            Return DataPortal.Fetch(Of ClsReceiptInfoList)(New FilteredCriteriaList(IDProject))
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

            Public ReadOnly Property IDProject() As Long
                Get
                    Return Me.mIDProject
                End Get
            End Property

            Public Sub New(ByVal idProject As Long)
                Me.mIDProject = idProject
            End Sub

        End Class

        <Serializable()> Private Class FilteredCriteriaList

            Private mIDProject() As Long

            Public ReadOnly Property IDProject() As Long()
                Get
                    Return Me.mIDProject
                End Get
            End Property

            Public Sub New(ByVal idProject() As Long)
                Me.mIDProject = idProject
            End Sub

        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            ' fetch with no filter
            Fetch()
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteria)
            Fetch(criteria.IDProject)
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteriaList)
            Fetch(criteria.IDProject)
        End Sub

        Private Sub Fetch()
            Fetch(DAClsappAdvertiserProjectReceipts.Fetch())
        End Sub

        Private Sub Fetch(ByVal IDProject As Long)
            Fetch(DAClsappAdvertiserProjectReceipts.Fetch(IDProject, 0))
        End Sub

        Private Sub Fetch(ByVal IDProject() As Long)
            Fetch(DAClsappAdvertiserProjectReceipts.Fetch(IDProject, New Long() {}))
        End Sub

        Private Sub Fetch(ByVal list As DAClsappAdvertiserProjectReceipts.Struct())
            RaiseListChangedEvents = False
            IsReadOnly = False
            For Each Struct As DAClsappAdvertiserProjectReceipts.Struct In list
                Me.Add(ClsReceiptInfo.GetReceiptInfo(Struct))
            Next
            IsReadOnly = True
            RaiseListChangedEvents = True
        End Sub

#End Region

    End Class
End Namespace

