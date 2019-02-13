Imports Csla
Imports SCT.DataAccess

Namespace Advertiser
    <Serializable()> Public Class ClsToDoInfo
        Inherits ReadOnlyBase(Of ClsToDoInfo)

#Region " Business Methods "

        Private mID As Long
        Private mContact As ClsContactInfo = ClsContactInfo.NewContactInfo
        Private mDateEntered As Date = "1900-01-01"
        Private mDateDue As Date = "1900-01-01"
        Private mTaskNotes As String = String.Empty
        Private mDateCompleted As Date = "1900-01-01"
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

        Public ReadOnly Property DateEntered() As DateTime
            Get
                CanReadProperty(True)
                Return Me.mDateEntered
            End Get
        End Property

        Public ReadOnly Property DateDue() As DateTime
            Get
                CanReadProperty(True)
                Return Me.mDateDue
            End Get
        End Property

        Public ReadOnly Property TaskNotes() As String
            Get
                CanReadProperty(True)
                Return Me.mTaskNotes
            End Get
        End Property

        Public ReadOnly Property DateCompleted() As DateTime
            Get
                CanReadProperty(True)
                Return Me.mDateCompleted
            End Get
        End Property

        Public ReadOnly Property CallBackRecord() As Boolean
            Get
                CanReadProperty(True)
                Return Me.mCallBackRecord
            End Get
        End Property

        Protected Overrides Function GetIdValue() As Object
            Return Me.mID
        End Function

        Public Overrides Function ToString() As String
            Return Me.mTaskNotes
        End Function

        Public Function ToListItem() As System.Web.UI.WebControls.ListItem
            Return New System.Web.UI.WebControls.ListItem(Me.ToString, Me.mID)
        End Function

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole(" ")
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function NewToDoInfo() As ClsToDoInfo
            Return New ClsToDoInfo
        End Function

        Public Shared Function GetToDoInfo(ByVal ID As Long) As ClsToDoInfo
            Return DataPortal.Fetch(Of ClsToDoInfo)(New IDCriteria(ID))
        End Function

        Public Shared Function GetToDoInfo(ByVal Struct As DAClsappAdvertiserContactToDo.Struct) As ClsToDoInfo
            Return DataPortal.Fetch(Of ClsToDoInfo)(Struct)
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        Private mStruct As DAClsappAdvertiserContactToDo.Struct = New DAClsappAdvertiserContactToDo.Struct

        Public Function GetTableStruct() As DAClsappAdvertiserContactToDo.Struct
            Return Me.mStruct
        End Function

        <Serializable()> Private Class IDCriteria

            Private mID As Long

            Public ReadOnly Property ID() As Long
                Get
                    Return Me.mID
                End Get
            End Property

            Public Sub New(ByVal ID As Long)
                Me.mID = ID
            End Sub

        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As IDCriteria)
            Dim List As DAClsappAdvertiserContactToDo.Struct() = DAClsappAdvertiserContactToDo.Fetch(criteria.ID)
            If List.Length > 0 Then
                Me.mStruct = List(0)
                Me.LoadObjectData()
            Else
                Throw New System.Security.SecurityException("To Do doesn't exists")
            End If
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal struct As DAClsappAdvertiserContactToDo.Struct)
            Me.mStruct = struct
            Me.LoadObjectData()
        End Sub

        ''' <summary>
        ''' Load the data of the object with the fetched data
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadObjectData()
            With Me.mStruct
                Me.mID = .ID.Value
                Me.mContact = ClsContactInfo.GetContactInfo(.IDAdvertiserContact.Value)
                Me.mDateEntered = .DateEntered.Value
                Me.mDateCompleted = .DateCompleted.Value
                Me.mTaskNotes = .TaskNotes.Value
            End With
        End Sub

#End Region

    End Class
End Namespace
