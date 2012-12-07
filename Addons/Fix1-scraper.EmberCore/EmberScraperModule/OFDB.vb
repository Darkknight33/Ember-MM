Imports EmberAPI
Imports EmberAPI.MediaContainers
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.CompilerServices
Imports System.Text.RegularExpressions
Imports System.Web

Namespace EmberScraperModule
    Public Class OFDB
        ' Methods
        Public Sub New(ByVal sID As String, ByRef mMovie As Movie)
            Me.Clear
            Me.imdbID = sID
            Me.OFDBMovie = mMovie
            Me.GetOFDBDetails
        End Sub

        <CompilerGenerated> _
        Private Shared Function _Lambda$__85(ByVal M As Object) As String
            Return HttpUtility.HtmlDecode(DirectCast(M, Match).Groups.Item("name").ToString)
        End Function

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

        Private Sub Clear()
            Me._title = String.Empty
            Me._outline = String.Empty
            Me._plot = String.Empty
            Me._genre = String.Empty
        End Sub

        Private Function GetFullPlot(ByVal sURL As String) As String
            Dim str As String = String.Empty
            Try 
                If String.IsNullOrEmpty(sURL) Then
                    Return str
                End If
                Dim str3 As String = New HTTP().DownloadData(sURL)
                Dim index As Integer = str3.IndexOf("Eine Inhaltsangabe von")
                If (index <= 0) Then
                    Return str
                End If
                Dim length As Integer = str3.Length
                Dim str4 As String = str3.Substring((index + &H16), (length - (index + &H16))).Trim
                Dim num3 As Integer = str4.IndexOf("</b></b><br><br>")
                If (num3 > 0) Then
                    Dim num As Integer = str4.IndexOf("</font></p>", CInt((num3 + &H10)))
                    str = HttpUtility.HtmlDecode(str4.Substring((num3 + &H10), (num - (num3 + &H10))).Replace("<br />", String.Empty).Replace(ChrW(13) & ChrW(10), " ").Trim)
                End If
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
            Return str
        End Function

        Private Sub GetOFDBDetails()
            Dim oFDBUrlFromIMDBID As String = Me.GetOFDBUrlFromIMDBID
            Try 
                If Not String.IsNullOrEmpty(oFDBUrlFromIMDBID) Then
                    Dim http As New HTTP
                    Dim str2 As String = http.DownloadData(oFDBUrlFromIMDBID)
                    http = Nothing
                    If Not String.IsNullOrEmpty(str2) Then
                        Dim index As Integer
                        Dim num3 As Integer
                        If (String.IsNullOrEmpty(Me.OFDBMovie.Title) OrElse Not Master.eSettings.LockTitle) Then
                            Dim str4 As String = Me.CleanTitle(HttpUtility.HtmlDecode(Regex.Match(str2, "<td width=""99\%""><h2><font face=""Arial,Helvetica,sans-serif"" size=""3""><b>([^<]+)</b></font></h2></td>").Groups.Item(1).Value.ToString))
                            Me._title = str4
                        End If
                        If (String.IsNullOrEmpty(Me.OFDBMovie.Outline) OrElse Not Master.eSettings.LockOutline) Then
                            index = str2.IndexOf("<b>Inhalt:</b>")
                            If (index > 0) Then
                                num3 = str2.IndexOf("<a href=""", CInt((index + 14)))
                                Me._outline = HttpUtility.HtmlDecode(str2.Substring((index + 14), (num3 - (index + 14))).Replace("<br />", String.Empty).Replace(ChrW(13) & ChrW(10), " ").Trim)
                            End If
                        End If
                        index = 0
                        num3 = 0
                        If (String.IsNullOrEmpty(Me.OFDBMovie.Plot) OrElse Not Master.eSettings.LockPlot) Then
                            index = str2.IndexOf("<b>Inhalt:</b>")
                            If (index > 0) Then
                                Dim length As Integer = str2.Length
                                Dim str3 As String = str2.Substring((index + 14), (length - (index + 14))).Trim
                                num3 = str3.IndexOf("<a href=""")
                                If (num3 > 0) Then
                                    Dim num As Integer = str3.IndexOf("""><b>[mehr]</b>", CInt((num3 + 9)))
                                    If (num > 0) Then
                                        Dim fullPlot As String = Me.GetFullPlot(("http://www.ofdb.de/" & str3.Substring((num3 + 9), (num - (num3 + 9)))))
                                        If Not String.IsNullOrEmpty(fullPlot) Then
                                            Me._plot = fullPlot
                                        End If
                                    End If
                                End If
                            End If
                        End If
                        index = 0
                        num3 = 0
                        If (String.IsNullOrEmpty(Me.OFDBMovie.Genre) OrElse Not Master.eSettings.LockGenre) Then
                            index = str2.IndexOf("class=""Normal"">Genre(s):</font></td>")
                            If (index > 0) Then
                                num3 = str2.IndexOf("</table>", index)
                                If (num3 > 0) Then
                                    Dim source As IEnumerable(Of String) = Regex.Matches(str2.Substring(index, (num3 - index)), "<a.*?href=[""'](?<url>.*?)[""'].*?>(?<name>.*?)</a>").Cast(Of Object)().Select(Of Object, String)(New Func(Of Object, String)(AddressOf OFDB._Lambda$__85))
                                    If (source.Count(Of String)() > 0) Then
                                        Dim str6 As String = Strings.Join(source.ToArray(Of String)(), "/").Trim
                                        Me._genre = Strings.Join(str6.Split(New Char() { Convert.ToChar("/") }), " / ").Trim
                                    End If
                                End If
                            End If
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

        Private Function GetOFDBUrlFromIMDBID() As String
            Dim str2 As String = String.Empty
            Try 
                Dim str3 As String = New HTTP().DownloadData(("http://www.ofdb.de/view.php?SText=" & Me.imdbID & "&Kat=IMDb&page=suchergebnis&sourceid=mozilla-search"))
                If String.IsNullOrEmpty(str3) Then
                    Return str2
                End If
                Dim matchs As MatchCollection = Regex.Matches(str3, "<a href=""film/([^<]+)"" onmouseover")
                If (matchs.Count > 0) Then
                    str2 = ("http://www.ofdb.de/" & Regex.Match(matchs.Item(0).Value.ToString, """(film/([^<]+))""").Groups.Item(1).Value.ToString)
                End If
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
            Return str2
        End Function


        ' Properties
        Public Property Genre As String
            Get
                Return Me._genre
            End Get
            Set(ByVal value As String)
                Me._genre = value
            End Set
        End Property

        Public Property Outline As String
            Get
                Return Me._outline
            End Get
            Set(ByVal value As String)
                Me._outline = value
            End Set
        End Property

        Public Property Plot As String
            Get
                Return Me._plot
            End Get
            Set(ByVal value As String)
                Me._plot = value
            End Set
        End Property

        Public Property Title As String
            Get
                Return Me._title
            End Get
            Set(ByVal value As String)
                Me._title = value
            End Set
        End Property


        ' Fields
        Private _genre As String
        Private _outline As String
        Private _plot As String
        Private _title As String
        Private imdbID As String
        Private OFDBMovie As Movie
    End Class
End Namespace

