Imports Csla
Imports SCT.DataAccess

Namespace Advertiser
    <Serializable()> Public Class ClsNoteInfo
        Inherits ReadOnlyBase(Of ClsNoteInfo)

#Region " Business Methods "

        Private mID As Long
        Private mContact As ClsContactInfo = ClsContactInfo.NewContactInfo
        Private mDateEntered As Date = New Date(1900, 1, 1)
        Private mDescription As String

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

        Public ReadOnly Property Description() As String
            Get
                CanReadProperty(True)
                Return Me.mDescription
            End Get
        End Property

        Protected Overrides Function GetIdValue() As Object
            Return Me.mID
        End Function

        Public Overrides Function ToString() As String
            Return Me.mDescription
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

        Public Shared Function NewNoteInfo() As ClsNoteInfo
            Return New ClsNoteInfo
        End Function

        Public Shared Function GetNoteInfo(ByVal ID As Long) As ClsNoteInfo
            Return DataPortal.Fetch(Of ClsNoteInfo)(New IDCriteria(ID))
        End Function

        Public Shared Function GetNoteInfo(ByVal Struct As DAClsappAdvertiserContactNotes.Struct) As ClsNoteInfo
            Return DataPortal.Fetch(Of ClsNoteInfo)(Struct)
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        Private mStruct As DAClsappAdvertiserContactNotes.Struct = New DAClsappAdvertiserContactNotes.Struct

        Public Function GetTableStruct() As DAClsappAdvertiserContactNotes.Struct
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
            Dim List As DAClsappAdvertiserContactNotes.Struct() = DAClsappAdvertiserContactNotes.Fetch(criteria.ID)
            If List.Length > 0 Then
                Me.mStruct = List(0)
                Me.LoadObjectData()
            Else
                Throw New System.Security.SecurityException("Note doesn't exists")
            End If
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal struct As DAClsappAdvertiserContactNotes.Struct)
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
                Me.mDescription = .DescriptionOfNotes.Value
            End With
        End Sub

#End Region

    End Class
End Namespace
