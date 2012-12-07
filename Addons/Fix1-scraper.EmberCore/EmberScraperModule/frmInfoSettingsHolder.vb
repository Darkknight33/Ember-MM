Imports EmberAPI
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
    Public Class frmInfoSettingsHolder
        Inherits Form
        ' Events
        Public Custom Event ModuleSettingsChanged As ModuleSettingsChangedEventHandler
        Public Custom Event SetupScraperChanged As SetupScraperChangedEventHandler

        ' Methods
        Public Sub New()
            frmInfoSettingsHolder.__ENCAddToList(Me)
            Me.InitializeComponent
            Me.SetUp
        End Sub

        <DebuggerNonUserCode> _
        Private Shared Sub __ENCAddToList(ByVal value As Object)
            Dim list As List(Of WeakReference) = frmInfoSettingsHolder.__ENCList
            SyncLock list
                If (frmInfoSettingsHolder.__ENCList.Count = frmInfoSettingsHolder.__ENCList.Capacity) Then
                    Dim index As Integer = 0
                    Dim num3 As Integer = (frmInfoSettingsHolder.__ENCList.Count - 1)
                    Dim i As Integer = 0
                    Do While (i <= num3)
                        Dim reference As WeakReference = frmInfoSettingsHolder.__ENCList.Item(i)
                        If reference.IsAlive Then
                            If (i <> index) Then
                                frmInfoSettingsHolder.__ENCList.Item(index) = frmInfoSettingsHolder.__ENCList.Item(i)
                            End If
                            index += 1
                        End If
                        i += 1
                    Loop
                    frmInfoSettingsHolder.__ENCList.RemoveRange(index, (frmInfoSettingsHolder.__ENCList.Count - index))
                    frmInfoSettingsHolder.__ENCList.Capacity = frmInfoSettingsHolder.__ENCList.Count
                End If
                frmInfoSettingsHolder.__ENCList.Add(New WeakReference(RuntimeHelpers.GetObjectValue(value)))
            End SyncLock
        End Sub

        <CompilerGenerated> _
        Private Shared Function _Lambda$__1(ByVal p As _externalScraperModuleClass) As Boolean
            Return (p.AssemblyName = EmberNativeScraperModule._AssemblyName)
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__2(ByVal y As _externalScraperModuleClass) As Boolean
            Return y.ProcessorModule.IsScraper
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__4(ByVal p As _externalScraperModuleClass) As Boolean
            Return (p.AssemblyName = EmberNativeScraperModule._AssemblyName)
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__5(ByVal p As _externalScraperModuleClass) As Boolean
            Return (p.AssemblyName = EmberNativeScraperModule._AssemblyName)
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__7(ByVal p As _externalScraperModuleClass) As Boolean
            Return (p.AssemblyName = EmberNativeScraperModule._AssemblyName)
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__8(ByVal p As _externalScraperModuleClass) As Boolean
            Return (p.AssemblyName = EmberNativeScraperModule._AssemblyName)
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__9(ByVal y As _externalScraperModuleClass) As Boolean
            Return y.ProcessorModule.IsScraper
        End Function

        Private Sub btnDown_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim e$__ As New _Closure$__1 With { _
                .$VB$Local_order = ModulesManager.Instance.externalScrapersModules.FirstOrDefault(Of _externalScraperModuleClass)(New Func(Of _externalScraperModuleClass, Boolean)(AddressOf frmInfoSettingsHolder._Lambda$__1)).ScraperOrder _
            }
            If (e$__.$VB$Local_order < (ModulesManager.Instance.externalScrapersModules.Where(Of _externalScraperModuleClass)(New Func(Of _externalScraperModuleClass, Boolean)(AddressOf frmInfoSettingsHolder._Lambda$__2)).Count(Of _externalScraperModuleClass)() - 1)) Then
                ModulesManager.Instance.externalScrapersModules.FirstOrDefault(Of _externalScraperModuleClass)(New Func(Of _externalScraperModuleClass, Boolean)(AddressOf e$__._Lambda$__3)).ScraperOrder = e$__.$VB$Local_order
                ModulesManager.Instance.externalScrapersModules.FirstOrDefault(Of _externalScraperModuleClass)(New Func(Of _externalScraperModuleClass, Boolean)(AddressOf frmInfoSettingsHolder._Lambda$__4)).ScraperOrder = (e$__.$VB$Local_order + 1)
                Dim setupScraperChangedEvent As SetupScraperChangedEventHandler = Me.SetupScraperChangedEvent
                If (Not setupScraperChangedEvent Is Nothing) Then
                    setupScraperChangedEvent.Invoke(Me.cbEnabled.Checked, 1)
                End If
                Me.orderChanged
            End If
        End Sub

        Private Sub btnUp_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim e$__ As New _Closure$__2 With { _
                .$VB$Local_order = ModulesManager.Instance.externalScrapersModules.FirstOrDefault(Of _externalScraperModuleClass)(New Func(Of _externalScraperModuleClass, Boolean)(AddressOf frmInfoSettingsHolder._Lambda$__5)).ScraperOrder _
            }
            If (e$__.$VB$Local_order > 0) Then
                ModulesManager.Instance.externalScrapersModules.FirstOrDefault(Of _externalScraperModuleClass)(New Func(Of _externalScraperModuleClass, Boolean)(AddressOf e$__._Lambda$__6)).ScraperOrder = e$__.$VB$Local_order
                ModulesManager.Instance.externalScrapersModules.FirstOrDefault(Of _externalScraperModuleClass)(New Func(Of _externalScraperModuleClass, Boolean)(AddressOf frmInfoSettingsHolder._Lambda$__7)).ScraperOrder = (e$__.$VB$Local_order - 1)
                Dim setupScraperChangedEvent As SetupScraperChangedEventHandler = Me.SetupScraperChangedEvent
                If (Not setupScraperChangedEvent Is Nothing) Then
                    setupScraperChangedEvent.Invoke(Me.cbEnabled.Checked, -1)
                End If
                Me.orderChanged
            End If
        End Sub

        Private Sub cbEnabled_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim setupScraperChangedEvent As SetupScraperChangedEventHandler = Me.SetupScraperChangedEvent
            If (Not setupScraperChangedEvent Is Nothing) Then
                setupScraperChangedEvent.Invoke(Me.cbEnabled.Checked, 0)
            End If
        End Sub

        Private Sub chkCast_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
        End Sub

        Private Sub chkCertification_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
        End Sub

        Private Sub chkCountry_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
        End Sub

        Private Sub chkCrew_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
        End Sub

        Private Sub chkDirector_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
        End Sub

        Private Sub chkFullCast_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
        End Sub

        Private Sub chkFullCrew_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
            Me.chkProducers.Enabled = Me.chkFullCrew.Checked
            Me.chkMusicBy.Enabled = Me.chkFullCrew.Checked
            Me.chkCrew.Enabled = Me.chkFullCrew.Checked
            If Not Me.chkFullCrew.Checked Then
                Me.chkProducers.Checked = False
                Me.chkMusicBy.Checked = False
                Me.chkCrew.Checked = False
            End If
        End Sub

        Private Sub chkGenre_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
        End Sub

        Private Sub chkMPAA_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
        End Sub

        Private Sub chkMusicBy_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
        End Sub

        Private Sub chkOFDBGenre_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
        End Sub

        Private Sub chkOFDBOutline_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
        End Sub

        Private Sub chkOFDBPlot_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
        End Sub

        Private Sub chkOFDBTitle_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
        End Sub

        Private Sub chkOutline_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
        End Sub

        Private Sub chkPlot_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
        End Sub

        Private Sub chkProducers_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
        End Sub

        Private Sub chkRating_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
        End Sub

        Private Sub chkRelease_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
        End Sub

        Private Sub chkRuntime_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
        End Sub

        Private Sub chkStudio_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
        End Sub

        Private Sub chkTagline_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
        End Sub

        Private Sub chkTitle_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
        End Sub

        Private Sub chkTop250_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
        End Sub

        Private Sub chkTrailer_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
        End Sub

        Private Sub chkVotes_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
        End Sub

        Private Sub chkWriters_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
        End Sub

        Private Sub chkYear_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
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

        <DebuggerStepThrough> _
        Private Sub InitializeComponent()
            Dim manager As New ComponentResourceManager(GetType(frmInfoSettingsHolder))
            Me.lblVersion = New Label
            Me.GroupBox30 = New GroupBox
            Me.Label18 = New Label
            Me.txtIMDBURL = New TextBox
            Me.GroupBox15 = New GroupBox
            Me.chkOFDBGenre = New CheckBox
            Me.chkOFDBPlot = New CheckBox
            Me.chkOFDBOutline = New CheckBox
            Me.chkOFDBTitle = New CheckBox
            Me.cbEnabled = New CheckBox
            Me.Panel1 = New Panel
            Me.Label2 = New Label
            Me.btnDown = New Button
            Me.btnUp = New Button
            Me.pnlSettings = New Panel
            Me.Label1 = New Label
            Me.PictureBox1 = New PictureBox
            Me.gbOptions = New GroupBox
            Me.chkCertification = New CheckBox
            Me.chkCountry = New CheckBox
            Me.chkTop250 = New CheckBox
            Me.chkCrew = New CheckBox
            Me.chkMusicBy = New CheckBox
            Me.chkProducers = New CheckBox
            Me.chkFullCast = New CheckBox
            Me.chkWriters = New CheckBox
            Me.chkStudio = New CheckBox
            Me.chkRuntime = New CheckBox
            Me.chkFullCrew = New CheckBox
            Me.chkPlot = New CheckBox
            Me.chkOutline = New CheckBox
            Me.chkGenre = New CheckBox
            Me.chkDirector = New CheckBox
            Me.chkTagline = New CheckBox
            Me.chkCast = New CheckBox
            Me.chkVotes = New CheckBox
            Me.chkTrailer = New CheckBox
            Me.chkRating = New CheckBox
            Me.chkRelease = New CheckBox
            Me.chkMPAA = New CheckBox
            Me.chkYear = New CheckBox
            Me.chkTitle = New CheckBox
            Me.GroupBox30.SuspendLayout
            Me.GroupBox15.SuspendLayout
            Me.Panel1.SuspendLayout
            Me.pnlSettings.SuspendLayout
            DirectCast(Me.PictureBox1, ISupportInitialize).BeginInit
            Me.gbOptions.SuspendLayout
            Me.SuspendLayout
            Dim point2 As New Point(&H11E, &H189)
            Me.lblVersion.Location = point2
            Me.lblVersion.Name = "lblVersion"
            Dim size2 As New Size(90, &H10)
            Me.lblVersion.Size = size2
            Me.lblVersion.TabIndex = &H4A
            Me.lblVersion.Text = "Version:"
            Me.GroupBox30.Controls.Add(Me.Label18)
            Me.GroupBox30.Controls.Add(Me.txtIMDBURL)
            Me.GroupBox30.Font = New Font("Segoe UI", 8.25!, FontStyle.Bold, GraphicsUnit.Point, &HEE)
            point2 = New Point(8, 40)
            Me.GroupBox30.Location = point2
            Me.GroupBox30.Name = "GroupBox30"
            size2 = New Size(&HD5, &H45)
            Me.GroupBox30.Size = size2
            Me.GroupBox30.TabIndex = &H4F
            Me.GroupBox30.TabStop = False
            Me.GroupBox30.Text = "IMDB"
            Me.Label18.AutoSize = True
            Me.Label18.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(6, &H12)
            Me.Label18.Location = point2
            Me.Label18.Name = "Label18"
            size2 = New Size(&H49, 13)
            Me.Label18.Size = size2
            Me.Label18.TabIndex = &H3E
            Me.Label18.Text = "IMDB Mirror:"
            Me.txtIMDBURL.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(8, &H20)
            Me.txtIMDBURL.Location = point2
            Me.txtIMDBURL.Name = "txtIMDBURL"
            size2 = New Size(&HC0, &H16)
            Me.txtIMDBURL.Size = size2
            Me.txtIMDBURL.TabIndex = 10
            Me.GroupBox15.Controls.Add(Me.chkOFDBGenre)
            Me.GroupBox15.Controls.Add(Me.chkOFDBPlot)
            Me.GroupBox15.Controls.Add(Me.chkOFDBOutline)
            Me.GroupBox15.Controls.Add(Me.chkOFDBTitle)
            Me.GroupBox15.Font = New Font("Segoe UI", 8.25!, FontStyle.Bold, GraphicsUnit.Point, &HEE)
            point2 = New Point(8, &H72)
            Me.GroupBox15.Location = point2
            Me.GroupBox15.Name = "GroupBox15"
            size2 = New Size(&HD5, &H57)
            Me.GroupBox15.Size = size2
            Me.GroupBox15.TabIndex = &H4E
            Me.GroupBox15.TabStop = False
            Me.GroupBox15.Text = "OFDB (German)"
            Me.chkOFDBGenre.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(6, &H41)
            Me.chkOFDBGenre.Location = point2
            Me.chkOFDBGenre.Name = "chkOFDBGenre"
            size2 = New Size(&HA8, &H11)
            Me.chkOFDBGenre.Size = size2
            Me.chkOFDBGenre.TabIndex = 3
            Me.chkOFDBGenre.Text = "Use OFDB Genre"
            Me.chkOFDBGenre.UseVisualStyleBackColor = True
            Me.chkOFDBPlot.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(6, &H31)
            Me.chkOFDBPlot.Location = point2
            Me.chkOFDBPlot.Name = "chkOFDBPlot"
            size2 = New Size(&HA8, &H11)
            Me.chkOFDBPlot.Size = size2
            Me.chkOFDBPlot.TabIndex = 2
            Me.chkOFDBPlot.Text = "Use OFDB Plot"
            Me.chkOFDBPlot.UseVisualStyleBackColor = True
            Me.chkOFDBOutline.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(6, &H21)
            Me.chkOFDBOutline.Location = point2
            Me.chkOFDBOutline.Name = "chkOFDBOutline"
            size2 = New Size(&HA8, &H11)
            Me.chkOFDBOutline.Size = size2
            Me.chkOFDBOutline.TabIndex = 1
            Me.chkOFDBOutline.Text = "Use OFDB Outline"
            Me.chkOFDBOutline.UseVisualStyleBackColor = True
            Me.chkOFDBTitle.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(6, &H11)
            Me.chkOFDBTitle.Location = point2
            Me.chkOFDBTitle.Name = "chkOFDBTitle"
            size2 = New Size(&HA8, &H11)
            Me.chkOFDBTitle.Size = size2
            Me.chkOFDBTitle.TabIndex = 0
            Me.chkOFDBTitle.Text = "Use OFDB Title"
            Me.chkOFDBTitle.UseVisualStyleBackColor = True
            Me.cbEnabled.AutoSize = True
            Me.cbEnabled.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(10, 5)
            Me.cbEnabled.Location = point2
            Me.cbEnabled.Name = "cbEnabled"
            size2 = New Size(&H44, &H11)
            Me.cbEnabled.Size = size2
            Me.cbEnabled.TabIndex = 80
            Me.cbEnabled.Text = "Enabled"
            Me.cbEnabled.UseVisualStyleBackColor = True
            Me.Panel1.Anchor = (AnchorStyles.Right Or (AnchorStyles.Left Or AnchorStyles.Top))
            Me.Panel1.BackColor = Color.WhiteSmoke
            Me.Panel1.Controls.Add(Me.Label2)
            Me.Panel1.Controls.Add(Me.btnDown)
            Me.Panel1.Controls.Add(Me.cbEnabled)
            Me.Panel1.Controls.Add(Me.btnUp)
            point2 = New Point(0, 0)
            Me.Panel1.Location = point2
            Me.Panel1.Name = "Panel1"
            size2 = New Size(&H465, &H19)
            Me.Panel1.Size = size2
            Me.Panel1.TabIndex = &H51
            Me.Label2.AutoSize = True
            Me.Label2.Font = New Font("Segoe UI", 6.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(500, 7)
            Me.Label2.Location = point2
            Me.Label2.Name = "Label2"
            size2 = New Size(&H3A, 12)
            Me.Label2.Size = size2
            Me.Label2.TabIndex = &H54
            Me.Label2.Text = "Scraper order"
            Me.btnDown.Anchor = (AnchorStyles.Left Or AnchorStyles.Bottom)
            Me.btnDown.Image = DirectCast(manager.GetObject("btnDown.Image"), Image)
            point2 = New Point(&H24F, 1)
            Me.btnDown.Location = point2
            Me.btnDown.Name = "btnDown"
            size2 = New Size(&H17, &H17)
            Me.btnDown.Size = size2
            Me.btnDown.TabIndex = &H53
            Me.btnDown.UseVisualStyleBackColor = True
            Me.btnUp.Anchor = (AnchorStyles.Left Or AnchorStyles.Bottom)
            Me.btnUp.Image = DirectCast(manager.GetObject("btnUp.Image"), Image)
            point2 = New Point(&H236, 1)
            Me.btnUp.Location = point2
            Me.btnUp.Name = "btnUp"
            size2 = New Size(&H17, &H17)
            Me.btnUp.Size = size2
            Me.btnUp.TabIndex = &H52
            Me.btnUp.UseVisualStyleBackColor = True
            Me.pnlSettings.Controls.Add(Me.Label1)
            Me.pnlSettings.Controls.Add(Me.PictureBox1)
            Me.pnlSettings.Controls.Add(Me.Panel1)
            Me.pnlSettings.Controls.Add(Me.GroupBox30)
            Me.pnlSettings.Controls.Add(Me.GroupBox15)
            Me.pnlSettings.Controls.Add(Me.gbOptions)
            point2 = New Point(12, 1)
            Me.pnlSettings.Location = point2
            Me.pnlSettings.Name = "pnlSettings"
            size2 = New Size(&H269, &H171)
            Me.pnlSettings.Size = size2
            Me.pnlSettings.TabIndex = &H52
            Me.Label1.Anchor = (AnchorStyles.Left Or AnchorStyles.Bottom)
            Me.Label1.Font = New Font("Segoe UI", 6.75!, FontStyle.Regular, GraphicsUnit.Point, &HEE)
            Me.Label1.ForeColor = Color.Blue
            point2 = New Point(&H25, &H151)
            Me.Label1.Location = point2
            Me.Label1.Name = "Label1"
            size2 = New Size(&HE1, &H1F)
            Me.Label1.Size = size2
            Me.Label1.TabIndex = &H61
            Me.Label1.Text = "These settings are specific to this module." & ChrW(13) & ChrW(10) & "Please refer to the global settings for more options."
            Me.Label1.TextAlign = ContentAlignment.MiddleLeft
            Me.PictureBox1.Anchor = (AnchorStyles.Left Or AnchorStyles.Bottom)
            Me.PictureBox1.Image = DirectCast(manager.GetObject("PictureBox1.Image"), Image)
            point2 = New Point(3, &H14F)
            Me.PictureBox1.Location = point2
            Me.PictureBox1.Name = "PictureBox1"
            size2 = New Size(30, &H1F)
            Me.PictureBox1.Size = size2
            Me.PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
            Me.PictureBox1.TabIndex = &H60
            Me.PictureBox1.TabStop = False
            Me.gbOptions.Controls.Add(Me.chkCertification)
            Me.gbOptions.Controls.Add(Me.chkCountry)
            Me.gbOptions.Controls.Add(Me.chkTop250)
            Me.gbOptions.Controls.Add(Me.chkCrew)
            Me.gbOptions.Controls.Add(Me.chkMusicBy)
            Me.gbOptions.Controls.Add(Me.chkProducers)
            Me.gbOptions.Controls.Add(Me.chkFullCast)
            Me.gbOptions.Controls.Add(Me.chkWriters)
            Me.gbOptions.Controls.Add(Me.chkStudio)
            Me.gbOptions.Controls.Add(Me.chkRuntime)
            Me.gbOptions.Controls.Add(Me.chkFullCrew)
            Me.gbOptions.Controls.Add(Me.chkPlot)
            Me.gbOptions.Controls.Add(Me.chkOutline)
            Me.gbOptions.Controls.Add(Me.chkGenre)
            Me.gbOptions.Controls.Add(Me.chkDirector)
            Me.gbOptions.Controls.Add(Me.chkTagline)
            Me.gbOptions.Controls.Add(Me.chkCast)
            Me.gbOptions.Controls.Add(Me.chkVotes)
            Me.gbOptions.Controls.Add(Me.chkTrailer)
            Me.gbOptions.Controls.Add(Me.chkRating)
            Me.gbOptions.Controls.Add(Me.chkRelease)
            Me.gbOptions.Controls.Add(Me.chkMPAA)
            Me.gbOptions.Controls.Add(Me.chkYear)
            Me.gbOptions.Controls.Add(Me.chkTitle)
            Me.gbOptions.Font = New Font("Segoe UI", 8.25!, FontStyle.Bold, GraphicsUnit.Point, 0)
            point2 = New Point(&HE3, 40)
            Me.gbOptions.Location = point2
            Me.gbOptions.Name = "gbOptions"
            size2 = New Size(&H183, &HA1)
            Me.gbOptions.Size = size2
            Me.gbOptions.TabIndex = &H4D
            Me.gbOptions.TabStop = False
            Me.gbOptions.Text = "Scraper Fields"
            Me.chkCertification.AutoSize = True
            Me.chkCertification.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(6, 70)
            Me.chkCertification.Location = point2
            Me.chkCertification.Name = "chkCertification"
            size2 = New Size(&H59, &H11)
            Me.chkCertification.Size = size2
            Me.chkCertification.TabIndex = &H18
            Me.chkCertification.Text = "Certification"
            Me.chkCertification.UseVisualStyleBackColor = True
            Me.chkCountry.AutoSize = True
            Me.chkCountry.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(&HED, &H35)
            Me.chkCountry.Location = point2
            Me.chkCountry.Name = "chkCountry"
            size2 = New Size(&H43, &H11)
            Me.chkCountry.Size = size2
            Me.chkCountry.TabIndex = &H19
            Me.chkCountry.Text = "Country"
            Me.chkCountry.UseVisualStyleBackColor = True
            Me.chkTop250.AutoSize = True
            Me.chkTop250.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(&HED, &H13)
            Me.chkTop250.Location = point2
            Me.chkTop250.Name = "chkTop250"
            size2 = New Size(&H42, &H11)
            Me.chkTop250.Size = size2
            Me.chkTop250.TabIndex = &H17
            Me.chkTop250.Text = "Top 250"
            Me.chkTop250.UseVisualStyleBackColor = True
            Me.chkCrew.AutoSize = True
            Me.chkCrew.Enabled = False
            Me.chkCrew.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(&HF6, &H68)
            Me.chkCrew.Location = point2
            Me.chkCrew.Name = "chkCrew"
            size2 = New Size(&H55, &H11)
            Me.chkCrew.Size = size2
            Me.chkCrew.TabIndex = &H12
            Me.chkCrew.Text = "Other Crew"
            Me.chkCrew.UseVisualStyleBackColor = True
            Me.chkMusicBy.AutoSize = True
            Me.chkMusicBy.Enabled = False
            Me.chkMusicBy.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(&HF6, &H79)
            Me.chkMusicBy.Location = point2
            Me.chkMusicBy.Name = "chkMusicBy"
            size2 = New Size(&H47, &H11)
            Me.chkMusicBy.Size = size2
            Me.chkMusicBy.TabIndex = &H11
            Me.chkMusicBy.Text = "Music By"
            Me.chkMusicBy.UseVisualStyleBackColor = True
            Me.chkProducers.AutoSize = True
            Me.chkProducers.Enabled = False
            Me.chkProducers.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(&HF6, &H8A)
            Me.chkProducers.Location = point2
            Me.chkProducers.Name = "chkProducers"
            size2 = New Size(&H4D, &H11)
            Me.chkProducers.Size = size2
            Me.chkProducers.TabIndex = &H10
            Me.chkProducers.Text = "Producers"
            Me.chkProducers.UseVisualStyleBackColor = True
            Me.chkFullCast.AutoSize = True
            Me.chkFullCast.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(&HED, 70)
            Me.chkFullCast.Location = point2
            Me.chkFullCast.Name = "chkFullCast"
            size2 = New Size(&H6B, &H11)
            Me.chkFullCast.Size = size2
            Me.chkFullCast.TabIndex = 0
            Me.chkFullCast.Text = "Scrape Full Cast"
            Me.chkFullCast.UseVisualStyleBackColor = True
            Me.chkWriters.AutoSize = True
            Me.chkWriters.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(&H83, &H79)
            Me.chkWriters.Location = point2
            Me.chkWriters.Name = "chkWriters"
            size2 = New Size(&H3F, &H11)
            Me.chkWriters.Size = size2
            Me.chkWriters.TabIndex = 15
            Me.chkWriters.Text = "Writers"
            Me.chkWriters.UseVisualStyleBackColor = True
            Me.chkStudio.AutoSize = True
            Me.chkStudio.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(&H83, &H13)
            Me.chkStudio.Location = point2
            Me.chkStudio.Name = "chkStudio"
            size2 = New Size(60, &H11)
            Me.chkStudio.Size = size2
            Me.chkStudio.TabIndex = 14
            Me.chkStudio.Text = "Studio"
            Me.chkStudio.UseVisualStyleBackColor = True
            Me.chkRuntime.AutoSize = True
            Me.chkRuntime.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(6, &H68)
            Me.chkRuntime.Location = point2
            Me.chkRuntime.Name = "chkRuntime"
            size2 = New Size(&H45, &H11)
            Me.chkRuntime.Size = size2
            Me.chkRuntime.TabIndex = 13
            Me.chkRuntime.Text = "Runtime"
            Me.chkRuntime.UseVisualStyleBackColor = True
            Me.chkFullCrew.AutoSize = True
            Me.chkFullCrew.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(&HED, &H57)
            Me.chkFullCrew.Location = point2
            Me.chkFullCrew.Name = "chkFullCrew"
            size2 = New Size(&H6F, &H11)
            Me.chkFullCrew.Size = size2
            Me.chkFullCrew.TabIndex = 2
            Me.chkFullCrew.Text = "Scrape Full Crew"
            Me.chkFullCrew.UseVisualStyleBackColor = True
            Me.chkPlot.AutoSize = True
            Me.chkPlot.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(&H83, 70)
            Me.chkPlot.Location = point2
            Me.chkPlot.Name = "chkPlot"
            size2 = New Size(&H2E, &H11)
            Me.chkPlot.Size = size2
            Me.chkPlot.TabIndex = 12
            Me.chkPlot.Text = "Plot"
            Me.chkPlot.UseVisualStyleBackColor = True
            Me.chkOutline.AutoSize = True
            Me.chkOutline.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(&H83, &H35)
            Me.chkOutline.Location = point2
            Me.chkOutline.Name = "chkOutline"
            size2 = New Size(&H41, &H11)
            Me.chkOutline.Size = size2
            Me.chkOutline.TabIndex = 11
            Me.chkOutline.Text = "Outline"
            Me.chkOutline.UseVisualStyleBackColor = True
            Me.chkGenre.AutoSize = True
            Me.chkGenre.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(&H83, &H8A)
            Me.chkGenre.Location = point2
            Me.chkGenre.Name = "chkGenre"
            size2 = New Size(&H39, &H11)
            Me.chkGenre.Size = size2
            Me.chkGenre.TabIndex = 10
            Me.chkGenre.Text = "Genre"
            Me.chkGenre.UseVisualStyleBackColor = True
            Me.chkDirector.AutoSize = True
            Me.chkDirector.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(&H83, &H68)
            Me.chkDirector.Location = point2
            Me.chkDirector.Name = "chkDirector"
            size2 = New Size(&H43, &H11)
            Me.chkDirector.Size = size2
            Me.chkDirector.TabIndex = 9
            Me.chkDirector.Text = "Director"
            Me.chkDirector.UseVisualStyleBackColor = True
            Me.chkTagline.AutoSize = True
            Me.chkTagline.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(&H83, &H24)
            Me.chkTagline.Location = point2
            Me.chkTagline.Name = "chkTagline"
            size2 = New Size(&H3F, &H11)
            Me.chkTagline.Size = size2
            Me.chkTagline.TabIndex = 8
            Me.chkTagline.Text = "Tagline"
            Me.chkTagline.UseVisualStyleBackColor = True
            Me.chkCast.AutoSize = True
            Me.chkCast.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(&H83, &H57)
            Me.chkCast.Location = point2
            Me.chkCast.Name = "chkCast"
            size2 = New Size(&H30, &H11)
            Me.chkCast.Size = size2
            Me.chkCast.TabIndex = 7
            Me.chkCast.Text = "Cast"
            Me.chkCast.UseVisualStyleBackColor = True
            Me.chkVotes.AutoSize = True
            Me.chkVotes.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(6, &H8A)
            Me.chkVotes.Location = point2
            Me.chkVotes.Name = "chkVotes"
            size2 = New Size(&H37, &H11)
            Me.chkVotes.Size = size2
            Me.chkVotes.TabIndex = 6
            Me.chkVotes.Text = "Votes"
            Me.chkVotes.UseVisualStyleBackColor = True
            Me.chkTrailer.AutoSize = True
            Me.chkTrailer.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(&HED, &H24)
            Me.chkTrailer.Location = point2
            Me.chkTrailer.Name = "chkTrailer"
            size2 = New Size(&H39, &H11)
            Me.chkTrailer.Size = size2
            Me.chkTrailer.TabIndex = 5
            Me.chkTrailer.Text = "Trailer"
            Me.chkTrailer.UseVisualStyleBackColor = True
            Me.chkRating.AutoSize = True
            Me.chkRating.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(6, &H79)
            Me.chkRating.Location = point2
            Me.chkRating.Name = "chkRating"
            size2 = New Size(60, &H11)
            Me.chkRating.Size = size2
            Me.chkRating.TabIndex = 4
            Me.chkRating.Text = "Rating"
            Me.chkRating.UseVisualStyleBackColor = True
            Me.chkRelease.AutoSize = True
            Me.chkRelease.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(6, &H57)
            Me.chkRelease.Location = point2
            Me.chkRelease.Name = "chkRelease"
            size2 = New Size(&H5C, &H11)
            Me.chkRelease.Size = size2
            Me.chkRelease.TabIndex = 3
            Me.chkRelease.Text = "Release Date"
            Me.chkRelease.UseVisualStyleBackColor = True
            Me.chkMPAA.AutoSize = True
            Me.chkMPAA.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(6, &H35)
            Me.chkMPAA.Location = point2
            Me.chkMPAA.Name = "chkMPAA"
            size2 = New Size(&H38, &H11)
            Me.chkMPAA.Size = size2
            Me.chkMPAA.TabIndex = 2
            Me.chkMPAA.Text = "MPAA"
            Me.chkMPAA.UseVisualStyleBackColor = True
            Me.chkYear.AutoSize = True
            Me.chkYear.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(6, &H24)
            Me.chkYear.Location = point2
            Me.chkYear.Name = "chkYear"
            size2 = New Size(&H2F, &H11)
            Me.chkYear.Size = size2
            Me.chkYear.TabIndex = 1
            Me.chkYear.Text = "Year"
            Me.chkYear.UseVisualStyleBackColor = True
            Me.chkTitle.AutoSize = True
            Me.chkTitle.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(6, &H13)
            Me.chkTitle.Location = point2
            Me.chkTitle.Name = "chkTitle"
            size2 = New Size(&H2F, &H11)
            Me.chkTitle.Size = size2
            Me.chkTitle.TabIndex = 0
            Me.chkTitle.Text = "Title"
            Me.chkTitle.UseVisualStyleBackColor = True
            Dim ef2 As New SizeF(96!, 96!)
            Me.AutoScaleDimensions = ef2
            Me.AutoScaleMode = AutoScaleMode.Dpi
            Me.BackColor = Color.White
            size2 = New Size(&H28C, &H184)
            Me.ClientSize = size2
            Me.Controls.Add(Me.pnlSettings)
            Me.Controls.Add(Me.lblVersion)
            Me.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            Me.FormBorderStyle = FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "frmInfoSettingsHolder"
            Me.ShowInTaskbar = False
            Me.StartPosition = FormStartPosition.CenterParent
            Me.Text = "Scraper Setup"
            Me.GroupBox30.ResumeLayout(False)
            Me.GroupBox30.PerformLayout
            Me.GroupBox15.ResumeLayout(False)
            Me.Panel1.ResumeLayout(False)
            Me.Panel1.PerformLayout
            Me.pnlSettings.ResumeLayout(False)
            DirectCast(Me.PictureBox1, ISupportInitialize).EndInit
            Me.gbOptions.ResumeLayout(False)
            Me.gbOptions.PerformLayout
            Me.ResumeLayout(False)
        End Sub

        Public Sub orderChanged()
            Dim scraperOrder As Integer = ModulesManager.Instance.externalScrapersModules.FirstOrDefault(Of _externalScraperModuleClass)(New Func(Of _externalScraperModuleClass, Boolean)(AddressOf frmInfoSettingsHolder._Lambda$__8)).ScraperOrder
            Me.btnDown.Enabled = (scraperOrder < (ModulesManager.Instance.externalScrapersModules.Where(Of _externalScraperModuleClass)(New Func(Of _externalScraperModuleClass, Boolean)(AddressOf frmInfoSettingsHolder._Lambda$__9)).Count(Of _externalScraperModuleClass)() - 1))
            Me.btnUp.Enabled = (scraperOrder > 1)
        End Sub

        Private Sub SetUp()
            Me.chkOFDBGenre.Text = Master.eLang.GetString(1, "Use OFDB Genre", False)
            Me.chkOFDBPlot.Text = Master.eLang.GetString(2, "Use OFDB Plot", False)
            Me.chkOFDBOutline.Text = Master.eLang.GetString(3, "Use OFDB Outline", False)
            Me.chkOFDBTitle.Text = Master.eLang.GetString(4, "Use OFDB Title", False)
            Me.Label18.Text = Master.eLang.GetString(5, "IMDB Mirror:", False)
            Me.gbOptions.Text = Master.eLang.GetString(6, "Scraper Fields - Scraper specific", False)
            Me.chkCrew.Text = Master.eLang.GetString(&H187, "Other Crew", True)
            Me.chkMusicBy.Text = Master.eLang.GetString(&H188, "Music By", True)
            Me.chkProducers.Text = Master.eLang.GetString(&H189, "Producers", True)
            Me.chkWriters.Text = Master.eLang.GetString(&H18A, "Writers", True)
            Me.chkStudio.Text = Master.eLang.GetString(&H18B, "Studio", True)
            Me.chkRuntime.Text = Master.eLang.GetString(&H18C, "Runtime", True)
            Me.chkPlot.Text = Master.eLang.GetString(&H41, "Plot", True)
            Me.chkOutline.Text = Master.eLang.GetString(&H40, "Plot Outline", True)
            Me.chkGenre.Text = Master.eLang.GetString(20, "Genres", True)
            Me.chkDirector.Text = Master.eLang.GetString(&H3E, "Director", True)
            Me.chkTagline.Text = Master.eLang.GetString(&H18D, "Tagline", True)
            Me.chkCast.Text = Master.eLang.GetString(&H18E, "Cast", True)
            Me.chkVotes.Text = Master.eLang.GetString(&H18F, "Votes", True)
            Me.chkTrailer.Text = Master.eLang.GetString(&H97, "Trailer", True)
            Me.chkRating.Text = Master.eLang.GetString(400, "Rating", True)
            Me.chkRelease.Text = Master.eLang.GetString(&H39, "Release Date", True)
            Me.chkMPAA.Text = Master.eLang.GetString(&H191, "MPAA", True)
            Me.chkYear.Text = Master.eLang.GetString(&H116, "Year", True)
            Me.chkTitle.Text = Master.eLang.GetString(&H15, "Title", True)
            Me.chkCertification.Text = Master.eLang.GetString(&H2D2, "Certification", True)
            Me.Label2.Text = Master.eLang.GetString(&HA8, "Scrape Order", True)
            Me.cbEnabled.Text = Master.eLang.GetString(&H306, "Enabled", True)
            Me.Label1.Text = String.Format(Master.eLang.GetString(&H67, "These settings are specific to this module.{0}Please refer to the global settings for more options.", False), ChrW(13) & ChrW(10))
        End Sub

        Private Sub txtIMDBURL_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
        End Sub


        ' Properties
        Friend Overridable Property btnDown As Button
            <DebuggerNonUserCode> _
            Get
                Return Me._btnDown
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Button)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.btnDown_Click)
                If (Not Me._btnDown Is Nothing) Then
                    RemoveHandler Me._btnDown.Click, handler
                End If
                Me._btnDown = WithEventsValue
                If (Not Me._btnDown Is Nothing) Then
                    AddHandler Me._btnDown.Click, handler
                End If
            End Set
        End Property

        Friend Overridable Property btnUp As Button
            <DebuggerNonUserCode> _
            Get
                Return Me._btnUp
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Button)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.btnUp_Click)
                If (Not Me._btnUp Is Nothing) Then
                    RemoveHandler Me._btnUp.Click, handler
                End If
                Me._btnUp = WithEventsValue
                If (Not Me._btnUp Is Nothing) Then
                    AddHandler Me._btnUp.Click, handler
                End If
            End Set
        End Property

        Friend Overridable Property cbEnabled As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._cbEnabled
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.cbEnabled_CheckedChanged)
                If (Not Me._cbEnabled Is Nothing) Then
                    RemoveHandler Me._cbEnabled.CheckedChanged, handler
                End If
                Me._cbEnabled = WithEventsValue
                If (Not Me._cbEnabled Is Nothing) Then
                    AddHandler Me._cbEnabled.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property chkCast As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._chkCast
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.chkCast_CheckedChanged)
                If (Not Me._chkCast Is Nothing) Then
                    RemoveHandler Me._chkCast.CheckedChanged, handler
                End If
                Me._chkCast = WithEventsValue
                If (Not Me._chkCast Is Nothing) Then
                    AddHandler Me._chkCast.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property chkCertification As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._chkCertification
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.chkCertification_CheckedChanged)
                If (Not Me._chkCertification Is Nothing) Then
                    RemoveHandler Me._chkCertification.CheckedChanged, handler
                End If
                Me._chkCertification = WithEventsValue
                If (Not Me._chkCertification Is Nothing) Then
                    AddHandler Me._chkCertification.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property chkCountry As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._chkCountry
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.chkCountry_CheckedChanged)
                If (Not Me._chkCountry Is Nothing) Then
                    RemoveHandler Me._chkCountry.CheckedChanged, handler
                End If
                Me._chkCountry = WithEventsValue
                If (Not Me._chkCountry Is Nothing) Then
                    AddHandler Me._chkCountry.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property chkCrew As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._chkCrew
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.chkCrew_CheckedChanged)
                If (Not Me._chkCrew Is Nothing) Then
                    RemoveHandler Me._chkCrew.CheckedChanged, handler
                End If
                Me._chkCrew = WithEventsValue
                If (Not Me._chkCrew Is Nothing) Then
                    AddHandler Me._chkCrew.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property chkDirector As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._chkDirector
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.chkDirector_CheckedChanged)
                If (Not Me._chkDirector Is Nothing) Then
                    RemoveHandler Me._chkDirector.CheckedChanged, handler
                End If
                Me._chkDirector = WithEventsValue
                If (Not Me._chkDirector Is Nothing) Then
                    AddHandler Me._chkDirector.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property chkFullCast As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._chkFullCast
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.chkFullCast_CheckedChanged)
                If (Not Me._chkFullCast Is Nothing) Then
                    RemoveHandler Me._chkFullCast.CheckedChanged, handler
                End If
                Me._chkFullCast = WithEventsValue
                If (Not Me._chkFullCast Is Nothing) Then
                    AddHandler Me._chkFullCast.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property chkFullCrew As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._chkFullCrew
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.chkFullCrew_CheckedChanged)
                If (Not Me._chkFullCrew Is Nothing) Then
                    RemoveHandler Me._chkFullCrew.CheckedChanged, handler
                End If
                Me._chkFullCrew = WithEventsValue
                If (Not Me._chkFullCrew Is Nothing) Then
                    AddHandler Me._chkFullCrew.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property chkGenre As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._chkGenre
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.chkGenre_CheckedChanged)
                If (Not Me._chkGenre Is Nothing) Then
                    RemoveHandler Me._chkGenre.CheckedChanged, handler
                End If
                Me._chkGenre = WithEventsValue
                If (Not Me._chkGenre Is Nothing) Then
                    AddHandler Me._chkGenre.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property chkMPAA As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._chkMPAA
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.chkMPAA_CheckedChanged)
                If (Not Me._chkMPAA Is Nothing) Then
                    RemoveHandler Me._chkMPAA.CheckedChanged, handler
                End If
                Me._chkMPAA = WithEventsValue
                If (Not Me._chkMPAA Is Nothing) Then
                    AddHandler Me._chkMPAA.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property chkMusicBy As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._chkMusicBy
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.chkMusicBy_CheckedChanged)
                If (Not Me._chkMusicBy Is Nothing) Then
                    RemoveHandler Me._chkMusicBy.CheckedChanged, handler
                End If
                Me._chkMusicBy = WithEventsValue
                If (Not Me._chkMusicBy Is Nothing) Then
                    AddHandler Me._chkMusicBy.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property chkOFDBGenre As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._chkOFDBGenre
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.chkOFDBGenre_CheckedChanged)
                If (Not Me._chkOFDBGenre Is Nothing) Then
                    RemoveHandler Me._chkOFDBGenre.CheckedChanged, handler
                End If
                Me._chkOFDBGenre = WithEventsValue
                If (Not Me._chkOFDBGenre Is Nothing) Then
                    AddHandler Me._chkOFDBGenre.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property chkOFDBOutline As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._chkOFDBOutline
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.chkOFDBOutline_CheckedChanged)
                If (Not Me._chkOFDBOutline Is Nothing) Then
                    RemoveHandler Me._chkOFDBOutline.CheckedChanged, handler
                End If
                Me._chkOFDBOutline = WithEventsValue
                If (Not Me._chkOFDBOutline Is Nothing) Then
                    AddHandler Me._chkOFDBOutline.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property chkOFDBPlot As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._chkOFDBPlot
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.chkOFDBPlot_CheckedChanged)
                If (Not Me._chkOFDBPlot Is Nothing) Then
                    RemoveHandler Me._chkOFDBPlot.CheckedChanged, handler
                End If
                Me._chkOFDBPlot = WithEventsValue
                If (Not Me._chkOFDBPlot Is Nothing) Then
                    AddHandler Me._chkOFDBPlot.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property chkOFDBTitle As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._chkOFDBTitle
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.chkOFDBTitle_CheckedChanged)
                If (Not Me._chkOFDBTitle Is Nothing) Then
                    RemoveHandler Me._chkOFDBTitle.CheckedChanged, handler
                End If
                Me._chkOFDBTitle = WithEventsValue
                If (Not Me._chkOFDBTitle Is Nothing) Then
                    AddHandler Me._chkOFDBTitle.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property chkOutline As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._chkOutline
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.chkOutline_CheckedChanged)
                If (Not Me._chkOutline Is Nothing) Then
                    RemoveHandler Me._chkOutline.CheckedChanged, handler
                End If
                Me._chkOutline = WithEventsValue
                If (Not Me._chkOutline Is Nothing) Then
                    AddHandler Me._chkOutline.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property chkPlot As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._chkPlot
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.chkPlot_CheckedChanged)
                If (Not Me._chkPlot Is Nothing) Then
                    RemoveHandler Me._chkPlot.CheckedChanged, handler
                End If
                Me._chkPlot = WithEventsValue
                If (Not Me._chkPlot Is Nothing) Then
                    AddHandler Me._chkPlot.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property chkProducers As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._chkProducers
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.chkProducers_CheckedChanged)
                If (Not Me._chkProducers Is Nothing) Then
                    RemoveHandler Me._chkProducers.CheckedChanged, handler
                End If
                Me._chkProducers = WithEventsValue
                If (Not Me._chkProducers Is Nothing) Then
                    AddHandler Me._chkProducers.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property chkRating As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._chkRating
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.chkRating_CheckedChanged)
                If (Not Me._chkRating Is Nothing) Then
                    RemoveHandler Me._chkRating.CheckedChanged, handler
                End If
                Me._chkRating = WithEventsValue
                If (Not Me._chkRating Is Nothing) Then
                    AddHandler Me._chkRating.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property chkRelease As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._chkRelease
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.chkRelease_CheckedChanged)
                If (Not Me._chkRelease Is Nothing) Then
                    RemoveHandler Me._chkRelease.CheckedChanged, handler
                End If
                Me._chkRelease = WithEventsValue
                If (Not Me._chkRelease Is Nothing) Then
                    AddHandler Me._chkRelease.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property chkRuntime As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._chkRuntime
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.chkRuntime_CheckedChanged)
                If (Not Me._chkRuntime Is Nothing) Then
                    RemoveHandler Me._chkRuntime.CheckedChanged, handler
                End If
                Me._chkRuntime = WithEventsValue
                If (Not Me._chkRuntime Is Nothing) Then
                    AddHandler Me._chkRuntime.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property chkStudio As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._chkStudio
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.chkStudio_CheckedChanged)
                If (Not Me._chkStudio Is Nothing) Then
                    RemoveHandler Me._chkStudio.CheckedChanged, handler
                End If
                Me._chkStudio = WithEventsValue
                If (Not Me._chkStudio Is Nothing) Then
                    AddHandler Me._chkStudio.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property chkTagline As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._chkTagline
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.chkTagline_CheckedChanged)
                If (Not Me._chkTagline Is Nothing) Then
                    RemoveHandler Me._chkTagline.CheckedChanged, handler
                End If
                Me._chkTagline = WithEventsValue
                If (Not Me._chkTagline Is Nothing) Then
                    AddHandler Me._chkTagline.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property chkTitle As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._chkTitle
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.chkTitle_CheckedChanged)
                If (Not Me._chkTitle Is Nothing) Then
                    RemoveHandler Me._chkTitle.CheckedChanged, handler
                End If
                Me._chkTitle = WithEventsValue
                If (Not Me._chkTitle Is Nothing) Then
                    AddHandler Me._chkTitle.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property chkTop250 As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._chkTop250
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.chkTop250_CheckedChanged)
                If (Not Me._chkTop250 Is Nothing) Then
                    RemoveHandler Me._chkTop250.CheckedChanged, handler
                End If
                Me._chkTop250 = WithEventsValue
                If (Not Me._chkTop250 Is Nothing) Then
                    AddHandler Me._chkTop250.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property chkTrailer As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._chkTrailer
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.chkTrailer_CheckedChanged)
                If (Not Me._chkTrailer Is Nothing) Then
                    RemoveHandler Me._chkTrailer.CheckedChanged, handler
                End If
                Me._chkTrailer = WithEventsValue
                If (Not Me._chkTrailer Is Nothing) Then
                    AddHandler Me._chkTrailer.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property chkVotes As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._chkVotes
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.chkVotes_CheckedChanged)
                If (Not Me._chkVotes Is Nothing) Then
                    RemoveHandler Me._chkVotes.CheckedChanged, handler
                End If
                Me._chkVotes = WithEventsValue
                If (Not Me._chkVotes Is Nothing) Then
                    AddHandler Me._chkVotes.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property chkWriters As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._chkWriters
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.chkWriters_CheckedChanged)
                If (Not Me._chkWriters Is Nothing) Then
                    RemoveHandler Me._chkWriters.CheckedChanged, handler
                End If
                Me._chkWriters = WithEventsValue
                If (Not Me._chkWriters Is Nothing) Then
                    AddHandler Me._chkWriters.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property chkYear As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._chkYear
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.chkYear_CheckedChanged)
                If (Not Me._chkYear Is Nothing) Then
                    RemoveHandler Me._chkYear.CheckedChanged, handler
                End If
                Me._chkYear = WithEventsValue
                If (Not Me._chkYear Is Nothing) Then
                    AddHandler Me._chkYear.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property gbOptions As GroupBox
            <DebuggerNonUserCode> _
            Get
                Return Me._gbOptions
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As GroupBox)
                Me._gbOptions = WithEventsValue
            End Set
        End Property

        Friend Overridable Property GroupBox15 As GroupBox
            <DebuggerNonUserCode> _
            Get
                Return Me._GroupBox15
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As GroupBox)
                Me._GroupBox15 = WithEventsValue
            End Set
        End Property

        Friend Overridable Property GroupBox30 As GroupBox
            <DebuggerNonUserCode> _
            Get
                Return Me._GroupBox30
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As GroupBox)
                Me._GroupBox30 = WithEventsValue
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

        Friend Overridable Property Label18 As Label
            <DebuggerNonUserCode> _
            Get
                Return Me._Label18
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Label)
                Me._Label18 = WithEventsValue
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

        Friend Overridable Property lblVersion As Label
            <DebuggerNonUserCode> _
            Get
                Return Me._lblVersion
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Label)
                Me._lblVersion = WithEventsValue
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

        Friend Overridable Property pnlSettings As Panel
            <DebuggerNonUserCode> _
            Get
                Return Me._pnlSettings
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Panel)
                Me._pnlSettings = WithEventsValue
            End Set
        End Property

        Friend Overridable Property txtIMDBURL As TextBox
            <DebuggerNonUserCode> _
            Get
                Return Me._txtIMDBURL
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As TextBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.txtIMDBURL_TextChanged)
                If (Not Me._txtIMDBURL Is Nothing) Then
                    RemoveHandler Me._txtIMDBURL.TextChanged, handler
                End If
                Me._txtIMDBURL = WithEventsValue
                If (Not Me._txtIMDBURL Is Nothing) Then
                    AddHandler Me._txtIMDBURL.TextChanged, handler
                End If
            End Set
        End Property


        ' Fields
        Private Shared __ENCList As List(Of WeakReference) = New List(Of WeakReference)
        <AccessedThroughProperty("btnDown")> _
        Private _btnDown As Button
        <AccessedThroughProperty("btnUp")> _
        Private _btnUp As Button
        <AccessedThroughProperty("cbEnabled")> _
        Private _cbEnabled As CheckBox
        <AccessedThroughProperty("chkCast")> _
        Private _chkCast As CheckBox
        <AccessedThroughProperty("chkCertification")> _
        Private _chkCertification As CheckBox
        <AccessedThroughProperty("chkCountry")> _
        Private _chkCountry As CheckBox
        <AccessedThroughProperty("chkCrew")> _
        Private _chkCrew As CheckBox
        <AccessedThroughProperty("chkDirector")> _
        Private _chkDirector As CheckBox
        <AccessedThroughProperty("chkFullCast")> _
        Private _chkFullCast As CheckBox
        <AccessedThroughProperty("chkFullCrew")> _
        Private _chkFullCrew As CheckBox
        <AccessedThroughProperty("chkGenre")> _
        Private _chkGenre As CheckBox
        <AccessedThroughProperty("chkMPAA")> _
        Private _chkMPAA As CheckBox
        <AccessedThroughProperty("chkMusicBy")> _
        Private _chkMusicBy As CheckBox
        <AccessedThroughProperty("chkOFDBGenre")> _
        Private _chkOFDBGenre As CheckBox
        <AccessedThroughProperty("chkOFDBOutline")> _
        Private _chkOFDBOutline As CheckBox
        <AccessedThroughProperty("chkOFDBPlot")> _
        Private _chkOFDBPlot As CheckBox
        <AccessedThroughProperty("chkOFDBTitle")> _
        Private _chkOFDBTitle As CheckBox
        <AccessedThroughProperty("chkOutline")> _
        Private _chkOutline As CheckBox
        <AccessedThroughProperty("chkPlot")> _
        Private _chkPlot As CheckBox
        <AccessedThroughProperty("chkProducers")> _
        Private _chkProducers As CheckBox
        <AccessedThroughProperty("chkRating")> _
        Private _chkRating As CheckBox
        <AccessedThroughProperty("chkRelease")> _
        Private _chkRelease As CheckBox
        <AccessedThroughProperty("chkRuntime")> _
        Private _chkRuntime As CheckBox
        <AccessedThroughProperty("chkStudio")> _
        Private _chkStudio As CheckBox
        <AccessedThroughProperty("chkTagline")> _
        Private _chkTagline As CheckBox
        <AccessedThroughProperty("chkTitle")> _
        Private _chkTitle As CheckBox
        <AccessedThroughProperty("chkTop250")> _
        Private _chkTop250 As CheckBox
        <AccessedThroughProperty("chkTrailer")> _
        Private _chkTrailer As CheckBox
        <AccessedThroughProperty("chkVotes")> _
        Private _chkVotes As CheckBox
        <AccessedThroughProperty("chkWriters")> _
        Private _chkWriters As CheckBox
        <AccessedThroughProperty("chkYear")> _
        Private _chkYear As CheckBox
        <AccessedThroughProperty("gbOptions")> _
        Private _gbOptions As GroupBox
        <AccessedThroughProperty("GroupBox15")> _
        Private _GroupBox15 As GroupBox
        <AccessedThroughProperty("GroupBox30")> _
        Private _GroupBox30 As GroupBox
        <AccessedThroughProperty("Label1")> _
        Private _Label1 As Label
        <AccessedThroughProperty("Label18")> _
        Private _Label18 As Label
        <AccessedThroughProperty("Label2")> _
        Private _Label2 As Label
        <AccessedThroughProperty("lblVersion")> _
        Private _lblVersion As Label
        <AccessedThroughProperty("Panel1")> _
        Private _Panel1 As Panel
        <AccessedThroughProperty("PictureBox1")> _
        Private _PictureBox1 As PictureBox
        <AccessedThroughProperty("pnlSettings")> _
        Private _pnlSettings As Panel
        <AccessedThroughProperty("txtIMDBURL")> _
        Private _txtIMDBURL As TextBox
        Private components As IContainer

        ' Nested Types
        <CompilerGenerated> _
        Friend Class _Closure$__1
            ' Methods
            <DebuggerNonUserCode> _
            Public Sub New()
            End Sub

            <DebuggerNonUserCode> _
            Public Sub New(ByVal other As _Closure$__1)
                If (Not other Is Nothing) Then
                    Me.$VB$Local_order = other.$VB$Local_order
                End If
            End Sub

            <CompilerGenerated> _
            Public Function _Lambda$__3(ByVal p As _externalScraperModuleClass) As Boolean
                Return (p.ScraperOrder = (Me.$VB$Local_order + 1))
            End Function


            ' Fields
            Public $VB$Local_order As Integer
        End Class

        <CompilerGenerated> _
        Friend Class _Closure$__2
            ' Methods
            <DebuggerNonUserCode> _
            Public Sub New()
            End Sub

            <DebuggerNonUserCode> _
            Public Sub New(ByVal other As _Closure$__2)
                If (Not other Is Nothing) Then
                    Me.$VB$Local_order = other.$VB$Local_order
                End If
            End Sub

            <CompilerGenerated> _
            Public Function _Lambda$__6(ByVal p As _externalScraperModuleClass) As Boolean
                Return (p.ScraperOrder = (Me.$VB$Local_order - 1))
            End Function


            ' Fields
            Public $VB$Local_order As Integer
        End Class

        Public Delegate Sub ModuleSettingsChangedEventHandler()

        Public Delegate Sub SetupScraperChangedEventHandler(ByVal state As Boolean, ByVal difforder As Integer)
    End Class
End Namespace

