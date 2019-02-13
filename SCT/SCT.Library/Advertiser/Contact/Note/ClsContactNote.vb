Imports Csla
Imports SCT.DataAccess

Namespace Advertiser
    <Serializable()> Public Class ClsContactNote
        Inherits BusinessBase(Of ClsContactNote)

#Region " Business Methods "

        Private mID As Long
        Private mContact As ClsContactInfo = ClsContactInfo.NewContactInfo
        Private mDateEntered As New DateTime
        Private mDescription As String

        Public ReadOnly Property ID() As Long
            Get
                CanReadProperty(True)
                Return Me.mID
            End Get
        End Property

        Public ReadOnly Property IDProject() As ClsContactInfo
            Get
                CanReadProperty(True)
                Return Me.mContact
            End Get
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
            Me.mContact = ClsContactInfo.GetContactInfo(contactId)
        End Sub

        Protected Overrides Function GetIdValue() As Object
            Return Me.mID
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("Description", 1000))
        End Sub

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

        Friend Shared Function NewContactNote() As ClsContactNote
            Return New ClsContactNote()
        End Function

        Friend Shared Function GetContactNote(ByVal note As DAClsappAdvertiserNotes.Struct) As ClsContactNote
            Return New ClsContactNote(note)
        End Function

        Private Sub New()
            MarkAsChild()
        End Sub

        Private Sub New(ByVal note As DAClsappAdvertiserNotes.Struct)
            MarkAsChild()
            Fetch(note)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal note As DAClsappAdvertiserNotes.Struct)
            With note
                Me.mID = .ID
                Me.mContact = ClsContactInfo.GetContactInfo(.IDAdvertiserContact)
                Me.mDateEntered = .DateEntered
                Me.mDescription = .DescriptionOfNotes
            End With
            MarkOld()
        End Sub

        Friend Sub Insert(ByVal contact As Object)

            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.mID = DAClsappAdvertiserNotes.Insert(Me.GetAdvertiserNoteStruct)
            MarkOld()

        End Sub

        Friend Sub Update(ByVal contact As Object)

            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            DAClsappAdvertiserNotes.Update(Me.GetAdvertiserNoteStruct)
            MarkOld()

        End Sub

        ''' <summary>
        ''' Collect the data of the object to fill to a structure of data and returns the structure 
        ''' </summary>
        ''' <returns>Structure with data</returns>
        ''' <remarks></remarks>
        Private Function GetAdvertiserNoteStruct() As DAClsappAdvertiserNotes.Struct
            Dim Struct As New DAClsappAdvertiserNotes.Struct
            With Struct
                .ID = Me.mID
                .IDAdvertiserContact = Me.mContact.ID
                .DateEntered = Me.mDateEntered
                .DescriptionOfNotes = Me.mDescription
            End With
            Return Struct
        End Function

        Friend Sub DeleteSelf()

            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            ' if we're new then don't update the database
            If Me.IsNew Then Exit Sub

            DAClsappAdvertiserNotes.Delete(Me.mID)
            MarkNew()

        End Sub

#End Region

    End Class
End Namespace