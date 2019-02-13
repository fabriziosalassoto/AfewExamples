Imports Csla
Imports SCT.DataAccess

Namespace Subscriber
    <Serializable()> Public Class ClsBinnacleTableFieldInfo
        Inherits ReadOnlyBase(Of ClsBinnacleTableFieldInfo)

#Region " Business Methods "

        Private mID As Long
        Private mBinnacleTable As ClsBinnacleTableInfo
        Private mFieldName As String
        Private mOldValue As String
        Private mNewValue As String

        Public ReadOnly Property ID() As Long
            Get
                Return Me.mID
            End Get
        End Property

        Public ReadOnly Property BinnacleTable() As ClsBinnacleTableInfo
            Get
                Return Me.mBinnacleTable
            End Get
        End Property

        Public ReadOnly Property FieldName() As String
            Get
                Return Me.mFieldName
            End Get
        End Property

        Public ReadOnly Property OldValue() As String
            Get
                Return Me.mOldValue
            End Get
        End Property

        Public ReadOnly Property NewValue() As String
            Get
                Return Me.mNewValue
            End Get
        End Property

        Protected Overrides Function GetIdValue() As Object
            Return Me.mID
        End Function

        Public Overrides Function ToString() As String
            Return Me.mID
        End Function

        Public Function ToListItem() As System.Web.UI.WebControls.ListItem
            Return New System.Web.UI.WebControls.ListItem(Me.ToString, Me.mID)
        End Function

        Public Function ToArray() As Object()
            Dim Array() As Object = {Me.mID, Me.mBinnacleTable, Me.mFieldName, Me.mOldValue, Me.mNewValue}
            Return Array
        End Function

        Public Function GetBinnacleTableField() As ClsBinnacleTableField
            Return ClsBinnacleTableField.GetBinnacleTableField(Me.mID)
        End Function

#End Region

#Region " Authorization Rules "

        'Protected Overrides Sub AddAuthorizationRules()
        '    AuthorizationRules.AllowRead("ID", " ")
        '    AuthorizationRules.AllowRead("BinnacleTable", " ")
        '    AuthorizationRules.AllowRead("FieldName", " ")
        '    AuthorizationRules.AllowRead("OldValue", " ")
        '    AuthorizationRules.AllowRead("NewValue", " ")
        'End Sub

        Public Shared Function CanGetObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole(" ")
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function NewBinnacleTableFieldInfo() As ClsBinnacleTableFieldInfo
            Return New ClsBinnacleTableFieldInfo
        End Function

        Public Shared Function GetBinnacleTableFieldInfo(ByVal ID As Long) As ClsBinnacleTableFieldInfo
            Return DataPortal.Fetch(Of ClsBinnacleTableFieldInfo)(New IDCriteria(ID))
        End Function

        Public Shared Function GetBinnacleTableFieldInfo(ByVal Struct As DAClsprgSubscriberBinnacleTableFields.Struct) As ClsBinnacleTableFieldInfo
            Return DataPortal.Fetch(Of ClsBinnacleTableFieldInfo)(Struct)
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

#End Region

#Region " Data Access "

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
            Dim List As DAClsprgSubscriberBinnacleTableFields.Struct() = DAClsprgSubscriberBinnacleTableFields.Fetch(New Parameter(Of Long)(criteria.ID, 0))
            If List.Length > 0 Then
                Me.LoadObjectData(List(0))
            Else
                Throw New System.Security.SecurityException("BinnacleTableField record doesn't exists")
            End If
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal Struct As DAClsprgSubscriberBinnacleTableFields.Struct)
            Me.LoadObjectData(Struct)
        End Sub

        ''' <summary>
        ''' Load the data of the object with the fetched data
        ''' </summary>
        ''' <param name="Struct">Struct with the data</param>
        ''' <remarks></remarks>
        Private Sub LoadObjectData(ByVal Struct As DAClsprgSubscriberBinnacleTableFields.Struct)
            With Struct
                Me.mID = .ID.Value
                Me.mBinnacleTable = ClsBinnacleTableInfo.GetBinnacleTableInfo(.IDBinnacleTable.Value)
                Me.mFieldName = .FieldName.Value
                Me.mOldValue = .OldValue.Value
                Me.mNewValue = .NewValue.Value
            End With
        End Sub

#End Region

    End Class
End Namespace