Imports Csla
Imports SCT.DataAccess

Namespace Advertiser.Contact
    <Serializable()> Public Class ClsToDo
        Inherits BusinessBase(Of ClsToDo)

#Region " Business Methods "

        Private mID As Long
        Private mContact As ClsContactInfo = ClsContactInfo.NewContactInfo
        Private mDateEntered As New Date(1900, 1, 1)
        Private mDateDue As New Date(1900, 1, 1)
        Private mTaskNotes As String = String.Empty
        Private mDateCompleted As New Date(1900, 1, 1)
        Private mCallBackRecord As Boolean

        Public ReadOnly Property ID() As Long
            Get
                CanReadProperty(True)
                Return Me.mID
            End Get
        End Property

        Public Property Contact() As ClsContactInfo
            Get
                CanReadProperty(True)
                Return Me.mContact
            End Get
            Set(ByVal value As ClsContactInfo)
                CanWriteProperty(True)
                If value IsNot Nothing AndAlso value.ID <> 0 Then
                    If Me.mContact.ID <> value.ID Then
                        Me.mContact = value
                        PropertyHasChanged()
                    End If
                Else
                    Throw New System.Security.SecurityException("Contact required.")
                End If
            End Set
        End Property

        Public Property DateEntered() As DateTime
            Get
                CanReadProperty(True)
                Return Me.mDateEntered
            End Get
            Set(ByVal value As DateTime)
                CanWriteProperty(True)
                If Me.mDateEntered <> value Then
                    Me.mDateEntered = value
                    ValidationRules.CheckRules("DateDue")
                    ValidationRules.CheckRules("DateCompleted")
                    ValidationRules.CheckRules("DateEntered")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property DateDue() As DateTime
            Get
                CanReadProperty(True)
                Return Me.mDateDue
            End Get
            Set(ByVal value As DateTime)
                CanWriteProperty(True)
                If Me.mDateDue <> value Then
                    Me.mDateDue = value
                    ValidationRules.CheckRules("DateEntered")
                    ValidationRules.CheckRules("DateDue")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property TaskNotes() As String
            Get
                CanReadProperty(True)
                Return Me.mTaskNotes
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mTaskNotes <> value Then
                    Me.mTaskNotes = value
                    ValidationRules.CheckRules("TaskNotes")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property DateCompleted() As DateTime
            Get
                CanReadProperty(True)
                Return Me.mDateCompleted
            End Get
            Set(ByVal value As DateTime)
                CanWriteProperty(True)
                If Me.mDateCompleted <> value Then
                    Me.mDateCompleted = value
                    ValidationRules.CheckRules("DateEntered")
                    ValidationRules.CheckRules("DateCompleted")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property CallBackRecord() As Boolean
            Get
                CanReadProperty(True)
                Return Me.mCallBackRecord
            End Get
            Set(ByVal value As Boolean)
                CanWriteProperty(True)
                If Me.mCallBackRecord <> value Then
                    Me.mCallBackRecord = value
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

        Public Sub AssignContact(ByVal contactId As Long)
            If contactId <> 0 Then
                If Me.mContact.ID <> contactId Then
                    Me.mContact = ClsContactInfo.GetContactInfo(contactId)
                    PropertyHasChanged("Contact")
                End If
            Else
                Throw New System.Security.SecurityException("Contact required.")
            End If
        End Sub

        Protected Overrides Function GetIdValue() As Object
            Return Me.mID
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()
            ValidationRules.AddRule(AddressOf AdContactRequired, "Contact")

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, "TaskNotes")
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("TaskNotes", 1000))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.MinValue(Of Date), New Validation.CommonRules.MinValueRuleArgs(Of Date)("DateEntered", New Date(1900, 1, 1)))
            ValidationRules.AddRule(AddressOf DateEnteredGTDateDue, "DateEntered")
            ValidationRules.AddRule(AddressOf DateEnteredGTDateCompleted, "DateEntered")

            ValidationRules.AddRule(AddressOf Validation.CommonRules.MinValue(Of Date), New Validation.CommonRules.MinValueRuleArgs(Of Date)("DateDue", New Date(1900, 1, 1)))
            ValidationRules.AddRule(AddressOf DateEnteredGTDateDue, "DateDue")

            ValidationRules.AddRule(AddressOf Validation.CommonRules.MinValue(Of Date), New Validation.CommonRules.MinValueRuleArgs(Of Date)("DateCompleted", New Date(1900, 1, 1)))
            ValidationRules.AddRule(AddressOf DateEnteredGTDateCompleted, "DateCompleted")
        End Sub

        Private Function AdContactRequired(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
            If Me.mContact.ID = 0 Then
                e.Description = "Contact required."
                Return False
            Else
                Return True
            End If
        End Function

        Private Function DateEnteredGTDateDue(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
            If Me.mDateDue.Date > New Date(1900, 1, 1) AndAlso Me.mDateEntered.Date > Me.mDateDue.Date Then
                e.Description = "Date Entered can't be after Date Due."
                Return False
            Else
                Return True
            End If
        End Function

        Private Function DateEnteredGTDateCompleted(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
            If Me.mDateCompleted.Date > New Date(1900, 1, 1) AndAlso Me.mDateEntered.Date > Me.mDateCompleted.Date Then
                e.Description = "Date Entered can't be after Date Completed."
                Return False
            Else
                Return True
            End If
        End Function

#End Region

#Region " Authorization Rules "

        'Protected Overrides Sub AddAuthorizationRules()
        '    AuthorizationRules.AllowRead("ID", "")
        '    AuthorizationRules.AllowRead("Contact", "")
        '    AuthorizationRules.AllowRead("DateEntered", "")
        '    AuthorizationRules.AllowRead("DateDue", "")
        '    AuthorizationRules.AllowRead("TaskNotes", "")
        '    AuthorizationRules.AllowRead("DateCompleted", "")
        '    AuthorizationRules.AllowRead("CallBackRecord", "")
        '    AuthorizationRules.AllowWrite("DateEntered", "")
        '    AuthorizationRules.AllowWrite("DateDue", "")
        '    AuthorizationRules.AllowWrite("TaskNotes", "")
        '    AuthorizationRules.AllowWrite("DateCompleted", "")
        '    AuthorizationRules.AllowWrite("CallBackRecord", "")
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

        Public Shared Function NewToDo() As ClsToDo
            If Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to a ToDo")
            End If
            Return DataPortal.Create(Of ClsToDo)(New Criteria(0))
        End Function

        Public Shared Function GetToDo(ByVal id As Long) As ClsToDo
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view a ToDo")
            End If
            Return DataPortal.Fetch(Of ClsToDo)(New Criteria(id))
        End Function

        Public Shared Sub DeleteToDo(ByVal id As Long)
            If Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove a ToDo")
            End If
            DataPortal.Delete(New Criteria(id))
        End Sub

        Public Overrides Function Save() As ClsToDo
            If IsDeleted AndAlso Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove a ToDo")

            ElseIf IsNew AndAlso Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add a ToDo")

            ElseIf Not CanEditObject() Then
                Throw New System.Security.SecurityException("User not authorized to update a ToDo")
            End If
            Return MyBase.Save
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub


#End Region

#Region " Child Methods "

        Friend Shared Function NewChildToDo() As ClsToDo
            Dim Child As New ClsToDo
            Child.ValidationRules.CheckRules("Contact")
            Child.ValidationRules.CheckRules("TaskNotes")
            Child.MarkAsChild()
            Return Child
        End Function

        Friend Shared Function GetChildToDo(ByVal ToDo As DAClsappAdvertiserToDo.Struct) As ClsToDo
            Return New ClsToDo(ToDo)
        End Function

        Friend Shared Function NewContactToDo() As ClsToDo
            Dim Child As New ClsToDo
            Child.ValidationRules.CheckRules("TaskNotes")
            Child.MarkAsChild()
            Return Child
        End Function

        Friend Shared Function GetContactToDo(ByVal ToDo As DAClsappAdvertiserToDo.Struct) As ClsToDo
            Return New ClsToDo(ToDo)
        End Function

        Private Sub New(ByVal ToDo As DAClsappAdvertiserToDo.Struct)
            MarkAsChild()
            Fetch(ToDo)
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
            Me.ValidationRules.CheckRules("Contact")
            Me.ValidationRules.CheckRules("TaskNotes")
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            Dim List As DAClsappAdvertiserToDo.Struct() = DAClsappAdvertiserToDo.Fetch(criteria.ID)
            If List.Length = 0 Then Throw New System.Security.SecurityException("ToDo doesn't exist")

            Me.mStruct = List(0)
            Me.LoadObjectData()
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Insert()
            Me.LoadTableStruct(Me.mContact)
            Me.mStruct = DAClsappAdvertiserToDo.Insert(Me.mStruct)
            Me.mID = Me.mStruct.ID.Value
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Update()
            If MyBase.IsDirty Then
                Me.LoadTableStruct(Me.mContact)
                Me.mStruct = DAClsappAdvertiserToDo.Update(Me.mStruct)
            End If
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_DeleteSelf()
            DataPortal_Delete(New Criteria(Me.mID))
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)
            DAClsappAdvertiserToDo.Delete(criteria.ID)
        End Sub

#End Region

#Region " Child Area "

        Private Sub Fetch(ByVal ToDo As DAClsappAdvertiserToDo.Struct)

            Me.mStruct = ToDo
            Me.LoadObjectData()
            MarkOld()
        End Sub

        Friend Sub Insert(ByVal parent As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct(parent)
            Me.mStruct = DAClsappAdvertiserToDo.Insert(Me.mStruct)
            Me.mID = Me.mStruct.ID.Value
            MarkOld()
        End Sub

        Friend Sub Update(ByVal parent As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct(parent)
            Me.mStruct = DAClsappAdvertiserToDo.Update(Me.mStruct)
            MarkOld()
        End Sub

        Friend Sub DeleteSelf()
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            ' if we're new then don't update the database
            If Me.IsNew Then Exit Sub

            DAClsappAdvertiserToDo.Delete(Me.mID)
            MarkNew()
        End Sub

#End Region

#Region " Common Area "

        Private mStruct As DAClsappAdvertiserToDo.Struct = New DAClsappAdvertiserToDo.Struct

        Public Function GetTableStruct() As DAClsappAdvertiserToDo.Struct
            Return Me.mStruct
        End Function

        ''' <summary>
        ''' Load the data of the object with the fetched data
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadObjectData()
            With Me.mStruct
                Me.mID = .ID.Value
                Me.mContact = ClsContactInfo.GetContactInfo(.IDAdvertiserContact.Value)
                Me.mDateEntered = .DateEntered.Value
                Me.mDateDue = .DateDue.Value
                Me.mTaskNotes = .TaskNotes.Value
                Me.mDateCompleted = .DateCompleted.Value
                Me.mCallBackRecord = .CallBackRecord.Value
            End With
        End Sub

        ''' <summary>
        ''' Collect the data of the object to fill to a structure of data and returns the structure 
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadTableStruct(ByVal parent)
            With Me.mStruct
                .ID.NewValue = Me.mID
                .IDAdvertiserContact.NewValue = parent.ID
                .DateEntered.NewValue = Me.mDateEntered
                .DateDue.NewValue = Me.mDateDue
                .TaskNotes.NewValue = Me.mTaskNotes
                .DateCompleted.NewValue = Me.mDateCompleted
                .CallBackRecord.NewValue = Me.mCallBackRecord
            End With
        End Sub

#End Region

#End Region

    End Class
End Namespace