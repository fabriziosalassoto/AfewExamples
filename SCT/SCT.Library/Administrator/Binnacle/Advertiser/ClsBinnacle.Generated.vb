Imports Csla
Imports SCT.DataAccess

Namespace Advertiser
    <Serializable()> Public Class ClsBinnacle
        Inherits BusinessBase(Of ClsBinnacle)

#Region " Business Methods "

        Private mID As Long
        Private mUser As ClsAccountInfo = ClsAccountInfo.NewAccountInfo
        Private mBDate As Date = New Date(1900, 1, 1)
        Private mBinnacleForms As ClsBinnacleFormList = ClsBinnacleFormList.NewChildBinnacleFormList
        Private mBinnacleTables As ClsBinnacleTableList = ClsBinnacleTableList.NewChildBinnacleTableList

        Public ReadOnly Property ID() As Long
            Get
                CanReadProperty(True)
                Return Me.mID
            End Get
        End Property

        Public Property User() As ClsAccountInfo
            Get
                CanReadProperty(True)
                Return Me.mUser
            End Get
            Set(ByVal value As ClsAccountInfo)
                CanWriteProperty(True)
                If value IsNot Nothing AndAlso value.ID <> 0 Then
                    If Me.mUser.ID <> value.ID Then
                        Me.mUser = value
                        ValidationRules.CheckRules("User")
                        PropertyHasChanged()
                    End If
                Else
                    Throw New System.Security.SecurityException("User required.")
                End If
            End Set
        End Property

        Public Property BDate() As Date
            Get
                CanReadProperty(True)
                Return Me.mBDate
            End Get
            Set(ByVal value As Date)
                CanWriteProperty(True)
                If Me.mBDate <> value Then
                    Me.mBDate = value
                    ValidationRules.CheckRules("BDate")
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public ReadOnly Property BinnacleForms() As ClsBinnacleFormList
            Get
                CanReadProperty(True)
                Return Me.mBinnacleForms
            End Get
        End Property

        Public ReadOnly Property BinnacleTables() As ClsBinnacleTableList
            Get
                CanReadProperty(True)
                Return Me.mBinnacleTables
            End Get
        End Property

        Public Overrides ReadOnly Property IsValid() As Boolean
            Get
                Return MyBase.IsValid AndAlso Me.mBinnacleForms.IsValid AndAlso Me.mBinnacleTables.IsValid
            End Get
        End Property

        Public Overrides ReadOnly Property IsDirty() As Boolean
            Get
                Return MyBase.IsDirty OrElse Me.mBinnacleForms.IsDirty OrElse Me.mBinnacleTables.IsDirty
            End Get
        End Property

        Public Sub AssignUser(ByVal UserId As Long)
            If UserId <> 0 Then
                If Me.mUser.ID <> UserId Then
                    Me.mUser = ClsAccountInfo.GetAccountInfo(UserId)
                    PropertyHasChanged("User")
                End If
            Else
                Throw New System.Security.SecurityException("User required.")
            End If
        End Sub

        Protected Overrides Function GetIdValue() As Object
            Return Me.mID
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()
            ValidationRules.AddRule(AddressOf UserRequired, "User")
            ValidationRules.AddRule(AddressOf IsValidMinDate, "BDate")
        End Sub

        Private Function UserRequired(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
            If Me.mUser.ID = 0 Then
                e.Description = "User required."
                Return False
            Else
                Return True
            End If
        End Function

        Private Function IsValidMinDate(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean
            If Me.mBDate.Date < New Date(1900, 1, 1).Date Then
                e.Description = "Invalid Date"
                Return False
            Else
                Return True
            End If
        End Function

#End Region

#Region " Authorization Rules "

        'Protected Overrides Sub AddAuthorizationRules()
        '    AuthorizationRules.AllowRead("ID", " ")
        '    AuthorizationRules.AllowRead("User", " ")
        '    AuthorizationRules.AllowRead("BDate", " ")
        '    AuthorizationRules.AllowRead("BinnacleForms", "")
        '    AuthorizationRules.AllowRead("BinnacleTables", "")
        '    AuthorizationRules.AllowWrite("User", New String() {" ", " "})
        '    AuthorizationRules.AllowWrite("BDate", New String() {" ", " "})
        '    AuthorizationRules.AllowWrite("BinnacleForms", New String() {" ", " "})
        '    AuthorizationRules.AllowWrite("BinnacleTables", New String() {" ", " "})
        'End Sub

        Public Shared Function CanAddObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole(" ")
        End Function

        Public Shared Function CanGetObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole(" ")
        End Function

        Public Shared Function CanDeleteObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole(" ")
        End Function

        Public Shared Function CanEditObject() As Boolean
            Return True 'ApplicationContext.User.IsInRole(" ")
        End Function

#End Region

#Region " Factory Methods "

#Region " Root Methods "

        Public Shared Function NewBinnacle() As ClsBinnacle
            If Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add Binnacle records.")
            End If
            Return DataPortal.Create(Of ClsBinnacle)(New Criteria(0))
        End Function

        Public Shared Function NewBinnacle(ByVal IDUser As Long, ByVal BDate As Date) As ClsBinnacle
            If Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add Binnacle records.")
            End If
            Return DataPortal.Create(Of ClsBinnacle)(New UserCriteria(IDUser, BDate))
        End Function

        Public Shared Function GetBinnacle(ByVal ID As Long) As ClsBinnacle
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view Binnacle records.")
            End If
            Return DataPortal.Fetch(Of ClsBinnacle)(New Criteria(ID))
        End Function

        Public Shared Function GetBinnacle(ByVal IDUser As Long, ByVal BDate As Date) As ClsBinnacle
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view Binnacle records.")
            End If
            Return DataPortal.Fetch(Of ClsBinnacle)(New UserCriteria(IDUser, BDate))
        End Function

        Public Shared Sub DeleteBinnacle(ByVal ID As Long)
            If Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove Binnacle records.")
            End If
            DataPortal.Delete(New Criteria(ID))
        End Sub

        Public Overrides Function Save() As ClsBinnacle
            If IsDeleted AndAlso Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove Binnacle records.")

            ElseIf IsNew AndAlso Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add Binnacle records.")

            ElseIf Not CanEditObject() Then
                Throw New System.Security.SecurityException("User not authorized to update Binnacle records.")
            End If
            Return MyBase.Save
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

#End Region

#Region " Child Methods "

        Friend Shared Function NewChildBinnacle() As ClsBinnacle
            Dim Child As New ClsBinnacle
            Child.ValidationRules.CheckRules()
            Child.MarkAsChild()
            Return Child
        End Function

        Friend Shared Function GetChildBinnacle(ByVal Binnacle As DAClsprgAdvertiserBinnacle.Struct) As ClsBinnacle
            Return New ClsBinnacle(Binnacle)
        End Function

        Private Sub New(ByVal Binnacle As DAClsprgAdvertiserBinnacle.Struct)
            MarkAsChild()
            Fetch(Binnacle)
        End Sub

#End Region

#End Region

#Region " Data Access "

#Region " Root Area "

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

        <Serializable()> Private Class UserCriteria

            Private mIDUser As Long
            Private mBDate As Date

            Public ReadOnly Property IDUser() As Long
                Get
                    Return Me.mIDUser
                End Get
            End Property

            Public ReadOnly Property BDate() As Date
                Get
                    Return Me.mBDate
                End Get
            End Property

            Public Sub New(ByVal idUser As Long, ByVal bDate As Date)
                Me.mIDUser = idUser
                Me.mBDate = bDate
            End Sub

        End Class

        <RunLocal()> Private Overloads Sub DataPortal_Create(ByVal criteria As Criteria)
            ValidationRules.CheckRules()
        End Sub

        <RunLocal()> Private Overloads Sub DataPortal_Create(ByVal criteria As UserCriteria)
            Me.AssignUser(criteria.IDUser)
            Me.mBDate = criteria.BDate.Date
            ValidationRules.CheckRules()
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            Dim List As DAClsprgAdvertiserBinnacle.Struct() = DAClsprgAdvertiserBinnacle.Fetch(New Parameter(Of Long)(criteria.ID, 0))
            If List.Length > 0 Then
                Me.mStruct = List(0)
                Me.LoadObjectChildData()
            Else
                Throw New System.Security.SecurityException("Binnacle record doesn't exists")
            End If
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As UserCriteria)
            Dim List As DAClsprgAdvertiserBinnacle.Struct() = DAClsprgAdvertiserBinnacle.Fetch(New Parameter(Of Long)(criteria.IDUser, 0), New Parameter(Of Date)(criteria.BDate, 0))
            If List.Length > 0 Then
                Me.mStruct = List(0)
                Me.LoadObjectData()
            Else
                Throw New System.Security.SecurityException("Binnacle record doesn't exists")
            End If
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Insert()
            Me.LoadTableStruct(New Object() {Me.mUser})
            Me.mStruct = DAClsprgAdvertiserBinnacle.Insert(Me.mStruct)
            Me.mID = Me.mStruct.ID.Value
            Me.mBinnacleForms.Update(Me)
            Me.mBinnacleTables.Update(Me)
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_Update()
            If MyBase.IsDirty Then
                Me.LoadTableStruct(New Object() {Me.mUser})
                Me.mStruct = DAClsprgAdvertiserBinnacle.Update(Me.mStruct)
            End If
            Me.mBinnacleForms.Update(Me)
            Me.mBinnacleTables.Update(Me)
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Protected Overrides Sub DataPortal_DeleteSelf()
            DataPortal_Delete(New Criteria(Me.mID))
        End Sub

        <Transactional(TransactionalTypes.TransactionScope)> Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)
            Me.mBinnacleForms.Clear()
            Me.mBinnacleTables.Clear()
            Me.mBinnacleForms.Update(Me)
            Me.mBinnacleTables.Update(Me)
            DAClsprgAdvertiserBinnacle.Delete(criteria.ID)
        End Sub

#End Region

#Region " Child Area "

        Private Sub Fetch(ByVal Binnacle As DAClsprgAdvertiserBinnacle.Struct)
            Me.mStruct = Binnacle
            Me.LoadObjectChildData()
            MarkOld()
        End Sub

        Friend Sub Insert(ByVal parent As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct(parent)
            Me.mStruct = DAClsprgAdvertiserBinnacle.Insert(Me.mStruct)
            Me.mID = Me.mStruct.ID.Value
            Me.mBinnacleForms.Update(Me)
            Me.mBinnacleTables.Update(Me)
            MarkOld()
        End Sub

        Friend Sub Update(ByVal parent As Object)
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Me.LoadTableStruct(parent)
            Me.mStruct = DAClsprgAdvertiserBinnacle.Update(Me.mStruct)
            Me.mBinnacleForms.Update(Me)
            Me.mBinnacleTables.Update(Me)
            MarkOld()
        End Sub

        Friend Sub DeleteSelf()
            ' if we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            ' if we're new then don't update the database
            If Me.IsNew Then Exit Sub

            Me.mBinnacleForms.Clear()
            Me.mBinnacleTables.Clear()
            Me.mBinnacleForms.Update(Me)
            Me.mBinnacleTables.Update(Me)
            DAClsprgAdvertiserBinnacle.Delete(Me.mID)
            MarkNew()
        End Sub

#End Region

#Region " Common Area "

        Private mStruct As DAClsprgAdvertiserBinnacle.Struct = New DAClsprgAdvertiserBinnacle.Struct

        Public Function GetTableStruct() As DAClsprgAdvertiserBinnacle.Struct
            Return Me.mStruct
        End Function

        ''' <summary>
        ''' Load the data of the object with the fetched data
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadObjectChildData()
            Me.LoadObjectData()
            Me.mBinnacleForms = ClsBinnacleFormList.GetChildBinnacleFormList(DAClsprgAdvertiserBinnacleForms.FetchByBinnacle(New Parameter(Of Long)(Me.mStruct.ID.Value, 0)))
            Me.mBinnacleTables = ClsBinnacleTableList.GetChildBinnacleTableList(DAClsprgAdvertiserBinnacleTables.FetchByBinnacle(New Parameter(Of Long)(Me.mStruct.ID.Value, 0)))
        End Sub

        ''' <summary>
        ''' Load the data of the object with the fetched data
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadObjectData()
            With Me.mStruct
                Me.mID = .ID.Value
                Me.mBDate = .BDate.Value
                Me.mUser = ClsAccountInfo.GetAccountInfo(.IDUser.Value)
            End With
        End Sub

        ''' <summary>
        ''' Collect the data of the object to fill to a structure of data and returns the structure 
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadTableStruct(ByVal parents() As Object)
            With Me.mStruct
                .ID.NewValue = Me.mID
                .IDUser.NewValue = parents(0).ID
                .BDate.NewValue = Me.mBDate
            End With
        End Sub

#End Region

#End Region

#Region " Exists "

        Public Shared Function Exists(ByVal id As Long) As Boolean
            Return DataPortal.Execute(Of ExistsCommand)(New ExistsCommand(id)).Exists
        End Function

        <Serializable()> Private Class ExistsCommand
            Inherits CommandBase

            Private mID As Long
            Private mExists As Boolean

            Public ReadOnly Property Exists() As Boolean
                Get
                    Return Me.mExists
                End Get
            End Property

            Public Sub New(ByVal id As String)
                Me.mID = id
            End Sub

            Protected Overrides Sub DataPortal_Execute()
                Me.mExists = (DAClsprgAdvertiserBinnacle.Fetch(New Parameter(Of Long)(Me.mID, 0)).Length > 0)
            End Sub

        End Class

        Public Shared Function ExistsUserBinnacle(ByVal userID As Long, ByVal bDate As Date) As Boolean
            Return DataPortal.Execute(Of UserBinnacleExistsCommand)(New UserBinnacleExistsCommand(userID, bDate)).Exists
        End Function

        <Serializable()> Private Class UserBinnacleExistsCommand
            Inherits CommandBase

            Private mIDUser As Long
            Private mBDate As Date
            Private mExists As Boolean

            Public ReadOnly Property Exists() As Boolean
                Get
                    Return Me.mExists
                End Get
            End Property

            Public Sub New(ByVal idUser As String, ByVal bDate As Date)
                Me.mIDUser = idUser
                Me.mBDate = bDate
            End Sub

            Protected Overrides Sub DataPortal_Execute()
                Me.mExists = (DAClsprgAdvertiserBinnacle.Fetch(New Parameter(Of Long)(Me.mIDUser, 0), New Parameter(Of Date)(Me.mBDate.Date, 0)).Length > 0)
            End Sub

        End Class

#End Region

    End Class
End Namespace
