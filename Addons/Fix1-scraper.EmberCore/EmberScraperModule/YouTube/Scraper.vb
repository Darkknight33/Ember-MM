Imports EmberAPI
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Runtime.CompilerServices
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Web
Imports System.Windows.Forms

Namespace EmberScraperModule.YouTube
    Public Class Scraper
        ' Events
        Public Custom Event Exception As ExceptionEventHandler
        Public Custom Event VideoLinksRetrieved As VideoLinksRetrievedEventHandler

        ' Methods
        Public Sub New()
            Scraper.__ENCAddToList(Me)
            Me.bwYT = New BackgroundWorker
        End Sub

        <DebuggerNonUserCode> _
        Private Shared Sub __ENCAddToList(ByVal value As Object)
            Dim list As List(Of WeakReference) = Scraper.__ENCList
            SyncLock list
                If (Scraper.__ENCList.Count = Scraper.__ENCList.Capacity) Then
                    Dim index As Integer = 0
                    Dim num3 As Integer = (Scraper.__ENCList.Count - 1)
                    Dim i As Integer = 0
                    Do While (i <= num3)
                        Dim reference As WeakReference = Scraper.__ENCList.Item(i)
                        If reference.IsAlive Then
                            If (i <> index) Then
                                Scraper.__ENCList.Item(index) = Scraper.__ENCList.Item(i)
                            End If
                            index += 1
                        End If
                        i += 1
                    Loop
                    Scraper.__ENCList.RemoveRange(index, (Scraper.__ENCList.Count - index))
                    Scraper.__ENCList.Capacity = Scraper.__ENCList.Count
                End If
                Scraper.__ENCList.Add(New WeakReference(RuntimeHelpers.GetObjectValue(value)))
            End SyncLock
        End Sub

        Private Sub bwYT_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
            Dim argument As String = CStr(e.Argument)
            Try 
                e.Result = Me.ParseYTFormats(argument, True)
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Private Sub bwYT_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
            Try 
                Dim videoLinksRetrievedEvent As VideoLinksRetrievedEventHandler
                If e.Cancelled Then
                    videoLinksRetrievedEvent = Me.VideoLinksRetrievedEvent
                    If (Not videoLinksRetrievedEvent Is Nothing) Then
                        videoLinksRetrievedEvent.Invoke(False)
                    End If
                ElseIf (Not e.Error Is Nothing) Then
                    Dim exceptionEvent As ExceptionEventHandler = Me.ExceptionEvent
                    If (Not exceptionEvent Is Nothing) Then
                        exceptionEvent.Invoke(e.Error)
                    End If
                ElseIf (Not e.Result Is Nothing) Then
                    Me._VideoLinks = DirectCast(e.Result, VideoLinkItemCollection)
                    videoLinksRetrievedEvent = Me.VideoLinksRetrievedEvent
                    If (Not videoLinksRetrievedEvent Is Nothing) Then
                        videoLinksRetrievedEvent.Invoke(True)
                    End If
                Else
                    videoLinksRetrievedEvent = Me.VideoLinksRetrievedEvent
                    If (Not videoLinksRetrievedEvent Is Nothing) Then
                        videoLinksRetrievedEvent.Invoke(False)
                    End If
                End If
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Public Sub CancelAsync()
            If Me.bwYT.IsBusy Then
                Me.bwYT.CancelAsync
            End If
            Do While Me.bwYT.IsBusy
                Application.DoEvents
                Thread.Sleep(50)
            Loop
        End Sub

        Public Sub GetVideoLinks(ByVal url As String)
            Try 
                Me._VideoLinks = Me.ParseYTFormats(url, False)
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Public Sub GetVideoLinksAsync(ByVal url As String)
            Try 
                If Not Me.bwYT.IsBusy Then
                    Me._VideoLinks = Nothing
                    Me.bwYT.WorkerSupportsCancellation = True
                    Me.bwYT.RunWorkerAsync(url)
                End If
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Private Function GetVideoTitle(ByVal HTML As String) As String
            Dim str3 As String = ""
            Dim pattern As String = "meta name=\""title\"" content=\s*\""([^']*?)\"""
            If Regex.IsMatch(HTML, pattern) Then
                str3 = Regex.Match(HTML, pattern).Groups.Item(1).Value
            End If
            Return str3
        End Function

        Private Function ParseYTFormats(ByVal url As String, ByVal doProgress As Boolean) As VideoLinkItemCollection
            Dim items2 As VideoLinkItemCollection
            Dim items As New VideoLinkItemCollection
            Dim http As New HTTP
            Try 
                If Me.bwYT.CancellationPending Then
                    Return items
                End If
                Dim hTML As String = http.DownloadData(url)
                If hTML.ToLower.Contains("page not found") Then
                    hTML = String.Empty
                End If
                If String.IsNullOrEmpty(hTML.Trim) Then
                    Return items
                End If
                If Me.bwYT.CancellationPending Then
                    Return items
                End If
                Dim str2 As String = Regex.Replace(Me.GetVideoTitle(hTML), "['?\\:*<>]*", "")
                Dim match As Match = Regex.Match(hTML, "url_encoded_fmt_stream_map=(.*?)\\u0026amp;", RegexOptions.IgnoreCase)
                If match.Success Then
                    Dim input As String = (HttpUtility.UrlDecode(match.Groups.Item(1).Value) & ",")
                    Dim pattern As String = "itag=(\d+)&url=(.*?),"
                    Dim matchs As MatchCollection = New Regex(pattern, RegexOptions.Singleline).Matches(input)
                    If (matchs.Count > 0) Then
                        Dim enumerator As IEnumerator
                        Try 
                            enumerator = matchs.GetEnumerator
                            Do While enumerator.MoveNext
                                Dim current As Match = DirectCast(enumerator.Current, Match)
                                Dim groups As GroupCollection = current.Groups
                                Dim link As New VideoLinkItem
                                Dim str6 As String = groups.Item(1).Value
                                If (str6 = "18") Then
                                    link.Description = "SQ (MP4)"
                                    link.FormatQuality = TrailerQuality.SQMP4
                                ElseIf (str6 = "22") Then
                                    link.Description = "720p"
                                    link.FormatQuality = TrailerQuality.HD720p
                                ElseIf (str6 = "34") Then
                                    link.Description = "SQ (FLV)"
                                    link.FormatQuality = TrailerQuality.SQFLV
                                ElseIf (str6 = "35") Then
                                    link.Description = "HQ (FLV)"
                                    link.FormatQuality = TrailerQuality.HQFLV
                                ElseIf (str6 = "37") Then
                                    link.Description = "1080p"
                                    link.FormatQuality = TrailerQuality.HD1080p
                                ElseIf (str6 = "46") Then
                                    link.Description = "1080p (VP8)"
                                    link.FormatQuality = TrailerQuality.HD1080pVP8
                                ElseIf (str6 = "45") Then
                                    link.Description = "720p (VP8)"
                                    link.FormatQuality = TrailerQuality.HD720pVP8
                                ElseIf (str6 = "44") Then
                                    link.Description = "HQ (VP8)"
                                    link.FormatQuality = TrailerQuality.HQVP8
                                ElseIf (str6 = "43") Then
                                    link.Description = "SQ (VP8)"
                                    link.FormatQuality = TrailerQuality.SQVP8
                                Else
                                    link.Description = "Other"
                                    link.FormatQuality = TrailerQuality.OTHERS
                                End If
                                link.URL = (HttpUtility.UrlDecode(groups.Item(2).Value) & HttpUtility.UrlEncode(("&title=" & str2)))
                                link.URL = link.URL.Replace("sig=", "signature=")
                                If Me.bwYT.CancellationPending Then
                                    Return items
                                End If
                                If (Not String.IsNullOrEmpty(link.URL) AndAlso http.IsValidURL(link.URL)) Then
                                    items.Add(link)
                                End If
                                If Me.bwYT.CancellationPending Then
                                    Return items
                                End If
                            Loop
                        Finally
                            If TypeOf enumerator Is IDisposable Then
                                TryCast(enumerator,IDisposable).Dispose
                            End If
                        End Try
                    End If
                End If
                items2 = items
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                items2 = New VideoLinkItemCollection
                ProjectData.ClearProjectError
                Return items2
                ProjectData.ClearProjectError
            Finally
                http = Nothing
            End Try
            Return items2
        End Function


        ' Properties
        Friend Overridable Property bwYT As BackgroundWorker
            <DebuggerNonUserCode> _
            Get
                Return Me._bwYT
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As BackgroundWorker)
                Dim handler As RunWorkerCompletedEventHandler = New RunWorkerCompletedEventHandler(AddressOf Me.bwYT_RunWorkerCompleted)
                Dim handler2 As DoWorkEventHandler = New DoWorkEventHandler(AddressOf Me.bwYT_DoWork)
                If (Not Me._bwYT Is Nothing) Then
                    RemoveHandler Me._bwYT.RunWorkerCompleted, handler
                    RemoveHandler Me._bwYT.DoWork, handler2
                End If
                Me._bwYT = WithEventsValue
                If (Not Me._bwYT Is Nothing) Then
                    AddHandler Me._bwYT.RunWorkerCompleted, handler
                    AddHandler Me._bwYT.DoWork, handler2
                End If
            End Set
        End Property

        Public ReadOnly Property VideoLinks As VideoLinkItemCollection
            Get
                If (Me._VideoLinks Is Nothing) Then
                    Me._VideoLinks = New VideoLinkItemCollection
                End If
                Return Me._VideoLinks
            End Get
        End Property


        ' Fields
        Private Shared __ENCList As List(Of WeakReference) = New List(Of WeakReference)
        <AccessedThroughProperty("bwYT")> _
        Private _bwYT As BackgroundWorker
        Private _VideoLinks As VideoLinkItemCollection

        ' Nested Types
        Public Delegate Sub ExceptionEventHandler(ByVal ex As Exception)

        Public Delegate Sub VideoLinksRetrievedEventHandler(ByVal bSuccess As Boolean)
    End Class
End Namespace

