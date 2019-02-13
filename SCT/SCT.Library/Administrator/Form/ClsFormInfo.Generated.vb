Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsFormInfo
    Inherits ReadOnlyBase(Of ClsFormInfo)

#Region " Business Methods "

    Private mID As Long
    Private mDescription As String = String.Empty
    Private mLogDescription As String = String.Empty

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

    Public ReadOnly Property LogDescription() As String
        Get
            Return Me.mLogDescription
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

#End Region

#Region " Authorization Rules "

    'Protected Overrides Sub AddAuthorizationRules()
    '    AuthorizationRules.AllowRead("ID", " ")
    '    AuthorizationRules.AllowRead("Description", " ")
    'End Sub

    Public Shared Function CanGetObject() As Boolean
        Return True 'ApplicationContext.User.IsInRole(" ")
    End Function

#End Region

#Region " Factory Methods "

    Public Shared Function NewFormInfo() As ClsFormInfo
        Return New ClsFormInfo
    End Function

    Public Shared Function GetFormInfo(ByVal ID As Long) As ClsFormInfo
        Return DataPortal.Fetch(Of ClsFormInfo)(New Criteria(ID))
    End Function

    Public Shared Function GetFormInfo(ByVal Struct As DAClsprgForms.Struct) As ClsFormInfo
        Return DataPortal.Fetch(Of ClsFormInfo)(Struct)
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
                Return mID
            End Get
        End Property

        Public Sub New(ByVal ID As Long)
            mID = ID
        End Sub

    End Class

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
        Dim List As DAClsprgForms.Struct() = DAClsprgForms.Fetch(criteria.ID)
        If List.Length > 0 Then
            Me.LoadObjectData(List(0))
        Else
            Throw New System.Security.SecurityException("Form doesn't exist")
        End If
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal Struct As DAClsprgForms.Struct)
        Me.LoadObjectData(Struct)
    End Sub

    ''' <summary>
    ''' Load the data of the object with the fetched data
    ''' </summary>
    ''' <param name="Struct">Struct with the data</param>
    ''' <remarks></remarks>
    Private Sub LoadObjectData(ByVal Struct As DAClsprgForms.Struct)
        With Struct
            Me.mID = .ID.Value
            Me.mDescription = .Description.Value
            Me.mLogDescription = .LogDescription.Value
        End With
    End Sub

#End Region

End Class
