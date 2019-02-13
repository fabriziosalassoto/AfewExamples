Imports Csla
Imports SCT.DataAccess

Namespace Advertiser
    <Serializable()> Public Class ClsContactInfo
        Inherits ReadOnlyBase(Of ClsContactInfo)

#Region " Business Methods "

        Private mID As Long
        Private mAdvertiser As ClsAccountInfo = ClsAccountInfo.NewAccountInfo
        Private mFirstName As String = String.Empty
        Private mLastName As String = String.Empty
        Private mMainCompanyAddress As Boolean = False
        Private mPrimaryAddress As String = String.Empty

        Public ReadOnly Property ID() As Long
            Get
                CanReadProperty(True)
                Return Me.mID
            End Get
        End Property

        Public ReadOnly Property Advertiser() As ClsAccountInfo
            Get
                CanReadProperty(True)
                Return Me.mAdvertiser
            End Get
        End Property

        Public ReadOnly Property FirstName() As String
            Get
                CanReadProperty(True)
                Return Me.mFirstName
            End Get
        End Property

        Public ReadOnly Property LastName() As String
            Get
                CanReadProperty(True)
                Return Me.mLastName
            End Get
        End Property

        Public ReadOnly Property FullName() As String
            Get
                If CanReadProperty("FirstName") AndAlso CanReadProperty("LastName") Then
                    Return Me.mLastName & ", " & Me.mFirstName
                Else
                    Throw New System.Security.SecurityException("FullName read not allowed")
                End If
            End Get
        End Property

        Public ReadOnly Property MainCompanyAddress() As Boolean
            Get
                CanReadProperty(True)
                Return Me.mMainCompanyAddress
            End Get
        End Property

        Public ReadOnly Property PrimaryAddress() As String
            Get
                CanReadProperty(True)
                Return Me.mPrimaryAddress
            End Get
        End Property

        Protected Overrides Function GetIdValue() As Object
            Return Me.mID
        End Function

        Public Overrides Function ToString() As String
            Return Me.FullName
        End Function

        Public Function ToListItem() As System.Web.UI.WebControls.ListItem
            Return New System.Web.UI.WebControls.ListItem(Me.ToString, Me.mID)
        End Function

        Public Function GetContact() As ClsContact
            Return ClsContact.GetContact(Me.mID)
        End Function

#End Region

#Region " Authorization Rules "

        'Protected Overrides Sub AddAuthorizationRules()
        '    AuthorizationRules.AllowRead("ID", "")
        '    AuthorizationRules.AllowRead("FirstName", "")
        '    AuthorizationRules.AllowRead("LastName", "")
        'End Sub

        Public Shared Function CanGetObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole("")
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function NewContactInfo() As ClsContactInfo
            Return New ClsContactInfo()
        End Function

        Public Shared Function GetContactInfo(ByVal id As Long) As ClsContactInfo
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view a contact info")
            End If
            Return DataPortal.Fetch(Of ClsContactInfo)(New Criteria(id))
        End Function

        Public Shared Function GetContactInfo(ByVal Struct As DAClsappAdvertiserContactInfo.Struct) As ClsContactInfo
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view a contact info")
            End If
            Return DataPortal.Fetch(Of ClsContactInfo)(Struct)
        End Function

#End Region

#Region " Data Access "

        <Serializable()> Private Class Criteria

            Private mID As Long
            Public ReadOnly Property ID() As Long
                Get
                    Return Me.mID
                End Get
            End Property

            Public Sub New(ByVal pID As Long)
                Me.mID = pID
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            Dim List As DAClsappAdvertiserContactInfo.Struct() = DAClsappAdvertiserContactInfo.Fetch(criteria.ID)
            If List.Length > 0 Then
                Me.LoadObjectData(List(0))
            Else
                Throw New System.Security.SecurityException("Contact doesn't exist")
            End If
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal Struct As DAClsappAdvertiserContactInfo.Struct)
            Me.LoadObjectData(Struct)
        End Sub

        ''' <summary>
        ''' Load the data of the object with the fetched data
        ''' </summary>
        ''' <param name="Struct">Struct with the data</param>
        ''' <remarks></remarks>
        Private Sub LoadObjectData(ByVal Struct As DAClsappAdvertiserContactInfo.Struct)
            With Struct
                Me.mID = .ID.Value
                Me.mAdvertiser = ClsAccountInfo.GetAccountInfo(.IDAdvertiser.Value)
                Me.mFirstName = .FirstName.Value
                Me.mLastName = .LastName.Value
                Me.mMainCompanyAddress = .MainCompanyAddress.Value
                Me.mPrimaryAddress = .PrimaryAddress.Value
            End With
        End Sub

#End Region

    End Class
End Namespace