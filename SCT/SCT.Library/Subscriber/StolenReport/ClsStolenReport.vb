Imports Csla
Imports SCT.DataAccess

Namespace Subscriber
    <Serializable()> Public Class ClsStolenReport
        Inherits BusinessBase(Of ClsStolenReport)

#Region " Business Methods "

        Private mID As Long
        Private mSubscriberAccount As ClsAccountInfo = ClsAccountInfo.NewAccountInfo
        Private mIDAsset As Long
        Private mDateReportMissing As Date = "1900-01-01"
        Private mLastKnownLocationDescription As String = String.Empty
        Private mDateReportFound As Date = "1900-01-01"
        Private mActiveForAlerts As Boolean = False
        Private mActionToTake As Integer = 0

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

        Public Property IDAsset() As Long
            Get
                CanReadProperty(True)
                Return Me.mIDAsset
            End Get
            Set(ByVal value As Long)
                CanWriteProperty(True)
                If Me.mIDAsset <> value Then
                    Me.mIDAsset = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property DateReportMissing() As DateTime
            Get
                CanReadProperty(True)
                Return Me.mDateReportMissing
            End Get
            Set(ByVal value As DateTime)
                CanWriteProperty(True)
                If Me.mDateReportMissing <> value Then
                    Me.mDateReportMissing = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property DateReportFound() As DateTime
            Get
                CanReadProperty(True)
                Return Me.mDateReportFound
            End Get
            Set(ByVal value As DateTime)
                CanWriteProperty(True)
                If Me.mDateReportFound <> value Then
                    Me.mDateReportFound = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property LastKnownLocationDescription() As String
            Get
                CanReadProperty(True)
                Return Me.mLastKnownLocationDescription
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mLastKnownLocationDescription <> value Then
                    Me.mLastKnownLocationDescription = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property ActiveForAlerts() As Boolean
            Get
                CanReadProperty(True)
                Return Me.mActiveForAlerts
            End Get
            Set(ByVal value As Boolean)
                CanWriteProperty(True)
                If Me.mActiveForAlerts <> value Then
                    Me.mActiveForAlerts = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property ActionToTake() As Integer
            Get
                CanReadProperty(True)
                Return Me.mActionToTake
            End Get
            Set(ByVal value As Integer)
                CanWriteProperty(True)
                If Me.mActionToTake <> value Then
                    Me.mActionToTake = value
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
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("LastKnownLocationDescription", 100))
        End Sub

#End Region

#Region " Authorization Rules "

        'Protected Overrides Sub AddAuthorizationRules()
        '    AuthorizationRules.AllowRead("ID", "")
        '    AuthorizationRules.AllowRead("SubscriberAccount", "")
        '    AuthorizationRules.AllowRead("IDAsset", "")
        '    AuthorizationRules.AllowRead("DateReportMissing", "")
        '    AuthorizationRules.AllowRead("LastKnownLocationDescription", "")
        '    AuthorizationRules.AllowRead("DateReportFound", "")
        '    AuthorizationRules.AllowRead("ActiveForAlerts", "")
        '    AuthorizationRules.AllowRead("ActionToTake", "")
        '    AuthorizationRules.AllowWrite("IDAsset", "")
        '    AuthorizationRules.AllowWrite("DateReportMissing", "")
        '    AuthorizationRules.AllowWrite("LastKnownLocationDescription", "")
        '    AuthorizationRules.AllowWrite("DateReportFound", "")
        '    AuthorizationRules.AllowWrite("ActiveForAlerts", "")
        '    AuthorizationRules.AllowWrite("ActionToTake", "")
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

        Public Shared Function NewStolenReport() As ClsStolenReport
            If Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add a stolen report")
            End If
            Return DataPortal.Create(Of ClsStolenReport)(New Criteria(0))
        End Function

        Public Shared Function GetStolenReport(ByVal id As Long) As ClsStolenReport
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to a stolen report")
            End If
            Return DataPortal.Fetch(Of ClsStolenReport)(New Criteria(id))
        End Function

        Public Shared Sub DeleteStolenReport(ByVal id As Long)
            If Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove a stolen report")
            End If
            DataPortal.Delete(New Criteria(id))
        End Sub

        Public Overrides Function Save() As ClsStolenReport
            If IsDeleted AndAlso Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove a stolen report")

            ElseIf IsNew AndAlso Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add a stolen report")

            ElseIf Not CanEditObject() Then
                Throw New System.Security.SecurityException("User not authorized to update a stolen report")
            End If
            Return MyBase.Save
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Child Methods "

        Friend Shared Function NewSubscriberStolenReport() As ClsStolenReport
            Dim Child As New ClsStolenReport
            If Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add a subscriber stolen report")
            End If
            Child.MarkAsChild()
            Return Child
        End Function

        Friend Shared Function NewSubscriberStolenReport(ByVal stolenReport As ClsStolenReport) As ClsStolenReport
            If Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add a subscriber stolen report")
            End If
            stolenReport.MarkAsChild()
            Return stolenReport
        End Function

        Friend Shared Function GetSubscriberStolenReport(ByVal stolenReport As DAClsappSubscriberStolenReports.Struct) As ClsStolenReport
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to a subscriber stolen report")
            End If
            Return New ClsStolenReport(stolenReport)
        End Function

        Private Sub New(ByVal stolenReport As DAClsappSubscriberStolenReports.Struct)
            MarkAsChild()
            Fetch(stolenReport)
        End Sub

#End Region

#End Region

#Region " Data Access "

#Region " Root Area "

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

        <RunLocal()> Private Overloads Sub DataPortal_Create(ByVal criteria As Criteria)
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            Dim List As DAClsappSubscriberStolenReports.Struct() = DAClsappSubscriberStolenReports.Fetch(criteria.ID)
            If List.Length = 0 Then Throw New System.Security.SecurityException("Stolen report doesn't exist")

            Me.mStruct = List(0)
            LoadObjectData()
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Insert()
            Me.LoadTableStruct(Me.mSubscriberAccount)
            Me.mStruct = DAClsappSubscriberStolenReports.Insert(Me.mStruct)
            Me.mID = Me.mStruct.ID.Value
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Update()
            If MyBase.IsDirty Then
                Me.LoadTableStruct(Me.mSubscriberAccount)
                Me.mStruct = DAClsappSubscriberStolenReports.Update(Me.mStruct)
            End If
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_DeleteSelf()
            DataPortal_Delete(New Criteria(Me.mID))
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)
            DAClsappSubscriberStolenReports.Delete(criteria.ID)
        End Sub

#End Region

#Region " Child Area "

        Private Sub Fetch(ByVal stolenReport As DAClsappSubscriberStolenReports.Struct)
            Me.mStruct = stolenReport
            LoadObjectData()
            MarkOld()
        End Sub

        Friend Sub Insert(ByVal parent As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct(parent)
            Me.mStruct = DAClsappSubscriberStolenReports.Insert(Me.mStruct)
            Me.mID = Me.mStruct.ID.Value
            MarkOld()
        End Sub

        Friend Sub Update(ByVal parent As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct(parent)
            Me.mStruct = DAClsappSubscriberStolenReports.Update(Me.mStruct)
            MarkOld()
        End Sub

        Friend Sub DeleteSelf()
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            ' if we're new then don't update the database
            If Me.IsNew Then Exit Sub

            DAClsappSubscriberStolenReports.Delete(Me.mID)
            MarkNew()
        End Sub

#End Region

#Region " Common Area "

        Private mStruct As DAClsappSubscriberStolenReports.Struct = New DAClsappSubscriberStolenReports.Struct

        Public Function GetTableStruct() As DAClsappSubscriberStolenReports.Struct
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
                Me.mIDAsset = .IDAsset.Value
                Me.mDateReportMissing = .DateReportMissing.Value
                Me.mDateReportFound = .DateReportFound.Value
                Me.mLastKnownLocationDescription = .LastKnownLocationDescription.Value
                Me.mActiveForAlerts = .ActiveForAlerts.Value
                Me.mActionToTake = .ActionToTake.Value
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
                .IDAsset.NewValue = Me.mIDAsset
                .DateReportMissing.NewValue = Me.mDateReportMissing
                .DateReportFound.NewValue = Me.mDateReportFound
                .LastKnownLocationDescription.NewValue = Me.mLastKnownLocationDescription
                .ActiveForAlerts.NewValue = Me.mActiveForAlerts
                .ActionToTake.NewValue = Me.mActionToTake
            End With
        End Sub

#End Region

#End Region

    End Class
End Namespace