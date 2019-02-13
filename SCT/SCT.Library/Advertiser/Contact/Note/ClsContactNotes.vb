Imports Csla
Imports SCT.DataAccess

Namespace Advertiser
    <Serializable()> Public Class ClsContactNotes
        Inherits BusinessListBase(Of ClsContactNotes, ClsNote)

#Region " Factory Methods "

        Friend Shared Function NewContactNotes() As ClsContactNotes
            Return New ClsContactNotes
        End Function

        Friend Shared Function GetContactNotes(ByVal list As List(Of DAClsappAdvertiserNotes.Struct)) As ClsContactNotes
            Return New ClsContactNotes(list)
        End Function

        Private Sub New()
            MarkAsChild()
        End Sub

        Private Sub New(ByVal list As List(Of DAClsappAdvertiserNotes.Struct))
            MarkAsChild()
            Fetch(list)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal list As List(Of DAClsappAdvertiserNotes.Struct))
            Me.RaiseListChangedEvents = False
            For Each Struct As DAClsappAdvertiserNotes.Struct In list
                Me.Add(ClsNote.GetContactNote(Struct))
            Next
            Me.RaiseListChangedEvents = True
        End Sub

        Friend Sub Update(ByVal contact As Object)
            RaiseListChangedEvents = False
            For Each item As ClsNote In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsNote In Me
                If item.IsNew Then
                    item.Insert(contact)
                Else
                    item.Update(contact)
                End If
            Next
            RaiseListChangedEvents = True
        End Sub

#End Region

    End Class
End Namespace