Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsForm
    Inherits BusinessBase(Of ClsForm)

#Region " Business Methods "

    Private mID As Long
    Private mDescription As String = String.Empty
    Private mLogDescription As String = String.Empty
    Private mProfiles As ClsFormProfileList = ClsFormProfileList.NewFormProfileList
    Private mGroups As ClsGroupList = ClsGroupList.NewFormGroupList

	Public ReadOnly Property ID() As Long
        Get
            CanReadProperty(True)
            Return Me.mID
        End Get
    End Property

	Public Property Description() As String
        Get
            CanReadProperty(True)
            Return Me.mDescription
        End Get
        Set(ByVal value As String)
            CanWriteProperty(True)
            If Me.mDescription <> value Then
                Me.mDescription = value
                ValidationRules.CheckRules("Description")
                PropertyHasChanged()
            End If
        End Set
    End Property

    Public Property LogDescription() As String
        Get
            CanReadProperty(True)
            Return Me.mLogDescription
        End Get
        Set(ByVal value As String)
            CanWriteProperty(True)
            If Me.mLogDescription <> value Then
                Me.mLogDescription = value
                ValidationRules.CheckRules("LogDescription")
                PropertyHasChanged()
            End If
        End Set
    End Property

	Public ReadOnly Property Groups() As ClsGroupList
        Get
			CanReadProperty(True)
            Return Me.mGroups
        End Get
    End Property

    Public ReadOnly Property Profiles() As ClsFormProfileList
        Get
            CanReadProperty(True)
            Return Me.mProfiles
        End Get
    End Property

	Public Overrides ReadOnly Property IsValid() As Boolean
		Get
            Return MyBase.IsValid AndAlso Me.mGroups.IsValid AndAlso Me.mProfiles.IsValid
       End Get
     End Property

	Public Overrides ReadOnly Property IsDirty() As Boolean
		Get
            Return MyBase.IsDirty OrElse Me.mGroups.IsDirty OrElse Me.mProfiles.IsDirty
		End Get
	End Property

	Protected Overrides Function GetIdValue() As Object
		Return Me.mID
    End Function

    Public Function ContainsUserProfile() As Boolean
        Return Me.mProfiles.ContainsUserProfile
    End Function

    Public Function GetForm() As ClsForm
        Return ClsForm.GetForm(Me.mID)
    End Function

    Public Function CanSelect() As Boolean
        Return Me.mProfiles.CanExecute("PSelect")
    End Function

    Public Function CanInsert() As Boolean
        Return Me.mProfiles.CanExecute("PInsert")
    End Function

    Public Function CanUpdate() As Boolean
        Return Me.mProfiles.CanExecute("PUpdate")
    End Function

    Public Function CanDelete() As Boolean
        Return Me.mProfiles.CanExecute("PDelete")
    End Function

    Public Function CanSelectField(ByVal fieldDescription As String) As Boolean
        Return Me.mGroups.CanExecuteInField(fieldDescription, "PSelect")
    End Function

    Public Function CanInsertField(ByVal fieldDescription As String) As Boolean
        Return Me.mGroups.CanExecuteInField(fieldDescription, "PInsert")
    End Function

    Public Function CanUpdateField(ByVal fieldDescription As String) As Boolean
        Return Me.mGroups.CanExecuteInField(fieldDescription, "PUpdate")
    End Function

    Public Function CanDeleteField(ByVal fieldDescription As String) As Boolean
        Return Me.mGroups.CanExecuteInField(fieldDescription, "PDelete")
    End Function

#End Region

#Region " Validation Rules "

    Protected Overrides Sub AddBusinessRules()
        ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, "Description")
        ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("Description", 100))
        ValidationRules.AddRule(AddressOf ExistsDescription, "Description")

        ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, "LogDescription")
        ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("LogDescription", 50))
    End Sub

    Private Function ExistsDescription(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
        If ClsForm.Exists(Me.mID, Me.mDescription) Then
            e.Description = "Description already assigned to another Form."
            Return False
        Else
            Return True
        End If
    End Function

#End Region

#Region " Authorization Rules "

    'Protected Overrides Sub AddAuthorizationRules()
    '    AuthorizationRules.AllowRead("ID", " ")
    '    AuthorizationRules.AllowRead("Description", " ")
    '    AuthorizationRules.AllowRead("LogDescription", " ")
    '    AuthorizationRules.AllowRead("Groups", "")
    '    AuthorizationRules.AllowRead("Profiles", "")
    '    AuthorizationRules.AllowWrite("Description", New String() {" ", " "})
    '    AuthorizationRules.AllowWrite("LogDescription", New String() {" ", " "})
    'End Sub

    Public Shared Function CanAddObject() As Boolean
        Return True 'ApplicationContext.User.IsInRole(" ")
    End Function

    Public Shared Function CanGetObject() As Boolean
        Return True 'ApplicationContext.User.IsInRole(" ")
    End Function

    Public Shared Function CanDeleteObject() As Boolean
        Return True 'ApplicationContext.User.IsInRole(" ")
    End Function

    Public Shared Function CanEditObject() As Boolean
        Return True 'ApplicationContext.User.IsInRole(" ")
    End Function

#End Region

#Region " Factory Methods "

#Region " Root Methods "

    Public Shared Function NewForm() As ClsForm
        If Not CanAddObject() Then
            Throw New System.Security.SecurityException("User not authorized to add Form records.")
        End If
        Return DataPortal.Create(Of ClsForm)(New IDCriteria(0))
    End Function

    Public Shared Function GetForm(ByVal ID As Long) As ClsForm
        If Not CanGetObject() Then
            Throw New System.Security.SecurityException("User not authorized to view Form records.")
        End If
        Return DataPortal.Fetch(Of ClsForm)(New IDCriteria(ID))
    End Function

    Public Shared Sub DeleteForm(ByVal ID As Long)
        If Not CanDeleteObject() Then
            Throw New System.Security.SecurityException("User not authorized to remove Form records.")
        End If
        DataPortal.Delete(New IDCriteria(ID))
    End Sub

    Public Overrides Function Save() As ClsForm
        If IsDeleted AndAlso Not CanDeleteObject() Then
            Throw New System.Security.SecurityException("User not authorized to remove Form records.")

        ElseIf IsNew AndAlso Not CanAddObject() Then
            Throw New System.Security.SecurityException("User not authorized to add Form records.")

        ElseIf Not CanEditObject() Then
            Throw New System.Security.SecurityException("User not authorized to update Form records.")
        End If
        Return MyBase.Save
    End Function

    Private Sub New()
        ' Require use of factory methods
    End Sub

#End Region

#Region " Child Methods "

    Friend Shared Function NewChildForm() As ClsForm
        Dim Child As New ClsForm
        Child.ValidationRules.CheckRules()
        Child.MarkAsChild()
        Return Child
    End Function

    Friend Shared Function GetChildForm(ByVal Form As DAClsprgForms.Struct) As ClsForm
        Return New ClsForm(Form)
    End Function

    Private Sub New(ByVal Form As DAClsprgForms.Struct)
        MarkAsChild()
        Fetch(Form)
    End Sub

#End Region

#End Region

#Region " Data Access "

#Region " Root Area "

    <Serializable()> Private Class IDCriteria

        Private mID As Long

        Public ReadOnly Property ID() As Long
            Get
                Return mID
            End Get
        End Property

        Public Sub New(ByVal ID As Long)
            mID = ID
        End Sub

    End Class

    <RunLocal()> Private Overloads Sub DataPortal_Create(ByVal criteria As IDCriteria)
        Me.ValidationRules.CheckRules()
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As IDCriteria)
        Dim List As DAClsprgForms.Struct() = DAClsprgForms.Fetch(criteria.ID)
        If List.Length > 0 Then
            Me.mStruct = List(0)
            Me.LoadObjectData()
        Else
            Throw New System.Security.SecurityException("Form doesn't exist")
        End If
    End Sub

    <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Insert()
        Me.LoadTableStruct(Nothing)
        Me.mStruct = DAClsprgForms.Insert(Me.mStruct)
        Me.mID = Me.mStruct.ID.Value
        Me.mGroups.Update(Me)
        Me.mProfiles.Update(Me)
    End Sub

    <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Update()
        If MyBase.IsDirty Then
            Me.LoadTableStruct(Nothing)
            Me.mStruct = DAClsprgForms.Update(Me.mStruct)
        End If
        Me.mGroups.Update(Me)
        Me.mProfiles.Update(Me)
    End Sub

    <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_DeleteSelf()
        DataPortal_Delete(New IDCriteria(Me.mID))
    End Sub

    <Transactional(TransactionalTypes.TransactionScope)> Private Overloads Sub DataPortal_Delete(ByVal criteria As IDCriteria)
        Me.mGroups.Clear()
        Me.mProfiles.Clear()
        Me.mGroups.Update(Me)
        Me.mProfiles.Update(Me)
        DAClsprgForms.Delete(criteria.ID)
    End Sub

#End Region

#Region " Child Area "

    Private Sub Fetch(ByVal Form As DAClsprgForms.Struct)
        Me.mStruct = Form
        Me.LoadObjectData()
        MarkOld()
    End Sub

    Friend Sub Insert(ByVal parent As Object)
        ' if we're not dirty then don't update the database
        If Not Me.IsDirty Then Exit Sub

        Me.LoadTableStruct(Nothing)
        Me.mStruct = DAClsprgForms.Insert(Me.mStruct)
        Me.mID = Me.mStruct.ID.Value
        Me.mGroups.Update(Me)
        Me.mProfiles.Update(Me)
        MarkOld()
    End Sub

    Friend Sub Update(ByVal parent As Object)
        ' if we're not dirty then don't update the database
        If Not Me.IsDirty Then Exit Sub

        Me.LoadTableStruct(Nothing)
        Me.mStruct = DAClsprgForms.Update(Me.mStruct)
        Me.mGroups.Update(Me)
        Me.mProfiles.Update(Me)
        MarkOld()
    End Sub

    Friend Sub DeleteSelf()
        ' if we're not dirty then don't update the database
        If Not Me.IsDirty Then Exit Sub

        ' if we're new then don't update the database
        If Me.IsNew Then Exit Sub

        Me.mGroups.Clear()
        Me.mProfiles.Clear()
        Me.mGroups.Update(Me)
        Me.mProfiles.Update(Me)
        DAClsprgForms.Delete(Me.mID)
        MarkNew()
    End Sub

#End Region

#Region " Common Area "

    Private mStruct As DAClsprgForms.Struct = New DAClsprgForms.Struct

    Public Function GetTableStruct() As DAClsprgForms.Struct
        Return Me.mStruct
    End Function

    ''' <summary>
    ''' Load the data of the object with the fetched data
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadObjectData()
        With Me.mStruct
            Me.mID = .ID.Value
            Me.mDescription = .Description.Value
            Me.mLogDescription = .LogDescription.Value
            Me.mGroups = ClsGroupList.GetFormGroupList(DAClsprgGroups.FetchByForm(.ID.Value))
            Me.mProfiles = ClsFormProfileList.GetFormProfileList(DAClsprgAdministrativeFormPermissions.FetchByForm(.ID.Value))
        End With
    End Sub

    ''' <summary>
    ''' Collect the data of the object to fill to a structure of data and returns the structure 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadTableStruct(ByVal parents() As Object)
        With Me.mStruct
            .ID.NewValue = Me.mID
            .Description.NewValue = Me.mDescription
            .LogDescription.NewValue = Me.mLogDescription
        End With
    End Sub

#End Region

#End Region

#Region " Exists "

    Public Shared Function Exists(ByVal id As Long, ByVal description As String) As Boolean
        Return DataPortal.Execute(Of ExistsCommand)(New ExistsCommand(id, description)).Exists
    End Function

    <Serializable()> Private Class ExistsCommand
        Inherits CommandBase

        Private mID As Long
        Private mDescription As String
        Private mExists As Boolean

        Public ReadOnly Property Exists() As Boolean
            Get
                Return Me.mExists
            End Get
        End Property

        Public Sub New(ByVal id As Long, ByVal description As String)
            Me.mID = id
            Me.mDescription = description
        End Sub

        Protected Overrides Sub DataPortal_Execute()
            Dim forms As DAClsprgForms.Struct() = DAClsprgForms.Fetch(Me.mDescription)
            Me.mExists = forms.Length > 0 AndAlso forms(0).ID.Value <> Me.mID
        End Sub

    End Class

#End Region

End Class
