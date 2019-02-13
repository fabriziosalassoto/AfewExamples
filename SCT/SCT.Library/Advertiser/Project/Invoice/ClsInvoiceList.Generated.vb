Imports Csla
Imports SCT.DataAccess

Namespace Advertiser.Project
    <Serializable()> Public Class ClsInvoiceList
        Inherits BusinessListBase(Of ClsInvoiceList, ClsInvoice)

#Region " Business Methods "

        Public Function GetItem(ByVal Id As Long) As ClsInvoice
            For Each item As ClsInvoice In Me
                If item.ID = Id Then
                    Return item
                End If
            Next
            Return Nothing
        End Function

        Public Overloads Sub Remove(ByVal Id As Long)
            For Each item As ClsInvoice In Me
                If item.ID = Id Then
                    Remove(item)
                    Exit For
                End If
            Next
        End Sub

        Public Overloads Function Contains(ByVal Id As Long) As Boolean
            For Each item As ClsInvoice In Me
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

        Public Shared Function NewInvoiceList() As ClsInvoiceList
            Return DataPortal.Create(Of ClsInvoiceList)(New Criteria())
        End Function

        Public Shared Function GetInvoiceList() As ClsInvoiceList
            Return DataPortal.Fetch(Of ClsInvoiceList)(New Criteria())
        End Function

        Public Shared Function GetInvoiceList(ByVal idProject As Long, ByVal fromDate As Date, ByVal toDate As Date) As ClsInvoiceList
            Return DataPortal.Fetch(Of ClsInvoiceList)(New FilteredCriteria(idProject, fromDate, toDate))
        End Function

        Public Shared Function GetInvoiceList(ByVal idProject() As Long, ByVal fromDate As Date, ByVal toDate As Date) As ClsInvoiceList
            Return DataPortal.Fetch(Of ClsInvoiceList)(New FilteredCriteriaList(idProject, fromDate, toDate))
        End Function

#End Region

#Region " Child Methods "

        Friend Shared Function NewProjectInvoice() As ClsInvoiceList
            Return New ClsInvoiceList
        End Function

        Friend Shared Function GetProjectInvoice(ByVal list As DAClsappAdvertiserProjectInvoices.Struct()) As ClsInvoiceList
            Return New ClsInvoiceList(list)
        End Function

        Private Sub New()
            MarkAsChild()
        End Sub

        Private Sub New(ByVal list As DAClsappAdvertiserProjectInvoices.Struct())
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

        <RunLocal()> Private Overloads Sub DataPortal_Create(ByVal criteria As Criteria)
        End Sub

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

        Protected Overrides Sub DataPortal_Update()
            RaiseListChangedEvents = False
            For Each item As ClsInvoice In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsInvoice In Me
                If item.IsNew Then
                    item.Insert(item.Project)
                Else
                    item.Update(item.Project)
                End If
            Next
            RaiseListChangedEvents = True
        End Sub

#End Region

#Region " Child Area "

        Friend Sub Update(ByVal parent As Object)
            RaiseListChangedEvents = False
            For Each item As ClsInvoice In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsInvoice In Me
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
            Fetch(DAClsappAdvertiserProjectInvoices.Fetch())
        End Sub

        Private Sub Fetch(ByVal idProject As Long, ByVal fromDate As Date, ByVal toDate As Date)
            Fetch(DAClsappAdvertiserProjectInvoices.Fetch(idProject, fromDate, toDate))
        End Sub

        Private Sub Fetch(ByVal idProject() As Long, ByVal fromDate As Date, ByVal toDate As Date)
            Fetch(DAClsappAdvertiserProjectInvoices.Fetch(idProject, fromDate, toDate))
        End Sub

        Private Sub Fetch(ByVal list As DAClsappAdvertiserProjectInvoices.Struct())
            Me.RaiseListChangedEvents = False
            For Each Struct As DAClsappAdvertiserProjectInvoices.Struct In list
                Me.Add(ClsInvoice.GetChildInvoice(Struct))
            Next
            Me.RaiseListChangedEvents = True
        End Sub

#End Region

#End Region

    End Class
End Namespace

