Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsOperation
    Inherits BusinessBase(Of ClsOperation)

#Region " Business Methods "

    Private mID As Long
	Private mDescription As String = String.Empty

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

	Protected Overrides Function GetIdValue() As Object
		Return Me.mID
	End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()
			ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("Description", 50))
		End Sub

#End Region

#Region " Authorization Rules "

    'Protected Overrides Sub AddAuthorizationRules()
    '    AuthorizationRules.AllowRead("ID", " ")
    '    AuthorizationRules.AllowRead("Description", " ")
    '    AuthorizationRules.AllowWrite("Description", New String() {" ", " "})
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

    Public Shared Function NewOperation() As ClsOperation
        If Not CanAddObject() Then
            Throw New System.Security.SecurityException("User not authorized to add Operation records.")
        End If
        Return DataPortal.Create(Of ClsOperation)(New IDCriteria(0))
    End Function

    Public Shared Function GetOperation(ByVal ID As Long) As ClsOperation
        If Not CanGetObject() Then
            Throw New System.Security.SecurityException("User not authorized to view Operation records.")
        End If
        Return DataPortal.Fetch(Of ClsOperation)(New IDCriteria(ID))
    End Function

    Public Shared Function GetOperation(ByVal Description As String) As ClsOperation
        If Not CanGetObject() Then
            Throw New System.Security.SecurityException("User not authorized to view Operation records.")
        End If
        Return DataPortal.Fetch(Of ClsOperation)(New DescriptionCriteria(Description))
    End Function

    Public Shared Sub DeleteOperation(ByVal ID As Long)
        If Not CanDeleteObject() Then
            Throw New System.Security.SecurityException("User not authorized to remove Operation records.")
        End If
        DataPortal.Delete(New IDCriteria(ID))
    End Sub

    Public Overrides Function Save() As ClsOperation
        If IsDeleted AndAlso Not CanDeleteObject() Then
            Throw New System.Security.SecurityException("User not authorized to remove Operation records.")

        ElseIf IsNew AndAlso Not CanAddObject() Then
            Throw New System.Security.SecurityException("User not authorized to add Operation records.")

        ElseIf Not CanEditObject() Then
            Throw New System.Security.SecurityException("User not authorized to update Operation records.")
        End If
        Return MyBase.Save
    End Function

    Private Sub New()
        ' Require use of factory methods
    End Sub

#End Region

#Region " Child Methods "

	Friend Shared Function NewChildOperation() As ClsOperation
        Dim Child As New ClsOperation
        Child.MarkAsChild()
        Return Child
    End Function

    Friend Shared Function GetChildOperation(ByVal Operation As DAClsprgOperations.Struct) As ClsOperation
        Return New ClsOperation(Operation)
    End Function

    Private Sub New(ByVal Operation As DAClsprgOperations.Struct)
        MarkAsChild()
        Fetch(Operation)
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

    <Serializable()> Private Class DescriptionCriteria

        Private mDescription As String

        Public ReadOnly Property Description() As String
            Get
                Return mDescription
            End Get
        End Property

        Public Sub New(ByVal description As String)
            mDescription = description
        End Sub

    End Class
	
    <RunLocal()> Private Overloads Sub DataPortal_Create(ByVal criteria As IDCriteria)
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As IDCriteria)
        Dim List As DAClsprgOperations.Struct() = DAClsprgOperations.Fetch(criteria.ID)
        If List.Length = 0 Then Throw New System.Security.SecurityException("Operation doesn't exist")

        Me.mStruct = List(0)
        Me.LoadObjectData()
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As DescriptionCriteria)
        Dim List As DAClsprgOperations.Struct() = DAClsprgOperations.Fetch(criteria.Description)
        If List.Length = 0 Then Throw New System.Security.SecurityException("Operation doesn't exist")

        Me.mStruct = List(0)
        Me.LoadObjectData()
    End Sub

    <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Insert()
        Me.LoadTableStruct()
        Me.mStruct = DAClsprgOperations.Insert(Me.mStruct)
        Me.mID = Me.mStruct.ID.Value
    End Sub

    <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Update()
        If MyBase.IsDirty Then
            Me.LoadTableStruct()
            Me.mStruct = DAClsprgOperations.Update(Me.mStruct)
        End If
  End Sub

    <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_DeleteSelf()
        DataPortal_Delete(New IDCriteria(Me.mID))
    End Sub

    <Transactional(TransactionalTypes.TransactionScope)> Private Overloads Sub DataPortal_Delete(ByVal criteria As IDCriteria)
        DAClsprgOperations.Delete(criteria.ID)
    End Sub
	
#End Region

#Region " Child Area "

    Private Sub Fetch(ByVal Operation As DAClsprgOperations.Struct)
        Me.mStruct = Operation
        Me.LoadObjectData()
        MarkOld()
    End Sub

    Friend Sub Insert(ByVal parent As Object)
		' if we're not dirty then don't update the database
		If Not Me.IsDirty Then Exit Sub

        Me.LoadTableStruct()
        Me.mStruct = DAClsprgOperations.Insert(Me.mStruct)
        Me.mID = Me.mStruct.ID.Value
        MarkOld()
	End Sub

    Friend Sub Update(ByVal parent As Object)
		' if we're not dirty then don't update the database
        If Not Me.IsDirty Then Exit Sub

        Me.LoadTableStruct()
        Me.mStruct = DAClsprgOperations.Update(Me.mStruct)
        MarkOld()
    End Sub

    Friend Sub DeleteSelf()
        ' if we're not dirty then don't update the database
		If Not Me.IsDirty Then Exit Sub

        ' if we're new then don't update the database
        If Me.IsNew Then Exit Sub

        DAClsprgOperations.Delete(Me.mID)
        MarkNew()
	End Sub

#End Region

#Region " Common Area "

    Private mStruct As DAClsprgOperations.Struct = New DAClsprgOperations.Struct

    Public Function GetTableStruct() As DAClsprgOperations.Struct
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
        End With
    End Sub

    ''' <summary>
    ''' Collect the data of the object to fill to a structure of data and returns the structure 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadTableStruct()
        With Me.mStruct
            .ID.NewValue = Me.mID
            .Description.NewValue = Me.mDescription
        End With
    End Sub

#End Region

#End Region

#Region " Exists "

    Public Shared Function Exists(ByVal id As Long) As Boolean
        Return DataPortal.Execute(Of IDExistsCommand)(New IDExistsCommand(id)).Exists
    End Function

    <Serializable()> Private Class IDExistsCommand
        Inherits CommandBase

        Private mID As Long
        Private mExists As Boolean

        Public ReadOnly Property Exists() As Boolean
            Get
                Return Me.mExists
            End Get
        End Property

        Public Sub New(ByVal id As Long)
            Me.mID = id
        End Sub

        Protected Overrides Sub DataPortal_Execute()
            Me.mExists = (DAClsprgOperations.Fetch(Me.mID).Length > 0)
        End Sub

    End Class

    Public Shared Function Exists(ByVal description As String) As Boolean
        Return DataPortal.Execute(Of DescriptionExistsCommand)(New DescriptionExistsCommand(description)).Exists
    End Function

    <Serializable()> Private Class DescriptionExistsCommand
        Inherits CommandBase

        Private mDescription As String
        Private mExists As Boolean

        Public ReadOnly Property Exists() As Boolean
            Get
                Return Me.mExists
            End Get
        End Property

        Public Sub New(ByVal description As String)
            Me.mDescription = description
        End Sub

        Protected Overrides Sub DataPortal_Execute()
            Me.mExists = (DAClsprgOperations.Fetch(Me.mDescription).Length > 0)
        End Sub

    End Class

#End Region

End Class
