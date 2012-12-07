Imports EmberAPI
Imports System

Namespace EmberScraperModule.YouTube
    Public Class VideoLinkItem
        ' Properties
        Public Property Description As String
            Get
                Return Me._Description
            End Get
            Set(ByVal value As String)
                Me._Description = value
            End Set
        End Property

        Friend Property FormatQuality As TrailerQuality
            Get
                Return Me._FormatQuality
            End Get
            Set(ByVal value As TrailerQuality)
                Me._FormatQuality = value
            End Set
        End Property

        Public Property URL As String
            Get
                Return Me._URL
            End Get
            Set(ByVal value As String)
                Me._URL = value
            End Set
        End Property


        ' Fields
        Private _Description As String
        Private _FormatQuality As TrailerQuality
        Private _URL As String
    End Class
End Namespace

