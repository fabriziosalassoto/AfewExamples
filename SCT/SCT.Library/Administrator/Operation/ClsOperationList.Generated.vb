Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsOperationList
    Inherits BusinessListBase(Of ClsOperationList, ClsOperation)

#Region " Business Methods "

    Public Function GetItem(ByVal Id As Long) As ClsOperation
        For Each item As ClsOperation In Me
            If item.ID = Id Then
                Return item
            End If
        Next
        Return Nothing
    End Function

    Public Function GetItem(ByVal description As String) As ClsOperation
        For Each item As ClsOperation In Me
            If item.Description = description Then
                Return item
            End If
        Next
        Return Nothing
    End Function

    Public Overloads Sub Remove(ByVal Id As Long)
        For Each item As ClsOperation In Me
            If item.ID = Id Then
                Remove(item)
                Exit For
            End If
        Next
    End Sub

    Public Overloads Function Contains(ByVal Id As Long) As Boolean
        For Each item As ClsOperation In Me
            If item.ID = Id Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Overloads Function Contains(ByVal description As String) As Boolean
        For Each item As ClsOperation In Me
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

	Public Shared Function NewOperationList() As ClsOperationList
		Return DataPortal.Create(Of ClsOperationList)(New Criteria())
    End Function

    Public Shared Function GetOperationList() As ClsOperationList
        Return DataPortal.Fetch(Of ClsOperationList)(New Criteria())
    End Function

#End Region

#Region " Child Methods "

    Friend Shared Function NewChildOperationList() As ClsOperationList
        Return New ClsOperationList
    End Function

    Friend Shared Function GetChildOperationList(ByVal list As DAClsprgOperations.Struct()) As ClsOperationList
        Return New ClsOperationList(list)
    End Function
	
    Private Sub New()
        MarkAsChild()
    End Sub

    Private Sub New(ByVal list As DAClsprgOperations.Struct())
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

    <RunLocal()> Private Overloads Sub DataPortal_Create(ByVal criteria As Criteria)
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
        ' fetch with no filter
        Fetch()
    End Sub

    Protected Overrides Sub DataPortal_Update()
        RaiseListChangedEvents = False
        For Each item As ClsOperation In DeletedList
            item.DeleteSelf()
        Next
        DeletedList.Clear()
        For Each item As ClsOperation In Me
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
        For Each item As ClsOperation In DeletedList
            item.DeleteSelf()
        Next
        DeletedList.Clear()
        For Each item As ClsOperation In Me
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
        Fetch(DAClsprgOperations.Fetch())
    End Sub

    Private Sub Fetch(ByVal list As DAClsprgOperations.Struct())
        Me.RaiseListChangedEvents = False
        For Each Struct As DAClsprgOperations.Struct In list
            Me.Add(ClsOperation.GetChildOperation(Struct))
        Next
        Me.RaiseListChangedEvents = True
    End Sub

#End Region

#End Region

End Class
