Imports Csla
Imports SCT.DataAccess

Namespace Advertiser.Contact
    <Serializable()> Public Class ClsNote
        Inherits BusinessBase(Of ClsNote)

#Region " Business Methods "

        Private mID As Long
        Private mContact As ClsContactInfo = ClsContactInfo.NewContactInfo
        Private mDateEntered As New Date(1900, 1, 1)
        Private mDescription As String

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
                    ValidationRules.CheckRules("DateEntered")
                    PropertyHasChanged()
                End If
            End Set
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
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("Description", 1000))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.MinValue(Of Date), New Validation.CommonRules.MinValueRuleArgs(Of Date)("DateEntered", New Date(1900, 1, 1)))
        End Sub

        Private Function AdContactRequired(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
            If Me.mContact.ID = 0 Then
                e.Description = "Contact required."
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
        '    AuthorizationRules.AllowRead("Description", "")
        '    AuthorizationRules.AllowWrite("DateEntered", "")
        '    AuthorizationRules.AllowWrite("Description", "")
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

        Public Shared Function NewNote() As ClsNote
            If Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to a note")
            End If
            Return DataPortal.Create(Of ClsNote)(New Criteria(0))
        End Function

        Public Shared Function GetNote(ByVal id As Long) As ClsNote
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view a note")
            End If
            Return DataPortal.Fetch(Of ClsNote)(New Criteria(id))
        End Function

        Public Shared Sub DeleteNote(ByVal id As Long)
            If Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove a note")
            End If
            DataPortal.Delete(New Criteria(id))
        End Sub

        Public Overrides Function Save() As ClsNote
            If IsDeleted AndAlso Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove a note")

            ElseIf IsNew AndAlso Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add a note")

            ElseIf Not CanEditObject() Then
                Throw New System.Security.SecurityException("User not authorized to update a note")
            End If
            Return MyBase.Save
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

#End Region

#Region " Child Methods "

        Friend Shared Function NewChildNote() As ClsNote
            Dim Child As New ClsNote
            Child.ValidationRules.CheckRules("Contact")
            Child.ValidationRules.CheckRules("Description")
            Child.MarkAsChild()
            Return Child
        End Function

        Friend Shared Function GetChildNote(ByVal note As DAClsappAdvertiserNotes.Struct) As ClsNote
            Return New ClsNote(note)
        End Function

        Friend Shared Function NewContactNote() As ClsNote
            Dim Child As New ClsNote
            Child.ValidationRules.CheckRules("Description")
            Child.MarkAsChild()
            Return Child
        End Function

        Friend Shared Function GetContactNote(ByVal note As DAClsappAdvertiserNotes.Struct) As ClsNote
            Return New ClsNote(note)
        End Function

        Private Sub New(ByVal note As DAClsappAdvertiserNotes.Struct)
            MarkAsChild()
            Fetch(note)
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
            Me.ValidationRules.CheckRules("Description")
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            Dim List As DAClsappAdvertiserNotes.Struct() = DAClsappAdvertiserNotes.Fetch(criteria.ID)
            If List.Length = 0 Then Throw New System.Security.SecurityException("Note doesn't exist")

            Me.mStruct = List(0)
            Me.LoadObjectData()
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Insert()
            Me.LoadTableStruct(Me.mContact)
            Me.mStruct = DAClsappAdvertiserNotes.Insert(Me.mStruct)
            Me.mID = Me.mStruct.ID.Value
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Update()
            If MyBase.IsDirty Then
                Me.LoadTableStruct(Me.mContact)
                Me.mStruct = DAClsappAdvertiserNotes.Update(Me.mStruct)
            End If
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_DeleteSelf()
            DataPortal_Delete(New Criteria(Me.mID))
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)
            DAClsappAdvertiserNotes.Delete(criteria.ID)
        End Sub

#End Region

#Region " Child Area "

        Private Sub Fetch(ByVal note As DAClsappAdvertiserNotes.Struct)
            Me.mStruct = note
            Me.LoadObjectData()
            MarkOld()
        End Sub

        Friend Sub Insert(ByVal parent As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct(parent)
            Me.mStruct = DAClsappAdvertiserNotes.Insert(Me.mStruct)
            Me.mID = Me.mStruct.ID.Value
            MarkOld()
        End Sub

        Friend Sub Update(ByVal parent As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct(parent)
            Me.mStruct = DAClsappAdvertiserNotes.Update(Me.mStruct)
            MarkOld()
        End Sub

        Friend Sub DeleteSelf()
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            ' if we're new then don't update the database
            If Me.IsNew Then Exit Sub

            DAClsappAdvertiserNotes.Delete(Me.mID)
            MarkNew()
        End Sub

#End Region

#Region " Common Area "

        Private mStruct As DAClsappAdvertiserNotes.Struct = New DAClsappAdvertiserNotes.Struct

        Public Function GetTableStruct() As DAClsappAdvertiserNotes.Struct
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
                Me.mDescription = .DescriptionOfNotes.Value
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
                .DescriptionOfNotes.NewValue = Me.mDescription
            End With
        End Sub

#End Region

#End Region

    End Class
End Namespace