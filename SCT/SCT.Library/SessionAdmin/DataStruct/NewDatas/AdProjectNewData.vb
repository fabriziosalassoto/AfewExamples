Option Strict On

<Serializable()> Public Class AdProjectNewData

    Private mID As New NewFieldData(Of Long)
    Private mAdvertiser As New AdAccountNewData
    Private mContact As New AdContactNewData
    Private mProjectDescription As New NewFieldData(Of String)
    Private mADUrl As New NewFieldData(Of String)
    Private mADHeight As New NewFieldData(Of Integer)
    Private mADWidth As New NewFieldData(Of Integer)
    Private mRunStartDate As New NewFieldData(Of Date)
    Private mRunEndDate As New NewFieldData(Of Date)
    Private mMinDisplays As New NewFieldData(Of Integer)
    Private mMaxDisplays As New NewFieldData(Of Integer)
    Private mMaxPerDay As New NewFieldData(Of Integer)
    Private mMinPerDay As New NewFieldData(Of Integer)
    Private mStartTimeBasedOnSubscribersTime As New NewFieldData(Of Date)
    Private mEndTimeBasedOnSubscribersTime As New NewFieldData(Of Date)
    Private mAdCountDisplayed As New NewFieldData(Of Integer)
    Private mAdVerifiedDate As New NewFieldData(Of Date)
    Private mAdOnlineDate As New NewFieldData(Of Date)
    Private mPromoCode As New NewFieldData(Of Integer)
    Private mComissionCode As New NewFieldData(Of Integer)
    Private mSex As New NewFieldData(Of String)
    Private mMinAge As New NewFieldData(Of String)
    Private mMaxAge As New NewFieldData(Of String)
    Private mOccupation As New NewFieldData(Of String)
    Private mCountry As New NewFieldData(Of String)
    Private mState As New NewFieldData(Of String)

    Public Property ID() As NewFieldData(Of Long)
        Get
            Return Me.mID
        End Get
        Set(ByVal value As NewFieldData(Of Long))
            Me.mID = value
        End Set
    End Property

    Public Property Advertiser() As AdAccountnewData
        Get
            Return Me.mAdvertiser
        End Get
        Set(ByVal value As AdAccountNEwData)
            Me.mAdvertiser = value
        End Set
    End Property

    Public Property Contact() As AdContactNewData
        Get
            Return Me.mContact
        End Get
        Set(ByVal value As AdContactNewData)
            Me.mContact = value
        End Set
    End Property

    Public Property ProjectDescription() As NewFieldData(Of String)
        Get
            Return Me.mProjectDescription
        End Get
        Set(ByVal value As NewFieldData(Of String))
            Me.mProjectDescription = value
        End Set
    End Property

    Public Property ADUrl() As NewFieldData(Of String)
        Get
            Return Me.mADUrl
        End Get
        Set(ByVal value As NewFieldData(Of String))
            Me.mADUrl = value
        End Set
    End Property

    Public Property ADHeight() As NewFieldData(Of Integer)
        Get
            Return Me.mADHeight
        End Get
        Set(ByVal value As NewFieldData(Of Integer))
            Me.mADHeight = value
        End Set
    End Property

    Public Property ADWidth() As NewFieldData(Of Integer)
        Get
            Return Me.mADWidth
        End Get
        Set(ByVal value As NewFieldData(Of Integer))
            Me.mADWidth = value
        End Set
    End Property

    Public Property RunStartDate() As NewFieldData(Of DateTime)
        Get
            Return Me.mRunStartDate
        End Get
        Set(ByVal value As NewFieldData(Of DateTime))
            Me.mRunStartDate = value
        End Set
    End Property

    Public Property RunEndDate() As NewFieldData(Of DateTime)
        Get
            Return Me.mRunEndDate
        End Get
        Set(ByVal value As NewFieldData(Of DateTime))
            Me.mRunEndDate = value
        End Set
    End Property

    Public Property MinDisplays() As NewFieldData(Of Integer)
        Get
            Return Me.mMinDisplays
        End Get
        Set(ByVal value As NewFieldData(Of Integer))
            Me.mMinDisplays = value
        End Set
    End Property

    Public Property MaxDisplays() As NewFieldData(Of Integer)
        Get
            Return Me.mMaxDisplays
        End Get
        Set(ByVal value As NewFieldData(Of Integer))
            Me.mMaxDisplays = value
        End Set
    End Property

    Public Property MaxPerDay() As NewFieldData(Of Integer)
        Get
            Return Me.mMaxPerDay
        End Get
        Set(ByVal value As NewFieldData(Of Integer))
            Me.mMaxPerDay = value
        End Set
    End Property

    Public Property MinPerDay() As NewFieldData(Of Integer)
        Get
            Return Me.mMinPerDay
        End Get
        Set(ByVal value As NewFieldData(Of Integer))
            Me.mMinPerDay = value
        End Set
    End Property

    Public Property StartTimeBasedOnSubscribersTime() As NewFieldData(Of DateTime)
        Get
            Return Me.mStartTimeBasedOnSubscribersTime
        End Get
        Set(ByVal value As NewFieldData(Of DateTime))
            Me.mStartTimeBasedOnSubscribersTime = value
        End Set
    End Property

    Public Property EndTimeBasedOnSubscribersTime() As NewFieldData(Of DateTime)
        Get
            Return Me.mEndTimeBasedOnSubscribersTime
        End Get
        Set(ByVal value As NewFieldData(Of DateTime))
            Me.mEndTimeBasedOnSubscribersTime = value
        End Set
    End Property

    Public Property AdCountDisplayed() As NewFieldData(Of Integer)
        Get
            Return Me.mAdCountDisplayed
        End Get
        Set(ByVal value As NewFieldData(Of Integer))
            Me.mAdCountDisplayed = value
        End Set
    End Property

    Public Property AdVerifiedDate() As NewFieldData(Of DateTime)
        Get
            Return Me.mAdVerifiedDate
        End Get
        Set(ByVal value As NewFieldData(Of DateTime))
            Me.mAdVerifiedDate = value
        End Set
    End Property

    Public Property AdOnlineDate() As NewFieldData(Of DateTime)
        Get
            Return Me.mAdOnlineDate
        End Get
        Set(ByVal value As NewFieldData(Of DateTime))
            Me.mAdOnlineDate = value
        End Set
    End Property

    Public Property PromoCode() As NewFieldData(Of Integer)
        Get
            Return Me.mPromoCode
        End Get
        Set(ByVal value As NewFieldData(Of Integer))
            Me.mPromoCode = value
        End Set
    End Property

    Public Property ComissionCode() As NewFieldData(Of Integer)
        Get
            Return Me.mComissionCode
        End Get
        Set(ByVal value As NewFieldData(Of Integer))
            Me.mComissionCode = value
        End Set
    End Property

    Public Property Sex() As NewFieldData(Of String)
        Get
            Return Me.mSex
        End Get
        Set(ByVal value As NewFieldData(Of String))
            Me.mSex = value
        End Set
    End Property

    Public Property MinAge() As NewFieldData(Of String)
        Get
            Return Me.mMinAge
        End Get
        Set(ByVal value As NewFieldData(Of String))
            Me.mMinAge = value
        End Set
    End Property

    Public Property MaxAge() As NewFieldData(Of String)
        Get
            Return Me.mMaxAge
        End Get
        Set(ByVal value As NewFieldData(Of String))
            Me.mMaxAge = value
        End Set
    End Property

    Public Property Occupation() As NewFieldData(Of String)
        Get
            Return Me.mOccupation
        End Get
        Set(ByVal value As NewFieldData(Of String))
            Me.mOccupation = value
        End Set
    End Property

    Public Property Country() As NewFieldData(Of String)
        Get
            Return Me.mCountry
        End Get
        Set(ByVal value As NewFieldData(Of String))
            Me.mCountry = value
        End Set
    End Property

    Public Property State() As NewFieldData(Of String)
        Get
            Return Me.mState
        End Get
        Set(ByVal value As NewFieldData(Of String))
            Me.mState = value
        End Set
    End Property

End Class
