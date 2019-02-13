Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsGroupList
    Inherits BusinessListBase(Of ClsGroupList, ClsGroup)

#Region " Business Methods "

    Public Function GetItem(ByVal Id As Long) As ClsGroup
        For Each item As ClsGroup In Me
            If item.ID = Id Then
                Return item
            End If
        Next
        Return Nothing
    End Function

    Public Function GetItem(ByVal description As String) As ClsGroup
        For Each item As ClsGroup In Me
            If item.Description = description Then
                Return item
            End If
        Next
        Return Nothing
    End Function

    Public Function GetField(ByVal Id As Long) As ClsField
        For Each item As ClsGroup In Me
            If item.Fields.Contains(Id) Then
                Return item.Fields.GetItem(Id)
            End If
        Next
        Return Nothing
    End Function

    Public Function GetField(ByVal description As String) As ClsField
        For Each item As ClsGroup In Me
            If item.Fields.Contains(description) Then
                Return item.Fields.GetItem(description)
            End If
        Next
        Return Nothing
    End Function

    Public Overloads Sub Remove(ByVal Id As Long)
        For Each item As ClsGroup In Me
            If item.ID = Id Then
                Remove(item)
                Exit For
            End If
        Next
    End Sub

    Public Overloads Function Contains(ByVal Id As Long) As Boolean
        For Each item As ClsGroup In Me
            If item.ID = Id Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Overloads Function Contains(ByVal description As String) As Boolean
        For Each item As ClsGroup In Me
            If item.Description = description Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Overloads Function ContainsField(ByVal Id As Long) As Boolean
        For Each item As ClsGroup In Me
            If item.Fields.Contains(Id) Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Overloads Function ContainsField(ByVal description As String) As Boolean
        For Each item As ClsGroup In Me
            If item.Fields.Contains(description) Then
                Return True
            End If
        Next
        Return False
    End Function

    'Public Sub RemoveProfiles(ByVal formProfile As ClsFormProfile)
    '    For Each item As ClsGroup In Me
    '        item.Profiles.Remove(formProfile.ID)
    '    Next
    'End Sub

    'Public Sub AssignFormProfile(ByVal formProfile As ClsFormProfileInfo)
    '    For Each item As ClsGroup In Me
    '        item.Profiles.AssignFormProfile(formProfile)
    '    Next
    'End Sub

    Public Function CanExecuteInField(ByVal fieldDescription As String, ByVal pName As String) As Boolean
        For Each item As ClsGroup In Me
            If item.CanExecuteInField(fieldDescription, pName) Then
                Return True
            End If
        Next
        Return False
    End Function

#End Region

#Region " Authorization Rules "

    Public Shared Function CanAddObject() As Boolean
        Return True 'ApplicationContext.User.IsInRole("")
    End Function

    Public Shared Function CanGetObject() As Boolean
        Return True 'ApplicationContext.User.IsInRole("")
    End Function

    Public Shared Function CanEditObject() As Boolean
        Return True 'ApplicationContext.User.IsInRole("")
    End Function

    Public Shared Function CanDeleteObject() As Boolean
        Return True 'ApplicationContext.User.IsInRole("")
	End Function

#End Region

#Region " Factory Methods "

#Region " Root Methods "

	Public Shared Function NewGroupList() As ClsGroupList
		Return DataPortal.Create(Of ClsGroupList)(New Criteria())
    End Function

    Public Shared Function GetGroupList() As ClsGroupList
        Return DataPortal.Fetch(Of ClsGroupList)(New Criteria())
    End Function

    Public Shared Function GetGroupList(ByVal idForm As Long, ByVal description As String) As ClsGroupList
        Return DataPortal.Fetch(Of ClsGroupList)(New FilteredCriteria(idForm, description))
    End Function

    Public Shared Function GetGroupList(ByVal idForm() As Long, ByVal description As String) As ClsGroupList
        Return DataPortal.Fetch(Of ClsGroupList)(New FilteredCriteriaList(idForm, description))
    End Function

#End Region

#Region " Child Methods "

    Friend Shared Function NewChildGroupList() As ClsGroupList
        Return New ClsGroupList
    End Function

    Friend Shared Function GetChildGroupList(ByVal list As DAClsprgGroups.Struct()) As ClsGroupList
        Return New ClsGroupList(list)
    End Function

    Friend Shared Function NewFormGroupList() As ClsGroupList
        Return New ClsGroupList
    End Function

    Friend Shared Function GetFormGroupList(ByVal list As DAClsprgGroups.Struct()) As ClsGroupList
        Return New ClsGroupList(list)
    End Function

    Private Sub New()
        MarkAsChild()
    End Sub

    Private Sub New(ByVal list As DAClsprgGroups.Struct())
        MarkAsChild()
        Fetch(list)
    End Sub
	
#End Region

#End Region

#Region " Data Access "

#Region " Root Area "

	<Serializable()> Private Class Criteria
        ' no criteria - retrieve all projects
    End Class

	<Serializable()> Private Class FilteredCriteria

        Private mIDForm As Long
        Private mDescription As String

        Public ReadOnly Property IDForm() As Long
            Get
                Return Me.mIDForm
            End Get
        End Property

        Public ReadOnly Property Description() As String
            Get
                Return Me.mDescription
            End Get
        End Property

        Public Sub New(ByVal idForm As Long, ByVal description As String)
            Me.mIDForm = idForm
            Me.mDescription = description
        End Sub

    End Class

    <Serializable()> Private Class FilteredCriteriaList

        Private mIDForm() As Long
        Private mDescription As String

        Public ReadOnly Property IDForm() As Long()
            Get
                Return Me.mIDForm
            End Get
        End Property

        Public ReadOnly Property Description() As String
            Get
                Return Me.mDescription
            End Get
        End Property

        Public Sub New(ByVal idForm() As Long, ByVal description As String)
            Me.mIDForm = idForm
            Me.mDescription = description
        End Sub

    End Class

    <RunLocal()> Private Overloads Sub DataPortal_Create(ByVal criteria As Criteria)
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
        ' fetch with no filter
        Fetch()
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteria)
        Fetch(criteria.IDForm, criteria.Description)
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteriaList)
        Fetch(criteria.IDForm, criteria.Description)
    End Sub

    Protected Overrides Sub DataPortal_Update()
        RaiseListChangedEvents = False
		For Each item As ClsGroup In DeletedList
			item.DeleteSelf()
		Next
		DeletedList.Clear()
		For Each item As ClsGroup In Me
			If item.IsNew Then
                item.Insert(item.Form)
			Else
                item.Update(item.Form)
			End If
        Next
		RaiseListChangedEvents = True
    End Sub

#End Region

#Region " Child Area "
	
	Friend Sub Update(ByVal parent As Object)
        RaiseListChangedEvents = False
        For Each item As ClsGroup In DeletedList
            item.DeleteSelf()
        Next
        DeletedList.Clear()
        For Each item As ClsGroup In Me
            If item.IsNew Then
                item.Insert(parent)
            Else
                item.Update(parent)
            End If
        Next
        RaiseListChangedEvents = True
    End Sub
	
#End Region
	
#Region " Common Area "

    Private Sub Fetch()
        Fetch(DAClsprgGroups.Fetch())
    End Sub

    Private Sub Fetch(ByVal idForm As Long, ByVal description As String)
        Fetch(DAClsprgGroups.Fetch(idForm, description))
    End Sub

    Private Sub Fetch(ByVal idForm() As Long, ByVal description As String)
        Fetch(DAClsprgGroups.Fetch(idForm, description))
    End Sub

    Private Sub Fetch(ByVal list As DAClsprgGroups.Struct())
        Me.RaiseListChangedEvents = False
        For Each Struct As DAClsprgGroups.Struct In list
            Me.Add(ClsGroup.GetChildGroup(Struct))
        Next
        Me.RaiseListChangedEvents = True
    End Sub

#End Region

#End Region

End Class
