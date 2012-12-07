Imports EmberAPI
Imports EmberAPI.MediaContainers
Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Drawing
Imports System.Reflection
Imports System.Runtime.CompilerServices

Namespace EmberScraperModule
    Public Class EmberNativeTVScraperModule
        Implements EmberTVScraperModule
        ' Events
        Public Custom Event EmberAPI.Interfaces+EmberTVScraperModule.ModuleSettingsChanged As ModuleSettingsChangedEventHandler
        Public Custom Event EmberAPI.Interfaces+EmberTVScraperModule.SetupPostScraperChanged As SetupPostScraperChangedEventHandler
        Public Custom Event EmberAPI.Interfaces+EmberTVScraperModule.SetupScraperChanged As SetupScraperChangedEventHandler
        Public Custom Event EmberAPI.Interfaces+EmberTVScraperModule.TVScraperEvent As TVScraperEventEventHandler
        Public Custom Event ModuleSettingsChanged As ModuleSettingsChangedEventHandler
        Public Custom Event SetupPostScraperChanged As SetupPostScraperChangedEventHandler
        Public Custom Event SetupScraperChanged As SetupScraperChangedEventHandler
        Public Custom Event TVScraperEvent As TVScraperEventEventHandler

        ' Methods
        Public Sub New()
            EmberNativeTVScraperModule.__ENCAddToList(Me)
            Me._Name = "Ember Native TV Scrapers"
            Me._PostScraperEnabled = False
            Me._ScraperEnabled = False
        End Sub

        <DebuggerNonUserCode> _
        Private Shared Sub __ENCAddToList(ByVal value As Object)
            Dim list As List(Of WeakReference) = EmberNativeTVScraperModule.__ENCList
            SyncLock list
                If (EmberNativeTVScraperModule.__ENCList.Count = EmberNativeTVScraperModule.__ENCList.Capacity) Then
                    Dim index As Integer = 0
                    Dim num3 As Integer = (EmberNativeTVScraperModule.__ENCList.Count - 1)
                    Dim i As Integer = 0
                    Do While (i <= num3)
                        Dim reference As WeakReference = EmberNativeTVScraperModule.__ENCList.Item(i)
                        If reference.IsAlive Then
                            If (i <> index) Then
                                EmberNativeTVScraperModule.__ENCList.Item(index) = EmberNativeTVScraperModule.__ENCList.Item(i)
                            End If
                            index += 1
                        End If
                        i += 1
                    Loop
                    EmberNativeTVScraperModule.__ENCList.RemoveRange(index, (EmberNativeTVScraperModule.__ENCList.Count - index))
                    EmberNativeTVScraperModule.__ENCList.Capacity = EmberNativeTVScraperModule.__ENCList.Count
                End If
                EmberNativeTVScraperModule.__ENCList.Add(New WeakReference(RuntimeHelpers.GetObjectValue(value)))
            End SyncLock
        End Sub

        Public Sub CancelAsync() Implements EmberTVScraperModule.CancelAsync
            EmberNativeTVScraperModule.TVScraper.CancelAsync
        End Sub

        Public Function ChangeEpisode(ByVal ShowID As Integer, ByVal TVDBID As String, ByVal Lang As String, ByRef epDet As EpisodeDetails) As ModuleResult Implements EmberTVScraperModule.ChangeEpisode
            epDet = EmberNativeTVScraperModule.TVScraper.ChangeEpisode(ShowID, TVDBID, Lang)
            Return New ModuleResult With { _
                .breakChain = False _
            }
        End Function

        Public Function GetLangs(ByVal sMirror As String, ByRef Langs As List(Of TVLanguage)) As ModuleResult Implements EmberTVScraperModule.GetLangs
            Langs = EmberNativeTVScraperModule.TVScraper.GetLangs(sMirror)
            Return New ModuleResult With { _
                .breakChain = True _
            }
        End Function

        Public Function GetSingleEpisode(ByVal ShowID As Integer, ByVal TVDBID As String, ByVal Season As Integer, ByVal Episode As Integer, ByVal Lang As String, ByVal Ordering As Ordering, ByVal Options As TVScrapeOptions, ByRef epDetails As EpisodeDetails) As ModuleResult Implements EmberTVScraperModule.GetSingleEpisode
            epDetails = EmberNativeTVScraperModule.TVScraper.GetSingleEpisode(ShowID, TVDBID, Season, Episode, Lang, Ordering, Options)
            Return New ModuleResult With { _
                .breakChain = False _
            }
        End Function

        Public Function GetSingleImage(ByVal Title As String, ByVal ShowID As Integer, ByVal TVDBID As String, ByVal Type As TVImageType, ByVal Season As Integer, ByVal Episode As Integer, ByVal Lang As String, ByVal Ordering As Ordering, ByVal CurrentImage As Image, ByRef Image As Image) As ModuleResult Implements EmberTVScraperModule.GetSingleImage
            Image = EmberNativeTVScraperModule.TVScraper.GetSingleImage(Title, ShowID, TVDBID, Type, Season, Episode, Lang, Ordering, CurrentImage)
            Return New ModuleResult With { _
                .breakChain = True _
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
            Dim setupPostScraperChangedEvent As SetupPostScraperChangedEventHandler = Me.SetupPostScraperChangedEvent
            If (Not setupPostScraperChangedEvent Is Nothing) Then
                setupPostScraperChangedEvent.Invoke((Me._Name & "PostScraper"), state, difforder)
            End If
        End Sub

        Private Sub Handle_SetupScraperChanged(ByVal state As Boolean, ByVal difforder As Integer)
            Me.ScraperEnabled = state
            Dim setupScraperChangedEvent As SetupScraperChangedEventHandler = Me.SetupScraperChangedEvent
            If (Not setupScraperChangedEvent Is Nothing) Then
                setupScraperChangedEvent.Invoke((Me._Name & "Scraper"), state, difforder)
            End If
        End Sub

        Public Sub Handler_ScraperEvent(ByVal eType As TVScraperEventType, ByVal iProgress As Integer, ByVal Parameter As Object)
            Dim tVScraperEventEvent As TVScraperEventEventHandler = Me.TVScraperEventEvent
            If (Not tVScraperEventEvent Is Nothing) Then
                tVScraperEventEvent.Invoke(eType, iProgress, RuntimeHelpers.GetObjectValue(Parameter))
            End If
        End Sub

        Public Sub Init(ByVal sAssemblyName As String) Implements EmberTVScraperModule.Init
            AddHandler EmberNativeTVScraperModule.TVScraper.ScraperEvent, New ScraperEventEventHandler(AddressOf Me.Handler_ScraperEvent)
        End Sub

        Public Function InjectSetupPostScraper() As SettingsPanel Implements EmberTVScraperModule.InjectSetupPostScraper
            Dim panel2 As New SettingsPanel
            Me._setupPost = New frmTVMediaSettingsHolder
            Me._setupPost.cbEnabled.Checked = Me._PostScraperEnabled
            panel2.Name = (Me._Name & "PostScraper")
            panel2.Text = Master.eLang.GetString(0, "Ember Native TV Scrapers", False)
            panel2.Type = Master.eLang.GetString(&H2BA, "TV Shows", True)
            panel2.ImageIndex = If(Me._ScraperEnabled, 9, 10)
            panel2.Order = 100
            panel2.Panel = Me._setupPost.pnlSettings
            panel2.Parent = "pnlTVMedia"
            AddHandler Me._setupPost.SetupPostScraperChanged, New SetupPostScraperChangedEventHandler(AddressOf Me.Handle_SetupPostScraperChanged)
            AddHandler Me._setupPost.ModuleSettingsChanged, New ModuleSettingsChangedEventHandler(AddressOf Me.Handle_ModuleSettingsChanged)
            Return panel2
        End Function

        Public Function InjectSetupScraper() As SettingsPanel Implements EmberTVScraperModule.InjectSetupScraper
            Dim panel2 As New SettingsPanel
            Me._setup = New frmTVInfoSettingsHolder
            Me._setup.cbEnabled.Checked = Me._ScraperEnabled
            panel2.Name = (Me._Name & "Scraper")
            panel2.Text = Master.eLang.GetString(0, "Ember Native TV Scrapers", False)
            panel2.Prefix = "NativeTV_"
            panel2.Type = Master.eLang.GetString(&H2BA, "TV Shows", True)
            panel2.ImageIndex = If(Me._ScraperEnabled, 9, 10)
            panel2.Order = 100
            panel2.Panel = Me._setup.pnlSettings
            panel2.Parent = "pnlTVData"
            AddHandler Me._setup.SetupScraperChanged, New SetupScraperChangedEventHandler(AddressOf Me.Handle_SetupScraperChanged)
            AddHandler Me._setup.ModuleSettingsChanged, New ModuleSettingsChangedEventHandler(AddressOf Me.Handle_PostModuleSettingsChanged)
            Return panel2
        End Function

        Public Function PostScraper(ByRef DBTV As DBTV, ByVal ScrapeType As ScrapeType) As ModuleResult Implements EmberTVScraperModule.PostScraper
            Dim result As ModuleResult
            Return result
        End Function

        Public Function SaveImages() As ModuleResult Implements EmberTVScraperModule.SaveImages
            EmberNativeTVScraperModule.TVScraper.SaveImages
            Return New ModuleResult With { _
                .breakChain = False _
            }
        End Function

        Public Sub SaveSetupPostScraper(ByVal DoDispose As Boolean) Implements EmberTVScraperModule.SaveSetupPostScraper
        End Sub

        Public Sub SaveSetupScraper(ByVal DoDispose As Boolean) Implements EmberTVScraperModule.SaveSetupScraper
        End Sub

        Public Function ScrapeEpisode(ByVal ShowID As Integer, ByVal ShowTitle As String, ByVal TVDBID As String, ByVal iEpisode As Integer, ByVal iSeason As Integer, ByVal Lang As String, ByVal Ordering As Ordering, ByVal Options As TVScrapeOptions) As ModuleResult Implements EmberTVScraperModule.ScrapeEpisode
            EmberNativeTVScraperModule.TVScraper.ScrapeEpisode(ShowID, ShowTitle, TVDBID, iEpisode, iSeason, Lang, Ordering, Options)
            Return New ModuleResult With { _
                .breakChain = False _
            }
        End Function

        Public Function Scraper(ByVal ShowID As Integer, ByVal ShowTitle As String, ByVal TVDBID As String, ByVal Lang As String, ByVal Ordering As Ordering, ByVal Options As TVScrapeOptions, ByVal ScrapeType As ScrapeType, ByVal WithCurrent As Boolean) As ModuleResult Implements EmberTVScraperModule.Scraper
            EmberNativeTVScraperModule.TVScraper.SingleScrape(ShowID, ShowTitle, TVDBID, Lang, Ordering, Options, ScrapeType, WithCurrent)
            Return New ModuleResult With { _
                .breakChain = False _
            }
        End Function

        Public Function ScrapeSeason(ByVal ShowID As Integer, ByVal ShowTitle As String, ByVal TVDBID As String, ByVal iSeason As Integer, ByVal Lang As String, ByVal Ordering As Ordering, ByVal Options As TVScrapeOptions) As ModuleResult Implements EmberTVScraperModule.ScrapeSeason
            EmberNativeTVScraperModule.TVScraper.ScrapeSeason(ShowID, ShowTitle, TVDBID, iSeason, Lang, Ordering, Options)
            Return New ModuleResult With { _
                .breakChain = False _
            }
        End Function


        ' Properties
        Public ReadOnly Property EmberAPI.Interfaces.EmberTVScraperModule.IsBusy As Boolean
            Get
                Return EmberNativeTVScraperModule.TVScraper.IsBusy
            End Get
        End Property

        Public ReadOnly Property EmberAPI.Interfaces.EmberTVScraperModule.IsPostScraper As Boolean
            Get
                Return True
            End Get
        End Property

        Public ReadOnly Property EmberAPI.Interfaces.EmberTVScraperModule.IsScraper As Boolean
            Get
                Return True
            End Get
        End Property

        Public ReadOnly Property EmberAPI.Interfaces.EmberTVScraperModule.ModuleName As String
            Get
                Return Me._Name
            End Get
        End Property

        Public ReadOnly Property EmberAPI.Interfaces.EmberTVScraperModule.ModuleVersion As String
            Get
                Return FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly.Location).FileVersion.ToString
            End Get
        End Property

        Public Property EmberAPI.Interfaces.EmberTVScraperModule.PostScraperEnabled As Boolean
            Get
                Return Me._PostScraperEnabled
            End Get
            Set(ByVal value As Boolean)
                Me._PostScraperEnabled = value
            End Set
        End Property

        Public Property EmberAPI.Interfaces.EmberTVScraperModule.ScraperEnabled As Boolean
            Get
                Return Me._ScraperEnabled
            End Get
            Set(ByVal value As Boolean)
                Me._ScraperEnabled = value
            End Set
        End Property

        Public ReadOnly Property IsBusy As Boolean
            Get
                Return EmberNativeTVScraperModule.TVScraper.IsBusy
            End Get
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
        Private _Name As String
        Private _PostScraperEnabled As Boolean
        Private _ScraperEnabled As Boolean
        Private _setup As frmTVInfoSettingsHolder
        Private _setupPost As frmTVMediaSettingsHolder
        Public Shared TVScraper As Scraper = New Scraper
    End Class
End Namespace

