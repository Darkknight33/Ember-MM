Imports EmberAPI
Imports EmberAPI.FileUtils
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Drawing
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Windows.Forms

Namespace EmberScraperModule
    <DesignerGenerated> _
    Public Class dlgTrailer
        Inherits Form
        ' Methods
        Public Sub New()
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.dlgTrailer_Load)
            AddHandler MyBase.Shown, New EventHandler(AddressOf Me.dlgTrailer_Shown)
            dlgTrailer.__ENCAddToList(Me)
            Me.bwCompileList = New BackgroundWorker
            Me.bwDownloadTrailer = New BackgroundWorker
            Me.cTrailer = New Trailers
            Me.imdbID = String.Empty
            Me.prePath = String.Empty
            Me.sPath = String.Empty
            Me.tArray = New List(Of String)
            Me.tURL = String.Empty
            Me.InitializeComponent
        End Sub

        <DebuggerNonUserCode> _
        Private Shared Sub __ENCAddToList(ByVal value As Object)
            Dim list As List(Of WeakReference) = dlgTrailer.__ENCList
            SyncLock list
                If (dlgTrailer.__ENCList.Count = dlgTrailer.__ENCList.Capacity) Then
                    Dim index As Integer = 0
                    Dim num3 As Integer = (dlgTrailer.__ENCList.Count - 1)
                    Dim i As Integer = 0
                    Do While (i <= num3)
                        Dim reference As WeakReference = dlgTrailer.__ENCList.Item(i)
                        If reference.IsAlive Then
                            If (i <> index) Then
                                dlgTrailer.__ENCList.Item(index) = dlgTrailer.__ENCList.Item(i)
                            End If
                            index += 1
                        End If
                        i += 1
                    Loop
                    dlgTrailer.__ENCList.RemoveRange(index, (dlgTrailer.__ENCList.Count - index))
                    dlgTrailer.__ENCList.Capacity = dlgTrailer.__ENCList.Count
                End If
                dlgTrailer.__ENCList.Add(New WeakReference(RuntimeHelpers.GetObjectValue(value)))
            End SyncLock
        End Sub

        Private Sub BeginDownload(ByVal CloseDialog As Boolean)
            Dim flag As Boolean = False
            Me.OK_Button.Enabled = False
            Me.btnSetNfo.Enabled = False
            Me.btnPlayTrailer.Enabled = False
            Me.lbTrailers.Enabled = False
            Me.txtYouTube.Enabled = False
            Me.txtManual.Enabled = False
            Me.btnBrowse.Enabled = False
            Me.lblStatus.Text = Master.eLang.GetString(&H38, "Downloading selected trailer...", False)
            Me.pbStatus.Style = ProgressBarStyle.Continuous
            Me.pbStatus.Value = 0
            Me.pnlStatus.Visible = True
            Application.DoEvents
            If (Not String.IsNullOrEmpty(Me.prePath) AndAlso File.Exists(Me.prePath)) Then
                If CloseDialog Then
                    Me.tURL = Path.Combine(Directory.GetParent(Me.sPath).FullName, Path.GetFileName(Me.prePath))
                    Common.MoveFileWithStream(Me.prePath, Me.tURL)
                    File.Delete(Me.prePath)
                    Me.DialogResult = DialogResult.OK
                    Me.Close
                Else
                    Process.Start(("""" & Me.prePath & """"))
                    flag = True
                End If
            ElseIf (Me.txtManual.Text.Length > 0) Then
                Me.lblStatus.Text = Master.eLang.GetString(&H39, "Copying specified file to trailer...", False)
                If (Master.eSettings.ValidExts.Contains(Path.GetExtension(Me.txtManual.Text)) AndAlso File.Exists(Me.txtManual.Text)) Then
                    If CloseDialog Then
                        Me.tURL = Path.Combine(Directory.GetParent(Me.sPath).FullName, (Path.GetFileNameWithoutExtension(Me.sPath) & If(Master.eSettings.DashTrailer, "-trailer", "[trailer]") & Path.GetExtension(Me.txtManual.Text)))
                        Common.MoveFileWithStream(Me.txtManual.Text, Me.tURL)
                        Me.DialogResult = DialogResult.OK
                        Me.Close
                    Else
                        Process.Start(("""" & Me.txtManual.Text & """"))
                        flag = True
                    End If
                Else
                    Interaction.MsgBox(Master.eLang.GetString(&HC0, "File is not valid.", True), MsgBoxStyle.Exclamation, Master.eLang.GetString(&HC2, "Not Valid", True))
                    flag = True
                End If
            ElseIf Regex.IsMatch(Me.txtYouTube.Text, "http:\/\/.*youtube.*\/watch\?v=(.{11})&?.*") Then
                Using format As dlgTrailerFormat = New dlgTrailerFormat
                    Dim str As String = format.ShowDialog(Me.txtYouTube.Text)
                    If Not String.IsNullOrEmpty(str) Then
                        Me.bwDownloadTrailer = New BackgroundWorker
                        Me.bwDownloadTrailer.WorkerReportsProgress = True
                        Me.bwDownloadTrailer.WorkerSupportsCancellation = True
                        Dim argument As New Arguments With { _
                            .Parameter = str, _
                            .bType = CloseDialog _
                        }
                        Me.bwDownloadTrailer.RunWorkerAsync(argument)
                    Else
                        flag = True
                    End If
                End Using
            Else
                Dim arguments As Arguments
                If StringUtils.isValidURL(Me.txtYouTube.Text) Then
                    Me.bwDownloadTrailer = New BackgroundWorker
                    Me.bwDownloadTrailer.WorkerReportsProgress = True
                    Me.bwDownloadTrailer.WorkerSupportsCancellation = True
                    arguments = New Arguments With { _
                        .Parameter = Me.txtYouTube.Text, _
                        .bType = CloseDialog _
                    }
                    Me.bwDownloadTrailer.RunWorkerAsync(arguments)
                ElseIf Regex.IsMatch(Me.lbTrailers.SelectedItem.ToString, "http:\/\/.*youtube.*\/watch\?v=(.{11})&?.*") Then
                    Using format2 As dlgTrailerFormat = New dlgTrailerFormat
                        Dim str2 As String = format2.ShowDialog(Me.lbTrailers.SelectedItem.ToString)
                        If Not String.IsNullOrEmpty(str2) Then
                            Me.bwDownloadTrailer = New BackgroundWorker
                            Me.bwDownloadTrailer.WorkerReportsProgress = True
                            Me.bwDownloadTrailer.WorkerSupportsCancellation = True
                            arguments = New Arguments With { _
                                .Parameter = str2, _
                                .bType = CloseDialog _
                            }
                            Me.bwDownloadTrailer.RunWorkerAsync(arguments)
                        Else
                            flag = True
                        End If
                    End Using
                Else
                    Me.bwDownloadTrailer = New BackgroundWorker
                    Me.bwDownloadTrailer.WorkerReportsProgress = True
                    Me.bwDownloadTrailer.WorkerSupportsCancellation = True
                    arguments = New Arguments With { _
                        .Parameter = Me.lbTrailers.SelectedItem.ToString, _
                        .bType = CloseDialog _
                    }
                    Me.bwDownloadTrailer.RunWorkerAsync(arguments)
                End If
            End If
            If flag Then
                Me.pnlStatus.Visible = False
                Me.lbTrailers.Enabled = True
                Me.txtYouTube.Enabled = True
                Me.txtManual.Enabled = True
                Me.btnBrowse.Enabled = True
                Me.SetEnabled(False)
            End If
        End Sub

        Private Sub btnBrowse_Click(ByVal sender As Object, ByVal e As EventArgs)
            Try 
                Dim ofdTrailer As OpenFileDialog = Me.ofdTrailer
                ofdTrailer.InitialDirectory = Directory.GetParent(Master.currMovie.Filename).FullName
                ofdTrailer.Filter = ("Supported Trailer Formats|*" & Functions.ListToStringWithSeparator(Of String)(Master.eSettings.ValidExts.ToArray, ";*"))
                ofdTrailer.FilterIndex = 0
                ofdTrailer = Nothing
                If (Me.ofdTrailer.ShowDialog = DialogResult.OK) Then
                    Me.txtManual.Text = Me.ofdTrailer.FileName
                End If
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Private Sub btnGetTrailers_Click(ByVal sender As Object, ByVal e As EventArgs)
            Me.OK_Button.Enabled = False
            Me.btnSetNfo.Enabled = False
            Me.btnPlayTrailer.Enabled = False
            Me.lbTrailers.Enabled = False
            Me.txtYouTube.Enabled = False
            Me.txtManual.Enabled = False
            Me.btnBrowse.Enabled = False
            Me.pnlStatus.Visible = True
            Me.bwCompileList = New BackgroundWorker
            Me.bwCompileList.WorkerSupportsCancellation = True
            Me.bwCompileList.RunWorkerAsync
        End Sub

        Private Sub btnPlayTrailer_Click(ByVal sender As Object, ByVal e As EventArgs)
            Try 
                Me.BeginDownload(False)
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Interaction.MsgBox(Master.eLang.GetString(&H3A, "The trailer could not be played. This could be due to an invalid URI or you do not have the proper player to play the trailer type.", False), MsgBoxStyle.Critical, Master.eLang.GetString(&H3B, "Error Playing Trailer", False))
                Me.pnlStatus.Visible = False
                Me.lbTrailers.Enabled = True
                Me.txtYouTube.Enabled = True
                Me.txtManual.Enabled = True
                Me.btnBrowse.Enabled = True
                Me.SetEnabled(False)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Private Sub btnSetNfo_Click(ByVal sender As Object, ByVal e As EventArgs)
            If (Me.btnSetNfo.Text = Master.eLang.GetString(60, "Move", False)) Then
                If (Master.eSettings.ValidExts.Contains(Path.GetExtension(Me.txtManual.Text)) AndAlso File.Exists(Me.txtManual.Text)) Then
                    Me.OK_Button.Enabled = False
                    Me.btnSetNfo.Enabled = False
                    Me.btnPlayTrailer.Enabled = False
                    Me.lbTrailers.Enabled = False
                    Me.txtYouTube.Enabled = False
                    Me.txtManual.Enabled = False
                    Me.btnBrowse.Enabled = False
                    Me.lblStatus.Text = Master.eLang.GetString(&H3E, "Moving specified file to trailer...", False)
                    Me.pbStatus.Style = ProgressBarStyle.Continuous
                    Me.pbStatus.Value = 0
                    Me.pnlStatus.Visible = True
                    Application.DoEvents
                    Me.tURL = Path.Combine(Directory.GetParent(Me.sPath).FullName, (Path.GetFileNameWithoutExtension(Me.sPath) & If(Master.eSettings.DashTrailer, "-trailer", "[trailer]") & Path.GetExtension(Me.txtManual.Text)))
                    File.Move(Me.txtManual.Text, Me.tURL)
                    Me.DialogResult = DialogResult.OK
                    Me.Close
                Else
                    Interaction.MsgBox(Master.eLang.GetString(&HC0, "File is not valid.", True), MsgBoxStyle.Exclamation, Master.eLang.GetString(&HC2, "Not Valid", True))
                    Me.pnlStatus.Visible = False
                    Me.lbTrailers.Enabled = True
                    Me.txtYouTube.Enabled = True
                    Me.txtManual.Enabled = True
                    Me.btnBrowse.Enabled = True
                    Me.SetEnabled(False)
                End If
            Else
                Dim flag As Boolean = False
                If StringUtils.isValidURL(Me.txtYouTube.Text) Then
                    Me.tURL = Me.txtYouTube.Text
                ElseIf (Me.lbTrailers.SelectedItems.Count > 0) Then
                    Me.tURL = Me.lbTrailers.SelectedItem.ToString
                Else
                    flag = True
                End If
                If Not flag Then
                    Me.DialogResult = DialogResult.OK
                    Me.Close
                End If
            End If
        End Sub

        Private Sub bwCompileList_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
            Try 
                Me.tArray = Me.cTrailer.GetTrailers(Me.imdbID, False)
                If Me.bwCompileList.CancellationPending Then
                    e.Cancel = True
                End If
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Private Sub bwCompileList_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
            If Not e.Cancelled Then
                If (Me.tArray.Count > 0) Then
                    Dim str As String
                    For Each str In Me.tArray
                        Me.lbTrailers.Items.Add(str)
                    Next
                    Me.btnGetTrailers.Visible = False
                Else
                    Me.btnGetTrailers.Enabled = False
                End If
            End If
            Me.pnlStatus.Visible = False
            Me.lbTrailers.Enabled = True
            Me.txtYouTube.Enabled = True
            Me.txtManual.Enabled = True
            Me.btnBrowse.Enabled = True
            Me.SetEnabled(False)
        End Sub

        Private Sub bwDownloadTrailer_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
            Dim argument As Arguments = DirectCast(e.Argument, Arguments)
            Try 
                If argument.bType Then
                    Me.tURL = Me.cTrailer.DownloadTrailer(Me.sPath, argument.Parameter)
                Else
                    Me.prePath = Me.cTrailer.DownloadTrailer(Path.Combine(Master.TempPath, Path.GetFileName(Me.sPath)), argument.Parameter)
                End If
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                ProjectData.ClearProjectError
            End Try
            e.Result = argument.bType
            If Me.bwDownloadTrailer.CancellationPending Then
                e.Cancel = True
            End If
        End Sub

        Private Sub bwDownloadTrailer_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
            Me.pbStatus.Value = e.ProgressPercentage
        End Sub

        Private Sub bwDownloadTrailer_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
            If Not e.Cancelled Then
                If Convert.ToBoolean(RuntimeHelpers.GetObjectValue(e.Result)) Then
                    Me.DialogResult = DialogResult.OK
                    Me.Close
                Else
                    Me.pnlStatus.Visible = False
                    Me.lbTrailers.Enabled = True
                    Me.txtYouTube.Enabled = True
                    Me.txtManual.Enabled = True
                    Me.btnBrowse.Enabled = True
                    Me.SetEnabled(False)
                    If Not String.IsNullOrEmpty(Me.prePath) Then
                        Process.Start(("""" & Me.prePath & """"))
                    End If
                End If
            End If
        End Sub

        Private Sub Cancel_Button_Click(ByVal sender As Object, ByVal e As EventArgs)
            Me.cTrailer.Cancel
            If Me.bwCompileList.IsBusy Then
                Me.bwCompileList.CancelAsync
            End If
            If Me.bwDownloadTrailer.IsBusy Then
                Me.bwDownloadTrailer.CancelAsync
            End If
            Do While (Me.bwCompileList.IsBusy OrElse Me.bwDownloadTrailer.IsBusy)
                Application.DoEvents
                Thread.Sleep(50)
            Loop
            Me.DialogResult = DialogResult.Cancel
            Me.Close
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

        Private Sub dlgTrailer_Load(ByVal sender As Object, ByVal e As EventArgs)
            Me.SetUp
            Me.cTrailer.IMDBURL = Me.IMDBURL
            AddHandler Me.cTrailer.ProgressUpdated, New ProgressUpdatedEventHandler(AddressOf Me.DownloadProgressUpdated)
        End Sub

        Private Sub dlgTrailer_Shown(ByVal sender As Object, ByVal e As EventArgs)
            Me.Activate
        End Sub

        Private Sub DownloadProgressUpdated(ByVal iProgress As Integer)
            Me.bwDownloadTrailer.ReportProgress(iProgress)
        End Sub

        Protected Overrides Sub Finalize()
            Me.cTrailer = Nothing
            MyBase.Finalize
        End Sub

        <DebuggerStepThrough> _
        Private Sub InitializeComponent()
            Dim manager As New ComponentResourceManager(GetType(dlgTrailer))
            Me.OK_Button = New Button
            Me.Cancel_Button = New Button
            Me.lbTrailers = New ListBox
            Me.GroupBox1 = New GroupBox
            Me.pnlStatus = New Panel
            Me.lblStatus = New Label
            Me.pbStatus = New ProgressBar
            Me.btnGetTrailers = New Button
            Me.GroupBox2 = New GroupBox
            Me.btnBrowse = New Button
            Me.txtManual = New TextBox
            Me.Label2 = New Label
            Me.txtYouTube = New TextBox
            Me.Label1 = New Label
            Me.btnPlayTrailer = New Button
            Me.btnSetNfo = New Button
            Me.ofdTrailer = New OpenFileDialog
            Me.Panel1 = New Panel
            Me.GroupBox1.SuspendLayout
            Me.pnlStatus.SuspendLayout
            Me.GroupBox2.SuspendLayout
            Me.Panel1.SuspendLayout
            Me.SuspendLayout
            Me.OK_Button.Enabled = False
            Dim point2 As New Point(290, &H153)
            Me.OK_Button.Location = point2
            Me.OK_Button.Name = "OK_Button"
            Dim size2 As New Size(&H4A, &H17)
            Me.OK_Button.Size = size2
            Me.OK_Button.TabIndex = 0
            Me.OK_Button.Text = "Download"
            Me.Cancel_Button.DialogResult = DialogResult.Cancel
            point2 = New Point(&H171, &H153)
            Me.Cancel_Button.Location = point2
            Me.Cancel_Button.Name = "Cancel_Button"
            size2 = New Size(&H43, &H17)
            Me.Cancel_Button.Size = size2
            Me.Cancel_Button.TabIndex = 1
            Me.Cancel_Button.Text = "Cancel"
            Me.lbTrailers.Enabled = False
            Me.lbTrailers.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            Me.lbTrailers.FormattingEnabled = True
            Me.lbTrailers.HorizontalScrollbar = True
            point2 = New Point(6, &H13)
            Me.lbTrailers.Location = point2
            Me.lbTrailers.Name = "lbTrailers"
            size2 = New Size(&H19B, &HAD)
            Me.lbTrailers.Size = size2
            Me.lbTrailers.TabIndex = 1
            Me.GroupBox1.Controls.Add(Me.pnlStatus)
            Me.GroupBox1.Controls.Add(Me.btnGetTrailers)
            Me.GroupBox1.Controls.Add(Me.GroupBox2)
            Me.GroupBox1.Controls.Add(Me.lbTrailers)
            Me.GroupBox1.Font = New Font("Segoe UI", 8.25!, FontStyle.Bold, GraphicsUnit.Point, 0)
            point2 = New Point(9, 9)
            Me.GroupBox1.Location = point2
            Me.GroupBox1.Name = "GroupBox1"
            size2 = New Size(&H1A8, &H13F)
            Me.GroupBox1.Size = size2
            Me.GroupBox1.TabIndex = 2
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Select Trailer to Scrape"
            Me.pnlStatus.BackColor = Color.White
            Me.pnlStatus.BorderStyle = BorderStyle.FixedSingle
            Me.pnlStatus.Controls.Add(Me.lblStatus)
            Me.pnlStatus.Controls.Add(Me.pbStatus)
            point2 = New Point(&H70, &H52)
            Me.pnlStatus.Location = point2
            Me.pnlStatus.Name = "pnlStatus"
            size2 = New Size(200, &H36)
            Me.pnlStatus.Size = size2
            Me.pnlStatus.TabIndex = &H45
            Me.pnlStatus.Visible = False
            Me.lblStatus.AutoSize = True
            Me.lblStatus.Font = New Font("Segoe UI", 8.25!, FontStyle.Bold, GraphicsUnit.Point, 0)
            point2 = New Point(3, 10)
            Me.lblStatus.Location = point2
            Me.lblStatus.Name = "lblStatus"
            size2 = New Size(&H79, 13)
            Me.lblStatus.Size = size2
            Me.lblStatus.TabIndex = 1
            Me.lblStatus.Text = "Compiling trailer list..."
            Me.lblStatus.TextAlign = ContentAlignment.MiddleLeft
            point2 = New Point(3, &H20)
            Me.pbStatus.Location = point2
            Me.pbStatus.MarqueeAnimationSpeed = &H19
            Me.pbStatus.Name = "pbStatus"
            size2 = New Size(&HC0, &H11)
            Me.pbStatus.Size = size2
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pbStatus.TabIndex = 0
            Me.btnGetTrailers.Font = New Font("Microsoft Sans Serif", 9.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
            Me.btnGetTrailers.Image = DirectCast(manager.GetObject("btnGetTrailers.Image"), Image)
            Me.btnGetTrailers.ImageAlign = ContentAlignment.MiddleLeft
            point2 = New Point(120, &H52)
            Me.btnGetTrailers.Location = point2
            Me.btnGetTrailers.Name = "btnGetTrailers"
            size2 = New Size(&HB8, &H17)
            Me.btnGetTrailers.Size = size2
            Me.btnGetTrailers.TabIndex = &H47
            Me.btnGetTrailers.Text = "Download Trailer List"
            Me.btnGetTrailers.TextAlign = ContentAlignment.MiddleRight
            Me.btnGetTrailers.UseVisualStyleBackColor = True
            Me.GroupBox2.Controls.Add(Me.btnBrowse)
            Me.GroupBox2.Controls.Add(Me.txtManual)
            Me.GroupBox2.Controls.Add(Me.Label2)
            Me.GroupBox2.Controls.Add(Me.txtYouTube)
            Me.GroupBox2.Controls.Add(Me.Label1)
            point2 = New Point(6, &HC9)
            Me.GroupBox2.Location = point2
            Me.GroupBox2.Name = "GroupBox2"
            size2 = New Size(&H19B, &H6F)
            Me.GroupBox2.Size = size2
            Me.GroupBox2.TabIndex = 70
            Me.GroupBox2.TabStop = False
            Me.GroupBox2.Text = "Manual Trailer Entry"
            point2 = New Point(&H178, &H52)
            Me.btnBrowse.Location = point2
            Me.btnBrowse.Name = "btnBrowse"
            size2 = New Size(&H19, &H17)
            Me.btnBrowse.Size = size2
            Me.btnBrowse.TabIndex = 5
            Me.btnBrowse.Text = "..."
            Me.btnBrowse.UseVisualStyleBackColor = True
            Me.txtManual.BorderStyle = BorderStyle.FixedSingle
            Me.txtManual.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(9, &H52)
            Me.txtManual.Location = point2
            Me.txtManual.Name = "txtManual"
            size2 = New Size(&H16D, &H16)
            Me.txtManual.Size = size2
            Me.txtManual.TabIndex = 4
            Me.Label2.AutoSize = True
            Me.Label2.Font = New Font("Segoe UI", 8.25!, FontStyle.Bold, GraphicsUnit.Point, 0)
            point2 = New Point(6, &H44)
            Me.Label2.Location = point2
            Me.Label2.Name = "Label2"
            size2 = New Size(&H48, 13)
            Me.Label2.Size = size2
            Me.Label2.TabIndex = 3
            Me.Label2.Text = "Local Trailer:"
            Me.txtYouTube.BorderStyle = BorderStyle.FixedSingle
            Me.txtYouTube.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(9, &H1C)
            Me.txtYouTube.Location = point2
            Me.txtYouTube.Name = "txtYouTube"
            size2 = New Size(&H188, &H16)
            Me.txtYouTube.Size = size2
            Me.txtYouTube.TabIndex = 1
            Me.Label1.AutoSize = True
            Me.Label1.Font = New Font("Segoe UI", 8.25!, FontStyle.Bold, GraphicsUnit.Point, 0)
            point2 = New Point(6, 14)
            Me.Label1.Location = point2
            Me.Label1.Name = "Label1"
            size2 = New Size(&H99, 13)
            Me.Label1.Size = size2
            Me.Label1.TabIndex = 0
            Me.Label1.Text = "Direct Link or YouTube URL:"
            Me.btnPlayTrailer.Enabled = False
            Me.btnPlayTrailer.Image = DirectCast(manager.GetObject("btnPlayTrailer.Image"), Image)
            Me.btnPlayTrailer.ImageAlign = ContentAlignment.MiddleLeft
            point2 = New Point(12, &H153)
            Me.btnPlayTrailer.Location = point2
            Me.btnPlayTrailer.Name = "btnPlayTrailer"
            size2 = New Size(&H6A, &H17)
            Me.btnPlayTrailer.Size = size2
            Me.btnPlayTrailer.TabIndex = &H6D
            Me.btnPlayTrailer.Text = "Preview Trailer"
            Me.btnPlayTrailer.TextAlign = ContentAlignment.MiddleRight
            Me.btnPlayTrailer.UseVisualStyleBackColor = True
            Me.btnSetNfo.Enabled = False
            point2 = New Point(&HCF, &H153)
            Me.btnSetNfo.Location = point2
            Me.btnSetNfo.Name = "btnSetNfo"
            size2 = New Size(&H4D, &H17)
            Me.btnSetNfo.Size = size2
            Me.btnSetNfo.TabIndex = 110
            Me.btnSetNfo.Text = "Set To Nfo"
            Me.Panel1.BackColor = Color.White
            Me.Panel1.Controls.Add(Me.GroupBox1)
            point2 = New Point(3, 3)
            Me.Panel1.Location = point2
            Me.Panel1.Name = "Panel1"
            size2 = New Size(&H1BC, &H14B)
            Me.Panel1.Size = size2
            Me.Panel1.TabIndex = &H6F
            Me.AcceptButton = Me.OK_Button
            Dim ef2 As New SizeF(96!, 96!)
            Me.AutoScaleDimensions = ef2
            Me.AutoScaleMode = AutoScaleMode.Dpi
            Me.CancelButton = Me.Cancel_Button
            size2 = New Size(450, &H16E)
            Me.ClientSize = size2
            Me.ControlBox = False
            Me.Controls.Add(Me.Panel1)
            Me.Controls.Add(Me.btnSetNfo)
            Me.Controls.Add(Me.Cancel_Button)
            Me.Controls.Add(Me.OK_Button)
            Me.Controls.Add(Me.btnPlayTrailer)
            Me.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            Me.FormBorderStyle = FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "dlgTrailer"
            Me.ShowIcon = False
            Me.StartPosition = FormStartPosition.CenterParent
            Me.Text = "Select Trailer"
            Me.GroupBox1.ResumeLayout(False)
            Me.pnlStatus.ResumeLayout(False)
            Me.pnlStatus.PerformLayout
            Me.GroupBox2.ResumeLayout(False)
            Me.GroupBox2.PerformLayout
            Me.Panel1.ResumeLayout(False)
            Me.ResumeLayout(False)
        End Sub

        Private Sub lbTrailers_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
            Me.SetEnabled(True)
        End Sub

        Private Sub OK_Button_Click(ByVal sender As Object, ByVal e As EventArgs)
            Me.BeginDownload(True)
        End Sub

        Private Sub SetEnabled(ByVal DeletePre As Boolean)
            If ((DeletePre AndAlso Not String.IsNullOrEmpty(Me.prePath)) AndAlso File.Exists(Me.prePath)) Then
                File.Delete(Me.prePath)
                Me.prePath = String.Empty
            End If
            If ((StringUtils.isValidURL(Me.txtYouTube.Text) OrElse (Me.lbTrailers.SelectedItems.Count > 0)) OrElse (Me.txtManual.Text.Length > 0)) Then
                Me.OK_Button.Enabled = True
                Me.btnSetNfo.Enabled = True
                Me.btnPlayTrailer.Enabled = True
                If (Me.txtManual.Text.Length > 0) Then
                    Me.OK_Button.Text = Master.eLang.GetString(&H3D, "Copy", False)
                    Me.btnSetNfo.Text = Master.eLang.GetString(60, "Move", False)
                Else
                    Me.OK_Button.Text = Master.eLang.GetString(&H175, "Download", True)
                    Me.btnSetNfo.Text = Master.eLang.GetString(&H3F, "Set To Nfo", False)
                End If
            Else
                Me.OK_Button.Enabled = False
                Me.OK_Button.Text = Master.eLang.GetString(&H175, "Download", True)
                Me.btnPlayTrailer.Enabled = False
                Me.btnSetNfo.Enabled = False
                Me.btnSetNfo.Text = Master.eLang.GetString(&H3F, "Set To Nfo", False)
            End If
        End Sub

        Private Sub SetUp()
            Me.Text = Master.eLang.GetString(&H40, "Select Trailer", False)
            Me.OK_Button.Text = Master.eLang.GetString(&H175, "Download", True)
            Me.Cancel_Button.Text = Master.eLang.GetString(&HA7, "Cancel", True)
            Me.GroupBox1.Text = Master.eLang.GetString(&H41, "Select Trailer to Download", False)
            Me.GroupBox2.Text = Master.eLang.GetString(&H42, "Manual Trailer Entry", False)
            Me.Label1.Text = Master.eLang.GetString(&H43, "Direct Link or YouTube URL:", False)
            Me.lblStatus.Text = Master.eLang.GetString(&H44, "Compiling trailer list...", False)
            Me.btnPlayTrailer.Text = Master.eLang.GetString(&H45, "Preview Trailer", False)
            Me.btnSetNfo.Text = Master.eLang.GetString(&H3F, "Set To Nfo", False)
            Me.Label2.Text = Master.eLang.GetString(70, "Local Trailer:", False)
        End Sub

        Public Function ShowDialog(ByVal _imdbID As String, ByVal _sPath As String) As String
            Me.imdbID = _imdbID
            Me.sPath = _sPath
            If (MyBase.ShowDialog = DialogResult.OK) Then
                If AdvancedSettings.GetBooleanSetting("UseTMDBTrailerXBMC", False, "") Then
                    Return Strings.Replace(Me.tURL, "http://www.youtube.com/watch?v=", "plugin://plugin.video.youtube/?action=play_video&videoid=", 1, -1, CompareMethod.Binary)
                End If
                Return Me.tURL
            End If
            Return String.Empty
        End Function

        Private Sub txtManual_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
            Me.SetEnabled(True)
        End Sub

        Private Sub txtYouTube_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
            Me.SetEnabled(True)
        End Sub


        ' Properties
        Friend Overridable Property btnBrowse As Button
            <DebuggerNonUserCode> _
            Get
                Return Me._btnBrowse
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Button)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.btnBrowse_Click)
                If (Not Me._btnBrowse Is Nothing) Then
                    RemoveHandler Me._btnBrowse.Click, handler
                End If
                Me._btnBrowse = WithEventsValue
                If (Not Me._btnBrowse Is Nothing) Then
                    AddHandler Me._btnBrowse.Click, handler
                End If
            End Set
        End Property

        Friend Overridable Property btnGetTrailers As Button
            <DebuggerNonUserCode> _
            Get
                Return Me._btnGetTrailers
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Button)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.btnGetTrailers_Click)
                If (Not Me._btnGetTrailers Is Nothing) Then
                    RemoveHandler Me._btnGetTrailers.Click, handler
                End If
                Me._btnGetTrailers = WithEventsValue
                If (Not Me._btnGetTrailers Is Nothing) Then
                    AddHandler Me._btnGetTrailers.Click, handler
                End If
            End Set
        End Property

        Friend Overridable Property btnPlayTrailer As Button
            <DebuggerNonUserCode> _
            Get
                Return Me._btnPlayTrailer
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Button)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.btnPlayTrailer_Click)
                If (Not Me._btnPlayTrailer Is Nothing) Then
                    RemoveHandler Me._btnPlayTrailer.Click, handler
                End If
                Me._btnPlayTrailer = WithEventsValue
                If (Not Me._btnPlayTrailer Is Nothing) Then
                    AddHandler Me._btnPlayTrailer.Click, handler
                End If
            End Set
        End Property

        Friend Overridable Property btnSetNfo As Button
            <DebuggerNonUserCode> _
            Get
                Return Me._btnSetNfo
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Button)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.btnSetNfo_Click)
                If (Not Me._btnSetNfo Is Nothing) Then
                    RemoveHandler Me._btnSetNfo.Click, handler
                End If
                Me._btnSetNfo = WithEventsValue
                If (Not Me._btnSetNfo Is Nothing) Then
                    AddHandler Me._btnSetNfo.Click, handler
                End If
            End Set
        End Property

        Friend Overridable Property bwCompileList As BackgroundWorker
            <DebuggerNonUserCode> _
            Get
                Return Me._bwCompileList
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As BackgroundWorker)
                Dim handler As RunWorkerCompletedEventHandler = New RunWorkerCompletedEventHandler(AddressOf Me.bwCompileList_RunWorkerCompleted)
                Dim handler2 As DoWorkEventHandler = New DoWorkEventHandler(AddressOf Me.bwCompileList_DoWork)
                If (Not Me._bwCompileList Is Nothing) Then
                    RemoveHandler Me._bwCompileList.RunWorkerCompleted, handler
                    RemoveHandler Me._bwCompileList.DoWork, handler2
                End If
                Me._bwCompileList = WithEventsValue
                If (Not Me._bwCompileList Is Nothing) Then
                    AddHandler Me._bwCompileList.RunWorkerCompleted, handler
                    AddHandler Me._bwCompileList.DoWork, handler2
                End If
            End Set
        End Property

        Friend Overridable Property bwDownloadTrailer As BackgroundWorker
            <DebuggerNonUserCode> _
            Get
                Return Me._bwDownloadTrailer
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As BackgroundWorker)
                Dim handler As RunWorkerCompletedEventHandler = New RunWorkerCompletedEventHandler(AddressOf Me.bwDownloadTrailer_RunWorkerCompleted)
                Dim handler2 As ProgressChangedEventHandler = New ProgressChangedEventHandler(AddressOf Me.bwDownloadTrailer_ProgressChanged)
                Dim handler3 As DoWorkEventHandler = New DoWorkEventHandler(AddressOf Me.bwDownloadTrailer_DoWork)
                If (Not Me._bwDownloadTrailer Is Nothing) Then
                    RemoveHandler Me._bwDownloadTrailer.RunWorkerCompleted, handler
                    RemoveHandler Me._bwDownloadTrailer.ProgressChanged, handler2
                    RemoveHandler Me._bwDownloadTrailer.DoWork, handler3
                End If
                Me._bwDownloadTrailer = WithEventsValue
                If (Not Me._bwDownloadTrailer Is Nothing) Then
                    AddHandler Me._bwDownloadTrailer.RunWorkerCompleted, handler
                    AddHandler Me._bwDownloadTrailer.ProgressChanged, handler2
                    AddHandler Me._bwDownloadTrailer.DoWork, handler3
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

        Friend Overridable Property GroupBox1 As GroupBox
            <DebuggerNonUserCode> _
            Get
                Return Me._GroupBox1
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As GroupBox)
                Me._GroupBox1 = WithEventsValue
            End Set
        End Property

        Friend Overridable Property GroupBox2 As GroupBox
            <DebuggerNonUserCode> _
            Get
                Return Me._GroupBox2
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As GroupBox)
                Me._GroupBox2 = WithEventsValue
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

        Friend Overridable Property lbTrailers As ListBox
            <DebuggerNonUserCode> _
            Get
                Return Me._lbTrailers
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As ListBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.lbTrailers_SelectedIndexChanged)
                If (Not Me._lbTrailers Is Nothing) Then
                    RemoveHandler Me._lbTrailers.SelectedIndexChanged, handler
                End If
                Me._lbTrailers = WithEventsValue
                If (Not Me._lbTrailers Is Nothing) Then
                    AddHandler Me._lbTrailers.SelectedIndexChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property ofdTrailer As OpenFileDialog
            <DebuggerNonUserCode> _
            Get
                Return Me._ofdTrailer
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As OpenFileDialog)
                Me._ofdTrailer = WithEventsValue
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

        Friend Overridable Property Panel1 As Panel
            <DebuggerNonUserCode> _
            Get
                Return Me._Panel1
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Panel)
                Me._Panel1 = WithEventsValue
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

        Friend Overridable Property txtManual As TextBox
            <DebuggerNonUserCode> _
            Get
                Return Me._txtManual
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As TextBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.txtManual_TextChanged)
                If (Not Me._txtManual Is Nothing) Then
                    RemoveHandler Me._txtManual.TextChanged, handler
                End If
                Me._txtManual = WithEventsValue
                If (Not Me._txtManual Is Nothing) Then
                    AddHandler Me._txtManual.TextChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property txtYouTube As TextBox
            <DebuggerNonUserCode> _
            Get
                Return Me._txtYouTube
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As TextBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.txtYouTube_TextChanged)
                If (Not Me._txtYouTube Is Nothing) Then
                    RemoveHandler Me._txtYouTube.TextChanged, handler
                End If
                Me._txtYouTube = WithEventsValue
                If (Not Me._txtYouTube Is Nothing) Then
                    AddHandler Me._txtYouTube.TextChanged, handler
                End If
            End Set
        End Property


        ' Fields
        Private Shared __ENCList As List(Of WeakReference) = New List(Of WeakReference)
        <AccessedThroughProperty("btnBrowse")> _
        Private _btnBrowse As Button
        <AccessedThroughProperty("btnGetTrailers")> _
        Private _btnGetTrailers As Button
        <AccessedThroughProperty("btnPlayTrailer")> _
        Private _btnPlayTrailer As Button
        <AccessedThroughProperty("btnSetNfo")> _
        Private _btnSetNfo As Button
        <AccessedThroughProperty("bwCompileList")> _
        Private _bwCompileList As BackgroundWorker
        <AccessedThroughProperty("bwDownloadTrailer")> _
        Private _bwDownloadTrailer As BackgroundWorker
        <AccessedThroughProperty("Cancel_Button")> _
        Private _Cancel_Button As Button
        <AccessedThroughProperty("GroupBox1")> _
        Private _GroupBox1 As GroupBox
        <AccessedThroughProperty("GroupBox2")> _
        Private _GroupBox2 As GroupBox
        <AccessedThroughProperty("Label1")> _
        Private _Label1 As Label
        <AccessedThroughProperty("Label2")> _
        Private _Label2 As Label
        <AccessedThroughProperty("lblStatus")> _
        Private _lblStatus As Label
        <AccessedThroughProperty("lbTrailers")> _
        Private _lbTrailers As ListBox
        <AccessedThroughProperty("ofdTrailer")> _
        Private _ofdTrailer As OpenFileDialog
        <AccessedThroughProperty("OK_Button")> _
        Private _OK_Button As Button
        <AccessedThroughProperty("Panel1")> _
        Private _Panel1 As Panel
        <AccessedThroughProperty("pbStatus")> _
        Private _pbStatus As ProgressBar
        <AccessedThroughProperty("pnlStatus")> _
        Private _pnlStatus As Panel
        <AccessedThroughProperty("txtManual")> _
        Private _txtManual As TextBox
        <AccessedThroughProperty("txtYouTube")> _
        Private _txtYouTube As TextBox
        Private components As IContainer
        Private cTrailer As Trailers
        Private imdbID As String
        Public IMDBURL As String
        Private prePath As String
        Private sPath As String
        Private tArray As List(Of String)
        Private tURL As String

        ' Nested Types
        <StructLayout(LayoutKind.Sequential)> _
        Private Structure Arguments
            Public bType As Boolean
            Public Parameter As String
        End Structure
    End Class
End Namespace

