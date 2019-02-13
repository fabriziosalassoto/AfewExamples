Imports Csla
Imports SCT.DataAccess

Namespace Advertiser
    <Serializable()> Public Class ClsReceiptInfoList
        Inherits ReadOnlyListBase(Of ClsReceiptInfoList, ClsReceiptInfo)

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole(" ")
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function NewReceiptInfoList() As ClsReceiptInfoList
            Return New ClsReceiptInfoList
        End Function

        Public Shared Function GetReceiptInfoList() As ClsReceiptInfoList
            Return DataPortal.Fetch(Of ClsReceiptInfoList)(New Criteria)
        End Function

        Public Shared Function GetReceiptInfoList(ByVal paymentType As Integer, ByVal fromDate As Date, ByVal toDate As Date) As ClsReceiptInfoList
            Return DataPortal.Fetch(Of ClsReceiptInfoList)(New FilteredCriteria(paymentType, fromDate, toDate))
        End Function

        Public Shared Function GetReceiptInfoList(ByVal paymentType() As Integer, ByVal fromDate As Date, ByVal toDate As Date) As ClsReceiptInfoList
            Return DataPortal.Fetch(Of ClsReceiptInfoList)(New FilteredCriteriaList(paymentType, fromDate, toDate))
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

        Private Sub Fetch()
            Fetch(DAClsappAdvertiserReceipts.Fetch())
        End Sub

        Private Sub Fetch(ByVal paymentType As Integer, ByVal fromDate As Date, ByVal toDate As Date)
            DAClsappAdvertiserReceipts.Fetch(paymentType, fromDate, toDate)
        End Sub

        Private Sub Fetch(ByVal paymentType() As Integer, ByVal fromDate As Date, ByVal toDate As Date)
            DAClsappAdvertiserReceipts.Fetch(paymentType, fromDate, toDate)
        End Sub

        Private Sub Fetch(ByVal list As DAClsappAdvertiserReceipts.Struct())
            RaiseListChangedEvents = False
            IsReadOnly = False
            For Each Struct As DAClsappAdvertiserReceipts.Struct In list
                Me.Add(ClsReceiptInfo.GetReceiptInfo(Struct))
            Next
            IsReadOnly = True
            RaiseListChangedEvents = True
        End Sub

#End Region

    End Class
End Namespace