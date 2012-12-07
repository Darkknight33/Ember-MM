Imports EmberAPI
Imports EmberAPI.MediaContainers
Imports EmberScraperModule
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.IO
Imports System.Linq
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Web
Imports System.Windows.Forms

Namespace EmberScraperModule.IMDB
    Public Class Scraper
        ' Events
        Public Custom Event Exception As ExceptionEventHandler
        Public Custom Event SearchMovieInfoDownloaded As SearchMovieInfoDownloadedEventHandler
        Public Custom Event SearchResultsDownloaded As SearchResultsDownloadedEventHandler

        ' Methods
        Public Sub New()
            Scraper.__ENCAddToList(Me)
            Me.bwIMDB = New BackgroundWorker
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
        Private Shared Function _Lambda$__46(ByVal M As Object) As String
            Return String.Format("{0}:{1}", DirectCast(M, Match).Groups.Item(1).ToString.Trim, DirectCast(M, Match).Groups.Item(2).ToString.Trim)
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__47(ByVal N As String) As String
            Return N
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__48(ByVal N As String) As Boolean
            Return N.Contains(Master.eSettings.CertificationLang)
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__49(ByVal M As Object) As VB$AnonymousType_0(Of Object, Match)
            Return New VB$AnonymousType_0(Of Object, Match)(RuntimeHelpers.GetObjectValue(M), Regex.Match(Regex.Match(M.ToString, "<td\sclass=""nm"">(.*?)</td>").ToString, "<a.*?href=[""'](?<url>.*?)[""'].*?>(?<name>.*?)</a>"))
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__50(ByVal $VB$It1 As VB$AnonymousType_0(Of Object, Match)) As VB$AnonymousType_1(Of VB$AnonymousType_0(Of Object, Match), String)
            Return New VB$AnonymousType_1(Of VB$AnonymousType_0(Of Object, Match), String)($VB$It1, Regex.Match($VB$It1.M.ToString, "(?<=<td\sclass=""char"">)(.*?)(?=</td>)(\s\(.*?\))?").ToString)
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__51(ByVal $VB$It1 As VB$AnonymousType_1(Of VB$AnonymousType_0(Of Object, Match), String)) As VB$AnonymousType_2(Of VB$AnonymousType_1(Of VB$AnonymousType_0(Of Object, Match), String), Match)
            Return New VB$AnonymousType_2(Of VB$AnonymousType_1(Of VB$AnonymousType_0(Of Object, Match), String), Match)($VB$It1, Regex.Match(Regex.Match($VB$It1.$VB$It1.M.ToString, "<td\sclass=""hs"">(.*?)</td>").ToString, "<img src=""(?<thumb>.*?)"" width=""\d{1,3}"" height=""\d{1,3}"" border="".{1,3}"">"))
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__52(ByVal $VB$It As VB$AnonymousType_2(Of VB$AnonymousType_1(Of VB$AnonymousType_0(Of Object, Match), String), Match)) As Person
            Return New Person(HttpUtility.HtmlDecode($VB$It.$VB$It1.$VB$It1.m1.Groups.Item("name").ToString.Trim), HttpUtility.HtmlDecode($VB$It.$VB$It1.m2.ToString.Trim), If((($VB$It.m3.Groups.Item("thumb").ToString.IndexOf("addtiny") > 0) OrElse ($VB$It.m3.Groups.Item("thumb").ToString.IndexOf("no_photo") > 0)), String.Empty, Strings.Replace(HttpUtility.HtmlDecode($VB$It.m3.Groups.Item("thumb").ToString.Trim), "._SY30_SX23_.jpg", "._SY275_SX400_.jpg", 1, -1, CompareMethod.Binary)))
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__53(ByVal p As Person) As Boolean
            Return Not String.IsNullOrEmpty(p.Thumb)
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__54(ByVal M As Object) As Boolean
            Return Not DirectCast(M, Match).Groups.Item("name").ToString.Contains("more")
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__55(ByVal M As Object) As String
            Return HttpUtility.HtmlDecode(DirectCast(M, Match).Groups.Item("name").ToString)
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__56(ByVal M As Object) As Boolean
            Return Not DirectCast(M, Match).Groups.Item("name").ToString.Contains("more")
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__57(ByVal M As Object) As String
            Return HttpUtility.HtmlDecode(DirectCast(M, Match).Groups.Item("name").ToString)
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__58(ByVal M As Object) As String
            Return HttpUtility.HtmlDecode(DirectCast(M, Match).Groups.Item("name").ToString)
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__59(ByVal N As String) As Boolean
            Return Not N.Contains("more")
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__60(ByVal P1 As Object) As Boolean
            Return (DirectCast(P1, Match).Groups.Item("name").ToString <> String.Empty)
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__61(ByVal P1 As Object) As String
            Return HttpUtility.HtmlDecode(DirectCast(P1, Match).Groups.Item("name").ToString)
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__62(ByVal M As Object) As Boolean
            Return (((DirectCast(M, Match).Groups.Item("name").ToString.Trim <> "more") AndAlso (DirectCast(M, Match).Groups.Item("name").ToString.Trim <> "(more)")) AndAlso ((DirectCast(M, Match).Groups.Item("name").ToString.Trim <> "WGA") AndAlso Not DirectCast(M, Match).Groups.Item("name").ToString.Trim.Contains("see more")))
        End Function

        <CompilerGenerated> _
        Private Function _Lambda$__64(ByVal Po As Object) As Boolean
            Return Not Po.ToString.Contains(("http://" & Me.IMDBURL & "/Glossary/"))
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__65(ByVal Po As Object) As VB$AnonymousType_3(Of Object, Match)
            Return New VB$AnonymousType_3(Of Object, Match)(RuntimeHelpers.GetObjectValue(Po), Regex.Match(Po.ToString, "<a\shref=[""""'](?<url>.*?)[""""'].*?>(?<name>.*?)</a>"))
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__66(ByVal $VB$It As VB$AnonymousType_3(Of Object, Match)) As Boolean
            Return Not String.IsNullOrEmpty($VB$It.P1.Groups.Item("name").ToString)
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__67(ByVal $VB$It As VB$AnonymousType_3(Of Object, Match)) As String
            Return HttpUtility.HtmlDecode(($VB$It.P1.Groups.Item("name").ToString & " (producer)"))
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__68(ByVal Mo As Object) As VB$AnonymousType_4(Of Object, Match)
            Return New VB$AnonymousType_4(Of Object, Match)(RuntimeHelpers.GetObjectValue(Mo), Regex.Match(Mo.ToString, "<a.*?href=[""'](?<url>.*?)[""'].*?>(?<name>.*?)</a>"))
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__69(ByVal $VB$It As VB$AnonymousType_4(Of Object, Match)) As Boolean
            Return Not String.IsNullOrEmpty($VB$It.M1.Groups.Item("name").ToString)
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__70(ByVal $VB$It As VB$AnonymousType_4(Of Object, Match)) As String
            Return HttpUtility.HtmlDecode(($VB$It.M1.Groups.Item("name").ToString & " (music by)"))
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__71(ByVal P1 As Object) As Boolean
            Return Not String.IsNullOrEmpty(DirectCast(P1, Match).Groups.Item("name").ToString)
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__72(ByVal P1 As Object) As String
            Return HttpUtility.HtmlDecode(DirectCast(P1, Match).Groups.Item("name").ToString)
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__73(ByVal P1 As Object) As Boolean
            Return (DirectCast(P1, Match).Groups.Item("name").ToString <> String.Empty)
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__74(ByVal P1 As Object) As String
            Return HttpUtility.HtmlDecode(DirectCast(P1, Match).Groups.Item("name").ToString)
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__75(ByVal Mtr As Object) As Boolean
            Return (Not DirectCast(Mtr, Match).Groups.Item("name").ToString.Contains("<img") AndAlso Not DirectCast(Mtr, Match).Groups.Item("type").ToString.Contains("VG"))
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__77(ByVal Mtr As Object) As Boolean
            Return (Not DirectCast(Mtr, Match).Groups.Item("name").ToString.Contains("<img") AndAlso Not DirectCast(Mtr, Match).Groups.Item("type").ToString.Contains("VG"))
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__79(ByVal Mtr As Object) As Boolean
            Return (Not DirectCast(Mtr, Match).Groups.Item("name").ToString.Contains("<img") AndAlso Not DirectCast(Mtr, Match).Groups.Item("type").ToString.Contains("VG"))
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__81(ByVal Mtr As Object) As Boolean
            Return (Not DirectCast(Mtr, Match).Groups.Item("name").ToString.Contains("<img") AndAlso Not DirectCast(Mtr, Match).Groups.Item("type").ToString.Contains("VG"))
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__83(ByVal m As Object) As String
            Return DirectCast(m, Match).Value
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__84(ByVal m As Object) As String
            Return DirectCast(m, Match).Value
        End Function

        Private Sub BW_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
            Dim result As Results = DirectCast(e.Result, Results)
            Try 
                Select Case result.ResultType
                    Case SearchType.Movies
                        Dim searchResultsDownloadedEvent As SearchResultsDownloadedEventHandler = Me.SearchResultsDownloadedEvent
                        If (Not searchResultsDownloadedEvent Is Nothing) Then
                            searchResultsDownloadedEvent.Invoke(DirectCast(result.Result, MovieSearchResults))
                        End If
                        Return
                    Case SearchType.Details
                        Return
                    Case SearchType.SearchDetails
                        Dim results2 As MovieSearchResults = DirectCast(result.Result, MovieSearchResults)
                        Dim searchMovieInfoDownloadedEvent As SearchMovieInfoDownloadedEventHandler = Me.SearchMovieInfoDownloadedEvent
                        If (Not searchMovieInfoDownloadedEvent Is Nothing) Then
                            searchMovieInfoDownloadedEvent.Invoke(Me.sPoster, result.Success)
                        End If
                        Return
                End Select
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Private Sub bwIMDB_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
            Dim argument As Arguments = DirectCast(e.Argument, Arguments)
            Try 
                Select Case argument.Search
                    Case SearchType.Movies
                        Dim results As MovieSearchResults = Me.SearchMovie(argument.Parameter)
                        Dim results3 As New Results With { _
                            .ResultType = SearchType.Movies, _
                            .Result = results _
                        }
                        e.Result = results3
                        Return
                    Case SearchType.Details
                        Return
                    Case SearchType.SearchDetails
                        Dim flag As Boolean = Me.GetMovieInfo(argument.Parameter, argument.IMDBMovie, False, False, True, argument.Options, True)
                        Dim results2 As New Results With { _
                            .ResultType = SearchType.SearchDetails, _
                            .Success = flag _
                        }
                        e.Result = results2
                        Return
                End Select
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Public Sub CancelAsync()
            If Me.bwIMDB.IsBusy Then
                Me.bwIMDB.CancelAsync
            End If
            Do While Me.bwIMDB.IsBusy
                Application.DoEvents
                Thread.Sleep(50)
            Loop
        End Sub

        Private Function CleanTitle(ByVal sString As String) As String
            Dim str As String = sString
            Try 
                If sString.StartsWith("""") Then
                    str = sString.Remove(0, 1)
                End If
                If sString.EndsWith("""") Then
                    str = str.Remove((str.Length - 1), 1)
                End If
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
            Return str
        End Function

        Private Function FindYear(ByVal tmpname As String, ByVal lst As List(Of Movie)) As Integer
            Dim expression As String = ""
            Dim num3 As Integer = -1
            tmpname = Path.GetFileNameWithoutExtension(tmpname)
            tmpname = tmpname.Replace(".", " ").Trim.Replace("(", " ").Replace(")", "").Trim
            Dim num2 As Integer = tmpname.LastIndexOf(" ")
            If (num2 >= 0) Then
                expression = tmpname.Substring((num2 + 1), ((tmpname.Length - num2) - 1))
                If (If((Versioned.IsNumeric(expression) AndAlso (Convert.ToInt32(expression) > &H79E)), 1, 0) = 0) Then
                    Return num3
                End If
                Dim num5 As Integer = (lst.Count - 1)
                Dim i As Integer = 0
                Do While (i <= num5)
                    If (lst.Item(i).Year = expression) Then
                        Return i
                    End If
                    i += 1
                Loop
            End If
            Return num3
        End Function

        Private Function GetForcedTitle(ByVal strID As String, ByVal oTitle As String) As String
            Dim str2 As String
            Dim str As String = oTitle
            Try 
                If Me.bwIMDB.CancellationPending Then
                    Return Nothing
                End If
                Dim str3 As String = New HTTP().DownloadData(String.Concat(New String() { "http://", Me.IMDBURL, "/title/tt", strID, "/releaseinfo#akas" }))
                Dim index As Integer = str3.IndexOf("<h5><a name=""akas"">Also Known As (AKA)</a></h5>")
                If (index > 0) Then
                    Dim num2 As Integer = str3.IndexOf("</table>", index)
                    Dim matchs As MatchCollection = Regex.Matches(str3.Substring(index, (num2 - index)), "<td>(?<title>.*?)</td>", (RegexOptions.IgnorePatternWhitespace Or RegexOptions.Multiline))
                    If (matchs.Count > 0) Then
                        Dim num4 As Integer = (matchs.Count - 1)
                        Dim i As Integer = 1
                        Do While (i <= num4)
                            If ((matchs.Item(i).Value.ToString.Contains(Master.eSettings.ForceTitle) AndAlso Not matchs.Item(i).Value.ToString.Contains((Master.eSettings.ForceTitle & " (working title)"))) AndAlso Not matchs.Item(i).Value.ToString.Contains((Master.eSettings.ForceTitle & " (fake working title)"))) Then
                                str = Me.CleanTitle(HttpUtility.HtmlDecode(matchs.Item((i - 1)).Groups.Item("title").Value.ToString.Trim))
                                Exit Do
                            End If
                            i = (i + 2)
                        Loop
                    End If
                End If
                str2 = str
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                str2 = str
                ProjectData.ClearProjectError
                Return str2
                ProjectData.ClearProjectError
            End Try
            Return str2
        End Function

        Private Function GetMovieID(ByVal strObj As String) As String
            Return Regex.Match(strObj, "tt\d\d\d\d\d\d\d").ToString.Replace("tt", String.Empty)
        End Function

        Public Function GetMovieInfo(ByVal strID As String, ByRef IMDBMovie As Movie, ByVal FullCrew As Boolean, ByVal FullCast As Boolean, ByVal GetPoster As Boolean, ByVal Options As ScrapeOptions, ByVal IsSearch As Boolean) As Boolean
            Dim e$__ As _Closure$__9
            Dim flag As Boolean
            e$__ = New _Closure$__9(e$__) With { _
                .$VB$Local_FullCrew = FullCrew _
            }
            Try 
                Dim index As Integer
                Dim num2 As Integer
                Dim num3 As Integer
                Dim title As String = String.Empty
                Dim outline As String = String.Empty
                Dim plot As String = String.Empty
                Dim genre As String = String.Empty
                If Me.bwIMDB.CancellationPending Then
                    Return False
                End If
                If (((Me.UseOFDBTitle AndAlso Options.bTitle) OrElse (Me.UseOFDBOutline AndAlso Options.bOutline)) OrElse ((Me.UseOFDBPlot AndAlso Options.bPlot) OrElse (Me.UseOFDBGenre AndAlso Options.bGenre))) Then
                    Dim ofdb As New OFDB(strID, IMDBMovie)
                    If (Me.UseOFDBTitle AndAlso Options.bTitle) Then
                        title = ofdb.Title
                    End If
                    If (Me.UseOFDBOutline AndAlso Options.bOutline) Then
                        outline = ofdb.Outline
                    End If
                    If (Me.UseOFDBPlot AndAlso Options.bPlot) Then
                        plot = ofdb.Plot
                    End If
                    If (Me.UseOFDBGenre AndAlso Options.bGenre) Then
                        genre = ofdb.Genre
                    End If
                End If
                If Me.bwIMDB.CancellationPending Then
                    Return False
                End If
                Dim input As String = New HTTP().DownloadData(String.Concat(New String() { "http://", Me.IMDBURL, "/title/tt", strID, "/combined" }))
                If Me.bwIMDB.CancellationPending Then
                    Return False
                End If
                Dim str7 As String = New HTTP().DownloadData(String.Concat(New String() { "http://", Me.IMDBURL, "/title/tt", strID, "/plotsummary" }))
                IMDBMovie.IMDBID = strID
                If Me.bwIMDB.CancellationPending Then
                    Return False
                End If
                Dim str6 As String = Regex.Match(input, "(?<=<(title)>).*(?=<\/\1>)").ToString
                If Options.bTitle Then
                    Dim originalTitle As String = IMDBMovie.OriginalTitle
                    IMDBMovie.OriginalTitle = Me.CleanTitle(HttpUtility.HtmlDecode(Regex.Match(str6, ".*(?=\s\(\d+.*?\))").ToString)).Trim
                    If (String.IsNullOrEmpty(IMDBMovie.Title) OrElse Not Master.eSettings.LockTitle) Then
                        If Not String.IsNullOrEmpty(title) Then
                            IMDBMovie.Title = title.Trim
                        ElseIf Not String.IsNullOrEmpty(Master.eSettings.ForceTitle) Then
                            IMDBMovie.Title = Me.GetForcedTitle(strID, IMDBMovie.OriginalTitle)
                        Else
                            IMDBMovie.Title = IMDBMovie.OriginalTitle.Trim
                        End If
                        If (String.IsNullOrEmpty(originalTitle) OrElse (originalTitle <> IMDBMovie.OriginalTitle)) Then
                            IMDBMovie.SortTitle = String.Empty
                        End If
                    End If
                End If
                If Me.bwIMDB.CancellationPending Then
                    Return False
                End If
                If GetPoster Then
                    Me.sPoster = Regex.Match(Regex.Match(input, "(?<=\b(name=""poster"")).*\b[</a>]\b").ToString, "(?<=\b(src=)).*\b(?=[</a>])").ToString.Replace("""", String.Empty).Replace("/></", String.Empty)
                End If
                If Me.bwIMDB.CancellationPending Then
                    Return False
                End If
                If Options.bYear Then
                    IMDBMovie.Year = Regex.Match(str6, "(?<=\()\d+(?=.*\))", RegexOptions.RightToLeft).ToString
                End If
                If Options.bMPAA Then
                    num2 = If((input.IndexOf("<h5><a href=""/mpaa"">MPAA</a>:</h5>") > 0), input.IndexOf("<h5><a href=""/mpaa"">MPAA</a>:</h5>"), 0)
                    index = If((num2 > 0), input.IndexOf("<div class=""info-content"">", num2), 0)
                    num3 = If((index > 0), input.IndexOf("</div", index), 0)
                    IMDBMovie.MPAA = If(((index > 0) AndAlso (num3 > 0)), HttpUtility.HtmlDecode(input.Substring(index, (num3 - index)).Remove(0, &H1A)).Trim, String.Empty)
                End If
                If Me.bwIMDB.CancellationPending Then
                    Return False
                End If
                If Options.bCert Then
                    index = input.IndexOf("<h5>Certification:</h5>")
                    If (index > 0) Then
                        num3 = input.IndexOf("</div>", index)
                        Dim source As MatchCollection = Regex.Matches(input.Substring(index, (num3 - index)), "<a href=""/search/title\?certificates=[^""]*"">([^<]*):([^<]*)</a>[^<]*(<i>([^<]*)</i>)?")
                        If (source.Count > 0) Then
                            Dim enumerable As IEnumerable(Of String) = source.Cast(Of Object)().Select(Of Object, String)(New Func(Of Object, String)(AddressOf Scraper._Lambda$__46)).OrderByDescending(Of String, String)(New Func(Of String, String)(AddressOf Scraper._Lambda$__47)).Where(Of String)(New Func(Of String, Boolean)(AddressOf Scraper._Lambda$__48))
                            If Not String.IsNullOrEmpty(Master.eSettings.CertificationLang) Then
                                If (enumerable.Count(Of String)() > 0) Then
                                    IMDBMovie.Certification = enumerable.ElementAtOrDefault(Of String)(0).ToString.Replace("West", String.Empty).Trim
                                    If ((Options.bMPAA AndAlso Master.eSettings.UseCertForMPAA) AndAlso ((Master.eSettings.CertificationLang <> "USA") OrElse ((Master.eSettings.CertificationLang = "USA") AndAlso String.IsNullOrEmpty(IMDBMovie.MPAA)))) Then
                                        IMDBMovie.MPAA = If((Master.eSettings.CertificationLang = "USA"), StringUtils.USACertToMPAA(IMDBMovie.Certification), If(Master.eSettings.OnlyValueForCert, IMDBMovie.Certification.Split(New Char() { Convert.ToChar(":") })(1), IMDBMovie.Certification))
                                    End If
                                End If
                            Else
                                IMDBMovie.Certification = Strings.Join(enumerable.ToArray(Of String)(), " / ").Trim
                            End If
                        End If
                    End If
                    If (String.IsNullOrEmpty(IMDBMovie.Certification) AndAlso Not String.IsNullOrEmpty(IMDBMovie.MPAA)) Then
                        IMDBMovie.Certification = IMDBMovie.MPAA
                    End If
                End If
                If Me.bwIMDB.CancellationPending Then
                    Return False
                End If
                If Options.bRelease Then
                    Dim s As String = Regex.Match(input, "\d+\s\w+\s\d\d\d\d\s").ToString.Trim
                    If (s <> String.Empty) Then
                        Dim time As DateTime
                        If DateTime.TryParse(s, time) Then
                            IMDBMovie.ReleaseDate = Strings.FormatDateTime(time, DateFormat.ShortDate).ToString
                        End If
                    Else
                        IMDBMovie.ReleaseDate = Nothing
                    End If
                End If
                If Me.bwIMDB.CancellationPending Then
                    Return False
                End If
                If (Options.bRating AndAlso (String.IsNullOrEmpty(IMDBMovie.Rating) OrElse Not Master.eSettings.LockRating)) Then
                    Dim str10 As String = Regex.Match(input, "\b\d\W\d/\d\d").ToString
                    If String.IsNullOrEmpty(str10) Then
                        IMDBMovie.Rating = String.Empty
                    Else
                        IMDBMovie.Rating = str10.Split(New Char() { Convert.ToChar("/") }).First(Of String)().Trim
                    End If
                End If
                If Me.bwIMDB.CancellationPending Then
                    Return False
                End If
                If (Options.bTrailer AndAlso (String.IsNullOrEmpty(IMDBMovie.Trailer) OrElse Not Master.eSettings.LockTrailer)) Then
                    IMDBMovie.Trailer = Me.GetTrailers(IMDBMovie.IMDBID).FirstOrDefault(Of String)()
                End If
                If Me.bwIMDB.CancellationPending Then
                    Return False
                End If
                If Options.bVotes Then
                    IMDBMovie.Votes = Regex.Match(input, "class=""tn15more"">([0-9,]+) votes</a>").Groups.Item(1).Value.Trim
                End If
                If Options.bTop250 Then
                    IMDBMovie.Top250 = Regex.Match(input, ("/chart/top\?tt" & IMDBMovie.IMDBID & """>Top 250: #([0-9]+)</a>")).Groups.Item(1).Value.Trim
                End If
                If Me.bwIMDB.CancellationPending Then
                    Return False
                End If
                If Options.bCast Then
                    Dim enumerable2 As IEnumerable(Of Person) = Regex.Matches(Regex.Match(input, "<table class=""cast"">(.*?)</table>").ToString, "<tr\sclass="".*?"">(.*?)</tr>").Cast(Of Object)().Select(Of Object, VB$AnonymousType_0(Of Object, Match))(New Func(Of Object, VB$AnonymousType_0(Of Object, Match))(AddressOf Scraper._Lambda$__49)).Select(Of VB$AnonymousType_0(Of Object, Match), VB$AnonymousType_1(Of VB$AnonymousType_0(Of Object, Match), String))(New Func(Of VB$AnonymousType_0(Of Object, Match), VB$AnonymousType_1(Of VB$AnonymousType_0(Of Object, Match), String))(AddressOf Scraper._Lambda$__50)).Select(Of VB$AnonymousType_1(Of VB$AnonymousType_0(Of Object, Match), String), VB$AnonymousType_2(Of VB$AnonymousType_1(Of VB$AnonymousType_0(Of Object, Match), String), Match))(New Func(Of VB$AnonymousType_1(Of VB$AnonymousType_0(Of Object, Match), String), VB$AnonymousType_2(Of VB$AnonymousType_1(Of VB$AnonymousType_0(Of Object, Match), String), Match))(AddressOf Scraper._Lambda$__51)).Select(Of VB$AnonymousType_2(Of VB$AnonymousType_1(Of VB$AnonymousType_0(Of Object, Match), String), Match), Person)(New Func(Of VB$AnonymousType_2(Of VB$AnonymousType_1(Of VB$AnonymousType_0(Of Object, Match), String), Match), Person)(AddressOf Scraper._Lambda$__52)).Take(Of Person)(If((Master.eSettings.ActorLimit > 0), Master.eSettings.ActorLimit, &HF423F))
                    If Master.eSettings.CastImagesOnly Then
                        enumerable2 = enumerable2.Where(Of Person)(New Func(Of Person, Boolean)(AddressOf Scraper._Lambda$__53))
                    End If
                    Dim list2 As List(Of Person) = enumerable2.ToList(Of Person)()
                    Dim person As Person
                    For Each person In list2
                        Dim enumerator As IEnumerator
                        Try 
                            enumerator = Regex.Matches(person.Role, "<a.*?href=[""'](?<url>.*?)[""'].*?>(?<name>.*?)</a>").GetEnumerator
                            Do While enumerator.MoveNext
                                Dim current As Match = DirectCast(enumerator.Current, Match)
                                person.Role = person.Role.Replace(current.Value, current.Groups.Item("name").Value.ToString.Trim)
                            Loop
                        Finally
                            If TypeOf enumerator Is IDisposable Then
                                TryCast(enumerator,IDisposable).Dispose
                            End If
                        End Try
                    Next
                    IMDBMovie.Actors = list2
                End If
                If Me.bwIMDB.CancellationPending Then
                    Return False
                End If
                index = 0
                num3 = 0
                If (Options.bTagline AndAlso (String.IsNullOrEmpty(IMDBMovie.Tagline) OrElse Not Master.eSettings.LockTagline)) Then
                    num2 = If((input.IndexOf("<h5>Tagline:</h5>") > 0), input.IndexOf("<h5>Tagline:</h5>"), 0)
                    index = If((num2 > 0), input.IndexOf("<div class=""info-content"">", num2), 0)
                    Dim num4 As Integer = If((index > 0), input.IndexOf("<a class=""tn15more inline""", index), 0)
                    Dim num5 As Integer = If((num4 > 0), num4, 0)
                    If (index > 0) Then
                        num3 = If((num5 > 0), num5, input.IndexOf("</div>", index))
                    End If
                    IMDBMovie.Tagline = If(((index > 0) AndAlso (num3 > 0)), HttpUtility.HtmlDecode(input.Substring(index, (num3 - index)).Replace("<h5>Tagline:</h5>", String.Empty).Split(ChrW(13) & ChrW(10).ToCharArray)(1)).Trim, String.Empty)
                End If
                If Me.bwIMDB.CancellationPending Then
                    Return False
                End If
                If Options.bDirector Then
                    index = If((input.IndexOf("<h5>Director:</h5>") > 0), input.IndexOf("<h5>Director:</h5>"), input.IndexOf("<h5>Directors:</h5>"))
                    num3 = If((index > 0), input.IndexOf("</div>", index), 0)
                    If ((index > 0) AndAlso (num3 > 0)) Then
                        Dim enumerable3 As IEnumerable(Of String) = Regex.Matches(input.Substring(index, (num3 - index)), "<a.*?href=[""'](?<url>.*?)[""'].*?>(?<name>.*?)</a>").Cast(Of Object)().Where(Of Object)(New Func(Of Object, Boolean)(AddressOf Scraper._Lambda$__54)).Select(Of Object, String)(New Func(Of Object, String)(AddressOf Scraper._Lambda$__55))
                        If (enumerable3.Count(Of String)() > 0) Then
                            IMDBMovie.Director = Strings.Join(enumerable3.ToArray(Of String)(), " / ").Trim
                        End If
                    End If
                End If
                If Me.bwIMDB.CancellationPending Then
                    Return False
                End If
                If Options.bCountry Then
                    index = If((input.IndexOf("<h5>Country:</h5>") > 0), input.IndexOf("<h5>Country:</h5>"), input.IndexOf("<h5>Countries:</h5>"))
                    num3 = If((index > 0), input.IndexOf("</div>", index), 0)
                    If ((index > 0) AndAlso (num3 > 0)) Then
                        Dim enumerable4 As IEnumerable(Of String) = Regex.Matches(input.Substring(index, (num3 - index)), "<a.*?href=[""'](?<url>.*?)[""'].*?>(?<name>.*?)</a>").Cast(Of Object)().Where(Of Object)(New Func(Of Object, Boolean)(AddressOf Scraper._Lambda$__56)).Select(Of Object, String)(New Func(Of Object, String)(AddressOf Scraper._Lambda$__57))
                        If (enumerable4.Count(Of String)() > 0) Then
                            IMDBMovie.Country = Strings.Join(enumerable4.ToArray(Of String)(), " / ").Trim
                        End If
                    End If
                End If
                If Me.bwIMDB.CancellationPending Then
                    Return False
                End If
                If (Options.bGenre AndAlso (String.IsNullOrEmpty(IMDBMovie.Genre) OrElse Not Master.eSettings.LockGenre)) Then
                    If Not String.IsNullOrEmpty(genre) Then
                        IMDBMovie.Genre = genre
                    Else
                        index = 0
                        num3 = 0
                        index = input.IndexOf("<h5>Genre:</h5>")
                        If (index > 0) Then
                            num3 = input.IndexOf("</div>", index)
                            If (num3 > 0) Then
                                Dim enumerable5 As IEnumerable(Of String) = Regex.Matches(input.Substring(index, (num3 - index)), "<a.*?href=[""'](?<url>.*?)[""'].*?>(?<name>.*?)</a>").Cast(Of Object)().Select(Of Object, String)(New Func(Of Object, String)(AddressOf Scraper._Lambda$__58)).Where(Of String)(New Func(Of String, Boolean)(AddressOf Scraper._Lambda$__59)).Take(Of String)(If((Master.eSettings.GenreLimit > 0), Master.eSettings.GenreLimit, &HF423F))
                                If (enumerable5.Count(Of String)() > 0) Then
                                    IMDBMovie.Genre = Strings.Join(enumerable5.ToArray(Of String)(), "/").Trim.Replace("/", " / ").Trim
                                End If
                            End If
                        End If
                    End If
                End If
                If Me.bwIMDB.CancellationPending Then
                    Return False
                End If
                If (Options.bOutline AndAlso (String.IsNullOrEmpty(IMDBMovie.Outline) OrElse Not Master.eSettings.LockOutline)) Then
                    If Not String.IsNullOrEmpty(outline) Then
                        IMDBMovie.Outline = outline
                    Else
                        index = 0
                        num3 = 0
                        Try 
                            Dim str12 As String
                            If IMDBMovie.Title.Contains("(VG)") Then
                                index = If((input.IndexOf("<h5>Plot Summary:</h5>") > 0), input.IndexOf("<h5>Plot Summary:</h5>"), input.IndexOf("<h5>Tagline:</h5>"))
                                If (index > 0) Then
                                    num3 = input.IndexOf("</div>", index)
                                End If
                            Else
                                num2 = If((input.IndexOf("<h5>Plot:</h5>") > 0), input.IndexOf("<h5>Plot:</h5>"), input.IndexOf("<h5>Plot Summary:</h5>"))
                                index = If((num2 > 0), input.IndexOf("<div class=""info-content"">", num2), 0)
                                If (index <= 0) Then
                                    index = input.IndexOf("<h5>Plot Synopsis:</h5>")
                                End If
                                If (index > 0) Then
                                    If (input.IndexOf("<a class=", index) > 0) Then
                                        num3 = input.IndexOf("</div>", index)
                                        goto Label_104C
                                    End If
                                    IMDBMovie.Outline = String.Empty
                                Else
                                    IMDBMovie.Outline = String.Empty
                                End If
                                goto Label_11B0
                            End If
                        Label_104C:
                            str12 = input.Substring(index, (num3 - index)).Remove(0, &H1A)
                            str12 = HttpUtility.HtmlDecode(If((str12.Contains("is empty") OrElse str12.Contains("View full synopsis")), String.Empty, str12.Replace("|", String.Empty).Replace("&raquo;", String.Empty)).Trim)
                            If Not String.IsNullOrEmpty(str12) Then
                                Dim enumerator3 As IEnumerator
                                Try 
                                    enumerator3 = Regex.Matches(str12, "<a.*?href=[""']/(title/tt\d{7}/|name/nm\d{7}/)[""'].*?>(?<text>.*?)</a>").GetEnumerator
                                    Do While enumerator3.MoveNext
                                        Dim match2 As Match = DirectCast(enumerator3.Current, Match)
                                        str12 = str12.Replace(match2.Value, match2.Groups.Item("text").Value.Trim)
                                    Loop
                                Finally
                                    If TypeOf enumerator3 Is IDisposable Then
                                        TryCast(enumerator3,IDisposable).Dispose
                                    End If
                                End Try
                                IMDBMovie.Outline = Regex.Replace(str12, "<a.*?href=[""'](?<url>.*?)[""'].*?>(?<name>.*?)</a>", String.Empty).Trim
                            Else
                                IMDBMovie.Outline = String.Empty
                            End If
                        Catch exception1 As Exception
                            ProjectData.SetProjectError(exception1)
                            Dim exception As Exception = exception1
                            IMDBMovie.Outline = String.Empty
                            ProjectData.ClearProjectError
                        End Try
                    End If
                End If
                If Me.bwIMDB.CancellationPending Then
                    Return False
                End If
            Label_11B0:
                If (Options.bPlot AndAlso (String.IsNullOrEmpty(IMDBMovie.Plot) OrElse Not Master.eSettings.LockPlot)) Then
                    If Not String.IsNullOrEmpty(plot) Then
                        IMDBMovie.Plot = plot
                    Else
                        Dim str15 As String = Regex.Match(str7, "<p class=.plotpar.>(.*?)</p>", &H13).Groups.Item(1).Value.ToString.Trim
                        Dim str14 As String = Regex.Match(str7, "<p class=.plotpar.>(.*?)<i>", &H13).Groups.Item(1).Value.ToString.Trim
                        Dim str13 As String = If((str14.Length < str15.Length), str14, str15)
                        If Not String.IsNullOrEmpty(str13) Then
                            Dim enumerator4 As IEnumerator
                            Try 
                                enumerator4 = Regex.Matches(str13, "<a.*?href=[""']/(title/tt\d{7}/|name/nm\d{7}/)[""'].*?>(?<text>.*?)</a>").GetEnumerator
                                Do While enumerator4.MoveNext
                                    Dim match3 As Match = DirectCast(enumerator4.Current, Match)
                                    str13 = str13.Replace(match3.Value, match3.Groups.Item("text").Value.Trim)
                                Loop
                            Finally
                                If TypeOf enumerator4 Is IDisposable Then
                                    TryCast(enumerator4,IDisposable).Dispose
                                End If
                            End Try
                            IMDBMovie.Plot = HttpUtility.HtmlDecode(str13.Replace("|", String.Empty)).Trim
                        End If
                    End If
                    If ((Master.eSettings.OutlineForPlot AndAlso String.IsNullOrEmpty(IMDBMovie.Plot)) AndAlso Not String.IsNullOrEmpty(IMDBMovie.Outline)) Then
                        IMDBMovie.Plot = IMDBMovie.Outline
                    End If
                End If
                If Me.bwIMDB.CancellationPending Then
                    Return False
                End If
                If Options.bRuntime Then
                    IMDBMovie.Runtime = HttpUtility.HtmlDecode(Regex.Match(input, "<h5>Runtime:</h5>[^0-9]*([^<]*)").Groups.Item(1).Value.Trim)
                End If
                If (Options.bStudio AndAlso (String.IsNullOrEmpty(IMDBMovie.Studio) OrElse Not Master.eSettings.LockStudio)) Then
                    index = 0
                    num3 = 0
                    If e$__.$VB$Local_FullCrew Then
                        index = input.IndexOf("<b class=""blackcatheader"">Production Companies</b>")
                        If (index > 0) Then
                            num3 = input.IndexOf("</ul>", index)
                        End If
                        If ((index > 0) AndAlso (num3 > 0)) Then
                            Dim enumerable6 As IEnumerable(Of String) = Regex.Matches(input.Substring(index, (num3 - index)), "<a.*?href=[""'](?<url>.*?)[""'].*?>(?<name>.*?)</a>").Cast(Of Object)().Where(Of Object)(New Func(Of Object, Boolean)(AddressOf Scraper._Lambda$__60)).Select(Of Object, String)(New Func(Of Object, String)(AddressOf Scraper._Lambda$__61)).Take(Of String)(1)
                            IMDBMovie.Studio = enumerable6.ElementAtOrDefault(Of String)(0).ToString.Trim
                        End If
                    Else
                        index = input.IndexOf("<h5>Company:</h5>")
                        If (index > 0) Then
                            num3 = input.IndexOf("</div>", index)
                        End If
                        If ((index > 0) AndAlso (num3 > 0)) Then
                            IMDBMovie.Studio = HttpUtility.HtmlDecode(Regex.Match(input.Substring(index, (num3 - index)), "<a.*?href=[""'](?<url>.*?)[""'].*?>(?<name>.*?)</a>").Groups.Item("name").ToString.Trim)
                        End If
                    End If
                End If
                If Me.bwIMDB.CancellationPending Then
                    Return False
                End If
                If Options.bWriters Then
                    index = 0
                    num3 = 0
                    index = input.IndexOf("<h5>Writer")
                    If (index > 0) Then
                        num3 = input.IndexOf("</div>", index)
                    End If
                    If ((index > 0) AndAlso (num3 > 0)) Then
                        Dim enumerable7 As IEnumerable(Of String) = Regex.Matches(input.Substring(index, (num3 - index)), "<a.*?href=[""'](?<url>.*?)[""'].*?>(?<name>.*?)</a>").Cast(Of Object)().Where(Of Object)(New Func(Of Object, Boolean)(AddressOf Scraper._Lambda$__62)).Select(Of Object, String)(New Func(Of Object, String)(AddressOf e$__._Lambda$__63))
                        If (enumerable7.Count(Of String)() > 0) Then
                            IMDBMovie.OldCredits = Strings.Join(enumerable7.ToArray(Of String)(), " / ").Trim
                        End If
                    End If
                End If
                If Me.bwIMDB.CancellationPending Then
                    Return False
                End If
                If e$__.$VB$Local_FullCrew Then
                    index = 0
                    num3 = 0
                    index = input.IndexOf("Directed by</a></h5>")
                    If (index > 0) Then
                        num3 = input.IndexOf("</body>", index)
                    End If
                    If ((index > 0) AndAlso (num3 > 0)) Then
                        Dim enumerator5 As IEnumerator
                        Dim matchs6 As MatchCollection = Regex.Matches(input.Substring(index, (num3 - index)), "<table.*?>\n?(.*?)</table>")
                        Try 
                            enumerator5 = matchs6.GetEnumerator
                            Do While enumerator5.MoveNext
                                Dim match4 As Match = DirectCast(enumerator5.Current, Match)
                                If Me.bwIMDB.CancellationPending Then
                                    Return False
                                End If
                                If (Options.bProducers AndAlso match4.ToString.Contains("Produced by</a></h5>")) Then
                                    Dim enumerable8 As IEnumerable(Of String) = Regex.Matches(match4.ToString, "<td\svalign=""top"">(.*?)</td>").Cast(Of Object)().Where(Of Object)(New Func(Of Object, Boolean)(AddressOf Me._Lambda$__64)).Select(Of Object, VB$AnonymousType_3(Of Object, Match))(New Func(Of Object, VB$AnonymousType_3(Of Object, Match))(AddressOf Scraper._Lambda$__65)).Where(Of VB$AnonymousType_3(Of Object, Match))(New Func(Of VB$AnonymousType_3(Of Object, Match), Boolean)(AddressOf Scraper._Lambda$__66)).Select(Of VB$AnonymousType_3(Of Object, Match), String)(New Func(Of VB$AnonymousType_3(Of Object, Match), String)(AddressOf Scraper._Lambda$__67))
                                    If (enumerable8.Count(Of String)() > 0) Then
                                        IMDBMovie.OldCredits = (IMDBMovie.OldCredits & " / " & Strings.Join(enumerable8.ToArray(Of String)(), " / ").Trim)
                                    End If
                                End If
                                If (Options.bMusicBy AndAlso match4.ToString.Contains("Original Music by</a></h5>")) Then
                                    Dim enumerable9 As IEnumerable(Of String) = Regex.Matches(match4.ToString, "<td\svalign=""top"">(.*?)</td>").Cast(Of Object)().Select(Of Object, VB$AnonymousType_4(Of Object, Match))(New Func(Of Object, VB$AnonymousType_4(Of Object, Match))(AddressOf Scraper._Lambda$__68)).Where(Of VB$AnonymousType_4(Of Object, Match))(New Func(Of VB$AnonymousType_4(Of Object, Match), Boolean)(AddressOf Scraper._Lambda$__69)).Select(Of VB$AnonymousType_4(Of Object, Match), String)(New Func(Of VB$AnonymousType_4(Of Object, Match), String)(AddressOf Scraper._Lambda$__70))
                                    If (enumerable9.Count(Of String)() > 0) Then
                                        IMDBMovie.OldCredits = (IMDBMovie.OldCredits & " / " & Strings.Join(enumerable9.ToArray(Of String)(), " / ").Trim)
                                    End If
                                End If
                            Loop
                        Finally
                            If TypeOf enumerator5 Is IDisposable Then
                                TryCast(enumerator5,IDisposable).Dispose
                            End If
                        End Try
                    End If
                    If Me.bwIMDB.CancellationPending Then
                        Return False
                    End If
                    If Options.bOtherCrew Then
                        index = input.IndexOf("<b class=""blackcatheader"">Special Effects</b>")
                        If (index > 0) Then
                            num3 = input.IndexOf("</ul>", index)
                        End If
                        If ((index > 0) AndAlso (num3 > 0)) Then
                            Dim enumerable10 As IEnumerable(Of String) = Regex.Matches(input.Substring(index, (num3 - index)), "<a.*?href=[""'](?<url>.*?)[""'].*?>(?<name>.*?)</a>").Cast(Of Object)().Where(Of Object)(New Func(Of Object, Boolean)(AddressOf Scraper._Lambda$__71)).Select(Of Object, String)(New Func(Of Object, String)(AddressOf Scraper._Lambda$__72))
                            If (enumerable10.Count(Of String)() > 0) Then
                                IMDBMovie.OldCredits = (IMDBMovie.OldCredits & " / " & Strings.Join(enumerable10.ToArray(Of String)(), " / ").Trim)
                            End If
                        End If
                    End If
                End If
                flag = True
            Catch exception3 As Exception
                ProjectData.SetProjectError(exception3)
                Dim exception2 As Exception = exception3
                Master.eLog.WriteToErrorLog(exception2.Message, exception2.StackTrace, "Error", True)
                flag = False
                ProjectData.ClearProjectError
                Return flag
                ProjectData.ClearProjectError
            End Try
            Return flag
        End Function

        Public Function GetMovieStudios(ByVal strID As String) As List(Of String)
            Dim num2 As Integer
            Dim list As New List(Of String)
            Dim str As String = New HTTP().DownloadData(String.Concat(New String() { "http://", Me.IMDBURL, "/title/tt", strID, "/combined" }))
            Dim index As Integer = str.IndexOf("<b class=""blackcatheader"">Production Companies</b>")
            If (index > 0) Then
                num2 = str.IndexOf("</ul>", index)
            End If
            If ((index > 0) AndAlso (num2 > 0)) Then
                Dim source As IEnumerable(Of String) = Regex.Matches(str.Substring(index, (num2 - index)), "<a.*?href=[""'](?<url>.*?)[""'].*?>(?<name>.*?)</a>").Cast(Of Object)().Where(Of Object)(New Func(Of Object, Boolean)(AddressOf Scraper._Lambda$__73)).Select(Of Object, String)(New Func(Of Object, String)(AddressOf Scraper._Lambda$__74))
                list.AddRange(source.ToArray(Of String)())
            End If
            Return list
        End Function

        Public Function GetSearchMovieInfo(ByVal sMovieName As String, ByRef dbMovie As DBMovie, ByVal iType As ScrapeType, ByVal Options As ScrapeOptions) As Movie
            Dim movie As Movie
            Dim res As MovieSearchResults = Me.SearchMovie(sMovieName)
            Dim flag As Boolean = False
            Dim iMDBMovie As Movie = dbMovie.Movie
            res.PopularTitles.Sort
            res.ExactMatches.Sort
            res.PartialMatches.Sort
            Try 
                Select Case CInt(iType)
                    Case 0, 1, 3, 7, 9, 11
                        Dim flag2 As Boolean = False
                        If ((((res.PopularTitles.Count > 0) AndAlso (res.PopularTitles.Item(0).Lev > 5)) OrElse (res.PopularTitles.Count = 0)) AndAlso (((res.ExactMatches.Count <= 0) OrElse (res.ExactMatches.Item(0).Lev <= 5)) AndAlso (res.ExactMatches.Count <> 0))) Then
                        End If
                        If (((res.PartialMatches.Count > 0) AndAlso (res.PartialMatches.Item(0).Lev > 5)) OrElse (res.PartialMatches.Count = 0)) Then
                            flag2 = True
                        End If
                        Dim num As Integer = Me.FindYear(dbMovie.Filename, res.ExactMatches)
                        Dim num2 As Integer = Me.FindYear(dbMovie.Filename, res.PopularTitles)
                        If (((res.ExactMatches.Count = 1) AndAlso (res.PopularTitles.Count = 0)) AndAlso (res.PartialMatches.Count = 0)) Then
                            flag = Me.GetMovieInfo(res.ExactMatches.Item(0).IMDBID, iMDBMovie, Master.eSettings.FullCrew, Master.eSettings.FullCast, False, Options, True)
                        ElseIf ((((num2 >= 0) OrElse (num = -1)) AndAlso (res.PopularTitles.Count > 0)) AndAlso ((res.PopularTitles.Item(If((num2 >= 0), num2, 0)).Lev <= 5) OrElse flag2)) Then
                            flag = Me.GetMovieInfo(res.PopularTitles.Item(If((num2 >= 0), num2, 0)).IMDBID, iMDBMovie, Master.eSettings.FullCrew, Master.eSettings.FullCast, False, Options, True)
                        ElseIf ((res.ExactMatches.Count > 0) AndAlso ((res.ExactMatches.Item(If((num >= 0), num, 0)).Lev <= 5) OrElse flag2)) Then
                            flag = Me.GetMovieInfo(res.ExactMatches.Item(If((num >= 0), num, 0)).IMDBID, iMDBMovie, Master.eSettings.FullCrew, Master.eSettings.FullCast, False, Options, True)
                        ElseIf (res.PartialMatches.Count > 0) Then
                            flag = Me.GetMovieInfo(res.PartialMatches.Item(0).IMDBID, iMDBMovie, Master.eSettings.FullCrew, Master.eSettings.FullCast, False, Options, True)
                        End If
                        goto Label_04D1
                    Case 2, 4, 8, 10, 12
                        If (If((((res.ExactMatches.Count = 1) AndAlso (res.PopularTitles.Count = 0)) AndAlso (res.PartialMatches.Count = 0)), 1, 0) = 0) Then
                            Exit Select
                        End If
                        flag = Me.GetMovieInfo(res.ExactMatches.Item(0).IMDBID, iMDBMovie, Master.eSettings.FullCrew, Master.eSettings.FullCast, False, Options, True)
                        goto Label_04D1
                    Case Else
                        goto Label_04D1
                End Select
                If ((res.PopularTitles.Count = 1) AndAlso (res.PopularTitles.Item(0).Lev <= 5)) Then
                    flag = Me.GetMovieInfo(res.PopularTitles.Item(0).IMDBID, iMDBMovie, Master.eSettings.FullCrew, Master.eSettings.FullCast, False, Options, True)
                ElseIf ((res.ExactMatches.Count = 1) AndAlso (res.ExactMatches.Item(0).Lev <= 5)) Then
                    flag = Me.GetMovieInfo(res.ExactMatches.Item(0).IMDBID, iMDBMovie, Master.eSettings.FullCrew, Master.eSettings.FullCast, False, Options, True)
                Else
                    Master.tmpMovie.Clear
                    Using results2 As dlgIMDBSearchResults = New dlgIMDBSearchResults
                        results2.IMDBURL = Me.IMDBURL
                        If (results2.ShowDialog(res, sMovieName) = DialogResult.OK) Then
                            If String.IsNullOrEmpty(Master.tmpMovie.IMDBID) Then
                                flag = False
                            Else
                                flag = Me.GetMovieInfo(Master.tmpMovie.IMDBID, iMDBMovie, Master.eSettings.FullCrew, Master.eSettings.FullCast, False, Options, True)
                            End If
                        Else
                            flag = False
                        End If
                    End Using
                End If
            Label_04D1:
                If flag Then
                    Return iMDBMovie
                End If
                movie = New Movie
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                movie = New Movie
                ProjectData.ClearProjectError
                Return movie
                ProjectData.ClearProjectError
            End Try
            Return movie
        End Function

        Public Sub GetSearchMovieInfoAsync(ByVal imdbID As String, ByVal IMDBMovie As Movie, ByVal Options As ScrapeOptions)
            Try 
                If Not Me.bwIMDB.IsBusy Then
                    Me.bwIMDB.WorkerReportsProgress = False
                    Me.bwIMDB.WorkerSupportsCancellation = True
                    Dim argument As New Arguments With { _
                        .Search = SearchType.SearchDetails, _
                        .Parameter = imdbID, _
                        .IMDBMovie = IMDBMovie, _
                        .Options = Options _
                    }
                    Me.bwIMDB.RunWorkerAsync(argument)
                End If
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Public Function GetTrailers(ByVal imdbID As String) As List(Of String)
            Dim list2 As New List(Of String)
            Dim num2 As Integer = 0
            Dim num As Integer = 0
            Dim http As New HTTP
            Dim str As String = String.Empty
            str = http.DownloadData(String.Concat(New String() { "http://", Me.IMDBURL, "/title/tt", imdbID, "/videogallery/content_type-Trailer" }))
            If str.ToLower.Contains("page not found") Then
                str = String.Empty
            End If
            If Not String.IsNullOrEmpty(str) Then
                Dim match As Match = Regex.Match(str, "of [0-9]{1,3}")
                If Not match.Success Then
                    Return list2
                End If
                num2 = Convert.ToInt32(match.Value.Substring(3))
                If (num2 <= 0) Then
                    Return list2
                End If
                num = Convert.ToInt32(Math.Ceiling(CDbl((CDbl(num2) / 10))))
                Dim num4 As Integer = num
                Dim i As Integer = 1
                Do While (i <= num4)
                    If (i <> 1) Then
                        str = http.DownloadData(String.Concat(New Object() { "http://", Me.IMDBURL, "/title/tt", imdbID, "/videogallery/content_type-Trailer?page=", i }))
                    End If
                    Dim first As String() = Regex.Matches(str, "screenplay/(vi[0-9]+)/").Cast(Of Object)().Select(Of Object, String)(New Func(Of Object, String)(AddressOf Scraper._Lambda$__83)).Distinct(Of String)().ToArray(Of String)()
                    Dim source As MatchCollection = Regex.Matches(str, "imdb/(vi[0-9]+)/")
                    Dim str4 As String
                    For Each str4 In first.Concat(Of String)(source.Cast(Of Object)().Select(Of Object, String)(New Func(Of Object, String)(AddressOf Scraper._Lambda$__84)).Distinct(Of String)().ToArray(Of String)()).ToArray(Of String)()
                        If str4.Contains("screenplay") Then
                            Dim str3 As String = HttpUtility.UrlDecode(Regex.Match(http.DownloadData(String.Concat(New String() { "http://", Me.IMDBURL, "/video/", str4, "player" })), "http.+mp4").Value)
                            If (Not String.IsNullOrEmpty(str3) AndAlso http.IsValidURL(str3)) Then
                                list2.Add(str3)
                            End If
                        End If
                    Next
                    i += 1
                Loop
            End If
            Return list2
        End Function

        Private Function SearchMovie(ByVal sMovie As String) As MovieSearchResults
            Dim e$__ As _Closure$__10
            Dim results As MovieSearchResults
            e$__ = New _Closure$__10(e$__) With { _
                .$VB$Local_sMovie = sMovie, _
                .$VB$Me = Me _
            }
            Try 
                Dim num2 As Integer
                Dim results2 As New MovieSearchResults
                Dim http As New HTTP
                Dim input As String = http.DownloadData(String.Concat(New String() { "http://", Me.IMDBURL, "/find?q=", HttpUtility.UrlEncode(e$__.$VB$Local_sMovie, Encoding.GetEncoding("ISO-8859-1")), "&s=all" }))
                Dim str3 As String = http.DownloadData(String.Concat(New String() { "http://", Me.IMDBURL, "/find?q=", HttpUtility.UrlEncode(e$__.$VB$Local_sMovie, Encoding.GetEncoding("ISO-8859-1")), "&s=tt&ttype=ft&ref_=fn_ft" }))
                Dim str2 As String = http.DownloadData(String.Concat(New String() { "http://", Me.IMDBURL, "/find?q=", HttpUtility.UrlEncode(e$__.$VB$Local_sMovie, Encoding.GetEncoding("ISO-8859-1")), "&s=tt&ttype=ft&exact=true&ref_=fn_tt_ex" }))
                Dim responseUri As String = http.ResponseUri
                http = Nothing
                If Regex.IsMatch(responseUri, "tt\d\d\d\d\d\d\d") Then
                    Dim item As New Movie(Regex.Match(responseUri, "tt\d\d\d\d\d\d\d").ToString, StringUtils.ProperCase(e$__.$VB$Local_sMovie), Regex.Match(Regex.Match(input, "(?<=<(title)>).*(?=<\/\1>)").ToString, "(?<=\()\d+(?=.*\))").ToString, 0)
                    results2.ExactMatches.Add(item)
                    Return results2
                End If
                Dim index As Integer = input.IndexOf("</a>Titles</h3>")
                If (index > 0) Then
                    num2 = (input.IndexOf("</table>", index) + 8)
                    results2.PopularTitles = Regex.Matches(Regex.Match(input.Substring(index, (num2 - index)), "<table.*?>\n?(.*?)</table>").ToString, "<a\shref=[""""'](?<url>.*?)[""""'].*?>(?<name>.*?)</a>((\s)+?(\((?<year>\d{4})(\/.*?)?\)))?((\s)+?(\((?<type>.*?)\)))?").Cast(Of Object)().Where(Of Object)(New Func(Of Object, Boolean)(AddressOf Scraper._Lambda$__75)).Select(Of Object, Movie)(New Func(Of Object, Movie)(AddressOf e$__._Lambda$__76)).ToList(Of Movie)()
                End If
                index = str3.IndexOf("</a>Titles</h3>")
                If (index > 0) Then
                    num2 = (str3.IndexOf("</table>", index) + 8)
                    results2.PartialMatches = Regex.Matches(Regex.Match(str3.Substring(index, (num2 - index)), "<table.*?>\n?(.*?)</table>").ToString, "<a\shref=[""""'](?<url>.*?)[""""'].*?>(?<name>.*?)</a>((\s)+?(\((?<year>\d{4})(\/.*?)?\)))?((\s)+?(\((?<type>.*?)\)))?").Cast(Of Object)().Where(Of Object)(New Func(Of Object, Boolean)(AddressOf Scraper._Lambda$__77)).Select(Of Object, Movie)(New Func(Of Object, Movie)(AddressOf e$__._Lambda$__78)).ToList(Of Movie)()
                End If
                index = input.IndexOf("Titles (Approx Matches)")
                If (index > 0) Then
                    num2 = (input.IndexOf("</table>", index) + 8)
                    Dim source As IEnumerable(Of Movie) = Regex.Matches(Regex.Match(input.Substring(index, (num2 - index)), "<table.*?>\n?(.*?)</table>").ToString, "<a\shref=[""""'](?<url>.*?)[""""'].*?>(?<name>.*?)</a>((\s)+?(\((?<year>\d{4})(\/.*?)?\)))?((\s)+?(\((?<type>.*?)\)))?").Cast(Of Object)().Where(Of Object)(New Func(Of Object, Boolean)(AddressOf Scraper._Lambda$__79)).Select(Of Object, Movie)(New Func(Of Object, Movie)(AddressOf e$__._Lambda$__80))
                    If Not Information.IsNothing(results2.PartialMatches) Then
                        results2.PartialMatches = results2.PartialMatches.Union(Of Movie)(source.ToList(Of Movie)()).ToList(Of Movie)()
                    Else
                        results2.PartialMatches = source.ToList(Of Movie)()
                    End If
                End If
                index = str2.IndexOf("</a>Titles</h3>")
                If (index > 0) Then
                    num2 = (str2.IndexOf("</table>", index) + 8)
                    results2.ExactMatches = Regex.Matches(Regex.Match(str2.Substring(index, (num2 - index)), "<table.*?>\n?(.*?)</table>").ToString, "<a\shref=[""""'](?<url>.*?)[""""'].*?>(?<name>.*?)</a>((\s)+?(\((?<year>\d{4})(\/.*?)?\)))?((\s)+?(\((?<type>.*?)\)))?").Cast(Of Object)().Where(Of Object)(New Func(Of Object, Boolean)(AddressOf Scraper._Lambda$__81)).Select(Of Object, Movie)(New Func(Of Object, Movie)(AddressOf e$__._Lambda$__82)).ToList(Of Movie)()
                End If
                results = results2
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                results = Nothing
                ProjectData.ClearProjectError
                Return results
                ProjectData.ClearProjectError
            End Try
            Return results
        End Function

        Public Sub SearchMovieAsync(ByVal sMovie As String, ByVal filterOptions As ScrapeOptions)
            Try 
                If Not Me.bwIMDB.IsBusy Then
                    Me.bwIMDB.WorkerReportsProgress = False
                    Me.bwIMDB.WorkerSupportsCancellation = True
                    Dim argument As New Arguments With { _
                        .Search = SearchType.Movies, _
                        .Parameter = sMovie, _
                        .Options = filterOptions _
                    }
                    Me.bwIMDB.RunWorkerAsync(argument)
                End If
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub


        ' Properties
        Friend Overridable Property bwIMDB As BackgroundWorker
            <DebuggerNonUserCode> _
            Get
                Return Me._bwIMDB
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As BackgroundWorker)
                Dim handler As DoWorkEventHandler = New DoWorkEventHandler(AddressOf Me.bwIMDB_DoWork)
                Dim handler2 As RunWorkerCompletedEventHandler = New RunWorkerCompletedEventHandler(AddressOf Me.BW_RunWorkerCompleted)
                If (Not Me._bwIMDB Is Nothing) Then
                    RemoveHandler Me._bwIMDB.DoWork, handler
                    RemoveHandler Me._bwIMDB.RunWorkerCompleted, handler2
                End If
                Me._bwIMDB = WithEventsValue
                If (Not Me._bwIMDB Is Nothing) Then
                    AddHandler Me._bwIMDB.DoWork, handler
                    AddHandler Me._bwIMDB.RunWorkerCompleted, handler2
                End If
            End Set
        End Property


        ' Fields
        Private Shared __ENCList As List(Of WeakReference) = New List(Of WeakReference)
        <AccessedThroughProperty("bwIMDB")> _
        Private _bwIMDB As BackgroundWorker
        Private Const ACTORTABLE_PATTERN As String = "<table class=""cast"">(.*?)</table>"
        Private Const HREF_PATTERN As String = "<a.*?href=[""'](?<url>.*?)[""'].*?>(?<name>.*?)</a>"
        Private Const HREF_PATTERN_2 As String = "<a\shref=[""""'](?<url>.*?)[""""'].*?>(?<name>.*?)</a>"
        Private Const HREF_PATTERN_3 As String = "<a href=""/search/title\?certificates=[^""]*"">([^<]*):([^<]*)</a>[^<]*(<i>([^<]*)</i>)?"
        Private Const HREF_PATTERN_4 As String = "<a.*?href=[""']/(title/tt\d{7}/|name/nm\d{7}/)[""'].*?>(?<text>.*?)</a>"
        Private Const IMDB_ID_REGEX As String = "tt\d\d\d\d\d\d\d"
        Public IMDBURL As String
        Private Const IMG_PATTERN As String = "<img src=""(?<thumb>.*?)"" width=""\d{1,3}"" height=""\d{1,3}"" border="".{1,3}"">"
        Private Const LINK_PATTERN As String = "<a[\s]+[^>]*?href[\s]?=[\s\""\']*(?<url>.*?)[\""\']*.*?>(?<name>[^<]+|.*?)?<\/a>"
        Private Const MOVIE_TITLE_PATTERN As String = "(?<=<(title)>).*(?=<\/\1>)"
        Private sPoster As String
        Private Const TABLE_PATTERN As String = "<table.*?>\n?(.*?)</table>"
        Private Const TD_PATTERN_1 As String = "<td\sclass=""nm"">(.*?)</td>"
        Private Const TD_PATTERN_2 As String = "(?<=<td\sclass=""char"">)(.*?)(?=</td>)(\s\(.*?\))?"
        Private Const TD_PATTERN_3 As String = "<td\sclass=""hs"">(.*?)</td>"
        Private Const TD_PATTERN_4 As String = "<td>(?<title>.*?)</td>"
        Private Const TITLE_PATTERN As String = "<a\shref=[""""'](?<url>.*?)[""""'].*?>(?<name>.*?)</a>((\s)+?(\((?<year>\d{4})(\/.*?)?\)))?((\s)+?(\((?<type>.*?)\)))?"
        Private Const TR_PATTERN As String = "<tr\sclass="".*?"">(.*?)</tr>"
        Public UseOFDBGenre As Boolean
        Public UseOFDBOutline As Boolean
        Public UseOFDBPlot As Boolean
        Public UseOFDBTitle As Boolean

        ' Nested Types
        <CompilerGenerated> _
        Friend Class _Closure$__10
            ' Methods
            <DebuggerNonUserCode> _
            Public Sub New()
            End Sub

            <DebuggerNonUserCode> _
            Public Sub New(ByVal other As _Closure$__10)
                If (Not other Is Nothing) Then
                    Me.$VB$Local_sMovie = other.$VB$Local_sMovie
                    Me.$VB$Me = other.$VB$Me
                End If
            End Sub

            <CompilerGenerated> _
            Public Function _Lambda$__76(ByVal Mtr As Object) As Movie
                Return New Movie(Me.$VB$Me.GetMovieID(DirectCast(Mtr, Match).Groups.Item("url").ToString), HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups.Item("name").ToString), HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups.Item("year").ToString), StringUtils.ComputeLevenshtein(StringUtils.FilterYear(Me.$VB$Local_sMovie).ToLower, StringUtils.FilterYear(HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups.Item("name").ToString)).ToLower))
            End Function

            <CompilerGenerated> _
            Public Function _Lambda$__78(ByVal Mtr As Object) As Movie
                Return New Movie(Me.$VB$Me.GetMovieID(DirectCast(Mtr, Match).Groups.Item("url").ToString), HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups.Item("name").ToString), HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups.Item("year").ToString), StringUtils.ComputeLevenshtein(StringUtils.FilterYear(Me.$VB$Local_sMovie).ToLower, StringUtils.FilterYear(HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups.Item("name").ToString)).ToLower))
            End Function

            <CompilerGenerated> _
            Public Function _Lambda$__80(ByVal Mtr As Object) As Movie
                Return New Movie(Me.$VB$Me.GetMovieID(DirectCast(Mtr, Match).Groups.Item("url").ToString), HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups.Item("name").ToString), HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups.Item("year").ToString), StringUtils.ComputeLevenshtein(StringUtils.FilterYear(Me.$VB$Local_sMovie).ToLower, StringUtils.FilterYear(HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups.Item("name").ToString)).ToLower))
            End Function

            <CompilerGenerated> _
            Public Function _Lambda$__82(ByVal Mtr As Object) As Movie
                Return New Movie(Me.$VB$Me.GetMovieID(DirectCast(Mtr, Match).Groups.Item("url").ToString), HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups.Item("name").ToString.ToString), HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups.Item("year").ToString), StringUtils.ComputeLevenshtein(StringUtils.FilterYear(Me.$VB$Local_sMovie).ToLower, StringUtils.FilterYear(HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups.Item("name").ToString)).ToLower))
            End Function


            ' Fields
            Public $VB$Local_sMovie As String
            Public $VB$Me As Scraper
        End Class

        <CompilerGenerated> _
        Friend Class _Closure$__9
            ' Methods
            <DebuggerNonUserCode> _
            Public Sub New()
            End Sub

            <DebuggerNonUserCode> _
            Public Sub New(ByVal other As _Closure$__9)
                If (Not other Is Nothing) Then
                    Me.$VB$Local_FullCrew = other.$VB$Local_FullCrew
                End If
            End Sub

            <CompilerGenerated> _
            Public Function _Lambda$__63(ByVal M As Object) As String
                Return HttpUtility.HtmlDecode((DirectCast(M, Match).Groups.Item("name").ToString & If(Me.$VB$Local_FullCrew, " (writer)", String.Empty)))
            End Function


            ' Fields
            Public $VB$Local_FullCrew As Boolean
        End Class

        <StructLayout(LayoutKind.Sequential)> _
        Private Structure Arguments
            Public FullCast As Boolean
            Public FullCrew As Boolean
            Public IMDBMovie As Movie
            Public Options As ScrapeOptions
            Public Parameter As String
            Public Search As SearchType
        End Structure

        Public Delegate Sub ExceptionEventHandler(ByVal ex As Exception)

        <StructLayout(LayoutKind.Sequential)> _
        Private Structure Results
            Public Result As Object
            Public ResultType As SearchType
            Public Success As Boolean
        End Structure

        Public Delegate Sub SearchMovieInfoDownloadedEventHandler(ByVal sPoster As String, ByVal bSuccess As Boolean)

        Public Delegate Sub SearchResultsDownloadedEventHandler(ByVal mResults As MovieSearchResults)

        Private Enum SearchType
            ' Fields
            Details = 1
            Movies = 0
            SearchDetails = 2
        End Enum
    End Class
End Namespace

