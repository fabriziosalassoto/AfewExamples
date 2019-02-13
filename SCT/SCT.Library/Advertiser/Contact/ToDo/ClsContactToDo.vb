Imports Csla
Imports SCT.DataAccess

Namespace Advertiser
    <Serializable()> Public Class ClsContactToDo
        Inherits BusinessBase(Of ClsContactToDo)

#Region " Business Methods "

        Private mID As Long
        Private mContact As ClsContactInfo = ClsContactInfo.NewContactInfo
        Private mDateEntered As New DateTime
        Private mDateDue As New DateTime
        Private mTaskNotes As String = ""
        Private mDateCompleted As New DateTime
        Private mCallBackRecord As Boolean

        Public ReadOnly Property ID() As Long
            Get
                CanReadProperty(True)
                Return Me.mID
            End Get
        End Property

        Public ReadOnly Property Contact() As ClsContactInfo
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

        Public Property DateDue() As DateTime
            Get
                CanReadProperty(True)
                Return Me.mDateDue
            End Get
            Set(ByVal value As DateTime)
                CanWriteProperty(True)
                If Me.mDateDue <> value Then
                    Me.mDateDue = value
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
            Me.mContact = ClsContactInfo.GetContactInfo(contactId)
        End Sub

        Protected Overrides Function GetIdValue() As Object
            Return Me.mID
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("TaskNotes", 1000))
        End Sub

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

        Friend Shared Function NewContactToDo() As ClsContactToDo
            Return New ClsContactToDo()
        End Function

        Friend Shared Function GetContactToDo(ByVal ToDo As DAClsappAdvertiserToDo.Struct) As ClsContactToDo
            Return New ClsContactToDo(ToDo)
        End Function

        Private Sub New()
            MarkAsChild()
        End Sub

        Private Sub New(ByVal ToDo As DAClsappAdvertiserToDo.Struct)
            MarkAsChild()
            Fetch(ToDo)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal ToDo As DAClsappAdvertiserToDo.Struct)
            With ToDo
                Me.mID = .ID
                Me.mContact = ClsContactInfo.GetContactInfo(.IDAdvertiserContact)
                Me.mDateEntered = .DateEntered
                Me.mDateDue = .DateDue
                Me.mTaskNotes = .TaskNotes
                Me.mDateCompleted = .DateCompleted
                Me.mCallBackRecord = .CallBackRecord
            End With
            MarkOld()
        End Sub

        Friend Sub Insert(ByVal contact As Object)

            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.mID = DAClsappAdvertiserToDo.Insert(Me.GetAdvertiserToDoStruct())
            MarkOld()

        End Sub

        Friend Sub Update(ByVal contact As Object)

            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            DAClsappAdvertiserToDo.Update(Me.GetAdvertiserToDoStruct())
            MarkOld()

        End Sub

        ''' <summary>
        ''' Collect the data of the object to fill to a structure of data and returns the structure 
        ''' </summary>
        ''' <returns>Structure with data</returns>
        ''' <remarks></remarks>
        Private Function GetAdvertiserToDoStruct() As DAClsappAdvertiserToDo.Struct
            Dim Struct As New DAClsappAdvertiserToDo.Struct
            With Struct
                .ID = Me.mID
                .IDAdvertiserContact = Me.Contact.ID
                .DateEntered = Me.mDateEntered
                .DateDue = Me.mDateDue
                .TaskNotes = Me.mTaskNotes
                .DateCompleted = Me.mDateCompleted
                .CallBackRecord = Me.mCallBackRecord
            End With
            Return Struct
        End Function

        Friend Sub DeleteSelf()

            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            ' if we're new then don't update the database
            If Me.IsNew Then Exit Sub

            DAClsappAdvertiserToDo.Delete(Me.mID)
            MarkNew()

        End Sub

#End Region

    End Class
End Namespace