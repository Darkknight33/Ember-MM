Imports EmberAPI
Imports EmberScraperModule.YouTube
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Drawing
Imports System.Linq
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms

Namespace EmberScraperModule
    <DesignerGenerated> _
    Public Class dlgTrailerFormat
        Inherits Form
        ' Methods
        <DebuggerNonUserCode> _
        Public Sub New()
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.dlgTrailerFormat_Load)
            dlgTrailerFormat.__ENCAddToList(Me)
            Me.InitializeComponent
        End Sub

        <DebuggerNonUserCode> _
        Private Shared Sub __ENCAddToList(ByVal value As Object)
            Dim list As List(Of WeakReference) = dlgTrailerFormat.__ENCList
            SyncLock list
                If (dlgTrailerFormat.__ENCList.Count = dlgTrailerFormat.__ENCList.Capacity) Then
                    Dim index As Integer = 0
                    Dim num3 As Integer = (dlgTrailerFormat.__ENCList.Count - 1)
                    Dim i As Integer = 0
                    Do While (i <= num3)
                        Dim reference As WeakReference = dlgTrailerFormat.__ENCList.Item(i)
                        If reference.IsAlive Then
                            If (i <> index) Then
                                dlgTrailerFormat.__ENCList.Item(index) = dlgTrailerFormat.__ENCList.Item(i)
                            End If
                            index += 1
                        End If
                        i += 1
                    Loop
                    dlgTrailerFormat.__ENCList.RemoveRange(index, (dlgTrailerFormat.__ENCList.Count - index))
                    dlgTrailerFormat.__ENCList.Capacity = dlgTrailerFormat.__ENCList.Count
                End If
                dlgTrailerFormat.__ENCList.Add(New WeakReference(RuntimeHelpers.GetObjectValue(value)))
            End SyncLock
        End Sub

        Private Sub Cancel_Button_Click(ByVal sender As Object, ByVal e As EventArgs)
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

        Private Sub dlgTrailerFormat_Load(ByVal sender As Object, ByVal e As EventArgs)
            Try 
                Me.SetUp
                Me.lstFormats.DataSource = Nothing
                Me.YouTube = New Scraper
                Me.YouTube.GetVideoLinksAsync(Me._yturl)
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Interaction.MsgBox(Master.eLang.GetString(&H47, "The video format links could not be retrieved.", False), MsgBoxStyle.Critical, Master.eLang.GetString(&H48, "Error Retrieving Video Format Links", False))
                ProjectData.ClearProjectError
            End Try
        End Sub

        <DebuggerStepThrough> _
        Private Sub InitializeComponent()
            Me.OK_Button = New Button
            Me.Cancel_Button = New Button
            Me.lstFormats = New ListBox
            Me.GroupBox1 = New GroupBox
            Me.pnlStatus = New Panel
            Me.lblStatus = New Label
            Me.pbStatus = New ProgressBar
            Me.Panel1 = New Panel
            Me.GroupBox1.SuspendLayout
            Me.pnlStatus.SuspendLayout
            Me.Panel1.SuspendLayout
            Me.SuspendLayout
            Me.OK_Button.Enabled = False
            Me.OK_Button.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, &HEE)
            Dim point2 As New Point(&H54, &H75)
            Me.OK_Button.Location = point2
            Me.OK_Button.Name = "OK_Button"
            Dim size2 As New Size(&H43, &H17)
            Me.OK_Button.Size = size2
            Me.OK_Button.TabIndex = 0
            Me.OK_Button.Text = "OK"
            Me.Cancel_Button.DialogResult = DialogResult.Cancel
            Me.Cancel_Button.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, &HEE)
            point2 = New Point(&H9A, &H75)
            Me.Cancel_Button.Location = point2
            Me.Cancel_Button.Name = "Cancel_Button"
            size2 = New Size(&H43, &H17)
            Me.Cancel_Button.Size = size2
            Me.Cancel_Button.TabIndex = 1
            Me.Cancel_Button.Text = "Cancel"
            Me.lstFormats.Enabled = False
            Me.lstFormats.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, &HEE)
            Me.lstFormats.FormattingEnabled = True
            point2 = New Point(&H11, &H13)
            Me.lstFormats.Location = point2
            Me.lstFormats.Name = "lstFormats"
            size2 = New Size(&H4E, &H45)
            Me.lstFormats.Size = size2
            Me.lstFormats.TabIndex = 3
            Me.GroupBox1.Controls.Add(Me.lstFormats)
            Me.GroupBox1.Font = New Font("Segoe UI", 8.25!, FontStyle.Bold, GraphicsUnit.Point, &HEE)
            point2 = New Point(&H35, 2)
            Me.GroupBox1.Location = point2
            Me.GroupBox1.Name = "GroupBox1"
            size2 = New Size(&H72, 100)
            Me.GroupBox1.Size = size2
            Me.GroupBox1.TabIndex = 4
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Available Formats"
            Me.pnlStatus.BackColor = Color.White
            Me.pnlStatus.BorderStyle = BorderStyle.FixedSingle
            Me.pnlStatus.Controls.Add(Me.lblStatus)
            Me.pnlStatus.Controls.Add(Me.pbStatus)
            point2 = New Point(10, &H1D)
            Me.pnlStatus.Location = point2
            Me.pnlStatus.Name = "pnlStatus"
            size2 = New Size(200, &H36)
            Me.pnlStatus.Size = size2
            Me.pnlStatus.TabIndex = 70
            Me.lblStatus.AutoSize = True
            Me.lblStatus.Font = New Font("Segoe UI", 8.25!, FontStyle.Bold, GraphicsUnit.Point, &HEE)
            point2 = New Point(3, 10)
            Me.lblStatus.Location = point2
            Me.lblStatus.Name = "lblStatus"
            size2 = New Size(&H93, 13)
            Me.lblStatus.Size = size2
            Me.lblStatus.TabIndex = 1
            Me.lblStatus.Text = "Getting available formats..."
            Me.lblStatus.TextAlign = ContentAlignment.MiddleLeft
            point2 = New Point(3, &H1D)
            Me.pbStatus.Location = point2
            Me.pbStatus.MarqueeAnimationSpeed = &H19
            Me.pbStatus.Name = "pbStatus"
            size2 = New Size(&HC0, &H11)
            Me.pbStatus.Size = size2
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pbStatus.TabIndex = 0
            Me.Panel1.BackColor = Color.White
            Me.Panel1.Controls.Add(Me.pnlStatus)
            Me.Panel1.Controls.Add(Me.GroupBox1)
            point2 = New Point(2, 4)
            Me.Panel1.Location = point2
            Me.Panel1.Name = "Panel1"
            size2 = New Size(220, &H6F)
            Me.Panel1.Size = size2
            Me.Panel1.TabIndex = &H47
            Me.AcceptButton = Me.OK_Button
            Dim ef2 As New SizeF(96!, 96!)
            Me.AutoScaleDimensions = ef2
            Me.AutoScaleMode = AutoScaleMode.Dpi
            Me.CancelButton = Me.Cancel_Button
            size2 = New Size(&HE0, &H8F)
            Me.ClientSize = size2
            Me.Controls.Add(Me.Panel1)
            Me.Controls.Add(Me.Cancel_Button)
            Me.Controls.Add(Me.OK_Button)
            Me.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            Me.FormBorderStyle = FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "dlgTrailerFormat"
            Me.ShowIcon = False
            Me.ShowInTaskbar = False
            Me.StartPosition = FormStartPosition.CenterParent
            Me.Text = "Select Format"
            Me.GroupBox1.ResumeLayout(False)
            Me.pnlStatus.ResumeLayout(False)
            Me.pnlStatus.PerformLayout
            Me.Panel1.ResumeLayout(False)
            Me.ResumeLayout(False)
        End Sub

        Private Sub lstFormats_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
            Try 
                Me._selectedformaturl = DirectCast(Me.lstFormats.SelectedItem, VideoLinkItem).URL
                If (Me.lstFormats.SelectedItems.Count > 0) Then
                    Me.OK_Button.Enabled = True
                Else
                    Me.OK_Button.Enabled = False
                End If
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Private Sub OK_Button_Click(ByVal sender As Object, ByVal e As EventArgs)
            Me.DialogResult = DialogResult.OK
            Me.Close
        End Sub

        Private Sub SetUp()
            Me.Text = Master.eLang.GetString(&H49, "Select Format", False)
            Me.lblStatus.Text = Master.eLang.GetString(&H4A, "Getting available formats...", False)
            Me.GroupBox1.Text = Master.eLang.GetString(&H4B, "Available Formats", False)
            Me.OK_Button.Text = Master.eLang.GetString(&HB3, "OK", True)
            Me.Cancel_Button.Text = Master.eLang.GetString(&HA7, "Cancel", True)
        End Sub

        Public Function ShowDialog(ByVal YTURL As String) As String
            Me._yturl = YTURL
            If (MyBase.ShowDialog = DialogResult.OK) Then
                Return Me._selectedformaturl
            End If
            Return String.Empty
        End Function

        Private Sub YouTube_VideoLinksRetrieved(ByVal bSuccess As Boolean)
            Try 
                If bSuccess Then
                    Me.lstFormats.DataSource = Me.YouTube.VideoLinks.Values.ToList(Of VideoLinkItem)()
                    Me.lstFormats.DisplayMember = "Description"
                    Me.lstFormats.ValueMember = "URL"
                    If Me.YouTube.VideoLinks.ContainsKey(Master.eSettings.PreferredTrailerQuality) Then
                        Me.lstFormats.SelectedIndex = Me.YouTube.VideoLinks.IndexOfKey(Master.eSettings.PreferredTrailerQuality)
                    ElseIf (Me.lstFormats.Items.Count = 1) Then
                        Me.lstFormats.SelectedIndex = 0
                    End If
                End If
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            Finally
                Me.pnlStatus.Visible = False
                Me.lstFormats.Enabled = True
            End Try
        End Sub


        ' Properties
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

        Friend Overridable Property lstFormats As ListBox
            <DebuggerNonUserCode> _
            Get
                Return Me._lstFormats
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As ListBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.lstFormats_SelectedIndexChanged)
                If (Not Me._lstFormats Is Nothing) Then
                    RemoveHandler Me._lstFormats.SelectedIndexChanged, handler
                End If
                Me._lstFormats = WithEventsValue
                If (Not Me._lstFormats Is Nothing) Then
                    AddHandler Me._lstFormats.SelectedIndexChanged, handler
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

        Private Overridable Property YouTube As Scraper
            <DebuggerNonUserCode> _
            Get
                Return Me._YouTube
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Scraper)
                Dim handler As VideoLinksRetrievedEventHandler = New VideoLinksRetrievedEventHandler(AddressOf Me.YouTube_VideoLinksRetrieved)
                If (Not Me._YouTube Is Nothing) Then
                    RemoveHandler Me._YouTube.VideoLinksRetrieved, handler
                End If
                Me._YouTube = WithEventsValue
                If (Not Me._YouTube Is Nothing) Then
                    AddHandler Me._YouTube.VideoLinksRetrieved, handler
                End If
            End Set
        End Property


        ' Fields
        Private Shared __ENCList As List(Of WeakReference) = New List(Of WeakReference)
        <AccessedThroughProperty("Cancel_Button")> _
        Private _Cancel_Button As Button
        <AccessedThroughProperty("GroupBox1")> _
        Private _GroupBox1 As GroupBox
        <AccessedThroughProperty("lblStatus")> _
        Private _lblStatus As Label
        <AccessedThroughProperty("lstFormats")> _
        Private _lstFormats As ListBox
        <AccessedThroughProperty("OK_Button")> _
        Private _OK_Button As Button
        <AccessedThroughProperty("Panel1")> _
        Private _Panel1 As Panel
        <AccessedThroughProperty("pbStatus")> _
        Private _pbStatus As ProgressBar
        <AccessedThroughProperty("pnlStatus")> _
        Private _pnlStatus As Panel
        Private _selectedformaturl As String
        <AccessedThroughProperty("YouTube")> _
        Private _YouTube As Scraper
        Private _yturl As String
        Private components As IContainer
    End Class
End Namespace

