Imports EmberAPI
Imports EmberAPI.FileUtils
Imports EmberAPI.MediaContainers
Imports EmberScraperModule.IMDB
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

Namespace EmberScraperModule
    Public Class EmberNativeScraperModule
        Implements EmberMovieScraperModule
        ' Events
        Public Custom Event EmberAPI.Interfaces+EmberMovieScraperModule.ModuleSettingsChanged As ModuleSettingsChangedEventHandler
        Public Custom Event EmberAPI.Interfaces+EmberMovieScraperModule.MovieScraperEvent As MovieScraperEventEventHandler
        Public Custom Event EmberAPI.Interfaces+EmberMovieScraperModule.PostScraperSetupChanged As PostScraperSetupChangedEventHandler
        Public Custom Event EmberAPI.Interfaces+EmberMovieScraperModule.ScraperSetupChanged As ScraperSetupChangedEventHandler
        Public Custom Event ModuleSettingsChanged As ModuleSettingsChangedEventHandler
        Public Custom Event MovieScraperEvent As MovieScraperEventEventHandler
        Public Custom Event SetupPostScraperChanged As PostScraperSetupChangedEventHandler
        Public Custom Event SetupScraperChanged As ScraperSetupChangedEventHandler

        ' Methods
        Public Sub New()
            EmberNativeScraperModule.__ENCAddToList(Me)
            Me.dFImgSelect = Nothing
            Me.IMDB = New Scraper
            Me.MySettings = New _MySettings
            Me._Name = "Ember Native Movie Scrapers"
            Me._PostScraperEnabled = False
            Me._ScraperEnabled = False
        End Sub

        <DebuggerNonUserCode> _
        Private Shared Sub __ENCAddToList(ByVal value As Object)
            Dim list As List(Of WeakReference) = EmberNativeScraperModule.__ENCList
            SyncLock list
                If (EmberNativeScraperModule.__ENCList.Count = EmberNativeScraperModule.__ENCList.Capacity) Then
                    Dim index As Integer = 0
                    Dim num3 As Integer = (EmberNativeScraperModule.__ENCList.Count - 1)
                    Dim i As Integer = 0
                    Do While (i <= num3)
                        Dim reference As WeakReference = EmberNativeScraperModule.__ENCList.Item(i)
                        If reference.IsAlive Then
                            If (i <> index) Then
                                EmberNativeScraperModule.__ENCList.Item(index) = EmberNativeScraperModule.__ENCList.Item(i)
                            End If
                            index += 1
                        End If
                        i += 1
                    Loop
                    EmberNativeScraperModule.__ENCList.RemoveRange(index, (EmberNativeScraperModule.__ENCList.Count - index))
                    EmberNativeScraperModule.__ENCList.Capacity = EmberNativeScraperModule.__ENCList.Count
                End If
                EmberNativeScraperModule.__ENCList.Add(New WeakReference(RuntimeHelpers.GetObjectValue(value)))
            End SyncLock
        End Sub

        <CompilerGenerated> _
        Private Shared Function _Lambda$__94(ByVal y As _externalScraperModuleClass) As Integer
            Return y.ScraperOrder
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__95(ByVal e As _externalScraperModuleClass) As Boolean
            Return (e.ProcessorModule.IsScraper AndAlso e.ProcessorModule.ScraperEnabled)
        End Function

        Public Function DownloadTrailer(ByRef DBMovie As DBMovie, ByRef sURL As String) As ModuleResult Implements EmberMovieScraperModule.DownloadTrailer
            Using trailer As dlgTrailer = New dlgTrailer
                trailer.IMDBURL = Me.MySettings.IMDBURL
                sURL = trailer.ShowDialog(DBMovie.Movie.IMDBID, DBMovie.Filename)
            End Using
            Return New ModuleResult With { _
                .breakChain = False _
            }
        End Function

        Public Function GetMovieStudio(ByRef DBMovie As DBMovie, ByRef studio As List(Of String)) As ModuleResult Implements EmberMovieScraperModule.GetMovieStudio
            studio = New Scraper() With { _
                .UseOFDBTitle = Me.MySettings.UseOFDBTitle, _
                .UseOFDBOutline = Me.MySettings.UseOFDBOutline, _
                .UseOFDBPlot = Me.MySettings.UseOFDBPlot, _
                .UseOFDBGenre = Me.MySettings.UseOFDBGenre, _
                .IMDBURL = Me.MySettings.IMDBURL _
            }.GetMovieStudios(DBMovie.Movie.IMDBID)
            Return New ModuleResult With { _
                .breakChain = False _
            }
        End Function

        Private Sub Handle_ModuleSettingsChanged()
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
        End Sub

        Private Sub Handle_PostModuleSettingsChanged()
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
        End Sub

        Private Sub Handle_SetupPostScraperChanged(ByVal state As Boolean, ByVal difforder As Integer)
            Me.PostScraperEnabled = state
            Dim setupPostScraperChangedEvent As PostScraperSetupChangedEventHandler = Me.SetupPostScraperChangedEvent
            If (Not setupPostScraperChangedEvent Is Nothing) Then
                setupPostScraperChangedEvent.Invoke((Me._Name & "PostScraper"), state, difforder)
            End If
        End Sub

        Private Sub Handle_SetupScraperChanged(ByVal state As Boolean, ByVal difforder As Integer)
            Me.ScraperEnabled = state
            Dim setupScraperChangedEvent As ScraperSetupChangedEventHandler = Me.SetupScraperChangedEvent
            If (Not setupScraperChangedEvent Is Nothing) Then
                setupScraperChangedEvent.Invoke((Me._Name & "Scraper"), state, difforder)
            End If
        End Sub

        Public Sub Init(ByVal sAssemblyName As String) Implements EmberMovieScraperModule.Init
            EmberNativeScraperModule._AssemblyName = sAssemblyName
            Me.LoadSettings
        End Sub

        Public Function InjectSetupPostScraper() As SettingsPanel Implements EmberMovieScraperModule.InjectSetupPostScraper
            Dim panel2 As New SettingsPanel
            Me._setupPost = New frmMediaSettingsHolder
            Me.LoadSettings
            Me._setupPost.cbEnabled.Checked = Me._PostScraperEnabled
            Me._setupPost.chkTrailerIMDB.Checked = Me.MySettings.UseIMDBTrailer
            Me._setupPost.chkTrailerTMDB.Checked = Me.MySettings.UseTMDBTrailer
            Me._setupPost.cbTrailerTMDBPref.Text = Me.MySettings.UseTMDBTrailerPref
            Me._setupPost.chkTrailerTMDBXBMC.Checked = Me.MySettings.UseTMDBTrailerXBMC
            Me._setupPost.chkScrapePoster.Checked = EmberNativeScraperModule.ConfigScrapeModifier.Poster
            Me._setupPost.chkScrapeFanart.Checked = EmberNativeScraperModule.ConfigScrapeModifier.Fanart
            Me._setupPost.chkUseTMDB.Checked = Me.MySettings.UseTMDB
            Me._setupPost.chkUseIMPA.Checked = Me.MySettings.UseIMPA
            Me._setupPost.chkUseMPDB.Checked = Me.MySettings.UseMPDB
            Me._setupPost.cbManualETSize.Text = Me.MySettings.ManualETSize
            Me._setupPost.txtTimeout.Text = Me.MySettings.TrailerTimeout.ToString
            Me._setupPost.chkDownloadTrailer.Checked = Me.MySettings.DownloadTrailers
            Me._setupPost.CheckTrailer
            Me._setupPost.orderChanged
            panel2.Name = (Me._Name & "PostScraper")
            panel2.Text = Master.eLang.GetString(&H68, "Ember Native Movie Scrapers", False)
            panel2.Prefix = "NativeMovieMedia_"
            panel2.Order = 110
            panel2.Parent = "pnlMovieMedia"
            panel2.Type = Master.eLang.GetString(&H24, "Movies", True)
            panel2.ImageIndex = If(Me._PostScraperEnabled, 9, 10)
            panel2.Panel = Me._setupPost.pnlSettings
            AddHandler Me._setupPost.SetupPostScraperChanged, New SetupPostScraperChangedEventHandler(AddressOf Me.Handle_SetupPostScraperChanged)
            AddHandler Me._setupPost.ModuleSettingsChanged, New ModuleSettingsChangedEventHandler(AddressOf Me.Handle_PostModuleSettingsChanged)
            Return panel2
        End Function

        Public Function InjectSetupScraper() As SettingsPanel Implements EmberMovieScraperModule.InjectSetupScraper
            Dim panel2 As New SettingsPanel
            Me._setup = New frmInfoSettingsHolder
            Me.LoadSettings
            Me._setup.cbEnabled.Checked = Me._ScraperEnabled
            Me._setup.chkTitle.Checked = EmberNativeScraperModule.ConfigOptions.bTitle
            Me._setup.chkYear.Checked = EmberNativeScraperModule.ConfigOptions.bYear
            Me._setup.chkMPAA.Checked = EmberNativeScraperModule.ConfigOptions.bMPAA
            Me._setup.chkRelease.Checked = EmberNativeScraperModule.ConfigOptions.bRelease
            Me._setup.chkRuntime.Checked = EmberNativeScraperModule.ConfigOptions.bRuntime
            Me._setup.chkRating.Checked = EmberNativeScraperModule.ConfigOptions.bRating
            Me._setup.chkVotes.Checked = EmberNativeScraperModule.ConfigOptions.bVotes
            Me._setup.chkStudio.Checked = EmberNativeScraperModule.ConfigOptions.bStudio
            Me._setup.chkTagline.Checked = EmberNativeScraperModule.ConfigOptions.bTagline
            Me._setup.chkOutline.Checked = EmberNativeScraperModule.ConfigOptions.bOutline
            Me._setup.chkPlot.Checked = EmberNativeScraperModule.ConfigOptions.bPlot
            Me._setup.chkCast.Checked = EmberNativeScraperModule.ConfigOptions.bCast
            Me._setup.chkDirector.Checked = EmberNativeScraperModule.ConfigOptions.bDirector
            Me._setup.chkWriters.Checked = EmberNativeScraperModule.ConfigOptions.bWriters
            Me._setup.chkProducers.Checked = EmberNativeScraperModule.ConfigOptions.bProducers
            Me._setup.chkGenre.Checked = EmberNativeScraperModule.ConfigOptions.bGenre
            Me._setup.chkTrailer.Checked = EmberNativeScraperModule.ConfigOptions.bTrailer
            Me._setup.chkMusicBy.Checked = EmberNativeScraperModule.ConfigOptions.bMusicBy
            Me._setup.chkCrew.Checked = EmberNativeScraperModule.ConfigOptions.bOtherCrew
            Me._setup.chkCountry.Checked = EmberNativeScraperModule.ConfigOptions.bCountry
            Me._setup.chkTop250.Checked = EmberNativeScraperModule.ConfigOptions.bTop250
            Me._setup.chkCertification.Checked = EmberNativeScraperModule.ConfigOptions.bCert
            Me._setup.chkOFDBTitle.Checked = Me.MySettings.UseOFDBTitle
            Me._setup.chkOFDBOutline.Checked = Me.MySettings.UseOFDBOutline
            Me._setup.chkOFDBPlot.Checked = Me.MySettings.UseOFDBPlot
            Me._setup.chkOFDBGenre.Checked = Me.MySettings.UseOFDBGenre
            Me._setup.chkFullCast.Checked = EmberNativeScraperModule.ConfigOptions.bFullCast
            Me._setup.chkFullCrew.Checked = EmberNativeScraperModule.ConfigOptions.bFullCrew
            If String.IsNullOrEmpty(Me.MySettings.IMDBURL) Then
                Me.MySettings.IMDBURL = "akas.imdb.com"
            End If
            Me._setup.txtIMDBURL.Text = Me.MySettings.IMDBURL
            Me._setup.orderChanged
            panel2.Name = (Me._Name & "Scraper")
            panel2.Text = Master.eLang.GetString(&H68, "Ember Native Movie Scrapers", False)
            panel2.Prefix = "NativeMovieInfo_"
            panel2.Order = 110
            panel2.Parent = "pnlMovieData"
            panel2.Type = Master.eLang.GetString(&H24, "Movies", True)
            panel2.ImageIndex = If(Me._ScraperEnabled, 9, 10)
            panel2.Panel = Me._setup.pnlSettings
            AddHandler Me._setup.SetupScraperChanged, New SetupScraperChangedEventHandler(AddressOf Me.Handle_SetupScraperChanged)
            AddHandler Me._setup.ModuleSettingsChanged, New ModuleSettingsChangedEventHandler(AddressOf Me.Handle_ModuleSettingsChanged)
            Return panel2
        End Function

        Public Sub LoadSettings()
            EmberNativeScraperModule.ConfigOptions.bTitle = AdvancedSettings.GetBooleanSetting("DoTitle", True, "")
            EmberNativeScraperModule.ConfigOptions.bYear = AdvancedSettings.GetBooleanSetting("DoYear", True, "")
            EmberNativeScraperModule.ConfigOptions.bMPAA = AdvancedSettings.GetBooleanSetting("DoMPAA", True, "")
            EmberNativeScraperModule.ConfigOptions.bRelease = AdvancedSettings.GetBooleanSetting("DoRelease", True, "")
            EmberNativeScraperModule.ConfigOptions.bRuntime = AdvancedSettings.GetBooleanSetting("DoRuntime", True, "")
            EmberNativeScraperModule.ConfigOptions.bRating = AdvancedSettings.GetBooleanSetting("DoRating", True, "")
            EmberNativeScraperModule.ConfigOptions.bVotes = AdvancedSettings.GetBooleanSetting("DoVotes", True, "")
            EmberNativeScraperModule.ConfigOptions.bStudio = AdvancedSettings.GetBooleanSetting("DoStudio", True, "")
            EmberNativeScraperModule.ConfigOptions.bTagline = AdvancedSettings.GetBooleanSetting("DoTagline", True, "")
            EmberNativeScraperModule.ConfigOptions.bOutline = AdvancedSettings.GetBooleanSetting("DoOutline", True, "")
            EmberNativeScraperModule.ConfigOptions.bPlot = AdvancedSettings.GetBooleanSetting("DoPlot", True, "")
            EmberNativeScraperModule.ConfigOptions.bCast = AdvancedSettings.GetBooleanSetting("DoCast", True, "")
            EmberNativeScraperModule.ConfigOptions.bDirector = AdvancedSettings.GetBooleanSetting("DoDirector", True, "")
            EmberNativeScraperModule.ConfigOptions.bWriters = AdvancedSettings.GetBooleanSetting("DoWriters", True, "")
            EmberNativeScraperModule.ConfigOptions.bProducers = AdvancedSettings.GetBooleanSetting("DoProducers", True, "")
            EmberNativeScraperModule.ConfigOptions.bGenre = AdvancedSettings.GetBooleanSetting("DoGenres", True, "")
            EmberNativeScraperModule.ConfigOptions.bTrailer = AdvancedSettings.GetBooleanSetting("DoTrailer", True, "")
            EmberNativeScraperModule.ConfigOptions.bMusicBy = AdvancedSettings.GetBooleanSetting("DoMusic", True, "")
            EmberNativeScraperModule.ConfigOptions.bOtherCrew = AdvancedSettings.GetBooleanSetting("DoOtherCrews", True, "")
            EmberNativeScraperModule.ConfigOptions.bFullCast = AdvancedSettings.GetBooleanSetting("DoFullCast", True, "")
            EmberNativeScraperModule.ConfigOptions.bFullCrew = AdvancedSettings.GetBooleanSetting("DoFullCrews", True, "")
            EmberNativeScraperModule.ConfigOptions.bTop250 = AdvancedSettings.GetBooleanSetting("DoTop250", True, "")
            EmberNativeScraperModule.ConfigOptions.bCountry = AdvancedSettings.GetBooleanSetting("DoCountry", True, "")
            EmberNativeScraperModule.ConfigOptions.bCert = AdvancedSettings.GetBooleanSetting("DoCert", True, "")
            EmberNativeScraperModule.ConfigOptions.bFullCast = AdvancedSettings.GetBooleanSetting("FullCast", True, "")
            EmberNativeScraperModule.ConfigOptions.bFullCrew = AdvancedSettings.GetBooleanSetting("FullCrew", True, "")
            Me.MySettings.IMDBURL = AdvancedSettings.GetSetting("IMDBURL", "akas.imdb.com", "")
            Me.MySettings.UseOFDBTitle = AdvancedSettings.GetBooleanSetting("UseOFDBTitle", False, "")
            Me.MySettings.UseOFDBOutline = AdvancedSettings.GetBooleanSetting("UseOFDBOutline", False, "")
            Me.MySettings.UseOFDBPlot = AdvancedSettings.GetBooleanSetting("UseOFDBPlot", False, "")
            Me.MySettings.UseOFDBGenre = AdvancedSettings.GetBooleanSetting("UseOFDBGenre", False, "")
            Me.MySettings.DownloadTrailers = AdvancedSettings.GetBooleanSetting("DownloadTraliers", False, "")
            Me.MySettings.TrailerTimeout = Convert.ToInt32(AdvancedSettings.GetSetting("TrailerTimeout", "10", ""))
            Me.MySettings.UseIMPA = AdvancedSettings.GetBooleanSetting("UseIMPA", False, "")
            Me.MySettings.UseMPDB = AdvancedSettings.GetBooleanSetting("UseMPDB", False, "")
            Me.MySettings.UseTMDB = AdvancedSettings.GetBooleanSetting("UseTMDB", True, "")
            Me.MySettings.UseIMDBTrailer = AdvancedSettings.GetBooleanSetting("UseIMDBTrailer", True, "")
            Me.MySettings.UseTMDBTrailer = AdvancedSettings.GetBooleanSetting("UseTMDBTrailer", True, "")
            Me.MySettings.UseTMDBTrailerXBMC = AdvancedSettings.GetBooleanSetting("UseTMDBTrailerXBMC", False, "")
            Me.MySettings.ManualETSize = Convert.ToString(AdvancedSettings.GetSetting("ManualETSize", "thumb", ""))
            Me.MySettings.UseTMDBTrailerPref = Convert.ToString(AdvancedSettings.GetSetting("UseTMDBTrailerPref", "en", ""))
            EmberNativeScraperModule.ConfigScrapeModifier.DoSearch = True
            EmberNativeScraperModule.ConfigScrapeModifier.Meta = True
            EmberNativeScraperModule.ConfigScrapeModifier.NFO = True
            EmberNativeScraperModule.ConfigScrapeModifier.Extra = True
            EmberNativeScraperModule.ConfigScrapeModifier.Actors = True
            EmberNativeScraperModule.ConfigScrapeModifier.Poster = AdvancedSettings.GetBooleanSetting("DoPoster", True, "")
            EmberNativeScraperModule.ConfigScrapeModifier.Fanart = AdvancedSettings.GetBooleanSetting("DoFanart", True, "")
            EmberNativeScraperModule.ConfigScrapeModifier.Trailer = AdvancedSettings.GetBooleanSetting("DoTrailer", True, "")
        End Sub

        Public Function PostScraper(ByRef DBMovie As DBMovie, ByVal ScrapeType As ScrapeType) As ModuleResult Implements EmberMovieScraperModule.PostScraper
            Dim flag As Boolean
            Dim movieScraperEventEvent As MovieScraperEventEventHandler
            Dim image As New Images
            Dim images As New Images
            Dim str As String = String.Empty
            Dim trailers As New Trailers
            Me.LoadSettings
            Dim globalScrapeMod As ScrapeModifier = Master.GlobalScrapeMod
            Master.GlobalScrapeMod = Functions.ScrapeModifierAndAlso(Master.GlobalScrapeMod, EmberNativeScraperModule.ConfigScrapeModifier)
            trailers.IMDBURL = Me.MySettings.IMDBURL
            If (Master.GlobalScrapeMod.Poster AndAlso ((Me.MySettings.UseIMPA OrElse Me.MySettings.UseMPDB) OrElse Me.MySettings.UseTMDB)) Then
                image.Clear
                If image.IsAllowedToDownload(DBMovie, ImageType.Posters, False) Then
                    Dim imgResult As New ImgResult
                    If ScrapeImages.GetPreferredImage(image, DBMovie.Movie.IMDBID, ImageType.Posters, imgResult, DBMovie.Filename, False, (((ScrapeType = ScrapeType.FullAsk) OrElse (ScrapeType = ScrapeType.NewAsk)) OrElse ((ScrapeType = ScrapeType.MarkAsk) OrElse (ScrapeType = ScrapeType.UpdateAsk)))) Then
                        If Not Information.IsNothing(image.Image) Then
                            imgResult.ImagePath = image.SaveAsPoster(DBMovie)
                            If Not String.IsNullOrEmpty(imgResult.ImagePath) Then
                                DBMovie.PosterPath = imgResult.ImagePath
                                movieScraperEventEvent = Me.MovieScraperEventEvent
                                If (Not movieScraperEventEvent Is Nothing) Then
                                    movieScraperEventEvent.Invoke(MovieScraperEventType.PosterItem, True)
                                End If
                                If (Master.GlobalScrapeMod.NFO AndAlso Not Master.eSettings.NoSaveImagesToNfo) Then
                                    DBMovie.Movie.Thumb = imgResult.Posters
                                End If
                            End If
                        ElseIf (((ScrapeType = ScrapeType.FullAsk) OrElse (ScrapeType = ScrapeType.NewAsk)) OrElse ((ScrapeType = ScrapeType.MarkAsk) OrElse (ScrapeType = ScrapeType.UpdateAsk))) Then
                            Interaction.MsgBox(Master.eLang.GetString(&H4C, "A poster of your preferred size could not be found. Please choose another.", False), MsgBoxStyle.Information, Master.eLang.GetString(&H4D, "No Preferred Size", False))
                            Using select As dlgImgSelect = New dlgImgSelect
                                [select].IMDBURL = Me.MySettings.IMDBURL
                                imgResult = [select].ShowDialog(DBMovie, ImageType.Posters, False)
                                If Not String.IsNullOrEmpty(imgResult.ImagePath) Then
                                    DBMovie.PosterPath = imgResult.ImagePath
                                    movieScraperEventEvent = Me.MovieScraperEventEvent
                                    If (Not movieScraperEventEvent Is Nothing) Then
                                        movieScraperEventEvent.Invoke(MovieScraperEventType.PosterItem, True)
                                    End If
                                    If (Master.GlobalScrapeMod.NFO AndAlso Not Master.eSettings.NoSaveImagesToNfo) Then
                                        DBMovie.Movie.Thumb = imgResult.Posters
                                    End If
                                End If
                            End Using
                        End If
                    End If
                End If
            End If
            If (Master.GlobalScrapeMod.Fanart AndAlso Me.MySettings.UseTMDB) Then
                images.Clear
                If images.IsAllowedToDownload(DBMovie, ImageType.Fanart, False) Then
                    Dim result As New ImgResult
                    flag = True
                    If ScrapeImages.GetPreferredImage(images, DBMovie.Movie.IMDBID, ImageType.Fanart, result, DBMovie.Filename, Master.GlobalScrapeMod.Extra, (((ScrapeType = ScrapeType.FullAsk) OrElse (ScrapeType = ScrapeType.NewAsk)) OrElse ((ScrapeType = ScrapeType.MarkAsk) OrElse (ScrapeType = ScrapeType.UpdateAsk)))) Then
                        If Not Information.IsNothing(images.Image) Then
                            result.ImagePath = images.SaveAsFanart(DBMovie)
                            If Not String.IsNullOrEmpty(result.ImagePath) Then
                                DBMovie.FanartPath = result.ImagePath
                                movieScraperEventEvent = Me.MovieScraperEventEvent
                                If (Not movieScraperEventEvent Is Nothing) Then
                                    movieScraperEventEvent.Invoke(MovieScraperEventType.FanartItem, True)
                                End If
                                If (Master.GlobalScrapeMod.NFO AndAlso Not Master.eSettings.NoSaveImagesToNfo) Then
                                    DBMovie.Movie.Fanart = result.Fanart
                                End If
                            End If
                        ElseIf (((ScrapeType = ScrapeType.FullAsk) OrElse (ScrapeType = ScrapeType.NewAsk)) OrElse ((ScrapeType = ScrapeType.MarkAsk) OrElse (ScrapeType = ScrapeType.UpdateAsk))) Then
                            Interaction.MsgBox(Master.eLang.GetString(&H4E, "Fanart of your preferred size could not be found. Please choose another.", False), MsgBoxStyle.Information, Master.eLang.GetString(&H4D, "No Preferred Size:", False))
                            Using select2 As dlgImgSelect = New dlgImgSelect
                                select2.IMDBURL = Me.MySettings.IMDBURL
                                result = select2.ShowDialog(DBMovie, ImageType.Fanart, False)
                                If Not String.IsNullOrEmpty(result.ImagePath) Then
                                    DBMovie.FanartPath = result.ImagePath
                                    movieScraperEventEvent = Me.MovieScraperEventEvent
                                    If (Not movieScraperEventEvent Is Nothing) Then
                                        movieScraperEventEvent.Invoke(MovieScraperEventType.FanartItem, True)
                                    End If
                                    If (Master.GlobalScrapeMod.NFO AndAlso Not Master.eSettings.NoSaveImagesToNfo) Then
                                        DBMovie.Movie.Fanart = result.Fanart
                                    End If
                                End If
                            End Using
                        End If
                    End If
                End If
            End If
            If (Master.GlobalScrapeMod.Trailer AndAlso Me.MySettings.DownloadTrailers) Then
                str = trailers.DownloadSingleTrailer(DBMovie.Filename, DBMovie.Movie.IMDBID, DBMovie.isSingle, DBMovie.Movie.Trailer)
                If Not String.IsNullOrEmpty(str) Then
                    If (str.Substring(0, &H16) = "http://www.youtube.com") Then
                        If AdvancedSettings.GetBooleanSetting("UseTMDBTrailerXBMC", False, "") Then
                            DBMovie.Movie.Trailer = Strings.Replace(str, "http://www.youtube.com/watch?v=", "plugin://plugin.video.youtube/?action=play_video&videoid=", 1, -1, CompareMethod.Binary)
                        Else
                            DBMovie.Movie.Trailer = str
                        End If
                    ElseIf (str.Substring(0, 7) = "http://") Then
                        DBMovie.Movie.Trailer = str
                    Else
                        DBMovie.TrailerPath = str
                        movieScraperEventEvent = Me.MovieScraperEventEvent
                        If (Not movieScraperEventEvent Is Nothing) Then
                            movieScraperEventEvent.Invoke(MovieScraperEventType.TrailerItem, True)
                        End If
                    End If
                End If
            End If
            If (Master.GlobalScrapeMod.Extra AndAlso (Master.eSettings.AutoET AndAlso DBMovie.isSingle)) Then
                Try 
                    ScrapeImages.GetPreferredFAasET(DBMovie.Movie.IMDBID, DBMovie.Filename)
                    movieScraperEventEvent = Me.MovieScraperEventEvent
                    If (Not movieScraperEventEvent Is Nothing) Then
                        movieScraperEventEvent.Invoke(MovieScraperEventType.ThumbsItem, True)
                    End If
                Catch exception1 As Exception
                    ProjectData.SetProjectError(exception1)
                    Dim exception As Exception = exception1
                    ProjectData.ClearProjectError
                End Try
            End If
            If (Master.GlobalScrapeMod.Actors AndAlso AdvancedSettings.GetBooleanSetting("ScrapeActorsThumbs", False, "")) Then
                Dim person As Person
                For Each person In DBMovie.Movie.Actors
                    Dim images3 As New Images
                    images3.FromWeb(person.Thumb)
                    images3.SaveAsActorThumb(person, Directory.GetParent(DBMovie.Filename).FullName)
                Next
            End If
            Master.GlobalScrapeMod = globalScrapeMod
            Return New ModuleResult With { _
                .breakChain = False, _
                .BoolProperty = flag _
            }
        End Function

        Public Sub PostScraperOrderChanged() Implements EmberMovieScraperModule.PostScraperOrderChanged
            Me._setup.orderChanged
        End Sub

        Public Function QueryPostScraperCapabilities(ByVal cap As PostScraperCapabilities) As Boolean Implements EmberMovieScraperModule.QueryPostScraperCapabilities
            Select Case CInt(cap)
                Case 1
                    If ((Me.MySettings.UseIMPA OrElse Me.MySettings.UseMPDB) OrElse Me.MySettings.UseTMDB) Then
                        Return True
                    End If
                    Exit Select
                Case 2
                    If Not Me.MySettings.UseTMDB Then
                        Exit Select
                    End If
                    Return True
                Case 3
                    If Not Me.MySettings.DownloadTrailers Then
                        Exit Select
                    End If
                    Return True
            End Select
            Return False
        End Function

        Public Sub SaveSettings()
            AdvancedSettings.SetBooleanSetting("DoFullCast", EmberNativeScraperModule.ConfigOptions.bFullCast, "", False)
            AdvancedSettings.SetBooleanSetting("DoFullCrews", EmberNativeScraperModule.ConfigOptions.bFullCrew, "", False)
            AdvancedSettings.SetBooleanSetting("DoTitle", EmberNativeScraperModule.ConfigOptions.bTitle, "", False)
            AdvancedSettings.SetBooleanSetting("DoYear", EmberNativeScraperModule.ConfigOptions.bYear, "", False)
            AdvancedSettings.SetBooleanSetting("DoMPAA", EmberNativeScraperModule.ConfigOptions.bMPAA, "", False)
            AdvancedSettings.SetBooleanSetting("DoRelease", EmberNativeScraperModule.ConfigOptions.bRelease, "", False)
            AdvancedSettings.SetBooleanSetting("DoRuntime", EmberNativeScraperModule.ConfigOptions.bRuntime, "", False)
            AdvancedSettings.SetBooleanSetting("DoRating", EmberNativeScraperModule.ConfigOptions.bRating, "", False)
            AdvancedSettings.SetBooleanSetting("DoVotes", EmberNativeScraperModule.ConfigOptions.bVotes, "", False)
            AdvancedSettings.SetBooleanSetting("DoStudio", EmberNativeScraperModule.ConfigOptions.bStudio, "", False)
            AdvancedSettings.SetBooleanSetting("DoTagline", EmberNativeScraperModule.ConfigOptions.bTagline, "", False)
            AdvancedSettings.SetBooleanSetting("DoOutline", EmberNativeScraperModule.ConfigOptions.bOutline, "", False)
            AdvancedSettings.SetBooleanSetting("DoPlot", EmberNativeScraperModule.ConfigOptions.bPlot, "", False)
            AdvancedSettings.SetBooleanSetting("DoCast", EmberNativeScraperModule.ConfigOptions.bCast, "", False)
            AdvancedSettings.SetBooleanSetting("DoDirector", EmberNativeScraperModule.ConfigOptions.bDirector, "", False)
            AdvancedSettings.SetBooleanSetting("DoWriters", EmberNativeScraperModule.ConfigOptions.bWriters, "", False)
            AdvancedSettings.SetBooleanSetting("DoProducers", EmberNativeScraperModule.ConfigOptions.bProducers, "", False)
            AdvancedSettings.SetBooleanSetting("DoGenres", EmberNativeScraperModule.ConfigOptions.bGenre, "", False)
            AdvancedSettings.SetBooleanSetting("DoTrailer", EmberNativeScraperModule.ConfigOptions.bTrailer, "", False)
            AdvancedSettings.SetBooleanSetting("DoMusic", EmberNativeScraperModule.ConfigOptions.bMusicBy, "", False)
            AdvancedSettings.SetBooleanSetting("DoOtherCrews", EmberNativeScraperModule.ConfigOptions.bOtherCrew, "", False)
            AdvancedSettings.SetBooleanSetting("DoCountry", EmberNativeScraperModule.ConfigOptions.bCountry, "", False)
            AdvancedSettings.SetBooleanSetting("DoTop250", EmberNativeScraperModule.ConfigOptions.bTop250, "", False)
            AdvancedSettings.SetBooleanSetting("DoCert", EmberNativeScraperModule.ConfigOptions.bCert, "", False)
            AdvancedSettings.SetSetting("IMDBURL", Me.MySettings.IMDBURL, "", False)
            AdvancedSettings.SetBooleanSetting("FullCast", EmberNativeScraperModule.ConfigOptions.bFullCast, "", False)
            AdvancedSettings.SetBooleanSetting("FullCrew", EmberNativeScraperModule.ConfigOptions.bFullCrew, "", False)
            AdvancedSettings.SetBooleanSetting("UseOFDBTitle", Me.MySettings.UseOFDBTitle, "", False)
            AdvancedSettings.SetBooleanSetting("UseOFDBOutline", Me.MySettings.UseOFDBOutline, "", False)
            AdvancedSettings.SetBooleanSetting("UseOFDBPlot", Me.MySettings.UseOFDBPlot, "", False)
            AdvancedSettings.SetBooleanSetting("UseOFDBGenre", Me.MySettings.UseOFDBGenre, "", False)
            AdvancedSettings.SetBooleanSetting("DownloadTraliers", Me.MySettings.DownloadTrailers, "", False)
            AdvancedSettings.SetSetting("TrailerTimeout", Me.MySettings.TrailerTimeout.ToString, "", False)
            AdvancedSettings.SetBooleanSetting("UseIMPA", Me.MySettings.UseIMPA, "", False)
            AdvancedSettings.SetBooleanSetting("UseMPDB", Me.MySettings.UseMPDB, "", False)
            AdvancedSettings.SetBooleanSetting("UseTMDB", Me.MySettings.UseTMDB, "", False)
            AdvancedSettings.SetBooleanSetting("UseIMDBTrailer", Me.MySettings.UseIMDBTrailer, "", False)
            AdvancedSettings.SetBooleanSetting("UseTMDBTrailer", Me.MySettings.UseTMDBTrailer, "", False)
            AdvancedSettings.SetBooleanSetting("UseTMDBTrailerXBMC", Me.MySettings.UseTMDBTrailerXBMC, "", False)
            AdvancedSettings.SetSetting("ManualETSize", Me.MySettings.ManualETSize.ToString, "", False)
            AdvancedSettings.SetSetting("UseTMDBTrailerPref", Me.MySettings.UseTMDBTrailerPref.ToString, "", False)
            AdvancedSettings.SetBooleanSetting("DoPoster", EmberNativeScraperModule.ConfigScrapeModifier.Poster, "", False)
            AdvancedSettings.SetBooleanSetting("DoFanart", EmberNativeScraperModule.ConfigScrapeModifier.Fanart, "", False)
        End Sub

        Public Sub SaveSetupPostScraper(ByVal DoDispose As Boolean) Implements EmberMovieScraperModule.SaveSetupPostScraper
            Me.MySettings.DownloadTrailers = Me._setupPost.chkDownloadTrailer.Checked
            Me.MySettings.UseIMDBTrailer = Me._setupPost.chkTrailerIMDB.Checked
            Me.MySettings.UseTMDBTrailer = Me._setupPost.chkTrailerTMDB.Checked
            Me.MySettings.UseTMDBTrailerXBMC = Me._setupPost.chkTrailerTMDBXBMC.Checked
            Me.MySettings.TrailerTimeout = Convert.ToInt32(Me._setupPost.txtTimeout.Text)
            Me.MySettings.UseTMDB = Me._setupPost.chkUseTMDB.Checked
            Me.MySettings.UseIMPA = Me._setupPost.chkUseIMPA.Checked
            Me.MySettings.UseMPDB = Me._setupPost.chkUseMPDB.Checked
            Me.MySettings.ManualETSize = Me._setupPost.cbManualETSize.Text
            Me.MySettings.UseTMDBTrailerPref = Me._setupPost.cbTrailerTMDBPref.Text
            EmberNativeScraperModule.ConfigScrapeModifier.Poster = Me._setupPost.chkScrapePoster.Checked
            EmberNativeScraperModule.ConfigScrapeModifier.Fanart = Me._setupPost.chkScrapeFanart.Checked
            Me.SaveSettings
            If DoDispose Then
                RemoveHandler Me._setupPost.SetupPostScraperChanged, New SetupPostScraperChangedEventHandler(AddressOf Me.Handle_SetupPostScraperChanged)
                RemoveHandler Me._setupPost.ModuleSettingsChanged, New ModuleSettingsChangedEventHandler(AddressOf Me.Handle_PostModuleSettingsChanged)
                Me._setupPost.Dispose
            End If
        End Sub

        Public Sub SaveSetupScraper(ByVal DoDispose As Boolean) Implements EmberMovieScraperModule.SaveSetupScraper
            If Not String.IsNullOrEmpty(Me._setup.txtIMDBURL.Text) Then
                Me.MySettings.IMDBURL = Strings.Replace(Me._setup.txtIMDBURL.Text, "http://", String.Empty, 1, -1, CompareMethod.Binary)
            Else
                Me.MySettings.IMDBURL = "akas.imdb.com"
            End If
            Me.MySettings.UseOFDBTitle = Me._setup.chkOFDBTitle.Checked
            Me.MySettings.UseOFDBOutline = Me._setup.chkOFDBOutline.Checked
            Me.MySettings.UseOFDBPlot = Me._setup.chkOFDBPlot.Checked
            Me.MySettings.UseOFDBGenre = Me._setup.chkOFDBGenre.Checked
            EmberNativeScraperModule.ConfigOptions.bTitle = Me._setup.chkTitle.Checked
            EmberNativeScraperModule.ConfigOptions.bYear = Me._setup.chkYear.Checked
            EmberNativeScraperModule.ConfigOptions.bMPAA = Me._setup.chkMPAA.Checked
            EmberNativeScraperModule.ConfigOptions.bRelease = Me._setup.chkRelease.Checked
            EmberNativeScraperModule.ConfigOptions.bRuntime = Me._setup.chkRuntime.Checked
            EmberNativeScraperModule.ConfigOptions.bRating = Me._setup.chkRating.Checked
            EmberNativeScraperModule.ConfigOptions.bVotes = Me._setup.chkVotes.Checked
            EmberNativeScraperModule.ConfigOptions.bStudio = Me._setup.chkStudio.Checked
            EmberNativeScraperModule.ConfigOptions.bTagline = Me._setup.chkTagline.Checked
            EmberNativeScraperModule.ConfigOptions.bOutline = Me._setup.chkOutline.Checked
            EmberNativeScraperModule.ConfigOptions.bPlot = Me._setup.chkPlot.Checked
            EmberNativeScraperModule.ConfigOptions.bCast = Me._setup.chkCast.Checked
            EmberNativeScraperModule.ConfigOptions.bDirector = Me._setup.chkDirector.Checked
            EmberNativeScraperModule.ConfigOptions.bWriters = Me._setup.chkWriters.Checked
            EmberNativeScraperModule.ConfigOptions.bProducers = Me._setup.chkProducers.Checked
            EmberNativeScraperModule.ConfigOptions.bGenre = Me._setup.chkGenre.Checked
            EmberNativeScraperModule.ConfigOptions.bTrailer = Me._setup.chkTrailer.Checked
            EmberNativeScraperModule.ConfigOptions.bMusicBy = Me._setup.chkMusicBy.Checked
            EmberNativeScraperModule.ConfigOptions.bOtherCrew = Me._setup.chkCrew.Checked
            EmberNativeScraperModule.ConfigOptions.bCountry = Me._setup.chkCountry.Checked
            EmberNativeScraperModule.ConfigOptions.bTop250 = Me._setup.chkTop250.Checked
            EmberNativeScraperModule.ConfigOptions.bCert = Me._setup.chkCertification.Checked
            EmberNativeScraperModule.ConfigOptions.bFullCrew = Me._setup.chkFullCrew.Checked
            EmberNativeScraperModule.ConfigOptions.bFullCast = Me._setup.chkFullCast.Checked
            Me.SaveSettings
            If DoDispose Then
                RemoveHandler Me._setup.SetupScraperChanged, New SetupScraperChangedEventHandler(AddressOf Me.Handle_SetupScraperChanged)
                RemoveHandler Me._setup.ModuleSettingsChanged, New ModuleSettingsChangedEventHandler(AddressOf Me.Handle_ModuleSettingsChanged)
                Me._setup.Dispose
            End If
        End Sub

        Public Function Scraper(ByRef DBMovie As DBMovie, ByRef ScrapeType As ScrapeType, ByRef Options As ScrapeOptions) As ModuleResult Implements EmberMovieScraperModule.Scraper
            Me.IMDB.IMDBURL = Me.MySettings.IMDBURL
            Me.IMDB.UseOFDBTitle = Me.MySettings.UseOFDBTitle
            Me.IMDB.UseOFDBOutline = Me.MySettings.UseOFDBOutline
            Me.IMDB.UseOFDBPlot = Me.MySettings.UseOFDBPlot
            Me.IMDB.UseOFDBGenre = Me.MySettings.UseOFDBGenre
            Dim str2 As String = String.Empty
            Dim title As String = DBMovie.Movie.Title
            If (Master.GlobalScrapeMod.NFO AndAlso Not Master.GlobalScrapeMod.DoSearch) Then
                If Not String.IsNullOrEmpty(DBMovie.Movie.IMDBID) Then
                    Me.IMDB.GetMovieInfo(DBMovie.Movie.IMDBID, DBMovie.Movie, Options.bFullCrew, Options.bFullCast, False, Options, False)
                ElseIf (Not ScrapeType Is ScrapeType.SingleScrape) Then
                    DBMovie.Movie = Me.IMDB.GetSearchMovieInfo(DBMovie.Movie.Title, DBMovie, ScrapeType, Options)
                    If String.IsNullOrEmpty(DBMovie.Movie.IMDBID) Then
                        Return New ModuleResult With { _
                            .breakChain = False, _
                            .Cancelled = True _
                        }
                    End If
                End If
            End If
            If (((ScrapeType Is ScrapeType.SingleScrape) AndAlso Master.GlobalScrapeMod.DoSearch) AndAlso (ModulesManager.Instance.externalScrapersModules.OrderBy(Of _externalScraperModuleClass, Integer)(New Func(Of _externalScraperModuleClass, Integer)(AddressOf EmberNativeScraperModule._Lambda$__94)).FirstOrDefault(Of _externalScraperModuleClass)(New Func(Of _externalScraperModuleClass, Boolean)(AddressOf EmberNativeScraperModule._Lambda$__95)).AssemblyName = EmberNativeScraperModule._AssemblyName)) Then
                DBMovie.Movie.IMDBID = String.Empty
                DBMovie.ClearExtras = True
                DBMovie.PosterPath = String.Empty
                DBMovie.FanartPath = String.Empty
                DBMovie.TrailerPath = String.Empty
                DBMovie.ExtraPath = String.Empty
                DBMovie.SubPath = String.Empty
                DBMovie.NfoPath = String.Empty
                DBMovie.Movie.Clear
            End If
            If String.IsNullOrEmpty(DBMovie.Movie.IMDBID) Then
                Select Case CInt(ScrapeType)
                    Case 1, 3, 7, 9, 11
                        Return New ModuleResult With { _
                            .breakChain = False _
                        }
                End Select
                If (ScrapeType Is ScrapeType.SingleScrape) Then
                    Using results As dlgIMDBSearchResults = New dlgIMDBSearchResults
                        results.IMDBURL = Me.MySettings.IMDBURL
                        Dim str3 As String = DBMovie.Movie.Title
                        If String.IsNullOrEmpty(str3) Then
                            If Common.isVideoTS(DBMovie.Filename) Then
                                str3 = StringUtils.FilterName(Directory.GetParent(Directory.GetParent(DBMovie.Filename).FullName).Name, False, False)
                            ElseIf Common.isBDRip(DBMovie.Filename) Then
                                str3 = StringUtils.FilterName(Directory.GetParent(Directory.GetParent(Directory.GetParent(DBMovie.Filename).FullName).FullName).Name, False, False)
                            Else
                                str3 = StringUtils.FilterName(If(DBMovie.isSingle, Directory.GetParent(DBMovie.Filename).Name, Path.GetFileNameWithoutExtension(DBMovie.Filename)), True, False)
                            End If
                        End If
                        Dim filterOptions As ScrapeOptions = Functions.ScrapeOptionsAndAlso(Options, EmberNativeScraperModule.ConfigOptions)
                        If (results.ShowDialog(str3, filterOptions) = DialogResult.OK) Then
                            If Not String.IsNullOrEmpty(Master.tmpMovie.IMDBID) Then
                                DBMovie.Movie.IMDBID = Master.tmpMovie.IMDBID
                            End If
                            If Not String.IsNullOrEmpty(DBMovie.Movie.IMDBID) Then
                                Master.currMovie.ClearExtras = True
                                Master.currMovie.PosterPath = String.Empty
                                Master.currMovie.FanartPath = String.Empty
                                Master.currMovie.TrailerPath = String.Empty
                                Master.currMovie.ExtraPath = String.Empty
                                Master.currMovie.SubPath = String.Empty
                                Master.currMovie.NfoPath = String.Empty
                                Me.IMDB.GetMovieInfo(DBMovie.Movie.IMDBID, DBMovie.Movie, filterOptions.bFullCrew, filterOptions.bFullCast, False, filterOptions, False)
                            End If
                        Else
                            Dim result2 As New ModuleResult
                            result2.breakChain = False
                            result2.Cancelled = True
                            Return result2
                        End If
                    End Using
                End If
            End If
            If Not String.IsNullOrEmpty(DBMovie.Movie.Title) Then
                str2 = StringUtils.FilterTokens(DBMovie.Movie.Title)
                If ((title <> DBMovie.Movie.Title) OrElse String.IsNullOrEmpty(DBMovie.Movie.SortTitle)) Then
                    DBMovie.Movie.SortTitle = str2
                End If
                If (Master.eSettings.DisplayYear AndAlso Not String.IsNullOrEmpty(DBMovie.Movie.Year)) Then
                    DBMovie.ListTitle = String.Format("{0} ({1})", str2, DBMovie.Movie.Year)
                Else
                    DBMovie.ListTitle = str2
                End If
            Else
                If Common.isVideoTS(DBMovie.Filename) Then
                    DBMovie.ListTitle = StringUtils.FilterName(Directory.GetParent(Directory.GetParent(DBMovie.Filename).FullName).Name, True, False)
                ElseIf Common.isBDRip(DBMovie.Filename) Then
                    DBMovie.ListTitle = StringUtils.FilterName(Directory.GetParent(Directory.GetParent(Directory.GetParent(DBMovie.Filename).FullName).FullName).Name, True, False)
                ElseIf (DBMovie.UseFolder AndAlso DBMovie.isSingle) Then
                    DBMovie.ListTitle = StringUtils.FilterName(Directory.GetParent(DBMovie.Filename).Name, True, False)
                Else
                    DBMovie.ListTitle = StringUtils.FilterName(Path.GetFileNameWithoutExtension(DBMovie.Filename), True, False)
                End If
                If ((title <> DBMovie.Movie.Title) OrElse String.IsNullOrEmpty(DBMovie.Movie.SortTitle)) Then
                    DBMovie.Movie.SortTitle = DBMovie.ListTitle
                End If
            End If
            Return New ModuleResult With { _
                .breakChain = False _
            }
        End Function

        Public Sub ScraperOrderChanged() Implements EmberMovieScraperModule.ScraperOrderChanged
            Me._setupPost.orderChanged
        End Sub

        Public Function SelectImageOfType(ByRef mMovie As DBMovie, ByVal _DLType As ImageType, ByRef pResults As ImgResult, ByVal Optional _isEdit As Boolean = False, ByVal Optional preload As Boolean = False) As ModuleResult Implements EmberMovieScraperModule.SelectImageOfType
            If ((preload AndAlso (_DLType = ImageType.Fanart)) AndAlso Not Information.IsNothing(Me.dFImgSelect)) Then
                pResults = Me.dFImgSelect.ShowDialog
                Me.dFImgSelect = Nothing
            Else
                Using select As dlgImgSelect = New dlgImgSelect
                    If preload Then
                        Me.dFImgSelect = New dlgImgSelect
                        Me.dFImgSelect.PreLoad(mMovie, ImageType.Fanart, _isEdit)
                    End If
                    [select].IMDBURL = Me.MySettings.IMDBURL
                    pResults = [select].ShowDialog(mMovie, _DLType, _isEdit)
                End Using
            End If
            Return New ModuleResult With { _
                .breakChain = False _
            }
        End Function


        ' Properties
        Public ReadOnly Property EmberAPI.Interfaces.EmberMovieScraperModule.IsPostScraper As Boolean
            Get
                Return True
            End Get
        End Property

        Public ReadOnly Property EmberAPI.Interfaces.EmberMovieScraperModule.IsScraper As Boolean
            Get
                Return True
            End Get
        End Property

        Public ReadOnly Property EmberAPI.Interfaces.EmberMovieScraperModule.ModuleName As String
            Get
                Return Me._Name
            End Get
        End Property

        Public ReadOnly Property EmberAPI.Interfaces.EmberMovieScraperModule.ModuleVersion As String
            Get
                Return FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly.Location).FileVersion.ToString
            End Get
        End Property

        Public Property EmberAPI.Interfaces.EmberMovieScraperModule.PostScraperEnabled As Boolean
            Get
                Return Me._PostScraperEnabled
            End Get
            Set(ByVal value As Boolean)
                Me._PostScraperEnabled = value
            End Set
        End Property

        Public Property EmberAPI.Interfaces.EmberMovieScraperModule.ScraperEnabled As Boolean
            Get
                Return Me._ScraperEnabled
            End Get
            Set(ByVal value As Boolean)
                Me._ScraperEnabled = value
            End Set
        End Property

        Public ReadOnly Property IsPostScraper As Boolean
            Get
                Return True
            End Get
        End Property

        Public ReadOnly Property IsScraper As Boolean
            Get
                Return True
            End Get
        End Property

        Public ReadOnly Property ModuleName As String
            Get
                Return Me._Name
            End Get
        End Property

        Public ReadOnly Property ModuleVersion As String
            Get
                Return FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly.Location).FileVersion.ToString
            End Get
        End Property

        Public Property PostScraperEnabled As Boolean
            Get
                Return Me._PostScraperEnabled
            End Get
            Set(ByVal value As Boolean)
                Me._PostScraperEnabled = value
            End Set
        End Property

        Public Property ScraperEnabled As Boolean
            Get
                Return Me._ScraperEnabled
            End Get
            Set(ByVal value As Boolean)
                Me._ScraperEnabled = value
            End Set
        End Property


        ' Fields
        Private Shared __ENCList As List(Of WeakReference) = New List(Of WeakReference)
        Public Shared _AssemblyName As String
        Private _Name As String
        Private _PostScraperEnabled As Boolean
        Private _ScraperEnabled As Boolean
        Private _setup As frmInfoSettingsHolder
        Private _setupPost As frmMediaSettingsHolder
        Public Shared ConfigOptions As ScrapeOptions = New ScrapeOptions
        Public Shared ConfigScrapeModifier As ScrapeModifier = New ScrapeModifier
        Private dFImgSelect As dlgImgSelect
        Private IMDB As Scraper
        Private MySettings As _MySettings

        ' Nested Types
        <StructLayout(LayoutKind.Sequential)> _
        Public Structure _MySettings
            Public DownloadTrailers As Boolean
            Public IMDBURL As String
            Public UseOFDBGenre As Boolean
            Public UseOFDBOutline As Boolean
            Public UseOFDBPlot As Boolean
            Public UseOFDBTitle As Boolean
            Public TrailerTimeout As Integer
            Public UseTMDB As Boolean
            Public UseIMPA As Boolean
            Public UseMPDB As Boolean
            Public UseTMDBTrailer As Boolean
            Public UseIMDBTrailer As Boolean
            Public UseTMDBTrailerXBMC As Boolean
            Public ManualETSize As String
            Public UseTMDBTrailerPref As String
        End Structure
    End Class
End Namespace

