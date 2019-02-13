Option Strict On

<Serializable()> Public Class AdProjectDemographicNewData

    Private mID As New NewFieldData(Of Long)
    Private mProject As New AdProjectNewData
    Private mTag As New NewFieldData(Of String)
    Private mRequirement As New NewFieldData(Of String)

    Public Property ID() As NewFieldData(Of Long)
        Get
            Return Me.mID
        End Get
        Set(ByVal value As NewFieldData(Of Long))
            Me.mID = value
        End Set
    End Property

    Public Property Project() As AdProjectNewData
        Get
            Return Me.mProject
        End Get
        Set(ByVal value As AdProjectNewData)
            Me.mProject = value
        End Set
    End Property

    Public Property Tag() As NewFieldData(Of String)
        Get
            Return Me.mTag
        End Get
        Set(ByVal value As NewFieldData(Of String))
            Me.mTag = value
        End Set
    End Property

    Public Property Requirement() As NewFieldData(Of String)
        Get
            Return Me.mRequirement
        End Get
        Set(ByVal value As NewFieldData(Of String))
            Me.mRequirement = value
        End Set
    End Property

End Class
