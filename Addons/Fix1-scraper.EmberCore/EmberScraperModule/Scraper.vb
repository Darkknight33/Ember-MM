Imports EmberAPI
Imports EmberAPI.MediaContainers
Imports ICSharpCode.SharpZipLib.Zip
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data.SQLite
Imports System.Diagnostics
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Text
Imports System.Threading
Imports System.Windows.Forms
Imports System.Xml.Linq

Namespace EmberScraperModule
    Public Class Scraper
        ' Events
        Public Custom Event ScraperEvent As ScraperEventEventHandler

        ' Methods
        Shared Sub New()
            Scraper.sObject = New ScraperObject
            Scraper.tEpisodes = New List(Of EpisodeDetails)
            Scraper.tmpTVDBShow = New TVDBShow
            Scraper.TVDBImages = New TVImages
        End Sub

        Public Sub New()
            Scraper.__ENCAddToList(Me)
            AddHandler Scraper.sObject.ScraperEvent, New ScraperEventEventHandler(AddressOf Me.InnerEvent)
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
        Private Shared Function _Lambda$__96(ByVal xLanguages As XElement) As XElement
            Return xLanguages
        End Function

        Public Sub CancelAsync()
            Scraper.sObject.CancelAsync
        End Sub

        Public Function ChangeEpisode(ByVal ShowID As Integer, ByVal TVDBID As String, ByVal Lang As String) As EpisodeDetails
            Dim sInfo As New ScrapeInfo With { _
                .ShowID = ShowID, _
                .TVDBID = TVDBID, _
                .SelectedLang = Lang, _
                .iSeason = -999 _
            }
            Return Scraper.sObject.ChangeEpisode(sInfo)
        End Function

        Public Function GetLangs(ByVal sMirror As String) As List(Of TVLanguage)
            Dim list2 As New List(Of TVLanguage)
            Dim str As String = New HTTP().DownloadData(String.Format("http://{0}/api/{1}/languages.xml", sMirror, "7B090234F418D074"))
            If Not String.IsNullOrEmpty(str) Then
                Dim document As XDocument
                Try 
                    document = XDocument.Parse(str)
                Catch exception1 As Exception
                    ProjectData.SetProjectError(exception1)
                    Dim list As List(Of TVLanguage) = list2
                    ProjectData.ClearProjectError
                    Return list
                End Try
                Dim enumerable As IEnumerable(Of XElement) = document.Descendants("Language").Select(Of XElement, XElement)(New Func(Of XElement, XElement)(AddressOf Scraper._Lambda$__96))
                Dim element As XElement
                For Each element In enumerable
                    Dim item As New TVLanguage With { _
                        .LongLang = element.Element("name").Value, _
                        .ShortLang = element.Element("abbreviation").Value _
                    }
                    list2.Add(item)
                Next
            End If
            Return list2
        End Function

        Public Function GetSingleEpisode(ByVal ShowID As Integer, ByVal TVDBID As String, ByVal Season As Integer, ByVal Episode As Integer, ByVal Lang As String, ByVal Ordering As Ordering, ByVal Options As TVScrapeOptions) As EpisodeDetails
            Dim sInfo As New ScrapeInfo With { _
                .ShowID = ShowID, _
                .TVDBID = TVDBID, _
                .iSeason = Season, _
                .iEpisode = Episode, _
                .SelectedLang = Lang, _
                .Ordering = Ordering, _
                .Options = Options _
            }
            Return Scraper.sObject.GetSingleEpisode(sInfo)
        End Function

        Public Function GetSingleImage(ByVal Title As String, ByVal ShowID As Integer, ByVal TVDBID As String, ByVal Type As TVImageType, ByVal Season As Integer, ByVal Episode As Integer, ByVal Lang As String, ByVal Ordering As Ordering, ByVal CurrentImage As Image) As Image
            Dim sInfo As New ScrapeInfo With { _
                .ShowTitle = Title, _
                .ShowID = ShowID, _
                .TVDBID = TVDBID, _
                .ImageType = Type, _
                .iSeason = Season, _
                .iEpisode = Episode, _
                .SelectedLang = Lang, _
                .Ordering = Ordering, _
                .CurrentImage = CurrentImage _
            }
            Return Scraper.sObject.GetSingleImage(sInfo)
        End Function

        Public Sub InnerEvent(ByVal eType As TVScraperEventType, ByVal iProgress As Integer, ByVal Parameter As Object)
            Dim scraperEventEvent As ScraperEventEventHandler = Me.ScraperEventEvent
            If (Not scraperEventEvent Is Nothing) Then
                scraperEventEvent.Invoke(eType, iProgress, RuntimeHelpers.GetObjectValue(Parameter))
            End If
        End Sub

        Public Function IsBusy() As Boolean
            Return Scraper.sObject.IsBusy
        End Function

        Public Sub SaveImages()
            Scraper.sObject.SaveImages
        End Sub

        Public Sub ScrapeEpisode(ByVal ShowID As Integer, ByVal ShowTitle As String, ByVal TVDBID As String, ByVal iEpisode As Integer, ByVal iSeason As Integer, ByVal Lang As String, ByVal Ordering As Ordering, ByVal Options As TVScrapeOptions)
            Dim sInfo As New ScrapeInfo With { _
                .ShowID = ShowID, _
                .ShowTitle = ShowTitle, _
                .TVDBID = TVDBID, _
                .iEpisode = iEpisode, _
                .iSeason = iSeason, _
                .SelectedLang = Lang, _
                .Ordering = Ordering, _
                .Options = Options _
            }
            Scraper.sObject.ScrapeEpisode(sInfo)
        End Sub

        Public Sub ScrapeSeason(ByVal ShowID As Integer, ByVal ShowTitle As String, ByVal TVDBID As String, ByVal iSeason As Integer, ByVal Lang As String, ByVal Ordering As Ordering, ByVal Options As TVScrapeOptions)
            Dim sInfo As New ScrapeInfo With { _
                .ShowID = ShowID, _
                .ShowTitle = ShowTitle, _
                .TVDBID = TVDBID, _
                .iSeason = iSeason, _
                .SelectedLang = Lang, _
                .Ordering = Ordering, _
                .Options = Options _
            }
            Scraper.sObject.ScrapeSeason(sInfo)
        End Sub

        Public Sub SingleScrape(ByVal ShowID As Integer, ByVal ShowTitle As String, ByVal TVDBID As String, ByVal Lang As String, ByVal Ordering As Ordering, ByVal Options As TVScrapeOptions, ByVal ScrapeType As ScrapeType, ByVal WithCurrent As Boolean)
            Dim sInfo As New ScrapeInfo With { _
                .ShowID = ShowID, _
                .ShowTitle = ShowTitle, _
                .TVDBID = TVDBID, _
                .SelectedLang = Lang, _
                .Ordering = Ordering, _
                .Options = Options, _
                .ScrapeType = ScrapeType, _
                .WithCurrent = WithCurrent, _
                .iSeason = -999 _
            }
            Scraper.sObject.SingleScrape(sInfo)
        End Sub


        ' Properties
        Public Shared Property sObject As ScraperObject
            <DebuggerNonUserCode> _
            Get
                Return Scraper._sObject
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As ScraperObject)
                Scraper._sObject = WithEventsValue
            End Set
        End Property


        ' Fields
        Private Shared __ENCList As List(Of WeakReference) = New List(Of WeakReference)
        <AccessedThroughProperty("sObject")> _
        Private Shared _sObject As ScraperObject
        Public Const APIKey As String = "7B090234F418D074"
        Public Shared tEpisodes As List(Of EpisodeDetails)
        Public Shared tmpTVDBShow As TVDBShow
        Public Shared TVDBImages As TVImages

        ' Nested Types
        Public Delegate Sub ScraperEventEventHandler(ByVal eType As TVScraperEventType, ByVal iProgress As Integer, ByVal Parameter As Object)

        Public Class ScraperObject
            ' Events
            Public Custom Event ScraperEvent As ScraperEventEventHandler

            ' Methods
            Public Sub New()
                ScraperObject.__ENCAddToList(Me)
                Me.bwTVDB = New BackgroundWorker
                Me.aXML = String.Empty
                Me.bXML = String.Empty
                Me.sXML = String.Empty
            End Sub

            <DebuggerNonUserCode> _
            Private Shared Sub __ENCAddToList(ByVal value As Object)
                Dim list As List(Of WeakReference) = ScraperObject.__ENCList
                SyncLock list
                    If (ScraperObject.__ENCList.Count = ScraperObject.__ENCList.Capacity) Then
                        Dim index As Integer = 0
                        Dim num3 As Integer = (ScraperObject.__ENCList.Count - 1)
                        Dim i As Integer = 0
                        Do While (i <= num3)
                            Dim reference As WeakReference = ScraperObject.__ENCList.Item(i)
                            If reference.IsAlive Then
                                If (i <> index) Then
                                    ScraperObject.__ENCList.Item(index) = ScraperObject.__ENCList.Item(i)
                                End If
                                index += 1
                            End If
                            i += 1
                        Loop
                        ScraperObject.__ENCList.RemoveRange(index, (ScraperObject.__ENCList.Count - index))
                        ScraperObject.__ENCList.Capacity = ScraperObject.__ENCList.Count
                    End If
                    ScraperObject.__ENCList.Add(New WeakReference(RuntimeHelpers.GetObjectValue(value)))
                End SyncLock
            End Sub

            <CompilerGenerated> _
            Private Shared Function _Lambda$__105(ByVal xSeries As XElement) As Boolean
                Return xSeries.HasElements
            End Function

            <CompilerGenerated> _
            Private Shared Function _Lambda$__106(ByVal s As XElement) As String
                Return s.Element("seriesid").Value.ToString
            End Function

            <CompilerGenerated> _
            Private Shared Function _Lambda$__107(ByVal group As IGrouping(Of String, XElement)) As String
                Return group.Key
            End Function

            <CompilerGenerated> _
            Private Shared Function _Lambda$__109(ByVal s As XElement) As Boolean
                Return s.HasElements
            End Function

            <CompilerGenerated> _
            Private Shared Function _Lambda$__113(ByVal xShow As XElement) As XElement
                Return xShow
            End Function

            <CompilerGenerated> _
            Private Shared Function _Lambda$__115(ByVal e As XElement) As Boolean
                Return ((Convert.ToInt32(e.Element("SeasonNumber").Value) > 0) AndAlso (Information.IsNothing(e.Element("absolute_number")) OrElse String.IsNullOrEmpty(e.Element("absolute_number").Value.ToString)))
            End Function

            <CompilerGenerated> _
            Private Shared Function _Lambda$__99(ByVal e As XElement) As Boolean
                Return ((Convert.ToInt32(e.Element("SeasonNumber").Value) > 0) AndAlso (Information.IsNothing(e.Element("absolute_number")) OrElse String.IsNullOrEmpty(e.Element("absolute_number").Value.ToString)))
            End Function

            Private Sub bwtvDB_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
                Dim argument As Arguments = DirectCast(e.Argument, Arguments)
                Try 
                    Dim results As Results
                    Select Case argument.Type
                        Case 0
                            Dim results2 As New Results With { _
                                .Type = 0, _
                                .Result = Me.SearchSeries(DirectCast(argument.Parameter, ScrapeInfo)) _
                            }
                            e.Result = results2
                            Return
                        Case 1
                            Me.DownloadSeries(DirectCast(argument.Parameter, ScrapeInfo), False)
                            results = New Results With { _
                                .Type = 1 _
                            }
                            e.Result = results
                            Return
                        Case 2
                            Dim parameter As ScrapeInfo = DirectCast(argument.Parameter, ScrapeInfo)
                            ScraperObject.LoadAllEpisodes(parameter.ShowID, &H3E7)
                            results = New Results With { _
                                .Type = 2, _
                                .Result = RuntimeHelpers.GetObjectValue(argument.Parameter) _
                            }
                            e.Result = results
                            Return
                        Case 3
                            Me.SaveAllTVInfo
                            results = New Results With { _
                                .Type = 3 _
                            }
                            e.Result = results
                            Return
                        Case 4
                            Dim info As ScrapeInfo = DirectCast(argument.Parameter, ScrapeInfo)
                            ScraperObject.LoadAllEpisodes(info.ShowID, info.iSeason)
                            results = New Results With { _
                                .Type = 2, _
                                .Result = RuntimeHelpers.GetObjectValue(argument.Parameter) _
                            }
                            e.Result = results
                            Return
                    End Select
                Catch exception1 As Exception
                    ProjectData.SetProjectError(exception1)
                    Dim exception As Exception = exception1
                    Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                    ProjectData.ClearProjectError
                End Try
            End Sub

            Private Sub bwTVDB_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
                Dim scraperEventEvent As ScraperEventEventHandler = Me.ScraperEventEvent
                If (Not scraperEventEvent Is Nothing) Then
                    scraperEventEvent.Invoke(TVScraperEventType.Progress, e.ProgressPercentage, e.UserState.ToString)
                End If
            End Sub

            Private Sub bwTVDB_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
                Dim result As Results = DirectCast(e.Result, Results)
                Try 
                    Dim scraperEventEvent As ScraperEventEventHandler
                    Select Case result.Type
                        Case 0
                            scraperEventEvent = Me.ScraperEventEvent
                            If (Not scraperEventEvent Is Nothing) Then
                                scraperEventEvent.Invoke(TVScraperEventType.SearchResultsDownloaded, 0, DirectCast(result.Result, List(Of TVSearchResults)))
                            End If
                            Return
                        Case 1
                            scraperEventEvent = Me.ScraperEventEvent
                            If (Not scraperEventEvent Is Nothing) Then
                                scraperEventEvent.Invoke(TVScraperEventType.ShowDownloaded, 0, Nothing)
                            End If
                            Return
                        Case 2
                            If e.Cancelled Then
                                Exit Select
                            End If
                            Me.StartSingleScraper(DirectCast(result.Result, ScrapeInfo))
                            Return
                        Case 3
                            scraperEventEvent = Me.ScraperEventEvent
                            If (Not scraperEventEvent Is Nothing) Then
                                scraperEventEvent.Invoke(TVScraperEventType.ScraperDone, 0, Nothing)
                            End If
                            Return
                        Case Else
                            Return
                    End Select
                    scraperEventEvent = Me.ScraperEventEvent
                    If (Not scraperEventEvent Is Nothing) Then
                        scraperEventEvent.Invoke(TVScraperEventType.ScraperDone, 0, Nothing)
                    End If
                Catch exception1 As Exception
                    ProjectData.SetProjectError(exception1)
                    Dim exception As Exception = exception1
                    Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                    ProjectData.ClearProjectError
                End Try
            End Sub

            Public Sub CancelAsync()
                If Me.bwTVDB.IsBusy Then
                    Me.bwTVDB.CancelAsync
                End If
            End Sub

            Public Function ChangeEpisode(ByVal sInfo As ScrapeInfo) As EpisodeDetails
                Try 
                    Dim listOfKnownEpisodes As List(Of EpisodeDetails) = Me.GetListOfKnownEpisodes(sInfo)
                    If (listOfKnownEpisodes.Count > 0) Then
                        Using ep As dlgTVChangeEp = New dlgTVChangeEp
                            Return ep.ShowDialog(listOfKnownEpisodes)
                        End Using
                    End If
                    Interaction.MsgBox(Master.eLang.GetString(&H4F, "There are no known episodes for this show. Scrape the show, season, or episode and try again.", False), MsgBoxStyle.ApplicationModal, Master.eLang.GetString(80, "No Known Episodes", False))
                Catch exception1 As Exception
                    ProjectData.SetProjectError(exception1)
                    Dim exception As Exception = exception1
                    Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                    ProjectData.ClearProjectError
                End Try
                Return Nothing
            End Function

            Private Function CreditsString(ByVal sGStars As String, ByVal sWriters As String) As String
                Dim list As New List(Of String)
                Dim str2 As String = Master.eLang.GetString(&H52, "Guest Star", False)
                Dim str3 As String = Master.eLang.GetString(&H309, "Writer", True)
                If Not String.IsNullOrEmpty(sGStars) Then
                    Dim str4 As String
                    For Each str4 In sGStars.Trim(New Char() { Convert.ToChar("|") }).Split(New Char() { Convert.ToChar("|") })
                        If Not String.IsNullOrEmpty(str4) Then
                            list.Add((str4 & String.Format(" ({0})", str2)))
                        End If
                    Next
                End If
                If Not String.IsNullOrEmpty(sWriters) Then
                    Dim str5 As String
                    For Each str5 In sWriters.Trim(New Char() { Convert.ToChar("|") }).Split(New Char() { Convert.ToChar("|") })
                        If Not String.IsNullOrEmpty(str5) Then
                            list.Add((str5 & String.Format(" ({0})", str3)))
                        End If
                    Next
                End If
                Return Strings.Join(list.ToArray, " / ")
            End Function

            Public Sub DownloadSeries(ByVal sInfo As ScrapeInfo, ByVal Optional ImagesOnly As Boolean = False)
                Try 
                    Dim path As String = Path.Combine(Master.TempPath, String.Concat(New String() { "Shows", Conversions.ToString(Path.DirectorySeparatorChar), sInfo.TVDBID, Conversions.ToString(Path.DirectorySeparatorChar), sInfo.SelectedLang, ".zip" }))
                    Dim flag2 As Boolean = File.Exists(path)
                    Dim flag As Boolean = False
                    Select Case Master.eSettings.TVUpdateTime
                        Case TVUpdateTime.Week
                            If (flag2 AndAlso (DateTime.Compare(File.GetCreationTime(path).AddDays(7), DateAndTime.Now) < 0)) Then
                                flag = True
                            End If
                            Exit Select
                        Case TVUpdateTime.BiWeekly
                            If (flag2 AndAlso (DateTime.Compare(File.GetCreationTime(path).AddDays(14), DateAndTime.Now) < 0)) Then
                                flag = True
                            End If
                            Exit Select
                        Case TVUpdateTime.Month
                            If (flag2 AndAlso (DateTime.Compare(File.GetCreationTime(path).AddMonths(1), DateAndTime.Now) < 0)) Then
                                flag = True
                            End If
                            Exit Select
                        Case TVUpdateTime.Never
                            flag = False
                            Exit Select
                        Case TVUpdateTime.Always
                            flag = True
                            Exit Select
                    End Select
                    If (flag OrElse Not flag2) Then
                        Dim http As New HTTP
                        Dim expression As Byte() = http.DownloadZip(String.Format("http://{0}/api/{1}/series/{2}/all/{3}.zip", New Object() { Master.eSettings.TVDBMirror, "7B090234F418D074", sInfo.TVDBID, sInfo.SelectedLang }))
                        http = Nothing
                        If (Not Information.IsNothing(expression) AndAlso (expression.Length > 0)) Then
                            Directory.CreateDirectory(Directory.GetParent(path).FullName)
                            Using stream As FileStream = New FileStream(path, FileMode.Create, FileAccess.Write)
                                stream.Write(expression, 0, expression.Length)
                            End Using
                            Me.ProcessTVDBZip(expression, sInfo)
                            Me.ShowFromXML(sInfo, ImagesOnly)
                        End If
                    Else
                        Using stream2 As FileStream = New FileStream(path, FileMode.Open, FileAccess.Read)
                            Dim tvZip As Byte() = Functions.ReadStreamToEnd(stream2)
                            Me.ProcessTVDBZip(tvZip, sInfo)
                            Me.ShowFromXML(sInfo, ImagesOnly)
                        End Using
                    End If
                Catch exception1 As Exception
                    ProjectData.SetProjectError(exception1)
                    Dim exception As Exception = exception1
                    Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                    ProjectData.ClearProjectError
                End Try
            End Sub

            Public Sub DownloadSeriesAsync(ByVal sInfo As ScrapeInfo)
                Try 
                    If Not Me.bwTVDB.IsBusy Then
                        Dim scraperEventEvent As ScraperEventEventHandler = Me.ScraperEventEvent
                        If (Not scraperEventEvent Is Nothing) Then
                            scraperEventEvent.Invoke(TVScraperEventType.StartingDownload, 0, Nothing)
                        End If
                        Me.bwTVDB.WorkerReportsProgress = True
                        Me.bwTVDB.WorkerSupportsCancellation = True
                        Dim argument As New Arguments With { _
                            .Type = 1, _
                            .Parameter = sInfo _
                        }
                        Me.bwTVDB.RunWorkerAsync(argument)
                    End If
                Catch exception1 As Exception
                    ProjectData.SetProjectError(exception1)
                    Dim exception As Exception = exception1
                    Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                    ProjectData.ClearProjectError
                End Try
            End Sub

            Public Function GetListOfKnownEpisodes(ByVal sInfo As ScrapeInfo) As List(Of EpisodeDetails)
                Dim e$__ As New _Closure$__13
                Dim list As New List(Of Person)
                Dim list3 As New List(Of EpisodeDetails)
                Dim item As New EpisodeDetails
                Dim path As String = Path.Combine(Master.TempPath, String.Concat(New String() { "Shows", Conversions.ToString(Path.DirectorySeparatorChar), sInfo.TVDBID, Conversions.ToString(Path.DirectorySeparatorChar), sInfo.SelectedLang, ".zip" }))
                e$__.$VB$Local_tSeas = -1
                Dim standard As Ordering = Ordering.Standard
                Try 
                    If Not File.Exists(path) Then
                        Return list3
                    End If
                    Using stream As FileStream = New FileStream(path, FileMode.Open, FileAccess.Read)
                        Dim tvZip As Byte() = Functions.ReadStreamToEnd(stream)
                        Me.ProcessTVDBZip(tvZip, sInfo)
                        Try 
                            If Not String.IsNullOrEmpty(Me.aXML) Then
                                Dim enumerator As IEnumerator(Of XElement)
                                Dim document As XDocument = XDocument.Parse(Me.aXML)
                                Try 
                                    enumerator = document.Descendants("Actor").GetEnumerator
                                    Do While enumerator.MoveNext
                                        Dim current As XElement = enumerator.Current
                                        If (Not Information.IsNothing(current.Element("Name")) AndAlso Not String.IsNullOrEmpty(current.Element("Name").Value)) Then
                                            Dim person As New Person With { _
                                                .Name = current.Element("Name").Value, _
                                                .Role = If(Information.IsNothing(current.Element("Role")), String.Empty, current.Element("Role").Value), _
                                                .Thumb = If((Information.IsNothing(current.Element("Image")) OrElse String.IsNullOrEmpty(current.Element("Image").Value)), String.Empty, String.Format("http://{0}/banners/{1}", Master.eSettings.TVDBMirror, current.Element("Image").Value)) _
                                            }
                                            list.Add(person)
                                        End If
                                    Loop
                                Finally
                                    If (Not enumerator Is Nothing) Then
                                        enumerator.Dispose
                                    End If
                                End Try
                            End If
                        Catch exception1 As Exception
                            ProjectData.SetProjectError(exception1)
                            Dim exception As Exception = exception1
                            Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                            ProjectData.ClearProjectError
                        End Try
                        If Not String.IsNullOrEmpty(Me.sXML) Then
                            Dim document2 As XDocument = XDocument.Parse(Me.sXML)
                            Dim element2 As XElement
                            For Each element2 In document2.Descendants("Episode")
                                If (Not Information.IsNothing(element2.Element("EpisodeName").Value) AndAlso Not String.IsNullOrEmpty(element2.Element("EpisodeName").Value)) Then
                                    item = New EpisodeDetails
                                    standard = Ordering.Standard
                                    If (sInfo.Ordering = Ordering.DVD) Then
                                        If (((Not Information.IsNothing(element2.Element("SeasonNumber")) AndAlso Not String.IsNullOrEmpty(element2.Element("SeasonNumber").Value.ToString)) AndAlso (Not Information.IsNothing(element2.Element("DVD_season")) AndAlso Not String.IsNullOrEmpty(element2.Element("DVD_season").Value.ToString))) AndAlso (Not Information.IsNothing(element2.Element("DVD_episodenumber")) AndAlso Not String.IsNullOrEmpty(element2.Element("DVD_episodenumber").Value.ToString))) Then
                                            e$__.$VB$Local_tSeas = Convert.ToInt32(element2.Element("SeasonNumber").Value)
                                            If ((sInfo.iSeason >= 0) AndAlso (e$__.$VB$Local_tSeas <> sInfo.iSeason)) Then
                                                Continue For
                                            End If
                                            If (document2.Descendants("Episode").Where(Of XElement)(New Func(Of XElement, Boolean)(AddressOf e$__._Lambda$__97)).Count(Of XElement)() = 0) Then
                                                standard = Ordering.DVD
                                            End If
                                        ElseIf ((Not Information.IsNothing(element2.Element("DVD_season")) AndAlso Not String.IsNullOrEmpty(element2.Element("DVD_season").Value.ToString)) AndAlso (Not Information.IsNothing(element2.Element("DVD_episodenumber")) AndAlso Not String.IsNullOrEmpty(element2.Element("DVD_episodenumber").Value.ToString))) Then
                                            e$__.$VB$Local_tSeas = Convert.ToInt32(element2.Element("DVD_season").Value)
                                            If (document2.Descendants("Episode").Where(Of XElement)(New Func(Of XElement, Boolean)(AddressOf e$__._Lambda$__98)).Count(Of XElement)() = 0) Then
                                                standard = Ordering.DVD
                                            End If
                                        End If
                                    ElseIf (sInfo.Ordering = Ordering.Absolute) Then
                                        If ((Not Information.IsNothing(element2.Element("absolute_number")) AndAlso Not String.IsNullOrEmpty(element2.Element("absolute_number").Value.ToString)) AndAlso (document2.Descendants("Episode").Where(Of XElement)(New Func(Of XElement, Boolean)(AddressOf ScraperObject._Lambda$__99)).Count(Of XElement)() = 0)) Then
                                            standard = Ordering.Absolute
                                        End If
                                    ElseIf ((sInfo.iSeason >= 0) AndAlso (Convert.ToInt32(element2.Element("SeasonNumber").Value) <> sInfo.iSeason)) Then
                                        Continue For
                                    End If
                                    Dim details2 As EpisodeDetails = item
                                    details2.Title = element2.Element("EpisodeName").Value
                                    If (standard = Ordering.DVD) Then
                                        details2.Season = Convert.ToInt32(element2.Element("DVD_season").Value)
                                        details2.Episode = Convert.ToInt32(Conversions.ToLong(element2.Element("DVD_episodenumber").Value))
                                    ElseIf (standard = Ordering.Absolute) Then
                                        details2.Season = 1
                                        details2.Episode = Convert.ToInt32(element2.Element("absolute_number").Value)
                                    Else
                                        details2.Season = If((Information.IsNothing(element2.Element("SeasonNumber")) OrElse String.IsNullOrEmpty(element2.Element("SeasonNumber").Value)), 0, Convert.ToInt32(element2.Element("SeasonNumber").Value))
                                        details2.Episode = If((Information.IsNothing(element2.Element("EpisodeNumber")) OrElse String.IsNullOrEmpty(element2.Element("EpisodeNumber").Value)), 0, Convert.ToInt32(element2.Element("EpisodeNumber").Value))
                                    End If
                                    details2.Aired = If(Information.IsNothing(element2.Element("FirstAired")), String.Empty, element2.Element("FirstAired").Value)
                                    details2.Rating = If(Information.IsNothing(element2.Element("Rating")), String.Empty, element2.Element("Rating").Value)
                                    details2.Plot = If(Information.IsNothing(element2.Element("Overview")), String.Empty, element2.Element("Overview").Value.ToString.Replace(ChrW(13) & ChrW(10), ChrW(10)).Replace(ChrW(10), ChrW(13) & ChrW(10)))
                                    details2.Director = If(Information.IsNothing(element2.Element("Director")), String.Empty, Strings.Join(element2.Element("Director").Value.Trim(New Char() { Convert.ToChar("|") }).Split(New Char() { Convert.ToChar("|") }), " / "))
                                    details2.Credits = Me.CreditsString(If(Information.IsNothing(element2.Element("GuestStars")), String.Empty, element2.Element("GuestStars").Value), If(Information.IsNothing(element2.Element("Writer")), String.Empty, element2.Element("Writer").Value))
                                    details2.Actors = list
                                    details2.PosterURL = If(Information.IsNothing(element2.Element("filename")), String.Empty, String.Format("http://{0}/banners/{1}", Master.eSettings.TVDBMirror, element2.Element("filename").Value))
                                    details2.LocalFile = If(Information.IsNothing(element2.Element("filename")), String.Empty, Path.Combine(Master.TempPath, String.Concat(New String() { "Shows", Conversions.ToString(Path.DirectorySeparatorChar), sInfo.TVDBID, Conversions.ToString(Path.DirectorySeparatorChar), "episodeposters", Conversions.ToString(Path.DirectorySeparatorChar), element2.Element("filename").Value.Replace(Convert.ToChar("/"), Path.DirectorySeparatorChar) })))
                                    details2 = Nothing
                                    list3.Add(item)
                                End If
                            Next
                        End If
                        Return list3
                    End Using
                Catch exception3 As Exception
                    ProjectData.SetProjectError(exception3)
                    Dim exception2 As Exception = exception3
                    Master.eLog.WriteToErrorLog(exception2.Message, exception2.StackTrace, "Error", True)
                    ProjectData.ClearProjectError
                End Try
                Return list3
            End Function

            Public Sub GetSearchResultsAsync(ByVal sInfo As ScrapeInfo)
                Try 
                    If Not Me.bwTVDB.IsBusy Then
                        Me.bwTVDB.WorkerReportsProgress = True
                        Me.bwTVDB.WorkerSupportsCancellation = True
                        Dim argument As New Arguments With { _
                            .Type = 0, _
                            .Parameter = sInfo _
                        }
                        Me.bwTVDB.RunWorkerAsync(argument)
                    End If
                Catch exception1 As Exception
                    ProjectData.SetProjectError(exception1)
                    Dim exception As Exception = exception1
                    Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                    ProjectData.ClearProjectError
                End Try
            End Sub

            Public Function GetSingleEpisode(ByVal sInfo As ScrapeInfo) As EpisodeDetails
                Dim e$__ As New _Closure$__14 With { _
                    .$VB$Local_sInfo = sInfo _
                }
                Dim expression As New EpisodeDetails
                Try 
                    expression = Me.GetListOfKnownEpisodes(e$__.$VB$Local_sInfo).FirstOrDefault(Of EpisodeDetails)(New Func(Of EpisodeDetails, Boolean)(AddressOf e$__._Lambda$__100))
                    If Not Information.IsNothing(expression) Then
                        Return expression
                    End If
                Catch exception1 As Exception
                    ProjectData.SetProjectError(exception1)
                    Dim exception As Exception = exception1
                    Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                    ProjectData.ClearProjectError
                End Try
                Return New EpisodeDetails
            End Function

            Public Function GetSingleImage(ByVal sInfo As ScrapeInfo) As Image
                Dim e$__ As New _Closure$__15 With { _
                    .$VB$Local_sInfo = sInfo _
                }
                Scraper.tmpTVDBShow = New TVDBShow
                If (e$__.$VB$Local_sInfo.ImageType = TVImageType.EpisodePoster) Then
                    If String.IsNullOrEmpty(e$__.$VB$Local_sInfo.TVDBID) Then
                        Using results As dlgTVDBSearchResults = New dlgTVDBSearchResults
                            e$__.$VB$Local_sInfo = results.ShowDialog(e$__.$VB$Local_sInfo, True)
                            If Not String.IsNullOrEmpty(e$__.$VB$Local_sInfo.TVDBID) Then
                                Master.currShow.TVShow.ID = e$__.$VB$Local_sInfo.TVDBID
                                Using images As Images = New Images
                                    Dim expression As EpisodeDetails = Me.GetListOfKnownEpisodes(e$__.$VB$Local_sInfo).FirstOrDefault(Of EpisodeDetails)(New Func(Of EpisodeDetails, Boolean)(AddressOf e$__._Lambda$__101))
                                    If Not Information.IsNothing(expression) Then
                                        If File.Exists(expression.LocalFile) Then
                                            images.FromFile(expression.LocalFile)
                                        Else
                                            images.FromWeb(expression.PosterURL)
                                            If Not Information.IsNothing(images.Image) Then
                                                Directory.CreateDirectory(Directory.GetParent(expression.LocalFile).FullName)
                                                images.Save(expression.LocalFile, 0)
                                            End If
                                        End If
                                        If Not Information.IsNothing(images.Image) Then
                                            Using poster As dlgTVEpisodePoster = New dlgTVEpisodePoster
                                                If (poster.ShowDialog(images.Image) = DialogResult.OK) Then
                                                    Return images.Image
                                                End If
                                                Return Nothing
                                            End Using
                                        End If
                                        Interaction.MsgBox(Master.eLang.GetString(&H51, "There is no poster available for this episode.", False), MsgBoxStyle.ApplicationModal, Master.eLang.GetString(&H1F, "No Posters Found", False))
                                    End If
                                    Return Nothing
                                End Using
                            End If
                            Return Nothing
                        End Using
                    End If
                    Using images2 As Images = New Images
                        Dim details2 As EpisodeDetails = Me.GetListOfKnownEpisodes(e$__.$VB$Local_sInfo).FirstOrDefault(Of EpisodeDetails)(New Func(Of EpisodeDetails, Boolean)(AddressOf e$__._Lambda$__102))
                        If Not Information.IsNothing(details2) Then
                            If File.Exists(details2.LocalFile) Then
                                images2.FromFile(details2.LocalFile)
                            Else
                                images2.FromWeb(details2.PosterURL)
                                If Not Information.IsNothing(images2.Image) Then
                                    Directory.CreateDirectory(Directory.GetParent(details2.LocalFile).FullName)
                                    images2.Save(details2.LocalFile, 0)
                                End If
                            End If
                            If Not Information.IsNothing(images2.Image) Then
                                Using poster2 As dlgTVEpisodePoster = New dlgTVEpisodePoster
                                    If (poster2.ShowDialog(images2.Image) = DialogResult.OK) Then
                                        Return images2.Image
                                    End If
                                    Return Nothing
                                End Using
                            End If
                            Interaction.MsgBox(Master.eLang.GetString(&H51, "There is no poster available for this episode.", False), MsgBoxStyle.ApplicationModal, Master.eLang.GetString(&H1F, "No Posters Found", False))
                        End If
                        Return Nothing
                    End Using
                End If
                If String.IsNullOrEmpty(e$__.$VB$Local_sInfo.TVDBID) Then
                    Using results2 As dlgTVDBSearchResults = New dlgTVDBSearchResults
                        e$__.$VB$Local_sInfo = results2.ShowDialog(e$__.$VB$Local_sInfo, True)
                        If Not String.IsNullOrEmpty(e$__.$VB$Local_sInfo.TVDBID) Then
                            Master.currShow.TVShow.ID = e$__.$VB$Local_sInfo.TVDBID
                            Me.DownloadSeries(e$__.$VB$Local_sInfo, True)
                            Using select As dlgTVImageSelect = New dlgTVImageSelect
                                Return [select].ShowDialog(e$__.$VB$Local_sInfo.ShowID, e$__.$VB$Local_sInfo.ImageType, e$__.$VB$Local_sInfo.iSeason, e$__.$VB$Local_sInfo.CurrentImage)
                            End Using
                        End If
                        Return Nothing
                    End Using
                End If
                Me.DownloadSeries(e$__.$VB$Local_sInfo, True)
                Using select2 As dlgTVImageSelect = New dlgTVImageSelect
                    Return select2.ShowDialog(e$__.$VB$Local_sInfo.ShowID, e$__.$VB$Local_sInfo.ImageType, e$__.$VB$Local_sInfo.iSeason, e$__.$VB$Local_sInfo.CurrentImage)
                End Using
            End Function

            Public Function IsBusy() As Boolean
                Return Me.bwTVDB.IsBusy
            End Function

            Public Shared Sub LoadAllEpisodes(ByVal _ID As Integer, ByVal OnlySeason As Integer)
                Try 
                    Scraper.tmpTVDBShow = New TVDBShow
                    Scraper.tmpTVDBShow.Show = Master.DB.LoadTVFullShowFromDB(CLng(_ID))
                    Scraper.tmpTVDBShow.AllSeason = Master.DB.LoadTVAllSeasonFromDB(CLng(_ID), False)
                    Using command As SQLiteCommand = Master.DB.MediaDBConn.CreateCommand
                        If (OnlySeason = &H3E7) Then
                            command.CommandText = ("SELECT COUNT(ID) AS eCount FROM TVEps WHERE TVShowID = " & _ID & " AND Missing = 0;")
                        Else
                            command.CommandText = String.Concat(New Object() { "SELECT COUNT(ID) AS eCount FROM TVEps WHERE TVShowID = ", _ID, " AND Season = ", OnlySeason, " AND Missing = 0;" })
                        End If
                        Using reader As SQLiteDataReader = command.ExecuteReader
                            If (Convert.ToInt32(RuntimeHelpers.GetObjectValue(reader.Item("eCount"))) > 0) Then
                                Using command2 As SQLiteCommand = Master.DB.MediaDBConn.CreateCommand
                                    If (OnlySeason = &H3E7) Then
                                        command2.CommandText = ("SELECT ID, Lock FROM TVEps WHERE TVShowID = " & _ID & " AND Missing = 0;")
                                    Else
                                        command2.CommandText = String.Concat(New Object() { "SELECT ID, Lock FROM TVEps WHERE TVShowID = ", _ID, " AND Season = ", OnlySeason, " AND Missing = 0;" })
                                    End If
                                    Using reader2 As SQLiteDataReader = command2.ExecuteReader
                                        Do While reader2.Read
                                            Scraper.tmpTVDBShow.Episodes.Add(Master.DB.LoadTVEpFromDB(Convert.ToInt64(RuntimeHelpers.GetObjectValue(reader2.Item("ID"))), True))
                                        Loop
                                    End Using
                                End Using
                            End If
                        End Using
                    End Using
                Catch exception1 As Exception
                    ProjectData.SetProjectError(exception1)
                    Dim exception As Exception = exception1
                    Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                    ProjectData.ClearProjectError
                End Try
            End Sub

            Public Sub PassEvent(ByVal eType As TVScraperEventType, ByVal iProgress As Integer, ByVal Parameter As Object)
                Dim scraperEventEvent As ScraperEventEventHandler = Me.ScraperEventEvent
                If (Not scraperEventEvent Is Nothing) Then
                    scraperEventEvent.Invoke(eType, iProgress, RuntimeHelpers.GetObjectValue(Parameter))
                End If
            End Sub

            Public Sub ProcessTVDBZip(ByVal tvZip As Byte(), ByVal sInfo As ScrapeInfo)
                Me.sXML = String.Empty
                Me.bXML = String.Empty
                Me.aXML = String.Empty
                Try 
                    Using stream As ZipInputStream = New ZipInputStream(New MemoryStream(tvZip))
                        Dim entry As ZipEntry = stream.GetNextEntry
                        Do While Not Information.IsNothing(entry)
                            Dim bytes As Byte() = Functions.ReadStreamToEnd(stream)
                            Dim flag As Boolean = True
                            If (flag = entry.Name.Equals((sInfo.SelectedLang & ".xml"))) Then
                                Me.sXML = Encoding.UTF8.GetString(bytes)
                            ElseIf (flag = entry.Name.Equals("banners.xml")) Then
                                Me.bXML = Encoding.UTF8.GetString(bytes)
                            ElseIf (flag = entry.Name.Equals("actors.xml")) Then
                                Me.aXML = Encoding.UTF8.GetString(bytes)
                            End If
                            entry = stream.GetNextEntry
                        Loop
                    End Using
                Catch exception1 As Exception
                    ProjectData.SetProjectError(exception1)
                    Dim exception As Exception = exception1
                    Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                    ProjectData.ClearProjectError
                End Try
            End Sub

            Private Sub SaveAllTVInfo()
                Dim e$__ As New _Closure$__16 With { _
                    .$VB$Local_iEp = -1, _
                    .$VB$Local_iSea = -1 _
                }
                Dim percentProgress As Integer = 1
                Dim dbtv As New DBTV
                Dim expression As New EpisodeDetails
                Me.bwTVDB.ReportProgress(Scraper.tmpTVDBShow.Episodes.Count, "max")
                Using transaction As SQLiteTransaction = Master.DB.MediaDBConn.BeginTransaction
                    If Master.eSettings.DisplayMissingEpisodes Then
                        Using command As SQLiteCommand = Master.DB.MediaDBConn.CreateCommand
                            command.CommandText = ("DELETE FROM TVEps WHERE Missing = 1 AND TVShowID = " & Master.currShow.ShowID & ";")
                            command.ExecuteNonQuery
                        End Using
                    End If
                    Try 
                        Dim dbtv2 As DBTV
                        For Each dbtv2 In Scraper.tmpTVDBShow.Episodes
                            Try 
                                If Me.bwTVDB.CancellationPending Then
                                    Return
                                End If
                                dbtv2.ShowID = Master.currShow.ShowID
                                e$__.$VB$Local_iEp = dbtv2.TVEp.Episode
                                e$__.$VB$Local_iSea = dbtv2.TVEp.Season
                                If Master.eSettings.DisplayMissingEpisodes Then
                                    expression = Scraper.tEpisodes.FirstOrDefault(Of EpisodeDetails)(New Func(Of EpisodeDetails, Boolean)(AddressOf e$__._Lambda$__103))
                                    If Not Information.IsNothing(expression) Then
                                        Scraper.tEpisodes.Remove(expression)
                                    End If
                                    dbtv = dbtv2
                                End If
                                If Me.bwTVDB.CancellationPending Then
                                    Return
                                End If
                                If (((dbtv2.TVEp.Season > -1) AndAlso (dbtv2.TVEp.Episode > -1)) AndAlso Not dbtv2.IsLockEp) Then
                                    If Not Information.IsNothing(dbtv2.TVEp.Poster.Image) Then
                                        dbtv2.EpPosterPath = dbtv2.TVEp.Poster.SaveAsEpPoster(dbtv2)
                                    End If
                                    If Me.bwTVDB.CancellationPending Then
                                        Return
                                    End If
                                    If (Master.eSettings.EpisodeFanartEnabled AndAlso Not Information.IsNothing(dbtv2.TVEp.Fanart.Image)) Then
                                        dbtv2.EpFanartPath = dbtv2.TVEp.Fanart.SaveAsEpFanart(dbtv2)
                                    End If
                                    If Me.bwTVDB.CancellationPending Then
                                        Return
                                    End If
                                    Dim source As IEnumerable(Of TVDBSeasonImage) = Scraper.TVDBImages.SeasonImageList.Where(Of TVDBSeasonImage)(New Func(Of TVDBSeasonImage, Boolean)(AddressOf e$__._Lambda$__104)).Take(Of TVDBSeasonImage)(1)
                                    If (source.Count(Of TVDBSeasonImage)() > 0) Then
                                        If Not Information.IsNothing(source.ElementAtOrDefault(Of TVDBSeasonImage)(0).Poster.Image) Then
                                            dbtv2.SeasonPosterPath = source.ElementAtOrDefault(Of TVDBSeasonImage)(0).Poster.SaveAsSeasonPoster(dbtv2)
                                        End If
                                        If Me.bwTVDB.CancellationPending Then
                                            Return
                                        End If
                                        If Master.eSettings.SeasonFanartEnabled Then
                                            If (Not String.IsNullOrEmpty(source.ElementAtOrDefault(Of TVDBSeasonImage)(0).Fanart.LocalFile) AndAlso File.Exists(source.ElementAtOrDefault(Of TVDBSeasonImage)(0).Fanart.LocalFile)) Then
                                                source.ElementAtOrDefault(Of TVDBSeasonImage)(0).Fanart.Image.FromFile(source.ElementAtOrDefault(Of TVDBSeasonImage)(0).Fanart.LocalFile)
                                                dbtv2.SeasonFanartPath = source.ElementAtOrDefault(Of TVDBSeasonImage)(0).Fanart.Image.SaveAsSeasonFanart(dbtv2)
                                            ElseIf (Not String.IsNullOrEmpty(source.ElementAtOrDefault(Of TVDBSeasonImage)(0).Fanart.URL) AndAlso Not String.IsNullOrEmpty(source.ElementAtOrDefault(Of TVDBSeasonImage)(0).Fanart.LocalFile)) Then
                                                source.ElementAtOrDefault(Of TVDBSeasonImage)(0).Fanart.Image.Clear
                                                source.ElementAtOrDefault(Of TVDBSeasonImage)(0).Fanart.Image.FromWeb(source.ElementAtOrDefault(Of TVDBSeasonImage)(0).Fanart.URL)
                                                If Not Information.IsNothing(source.ElementAtOrDefault(Of TVDBSeasonImage)(0).Fanart.Image.Image) Then
                                                    Directory.CreateDirectory(Directory.GetParent(source.ElementAtOrDefault(Of TVDBSeasonImage)(0).Fanart.LocalFile).FullName)
                                                    source.ElementAtOrDefault(Of TVDBSeasonImage)(0).Fanart.Image.Save(source.ElementAtOrDefault(Of TVDBSeasonImage)(0).Fanart.LocalFile, 0)
                                                    dbtv2.SeasonFanartPath = source.ElementAtOrDefault(Of TVDBSeasonImage)(0).Fanart.Image.SaveAsSeasonFanart(dbtv2)
                                                End If
                                            End If
                                        End If
                                    End If
                                    If Me.bwTVDB.CancellationPending Then
                                        Return
                                    End If
                                    If Master.eSettings.ScanTVMediaInfo Then
                                        MediaInfo.UpdateTVMediaInfo(dbtv2)
                                    End If
                                    Master.DB.SaveTVEpToDB(dbtv2, False, True, True, True)
                                    If Me.bwTVDB.CancellationPending Then
                                        Return
                                    End If
                                End If
                                Me.bwTVDB.ReportProgress(percentProgress, "progress")
                                percentProgress += 1
                            Catch exception1 As Exception
                                ProjectData.SetProjectError(exception1)
                                Dim exception As Exception = exception1
                                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                                ProjectData.ClearProjectError
                            End Try
                        Next
                        If Master.eSettings.DisplayMissingEpisodes Then
                            dbtv.Filename = String.Empty
                            dbtv.EpFanartPath = String.Empty
                            dbtv.EpPosterPath = String.Empty
                            dbtv.EpNfoPath = String.Empty
                            dbtv.SeasonFanartPath = String.Empty
                            dbtv.SeasonPosterPath = String.Empty
                            dbtv.ShowFanartPath = String.Empty
                            dbtv.IsLockEp = False
                            dbtv.IsMarkEp = False
                            dbtv.EpID = -1
                            If (Scraper.tEpisodes.Count > 0) Then
                                Dim details2 As EpisodeDetails
                                For Each details2 In Scraper.tEpisodes
                                    dbtv.TVEp = details2
                                    Master.DB.SaveTVEpToDB(dbtv, True, True, True, False)
                                Next
                            End If
                        End If
                        If Not Me.bwTVDB.CancellationPending Then
                            transaction.Commit
                        End If
                    Catch exception3 As Exception
                        ProjectData.SetProjectError(exception3)
                        Dim exception2 As Exception = exception3
                        Master.eLog.WriteToErrorLog(exception2.Message, exception2.StackTrace, "Error", True)
                        ProjectData.ClearProjectError
                    End Try
                End Using
            End Sub

            Public Sub SaveImages()
                Dim scraperEventEvent As ScraperEventEventHandler = Me.ScraperEventEvent
                If (Not scraperEventEvent Is Nothing) Then
                    scraperEventEvent.Invoke(TVScraperEventType.SavingStarted, 0, Nothing)
                End If
                Me.bwTVDB = New BackgroundWorker
                Me.bwTVDB.WorkerReportsProgress = True
                Me.bwTVDB.WorkerSupportsCancellation = True
                Dim argument As New Arguments With { _
                    .Type = 3 _
                }
                Me.bwTVDB.RunWorkerAsync(argument)
            End Sub

            Private Sub SaveToCache(ByVal sID As String, ByVal sURL As String, ByVal sPath As String)
                Dim http As New HTTP
                Dim images As New Images
                http.StartDownloadImage(sURL)
                Do While http.IsDownloading
                    Application.DoEvents
                    Thread.Sleep(50)
                Loop
                images.Image = http.Image
                If Not Information.IsNothing(images.Image) Then
                    images.Save(Path.Combine(Master.TempPath, String.Concat(New String() { "Shows", Conversions.ToString(Path.DirectorySeparatorChar), sID, Conversions.ToString(Path.DirectorySeparatorChar), sPath.Replace(Convert.ToChar("/"), Path.DirectorySeparatorChar) })), 0)
                End If
                images = Nothing
                http = Nothing
            End Sub

            Public Sub ScrapeEpisode(ByVal sInfo As ScrapeInfo)
                Try 
                    Dim scraperEventEvent As ScraperEventEventHandler
                    Scraper.tmpTVDBShow = New TVDBShow
                    Scraper.tmpTVDBShow.Episodes.Add(Master.currShow)
                    If String.IsNullOrEmpty(sInfo.TVDBID) Then
                        scraperEventEvent = Me.ScraperEventEvent
                        If (Not scraperEventEvent Is Nothing) Then
                            scraperEventEvent.Invoke(TVScraperEventType.Searching, 0, Nothing)
                        End If
                        Using results As dlgTVDBSearchResults = New dlgTVDBSearchResults
                            If (results.ShowDialog(sInfo) = DialogResult.OK) Then
                                Master.currShow = Scraper.tmpTVDBShow.Episodes.Item(0)
                                If (Not String.IsNullOrEmpty(Master.currShow.TVEp.LocalFile) AndAlso File.Exists(Master.currShow.TVEp.LocalFile)) Then
                                    Master.currShow.TVEp.Poster.FromWeb(Master.currShow.TVEp.PosterURL)
                                    If Not Information.IsNothing(Master.currShow.TVEp.Poster.Image) Then
                                        Directory.CreateDirectory(Directory.GetParent(Master.currShow.TVEp.LocalFile).FullName)
                                        Master.currShow.TVEp.Poster.Save(Master.currShow.TVEp.LocalFile, 0)
                                    End If
                                End If
                                If Not String.IsNullOrEmpty(Master.currShow.TVEp.LocalFile) Then
                                    Master.currShow.EpPosterPath = Master.currShow.TVEp.LocalFile
                                End If
                                If String.IsNullOrEmpty(Master.currShow.EpFanartPath) Then
                                    Master.currShow.EpFanartPath = Master.currShow.ShowFanartPath
                                End If
                                If Master.eSettings.ScanTVMediaInfo Then
                                    MediaInfo.UpdateTVMediaInfo(Master.currShow)
                                End If
                                scraperEventEvent = Me.ScraperEventEvent
                                If (Not scraperEventEvent Is Nothing) Then
                                    scraperEventEvent.Invoke(TVScraperEventType.Verifying, 1, Nothing)
                                End If
                            Else
                                scraperEventEvent = Me.ScraperEventEvent
                                If (Not scraperEventEvent Is Nothing) Then
                                    scraperEventEvent.Invoke(TVScraperEventType.Cancelled, 0, Nothing)
                                End If
                            End If
                        End Using
                    Else
                        Me.DownloadSeries(sInfo, False)
                        Dim dbtv As DBTV = Scraper.tmpTVDBShow.Episodes.Item(0)
                        If (dbtv.TVShow.ID.Length > 0) Then
                            Master.currShow = Scraper.tmpTVDBShow.Episodes.Item(0)
                            If (Not String.IsNullOrEmpty(Master.currShow.TVEp.LocalFile) AndAlso Not File.Exists(Master.currShow.TVEp.LocalFile)) Then
                                Master.currShow.TVEp.Poster.FromWeb(Master.currShow.TVEp.PosterURL)
                                If Not Information.IsNothing(Master.currShow.TVEp.Poster.Image) Then
                                    Directory.CreateDirectory(Directory.GetParent(Master.currShow.TVEp.LocalFile).FullName)
                                    Master.currShow.TVEp.Poster.Save(Master.currShow.TVEp.LocalFile, 0)
                                End If
                            End If
                            If Not String.IsNullOrEmpty(Master.currShow.TVEp.LocalFile) Then
                                Master.currShow.EpPosterPath = Master.currShow.TVEp.LocalFile
                            End If
                            If String.IsNullOrEmpty(Master.currShow.EpFanartPath) Then
                                Master.currShow.EpFanartPath = Master.currShow.ShowFanartPath
                            End If
                            If Master.eSettings.ScanTVMediaInfo Then
                                MediaInfo.UpdateTVMediaInfo(Master.currShow)
                            End If
                            scraperEventEvent = Me.ScraperEventEvent
                            If (Not scraperEventEvent Is Nothing) Then
                                scraperEventEvent.Invoke(TVScraperEventType.Verifying, 1, Nothing)
                            End If
                        Else
                            scraperEventEvent = Me.ScraperEventEvent
                            If (Not scraperEventEvent Is Nothing) Then
                                scraperEventEvent.Invoke(TVScraperEventType.Searching, 0, Nothing)
                            End If
                            Using results2 As dlgTVDBSearchResults = New dlgTVDBSearchResults
                                If (results2.ShowDialog(sInfo) = DialogResult.OK) Then
                                    Master.currShow = Scraper.tmpTVDBShow.Episodes.Item(0)
                                    If (Not String.IsNullOrEmpty(Master.currShow.TVEp.LocalFile) AndAlso Not File.Exists(Master.currShow.TVEp.LocalFile)) Then
                                        Master.currShow.TVEp.Poster.FromWeb(Master.currShow.TVEp.PosterURL)
                                        If Not Information.IsNothing(Master.currShow.TVEp.Poster) Then
                                            Directory.CreateDirectory(Directory.GetParent(Master.currShow.TVEp.LocalFile).FullName)
                                            Master.currShow.TVEp.Poster.Save(Master.currShow.TVEp.LocalFile, 0)
                                        End If
                                    End If
                                    If Not String.IsNullOrEmpty(Master.currShow.TVEp.LocalFile) Then
                                        Master.currShow.EpPosterPath = Master.currShow.TVEp.LocalFile
                                    End If
                                    If String.IsNullOrEmpty(Master.currShow.EpFanartPath) Then
                                        Master.currShow.EpFanartPath = Master.currShow.ShowFanartPath
                                    End If
                                    If Master.eSettings.ScanTVMediaInfo Then
                                        MediaInfo.UpdateTVMediaInfo(Master.currShow)
                                    End If
                                    scraperEventEvent = Me.ScraperEventEvent
                                    If (Not scraperEventEvent Is Nothing) Then
                                        scraperEventEvent.Invoke(TVScraperEventType.Verifying, 1, Nothing)
                                    End If
                                Else
                                    scraperEventEvent = Me.ScraperEventEvent
                                    If (Not scraperEventEvent Is Nothing) Then
                                        scraperEventEvent.Invoke(TVScraperEventType.Cancelled, 0, Nothing)
                                    End If
                                End If
                            End Using
                        End If
                    End If
                Catch exception1 As Exception
                    ProjectData.SetProjectError(exception1)
                    Dim exception As Exception = exception1
                    Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                    ProjectData.ClearProjectError
                End Try
            End Sub

            Public Sub ScrapeSeason(ByVal sInfo As ScrapeInfo)
                Dim scraperEventEvent As ScraperEventEventHandler = Me.ScraperEventEvent
                If (Not scraperEventEvent Is Nothing) Then
                    scraperEventEvent.Invoke(TVScraperEventType.LoadingEpisodes, 0, Nothing)
                End If
                Me.bwTVDB.WorkerReportsProgress = True
                Me.bwTVDB.WorkerSupportsCancellation = True
                Dim argument As New Arguments With { _
                    .Type = 4, _
                    .Parameter = sInfo _
                }
                Me.bwTVDB.RunWorkerAsync(argument)
            End Sub

            Private Function SearchSeries(ByVal sInfo As ScrapeInfo) As List(Of TVSearchResults)
                Dim e$__ As New _Closure$__17
                Dim list2 As New List(Of TVSearchResults)
                e$__.$VB$Local_cResult = New TVSearchResults
                Dim http As New HTTP
                e$__.$VB$Local_sLang = String.Empty
                e$__.$VB$Local_tmpID = String.Empty
                Try 
                    Dim str As String = http.DownloadData(String.Format("http://{0}/api/GetSeries.php?seriesname={1}&language={2}", Master.eSettings.TVDBMirror, sInfo.ShowTitle, Master.eSettings.TVDBLanguage))
                    If Not String.IsNullOrEmpty(str) Then
                        Dim document2 As XDocument
                        Dim language As TVLanguage
                        Dim enumerator As IEnumerator(Of XElement)
                        Try 
                            document2 = XDocument.Parse(str)
                        Catch exception1 As Exception
                            ProjectData.SetProjectError(exception1)
                            Dim list As List(Of TVSearchResults) = list2
                            ProjectData.ClearProjectError
                            Return list
                        End Try
                        Dim source As IEnumerable(Of XElement) = document2.Descendants("Series").Where(Of XElement)(New Func(Of XElement, Boolean)(AddressOf ScraperObject._Lambda$__105))
                        Dim str2 As String
                        For Each str2 In source.GroupBy(Of XElement, String)(New Func(Of XElement, String)(AddressOf ScraperObject._Lambda$__106)).Select(Of IGrouping(Of String, XElement), String)(New Func(Of IGrouping(Of String, XElement), String)(AddressOf ScraperObject._Lambda$__107))
                            e$__.$VB$Local_tmpID = str2
                            If (source.Where(Of XElement)(New Func(Of XElement, Boolean)(AddressOf e$__._Lambda$__108)).Count(Of XElement)() = 0) Then
                                Dim str3 As String = http.DownloadData(String.Format("http://{0}/api/{1}/series/{2}/{3}.xml", New Object() { Master.eSettings.TVDBMirror, "7B090234F418D074", e$__.$VB$Local_tmpID, Master.eSettings.TVDBLanguage }))
                                If Not String.IsNullOrEmpty(str3) Then
                                    Dim document As XDocument
                                    Try 
                                        document = XDocument.Parse(str3)
                                    Catch exception2 As Exception
                                        ProjectData.SetProjectError(exception2)
                                        ProjectData.ClearProjectError
                                        Continue For
                                    End Try
                                    Dim element As XElement
                                    For Each element In document.Descendants("Series").Where(Of XElement)(New Func(Of XElement, Boolean)(AddressOf ScraperObject._Lambda$__109))
                                        e$__.$VB$Local_sLang = String.Empty
                                        e$__.$VB$Local_cResult = New TVSearchResults
                                        e$__.$VB$Local_cResult.ID = Convert.ToInt32(element.Element("id").Value)
                                        e$__.$VB$Local_cResult.Name = If(Not Information.IsNothing(element.Element("SeriesName")), element.Element("SeriesName").Value, String.Empty)
                                        If (Not Information.IsNothing(element.Element("Language")) AndAlso (Master.eSettings.TVDBLanguages.Count > 0)) Then
                                            e$__.$VB$Local_sLang = element.Element("Language").Value
                                            e$__.$VB$Local_cResult.Language = Master.eSettings.TVDBLanguages.FirstOrDefault(Of TVLanguage)(New Func(Of TVLanguage, Boolean)(AddressOf e$__._Lambda$__110))
                                        ElseIf Not Information.IsNothing(element.Element("Language")) Then
                                            e$__.$VB$Local_sLang = element.Element("Language").Value
                                            language = New TVLanguage With { _
                                                .LongLang = String.Format("Unknown ({0})", e$__.$VB$Local_sLang), _
                                                .ShortLang = e$__.$VB$Local_sLang _
                                            }
                                            e$__.$VB$Local_cResult.Language = language
                                        Else
                                            Continue For
                                        End If
                                        e$__.$VB$Local_cResult.Aired = If(Not Information.IsNothing(element.Element("FirstAired")), element.Element("FirstAired").Value, String.Empty)
                                        e$__.$VB$Local_cResult.Overview = If(Not Information.IsNothing(element.Element("Overview")), element.Element("Overview").Value.ToString.Replace(ChrW(13) & ChrW(10), ChrW(10)).Replace(ChrW(10), ChrW(13) & ChrW(10)), String.Empty)
                                        e$__.$VB$Local_cResult.Banner = If(Not Information.IsNothing(element.Element("banner")), element.Element("banner").Value, String.Empty)
                                        If ((Not String.IsNullOrEmpty(e$__.$VB$Local_cResult.Name) AndAlso Not String.IsNullOrEmpty(e$__.$VB$Local_sLang)) AndAlso (source.Where(Of XElement)(New Func(Of XElement, Boolean)(AddressOf e$__._Lambda$__111)).Count(Of XElement)() = 0)) Then
                                            e$__.$VB$Local_cResult.Lev = StringUtils.ComputeLevenshtein(sInfo.ShowTitle, e$__.$VB$Local_cResult.Name)
                                            list2.Add(e$__.$VB$Local_cResult)
                                        End If
                                    Next
                                End If
                            End If
                        Next
                        http = Nothing
                        Try 
                            enumerator = source.GetEnumerator
                            Do While enumerator.MoveNext
                                Dim current As XElement = enumerator.Current
                                e$__.$VB$Local_sLang = String.Empty
                                e$__.$VB$Local_cResult = New TVSearchResults
                                e$__.$VB$Local_cResult.ID = Convert.ToInt32(current.Element("seriesid").Value)
                                e$__.$VB$Local_cResult.Name = If(Not Information.IsNothing(current.Element("SeriesName")), current.Element("SeriesName").Value, String.Empty)
                                If (Not Information.IsNothing(current.Element("language")) AndAlso (Master.eSettings.TVDBLanguages.Count > 0)) Then
                                    e$__.$VB$Local_sLang = current.Element("language").Value
                                    e$__.$VB$Local_cResult.Language = Master.eSettings.TVDBLanguages.FirstOrDefault(Of TVLanguage)(New Func(Of TVLanguage, Boolean)(AddressOf e$__._Lambda$__112))
                                ElseIf Not Information.IsNothing(current.Element("language")) Then
                                    e$__.$VB$Local_sLang = current.Element("language").Value
                                    language = New TVLanguage With { _
                                        .LongLang = String.Format("Unknown ({0})", e$__.$VB$Local_sLang), _
                                        .ShortLang = e$__.$VB$Local_sLang _
                                    }
                                    e$__.$VB$Local_cResult.Language = language
                                Else
                                    Continue Do
                                End If
                                e$__.$VB$Local_cResult.Aired = If(Not Information.IsNothing(current.Element("FirstAired")), current.Element("FirstAired").Value, String.Empty)
                                e$__.$VB$Local_cResult.Overview = If(Not Information.IsNothing(current.Element("Overview")), current.Element("Overview").Value.ToString.Replace(ChrW(13) & ChrW(10), ChrW(10)).Replace(ChrW(10), ChrW(13) & ChrW(10)), String.Empty)
                                e$__.$VB$Local_cResult.Banner = If(Not Information.IsNothing(current.Element("banner")), current.Element("banner").Value, String.Empty)
                                If (Not String.IsNullOrEmpty(e$__.$VB$Local_cResult.Name) AndAlso Not String.IsNullOrEmpty(e$__.$VB$Local_sLang)) Then
                                    e$__.$VB$Local_cResult.Lev = StringUtils.ComputeLevenshtein(sInfo.ShowTitle, e$__.$VB$Local_cResult.Name)
                                    list2.Add(e$__.$VB$Local_cResult)
                                End If
                            Loop
                        Finally
                            If (Not enumerator Is Nothing) Then
                                enumerator.Dispose
                            End If
                        End Try
                    End If
                Catch exception3 As Exception
                    ProjectData.SetProjectError(exception3)
                    Dim exception As Exception = exception3
                    Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                    ProjectData.ClearProjectError
                End Try
                Return list2
            End Function

            Private Sub ShowFromXML(ByVal sInfo As ScrapeInfo, ByVal ImagesOnly As Boolean)
                Dim e$__ As New _Closure$__18
                Dim list As New List(Of Person)
                Dim tVDBID As String = String.Empty
                e$__.$VB$Local_iEp = -1
                e$__.$VB$Local_iSeas = -1
                e$__.$VB$Local_sTitle = String.Empty
                Dim flag As Boolean = False
                Dim expression As XElement = Nothing
                Dim dbtv As DBTV = Scraper.tmpTVDBShow.Show
                Dim standard As Ordering = Ordering.Standard
                If Not ImagesOnly Then
                    If Master.eSettings.DisplayMissingEpisodes Then
                        Scraper.tEpisodes = Me.GetListOfKnownEpisodes(sInfo)
                    End If
                    Try 
                        If ((sInfo.Options.bShowActors OrElse sInfo.Options.bEpActors) AndAlso Not String.IsNullOrEmpty(Me.aXML)) Then
                            Dim enumerator As IEnumerator(Of XElement)
                            Dim document As XDocument = XDocument.Parse(Me.aXML)
                            Try 
                                enumerator = document.Descendants("Actor").GetEnumerator
                                Do While enumerator.MoveNext
                                    Dim current As XElement = enumerator.Current
                                    If (Not Information.IsNothing(current.Element("Name")) AndAlso Not String.IsNullOrEmpty(current.Element("Name").Value)) Then
                                        Dim item As New Person With { _
                                            .Name = current.Element("Name").Value, _
                                            .Role = current.Element("Role").Value, _
                                            .Thumb = If((Information.IsNothing(current.Element("Image")) OrElse String.IsNullOrEmpty(current.Element("Image").Value)), String.Empty, String.Format("http://{0}/banners/{1}", Master.eSettings.TVDBMirror, current.Element("Image").Value)) _
                                        }
                                        list.Add(item)
                                    End If
                                Loop
                            Finally
                                If (Not enumerator Is Nothing) Then
                                    enumerator.Dispose
                                End If
                            End Try
                        End If
                    Catch exception1 As Exception
                        ProjectData.SetProjectError(exception1)
                        Dim exception As Exception = exception1
                        Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                        ProjectData.ClearProjectError
                    End Try
                    Try 
                        If Not String.IsNullOrEmpty(Me.sXML) Then
                            Dim document2 As XDocument = XDocument.Parse(Me.sXML)
                            Dim source As IEnumerable(Of XElement) = document2.Descendants("Series").Select(Of XElement, XElement)(New Func(Of XElement, XElement)(AddressOf ScraperObject._Lambda$__113))
                            If (source.Count(Of XElement)() > 0) Then
                                dbtv.ShowLanguage = sInfo.SelectedLang
                                If Not Information.IsNothing(dbtv.TVShow) Then
                                    Dim tVShow As TVShow = dbtv.TVShow
                                    tVDBID = source.ElementAtOrDefault(Of XElement)(0).Element("id").Value
                                    tVShow.ID = tVDBID
                                    If (sInfo.Options.bShowTitle AndAlso (String.IsNullOrEmpty(tVShow.Title) OrElse Not Master.eSettings.ShowLockTitle)) Then
                                        tVShow.Title = If(Information.IsNothing(source.ElementAtOrDefault(Of XElement)(0).Element("SeriesName")), tVShow.Title, source.ElementAtOrDefault(Of XElement)(0).Element("SeriesName").Value)
                                    End If
                                    If sInfo.Options.bShowEpisodeGuide Then
                                        tVShow.EpisodeGuideURL = If(Not String.IsNullOrEmpty(Master.eSettings.ExternalTVDBAPIKey), String.Format("http://{0}/api/{1}/series/{2}/all/{3}.zip", New Object() { Master.eSettings.TVDBMirror, Master.eSettings.ExternalTVDBAPIKey, tVDBID, Master.eSettings.TVDBLanguage }), String.Empty)
                                    End If
                                    If (sInfo.Options.bShowGenre AndAlso (String.IsNullOrEmpty(tVShow.Genre) OrElse Not Master.eSettings.ShowLockGenre)) Then
                                        tVShow.Genre = If(Information.IsNothing(source.ElementAtOrDefault(Of XElement)(0).Element("Genre")), tVShow.Genre, Strings.Join(source.ElementAtOrDefault(Of XElement)(0).Element("Genre").Value.Trim(New Char() { Convert.ToChar("|") }).Split(New Char() { Convert.ToChar("|") }), " / "))
                                    End If
                                    If sInfo.Options.bShowMPAA Then
                                        tVShow.MPAA = If(Information.IsNothing(source.ElementAtOrDefault(Of XElement)(0).Element("ContentRating")), tVShow.MPAA, source.ElementAtOrDefault(Of XElement)(0).Element("ContentRating").Value)
                                    End If
                                    If (sInfo.Options.bShowPlot AndAlso (String.IsNullOrEmpty(tVShow.Plot) OrElse Not Master.eSettings.ShowLockPlot)) Then
                                        tVShow.Plot = If(Information.IsNothing(source.ElementAtOrDefault(Of XElement)(0).Element("Overview")), tVShow.Plot, source.ElementAtOrDefault(Of XElement)(0).Element("Overview").Value.ToString.Replace(ChrW(13) & ChrW(10), ChrW(10)).Replace(ChrW(10), ChrW(13) & ChrW(10)))
                                    End If
                                    If sInfo.Options.bShowPremiered Then
                                        tVShow.Premiered = If(Information.IsNothing(source.ElementAtOrDefault(Of XElement)(0).Element("FirstAired")), tVShow.Premiered, source.ElementAtOrDefault(Of XElement)(0).Element("FirstAired").Value)
                                    End If
                                    If (sInfo.Options.bShowRating AndAlso (String.IsNullOrEmpty(tVShow.Rating) OrElse Not Master.eSettings.ShowLockRating)) Then
                                        tVShow.Rating = If(Information.IsNothing(source.ElementAtOrDefault(Of XElement)(0).Element("Rating")), tVShow.Rating, source.ElementAtOrDefault(Of XElement)(0).Element("Rating").Value)
                                    End If
                                    If (sInfo.Options.bShowStudio AndAlso (String.IsNullOrEmpty(tVShow.Studio) OrElse Not Master.eSettings.ShowLockStudio)) Then
                                        tVShow.Studio = If(Information.IsNothing(source.ElementAtOrDefault(Of XElement)(0).Element("Network")), tVShow.Studio, source.ElementAtOrDefault(Of XElement)(0).Element("Network").Value)
                                    End If
                                    If sInfo.Options.bShowActors Then
                                        tVShow.Actors = list
                                    End If
                                    tVShow = Nothing
                                End If
                                Scraper.tmpTVDBShow.Show = dbtv
                                Dim dbtv2 As DBTV
                                For Each dbtv2 In Scraper.tmpTVDBShow.Episodes
                                    dbtv2.ShowLanguage = sInfo.SelectedLang
                                    e$__.$VB$Local_iEp = dbtv2.TVEp.Episode
                                    e$__.$VB$Local_iSeas = dbtv2.TVEp.Season
                                    e$__.$VB$Local_sTitle = dbtv2.TVEp.Title
                                    flag = False
                                    standard = Ordering.Standard
                                    If Not Information.IsNothing(dbtv.TVShow) Then
                                        dbtv2.TVShow = dbtv.TVShow
                                    End If
                                    If (sInfo.Ordering = Ordering.DVD) Then
                                        If (document2.Descendants("Episode").Where(Of XElement)(New Func(Of XElement, Boolean)(AddressOf e$__._Lambda$__114)).Count(Of XElement)() = 0) Then
                                            standard = Ordering.DVD
                                        End If
                                    ElseIf ((sInfo.Ordering = Ordering.Absolute) AndAlso (document2.Descendants("Episode").Where(Of XElement)(New Func(Of XElement, Boolean)(AddressOf ScraperObject._Lambda$__115)).Count(Of XElement)() = 0)) Then
                                        standard = Ordering.Absolute
                                    End If
                                    If (standard = Ordering.DVD) Then
                                        expression = document2.Descendants("Episode").FirstOrDefault(Of XElement)(New Func(Of XElement, Boolean)(AddressOf e$__._Lambda$__116))
                                    ElseIf (standard = Ordering.Absolute) Then
                                        If (e$__.$VB$Local_iSeas = 1) Then
                                            expression = document2.Descendants("Episode").FirstOrDefault(Of XElement)(New Func(Of XElement, Boolean)(AddressOf e$__._Lambda$__117))
                                        Else
                                            expression = document2.Descendants("Episode").FirstOrDefault(Of XElement)(New Func(Of XElement, Boolean)(AddressOf e$__._Lambda$__118))
                                        End If
                                    Else
                                        expression = document2.Descendants("Episode").FirstOrDefault(Of XElement)(New Func(Of XElement, Boolean)(AddressOf e$__._Lambda$__119))
                                    End If
                                    If Information.IsNothing(expression) Then
                                        expression = document2.Descendants("Episode").FirstOrDefault(Of XElement)(New Func(Of XElement, Boolean)(AddressOf e$__._Lambda$__120))
                                        flag = True
                                    End If
                                    If Information.IsNothing(expression) Then
                                        Continue For
                                    End If
                                    Dim tVEp As EpisodeDetails = dbtv2.TVEp
                                    If ((sInfo.Options.bEpTitle AndAlso (String.IsNullOrEmpty(tVEp.Title) OrElse Not Master.eSettings.EpLockTitle)) AndAlso Not String.IsNullOrEmpty(expression.Element("EpisodeName").Value)) Then
                                        tVEp.Title = expression.Element("EpisodeName").Value
                                    End If
                                    If flag Then
                                        Select Case standard
                                            Case Ordering.DVD
                                                If sInfo.Options.bEpSeason Then
                                                    tVEp.Season = If((Information.IsNothing(expression.Element("DVD_season")) OrElse String.IsNullOrEmpty(expression.Element("DVD_season").Value)), 0, Convert.ToInt32(expression.Element("DVD_season").Value))
                                                End If
                                                If sInfo.Options.bEpEpisode Then
                                                    tVEp.Episode = If((Information.IsNothing(expression.Element("DVD_episodenumber")) OrElse String.IsNullOrEmpty(expression.Element("DVD_episodenumber").Value)), 0, Convert.ToInt32(expression.Element("DVD_episodenumber").Value))
                                                End If
                                                goto Label_0C38
                                            Case Ordering.Absolute
                                                If sInfo.Options.bEpSeason Then
                                                    tVEp.Season = 1
                                                End If
                                                If sInfo.Options.bEpEpisode Then
                                                    tVEp.Episode = If((Information.IsNothing(expression.Element("absolute_number")) OrElse String.IsNullOrEmpty(expression.Element("absolute_number").Value)), 0, Convert.ToInt32(expression.Element("absolute_number").Value))
                                                End If
                                                goto Label_0C38
                                        End Select
                                        If sInfo.Options.bEpSeason Then
                                            tVEp.Season = If((Information.IsNothing(expression.Element("SeasonNumber")) OrElse String.IsNullOrEmpty(expression.Element("SeasonNumber").Value)), 0, Convert.ToInt32(expression.Element("SeasonNumber").Value))
                                        End If
                                        If sInfo.Options.bEpEpisode Then
                                            tVEp.Episode = If((Information.IsNothing(expression.Element("EpisodeNumber")) OrElse String.IsNullOrEmpty(expression.Element("EpisodeNumber").Value)), 0, Convert.ToInt32(expression.Element("EpisodeNumber").Value))
                                        End If
                                    End If
                                Label_0C38:
                                    If sInfo.Options.bEpAired Then
                                        tVEp.Aired = If(Information.IsNothing(expression.Element("FirstAired")), tVEp.Aired, expression.Element("FirstAired").Value)
                                    End If
                                    If (sInfo.Options.bEpRating AndAlso (String.IsNullOrEmpty(tVEp.Rating) OrElse Not Master.eSettings.EpLockRating)) Then
                                        tVEp.Rating = If(Information.IsNothing(expression.Element("Rating")), tVEp.Rating, expression.Element("Rating").Value)
                                    End If
                                    If (sInfo.Options.bEpPlot AndAlso (String.IsNullOrEmpty(tVEp.Plot) OrElse Not Master.eSettings.EpLockPlot)) Then
                                        tVEp.Plot = If(Information.IsNothing(expression.Element("Overview")), tVEp.Plot, expression.Element("Overview").Value.ToString.Replace(ChrW(13) & ChrW(10), ChrW(10)).Replace(ChrW(10), ChrW(13) & ChrW(10)))
                                    End If
                                    If sInfo.Options.bEpDirector Then
                                        tVEp.Director = If(Information.IsNothing(expression.Element("Director")), tVEp.Director, Strings.Join(expression.Element("Director").Value.Trim(New Char() { Convert.ToChar("|") }).Split(New Char() { Convert.ToChar("|") }), " / "))
                                    End If
                                    If sInfo.Options.bEpCredits Then
                                        tVEp.Credits = Me.CreditsString(If(Information.IsNothing(expression.Element("GuestStars")), String.Empty, expression.Element("GuestStars").Value), If(Information.IsNothing(expression.Element("Writer")), String.Empty, expression.Element("Writer").Value))
                                    End If
                                    If sInfo.Options.bEpActors Then
                                        tVEp.Actors = list
                                    End If
                                    tVEp.PosterURL = If((Information.IsNothing(expression.Element("filename")) OrElse String.IsNullOrEmpty(expression.Element("filename").Value)), String.Empty, String.Format("http://{0}/banners/{1}", Master.eSettings.TVDBMirror, expression.Element("filename").Value))
                                    tVEp.LocalFile = If((Information.IsNothing(expression.Element("filename")) OrElse String.IsNullOrEmpty(expression.Element("filename").Value)), String.Empty, Path.Combine(Master.TempPath, String.Concat(New String() { "Shows", Conversions.ToString(Path.DirectorySeparatorChar), tVDBID, Conversions.ToString(Path.DirectorySeparatorChar), "episodeposters", Conversions.ToString(Path.DirectorySeparatorChar), expression.Element("filename").Value.Replace(Convert.ToChar("/"), Path.DirectorySeparatorChar) })))
                                    tVEp = Nothing
                                Next
                            End If
                        End If
                    Catch exception4 As Exception
                        ProjectData.SetProjectError(exception4)
                        Dim exception2 As Exception = exception4
                        Master.eLog.WriteToErrorLog(exception2.Message, exception2.StackTrace, "Error", True)
                        ProjectData.ClearProjectError
                    End Try
                Else
                    tVDBID = sInfo.TVDBID
                End If
                Try 
                    If ((ImagesOnly OrElse Not Information.IsNothing(dbtv.TVShow)) AndAlso Not String.IsNullOrEmpty(Me.bXML)) Then
                        Dim enumerator3 As IEnumerator(Of XElement)
                        Dim document3 As XDocument = XDocument.Parse(Me.bXML)
                        Try 
                            enumerator3 = document3.Descendants("Banner").GetEnumerator
                            Do While enumerator3.MoveNext
                                Dim element3 As XElement = enumerator3.Current
                                If ((Not Information.IsNothing(element3.Element("BannerPath")) AndAlso Not String.IsNullOrEmpty(element3.Element("BannerPath").Value)) AndAlso ((Not Master.eSettings.OnlyGetTVImagesForSelectedLanguage OrElse (Not Information.IsNothing(element3.Element("Language")) AndAlso (element3.Element("Language").Value = Master.eSettings.TVDBLanguage))) OrElse ((Information.IsNothing(element3.Element("Language")) OrElse (element3.Element("Language").Value = "en")) AndAlso Master.eSettings.AlwaysGetEnglishTVImages))) Then
                                    Dim size As Size
                                    Dim size2 As Size
                                    Select Case element3.Element("BannerType").Value
                                        Case "fanart"
                                            Dim fanart As New TVDBFanart With { _
                                                .URL = String.Format("http://{0}/banners/{1}", Master.eSettings.TVDBMirror, element3.Element("BannerPath").Value), _
                                                .ThumbnailURL = If((Information.IsNothing(element3.Element("ThumbnailPath")) OrElse String.IsNullOrEmpty(element3.Element("ThumbnailPath").Value)), String.Empty, String.Format("http://{0}/banners/{1}", Master.eSettings.TVDBMirror, element3.Element("ThumbnailPath").Value)), _
                                                .Size = If((Information.IsNothing(element3.Element("BannerType2")) OrElse String.IsNullOrEmpty(element3.Element("BannerType2").Value)), size2 = size = New Size, StringUtils.StringToSize(element3.Element("BannerType2").Value)), _
                                                .LocalFile = Path.Combine(Master.TempPath, String.Concat(New String() { "Shows", Conversions.ToString(Path.DirectorySeparatorChar), tVDBID, Conversions.ToString(Path.DirectorySeparatorChar), "fanart", Conversions.ToString(Path.DirectorySeparatorChar), element3.Element("BannerPath").Value.Replace(Convert.ToChar("/"), Path.DirectorySeparatorChar) })), _
                                                .LocalThumb = Path.Combine(Master.TempPath, String.Concat(New String() { "Shows", Conversions.ToString(Path.DirectorySeparatorChar), tVDBID, Conversions.ToString(Path.DirectorySeparatorChar), "fanart", Conversions.ToString(Path.DirectorySeparatorChar), element3.Element("ThumbnailPath").Value.Replace(Convert.ToChar("/"), Path.DirectorySeparatorChar) })), _
                                                .Language = If((Information.IsNothing(element3.Element("Language")) OrElse String.IsNullOrEmpty(element3.Element("Language").Value)), String.Empty, element3.Element("Language").Value) _
                                            }
                                            Scraper.tmpTVDBShow.Fanart.Add(fanart)
                                            Exit Select
                                        Case "poster"
                                            Dim poster As New TVDBPoster With { _
                                                .URL = String.Format("http://{0}/banners/{1}", Master.eSettings.TVDBMirror, element3.Element("BannerPath").Value), _
                                                .Size = If((Information.IsNothing(element3.Element("BannerType2")) OrElse String.IsNullOrEmpty(element3.Element("BannerType2").Value)), size = size2 = New Size, StringUtils.StringToSize(element3.Element("BannerType2").Value)), _
                                                .LocalFile = Path.Combine(Master.TempPath, String.Concat(New String() { "Shows", Conversions.ToString(Path.DirectorySeparatorChar), tVDBID, Conversions.ToString(Path.DirectorySeparatorChar), "posters", Conversions.ToString(Path.DirectorySeparatorChar), element3.Element("BannerPath").Value.Replace(Convert.ToChar("/"), Path.DirectorySeparatorChar) })), _
                                                .Language = If((Information.IsNothing(element3.Element("Language")) OrElse String.IsNullOrEmpty(element3.Element("Language").Value)), String.Empty, element3.Element("Language").Value) _
                                            }
                                            Scraper.tmpTVDBShow.Posters.Add(poster)
                                            Exit Select
                                        Case "season"
                                            Dim poster2 As New TVDBSeasonPoster With { _
                                                .URL = String.Format("http://{0}/banners/{1}", Master.eSettings.TVDBMirror, element3.Element("BannerPath").Value), _
                                                .Season = If((Information.IsNothing(element3.Element("Season")) OrElse String.IsNullOrEmpty(element3.Element("Season").Value)), 0, Convert.ToInt32(element3.Element("Season").Value)), _
                                                .Type = If((Information.IsNothing(element3.Element("BannerType2")) OrElse String.IsNullOrEmpty(element3.Element("BannerType2").Value)), SeasonPosterType.None, Me.StringToSeasonPosterType(element3.Element("BannerType2").Value)), _
                                                .LocalFile = Path.Combine(Master.TempPath, String.Concat(New String() { "Shows", Conversions.ToString(Path.DirectorySeparatorChar), tVDBID, Conversions.ToString(Path.DirectorySeparatorChar), "seasonposters", Conversions.ToString(Path.DirectorySeparatorChar), element3.Element("BannerPath").Value.Replace(Convert.ToChar("/"), Path.DirectorySeparatorChar) })), _
                                                .Language = If((Information.IsNothing(element3.Element("Language")) OrElse String.IsNullOrEmpty(element3.Element("Language").Value)), String.Empty, element3.Element("Language").Value) _
                                            }
                                            Scraper.tmpTVDBShow.SeasonPosters.Add(poster2)
                                            Exit Select
                                        Case "series"
                                            Dim poster3 As New TVDBShowPoster With { _
                                                .URL = String.Format("http://{0}/banners/{1}", Master.eSettings.TVDBMirror, element3.Element("BannerPath").Value), _
                                                .Type = If((Information.IsNothing(element3.Element("BannerType2")) OrElse String.IsNullOrEmpty(element3.Element("BannerType2").Value)), ShowBannerType.None, Me.StringToShowPosterType(element3.Element("BannerType2").Value)), _
                                                .LocalFile = Path.Combine(Master.TempPath, String.Concat(New String() { "Shows", Conversions.ToString(Path.DirectorySeparatorChar), tVDBID, Conversions.ToString(Path.DirectorySeparatorChar), "seriesposters", Conversions.ToString(Path.DirectorySeparatorChar), element3.Element("BannerPath").Value.Replace(Convert.ToChar("/"), Path.DirectorySeparatorChar) })), _
                                                .Language = If((Information.IsNothing(element3.Element("Language")) OrElse String.IsNullOrEmpty(element3.Element("Language").Value)), String.Empty, element3.Element("Language").Value) _
                                            }
                                            Scraper.tmpTVDBShow.ShowPosters.Add(poster3)
                                            Exit Select
                                    End Select
                                End If
                            Loop
                        Finally
                            If (Not enumerator3 Is Nothing) Then
                                enumerator3.Dispose
                            End If
                        End Try
                    End If
                Catch exception5 As Exception
                    ProjectData.SetProjectError(exception5)
                    Dim exception3 As Exception = exception5
                    Master.eLog.WriteToErrorLog(exception3.Message, exception3.StackTrace, "Error", True)
                    ProjectData.ClearProjectError
                End Try
            End Sub

            Public Sub SingleScrape(ByVal sInfo As ScrapeInfo)
                Dim scraperEventEvent As ScraperEventEventHandler = Me.ScraperEventEvent
                If (Not scraperEventEvent Is Nothing) Then
                    scraperEventEvent.Invoke(TVScraperEventType.LoadingEpisodes, 0, Nothing)
                End If
                Me.bwTVDB.WorkerReportsProgress = False
                Me.bwTVDB.WorkerSupportsCancellation = True
                Dim argument As New Arguments With { _
                    .Type = 2, _
                    .Parameter = sInfo _
                }
                Me.bwTVDB.RunWorkerAsync(argument)
                Do While Me.bwTVDB.IsBusy
                    Application.DoEvents
                    Thread.Sleep(50)
                Loop
            End Sub

            Public Sub StartSingleScraper(ByVal sInfo As ScrapeInfo)
                Try 
                    Dim scraperEventEvent As ScraperEventEventHandler
                    If (String.IsNullOrEmpty(sInfo.TVDBID) AndAlso (sInfo.ScrapeType = ScrapeType.FullAsk)) Then
                        scraperEventEvent = Me.ScraperEventEvent
                        If (Not scraperEventEvent Is Nothing) Then
                            scraperEventEvent.Invoke(TVScraperEventType.Searching, 0, Nothing)
                        End If
                        Using results As dlgTVDBSearchResults = New dlgTVDBSearchResults
                            If (results.ShowDialog(sInfo) = DialogResult.OK) Then
                                Master.currShow = Scraper.tmpTVDBShow.Show
                                scraperEventEvent = Me.ScraperEventEvent
                                If (Not scraperEventEvent Is Nothing) Then
                                    scraperEventEvent.Invoke(TVScraperEventType.SelectImages, 0, Nothing)
                                End If
                                Using select As dlgTVImageSelect = New dlgTVImageSelect
                                    If ([select].ShowDialog(sInfo.ShowID, TVImageType.All, sInfo.ScrapeType, sInfo.WithCurrent) = DialogResult.OK) Then
                                        If (Not Information.IsNothing(sInfo.iSeason) AndAlso (sInfo.iSeason >= 0)) Then
                                            Me.SaveImages
                                        Else
                                            scraperEventEvent = Me.ScraperEventEvent
                                            If (Not scraperEventEvent Is Nothing) Then
                                                scraperEventEvent.Invoke(TVScraperEventType.Verifying, 0, Nothing)
                                            End If
                                        End If
                                    Else
                                        scraperEventEvent = Me.ScraperEventEvent
                                        If (Not scraperEventEvent Is Nothing) Then
                                            scraperEventEvent.Invoke(TVScraperEventType.Cancelled, 0, Nothing)
                                        End If
                                    End If
                                End Using
                            Else
                                scraperEventEvent = Me.ScraperEventEvent
                                If (Not scraperEventEvent Is Nothing) Then
                                    scraperEventEvent.Invoke(TVScraperEventType.Cancelled, 0, Nothing)
                                End If
                            End If
                        End Using
                    Else
                        Me.DownloadSeries(sInfo, False)
                        If (Scraper.tmpTVDBShow.Show.TVShow.ID.Length > 0) Then
                            Master.currShow = Scraper.tmpTVDBShow.Show
                            scraperEventEvent = Me.ScraperEventEvent
                            If (Not scraperEventEvent Is Nothing) Then
                                scraperEventEvent.Invoke(TVScraperEventType.SelectImages, 0, Nothing)
                            End If
                            Using select2 As dlgTVImageSelect = New dlgTVImageSelect
                                If (select2.ShowDialog(sInfo.ShowID, TVImageType.All, sInfo.ScrapeType, sInfo.WithCurrent) = DialogResult.OK) Then
                                    If (Not Information.IsNothing(sInfo.iSeason) AndAlso (sInfo.iSeason >= 0)) Then
                                        Me.SaveImages
                                    ElseIf (sInfo.ScrapeType = ScrapeType.FullAuto) Then
                                        scraperEventEvent = Me.ScraperEventEvent
                                        If (Not scraperEventEvent Is Nothing) Then
                                            scraperEventEvent.Invoke(TVScraperEventType.SaveAuto, 0, Nothing)
                                        End If
                                    Else
                                        scraperEventEvent = Me.ScraperEventEvent
                                        If (Not scraperEventEvent Is Nothing) Then
                                            scraperEventEvent.Invoke(TVScraperEventType.Verifying, 0, Nothing)
                                        End If
                                    End If
                                Else
                                    scraperEventEvent = Me.ScraperEventEvent
                                    If (Not scraperEventEvent Is Nothing) Then
                                        scraperEventEvent.Invoke(TVScraperEventType.Cancelled, 0, Nothing)
                                    End If
                                End If
                            End Using
                        ElseIf (sInfo.ScrapeType = ScrapeType.FullAsk) Then
                            scraperEventEvent = Me.ScraperEventEvent
                            If (Not scraperEventEvent Is Nothing) Then
                                scraperEventEvent.Invoke(TVScraperEventType.Searching, 0, Nothing)
                            End If
                            Using results2 As dlgTVDBSearchResults = New dlgTVDBSearchResults
                                If (results2.ShowDialog(sInfo) = DialogResult.OK) Then
                                    Master.currShow = Scraper.tmpTVDBShow.Show
                                    scraperEventEvent = Me.ScraperEventEvent
                                    If (Not scraperEventEvent Is Nothing) Then
                                        scraperEventEvent.Invoke(TVScraperEventType.SelectImages, 0, Nothing)
                                    End If
                                    Using select3 As dlgTVImageSelect = New dlgTVImageSelect
                                        If (select3.ShowDialog(sInfo.ShowID, TVImageType.All, sInfo.ScrapeType, sInfo.WithCurrent) = DialogResult.OK) Then
                                            If (Not Information.IsNothing(sInfo.iSeason) AndAlso (sInfo.iSeason >= 0)) Then
                                                Me.SaveImages
                                            Else
                                                scraperEventEvent = Me.ScraperEventEvent
                                                If (Not scraperEventEvent Is Nothing) Then
                                                    scraperEventEvent.Invoke(TVScraperEventType.Verifying, 0, Nothing)
                                                End If
                                            End If
                                        Else
                                            scraperEventEvent = Me.ScraperEventEvent
                                            If (Not scraperEventEvent Is Nothing) Then
                                                scraperEventEvent.Invoke(TVScraperEventType.Cancelled, 0, Nothing)
                                            End If
                                        End If
                                    End Using
                                Else
                                    scraperEventEvent = Me.ScraperEventEvent
                                    If (Not scraperEventEvent Is Nothing) Then
                                        scraperEventEvent.Invoke(TVScraperEventType.Cancelled, 0, Nothing)
                                    End If
                                End If
                            End Using
                        Else
                            scraperEventEvent = Me.ScraperEventEvent
                            If (Not scraperEventEvent Is Nothing) Then
                                scraperEventEvent.Invoke(TVScraperEventType.Cancelled, 0, Nothing)
                            End If
                        End If
                    End If
                Catch exception1 As Exception
                    ProjectData.SetProjectError(exception1)
                    Dim exception As Exception = exception1
                    Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                    ProjectData.ClearProjectError
                End Try
            End Sub

            Private Function StringToSeasonPosterType(ByVal sType As String) As SeasonPosterType
                Select Case sType.ToLower
                    Case "season"
                        Return SeasonPosterType.Poster
                    Case "seasonwide"
                        Return SeasonPosterType.Wide
                End Select
                Return SeasonPosterType.None
            End Function

            Private Function StringToShowPosterType(ByVal sType As String) As ShowBannerType
                Select Case sType.ToLower
                    Case "blank"
                        Return ShowBannerType.Blank
                    Case "graphical"
                        Return ShowBannerType.Graphical
                    Case "text"
                        Return ShowBannerType.Text
                End Select
                Return ShowBannerType.None
            End Function


            ' Properties
            Friend Overridable Property bwTVDB As BackgroundWorker
                <DebuggerNonUserCode> _
                Get
                    Return Me._bwTVDB
                End Get
                <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
                Set(ByVal WithEventsValue As BackgroundWorker)
                    Dim handler As RunWorkerCompletedEventHandler = New RunWorkerCompletedEventHandler(AddressOf Me.bwTVDB_RunWorkerCompleted)
                    Dim handler2 As ProgressChangedEventHandler = New ProgressChangedEventHandler(AddressOf Me.bwTVDB_ProgressChanged)
                    Dim handler3 As DoWorkEventHandler = New DoWorkEventHandler(AddressOf Me.bwtvDB_DoWork)
                    If (Not Me._bwTVDB Is Nothing) Then
                        RemoveHandler Me._bwTVDB.RunWorkerCompleted, handler
                        RemoveHandler Me._bwTVDB.ProgressChanged, handler2
                        RemoveHandler Me._bwTVDB.DoWork, handler3
                    End If
                    Me._bwTVDB = WithEventsValue
                    If (Not Me._bwTVDB Is Nothing) Then
                        AddHandler Me._bwTVDB.RunWorkerCompleted, handler
                        AddHandler Me._bwTVDB.ProgressChanged, handler2
                        AddHandler Me._bwTVDB.DoWork, handler3
                    End If
                End Set
            End Property


            ' Fields
            Private Shared __ENCList As List(Of WeakReference) = New List(Of WeakReference)
            <AccessedThroughProperty("bwTVDB")> _
            Private _bwTVDB As BackgroundWorker
            Private aXML As String
            Private bXML As String
            Private sXML As String

            ' Nested Types
            <CompilerGenerated> _
            Friend Class _Closure$__13
                ' Methods
                <DebuggerNonUserCode> _
                Public Sub New()
                End Sub

                <DebuggerNonUserCode> _
                Public Sub New(ByVal other As _Closure$__13)
                    If (Not other Is Nothing) Then
                        Me.$VB$Local_tSeas = other.$VB$Local_tSeas
                    End If
                End Sub

                <CompilerGenerated> _
                Public Function _Lambda$__97(ByVal e As XElement) As Boolean
                    Return ((Convert.ToInt32(e.Element("SeasonNumber").Value) = Me.$VB$Local_tSeas) AndAlso ((Information.IsNothing(e.Element("DVD_season")) OrElse String.IsNullOrEmpty(e.Element("DVD_season").Value.ToString)) OrElse (Information.IsNothing(e.Element("DVD_episodenumber")) OrElse String.IsNullOrEmpty(e.Element("DVD_episodenumber").Value.ToString))))
                End Function

                <CompilerGenerated> _
                Public Function _Lambda$__98(ByVal e As XElement) As Boolean
                    Return ((Convert.ToInt32(e.Element("DVD_season").Value) = Me.$VB$Local_tSeas) AndAlso (Information.IsNothing(e.Element("DVD_episodenumber")) OrElse String.IsNullOrEmpty(e.Element("DVD_episodenumber").Value.ToString)))
                End Function


                ' Fields
                Public $VB$Local_tSeas As Integer
            End Class

            <CompilerGenerated> _
            Friend Class _Closure$__14
                ' Methods
                <DebuggerNonUserCode> _
                Public Sub New()
                End Sub

                <DebuggerNonUserCode> _
                Public Sub New(ByVal other As _Closure$__14)
                    If (Not other Is Nothing) Then
                        Me.$VB$Local_sInfo = other.$VB$Local_sInfo
                    End If
                End Sub

                <CompilerGenerated> _
                Public Function _Lambda$__100(ByVal e As EpisodeDetails) As Boolean
                    Return ((e.Season = Me.$VB$Local_sInfo.iSeason) AndAlso (e.Episode = Me.$VB$Local_sInfo.iEpisode))
                End Function


                ' Fields
                Public $VB$Local_sInfo As ScrapeInfo
            End Class

            <CompilerGenerated> _
            Friend Class _Closure$__15
                ' Methods
                <DebuggerNonUserCode> _
                Public Sub New()
                End Sub

                <DebuggerNonUserCode> _
                Public Sub New(ByVal other As _Closure$__15)
                    If (Not other Is Nothing) Then
                        Me.$VB$Local_sInfo = other.$VB$Local_sInfo
                    End If
                End Sub

                <CompilerGenerated> _
                Public Function _Lambda$__101(ByVal e As EpisodeDetails) As Boolean
                    Return ((e.Episode = Me.$VB$Local_sInfo.iEpisode) AndAlso (e.Season = Me.$VB$Local_sInfo.iSeason))
                End Function

                <CompilerGenerated> _
                Public Function _Lambda$__102(ByVal e As EpisodeDetails) As Boolean
                    Return ((e.Episode = Me.$VB$Local_sInfo.iEpisode) AndAlso (e.Season = Me.$VB$Local_sInfo.iSeason))
                End Function


                ' Fields
                Public $VB$Local_sInfo As ScrapeInfo
            End Class

            <CompilerGenerated> _
            Friend Class _Closure$__16
                ' Methods
                <DebuggerNonUserCode> _
                Public Sub New()
                End Sub

                <DebuggerNonUserCode> _
                Public Sub New(ByVal other As _Closure$__16)
                    If (Not other Is Nothing) Then
                        Me.$VB$Local_iEp = other.$VB$Local_iEp
                        Me.$VB$Local_iSea = other.$VB$Local_iSea
                    End If
                End Sub

                <CompilerGenerated> _
                Public Function _Lambda$__103(ByVal e As EpisodeDetails) As Boolean
                    Return ((e.Episode = Me.$VB$Local_iEp) AndAlso (e.Season = Me.$VB$Local_iSea))
                End Function

                <CompilerGenerated> _
                Public Function _Lambda$__104(ByVal cSeason As TVDBSeasonImage) As Boolean
                    Return (cSeason.Season = Me.$VB$Local_iSea)
                End Function


                ' Fields
                Public $VB$Local_iEp As Integer
                Public $VB$Local_iSea As Integer
            End Class

            <CompilerGenerated> _
            Friend Class _Closure$__17
                ' Methods
                <DebuggerNonUserCode> _
                Public Sub New()
                End Sub

                <DebuggerNonUserCode> _
                Public Sub New(ByVal other As _Closure$__17)
                    If (Not other Is Nothing) Then
                        Me.$VB$Local_tmpID = other.$VB$Local_tmpID
                        Me.$VB$Local_cResult = other.$VB$Local_cResult
                        Me.$VB$Local_sLang = other.$VB$Local_sLang
                    End If
                End Sub

                <CompilerGenerated> _
                Public Function _Lambda$__108(ByVal s As XElement) As Boolean
                    Return ((s.Element("seriesid").Value.ToString = Me.$VB$Local_tmpID) AndAlso (s.Element("language").Value.ToString = Master.eSettings.TVDBLanguage))
                End Function

                <CompilerGenerated> _
                Public Function _Lambda$__110(ByVal s As TVLanguage) As Boolean
                    Return (s.ShortLang = Me.$VB$Local_sLang)
                End Function

                <CompilerGenerated> _
                Public Function _Lambda$__111(ByVal s As XElement) As Boolean
                    Return ((s.Element("seriesid").Value.ToString = Me.$VB$Local_cResult.ID.ToString) AndAlso (s.Element("language").Value.ToString = Me.$VB$Local_sLang))
                End Function

                <CompilerGenerated> _
                Public Function _Lambda$__112(ByVal s As TVLanguage) As Boolean
                    Return (s.ShortLang = Me.$VB$Local_sLang)
                End Function


                ' Fields
                Public $VB$Local_cResult As TVSearchResults
                Public $VB$Local_sLang As String
                Public $VB$Local_tmpID As String
            End Class

            <CompilerGenerated> _
            Friend Class _Closure$__18
                ' Methods
                <DebuggerNonUserCode> _
                Public Sub New()
                End Sub

                <DebuggerNonUserCode> _
                Public Sub New(ByVal other As _Closure$__18)
                    If (Not other Is Nothing) Then
                        Me.$VB$Local_iSeas = other.$VB$Local_iSeas
                        Me.$VB$Local_iEp = other.$VB$Local_iEp
                        Me.$VB$Local_sTitle = other.$VB$Local_sTitle
                    End If
                End Sub

                <CompilerGenerated> _
                Public Function _Lambda$__114(ByVal e As XElement) As Boolean
                    Return ((Not Information.IsNothing(e.Element("SeasonNumber")) AndAlso (Convert.ToInt32(e.Element("SeasonNumber").Value) = Me.$VB$Local_iSeas)) AndAlso ((Information.IsNothing(e.Element("DVD_season")) OrElse String.IsNullOrEmpty(e.Element("DVD_season").Value.ToString)) OrElse (Information.IsNothing(e.Element("DVD_episodenumber")) OrElse String.IsNullOrEmpty(e.Element("DVD_episodenumber").Value.ToString))))
                End Function

                <CompilerGenerated> _
                Public Function _Lambda$__116(ByVal e As XElement) As Boolean
                    Return (((Not Information.IsNothing(e.Element("DVD_episodenumber")) AndAlso Not String.IsNullOrEmpty(e.Element("DVD_episodenumber").Value.ToString)) AndAlso ((Convert.ToInt32(Conversions.ToLong(e.Element("DVD_episodenumber").Value.ToString)) = Me.$VB$Local_iEp) AndAlso Not Information.IsNothing(e.Element("DVD_season")))) AndAlso (Not String.IsNullOrEmpty(e.Element("DVD_season").Value.ToString) AndAlso (Convert.ToInt32(e.Element("DVD_season").Value) = Me.$VB$Local_iSeas)))
                End Function

                <CompilerGenerated> _
                Public Function _Lambda$__117(ByVal e As XElement) As Boolean
                    Return ((Not Information.IsNothing(e.Element("absolute_number")) AndAlso Not String.IsNullOrEmpty(e.Element("absolute_number").Value.ToString)) AndAlso (Convert.ToInt32(e.Element("absolute_number").Value.ToString) = Me.$VB$Local_iEp))
                End Function

                <CompilerGenerated> _
                Public Function _Lambda$__118(ByVal e As XElement) As Boolean
                    Return ((Not Information.IsNothing(e.Element("absolute_number")) AndAlso Not String.IsNullOrEmpty(e.Element("absolute_number").Value.ToString)) AndAlso ((Convert.ToInt32(e.Element("EpisodeNumber").Value.ToString) = Me.$VB$Local_iEp) AndAlso (Convert.ToInt32(e.Element("SeasonNumber").Value) = Me.$VB$Local_iSeas)))
                End Function

                <CompilerGenerated> _
                Public Function _Lambda$__119(ByVal e As XElement) As Boolean
                    Return ((Convert.ToInt32(e.Element("EpisodeNumber").Value) = Me.$VB$Local_iEp) AndAlso (Convert.ToInt32(e.Element("SeasonNumber").Value) = Me.$VB$Local_iSeas))
                End Function

                <CompilerGenerated> _
                Public Function _Lambda$__120(ByVal e As XElement) As Boolean
                    Return (StringUtils.ComputeLevenshtein(e.Element("EpisodeName").Value, Me.$VB$Local_sTitle) < 5)
                End Function


                ' Fields
                Public $VB$Local_iEp As Integer
                Public $VB$Local_iSeas As Integer
                Public $VB$Local_sTitle As String
            End Class

            <StructLayout(LayoutKind.Sequential)> _
            Private Structure Arguments
                Public Parameter As Object
                Public Type As Integer
            End Structure

            <StructLayout(LayoutKind.Sequential)> _
            Private Structure Results
                Public Result As Object
                Public Type As Integer
            End Structure

            Public Delegate Sub ScraperEventEventHandler(ByVal eType As TVScraperEventType, ByVal iProgress As Integer, ByVal Parameter As Object)
        End Class

        <Serializable> _
        Public Class TVDBFanart
            ' Methods
            Public Sub New()
                Me.Clear
            End Sub

            Public Sub Clear()
                Me._url = String.Empty
                Me._thumbnailurl = String.Empty
                Me._size = New Size
                Me._localfile = String.Empty
                Me._localthumb = String.Empty
                Me._image = New Images
                Me._language = String.Empty
            End Sub


            ' Properties
            Public Property Image As Images
                Get
                    Return Me._image
                End Get
                Set(ByVal value As Images)
                    Me._image = value
                End Set
            End Property

            Public Property Language As String
                Get
                    Return Me._language
                End Get
                Set(ByVal value As String)
                    Me._language = value
                End Set
            End Property

            Public Property LocalFile As String
                Get
                    Return Me._localfile
                End Get
                Set(ByVal value As String)
                    Me._localfile = value
                End Set
            End Property

            Public Property LocalThumb As String
                Get
                    Return Me._localthumb
                End Get
                Set(ByVal value As String)
                    Me._localthumb = value
                End Set
            End Property

            Public Property Size As Size
                Get
                    Return Me._size
                End Get
                Set(ByVal value As Size)
                    Me._size = value
                End Set
            End Property

            Public Property ThumbnailURL As String
                Get
                    Return Me._thumbnailurl
                End Get
                Set(ByVal value As String)
                    Me._thumbnailurl = value
                End Set
            End Property

            Public Property URL As String
                Get
                    Return Me._url
                End Get
                Set(ByVal value As String)
                    Me._url = value
                End Set
            End Property


            ' Fields
            Private _image As Images
            Private _language As String
            Private _localfile As String
            Private _localthumb As String
            Private _size As Size
            Private _thumbnailurl As String
            Private _url As String
        End Class

        Public Class TVDBPoster
            ' Methods
            Public Sub New()
                Me.Clear
            End Sub

            Public Sub Clear()
                Me._url = String.Empty
                Me._size = New Size
                Me._localfile = String.Empty
                Me._image = New Images
                Me._language = String.Empty
            End Sub


            ' Properties
            Public Property Image As Images
                Get
                    Return Me._image
                End Get
                Set(ByVal value As Images)
                    Me._image = value
                End Set
            End Property

            Public Property Language As String
                Get
                    Return Me._language
                End Get
                Set(ByVal value As String)
                    Me._language = value
                End Set
            End Property

            Public Property LocalFile As String
                Get
                    Return Me._localfile
                End Get
                Set(ByVal value As String)
                    Me._localfile = value
                End Set
            End Property

            Public Property Size As Size
                Get
                    Return Me._size
                End Get
                Set(ByVal value As Size)
                    Me._size = value
                End Set
            End Property

            Public Property URL As String
                Get
                    Return Me._url
                End Get
                Set(ByVal value As String)
                    Me._url = value
                End Set
            End Property


            ' Fields
            Private _image As Images
            Private _language As String
            Private _localfile As String
            Private _size As Size
            Private _url As String
        End Class

        <Serializable> _
        Public Class TVDBSeasonImage
            ' Methods
            Public Sub New()
                Me.Clear
            End Sub

            Public Sub Clear()
                Me._season = -1
                Me._poster = New Images
                Me._fanart = New TVDBFanart
            End Sub


            ' Properties
            Public Property Fanart As TVDBFanart
                Get
                    Return Me._fanart
                End Get
                Set(ByVal value As TVDBFanart)
                    Me._fanart = value
                End Set
            End Property

            Public Property Poster As Images
                Get
                    Return Me._poster
                End Get
                Set(ByVal value As Images)
                    Me._poster = value
                End Set
            End Property

            Public Property Season As Integer
                Get
                    Return Me._season
                End Get
                Set(ByVal value As Integer)
                    Me._season = value
                End Set
            End Property


            ' Fields
            Private _fanart As TVDBFanart
            Private _poster As Images
            Private _season As Integer
        End Class

        Public Class TVDBSeasonPoster
            ' Methods
            Public Sub New()
                Me.Clear
            End Sub

            Public Sub Clear()
                Me._url = String.Empty
                Me._season = 0
                Me._type = SeasonPosterType.None
                Me._localfile = String.Empty
                Me._image = New Images
                Me._language = String.Empty
            End Sub


            ' Properties
            Public Property Image As Images
                Get
                    Return Me._image
                End Get
                Set(ByVal value As Images)
                    Me._image = value
                End Set
            End Property

            Public Property Language As String
                Get
                    Return Me._language
                End Get
                Set(ByVal value As String)
                    Me._language = value
                End Set
            End Property

            Public Property LocalFile As String
                Get
                    Return Me._localfile
                End Get
                Set(ByVal value As String)
                    Me._localfile = value
                End Set
            End Property

            Public Property Season As Integer
                Get
                    Return Me._season
                End Get
                Set(ByVal value As Integer)
                    Me._season = value
                End Set
            End Property

            Public Property Type As SeasonPosterType
                Get
                    Return Me._type
                End Get
                Set(ByVal value As SeasonPosterType)
                    Me._type = value
                End Set
            End Property

            Public Property URL As String
                Get
                    Return Me._url
                End Get
                Set(ByVal value As String)
                    Me._url = value
                End Set
            End Property


            ' Fields
            Private _image As Images
            Private _language As String
            Private _localfile As String
            Private _season As Integer
            Private _type As SeasonPosterType
            Private _url As String
        End Class

        Public Class TVDBShow
            ' Methods
            Public Sub New()
                Me.Clear
            End Sub

            Public Sub Clear()
                Me._show = New DBTV
                Me._allseason = New DBTV
                Me._episodes = New List(Of DBTV)
                Me._fanart = New List(Of TVDBFanart)
                Me._showposters = New List(Of TVDBShowPoster)
                Me._seasonposters = New List(Of TVDBSeasonPoster)
                Me._posters = New List(Of TVDBPoster)
            End Sub


            ' Properties
            Public Property AllSeason As DBTV
                Get
                    Return Me._allseason
                End Get
                Set(ByVal value As DBTV)
                    Me._allseason = value
                End Set
            End Property

            Public Property Episodes As List(Of DBTV)
                Get
                    Return Me._episodes
                End Get
                Set(ByVal value As List(Of DBTV))
                    Me._episodes = value
                End Set
            End Property

            Public Property Fanart As List(Of TVDBFanart)
                Get
                    Return Me._fanart
                End Get
                Set(ByVal value As List(Of TVDBFanart))
                    Me._fanart = value
                End Set
            End Property

            Public Property Posters As List(Of TVDBPoster)
                Get
                    Return Me._posters
                End Get
                Set(ByVal value As List(Of TVDBPoster))
                    Me._posters = value
                End Set
            End Property

            Public Property SeasonPosters As List(Of TVDBSeasonPoster)
                Get
                    Return Me._seasonposters
                End Get
                Set(ByVal value As List(Of TVDBSeasonPoster))
                    Me._seasonposters = value
                End Set
            End Property

            Public Property Show As DBTV
                Get
                    Return Me._show
                End Get
                Set(ByVal value As DBTV)
                    Me._show = value
                End Set
            End Property

            Public Property ShowPosters As List(Of TVDBShowPoster)
                Get
                    Return Me._showposters
                End Get
                Set(ByVal value As List(Of TVDBShowPoster))
                    Me._showposters = value
                End Set
            End Property


            ' Fields
            Private _allseason As DBTV
            Private _episodes As List(Of DBTV) = New List(Of DBTV)
            Private _fanart As List(Of TVDBFanart) = New List(Of TVDBFanart)
            Private _posters As List(Of TVDBPoster) = New List(Of TVDBPoster)
            Private _seasonposters As List(Of TVDBSeasonPoster) = New List(Of TVDBSeasonPoster)
            Private _show As DBTV
            Private _showposters As List(Of TVDBShowPoster) = New List(Of TVDBShowPoster)
        End Class

        <Serializable> _
        Public Class TVDBShowPoster
            ' Methods
            Public Sub New()
                Me.Clear
            End Sub

            Public Sub Clear()
                Me._url = String.Empty
                Me._type = ShowBannerType.None
                Me._localfile = String.Empty
                Me._image = New Images
                Me._language = String.Empty
            End Sub


            ' Properties
            Public Property Image As Images
                Get
                    Return Me._image
                End Get
                Set(ByVal value As Images)
                    Me._image = value
                End Set
            End Property

            Public Property Language As String
                Get
                    Return Me._language
                End Get
                Set(ByVal value As String)
                    Me._language = value
                End Set
            End Property

            Public Property LocalFile As String
                Get
                    Return Me._localfile
                End Get
                Set(ByVal value As String)
                    Me._localfile = value
                End Set
            End Property

            Public Property Type As ShowBannerType
                Get
                    Return Me._type
                End Get
                Set(ByVal value As ShowBannerType)
                    Me._type = value
                End Set
            End Property

            Public Property URL As String
                Get
                    Return Me._url
                End Get
                Set(ByVal value As String)
                    Me._url = value
                End Set
            End Property


            ' Fields
            Private _image As Images
            Private _language As String
            Private _localfile As String
            Private _type As ShowBannerType
            Private _url As String
        End Class

        <Serializable, StructLayout(LayoutKind.Sequential)> _
        Public Structure TVImages
            Public AllSeasonPoster As TVDBShowPoster
            Public SeasonImageList As List(Of TVDBSeasonImage)
            Public ShowFanart As TVDBFanart
            Public ShowPoster As TVDBShowPoster
            Public Function Clone() As TVImages
                Dim images2 As New TVImages
                Try 
                    Using stream As MemoryStream = New MemoryStream
                        Dim formatter As New BinaryFormatter
                        formatter.Serialize(stream, Me)
                        stream.Position = 0
                        images2 = DirectCast(formatter.Deserialize(stream), TVImages)
                        stream.Close
                    End Using
                Catch exception1 As Exception
                    ProjectData.SetProjectError(exception1)
                    Dim exception As Exception = exception1
                    Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                    ProjectData.ClearProjectError
                End Try
                Return images2
            End Function
        End Structure

        Public Class TVSearchResults
            ' Methods
            Public Sub New()
                Me.Clear
            End Sub

            Public Sub Clear()
                Me._id = 0
                Me._name = String.Empty
                Me._aired = String.Empty
                Me._language = New TVLanguage
                Me._overview = String.Empty
                Me._banner = String.Empty
                Me._lev = 0
            End Sub


            ' Properties
            Public Property Aired As String
                Get
                    Return Me._aired
                End Get
                Set(ByVal value As String)
                    Me._aired = value
                End Set
            End Property

            Public Property Banner As String
                Get
                    Return Me._banner
                End Get
                Set(ByVal value As String)
                    Me._banner = value
                End Set
            End Property

            Public Property ID As Integer
                Get
                    Return Me._id
                End Get
                Set(ByVal value As Integer)
                    Me._id = value
                End Set
            End Property

            Public Property Language As TVLanguage
                Get
                    Return Me._language
                End Get
                Set(ByVal value As TVLanguage)
                    Me._language = value
                End Set
            End Property

            Public Property Lev As Integer
                Get
                    Return Me._lev
                End Get
                Set(ByVal value As Integer)
                    Me._lev = value
                End Set
            End Property

            Public Property Name As String
                Get
                    Return Me._name
                End Get
                Set(ByVal value As String)
                    Me._name = value
                End Set
            End Property

            Public Property Overview As String
                Get
                    Return Me._overview
                End Get
                Set(ByVal value As String)
                    Me._overview = value
                End Set
            End Property


            ' Fields
            Private _aired As String
            Private _banner As String
            Private _id As Integer
            Private _language As TVLanguage
            Private _lev As Integer
            Private _name As String
            Private _overview As String
        End Class
    End Class
End Namespace

