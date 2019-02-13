Imports Csla
Imports SCT.DataAccess

Namespace Advertiser.Project
    <Serializable()> Public Class ClsDemographic
        Inherits BusinessBase(Of ClsDemographic)

#Region " Business Methods "

        Private mID As Long
        Private mProject As ClsProjectInfo = ClsProjectInfo.NewProjectInfo
        Private mTag As String = String.Empty
        Private mRequirement As String = String.Empty

        Public ReadOnly Property ID() As Long
            Get
                CanReadProperty(True)
                Return Me.mID
            End Get
        End Property

        Public Property Project() As ClsProjectInfo
            Get
                CanReadProperty(True)
                Return Me.mProject
            End Get
            Set(ByVal value As ClsProjectInfo)
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

        Public Property Tag() As String
            Get
                CanReadProperty(True)
                Return Me.mTag
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mTag <> value Then
                    Me.mTag = value
                    ValidationRules.CheckRules("Tag")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property Requirement() As String
            Get
                CanReadProperty(True)
                Return Me.mRequirement
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mRequirement <> value Then
                    Me.mRequirement = value
                    ValidationRules.CheckRules("Requirement")
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

        Protected Overrides Function GetIdValue() As Object
            Return Me.mID
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()
            ValidationRules.AddRule(AddressOf ProjectRequired, "Project")

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, "Tag")
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("Tag", 100))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("Requirement", 100))
        End Sub

        Private Function ProjectRequired(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
            If Me.mProject.ID = 0 Then
                e.Description = "Project required."
                Return False
            Else
                Return True
            End If
        End Function

#End Region

#Region " Authorization Rules "

        'Protected Overrides Sub AddAuthorizationRules()
        '    AuthorizationRules.AllowRead("ID", "")
        '    AuthorizationRules.AllowRead("Project", "")
        '    AuthorizationRules.AllowRead("Tag", "")
        '    AuthorizationRules.AllowRead("Requirement", "")
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
                Throw New System.Security.SecurityException("User not authorized to add advertiser project demographic information")
            End If
            Return DataPortal.Create(Of ClsDemographic)(New Criteria(0))
        End Function

        Public Shared Function GetDemographic(ByVal id As Long) As ClsDemographic
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view the advertiser project demographic information")
            End If
            Return DataPortal.Fetch(Of ClsDemographic)(New Criteria(id))
        End Function

        Public Shared Sub DeleteDemographic(ByVal id As Long)
            If Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove the advertiser project demographic information")
            End If
            DataPortal.Delete(New Criteria(id))
        End Sub

        Public Overrides Function Save() As ClsDemographic
            If IsDeleted AndAlso Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove the advertiser project demographic information")

            ElseIf IsNew AndAlso Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add advertiser project demographic information")

            ElseIf Not CanEditObject() Then
                Throw New System.Security.SecurityException("User not authorized to update the advertiser project demographic information")
            End If
            Return MyBase.Save
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Child Methods "

        Friend Shared Function NewChildDemographic() As ClsDemographic
            Dim Child As New ClsDemographic
            Child.ValidationRules.CheckRules("Project")
            Child.ValidationRules.CheckRules("Tag")
            Child.MarkAsChild()
            Return Child
        End Function

        Friend Shared Function NewChildDemographic(ByVal tag As String, ByVal requirement As String) As ClsDemographic
            Dim Child As New ClsDemographic
            Child.mTag = tag
            Child.mRequirement = requirement
            Child.ValidationRules.CheckRules("Project")
            Child.ValidationRules.CheckRules("Tag")
            Child.MarkAsChild()
            Return Child
        End Function

        Friend Shared Function GetChildDemographic(ByVal demographic As DAClsappAdvertiserDemographics.Struct) As ClsDemographic
            Return New ClsDemographic(demographic)
        End Function

        Friend Shared Function NewProjectDemographic() As ClsDemographic
            Dim Child As New ClsDemographic
            Child.ValidationRules.CheckRules("Tag")
            Child.MarkAsChild()
            Return Child
        End Function

        Friend Shared Function NewProjectDemographic(ByVal tag As String, ByVal requirement As String) As ClsDemographic
            Dim Child As New ClsDemographic
            Child.mTag = tag
            Child.mRequirement = requirement
            Child.ValidationRules.CheckRules("Tag")
            Child.MarkAsChild()
            Return Child
        End Function

        Friend Shared Function GetProjectDemographic(ByVal demographic As DAClsappAdvertiserDemographics.Struct) As ClsDemographic
            Return New ClsDemographic(demographic)
        End Function

        Private Sub New(ByVal demographic As DAClsappAdvertiserDemographics.Struct)
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
            Me.ValidationRules.CheckRules("Project")
            Me.ValidationRules.CheckRules("Tag")
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            Dim List As DAClsappAdvertiserDemographics.Struct() = DAClsappAdvertiserDemographics.Fetch(criteria.ID)
            If List.Length = 0 Then Throw New System.Security.SecurityException("Advertiser Demagraphic information doesn't exist")

            Me.mStruct = List(0)
            LoadObjectData()
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Insert()
            Me.LoadTableStruct(Me.mProject)
            Me.mStruct = DAClsappAdvertiserDemographics.Insert(Me.mStruct)
            Me.mID = Me.mStruct.ID.Value
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Update()
            If MyBase.IsDirty Then
                Me.LoadTableStruct(Me.mProject)
                Me.mStruct = DAClsappAdvertiserDemographics.Update(Me.mStruct)
            End If
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_DeleteSelf()
            DataPortal_Delete(New Criteria(Me.mID))
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)
            DAClsappAdvertiserDemographics.Delete(criteria.ID)
        End Sub

#End Region

#Region " Child Area "

        Private Sub Fetch(ByVal demographic As DAClsappAdvertiserDemographics.Struct)
            Me.mStruct = demographic
            Me.LoadObjectData()
            MarkOld()
        End Sub

        Friend Sub Insert(ByVal parent As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct(parent)
            Me.mStruct = DAClsappAdvertiserDemographics.Insert(Me.mStruct)
            Me.mID = Me.mStruct.ID.Value
            MarkOld()
        End Sub

        Friend Sub Update(ByVal parent As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct(parent)
            Me.mStruct = DAClsappAdvertiserDemographics.Update(Me.mStruct)
            MarkOld()
        End Sub

        Friend Sub DeleteSelf()
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            ' if we're new then don't update the database
            If Me.IsNew Then Exit Sub

            DAClsappAdvertiserDemographics.Delete(Me.mID)
            MarkNew()
        End Sub

#End Region

#Region " Common Area "

        Private mStruct As DAClsappAdvertiserDemographics.Struct = New DAClsappAdvertiserDemographics.Struct

        Public Function GetTableStruct() As DAClsappAdvertiserDemographics.Struct
            Return Me.mStruct
        End Function

        ''' <summary>
        ''' Load the data of the object with the fetched data
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadObjectData()
            With Me.mStruct
                Me.mID = .ID.Value
                Me.mProject = ClsProjectInfo.GetProjectInfo(.IDAdvertiserProject.Value)
                Me.mTag = .DemographicTag.Value
                Me.mRequirement = .DemographicRequirement.Value
            End With
        End Sub

        ''' <summary>
        ''' Collect the data of the object to fill to a structure of data and returns the structure 
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadTableStruct(ByVal parent As Object)
            With Me.mStruct
                .ID.NewValue = Me.mID
                .IDAdvertiserProject.NewValue = parent.ID
                .DemographicTag.NewValue = Me.mTag
                .DemographicRequirement.NewValue = Me.mRequirement
            End With
        End Sub

#End Region

#End Region

    End Class
End Namespace