Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsGroupInfo
    Inherits ReadOnlyBase(Of ClsGroupInfo)

#Region " Business Methods "

    Private mID As Long
    Private mForm As ClsFormInfo = ClsFormInfo.NewFormInfo
    Private mDescription As String = String.Empty

	Public ReadOnly Property ID() As Long
        Get
            Return Me.mID
        End Get
    End Property

    Public ReadOnly Property Form() As ClsFormInfo
        Get
            Return Me.mForm
        End Get
    End Property

    Public ReadOnly Property Description() As String
        Get
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

    Public Function ToArray() As Object()
        Dim Array() As Object = {Me.mID, Me.mDescription}
        Return Array
    End Function

    Public Function GetGroup() As ClsGroup
        Return ClsGroup.GetGroup(Me.mID)
    End Function

#End Region

#Region " Authorization Rules "

    'Protected Overrides Sub AddAuthorizationRules()
    '    AuthorizationRules.AllowRead("ID", " ")
    '    AuthorizationRules.AllowRead("IDForm", " ")
    '    AuthorizationRules.AllowRead("Description", " ")
    'End Sub

    Public Shared Function CanGetObject() As Boolean
        Return True 'ApplicationContext.User.IsInRole(" ")
    End Function

#End Region

#Region " Factory Methods "

    Public Shared Function NewGroupInfo() As ClsGroupInfo
        Return New ClsGroupInfo
    End Function

    Public Shared Function GetGroupInfo(ByVal ID As Long) As ClsGroupInfo
        Return DataPortal.Fetch(Of ClsGroupInfo)(New Criteria(ID))
    End Function

    Public Shared Function GetGroupInfo(ByVal Struct As DAClsprgGroups.Struct) As ClsGroupInfo
        Return DataPortal.Fetch(Of ClsGroupInfo)(Struct)
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

        Public Sub New(ByVal ID As Long)
            Me.mID = ID
        End Sub

    End Class

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
        Dim List As DAClsprgGroups.Struct() = DAClsprgGroups.Fetch(criteria.ID)
        If List.Length > 0 Then
            Me.LoadObjectData(List(0))
        Else
            Throw New System.Security.SecurityException("Group doesn't exist")
        End If
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal Struct As DAClsprgGroups.Struct)
        Me.LoadObjectData(Struct)
    End Sub

    ''' <summary>
    ''' Load the data of the object with the fetched data
    ''' </summary>
    ''' <param name="Struct">Struct with the data</param>
    ''' <remarks></remarks>
    Private Sub LoadObjectData(ByVal Struct As DAClsprgGroups.Struct)
        With Struct
            Me.mID = .ID.Value
            Me.mForm = ClsFormInfo.GetFormInfo(.IDForm.Value)
            Me.mDescription = .Description.Value
        End With
    End Sub

#End Region

End Class