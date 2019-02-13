Option Strict On

<Serializable()> Public Class AdProjectData

    Private mID As Long
    Private mAdvertiser As New AdAccountData
    Private mContact As New AdContactData
    Private mProjectDescription As String = String.Empty
    Private mADUrl As String = String.Empty
    Private mADHeight As Integer
    Private mADWidth As Integer
    Private mRunStartDate As New Date(1900, 1, 1)
    Private mRunEndDate As New Date(1900, 1, 1)
    Private mMinDisplays As Integer
    Private mMaxDisplays As Integer
    Private mMaxPerDay As Integer
    Private mMinPerDay As Integer
    Private mStartTimeBasedOnSubscribersTime As New Date(1900, 1, 1, 0, 1, 0)
    Private mEndTimeBasedOnSubscribersTime As New Date(1900, 1, 1, 23, 59, 0)
    Private mAdCountDisplayed As Integer
    Private mAdVerifiedDate As New Date(1900, 1, 1)
    Private mAdOnlineDate As New Date(1900, 1, 1)
    Private mPromoCode As Integer
    Private mComissionCode As Integer
    Private mSex As String = String.Empty
    Private mMinAge As String = String.Empty
    Private mMaxAge As String = String.Empty
    Private mOccupation As String = String.Empty
    Private mCountry As String = String.Empty
    Private mState As String = String.Empty
    Private mDemographics As New Generic.List(Of AdProjectDemographicData)
    Private mAdHistories As New Generic.List(Of AdHistoryData)
    Private mInvoices As New Generic.List(Of AdInvoiceData)
    Private mReceipts As New Generic.List(Of AdReceiptData)
    'Private mPrices As New Generic.List(Of AdPriceData)

    Public Property ID() As Long
        Get
            Return Me.mID
        End Get
        Set(ByVal value As Long)
            Me.mID = value
        End Set
    End Property

    Public Property Advertiser() As AdAccountData
        Get
            Return Me.mAdvertiser
        End Get
        Set(ByVal value As AdAccountData)
            Me.mAdvertiser = value
        End Set
    End Property

    Public Property Contact() As AdContactData
        Get
            Return Me.mContact
        End Get
        Set(ByVal value As AdContactData)
            Me.mContact = value
        End Set
    End Property

    Public Property ProjectDescription() As String
        Get
            Return Me.mProjectDescription
        End Get
        Set(ByVal value As String)
            Me.mProjectDescription = value
        End Set
    End Property

    Public Property ADUrl() As String
        Get
            Return Me.mADUrl
        End Get
        Set(ByVal value As String)
            Me.mADUrl = value
        End Set
    End Property

    Public Property ADHeight() As Integer
        Get
            Return Me.mADHeight
        End Get
        Set(ByVal value As Integer)
            Me.mADHeight = value
        End Set
    End Property

    Public Property ADWidth() As Integer
        Get
            Return Me.mADWidth
        End Get
        Set(ByVal value As Integer)
            Me.mADWidth = value
        End Set
    End Property

    Public Property RunStartDate() As DateTime
        Get
            Return Me.mRunStartDate
        End Get
        Set(ByVal value As DateTime)
            Me.mRunStartDate = value
        End Set
    End Property

    Public Property RunEndDate() As DateTime
        Get
            Return Me.mRunEndDate
        End Get
        Set(ByVal value As DateTime)
            Me.mRunEndDate = value
        End Set
    End Property

    Public Property MinDisplays() As Integer
        Get
            Return Me.mMinDisplays
        End Get
        Set(ByVal value As Integer)
            Me.mMinDisplays = value
        End Set
    End Property

    Public Property MaxDisplays() As Integer
        Get
            Return Me.mMaxDisplays
        End Get
        Set(ByVal value As Integer)
            Me.mMaxDisplays = value
        End Set
    End Property

    Public Property MaxPerDay() As Integer
        Get
            Return Me.mMaxPerDay
        End Get
        Set(ByVal value As Integer)
            Me.mMaxPerDay = value
        End Set
    End Property

    Public Property MinPerDay() As Integer
        Get
            Return Me.mMinPerDay
        End Get
        Set(ByVal value As Integer)
            Me.mMinPerDay = value
        End Set
    End Property

    Public Property StartTimeBasedOnSubscribersTime() As DateTime
        Get
            Return Me.mStartTimeBasedOnSubscribersTime
        End Get
        Set(ByVal value As DateTime)
            Me.mStartTimeBasedOnSubscribersTime = value
        End Set
    End Property

    Public Property EndTimeBasedOnSubscribersTime() As DateTime
        Get
            Return Me.mEndTimeBasedOnSubscribersTime
        End Get
        Set(ByVal value As DateTime)
            Me.mEndTimeBasedOnSubscribersTime = value
        End Set
    End Property

    Public Property AdCountDisplayed() As Integer
        Get
            Return Me.mAdCountDisplayed
        End Get
        Set(ByVal value As Integer)
            Me.mAdCountDisplayed = value
        End Set
    End Property

    Public Property AdVerifiedDate() As DateTime
        Get
            Return Me.mAdVerifiedDate
        End Get
        Set(ByVal value As DateTime)
            Me.mAdVerifiedDate = value
        End Set
    End Property

    Public Property AdOnlineDate() As DateTime
        Get
            Return Me.mAdOnlineDate
        End Get
        Set(ByVal value As DateTime)
            Me.mAdOnlineDate = value
        End Set
    End Property

    Public Property PromoCode() As Integer
        Get
            Return Me.mPromoCode
        End Get
        Set(ByVal value As Integer)
            Me.mPromoCode = value
        End Set
    End Property

    Public Property ComissionCode() As Integer
        Get
            Return Me.mComissionCode
        End Get
        Set(ByVal value As Integer)
            Me.mComissionCode = value
        End Set
    End Property

    Public Property Sex() As String
        Get
            Return Me.mSex
        End Get
        Set(ByVal value As String)
            Me.mSex = value
        End Set
    End Property

    Public Property MinAge() As String
        Get
            Return Me.mMinAge
        End Get
        Set(ByVal value As String)
            Me.mMinAge = value
        End Set
    End Property

    Public Property MaxAge() As String
        Get
            Return Me.mMaxAge
        End Get
        Set(ByVal value As String)
            Me.mMaxAge = value
        End Set
    End Property

    Public Property Occupation() As String
        Get
            Return Me.mOccupation
        End Get
        Set(ByVal value As String)
            Me.mOccupation = value
        End Set
    End Property

    Public Property Country() As String
        Get
            Return Me.mCountry
        End Get
        Set(ByVal value As String)
            Me.mCountry = value
        End Set
    End Property

    Public Property State() As String
        Get
            Return Me.mState
        End Get
        Set(ByVal value As String)
            Me.mState = value
        End Set
    End Property

    Public Sub AddDemographic(ByVal demographic As AdProjectDemographicData)
        Me.mDemographics.Add(demographic)
    End Sub

    Public Property Demographics() As AdProjectDemographicData()
        Get
            Return Me.mDemographics.ToArray
        End Get
        Set(ByVal value As AdProjectDemographicData())
            Me.mDemographics = New Generic.List(Of AdProjectDemographicData)(value)
        End Set
    End Property

    Public Sub AddInvioces(ByVal detail As AdInvoiceData)
        Me.mInvoices.Add(detail)
    End Sub

    Public Property Invoices() As AdInvoiceData()
        Get
            Return Me.mInvoices.ToArray
        End Get
        Set(ByVal value As AdInvoiceData())
            Me.mInvoices = New Generic.List(Of AdInvoiceData)(value)
        End Set
    End Property

    Public Sub AddReceipts(ByVal receipt As AdReceiptData)
        Me.mReceipts.Add(receipt)
    End Sub

    Public Property Receipts() As AdReceiptData()
        Get
            Return Me.mReceipts.ToArray
        End Get
        Set(ByVal value As AdReceiptData())
            Me.mReceipts = New Generic.List(Of AdReceiptData)(value)
        End Set
    End Property

    Public Sub AddAdHistories(ByVal adHistory As AdHistoryData)
        Me.mAdHistories.Add(adHistory)
    End Sub

    Public Property AdHistories() As AdHistoryData()
        Get
            Return Me.mAdHistories.ToArray
        End Get
        Set(ByVal value As AdHistoryData())
            Me.mAdHistories = New Generic.List(Of AdHistoryData)(value)
        End Set
    End Property

End Class
