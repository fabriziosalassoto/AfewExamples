Option Strict On

<Serializable()> Public Class AdContactNewData

    Private mID As New NewFieldData(Of Long)
    Private mAdvertiser As New AdAccountNewData
    Private mFirstName As New NewFieldData(Of String)
    Private mLastName As New NewFieldData(Of String)
    Private mFullName As New NewFieldData(Of String)
    Private mMainCompanyAddress As New NewFieldData(Of Boolean)
    Private mPrimaryAddress As New NewFieldData(Of String)
    Private mSecondaryAddress As New NewFieldData(Of String)
    Private mCity As New NewFieldData(Of String)
    Private mState As New NewFieldData(Of String)
    Private mCountry As New NewFieldData(Of String)
    Private mZipCode As New NewFieldData(Of String)
    Private mProvidence As New NewFieldData(Of String)
    Private mDepartment As New NewFieldData(Of String)
    Private mResposibleForNotes As New NewFieldData(Of String)

    Public Property ID() As NewFieldData(Of Long)
        Get
            Return Me.mID
        End Get
        Set(ByVal value As NewFieldData(Of Long))
            Me.mID = value
        End Set
    End Property

    Public Property Advertiser() As AdAccountNewData
        Get
            Return Me.mAdvertiser
        End Get
        Set(ByVal value As AdAccountNewData)
            Me.mAdvertiser = value
        End Set
    End Property

    Public Property FirstName() As NewFieldData(Of String)
        Get
            Return Me.mFirstName
        End Get
        Set(ByVal value As NewFieldData(Of String))
            Me.mFirstName = value
        End Set
    End Property

    Public Property LastName() As NewFieldData(Of String)
        Get
            Return Me.mLastName
        End Get
        Set(ByVal value As NewFieldData(Of String))
            Me.mLastName = value
        End Set
    End Property

    Public Property FullName() As NewFieldData(Of String)
        Get
            Return Me.mFullName
        End Get
        Set(ByVal value As NewFieldData(Of String))
            Me.mFullName = value
        End Set
    End Property

    Public Property MainCompanyAddress() As NewFieldData(Of Boolean)
        Get
            Return Me.mMainCompanyAddress
        End Get
        Set(ByVal value As NewFieldData(Of Boolean))
            Me.mMainCompanyAddress = value
        End Set
    End Property

    Public Property PrimaryAddress() As NewFieldData(Of String)
        Get
            Return Me.mPrimaryAddress
        End Get
        Set(ByVal value As NewFieldData(Of String))
            Me.mPrimaryAddress = value
        End Set
    End Property

    Public Property SecondaryAddress() As NewFieldData(Of String)
        Get
            Return Me.mSecondaryAddress
        End Get
        Set(ByVal value As NewFieldData(Of String))
            Me.mSecondaryAddress = value
        End Set
    End Property

    Public Property City() As NewFieldData(Of String)
        Get
            Return Me.mCity
        End Get
        Set(ByVal value As NewFieldData(Of String))
            Me.mCity = value
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

    Public Property Country() As NewFieldData(Of String)
        Get
            Return Me.mCountry
        End Get
        Set(ByVal value As NewFieldData(Of String))
            Me.mCountry = value
        End Set
    End Property


    Public Property ZipCode() As NewFieldData(Of String)
        Get
            Return Me.mZipCode
        End Get
        Set(ByVal value As NewFieldData(Of String))
            Me.mZipCode = value
        End Set
    End Property

    Public Property Providence() As NewFieldData(Of String)
        Get
            Return Me.mProvidence
        End Get
        Set(ByVal value As NewFieldData(Of String))
            Me.mProvidence = value
        End Set
    End Property

    Public Property Department() As NewFieldData(Of String)
        Get
            Return Me.mDepartment
        End Get
        Set(ByVal value As NewFieldData(Of String))
            Me.mDepartment = value
        End Set
    End Property

    Public Property ResposibleForNotes() As NewFieldData(Of String)
        Get
            Return Me.mResposibleForNotes
        End Get
        Set(ByVal value As NewFieldData(Of String))
            Me.mResposibleForNotes = value
        End Set
    End Property

End Class
