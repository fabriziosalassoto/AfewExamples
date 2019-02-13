Imports Csla
Imports SCT.DataAccess

Namespace Advertiser.Project
    <Serializable()> Public Class ClsPrice
        Inherits BusinessBase(Of ClsPrice)

#Region " Business Methods "

        Private mID As Long
        Private mProject As ClsProjectInfo = ClsProjectInfo.NewProjectInfo
        Private mCostPerDisplay As Double = 0
        Private mCostPerClickThrough As Double = 0

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

        Public Property CostPerDisplay() As Double
            Get
                CanReadProperty(True)
                Return Me.mCostPerDisplay
            End Get
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If Me.mCostPerDisplay <> value Then
                    Me.mCostPerDisplay = value
                    ValidationRules.CheckRules("CostPerDisplay")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property CostPerClickThrough() As Double
            Get
                CanReadProperty(True)
                Return Me.mCostPerClickThrough
            End Get
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If Me.mCostPerClickThrough <> value Then
                    Me.mCostPerClickThrough = value
                    ValidationRules.CheckRules("CostPerClickThrough")
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
            ValidationRules.AddRule(AddressOf Validation.CommonRules.MinValue(Of Double), New Validation.CommonRules.MinValueRuleArgs(Of Double)("CostPerDisplay", 0))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.MinValue(Of Double), New Validation.CommonRules.MinValueRuleArgs(Of Double)("CostPerClickThrough", 0))
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
        '    AuthorizationRules.AllowRead("CostPerDisplay", "")
        '    AuthorizationRules.AllowRead("TotalPaid", "")
        '    AuthorizationRules.AllowRead("DatePaid", "")
        '    AuthorizationRules.AllowRead("CheckNumber", "")
        '    AuthorizationRules.AllowRead("InvoiceNumber", "")
        '    AuthorizationRules.AllowRead("PaidByClickThrough", "")
        '    AuthorizationRules.AllowRead("PaidByDisplay", "")
        '    AuthorizationRules.AllowWrite("CostPerDisplay", "")
        '    AuthorizationRules.AllowWrite("TotalPaid", "")
        '    AuthorizationRules.AllowWrite("DatePaid", "")
        '    AuthorizationRules.AllowWrite("CheckNumber", "")
        '    AuthorizationRules.AllowWrite("InvoiceNumber", "")
        '    AuthorizationRules.AllowWrite("PaidByClickThrough", "")
        '    AuthorizationRules.AllowWrite("PaidByDisplay", "")
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

        Public Shared Function NewPrice() As ClsPrice
            If Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add project price information")
            End If
            Return DataPortal.Create(Of ClsPrice)(New Criteria(0))
        End Function

        Public Shared Function GetPrice(ByVal id As Long) As ClsPrice
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view the project price information")
            End If
            Return DataPortal.Fetch(Of ClsPrice)(New Criteria(id))
        End Function

        Public Shared Sub DeletePrice(ByVal id As Long)
            If Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove the project price information")
            End If
            DataPortal.Delete(New Criteria(id))
        End Sub

        Public Overrides Function Save() As ClsPrice
            If IsDeleted AndAlso Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove the project price information")

            ElseIf IsNew AndAlso Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add project price information")

            ElseIf Not CanEditObject() Then
                Throw New System.Security.SecurityException("User not authorized to update the project price information")
            End If
            Return MyBase.Save
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Child Methods "

        Friend Shared Function NewChildPrice() As ClsPrice
            Dim Child As New ClsPrice
            Child.ValidationRules.CheckRules("Project")
            Child.MarkAsChild()
            Return Child
        End Function

        Friend Shared Function GetChildPrice(ByVal price As DAClsappAdvertiserProjectPriceInfo.Struct) As ClsPrice
            Return New ClsPrice(price)
        End Function

        Friend Shared Function NewProjectPrice() As ClsPrice
            Dim Child As New ClsPrice
            Child.MarkAsChild()
            Return Child
        End Function

        Friend Shared Function GetProjectPrice(ByVal price As DAClsappAdvertiserProjectPriceInfo.Struct) As ClsPrice
            Return New ClsPrice(price)
        End Function

        Private Sub New(ByVal price As DAClsappAdvertiserProjectPriceInfo.Struct)
            MarkAsChild()
            Fetch(price)
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
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            Dim List As DAClsappAdvertiserProjectPriceInfo.Struct() = DAClsappAdvertiserProjectPriceInfo.Fetch(criteria.ID)
            If List.Length = 0 Then Throw New System.Security.SecurityException("Project price information doesn't exist")

            Me.mStruct = List(0)
            LoadObjectData()
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Insert()
            Me.LoadTableStruct(Me.mProject)
            Me.mStruct = DAClsappAdvertiserProjectPriceInfo.Insert(Me.mStruct)
            Me.mID = Me.mStruct.ID.Value
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Update()
            If MyBase.IsDirty Then
                Me.LoadTableStruct(Me.mProject)
                Me.mStruct = DAClsappAdvertiserProjectPriceInfo.Update(Me.mStruct)
            End If
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_DeleteSelf()
            DataPortal_Delete(New Criteria(Me.mID))
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)
            DAClsappAdvertiserProjectPriceInfo.Delete(criteria.ID)
        End Sub

#End Region

#Region " Child Area "

        Private Sub Fetch(ByVal price As DAClsappAdvertiserProjectPriceInfo.Struct)
            Me.mStruct = price
            LoadObjectData()
            MarkOld()
        End Sub

        Friend Sub Insert(ByVal parent As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct(parent)
            Me.mStruct = DAClsappAdvertiserProjectPriceInfo.Insert(Me.mStruct)
            Me.mID = Me.mStruct.ID.Value
            MarkOld()
        End Sub

        Friend Sub Update(ByVal parent As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct(parent)
            Me.mStruct = DAClsappAdvertiserProjectPriceInfo.Update(Me.mStruct)
            MarkOld()
        End Sub

        Friend Sub DeleteSelf()
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            ' if we're new then don't update the database
            If Me.IsNew Then Exit Sub

            DAClsappAdvertiserProjectPriceInfo.Delete(Me.mID)
            MarkNew()
        End Sub

#End Region

#Region " Common Area "

        Private mStruct As DAClsappAdvertiserProjectPriceInfo.Struct = New DAClsappAdvertiserProjectPriceInfo.Struct

        Public Function GetTableStruct() As DAClsappAdvertiserProjectPriceInfo.Struct
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
                Me.mCostPerDisplay = .CostPerDisplay.Value
                Me.mCostPerClickThrough = .CostPerClickThrough.Value
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
                .CostPerDisplay.NewValue = Me.mCostPerDisplay
                .CostPerClickThrough.NewValue = Me.mCostPerClickThrough
            End With
        End Sub

#End Region

#End Region

    End Class
End Namespace