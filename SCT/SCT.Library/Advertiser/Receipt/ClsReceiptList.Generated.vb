Imports Csla
Imports SCT.DataAccess

Namespace Advertiser
    <Serializable()> Public Class ClsReceiptList
        Inherits BusinessListBase(Of ClsReceiptList, ClsReceipt)

#Region " Business Methods "

        Public Function GetItem(ByVal Id As Long) As ClsReceipt
            For Each item As ClsReceipt In Me
                If item.ID = Id Then
                    Return item
                End If
            Next
            Return Nothing
        End Function

        Public Overloads Sub Remove(ByVal Id As Long)
            For Each item As ClsReceipt In Me
                If item.ID = Id Then
                    Remove(item)
                    Exit For
                End If
            Next
        End Sub

        Public Overloads Function Contains(ByVal Id As Long) As Boolean
            For Each item As ClsReceipt In Me
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

        Public Shared Function NewReceiptList() As ClsReceiptList
            Return DataPortal.Create(Of ClsReceiptList)(New Criteria())
        End Function

        Public Shared Function GetReceiptList() As ClsReceiptList
            Return DataPortal.Fetch(Of ClsReceiptList)(New Criteria())
        End Function

        Public Shared Function GetReceiptList(ByVal paymentType As Integer, ByVal fromDate As Date, ByVal toDate As Date) As ClsReceiptList
            Return DataPortal.Fetch(Of ClsReceiptList)(New FilteredCriteria(paymentType, fromDate, toDate))
        End Function

        Public Shared Function GetReceiptList(ByVal paymentType() As Integer, ByVal fromDate As Date, ByVal toDate As Date) As ClsReceiptList
            Return DataPortal.Fetch(Of ClsReceiptList)(New FilteredCriteriaList(paymentType, fromDate, toDate))
        End Function

#End Region

#Region " Child Methods "

        Friend Shared Function NewChildReceiptList() As ClsReceiptList
            Return New ClsReceiptList
        End Function

        Friend Shared Function GetChildReceiptList(ByVal list As DAClsappAdvertiserReceipts.Struct()) As ClsReceiptList
            Return New ClsReceiptList(list)
        End Function

        Private Sub New()
            MarkAsChild()
        End Sub

        Private Sub New(ByVal list As DAClsappAdvertiserReceipts.Struct())
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

            Private mPaymentType As Integer
            Private mFromDate As Date
            Private mToDate As Date

            Public ReadOnly Property PaymentType() As Integer
                Get
                    Return Me.mPaymentType
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

            Public Sub New(ByVal paymentType As Integer, ByVal fromDate As Date, ByVal toDate As Date)
                Me.mPaymentType = paymentType
                Me.mFromDate = fromDate
                Me.mToDate = toDate
            End Sub

        End Class

        <Serializable()> Private Class FilteredCriteriaList

            Private mPaymentType() As Integer
            Private mFromDate As Date
            Private mToDate As Date

            Public ReadOnly Property PaymentType() As Integer()
                Get
                    Return Me.mPaymentType
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

            Public Sub New(ByVal paymentType() As Integer, ByVal fromDate As Date, ByVal toDate As Date)
                Me.mPaymentType = paymentType
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
            Fetch(criteria.PaymentType, criteria.FromDate, criteria.ToDate)
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteriaList)
            Fetch(criteria.PaymentType, criteria.FromDate, criteria.ToDate)
        End Sub

        Protected Overrides Sub DataPortal_Update()
            RaiseListChangedEvents = False
            For Each item As ClsReceipt In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsReceipt In Me
                If item.IsNew Then
                    item.Insert(Nothing)
                Else
                    item.Update(Nothing)
                End If
            Next
            RaiseListChangedEvents = True
        End Sub

#End Region

#Region " Child Area "

        Friend Sub Update(ByVal parent As Object)
            RaiseListChangedEvents = False
            For Each item As ClsReceipt In DeletedList
                item.DeleteSelf()
            Next
            DeletedList.Clear()
            For Each item As ClsReceipt In Me
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
            Fetch(DAClsappAdvertiserReceipts.Fetch())
        End Sub

        Private Sub Fetch(ByVal paymentType As Integer, ByVal fromDate As Date, ByVal toDate As Date)
            Fetch(DAClsappAdvertiserReceipts.Fetch(paymentType, fromDate, toDate))
        End Sub

        Private Sub Fetch(ByVal paymentType() As Integer, ByVal fromDate As Date, ByVal toDate As Date)
            Fetch(DAClsappAdvertiserReceipts.Fetch(paymentType, fromDate, toDate))
        End Sub

        Private Sub Fetch(ByVal list As DAClsappAdvertiserReceipts.Struct())
            Me.RaiseListChangedEvents = False
            For Each Struct As DAClsappAdvertiserReceipts.Struct In list
                Me.Add(ClsReceipt.GetChildReceipt(Struct))
            Next
            Me.RaiseListChangedEvents = True
        End Sub

#End Region

#End Region

    End Class
End Namespace