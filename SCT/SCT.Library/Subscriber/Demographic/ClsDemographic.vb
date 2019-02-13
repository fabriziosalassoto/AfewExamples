Imports Csla
Imports SCT.DataAccess

Namespace Subscriber
    <Serializable()> Public Class ClsDemographic
        Inherits BusinessBase(Of ClsDemographic)

#Region " Business Methods "

        Private mID As Long
        Private mSubscriberAccount As ClsAccountInfo = ClsAccountInfo.NewAccountInfo
        Private mTag As String = String.Empty
        Private mAnswer As String = String.Empty

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

        Public Property Tag() As String
            Get
                CanReadProperty(True)
                Return Me.mTag
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mTag <> value Then
                    Me.mTag = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property Answer() As String
            Get
                CanReadProperty(True)
                Return Me.mAnswer
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mAnswer <> value Then
                    Me.mAnswer = value
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
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("Tag", 100))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("Answer", 100))
        End Sub

#End Region

#Region " Authorization Rules "

        'Protected Overrides Sub AddAuthorizationRules()
        '    AuthorizationRules.AllowRead("ID", "")
        '    AuthorizationRules.AllowRead("SubscriberAccount", "")
        '    AuthorizationRules.AllowRead("Tag", "")
        '    AuthorizationRules.AllowRead("Answer", "")
        '    AuthorizationRules.AllowWrite("Tag", "")
        '    AuthorizationRules.AllowWrite("Requirement", "")
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

        Public Shared Function NewDemographic() As ClsDemographic
            If Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add subscriber demographic information")
            End If
            Return DataPortal.Create(Of ClsDemographic)(New Criteria(0))
        End Function

        Public Shared Function GetDemographic(ByVal id As Long) As ClsDemographic
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view the subscriber demographic information")
            End If
            Return DataPortal.Fetch(Of ClsDemographic)(New Criteria(id))
        End Function

        Public Shared Sub DeleteDemographic(ByVal id As Long)
            If Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove the subscriber demographic information")
            End If
            DataPortal.Delete(New Criteria(id))
        End Sub

        Public Overrides Function Save() As ClsDemographic
            If IsDeleted AndAlso Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove the subscriber demographic information")

            ElseIf IsNew AndAlso Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add subscriber demographic information")

            ElseIf Not CanEditObject() Then
                Throw New System.Security.SecurityException("User not authorized to update the subscriber demographic information")
            End If
            Return MyBase.Save
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Child Methods "

        Friend Shared Function NewSubscriberDemographic() As ClsDemographic
            If Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add subscriber demographic information")
            End If
            Return DataPortal.Create(Of ClsDemographic)(New EmptyChildCriteria())
        End Function

        Friend Shared Function NewSubscriberDemographic(ByVal tag As String, ByVal answer As String) As ClsDemographic
            If Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add subscriber demographic information")
            End If
            Return DataPortal.Create(Of ClsDemographic)(New ChildCriteria(tag, answer))
        End Function

        Friend Shared Function GetSubscriberDemographic(ByVal demographic As DAClsappSubscribersDemographics.Struct) As ClsDemographic
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view the subscriber demographic information")
            End If
            Return New ClsDemographic(demographic)
        End Function

        Private Sub New(ByVal demographic As DAClsappSubscribersDemographics.Struct)
            MarkAsChild()
            Fetch(demographic)
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
            Dim List As DAClsappSubscribersDemographics.Struct() = DAClsappSubscribersDemographics.Fetch(criteria.ID)
            If List.Length = 0 Then Throw New System.Security.SecurityException("Subscriber demagraphic information doesn't exist")

            Me.mStruct = List(0)
            LoadObjectData()
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Insert()
            Me.LoadTableStruct(Me.mSubscriberAccount)
            Me.mStruct = DAClsappSubscribersDemographics.Insert(Me.mStruct)
            Me.mID = Me.mStruct.ID.Value
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Update()
            If MyBase.IsDirty Then
                Me.LoadTableStruct(Me.mSubscriberAccount)
                Me.mStruct = DAClsappSubscribersDemographics.Update(Me.mStruct)
            End If
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_DeleteSelf()
            DataPortal_Delete(New Criteria(Me.mID))
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)
            DAClsappSubscribersDemographics.Delete(criteria.ID)
        End Sub

#End Region

#Region " Child Area "

        <Serializable()> Private Class EmptyChildCriteria

        End Class

        <Serializable()> Private Class ChildCriteria

            Private mTag As String
            Private mAnswer As String

            Public ReadOnly Property Tag() As String
                Get
                    Return Me.mTag
                End Get
            End Property

            Public ReadOnly Property Answer() As String
                Get
                    Return Me.mAnswer
                End Get
            End Property

            Public Sub New(ByVal tag As String, ByVal answer As String)
                Me.mTag = tag
                Me.mAnswer = answer
            End Sub

        End Class

        <RunLocal()> Private Overloads Sub DataPortal_Create(ByVal criteria As EmptyChildCriteria)
            MarkAsChild()
        End Sub

        <RunLocal()> Private Overloads Sub DataPortal_Create(ByVal criteria As ChildCriteria)
            Me.mTag = criteria.Tag
            Me.mAnswer = criteria.Answer
            MarkAsChild()
        End Sub

        Private Sub Fetch(ByVal demographic As DAClsappSubscribersDemographics.Struct)
            Me.mStruct = demographic
            LoadObjectData()
            MarkOld()
        End Sub

        Friend Sub Insert(ByVal parent As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct(parent)
            Me.mStruct = DAClsappSubscribersDemographics.Insert(Me.mStruct)
            Me.mID = Me.mStruct.ID.Value
            MarkOld()
        End Sub

        Friend Sub Update(ByVal parent As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct(parent)
            Me.mStruct = DAClsappSubscribersDemographics.Update(Me.mStruct)
            MarkOld()
        End Sub

        Friend Sub DeleteSelf()
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            ' if we're new then don't update the database
            If Me.IsNew Then Exit Sub

            DAClsappSubscribersDemographics.Delete(Me.mID)
            MarkNew()
        End Sub

#End Region

#Region " Common Area "

        Private mStruct As DAClsappSubscribersDemographics.Struct = New DAClsappSubscribersDemographics.Struct

        Public Function GetTableStruct() As DAClsappSubscribersDemographics.Struct
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
                Me.mTag = .DemographicTag.Value
                Me.mAnswer = .DemographicAnswer.Value
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
                .DemographicTag.NewValue = Me.mTag
                .DemographicAnswer.NewValue = Me.mAnswer
            End With
        End Sub

#End Region

#End Region

    End Class
End Namespace