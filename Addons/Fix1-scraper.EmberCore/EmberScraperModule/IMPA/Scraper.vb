Imports EmberAPI
Imports EmberAPI.MediaContainers
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Windows.Forms

Namespace EmberScraperModule.IMPA
    Public Class Scraper
        ' Events
        Public Custom Event PostersDownloaded As PostersDownloadedEventHandler
        Public Custom Event ProgressUpdated As ProgressUpdatedEventHandler

        ' Methods
        Public Sub New()
            Scraper.__ENCAddToList(Me)
            Me.bwIMPA = New BackgroundWorker
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

        Private Sub bwIMPA_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
            Dim argument As Arguments = DirectCast(e.Argument, Arguments)
            Try 
                e.Result = Me.GetIMPAPosters(argument.Parameter)
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                e.Result = Nothing
                ProjectData.ClearProjectError
            End Try
        End Sub

        Private Sub bwIMPA_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
            If Not Me.bwIMPA.CancellationPending Then
                Dim progressUpdatedEvent As ProgressUpdatedEventHandler = Me.ProgressUpdatedEvent
                If (Not progressUpdatedEvent Is Nothing) Then
                    progressUpdatedEvent.Invoke(e.ProgressPercentage)
                End If
            End If
        End Sub

        Private Sub bwIMPA_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
            If Not Information.IsNothing(RuntimeHelpers.GetObjectValue(e.Result)) Then
                Dim postersDownloadedEvent As PostersDownloadedEventHandler = Me.PostersDownloadedEvent
                If (Not postersDownloadedEvent Is Nothing) Then
                    postersDownloadedEvent.Invoke(DirectCast(e.Result, List(Of Image)))
                End If
            End If
        End Sub

        Public Sub Cancel()
            If Me.bwIMPA.IsBusy Then
                Me.bwIMPA.CancelAsync
            End If
            Do While Me.bwIMPA.IsBusy
                Application.DoEvents
                Thread.Sleep(50)
            Loop
        End Sub

        Public Sub GetImagesAsync(ByVal sURL As String)
            Try 
                If Not Me.bwIMPA.IsBusy Then
                    Me.bwIMPA.WorkerSupportsCancellation = True
                    Me.bwIMPA.WorkerReportsProgress = True
                    Dim argument As New Arguments With { _
                        .Parameter = sURL _
                    }
                    Me.bwIMPA.RunWorkerAsync(argument)
                End If
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Public Function GetIMPAPosters(ByVal imdbID As String) As List(Of Image)
            Dim list As New List(Of Image)
            Try 
                If Me.bwIMPA.CancellationPending Then
                    Return Nothing
                End If
                Dim link As String = Me.GetLink(imdbID)
                If Not String.IsNullOrEmpty(link) Then
                    Dim enumerator As IEnumerator
                    Dim http As New HTTP
                    Dim input As String = http.DownloadData(link)
                    http = Nothing
                    If Me.bwIMPA.CancellationPending Then
                        Return Nothing
                    End If
                    Dim matchs As MatchCollection = Regex.Matches(input, "(thumbs/imp_([^>]*ver[^>]*.jpg))|(thumbs/imp_([^>]*.jpg))")
                    Try 
                        enumerator = matchs.GetEnumerator
                        Do While enumerator.MoveNext
                            Dim current As Match = DirectCast(enumerator.Current, Match)
                            If Me.bwIMPA.CancellationPending Then
                                Return Nothing
                            End If
                            Dim str3 As String = Strings.Replace(String.Format("{0}/{1}", link.Substring(0, link.LastIndexOf("/")), current.Value.ToString).Replace("thumbs", "posters"), "imp_", String.Empty, 1, -1, CompareMethod.Binary)
                            Dim item As New Image With { _
                                .Description = "poster", _
                                .URL = str3 _
                            }
                            list.Add(item)
                            str3 = str3.Insert(str3.LastIndexOf("."), "_xlg")
                            item = New Image With { _
                                .Description = "original", _
                                .URL = str3 _
                            }
                            list.Add(item)
                        Loop
                    Finally
                        If TypeOf enumerator Is IDisposable Then
                            TryCast(enumerator,IDisposable).Dispose
                        End If
                    End Try
                End If
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
            Return list
        End Function

        Private Function GetLink(ByVal IMDBID As String) As String
            Dim str As String
            Try 
                Dim input As String = New HTTP().DownloadData(String.Concat(New String() { "http://", Me.IMDBURL, "/title/tt", IMDBID, "/posters" }))
                Dim matchs As MatchCollection = Regex.Matches(input, "http://([^""]*)impawards.com/([^""]*)")
                If (matchs.Count > 0) Then
                    Return matchs.Item(0).Value.ToString
                End If
                str = String.Empty
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                str = String.Empty
                ProjectData.ClearProjectError
                Return str
                ProjectData.ClearProjectError
            End Try
            Return str
        End Function


        ' Properties
        Friend Overridable Property bwIMPA As BackgroundWorker
            <DebuggerNonUserCode> _
            Get
                Return Me._bwIMPA
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As BackgroundWorker)
                Dim handler As RunWorkerCompletedEventHandler = New RunWorkerCompletedEventHandler(AddressOf Me.bwIMPA_RunWorkerCompleted)
                Dim handler2 As ProgressChangedEventHandler = New ProgressChangedEventHandler(AddressOf Me.bwIMPA_ProgressChanged)
                Dim handler3 As DoWorkEventHandler = New DoWorkEventHandler(AddressOf Me.bwIMPA_DoWork)
                If (Not Me._bwIMPA Is Nothing) Then
                    RemoveHandler Me._bwIMPA.RunWorkerCompleted, handler
                    RemoveHandler Me._bwIMPA.ProgressChanged, handler2
                    RemoveHandler Me._bwIMPA.DoWork, handler3
                End If
                Me._bwIMPA = WithEventsValue
                If (Not Me._bwIMPA Is Nothing) Then
                    AddHandler Me._bwIMPA.RunWorkerCompleted, handler
                    AddHandler Me._bwIMPA.ProgressChanged, handler2
                    AddHandler Me._bwIMPA.DoWork, handler3
                End If
            End Set
        End Property


        ' Fields
        Private Shared __ENCList As List(Of WeakReference) = New List(Of WeakReference)
        <AccessedThroughProperty("bwIMPA")> _
        Private _bwIMPA As BackgroundWorker
        Public IMDBURL As String

        ' Nested Types
        <StructLayout(LayoutKind.Sequential)> _
        Private Structure Arguments
            Public Parameter As String
            Public sType As String
        End Structure

        Public Delegate Sub PostersDownloadedEventHandler(ByVal Posters As List(Of Image))

        Public Delegate Sub ProgressUpdatedEventHandler(ByVal iPercent As Integer)

        <StructLayout(LayoutKind.Sequential)> _
        Private Structure Results
            Public Result As Object
            Public ResultList As List(Of Image)
        End Structure
    End Class
End Namespace

