Imports Csla
Imports SCT.DataAccess

Namespace Subscriber
    <Serializable()> Public Class ClsAccountInfo
        Inherits ReadOnlyBase(Of ClsAccountInfo)

#Region " Business Methods "

        Private mID As Long
        Private mComputerSerialNumber As String = ""

        Public ReadOnly Property ID() As Long
            Get
                CanReadProperty(True)
                Return Me.mID
            End Get
        End Property

        Public ReadOnly Property ComputerSerialNumber() As String
            Get
                CanReadProperty(True)
                Return Me.mComputerSerialNumber
            End Get
        End Property

        Protected Overrides Function GetIdValue() As Object
            Return Me.mID
        End Function

        Public Overrides Function ToString() As String
            Return Me.mComputerSerialNumber
        End Function

        Public Function ToListItem() As System.Web.UI.WebControls.ListItem
            Return New System.Web.UI.WebControls.ListItem(Me.mComputerSerialNumber, Me.mID)
        End Function

        Public Function GetAccount() As ClsAccount
            Return ClsAccount.GetAccount(Me.mID)
        End Function

#End Region

#Region " Authorization Rules "

        'Protected Overrides Sub AddAuthorizationRules()
        '    AuthorizationRules.AllowRead("ID", "")
        '    AuthorizationRules.AllowRead("ComputerSerialNumber", "")
        'End Sub

        Public Shared Function CanAddObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole("")
        End Function

        Public Shared Function CanGetObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole("")
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function NewAccountInfo() As ClsAccountInfo
            If Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add a subscriber account info")
            End If
            Return New ClsAccountInfo()
        End Function

        Public Shared Function GetAccountInfo(ByVal id As Long) As ClsAccountInfo
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view a subscriber account info")
            End If
            Return DataPortal.Fetch(Of ClsAccountInfo)(New Criteria(id))
        End Function

        Public Shared Function GetAccountInfo(ByVal Struct As DAClsappSubscribersAccounts.Struct) As ClsAccountInfo
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view a subscriber account info")
            End If
            Return DataPortal.Fetch(Of ClsAccountInfo)(Struct)
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

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
            Dim List As DAClsappSubscribersAccounts.Struct() = DAClsappSubscribersAccounts.Fetch(criteria.ID)
            If List.Length > 0 Then
                Me.LoadObjectData(List(0))
            Else
                Throw New System.Security.SecurityException("Subscriber account doesn't exist")
            End If
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal Struct As DAClsappSubscribersAccounts.Struct)
            Me.LoadObjectData(Struct)
        End Sub

        ''' <summary>
        ''' Load the data of the object with the fetched data
        ''' </summary>
        ''' <param name="Struct">Struct with the data</param>
        ''' <remarks></remarks>
        Private Sub LoadObjectData(ByVal Struct As DAClsappSubscribersAccounts.Struct)
            With Struct
                Me.mID = .ID.Value
                Me.mComputerSerialNumber = .ComputerSerialNumber.Value
            End With
        End Sub

#End Region

    End Class
End Namespace