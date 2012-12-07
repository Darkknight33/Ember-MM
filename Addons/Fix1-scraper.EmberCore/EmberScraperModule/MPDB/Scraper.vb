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

Namespace EmberScraperModule.MPDB
    Public Class Scraper
        ' Events
        Public Custom Event PostersDownloaded As PostersDownloadedEventHandler
        Public Custom Event ProgressUpdated As ProgressUpdatedEventHandler

        ' Methods
        Public Sub New()
            Scraper.__ENCAddToList(Me)
            Me.bwMPDB = New BackgroundWorker
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

        Private Sub bwMPDB_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
            Dim argument As Arguments = DirectCast(e.Argument, Arguments)
            Try 
                e.Result = Me.GetMPDBPosters(argument.Parameter)
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                e.Result = Nothing
                ProjectData.ClearProjectError
            End Try
        End Sub

        Private Sub bwMPDB_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
            If Not Me.bwMPDB.CancellationPending Then
                Dim progressUpdatedEvent As ProgressUpdatedEventHandler = Me.ProgressUpdatedEvent
                If (Not progressUpdatedEvent Is Nothing) Then
                    progressUpdatedEvent.Invoke(e.ProgressPercentage)
                End If
            End If
        End Sub

        Private Sub bwMPDB_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
            If Not Information.IsNothing(RuntimeHelpers.GetObjectValue(e.Result)) Then
                Dim postersDownloadedEvent As PostersDownloadedEventHandler = Me.PostersDownloadedEvent
                If (Not postersDownloadedEvent Is Nothing) Then
                    postersDownloadedEvent.Invoke(DirectCast(e.Result, List(Of Image)))
                End If
            End If
        End Sub

        Public Sub Cancel()
            If Me.bwMPDB.IsBusy Then
                Me.bwMPDB.CancelAsync
            End If
            Do While Me.bwMPDB.IsBusy
                Application.DoEvents
                Thread.Sleep(50)
            Loop
        End Sub

        Public Sub GetImagesAsync(ByVal imdbID As String)
            Try 
                If Not Me.bwMPDB.IsBusy Then
                    Me.bwMPDB.WorkerSupportsCancellation = True
                    Me.bwMPDB.WorkerReportsProgress = True
                    Dim argument As New Arguments With { _
                        .Parameter = imdbID _
                    }
                    Me.bwMPDB.RunWorkerAsync(argument)
                End If
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Public Function GetMPDBPosters(ByVal imdbID As String) As List(Of Image)
            Dim list As New List(Of Image)
            If Me.bwMPDB.CancellationPending Then
                Return Nothing
            End If
            Try 
                Dim input As String = New HTTP().DownloadData(("http://www.movieposterdb.com/movie/" & imdbID))
                If Me.bwMPDB.CancellationPending Then
                    Return Nothing
                End If
                If Me.bwMPDB.WorkerReportsProgress Then
                    Me.bwMPDB.ReportProgress(1)
                End If
                If Regex.IsMatch(input, ("http://www.imdb.com/title/tt" & imdbID), (RegexOptions.Singleline Or (RegexOptions.Multiline Or RegexOptions.IgnoreCase))) Then
                    Dim enumerator As IEnumerator
                    Dim matchs As MatchCollection = Regex.Matches(input, "http://www.movieposterdb.com/posters/[0-9_](.*?)/[0-9](.*?)/[0-9](.*?)/[a-z0-9_](.*?).jpg")
                    Dim str2 As String = String.Empty
                    Try 
                        enumerator = matchs.GetEnumerator
                        Do While enumerator.MoveNext
                            Dim current As Match = DirectCast(enumerator.Current, Match)
                            If Me.bwMPDB.CancellationPending Then
                                Return Nothing
                            End If
                            str2 = current.Value.Remove((current.Value.LastIndexOf("/") + 1), 1).Insert((current.Value.LastIndexOf("/") + 1), "l")
                            Dim item As New Image With { _
                                .Description = "poster", _
                                .URL = str2 _
                            }
                            list.Add(item)
                        Loop
                    Finally
                        If TypeOf enumerator Is IDisposable Then
                            TryCast(enumerator,IDisposable).Dispose
                        End If
                    End Try
                End If
                If Me.bwMPDB.WorkerReportsProgress Then
                    Me.bwMPDB.ReportProgress(3)
                End If
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
            Return list
        End Function


        ' Properties
        Friend Overridable Property bwMPDB As BackgroundWorker
            <DebuggerNonUserCode> _
            Get
                Return Me._bwMPDB
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As BackgroundWorker)
                Dim handler As RunWorkerCompletedEventHandler = New RunWorkerCompletedEventHandler(AddressOf Me.bwMPDB_RunWorkerCompleted)
                Dim handler2 As ProgressChangedEventHandler = New ProgressChangedEventHandler(AddressOf Me.bwMPDB_ProgressChanged)
                Dim handler3 As DoWorkEventHandler = New DoWorkEventHandler(AddressOf Me.bwMPDB_DoWork)
                If (Not Me._bwMPDB Is Nothing) Then
                    RemoveHandler Me._bwMPDB.RunWorkerCompleted, handler
                    RemoveHandler Me._bwMPDB.ProgressChanged, handler2
                    RemoveHandler Me._bwMPDB.DoWork, handler3
                End If
                Me._bwMPDB = WithEventsValue
                If (Not Me._bwMPDB Is Nothing) Then
                    AddHandler Me._bwMPDB.RunWorkerCompleted, handler
                    AddHandler Me._bwMPDB.ProgressChanged, handler2
                    AddHandler Me._bwMPDB.DoWork, handler3
                End If
            End Set
        End Property


        ' Fields
        Private Shared __ENCList As List(Of WeakReference) = New List(Of WeakReference)
        <AccessedThroughProperty("bwMPDB")> _
        Private _bwMPDB As BackgroundWorker
        Public IMDBURL As String

        ' Nested Types
        <StructLayout(LayoutKind.Sequential)> _
        Private Structure Arguments
            Public Parameter As String
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

