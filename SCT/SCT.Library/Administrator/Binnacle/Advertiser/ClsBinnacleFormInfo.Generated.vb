Imports Csla
Imports SCT.DataAccess

Namespace Advertiser
    <Serializable()> Public Class ClsBinnacleFormInfo
        Inherits ReadOnlyBase(Of ClsBinnacleFormInfo)

#Region " Business Methods "

        Private mID As Long
        Private mBinnacle As ClsBinnacleInfo = ClsBinnacleInfo.NewBinnacleInfo
        Private mForm As ClsFormInfo = ClsFormInfo.NewFormInfo
        Private mOperation As ClsOperationInfo = ClsOperationInfo.NewOperationInfo
        Private mBHour As Date

        Public ReadOnly Property ID() As Long
            Get
                Return Me.mID
            End Get
        End Property

        Public ReadOnly Property Binnacle() As ClsBinnacleInfo
            Get
                Return Me.mBinnacle
            End Get
        End Property

        Public ReadOnly Property Form() As ClsFormInfo
            Get
                Return Me.mForm
            End Get
        End Property

        Public ReadOnly Property Operation() As ClsOperationInfo
            Get
                Return Me.mOperation
            End Get
        End Property

        Public ReadOnly Property BHour() As Date
            Get
                Return Me.mBHour
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
            Dim Array() As Object = {Me.mID, Me.mBinnacle, Me.mForm, Me.mOperation, Me.mBHour}
            Return Array
        End Function

        Public Function GetBinnacleForm() As ClsBinnacleForm
            Return ClsBinnacleForm.GetBinnacleForm(Me.mID)
        End Function

#End Region

#Region " Authorization Rules "

        'Protected Overrides Sub AddAuthorizationRules()
        '    AuthorizationRules.AllowRead("ID", " ")
        '    AuthorizationRules.AllowRead("Binnacle", " ")
        '    AuthorizationRules.AllowRead("Form", " ")
        '    AuthorizationRules.AllowRead("Operation", " ")
        '    AuthorizationRules.AllowRead("BHour", " ")
        'End Sub

        Public Shared Function CanGetObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole(" ")
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function NewBinnacleFormInfo() As ClsBinnacleFormInfo
            Return New ClsBinnacleFormInfo
        End Function

        Public Shared Function GetBinnacleFormInfo(ByVal ID As Long) As ClsBinnacleFormInfo
            Return DataPortal.Fetch(Of ClsBinnacleFormInfo)(New IDCriteria(ID))
        End Function

        Public Shared Function GetBinnacleFormInfo(ByVal Struct As DAClsprgAdvertiserBinnacleForms.Struct) As ClsBinnacleFormInfo
            Return DataPortal.Fetch(Of ClsBinnacleFormInfo)(Struct)
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
            Dim List As DAClsprgAdvertiserBinnacleForms.Struct() = DAClsprgAdvertiserBinnacleForms.Fetch(New Parameter(Of Long)(criteria.ID, 0))
            If List.Length > 0 Then
                Me.LoadObjectData(List(0))
            Else
                Throw New System.Security.SecurityException("BinnacleForm record doesn't exists")
            End If
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal Struct As DAClsprgAdvertiserBinnacleForms.Struct)
            Me.LoadObjectData(Struct)
        End Sub

        ''' <summary>
        ''' Load the data of the object with the fetched data
        ''' </summary>
        ''' <param name="Struct">Struct with the data</param>
        ''' <remarks></remarks>
        Private Sub LoadObjectData(ByVal Struct As DAClsprgAdvertiserBinnacleForms.Struct)
            With Struct
                Me.mID = .ID.Value
                Me.mBinnacle = ClsBinnacleInfo.GetBinnacleInfo(.IDBinnacle.Value)
                Me.mForm = ClsFormInfo.GetFormInfo(.IDForm.Value)
                Me.mOperation = ClsOperationInfo.GetOperationInfo(.IDOperation.Value)
                Me.mBHour = .BHour.Value
            End With
        End Sub

#End Region

    End Class
End NameSpace
