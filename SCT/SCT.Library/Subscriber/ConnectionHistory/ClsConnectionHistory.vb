Imports Csla
Imports SCT.DataAccess

Namespace Subscriber
    <Serializable()> Public Class ClsConnectionHistory
        Inherits BusinessBase(Of ClsConnectionHistory)

#Region " Business Methods "

        Private mID As Long
        Private mSubscriberAccount As ClsAccountInfo = ClsAccountInfo.NewAccountInfo
        Private mHostIP As String = String.Empty
        Private mHostLocalIP As String = String.Empty
        Private mConnectionDate As Date = New Date(1900, 1, 1)
        Private mConnectionTime As Date = New Date(1900, 1, 1, 0, 0, 0)
        Private mDNSResolutionIP As String = String.Empty
        Private mIPState As String = String.Empty
        Private mIPCity As String = String.Empty
        Private mActivityStatus As String = String.Empty

        Public ReadOnly Property ID() As Long
            Get
                CanReadProperty(True)
                Return Me.mID
            End Get
        End Property

        Public ReadOnly Property SubscriberAccount() As ClsAccountInfo
            Get
                CanReadProperty(True)
                Return Me.mSubscriberAccount
            End Get
        End Property

        Public Property HostIP() As String
            Get
                CanReadProperty(True)
                Return Me.mHostIP
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mHostIP <> value Then
                    Me.mHostIP = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property HostLocalIP() As String
            Get
                CanReadProperty(True)
                Return Me.mHostLocalIP
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mHostLocalIP <> value Then
                    Me.mHostLocalIP = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property ConnectionDate() As DateTime
            Get
                CanReadProperty(True)
                Return Me.mConnectionDate
            End Get
            Set(ByVal value As DateTime)
                CanWriteProperty(True)
                If Me.mConnectionDate <> value Then
                    Me.mConnectionDate = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property ConnectionTime() As Date
            Get
                Return Me.mConnectionTime
            End Get
            Set(ByVal value As DateTime)
                CanWriteProperty(True)
                If Me.mConnectionTime <> value Then
                    Me.mConnectionTime = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property DNSResolutionIP() As String
            Get
                CanReadProperty(True)
                Return Me.mDNSResolutionIP
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mDNSResolutionIP <> value Then
                    Me.mDNSResolutionIP = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property IPState() As String
            Get
                CanReadProperty(True)
                Return Me.mIPState
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mIPState <> value Then
                    Me.mIPState = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Property IPCity() As String
            Get
                CanReadProperty(True)
                Return Me.mIPCity
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mIPCity <> value Then
                    Me.mIPCity = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property ActivityStatus() As String
            Get
                CanReadProperty(True)
                Return Me.mActivityStatus
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mActivityStatus <> value Then
                    Me.mActivityStatus = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Overrides ReadOnly Property IsValid() As Boolean
            Get
                Return MyBase.IsValid
            End Get
        End Property

        Public Overrides ReadOnly Property IsDirty() As Boolean
            Get
                Return MyBase.IsDirty
            End Get
        End Property

        Public Sub AssignSubscriberAccount(ByVal subscriberAccountId As Long)
            Me.mSubscriberAccount = ClsAccountInfo.GetAccountInfo(subscriberAccountId)
        End Sub

        Protected Overrides Function GetIdValue() As Object
            Return Me.mID
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("HostIP", 15))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("HostLocalIP", 15))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("DNSResolutionIP", 100))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("IPState", 15))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("IPCity", 15))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("ActivityStatus", 50))
        End Sub

#End Region

#Region " Authorization Rules "

        'Protected Overrides Sub AddAuthorizationRules()
        '    AuthorizationRules.AllowRead("ID", "")
        '    AuthorizationRules.AllowRead("SubscriberAccount", "")
        '    AuthorizationRules.AllowRead("HostIP", "")
        '    AuthorizationRules.AllowRead("HostLocalIP", "")
        '    AuthorizationRules.AllowRead("ConnectionDate", "")
        '    AuthorizationRules.AllowRead("ConnectionTime", "")
        '    AuthorizationRules.AllowRead("DNSResolutionIP", "")
        '    AuthorizationRules.AllowRead("IPState", "")
        '    AuthorizationRules.AllowRead("IPCity", "")
        '    AuthorizationRules.AllowRead("ActivityStatus", "")
        '    AuthorizationRules.AllowWrite("HostIP", "")
        '    AuthorizationRules.AllowWrite("HostLocalIP", "")
        '    AuthorizationRules.AllowWrite("ConnectionDate", "")
        '    AuthorizationRules.AllowWrite("ConnectionTime", "")
        '    AuthorizationRules.AllowWrite("DNSResolutionIP", "")
        '    AuthorizationRules.AllowWrite("IPState", "")
        '    AuthorizationRules.AllowWrite("IPCity", "")
        '    AuthorizationRules.AllowWrite("ActivityStatus", "")
        'End Sub

        Public Shared Function CanAddObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole("")
        End Function

        Public Shared Function CanGetObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole("")
        End Function

        Public Shared Function CanDeleteObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole("")
        End Function

        Public Shared Function CanEditObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole("")
        End Function

#End Region

#Region " Factory Methods "

#Region " Root Methods "

        Public Shared Function NewConnectionHistory() As ClsConnectionHistory
            If Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add a connection history information")
            End If
            Return DataPortal.Create(Of ClsConnectionHistory)(New RootCriteria(0))
        End Function

        Public Shared Function GetConecctionHistory(ByVal id As Long) As ClsConnectionHistory
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to a connection history information")
            End If
            Return DataPortal.Fetch(Of ClsConnectionHistory)(New RootCriteria(id))
        End Function

        Public Shared Sub DeleteConnectionHistory(ByVal id As Long)
            If Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove a connection history information")
            End If
            DataPortal.Delete(New RootCriteria(id))
        End Sub

        Public Overrides Function Save() As ClsConnectionHistory
            If IsDeleted AndAlso Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove a connection history information")

            ElseIf IsNew AndAlso Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add a connection history information")

            ElseIf Not CanEditObject() Then
                Throw New System.Security.SecurityException("User not authorized to update a connection history information")
            End If
            Return MyBase.Save
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Child Methods "

        Friend Shared Function NewSubscriberConnectionHistory() As ClsConnectionHistory
            If Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add a subscriber connection history information")
            End If
            Return DataPortal.Create(Of ClsConnectionHistory)(New EmptyChildCriteria())
        End Function

        Friend Shared Function NewSubscriberConnectionHistory(ByVal hostIP As String, ByVal hostLocalIP As String) As ClsConnectionHistory
            If Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add a subscriber connection history information")
            End If
            Return DataPortal.Create(Of ClsConnectionHistory)(New ChildCriteria(hostIP, hostLocalIP))
        End Function

        Friend Shared Function GetSubscriberConnectionHistory(ByVal connectionHistory As DAClsappSubscriberConnectionHistory.Struct) As ClsConnectionHistory
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to a subscriber connection history information")
            End If
            Return New ClsConnectionHistory(connectionHistory)
        End Function

        Private Sub New(ByVal connectionHistory As DAClsappSubscriberConnectionHistory.Struct)
            MarkAsChild()
            Fetch(connectionHistory)
        End Sub

#End Region

#End Region

#Region " Data Access "

#Region " Root Area "

        <Serializable()> Private Class RootCriteria

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

        <RunLocal()> Private Overloads Sub DataPortal_Create(ByVal criteria As RootCriteria)
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As RootCriteria)
            Dim List As DAClsappSubscriberConnectionHistory.Struct() = DAClsappSubscriberConnectionHistory.Fetch(criteria.ID)
            If List.Length = 0 Then Throw New System.Security.SecurityException("Connection history information doesn't exist")

            Me.mStruct = List(0)
            LoadObjectData()
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Insert()
            Me.LoadTableStruct(Me.mSubscriberAccount)
            Me.mStruct = DAClsappSubscriberConnectionHistory.Insert(Me.mStruct)
            Me.mID = Me.mStruct.ID.Value
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Update()
            If MyBase.IsDirty Then
                Me.LoadTableStruct(Me.mSubscriberAccount)
                Me.mStruct = DAClsappSubscriberConnectionHistory.Update(Me.mStruct)
            End If
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_DeleteSelf()
            DataPortal_Delete(New RootCriteria(Me.mID))
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Private Overloads Sub DataPortal_Delete(ByVal criteria As RootCriteria)
            DAClsappSubscriberConnectionHistory.Delete(criteria.ID)
        End Sub

#End Region

#Region " Child Area "

        <Serializable()> Private Class EmptyChildCriteria

        End Class

        <Serializable()> Private Class ChildCriteria

            Private mHostIP As String
            Private mHostLocalIP As String

            Public ReadOnly Property HostIP() As String
                Get
                    Return Me.mHostIP
                End Get
            End Property

            Public ReadOnly Property HostLocalIP() As String
                Get
                    Return Me.mHostLocalIP
                End Get
            End Property

            Public Sub New(ByVal hostIP As String, ByVal hostLocalIP As String)
                Me.mHostIP = hostIP
                Me.mHostLocalIP = hostLocalIP
            End Sub

        End Class

        <RunLocal()> Private Overloads Sub DataPortal_Create(ByVal criteria As EmptyChildCriteria)
            MarkAsChild()
        End Sub

        <RunLocal()> Private Overloads Sub DataPortal_Create(ByVal criteria As ChildCriteria)
            Me.mHostIP = criteria.HostIP
            Me.mHostLocalIP = criteria.HostLocalIP
            Me.mConnectionDate = Date.Today
            Me.mConnectionTime = Date.Now
            MarkAsChild()
        End Sub

        Private Sub Fetch(ByVal connectionHistory As DAClsappSubscriberConnectionHistory.Struct)
            Me.mStruct = connectionHistory
            LoadObjectData()
            MarkOld()
        End Sub

        Friend Sub Insert(ByVal parent As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct(parent)
            Me.mStruct = DAClsappSubscriberConnectionHistory.Insert(Me.mStruct)
            Me.mID = Me.mStruct.ID.Value
            MarkOld()
        End Sub

        Friend Sub Update(ByVal parent As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct(parent)
            Me.mStruct = DAClsappSubscriberConnectionHistory.Update(Me.mStruct)
            MarkOld()
        End Sub

        Friend Sub DeleteSelf()
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            ' if we're new then don't update the database
            If Me.IsNew Then Exit Sub

            DAClsappSubscriberConnectionHistory.Delete(Me.mID)
            MarkNew()
        End Sub

#End Region

#Region " Common Area "

        Private mStruct As DAClsappSubscriberConnectionHistory.Struct = New DAClsappSubscriberConnectionHistory.Struct

        Public Function GetTableStruct() As DAClsappSubscriberConnectionHistory.Struct
            Return Me.mStruct
        End Function

        ''' <summary>
        ''' Load the data of the object with the fetched data
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadObjectData()
            With Me.mStruct
                Me.mID = .ID.Value
                Me.mSubscriberAccount = ClsAccountInfo.GetAccountInfo(.IDSubscriber.Value)
                Me.mHostIP = .HostIP.Value
                Me.mHostLocalIP = .HostLocalIP.Value
                Me.mConnectionDate = .ConnectionDate.Value
                Me.mConnectionTime = .ConnectionTime.Value
                Me.mDNSResolutionIP = .DNSResolutionIP.Value
                Me.mIPState = .IPState.Value
                Me.mIPCity = .IPCity.Value
                Me.mActivityStatus = .ActivityStatus.Value
            End With
        End Sub

        ''' <summary>
        ''' Collect the data of the object to fill to a structure of data and returns the structure 
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadTableStruct(ByVal parent As Object)
            With Me.mStruct
                .ID.NewValue = Me.mID
                .IDSubscriber.NewValue = parent.ID
                .HostIP.NewValue = Me.mHostIP
                .HostLocalIP.NewValue = Me.mHostLocalIP
                .ConnectionDate.NewValue = Me.mConnectionDate
                .ConnectionTime.NewValue = Me.mConnectionTime
                .DNSResolutionIP.NewValue = Me.mDNSResolutionIP
                .IPState.NewValue = Me.mIPState
                .IPCity.NewValue = Me.mIPCity
                .ActivityStatus.NewValue = Me.mActivityStatus
            End With
        End Sub

#End Region

#End Region

    End Class
End Namespace