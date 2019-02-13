Imports Csla
Imports SCT.DataAccess

Namespace Advertiser.Project.Invoice
    <Serializable()> Public Class ClsDetailList
        Inherits BusinessListBase(Of ClsDetailList, ClsDetail)

#Region " Business Methods "

        Public Function GetItem(ByVal Id As Long) As ClsDetail
            For Each item As ClsDetail In Me
                If item.ID = Id Then
                    Return item
                End If
            Next
            Return Nothing
        End Function

        Public Overloads Sub Remove(ByVal Id As Long)
            For Each item As ClsDetail In Me
                If item.ID = Id Then
                    Remove(item)
                    Exit For
                End If
            Next
        End Sub

        Public Overloads Function Contains(ByVal Id As Long) As Boolean
            For Each item As ClsDetail In Me
                If item.ID = Id Then
                    Return True
                End If
            Next
            Return False
        End Function

#End Region

#Region " Authorization Rules "

        Public Shared Function CanAddObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole("")
        End Function

        Public Shared Function CanGetObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole("")
        End Function

        Public Shared Function CanEditObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole("")
        End Function

        Public Shared Function CanDeleteObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole("")
        End Function

#End Region

#Region " Factory Methods "

#Region " Root Methods "

        Public Shared Function NewDetailList() As ClsDetailList
            Return DataPortal.Create(Of ClsDetailList)(New Criteria())
        End Function

        Public Shared Function GetDetailList() As ClsDetailList
            Return DataPortal.Fetch(Of ClsDetailList)(New Criteria())
        End Function

#End Region

#Region " Child Methods "

        Friend Shared Function NewInvoiceDetailList() As ClsDetailList
            Return New ClsDetailList
        End Function

        Friend Shared Function GetInvoiceDetailList(ByVal list As DAClsappAdvertiserProjectInvoiceDetails.Struct()) As ClsDetailList
            Return New ClsDetailList(list)
        End Function

        Private Sub New()
            MarkAsChild()
        End Sub

        Private Sub New(ByVal list As DAClsappAdvertiserProjectInvoiceDetails.Struct())
            MarkAsChild()
            Fetch(list)
        End Sub

#End Region

#End Region

#Region " Data Access "

#Region " Root Area "

        <Serializable()> Private Class Criteria
            ' no criteria - retrieve all projects
        End Class

        <RunLocal()> Private Overloads Sub DataPortal_Create(ByVal criteria As Criteria)
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            ' fetch with no filter
            Fetch()
        End Sub

        Protected Overrides Sub DataPortal_Update()
            RaiseListChangedEvents = False
            For Each item As ClsDetail In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsDetail In Me
                If item.IsNew Then
                    item.Insert(item.Invoice)
                Else
                    item.Update(item.Invoice)
                End If
            Next
            RaiseListChangedEvents = True
        End Sub

#End Region

#Region " Child Area "

        Friend Sub Update(ByVal parent As Object)
            RaiseListChangedEvents = False
            For Each item As ClsDetail In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsDetail In Me
                If item.IsNew Then
                    item.Insert(parent)
                Else
                    item.Update(parent)
                End If
            Next
            RaiseListChangedEvents = True
        End Sub

#End Region

#Region " Common Area "

        Private Sub Fetch()
            Fetch(DAClsappAdvertiserProjectInvoiceDetails.Fetch())
        End Sub

        Private Sub Fetch(ByVal list As DAClsappAdvertiserProjectInvoiceDetails.Struct())
            Me.RaiseListChangedEvents = False
            For Each Struct As DAClsappAdvertiserProjectInvoiceDetails.Struct In list
                Me.Add(ClsDetail.GetChildDetail(Struct))
            Next
            Me.RaiseListChangedEvents = True
        End Sub

#End Region

#End Region

    End Class
End Namespace

