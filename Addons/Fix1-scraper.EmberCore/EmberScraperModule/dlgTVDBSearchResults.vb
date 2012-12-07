Imports EmberAPI
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Linq
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Threading
Imports System.Windows.Forms
Imports System.Xml.Linq

Namespace EmberScraperModule
    <DesignerGenerated> _
    Public Class dlgTVDBSearchResults
        Inherits Form
        ' Methods
        Public Sub New()
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.dlgTVDBSearchResults_Load)
            dlgTVDBSearchResults.__ENCAddToList(Me)
            Me.bwDownloadPic = New BackgroundWorker
            Me.lvResultsSorter = New ListViewColumnSorter
            Me.sHTTP = New HTTP
            Me._manualresult = Nothing
            Me._skipdownload = False
            Me.InitializeComponent
        End Sub

        <DebuggerNonUserCode> _
        Private Shared Sub __ENCAddToList(ByVal value As Object)
            Dim list As List(Of WeakReference) = dlgTVDBSearchResults.__ENCList
            SyncLock list
                If (dlgTVDBSearchResults.__ENCList.Count = dlgTVDBSearchResults.__ENCList.Capacity) Then
                    Dim index As Integer = 0
                    Dim num3 As Integer = (dlgTVDBSearchResults.__ENCList.Count - 1)
                    Dim i As Integer = 0
                    Do While (i <= num3)
                        Dim reference As WeakReference = dlgTVDBSearchResults.__ENCList.Item(i)
                        If reference.IsAlive Then
                            If (i <> index) Then
                                dlgTVDBSearchResults.__ENCList.Item(index) = dlgTVDBSearchResults.__ENCList.Item(i)
                            End If
                            index += 1
                        End If
                        i += 1
                    Loop
                    dlgTVDBSearchResults.__ENCList.RemoveRange(index, (dlgTVDBSearchResults.__ENCList.Count - index))
                    dlgTVDBSearchResults.__ENCList.Capacity = dlgTVDBSearchResults.__ENCList.Count
                End If
                dlgTVDBSearchResults.__ENCList.Add(New WeakReference(RuntimeHelpers.GetObjectValue(value)))
            End SyncLock
        End Sub

        <CompilerGenerated> _
        Private Shared Function _Lambda$__125(ByVal s As XElement) As Boolean
            Return s.HasElements
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__127(ByVal r As TVSearchResults) As Integer
            Return r.Lev
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__128(ByVal s As TVSearchResults) As Integer
            Return s.ID
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__129(ByVal s As TVSearchResults) As Boolean
            Return (s.Lev <= 5)
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__130(ByVal s As TVSearchResults) As Integer
            Return s.Lev
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__131(ByVal s As TVSearchResults) As Boolean
            Return (s.Language.ShortLang = "en")
        End Function

        Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs)
            If Not String.IsNullOrEmpty(Me.txtSearch.Text) Then
                Me.lvSearchResults.Enabled = False
                Me.sInfo.ShowTitle = Me.txtSearch.Text
                Me.ClearInfo
                Me.chkManual.Enabled = False
                Me.chkManual.Checked = False
                Me.txtSearch.Text = String.Empty
                Me.btnVerify.Enabled = False
                Scraper.sObject.GetSearchResultsAsync(Me.sInfo)
                Me.pnlLoading.Visible = True
            End If
        End Sub

        Private Sub btnVerify_Click(ByVal sender As Object, ByVal e As EventArgs)
            If (Versioned.IsNumeric(Me.txtTVDBID.Text) AndAlso (Me.txtTVDBID.Text.Length >= 5)) Then
                Dim e$__ As New _Closure$__20
                Dim expression As XDocument = Nothing
                e$__.$VB$Local_sLang = String.Empty
                Me.ClearInfo
                Me.pnlLoading.Visible = True
                Application.DoEvents
                Dim str As String = Me.sHTTP.DownloadData(String.Format("http://{0}/api/{1}/series/{2}/{3}.xml", New Object() { Master.eSettings.TVDBMirror, "7B090234F418D074", Me.txtTVDBID.Text, Master.eSettings.TVDBLanguage }))
                If Not String.IsNullOrEmpty(str) Then
                    Try 
                        expression = XDocument.Parse(str)
                    Catch exception1 As Exception
                        ProjectData.SetProjectError(exception1)
                        ProjectData.ClearProjectError
                    End Try
                    If Not Information.IsNothing(expression) Then
                        Dim element As XElement = expression.Descendants("Series").FirstOrDefault(Of XElement)(New Func(Of XElement, Boolean)(AddressOf dlgTVDBSearchResults._Lambda$__125))
                        If Not Information.IsNothing(element) Then
                            Me._manualresult = New TVSearchResults
                            Me._manualresult.ID = Convert.ToInt32(element.Element("id").Value)
                            Me._manualresult.Name = If(Not Information.IsNothing(element.Element("SeriesName")), element.Element("SeriesName").Value, String.Empty)
                            If (Not Information.IsNothing(element.Element("Language")) AndAlso (Master.eSettings.TVDBLanguages.Count > 0)) Then
                                e$__.$VB$Local_sLang = element.Element("Language").Value
                                Me._manualresult.Language = Master.eSettings.TVDBLanguages.FirstOrDefault(Of TVLanguage)(New Func(Of TVLanguage, Boolean)(AddressOf e$__._Lambda$__126))
                            ElseIf Not Information.IsNothing(element.Element("Language")) Then
                                e$__.$VB$Local_sLang = element.Element("Language").Value
                                Dim language As New TVLanguage With { _
                                    .LongLang = String.Format("Unknown ({0})", e$__.$VB$Local_sLang), _
                                    .ShortLang = e$__.$VB$Local_sLang _
                                }
                                Me._manualresult.Language = language
                            End If
                            Me._manualresult.Aired = If(Not Information.IsNothing(element.Element("FirstAired")), element.Element("FirstAired").Value, String.Empty)
                            Me._manualresult.Overview = If(Not Information.IsNothing(element.Element("Overview")), element.Element("Overview").Value, String.Empty)
                            Me._manualresult.Banner = If(Not Information.IsNothing(element.Element("banner")), element.Element("banner").Value, String.Empty)
                            If (Not String.IsNullOrEmpty(Me._manualresult.Name) AndAlso Not String.IsNullOrEmpty(e$__.$VB$Local_sLang)) Then
                                If Not String.IsNullOrEmpty(Me._manualresult.Banner) Then
                                    If Me.bwDownloadPic.IsBusy Then
                                        Me.bwDownloadPic.CancelAsync
                                    End If
                                    Me.bwDownloadPic = New BackgroundWorker
                                    Me.bwDownloadPic.WorkerSupportsCancellation = True
                                    Dim argument As New Arguments With { _
                                        .pURL = Me._manualresult.Banner _
                                    }
                                    Me.bwDownloadPic.RunWorkerAsync(argument)
                                End If
                                Me.OK_Button.Tag = Me._manualresult.ID
                                Me.lblTitle.Text = Me._manualresult.Name
                                Me.txtOutline.Text = Me._manualresult.Overview
                                Me.lblAired.Text = Me._manualresult.Aired
                                Me.OK_Button.Enabled = True
                                Me.pnlLoading.Visible = False
                                Me.ControlsVisible(True)
                            Else
                                Me.pnlLoading.Visible = False
                            End If
                        Else
                            Me.pnlLoading.Visible = False
                        End If
                    Else
                        Me.pnlLoading.Visible = False
                    End If
                Else
                    Me.pnlLoading.Visible = False
                End If
            Else
                Interaction.MsgBox(Master.eLang.GetString(&H53, "The ID you entered is not a valid TVDB ID.", False), MsgBoxStyle.Exclamation, Master.eLang.GetString(&H124, "Invalid Entry", True))
            End If
        End Sub

        Private Sub bwDownloadPic_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
            Dim argument As Arguments = DirectCast(e.Argument, Arguments)
            Me.sHTTP.StartDownloadImage(String.Format("http://{0}/banners/_cache/{1}", Master.eSettings.TVDBMirror, argument.pURL))
            Do While Me.sHTTP.IsDownloading
                Application.DoEvents
                Thread.Sleep(50)
            Loop
            Dim results2 As New Results With { _
                .Result = Me.sHTTP.Image _
            }
            e.Result = results2
        End Sub

        Private Sub bwDownloadPic_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
            Dim result As Results = DirectCast(e.Result, Results)
            Try 
                Me.pbBanner.Image = result.Result
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Private Sub Cancel_Button_Click(ByVal sender As Object, ByVal e As EventArgs)
            Me.DialogResult = DialogResult.Cancel
            Me.Close
        End Sub

        Private Sub chkManual_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Me.ClearInfo
            Me.OK_Button.Enabled = False
            Me.txtTVDBID.Enabled = Me.chkManual.Checked
            Me.btnVerify.Enabled = Me.chkManual.Checked
            Me.lvSearchResults.Enabled = Not Me.chkManual.Checked
            If Not Me.chkManual.Checked Then
                Me.txtTVDBID.Text = String.Empty
            ElseIf (Me.lvSearchResults.SelectedItems.Count > 0) Then
                Me.lvSearchResults.SelectedItems.Item(0).Selected = False
            End If
        End Sub

        Private Sub ClearInfo()
            Me.ControlsVisible(False)
            Me.lblTitle.Text = String.Empty
            Me.lblAired.Text = String.Empty
            Me.pbBanner.Image = Nothing
            Scraper.sObject.CancelAsync
        End Sub

        Private Sub ControlsVisible(ByVal areVisible As Boolean)
            Me.pbBanner.Visible = areVisible
            Me.lblTitle.Visible = areVisible
            Me.lblAiredHeader.Visible = areVisible
            Me.lblAired.Visible = areVisible
            Me.lblPlotHeader.Visible = areVisible
            Me.txtOutline.Visible = areVisible
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

        Private Sub dlgTVDBSearchResults_Load(ByVal sender As Object, ByVal e As EventArgs)
            Try 
                AddHandler ModulesManager.Instance.TVScraperEvent, New TVScraperEventEventHandler(AddressOf Me.TVScraperEvent)
                Dim image As New Bitmap(Me.pnlTop.Width, Me.pnlTop.Height)
                Using graphics As Graphics = Graphics.FromImage(image)
                    graphics.FillRectangle(New LinearGradientBrush(Me.pnlTop.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, LinearGradientMode.Horizontal), Me.pnlTop.ClientRectangle)
                    Me.pnlTop.BackgroundImage = image
                End Using
                Me.lvSearchResults.ListViewItemSorter = Me.lvResultsSorter
                Me.SetUp
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub

        <DebuggerStepThrough> _
        Private Sub InitializeComponent()
            Dim manager As New ComponentResourceManager(GetType(dlgTVDBSearchResults))
            Me.OK_Button = New Button
            Me.Cancel_Button = New Button
            Me.pbBanner = New PictureBox
            Me.lblTitle = New Label
            Me.lblAiredHeader = New Label
            Me.lblAired = New Label
            Me.lblPlotHeader = New Label
            Me.txtOutline = New TextBox
            Me.lvSearchResults = New ListView
            Me.colName = New ColumnHeader
            Me.colLang = New ColumnHeader
            Me.colLev = New ColumnHeader
            Me.colID = New ColumnHeader
            Me.colSLang = New ColumnHeader
            Me.pnlLoading = New Panel
            Me.Label3 = New Label
            Me.ProgressBar1 = New ProgressBar
            Me.pnlTop = New Panel
            Me.Label2 = New Label
            Me.Label1 = New Label
            Me.PictureBox1 = New PictureBox
            Me.btnSearch = New Button
            Me.txtSearch = New TextBox
            Me.btnVerify = New Button
            Me.chkManual = New CheckBox
            Me.txtTVDBID = New TextBox
            DirectCast(Me.pbBanner, ISupportInitialize).BeginInit
            Me.pnlLoading.SuspendLayout
            Me.pnlTop.SuspendLayout
            DirectCast(Me.PictureBox1, ISupportInitialize).BeginInit
            Me.SuspendLayout
            Me.OK_Button.Enabled = False
            Dim point2 As New Point(&H25E, &H1A3)
            Me.OK_Button.Location = point2
            Dim padding2 As New Padding(4)
            Me.OK_Button.Margin = padding2
            Me.OK_Button.Name = "OK_Button"
            Dim size2 As New Size(&H54, &H1D)
            Me.OK_Button.Size = size2
            Me.OK_Button.TabIndex = 0
            Me.OK_Button.Text = "OK"
            Me.Cancel_Button.DialogResult = DialogResult.Cancel
            point2 = New Point(&H2BA, &H1A3)
            Me.Cancel_Button.Location = point2
            padding2 = New Padding(4)
            Me.Cancel_Button.Margin = padding2
            Me.Cancel_Button.Name = "Cancel_Button"
            size2 = New Size(&H54, &H1D)
            Me.Cancel_Button.Size = size2
            Me.Cancel_Button.TabIndex = 1
            Me.Cancel_Button.Text = "Cancel"
            point2 = New Point(&H196, &H56)
            Me.pbBanner.Location = point2
            padding2 = New Padding(4)
            Me.pbBanner.Margin = padding2
            Me.pbBanner.Name = "pbBanner"
            size2 = New Size(&H177, &H45)
            Me.pbBanner.Size = size2
            Me.pbBanner.SizeMode = PictureBoxSizeMode.Zoom
            Me.pbBanner.TabIndex = 3
            Me.pbBanner.TabStop = False
            Me.lblTitle.Font = New Font("Microsoft Sans Serif", 9.75!, FontStyle.Bold, GraphicsUnit.Point, 0)
            point2 = New Point(&H196, &HA5)
            Me.lblTitle.Location = point2
            padding2 = New Padding(4, 0, 4, 0)
            Me.lblTitle.Margin = padding2
            Me.lblTitle.Name = "lblTitle"
            size2 = New Size(&H177, &H18)
            Me.lblTitle.Size = size2
            Me.lblTitle.TabIndex = 4
            Me.lblTitle.Visible = False
            Me.lblAiredHeader.AutoSize = True
            Me.lblAiredHeader.Font = New Font("Segoe UI", 8.25!, FontStyle.Bold, GraphicsUnit.Point, 0)
            point2 = New Point(&H196, &HC9)
            Me.lblAiredHeader.Location = point2
            padding2 = New Padding(4, 0, 4, 0)
            Me.lblAiredHeader.Margin = padding2
            Me.lblAiredHeader.Name = "lblAiredHeader"
            size2 = New Size(50, &H13)
            Me.lblAiredHeader.Size = size2
            Me.lblAiredHeader.TabIndex = &H3F
            Me.lblAiredHeader.Text = "Aired:"
            Me.lblAiredHeader.Visible = False
            Me.lblAired.AutoSize = True
            point2 = New Point(&H1E4, &HC9)
            Me.lblAired.Location = point2
            padding2 = New Padding(4, 0, 4, 0)
            Me.lblAired.Margin = padding2
            Me.lblAired.Name = "lblAired"
            size2 = New Size(&H53, &H13)
            Me.lblAired.Size = size2
            Me.lblAired.TabIndex = &H3E
            Me.lblAired.Text = "00/00/0000"
            Me.lblAired.Visible = False
            Me.lblPlotHeader.AutoSize = True
            Me.lblPlotHeader.Font = New Font("Segoe UI", 8.25!, FontStyle.Bold, GraphicsUnit.Point, 0)
            point2 = New Point(&H196, &HEC)
            Me.lblPlotHeader.Location = point2
            padding2 = New Padding(4, 0, 4, 0)
            Me.lblPlotHeader.Margin = padding2
            Me.lblPlotHeader.Name = "lblPlotHeader"
            size2 = New Size(&H6C, &H13)
            Me.lblPlotHeader.Size = size2
            Me.lblPlotHeader.TabIndex = &H43
            Me.lblPlotHeader.Text = "Plot Summary:"
            Me.lblPlotHeader.Visible = False
            Me.txtOutline.BorderStyle = BorderStyle.FixedSingle
            point2 = New Point(&H196, &H100)
            Me.txtOutline.Location = point2
            padding2 = New Padding(4)
            Me.txtOutline.Margin = padding2
            Me.txtOutline.Multiline = True
            Me.txtOutline.Name = "txtOutline"
            size2 = New Size(&H176, &H9E)
            Me.txtOutline.Size = size2
            Me.txtOutline.TabIndex = &H42
            Me.txtOutline.TabStop = False
            Me.txtOutline.Visible = False
            Me.lvSearchResults.Columns.AddRange(New ColumnHeader() { Me.colName, Me.colLang, Me.colLev, Me.colID, Me.colSLang })
            Me.lvSearchResults.FullRowSelect = True
            Me.lvSearchResults.HideSelection = False
            point2 = New Point(4, 120)
            Me.lvSearchResults.Location = point2
            padding2 = New Padding(4)
            Me.lvSearchResults.Margin = padding2
            Me.lvSearchResults.MultiSelect = False
            Me.lvSearchResults.Name = "lvSearchResults"
            size2 = New Size(&H18A, &H126)
            Me.lvSearchResults.Size = size2
            Me.lvSearchResults.TabIndex = &H44
            Me.lvSearchResults.UseCompatibleStateImageBehavior = False
            Me.lvSearchResults.View = View.Details
            Me.colName.Text = "Title"
            Me.colName.Width = &HDF
            Me.colLang.Text = "Language"
            Me.colLang.Width = &H59
            Me.colLev.Width = 0
            Me.colID.Width = 0
            Me.colSLang.Width = 0
            Me.pnlLoading.BackColor = Color.White
            Me.pnlLoading.BorderStyle = BorderStyle.FixedSingle
            Me.pnlLoading.Controls.Add(Me.Label3)
            Me.pnlLoading.Controls.Add(Me.ProgressBar1)
            point2 = New Point(&H1DB, &HC0)
            Me.pnlLoading.Location = point2
            padding2 = New Padding(4)
            Me.pnlLoading.Margin = padding2
            Me.pnlLoading.Name = "pnlLoading"
            size2 = New Size(250, &H43)
            Me.pnlLoading.Size = size2
            Me.pnlLoading.TabIndex = &H45
            Me.Label3.AutoSize = True
            Me.Label3.Font = New Font("Segoe UI", 8.25!, FontStyle.Bold, GraphicsUnit.Point, 0)
            point2 = New Point(4, 12)
            Me.Label3.Location = point2
            padding2 = New Padding(4, 0, 4, 0)
            Me.Label3.Margin = padding2
            Me.Label3.Name = "Label3"
            size2 = New Size(&H7F, &H13)
            Me.Label3.Size = size2
            Me.Label3.TabIndex = 1
            Me.Label3.Text = "Searching TVDB..."
            Me.Label3.TextAlign = ContentAlignment.MiddleLeft
            point2 = New Point(4, 40)
            Me.ProgressBar1.Location = point2
            padding2 = New Padding(4)
            Me.ProgressBar1.Margin = padding2
            Me.ProgressBar1.MarqueeAnimationSpeed = &H19
            Me.ProgressBar1.Name = "ProgressBar1"
            size2 = New Size(240, &H15)
            Me.ProgressBar1.Size = size2
            Me.ProgressBar1.Style = ProgressBarStyle.Marquee
            Me.ProgressBar1.TabIndex = 0
            Me.pnlTop.BackColor = Color.LightSteelBlue
            Me.pnlTop.BorderStyle = BorderStyle.FixedSingle
            Me.pnlTop.Controls.Add(Me.Label2)
            Me.pnlTop.Controls.Add(Me.Label1)
            Me.pnlTop.Controls.Add(Me.PictureBox1)
            Me.pnlTop.Dock = DockStyle.Top
            point2 = New Point(0, 0)
            Me.pnlTop.Location = point2
            padding2 = New Padding(4)
            Me.pnlTop.Margin = padding2
            Me.pnlTop.Name = "pnlTop"
            size2 = New Size(&H324, 80)
            Me.pnlTop.Size = size2
            Me.pnlTop.TabIndex = 70
            Me.Label2.AutoSize = True
            Me.Label2.BackColor = Color.Transparent
            Me.Label2.ForeColor = Color.White
            point2 = New Point(&H4C, &H30)
            Me.Label2.Location = point2
            padding2 = New Padding(4, 0, 4, 0)
            Me.Label2.Margin = padding2
            Me.Label2.Name = "Label2"
            size2 = New Size(&H153, &H13)
            Me.Label2.Size = size2
            Me.Label2.TabIndex = 2
            Me.Label2.Text = "View details of each result to find the proper TV show."
            Me.Label1.AutoSize = True
            Me.Label1.BackColor = Color.Transparent
            Me.Label1.Font = New Font("Segoe UI", 18!, FontStyle.Bold, GraphicsUnit.Point, 0)
            Me.Label1.ForeColor = Color.White
            point2 = New Point(&H48, 4)
            Me.Label1.Location = point2
            padding2 = New Padding(4, 0, 4, 0)
            Me.Label1.Margin = padding2
            Me.Label1.Name = "Label1"
            size2 = New Size(&H10A, &H29)
            Me.Label1.Size = size2
            Me.Label1.TabIndex = 1
            Me.Label1.Text = "TV Search Results"
            Me.PictureBox1.BackColor = Color.Transparent
            Me.PictureBox1.Image = DirectCast(manager.GetObject("PictureBox1.Image"), Image)
            point2 = New Point(9, 10)
            Me.PictureBox1.Location = point2
            padding2 = New Padding(4)
            Me.PictureBox1.Margin = padding2
            Me.PictureBox1.Name = "PictureBox1"
            size2 = New Size(&H30, &H30)
            Me.PictureBox1.Size = size2
            Me.PictureBox1.SizeMode = PictureBoxSizeMode.AutoSize
            Me.PictureBox1.TabIndex = 0
            Me.PictureBox1.TabStop = False
            Me.btnSearch.Image = DirectCast(manager.GetObject("btnSearch.Image"), Image)
            point2 = New Point(370, &H54)
            Me.btnSearch.Location = point2
            padding2 = New Padding(4)
            Me.btnSearch.Margin = padding2
            Me.btnSearch.Name = "btnSearch"
            size2 = New Size(&H1D, &H1D)
            Me.btnSearch.Size = size2
            Me.btnSearch.TabIndex = &H48
            Me.btnSearch.UseVisualStyleBackColor = True
            point2 = New Point(4, &H55)
            Me.txtSearch.Location = point2
            padding2 = New Padding(4)
            Me.txtSearch.Margin = padding2
            Me.txtSearch.Name = "txtSearch"
            size2 = New Size(&H166, &H1A)
            Me.txtSearch.Size = size2
            Me.txtSearch.TabIndex = &H47
            Me.btnVerify.Enabled = False
            Me.btnVerify.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(&H131, &H1A5)
            Me.btnVerify.Location = point2
            padding2 = New Padding(4)
            Me.btnVerify.Margin = padding2
            Me.btnVerify.Name = "btnVerify"
            size2 = New Size(&H5E, &H1C)
            Me.btnVerify.Size = size2
            Me.btnVerify.TabIndex = &H4B
            Me.btnVerify.Text = "Verify"
            Me.btnVerify.UseVisualStyleBackColor = True
            Me.chkManual.AutoSize = True
            Me.chkManual.Enabled = False
            Me.chkManual.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(4, &H1AA)
            Me.chkManual.Location = point2
            padding2 = New Padding(4)
            Me.chkManual.Margin = padding2
            Me.chkManual.Name = "chkManual"
            size2 = New Size(&H9A, &H17)
            Me.chkManual.Size = size2
            Me.chkManual.TabIndex = &H49
            Me.chkManual.Text = "Manual TVDB Entry:"
            Me.chkManual.UseVisualStyleBackColor = True
            Me.txtTVDBID.Enabled = False
            Me.txtTVDBID.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(&HAC, &H1A5)
            Me.txtTVDBID.Location = point2
            padding2 = New Padding(4)
            Me.txtTVDBID.Margin = padding2
            Me.txtTVDBID.Name = "txtTVDBID"
            size2 = New Size(&H7C, &H1A)
            Me.txtTVDBID.Size = size2
            Me.txtTVDBID.TabIndex = &H4A
            Me.AcceptButton = Me.OK_Button
            Dim ef2 As New SizeF(120!, 120!)
            Me.AutoScaleDimensions = ef2
            Me.AutoScaleMode = AutoScaleMode.Dpi
            Me.CancelButton = Me.Cancel_Button
            size2 = New Size(&H324, &H1D3)
            Me.ClientSize = size2
            Me.ControlBox = False
            Me.Controls.Add(Me.btnVerify)
            Me.Controls.Add(Me.chkManual)
            Me.Controls.Add(Me.txtTVDBID)
            Me.Controls.Add(Me.btnSearch)
            Me.Controls.Add(Me.txtSearch)
            Me.Controls.Add(Me.pnlTop)
            Me.Controls.Add(Me.pnlLoading)
            Me.Controls.Add(Me.lvSearchResults)
            Me.Controls.Add(Me.lblPlotHeader)
            Me.Controls.Add(Me.txtOutline)
            Me.Controls.Add(Me.lblAiredHeader)
            Me.Controls.Add(Me.lblAired)
            Me.Controls.Add(Me.lblTitle)
            Me.Controls.Add(Me.pbBanner)
            Me.Controls.Add(Me.Cancel_Button)
            Me.Controls.Add(Me.OK_Button)
            Me.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            Me.FormBorderStyle = FormBorderStyle.FixedDialog
            padding2 = New Padding(4)
            Me.Margin = padding2
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            size2 = New Size(810, 500)
            Me.MinimumSize = size2
            Me.Name = "dlgTVDBSearchResults"
            Me.ShowInTaskbar = False
            Me.StartPosition = FormStartPosition.CenterParent
            Me.Text = "TV Search Results"
            DirectCast(Me.pbBanner, ISupportInitialize).EndInit
            Me.pnlLoading.ResumeLayout(False)
            Me.pnlLoading.PerformLayout
            Me.pnlTop.ResumeLayout(False)
            Me.pnlTop.PerformLayout
            DirectCast(Me.PictureBox1, ISupportInitialize).EndInit
            Me.ResumeLayout(False)
            Me.PerformLayout
        End Sub

        Private Sub lvSearchResults_ColumnClick(ByVal sender As Object, ByVal e As ColumnClickEventArgs)
            Try 
                If (e.Column = Me.lvResultsSorter.SortColumn) Then
                    If (Me.lvResultsSorter.Order = SortOrder.Ascending) Then
                        Me.lvResultsSorter.Order = SortOrder.Descending
                    Else
                        Me.lvResultsSorter.Order = SortOrder.Ascending
                    End If
                Else
                    Me.lvResultsSorter.SortColumn = e.Column
                    Me.lvResultsSorter.Order = SortOrder.Ascending
                End If
                Me.lvSearchResults.Sort
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Private Sub lvSearchResults_GotFocus(ByVal sender As Object, ByVal e As EventArgs)
            Me.AcceptButton = Me.OK_Button
        End Sub

        Private Sub lvSearchResults_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
            Me.ClearInfo
            If ((Me.lvSearchResults.SelectedItems.Count > 0) AndAlso Not Me.chkManual.Checked) Then
                Dim tag As TVSearchResults = DirectCast(Me.lvSearchResults.SelectedItems.Item(0).Tag, TVSearchResults)
                If Not String.IsNullOrEmpty(tag.Banner) Then
                    If Me.bwDownloadPic.IsBusy Then
                        Me.bwDownloadPic.CancelAsync
                    End If
                    Me.bwDownloadPic = New BackgroundWorker
                    Me.bwDownloadPic.WorkerSupportsCancellation = True
                    Dim argument As New Arguments With { _
                        .pURL = tag.Banner _
                    }
                    Me.bwDownloadPic.RunWorkerAsync(argument)
                End If
                Me.OK_Button.Tag = tag.ID
                Me.lblTitle.Text = tag.Name
                Me.txtOutline.Text = tag.Overview
                Me.lblAired.Text = tag.Aired
                Me.OK_Button.Enabled = True
                Me.ControlsVisible(True)
            End If
        End Sub

        Private Sub OK_Button_Click(ByVal sender As Object, ByVal e As EventArgs)
            If (Me.lvSearchResults.SelectedItems.Count > 0) Then
                Dim tag As TVSearchResults = DirectCast(Me.lvSearchResults.SelectedItems.Item(0).Tag, TVSearchResults)
                Me.sInfo.TVDBID = tag.ID.ToString
                Me.sInfo.SelectedLang = tag.Language.ShortLang
                If Not Me._skipdownload Then
                    Me.Label3.Text = Master.eLang.GetString(&H54, "Downloading show info...", False)
                    Me.pnlLoading.Visible = True
                    Scraper.sObject.DownloadSeriesAsync(Me.sInfo)
                Else
                    Me.DialogResult = DialogResult.OK
                    Me.Close
                End If
            ElseIf (Me.chkManual.Checked AndAlso Not Information.IsNothing(Me._manualresult)) Then
                Me.sInfo.TVDBID = Me._manualresult.ID.ToString
                Me.sInfo.SelectedLang = Me._manualresult.Language.ShortLang
                If Not Me._skipdownload Then
                    Me.Label3.Text = Master.eLang.GetString(&H54, "Downloading show info...", False)
                    Me.pnlLoading.Visible = True
                    Scraper.sObject.DownloadSeriesAsync(Me.sInfo)
                Else
                    Me.DialogResult = DialogResult.OK
                    Me.Close
                End If
            End If
        End Sub

        Private Sub SetUp()
            Me.Label1.Text = Master.eLang.GetString(&H55, "TV Search Results", False)
            Me.Label2.Text = Master.eLang.GetString(&H56, "View details of each result to find the proper TV show.", False)
            Me.lblAiredHeader.Text = Master.eLang.GetString(&H292, "Aired:", True)
            Me.lblPlotHeader.Text = Master.eLang.GetString(&H30F, "Plot Summary:", True)
            Me.lvSearchResults.Columns.Item(0).Text = Master.eLang.GetString(&H15, "Title", True)
            Me.lvSearchResults.Columns.Item(1).Text = Master.eLang.GetString(610, "Language", True)
            Me.OK_Button.Text = Master.eLang.GetString(&HB3, "OK", True)
            Me.Cancel_Button.Text = Master.eLang.GetString(&HA7, "Cancel", True)
        End Sub

        Public Function ShowDialog(ByVal _sInfo As ScrapeInfo) As DialogResult
            Me.sInfo = _sInfo
            Me.Text = (Master.eLang.GetString(&H55, "TV Search Results", False) & " - " & Me.sInfo.ShowTitle)
            Scraper.sObject.GetSearchResultsAsync(Me.sInfo)
            Return MyBase.ShowDialog
        End Function

        Public Function ShowDialog(ByVal _sinfo As ScrapeInfo, ByVal SkipDownload As Boolean) As ScrapeInfo
            Me.sInfo = _sinfo
            Me._skipdownload = SkipDownload
            Me.Text = (Master.eLang.GetString(&H55, "TV Search Results", False) & " - " & Me.sInfo.ShowTitle)
            Scraper.sObject.GetSearchResultsAsync(Me.sInfo)
            If (MyBase.ShowDialog = DialogResult.OK) Then
                Return Me.sInfo
            End If
            Return _sinfo
        End Function

        Private Sub TVScraperEvent(ByVal eType As TVScraperEventType, ByVal iProgress As Integer, ByVal Parameter As Object)
            Dim list As List(Of TVSearchResults)
            Select Case CInt(eType)
                Case 1
                    Dim enumerator3 As IEnumerator
                    list = DirectCast(Parameter, List(Of TVSearchResults))
                    Me.lvSearchResults.Items.Clear
                    If (Not Information.IsNothing(list) AndAlso (list.Count > 0)) Then
                        Dim results As TVSearchResults
                        For Each results In list.OrderBy(Of TVSearchResults, Integer)(New Func(Of TVSearchResults, Integer)(AddressOf dlgTVDBSearchResults._Lambda$__127))
                            Dim item As New ListViewItem(results.Name)
                            item.SubItems.Add(results.Language.LongLang)
                            item.SubItems.Add(results.Lev.ToString)
                            item.SubItems.Add(results.ID.ToString)
                            item.SubItems.Add(results.Language.ShortLang)
                            item.Tag = results
                            Me.lvSearchResults.Items.Add(item)
                        Next
                    End If
                    Me.pnlLoading.Visible = False
                    If (Me.lvSearchResults.Items.Count <= 0) Then
                        goto Label_0442
                    End If
                    If (list.Select(Of TVSearchResults, Integer)(New Func(Of TVSearchResults, Integer)(AddressOf dlgTVDBSearchResults._Lambda$__128)).Distinct(Of Integer)().Count(Of Integer)() = 1) Then
                        Dim enumerator As IEnumerator
                        Try 
                            enumerator = Me.lvSearchResults.Items.GetEnumerator
                            Do While enumerator.MoveNext
                                Dim current As ListViewItem = DirectCast(enumerator.Current, ListViewItem)
                                If (current.SubItems.Item(4).Text = Master.eSettings.TVDBLanguage) Then
                                    current.Selected = True
                                    current.EnsureVisible
                                    goto Label_0403
                                End If
                            Loop
                        Finally
                            If TypeOf enumerator Is IDisposable Then
                                TryCast(enumerator,IDisposable).Dispose
                            End If
                        End Try
                        goto Label_0403
                    End If
                    If (list.Where(Of TVSearchResults)(New Func(Of TVSearchResults, Boolean)(AddressOf dlgTVDBSearchResults._Lambda$__129)).Count(Of TVSearchResults)() <= 0) Then
                        goto Label_0403
                    End If
                    Try 
                        enumerator3 = Me.lvSearchResults.Items.GetEnumerator
                        Do While enumerator3.MoveNext
                            Dim item3 As ListViewItem = DirectCast(enumerator3.Current, ListViewItem)
                            If ((Convert.ToInt32(item3.SubItems.Item(2).Text) <= 5) AndAlso (item3.SubItems.Item(4).Text = Master.eSettings.TVDBLanguage)) Then
                                item3.Selected = True
                                item3.EnsureVisible
                                Exit Do
                            End If
                        Loop
                    Finally
                        If TypeOf enumerator3 Is IDisposable Then
                            TryCast(enumerator3,IDisposable).Dispose
                        End If
                    End Try
                    Exit Select
                Case 2
                    Return
                Case 3
                    Me.DialogResult = DialogResult.OK
                    Me.Close
                    Return
                Case Else
                    Return
            End Select
            If (Me.lvSearchResults.SelectedItems.Count = 0) Then
                Dim iD As Integer = list.OrderBy(Of TVSearchResults, Integer)(New Func(Of TVSearchResults, Integer)(AddressOf dlgTVDBSearchResults._Lambda$__130)).FirstOrDefault(Of TVSearchResults)(New Func(Of TVSearchResults, Boolean)(AddressOf dlgTVDBSearchResults._Lambda$__131)).ID
                If (iD > 0) Then
                    Dim enumerator4 As IEnumerator
                    Try 
                        enumerator4 = Me.lvSearchResults.Items.GetEnumerator
                        Do While enumerator4.MoveNext
                            Dim item4 As ListViewItem = DirectCast(enumerator4.Current, ListViewItem)
                            If ((Convert.ToInt32(item4.SubItems.Item(3).Text) = iD) AndAlso (item4.SubItems.Item(4).Text = Master.eSettings.TVDBLanguage)) Then
                                item4.Selected = True
                                item4.EnsureVisible
                                goto Label_0403
                            End If
                        Loop
                    Finally
                        If TypeOf enumerator4 Is IDisposable Then
                            TryCast(enumerator4,IDisposable).Dispose
                        End If
                    End Try
                End If
            End If
        Label_0403:
            If (Me.lvSearchResults.SelectedItems.Count = 0) Then
                Me.lvSearchResults.Items.Item(0).Selected = True
            End If
            Me.lvSearchResults.Select
        Label_0442:
            Me.chkManual.Enabled = True
            If Not Me.chkManual.Checked Then
                Me.lvSearchResults.Enabled = True
            End If
        End Sub

        Private Sub txtSearch_GotFocus(ByVal sender As Object, ByVal e As EventArgs)
            Me.AcceptButton = Me.btnSearch
        End Sub

        Private Sub txtTVDBID_GotFocus(ByVal sender As Object, ByVal e As EventArgs)
            Me.AcceptButton = Me.btnVerify
        End Sub

        Private Sub txtTVDBID_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
            e.Handled = StringUtils.NumericOnly(e.KeyChar, True)
        End Sub

        Private Sub txtTVDBID_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
            If String.IsNullOrEmpty(Me.txtTVDBID.Text) Then
                Me.btnVerify.Enabled = False
                Me.OK_Button.Enabled = False
            ElseIf Me.chkManual.Checked Then
                Me.btnVerify.Enabled = True
                Me.OK_Button.Enabled = False
            End If
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

        Friend Overridable Property colID As ColumnHeader
            <DebuggerNonUserCode> _
            Get
                Return Me._colID
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As ColumnHeader)
                Me._colID = WithEventsValue
            End Set
        End Property

        Friend Overridable Property colLang As ColumnHeader
            <DebuggerNonUserCode> _
            Get
                Return Me._colLang
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As ColumnHeader)
                Me._colLang = WithEventsValue
            End Set
        End Property

        Friend Overridable Property colLev As ColumnHeader
            <DebuggerNonUserCode> _
            Get
                Return Me._colLev
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As ColumnHeader)
                Me._colLev = WithEventsValue
            End Set
        End Property

        Friend Overridable Property colName As ColumnHeader
            <DebuggerNonUserCode> _
            Get
                Return Me._colName
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As ColumnHeader)
                Me._colName = WithEventsValue
            End Set
        End Property

        Friend Overridable Property colSLang As ColumnHeader
            <DebuggerNonUserCode> _
            Get
                Return Me._colSLang
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As ColumnHeader)
                Me._colSLang = WithEventsValue
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

        Friend Overridable Property lblAired As Label
            <DebuggerNonUserCode> _
            Get
                Return Me._lblAired
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Label)
                Me._lblAired = WithEventsValue
            End Set
        End Property

        Friend Overridable Property lblAiredHeader As Label
            <DebuggerNonUserCode> _
            Get
                Return Me._lblAiredHeader
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Label)
                Me._lblAiredHeader = WithEventsValue
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

        Friend Overridable Property lvSearchResults As ListView
            <DebuggerNonUserCode> _
            Get
                Return Me._lvSearchResults
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As ListView)
                Dim handler As ColumnClickEventHandler = New ColumnClickEventHandler(AddressOf Me.lvSearchResults_ColumnClick)
                Dim handler2 As EventHandler = New EventHandler(AddressOf Me.lvSearchResults_SelectedIndexChanged)
                Dim handler3 As EventHandler = New EventHandler(AddressOf Me.lvSearchResults_GotFocus)
                If (Not Me._lvSearchResults Is Nothing) Then
                    RemoveHandler Me._lvSearchResults.ColumnClick, handler
                    RemoveHandler Me._lvSearchResults.SelectedIndexChanged, handler2
                    RemoveHandler Me._lvSearchResults.GotFocus, handler3
                End If
                Me._lvSearchResults = WithEventsValue
                If (Not Me._lvSearchResults Is Nothing) Then
                    AddHandler Me._lvSearchResults.ColumnClick, handler
                    AddHandler Me._lvSearchResults.SelectedIndexChanged, handler2
                    AddHandler Me._lvSearchResults.GotFocus, handler3
                End If
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

        Friend Overridable Property pbBanner As PictureBox
            <DebuggerNonUserCode> _
            Get
                Return Me._pbBanner
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As PictureBox)
                Me._pbBanner = WithEventsValue
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

        Friend Overridable Property txtTVDBID As TextBox
            <DebuggerNonUserCode> _
            Get
                Return Me._txtTVDBID
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As TextBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.txtTVDBID_TextChanged)
                Dim handler2 As KeyPressEventHandler = New KeyPressEventHandler(AddressOf Me.txtTVDBID_KeyPress)
                Dim handler3 As EventHandler = New EventHandler(AddressOf Me.txtTVDBID_GotFocus)
                If (Not Me._txtTVDBID Is Nothing) Then
                    RemoveHandler Me._txtTVDBID.TextChanged, handler
                    RemoveHandler Me._txtTVDBID.KeyPress, handler2
                    RemoveHandler Me._txtTVDBID.GotFocus, handler3
                End If
                Me._txtTVDBID = WithEventsValue
                If (Not Me._txtTVDBID Is Nothing) Then
                    AddHandler Me._txtTVDBID.TextChanged, handler
                    AddHandler Me._txtTVDBID.KeyPress, handler2
                    AddHandler Me._txtTVDBID.GotFocus, handler3
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
        <AccessedThroughProperty("colID")> _
        Private _colID As ColumnHeader
        <AccessedThroughProperty("colLang")> _
        Private _colLang As ColumnHeader
        <AccessedThroughProperty("colLev")> _
        Private _colLev As ColumnHeader
        <AccessedThroughProperty("colName")> _
        Private _colName As ColumnHeader
        <AccessedThroughProperty("colSLang")> _
        Private _colSLang As ColumnHeader
        <AccessedThroughProperty("Label1")> _
        Private _Label1 As Label
        <AccessedThroughProperty("Label2")> _
        Private _Label2 As Label
        <AccessedThroughProperty("Label3")> _
        Private _Label3 As Label
        <AccessedThroughProperty("lblAired")> _
        Private _lblAired As Label
        <AccessedThroughProperty("lblAiredHeader")> _
        Private _lblAiredHeader As Label
        <AccessedThroughProperty("lblPlotHeader")> _
        Private _lblPlotHeader As Label
        <AccessedThroughProperty("lblTitle")> _
        Private _lblTitle As Label
        <AccessedThroughProperty("lvSearchResults")> _
        Private _lvSearchResults As ListView
        Private _manualresult As TVSearchResults
        <AccessedThroughProperty("OK_Button")> _
        Private _OK_Button As Button
        <AccessedThroughProperty("pbBanner")> _
        Private _pbBanner As PictureBox
        <AccessedThroughProperty("PictureBox1")> _
        Private _PictureBox1 As PictureBox
        <AccessedThroughProperty("pnlLoading")> _
        Private _pnlLoading As Panel
        <AccessedThroughProperty("pnlTop")> _
        Private _pnlTop As Panel
        <AccessedThroughProperty("ProgressBar1")> _
        Private _ProgressBar1 As ProgressBar
        Private _skipdownload As Boolean
        <AccessedThroughProperty("txtOutline")> _
        Private _txtOutline As TextBox
        <AccessedThroughProperty("txtSearch")> _
        Private _txtSearch As TextBox
        <AccessedThroughProperty("txtTVDBID")> _
        Private _txtTVDBID As TextBox
        Private components As IContainer
        Private lvResultsSorter As ListViewColumnSorter
        Private sHTTP As HTTP
        Private sInfo As ScrapeInfo

        ' Nested Types
        <CompilerGenerated> _
        Friend Class _Closure$__20
            ' Methods
            <DebuggerNonUserCode> _
            Public Sub New()
            End Sub

            <DebuggerNonUserCode> _
            Public Sub New(ByVal other As _Closure$__20)
                If (Not other Is Nothing) Then
                    Me.$VB$Local_sLang = other.$VB$Local_sLang
                End If
            End Sub

            <CompilerGenerated> _
            Public Function _Lambda$__126(ByVal s As TVLanguage) As Boolean
                Return (s.ShortLang = Me.$VB$Local_sLang)
            End Function


            ' Fields
            Public $VB$Local_sLang As String
        End Class

        <StructLayout(LayoutKind.Sequential)> _
        Private Structure Arguments
            Public pURL As String
        End Structure

        <StructLayout(LayoutKind.Sequential)> _
        Private Structure Results
            Public Result As Image
        End Structure
    End Class
End Namespace

