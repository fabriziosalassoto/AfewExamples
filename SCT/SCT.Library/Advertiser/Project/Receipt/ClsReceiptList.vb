Imports Csla
Imports SCT.DataAccess

Namespace Advertiser.Project
    <Serializable()> Public Class ClsReceiptList
        Inherits BusinessListBase(Of ClsReceiptList, ClsReceipt)

#Region " Business Methods "

        Public Function GetItem(ByVal idReceipt As Long) As ClsReceipt
            For Each receipt As ClsReceipt In Me
                If receipt.ID = idReceipt Then
                    Return receipt
                End If
            Next
            Return Nothing
        End Function

        Public Function GetDeletedItem(ByVal idReceipt As Long) As ClsReceipt
            For Each receipt As ClsReceipt In DeletedList
                If receipt.ID = idReceipt Then
                    Return receipt
                End If
            Next
            Return Nothing
        End Function

        Public Overloads Sub Add(ByVal idProject As Long, ByVal idReceipt As Long, ByVal paidByDisplay As Double, ByVal paidByClickThrough As Double, ByVal totalPaid As Double)
            If Not Contains(idProject) Then
                Me.Add(ClsReceipt.NewProjectReceipt(idProject, idReceipt, paidByDisplay, paidByClickThrough, totalPaid))
            Else
                Throw New InvalidOperationException("Receipt already assigned to Project")
            End If
        End Sub

        Public Sub Edit(ByVal idReceipt As Long, ByVal paidByDisplay As Double, ByVal paidByClickThrough As Double, ByVal totalPaid As Double)
            If Contains(idReceipt) Then
                For Each receipt As ClsReceipt In Me
                    If receipt.ID = idReceipt Then
                        receipt.PaidByClickThrough = paidByClickThrough
                        receipt.PaidByDisplay = paidByDisplay
                        receipt.TotalPaid = totalPaid
                        Exit For
                    End If
                Next
            Else
                Throw New InvalidOperationException("Receipt already assigned to Project")
            End If
        End Sub

        Public Overloads Sub Remove(ByVal idReceipt As Long)
            If Contains(idReceipt) Then
                For Each receipt As ClsReceipt In Me
                    If receipt.ID = idReceipt Then
                        Remove(receipt)
                        Exit For
                    End If
                Next
            Else
                Throw New InvalidOperationException("Receipt no assigned to Project")
            End If
        End Sub

        Public Overloads Function Contains(ByVal idReceipt As Long) As Boolean
            For Each receipt As ClsReceipt In Me
                If receipt.ID = idReceipt Then
                    Return True
                End If
            Next
            Return False
        End Function

        Public Overloads Function ContainsDeleted(ByVal idReceipt As Long) As Boolean
            For Each receipt As ClsReceipt In DeletedList
                If receipt.ID = idReceipt Then
                    Return True
                End If
            Next
            Return False
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function NewProjectReceiptList() As ClsReceiptList
            Return New ClsReceiptList
        End Function

        Friend Shared Function GetProjectReceiptList(ByVal list As DAClsappAdvertiserProjectReceipts.Struct()) As ClsReceiptList
            Return New ClsReceiptList(list)
        End Function

        Private Sub New()
            MarkAsChild()
        End Sub

        Private Sub New(ByVal list As DAClsappAdvertiserProjectReceipts.Struct())
            MarkAsChild()
            Fetch(list)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal list As DAClsappAdvertiserProjectReceipts.Struct())
            Me.RaiseListChangedEvents = False
            For Each Struct As DAClsappAdvertiserProjectReceipts.Struct In list
                Me.Add(ClsReceipt.GetProjectReceipt(Struct))
            Next
            Me.RaiseListChangedEvents = True
        End Sub

        Friend Sub Update(ByVal parent As Object)
            Me.RaiseListChangedEvents = False
            ' update (thus deleting) any deleted child objects
            For Each item As ClsReceipt In DeletedList
                item.DeleteSelf(parent)
            Next
            ' now that they are deleted, remove them from memory too
            DeletedList.Clear()

            ' add/update any current child objects
            For Each item As ClsReceipt In Me
                If item.IsNew Then
                    item.Insert(parent)
                Else
                    item.Update(parent)
                End If
            Next
            Me.RaiseListChangedEvents = True
        End Sub

#End Region

    End Class
End Namespace

