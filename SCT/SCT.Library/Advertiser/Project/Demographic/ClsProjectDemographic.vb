Imports Csla
Imports SCT.DataAccess

Namespace Advertiser
    <Serializable()> Public Class ClsProjectDemographic
        Inherits BusinessBase(Of ClsProjectDemographic)

#Region " Business Methods "

        Private mID As Long
        Private mProject As ClsProjectInfo = ClsProjectInfo.NewProjectInfo
        Private mTag As String = ""
        Private mRequirement As String = ""

        Public ReadOnly Property ID() As Long
            Get
                CanReadProperty(True)
                Return Me.mID
            End Get
        End Property

        Public ReadOnly Property Project() As ClsProjectInfo
            Get
                CanReadProperty(True)
                Return Me.mProject
            End Get
        End Property

        Public Property Tag() As String
            Get
                CanReadProperty(True)
                Return Me.mTag
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mTag <> value Then
                    Me.mTag = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property Requirement() As String
            Get
                CanReadProperty(True)
                Return Me.mRequirement
            End Get
            Set(ByVal value As String)
                CanWriteProperty(True)
                If Me.mRequirement <> value Then
                    Me.mRequirement = value
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

        Public Sub AssignProject(ByVal projectId As Long)
            Me.mProject = ClsProjectInfo.GetProjectInfo(projectId)
        End Sub

        Protected Overrides Function GetIdValue() As Object
            Return Me.mID
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("Tag", 100))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, New Validation.CommonRules.MaxLengthRuleArgs("Requirement", 100))
        End Sub

#End Region

#Region " Authorization Rules "

        'Protected Overrides Sub AddAuthorizationRules()
        '    AuthorizationRules.AllowRead("ID", "")
        '    AuthorizationRules.AllowRead("Project", "")
        '    AuthorizationRules.AllowRead("Tag", "")
        '    AuthorizationRules.AllowRead("Requirement", "")
        '    AuthorizationRules.AllowWrite("Tag", "")
        '    AuthorizationRules.AllowWrite("Requirement", "")
        'End Sub

#End Region

#Region " Factory Methods "

        Friend Shared Function NewProjectDemographic() As ClsProjectDemographic
            Return New ClsProjectDemographic()
        End Function

        Friend Shared Function GetProjectDemographic(ByVal demographic As DAClsappAdvertiserDemographics.Struct) As ClsProjectDemographic
            Return New ClsProjectDemographic(demographic)
        End Function

        Private Sub New()
            MarkAsChild()
        End Sub

        Private Sub New(ByVal demographic As DAClsappAdvertiserDemographics.Struct)
            MarkAsChild()
            Fetch(demographic)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal demographic As DAClsappAdvertiserDemographics.Struct)
            With demographic
                Me.mID = .ID
                Me.mProject = ClsProjectInfo.GetProjectInfo(.IDAdvertiserProject)
                Me.mTag = .DemographicTag
                Me.mRequirement = .DemographicRequirement
            End With
            MarkOld()
        End Sub

        Friend Sub Insert(ByVal project As Object)

            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.mID = DAClsappAdvertiserDemographics.Insert(Me.GetAdvertiserDemographicStruct())
            MarkOld()

        End Sub

        Friend Sub Update(ByVal project As Object)

            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            DAClsappAdvertiserDemographics.Update(Me.GetAdvertiserDemographicStruct())
            MarkOld()

        End Sub

        ''' <summary>
        ''' Collect the data of the object to fill to a structure of data and returns the structure 
        ''' </summary>
        ''' <returns>Structure with data</returns>
        ''' <remarks></remarks>
        Private Function GetAdvertiserDemographicStruct() As DAClsappAdvertiserDemographics.Struct
            Dim Struct As New DAClsappAdvertiserDemographics.Struct
            With Struct
                .ID = Me.mID
                .IDAdvertiserProject = Me.Project.ID
                .DemographicTag = Me.mTag
                .DemographicRequirement = Me.mRequirement
            End With
            Return Struct
        End Function

        Friend Sub DeleteSelf()

            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            ' if we're new then don't update the database
            If Me.IsNew Then Exit Sub

            DAClsappAdvertiserDemographics.Delete(Me.mID)
            MarkNew()

        End Sub

#End Region

    End Class
End Namespace