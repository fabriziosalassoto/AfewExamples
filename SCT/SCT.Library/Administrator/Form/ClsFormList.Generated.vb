Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsFormList
    Inherits BusinessListBase(Of ClsFormList, ClsForm)

#Region " Business Methods "

    Public Function GetItem(ByVal Id As Long) As ClsForm
        For Each item As ClsForm In Me
            If item.ID = Id Then
                Return item
            End If
        Next
        Return Nothing
    End Function

    Public Function GetItem(ByVal description As String) As ClsForm
        For Each item As ClsForm In Me
            If item.Description = description Then
                Return item
            End If
        Next
        Return Nothing
    End Function

    Public Overloads Sub Remove(ByVal Id As Long)
        For Each item As ClsForm In Me
            If item.ID = Id Then
                Remove(item)
                Exit For
            End If
        Next
    End Sub

    Public Overloads Function Contains(ByVal Id As Long) As Boolean
        For Each item As ClsForm In Me
            If item.ID = Id Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Overloads Function Contains(ByVal description As String) As Boolean
        For Each item As ClsForm In Me
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

	Public Shared Function NewFormList() As ClsFormList
		Return DataPortal.Create(Of ClsFormList)(New Criteria())
    End Function

    Public Shared Function GetFormList() As ClsFormList
        Return DataPortal.Fetch(Of ClsFormList)(New Criteria())
    End Function

    Public Shared Function GetFormList(ByVal description As String, ByVal logDescription As String) As ClsFormList
        Return DataPortal.Fetch(Of ClsFormList)(New FilteredCriteria(description, logDescription))
    End Function

    Public Shared Function GetFormList(ByVal description As String, ByVal logDescription() As String) As ClsFormList
        Return DataPortal.Fetch(Of ClsFormList)(New FilteredCriteriaList(description, logDescription))
    End Function

#End Region

#Region " Child Methods "

    Friend Shared Function NewChildFormList() As ClsFormList
        Return New ClsFormList
    End Function

    Friend Shared Function GetChildFormList(ByVal list As DAClsprgForms.Struct()) As ClsFormList
        Return New ClsFormList(list)
    End Function
	
    Private Sub New()
        MarkAsChild()
    End Sub

    Private Sub New(ByVal list As DAClsprgForms.Struct())
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

        Private mDescription As String
        Private mLogDescription As String

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

        Public Sub New(ByVal description As String, ByVal logDescription As String)
            Me.mDescription = description
            Me.mLogDescription = logDescription
        End Sub

    End Class

    <Serializable()> Private Class FilteredCriteriaList

        Private mDescription As String
        Private mLogDescription() As String

        Public ReadOnly Property Description() As String
            Get
                Return Me.mDescription
            End Get
        End Property

        Public ReadOnly Property LogDescription() As String()
            Get
                Return Me.mLogDescription
            End Get
        End Property

        Public Sub New(ByVal description As String, ByVal logDescription() As String)
            Me.mDescription = description
            Me.mLogDescription = logDescription
        End Sub

    End Class

    <RunLocal()> Private Overloads Sub DataPortal_Create(ByVal criteria As Criteria)
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
        ' fetch with no filter
        Fetch()
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteria)
        Fetch(criteria.Description, criteria.LogDescription)
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteriaList)
        Fetch(criteria.Description, criteria.LogDescription)
    End Sub

    Protected Overrides Sub DataPortal_Update()
        RaiseListChangedEvents = False
		For Each item As ClsForm In DeletedList
			item.DeleteSelf()
		Next
		DeletedList.Clear()
		For Each item As ClsForm In Me
			If item.IsNew Then
                item.Insert(Nothing)
			Else
                item.Update(Nothing)
			End If
        Next
		RaiseListChangedEvents = True
    End Sub

#End Region

#Region " Child Area "
	
	Friend Sub Update(ByVal parent As Object)
        RaiseListChangedEvents = False
        For Each item As ClsForm In DeletedList
            item.DeleteSelf()
        Next
        DeletedList.Clear()
        For Each item As ClsForm In Me
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
        Fetch(DAClsprgForms.Fetch())
    End Sub

    Private Sub Fetch(ByVal description As String, ByVal logDescription As String)
        Fetch(DAClsprgForms.Fetch(description, logDescription))
    End Sub

    Private Sub Fetch(ByVal description As String, ByVal logDescription() As String)
        Fetch(DAClsprgForms.Fetch(description, logDescription))
    End Sub

    Private Sub Fetch(ByVal list As DAClsprgForms.Struct())
        Me.RaiseListChangedEvents = False
        For Each Struct As DAClsprgForms.Struct In list
            Me.Add(ClsForm.GetChildForm(Struct))
        Next
        Me.RaiseListChangedEvents = True
    End Sub

#End Region

#End Region

End Class
