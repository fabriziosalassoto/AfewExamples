Imports Csla
Imports SCT.DataAccess

<Serializable()> Public Class ClsOperationInfoList
    Inherits ReadOnlyListBase(Of ClsOperationInfoList, ClsOperationInfo)

#Region " Business Methods "

    Public Function GetItem(ByVal Id As Long) As ClsOperationInfo
        For Each item As ClsOperationInfo In Me
            If item.ID = Id Then
                Return item
            End If
        Next
        Return Nothing
    End Function

    Public Function GetItem(ByVal description As String) As ClsOperationInfo
        For Each item As ClsOperationInfo In Me
            If item.Description = description Then
                Return item
            End If
        Next
        Return Nothing
    End Function

    Public Overloads Function Contains(ByVal Id As Long) As Boolean
        For Each item As ClsOperationInfo In Me
            If item.ID = Id Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Overloads Function Contains(ByVal description As String) As Boolean
        For Each item As ClsOperationInfo In Me
            If item.Description = description Then
                Return True
            End If
        Next
        Return False
    End Function

#End Region

#Region " Authorization Rules "

    Public Shared Function CanGetObject() As Boolean
        Return True 'ApplicationContext.User.IsInRole(" ")
    End Function

#End Region

#Region " Factory Methods "

    Public Shared Function NewOperationInfoList() As ClsOperationInfoList
        Return New ClsOperationInfoList
    End Function

    Public Shared Function GetOperationInfoList() As ClsOperationInfoList
        Return DataPortal.Fetch(Of ClsOperationInfoList)(New Criteria)
    End Function

    Private Sub New()
        ' Require use of factory methods
    End Sub

#End Region

#Region " Data Access "

    <Serializable()> Private Class Criteria
        ' no criteria - retrieve all records
    End Class

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
        ' fetch with no filter
        Fetch()
    End Sub

    Private Sub Fetch()
        Fetch(DAClsprgOperations.Fetch())
    End Sub

    Private Sub Fetch(ByVal list As DAClsprgOperations.Struct())
        RaiseListChangedEvents = False
        IsReadOnly = False
        For Each Struct As DAClsprgOperations.Struct In list
            Me.Add(ClsOperationInfo.GetOperationInfo(Struct))
        Next
        IsReadOnly = True
        RaiseListChangedEvents = True
    End Sub

#End Region

End Class
