Imports EmberAPI
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Windows.Forms

Namespace EmberScraperModule
    <DesignerGenerated> _
    Public Class dlgTVImageSelect
        Inherits Form
        ' Methods
        Public Sub New()
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.dlgTVImageSelect_Load)
            AddHandler MyBase.Shown, New EventHandler(AddressOf Me.dlgTVImageSelect_Shown)
            dlgTVImageSelect.__ENCAddToList(Me)
            Me.bwDownloadFanart = New BackgroundWorker
            Me.bwLoadData = New BackgroundWorker
            Me.bwLoadImages = New BackgroundWorker
            Me.DefaultImages = New TVImages
            Me.FanartList = New List(Of TVDBFanart)
            Me.GenericPosterList = New List(Of TVDBPoster)
            Me.iCounter = 0
            Me.iLeft = 5
            Me.iTop = 5
            Me.SeasonList = New List(Of TVDBSeasonPoster)
            Me.SelIsPoster = True
            Me.SelSeason = -999
            Me.ShowPosterList = New List(Of TVDBShowPoster)
            Me._fanartchanged = False
            Me._id = -1
            Me._season = -999
            Me._type = TVImageType.All
            Me._withcurrent = True
            Me.InitializeComponent
        End Sub

        <DebuggerNonUserCode> _
        Private Shared Sub __ENCAddToList(ByVal value As Object)
            Dim list As List(Of WeakReference) = dlgTVImageSelect.__ENCList
            SyncLock list
                If (dlgTVImageSelect.__ENCList.Count = dlgTVImageSelect.__ENCList.Capacity) Then
                    Dim index As Integer = 0
                    Dim num3 As Integer = (dlgTVImageSelect.__ENCList.Count - 1)
                    Dim i As Integer = 0
                    Do While (i <= num3)
                        Dim reference As WeakReference = dlgTVImageSelect.__ENCList.Item(i)
                        If reference.IsAlive Then
                            If (i <> index) Then
                                dlgTVImageSelect.__ENCList.Item(index) = dlgTVImageSelect.__ENCList.Item(i)
                            End If
                            index += 1
                        End If
                        i += 1
                    Loop
                    dlgTVImageSelect.__ENCList.RemoveRange(index, (dlgTVImageSelect.__ENCList.Count - index))
                    dlgTVImageSelect.__ENCList.Capacity = dlgTVImageSelect.__ENCList.Count
                End If
                dlgTVImageSelect.__ENCList.Add(New WeakReference(RuntimeHelpers.GetObjectValue(value)))
            End SyncLock
        End Sub

        <CompilerGenerated> _
        Private Shared Function _Lambda$__132(ByVal p As TVDBShowPoster) As Boolean
            Return ((Not Information.IsNothing(p.Image.Image) AndAlso (p.Type = Master.eSettings.PreferredShowBannerType)) AndAlso (p.Language = Master.eSettings.TVDBLanguage))
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__133(ByVal p As TVDBShowPoster) As Boolean
            Return (Not Information.IsNothing(p.Image.Image) AndAlso (p.Language = Master.eSettings.TVDBLanguage))
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__134(ByVal p As TVDBShowPoster) As Boolean
            Return (Not Information.IsNothing(p.Image.Image) AndAlso (p.Type = Master.eSettings.PreferredShowBannerType))
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__135(ByVal p As TVDBShowPoster) As Boolean
            Return Not Information.IsNothing(p.Image.Image)
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__136(ByVal p As TVDBPoster) As Boolean
            Return ((p.Language = Master.eSettings.TVDBLanguage) AndAlso Not Information.IsNothing(p.Image.Image))
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__137(ByVal p As TVDBPoster) As Boolean
            Return Not Information.IsNothing(p.Image.Image)
        End Function

        <CompilerGenerated> _
        Private Function _Lambda$__138(ByVal p As TVDBPoster) As Boolean
            Return ((Not Information.IsNothing(p.Image.Image) AndAlso (Me.GetPosterDims(p.Size) = Master.eSettings.PreferredShowPosterSize)) AndAlso (p.Language = Master.eSettings.TVDBLanguage))
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__139(ByVal p As TVDBPoster) As Boolean
            Return (Not Information.IsNothing(p.Image.Image) AndAlso (p.Language = Master.eSettings.TVDBLanguage))
        End Function

        <CompilerGenerated> _
        Private Function _Lambda$__140(ByVal p As TVDBPoster) As Boolean
            Return (Not Information.IsNothing(p.Image.Image) AndAlso (Me.GetPosterDims(p.Size) = Master.eSettings.PreferredShowPosterSize))
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__141(ByVal p As TVDBPoster) As Boolean
            Return Not Information.IsNothing(p.Image.Image)
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__142(ByVal p As TVDBShowPoster) As Boolean
            Return (Not Information.IsNothing(p.Image.Image) AndAlso (p.Language = Master.eSettings.TVDBLanguage))
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__143(ByVal p As TVDBShowPoster) As Boolean
            Return Not Information.IsNothing(p.Image.Image)
        End Function

        <CompilerGenerated> _
        Private Function _Lambda$__144(ByVal f As TVDBFanart) As Boolean
            Return ((Not Information.IsNothing(f.Image.Image) AndAlso (Me.GetFanartDims(f.Size) = Master.eSettings.PreferredShowFanartSize)) AndAlso (f.Language = Master.eSettings.TVDBLanguage))
        End Function

        <CompilerGenerated> _
        Private Function _Lambda$__145(ByVal f As TVDBFanart) As Boolean
            Return (Not Information.IsNothing(f.Image.Image) AndAlso (Me.GetFanartDims(f.Size) = Master.eSettings.PreferredShowFanartSize))
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__146(ByVal f As TVDBFanart) As Boolean
            Return Not Information.IsNothing(f.Image.Image)
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__147(ByVal p As TVDBShowPoster) As Boolean
            Return ((Not Information.IsNothing(p.Image.Image) AndAlso (p.Type = Master.eSettings.PreferredAllSBannerType)) AndAlso (p.Language = Master.eSettings.TVDBLanguage))
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__148(ByVal p As TVDBShowPoster) As Boolean
            Return (Not Information.IsNothing(p.Image.Image) AndAlso (p.Type = Master.eSettings.PreferredAllSBannerType))
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__149(ByVal p As TVDBShowPoster) As Boolean
            Return Not Information.IsNothing(p.Image.Image)
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__150(ByVal p As TVDBPoster) As Boolean
            Return (Not Information.IsNothing(p.Image.Image) AndAlso (p.Language = Master.eSettings.TVDBLanguage))
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__151(ByVal p As TVDBPoster) As Boolean
            Return Not Information.IsNothing(p.Image.Image)
        End Function

        <CompilerGenerated> _
        Private Function _Lambda$__152(ByVal p As TVDBPoster) As Boolean
            Return ((Not Information.IsNothing(p.Image.Image) AndAlso (Me.GetPosterDims(p.Size) = Master.eSettings.PreferredAllSPosterSize)) AndAlso (p.Language = Master.eSettings.TVDBLanguage))
        End Function

        <CompilerGenerated> _
        Private Function _Lambda$__153(ByVal p As TVDBPoster) As Boolean
            Return (Not Information.IsNothing(p.Image.Image) AndAlso (Me.GetPosterDims(p.Size) = Master.eSettings.PreferredAllSPosterSize))
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__154(ByVal p As TVDBPoster) As Boolean
            Return Not Information.IsNothing(p.Image.Image)
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__155(ByVal p As TVDBShowPoster) As Boolean
            Return (Not Information.IsNothing(p.Image.Image) AndAlso (p.Language = Master.eSettings.TVDBLanguage))
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__156(ByVal p As TVDBShowPoster) As Boolean
            Return Not Information.IsNothing(p.Image.Image)
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__162(ByVal s As TVDBSeasonImage) As Integer
            Return s.Season
        End Function

        <CompilerGenerated> _
        Private Function _Lambda$__163(ByVal s As TVDBSeasonImage) As Boolean
            Return (s.Season = Me.SelSeason)
        End Function

        <CompilerGenerated> _
        Private Function _Lambda$__164(ByVal s As TVDBSeasonImage) As Boolean
            Return (s.Season = Me.SelSeason)
        End Function

        <CompilerGenerated> _
        Private Function _Lambda$__165(ByVal s As TVDBSeasonImage) As Boolean
            Return (s.Season = Me.SelSeason)
        End Function

        <CompilerGenerated> _
        Private Function _Lambda$__166(ByVal s As TVDBSeasonImage) As Boolean
            Return (s.Season = Me.SelSeason)
        End Function

        <CompilerGenerated> _
        Private Function _Lambda$__167(ByVal s As TVDBSeasonImage) As Boolean
            Return (s.Season = Me.SelSeason)
        End Function

        <CompilerGenerated> _
        Private Function _Lambda$__168(ByVal s As TVDBSeasonImage) As Boolean
            Return (s.Season = Me.SelSeason)
        End Function

        <CompilerGenerated> _
        Private Function _Lambda$__170(ByVal f As TVDBSeasonImage) As Boolean
            Return (f.Season = Me.SelSeason)
        End Function

        Private Sub AddImage(ByVal iImage As Image, ByVal sDescription As String, ByVal iIndex As Integer, ByVal fTag As ImageTag)
            Try 
                Me.pnlImage = DirectCast(Utils.CopyArray(DirectCast(Me.pnlImage, Array), New Panel((iIndex + 1)  - 1) {}), Panel())
                Me.pbImage = DirectCast(Utils.CopyArray(DirectCast(Me.pbImage, Array), New PictureBox((iIndex + 1)  - 1) {}), PictureBox())
                Me.lblImage = DirectCast(Utils.CopyArray(DirectCast(Me.lblImage, Array), New Label((iIndex + 1)  - 1) {}), Label())
                Me.pnlImage(iIndex) = New Panel
                Me.pbImage(iIndex) = New PictureBox
                Me.lblImage(iIndex) = New Label
                Me.pbImage(iIndex).Name = iIndex.ToString
                Me.pnlImage(iIndex).Name = iIndex.ToString
                Me.lblImage(iIndex).Name = iIndex.ToString
                Dim size2 As New Size(&HBB, &HBB)
                Me.pnlImage(iIndex).Size = size2
                size2 = New Size(&HB5, &H97)
                Me.pbImage(iIndex).Size = size2
                size2 = New Size(&HB5, 30)
                Me.lblImage(iIndex).Size = size2
                Me.pnlImage(iIndex).BackColor = Color.White
                Me.pnlImage(iIndex).BorderStyle = BorderStyle.FixedSingle
                Me.pbImage(iIndex).SizeMode = PictureBoxSizeMode.Zoom
                Me.lblImage(iIndex).AutoSize = False
                Me.lblImage(iIndex).BackColor = Color.White
                Me.lblImage(iIndex).TextAlign = ContentAlignment.MiddleCenter
                Me.lblImage(iIndex).Text = sDescription
                Me.pbImage(iIndex).Image = iImage
                Me.pnlImage(iIndex).Left = Me.iLeft
                Me.pbImage(iIndex).Left = 3
                Me.lblImage(iIndex).Left = 0
                Me.pnlImage(iIndex).Top = Me.iTop
                Me.pbImage(iIndex).Top = 3
                Me.lblImage(iIndex).Top = &H97
                Me.pnlImage(iIndex).Tag = fTag
                Me.pbImage(iIndex).Tag = fTag
                Me.lblImage(iIndex).Tag = fTag
                Me.pnlImages.Controls.Add(Me.pnlImage(iIndex))
                Me.pnlImage(iIndex).Controls.Add(Me.pbImage(iIndex))
                Me.pnlImage(iIndex).Controls.Add(Me.lblImage(iIndex))
                Me.pnlImage(iIndex).BringToFront
                AddHandler Me.pbImage(iIndex).Click, New EventHandler(AddressOf Me.pbImage_Click)
                AddHandler Me.pbImage(iIndex).DoubleClick, New EventHandler(AddressOf Me.pbImage_DoubleClick)
                AddHandler Me.pnlImage(iIndex).Click, New EventHandler(AddressOf Me.pnlImage_Click)
                AddHandler Me.lblImage(iIndex).Click, New EventHandler(AddressOf Me.lblImage_Click)
                AddHandler Me.pbImage(iIndex).MouseWheel, New MouseEventHandler(AddressOf Me.MouseWheelEvent)
                AddHandler Me.pnlImage(iIndex).MouseWheel, New MouseEventHandler(AddressOf Me.MouseWheelEvent)
                AddHandler Me.lblImage(iIndex).MouseWheel, New MouseEventHandler(AddressOf Me.MouseWheelEvent)
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
                Me.iTop = (Me.iTop + &HC0)
            Else
                Me.iLeft = (Me.iLeft + &HC0)
            End If
        End Sub

        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs)
            If Me.bwLoadData.IsBusy Then
                Me.bwLoadData.CancelAsync
            End If
            If Me.bwLoadImages.IsBusy Then
                Me.bwLoadImages.CancelAsync
            End If
            Do While (Me.bwLoadData.IsBusy OrElse Me.bwLoadImages.IsBusy)
                Application.DoEvents
                Thread.Sleep(50)
            Loop
            Me.DialogResult = DialogResult.Cancel
            Me.Close
        End Sub

        Private Sub btnOK_Click(ByVal sender As Object, ByVal e As EventArgs)
            Me.DoneAndClose
        End Sub

        Private Sub bwLoadData_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
            Dim e$__ As New _Closure$__22
            Dim percentProgress As Integer = 1
            e$__.$VB$Local_iSeason = -1
            Me.bwLoadData.ReportProgress(Scraper.tmpTVDBShow.Episodes.Count, "current")
            Scraper.TVDBImages.ShowPoster = New TVDBShowPoster
            Scraper.TVDBImages.ShowFanart = New TVDBFanart
            Scraper.TVDBImages.AllSeasonPoster = New TVDBShowPoster
            Scraper.TVDBImages.SeasonImageList = New List(Of TVDBSeasonImage)
            If Me.bwLoadData.CancellationPending Then
                e.Cancel = True
            Else
                Dim image As TVDBSeasonImage
                Select Case Me._type
                    Case TVImageType.All
                        If Not Me._withcurrent Then
                            Dim dbtv2 As DBTV
                            For Each dbtv2 In Scraper.tmpTVDBShow.Episodes
                                Try 
                                    e$__.$VB$Local_iSeason = dbtv2.TVEp.Season
                                    If (Scraper.TVDBImages.SeasonImageList.Where(Of TVDBSeasonImage)(New Func(Of TVDBSeasonImage, Boolean)(AddressOf e$__._Lambda$__161)).Count(Of TVDBSeasonImage)() = 0) Then
                                        image = New TVDBSeasonImage With { _
                                            .Season = e$__.$VB$Local_iSeason _
                                        }
                                        Scraper.TVDBImages.SeasonImageList.Add(image)
                                    End If
                                    If Me.bwLoadData.CancellationPending Then
                                        e.Cancel = True
                                        Exit For
                                    End If
                                    Me.bwLoadData.ReportProgress(percentProgress, "progress")
                                    percentProgress += 1
                                Catch exception3 As Exception
                                    ProjectData.SetProjectError(exception3)
                                    Dim exception2 As Exception = exception3
                                    Master.eLog.WriteToErrorLog(exception2.Message, exception2.StackTrace, "Error", True)
                                    ProjectData.ClearProjectError
                                End Try
                            Next
                            Exit Select
                        End If
                        If Not String.IsNullOrEmpty(Scraper.tmpTVDBShow.Show.ShowPosterPath) Then
                            Scraper.TVDBImages.ShowPoster.Image.FromFile(Scraper.tmpTVDBShow.Show.ShowPosterPath)
                            Scraper.TVDBImages.ShowPoster.LocalFile = Scraper.tmpTVDBShow.Show.ShowPosterPath
                        End If
                        If Me.bwLoadData.CancellationPending Then
                            e.Cancel = True
                        Else
                            If Not String.IsNullOrEmpty(Scraper.tmpTVDBShow.Show.ShowFanartPath) Then
                                Scraper.TVDBImages.ShowFanart.Image.FromFile(Scraper.tmpTVDBShow.Show.ShowFanartPath)
                                Scraper.TVDBImages.ShowFanart.LocalFile = Scraper.tmpTVDBShow.Show.ShowFanartPath
                            End If
                            If Me.bwLoadData.CancellationPending Then
                                e.Cancel = True
                            Else
                                If (Master.eSettings.AllSeasonPosterEnabled AndAlso Not String.IsNullOrEmpty(Scraper.tmpTVDBShow.AllSeason.SeasonPosterPath)) Then
                                    Scraper.TVDBImages.AllSeasonPoster.Image.FromFile(Scraper.tmpTVDBShow.AllSeason.SeasonPosterPath)
                                    Scraper.TVDBImages.AllSeasonPoster.LocalFile = Scraper.tmpTVDBShow.AllSeason.SeasonPosterPath
                                End If
                                If Me.bwLoadData.CancellationPending Then
                                    e.Cancel = True
                                Else
                                    Dim dbtv As DBTV
                                    For Each dbtv In Scraper.tmpTVDBShow.Episodes
                                        Try 
                                            e$__.$VB$Local_iSeason = dbtv.TVEp.Season
                                            If (e$__.$VB$Local_iSeason > -1) Then
                                                If (Information.IsNothing(Scraper.TVDBImages.ShowPoster.Image) AndAlso Not String.IsNullOrEmpty(dbtv.ShowPosterPath)) Then
                                                    Scraper.TVDBImages.ShowPoster.Image.FromFile(dbtv.ShowPosterPath)
                                                End If
                                                If Me.bwLoadData.CancellationPending Then
                                                    e.Cancel = True
                                                    Exit For
                                                End If
                                                If ((Master.eSettings.EpisodeFanartEnabled AndAlso Information.IsNothing(Scraper.TVDBImages.ShowFanart.Image.Image)) AndAlso Not String.IsNullOrEmpty(dbtv.ShowFanartPath)) Then
                                                    Scraper.TVDBImages.ShowFanart.Image.FromFile(dbtv.ShowFanartPath)
                                                    Scraper.TVDBImages.ShowFanart.LocalFile = dbtv.ShowFanartPath
                                                End If
                                                If Me.bwLoadData.CancellationPending Then
                                                    e.Cancel = True
                                                    Exit For
                                                End If
                                                If (Scraper.TVDBImages.SeasonImageList.Where(Of TVDBSeasonImage)(New Func(Of TVDBSeasonImage, Boolean)(AddressOf e$__._Lambda$__160)).Count(Of TVDBSeasonImage)() = 0) Then
                                                    image = New TVDBSeasonImage With { _
                                                        .Season = e$__.$VB$Local_iSeason _
                                                    }
                                                    If Not String.IsNullOrEmpty(dbtv.SeasonPosterPath) Then
                                                        image.Poster.FromFile(dbtv.SeasonPosterPath)
                                                    End If
                                                    If (Master.eSettings.SeasonFanartEnabled AndAlso Not String.IsNullOrEmpty(dbtv.SeasonFanartPath)) Then
                                                        image.Fanart.Image.FromFile(dbtv.SeasonFanartPath)
                                                        image.Fanart.LocalFile = dbtv.SeasonFanartPath
                                                    End If
                                                    Scraper.TVDBImages.SeasonImageList.Add(image)
                                                End If
                                                If Me.bwLoadData.CancellationPending Then
                                                    e.Cancel = True
                                                    Exit For
                                                End If
                                            End If
                                            Me.bwLoadData.ReportProgress(percentProgress, "progress")
                                            percentProgress += 1
                                        Catch exception1 As Exception
                                            ProjectData.SetProjectError(exception1)
                                            Dim exception As Exception = exception1
                                            Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                                            ProjectData.ClearProjectError
                                        End Try
                                    Next
                                End If
                            End If
                        End If
                        Exit Select
                    Case TVImageType.ShowPoster
                        Scraper.TVDBImages.ShowPoster.Image.Image = Me.pbCurrent.Image
                        Exit Select
                    Case TVImageType.ShowFanart, TVImageType.EpisodeFanart
                        Scraper.TVDBImages.ShowFanart.Image.Image = Me.pbCurrent.Image
                        Exit Select
                    Case TVImageType.SeasonPoster
                        image = New TVDBSeasonImage With { _
                            .Season = Me._season _
                        }
                        image.Poster.Image = Me.pbCurrent.Image
                        Scraper.TVDBImages.SeasonImageList.Add(image)
                        Exit Select
                    Case TVImageType.SeasonFanart
                        image = New TVDBSeasonImage With { _
                            .Season = Me._season _
                        }
                        image.Fanart.Image.Image = Me.pbCurrent.Image
                        Scraper.TVDBImages.SeasonImageList.Add(image)
                        Exit Select
                    Case TVImageType.AllSeasonPoster
                        Scraper.TVDBImages.AllSeasonPoster.Image.Image = Me.pbCurrent.Image
                        Exit Select
                End Select
            End If
        End Sub

        Private Sub bwLoadData_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
            Try 
                If (e.UserState.ToString = "progress") Then
                    Me.pbStatus.Value = e.ProgressPercentage
                ElseIf (e.UserState.ToString = "current") Then
                    Me.lblStatus.Text = Master.eLang.GetString(&H58, "Loading Current Images...", False)
                    Me.pbStatus.Value = 0
                    Me.pbStatus.Maximum = e.ProgressPercentage
                Else
                    Me.pbStatus.Value = 0
                    Me.pbStatus.Maximum = e.ProgressPercentage
                End If
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Private Sub bwLoadData_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
            If Not e.Cancelled Then
                Me.GenerateList
                Me.lblStatus.Text = Master.eLang.GetString(&H59, "(Down)Loading New Images...", False)
                Me.bwLoadImages.WorkerReportsProgress = True
                Me.bwLoadImages.WorkerSupportsCancellation = True
                Me.bwLoadImages.RunWorkerAsync
            End If
        End Sub

        Private Sub bwLoadImages_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
            e.Cancel = Me.DownloadAllImages
        End Sub

        Private Sub bwLoadImages_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
            Try 
                If (e.UserState.ToString = "progress") Then
                    Me.pbStatus.Value = e.ProgressPercentage
                ElseIf (e.UserState.ToString = "defaults") Then
                    Me.lblStatus.Text = Master.eLang.GetString(90, "Setting Defaults...", False)
                    Me.pbStatus.Value = 0
                    Me.pbStatus.Maximum = e.ProgressPercentage
                Else
                    Me.pbStatus.Value = 0
                    Me.pbStatus.Maximum = e.ProgressPercentage
                End If
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Private Sub bwLoadImages_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
            Me.pnlStatus.Visible = False
            If (Me._ScrapeType = ScrapeType.FullAuto) Then
                Me.DoneAndClose
            Else
                If Not e.Cancelled Then
                    Me.tvList.Enabled = True
                    Me.tvList.Visible = True
                    Me.tvList.SelectedNode = Me.tvList.Nodes.Item(0)
                    Me.tvList.Focus
                    Me.btnOK.Enabled = True
                End If
                Me.pbCurrent.Visible = True
                Me.lblCurrentImage.Visible = True
            End If
        End Sub

        Private Sub CheckCurrentImage()
            Me.pbDelete.Visible = (Not Information.IsNothing(Me.pbCurrent.Image) AndAlso Me.pbCurrent.Visible)
            Me.pbUndo.Visible = Me.pbCurrent.Visible
        End Sub

        Private Sub ClearImages()
            Try 
                Dim num2 As Integer
                Me.iCounter = 0
                Me.iLeft = 5
                Me.iTop = 5
                Me.pbCurrent.Image = Nothing
                If (Me.pnlImages.Controls.Count <= 0) Then
                    Return
                End If
                Dim index As Integer = Information.UBound(Me.pnlImage, 1)
                goto Label_0137
            Label_0050:
                If Not Information.IsNothing(Me.pnlImage(index)) Then
                    If (Not Information.IsNothing(Me.lblImage(index)) AndAlso Me.pnlImage(index).Contains(Me.lblImage(index))) Then
                        Me.pnlImage(index).Controls.Remove(Me.lblImage(index))
                    End If
                    If (Not Information.IsNothing(Me.pbImage(index)) AndAlso Me.pnlImage(index).Contains(Me.pbImage(index))) Then
                        Me.pnlImage(index).Controls.Remove(Me.pbImage(index))
                    End If
                    If Me.pnlImages.Contains(Me.pnlImage(index)) Then
                        Me.pnlImages.Controls.Remove(Me.pnlImage(index))
                    End If
                End If
                index = (index + -1)
            Label_0137:
                num2 = 0
                If (index >= num2) Then
                    goto Label_0050
                End If
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
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

        Private Sub dlgTVImageSelect_Load(ByVal sender As Object, ByVal e As EventArgs)
            AddHandler Me.pnlImages.MouseWheel, New MouseEventHandler(AddressOf Me.MouseWheelEvent)
            AddHandler MyBase.MouseWheel, New MouseEventHandler(AddressOf Me.MouseWheelEvent)
            AddHandler Me.tvList.MouseWheel, New MouseEventHandler(AddressOf Me.MouseWheelEvent)
            Dim pnlImages As Panel = Me.pnlImages
            Functions.PNLDoubleBuffer(pnlImages)
            Me.pnlImages = pnlImages
            Me.SetUp
        End Sub

        Private Sub dlgTVImageSelect_Shown(ByVal sender As Object, ByVal e As EventArgs)
            Me.bwLoadData.WorkerReportsProgress = True
            Me.bwLoadData.WorkerSupportsCancellation = True
            Me.bwLoadData.RunWorkerAsync
        End Sub

        Private Sub DoneAndClose()
            If (Me._type = TVImageType.All) Then
                Me.lblStatus.Text = Master.eLang.GetString(&H57, "Downloading Fullsize Fanart Image...", False)
                Me.pbStatus.Style = ProgressBarStyle.Marquee
                Me.pnlStatus.Visible = True
                Master.currShow.ShowPosterPath = Scraper.TVDBImages.ShowPoster.LocalFile
                If (Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowFanart.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.ShowFanart.LocalFile)) Then
                    Scraper.TVDBImages.ShowFanart.Image.FromFile(Scraper.TVDBImages.ShowFanart.LocalFile)
                    Master.currShow.ShowFanartPath = Scraper.TVDBImages.ShowFanart.LocalFile
                ElseIf (Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowFanart.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowFanart.LocalFile)) Then
                    Scraper.TVDBImages.ShowFanart.Image.Clear
                    Scraper.TVDBImages.ShowFanart.Image.FromWeb(Scraper.TVDBImages.ShowFanart.URL)
                    If Not Information.IsNothing(Scraper.TVDBImages.ShowFanart.Image.Image) Then
                        Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.ShowFanart.LocalFile).FullName)
                        Scraper.TVDBImages.ShowFanart.Image.Save(Scraper.TVDBImages.ShowFanart.LocalFile, 0)
                        Master.currShow.ShowFanartPath = Scraper.TVDBImages.ShowFanart.LocalFile
                    End If
                End If
                If (Master.eSettings.AllSeasonPosterEnabled AndAlso Not Information.IsNothing(Scraper.TVDBImages.AllSeasonPoster.Image.Image)) Then
                    Master.currShow.SeasonPosterPath = Scraper.TVDBImages.AllSeasonPoster.LocalFile
                End If
            ElseIf ((Me._type = TVImageType.SeasonFanart) AndAlso Me._fanartchanged) Then
                Me.lblStatus.Text = Master.eLang.GetString(&H57, "Downloading Fullsize Fanart Image...", False)
                Me.pbStatus.Style = ProgressBarStyle.Marquee
                Me.pnlStatus.Visible = True
                If (Not String.IsNullOrEmpty(Scraper.TVDBImages.SeasonImageList.Item(0).Fanart.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.SeasonImageList.Item(0).Fanart.LocalFile)) Then
                    Scraper.TVDBImages.SeasonImageList.Item(0).Fanart.Image.FromFile(Scraper.TVDBImages.SeasonImageList.Item(0).Fanart.LocalFile)
                    Me.pbCurrent.Image = Scraper.TVDBImages.SeasonImageList.Item(0).Fanart.Image.Image
                ElseIf (Not String.IsNullOrEmpty(Scraper.TVDBImages.SeasonImageList.Item(0).Fanart.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.SeasonImageList.Item(0).Fanart.LocalFile)) Then
                    Scraper.TVDBImages.SeasonImageList.Item(0).Fanart.Image.Clear
                    Scraper.TVDBImages.SeasonImageList.Item(0).Fanart.Image.FromWeb(Scraper.TVDBImages.SeasonImageList.Item(0).Fanart.URL)
                    If Not Information.IsNothing(Scraper.TVDBImages.SeasonImageList.Item(0).Fanart.Image.Image) Then
                        Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.SeasonImageList.Item(0).Fanart.LocalFile).FullName)
                        Scraper.TVDBImages.SeasonImageList.Item(0).Fanart.Image.Save(Scraper.TVDBImages.SeasonImageList.Item(0).Fanart.LocalFile, 0)
                        Me.pbCurrent.Image = Scraper.TVDBImages.SeasonImageList.Item(0).Fanart.Image.Image
                    End If
                End If
            ElseIf (((Me._type = TVImageType.ShowFanart) OrElse (Me._type = TVImageType.EpisodeFanart)) AndAlso Me._fanartchanged) Then
                Me.lblStatus.Text = Master.eLang.GetString(&H57, "Downloading Fullsize Fanart Image...", False)
                Me.pbStatus.Style = ProgressBarStyle.Marquee
                Me.pnlStatus.Visible = True
                If (Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowFanart.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.ShowFanart.LocalFile)) Then
                    Scraper.TVDBImages.ShowFanart.Image.FromFile(Scraper.TVDBImages.ShowFanart.LocalFile)
                    Me.pbCurrent.Image = Scraper.TVDBImages.ShowFanart.Image.Image
                ElseIf (Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowFanart.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowFanart.LocalFile)) Then
                    Scraper.TVDBImages.ShowFanart.Image.Clear
                    Scraper.TVDBImages.ShowFanart.Image.FromWeb(Scraper.TVDBImages.ShowFanart.URL)
                    If Not Information.IsNothing(Scraper.TVDBImages.ShowFanart.Image.Image) Then
                        Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.ShowFanart.LocalFile).FullName)
                        Scraper.TVDBImages.ShowFanart.Image.Save(Scraper.TVDBImages.ShowFanart.LocalFile, 0)
                        Me.pbCurrent.Image = Scraper.TVDBImages.ShowFanart.Image.Image
                    End If
                End If
            End If
            Me.DialogResult = DialogResult.OK
            Me.Close
        End Sub

        Private Sub DoSelect(ByVal iIndex As Integer, ByVal SelImage As Image, ByVal SelTag As ImageTag)
            Try 
                Dim num2 As Integer = Information.UBound(Me.pnlImage, 1)
                Dim i As Integer = 0
                Do While (i <= num2)
                    Me.pnlImage(i).BackColor = Color.White
                    Me.lblImage(i).BackColor = Color.White
                    Me.lblImage(i).ForeColor = Color.Black
                    i += 1
                Loop
                Me.pnlImage(iIndex).BackColor = Color.Blue
                Me.lblImage(iIndex).BackColor = Color.Blue
                Me.lblImage(iIndex).ForeColor = Color.White
                Me.SetImage(SelImage, SelTag)
                Me.CheckCurrentImage
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Private Function DownloadAllImages() As Boolean
            Dim percentProgress As Integer = 1
            Try 
                Me.bwLoadImages.ReportProgress(((((Scraper.tmpTVDBShow.Episodes.Count + Scraper.tmpTVDBShow.SeasonPosters.Count) + Scraper.tmpTVDBShow.ShowPosters.Count) + Scraper.tmpTVDBShow.Fanart.Count) + Scraper.tmpTVDBShow.Posters.Count), "max")
                If (Me._type = TVImageType.All) Then
                    Dim dbtv As DBTV
                    For Each dbtv In Scraper.tmpTVDBShow.Episodes
                        Try 
                            If Not File.Exists(dbtv.TVEp.LocalFile) Then
                                If Not String.IsNullOrEmpty(dbtv.TVEp.PosterURL) Then
                                    dbtv.TVEp.Poster.FromWeb(dbtv.TVEp.PosterURL)
                                    If Not Information.IsNothing(dbtv.TVEp.Poster.Image) Then
                                        Directory.CreateDirectory(Directory.GetParent(dbtv.TVEp.LocalFile).FullName)
                                        dbtv.TVEp.Poster.Save(dbtv.TVEp.LocalFile, 0)
                                    End If
                                End If
                            Else
                                dbtv.TVEp.Poster.FromFile(dbtv.TVEp.LocalFile)
                            End If
                            If Me.bwLoadImages.CancellationPending Then
                                Return True
                            End If
                            Me.bwLoadImages.ReportProgress(percentProgress, "progress")
                            percentProgress += 1
                        Catch exception1 As Exception
                            ProjectData.SetProjectError(exception1)
                            Dim exception As Exception = exception1
                            Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                            ProjectData.ClearProjectError
                        End Try
                    Next
                End If
                If (((Me._type = TVImageType.All) OrElse (Me._type = TVImageType.SeasonPoster)) OrElse (Me._type = TVImageType.AllSeasonPoster)) Then
                    Dim poster As TVDBSeasonPoster
                    For Each poster In Scraper.tmpTVDBShow.SeasonPosters
                        Try 
                            If Not File.Exists(poster.LocalFile) Then
                                If Not String.IsNullOrEmpty(poster.URL) Then
                                    poster.Image.FromWeb(poster.URL)
                                    If Not Information.IsNothing(poster.Image.Image) Then
                                        Directory.CreateDirectory(Directory.GetParent(poster.LocalFile).FullName)
                                        poster.Image.Save(poster.LocalFile, 0)
                                        Me.SeasonList.Add(poster)
                                    End If
                                End If
                            Else
                                poster.Image.FromFile(poster.LocalFile)
                                Me.SeasonList.Add(poster)
                            End If
                            If Me.bwLoadImages.CancellationPending Then
                                Return True
                            End If
                            Me.bwLoadImages.ReportProgress(percentProgress, "progress")
                            percentProgress += 1
                        Catch exception7 As Exception
                            ProjectData.SetProjectError(exception7)
                            Dim exception2 As Exception = exception7
                            Master.eLog.WriteToErrorLog(exception2.Message, exception2.StackTrace, "Error", True)
                            ProjectData.ClearProjectError
                        End Try
                    Next
                End If
                If (((Me._type = TVImageType.All) OrElse (Me._type = TVImageType.ShowPoster)) OrElse (Me._type = TVImageType.AllSeasonPoster)) Then
                    Dim poster2 As TVDBShowPoster
                    For Each poster2 In Scraper.tmpTVDBShow.ShowPosters
                        Try 
                            If Not File.Exists(poster2.LocalFile) Then
                                If Not String.IsNullOrEmpty(poster2.URL) Then
                                    poster2.Image.FromWeb(poster2.URL)
                                    If Not Information.IsNothing(poster2.Image.Image) Then
                                        Directory.CreateDirectory(Directory.GetParent(poster2.LocalFile).FullName)
                                        poster2.Image.Save(poster2.LocalFile, 0)
                                        Me.ShowPosterList.Add(poster2)
                                    End If
                                End If
                            Else
                                poster2.Image.FromFile(poster2.LocalFile)
                                Me.ShowPosterList.Add(poster2)
                            End If
                            If Me.bwLoadImages.CancellationPending Then
                                Return True
                            End If
                            Me.bwLoadImages.ReportProgress(percentProgress, "progress")
                            percentProgress += 1
                        Catch exception8 As Exception
                            ProjectData.SetProjectError(exception8)
                            Dim exception3 As Exception = exception8
                            Master.eLog.WriteToErrorLog(exception3.Message, exception3.StackTrace, "Error", True)
                            ProjectData.ClearProjectError
                        End Try
                    Next
                End If
                If (((Me._type = TVImageType.All) OrElse (Me._type = TVImageType.ShowFanart)) OrElse ((Me._type = TVImageType.SeasonFanart) OrElse (Me._type = TVImageType.EpisodeFanart))) Then
                    Dim fanart As TVDBFanart
                    For Each fanart In Scraper.tmpTVDBShow.Fanart
                        Try 
                            If Not File.Exists(fanart.LocalThumb) Then
                                If Not String.IsNullOrEmpty(fanart.ThumbnailURL) Then
                                    fanart.Image.FromWeb(fanart.ThumbnailURL)
                                    If Not Information.IsNothing(fanart.Image.Image) Then
                                        Directory.CreateDirectory(Directory.GetParent(fanart.LocalThumb).FullName)
                                        fanart.Image.Image.Save(fanart.LocalThumb)
                                        Me.FanartList.Add(fanart)
                                    End If
                                End If
                            Else
                                fanart.Image.FromFile(fanart.LocalThumb)
                                Me.FanartList.Add(fanart)
                            End If
                            If Me.bwLoadImages.CancellationPending Then
                                Return True
                            End If
                            Me.bwLoadImages.ReportProgress(percentProgress, "progress")
                            percentProgress += 1
                        Catch exception9 As Exception
                            ProjectData.SetProjectError(exception9)
                            Dim exception4 As Exception = exception9
                            Master.eLog.WriteToErrorLog(exception4.Message, exception4.StackTrace, "Error", True)
                            ProjectData.ClearProjectError
                        End Try
                    Next
                End If
                If (((Me._type = TVImageType.All) OrElse (Me._type = TVImageType.ShowPoster)) OrElse ((Me._type = TVImageType.SeasonPoster) OrElse (Me._type = TVImageType.AllSeasonPoster))) Then
                    Dim enumerator As Enumerator(Of TVDBPoster)
                    Try 
                        enumerator = Scraper.tmpTVDBShow.Posters.GetEnumerator
                        Do While enumerator.MoveNext
                            Dim current As TVDBPoster = enumerator.Current
                            Try 
                                If Not File.Exists(current.LocalFile) Then
                                    If Not String.IsNullOrEmpty(current.URL) Then
                                        current.Image.FromWeb(current.URL)
                                        If Not Information.IsNothing(current.Image.Image) Then
                                            Directory.CreateDirectory(Directory.GetParent(current.LocalFile).FullName)
                                            current.Image.Save(current.LocalFile, 0)
                                            Me.GenericPosterList.Add(current)
                                        End If
                                    End If
                                Else
                                    current.Image.FromFile(current.LocalFile)
                                    Me.GenericPosterList.Add(current)
                                End If
                                If Me.bwLoadImages.CancellationPending Then
                                    Return True
                                End If
                                Me.bwLoadImages.ReportProgress(percentProgress, "progress")
                                percentProgress += 1
                            Catch exception10 As Exception
                                ProjectData.SetProjectError(exception10)
                                Dim exception5 As Exception = exception10
                                Master.eLog.WriteToErrorLog(exception5.Message, exception5.StackTrace, "Error", True)
                                ProjectData.ClearProjectError
                            End Try
                        Loop
                    Finally
                        enumerator.Dispose
                    End Try
                End If
            Catch exception11 As Exception
                ProjectData.SetProjectError(exception11)
                Dim exception6 As Exception = exception11
                Master.eLog.WriteToErrorLog(exception6.Message, exception6.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
            Return Me.SetDefaults
        End Function

        Private Function DownloadFanart(ByVal iTag As ImageTag) As Image
            Dim http As New HTTP
            Using images As Images = New Images
                If (Not String.IsNullOrEmpty(iTag.Path) AndAlso File.Exists(iTag.Path)) Then
                    images.FromFile(iTag.Path)
                ElseIf (Not String.IsNullOrEmpty(iTag.Path) AndAlso Not String.IsNullOrEmpty(iTag.URL)) Then
                    Me.lblStatus.Text = Master.eLang.GetString(&H57, "Downloading Fullsize Fanart Image...", False)
                    Me.pbStatus.Style = ProgressBarStyle.Marquee
                    Me.pnlStatus.Visible = True
                    Application.DoEvents
                    images.FromWeb(iTag.URL)
                    If Not Information.IsNothing(images.Image) Then
                        Directory.CreateDirectory(Directory.GetParent(iTag.Path).FullName)
                        images.Save(iTag.Path, 0)
                    End If
                    http = Nothing
                    Me.pnlStatus.Visible = False
                End If
                Return images.Image
            End Using
        End Function

        Private Sub GenerateList()
            Try 
                Dim node2 As TreeNode
                If ((Me._type = TVImageType.All) OrElse (Me._type = TVImageType.ShowPoster)) Then
                    node2 = New TreeNode With { _
                        .Text = Master.eLang.GetString(&H5B, "Show Poster", False), _
                        .Tag = "showp", _
                        .ImageIndex = 0, _
                        .SelectedImageIndex = 0 _
                    }
                    Me.tvList.Nodes.Add(node2)
                End If
                If (((Me._type = TVImageType.All) OrElse (Me._type = TVImageType.ShowFanart)) OrElse (Me._type = TVImageType.EpisodeFanart)) Then
                    node2 = New TreeNode With { _
                        .Text = If((Me._type = TVImageType.EpisodeFanart), Master.eLang.GetString(&H5C, "Episode Fanart", False), Master.eLang.GetString(&H5D, "Show Fanart", False)), _
                        .Tag = "showf", _
                        .ImageIndex = 1, _
                        .SelectedImageIndex = 1 _
                    }
                    Me.tvList.Nodes.Add(node2)
                End If
                If (((Me._type = TVImageType.All) OrElse (Me._type = TVImageType.AllSeasonPoster)) AndAlso Master.eSettings.AllSeasonPosterEnabled) Then
                    node2 = New TreeNode With { _
                        .Text = Master.eLang.GetString(&H5E, "All Seasons Poster", False), _
                        .Tag = "allp", _
                        .ImageIndex = 2, _
                        .SelectedImageIndex = 2 _
                    }
                    Me.tvList.Nodes.Add(node2)
                End If
                If (Me._type = TVImageType.All) Then
                    Dim image As TVDBSeasonImage
                    For Each image In Scraper.TVDBImages.SeasonImageList.OrderBy(Of TVDBSeasonImage, Integer)(New Func(Of TVDBSeasonImage, Integer)(AddressOf dlgTVImageSelect._Lambda$__162))
                        Try 
                            Dim node As New TreeNode(String.Format(Master.eLang.GetString(&H2D6, "Season {0}", True), image.Season), 3, 3)
                            node2 = New TreeNode With { _
                                .Text = Master.eLang.GetString(&H5F, "Season Posters", False), _
                                .Tag = ("p" & image.Season.ToString), _
                                .ImageIndex = 0, _
                                .SelectedImageIndex = 0 _
                            }
                            node.Nodes.Add(node2)
                            If Master.eSettings.SeasonFanartEnabled Then
                                node2 = New TreeNode With { _
                                    .Text = Master.eLang.GetString(&H60, "Season Fanart", False), _
                                    .Tag = ("f" & image.Season.ToString), _
                                    .ImageIndex = 1, _
                                    .SelectedImageIndex = 1 _
                                }
                                node.Nodes.Add(node2)
                            End If
                            Me.tvList.Nodes.Add(node)
                        Catch exception1 As Exception
                            ProjectData.SetProjectError(exception1)
                            Dim exception As Exception = exception1
                            Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                            ProjectData.ClearProjectError
                        End Try
                    Next
                ElseIf (Me._type = TVImageType.SeasonPoster) Then
                    node2 = New TreeNode With { _
                        .Text = String.Format(Master.eLang.GetString(&H61, "Season {0} Posters", False), Me._season), _
                        .Tag = ("p" & Me._season) _
                    }
                    Me.tvList.Nodes.Add(node2)
                ElseIf ((Me._type = TVImageType.SeasonFanart) AndAlso Master.eSettings.SeasonFanartEnabled) Then
                    node2 = New TreeNode With { _
                        .Text = String.Format(Master.eLang.GetString(&H63, "Season {0} Fanart", False), Me._season), _
                        .Tag = ("f" & Me._season) _
                    }
                    Me.tvList.Nodes.Add(node2)
                End If
                Me.tvList.ExpandAll
            Catch exception3 As Exception
                ProjectData.SetProjectError(exception3)
                Dim exception2 As Exception = exception3
                Master.eLog.WriteToErrorLog(exception2.Message, exception2.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Private Function GetFanartDims(ByVal fSize As Size) As FanartSize
            Dim small As FanartSize
            Try 
                If (((fSize.Width > &H3E8) AndAlso (fSize.Height > 750)) OrElse ((fSize.Height > &H3E8) AndAlso (fSize.Width > 750))) Then
                    Return FanartSize.Lrg
                End If
                If (((fSize.Width > 700) AndAlso (fSize.Height > 400)) OrElse ((fSize.Height > 700) AndAlso (fSize.Width > 400))) Then
                    Return FanartSize.Mid
                End If
                small = FanartSize.Small
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
            Return small
        End Function

        Private Function GetPosterDims(ByVal pSize As Size) As PosterSize
            Dim size As PosterSize
            Try 
                If (((pSize.Width > pSize.Height) AndAlso (pSize.Width > (pSize.Height * 2))) AndAlso (pSize.Width > 300)) Then
                    Return PosterSize.Wide
                End If
                If (((pSize.Height > &H3E8) AndAlso (pSize.Width > 750)) OrElse ((pSize.Width > &H3E8) AndAlso (pSize.Height > 750))) Then
                    Return PosterSize.Xlrg
                End If
                If (((pSize.Height > 700) AndAlso (pSize.Width > 500)) OrElse ((pSize.Width > 700) AndAlso (pSize.Height > 500))) Then
                    Return PosterSize.Lrg
                End If
                If (((pSize.Height > 250) AndAlso (pSize.Width > 150)) OrElse ((pSize.Width > 250) AndAlso (pSize.Height > 150))) Then
                    Return PosterSize.Mid
                End If
                Return PosterSize.Small
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
            Return size
        End Function

        <DebuggerStepThrough> _
        Private Sub InitializeComponent()
            Me.components = New Container
            Dim manager As New ComponentResourceManager(GetType(dlgTVImageSelect))
            Me.tvList = New TreeView
            Me.ImageList1 = New ImageList(Me.components)
            Me.pnlImages = New Panel
            Me.pbCurrent = New PictureBox
            Me.pnlStatus = New Panel
            Me.lblStatus = New Label
            Me.pbStatus = New ProgressBar
            Me.btnOK = New Button
            Me.btnCancel = New Button
            Me.pbDelete = New PictureBox
            Me.pbUndo = New PictureBox
            Me.lblCurrentImage = New Label
            DirectCast(Me.pbCurrent, ISupportInitialize).BeginInit
            Me.pnlStatus.SuspendLayout
            DirectCast(Me.pbDelete, ISupportInitialize).BeginInit
            DirectCast(Me.pbUndo, ISupportInitialize).BeginInit
            Me.SuspendLayout
            Me.tvList.Enabled = False
            Me.tvList.ImageIndex = 0
            Me.tvList.ImageList = Me.ImageList1
            Dim point2 As New Point(4, 5)
            Me.tvList.Location = point2
            Dim padding2 As New Padding(4, 4, 4, 4)
            Me.tvList.Margin = padding2
            Me.tvList.Name = "tvList"
            Me.tvList.SelectedImageIndex = 0
            Dim size2 As New Size(&H10A, &H146)
            Me.tvList.Size = size2
            Me.tvList.TabIndex = 0
            Me.tvList.Visible = False
            Me.ImageList1.ImageStream = DirectCast(manager.GetObject("ImageList1.ImageStream"), ImageListStreamer)
            Me.ImageList1.TransparentColor = Color.Transparent
            Me.ImageList1.Images.SetKeyName(0, "new_page.png")
            Me.ImageList1.Images.SetKeyName(1, "image.png")
            Me.ImageList1.Images.SetKeyName(2, "artwork.png")
            Me.ImageList1.Images.SetKeyName(3, "star_full.png")
            Me.pnlImages.AutoScroll = True
            Me.pnlImages.BackColor = SystemColors.Control
            point2 = New Point(&H116, 5)
            Me.pnlImages.Location = point2
            padding2 = New Padding(4, 4, 4, 4)
            Me.pnlImages.Margin = padding2
            Me.pnlImages.Name = "pnlImages"
            size2 = New Size(&H30A, &H20E)
            Me.pnlImages.Size = size2
            Me.pnlImages.TabIndex = 1
            Me.pbCurrent.BackColor = SystemColors.Control
            point2 = New Point(4, &H16E)
            Me.pbCurrent.Location = point2
            padding2 = New Padding(4, 4, 4, 4)
            Me.pbCurrent.Margin = padding2
            Me.pbCurrent.Name = "pbCurrent"
            size2 = New Size(&H10C, &HC4)
            Me.pbCurrent.Size = size2
            Me.pbCurrent.SizeMode = PictureBoxSizeMode.Zoom
            Me.pbCurrent.TabIndex = 2
            Me.pbCurrent.TabStop = False
            Me.pbCurrent.Visible = False
            Me.pnlStatus.BackColor = Color.White
            Me.pnlStatus.BorderStyle = BorderStyle.FixedSingle
            Me.pnlStatus.Controls.Add(Me.lblStatus)
            Me.pnlStatus.Controls.Add(Me.pbStatus)
            point2 = New Point(330, 240)
            Me.pnlStatus.Location = point2
            padding2 = New Padding(4, 4, 4, 4)
            Me.pnlStatus.Margin = padding2
            Me.pnlStatus.Name = "pnlStatus"
            size2 = New Size(&H191, &H5D)
            Me.pnlStatus.Size = size2
            Me.pnlStatus.TabIndex = 10
            Me.lblStatus.Font = New Font("Segoe UI", 8.25!, FontStyle.Bold, GraphicsUnit.Point, 0)
            point2 = New Point(6, 12)
            Me.lblStatus.Location = point2
            padding2 = New Padding(4, 0, 4, 0)
            Me.lblStatus.Margin = padding2
            Me.lblStatus.Name = "lblStatus"
            size2 = New Size(&H184, &H10)
            Me.lblStatus.Size = size2
            Me.lblStatus.TabIndex = 7
            Me.lblStatus.Text = "Gathering Data..."
            point2 = New Point(8, &H41)
            Me.pbStatus.Location = point2
            padding2 = New Padding(4, 4, 4, 4)
            Me.pbStatus.Margin = padding2
            Me.pbStatus.MarqueeAnimationSpeed = &H19
            Me.pbStatus.Name = "pbStatus"
            size2 = New Size(&H182, &H18)
            Me.pbStatus.Size = size2
            Me.pbStatus.Step = 1
            Me.pbStatus.Style = ProgressBarStyle.Continuous
            Me.pbStatus.TabIndex = 6
            Me.btnOK.Enabled = False
            point2 = New Point(&H375, &H21B)
            Me.btnOK.Location = point2
            padding2 = New Padding(4, 4, 4, 4)
            Me.btnOK.Margin = padding2
            Me.btnOK.Name = "btnOK"
            size2 = New Size(&H51, &H1C)
            Me.btnOK.Size = size2
            Me.btnOK.TabIndex = 11
            Me.btnOK.Text = "OK"
            Me.btnOK.UseVisualStyleBackColor = True
            Me.btnCancel.DialogResult = DialogResult.Cancel
            point2 = New Point(&H3CE, &H21B)
            Me.btnCancel.Location = point2
            padding2 = New Padding(4, 4, 4, 4)
            Me.btnCancel.Margin = padding2
            Me.btnCancel.Name = "btnCancel"
            size2 = New Size(&H51, &H1C)
            Me.btnCancel.Size = size2
            Me.btnCancel.TabIndex = 12
            Me.btnCancel.Text = "Cancel"
            Me.btnCancel.UseVisualStyleBackColor = True
            Me.pbDelete.Image = DirectCast(manager.GetObject("pbDelete.Image"), Image)
            point2 = New Point(4, &H16E)
            Me.pbDelete.Location = point2
            padding2 = New Padding(4, 4, 4, 4)
            Me.pbDelete.Margin = padding2
            Me.pbDelete.Name = "pbDelete"
            size2 = New Size(20, 20)
            Me.pbDelete.Size = size2
            Me.pbDelete.TabIndex = 13
            Me.pbDelete.TabStop = False
            Me.pbDelete.Visible = False
            Me.pbUndo.Image = DirectCast(manager.GetObject("pbUndo.Image"), Image)
            point2 = New Point(&HFB, &H16E)
            Me.pbUndo.Location = point2
            padding2 = New Padding(4, 4, 4, 4)
            Me.pbUndo.Margin = padding2
            Me.pbUndo.Name = "pbUndo"
            size2 = New Size(20, 20)
            Me.pbUndo.Size = size2
            Me.pbUndo.TabIndex = 14
            Me.pbUndo.TabStop = False
            Me.pbUndo.Visible = False
            Me.lblCurrentImage.AutoSize = True
            Me.lblCurrentImage.Font = New Font("Segoe UI", 9.75!, FontStyle.Bold, GraphicsUnit.Point, 0)
            point2 = New Point(0, &H156)
            Me.lblCurrentImage.Location = point2
            padding2 = New Padding(4, 0, 4, 0)
            Me.lblCurrentImage.Margin = padding2
            Me.lblCurrentImage.Name = "lblCurrentImage"
            size2 = New Size(&H83, &H17)
            Me.lblCurrentImage.Size = size2
            Me.lblCurrentImage.TabIndex = 15
            Me.lblCurrentImage.Text = "Current Image:"
            Me.lblCurrentImage.Visible = False
            Me.AcceptButton = Me.btnOK
            Dim ef2 As New SizeF(120!, 120!)
            Me.AutoScaleDimensions = ef2
            Me.AutoScaleMode = AutoScaleMode.Dpi
            Me.CancelButton = Me.btnCancel
            size2 = New Size(&H425, &H23E)
            Me.ClientSize = size2
            Me.ControlBox = False
            Me.Controls.Add(Me.lblCurrentImage)
            Me.Controls.Add(Me.pbUndo)
            Me.Controls.Add(Me.pbDelete)
            Me.Controls.Add(Me.btnCancel)
            Me.Controls.Add(Me.btnOK)
            Me.Controls.Add(Me.pnlStatus)
            Me.Controls.Add(Me.pbCurrent)
            Me.Controls.Add(Me.pnlImages)
            Me.Controls.Add(Me.tvList)
            Me.DoubleBuffered = True
            Me.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            Me.FormBorderStyle = FormBorderStyle.FixedDialog
            padding2 = New Padding(4, 4, 4, 4)
            Me.Margin = padding2
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            size2 = New Size(&H42B, &H25F)
            Me.MinimumSize = size2
            Me.Name = "dlgTVImageSelect"
            Me.StartPosition = FormStartPosition.CenterScreen
            Me.Text = "TV Image Selection"
            DirectCast(Me.pbCurrent, ISupportInitialize).EndInit
            Me.pnlStatus.ResumeLayout(False)
            DirectCast(Me.pbDelete, ISupportInitialize).EndInit
            DirectCast(Me.pbUndo, ISupportInitialize).EndInit
            Me.ResumeLayout(False)
            Me.PerformLayout
        End Sub

        Private Sub lblImage_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim iIndex As Integer = Convert.ToInt32(DirectCast(sender, Label).Name)
            Me.DoSelect(iIndex, Me.pbImage(iIndex).Image, DirectCast(DirectCast(sender, Label).Tag, ImageTag))
        End Sub

        Private Sub MouseWheelEvent(ByVal sender As Object, ByVal e As MouseEventArgs)
            Dim verticalScroll As VScrollProperties
            If (e.Delta < 0) Then
                If ((Me.pnlImages.VerticalScroll.Value + 50) <= Me.pnlImages.VerticalScroll.Maximum) Then
                    verticalScroll = Me.pnlImages.VerticalScroll
                    verticalScroll.Value = (verticalScroll.Value + 50)
                Else
                    Me.pnlImages.VerticalScroll.Value = Me.pnlImages.VerticalScroll.Maximum
                End If
            ElseIf ((Me.pnlImages.VerticalScroll.Value - 50) >= Me.pnlImages.VerticalScroll.Minimum) Then
                verticalScroll = Me.pnlImages.VerticalScroll
                verticalScroll.Value = (verticalScroll.Value - 50)
            Else
                Me.pnlImages.VerticalScroll.Value = Me.pnlImages.VerticalScroll.Minimum
            End If
        End Sub

        Private Sub pbDelete_Click(ByVal sender As Object, ByVal e As EventArgs)
            Me.pbCurrent.Image = Nothing
            Me.SetImage(Nothing, New ImageTag)
        End Sub

        Private Sub pbImage_Click(ByVal sender As Object, ByVal e As EventArgs)
            Me.DoSelect(Convert.ToInt32(DirectCast(sender, PictureBox).Name), DirectCast(sender, PictureBox).Image, DirectCast(DirectCast(sender, PictureBox).Tag, ImageTag))
        End Sub

        Private Sub pbImage_DoubleClick(ByVal sender As Object, ByVal e As EventArgs)
            Dim image As Image = Nothing
            Dim expression As ImageTag = DirectCast(DirectCast(sender, PictureBox).Tag, ImageTag)
            If (Not Information.IsNothing(expression) OrElse Not expression.isFanart) Then
                image = Me.DownloadFanart(expression)
            Else
                image = DirectCast(sender, PictureBox).Image
            End If
            ModulesManager.Instance.RuntimeObjects.InvokeOpenImageViewer(image)
        End Sub

        Private Sub pbUndo_Click(ByVal sender As Object, ByVal e As EventArgs)
            If (Me.SelSeason = -999) Then
                If Me.SelIsPoster Then
                    Scraper.TVDBImages.ShowPoster.Image.Image = Me.DefaultImages.ShowPoster.Image.Image
                    Scraper.TVDBImages.ShowPoster.LocalFile = Me.DefaultImages.ShowPoster.LocalFile
                    Scraper.TVDBImages.ShowPoster.URL = Me.DefaultImages.ShowPoster.URL
                    Me.pbCurrent.Image = Scraper.TVDBImages.ShowPoster.Image.Image
                Else
                    Scraper.TVDBImages.ShowFanart.Image.Image = Me.DefaultImages.ShowFanart.Image.Image
                    Scraper.TVDBImages.ShowFanart.LocalFile = Me.DefaultImages.ShowFanart.LocalFile
                    Scraper.TVDBImages.ShowFanart.URL = Me.DefaultImages.ShowFanart.URL
                    Me.pbCurrent.Image = Scraper.TVDBImages.ShowFanart.Image.Image
                End If
            ElseIf (Me.SelSeason = &H3E7) Then
                Scraper.TVDBImages.AllSeasonPoster.Image.Image = Me.DefaultImages.AllSeasonPoster.Image.Image
                Scraper.TVDBImages.AllSeasonPoster.LocalFile = Me.DefaultImages.AllSeasonPoster.LocalFile
                Scraper.TVDBImages.AllSeasonPoster.URL = Me.DefaultImages.AllSeasonPoster.URL
                Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonPoster.Image.Image
            ElseIf Me.SelIsPoster Then
                Dim image As Image = Me.DefaultImages.SeasonImageList.FirstOrDefault(Of TVDBSeasonImage)(New Func(Of TVDBSeasonImage, Boolean)(AddressOf Me._Lambda$__163)).Poster.Image
                Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Of TVDBSeasonImage)(New Func(Of TVDBSeasonImage, Boolean)(AddressOf Me._Lambda$__164)).Poster.Image = image
                Me.pbCurrent.Image = image
            Else
                Dim fanart As TVDBFanart = Me.DefaultImages.SeasonImageList.FirstOrDefault(Of TVDBSeasonImage)(New Func(Of TVDBSeasonImage, Boolean)(AddressOf Me._Lambda$__165)).Fanart
                Dim fanart2 As TVDBFanart = Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Of TVDBSeasonImage)(New Func(Of TVDBSeasonImage, Boolean)(AddressOf Me._Lambda$__166)).Fanart
                fanart2.Image.Image = fanart.Image.Image
                fanart2.LocalFile = fanart.LocalFile
                fanart2.URL = fanart.URL
                Me.pbCurrent.Image = fanart.Image.Image
            End If
        End Sub

        Private Sub pnlImage_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim iIndex As Integer = Convert.ToInt32(DirectCast(sender, Panel).Name)
            Me.DoSelect(iIndex, Me.pbImage(iIndex).Image, DirectCast(DirectCast(sender, Panel).Tag, ImageTag))
        End Sub

        Public Function SetDefaults() As Boolean
            Dim e$__ As New _Closure$__21 With { _
                .$VB$Local_iSeason = -1 _
            }
            Dim percentProgress As Integer = 3
            Try 
                Me.bwLoadImages.ReportProgress(((Scraper.TVDBImages.SeasonImageList.Count + Scraper.tmpTVDBShow.Episodes.Count) + 3), "defaults")
                If (((Me._type = TVImageType.All) OrElse (Me._type = TVImageType.ShowPoster)) AndAlso Information.IsNothing(Scraper.TVDBImages.ShowPoster.Image.Image)) Then
                    If Master.eSettings.IsShowBanner Then
                        Dim expression As TVDBShowPoster = Me.ShowPosterList.FirstOrDefault(Of TVDBShowPoster)(New Func(Of TVDBShowPoster, Boolean)(AddressOf dlgTVImageSelect._Lambda$__132))
                        If (Master.eSettings.OnlyGetTVImagesForSelectedLanguage AndAlso Information.IsNothing(expression)) Then
                            expression = Me.ShowPosterList.FirstOrDefault(Of TVDBShowPoster)(New Func(Of TVDBShowPoster, Boolean)(AddressOf dlgTVImageSelect._Lambda$__133))
                        End If
                        If Information.IsNothing(expression) Then
                            expression = Me.ShowPosterList.FirstOrDefault(Of TVDBShowPoster)(New Func(Of TVDBShowPoster, Boolean)(AddressOf dlgTVImageSelect._Lambda$__134))
                        End If
                        If Information.IsNothing(expression) Then
                            expression = Me.ShowPosterList.FirstOrDefault(Of TVDBShowPoster)(New Func(Of TVDBShowPoster, Boolean)(AddressOf dlgTVImageSelect._Lambda$__135))
                        End If
                        If Not Information.IsNothing(expression) Then
                            Scraper.TVDBImages.ShowPoster.Image.Image = expression.Image.Image
                            Scraper.TVDBImages.ShowPoster.LocalFile = expression.LocalFile
                            Scraper.TVDBImages.ShowPoster.URL = expression.URL
                        Else
                            Dim poster3 As TVDBPoster = Me.GenericPosterList.FirstOrDefault(Of TVDBPoster)(New Func(Of TVDBPoster, Boolean)(AddressOf dlgTVImageSelect._Lambda$__136))
                            If Information.IsNothing(poster3) Then
                                poster3 = Me.GenericPosterList.FirstOrDefault(Of TVDBPoster)(New Func(Of TVDBPoster, Boolean)(AddressOf dlgTVImageSelect._Lambda$__137))
                            End If
                            If Not Information.IsNothing(poster3) Then
                                Scraper.TVDBImages.ShowPoster.Image.Image = poster3.Image.Image
                                Scraper.TVDBImages.ShowPoster.LocalFile = poster3.LocalFile
                                Scraper.TVDBImages.ShowPoster.URL = poster3.URL
                            End If
                        End If
                    Else
                        Dim poster4 As TVDBPoster = Me.GenericPosterList.FirstOrDefault(Of TVDBPoster)(New Func(Of TVDBPoster, Boolean)(AddressOf Me._Lambda$__138))
                        If (Master.eSettings.OnlyGetTVImagesForSelectedLanguage AndAlso Information.IsNothing(poster4)) Then
                            poster4 = Me.GenericPosterList.FirstOrDefault(Of TVDBPoster)(New Func(Of TVDBPoster, Boolean)(AddressOf dlgTVImageSelect._Lambda$__139))
                        End If
                        If Information.IsNothing(poster4) Then
                            poster4 = Me.GenericPosterList.FirstOrDefault(Of TVDBPoster)(New Func(Of TVDBPoster, Boolean)(AddressOf Me._Lambda$__140))
                        End If
                        If Information.IsNothing(poster4) Then
                            poster4 = Me.GenericPosterList.FirstOrDefault(Of TVDBPoster)(New Func(Of TVDBPoster, Boolean)(AddressOf dlgTVImageSelect._Lambda$__141))
                        End If
                        If Not Information.IsNothing(poster4) Then
                            Scraper.TVDBImages.ShowPoster.Image.Image = poster4.Image.Image
                            Scraper.TVDBImages.ShowPoster.LocalFile = poster4.LocalFile
                            Scraper.TVDBImages.ShowPoster.URL = poster4.URL
                        Else
                            Dim poster5 As TVDBShowPoster = Me.ShowPosterList.FirstOrDefault(Of TVDBShowPoster)(New Func(Of TVDBShowPoster, Boolean)(AddressOf dlgTVImageSelect._Lambda$__142))
                            If Information.IsNothing(poster5) Then
                                poster5 = Me.ShowPosterList.FirstOrDefault(Of TVDBShowPoster)(New Func(Of TVDBShowPoster, Boolean)(AddressOf dlgTVImageSelect._Lambda$__143))
                            End If
                            If Not Information.IsNothing(poster5) Then
                                Scraper.TVDBImages.ShowPoster.Image.Image = poster5.Image.Image
                                Scraper.TVDBImages.ShowPoster.LocalFile = poster5.LocalFile
                                Scraper.TVDBImages.ShowPoster.URL = poster5.URL
                            End If
                        End If
                    End If
                End If
                If Me.bwLoadImages.CancellationPending Then
                    Return True
                End If
                Me.bwLoadImages.ReportProgress(1, "progress")
                If (((Me._type <> TVImageType.All) AndAlso (Me._type <> TVImageType.ShowFanart)) AndAlso (Me._type <> TVImageType.EpisodeFanart)) Then
                End If
                If Information.IsNothing(Scraper.TVDBImages.ShowFanart.Image.Image) Then
                    Dim fanart As TVDBFanart = Me.FanartList.FirstOrDefault(Of TVDBFanart)(New Func(Of TVDBFanart, Boolean)(AddressOf Me._Lambda$__144))
                    If Information.IsNothing(fanart) Then
                        fanart = Me.FanartList.FirstOrDefault(Of TVDBFanart)(New Func(Of TVDBFanart, Boolean)(AddressOf Me._Lambda$__145))
                    End If
                    If Information.IsNothing(fanart) Then
                        fanart = Me.FanartList.FirstOrDefault(Of TVDBFanart)(New Func(Of TVDBFanart, Boolean)(AddressOf dlgTVImageSelect._Lambda$__146))
                    End If
                    If Not Information.IsNothing(fanart) Then
                        If (Not String.IsNullOrEmpty(fanart.LocalFile) AndAlso File.Exists(fanart.LocalFile)) Then
                            Scraper.TVDBImages.ShowFanart.Image.FromFile(fanart.LocalFile)
                            Scraper.TVDBImages.ShowFanart.LocalFile = fanart.LocalFile
                            Scraper.TVDBImages.ShowFanart.URL = fanart.URL
                        ElseIf (Not String.IsNullOrEmpty(fanart.LocalFile) AndAlso Not String.IsNullOrEmpty(fanart.URL)) Then
                            Scraper.TVDBImages.ShowFanart.Image.FromWeb(fanart.URL)
                            If Not Information.IsNothing(Scraper.TVDBImages.ShowFanart.Image.Image) Then
                                Directory.CreateDirectory(Directory.GetParent(fanart.LocalFile).FullName)
                                Scraper.TVDBImages.ShowFanart.Image.Save(fanart.LocalFile, 0)
                                Scraper.TVDBImages.ShowFanart.LocalFile = fanart.LocalFile
                                Scraper.TVDBImages.ShowFanart.URL = fanart.URL
                            End If
                        End If
                    End If
                End If
                If Me.bwLoadImages.CancellationPending Then
                    Return True
                End If
                Me.bwLoadImages.ReportProgress(2, "progress")
                If (((Me._type = TVImageType.All) OrElse (Me._type = TVImageType.AllSeasonPoster)) AndAlso (Master.eSettings.AllSeasonPosterEnabled AndAlso Information.IsNothing(Scraper.TVDBImages.AllSeasonPoster.Image.Image))) Then
                    If Master.eSettings.IsAllSBanner Then
                        Dim poster6 As TVDBShowPoster = Me.ShowPosterList.FirstOrDefault(Of TVDBShowPoster)(New Func(Of TVDBShowPoster, Boolean)(AddressOf dlgTVImageSelect._Lambda$__147))
                        If Information.IsNothing(poster6) Then
                            poster6 = Me.ShowPosterList.FirstOrDefault(Of TVDBShowPoster)(New Func(Of TVDBShowPoster, Boolean)(AddressOf dlgTVImageSelect._Lambda$__148))
                        End If
                        If Information.IsNothing(poster6) Then
                            poster6 = Me.ShowPosterList.FirstOrDefault(Of TVDBShowPoster)(New Func(Of TVDBShowPoster, Boolean)(AddressOf dlgTVImageSelect._Lambda$__149))
                        End If
                        If Not Information.IsNothing(poster6) Then
                            Scraper.TVDBImages.AllSeasonPoster.Image.Image = poster6.Image.Image
                            Scraper.TVDBImages.AllSeasonPoster.LocalFile = poster6.LocalFile
                            Scraper.TVDBImages.AllSeasonPoster.URL = poster6.URL
                        Else
                            Dim poster7 As TVDBPoster = Me.GenericPosterList.FirstOrDefault(Of TVDBPoster)(New Func(Of TVDBPoster, Boolean)(AddressOf dlgTVImageSelect._Lambda$__150))
                            If Information.IsNothing(poster7) Then
                                poster7 = Me.GenericPosterList.FirstOrDefault(Of TVDBPoster)(New Func(Of TVDBPoster, Boolean)(AddressOf dlgTVImageSelect._Lambda$__151))
                            End If
                            If Not Information.IsNothing(poster7) Then
                                Scraper.TVDBImages.AllSeasonPoster.Image.Image = poster7.Image.Image
                                Scraper.TVDBImages.AllSeasonPoster.LocalFile = poster7.LocalFile
                                Scraper.TVDBImages.AllSeasonPoster.URL = poster7.URL
                            End If
                        End If
                    Else
                        Dim poster8 As TVDBPoster = Me.GenericPosterList.FirstOrDefault(Of TVDBPoster)(New Func(Of TVDBPoster, Boolean)(AddressOf Me._Lambda$__152))
                        If Information.IsNothing(poster8) Then
                            poster8 = Me.GenericPosterList.FirstOrDefault(Of TVDBPoster)(New Func(Of TVDBPoster, Boolean)(AddressOf Me._Lambda$__153))
                        End If
                        If Information.IsNothing(poster8) Then
                            poster8 = Me.GenericPosterList.FirstOrDefault(Of TVDBPoster)(New Func(Of TVDBPoster, Boolean)(AddressOf dlgTVImageSelect._Lambda$__154))
                        End If
                        If Not Information.IsNothing(poster8) Then
                            Scraper.TVDBImages.AllSeasonPoster.Image.Image = poster8.Image.Image
                            Scraper.TVDBImages.AllSeasonPoster.LocalFile = poster8.LocalFile
                            Scraper.TVDBImages.AllSeasonPoster.URL = poster8.URL
                        Else
                            Dim poster9 As TVDBShowPoster = Me.ShowPosterList.FirstOrDefault(Of TVDBShowPoster)(New Func(Of TVDBShowPoster, Boolean)(AddressOf dlgTVImageSelect._Lambda$__155))
                            If Information.IsNothing(poster9) Then
                                poster9 = Me.ShowPosterList.FirstOrDefault(Of TVDBShowPoster)(New Func(Of TVDBShowPoster, Boolean)(AddressOf dlgTVImageSelect._Lambda$__156))
                            End If
                            If Not Information.IsNothing(poster9) Then
                                Scraper.TVDBImages.AllSeasonPoster.Image.Image = poster9.Image.Image
                                Scraper.TVDBImages.AllSeasonPoster.LocalFile = poster9.LocalFile
                                Scraper.TVDBImages.AllSeasonPoster.URL = poster9.URL
                            End If
                        End If
                    End If
                End If
                If Me.bwLoadImages.CancellationPending Then
                    Return True
                End If
                Me.bwLoadImages.ReportProgress(3, "progress")
                If (((Me._type = TVImageType.All) OrElse (Me._type = TVImageType.SeasonPoster)) OrElse (Me._type = TVImageType.SeasonFanart)) Then
                    Dim image As TVDBSeasonImage
                    For Each image In Scraper.TVDBImages.SeasonImageList
                        Try 
                            e$__.$VB$Local_iSeason = image.Season
                            If (((Me._type = TVImageType.All) OrElse (Me._type = TVImageType.SeasonPoster)) AndAlso Information.IsNothing(image.Poster.Image)) Then
                                Dim poster As TVDBSeasonPoster = Me.SeasonList.FirstOrDefault(Of TVDBSeasonPoster)(New Func(Of TVDBSeasonPoster, Boolean)(AddressOf e$__._Lambda$__157))
                                If Information.IsNothing(poster) Then
                                    poster = Me.SeasonList.FirstOrDefault(Of TVDBSeasonPoster)(New Func(Of TVDBSeasonPoster, Boolean)(AddressOf e$__._Lambda$__158))
                                End If
                                If Information.IsNothing(poster) Then
                                    poster = Me.SeasonList.FirstOrDefault(Of TVDBSeasonPoster)(New Func(Of TVDBSeasonPoster, Boolean)(AddressOf e$__._Lambda$__159))
                                End If
                                If Not Information.IsNothing(poster) Then
                                    image.Poster.Image = poster.Image.Image
                                End If
                            End If
                            If (((Me._type = TVImageType.All) OrElse (Me._type = TVImageType.SeasonFanart)) AndAlso ((Master.eSettings.SeasonFanartEnabled AndAlso Information.IsNothing(image.Fanart.Image.Image)) AndAlso Not Information.IsNothing(Scraper.TVDBImages.ShowFanart.Image.Image))) Then
                                image.Fanart.Image.Image = Scraper.TVDBImages.ShowFanart.Image.Image
                            End If
                            If Me.bwLoadImages.CancellationPending Then
                                Return True
                            End If
                            Me.bwLoadImages.ReportProgress(percentProgress, "progress")
                            percentProgress += 1
                        Catch exception1 As Exception
                            ProjectData.SetProjectError(exception1)
                            Dim exception As Exception = exception1
                            Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                            ProjectData.ClearProjectError
                        End Try
                    Next
                End If
                If (Me._type = TVImageType.All) Then
                    Dim enumerator As Enumerator(Of DBTV)
                    Try 
                        enumerator = Scraper.tmpTVDBShow.Episodes.GetEnumerator
                        Do While enumerator.MoveNext
                            Dim current As DBTV = enumerator.Current
                            Try 
                                If Not String.IsNullOrEmpty(current.TVEp.LocalFile) Then
                                    current.TVEp.Poster.FromFile(current.TVEp.LocalFile)
                                ElseIf Not String.IsNullOrEmpty(current.EpPosterPath) Then
                                    current.TVEp.Poster.FromFile(current.EpPosterPath)
                                End If
                                If Master.eSettings.EpisodeFanartEnabled Then
                                    If Not String.IsNullOrEmpty(current.EpFanartPath) Then
                                        current.TVEp.Fanart.FromFile(current.EpFanartPath)
                                    ElseIf Not Information.IsNothing(Scraper.TVDBImages.ShowFanart.Image.Image) Then
                                        current.TVEp.Fanart.Image = Scraper.TVDBImages.ShowFanart.Image.Image
                                    End If
                                End If
                                If Me.bwLoadImages.CancellationPending Then
                                    Return True
                                End If
                                Me.bwLoadImages.ReportProgress(percentProgress, "progress")
                                percentProgress += 1
                            Catch exception4 As Exception
                                ProjectData.SetProjectError(exception4)
                                Dim exception2 As Exception = exception4
                                Master.eLog.WriteToErrorLog(exception2.Message, exception2.StackTrace, "Error", True)
                                ProjectData.ClearProjectError
                            End Try
                        Loop
                    Finally
                        enumerator.Dispose
                    End Try
                End If
            Catch exception5 As Exception
                ProjectData.SetProjectError(exception5)
                Dim exception3 As Exception = exception5
                Master.eLog.WriteToErrorLog(exception3.Message, exception3.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
            Me.DefaultImages = Scraper.TVDBImages.Clone
            Return False
        End Function

        Private Sub SetImage(ByVal SelImage As Image, ByVal SelTag As ImageTag)
            Me.pbCurrent.Image = SelImage
            Me._fanartchanged = True
            If (Me.SelSeason = -999) Then
                If Me.SelIsPoster Then
                    Scraper.TVDBImages.ShowPoster.Image.Image = SelImage
                    Scraper.TVDBImages.ShowPoster.LocalFile = SelTag.Path
                    Scraper.TVDBImages.ShowPoster.URL = SelTag.URL
                Else
                    Scraper.TVDBImages.ShowFanart.Image.Image = SelImage
                    Scraper.TVDBImages.ShowFanart.LocalFile = SelTag.Path
                    Scraper.TVDBImages.ShowFanart.URL = SelTag.URL
                End If
            ElseIf (Me.SelSeason = &H3E7) Then
                Scraper.TVDBImages.AllSeasonPoster.Image.Image = SelImage
                Scraper.TVDBImages.AllSeasonPoster.LocalFile = SelTag.Path
                Scraper.TVDBImages.AllSeasonPoster.URL = SelTag.URL
            ElseIf Me.SelIsPoster Then
                Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Of TVDBSeasonImage)(New Func(Of TVDBSeasonImage, Boolean)(AddressOf Me._Lambda$__167)).Poster.Image = SelImage
            Else
                Dim expression As TVDBFanart = Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Of TVDBSeasonImage)(New Func(Of TVDBSeasonImage, Boolean)(AddressOf Me._Lambda$__168)).Fanart
                If Not Information.IsNothing(expression) Then
                    expression.Image.Image = SelImage
                    expression.LocalFile = SelTag.Path
                    expression.URL = SelTag.URL
                End If
            End If
        End Sub

        Private Sub SetUp()
            Me.Text = Master.eLang.GetString(&H63, "TV Image Selection", False)
            Me.btnOK.Text = Master.eLang.GetString(&HB3, "OK", True)
            Me.btnCancel.Text = Master.eLang.GetString(&HA7, "Cancel", True)
            Me.lblCurrentImage.Text = Master.eLang.GetString(100, "Current Image:", False)
        End Sub

        Public Function ShowDialog(ByVal ShowID As Integer, ByVal Type As TVImageType, ByVal ScrapeType As ScrapeType, ByVal WithCurrent As Boolean) As DialogResult
            Me._id = ShowID
            Me._type = Type
            Me._withcurrent = WithCurrent
            Me._ScrapeType = ScrapeType
            Return MyBase.ShowDialog
        End Function

        Public Function ShowDialog(ByVal ShowID As Integer, ByVal Type As TVImageType, ByVal Season As Integer, ByVal CurrentImage As Image) As Image
            Me._id = ShowID
            Me._type = Type
            Me._season = Season
            Me.pbCurrent.Image = CurrentImage
            If (MyBase.ShowDialog = DialogResult.OK) Then
                Return Me.pbCurrent.Image
            End If
            Return Nothing
        End Function

        Private Sub tvList_AfterSelect(ByVal sender As Object, ByVal e As TreeViewEventArgs)
            Dim iIndex As Integer = 0
            Try 
                Me.ClearImages
                If (Not Information.IsNothing(RuntimeHelpers.GetObjectValue(e.Node.Tag)) AndAlso Not String.IsNullOrEmpty(e.Node.Tag.ToString)) Then
                    Dim tag As ImageTag
                    Dim tag2 As ImageTag
                    Me.pbCurrent.Visible = True
                    Me.lblCurrentImage.Visible = True
                    If (e.Node.Tag.ToString = "showp") Then
                        Me.SelSeason = -999
                        Me.SelIsPoster = True
                        If ((Not Information.IsNothing(Scraper.TVDBImages.ShowPoster) AndAlso Not Information.IsNothing(Scraper.TVDBImages.ShowPoster.Image)) AndAlso Not Information.IsNothing(Scraper.TVDBImages.ShowPoster.Image.Image)) Then
                            Me.pbCurrent.Image = Scraper.TVDBImages.ShowPoster.Image.Image
                        Else
                            Me.pbCurrent.Image = Nothing
                        End If
                        iIndex = Me.ShowPosterList.Count
                        Dim num8 As Integer = (iIndex - 1)
                        Dim i As Integer = 0
                        Do While (i <= num8)
                            If ((Not Information.IsNothing(Me.ShowPosterList.Item(i)) AndAlso Not Information.IsNothing(Me.ShowPosterList.Item(i).Image)) AndAlso Not Information.IsNothing(Me.ShowPosterList.Item(i).Image.Image)) Then
                                tag = New ImageTag
                                tag2 = tag
                                tag2.URL = Me.ShowPosterList.Item(i).URL
                                tag2.Path = Me.ShowPosterList.Item(i).LocalFile
                                tag2.isFanart = False
                                Me.AddImage(Me.ShowPosterList.Item(i).Image.Image, String.Format("{0}x{1}", Me.ShowPosterList.Item(i).Image.Image.Width, Me.ShowPosterList.Item(i).Image.Image.Height), i, tag2)
                            End If
                            i += 1
                        Loop
                        Dim num9 As Integer = (Me.GenericPosterList.Count - 1)
                        Dim j As Integer = 0
                        Do While (j <= num9)
                            If ((Not Information.IsNothing(Me.GenericPosterList.Item(j)) AndAlso Not Information.IsNothing(Me.GenericPosterList.Item(j).Image)) AndAlso Not Information.IsNothing(Me.GenericPosterList.Item(j).Image.Image)) Then
                                tag2 = New ImageTag
                                tag = tag2
                                tag.URL = Me.GenericPosterList.Item(j).URL
                                tag.Path = Me.GenericPosterList.Item(j).LocalFile
                                tag.isFanart = False
                                Me.AddImage(Me.GenericPosterList.Item(j).Image.Image, String.Format("{0}x{1}", Me.GenericPosterList.Item(j).Image.Image.Width, Me.GenericPosterList.Item(j).Image.Image.Height), (j + iIndex), tag)
                            End If
                            j += 1
                        Loop
                    ElseIf (e.Node.Tag.ToString = "showf") Then
                        Me.SelSeason = -999
                        Me.SelIsPoster = False
                        If ((Not Information.IsNothing(Scraper.TVDBImages.ShowFanart) AndAlso Not Information.IsNothing(Scraper.TVDBImages.ShowFanart.Image)) AndAlso Not Information.IsNothing(Scraper.TVDBImages.ShowFanart.Image.Image)) Then
                            Me.pbCurrent.Image = Scraper.TVDBImages.ShowFanart.Image.Image
                        Else
                            Me.pbCurrent.Image = Nothing
                        End If
                        Dim num10 As Integer = (Me.FanartList.Count - 1)
                        Dim k As Integer = 0
                        Do While (k <= num10)
                            If ((Not Information.IsNothing(Me.FanartList.Item(k)) AndAlso Not Information.IsNothing(Me.FanartList.Item(k).Image)) AndAlso Not Information.IsNothing(Me.FanartList.Item(k).Image.Image)) Then
                                tag2 = New ImageTag
                                tag = tag2
                                tag.URL = Me.FanartList.Item(k).URL
                                tag.Path = Me.FanartList.Item(k).LocalFile
                                tag.isFanart = True
                                Me.AddImage(Me.FanartList.Item(k).Image.Image, String.Format("{0}x{1}", Me.FanartList.Item(k).Size.Width, Me.FanartList.Item(k).Size.Height), k, tag)
                            End If
                            k += 1
                        Loop
                    ElseIf (e.Node.Tag.ToString = "allp") Then
                        Me.SelSeason = &H3E7
                        Me.SelIsPoster = True
                        If ((Not Information.IsNothing(Scraper.TVDBImages.AllSeasonPoster) AndAlso Not Information.IsNothing(Scraper.TVDBImages.AllSeasonPoster.Image)) AndAlso Not Information.IsNothing(Scraper.TVDBImages.AllSeasonPoster.Image.Image)) Then
                            Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonPoster.Image.Image
                        Else
                            Me.pbCurrent.Image = Nothing
                        End If
                        iIndex = Me.GenericPosterList.Count
                        Dim num11 As Integer = (iIndex - 1)
                        Dim m As Integer = 0
                        Do While (m <= num11)
                            If ((Not Information.IsNothing(Me.GenericPosterList.Item(m)) AndAlso Not Information.IsNothing(Me.GenericPosterList.Item(m).Image)) AndAlso Not Information.IsNothing(Me.GenericPosterList.Item(m).Image.Image)) Then
                                tag2 = New ImageTag
                                tag = tag2
                                tag.URL = Me.GenericPosterList.Item(m).URL
                                tag.Path = Me.GenericPosterList.Item(m).LocalFile
                                tag.isFanart = False
                                Me.AddImage(Me.GenericPosterList.Item(m).Image.Image, String.Format("{0}x{1}", Me.GenericPosterList.Item(m).Image.Image.Width, Me.GenericPosterList.Item(m).Image.Image.Height), m, tag)
                            End If
                            m += 1
                        Loop
                        Dim num12 As Integer = (Me.ShowPosterList.Count - 1)
                        Dim n As Integer = 0
                        Do While (n <= num12)
                            If ((Not Information.IsNothing(Me.ShowPosterList.Item(n)) AndAlso Not Information.IsNothing(Me.ShowPosterList.Item(n).Image)) AndAlso Not Information.IsNothing(Me.ShowPosterList.Item(n).Image.Image)) Then
                                tag2 = New ImageTag
                                tag = tag2
                                tag.URL = Me.ShowPosterList.Item(n).URL
                                tag.Path = Me.ShowPosterList.Item(n).LocalFile
                                tag.isFanart = False
                                Me.AddImage(Me.ShowPosterList.Item(n).Image.Image, String.Format("{0}x{1}", Me.ShowPosterList.Item(n).Image.Image.Width, Me.ShowPosterList.Item(n).Image.Image.Height), (n + iIndex), tag)
                            End If
                            n += 1
                        Loop
                    Else
                        Dim e$__ As New _Closure$__23 With { _
                            .$VB$Local_tMatch = Regex.Match(e.Node.Tag.ToString, "(?<type>f|p)(?<num>[0-9]+)") _
                        }
                        If e$__.$VB$Local_tMatch.Success Then
                            If (e$__.$VB$Local_tMatch.Groups.Item("type").Value = "f") Then
                                Me.SelSeason = Convert.ToInt32(e$__.$VB$Local_tMatch.Groups.Item("num").Value)
                                Me.SelIsPoster = False
                                Dim expression As TVDBSeasonImage = Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Of TVDBSeasonImage)(New Func(Of TVDBSeasonImage, Boolean)(AddressOf e$__._Lambda$__169))
                                If ((Not Information.IsNothing(expression) AndAlso Not Information.IsNothing(expression.Fanart)) AndAlso (Not Information.IsNothing(expression.Fanart.Image) AndAlso Not Information.IsNothing(expression.Fanart.Image.Image))) Then
                                    Me.pbCurrent.Image = expression.Fanart.Image.Image
                                Else
                                    Me.pbCurrent.Image = Nothing
                                End If
                                Dim num13 As Integer = (Me.FanartList.Count - 1)
                                Dim num7 As Integer = 0
                                Do While (num7 <= num13)
                                    If ((Not Information.IsNothing(Me.FanartList.Item(num7)) AndAlso Not Information.IsNothing(Me.FanartList.Item(num7).Image)) AndAlso Not Information.IsNothing(Me.FanartList.Item(num7).Image.Image)) Then
                                        tag2 = New ImageTag
                                        tag = tag2
                                        tag.URL = Me.FanartList.Item(num7).URL
                                        tag.Path = Me.FanartList.Item(num7).LocalFile
                                        tag.isFanart = True
                                        Me.AddImage(Me.FanartList.Item(num7).Image.Image, String.Format("{0}x{1}", Me.FanartList.Item(num7).Size.Width, Me.FanartList.Item(num7).Size.Height), num7, tag)
                                    End If
                                    num7 += 1
                                Loop
                            ElseIf (e$__.$VB$Local_tMatch.Groups.Item("type").Value = "p") Then
                                Me.SelSeason = Convert.ToInt32(e$__.$VB$Local_tMatch.Groups.Item("num").Value)
                                Me.SelIsPoster = True
                                Dim image2 As TVDBSeasonImage = Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Of TVDBSeasonImage)(New Func(Of TVDBSeasonImage, Boolean)(AddressOf Me._Lambda$__170))
                                If ((Not Information.IsNothing(image2) AndAlso Not Information.IsNothing(image2.Poster)) AndAlso Not Information.IsNothing(image2.Poster.Image)) Then
                                    Me.pbCurrent.Image = image2.Poster.Image
                                Else
                                    Me.pbCurrent.Image = Nothing
                                End If
                                iIndex = 0
                                Dim poster As TVDBSeasonPoster
                                For Each poster In Me.SeasonList.Where(Of TVDBSeasonPoster)(New Func(Of TVDBSeasonPoster, Boolean)(AddressOf e$__._Lambda$__171))
                                    If (Not Information.IsNothing(poster.Image) AndAlso Not Information.IsNothing(poster.Image.Image)) Then
                                        tag2 = New ImageTag
                                        tag = tag2
                                        tag.URL = poster.URL
                                        tag.Path = poster.LocalFile
                                        tag.isFanart = False
                                        Me.AddImage(poster.Image.Image, String.Format("{0}x{1}", poster.Image.Image.Width, poster.Image.Image.Height), iIndex, tag)
                                    End If
                                    iIndex += 1
                                Next
                            End If
                        End If
                    End If
                Else
                    Me.pbCurrent.Image = Nothing
                    Me.pbCurrent.Visible = False
                    Me.lblCurrentImage.Visible = False
                End If
                Me.CheckCurrentImage
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub


        ' Properties
        Friend Overridable Property btnCancel As Button
            <DebuggerNonUserCode> _
            Get
                Return Me._btnCancel
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Button)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.btnCancel_Click)
                If (Not Me._btnCancel Is Nothing) Then
                    RemoveHandler Me._btnCancel.Click, handler
                End If
                Me._btnCancel = WithEventsValue
                If (Not Me._btnCancel Is Nothing) Then
                    AddHandler Me._btnCancel.Click, handler
                End If
            End Set
        End Property

        Friend Overridable Property btnOK As Button
            <DebuggerNonUserCode> _
            Get
                Return Me._btnOK
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Button)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.btnOK_Click)
                If (Not Me._btnOK Is Nothing) Then
                    RemoveHandler Me._btnOK.Click, handler
                End If
                Me._btnOK = WithEventsValue
                If (Not Me._btnOK Is Nothing) Then
                    AddHandler Me._btnOK.Click, handler
                End If
            End Set
        End Property

        Friend Overridable Property bwDownloadFanart As BackgroundWorker
            <DebuggerNonUserCode> _
            Get
                Return Me._bwDownloadFanart
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As BackgroundWorker)
                Me._bwDownloadFanart = WithEventsValue
            End Set
        End Property

        Friend Overridable Property bwLoadData As BackgroundWorker
            <DebuggerNonUserCode> _
            Get
                Return Me._bwLoadData
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As BackgroundWorker)
                Dim handler As RunWorkerCompletedEventHandler = New RunWorkerCompletedEventHandler(AddressOf Me.bwLoadData_RunWorkerCompleted)
                Dim handler2 As ProgressChangedEventHandler = New ProgressChangedEventHandler(AddressOf Me.bwLoadData_ProgressChanged)
                Dim handler3 As DoWorkEventHandler = New DoWorkEventHandler(AddressOf Me.bwLoadData_DoWork)
                If (Not Me._bwLoadData Is Nothing) Then
                    RemoveHandler Me._bwLoadData.RunWorkerCompleted, handler
                    RemoveHandler Me._bwLoadData.ProgressChanged, handler2
                    RemoveHandler Me._bwLoadData.DoWork, handler3
                End If
                Me._bwLoadData = WithEventsValue
                If (Not Me._bwLoadData Is Nothing) Then
                    AddHandler Me._bwLoadData.RunWorkerCompleted, handler
                    AddHandler Me._bwLoadData.ProgressChanged, handler2
                    AddHandler Me._bwLoadData.DoWork, handler3
                End If
            End Set
        End Property

        Friend Overridable Property bwLoadImages As BackgroundWorker
            <DebuggerNonUserCode> _
            Get
                Return Me._bwLoadImages
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As BackgroundWorker)
                Dim handler As RunWorkerCompletedEventHandler = New RunWorkerCompletedEventHandler(AddressOf Me.bwLoadImages_RunWorkerCompleted)
                Dim handler2 As ProgressChangedEventHandler = New ProgressChangedEventHandler(AddressOf Me.bwLoadImages_ProgressChanged)
                Dim handler3 As DoWorkEventHandler = New DoWorkEventHandler(AddressOf Me.bwLoadImages_DoWork)
                If (Not Me._bwLoadImages Is Nothing) Then
                    RemoveHandler Me._bwLoadImages.RunWorkerCompleted, handler
                    RemoveHandler Me._bwLoadImages.ProgressChanged, handler2
                    RemoveHandler Me._bwLoadImages.DoWork, handler3
                End If
                Me._bwLoadImages = WithEventsValue
                If (Not Me._bwLoadImages Is Nothing) Then
                    AddHandler Me._bwLoadImages.RunWorkerCompleted, handler
                    AddHandler Me._bwLoadImages.ProgressChanged, handler2
                    AddHandler Me._bwLoadImages.DoWork, handler3
                End If
            End Set
        End Property

        Friend Overridable Property ImageList1 As ImageList
            <DebuggerNonUserCode> _
            Get
                Return Me._ImageList1
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As ImageList)
                Me._ImageList1 = WithEventsValue
            End Set
        End Property

        Friend Overridable Property lblCurrentImage As Label
            <DebuggerNonUserCode> _
            Get
                Return Me._lblCurrentImage
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Label)
                Me._lblCurrentImage = WithEventsValue
            End Set
        End Property

        Friend Overridable Property lblStatus As Label
            <DebuggerNonUserCode> _
            Get
                Return Me._lblStatus
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Label)
                Me._lblStatus = WithEventsValue
            End Set
        End Property

        Friend Overridable Property pbCurrent As PictureBox
            <DebuggerNonUserCode> _
            Get
                Return Me._pbCurrent
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As PictureBox)
                Me._pbCurrent = WithEventsValue
            End Set
        End Property

        Friend Overridable Property pbDelete As PictureBox
            <DebuggerNonUserCode> _
            Get
                Return Me._pbDelete
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As PictureBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.pbDelete_Click)
                If (Not Me._pbDelete Is Nothing) Then
                    RemoveHandler Me._pbDelete.Click, handler
                End If
                Me._pbDelete = WithEventsValue
                If (Not Me._pbDelete Is Nothing) Then
                    AddHandler Me._pbDelete.Click, handler
                End If
            End Set
        End Property

        Friend Overridable Property pbStatus As ProgressBar
            <DebuggerNonUserCode> _
            Get
                Return Me._pbStatus
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As ProgressBar)
                Me._pbStatus = WithEventsValue
            End Set
        End Property

        Friend Overridable Property pbUndo As PictureBox
            <DebuggerNonUserCode> _
            Get
                Return Me._pbUndo
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As PictureBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.pbUndo_Click)
                If (Not Me._pbUndo Is Nothing) Then
                    RemoveHandler Me._pbUndo.Click, handler
                End If
                Me._pbUndo = WithEventsValue
                If (Not Me._pbUndo Is Nothing) Then
                    AddHandler Me._pbUndo.Click, handler
                End If
            End Set
        End Property

        Friend Overridable Property pnlImages As Panel
            <DebuggerNonUserCode> _
            Get
                Return Me._pnlImages
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Panel)
                Me._pnlImages = WithEventsValue
            End Set
        End Property

        Friend Overridable Property pnlStatus As Panel
            <DebuggerNonUserCode> _
            Get
                Return Me._pnlStatus
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Panel)
                Me._pnlStatus = WithEventsValue
            End Set
        End Property

        Friend Overridable Property tvList As TreeView
            <DebuggerNonUserCode> _
            Get
                Return Me._tvList
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As TreeView)
                Dim handler As TreeViewEventHandler = New TreeViewEventHandler(AddressOf Me.tvList_AfterSelect)
                If (Not Me._tvList Is Nothing) Then
                    RemoveHandler Me._tvList.AfterSelect, handler
                End If
                Me._tvList = WithEventsValue
                If (Not Me._tvList Is Nothing) Then
                    AddHandler Me._tvList.AfterSelect, handler
                End If
            End Set
        End Property


        ' Fields
        Private Shared __ENCList As List(Of WeakReference) = New List(Of WeakReference)
        <AccessedThroughProperty("btnCancel")> _
        Private _btnCancel As Button
        <AccessedThroughProperty("btnOK")> _
        Private _btnOK As Button
        <AccessedThroughProperty("bwDownloadFanart")> _
        Private _bwDownloadFanart As BackgroundWorker
        <AccessedThroughProperty("bwLoadData")> _
        Private _bwLoadData As BackgroundWorker
        <AccessedThroughProperty("bwLoadImages")> _
        Private _bwLoadImages As BackgroundWorker
        Private _fanartchanged As Boolean
        Private _id As Integer
        <AccessedThroughProperty("ImageList1")> _
        Private _ImageList1 As ImageList
        <AccessedThroughProperty("lblCurrentImage")> _
        Private _lblCurrentImage As Label
        <AccessedThroughProperty("lblStatus")> _
        Private _lblStatus As Label
        <AccessedThroughProperty("pbCurrent")> _
        Private _pbCurrent As PictureBox
        <AccessedThroughProperty("pbDelete")> _
        Private _pbDelete As PictureBox
        <AccessedThroughProperty("pbStatus")> _
        Private _pbStatus As ProgressBar
        <AccessedThroughProperty("pbUndo")> _
        Private _pbUndo As PictureBox
        <AccessedThroughProperty("pnlImages")> _
        Private _pnlImages As Panel
        <AccessedThroughProperty("pnlStatus")> _
        Private _pnlStatus As Panel
        Private _ScrapeType As ScrapeType
        Private _season As Integer
        <AccessedThroughProperty("tvList")> _
        Private _tvList As TreeView
        Private _type As TVImageType
        Private _withcurrent As Boolean
        Private components As IContainer
        Private DefaultImages As TVImages
        Private FanartList As List(Of TVDBFanart)
        Private GenericPosterList As List(Of TVDBPoster)
        Private iCounter As Integer
        Private iLeft As Integer
        Private iTop As Integer
        Private lblImage As Label()
        Private pbImage As PictureBox()
        Private pnlImage As Panel()
        Private SeasonList As List(Of TVDBSeasonPoster)
        Private SelIsPoster As Boolean
        Private SelSeason As Integer
        Private ShowPosterList As List(Of TVDBShowPoster)

        ' Nested Types
        <CompilerGenerated> _
        Friend Class _Closure$__21
            ' Methods
            <DebuggerNonUserCode> _
            Public Sub New()
            End Sub

            <DebuggerNonUserCode> _
            Public Sub New(ByVal other As _Closure$__21)
                If (Not other Is Nothing) Then
                    Me.$VB$Local_iSeason = other.$VB$Local_iSeason
                End If
            End Sub

            <CompilerGenerated> _
            Public Function _Lambda$__157(ByVal p As TVDBSeasonPoster) As Boolean
                Return ((Not Information.IsNothing(p.Image.Image) AndAlso (p.Season = Me.$VB$Local_iSeason)) AndAlso ((p.Type = Master.eSettings.PreferredSeasonPosterSize) AndAlso (p.Language = Master.eSettings.TVDBLanguage)))
            End Function

            <CompilerGenerated> _
            Public Function _Lambda$__158(ByVal p As TVDBSeasonPoster) As Boolean
                Return ((Not Information.IsNothing(p.Image.Image) AndAlso (p.Season = Me.$VB$Local_iSeason)) AndAlso (p.Type = Master.eSettings.PreferredSeasonPosterSize))
            End Function

            <CompilerGenerated> _
            Public Function _Lambda$__159(ByVal p As TVDBSeasonPoster) As Boolean
                Return (Not Information.IsNothing(p.Image.Image) AndAlso (p.Season = Me.$VB$Local_iSeason))
            End Function


            ' Fields
            Public $VB$Local_iSeason As Integer
        End Class

        <CompilerGenerated> _
        Friend Class _Closure$__22
            ' Methods
            <DebuggerNonUserCode> _
            Public Sub New()
            End Sub

            <DebuggerNonUserCode> _
            Public Sub New(ByVal other As _Closure$__22)
                If (Not other Is Nothing) Then
                    Me.$VB$Local_iSeason = other.$VB$Local_iSeason
                End If
            End Sub

            <CompilerGenerated> _
            Public Function _Lambda$__160(ByVal s As TVDBSeasonImage) As Boolean
                Return (s.Season = Me.$VB$Local_iSeason)
            End Function

            <CompilerGenerated> _
            Public Function _Lambda$__161(ByVal s As TVDBSeasonImage) As Boolean
                Return (s.Season = Me.$VB$Local_iSeason)
            End Function


            ' Fields
            Public $VB$Local_iSeason As Integer
        End Class

        <CompilerGenerated> _
        Friend Class _Closure$__23
            ' Methods
            <DebuggerNonUserCode> _
            Public Sub New()
            End Sub

            <DebuggerNonUserCode> _
            Public Sub New(ByVal other As _Closure$__23)
                If (Not other Is Nothing) Then
                    Me.$VB$Local_tMatch = other.$VB$Local_tMatch
                End If
            End Sub

            <CompilerGenerated> _
            Public Function _Lambda$__169(ByVal f As TVDBSeasonImage) As Boolean
                Return (f.Season = Convert.ToInt32(Me.$VB$Local_tMatch.Groups.Item("num").Value))
            End Function

            <CompilerGenerated> _
            Public Function _Lambda$__171(ByVal s As TVDBSeasonPoster) As Boolean
                Return (s.Season = Convert.ToInt32(Me.$VB$Local_tMatch.Groups.Item("num").Value))
            End Function


            ' Fields
            Public $VB$Local_tMatch As Match
        End Class

        <StructLayout(LayoutKind.Sequential)> _
        Private Structure ImageTag
            Public isFanart As Boolean
            Public Path As String
            Public URL As String
        End Structure
    End Class
End Namespace

