Imports EmberAPI
Imports EmberAPI.MediaContainers
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
Imports System.Windows.Forms

Namespace EmberScraperModule
    <DesignerGenerated> _
    Public Class dlgTVChangeEp
        Inherits Form
        ' Methods
        Public Sub New()
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.dlgTVChangeEp_Load)
            dlgTVChangeEp.__ENCAddToList(Me)
            Me._episode = Nothing
            Me._tepisodes = New List(Of EpisodeDetails)
            Me.InitializeComponent
        End Sub

        <DebuggerNonUserCode> _
        Private Shared Sub __ENCAddToList(ByVal value As Object)
            Dim list As List(Of WeakReference) = dlgTVChangeEp.__ENCList
            SyncLock list
                If (dlgTVChangeEp.__ENCList.Count = dlgTVChangeEp.__ENCList.Capacity) Then
                    Dim index As Integer = 0
                    Dim num3 As Integer = (dlgTVChangeEp.__ENCList.Count - 1)
                    Dim i As Integer = 0
                    Do While (i <= num3)
                        Dim reference As WeakReference = dlgTVChangeEp.__ENCList.Item(i)
                        If reference.IsAlive Then
                            If (i <> index) Then
                                dlgTVChangeEp.__ENCList.Item(index) = dlgTVChangeEp.__ENCList.Item(i)
                            End If
                            index += 1
                        End If
                        i += 1
                    Loop
                    dlgTVChangeEp.__ENCList.RemoveRange(index, (dlgTVChangeEp.__ENCList.Count - index))
                    dlgTVChangeEp.__ENCList.Capacity = dlgTVChangeEp.__ENCList.Count
                End If
                dlgTVChangeEp.__ENCList.Add(New WeakReference(RuntimeHelpers.GetObjectValue(value)))
            End SyncLock
        End Sub

        <CompilerGenerated> _
        Private Shared Function _Lambda$__121(ByVal s As EpisodeDetails) As Integer
            Return s.Season
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__122(ByVal group As IGrouping(Of Integer, EpisodeDetails)) As Integer
            Return group.Key
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__124(ByVal s As EpisodeDetails) As Integer
            Return s.Episode
        End Function

        Private Sub Cancel_Button_Click(ByVal sender As Object, ByVal e As EventArgs)
            Me.DialogResult = DialogResult.Cancel
            Me.Close
        End Sub

        Private Sub ClearInfo()
            Me.pbPreview.Image = Nothing
            Me.lblTitle.Text = String.Empty
            Me.txtPlot.Text = String.Empty
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

        Private Sub dlgTVChangeEp_Load(ByVal sender As Object, ByVal e As EventArgs)
            Dim enumerator As IEnumerator(Of Integer)
            Dim e$__ As New _Closure$__19
            Me.SetUp
            Dim group As New ListViewGroup
            Dim item As New ListViewItem
            e$__.$VB$Local_tSeason = 0
            Try 
                enumerator = Me._tepisodes.GroupBy(Of EpisodeDetails, Integer)(New Func(Of EpisodeDetails, Integer)(AddressOf dlgTVChangeEp._Lambda$__121)).Select(Of IGrouping(Of Integer, EpisodeDetails), Integer)(New Func(Of IGrouping(Of Integer, EpisodeDetails), Integer)(AddressOf dlgTVChangeEp._Lambda$__122)).GetEnumerator
                Do While enumerator.MoveNext
                    Dim enumerator2 As IEnumerator(Of EpisodeDetails)
                    Dim current As Integer = enumerator.Current
                    e$__.$VB$Local_tSeason = current
                    group = New ListViewGroup With { _
                        .Header = String.Format(Master.eLang.GetString(&H2D6, "Season {0}", True), e$__.$VB$Local_tSeason) _
                    }
                    Me.lvEpisodes.Groups.Add(group)
                    Try 
                        enumerator2 = Me._tepisodes.Where(Of EpisodeDetails)(New Func(Of EpisodeDetails, Boolean)(AddressOf e$__._Lambda$__123)).OrderBy(Of EpisodeDetails, Integer)(New Func(Of EpisodeDetails, Integer)(AddressOf dlgTVChangeEp._Lambda$__124)).GetEnumerator
                        Do While enumerator2.MoveNext
                            Dim details As EpisodeDetails = enumerator2.Current
                            item = Me.lvEpisodes.Items.Add(details.Episode.ToString)
                            item.Tag = details
                            item.SubItems.Add(details.Title)
                            group.Items.Add(item)
                        Loop
                    Finally
                        If (Not enumerator2 Is Nothing) Then
                            enumerator2.Dispose
                        End If
                    End Try
                Loop
            Finally
                If (Not enumerator Is Nothing) Then
                    enumerator.Dispose
                End If
            End Try
        End Sub

        <DebuggerStepThrough> _
        Private Sub InitializeComponent()
            Me.OK_Button = New Button
            Me.Cancel_Button = New Button
            Me.lvEpisodes = New ListView
            Me.colEpisode = New ColumnHeader
            Me.colTitle = New ColumnHeader
            Me.pbPreview = New PictureBox
            Me.lblTitle = New Label
            Me.txtPlot = New TextBox
            DirectCast(Me.pbPreview, ISupportInitialize).BeginInit
            Me.SuspendLayout
            Dim point2 As New Point(&H1F9, &H11E)
            Me.OK_Button.Location = point2
            Me.OK_Button.Name = "OK_Button"
            Dim size2 As New Size(&H43, &H17)
            Me.OK_Button.Size = size2
            Me.OK_Button.TabIndex = 0
            Me.OK_Button.Text = "OK"
            Me.Cancel_Button.DialogResult = DialogResult.Cancel
            point2 = New Point(&H23F, &H11E)
            Me.Cancel_Button.Location = point2
            Me.Cancel_Button.Name = "Cancel_Button"
            size2 = New Size(&H43, &H17)
            Me.Cancel_Button.Size = size2
            Me.Cancel_Button.TabIndex = 1
            Me.Cancel_Button.Text = "Cancel"
            Me.lvEpisodes.Columns.AddRange(New ColumnHeader() { Me.colEpisode, Me.colTitle })
            Me.lvEpisodes.FullRowSelect = True
            Me.lvEpisodes.HideSelection = False
            point2 = New Point(3, 3)
            Me.lvEpisodes.Location = point2
            Me.lvEpisodes.MultiSelect = False
            Me.lvEpisodes.Name = "lvEpisodes"
            size2 = New Size(&H166, &H116)
            Me.lvEpisodes.Size = size2
            Me.lvEpisodes.TabIndex = 2
            Me.lvEpisodes.UseCompatibleStateImageBehavior = False
            Me.lvEpisodes.View = View.Details
            Me.colEpisode.Text = "Episode"
            Me.colTitle.Text = "Title"
            Me.colTitle.Width = &H114
            point2 = New Point(&H1A1, 12)
            Me.pbPreview.Location = point2
            Me.pbPreview.Name = "pbPreview"
            size2 = New Size(&HAE, &H75)
            Me.pbPreview.Size = size2
            Me.pbPreview.SizeMode = PictureBoxSizeMode.Zoom
            Me.pbPreview.TabIndex = 3
            Me.pbPreview.TabStop = False
            Me.lblTitle.Font = New Font("Segoe UI", 9.75!, FontStyle.Bold, GraphicsUnit.Point, 0)
            point2 = New Point(&H16F, 140)
            Me.lblTitle.Location = point2
            Me.lblTitle.Name = "lblTitle"
            size2 = New Size(&H113, &H15)
            Me.lblTitle.Size = size2
            Me.lblTitle.TabIndex = 4
            Me.txtPlot.BackColor = SystemColors.Control
            Me.txtPlot.BorderStyle = BorderStyle.None
            point2 = New Point(&H16E, &HA9)
            Me.txtPlot.Location = point2
            Me.txtPlot.Multiline = True
            Me.txtPlot.Name = "txtPlot"
            Me.txtPlot.ScrollBars = ScrollBars.Vertical
            size2 = New Size(&H114, &H6F)
            Me.txtPlot.Size = size2
            Me.txtPlot.TabIndex = 5
            Me.AcceptButton = Me.OK_Button
            Dim ef2 As New SizeF(96!, 96!)
            Me.AutoScaleDimensions = ef2
            Me.AutoScaleMode = AutoScaleMode.Dpi
            Me.CancelButton = Me.Cancel_Button
            size2 = New Size(&H286, &H13B)
            Me.ClientSize = size2
            Me.ControlBox = False
            Me.Controls.Add(Me.txtPlot)
            Me.Controls.Add(Me.lblTitle)
            Me.Controls.Add(Me.pbPreview)
            Me.Controls.Add(Me.lvEpisodes)
            Me.Controls.Add(Me.OK_Button)
            Me.Controls.Add(Me.Cancel_Button)
            Me.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            Me.FormBorderStyle = FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "dlgTVChangeEp"
            Me.ShowInTaskbar = False
            Me.StartPosition = FormStartPosition.CenterParent
            Me.Text = "Change Episode"
            DirectCast(Me.pbPreview, ISupportInitialize).EndInit
            Me.ResumeLayout(False)
            Me.PerformLayout
        End Sub

        Private Sub lvEpisodes_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
            Me.ClearInfo
            If ((Me.lvEpisodes.SelectedItems.Count > 0) AndAlso Not Information.IsNothing(RuntimeHelpers.GetObjectValue(Me.lvEpisodes.SelectedItems.Item(0).Tag))) Then
                Me._episode = DirectCast(Me.lvEpisodes.SelectedItems.Item(0).Tag, EpisodeDetails)
                If Not Information.IsNothing(Me._episode.Poster.Image) Then
                    Me.pbPreview.Image = Me._episode.Poster.Image
                ElseIf (Not String.IsNullOrEmpty(Me._episode.LocalFile) AndAlso File.Exists(Me._episode.LocalFile)) Then
                    Me._episode.Poster.FromFile(Me._episode.LocalFile)
                    If Not Information.IsNothing(Me._episode.Poster.Image) Then
                        Me.pbPreview.Image = Me._episode.Poster.Image
                    End If
                ElseIf Not String.IsNullOrEmpty(Me._episode.PosterURL) Then
                    Me._episode.Poster.FromWeb(Me._episode.PosterURL)
                    If Not Information.IsNothing(Me._episode.Poster.Image) Then
                        Directory.CreateDirectory(Directory.GetParent(Me._episode.LocalFile).FullName)
                        Me._episode.Poster.Save(Me._episode.LocalFile, 0)
                        Me.pbPreview.Image = Me._episode.Poster.Image
                    End If
                End If
                Me.lblTitle.Text = Me._episode.Title
                Me.txtPlot.Text = Me._episode.Plot
            End If
        End Sub

        Private Sub OK_Button_Click(ByVal sender As Object, ByVal e As EventArgs)
            Me.DialogResult = DialogResult.OK
            Me.Close
        End Sub

        Private Sub SetUp()
            Me.Text = Master.eLang.GetString(&H304, "Change Episode", True)
            Me.lvEpisodes.Columns.Item(0).Text = Master.eLang.GetString(&H2D7, "Episode", True)
            Me.lvEpisodes.Columns.Item(1).Text = Master.eLang.GetString(&H15, "Title", True)
            Me.OK_Button.Text = Master.eLang.GetString(&HB3, "OK", True)
            Me.Cancel_Button.Text = Master.eLang.GetString(&HA7, "Cancel", True)
        End Sub

        Public Function ShowDialog(ByVal tEpisodes As List(Of EpisodeDetails)) As EpisodeDetails
            Me._tepisodes = tEpisodes
            If (MyBase.ShowDialog = DialogResult.OK) Then
                Return Me._episode
            End If
            Return Nothing
        End Function


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

        Friend Overridable Property colEpisode As ColumnHeader
            <DebuggerNonUserCode> _
            Get
                Return Me._colEpisode
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As ColumnHeader)
                Me._colEpisode = WithEventsValue
            End Set
        End Property

        Friend Overridable Property colTitle As ColumnHeader
            <DebuggerNonUserCode> _
            Get
                Return Me._colTitle
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As ColumnHeader)
                Me._colTitle = WithEventsValue
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

        Friend Overridable Property lvEpisodes As ListView
            <DebuggerNonUserCode> _
            Get
                Return Me._lvEpisodes
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As ListView)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.lvEpisodes_SelectedIndexChanged)
                If (Not Me._lvEpisodes Is Nothing) Then
                    RemoveHandler Me._lvEpisodes.SelectedIndexChanged, handler
                End If
                Me._lvEpisodes = WithEventsValue
                If (Not Me._lvEpisodes Is Nothing) Then
                    AddHandler Me._lvEpisodes.SelectedIndexChanged, handler
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

        Friend Overridable Property pbPreview As PictureBox
            <DebuggerNonUserCode> _
            Get
                Return Me._pbPreview
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As PictureBox)
                Me._pbPreview = WithEventsValue
            End Set
        End Property

        Friend Overridable Property txtPlot As TextBox
            <DebuggerNonUserCode> _
            Get
                Return Me._txtPlot
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As TextBox)
                Me._txtPlot = WithEventsValue
            End Set
        End Property


        ' Fields
        Private Shared __ENCList As List(Of WeakReference) = New List(Of WeakReference)
        <AccessedThroughProperty("Cancel_Button")> _
        Private _Cancel_Button As Button
        <AccessedThroughProperty("colEpisode")> _
        Private _colEpisode As ColumnHeader
        <AccessedThroughProperty("colTitle")> _
        Private _colTitle As ColumnHeader
        Private _episode As EpisodeDetails
        <AccessedThroughProperty("lblTitle")> _
        Private _lblTitle As Label
        <AccessedThroughProperty("lvEpisodes")> _
        Private _lvEpisodes As ListView
        <AccessedThroughProperty("OK_Button")> _
        Private _OK_Button As Button
        <AccessedThroughProperty("pbPreview")> _
        Private _pbPreview As PictureBox
        Private _tepisodes As List(Of EpisodeDetails)
        <AccessedThroughProperty("txtPlot")> _
        Private _txtPlot As TextBox
        Private components As IContainer

        ' Nested Types
        <CompilerGenerated> _
        Friend Class _Closure$__19
            ' Methods
            <DebuggerNonUserCode> _
            Public Sub New()
            End Sub

            <DebuggerNonUserCode> _
            Public Sub New(ByVal other As _Closure$__19)
                If (Not other Is Nothing) Then
                    Me.$VB$Local_tSeason = other.$VB$Local_tSeason
                End If
            End Sub

            <CompilerGenerated> _
            Public Function _Lambda$__123(ByVal s As EpisodeDetails) As Boolean
                Return (s.Season = Me.$VB$Local_tSeason)
            End Function


            ' Fields
            Public $VB$Local_tSeason As Integer
        End Class
    End Class
End Namespace

