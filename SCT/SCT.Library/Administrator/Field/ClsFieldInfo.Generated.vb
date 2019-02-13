Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsFieldInfo
    Inherits ReadOnlyBase(Of ClsFieldInfo)

#Region " Business Methods "

    Private mID As Long
    Private mGroup As ClsGroupInfo = ClsGroupInfo.NewGroupInfo
    Private mDescription As String = String.Empty

	Public ReadOnly Property ID() As Long
        Get
            Return Me.mID
        End Get
    End Property

    Public ReadOnly Property Description() As String
        Get
            Return Me.mDescription
        End Get
    End Property

    Public ReadOnly Property Group() As ClsGroupInfo
        Get
            Return Me.mGroup
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

    Public Function ToArray() As Object()
        Dim Array() As Object = {Me.mID, Me.mDescription}
        Return Array
    End Function

    Public Function GetField() As ClsField
        Return ClsField.GetField(Me.mID)
    End Function

#End Region

#Region " Authorization Rules "

    'Protected Overrides Sub AddAuthorizationRules()
    '    AuthorizationRules.AllowRead("ID", " ")
    '    AuthorizationRules.AllowRead("IDGroup", " ")
    '    AuthorizationRules.AllowRead("Description", " ")
    'End Sub

    Public Shared Function CanGetObject() As Boolean
        Return True 'ApplicationContext.User.IsInRole(" ")
    End Function

#End Region

#Region " Factory Methods "

    Public Shared Function NewFieldInfo() As ClsFieldInfo
        Return New ClsFieldInfo
    End Function

    Public Shared Function GetFieldInfo(ByVal ID As Long) As ClsFieldInfo ', ByVal IDGroup As Long, ByVal IDForm As Long) As ClsFieldInfo
        If Not CanGetObject() Then
            Throw New System.Security.SecurityException("User not authorized to view Field info records.")
        End If
        Return DataPortal.Fetch(Of ClsFieldInfo)(New Criteria(ID)) ', IDGroup, IDForm))
    End Function

    Public Shared Function GetFieldInfo(ByVal Struct As DAClsprgFields.Struct) As ClsFieldInfo
        If Not CanGetObject() Then
            Throw New System.Security.SecurityException("User not authorized to view Field info records.")
        End If
        Return DataPortal.Fetch(Of ClsFieldInfo)(Struct)
    End Function

    Private Sub New()
        ' Require use of factory methods
    End Sub

#End Region

#Region " Data Access "

	<Serializable()> Private Class Criteria
		
        Private mID As Long

        Public ReadOnly Property ID() As Long
            Get
                Return Me.mID
            End Get
        End Property

        Public Sub New(ByVal id As Long)
            Me.mID = id
        End Sub
		
	End Class

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
        Dim List As DAClsprgFields.Struct() = DAClsprgFields.Fetch(criteria.ID)
        If List.Length > 0 Then
            Me.LoadObjectData(List(0))
        Else
            Throw New System.Security.SecurityException("Field doesn't exist")
        End If
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal Struct As DAClsprgFields.Struct)
        Me.LoadObjectData(Struct)
    End Sub

    ''' <summary>
    ''' Load the data of the object with the fetched data
    ''' </summary>
    ''' <param name="Struct">Struct with the data</param>
    ''' <remarks></remarks>
    Private Sub LoadObjectData(ByVal Struct As DAClsprgFields.Struct)
        With Struct
            Me.mID = .ID.Value
            Me.mGroup = ClsGroupInfo.GetGroupInfo(.IDGroup.Value)
            Me.mDescription = .Description.Value
      End With
    End Sub

#End Region

End Class
