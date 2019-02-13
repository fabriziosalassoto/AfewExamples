Imports Csla
Imports SCT.DataAccess

Namespace Administrator
    <Serializable()> Public Class ClsBinnacleFormFieldInfo
        Inherits ReadOnlyBase(Of ClsBinnacleFormFieldInfo)

#Region " Business Methods "

        Private mID As Long
        Private mBinnacleForm As ClsBinnacleFormInfo = ClsBinnacleFormInfo.NewBinnacleFormInfo
        Private mField As ClsFieldInfo = ClsFieldInfo.NewFieldInfo
        Private mOldValue As String = String.Empty
        Private mNewValue As String = String.Empty

        Public ReadOnly Property ID() As Long
            Get
                Return Me.mID
            End Get
        End Property

        Public ReadOnly Property BinnacleForm() As ClsBinnacleFormInfo
            Get
                Return Me.mBinnacleForm
            End Get
        End Property

        Public ReadOnly Property Field() As ClsFieldInfo
            Get
                Return Me.mField
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
            Dim Array() As Object = {Me.mID, Me.mBinnacleForm, Me.mField, Me.mOldValue, Me.mNewValue}
            Return Array
        End Function

        Public Function GetBinnacleFormField() As ClsBinnacleFormField
            Return ClsBinnacleFormField.GetBinnacleFormField(Me.mID)
        End Function

#End Region

#Region " Authorization Rules "

        'Protected Overrides Sub AddAuthorizationRules()
        '    AuthorizationRules.AllowRead("ID", " ")
        '    AuthorizationRules.AllowRead("BinnacleForm", " ")
        '    AuthorizationRules.AllowRead("Field", " ")
        '    AuthorizationRules.AllowRead("OldValue", " ")
        '    AuthorizationRules.AllowRead("NewValue", " ")
        'End Sub

        Public Shared Function CanGetObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole(" ")
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function NewBinnacleFormFieldInfo() As ClsBinnacleFormFieldInfo
            Return New ClsBinnacleFormFieldInfo
        End Function

        Public Shared Function GetBinnacleFormFieldInfo(ByVal ID As Long) As ClsBinnacleFormFieldInfo
            Return DataPortal.Fetch(Of ClsBinnacleFormFieldInfo)(New Criteria(ID))
        End Function

        Public Shared Function GetBinnacleFormFieldInfo(ByVal Struct As DAClsprgAdministrativeBinnacleFormFields.Struct) As ClsBinnacleFormFieldInfo
            Return DataPortal.Fetch(Of ClsBinnacleFormFieldInfo)(Struct)
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
            Dim List As DAClsprgAdministrativeBinnacleFormFields.Struct() = DAClsprgAdministrativeBinnacleFormFields.Fetch(New Parameter(Of Long)(criteria.ID, 0))
            If List.Length > 0 Then
                Me.LoadObjectData(List(0))
            Else
                Throw New System.Security.SecurityException("BinnacleFormField record doesn't exists")
            End If
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal Struct As DAClsprgAdministrativeBinnacleFormFields.Struct)
            Me.LoadObjectData(Struct)
        End Sub

        ''' <summary>
        ''' Load the data of the object with the fetched data
        ''' </summary>
        ''' <param name="Struct">Struct with the data</param>
        ''' <remarks></remarks>
        Private Sub LoadObjectData(ByVal Struct As DAClsprgAdministrativeBinnacleFormFields.Struct)
            With Struct
                Me.mID = .ID.Value
                Me.mBinnacleForm = ClsBinnacleFormInfo.GetBinnacleFormInfo(.IDBinnacleForm.Value)
                Me.mField = ClsFieldInfo.GetFieldInfo(.IDField.Value)
                Me.mOldValue = .OldValue.Value
                Me.mNewValue = .NewValue.Value
            End With
        End Sub

#End Region

    End Class
End Namespace
