Imports Csla
Imports SCT.DataAccess

Namespace Advertiser.Project
    <Serializable()> Public Class ClsAdHistory
        Inherits BusinessBase(Of ClsAdHistory)

#Region " Business Methods "

        Private mID As Long
        Private mProject As Advertiser.ClsProjectInfo = Advertiser.ClsProjectInfo.NewProjectInfo
        Private mSubscriber As Subscriber.ClsAccountInfo = Subscriber.ClsAccountInfo.NewAccountInfo
        Private mDateAdDisplay As Date = New Date(1900, 1, 1)
        Private mTimeAdDisplay As Date = New Date(1900, 1, 1, 0, 0, 0)
        Private mDateAdClickThru As Date = New Date(1900, 1, 1)
        Private mTimeAdClickThru As Date = New Date(1900, 1, 1, 0, 0, 0)
        Private mURLAdDisplay As String = String.Empty
        Private mURLAdClickThru As String = String.Empty

        Public ReadOnly Property ID() As Long
            Get
                CanReadProperty(True)
                Return Me.mID
            End Get
        End Property

        Public Property Project() As Advertiser.ClsProjectInfo
            Get
                CanReadProperty(True)
                Return Me.mProject
            End Get
            Set(ByVal value As Advertiser.ClsProjectInfo)
                CanWriteProperty(True)
                If value IsNot Nothing AndAlso value.ID <> 0 Then
                    If Me.mProject.ID <> value.ID Then
                        Me.mProject = value
                        PropertyHasChanged()
                    End If
                Else
                    Throw New System.Security.SecurityException("Project required.")
                End If
            End Set
        End Property

        Public Property SubscriberAccount() As Subscriber.ClsAccountInfo
            Get
                CanReadProperty(True)
                Return Me.mSubscriber
            End Get
            Set(ByVal value As Subscriber.ClsAccountInfo)
                CanWriteProperty(True)
                If value IsNot Nothing AndAlso value.ID <> 0 Then
                    If Me.mSubscriber.ID <> value.ID Then
                        Me.mSubscriber = value
                        PropertyHasChanged()
                    End If
                Else
                    Throw New System.Security.SecurityException("Subscriber Account required.")
                End If
            End Set
        End Property

        Public Property DateAdDisplay() As Date
            Get
                CanReadProperty(True)
                Return Me.mDateAdDisplay
            End Get
            Set(ByVal value As DateTime)
                CanWriteProperty(True)
                If Me.mDateAdDisplay <> value Then
                    Me.mDateAdDisplay = value
                    ValidationRules.CheckRules("DateAdDisplay")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property TimeAdDisplay() As Date
            Get
                CanReadProperty(True)
                Return Me.mTimeAdDisplay
            End Get
            Set(ByVal value As DateTime)
                CanWriteProperty(True)
                If Me.mTimeAdDisplay <> value Then
                    Me.mTimeAdDisplay = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property DateAdClickThru() As Date
            Get
                CanReadProperty(True)
                Return Me.mDateAdClickThru
            End Get
            Set(ByVal value As DateTime)
                CanWriteProperty(True)
                If Me.mDateAdClickThru <> value Then
                    Me.mDateAdClickThru = value
                    ValidationRules.CheckRules("DateAdClickThru")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property TimeAdClickThru() As Date
            Get
                CanReadProperty(True)
                Return Me.mTimeAdClickThru
            End Get
            Set(ByVal value As DateTime)
                CanWriteProperty(True)
                If Me.mTimeAdClickThru <> value Then
                    Me.mTimeAdClickThru = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property URLAdDisplay() As String
            Get
                CanReadProperty(True)
                Return Me.mURLAdDisplay
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mURLAdDisplay <> value Then
                    Me.mURLAdDisplay = value
                    ValidationRules.CheckRules("URLAdDisplay")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property URLAdClickThru() As String
            Get
                CanReadProperty(True)
                Return Me.mURLAdClickThru
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mURLAdClickThru <> value Then
                    Me.mURLAdClickThru = value
                    ValidationRules.CheckRules("URLAdClickThru")
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

        Public Sub AssignProject(ByVal projectId As Long)
            If projectId <> 0 Then
                If Me.mProject.ID <> projectId Then
                    Me.mProject = ClsProjectInfo.GetProjectInfo(projectId)
                    PropertyHasChanged("Project")
                End If
            Else
                Throw New System.Security.SecurityException("Project required.")
            End If
        End Sub

        Public Sub AssignSubscriberAccount(ByVal subscriberId As Long)
            If subscriberId <> 0 Then
                If Me.mSubscriber.ID <> subscriberId Then
                    Me.mSubscriber = Subscriber.ClsAccountInfo.GetAccountInfo(subscriberId)
                    PropertyHasChanged("SubscriberAccount")
                End If
            Else
                Throw New System.Security.SecurityException("Subscriber Account required.")
            End If
        End Sub

        Protected Overrides Function GetIdValue() As Object
            Return Me.mID
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()
            ValidationRules.AddRule(AddressOf ProjectRequired, "Project")
            ValidationRules.AddRule(AddressOf SubscriberAccountRequired, "SubscriberAccount")
            ValidationRules.AddRule(AddressOf Validation.CommonRules.MinValue(Of Date), New Validation.CommonRules.MinValueRuleArgs(Of Date)("DateAdDisplay", New Date(1900, 1, 1)))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.MinValue(Of Date), New Validation.CommonRules.MinValueRuleArgs(Of Date)("DateAdClickThru", New Date(1900, 1, 1)))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("URLAdDisplay", 200))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("URLAdClickThru ", 200))
        End Sub

        Private Function ProjectRequired(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
            If Me.mProject.ID = 0 Then
                e.Description = "Project Account required."
                Return False
            Else
                Return True
            End If
        End Function

        Private Function SubscriberAccountRequired(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
            If Me.mSubscriber.ID = 0 Then
                e.Description = "Subscriber required."
                Return False
            Else
                Return True
            End If
        End Function

#End Region

#Region " Authorization Rules "

        'Protected Overrides Sub AddAuthorizationRules()
        '    AuthorizationRules.AllowRead("ID", "")
        '    AuthorizationRules.AllowRead("AdvertiserAccount", "")
        '    AuthorizationRules.AllowRead("SubscriberAccount", "")
        '    AuthorizationRules.AllowRead("DateAdDisplay", "")
        '    AuthorizationRules.AllowRead("TimeAdDisplay", "")
        '    AuthorizationRules.AllowRead("DateAdClickThru", "")
        '    AuthorizationRules.AllowRead("TimeAdClickThru", "")
        '    AuthorizationRules.AllowRead("URLAdDisplay", "")
        '    AuthorizationRules.AllowRead("URLAdClickThru", "")
        '    AuthorizationRules.AllowWrite("DateAdDisplay", "")
        '    AuthorizationRules.AllowWrite("TimeAdDisplay", "")
        '    AuthorizationRules.AllowWrite("DateAdClickThru", "")
        '    AuthorizationRules.AllowWrite("TimeAdClickThru", "")
        '    AuthorizationRules.AllowWrite("URLAdDisplay", "")
        '    AuthorizationRules.AllowWrite("URLAdClickThru", "")
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

        Public Shared Function NewAdHistory() As ClsAdHistory
            If Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add a AdHistory")
            End If
            Return DataPortal.Create(Of ClsAdHistory)(New Criteria(0))
        End Function

        Public Shared Function GetAdHistory(ByVal id As Long) As ClsAdHistory
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view a AdHistory")
            End If
            Return DataPortal.Fetch(Of ClsAdHistory)(New Criteria(id))
        End Function

        Public Shared Sub DeleteAdHistory(ByVal id As Long)
            If Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove a AdHistory")
            End If
            DataPortal.Delete(New Criteria(id))
        End Sub

        Public Overrides Function Save() As ClsAdHistory
            If IsDeleted AndAlso Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove a AdHistory")

            ElseIf IsNew AndAlso Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add project a AdHistory")

            ElseIf Not CanEditObject() Then
                Throw New System.Security.SecurityException("User not authorized to update a AdHistory")
            End If
            Return MyBase.Save
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Child Methods "

        Friend Shared Function NewChildAdHistory() As ClsAdHistory
            Dim Child As New ClsAdHistory
            Child.ValidationRules.CheckRules("SubscriberAccount")
            Child.ValidationRules.CheckRules("Project")
            Child.MarkAsChild()
            Return Child
        End Function

        Friend Shared Function NewChildAdHistory(ByVal projectId As Long, ByVal subscriberId As Long) As ClsAdHistory
            Dim Child As New ClsAdHistory
            Child.AssignProject(projectId)
            Child.AssignSubscriberAccount(subscriberId)
            Child.MarkAsChild()
            Return Child
        End Function

        Friend Shared Function GetChildAdHistory(ByVal AdHistory As DAClsappAdvertiserAdHistory.Struct) As ClsAdHistory
            Return New ClsAdHistory(AdHistory)
        End Function

        Friend Shared Function NewProjectAdHistory(ByVal subscriberId As Long) As ClsAdHistory
            Dim Child As New ClsAdHistory
            Child.AssignSubscriberAccount(subscriberId)
            Child.MarkAsChild()
            Return Child
        End Function

        Friend Shared Function NewProjectAdHistory() As ClsAdHistory
            Dim Child As New ClsAdHistory
            Child.MarkAsChild()
            Return Child
        End Function

        Friend Shared Function GetProjectAdHistory(ByVal AdHistory As DAClsappAdvertiserAdHistory.Struct) As ClsAdHistory
            Return New ClsAdHistory(AdHistory)
        End Function

        Friend Shared Function NewSubscriberAdHistory() As ClsAdHistory
            Dim Child As New ClsAdHistory
            Child.MarkAsChild()
            Return Child
        End Function

        Friend Shared Function NewSubscriberAdHistory(ByVal projectId As Long) As ClsAdHistory
            Dim Child As New ClsAdHistory
            Child.AssignProject(projectId)
            Child.MarkAsChild()
            Return Child
        End Function

        Friend Shared Function GetSubscriberAdHistory(ByVal AdHistory As DAClsappAdvertiserAdHistory.Struct) As ClsAdHistory
            Return New ClsAdHistory(AdHistory)
        End Function

        Private Sub New(ByVal AdHistory As DAClsappAdvertiserAdHistory.Struct)
            MarkAsChild()
            Fetch(AdHistory)
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
            Me.ValidationRules.CheckRules("SubscriberAccount")
            Me.ValidationRules.CheckRules("Project")
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            Dim List As DAClsappAdvertiserAdHistory.Struct() = DAClsappAdvertiserAdHistory.Fetch(criteria.ID)
            If List.Length = 0 Then Throw New System.Security.SecurityException("AdHistory record doesn't exist")

            Me.mStruct = List(0)
            Me.LoadObjectData()
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Insert()
            Me.LoadTableStruct(New Object() {Me.mProject, Me.mSubscriber})
            Me.mStruct = DAClsappAdvertiserAdHistory.Insert(Me.mStruct)
            Me.mID = Me.mStruct.ID.Value
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Update()
            If MyBase.IsDirty Then
                Me.LoadTableStruct(New Object() {Me.mProject, Me.mSubscriber})
                Me.mStruct = DAClsappAdvertiserAdHistory.Update(Me.mStruct)
            End If
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_DeleteSelf()
            DataPortal_Delete(New Criteria(Me.mID))
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)
            DAClsappAdvertiserAdHistory.Delete(criteria.ID)
        End Sub

#End Region

#Region " Child Area "

        Private Sub Fetch(ByVal AdHistory As DAClsappAdvertiserAdHistory.Struct)
            Me.mStruct = AdHistory
            Me.LoadObjectData()
            MarkOld()
        End Sub

        Friend Sub Insert(ByVal parents() As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct(parents)
            Me.mStruct = DAClsappAdvertiserAdHistory.Insert(Me.mStruct)
            Me.mID = Me.mStruct.ID.Value
            MarkOld()
        End Sub

        Friend Sub Update(ByVal parents() As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct(parents)
            Me.mStruct = DAClsappAdvertiserAdHistory.Update(Me.mStruct)
            MarkOld()
        End Sub

        Friend Sub DeleteSelf()
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            ' if we're new then don't update the database
            If Me.IsNew Then Exit Sub

            DAClsappAdvertiserAdHistory.Delete(Me.mID)
            MarkNew()
        End Sub

#End Region

#Region " Common Area "

        Private mStruct As DAClsappAdvertiserAdHistory.Struct = New DAClsappAdvertiserAdHistory.Struct

        Public Function GetTableStruct() As DAClsappAdvertiserAdHistory.Struct
            Return Me.mStruct
        End Function

        ''' <summary>
        ''' Load the data of the object with the fetched data
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadObjectData()
            With Me.mStruct
                Me.mID = .ID.Value
                Me.mProject = Advertiser.ClsProjectInfo.GetProjectInfo(.IDProject.Value)
                Me.mSubscriber = Subscriber.ClsAccountInfo.GetAccountInfo(.IDSubscriber.Value)
                Me.mDateAdDisplay = .DateAdDisplay.Value
                Me.mTimeAdDisplay = .TimeAdDisplay.Value
                Me.mDateAdClickThru = .DateAdClickThru.Value
                Me.mTimeAdClickThru = .TimeAdClickThru.Value
                Me.mURLAdDisplay = .URLAdDisplay.Value
                Me.mURLAdClickThru = .URLAdClickThru.Value
            End With
        End Sub

        ''' <summary>
        ''' Collect the data of the object to fill to a structure of data and returns the structure 
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadTableStruct(ByVal parents() As Object)
            With Me.mStruct
                .ID.NewValue = Me.mID
                .IDProject.NewValue = parents(0).ID
                .IDSubscriber.NewValue = parents(1).ID
                .DateAdDisplay.NewValue = Me.mDateAdDisplay
                .TimeAdDisplay.NewValue = Me.mTimeAdDisplay
                .DateAdClickThru.NewValue = Me.mDateAdClickThru
                .TimeAdClickThru.NewValue = Me.mTimeAdClickThru
                .URLAdDisplay.NewValue = Me.mURLAdDisplay
                .URLAdClickThru.NewValue = Me.mURLAdClickThru
            End With
        End Sub

#End Region

#End Region

    End Class
End Namespace