Imports EmberAPI
Imports EmberAPI.MediaContainers
Imports EmberScraperModule.IMDB
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Windows.Forms

Namespace EmberScraperModule
    <DesignerGenerated> _
    Public Class dlgIMDBSearchResults
        Inherits Form
        ' Methods
        Public Sub New()
            AddHandler MyBase.GotFocus, New EventHandler(AddressOf Me.dlgIMDBSearchResults_GotFocus)
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.dlgIMDBSearchResults_Load)
            AddHandler MyBase.Shown, New EventHandler(AddressOf Me.dlgIMDBSearchResults_Shown)
            dlgIMDBSearchResults.__ENCAddToList(Me)
            Me.bwDownloadPic = New BackgroundWorker
            Me.tmrLoad = New Timer
            Me.tmrWait = New Timer
            Me.IMDB = New Scraper
            Me.sHTTP = New HTTP
            Me._currnode = -1
            Me._prevnode = -2
            Me._InfoCache = New Dictionary(Of String, Movie)
            Me._PosterCache = New Dictionary(Of String, Image)
            Me.InitializeComponent
        End Sub

        <DebuggerNonUserCode> _
        Private Shared Sub __ENCAddToList(ByVal value As Object)
            Dim list As List(Of WeakReference) = dlgIMDBSearchResults.__ENCList
            SyncLock list
                If (dlgIMDBSearchResults.__ENCList.Count = dlgIMDBSearchResults.__ENCList.Capacity) Then
                    Dim index As Integer = 0
                    Dim num3 As Integer = (dlgIMDBSearchResults.__ENCList.Count - 1)
                    Dim i As Integer = 0
                    Do While (i <= num3)
                        Dim reference As WeakReference = dlgIMDBSearchResults.__ENCList.Item(i)
                        If reference.IsAlive Then
                            If (i <> index) Then
                                dlgIMDBSearchResults.__ENCList.Item(index) = dlgIMDBSearchResults.__ENCList.Item(i)
                            End If
                            index += 1
                        End If
                        i += 1
                    Loop
                    dlgIMDBSearchResults.__ENCList.RemoveRange(index, (dlgIMDBSearchResults.__ENCList.Count - index))
                    dlgIMDBSearchResults.__ENCList.Capacity = dlgIMDBSearchResults.__ENCList.Count
                End If
                dlgIMDBSearchResults.__ENCList.Add(New WeakReference(RuntimeHelpers.GetObjectValue(value)))
            End SyncLock
        End Sub

        Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs)
            If Not String.IsNullOrEmpty(Me.txtSearch.Text) Then
                Me.OK_Button.Enabled = False
                Me.pnlPicStatus.Visible = False
                Me._InfoCache.Clear
                Me._PosterCache.Clear
                Me.ClearInfo
                Me.Label3.Text = Master.eLang.GetString(11, "Searching IMDB...", False)
                Me.pnlLoading.Visible = True
                Me.chkManual.Enabled = False
                Me.IMDB.CancelAsync
                Me.IMDB.IMDBURL = Me.IMDBURL
                Me.IMDB.SearchMovieAsync(Me.txtSearch.Text, Me._filterOptions)
            End If
        End Sub

        Private Sub btnVerify_Click(ByVal sender As Object, ByVal e As EventArgs)
            If Regex.IsMatch(Me.txtIMDBID.Text.Replace("tt", String.Empty), "\d\d\d\d\d\d\d") Then
                Me.IMDB.IMDBURL = Me.IMDBURL
                Me.IMDB.GetSearchMovieInfoAsync(Me.txtIMDBID.Text.Replace("tt", String.Empty), Master.tmpMovie, Master.DefaultOptions)
            Else
                Interaction.MsgBox(Master.eLang.GetString(12, "The ID you entered is not a valid IMDB ID.", False), MsgBoxStyle.Exclamation, Master.eLang.GetString(&H124, "Invalid Entry", True))
            End If
        End Sub

        Private Sub bwDownloadPic_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
            Dim argument As Arguments = DirectCast(e.Argument, Arguments)
            Me.sHTTP.StartDownloadImage(argument.pURL)
            Do While Me.sHTTP.IsDownloading
                Application.DoEvents
                Thread.Sleep(50)
            Loop
            Dim results2 As New Results With { _
                .Result = Me.sHTTP.Image, _
                .IMDBId = argument.IMDBId _
            }
            e.Result = results2
        End Sub

        Private Sub bwDownloadPic_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
            Dim result As Results = DirectCast(e.Result, Results)
            Try 
                Me.pbPoster.Image = result.Result
                If Not Me._PosterCache.ContainsKey(result.IMDBId) Then
                    Me._PosterCache.Add(result.IMDBId, result.Result)
                End If
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            Finally
                Me.pnlPicStatus.Visible = False
            End Try
        End Sub

        Private Sub Cancel_Button_Click(ByVal sender As Object, ByVal e As EventArgs)
            Master.tmpMovie.Clear
            Me.DialogResult = DialogResult.Cancel
            Me.Close
        End Sub

        Private Sub chkManual_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Me.ClearInfo
            Me.OK_Button.Enabled = False
            Me.txtIMDBID.Enabled = Me.chkManual.Checked
            Me.btnVerify.Enabled = Me.chkManual.Checked
            Me.tvResults.Enabled = Not Me.chkManual.Checked
            If Not Me.chkManual.Checked Then
                Me.txtIMDBID.Text = String.Empty
            End If
        End Sub

        Private Sub ClearInfo()
            Me.ControlsVisible(False)
            Me.lblTitle.Text = String.Empty
            Me.lblTagline.Text = String.Empty
            Me.lblYear.Text = String.Empty
            Me.lblDirector.Text = String.Empty
            Me.lblGenre.Text = String.Empty
            Me.txtOutline.Text = String.Empty
            Me.lblIMDB.Text = String.Empty
            Me.pbPoster.Image = Nothing
            Master.tmpMovie.Clear
            Me.IMDB.CancelAsync
        End Sub

        Private Sub ControlsVisible(ByVal areVisible As Boolean)
            Me.lblYearHeader.Visible = areVisible
            Me.lblDirectorHeader.Visible = areVisible
            Me.lblGenreHeader.Visible = areVisible
            Me.lblPlotHeader.Visible = areVisible
            Me.lblIMDBHeader.Visible = areVisible
            Me.txtOutline.Visible = areVisible
            Me.lblYear.Visible = areVisible
            Me.lblTagline.Visible = areVisible
            Me.lblTitle.Visible = areVisible
            Me.lblDirector.Visible = areVisible
            Me.lblGenre.Visible = areVisible
            Me.lblIMDB.Visible = areVisible
            Me.pbPoster.Visible = areVisible
        End Sub

        <DebuggerNonUserCode> _
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try 
                If (disposing AndAlso (Not Me.components Is Nothing)) Then
                    Me.components.Dispose
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        Private Sub dlgIMDBSearchResults_GotFocus(ByVal sender As Object, ByVal e As EventArgs)
            Me.AcceptButton = Me.OK_Button
        End Sub

        Private Sub dlgIMDBSearchResults_Load(ByVal sender As Object, ByVal e As EventArgs)
            Me.SetUp
            Me.pnlPicStatus.Visible = False
            Me.IMDB.IMDBURL = Me.IMDBURL
            Me.IMDB.UseOFDBTitle = Me.UseOFDBTitle
            Me.IMDB.UseOFDBOutline = Me.UseOFDBOutline
            Me.IMDB.UseOFDBPlot = Me.UseOFDBPlot
            Me.IMDB.UseOFDBGenre = Me.UseOFDBGenre
            AddHandler Me.IMDB.SearchMovieInfoDownloaded, New SearchMovieInfoDownloadedEventHandler(AddressOf Me.SearchMovieInfoDownloaded)
            AddHandler Me.IMDB.SearchResultsDownloaded, New SearchResultsDownloadedEventHandler(AddressOf Me.SearchResultsDownloaded)
            Try 
                Dim image As New Bitmap(Me.pnlTop.Width, Me.pnlTop.Height)
                Using graphics As Graphics = Graphics.FromImage(image)
                    graphics.FillRectangle(New LinearGradientBrush(Me.pnlTop.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, LinearGradientMode.Horizontal), Me.pnlTop.ClientRectangle)
                    Me.pnlTop.BackgroundImage = image
                End Using
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Private Sub dlgIMDBSearchResults_Shown(ByVal sender As Object, ByVal e As EventArgs)
            Me.Activate
            Me.tvResults.Focus
        End Sub

        Private Function GetMovieClone(ByVal original As Movie) As Movie
            Dim movie2 As New Movie
            Dim movie3 As Movie = movie2
            movie3.IMDBID = original.IMDBID
            movie3.Genre = original.Genre
            movie3.Title = original.Title
            movie3.Tagline = original.Tagline
            movie3.Year = original.Year
            movie3.Director = original.Director
            movie3.Genre = original.Genre
            movie3.Outline = original.Outline
            movie3 = Nothing
            Return movie2
        End Function

        <DebuggerStepThrough> _
        Private Sub InitializeComponent()
            Dim manager As New ComponentResourceManager(GetType(dlgIMDBSearchResults))
            Me.OK_Button = New Button
            Me.Cancel_Button = New Button
            Me.tvResults = New TreeView
            Me.pbPoster = New PictureBox
            Me.lblTitle = New Label
            Me.lblTagline = New Label
            Me.txtOutline = New TextBox
            Me.lblYear = New Label
            Me.lblDirector = New Label
            Me.lblGenre = New Label
            Me.txtIMDBID = New TextBox
            Me.pnlTop = New Panel
            Me.Label2 = New Label
            Me.Label1 = New Label
            Me.PictureBox1 = New PictureBox
            Me.chkManual = New CheckBox
            Me.btnVerify = New Button
            Me.lblIMDB = New Label
            Me.lblYearHeader = New Label
            Me.lblDirectorHeader = New Label
            Me.lblGenreHeader = New Label
            Me.lblIMDBHeader = New Label
            Me.lblPlotHeader = New Label
            Me.btnSearch = New Button
            Me.txtSearch = New TextBox
            Me.pnlLoading = New Panel
            Me.Label3 = New Label
            Me.ProgressBar1 = New ProgressBar
            Me.pnlPicStatus = New Panel
            Me.Label4 = New Label
            DirectCast(Me.pbPoster, ISupportInitialize).BeginInit
            Me.pnlTop.SuspendLayout
            DirectCast(Me.PictureBox1, ISupportInitialize).BeginInit
            Me.pnlLoading.SuspendLayout
            Me.pnlPicStatus.SuspendLayout
            Me.SuspendLayout
            Me.OK_Button.Enabled = False
            Me.OK_Button.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            Dim point2 As New Point(&H1F1, &H191)
            Me.OK_Button.Location = point2
            Me.OK_Button.Name = "OK_Button"
            Dim size2 As New Size(&H43, &H16)
            Me.OK_Button.Size = size2
            Me.OK_Button.TabIndex = 4
            Me.OK_Button.Text = "OK"
            Me.Cancel_Button.DialogResult = DialogResult.Cancel
            Me.Cancel_Button.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(570, &H191)
            Me.Cancel_Button.Location = point2
            Me.Cancel_Button.Name = "Cancel_Button"
            size2 = New Size(&H43, &H16)
            Me.Cancel_Button.Size = size2
            Me.Cancel_Button.TabIndex = 5
            Me.Cancel_Button.Text = "Cancel"
            Me.tvResults.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            Me.tvResults.HideSelection = False
            point2 = New Point(4, &H60)
            Me.tvResults.Location = point2
            Me.tvResults.Name = "tvResults"
            size2 = New Size(&H119, &H12B)
            Me.tvResults.Size = size2
            Me.tvResults.TabIndex = 0
            point2 = New Point(&H126, 130)
            Me.pbPoster.Location = point2
            Me.pbPoster.Name = "pbPoster"
            size2 = New Size(110, 130)
            Me.pbPoster.Size = size2
            Me.pbPoster.SizeMode = PictureBoxSizeMode.Zoom
            Me.pbPoster.TabIndex = 2
            Me.pbPoster.TabStop = False
            Me.pbPoster.Visible = False
            Me.lblTitle.Font = New Font("Segoe UI", 9.75!, FontStyle.Bold, GraphicsUnit.Point, &HEE)
            point2 = New Point(&H123, 70)
            Me.lblTitle.Location = point2
            Me.lblTitle.Name = "lblTitle"
            size2 = New Size(&H15A, &H13)
            Me.lblTitle.Size = size2
            Me.lblTitle.TabIndex = 3
            Me.lblTitle.Text = "Title"
            Me.lblTitle.Visible = False
            Me.lblTagline.Font = New Font("Segoe UI", 8.25!, FontStyle.Bold, GraphicsUnit.Point, &HEE)
            point2 = New Point(&H123, &H59)
            Me.lblTagline.Location = point2
            Me.lblTagline.Name = "lblTagline"
            size2 = New Size(&H15A, &H10)
            Me.lblTagline.Size = size2
            Me.lblTagline.TabIndex = 4
            Me.lblTagline.Text = "Tagline"
            Me.lblTagline.Visible = False
            Me.txtOutline.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(&H126, &H127)
            Me.txtOutline.Location = point2
            Me.txtOutline.Multiline = True
            Me.txtOutline.Name = "txtOutline"
            size2 = New Size(&H157, 100)
            Me.txtOutline.Size = size2
            Me.txtOutline.TabIndex = 5
            Me.txtOutline.TabStop = False
            Me.txtOutline.Visible = False
            Me.lblYear.AutoSize = True
            Me.lblYear.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(&H1D8, 130)
            Me.lblYear.Location = point2
            Me.lblYear.Name = "lblYear"
            size2 = New Size(&H1F, 13)
            Me.lblYear.Size = size2
            Me.lblYear.TabIndex = 6
            Me.lblYear.Text = "0000"
            Me.lblYear.Visible = False
            Me.lblDirector.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(&H1D8, &H9C)
            Me.lblDirector.Location = point2
            Me.lblDirector.Name = "lblDirector"
            size2 = New Size(&HA5, &H10)
            Me.lblDirector.Size = size2
            Me.lblDirector.TabIndex = 7
            Me.lblDirector.Text = "Director"
            Me.lblDirector.Visible = False
            Me.lblGenre.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(&H1D8, &HB7)
            Me.lblGenre.Location = point2
            Me.lblGenre.Name = "lblGenre"
            size2 = New Size(&HA5, &H34)
            Me.lblGenre.Size = size2
            Me.lblGenre.TabIndex = 8
            Me.lblGenre.Text = "Genre"
            Me.lblGenre.Visible = False
            Me.txtIMDBID.Enabled = False
            Me.txtIMDBID.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(&H8B, &H18F)
            Me.txtIMDBID.Location = point2
            Me.txtIMDBID.Name = "txtIMDBID"
            size2 = New Size(100, &H16)
            Me.txtIMDBID.Size = size2
            Me.txtIMDBID.TabIndex = 2
            Me.pnlTop.BackColor = Color.LightSteelBlue
            Me.pnlTop.BorderStyle = BorderStyle.FixedSingle
            Me.pnlTop.Controls.Add(Me.Label2)
            Me.pnlTop.Controls.Add(Me.Label1)
            Me.pnlTop.Controls.Add(Me.PictureBox1)
            Me.pnlTop.Dock = DockStyle.Top
            point2 = New Point(0, 0)
            Me.pnlTop.Location = point2
            Me.pnlTop.Name = "pnlTop"
            size2 = New Size(&H283, &H40)
            Me.pnlTop.Size = size2
            Me.pnlTop.TabIndex = &H39
            Me.Label2.AutoSize = True
            Me.Label2.BackColor = Color.Transparent
            Me.Label2.Font = New Font("Segoe UI", 8.25!, FontStyle.Bold, GraphicsUnit.Point, &HEE)
            Me.Label2.ForeColor = Color.White
            point2 = New Point(&H3D, &H26)
            Me.Label2.Location = point2
            Me.Label2.Name = "Label2"
            size2 = New Size(&H114, 13)
            Me.Label2.Size = size2
            Me.Label2.TabIndex = 2
            Me.Label2.Text = "View details of each result to find the proper movie."
            Me.Label1.AutoSize = True
            Me.Label1.BackColor = Color.Transparent
            Me.Label1.Font = New Font("Segoe UI", 18!, FontStyle.Bold, GraphicsUnit.Point, &HEE)
            Me.Label1.ForeColor = Color.White
            point2 = New Point(&H3A, 3)
            Me.Label1.Location = point2
            Me.Label1.Name = "Label1"
            size2 = New Size(&H101, &H20)
            Me.Label1.Size = size2
            Me.Label1.TabIndex = 1
            Me.Label1.Text = "Movie Search Results"
            Me.PictureBox1.BackColor = Color.Transparent
            Me.PictureBox1.Image = DirectCast(manager.GetObject("PictureBox1.Image"), Image)
            point2 = New Point(7, 8)
            Me.PictureBox1.Location = point2
            Me.PictureBox1.Name = "PictureBox1"
            size2 = New Size(&H30, &H30)
            Me.PictureBox1.Size = size2
            Me.PictureBox1.SizeMode = PictureBoxSizeMode.AutoSize
            Me.PictureBox1.TabIndex = 0
            Me.PictureBox1.TabStop = False
            Me.chkManual.AutoSize = True
            Me.chkManual.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(4, &H193)
            Me.chkManual.Location = point2
            Me.chkManual.Name = "chkManual"
            size2 = New Size(&H80, &H11)
            Me.chkManual.Size = size2
            Me.chkManual.TabIndex = 1
            Me.chkManual.Text = "Manual IMDB Entry:"
            Me.chkManual.UseVisualStyleBackColor = True
            Me.btnVerify.Enabled = False
            Me.btnVerify.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(&HF5, &H18F)
            Me.btnVerify.Location = point2
            Me.btnVerify.Name = "btnVerify"
            size2 = New Size(&H4B, &H16)
            Me.btnVerify.Size = size2
            Me.btnVerify.TabIndex = 3
            Me.btnVerify.Text = "Verify"
            Me.btnVerify.UseVisualStyleBackColor = True
            Me.lblIMDB.AutoSize = True
            Me.lblIMDB.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(&H1D8, &HF7)
            Me.lblIMDB.Location = point2
            Me.lblIMDB.Name = "lblIMDB"
            size2 = New Size(&H23, 13)
            Me.lblIMDB.Size = size2
            Me.lblIMDB.TabIndex = 60
            Me.lblIMDB.Text = "IMDB"
            Me.lblIMDB.Visible = False
            Me.lblYearHeader.AutoSize = True
            Me.lblYearHeader.Font = New Font("Segoe UI", 8.25!, FontStyle.Bold, GraphicsUnit.Point, &HEE)
            point2 = New Point(410, 130)
            Me.lblYearHeader.Location = point2
            Me.lblYearHeader.Name = "lblYearHeader"
            size2 = New Size(&H21, 13)
            Me.lblYearHeader.Size = size2
            Me.lblYearHeader.TabIndex = &H3D
            Me.lblYearHeader.Text = "Year:"
            Me.lblYearHeader.Visible = False
            Me.lblDirectorHeader.AutoSize = True
            Me.lblDirectorHeader.Font = New Font("Segoe UI", 8.25!, FontStyle.Bold, GraphicsUnit.Point, &HEE)
            point2 = New Point(410, &H9C)
            Me.lblDirectorHeader.Location = point2
            Me.lblDirectorHeader.Name = "lblDirectorHeader"
            size2 = New Size(&H33, 13)
            Me.lblDirectorHeader.Size = size2
            Me.lblDirectorHeader.TabIndex = &H3E
            Me.lblDirectorHeader.Text = "Director:"
            Me.lblDirectorHeader.Visible = False
            Me.lblGenreHeader.AutoSize = True
            Me.lblGenreHeader.Font = New Font("Segoe UI", 8.25!, FontStyle.Bold, GraphicsUnit.Point, &HEE)
            point2 = New Point(410, &HB7)
            Me.lblGenreHeader.Location = point2
            Me.lblGenreHeader.Name = "lblGenreHeader"
            size2 = New Size(&H36, 13)
            Me.lblGenreHeader.Size = size2
            Me.lblGenreHeader.TabIndex = &H3F
            Me.lblGenreHeader.Text = "Genre(s):"
            Me.lblGenreHeader.Visible = False
            Me.lblIMDBHeader.AutoSize = True
            Me.lblIMDBHeader.Font = New Font("Segoe UI", 8.25!, FontStyle.Bold, GraphicsUnit.Point, &HEE)
            point2 = New Point(410, &HF7)
            Me.lblIMDBHeader.Location = point2
            Me.lblIMDBHeader.Name = "lblIMDBHeader"
            size2 = New Size(&H35, 13)
            Me.lblIMDBHeader.Size = size2
            Me.lblIMDBHeader.TabIndex = &H40
            Me.lblIMDBHeader.Text = "IMDB ID:"
            Me.lblIMDBHeader.Visible = False
            Me.lblPlotHeader.AutoSize = True
            Me.lblPlotHeader.Font = New Font("Segoe UI", 8.25!, FontStyle.Bold, GraphicsUnit.Point, &HEE)
            point2 = New Point(&H123, &H117)
            Me.lblPlotHeader.Location = point2
            Me.lblPlotHeader.Name = "lblPlotHeader"
            size2 = New Size(&H53, 13)
            Me.lblPlotHeader.Size = size2
            Me.lblPlotHeader.TabIndex = &H41
            Me.lblPlotHeader.Text = "Plot Summary:"
            Me.lblPlotHeader.Visible = False
            Me.btnSearch.Image = DirectCast(manager.GetObject("btnSearch.Image"), Image)
            point2 = New Point(&H106, 70)
            Me.btnSearch.Location = point2
            Me.btnSearch.Name = "btnSearch"
            size2 = New Size(&H17, &H17)
            Me.btnSearch.Size = size2
            Me.btnSearch.TabIndex = &H43
            Me.btnSearch.UseVisualStyleBackColor = True
            point2 = New Point(4, &H47)
            Me.txtSearch.Location = point2
            Me.txtSearch.Name = "txtSearch"
            size2 = New Size(&HFC, &H16)
            Me.txtSearch.Size = size2
            Me.txtSearch.TabIndex = &H42
            Me.pnlLoading.BackColor = Color.White
            Me.pnlLoading.BorderStyle = BorderStyle.FixedSingle
            Me.pnlLoading.Controls.Add(Me.Label3)
            Me.pnlLoading.Controls.Add(Me.ProgressBar1)
            point2 = New Point(&H16C, &HB7)
            Me.pnlLoading.Location = point2
            Me.pnlLoading.Name = "pnlLoading"
            size2 = New Size(200, &H36)
            Me.pnlLoading.Size = size2
            Me.pnlLoading.TabIndex = &H44
            Me.Label3.AutoSize = True
            point2 = New Point(3, 10)
            Me.Label3.Location = point2
            Me.Label3.Name = "Label3"
            size2 = New Size(&H62, 13)
            Me.Label3.Size = size2
            Me.Label3.TabIndex = 1
            Me.Label3.Text = "Searching IMDB..."
            Me.Label3.TextAlign = ContentAlignment.MiddleLeft
            point2 = New Point(3, &H20)
            Me.ProgressBar1.Location = point2
            Me.ProgressBar1.MarqueeAnimationSpeed = &H19
            Me.ProgressBar1.Name = "ProgressBar1"
            size2 = New Size(&HC0, &H11)
            Me.ProgressBar1.Size = size2
            Me.ProgressBar1.Style = ProgressBarStyle.Marquee
            Me.ProgressBar1.TabIndex = 0
            Me.pnlPicStatus.BackColor = Color.LightSteelBlue
            Me.pnlPicStatus.BorderStyle = BorderStyle.FixedSingle
            Me.pnlPicStatus.Controls.Add(Me.Label4)
            point2 = New Point(&H134, &H9B)
            Me.pnlPicStatus.Location = point2
            Me.pnlPicStatus.Name = "pnlPicStatus"
            size2 = New Size(&H51, &H2D)
            Me.pnlPicStatus.Size = size2
            Me.pnlPicStatus.TabIndex = &H45
            point2 = New Point(5, 5)
            Me.Label4.Location = point2
            Me.Label4.Name = "Label4"
            size2 = New Size(70, &H21)
            Me.Label4.Size = size2
            Me.Label4.TabIndex = 0
            Me.Label4.Text = "Fetching Poster..."
            Me.Label4.TextAlign = ContentAlignment.MiddleCenter
            Me.AcceptButton = Me.OK_Button
            Dim ef2 As New SizeF(96!, 96!)
            Me.AutoScaleDimensions = ef2
            Me.AutoScaleMode = AutoScaleMode.Dpi
            Me.CancelButton = Me.Cancel_Button
            size2 = New Size(&H283, &H1AF)
            Me.ClientSize = size2
            Me.ControlBox = False
            Me.Controls.Add(Me.pnlLoading)
            Me.Controls.Add(Me.pnlPicStatus)
            Me.Controls.Add(Me.Cancel_Button)
            Me.Controls.Add(Me.OK_Button)
            Me.Controls.Add(Me.btnSearch)
            Me.Controls.Add(Me.txtSearch)
            Me.Controls.Add(Me.lblPlotHeader)
            Me.Controls.Add(Me.lblIMDBHeader)
            Me.Controls.Add(Me.lblGenreHeader)
            Me.Controls.Add(Me.lblDirectorHeader)
            Me.Controls.Add(Me.lblYearHeader)
            Me.Controls.Add(Me.lblIMDB)
            Me.Controls.Add(Me.btnVerify)
            Me.Controls.Add(Me.chkManual)
            Me.Controls.Add(Me.pnlTop)
            Me.Controls.Add(Me.txtIMDBID)
            Me.Controls.Add(Me.lblGenre)
            Me.Controls.Add(Me.lblDirector)
            Me.Controls.Add(Me.lblYear)
            Me.Controls.Add(Me.txtOutline)
            Me.Controls.Add(Me.lblTagline)
            Me.Controls.Add(Me.lblTitle)
            Me.Controls.Add(Me.pbPoster)
            Me.Controls.Add(Me.tvResults)
            Me.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            Me.FormBorderStyle = FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            size2 = New Size(&H289, 460)
            Me.MinimumSize = size2
            Me.Name = "dlgIMDBSearchResults"
            Me.ShowIcon = False
            Me.StartPosition = FormStartPosition.CenterParent
            Me.Text = "Search Results"
            DirectCast(Me.pbPoster, ISupportInitialize).EndInit
            Me.pnlTop.ResumeLayout(False)
            Me.pnlTop.PerformLayout
            DirectCast(Me.PictureBox1, ISupportInitialize).EndInit
            Me.pnlLoading.ResumeLayout(False)
            Me.pnlLoading.PerformLayout
            Me.pnlPicStatus.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout
        End Sub

        Private Sub OK_Button_Click(ByVal sender As Object, ByVal e As EventArgs)
            Try 
                If (Me.chkManual.Checked AndAlso Me.btnVerify.Enabled) Then
                    If Not Regex.IsMatch(Me.txtIMDBID.Text.Replace("tt", String.Empty), "\d\d\d\d\d\d\d") Then
                        Interaction.MsgBox(Master.eLang.GetString(12, "The ID you entered is not a valid IMDB ID.", False), MsgBoxStyle.Exclamation, Master.eLang.GetString(&H124, "Invalid Entry", True))
                        Return
                    End If
                    If (Interaction.MsgBox((Master.eLang.GetString(13, "You have manually entered an IMDB ID but have not verified it is correct.", False) & ChrW(13) & ChrW(10) & ChrW(13) & ChrW(10) & Master.eLang.GetString(&H65, "Are you sure you want to continue?", True)), MsgBoxStyle.YesNo, Master.eLang.GetString(14, "Continue without verification?", False)) = MsgBoxResult.No) Then
                        Return
                    End If
                    Master.tmpMovie.IMDBID = Me.txtIMDBID.Text.Replace("tt", String.Empty)
                End If
                Me.DialogResult = DialogResult.OK
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
            Me.Close
        End Sub

        Private Sub SearchMovieInfoDownloaded(ByVal sPoster As String, ByVal bSuccess As Boolean)
            Me.pnlLoading.Visible = False
            Me.OK_Button.Enabled = True
            Try 
                If bSuccess Then
                    Me.ControlsVisible(True)
                    Me.lblTitle.Text = Master.tmpMovie.Title
                    Me.lblTagline.Text = Master.tmpMovie.Tagline
                    Me.lblYear.Text = Master.tmpMovie.Year
                    Me.lblDirector.Text = Master.tmpMovie.Director
                    Me.lblGenre.Text = Master.tmpMovie.Genre
                    Me.txtOutline.Text = Master.tmpMovie.Outline
                    Me.lblIMDB.Text = Master.tmpMovie.IMDBID
                    If Me._PosterCache.ContainsKey(Master.tmpMovie.IMDBID) Then
                        Me.pbPoster.Image = Me._PosterCache.Item(Master.tmpMovie.IMDBID)
                    ElseIf Not String.IsNullOrEmpty(sPoster) Then
                        If Me.bwDownloadPic.IsBusy Then
                            Me.bwDownloadPic.CancelAsync
                        End If
                        Me.pnlPicStatus.Visible = True
                        Me.bwDownloadPic = New BackgroundWorker
                        Me.bwDownloadPic.WorkerSupportsCancellation = True
                        Dim argument As New Arguments With { _
                            .pURL = sPoster, _
                            .IMDBId = Master.tmpMovie.IMDBID _
                        }
                        Me.bwDownloadPic.RunWorkerAsync(argument)
                    End If
                    If Not Me._InfoCache.ContainsKey(Master.tmpMovie.IMDBID) Then
                        Me._InfoCache.Add(Master.tmpMovie.IMDBID, Me.GetMovieClone(Master.tmpMovie))
                    End If
                    Me.btnVerify.Enabled = False
                ElseIf Me.chkManual.Checked Then
                    Interaction.MsgBox(Master.eLang.GetString(15, "Unable to retrieve movie details for the entered IMDB ID. Please check your entry and try again.", False), MsgBoxStyle.Exclamation, Master.eLang.GetString(&H10, "Verification Failed", False))
                    Me.btnVerify.Enabled = True
                End If
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Private Sub SearchResultsDownloaded(ByVal M As MovieSearchResults)
            Try 
                Me.tvResults.Nodes.Clear
                Me.ClearInfo
                If Not Information.IsNothing(M) Then
                    Dim node3 As TreeNode
                    If (((M.PartialMatches.Count > 0) OrElse (M.PopularTitles.Count > 0)) OrElse (M.ExactMatches.Count > 0)) Then
                        Dim node2 As New TreeNode(String.Format(Master.eLang.GetString(&H11, "Partial Matches ({0})", False), M.PartialMatches.Count))
                        Dim firstNode As New TreeNode
                        If (M.PartialMatches.Count > 0) Then
                            M.PartialMatches.Sort
                            Dim movie As Movie
                            For Each movie In M.PartialMatches
                                node3 = New TreeNode With { _
                                    .Text = (movie.Title & If(Not String.IsNullOrEmpty(movie.Year), String.Format(" ({0})", movie.Year), String.Empty)), _
                                    .Tag = movie.IMDBID _
                                }
                                node2.Nodes.Add(node3)
                            Next
                            node2.Expand
                            Me.tvResults.Nodes.Add(node2)
                            firstNode = node2.FirstNode
                        End If
                        If (M.ExactMatches.Count > 0) Then
                            M.ExactMatches.Sort
                            If (M.PartialMatches.Count > 0) Then
                                Me.tvResults.Nodes.Item(node2.Index).Collapse
                            End If
                            node2 = New TreeNode(String.Format(Master.eLang.GetString(&H12, "Exact Matches ({0})", False), M.ExactMatches.Count))
                            Dim movie2 As Movie
                            For Each movie2 In M.ExactMatches
                                node3 = New TreeNode With { _
                                    .Text = (movie2.Title & If(Not String.IsNullOrEmpty(movie2.Year), String.Format(" ({0})", movie2.Year), String.Empty)), _
                                    .Tag = movie2.IMDBID _
                                }
                                node2.Nodes.Add(node3)
                            Next
                            node2.Expand
                            Me.tvResults.Nodes.Add(node2)
                            firstNode = node2.FirstNode
                        End If
                        If (M.PopularTitles.Count > 0) Then
                            M.PopularTitles.Sort
                            If ((M.PartialMatches.Count > 0) OrElse (M.ExactMatches.Count > 0)) Then
                                Me.tvResults.Nodes.Item(node2.Index).Collapse
                            End If
                            node2 = New TreeNode(String.Format(Master.eLang.GetString(&H13, "Popular Titles ({0})", False), M.PopularTitles.Count))
                            Dim movie3 As Movie
                            For Each movie3 In M.PopularTitles
                                node3 = New TreeNode With { _
                                    .Text = (movie3.Title & If(Not String.IsNullOrEmpty(movie3.Year), String.Format(" ({0})", movie3.Year), String.Empty)), _
                                    .Tag = movie3.IMDBID _
                                }
                                node2.Nodes.Add(node3)
                            Next
                            node2.Expand
                            Me.tvResults.Nodes.Add(node2)
                            firstNode = node2.FirstNode
                        End If
                        Me._prevnode = -2
                        If (M.ExactMatches.Count > 0) Then
                            If (M.ExactMatches.Count = 1) Then
                                Me.tvResults.SelectedNode = firstNode
                            Else
                                Me.tvResults.SelectedNode = Nothing
                            End If
                        Else
                            Me.tvResults.SelectedNode = Nothing
                        End If
                        Me.tvResults.Focus
                    Else
                        node3 = New TreeNode With { _
                            .Text = Master.eLang.GetString(20, "No Matches Found", False) _
                        }
                        Me.tvResults.Nodes.Add(node3)
                    End If
                End If
                Me.pnlLoading.Visible = False
                Me.chkManual.Enabled = True
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Private Sub SetUp()
            Me.OK_Button.Text = Master.eLang.GetString(&HB3, "OK", True)
            Me.Cancel_Button.Text = Master.eLang.GetString(&HA7, "Cancel", True)
            Me.Label2.Text = Master.eLang.GetString(&H15, "View details of each result to find the proper movie.", False)
            Me.Label1.Text = Master.eLang.GetString(&H16, "Movie Search Results", False)
            Me.chkManual.Text = Master.eLang.GetString(&H17, "Manual IMDB Entry:", False)
            Me.btnVerify.Text = Master.eLang.GetString(&H18, "Verify", False)
            Me.lblYearHeader.Text = Master.eLang.GetString(&H31, "Year:", True)
            Me.lblDirectorHeader.Text = Master.eLang.GetString(&HEF, "Director:", True)
            Me.lblGenreHeader.Text = Master.eLang.GetString(&H33, "Genre(s):", True)
            Me.lblIMDBHeader.Text = Master.eLang.GetString(&H121, "IMDB ID:", True)
            Me.lblPlotHeader.Text = Master.eLang.GetString(&HF2, "Plot Outline:", True)
            Me.Label3.Text = Master.eLang.GetString(&H19, "Searching IMDB...", False)
        End Sub

        Public Function ShowDialog(ByVal Res As MovieSearchResults, ByVal sMovieTitle As String) As DialogResult
            Me.tmrWait.Enabled = False
            Me.tmrWait.Interval = 250
            Me.tmrLoad.Enabled = False
            Me.tmrLoad.Interval = 100
            Me.Text = (Master.eLang.GetString(10, "Search Results - ", False) & sMovieTitle)
            Me.txtSearch.Text = sMovieTitle
            Me.SearchResultsDownloaded(Res)
            Return MyBase.ShowDialog
        End Function

        Public Function ShowDialog(ByVal sMovieTitle As String, ByVal filterOptions As ScrapeOptions) As DialogResult
            Me.tmrWait.Enabled = False
            Me.tmrWait.Interval = 250
            Me.tmrLoad.Enabled = False
            Me.tmrLoad.Interval = 100
            Me._filterOptions = filterOptions
            Me.Text = (Master.eLang.GetString(10, "Search Results - ", False) & sMovieTitle)
            Me.txtSearch.Text = sMovieTitle
            Me.chkManual.Enabled = False
            Me.IMDB.IMDBURL = Me.IMDBURL
            Me.IMDB.SearchMovieAsync(sMovieTitle, Me._filterOptions)
            Return MyBase.ShowDialog
        End Function

        Private Sub tmrLoad_Tick(ByVal sender As Object, ByVal e As EventArgs)
            Me.tmrWait.Stop
            Me.tmrLoad.Stop
            Me.pnlLoading.Visible = True
            Me.Label3.Text = Master.eLang.GetString(&H1A, "Downloading details...", False)
            Me.IMDB.IMDBURL = Me.IMDBURL
            Me.IMDB.GetSearchMovieInfoAsync(Me.tvResults.SelectedNode.Tag.ToString, Master.tmpMovie, Master.DefaultOptions)
        End Sub

        Private Sub tmrWait_Tick(ByVal sender As Object, ByVal e As EventArgs)
            If (Me._prevnode <> Me._currnode) Then
                Me._prevnode = Me._currnode
                Me.tmrWait.Stop
                Me.tmrLoad.Start
            Else
                Me.tmrLoad.Stop
                Me.tmrWait.Stop
            End If
        End Sub

        Private Sub tvResults_AfterSelect(ByVal sender As Object, ByVal e As TreeViewEventArgs)
            Try 
                Me.tmrWait.Stop
                Me.tmrLoad.Stop
                Me.ClearInfo
                Me.OK_Button.Enabled = False
                If (Not Information.IsNothing(RuntimeHelpers.GetObjectValue(Me.tvResults.SelectedNode.Tag)) AndAlso Not String.IsNullOrEmpty(Me.tvResults.SelectedNode.Tag.ToString)) Then
                    Me._currnode = Me.tvResults.SelectedNode.Index
                    If Me._InfoCache.ContainsKey(Me.tvResults.SelectedNode.Tag.ToString) Then
                        Master.tmpMovie = Me.GetMovieClone(Me._InfoCache.Item(Me.tvResults.SelectedNode.Tag.ToString))
                        Me.SearchMovieInfoDownloaded(String.Empty, True)
                    Else
                        Me.pnlLoading.Visible = True
                        Me.tmrWait.Start
                    End If
                Else
                    Me.pnlLoading.Visible = False
                End If
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Private Sub tvResults_GotFocus(ByVal sender As Object, ByVal e As EventArgs)
            Me.AcceptButton = Me.OK_Button
        End Sub

        Private Sub txtIMDBID_GotFocus(ByVal sender As Object, ByVal e As EventArgs)
            Me.AcceptButton = Me.btnVerify
        End Sub

        Private Sub txtIMDBID_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
            If Me.chkManual.Checked Then
                Me.btnVerify.Enabled = True
                Me.OK_Button.Enabled = False
            End If
        End Sub

        Private Sub txtSearch_GotFocus(ByVal sender As Object, ByVal e As EventArgs)
            Me.AcceptButton = Me.btnSearch
        End Sub


        ' Properties
        Friend Overridable Property btnSearch As Button
            <DebuggerNonUserCode> _
            Get
                Return Me._btnSearch
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Button)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.btnSearch_Click)
                If (Not Me._btnSearch Is Nothing) Then
                    RemoveHandler Me._btnSearch.Click, handler
                End If
                Me._btnSearch = WithEventsValue
                If (Not Me._btnSearch Is Nothing) Then
                    AddHandler Me._btnSearch.Click, handler
                End If
            End Set
        End Property

        Friend Overridable Property btnVerify As Button
            <DebuggerNonUserCode> _
            Get
                Return Me._btnVerify
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Button)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.btnVerify_Click)
                If (Not Me._btnVerify Is Nothing) Then
                    RemoveHandler Me._btnVerify.Click, handler
                End If
                Me._btnVerify = WithEventsValue
                If (Not Me._btnVerify Is Nothing) Then
                    AddHandler Me._btnVerify.Click, handler
                End If
            End Set
        End Property

        Friend Overridable Property bwDownloadPic As BackgroundWorker
            <DebuggerNonUserCode> _
            Get
                Return Me._bwDownloadPic
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As BackgroundWorker)
                Dim handler As RunWorkerCompletedEventHandler = New RunWorkerCompletedEventHandler(AddressOf Me.bwDownloadPic_RunWorkerCompleted)
                Dim handler2 As DoWorkEventHandler = New DoWorkEventHandler(AddressOf Me.bwDownloadPic_DoWork)
                If (Not Me._bwDownloadPic Is Nothing) Then
                    RemoveHandler Me._bwDownloadPic.RunWorkerCompleted, handler
                    RemoveHandler Me._bwDownloadPic.DoWork, handler2
                End If
                Me._bwDownloadPic = WithEventsValue
                If (Not Me._bwDownloadPic Is Nothing) Then
                    AddHandler Me._bwDownloadPic.RunWorkerCompleted, handler
                    AddHandler Me._bwDownloadPic.DoWork, handler2
                End If
            End Set
        End Property

        Friend Overridable Property Cancel_Button As Button
            <DebuggerNonUserCode> _
            Get
                Return Me._Cancel_Button
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Button)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.Cancel_Button_Click)
                If (Not Me._Cancel_Button Is Nothing) Then
                    RemoveHandler Me._Cancel_Button.Click, handler
                End If
                Me._Cancel_Button = WithEventsValue
                If (Not Me._Cancel_Button Is Nothing) Then
                    AddHandler Me._Cancel_Button.Click, handler
                End If
            End Set
        End Property

        Friend Overridable Property chkManual As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._chkManual
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.chkManual_CheckedChanged)
                If (Not Me._chkManual Is Nothing) Then
                    RemoveHandler Me._chkManual.CheckedChanged, handler
                End If
                Me._chkManual = WithEventsValue
                If (Not Me._chkManual Is Nothing) Then
                    AddHandler Me._chkManual.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property Label1 As Label
            <DebuggerNonUserCode> _
            Get
                Return Me._Label1
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Label)
                Me._Label1 = WithEventsValue
            End Set
        End Property

        Friend Overridable Property Label2 As Label
            <DebuggerNonUserCode> _
            Get
                Return Me._Label2
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Label)
                Me._Label2 = WithEventsValue
            End Set
        End Property

        Friend Overridable Property Label3 As Label
            <DebuggerNonUserCode> _
            Get
                Return Me._Label3
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Label)
                Me._Label3 = WithEventsValue
            End Set
        End Property

        Friend Overridable Property Label4 As Label
            <DebuggerNonUserCode> _
            Get
                Return Me._Label4
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Label)
                Me._Label4 = WithEventsValue
            End Set
        End Property

        Friend Overridable Property lblDirector As Label
            <DebuggerNonUserCode> _
            Get
                Return Me._lblDirector
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Label)
                Me._lblDirector = WithEventsValue
            End Set
        End Property

        Friend Overridable Property lblDirectorHeader As Label
            <DebuggerNonUserCode> _
            Get
                Return Me._lblDirectorHeader
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Label)
                Me._lblDirectorHeader = WithEventsValue
            End Set
        End Property

        Friend Overridable Property lblGenre As Label
            <DebuggerNonUserCode> _
            Get
                Return Me._lblGenre
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Label)
                Me._lblGenre = WithEventsValue
            End Set
        End Property

        Friend Overridable Property lblGenreHeader As Label
            <DebuggerNonUserCode> _
            Get
                Return Me._lblGenreHeader
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Label)
                Me._lblGenreHeader = WithEventsValue
            End Set
        End Property

        Friend Overridable Property lblIMDB As Label
            <DebuggerNonUserCode> _
            Get
                Return Me._lblIMDB
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Label)
                Me._lblIMDB = WithEventsValue
            End Set
        End Property

        Friend Overridable Property lblIMDBHeader As Label
            <DebuggerNonUserCode> _
            Get
                Return Me._lblIMDBHeader
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Label)
                Me._lblIMDBHeader = WithEventsValue
            End Set
        End Property

        Friend Overridable Property lblPlotHeader As Label
            <DebuggerNonUserCode> _
            Get
                Return Me._lblPlotHeader
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Label)
                Me._lblPlotHeader = WithEventsValue
            End Set
        End Property

        Friend Overridable Property lblTagline As Label
            <DebuggerNonUserCode> _
            Get
                Return Me._lblTagline
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Label)
                Me._lblTagline = WithEventsValue
            End Set
        End Property

        Friend Overridable Property lblTitle As Label
            <DebuggerNonUserCode> _
            Get
                Return Me._lblTitle
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Label)
                Me._lblTitle = WithEventsValue
            End Set
        End Property

        Friend Overridable Property lblYear As Label
            <DebuggerNonUserCode> _
            Get
                Return Me._lblYear
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Label)
                Me._lblYear = WithEventsValue
            End Set
        End Property

        Friend Overridable Property lblYearHeader As Label
            <DebuggerNonUserCode> _
            Get
                Return Me._lblYearHeader
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Label)
                Me._lblYearHeader = WithEventsValue
            End Set
        End Property

        Friend Overridable Property OK_Button As Button
            <DebuggerNonUserCode> _
            Get
                Return Me._OK_Button
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Button)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.OK_Button_Click)
                If (Not Me._OK_Button Is Nothing) Then
                    RemoveHandler Me._OK_Button.Click, handler
                End If
                Me._OK_Button = WithEventsValue
                If (Not Me._OK_Button Is Nothing) Then
                    AddHandler Me._OK_Button.Click, handler
                End If
            End Set
        End Property

        Friend Overridable Property pbPoster As PictureBox
            <DebuggerNonUserCode> _
            Get
                Return Me._pbPoster
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As PictureBox)
                Me._pbPoster = WithEventsValue
            End Set
        End Property

        Friend Overridable Property PictureBox1 As PictureBox
            <DebuggerNonUserCode> _
            Get
                Return Me._PictureBox1
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As PictureBox)
                Me._PictureBox1 = WithEventsValue
            End Set
        End Property

        Friend Overridable Property pnlLoading As Panel
            <DebuggerNonUserCode> _
            Get
                Return Me._pnlLoading
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Panel)
                Me._pnlLoading = WithEventsValue
            End Set
        End Property

        Friend Overridable Property pnlPicStatus As Panel
            <DebuggerNonUserCode> _
            Get
                Return Me._pnlPicStatus
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Panel)
                Me._pnlPicStatus = WithEventsValue
            End Set
        End Property

        Friend Overridable Property pnlTop As Panel
            <DebuggerNonUserCode> _
            Get
                Return Me._pnlTop
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Panel)
                Me._pnlTop = WithEventsValue
            End Set
        End Property

        Friend Overridable Property ProgressBar1 As ProgressBar
            <DebuggerNonUserCode> _
            Get
                Return Me._ProgressBar1
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As ProgressBar)
                Me._ProgressBar1 = WithEventsValue
            End Set
        End Property

        Friend Overridable Property tmrLoad As Timer
            <DebuggerNonUserCode> _
            Get
                Return Me._tmrLoad
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Timer)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.tmrLoad_Tick)
                If (Not Me._tmrLoad Is Nothing) Then
                    RemoveHandler Me._tmrLoad.Tick, handler
                End If
                Me._tmrLoad = WithEventsValue
                If (Not Me._tmrLoad Is Nothing) Then
                    AddHandler Me._tmrLoad.Tick, handler
                End If
            End Set
        End Property

        Friend Overridable Property tmrWait As Timer
            <DebuggerNonUserCode> _
            Get
                Return Me._tmrWait
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Timer)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.tmrWait_Tick)
                If (Not Me._tmrWait Is Nothing) Then
                    RemoveHandler Me._tmrWait.Tick, handler
                End If
                Me._tmrWait = WithEventsValue
                If (Not Me._tmrWait Is Nothing) Then
                    AddHandler Me._tmrWait.Tick, handler
                End If
            End Set
        End Property

        Friend Overridable Property tvResults As TreeView
            <DebuggerNonUserCode> _
            Get
                Return Me._tvResults
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As TreeView)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.tvResults_GotFocus)
                Dim handler2 As TreeViewEventHandler = New TreeViewEventHandler(AddressOf Me.tvResults_AfterSelect)
                If (Not Me._tvResults Is Nothing) Then
                    RemoveHandler Me._tvResults.GotFocus, handler
                    RemoveHandler Me._tvResults.AfterSelect, handler2
                End If
                Me._tvResults = WithEventsValue
                If (Not Me._tvResults Is Nothing) Then
                    AddHandler Me._tvResults.GotFocus, handler
                    AddHandler Me._tvResults.AfterSelect, handler2
                End If
            End Set
        End Property

        Friend Overridable Property txtIMDBID As TextBox
            <DebuggerNonUserCode> _
            Get
                Return Me._txtIMDBID
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As TextBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.txtIMDBID_TextChanged)
                Dim handler2 As EventHandler = New EventHandler(AddressOf Me.txtIMDBID_GotFocus)
                If (Not Me._txtIMDBID Is Nothing) Then
                    RemoveHandler Me._txtIMDBID.TextChanged, handler
                    RemoveHandler Me._txtIMDBID.GotFocus, handler2
                End If
                Me._txtIMDBID = WithEventsValue
                If (Not Me._txtIMDBID Is Nothing) Then
                    AddHandler Me._txtIMDBID.TextChanged, handler
                    AddHandler Me._txtIMDBID.GotFocus, handler2
                End If
            End Set
        End Property

        Friend Overridable Property txtOutline As TextBox
            <DebuggerNonUserCode> _
            Get
                Return Me._txtOutline
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As TextBox)
                Me._txtOutline = WithEventsValue
            End Set
        End Property

        Friend Overridable Property txtSearch As TextBox
            <DebuggerNonUserCode> _
            Get
                Return Me._txtSearch
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As TextBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.txtSearch_GotFocus)
                If (Not Me._txtSearch Is Nothing) Then
                    RemoveHandler Me._txtSearch.GotFocus, handler
                End If
                Me._txtSearch = WithEventsValue
                If (Not Me._txtSearch Is Nothing) Then
                    AddHandler Me._txtSearch.GotFocus, handler
                End If
            End Set
        End Property


        ' Fields
        Private Shared __ENCList As List(Of WeakReference) = New List(Of WeakReference)
        <AccessedThroughProperty("btnSearch")> _
        Private _btnSearch As Button
        <AccessedThroughProperty("btnVerify")> _
        Private _btnVerify As Button
        <AccessedThroughProperty("bwDownloadPic")> _
        Private _bwDownloadPic As BackgroundWorker
        <AccessedThroughProperty("Cancel_Button")> _
        Private _Cancel_Button As Button
        <AccessedThroughProperty("chkManual")> _
        Private _chkManual As CheckBox
        Private _currnode As Integer
        Private _filterOptions As ScrapeOptions
        Private _InfoCache As Dictionary(Of String, Movie)
        <AccessedThroughProperty("Label1")> _
        Private _Label1 As Label
        <AccessedThroughProperty("Label2")> _
        Private _Label2 As Label
        <AccessedThroughProperty("Label3")> _
        Private _Label3 As Label
        <AccessedThroughProperty("Label4")> _
        Private _Label4 As Label
        <AccessedThroughProperty("lblDirector")> _
        Private _lblDirector As Label
        <AccessedThroughProperty("lblDirectorHeader")> _
        Private _lblDirectorHeader As Label
        <AccessedThroughProperty("lblGenre")> _
        Private _lblGenre As Label
        <AccessedThroughProperty("lblGenreHeader")> _
        Private _lblGenreHeader As Label
        <AccessedThroughProperty("lblIMDB")> _
        Private _lblIMDB As Label
        <AccessedThroughProperty("lblIMDBHeader")> _
        Private _lblIMDBHeader As Label
        <AccessedThroughProperty("lblPlotHeader")> _
        Private _lblPlotHeader As Label
        <AccessedThroughProperty("lblTagline")> _
        Private _lblTagline As Label
        <AccessedThroughProperty("lblTitle")> _
        Private _lblTitle As Label
        <AccessedThroughProperty("lblYear")> _
        Private _lblYear As Label
        <AccessedThroughProperty("lblYearHeader")> _
        Private _lblYearHeader As Label
        <AccessedThroughProperty("OK_Button")> _
        Private _OK_Button As Button
        <AccessedThroughProperty("pbPoster")> _
        Private _pbPoster As PictureBox
        <AccessedThroughProperty("PictureBox1")> _
        Private _PictureBox1 As PictureBox
        <AccessedThroughProperty("pnlLoading")> _
        Private _pnlLoading As Panel
        <AccessedThroughProperty("pnlPicStatus")> _
        Private _pnlPicStatus As Panel
        <AccessedThroughProperty("pnlTop")> _
        Private _pnlTop As Panel
        Private _PosterCache As Dictionary(Of String, Image)
        Private _prevnode As Integer
        <AccessedThroughProperty("ProgressBar1")> _
        Private _ProgressBar1 As ProgressBar
        <AccessedThroughProperty("tmrLoad")> _
        Private _tmrLoad As Timer
        <AccessedThroughProperty("tmrWait")> _
        Private _tmrWait As Timer
        <AccessedThroughProperty("tvResults")> _
        Private _tvResults As TreeView
        <AccessedThroughProperty("txtIMDBID")> _
        Private _txtIMDBID As TextBox
        <AccessedThroughProperty("txtOutline")> _
        Private _txtOutline As TextBox
        <AccessedThroughProperty("txtSearch")> _
        Private _txtSearch As TextBox
        Private components As IContainer
        Private IMDB As Scraper
        Public IMDBURL As String
        Private sHTTP As HTTP
        Private UseOFDBGenre As Boolean
        Private UseOFDBOutline As Boolean
        Private UseOFDBPlot As Boolean
        Private UseOFDBTitle As Boolean

        ' Nested Types
        <StructLayout(LayoutKind.Sequential)> _
        Private Structure Arguments
            Public pURL As String
            Public IMDBId As String
        End Structure

        <StructLayout(LayoutKind.Sequential)> _
        Private Structure Results
            Public Result As Image
            Public IMDBId As String
        End Structure
    End Class
End Namespace

