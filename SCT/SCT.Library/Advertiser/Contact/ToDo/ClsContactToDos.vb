Imports Csla
Imports SCT.DataAccess

Namespace Advertiser
    <Serializable()> Public Class ClsContactToDos
        Inherits BusinessListBase(Of ClsContactToDos, ClsToDo)

#Region " Factory Methods "

        Friend Shared Function NewContactToDos() As ClsContactToDos
            Return New ClsContactToDos
        End Function

        Friend Shared Function GetContactToDos(ByVal list As List(Of DAClsappAdvertiserToDo.Struct)) As ClsContactToDos
            Return New ClsContactToDos(list)
        End Function

        Private Sub New()
            MarkAsChild()
        End Sub

        Private Sub New(ByVal list As List(Of DAClsappAdvertiserToDo.Struct))
            MarkAsChild()
            Fetch(list)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal list As List(Of DAClsappAdvertiserToDo.Struct))

            Me.RaiseListChangedEvents = False
            For Each Struct As DAClsappAdvertiserToDo.Struct In list
                Me.Add(ClsToDo.GetContactToDo(Struct))
            Next
            Me.RaiseListChangedEvents = True

        End Sub

        Friend Sub Update(ByVal contact As Object)
            RaiseListChangedEvents = False
            For Each item As ClsToDo In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsToDo In Me
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