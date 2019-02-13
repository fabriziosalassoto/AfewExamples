Imports Csla
Imports SCT.DataAccess

Namespace Advertiser.Receipt
    <Serializable()> Public Class ClsProjectList
        Inherits BusinessListBase(Of ClsProjectList, ClsProject)

#Region " Business Methods "

        Public Function GetItem(ByVal idProject As Long) As ClsProject
            For Each project As ClsProject In Me
                If project.ID = idProject Then
                    Return project
                End If
            Next
            Return Nothing
        End Function

        Public Function GetDeletedItem(ByVal idProject As Long) As ClsProject
            For Each project As ClsProject In DeletedList
                If project.ID = idProject Then
                    Return project
                End If
            Next
            Return Nothing
        End Function

        Public Overloads Sub Add(ByVal idProject As Long, ByVal idReceipt As Long, ByVal paidByDisplay As Double, ByVal paidByClickThrough As Double, ByVal totalPaid As Double)
            If Not Contains(idProject) Then
                Me.Add(ClsProject.NewReceiptProject(idProject, idReceipt, paidByDisplay, paidByClickThrough, totalPaid))
            Else
                Throw New InvalidOperationException("Project already assigned to Receipt")
            End If
        End Sub

        Public Sub Edit(ByVal idProject As Long, ByVal paidByDisplay As Double, ByVal paidByClickThrough As Double, ByVal totalPaid As Double)
            If Contains(idProject) Then
                For Each project As ClsProject In Me
                    If project.ID = idProject Then
                        project.PaidByClickThrough = paidByClickThrough
                        project.PaidByDisplay = paidByDisplay
                        project.TotalPaid = totalPaid
                        Exit For
                    End If
                Next
            Else
                Throw New InvalidOperationException("Project already assigned to Receipt")
            End If
        End Sub

        Public Overloads Sub Remove(ByVal idProject As Long)
            If Contains(idProject) Then
                For Each project As ClsProject In Me
                    If project.ID = idProject Then
                        Remove(project)
                        Exit For
                    End If
                Next
            Else
                Throw New InvalidOperationException("Project no assigned to Receipt")
            End If
        End Sub

        Public Overloads Function Contains(ByVal idProject As Long) As Boolean
            For Each project As ClsProject In Me
                If project.ID = idProject Then
                    Return True
                End If
            Next
            Return False
        End Function

        Public Overloads Function ContainsDeleted(ByVal idProject As Long) As Boolean
            For Each project As ClsProject In DeletedList
                If project.ID = idProject Then
                    Return True
                End If
            Next
            Return False
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function NewReceiptProjectList() As ClsProjectList
            Return New ClsProjectList
        End Function

        Friend Shared Function GetReceiptProjectList(ByVal list As DAClsappAdvertiserProjectReceipts.Struct()) As ClsProjectList
            Return New ClsProjectList(list)
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
                Me.Add(ClsProject.GetReceiptProject(Struct))
            Next
            Me.RaiseListChangedEvents = True
        End Sub

        Friend Sub Update(ByVal parent As Object)
            Me.RaiseListChangedEvents = False
            ' update (thus deleting) any deleted child objects
            For Each item As ClsProject In DeletedList
                item.DeleteSelf(parent)
            Next
            ' now that they are deleted, remove them from memory too
            DeletedList.Clear()

            ' add/update any current child objects
            For Each item As ClsProject In Me
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