Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsFieldList
    Inherits BusinessListBase(Of ClsFieldList, ClsField)

#Region " Business Methods "

    Public Function GetItem(ByVal Id As Long) As ClsField
        For Each item As ClsField In Me
            If item.ID = Id Then
                Return item
            End If
        Next
        Return Nothing
    End Function

    Public Function GetItem(ByVal description As String) As ClsField
        For Each item As ClsField In Me
            If item.Description = description Then
                Return item
            End If
        Next
        Return Nothing
    End Function

    Public Overloads Sub Remove(ByVal Id As Long)
        For Each item As ClsField In Me
            If item.ID = Id Then
                Remove(item)
                Exit For
            End If
        Next
    End Sub

    Public Overloads Sub Remove(ByVal description As String)
        For Each item As ClsField In Me
            If item.Description = description Then
                Remove(item)
                Exit For
            End If
        Next
    End Sub

    Public Overloads Function Contains(ByVal Id As Long) As Boolean
        For Each item As ClsField In Me
            If item.ID = Id Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Overloads Function Contains(ByVal description As String) As Boolean
        For Each item As ClsField In Me
            If item.Description = description Then
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

	Public Shared Function NewFieldList() As ClsFieldList
		Return DataPortal.Create(Of ClsFieldList)(New Criteria())
    End Function

    Public Shared Function GetFieldList() As ClsFieldList
        Return DataPortal.Fetch(Of ClsFieldList)(New Criteria())
    End Function

    Public Shared Function GetFieldList(ByVal idForm As Long, ByVal idGroup As Long, ByVal description As String) As ClsFieldList
        Return DataPortal.Fetch(Of ClsFieldList)(New FilteredCriteria(idForm, idGroup, description))
    End Function

    Public Shared Function GetFieldList(ByVal idForm() As Long, ByVal idGroup() As Long, ByVal description As String) As ClsFieldList
        Return DataPortal.Fetch(Of ClsFieldList)(New FilteredCriteriaList(idForm, idGroup, description))
    End Function

#End Region

#Region " Child Methods "

    Friend Shared Function NewChildFieldList() As ClsFieldList
        Return New ClsFieldList
    End Function

    Friend Shared Function GetChildFieldList(ByVal list As DAClsprgFields.Struct()) As ClsFieldList
        Return New ClsFieldList(list)
    End Function

    Friend Shared Function NewGroupFieldList() As ClsFieldList
        Return New ClsFieldList
    End Function

    Friend Shared Function GetGroupFieldList(ByVal list As DAClsprgFields.Struct()) As ClsFieldList
        Return New ClsFieldList(list)
    End Function

    Private Sub New()
        MarkAsChild()
    End Sub

    Private Sub New(ByVal list As DAClsprgFields.Struct())
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
        Private mIDGroup As Long
        Private mDescription As String

        Public ReadOnly Property IDForm() As Long
            Get
                Return Me.mIDForm
            End Get
        End Property

        Public ReadOnly Property IDGroup() As Long
            Get
                Return Me.mIDGroup
            End Get
        End Property

        Public ReadOnly Property Description() As String
            Get
                Return Me.mDescription
            End Get
        End Property

        Public Sub New(ByVal idForm As Long, ByVal idGroup As Long, ByVal description As String)
            Me.mIDForm = idForm
            Me.mIDGroup = idGroup
            Me.mDescription = description
        End Sub

    End Class

    <Serializable()> Private Class FilteredCriteriaList

        Private mIDForm() As Long
        Private mIDGroup() As Long
        Private mDescription As String

        Public ReadOnly Property IDForm() As Long()
            Get
                Return Me.mIDForm
            End Get
        End Property

        Public ReadOnly Property IDGroup() As Long()
            Get
                Return Me.mIDGroup
            End Get
        End Property

        Public ReadOnly Property Description() As String
            Get
                Return Me.mDescription
            End Get
        End Property

        Public Sub New(ByVal idForm() As Long, ByVal idGroup() As Long, ByVal description As String)
            Me.mIDForm = idForm
            Me.mIDGroup = idGroup
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
        Fetch(criteria.IDForm, criteria.IDGroup, criteria.Description)
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteriaList)
        Fetch(criteria.IDForm, criteria.IDGroup, criteria.Description)
    End Sub

    Protected Overrides Sub DataPortal_Update()
        RaiseListChangedEvents = False
        For Each item As ClsField In DeletedList
            item.DeleteSelf()
        Next
        DeletedList.Clear()
        For Each item As ClsField In Me
            If item.IsNew Then
                item.Insert(item.Group)
            Else
                item.Update(item.Group)
            End If
        Next
        RaiseListChangedEvents = True
    End Sub

#End Region

#Region " Child Area "
	
	Friend Sub Update(ByVal parent As Object)
        RaiseListChangedEvents = False
        For Each item As ClsField In DeletedList
            item.DeleteSelf()
        Next
        DeletedList.Clear()
        For Each item As ClsField In Me
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
        Fetch(DAClsprgFields.Fetch())
    End Sub

    Private Sub Fetch(ByVal idForm As Long, ByVal idGroup As Long, ByVal description As String)
        Fetch(DAClsprgFields.Fetch(idGroup, idForm, description))
    End Sub

    Private Sub Fetch(ByVal idForm() As Long, ByVal idGroup() As Long, ByVal description As String)
        Fetch(DAClsprgFields.Fetch(idGroup, idForm, description))
    End Sub

    Private Sub Fetch(ByVal list As DAClsprgFields.Struct())
        Me.RaiseListChangedEvents = False
        For Each Struct As DAClsprgFields.Struct In list
            Me.Add(ClsField.GetChildField(Struct))
        Next
        Me.RaiseListChangedEvents = True
    End Sub

#End Region

#End Region

End Class
