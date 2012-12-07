Imports System
Imports System.Collections.Generic

Namespace EmberScraperModule.YouTube
    Public Class VideoLinkItemCollection
        Inherits SortedList(Of TrailerQuality, VideoLinkItem)
        ' Methods
        Public Sub Add(ByVal Link As VideoLinkItem)
            If Not MyBase.ContainsKey(Link.FormatQuality) Then
                MyBase.Add(Link.FormatQuality, Link)
            End If
        End Sub

    End Class
End Namespace

