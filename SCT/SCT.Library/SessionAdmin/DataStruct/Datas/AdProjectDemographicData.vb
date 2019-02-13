Option Strict On

<Serializable()> Public Class AdProjectDemographicData

    Private mID As Long
    Private mProject As New AdProjectData
    Private mTag As String = String.Empty
    Private mRequirement As String = String.Empty

    Public Property ID() As Long
        Get
            Return Me.mID
        End Get
        Set(ByVal value As Long)
            Me.mID = value
        End Set
    End Property

    Public Property Project() As AdProjectData
        Get
            Return Me.mProject
        End Get
        Set(ByVal value As AdProjectData)
            Me.mProject = value
        End Set
    End Property

    Public Property Tag() As String
        Get
            Return Me.mTag
        End Get
        Set(ByVal value As String)
            Me.mTag = value
        End Set
    End Property

    Public Property Requirement() As String
        Get
            Return Me.mRequirement
        End Get
        Set(ByVal value As String)
            Me.mRequirement = value
        End Set
    End Property

End Class
