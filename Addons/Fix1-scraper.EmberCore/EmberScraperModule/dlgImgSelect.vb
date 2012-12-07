Imports EmberAPI
Imports EmberAPI.FileUtils
Imports EmberAPI.MediaContainers
Imports EmberScraperModule.IMPA
Imports EmberScraperModule.MPDB
Imports EmberScraperModule.TMDB
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Linq
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Windows.Forms

Namespace EmberScraperModule
    <DesignerGenerated> _
    Public Class dlgImgSelect
        Inherits Form
        ' Events
        Private Custom Event IMPADone As IMPADoneEventHandler
        Private Custom Event MPDBDone As MPDBDoneEventHandler
        Private Custom Event TMDBDone As TMDBDoneEventHandler

        ' Methods
        Public Sub New()
            AddHandler MyBase.Disposed, New EventHandler(AddressOf Me.dlgImgSelect_Disposed)
            AddHandler MyBase.FormClosing, New FormClosingEventHandler(AddressOf Me.dlgImgSelect_FormClosing)
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.dlgImgSelect_Load)
            AddHandler MyBase.Shown, New EventHandler(AddressOf Me.dlgImgSelect_Shown)
            dlgImgSelect.__ENCAddToList(Me)
            Me.bwIMPADownload = New BackgroundWorker
            Me.bwMPDBDownload = New BackgroundWorker
            Me.bwTMDBDownload = New BackgroundWorker
            Me.CachePath = String.Empty
            Me.ETHashes = New List(Of String)
            Me.iCounter = 0
            Me.iLeft = 5
            Me.IMPA = New Scraper
            Me.IMPAPosters = New List(Of Image)
            Me.isEdit = False
            Me.isShown = False
            Me.iTop = 5
            Me.MPDB = New Scraper
            Me.MPDBPosters = New List(Of Image)
            Me.noImages = False
            Me.PreDL = False
            Me.Results = New ImgResult
            Me.selIndex = -1
            Me.TMDB = New Scraper
            Me.TMDBPosters = New List(Of Image)
            Me.tMovie = New DBMovie
            Me.tmpImage = New Images
            Me._impaDone = True
            Me._mpdbDone = True
            Me._tmdbDone = True
            Me.InitializeComponent
        End Sub

        <DebuggerNonUserCode> _
        Private Shared Sub __ENCAddToList(ByVal value As Object)
            Dim list As List(Of WeakReference) = dlgImgSelect.__ENCList
            SyncLock list
                If (dlgImgSelect.__ENCList.Count = dlgImgSelect.__ENCList.Capacity) Then
                    Dim index As Integer = 0
                    Dim num3 As Integer = (dlgImgSelect.__ENCList.Count - 1)
                    Dim i As Integer = 0
                    Do While (i <= num3)
                        Dim reference As WeakReference = dlgImgSelect.__ENCList.Item(i)
                        If reference.IsAlive Then
                            If (i <> index) Then
                                dlgImgSelect.__ENCList.Item(index) = dlgImgSelect.__ENCList.Item(i)
                            End If
                            index += 1
                        End If
                        i += 1
                    Loop
                    dlgImgSelect.__ENCList.RemoveRange(index, (dlgImgSelect.__ENCList.Count - index))
                    dlgImgSelect.__ENCList.Capacity = dlgImgSelect.__ENCList.Count
                End If
                dlgImgSelect.__ENCList.Add(New WeakReference(RuntimeHelpers.GetObjectValue(value)))
            End SyncLock
        End Sub

        <CompilerGenerated> _
        Private Shared Function _Lambda$__91(ByVal s As Image) As Boolean
            Return (s.Description = "cover")
        End Function

        <CompilerGenerated> _
        Private Function _Lambda$__92(ByVal p As Image) As String
            Return Me.RemoveServerURL(p.URL)
        End Function

        Private Sub AddImage(ByVal iImage As Image, ByVal sDescription As String, ByVal iIndex As Integer, ByVal sURL As String, ByVal isChecked As Boolean, ByVal poster As Image)
            Try 
                Me.pnlImage = DirectCast(Utils.CopyArray(DirectCast(Me.pnlImage, Array), New Panel((iIndex + 1)  - 1) {}), Panel())
                Me.pbImage = DirectCast(Utils.CopyArray(DirectCast(Me.pbImage, Array), New PictureBox((iIndex + 1)  - 1) {}), PictureBox())
                Me.pnlImage(iIndex) = New Panel
                Me.pbImage(iIndex) = New PictureBox
                Me.pbImage(iIndex).Name = iIndex.ToString
                Me.pnlImage(iIndex).Name = iIndex.ToString
                Dim size2 As New Size(&H100, &H11E)
                Me.pnlImage(iIndex).Size = size2
                size2 = New Size(250, 250)
                Me.pbImage(iIndex).Size = size2
                Me.pnlImage(iIndex).BackColor = Color.White
                Me.pnlImage(iIndex).BorderStyle = BorderStyle.FixedSingle
                Me.pbImage(iIndex).SizeMode = PictureBoxSizeMode.Zoom
                Me.pnlImage(iIndex).Tag = poster
                Me.pbImage(iIndex).Tag = poster
                Me.pbImage(iIndex).Image = iImage
                Me.pnlImage(iIndex).Left = Me.iLeft
                Me.pbImage(iIndex).Left = 3
                Me.pnlImage(iIndex).Top = Me.iTop
                Me.pbImage(iIndex).Top = 3
                Me.pnlBG.Controls.Add(Me.pnlImage(iIndex))
                Me.pnlImage(iIndex).Controls.Add(Me.pbImage(iIndex))
                Me.pnlImage(iIndex).BringToFront
                AddHandler Me.pbImage(iIndex).Click, New EventHandler(AddressOf Me.pbImage_Click)
                AddHandler Me.pbImage(iIndex).DoubleClick, New EventHandler(AddressOf Me.pbImage_DoubleClick)
                AddHandler Me.pnlImage(iIndex).Click, New EventHandler(AddressOf Me.pnlImage_Click)
                AddHandler Me.pbImage(iIndex).MouseWheel, New MouseEventHandler(AddressOf Me.MouseWheelEvent)
                AddHandler Me.pnlImage(iIndex).MouseWheel, New MouseEventHandler(AddressOf Me.MouseWheelEvent)
                If (Me.DLType = ImageType.Fanart) Then
                    Me.chkImage = DirectCast(Utils.CopyArray(DirectCast(Me.chkImage, Array), New CheckBox((iIndex + 1)  - 1) {}), CheckBox())
                    Me.chkImage(iIndex) = New CheckBox
                    Me.chkImage(iIndex).Name = iIndex.ToString
                    size2 = New Size(250, 30)
                    Me.chkImage(iIndex).Size = size2
                    Me.chkImage(iIndex).AutoSize = False
                    Me.chkImage(iIndex).BackColor = Color.White
                    Me.chkImage(iIndex).TextAlign = ContentAlignment.MiddleCenter
                    Me.chkImage(iIndex).Text = Master.eLang.GetString(&H37, "Multiple", False)
                    Me.chkImage(iIndex).Left = 0
                    Me.chkImage(iIndex).Top = 250
                    Me.chkImage(iIndex).Checked = isChecked
                    Me.pnlImage(iIndex).Controls.Add(Me.chkImage(iIndex))
                    AddHandler Me.pnlImage(iIndex).MouseWheel, New MouseEventHandler(AddressOf Me.MouseWheelEvent)
                Else
                    Me.lblImage = DirectCast(Utils.CopyArray(DirectCast(Me.lblImage, Array), New Label((iIndex + 1)  - 1) {}), Label())
                    Me.lblImage(iIndex) = New Label
                    Me.lblImage(iIndex).Name = iIndex.ToString
                    size2 = New Size(250, 30)
                    Me.lblImage(iIndex).Size = size2
                    Me.lblImage(iIndex).AutoSize = False
                    Me.lblImage(iIndex).BackColor = Color.White
                    Me.lblImage(iIndex).TextAlign = ContentAlignment.MiddleCenter
                    If Me.IsTMDBURL(sURL) Then
                        Me.lblImage(iIndex).Text = Master.eLang.GetString(&H37, "Multiple", False)
                    Else
                        Me.lblImage(iIndex).Text = String.Format("{0}x{1} ({2})", Me.pbImage(iIndex).Image.Width.ToString, Me.pbImage(iIndex).Image.Height.ToString, sDescription)
                    End If
                    Me.lblImage(iIndex).Tag = poster
                    Me.lblImage(iIndex).Left = 0
                    Me.lblImage(iIndex).Top = 250
                    Me.pnlImage(iIndex).Controls.Add(Me.lblImage(iIndex))
                    AddHandler Me.lblImage(iIndex).Click, New EventHandler(AddressOf Me.lblImage_Click)
                    AddHandler Me.lblImage(iIndex).MouseWheel, New MouseEventHandler(AddressOf Me.MouseWheelEvent)
                End If
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
            Me.iCounter += 1
            If (Me.iCounter = 3) Then
                Me.iCounter = 0
                Me.iLeft = 5
                Me.iTop = (Me.iTop + &H12D)
            Else
                Me.iLeft = (Me.iLeft + &H10F)
            End If
        End Sub

        Private Sub AllDoneDownloading()
            If ((Me._impaDone AndAlso Me._tmdbDone) AndAlso Me._mpdbDone) Then
                Me.pnlDLStatus.Visible = False
                Me.TMDBPosters.AddRange(Me.IMPAPosters)
                Me.TMDBPosters.AddRange(Me.MPDBPosters)
                Me.ProcessPics(Me.TMDBPosters)
                Me.pnlBG.Visible = True
            End If
        End Sub

        Private Sub btnPreview_Click(ByVal sender As Object, ByVal e As EventArgs)
            Me.PreviewImage
        End Sub

        Private Sub bwIMPADownload_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
            Dim num2 As Integer = (Me.IMPAPosters.Count - 1)
            Dim i As Integer = 0
            Do While (i <= num2)
                If Me.bwIMPADownload.CancellationPending Then
                    e.Cancel = True
                    Exit Do
                End If
                Me.bwIMPADownload.ReportProgress((i + 1), Me.IMPAPosters.Item(i).URL)
                Try 
                    Me.IMPAPosters.Item(i).WebImage.FromWeb(Me.IMPAPosters.Item(i).URL)
                    If Not Master.eSettings.NoSaveImagesToNfo Then
                        Me.Results.Posters.Add(Me.IMPAPosters.Item(i).URL)
                    End If
                    If Master.eSettings.UseImgCache Then
                        Try 
                            Me.IMPAPosters.Item(i).URL = StringUtils.CleanURL(Me.IMPAPosters.Item(i).URL)
                            Me.IMPAPosters.Item(i).WebImage.Save(Path.Combine(Me.CachePath, String.Concat(New String() { "poster_(", Me.IMPAPosters.Item(i).Description, ")_(url=", Me.IMPAPosters.Item(i).URL, ").jpg" })), 0)
                        Catch exception1 As Exception
                            ProjectData.SetProjectError(exception1)
                            ProjectData.ClearProjectError
                        End Try
                    End If
                Catch exception2 As Exception
                    ProjectData.SetProjectError(exception2)
                    ProjectData.ClearProjectError
                End Try
                i += 1
            Loop
        End Sub

        Private Sub bwIMPADownload_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
            Try 
                Dim sString As String = e.UserState.ToString
                Me.lblDL2Status.Text = String.Format(Master.eLang.GetString(&H1B, "Downloading {0}", False), If((sString.Length > 40), StringUtils.TruncateURL(sString, 40, False), sString))
                Me.pbDL2.Value = e.ProgressPercentage
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Private Sub bwIMPADownload_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
            If Not e.Cancelled Then
                Me._impaDone = True
                Dim iMPADoneEvent As IMPADoneEventHandler = Me.IMPADoneEvent
                If (Not iMPADoneEvent Is Nothing) Then
                    iMPADoneEvent.Invoke
                End If
            End If
        End Sub

        Private Sub bwMPDBDownload_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
            Dim num2 As Integer = (Me.MPDBPosters.Count - 1)
            Dim i As Integer = 0
            Do While (i <= num2)
                Try 
                    If Me.bwMPDBDownload.CancellationPending Then
                        e.Cancel = True
                        Exit Do
                    End If
                    Me.bwMPDBDownload.ReportProgress((i + 1), Me.MPDBPosters.Item(i).URL)
                    Try 
                        Me.MPDBPosters.Item(i).WebImage.FromWeb(Me.MPDBPosters.Item(i).URL)
                        If Not Master.eSettings.NoSaveImagesToNfo Then
                            Me.Results.Posters.Add(Me.MPDBPosters.Item(i).URL)
                        End If
                        If Master.eSettings.UseImgCache Then
                            Try 
                                Me.MPDBPosters.Item(i).URL = StringUtils.CleanURL(Me.MPDBPosters.Item(i).URL)
                                Me.MPDBPosters.Item(i).WebImage.Save(Path.Combine(Me.CachePath, String.Concat(New String() { "poster_(", Me.MPDBPosters.Item(i).Description, ")_(url=", Me.MPDBPosters.Item(i).URL, ").jpg" })), 0)
                            Catch exception1 As Exception
                                ProjectData.SetProjectError(exception1)
                                ProjectData.ClearProjectError
                            End Try
                        End If
                    Catch exception2 As Exception
                        ProjectData.SetProjectError(exception2)
                        ProjectData.ClearProjectError
                    End Try
                Catch exception3 As Exception
                    ProjectData.SetProjectError(exception3)
                    ProjectData.ClearProjectError
                End Try
                i += 1
            Loop
        End Sub

        Private Sub bwMPDBDownload_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
            Try 
                Dim sString As String = e.UserState.ToString
                Me.lblDL3Status.Text = String.Format(Master.eLang.GetString(&H1B, "Downloading {0}", False), If((sString.Length > 40), StringUtils.TruncateURL(sString, 40, False), sString))
                Me.pbDL3.Value = e.ProgressPercentage
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Private Sub bwMPDBDownload_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
            If Not e.Cancelled Then
                Me._mpdbDone = True
                Dim mPDBDoneEvent As MPDBDoneEventHandler = Me.MPDBDoneEvent
                If (Not mPDBDoneEvent Is Nothing) Then
                    mPDBDoneEvent.Invoke
                End If
            End If
        End Sub

        Private Sub bwTMDBDownload_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
            Dim imageArray As Image()
            Dim e$__ As New _Closure$__11
            Dim str2 As String = String.Empty
            Dim sPath As String = String.Empty
            e$__.$VB$Local_extrathumbSize = String.Empty
            e$__.$VB$Local_extrathumbSize = AdvancedSettings.GetSetting("ManualETSize", "thumb", "")
            If (Me.DLType = ImageType.Fanart) Then
                imageArray = Me.TMDBPosters.Where(Of Image)(New Func(Of Image, Boolean)(AddressOf e$__._Lambda$__90)).ToArray(Of Image)()
            Else
                imageArray = Me.TMDBPosters.Where(Of Image)(New Func(Of Image, Boolean)(AddressOf dlgImgSelect._Lambda$__91)).ToArray(Of Image)()
            End If
            Dim num As Double = (100 / CDbl(imageArray.Count(Of Image)()))
            Dim num3 As Integer = (imageArray.Count(Of Image)() - 1)
            Dim i As Integer = 0
            Do While (i <= num3)
                Try 
                    If (((Me.DLType = ImageType.Fanart) OrElse Master.eSettings.UseImgCache) OrElse ((imageArray(i).Description = "cover") OrElse Master.eSettings.PosterPrefSizeOnly)) Then
                        If Me.bwTMDBDownload.CancellationPending Then
                            e.Cancel = True
                            Exit Do
                        End If
                        Me.bwTMDBDownload.ReportProgress(Convert.ToInt32(CDbl(((i + 1) * num))), imageArray(i).URL)
                        Try 
                            imageArray(i).WebImage.FromWeb(imageArray(i).URL)
                            If Not Master.eSettings.NoSaveImagesToNfo Then
                                If (Me.DLType = ImageType.Fanart) Then
                                    If Not imageArray(i).URL.Contains("-thumb.") Then
                                        Me.Results.Fanart.URL = Me.GetServerURL(imageArray(i).URL)
                                        str2 = Me.RemoveServerURL(imageArray(i).URL).Replace("-poster.", "-thumb.").Replace("-original.", "-thumb.")
                                        Dim item As New Thumb With { _
                                            .Preview = str2, _
                                            .Text = imageArray(i).URL.Replace("http://images.themoviedb.org", String.Empty) _
                                        }
                                        Me.Results.Fanart.Thumb.Add(item)
                                    End If
                                Else
                                    Me.Results.Posters.Add(imageArray(i).URL)
                                End If
                            End If
                            If (Master.eSettings.UseImgCache OrElse Master.eSettings.AutoET) Then
                                Try 
                                    imageArray(i).URL = Me.CleanTMDBURL(imageArray(i).URL)
                                    sPath = Path.Combine(Me.CachePath, String.Concat(New String() { If((Me.DLType = ImageType.Fanart), "fanart_(", "poster_("), imageArray(i).Description, ")_(url=", imageArray(i).URL, ").jpg" }))
                                    imageArray(i).WebImage.Save(sPath, 0)
                                    If Master.eSettings.AutoET Then
                                        Dim lrg As New FanartSize
                                        Select Case imageArray(i).Description.ToLower
                                            Case "original"
                                                lrg = FanartSize.Lrg
                                                Exit Select
                                            Case "mid"
                                                lrg = FanartSize.Mid
                                                Exit Select
                                            Case "thumb"
                                                lrg = FanartSize.Small
                                                Exit Select
                                        End Select
                                        If ((Master.eSettings.AutoETSize = lrg) AndAlso Not Me.ETHashes.Contains(HashFile.HashCalcFile(sPath))) Then
                                            imageArray(i).isChecked = True
                                        End If
                                    End If
                                Catch exception1 As Exception
                                    ProjectData.SetProjectError(exception1)
                                    ProjectData.ClearProjectError
                                End Try
                            End If
                        Catch exception2 As Exception
                            ProjectData.SetProjectError(exception2)
                            ProjectData.ClearProjectError
                        End Try
                    End If
                Catch exception3 As Exception
                    ProjectData.SetProjectError(exception3)
                    ProjectData.ClearProjectError
                End Try
                i += 1
            Loop
        End Sub

        Private Sub bwTMDBDownload_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
            Try 
                Dim sString As String = e.UserState.ToString
                Me.lblDL1Status.Text = String.Format(Master.eLang.GetString(&H1B, "Downloading {0}", False), If((sString.Length > 40), StringUtils.TruncateURL(sString, 40, False), sString))
                Me.pbDL1.Value = e.ProgressPercentage
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Private Sub bwTMDBDownload_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
            If Not e.Cancelled Then
                Me._tmdbDone = True
                Dim tMDBDoneEvent As TMDBDoneEventHandler = Me.TMDBDoneEvent
                If (Not tMDBDoneEvent Is Nothing) Then
                    tMDBDoneEvent.Invoke
                End If
            End If
        End Sub

        Private Sub Cancel_Button_Click(ByVal sender As Object, ByVal e As EventArgs)
            Me.IMPA.Cancel
            Me.MPDB.Cancel
            Me.TMDB.Cancel
            If Me.bwIMPADownload.IsBusy Then
                Me.bwIMPADownload.CancelAsync
            End If
            If Me.bwMPDBDownload.IsBusy Then
                Me.bwMPDBDownload.CancelAsync
            End If
            If Me.bwTMDBDownload.IsBusy Then
                Me.bwTMDBDownload.CancelAsync
            End If
            Do While ((Me.bwIMPADownload.IsBusy OrElse Me.bwMPDBDownload.IsBusy) OrElse Me.bwTMDBDownload.IsBusy)
                Application.DoEvents
                Thread.Sleep(50)
            Loop
            Me.DialogResult = DialogResult.Cancel
            Me.Close
        End Sub

        Private Sub CheckAll(ByVal sType As String, ByVal Checked As Boolean)
            Dim num2 As Integer = Information.UBound(Me.chkImage, 1)
            Dim i As Integer = 0
            Do While (i <= num2)
                If Me.chkImage(i).Text.ToLower.Contains(sType) Then
                    Me.chkImage(i).Checked = Checked
                End If
                i += 1
            Loop
        End Sub

        Private Sub chkMid_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Me.CheckAll("(poster)", Me.chkMid.Checked)
        End Sub

        Private Sub chkOriginal_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Me.CheckAll("(original)", Me.chkOriginal.Checked)
        End Sub

        Private Sub chkThumb_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Me.CheckAll("(thumb)", Me.chkThumb.Checked)
        End Sub

        Private Function CleanTMDBURL(ByVal sURL As String) As String
            If Me.IsTMDBURL(sURL) Then
                Dim uri As New Uri(sURL)
                sURL = ("$$[themoviedb.org]" & uri.GetComponents(UriComponents.Path, UriFormat.UriEscaped))
            Else
                sURL = StringUtils.TruncateURL(sURL, 40, True)
            End If
            Return sURL.Replace(":", "$c$").Replace("/", "$s$")
        End Function

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

        Private Sub dlgImgSelect_Disposed(ByVal sender As Object, ByVal e As EventArgs)
            Me.IMPA = Nothing
            Me.MPDB = Nothing
            Me.TMDB = Nothing
            Me.IMPAPosters = Nothing
            Me.MPDBPosters = Nothing
            Me.TMDBPosters = Nothing
        End Sub

        Private Sub dlgImgSelect_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs)
            If (Master.eSettings.AutoET AndAlso Not Master.eSettings.UseImgCache) Then
                Delete.DeleteDirectory(Me.CachePath)
            End If
        End Sub

        Private Sub dlgImgSelect_Load(ByVal sender As Object, ByVal e As EventArgs)
            If Not Me.PreDL Then
                Me.SetUp
            End If
        End Sub

        Private Sub dlgImgSelect_Shown(ByVal sender As Object, ByVal e As EventArgs)
            Try 
                Application.DoEvents
                If Not Me.PreDL Then
                    Me.StartDownload
                ElseIf Me.noImages Then
                    If (Me.DLType = ImageType.Fanart) Then
                        Interaction.MsgBox(Master.eLang.GetString(&H1C, "No Fanart found for this movie.", False), MsgBoxStyle.Information, Master.eLang.GetString(&H1D, "No Fanart Found", False))
                    Else
                        Interaction.MsgBox(Master.eLang.GetString(30, "No Posters found for this movie.", False), MsgBoxStyle.Information, Master.eLang.GetString(&H1F, "No Posters Found", False))
                    End If
                    Me.DialogResult = DialogResult.Cancel
                    Me.Close
                End If
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Private Sub DoSelect(ByVal iIndex As Integer, ByVal poster As Image)
            Try 
                Dim num2 As Integer = Information.UBound(Me.pnlImage, 1)
                Dim i As Integer = 0
                Do While (i <= num2)
                    Me.pnlImage(i).BackColor = Color.White
                    If (Me.DLType = ImageType.Fanart) Then
                        Me.chkImage(i).BackColor = Color.White
                        Me.chkImage(i).ForeColor = Color.Black
                    Else
                        Me.lblImage(i).BackColor = Color.White
                        Me.lblImage(i).ForeColor = Color.Black
                    End If
                    i += 1
                Loop
                Me.pnlImage(iIndex).BackColor = Color.Blue
                If (Me.DLType = ImageType.Fanart) Then
                    Me.chkImage(iIndex).BackColor = Color.Blue
                    Me.chkImage(iIndex).ForeColor = Color.White
                Else
                    Me.lblImage(iIndex).BackColor = Color.Blue
                    Me.lblImage(iIndex).ForeColor = Color.White
                End If
                Me.selIndex = iIndex
                Me.pnlSize.Visible = False
                If Me.IsTMDBURL(poster.URL) Then
                    Me.SetupSizes(poster.ParentID)
                    If ((Not Me.rbLarge.Checked AndAlso Not Me.rbMedium.Checked) AndAlso (Not Me.rbSmall.Checked AndAlso Not Me.rbXLarge.Checked)) Then
                        Me.OK_Button.Enabled = False
                    Else
                        Me.OK_Button.Focus
                    End If
                    Me.tmpImage.Clear
                Else
                    Me.rbXLarge.Checked = False
                    Me.rbLarge.Checked = False
                    Me.rbMedium.Checked = False
                    Me.rbSmall.Checked = False
                    Me.OK_Button.Enabled = True
                    Me.OK_Button.Focus
                    Me.tmpImage.Image = Me.pbImage(iIndex).Image
                End If
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Private Sub GetFanart()
            Try 
                Dim flag As Boolean = True
                If Master.eSettings.UseImgCache Then
                    Dim info As New DirectoryInfo(Me.CachePath)
                    Dim list As New List(Of FileInfo)
                    If Not Directory.Exists(Me.CachePath) Then
                        Directory.CreateDirectory(Me.CachePath)
                    Else
                        Try 
                            list.AddRange(info.GetFiles("*.jpg"))
                        Catch exception1 As Exception
                            ProjectData.SetProjectError(exception1)
                            ProjectData.ClearProjectError
                        End Try
                    End If
                    If (list.Count > 0) Then
                        Me.pnlDLStatus.Visible = True
                        Application.DoEvents
                        flag = False
                        Dim info2 As FileInfo
                        For Each info2 In list
                            Dim item As New Image
                            item.WebImage.FromFile(info2.FullName)
                            If Not Information.IsNothing(item.WebImage.Image) Then
                                Dim flag2 As Boolean = True
                                If (flag2 = info2.Name.Contains("(original)")) Then
                                    item.Description = "original"
                                    If ((Master.eSettings.AutoET AndAlso (Master.eSettings.AutoETSize = FanartSize.Lrg)) AndAlso Not Me.ETHashes.Contains(HashFile.HashCalcFile(info2.FullName))) Then
                                        item.isChecked = True
                                    End If
                                ElseIf (flag2 = info2.Name.Contains("(poster)")) Then
                                    item.Description = "poster"
                                    If ((Master.eSettings.AutoET AndAlso (Master.eSettings.AutoETSize = FanartSize.Mid)) AndAlso Not Me.ETHashes.Contains(HashFile.HashCalcFile(info2.FullName))) Then
                                        item.isChecked = True
                                    End If
                                ElseIf (flag2 = info2.Name.Contains("(thumb)")) Then
                                    item.Description = "thumb"
                                    If ((Master.eSettings.AutoET AndAlso (Master.eSettings.AutoETSize = FanartSize.Small)) AndAlso Not Me.ETHashes.Contains(HashFile.HashCalcFile(info2.FullName))) Then
                                        item.isChecked = True
                                    End If
                                End If
                                item.URL = Regex.Match(info2.Name, "\(url=(.*?)\)").Groups.Item(1).ToString
                                Me.TMDBPosters.Add(item)
                            End If
                        Next
                        Me.ProcessPics(Me.TMDBPosters)
                        Me.pnlDLStatus.Visible = False
                        Me.pnlBG.Visible = True
                    End If
                    list = Nothing
                    info = Nothing
                End If
                If (flag AndAlso AdvancedSettings.GetBooleanSetting("UseTMDB", True, "")) Then
                    If (Master.eSettings.AutoET AndAlso Not Directory.Exists(Me.CachePath)) Then
                        Directory.CreateDirectory(Me.CachePath)
                    End If
                    Me.lblDL1.Text = Master.eLang.GetString(&H20, "Retrieving data from TheMovieDB.com...", False)
                    Me.lblDL1Status.Text = String.Empty
                    Me.pbDL1.Maximum = 3
                    Me.pnlDLStatus.Visible = True
                    Me.Refresh
                    Me.TMDB.GetImagesAsync(Me.tMovie.Movie.IMDBID, "backdrop")
                End If
            Catch exception2 As Exception
                ProjectData.SetProjectError(exception2)
                Dim exception As Exception = exception2
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Private Sub GetPosters()
            Try 
                Dim flag As Boolean = True
                If Master.eSettings.UseImgCache Then
                    Dim list As New List(Of FileInfo)
                    Dim info As New DirectoryInfo(Me.CachePath)
                    If Not Directory.Exists(Me.CachePath) Then
                        Directory.CreateDirectory(Me.CachePath)
                    Else
                        Try 
                            list.AddRange(info.GetFiles("*.jpg"))
                        Catch exception1 As Exception
                            ProjectData.SetProjectError(exception1)
                            ProjectData.ClearProjectError
                        End Try
                    End If
                    If (list.Count > 0) Then
                        Me.pnlDLStatus.Height = &H4B
                        Me.pnlDLStatus.Top = &HCF
                        Me.pnlDLStatus.Visible = True
                        Application.DoEvents
                        flag = False
                        Dim info2 As FileInfo
                        For Each info2 In list
                            Dim item As New Image
                            item.WebImage.FromFile(info2.FullName)
                            Dim flag2 As Boolean = True
                            If (flag2 = info2.Name.Contains("(original)")) Then
                                item.Description = "original"
                            ElseIf (flag2 = info2.Name.Contains("(mid)")) Then
                                item.Description = "mid"
                            ElseIf (flag2 = info2.Name.Contains("(cover)")) Then
                                item.Description = "cover"
                            ElseIf (flag2 = info2.Name.Contains("(thumb)")) Then
                                item.Description = "thumb"
                            ElseIf (flag2 = info2.Name.Contains("(poster)")) Then
                                item.Description = "poster"
                            End If
                            item.URL = Regex.Match(info2.Name, "\(url=(.*?)\)").Groups.Item(1).ToString
                            Me.TMDBPosters.Add(item)
                        Next
                        Me.ProcessPics(Me.TMDBPosters)
                        Me.pnlDLStatus.Visible = False
                        Me.pnlBG.Visible = True
                    End If
                    list = Nothing
                    info = Nothing
                End If
                If flag Then
                    If AdvancedSettings.GetBooleanSetting("UseTMDB", True, "") Then
                        Me.lblDL1.Text = Master.eLang.GetString(&H20, "Retrieving data from TheMovieDB.com...", False)
                        Me.lblDL1Status.Text = String.Empty
                        Me.pbDL1.Maximum = 3
                        Me.pnlDLStatus.Visible = True
                        Me.Refresh
                        Me._tmdbDone = False
                        Me.TMDB.GetImagesAsync(Me.tMovie.Movie.IMDBID, "poster")
                    Else
                        Me.lblDL1.Text = Master.eLang.GetString(&H21, "TheMovieDB.com is not enabled", False)
                    End If
                    If AdvancedSettings.GetBooleanSetting("UseIMPA", False, "") Then
                        Me.lblDL2.Text = Master.eLang.GetString(&H22, "Retrieving data from IMPAwards.com...", False)
                        Me.lblDL2Status.Text = String.Empty
                        Me.pbDL2.Maximum = 3
                        Me.pnlDLStatus.Visible = True
                        Me.Refresh
                        Me._impaDone = False
                        Me.IMPA.GetImagesAsync(Me.tMovie.Movie.IMDBID)
                    Else
                        Me.lblDL2.Text = Master.eLang.GetString(&H23, "IMPAwards.com is not enabled", False)
                    End If
                    If AdvancedSettings.GetBooleanSetting("UseMPDB", False, "") Then
                        Me.lblDL3.Text = Master.eLang.GetString(&H24, "Retrieving data from MoviePosterDB.com...", False)
                        Me.lblDL3Status.Text = String.Empty
                        Me.pbDL3.Maximum = 3
                        Me.pnlDLStatus.Visible = True
                        Me.Refresh
                        Me._mpdbDone = False
                        Me.MPDB.GetImagesAsync(Me.tMovie.Movie.IMDBID)
                    Else
                        Me.lblDL3.Text = Master.eLang.GetString(&H25, "MoviePostersDB.com is not enabled", False)
                    End If
                End If
            Catch exception2 As Exception
                ProjectData.SetProjectError(exception2)
                Dim exception As Exception = exception2
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Private Function GetServerURL(ByVal sURL As String) As String
            If (sURL.StartsWith("http://") AndAlso (sURL.IndexOf("/", 7) >= 0)) Then
                Return sURL.Substring(0, sURL.IndexOf("/", 7))
            End If
            Return sURL
        End Function

        Private Sub IMPADoneDownloading()
            Try 
                Me._impaDone = True
                Me.AllDoneDownloading
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Private Sub IMPAPostersDownloaded(ByVal Posters As List(Of Image))
            Try 
                Me.pbDL2.Value = 0
                Me.lblDL2.Text = Master.eLang.GetString(&H26, "Preparing images...", False)
                Me.lblDL2Status.Text = String.Empty
                Me.pbDL2.Maximum = Posters.Count
                Me.IMPAPosters = Posters
                Me.bwIMPADownload.WorkerSupportsCancellation = True
                Me.bwIMPADownload.WorkerReportsProgress = True
                Me.bwIMPADownload.RunWorkerAsync
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Private Sub IMPAProgressUpdated(ByVal iPercent As Integer)
            Me.pbDL2.Value = iPercent
        End Sub

        <DebuggerStepThrough> _
        Private Sub InitializeComponent()
            Dim manager As New ComponentResourceManager(GetType(dlgImgSelect))
            Me.TableLayoutPanel1 = New TableLayoutPanel
            Me.OK_Button = New Button
            Me.Cancel_Button = New Button
            Me.pnlBG = New Panel
            Me.pnlBottomMain = New Panel
            Me.pnlSize = New Panel
            Me.btnPreview = New Button
            Me.rbSmall = New RadioButton
            Me.rbMedium = New RadioButton
            Me.rbLarge = New RadioButton
            Me.rbXLarge = New RadioButton
            Me.pnlFanart = New Panel
            Me.chkThumb = New CheckBox
            Me.chkMid = New CheckBox
            Me.chkOriginal = New CheckBox
            Me.lblInfo = New Label
            Me.pnlDLStatus = New Panel
            Me.pnlMPDB = New Panel
            Me.lblDL3Status = New Label
            Me.lblDL3 = New Label
            Me.pbDL3 = New ProgressBar
            Me.pnlIMPA = New Panel
            Me.lblDL2Status = New Label
            Me.lblDL2 = New Label
            Me.pbDL2 = New ProgressBar
            Me.lblDL1Status = New Label
            Me.lblDL1 = New Label
            Me.pbDL1 = New ProgressBar
            Me.pnlSinglePic = New Panel
            Me.Label2 = New Label
            Me.ProgressBar1 = New ProgressBar
            Me.TableLayoutPanel1.SuspendLayout
            Me.pnlBottomMain.SuspendLayout
            Me.pnlSize.SuspendLayout
            Me.pnlFanart.SuspendLayout
            Me.pnlDLStatus.SuspendLayout
            Me.pnlMPDB.SuspendLayout
            Me.pnlIMPA.SuspendLayout
            Me.pnlSinglePic.SuspendLayout
            Me.SuspendLayout
            Me.TableLayoutPanel1.Anchor = (AnchorStyles.Right Or AnchorStyles.Bottom)
            Me.TableLayoutPanel1.ColumnCount = 2
            Me.TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50!))
            Me.TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50!))
            Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
            Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
            Dim point2 As New Point(&H2AF, 11)
            Me.TableLayoutPanel1.Location = point2
            Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
            Me.TableLayoutPanel1.RowCount = 1
            Me.TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 50!))
            Dim size2 As New Size(&H92, &H1D)
            Me.TableLayoutPanel1.Size = size2
            Me.TableLayoutPanel1.TabIndex = 0
            Me.OK_Button.Anchor = AnchorStyles.None
            Me.OK_Button.Enabled = False
            Me.OK_Button.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, &HEE)
            point2 = New Point(3, 3)
            Me.OK_Button.Location = point2
            Me.OK_Button.Name = "OK_Button"
            size2 = New Size(&H43, &H17)
            Me.OK_Button.Size = size2
            Me.OK_Button.TabIndex = 0
            Me.OK_Button.Text = "OK"
            Me.Cancel_Button.Anchor = AnchorStyles.None
            Me.Cancel_Button.DialogResult = DialogResult.Cancel
            Me.Cancel_Button.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, &HEE)
            point2 = New Point(&H4C, 3)
            Me.Cancel_Button.Location = point2
            Me.Cancel_Button.Name = "Cancel_Button"
            size2 = New Size(&H43, &H17)
            Me.Cancel_Button.Size = size2
            Me.Cancel_Button.TabIndex = 1
            Me.Cancel_Button.Text = "Cancel"
            Me.pnlBG.AutoScroll = True
            Me.pnlBG.Dock = DockStyle.Fill
            point2 = New Point(0, 0)
            Me.pnlBG.Location = point2
            Me.pnlBG.Name = "pnlBG"
            size2 = New Size(&H344, &H1EF)
            Me.pnlBG.Size = size2
            Me.pnlBG.TabIndex = 4
            Me.pnlBG.Visible = False
            Me.pnlBottomMain.Controls.Add(Me.pnlSize)
            Me.pnlBottomMain.Controls.Add(Me.pnlFanart)
            Me.pnlBottomMain.Controls.Add(Me.lblInfo)
            Me.pnlBottomMain.Controls.Add(Me.TableLayoutPanel1)
            Me.pnlBottomMain.Dock = DockStyle.Bottom
            point2 = New Point(0, &H1EF)
            Me.pnlBottomMain.Location = point2
            Me.pnlBottomMain.Name = "pnlBottomMain"
            size2 = New Size(&H344, 50)
            Me.pnlBottomMain.Size = size2
            Me.pnlBottomMain.TabIndex = 5
            Me.pnlSize.BackColor = Color.White
            Me.pnlSize.BorderStyle = BorderStyle.Fixed3D
            Me.pnlSize.Controls.Add(Me.btnPreview)
            Me.pnlSize.Controls.Add(Me.rbSmall)
            Me.pnlSize.Controls.Add(Me.rbMedium)
            Me.pnlSize.Controls.Add(Me.rbLarge)
            Me.pnlSize.Controls.Add(Me.rbXLarge)
            point2 = New Point(8, 8)
            Me.pnlSize.Location = point2
            Me.pnlSize.Name = "pnlSize"
            size2 = New Size(&H2A1, &H22)
            Me.pnlSize.Size = size2
            Me.pnlSize.TabIndex = 4
            Me.pnlSize.Visible = False
            Me.btnPreview.Enabled = False
            Me.btnPreview.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, &HEE)
            Me.btnPreview.Image = DirectCast(manager.GetObject("btnPreview.Image"), Image)
            Me.btnPreview.ImageAlign = ContentAlignment.MiddleLeft
            point2 = New Point(&H251, 5)
            Me.btnPreview.Location = point2
            Me.btnPreview.Name = "btnPreview"
            size2 = New Size(&H4B, &H17)
            Me.btnPreview.Size = size2
            Me.btnPreview.TabIndex = 6
            Me.btnPreview.Text = "Preview"
            Me.btnPreview.TextAlign = ContentAlignment.MiddleRight
            Me.btnPreview.UseVisualStyleBackColor = True
            Me.rbSmall.AutoSize = True
            Me.rbSmall.Enabled = False
            Me.rbSmall.Font = New Font("Segoe UI", 8.25!, FontStyle.Bold, GraphicsUnit.Point, &HEE)
            point2 = New Point(&H1DB, 8)
            Me.rbSmall.Location = point2
            Me.rbSmall.Name = "rbSmall"
            size2 = New Size(&H35, &H11)
            Me.rbSmall.Size = size2
            Me.rbSmall.TabIndex = 5
            Me.rbSmall.TabStop = True
            Me.rbSmall.Text = "Small"
            Me.rbSmall.UseVisualStyleBackColor = True
            Me.rbMedium.AutoSize = True
            Me.rbMedium.Font = New Font("Segoe UI", 8.25!, FontStyle.Bold, GraphicsUnit.Point, &HEE)
            point2 = New Point(&HC9, 8)
            Me.rbMedium.Location = point2
            Me.rbMedium.Name = "rbMedium"
            size2 = New Size(&H45, &H11)
            Me.rbMedium.Size = size2
            Me.rbMedium.TabIndex = 4
            Me.rbMedium.TabStop = True
            Me.rbMedium.Text = "Medium"
            Me.rbMedium.UseVisualStyleBackColor = True
            Me.rbLarge.AutoSize = True
            Me.rbLarge.Enabled = False
            Me.rbLarge.Font = New Font("Segoe UI", 8.25!, FontStyle.Bold, GraphicsUnit.Point, &HEE)
            point2 = New Point(&H157, 8)
            Me.rbLarge.Location = point2
            Me.rbLarge.Name = "rbLarge"
            size2 = New Size(&H37, &H11)
            Me.rbLarge.Size = size2
            Me.rbLarge.TabIndex = 3
            Me.rbLarge.TabStop = True
            Me.rbLarge.Text = "Cover"
            Me.rbLarge.UseVisualStyleBackColor = True
            Me.rbXLarge.AutoSize = True
            Me.rbXLarge.Enabled = False
            Me.rbXLarge.Font = New Font("Segoe UI", 8.25!, FontStyle.Bold, GraphicsUnit.Point, &HEE)
            point2 = New Point(&H1A, 8)
            Me.rbXLarge.Location = point2
            Me.rbXLarge.Name = "rbXLarge"
            size2 = New Size(&H42, &H11)
            Me.rbXLarge.Size = size2
            Me.rbXLarge.TabIndex = 2
            Me.rbXLarge.TabStop = True
            Me.rbXLarge.Text = "Original"
            Me.rbXLarge.UseVisualStyleBackColor = True
            Me.pnlFanart.BackColor = Color.White
            Me.pnlFanart.BorderStyle = BorderStyle.FixedSingle
            Me.pnlFanart.Controls.Add(Me.chkThumb)
            Me.pnlFanart.Controls.Add(Me.chkMid)
            Me.pnlFanart.Controls.Add(Me.chkOriginal)
            point2 = New Point(8, 8)
            Me.pnlFanart.Location = point2
            Me.pnlFanart.Name = "pnlFanart"
            size2 = New Size(&H153, &H22)
            Me.pnlFanart.Size = size2
            Me.pnlFanart.TabIndex = 5
            Me.pnlFanart.Visible = False
            Me.chkThumb.AutoSize = True
            point2 = New Point(&HE7, 8)
            Me.chkThumb.Location = point2
            Me.chkThumb.Name = "chkThumb"
            size2 = New Size(&H6F, &H11)
            Me.chkThumb.Size = size2
            Me.chkThumb.TabIndex = 9
            Me.chkThumb.Text = "Check All Thumb"
            Me.chkThumb.UseVisualStyleBackColor = True
            Me.chkMid.AutoSize = True
            point2 = New Point(&H83, 8)
            Me.chkMid.Location = point2
            Me.chkMid.Name = "chkMid"
            size2 = New Size(&H60, &H11)
            Me.chkMid.Size = size2
            Me.chkMid.TabIndex = 7
            Me.chkMid.Text = "Check All Mid"
            Me.chkMid.UseVisualStyleBackColor = True
            Me.chkOriginal.AutoSize = True
            point2 = New Point(7, 8)
            Me.chkOriginal.Location = point2
            Me.chkOriginal.Name = "chkOriginal"
            size2 = New Size(&H76, &H11)
            Me.chkOriginal.Size = size2
            Me.chkOriginal.TabIndex = 8
            Me.chkOriginal.Text = "Check All Original"
            Me.chkOriginal.UseVisualStyleBackColor = True
            point2 = New Point(&H192, 10)
            Me.lblInfo.Location = point2
            Me.lblInfo.Name = "lblInfo"
            size2 = New Size(240, &H1F)
            Me.lblInfo.Size = size2
            Me.lblInfo.TabIndex = 3
            Me.lblInfo.Text = "Selected item will be set as fanart. All checked items will be saved to \extrathumbs."
            Me.lblInfo.TextAlign = ContentAlignment.MiddleCenter
            Me.lblInfo.Visible = False
            Me.pnlDLStatus.BackColor = Color.White
            Me.pnlDLStatus.BorderStyle = BorderStyle.FixedSingle
            Me.pnlDLStatus.Controls.Add(Me.pnlMPDB)
            Me.pnlDLStatus.Controls.Add(Me.pnlIMPA)
            Me.pnlDLStatus.Controls.Add(Me.lblDL1Status)
            Me.pnlDLStatus.Controls.Add(Me.lblDL1)
            Me.pnlDLStatus.Controls.Add(Me.pbDL1)
            point2 = New Point(&H101, &H84)
            Me.pnlDLStatus.Location = point2
            Me.pnlDLStatus.Name = "pnlDLStatus"
            size2 = New Size(&H143, &HDF)
            Me.pnlDLStatus.Size = size2
            Me.pnlDLStatus.TabIndex = 0
            Me.pnlDLStatus.Visible = False
            Me.pnlMPDB.Controls.Add(Me.lblDL3Status)
            Me.pnlMPDB.Controls.Add(Me.lblDL3)
            Me.pnlMPDB.Controls.Add(Me.pbDL3)
            point2 = New Point(0, &H93)
            Me.pnlMPDB.Location = point2
            Me.pnlMPDB.Name = "pnlMPDB"
            size2 = New Size(&H141, &H4B)
            Me.pnlMPDB.Size = size2
            Me.pnlMPDB.TabIndex = 9
            Me.lblDL3Status.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, &HEE)
            point2 = New Point(5, &H22)
            Me.lblDL3Status.Location = point2
            Me.lblDL3Status.Name = "lblDL3Status"
            size2 = New Size(310, 13)
            Me.lblDL3Status.Size = size2
            Me.lblDL3Status.TabIndex = 8
            Me.lblDL3.Font = New Font("Segoe UI", 8.25!, FontStyle.Bold, GraphicsUnit.Point, &HEE)
            point2 = New Point(5, 10)
            Me.lblDL3.Location = point2
            Me.lblDL3.Name = "lblDL3"
            size2 = New Size(310, 13)
            Me.lblDL3.Size = size2
            Me.lblDL3.TabIndex = 7
            Me.lblDL3.Text = "Performing Preliminary Tasks..."
            point2 = New Point(6, &H35)
            Me.pbDL3.Location = point2
            Me.pbDL3.Name = "pbDL3"
            size2 = New Size(&H135, &H13)
            Me.pbDL3.Size = size2
            Me.pbDL3.Style = ProgressBarStyle.Continuous
            Me.pbDL3.TabIndex = 6
            Me.pnlIMPA.Controls.Add(Me.lblDL2Status)
            Me.pnlIMPA.Controls.Add(Me.lblDL2)
            Me.pnlIMPA.Controls.Add(Me.pbDL2)
            point2 = New Point(0, &H49)
            Me.pnlIMPA.Location = point2
            Me.pnlIMPA.Name = "pnlIMPA"
            size2 = New Size(&H141, &H4B)
            Me.pnlIMPA.Size = size2
            Me.pnlIMPA.TabIndex = 6
            Me.lblDL2Status.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, &HEE)
            point2 = New Point(5, &H22)
            Me.lblDL2Status.Location = point2
            Me.lblDL2Status.Name = "lblDL2Status"
            size2 = New Size(310, 13)
            Me.lblDL2Status.Size = size2
            Me.lblDL2Status.TabIndex = 8
            Me.lblDL2.Font = New Font("Segoe UI", 8.25!, FontStyle.Bold, GraphicsUnit.Point, 0)
            point2 = New Point(5, 10)
            Me.lblDL2.Location = point2
            Me.lblDL2.Name = "lblDL2"
            size2 = New Size(310, 13)
            Me.lblDL2.Size = size2
            Me.lblDL2.TabIndex = 7
            Me.lblDL2.Text = "Performing Preliminary Tasks..."
            point2 = New Point(6, &H34)
            Me.pbDL2.Location = point2
            Me.pbDL2.Name = "pbDL2"
            size2 = New Size(&H135, &H13)
            Me.pbDL2.Size = size2
            Me.pbDL2.Style = ProgressBarStyle.Continuous
            Me.pbDL2.TabIndex = 6
            Me.lblDL1Status.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, &HEE)
            point2 = New Point(5, &H1D)
            Me.lblDL1Status.Location = point2
            Me.lblDL1Status.Name = "lblDL1Status"
            size2 = New Size(310, 13)
            Me.lblDL1Status.Size = size2
            Me.lblDL1Status.TabIndex = 2
            Me.lblDL1.Font = New Font("Segoe UI", 8.25!, FontStyle.Bold, GraphicsUnit.Point, &HEE)
            point2 = New Point(5, 6)
            Me.lblDL1.Location = point2
            Me.lblDL1.Name = "lblDL1"
            size2 = New Size(310, 13)
            Me.lblDL1.Size = size2
            Me.lblDL1.TabIndex = 1
            Me.lblDL1.Text = "Performing Preliminary Tasks..."
            point2 = New Point(6, &H31)
            Me.pbDL1.Location = point2
            Me.pbDL1.Name = "pbDL1"
            size2 = New Size(&H135, &H13)
            Me.pbDL1.Size = size2
            Me.pbDL1.Style = ProgressBarStyle.Continuous
            Me.pbDL1.TabIndex = 0
            Me.pnlSinglePic.BackColor = Color.White
            Me.pnlSinglePic.BorderStyle = BorderStyle.FixedSingle
            Me.pnlSinglePic.Controls.Add(Me.Label2)
            Me.pnlSinglePic.Controls.Add(Me.ProgressBar1)
            point2 = New Point(&H102, &HCE)
            Me.pnlSinglePic.Location = point2
            Me.pnlSinglePic.Name = "pnlSinglePic"
            size2 = New Size(&H141, &H4B)
            Me.pnlSinglePic.Size = size2
            Me.pnlSinglePic.TabIndex = 9
            Me.pnlSinglePic.Visible = False
            Me.Label2.Font = New Font("Segoe UI", 8.25!, FontStyle.Bold, GraphicsUnit.Point, &HEE)
            point2 = New Point(5, 10)
            Me.Label2.Location = point2
            Me.Label2.Name = "Label2"
            size2 = New Size(310, 13)
            Me.Label2.Size = size2
            Me.Label2.TabIndex = 7
            Me.Label2.Text = "Downloading Selected Image..."
            point2 = New Point(6, &H35)
            Me.ProgressBar1.Location = point2
            Me.ProgressBar1.MarqueeAnimationSpeed = &H19
            Me.ProgressBar1.Name = "ProgressBar1"
            size2 = New Size(&H135, &H13)
            Me.ProgressBar1.Size = size2
            Me.ProgressBar1.Style = ProgressBarStyle.Marquee
            Me.ProgressBar1.TabIndex = 6
            Me.AcceptButton = Me.OK_Button
            Dim ef2 As New SizeF(96!, 96!)
            Me.AutoScaleDimensions = ef2
            Me.AutoScaleMode = AutoScaleMode.Dpi
            Me.AutoScroll = True
            Me.CancelButton = Me.Cancel_Button
            size2 = New Size(&H344, &H221)
            Me.ClientSize = size2
            Me.ControlBox = False
            Me.Controls.Add(Me.pnlSinglePic)
            Me.Controls.Add(Me.pnlDLStatus)
            Me.Controls.Add(Me.pnlBG)
            Me.Controls.Add(Me.pnlBottomMain)
            Me.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            Me.FormBorderStyle = FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "dlgImgSelect"
            Me.ShowIcon = False
            Me.StartPosition = FormStartPosition.CenterParent
            Me.Text = "Select Poster"
            Me.TableLayoutPanel1.ResumeLayout(False)
            Me.pnlBottomMain.ResumeLayout(False)
            Me.pnlSize.ResumeLayout(False)
            Me.pnlSize.PerformLayout
            Me.pnlFanart.ResumeLayout(False)
            Me.pnlFanart.PerformLayout
            Me.pnlDLStatus.ResumeLayout(False)
            Me.pnlMPDB.ResumeLayout(False)
            Me.pnlIMPA.ResumeLayout(False)
            Me.pnlSinglePic.ResumeLayout(False)
            Me.ResumeLayout(False)
        End Sub

        Private Function IsTMDBURL(ByVal sURL As String) As Boolean
            Return ((sURL.ToLower.Contains("themoviedb.org") OrElse sURL.ToLower.Contains("cf1.imgobject.com")) OrElse sURL.ToLower.Contains("cf2.imgobject.com"))
        End Function

        Private Sub lblImage_Click(ByVal sender As Object, ByVal e As EventArgs)
            Me.DoSelect(Convert.ToInt32(DirectCast(sender, Label).Name), DirectCast(DirectCast(sender, Label).Tag, Image))
        End Sub

        Private Sub MouseWheelEvent(ByVal sender As Object, ByVal e As MouseEventArgs)
            Dim verticalScroll As VScrollProperties
            If (e.Delta < 0) Then
                If ((Me.pnlBG.VerticalScroll.Value + 50) <= Me.pnlBG.VerticalScroll.Maximum) Then
                    verticalScroll = Me.pnlBG.VerticalScroll
                    verticalScroll.Value = (verticalScroll.Value + 50)
                Else
                    Me.pnlBG.VerticalScroll.Value = Me.pnlBG.VerticalScroll.Maximum
                End If
            ElseIf ((Me.pnlBG.VerticalScroll.Value - 50) >= Me.pnlBG.VerticalScroll.Minimum) Then
                verticalScroll = Me.pnlBG.VerticalScroll
                verticalScroll.Value = (verticalScroll.Value - 50)
            Else
                Me.pnlBG.VerticalScroll.Value = Me.pnlBG.VerticalScroll.Minimum
            End If
        End Sub

        Private Sub MPDBDoneDownloading()
            Try 
                Me._mpdbDone = True
                Me.AllDoneDownloading
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Private Sub MPDBPostersDownloaded(ByVal Posters As List(Of Image))
            Try 
                Me.pbDL3.Value = 0
                Me.lblDL3.Text = Master.eLang.GetString(&H26, "Preparing images...", False)
                Me.lblDL3Status.Text = String.Empty
                Me.pbDL3.Maximum = Posters.Count
                Me.MPDBPosters = Posters
                Me.bwMPDBDownload.WorkerSupportsCancellation = True
                Me.bwMPDBDownload.WorkerReportsProgress = True
                Me.bwMPDBDownload.RunWorkerAsync
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Private Sub MPDBProgressUpdated(ByVal iPercent As Integer)
            Me.pbDL3.Value = iPercent
        End Sub

        Private Sub OK_Button_Click(ByVal sender As Object, ByVal e As EventArgs)
            Try 
                Dim sPath As String = String.Empty
                Dim str As String = AdvancedSettings.GetSetting("ManualETSize", "thumb", "")
                If (Me.DLType = ImageType.Fanart) Then
                    sPath = Path.Combine(Master.TempPath, "fanart.jpg")
                Else
                    sPath = Path.Combine(Master.TempPath, "poster.jpg")
                End If
                If Not Information.IsNothing(Me.tmpImage.Image) Then
                    If Me.isEdit Then
                        Me.tmpImage.Save(sPath, 0)
                        Me.Results.ImagePath = sPath
                    ElseIf (Me.DLType = ImageType.Fanart) Then
                        Me.Results.ImagePath = Me.tmpImage.SaveAsFanart(Me.tMovie)
                    Else
                        Me.Results.ImagePath = Me.tmpImage.SaveAsPoster(Me.tMovie)
                    End If
                Else
                    Me.pnlBG.Visible = False
                    Me.pnlSinglePic.Visible = True
                    Me.Refresh
                    Application.DoEvents
                    Dim flag2 As Boolean = True
                    If (flag2 = Me.rbXLarge.Checked) Then
                        If Master.eSettings.UseImgCache Then
                            Me.tmpImage.FromFile(Path.Combine(Me.CachePath, ("poster_(original)_(url=" & RuntimeHelpers.GetObjectValue(Me.rbXLarge.Tag) & ").jpg")))
                        ElseIf ((str = "original") And (Me.DLType = ImageType.Fanart)) Then
                            Me.tmpImage.Image = Me.pbImage(Me.selIndex).Image
                        Else
                            Me.tmpImage.FromWeb(Me.rbXLarge.Tag.ToString)
                        End If
                    ElseIf (flag2 = Me.rbLarge.Checked) Then
                        If Master.eSettings.UseImgCache Then
                            Me.tmpImage.FromFile(Path.Combine(Me.CachePath, ("poster_(mid)_(url=" & RuntimeHelpers.GetObjectValue(Me.rbLarge.Tag) & ").jpg")))
                        ElseIf (((str = "w1280") And (Me.DLType = ImageType.Fanart)) Or (Me.DLType <> ImageType.Fanart)) Then
                            Me.tmpImage.Image = Me.pbImage(Me.selIndex).Image
                        Else
                            Me.tmpImage.FromWeb(Me.rbLarge.Tag.ToString)
                        End If
                    ElseIf (flag2 = Me.rbMedium.Checked) Then
                        If ((str = "poster") And (Me.DLType = ImageType.Fanart)) Then
                            Me.tmpImage.Image = Me.pbImage(Me.selIndex).Image
                        Else
                            Me.tmpImage.FromWeb(Me.rbMedium.Tag.ToString)
                        End If
                    ElseIf (flag2 = Me.rbSmall.Checked) Then
                        If Master.eSettings.UseImgCache Then
                            Me.tmpImage.FromFile(Path.Combine(Me.CachePath, ("poster_(thumb)_(url=" & RuntimeHelpers.GetObjectValue(Me.rbSmall.Tag) & ").jpg")))
                        ElseIf ((str = "thumb") And (Me.DLType = ImageType.Fanart)) Then
                            Me.tmpImage.Image = Me.pbImage(Me.selIndex).Image
                        Else
                            Me.tmpImage.FromWeb(Me.rbSmall.Tag.ToString)
                        End If
                    End If
                    If Not Information.IsNothing(Me.tmpImage.Image) Then
                        If Me.isEdit Then
                            Me.tmpImage.Save(sPath, 0)
                            Me.Results.ImagePath = sPath
                        ElseIf (Me.DLType = ImageType.Fanart) Then
                            Me.Results.ImagePath = Me.tmpImage.SaveAsFanart(Me.tMovie)
                        Else
                            Me.Results.ImagePath = Me.tmpImage.SaveAsPoster(Me.tMovie)
                        End If
                    End If
                    Me.pnlSinglePic.Visible = False
                End If
                If (Me.DLType = ImageType.Fanart) Then
                    Dim num2 As Integer = 1
                    Dim str3 As String = String.Empty
                    Dim flag As Boolean = False
                    Dim num5 As Integer = Information.UBound(Me.chkImage, 1)
                    Dim i As Integer = 0
                    Do While (i <= num5)
                        If Me.chkImage(i).Checked Then
                            flag = True
                            Exit Do
                        End If
                        i += 1
                    Loop
                    If flag Then
                        Dim stream As FileStream
                        Dim str4 As String = AdvancedSettings.GetSetting("ExtraThumbsFolderName", "extrathumbs", "")
                        If Me.isEdit Then
                            str3 = Path.Combine(Master.TempPath, str4)
                        Else
                            If (Master.eSettings.VideoTSParent AndAlso Common.isVideoTS(Me.tMovie.Filename)) Then
                                str3 = Path.Combine(Directory.GetParent(Directory.GetParent(Me.tMovie.Filename).FullName).FullName, str4)
                            ElseIf (Master.eSettings.VideoTSParent AndAlso Common.isBDRip(Me.tMovie.Filename)) Then
                                str3 = Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(Me.tMovie.Filename).FullName).FullName).FullName, str4)
                            Else
                                str3 = Path.Combine(Directory.GetParent(Me.tMovie.Filename).FullName, str4)
                            End If
                            num2 = (Functions.GetExtraModifier(str3) + 1)
                        End If
                        If Not Directory.Exists(str3) Then
                            Directory.CreateDirectory(str3)
                        End If
                        Dim num6 As Integer = Information.UBound(Me.chkImage, 1)
                        Dim j As Integer = 0
                        Do While (j <= num6)
                            If Me.chkImage(j).Checked Then
                                stream = New FileStream(Path.Combine(str3, ("thumb" & num2 & ".jpg")), FileMode.Create, FileAccess.ReadWrite)
                                Me.pbImage(j).Image.Save(stream, ImageFormat.Jpeg)
                                stream.Close
                                num2 += 1
                            End If
                            j += 1
                        Loop
                        stream = Nothing
                    End If
                End If
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
            Me.DialogResult = DialogResult.OK
            Me.Close
        End Sub

        Private Sub pbImage_Click(ByVal sender As Object, ByVal e As EventArgs)
            Me.DoSelect(Convert.ToInt32(DirectCast(sender, PictureBox).Name), DirectCast(DirectCast(sender, PictureBox).Tag, Image))
        End Sub

        Private Sub pbImage_DoubleClick(ByVal sender As Object, ByVal e As EventArgs)
            Try 
                Me.PreviewImage
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Private Sub pnlImage_Click(ByVal sender As Object, ByVal e As EventArgs)
            Me.DoSelect(Convert.ToInt32(DirectCast(sender, Panel).Name), DirectCast(DirectCast(sender, Panel).Tag, Image))
        End Sub

        Public Sub PreLoad(ByVal mMovie As DBMovie, ByVal _DLType As ImageType, ByVal Optional _isEdit As Boolean = False)
            Me.tMovie = mMovie
            Me.DLType = _DLType
            Me.isEdit = _isEdit
            Me.PreDL = True
            Me.SetUp
            Me.StartDownload
        End Sub

        Private Sub PreviewImage()
            Try 
                Dim images As New Images
                Me.pnlSinglePic.Visible = True
                Application.DoEvents
                Dim flag As Boolean = True
                If (flag = Me.rbXLarge.Checked) Then
                    If Master.eSettings.UseImgCache Then
                        images.FromFile(Path.Combine(Me.CachePath, ("poster_(original)_(url=" & RuntimeHelpers.GetObjectValue(Me.rbXLarge.Tag) & ").jpg")))
                    Else
                        images.FromWeb(Me.rbXLarge.Tag.ToString)
                    End If
                ElseIf (flag = Me.rbLarge.Checked) Then
                    If Master.eSettings.UseImgCache Then
                        images.FromFile(Path.Combine(Me.CachePath, ("poster_(mid)_(url=" & RuntimeHelpers.GetObjectValue(Me.rbLarge.Tag) & ").jpg")))
                    Else
                        images.FromWeb(Me.rbLarge.Tag.ToString)
                    End If
                ElseIf (flag = Me.rbMedium.Checked) Then
                    If Master.eSettings.UseImgCache Then
                        images.FromFile(Path.Combine(Me.CachePath, ("poster_(cover)_(url=" & RuntimeHelpers.GetObjectValue(Me.rbMedium.Tag) & ").jpg")))
                    Else
                        images.FromWeb(Me.rbMedium.Tag.ToString)
                    End If
                ElseIf (flag = Me.rbSmall.Checked) Then
                    If Master.eSettings.UseImgCache Then
                        images.FromFile(Path.Combine(Me.CachePath, ("poster_(thumb)_(url=" & RuntimeHelpers.GetObjectValue(Me.rbSmall.Tag) & ").jpg")))
                    Else
                        images.FromWeb(Me.rbSmall.Tag.ToString)
                    End If
                End If
                Me.pnlSinglePic.Visible = False
                If Not Information.IsNothing(images.Image) Then
                    Dim images2 As Images = images
                    Dim image As Image = images2.Image
                    ModulesManager.Instance.RuntimeObjects.InvokeOpenImageViewer(image)
                    images2.Image = image
                End If
                images.Dispose
                images = Nothing
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Me.pnlSinglePic.Visible = False
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Private Sub ProcessPics(ByVal posters As List(Of Image))
            Try 
                Dim num3 As Integer
                Dim iIndex As Integer = 0
                If Not Master.eSettings.UseImgCache Then
                    goto Label_0051
                End If
                Dim index As Integer = (posters.Count - 1)
                goto Label_0049
            Label_001F:
                If Information.IsNothing(posters.Item(index).WebImage.Image) Then
                    posters.RemoveAt(index)
                End If
                index = (index + -1)
            Label_0049:
                num3 = 0
                If (index >= num3) Then
                    goto Label_001F
                End If
            Label_0051:
                If (posters.Count > 0) Then
                    Dim image As Image
                    For Each image In posters.OrderBy(Of Image, String)(New Func(Of Image, String)(AddressOf Me._Lambda$__92))
                        If (Not Information.IsNothing(image.WebImage.Image) AndAlso (((Me.DLType = ImageType.Fanart) OrElse Not Me.IsTMDBURL(image.URL)) OrElse (image.Description = "cover"))) Then
                            Me.AddImage(image.WebImage.Image, image.Description, iIndex, image.URL, image.isChecked, image)
                            iIndex += 1
                        End If
                    Next
                ElseIf (Not Me.PreDL OrElse Me.isShown) Then
                    If (Me.DLType = ImageType.Fanart) Then
                        Interaction.MsgBox(Master.eLang.GetString(&H1C, "No Fanart found for this movie.", False), MsgBoxStyle.Information, Master.eLang.GetString(&H1D, "No Fanart Found", False))
                    Else
                        Interaction.MsgBox(Master.eLang.GetString(30, "No Posters found for this movie.", False), MsgBoxStyle.Information, Master.eLang.GetString(&H1F, "No Posters Found", False))
                    End If
                    Me.DialogResult = DialogResult.Cancel
                    Me.Close
                Else
                    Me.noImages = True
                End If
                Me.Activate
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Private Sub rbLarge_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Me.OK_Button.Enabled = True
            Me.btnPreview.Enabled = True
        End Sub

        Private Sub rbMedium_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Me.OK_Button.Enabled = True
            Me.btnPreview.Enabled = True
        End Sub

        Private Sub rbSmall_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Me.OK_Button.Enabled = True
            Me.btnPreview.Enabled = True
        End Sub

        Private Sub rbXLarge_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Me.OK_Button.Enabled = True
            Me.btnPreview.Enabled = True
        End Sub

        Private Function RemoveServerURL(ByVal sURL As String) As String
            If (sURL.StartsWith("http://") AndAlso (sURL.IndexOf("/", 7) >= 0)) Then
                Return sURL.Substring(sURL.IndexOf("/", 7))
            End If
            Return sURL
        End Function

        Private Sub SetUp()
            Try 
                Me.IMPA.IMDBURL = Me.IMDBURL
                Me.TMDB.IMDBURL = Me.IMDBURL
                Me.MPDB.IMDBURL = Me.IMDBURL
                AddHandler Me.TMDB.PostersDownloaded, New PostersDownloadedEventHandler(AddressOf Me.TMDBPostersDownloaded)
                AddHandler Me.TMDB.ProgressUpdated, New ProgressUpdatedEventHandler(AddressOf Me.TMDBProgressUpdated)
                AddHandler Me.IMPA.PostersDownloaded, New PostersDownloadedEventHandler(AddressOf Me.IMPAPostersDownloaded)
                AddHandler Me.IMPA.ProgressUpdated, New ProgressUpdatedEventHandler(AddressOf Me.IMPAProgressUpdated)
                AddHandler Me.MPDB.PostersDownloaded, New PostersDownloadedEventHandler(AddressOf Me.MPDBPostersDownloaded)
                AddHandler Me.MPDB.ProgressUpdated, New ProgressUpdatedEventHandler(AddressOf Me.MPDBProgressUpdated)
                AddHandler IMPADone, New IMPADoneEventHandler(AddressOf Me.IMPADoneDownloading)
                AddHandler TMDBDone, New TMDBDoneEventHandler(AddressOf Me.TMDBDoneDownloading)
                AddHandler MPDBDone, New MPDBDoneEventHandler(AddressOf Me.MPDBDoneDownloading)
                AddHandler MyBase.MouseWheel, New MouseEventHandler(AddressOf Me.MouseWheelEvent)
                AddHandler Me.pnlBG.MouseWheel, New MouseEventHandler(AddressOf Me.MouseWheelEvent)
                Dim pnlBG As Panel = Me.pnlBG
                Functions.PNLDoubleBuffer(pnlBG)
                Me.pnlBG = pnlBG
                If (Me.DLType = ImageType.Posters) Then
                    Me.Text = (Master.eLang.GetString(&H27, "Select Poster - ", False) & If(Not String.IsNullOrEmpty(Me.tMovie.Movie.Title), Me.tMovie.Movie.Title, Me.tMovie.ListTitle))
                Else
                    Me.Text = (Master.eLang.GetString(40, "Select Fanart - ", False) & If(Not String.IsNullOrEmpty(Me.tMovie.Movie.Title), Me.tMovie.Movie.Title, Me.tMovie.ListTitle))
                    Me.pnlDLStatus.Height = &H4B
                    Me.pnlDLStatus.Top = &HCF
                    If Master.eSettings.AutoET Then
                        Me.ETHashes = HashFile.CurrentETHashes(Me.tMovie.Filename)
                    End If
                End If
                Me.CachePath = String.Concat(New String() { Master.TempPath, Conversions.ToString(Path.DirectorySeparatorChar), Me.tMovie.Movie.IMDBID, Conversions.ToString(Path.DirectorySeparatorChar), If((Me.DLType = ImageType.Posters), "posters", "fanart") })
                Me.OK_Button.Text = Master.eLang.GetString(&HB3, "OK", True)
                Me.Cancel_Button.Text = Master.eLang.GetString(&HA7, "Cancel", True)
                Me.btnPreview.Text = Master.eLang.GetString(180, "Preview", True)
                Me.chkThumb.Text = Master.eLang.GetString(&H29, "Check All Thumb", False)
                Me.chkMid.Text = Master.eLang.GetString(&H2A, "Check All Mid", False)
                Me.chkOriginal.Text = Master.eLang.GetString(&H2B, "Check All Original", False)
                Me.lblInfo.Text = Master.eLang.GetString(&H2C, "Selected item will be set as fanart. All checked items will be saved to \extrathumbs.", False)
                Me.lblDL3.Text = Master.eLang.GetString(&H2D, "Performing Preliminary Tasks...", False)
                Me.lblDL2.Text = Me.lblDL3.Text
                Me.lblDL1.Text = Me.lblDL3.Text
                Me.Label2.Text = Master.eLang.GetString(&H2E, "Downloading Selected Image...", False)
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Private Sub SetupSizes(ByVal ParentID As String)
            Dim e$__ As New _Closure$__12 With { _
                .$VB$Local_ParentID = ParentID _
            }
            Try 
                Me.rbXLarge.Checked = False
                Me.rbXLarge.Enabled = False
                Me.rbXLarge.Text = Master.eLang.GetString(&H2F, "Original", False)
                Me.rbLarge.Checked = False
                Me.rbLarge.Enabled = False
                Me.rbMedium.Checked = False
                Me.rbSmall.Checked = False
                Me.rbSmall.Enabled = False
                If (Me.DLType = ImageType.Fanart) Then
                    Me.rbLarge.Text = "w1280"
                    Me.rbMedium.Text = "poster"
                    Me.rbSmall.Text = "thumb"
                Else
                    Me.rbLarge.Text = Master.eLang.GetString(&H30, "Cover", False)
                    Me.rbMedium.Text = Master.eLang.GetString(&H31, "Medium", False)
                    Me.rbSmall.Text = Master.eLang.GetString(50, "Small", False)
                End If
                Dim image As Image
                For Each image In Me.TMDBPosters.Where(Of Image)(New Func(Of Image, Boolean)(AddressOf e$__._Lambda$__93))
                    Select Case image.Description
                        Case "original"
                            If (Not Master.eSettings.UseImgCache OrElse Not Information.IsNothing(image.WebImage.Image)) Then
                                Me.rbXLarge.Enabled = True
                                Me.rbXLarge.Tag = image.URL
                                Me.rbXLarge.Text = String.Format(Master.eLang.GetString(&H33, "Original ({0}x{1})", False), image.Width, image.Height)
                            End If
                            Exit Select
                        Case "cover"
                            If (Not Master.eSettings.UseImgCache OrElse Not Information.IsNothing(image.WebImage.Image)) Then
                                Me.rbLarge.Enabled = True
                                Me.rbLarge.Tag = image.URL
                                Me.rbLarge.Text = String.Format(Master.eLang.GetString(&H34, "Cover ({0}x{1})", False), image.Width, image.Height)
                            End If
                            Exit Select
                        Case "w1280"
                            If (Not Master.eSettings.UseImgCache OrElse Not Information.IsNothing(image.WebImage.Image)) Then
                                Me.rbLarge.Enabled = True
                                Me.rbLarge.Tag = image.URL
                                Me.rbLarge.Text = String.Format("w1280 ({0}x{1})", image.Width, image.Height)
                            End If
                            Exit Select
                        Case "thumb"
                            If (Not Master.eSettings.UseImgCache OrElse Not Information.IsNothing(image.WebImage.Image)) Then
                                Me.rbSmall.Enabled = True
                                Me.rbSmall.Tag = image.URL
                                Me.rbSmall.Text = String.Format(Master.eLang.GetString(&H35, "Small ({0}x{1})", False), image.Width, image.Height)
                            End If
                            Exit Select
                        Case "mid"
                            Me.rbMedium.Text = String.Format(Master.eLang.GetString(&H36, "Medium ({0}x{1})", False), image.Width, image.Height)
                            Me.rbMedium.Tag = image.URL
                            Exit Select
                        Case "poster"
                            Me.rbMedium.Text = String.Format("Poster ({0}x{1})", image.Width, image.Height)
                            Me.rbMedium.Tag = image.URL
                            Exit Select
                    End Select
                Next
                Select Case Master.eSettings.PreferredPosterSize
                    Case PosterSize.Xlrg
                        Me.rbXLarge.Checked = Me.rbXLarge.Enabled
                        Exit Select
                    Case PosterSize.Lrg
                        Me.rbLarge.Checked = Me.rbLarge.Enabled
                        Exit Select
                    Case PosterSize.Mid
                        Me.rbMedium.Checked = Me.rbMedium.Enabled
                        Exit Select
                    Case PosterSize.Small
                        Me.rbSmall.Checked = Me.rbSmall.Enabled
                        Exit Select
                End Select
                Me.pnlSize.Visible = True
                Me.Invalidate
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Public Function ShowDialog() As ImgResult
            Me.isShown = True
            MyBase.ShowDialog
            Return Me.Results
        End Function

        Public Function ShowDialog(ByVal mMovie As DBMovie, ByVal _DLType As ImageType, ByVal Optional _isEdit As Boolean = False) As ImgResult
            Me.tMovie = mMovie
            Me.DLType = _DLType
            Me.isEdit = _isEdit
            Me.isShown = True
            MyBase.ShowDialog
            Return Me.Results
        End Function

        Private Sub StartDownload()
            Try 
                Select Case Me.DLType
                    Case ImageType.Posters
                        Me.GetPosters
                        Return
                    Case ImageType.Fanart
                        Me.GetFanart
                        Return
                End Select
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Private Sub TMDBDoneDownloading()
            Try 
                If (Me.DLType = ImageType.Posters) Then
                    Me._tmdbDone = True
                    Me.AllDoneDownloading
                Else
                    Me.pnlDLStatus.Visible = False
                    Me.ProcessPics(Me.TMDBPosters)
                    Me.pnlBG.Visible = True
                End If
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Private Sub TMDBPostersDownloaded(ByVal Posters As List(Of Image))
            Try 
                Me.pbDL1.Value = 0
                Me.lblDL1.Text = Master.eLang.GetString(&H26, "Preparing images...", False)
                Me.lblDL1Status.Text = String.Empty
                Me.TMDBPosters = Posters
                Me.pbDL1.Maximum = 100
                Me.bwTMDBDownload.WorkerSupportsCancellation = True
                Me.bwTMDBDownload.WorkerReportsProgress = True
                Me.bwTMDBDownload.RunWorkerAsync
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Private Sub TMDBProgressUpdated(ByVal iPercent As Integer)
            Me.pbDL1.Value = iPercent
        End Sub


        ' Properties
        Friend Overridable Property btnPreview As Button
            <DebuggerNonUserCode> _
            Get
                Return Me._btnPreview
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Button)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.btnPreview_Click)
                If (Not Me._btnPreview Is Nothing) Then
                    RemoveHandler Me._btnPreview.Click, handler
                End If
                Me._btnPreview = WithEventsValue
                If (Not Me._btnPreview Is Nothing) Then
                    AddHandler Me._btnPreview.Click, handler
                End If
            End Set
        End Property

        Friend Overridable Property bwIMPADownload As BackgroundWorker
            <DebuggerNonUserCode> _
            Get
                Return Me._bwIMPADownload
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As BackgroundWorker)
                Dim handler As RunWorkerCompletedEventHandler = New RunWorkerCompletedEventHandler(AddressOf Me.bwIMPADownload_RunWorkerCompleted)
                Dim handler2 As ProgressChangedEventHandler = New ProgressChangedEventHandler(AddressOf Me.bwIMPADownload_ProgressChanged)
                Dim handler3 As DoWorkEventHandler = New DoWorkEventHandler(AddressOf Me.bwIMPADownload_DoWork)
                If (Not Me._bwIMPADownload Is Nothing) Then
                    RemoveHandler Me._bwIMPADownload.RunWorkerCompleted, handler
                    RemoveHandler Me._bwIMPADownload.ProgressChanged, handler2
                    RemoveHandler Me._bwIMPADownload.DoWork, handler3
                End If
                Me._bwIMPADownload = WithEventsValue
                If (Not Me._bwIMPADownload Is Nothing) Then
                    AddHandler Me._bwIMPADownload.RunWorkerCompleted, handler
                    AddHandler Me._bwIMPADownload.ProgressChanged, handler2
                    AddHandler Me._bwIMPADownload.DoWork, handler3
                End If
            End Set
        End Property

        Friend Overridable Property bwMPDBDownload As BackgroundWorker
            <DebuggerNonUserCode> _
            Get
                Return Me._bwMPDBDownload
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As BackgroundWorker)
                Dim handler As RunWorkerCompletedEventHandler = New RunWorkerCompletedEventHandler(AddressOf Me.bwMPDBDownload_RunWorkerCompleted)
                Dim handler2 As ProgressChangedEventHandler = New ProgressChangedEventHandler(AddressOf Me.bwMPDBDownload_ProgressChanged)
                Dim handler3 As DoWorkEventHandler = New DoWorkEventHandler(AddressOf Me.bwMPDBDownload_DoWork)
                If (Not Me._bwMPDBDownload Is Nothing) Then
                    RemoveHandler Me._bwMPDBDownload.RunWorkerCompleted, handler
                    RemoveHandler Me._bwMPDBDownload.ProgressChanged, handler2
                    RemoveHandler Me._bwMPDBDownload.DoWork, handler3
                End If
                Me._bwMPDBDownload = WithEventsValue
                If (Not Me._bwMPDBDownload Is Nothing) Then
                    AddHandler Me._bwMPDBDownload.RunWorkerCompleted, handler
                    AddHandler Me._bwMPDBDownload.ProgressChanged, handler2
                    AddHandler Me._bwMPDBDownload.DoWork, handler3
                End If
            End Set
        End Property

        Friend Overridable Property bwTMDBDownload As BackgroundWorker
            <DebuggerNonUserCode> _
            Get
                Return Me._bwTMDBDownload
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As BackgroundWorker)
                Dim handler As DoWorkEventHandler = New DoWorkEventHandler(AddressOf Me.bwTMDBDownload_DoWork)
                Dim handler2 As RunWorkerCompletedEventHandler = New RunWorkerCompletedEventHandler(AddressOf Me.bwTMDBDownload_RunWorkerCompleted)
                Dim handler3 As ProgressChangedEventHandler = New ProgressChangedEventHandler(AddressOf Me.bwTMDBDownload_ProgressChanged)
                If (Not Me._bwTMDBDownload Is Nothing) Then
                    RemoveHandler Me._bwTMDBDownload.DoWork, handler
                    RemoveHandler Me._bwTMDBDownload.RunWorkerCompleted, handler2
                    RemoveHandler Me._bwTMDBDownload.ProgressChanged, handler3
                End If
                Me._bwTMDBDownload = WithEventsValue
                If (Not Me._bwTMDBDownload Is Nothing) Then
                    AddHandler Me._bwTMDBDownload.DoWork, handler
                    AddHandler Me._bwTMDBDownload.RunWorkerCompleted, handler2
                    AddHandler Me._bwTMDBDownload.ProgressChanged, handler3
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

        Friend Overridable Property chkMid As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._chkMid
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.chkMid_CheckedChanged)
                If (Not Me._chkMid Is Nothing) Then
                    RemoveHandler Me._chkMid.CheckedChanged, handler
                End If
                Me._chkMid = WithEventsValue
                If (Not Me._chkMid Is Nothing) Then
                    AddHandler Me._chkMid.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property chkOriginal As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._chkOriginal
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.chkOriginal_CheckedChanged)
                If (Not Me._chkOriginal Is Nothing) Then
                    RemoveHandler Me._chkOriginal.CheckedChanged, handler
                End If
                Me._chkOriginal = WithEventsValue
                If (Not Me._chkOriginal Is Nothing) Then
                    AddHandler Me._chkOriginal.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property chkThumb As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._chkThumb
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.chkThumb_CheckedChanged)
                If (Not Me._chkThumb Is Nothing) Then
                    RemoveHandler Me._chkThumb.CheckedChanged, handler
                End If
                Me._chkThumb = WithEventsValue
                If (Not Me._chkThumb Is Nothing) Then
                    AddHandler Me._chkThumb.CheckedChanged, handler
                End If
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

        Friend Overridable Property lblDL1 As Label
            <DebuggerNonUserCode> _
            Get
                Return Me._lblDL1
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Label)
                Me._lblDL1 = WithEventsValue
            End Set
        End Property

        Friend Overridable Property lblDL1Status As Label
            <DebuggerNonUserCode> _
            Get
                Return Me._lblDL1Status
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Label)
                Me._lblDL1Status = WithEventsValue
            End Set
        End Property

        Friend Overridable Property lblDL2 As Label
            <DebuggerNonUserCode> _
            Get
                Return Me._lblDL2
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Label)
                Me._lblDL2 = WithEventsValue
            End Set
        End Property

        Friend Overridable Property lblDL2Status As Label
            <DebuggerNonUserCode> _
            Get
                Return Me._lblDL2Status
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Label)
                Me._lblDL2Status = WithEventsValue
            End Set
        End Property

        Friend Overridable Property lblDL3 As Label
            <DebuggerNonUserCode> _
            Get
                Return Me._lblDL3
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Label)
                Me._lblDL3 = WithEventsValue
            End Set
        End Property

        Friend Overridable Property lblDL3Status As Label
            <DebuggerNonUserCode> _
            Get
                Return Me._lblDL3Status
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Label)
                Me._lblDL3Status = WithEventsValue
            End Set
        End Property

        Friend Overridable Property lblInfo As Label
            <DebuggerNonUserCode> _
            Get
                Return Me._lblInfo
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Label)
                Me._lblInfo = WithEventsValue
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

        Friend Overridable Property pbDL1 As ProgressBar
            <DebuggerNonUserCode> _
            Get
                Return Me._pbDL1
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As ProgressBar)
                Me._pbDL1 = WithEventsValue
            End Set
        End Property

        Friend Overridable Property pbDL2 As ProgressBar
            <DebuggerNonUserCode> _
            Get
                Return Me._pbDL2
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As ProgressBar)
                Me._pbDL2 = WithEventsValue
            End Set
        End Property

        Friend Overridable Property pbDL3 As ProgressBar
            <DebuggerNonUserCode> _
            Get
                Return Me._pbDL3
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As ProgressBar)
                Me._pbDL3 = WithEventsValue
            End Set
        End Property

        Friend Overridable Property pnlBG As Panel
            <DebuggerNonUserCode> _
            Get
                Return Me._pnlBG
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Panel)
                Me._pnlBG = WithEventsValue
            End Set
        End Property

        Friend Overridable Property pnlBottomMain As Panel
            <DebuggerNonUserCode> _
            Get
                Return Me._pnlBottomMain
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Panel)
                Me._pnlBottomMain = WithEventsValue
            End Set
        End Property

        Friend Overridable Property pnlDLStatus As Panel
            <DebuggerNonUserCode> _
            Get
                Return Me._pnlDLStatus
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Panel)
                Me._pnlDLStatus = WithEventsValue
            End Set
        End Property

        Friend Overridable Property pnlFanart As Panel
            <DebuggerNonUserCode> _
            Get
                Return Me._pnlFanart
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Panel)
                Me._pnlFanart = WithEventsValue
            End Set
        End Property

        Friend Overridable Property pnlIMPA As Panel
            <DebuggerNonUserCode> _
            Get
                Return Me._pnlIMPA
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Panel)
                Me._pnlIMPA = WithEventsValue
            End Set
        End Property

        Friend Overridable Property pnlMPDB As Panel
            <DebuggerNonUserCode> _
            Get
                Return Me._pnlMPDB
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Panel)
                Me._pnlMPDB = WithEventsValue
            End Set
        End Property

        Friend Overridable Property pnlSinglePic As Panel
            <DebuggerNonUserCode> _
            Get
                Return Me._pnlSinglePic
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Panel)
                Me._pnlSinglePic = WithEventsValue
            End Set
        End Property

        Friend Overridable Property pnlSize As Panel
            <DebuggerNonUserCode> _
            Get
                Return Me._pnlSize
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Panel)
                Me._pnlSize = WithEventsValue
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

        Friend Overridable Property rbLarge As RadioButton
            <DebuggerNonUserCode> _
            Get
                Return Me._rbLarge
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As RadioButton)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.rbLarge_CheckedChanged)
                If (Not Me._rbLarge Is Nothing) Then
                    RemoveHandler Me._rbLarge.CheckedChanged, handler
                End If
                Me._rbLarge = WithEventsValue
                If (Not Me._rbLarge Is Nothing) Then
                    AddHandler Me._rbLarge.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property rbMedium As RadioButton
            <DebuggerNonUserCode> _
            Get
                Return Me._rbMedium
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As RadioButton)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.rbMedium_CheckedChanged)
                If (Not Me._rbMedium Is Nothing) Then
                    RemoveHandler Me._rbMedium.CheckedChanged, handler
                End If
                Me._rbMedium = WithEventsValue
                If (Not Me._rbMedium Is Nothing) Then
                    AddHandler Me._rbMedium.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property rbSmall As RadioButton
            <DebuggerNonUserCode> _
            Get
                Return Me._rbSmall
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As RadioButton)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.rbSmall_CheckedChanged)
                If (Not Me._rbSmall Is Nothing) Then
                    RemoveHandler Me._rbSmall.CheckedChanged, handler
                End If
                Me._rbSmall = WithEventsValue
                If (Not Me._rbSmall Is Nothing) Then
                    AddHandler Me._rbSmall.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property rbXLarge As RadioButton
            <DebuggerNonUserCode> _
            Get
                Return Me._rbXLarge
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As RadioButton)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.rbXLarge_CheckedChanged)
                If (Not Me._rbXLarge Is Nothing) Then
                    RemoveHandler Me._rbXLarge.CheckedChanged, handler
                End If
                Me._rbXLarge = WithEventsValue
                If (Not Me._rbXLarge Is Nothing) Then
                    AddHandler Me._rbXLarge.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property TableLayoutPanel1 As TableLayoutPanel
            <DebuggerNonUserCode> _
            Get
                Return Me._TableLayoutPanel1
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As TableLayoutPanel)
                Me._TableLayoutPanel1 = WithEventsValue
            End Set
        End Property


        ' Fields
        Private Shared __ENCList As List(Of WeakReference) = New List(Of WeakReference)
        <AccessedThroughProperty("btnPreview")> _
        Private _btnPreview As Button
        <AccessedThroughProperty("bwIMPADownload")> _
        Private _bwIMPADownload As BackgroundWorker
        <AccessedThroughProperty("bwMPDBDownload")> _
        Private _bwMPDBDownload As BackgroundWorker
        <AccessedThroughProperty("bwTMDBDownload")> _
        Private _bwTMDBDownload As BackgroundWorker
        <AccessedThroughProperty("Cancel_Button")> _
        Private _Cancel_Button As Button
        <AccessedThroughProperty("chkMid")> _
        Private _chkMid As CheckBox
        <AccessedThroughProperty("chkOriginal")> _
        Private _chkOriginal As CheckBox
        <AccessedThroughProperty("chkThumb")> _
        Private _chkThumb As CheckBox
        Private _impaDone As Boolean
        <AccessedThroughProperty("Label2")> _
        Private _Label2 As Label
        <AccessedThroughProperty("lblDL1")> _
        Private _lblDL1 As Label
        <AccessedThroughProperty("lblDL1Status")> _
        Private _lblDL1Status As Label
        <AccessedThroughProperty("lblDL2")> _
        Private _lblDL2 As Label
        <AccessedThroughProperty("lblDL2Status")> _
        Private _lblDL2Status As Label
        <AccessedThroughProperty("lblDL3")> _
        Private _lblDL3 As Label
        <AccessedThroughProperty("lblDL3Status")> _
        Private _lblDL3Status As Label
        <AccessedThroughProperty("lblInfo")> _
        Private _lblInfo As Label
        Private _mpdbDone As Boolean
        <AccessedThroughProperty("OK_Button")> _
        Private _OK_Button As Button
        <AccessedThroughProperty("pbDL1")> _
        Private _pbDL1 As ProgressBar
        <AccessedThroughProperty("pbDL2")> _
        Private _pbDL2 As ProgressBar
        <AccessedThroughProperty("pbDL3")> _
        Private _pbDL3 As ProgressBar
        <AccessedThroughProperty("pnlBG")> _
        Private _pnlBG As Panel
        <AccessedThroughProperty("pnlBottomMain")> _
        Private _pnlBottomMain As Panel
        <AccessedThroughProperty("pnlDLStatus")> _
        Private _pnlDLStatus As Panel
        <AccessedThroughProperty("pnlFanart")> _
        Private _pnlFanart As Panel
        <AccessedThroughProperty("pnlIMPA")> _
        Private _pnlIMPA As Panel
        <AccessedThroughProperty("pnlMPDB")> _
        Private _pnlMPDB As Panel
        <AccessedThroughProperty("pnlSinglePic")> _
        Private _pnlSinglePic As Panel
        <AccessedThroughProperty("pnlSize")> _
        Private _pnlSize As Panel
        <AccessedThroughProperty("ProgressBar1")> _
        Private _ProgressBar1 As ProgressBar
        <AccessedThroughProperty("rbLarge")> _
        Private _rbLarge As RadioButton
        <AccessedThroughProperty("rbMedium")> _
        Private _rbMedium As RadioButton
        <AccessedThroughProperty("rbSmall")> _
        Private _rbSmall As RadioButton
        <AccessedThroughProperty("rbXLarge")> _
        Private _rbXLarge As RadioButton
        <AccessedThroughProperty("TableLayoutPanel1")> _
        Private _TableLayoutPanel1 As TableLayoutPanel
        Private _tmdbDone As Boolean
        Private CachePath As String
        Private chkImage As CheckBox()
        Private components As IContainer
        Private DLType As ImageType
        Private ETHashes As List(Of String)
        Private iCounter As Integer
        Private iLeft As Integer
        Public IMDBURL As String
        Private IMPA As Scraper
        Private IMPAPosters As List(Of Image)
        Private isEdit As Boolean
        Private isShown As Boolean
        Private iTop As Integer
        Private lblImage As Label()
        Private MPDB As Scraper
        Private MPDBPosters As List(Of Image)
        Private noImages As Boolean
        Private pbImage As PictureBox()
        Private pnlImage As Panel()
        Private PreDL As Boolean
        Private Results As ImgResult
        Private selIndex As Integer
        Private TMDB As Scraper
        Private TMDBPosters As List(Of Image)
        Private tMovie As DBMovie
        Private tmpImage As Images

        ' Nested Types
        <CompilerGenerated> _
        Friend Class _Closure$__11
            ' Methods
            <DebuggerNonUserCode> _
            Public Sub New()
            End Sub

            <DebuggerNonUserCode> _
            Public Sub New(ByVal other As _Closure$__11)
                If (Not other Is Nothing) Then
                    Me.$VB$Local_extrathumbSize = other.$VB$Local_extrathumbSize
                End If
            End Sub

            <CompilerGenerated> _
            Public Function _Lambda$__90(ByVal s As Image) As Boolean
                Return (s.Description = Me.$VB$Local_extrathumbSize)
            End Function


            ' Fields
            Public $VB$Local_extrathumbSize As String
        End Class

        <CompilerGenerated> _
        Friend Class _Closure$__12
            ' Methods
            <DebuggerNonUserCode> _
            Public Sub New()
            End Sub

            <DebuggerNonUserCode> _
            Public Sub New(ByVal other As _Closure$__12)
                If (Not other Is Nothing) Then
                    Me.$VB$Local_ParentID = other.$VB$Local_ParentID
                End If
            End Sub

            <CompilerGenerated> _
            Public Function _Lambda$__93(ByVal f As Image) As Boolean
                Return (f.ParentID = Me.$VB$Local_ParentID)
            End Function


            ' Fields
            Public $VB$Local_ParentID As String
        End Class

        Private Delegate Sub IMPADoneEventHandler()

        Private Delegate Sub MPDBDoneEventHandler()

        Private Delegate Sub TMDBDoneEventHandler()
    End Class
End Namespace

