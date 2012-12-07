Imports EmberAPI
Imports EmberScraperModule.IMDB
Imports EmberScraperModule.TMDB
Imports EmberScraperModule.YouTube
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions

Namespace EmberScraperModule
    Public Class Trailers
        ' Events
        Public Custom Event ProgressUpdated As ProgressUpdatedEventHandler

        ' Methods
        Public Sub New()
            Trailers.__ENCAddToList(Me)
            Me.WebPage = New HTTP
            Me._ImdbID = String.Empty
            Me._ImdbTrailerPage = String.Empty
            Me._TrailerList = New List(Of String)
            Me.Clear
            AddHandler Me.WebPage.ProgressUpdated, New ProgressUpdatedEventHandler(AddressOf Me.DownloadProgressUpdated)
        End Sub

        <DebuggerNonUserCode> _
        Private Shared Sub __ENCAddToList(ByVal value As Object)
            Dim list As List(Of WeakReference) = Trailers.__ENCList
            SyncLock list
                If (Trailers.__ENCList.Count = Trailers.__ENCList.Capacity) Then
                    Dim index As Integer = 0
                    Dim num3 As Integer = (Trailers.__ENCList.Count - 1)
                    Dim i As Integer = 0
                    Do While (i <= num3)
                        Dim reference As WeakReference = Trailers.__ENCList.Item(i)
                        If reference.IsAlive Then
                            If (i <> index) Then
                                Trailers.__ENCList.Item(index) = Trailers.__ENCList.Item(i)
                            End If
                            index += 1
                        End If
                        i += 1
                    Loop
                    Trailers.__ENCList.RemoveRange(index, (Trailers.__ENCList.Count - index))
                    Trailers.__ENCList.Capacity = Trailers.__ENCList.Count
                End If
                Trailers.__ENCList.Add(New WeakReference(RuntimeHelpers.GetObjectValue(value)))
            End SyncLock
        End Sub

        Public Sub Cancel()
            Me.WebPage.Cancel
        End Sub

        Public Sub Clear()
            Me._TrailerList.Clear
            Me._ImdbID = String.Empty
            Me._ImdbTrailerPage = String.Empty
        End Sub

        Public Sub DeleteTrailers(ByVal sPath As String, ByVal NewTrailer As String)
            Dim enumerator As Enumerator(Of String)
            Dim fullName As String = Directory.GetParent(sPath).FullName
            Dim str2 As String = Path.Combine(fullName, StringUtils.CleanStackingMarkers(Path.GetFileNameWithoutExtension(sPath), False))
            Dim str3 As String = Path.Combine(fullName, Path.GetFileNameWithoutExtension(sPath))
            Try 
                enumerator = Master.eSettings.ValidExts.GetEnumerator
                Do While enumerator.MoveNext
                    Dim current As String = enumerator.Current
                    If (File.Exists((str2 & "-trailer" & current)) AndAlso ((str2 & "-trailer" & current).ToLower <> NewTrailer.ToLower)) Then
                        File.Delete((str2 & "-trailer" & current))
                    ElseIf (File.Exists((str2 & "[trailer]" & current)) AndAlso ((str2 & "[trailer]" & current).ToLower <> NewTrailer.ToLower)) Then
                        File.Delete((str2 & "[trailer]" & current))
                    ElseIf (File.Exists((str3 & "-trailer" & current)) AndAlso ((str3 & "-trailer" & current).ToLower <> NewTrailer.ToLower)) Then
                        File.Delete((str3 & "-trailer" & current))
                    ElseIf (File.Exists((str3 & "[trailer]" & current)) AndAlso ((str3 & "[trailer]" & current).ToLower <> NewTrailer.ToLower)) Then
                        File.Delete((str3 & "[trailer]" & current))
                    End If
                Loop
            Finally
                enumerator.Dispose
            End Try
        End Sub

        Public Sub DownloadProgressUpdated(ByVal iPercent As Integer)
            Dim progressUpdatedEvent As ProgressUpdatedEventHandler = Me.ProgressUpdatedEvent
            If (Not progressUpdatedEvent Is Nothing) Then
                progressUpdatedEvent.Invoke(iPercent)
            End If
        End Sub

        Public Function DownloadSingleTrailer(ByVal sPath As String, ByVal ImdbID As String, ByVal isSingle As Boolean, ByVal currNfoTrailer As String) As String
            Dim str2 As String = String.Empty
            Try 
                Me._TrailerList.Clear
                If (If((Not Master.eSettings.UpdaterTrailersNoDownload AndAlso Me.IsAllowedToDownload(sPath, isSingle, currNfoTrailer, True)), 1, 0) = 0) Then
                    goto Label_040F
                End If
                Me.GetTrailers(ImdbID, True)
                If (Me._TrailerList.Count <= 0) Then
                    Return str2
                End If
                Dim uRL As String = String.Empty
                If Not Regex.IsMatch(Me._TrailerList.Item(0).ToString, "http:\/\/.*youtube.*\/watch\?v=(.{11})&?.*") Then
                    goto Label_03AA
                End If
                Dim scraper As New Scraper
                scraper.GetVideoLinks(Me._TrailerList.Item(0).ToString)
                If scraper.VideoLinks.ContainsKey(Master.eSettings.PreferredTrailerQuality) Then
                    uRL = scraper.VideoLinks.Item(Master.eSettings.PreferredTrailerQuality).URL
                Else
                    Select Case Master.eSettings.PreferredTrailerQuality
                        Case TrailerQuality.HD1080p
                            If Not scraper.VideoLinks.ContainsKey(TrailerQuality.HD720p) Then
                                goto Label_0143
                            End If
                            uRL = scraper.VideoLinks.Item(TrailerQuality.HD720p).URL
                            Exit Select
                        Case TrailerQuality.HD1080pVP8
                            If Not scraper.VideoLinks.ContainsKey(TrailerQuality.HD720pVP8) Then
                                goto Label_01E0
                            End If
                            uRL = scraper.VideoLinks.Item(TrailerQuality.HD720pVP8).URL
                            Exit Select
                        Case TrailerQuality.HD720p
                            If Not scraper.VideoLinks.ContainsKey(TrailerQuality.HQFLV) Then
                                goto Label_0257
                            End If
                            uRL = scraper.VideoLinks.Item(TrailerQuality.HQFLV).URL
                            Exit Select
                        Case TrailerQuality.HD720pVP8
                            If Not scraper.VideoLinks.ContainsKey(TrailerQuality.HQVP8) Then
                                goto Label_02CE
                            End If
                            uRL = scraper.VideoLinks.Item(TrailerQuality.HQVP8).URL
                            Exit Select
                        Case TrailerQuality.SQMP4
                            If scraper.VideoLinks.ContainsKey(TrailerQuality.SQFLV) Then
                                uRL = scraper.VideoLinks.Item(TrailerQuality.SQFLV).URL
                            End If
                            Exit Select
                        Case TrailerQuality.HQFLV
                            If Not scraper.VideoLinks.ContainsKey(TrailerQuality.SQMP4) Then
                                goto Label_031F
                            End If
                            uRL = scraper.VideoLinks.Item(TrailerQuality.SQMP4).URL
                            Exit Select
                        Case TrailerQuality.HQVP8
                            If scraper.VideoLinks.ContainsKey(TrailerQuality.SQVP8) Then
                                uRL = scraper.VideoLinks.Item(TrailerQuality.SQVP8).URL
                            End If
                            Exit Select
                        Case TrailerQuality.SQFLV
                            uRL = String.Empty
                            Exit Select
                        Case TrailerQuality.SQVP8
                            uRL = String.Empty
                            Exit Select
                    End Select
                End If
                goto Label_03BD
            Label_0143:
                If scraper.VideoLinks.ContainsKey(TrailerQuality.HQFLV) Then
                    uRL = scraper.VideoLinks.Item(TrailerQuality.HQFLV).URL
                ElseIf scraper.VideoLinks.ContainsKey(TrailerQuality.SQMP4) Then
                    uRL = scraper.VideoLinks.Item(TrailerQuality.SQMP4).URL
                ElseIf scraper.VideoLinks.ContainsKey(TrailerQuality.SQFLV) Then
                    uRL = scraper.VideoLinks.Item(TrailerQuality.SQFLV).URL
                End If
                goto Label_03BD
            Label_01E0:
                If scraper.VideoLinks.ContainsKey(TrailerQuality.HQVP8) Then
                    uRL = scraper.VideoLinks.Item(TrailerQuality.HQVP8).URL
                ElseIf scraper.VideoLinks.ContainsKey(TrailerQuality.SQVP8) Then
                    uRL = scraper.VideoLinks.Item(TrailerQuality.SQVP8).URL
                End If
                goto Label_03BD
            Label_0257:
                If scraper.VideoLinks.ContainsKey(TrailerQuality.SQMP4) Then
                    uRL = scraper.VideoLinks.Item(TrailerQuality.SQMP4).URL
                ElseIf scraper.VideoLinks.ContainsKey(TrailerQuality.SQFLV) Then
                    uRL = scraper.VideoLinks.Item(TrailerQuality.SQFLV).URL
                End If
                goto Label_03BD
            Label_02CE:
                If scraper.VideoLinks.ContainsKey(TrailerQuality.SQVP8) Then
                    uRL = scraper.VideoLinks.Item(TrailerQuality.SQVP8).URL
                End If
                goto Label_03BD
            Label_031F:
                If scraper.VideoLinks.ContainsKey(TrailerQuality.SQFLV) Then
                    uRL = scraper.VideoLinks.Item(TrailerQuality.SQFLV).URL
                End If
                goto Label_03BD
            Label_03AA:
                uRL = Me._TrailerList.Item(0).ToString
            Label_03BD:
                If Not String.IsNullOrEmpty(uRL) Then
                    str2 = Me.WebPage.DownloadFile(uRL, sPath, False, "trailer")
                    If String.IsNullOrEmpty(str2) Then
                        Return str2
                    End If
                    If Master.eSettings.DeleteAllTrailers Then
                        Me.DeleteTrailers(sPath, str2)
                    End If
                End If
                Return str2
            Label_040F:
                If (If((Master.eSettings.UpdaterTrailersNoDownload AndAlso Me.IsAllowedToDownload(sPath, isSingle, currNfoTrailer, False)), 1, 0) = 0) Then
                    Return str2
                End If
                Me.GetTrailers(ImdbID, True)
                If (Me._TrailerList.Count > 0) Then
                    str2 = Me._TrailerList.Item(0).ToString
                End If
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
            Return str2
        End Function

        Public Function DownloadTrailer(ByVal sPath As String, ByVal sURL As String) As String
            Dim str2 As String = String.Empty
            If Not String.IsNullOrEmpty(sURL) Then
                str2 = Me.WebPage.DownloadFile(sURL, sPath, True, "trailer")
                If String.IsNullOrEmpty(str2) Then
                    Return str2
                End If
                If Master.eSettings.DeleteAllTrailers Then
                    Me.DeleteTrailers(sPath, str2)
                End If
            End If
            Return str2
        End Function

        Private Sub GetIMDBTrailer()
            Dim trailers As List(Of String) = New Scraper() With { _
                .IMDBURL = AdvancedSettings.GetSetting("IMDBURL", "akas.imdb.com", "") _
            }.GetTrailers(Me._ImdbID)
            Me._TrailerList.AddRange(trailers)
        End Sub

        Private Sub GetTMDBTrailer()
            Dim trailers As String = New Scraper().GetTrailers(Me._ImdbID)
            If Not String.IsNullOrEmpty(trailers) Then
                Me._TrailerList.Add(trailers)
            End If
        End Sub

        Public Function GetTrailers(ByVal ImdbID As String, ByVal Optional BreakAfterFound As Boolean = True) As List(Of String)
            Me._ImdbID = ImdbID
            If AdvancedSettings.GetBooleanSetting("UseIMDBTrailer", False, "") Then
                Me.GetIMDBTrailer
            End If
            If AdvancedSettings.GetBooleanSetting("UseTMDBTrailer", False, "") Then
                Me.GetTMDBTrailer
            End If
            Return Me._TrailerList
        End Function

        Public Function IsAllowedToDownload(ByVal sPath As String, ByVal isDL As Boolean, ByVal currNfoTrailer As String, ByVal Optional isSS As Boolean = False) As Boolean
            Dim scanner As New Scanner
            If isDL Then
                Return ((String.IsNullOrEmpty(scanner.GetTrailerPath(sPath)) OrElse Master.eSettings.OverwriteTrailer) OrElse ((isSS AndAlso String.IsNullOrEmpty(scanner.GetTrailerPath(sPath))) AndAlso (String.IsNullOrEmpty(currNfoTrailer) OrElse Not Master.eSettings.LockTrailer)))
            End If
            Return (String.IsNullOrEmpty(currNfoTrailer) OrElse Not Master.eSettings.LockTrailer)
        End Function


        ' Fields
        Private Shared __ENCList As List(Of WeakReference) = New List(Of WeakReference)
        Private _ImdbID As String
        Private _ImdbTrailerPage As String
        Private _TrailerList As List(Of String)
        Public IMDBURL As String
        Private WebPage As HTTP

        ' Nested Types
        Public Delegate Sub ProgressUpdatedEventHandler(ByVal iPercent As Integer)
    End Class
End Namespace

