Option Strict On

Imports Microsoft.VisualBasic

Public Class SubDemographicData

    Private mID As Long
    Private mTag As String
    Private mAnswer As String

    Public Property ID() As Long
        Get
            Return Me.mID
        End Get
        Set(ByVal value As Long)
            Me.mID = value
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

    Public Property Answer() As String
        Get
            Return Me.mAnswer
        End Get
        Set(ByVal value As String)
            Me.mAnswer = value
        End Set
    End Property

End Class
