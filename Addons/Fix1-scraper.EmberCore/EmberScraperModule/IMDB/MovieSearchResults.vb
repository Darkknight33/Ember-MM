Imports System
Imports System.Collections.Generic

Namespace EmberScraperModule.IMDB
    Public Class MovieSearchResults
        ' Properties
        Public Property ExactMatches As List(Of Movie)
            Get
                Return Me._ExactMatches
            End Get
            Set(ByVal value As List(Of Movie))
                Me._ExactMatches = value
            End Set
        End Property

        Public Property PartialMatches As List(Of Movie)
            Get
                Return Me._PartialMatches
            End Get
            Set(ByVal value As List(Of Movie))
                Me._PartialMatches = value
            End Set
        End Property

        Public Property PopularTitles As List(Of Movie)
            Get
                Return Me._PopularTitles
            End Get
            Set(ByVal value As List(Of Movie))
                Me._PopularTitles = value
            End Set
        End Property


        ' Fields
        Private _ExactMatches As List(Of Movie) = New List(Of Movie)
        Private _PartialMatches As List(Of Movie) = New List(Of Movie)
        Private _PopularTitles As List(Of Movie) = New List(Of Movie)
    End Class
End Namespace

