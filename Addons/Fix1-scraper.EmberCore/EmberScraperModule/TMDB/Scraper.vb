Imports EmberAPI
Imports EmberAPI.MediaContainers
Imports EmberScraperModule.My
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Linq
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Threading
Imports System.Windows.Forms
Imports System.Xml.Linq

Namespace EmberScraperModule.TMDB
    Public Class Scraper
        ' Events
        Public Custom Event PostersDownloaded As PostersDownloadedEventHandler
        Public Custom Event ProgressUpdated As ProgressUpdatedEventHandler

        ' Methods
        Public Sub New()
            Scraper.__ENCAddToList(Me)
            Me.bwTMDB = New BackgroundWorker
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

        <CompilerGenerated> _
        Private Shared Function _Lambda$__86(ByVal iNode As XElement) As XElement
            Return iNode
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__87(ByVal iNode As XElement) As XElement
            Return iNode
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__88(ByVal xNode As XElement) As XElement
            Return xNode
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__89(ByVal tNode As XElement) As IEnumerable(Of XElement)
            Return tNode.Elements(XName.Get("trailer", ""))
        End Function

        Private Sub bwTMDB_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
            Dim argument As Arguments = DirectCast(e.Argument, Arguments)
            Try 
                e.Result = Me.GetTMDBImages(argument.Parameter, argument.sType)
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                e.Result = Nothing
                ProjectData.ClearProjectError
            End Try
        End Sub

        Private Sub bwTMDB_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
            If Not Me.bwTMDB.CancellationPending Then
                Dim progressUpdatedEvent As ProgressUpdatedEventHandler = Me.ProgressUpdatedEvent
                If (Not progressUpdatedEvent Is Nothing) Then
                    progressUpdatedEvent.Invoke(e.ProgressPercentage)
                End If
            End If
        End Sub

        Private Sub bwTMDB_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
            If Not Information.IsNothing(RuntimeHelpers.GetObjectValue(e.Result)) Then
                Dim postersDownloadedEvent As PostersDownloadedEventHandler = Me.PostersDownloadedEvent
                If (Not postersDownloadedEvent Is Nothing) Then
                    postersDownloadedEvent.Invoke(DirectCast(e.Result, List(Of Image)))
                End If
            End If
        End Sub

        Public Sub Cancel()
            If Me.bwTMDB.IsBusy Then
                Me.bwTMDB.CancelAsync
            End If
            Do While Me.bwTMDB.IsBusy
                Application.DoEvents
                Thread.Sleep(50)
            Loop
        End Sub

        Public Sub GetImagesAsync(ByVal imdbID As String, ByVal sType As String)
            Try 
                If Not Me.bwTMDB.IsBusy Then
                    Me.bwTMDB.WorkerSupportsCancellation = True
                    Me.bwTMDB.WorkerReportsProgress = True
                    Dim argument As New Arguments With { _
                        .Parameter = imdbID, _
                        .sType = sType _
                    }
                    Me.bwTMDB.RunWorkerAsync(argument)
                End If
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Public Function GetTMDBImages(ByVal imdbID As String, ByVal sType As String) As List(Of Image)
            Dim list As New List(Of Image)
            Dim http As New HTTP
            If Me.bwTMDB.CancellationPending Then
                Return Nothing
            End If
            Try 
                Dim str As String = http.DownloadData(String.Format("http://api.themoviedb.org/2.1/Movie.getImages/en/xml/{0}/tt{1}", "b1ecff8c76278262b27de1569f57f6bd", imdbID))
                If Not String.IsNullOrEmpty(str) Then
                    Dim document As XDocument
                    Try 
                        document = XDocument.Parse(str)
                    Catch exception1 As Exception
                        ProjectData.SetProjectError(exception1)
                        Dim list2 As List(Of Image) = list
                        ProjectData.ClearProjectError
                        Return list2
                    End Try
                    If Me.bwTMDB.WorkerReportsProgress Then
                        Me.bwTMDB.ReportProgress(1)
                    End If
                    If Me.bwTMDB.CancellationPending Then
                        Return Nothing
                    End If
                    If (InternalXmlHelper.get_Value(document.Descendants(XName.Get("OpenSearchDescription", "")).Descendants(Of XElement)(XName.Get("movies", ""))) <> "Nothing found.") Then
                        Dim image3 As Image
                        If (sType = "poster") Then
                            Dim source As IEnumerable(Of XElement) = document.Descendants(XName.Get("OpenSearchDescription", "")).Descendants(Of XElement)(XName.Get("movies", "")).Descendants(Of XElement)(XName.Get("movie", "")).Descendants(Of XElement)(XName.Get("images", "")).Descendants(Of XElement)(XName.Get("poster", "")).Elements(Of XElement)().Select(Of XElement, XElement)(New Func(Of XElement, XElement)(AddressOf Scraper._Lambda$__86))
                            If (source.Count(Of XElement)() > 0) Then
                                Dim element As XElement
                                For Each element In source
                                    Dim str2 As String = element.Parent.Attribute("id").Value
                                    If Me.bwTMDB.CancellationPending Then
                                        Return Nothing
                                    End If
                                    image3 = New Image With { _
                                        .URL = InternalXmlHelper.get_AttributeValue(element, XName.Get("url", "")), _
                                        .Description = InternalXmlHelper.get_AttributeValue(element, XName.Get("size", "")), _
                                        .Width = InternalXmlHelper.get_AttributeValue(element, XName.Get("width", "")), _
                                        .Height = InternalXmlHelper.get_AttributeValue(element, XName.Get("height", "")), _
                                        .ParentID = str2 _
                                    }
                                    Dim item As Image = image3
                                    list.Add(item)
                                Next
                            End If
                        ElseIf (sType = "backdrop") Then
                            Dim enumerable2 As IEnumerable(Of XElement) = document.Descendants(XName.Get("OpenSearchDescription", "")).Descendants(Of XElement)(XName.Get("movies", "")).Descendants(Of XElement)(XName.Get("movie", "")).Descendants(Of XElement)(XName.Get("images", "")).Descendants(Of XElement)(XName.Get("backdrop", "")).Elements(Of XElement)().Select(Of XElement, XElement)(New Func(Of XElement, XElement)(AddressOf Scraper._Lambda$__87))
                            If (enumerable2.Count(Of XElement)() > 0) Then
                                Dim element2 As XElement
                                For Each element2 In enumerable2
                                    Dim str3 As String = element2.Parent.Attribute("id").Value
                                    If Me.bwTMDB.CancellationPending Then
                                        Return Nothing
                                    End If
                                    image3 = New Image With { _
                                        .URL = InternalXmlHelper.get_AttributeValue(element2, XName.Get("url", "")), _
                                        .Description = InternalXmlHelper.get_AttributeValue(element2, XName.Get("size", "")), _
                                        .Width = InternalXmlHelper.get_AttributeValue(element2, XName.Get("width", "")), _
                                        .Height = InternalXmlHelper.get_AttributeValue(element2, XName.Get("height", "")), _
                                        .ParentID = str3 _
                                    }
                                    Dim image2 As Image = image3
                                    list.Add(image2)
                                Next
                            End If
                        End If
                    End If
                End If
                If Me.bwTMDB.WorkerReportsProgress Then
                    Me.bwTMDB.ReportProgress(2)
                End If
            Catch exception2 As Exception
                ProjectData.SetProjectError(exception2)
                Dim exception As Exception = exception2
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
            http = Nothing
            Return list
        End Function

        Public Function GetTrailers(ByVal imdbID As String) As String
            Dim http As New HTTP
            Dim str2 As String = AdvancedSettings.GetSetting("UseTMDBTrailerPref", "en", "")
            If Me.bwTMDB.CancellationPending Then
                Return Nothing
            End If
            Try 
                Dim str3 As String = http.DownloadData(String.Format("http://api.themoviedb.org/2.1/Movie.imdbLookup/en/xml/{0}/tt{1}", "b1ecff8c76278262b27de1569f57f6bd", imdbID))
                http = Nothing
                If Not String.IsNullOrEmpty(str3) Then
                    Dim str As String
                    Dim document As XDocument
                    Try 
                        document = XDocument.Parse(str3)
                    Catch exception1 As Exception
                        ProjectData.SetProjectError(exception1)
                        str = String.Empty
                        ProjectData.ClearProjectError
                        Return str
                    End Try
                    If Me.bwTMDB.WorkerReportsProgress Then
                        Me.bwTMDB.ReportProgress(1)
                    End If
                    If Me.bwTMDB.CancellationPending Then
                        Return Nothing
                    End If
                    Dim source As IEnumerable(Of XElement) = document.Elements.Select(Of XElement, XElement)(New Func(Of XElement, XElement)(AddressOf Scraper._Lambda$__88))
                    If ((source.Count(Of XElement)() > 0) AndAlso (source.ElementAtOrDefault(Of XElement)(0).Value <> "Your query didn't return any results.")) Then
                        Dim num2 As Integer
                        Dim str4 As String = InternalXmlHelper.get_Value(document.Descendants(XName.Get("OpenSearchDescription", "")).Descendants(Of XElement)(XName.Get("movies", "")).Descendants(Of XElement)(XName.Get("movie", "")).Descendants(Of XElement)(XName.Get("id", "")))
                        Dim num As Integer = 0
                        Do
                            http = New HTTP
                            str3 = http.DownloadData(String.Format("http://api.themoviedb.org/2.1/Movie.getInfo/{0}/xml/{1}/{2}", str2, "b1ecff8c76278262b27de1569f57f6bd", str4))
                            http = Nothing
                            If Not String.IsNullOrEmpty(str3) Then
                                Try 
                                    document = XDocument.Parse(str3)
                                Catch exception2 As Exception
                                    ProjectData.SetProjectError(exception2)
                                    str = String.Empty
                                    ProjectData.ClearProjectError
                                    Return str
                                    ProjectData.ClearProjectError
                                End Try
                                If Me.bwTMDB.WorkerReportsProgress Then
                                    Me.bwTMDB.ReportProgress(2)
                                End If
                                If Me.bwTMDB.CancellationPending Then
                                    Return Nothing
                                End If
                                Dim enumerable2 As IEnumerable(Of IEnumerable(Of XElement)) = document.Descendants(XName.Get("OpenSearchDescription", "")).Descendants(Of XElement)(XName.Get("movies", "")).Descendants(Of XElement)(XName.Get("movie", "")).Select(Of XElement, IEnumerable(Of XElement))(New Func(Of XElement, IEnumerable(Of XElement))(AddressOf Scraper._Lambda$__89))
                                If ((enumerable2.Count(Of IEnumerable(Of XElement))() > 0) AndAlso Not String.IsNullOrEmpty(InternalXmlHelper.get_Value(enumerable2.ElementAtOrDefault(Of IEnumerable(Of XElement))(0)))) Then
                                    If (InternalXmlHelper.get_Value(enumerable2.ElementAtOrDefault(Of IEnumerable(Of XElement))(0)).ToLower.IndexOf("youtube.com") > 0) Then
                                        Return InternalXmlHelper.get_Value(enumerable2.ElementAtOrDefault(Of IEnumerable(Of XElement))(0))
                                        num += 1
                                    End If
                                Else
                                    str2 = "en"
                                End If
                            End If
                            num += 1
                            num2 = 1
                        Loop While (num <= num2)
                    End If
                End If
                If Me.bwTMDB.WorkerReportsProgress Then
                    Me.bwTMDB.ReportProgress(3)
                End If
            Catch exception3 As Exception
                ProjectData.SetProjectError(exception3)
                Dim exception As Exception = exception3
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
            Return String.Empty
        End Function


        ' Properties
        Friend Overridable Property bwTMDB As BackgroundWorker
            <DebuggerNonUserCode> _
            Get
                Return Me._bwTMDB
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As BackgroundWorker)
                Dim handler As ProgressChangedEventHandler = New ProgressChangedEventHandler(AddressOf Me.bwTMDB_ProgressChanged)
                Dim handler2 As DoWorkEventHandler = New DoWorkEventHandler(AddressOf Me.bwTMDB_DoWork)
                Dim handler3 As RunWorkerCompletedEventHandler = New RunWorkerCompletedEventHandler(AddressOf Me.bwTMDB_RunWorkerCompleted)
                If (Not Me._bwTMDB Is Nothing) Then
                    RemoveHandler Me._bwTMDB.ProgressChanged, handler
                    RemoveHandler Me._bwTMDB.DoWork, handler2
                    RemoveHandler Me._bwTMDB.RunWorkerCompleted, handler3
                End If
                Me._bwTMDB = WithEventsValue
                If (Not Me._bwTMDB Is Nothing) Then
                    AddHandler Me._bwTMDB.ProgressChanged, handler
                    AddHandler Me._bwTMDB.DoWork, handler2
                    AddHandler Me._bwTMDB.RunWorkerCompleted, handler3
                End If
            End Set
        End Property


        ' Fields
        Private Shared __ENCList As List(Of WeakReference) = New List(Of WeakReference)
        <AccessedThroughProperty("bwTMDB")> _
        Private _bwTMDB As BackgroundWorker
        Private Const APIKey As String = "b1ecff8c76278262b27de1569f57f6bd"
        Public IMDBURL As String

        ' Nested Types
        <StructLayout(LayoutKind.Sequential)> _
        Private Structure Arguments
            Public Parameter As String
            Public sType As String
        End Structure

        Public Delegate Sub PostersDownloadedEventHandler(ByVal Posters As List(Of Image))

        Public Delegate Sub ProgressUpdatedEventHandler(ByVal iPercent As Integer)
    End Class
End Namespace

