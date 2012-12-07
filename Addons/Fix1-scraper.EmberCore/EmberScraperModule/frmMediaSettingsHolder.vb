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
    Public Class frmMediaSettingsHolder
        Inherits Form
        ' Events
        Public Custom Event ModuleSettingsChanged As ModuleSettingsChangedEventHandler
        Public Custom Event SetupPostScraperChanged As SetupPostScraperChangedEventHandler

        ' Methods
        Public Sub New()
            frmMediaSettingsHolder.__ENCAddToList(Me)
            Me.InitializeComponent
            Me.SetUp
        End Sub

        <DebuggerNonUserCode> _
        Private Shared Sub __ENCAddToList(ByVal value As Object)
            Dim list As List(Of WeakReference) = frmMediaSettingsHolder.__ENCList
            SyncLock list
                If (frmMediaSettingsHolder.__ENCList.Count = frmMediaSettingsHolder.__ENCList.Capacity) Then
                    Dim index As Integer = 0
                    Dim num3 As Integer = (frmMediaSettingsHolder.__ENCList.Count - 1)
                    Dim i As Integer = 0
                    Do While (i <= num3)
                        Dim reference As WeakReference = frmMediaSettingsHolder.__ENCList.Item(i)
                        If reference.IsAlive Then
                            If (i <> index) Then
                                frmMediaSettingsHolder.__ENCList.Item(index) = frmMediaSettingsHolder.__ENCList.Item(i)
                            End If
                            index += 1
                        End If
                        i += 1
                    Loop
                    frmMediaSettingsHolder.__ENCList.RemoveRange(index, (frmMediaSettingsHolder.__ENCList.Count - index))
                    frmMediaSettingsHolder.__ENCList.Capacity = frmMediaSettingsHolder.__ENCList.Count
                End If
                frmMediaSettingsHolder.__ENCList.Add(New WeakReference(RuntimeHelpers.GetObjectValue(value)))
            End SyncLock
        End Sub

        <CompilerGenerated> _
        Private Shared Function _Lambda$__10(ByVal p As _externalScraperModuleClass) As Boolean
            Return (p.AssemblyName = EmberNativeScraperModule._AssemblyName)
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__11(ByVal y As _externalScraperModuleClass) As Boolean
            Return y.ProcessorModule.IsPostScraper
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__13(ByVal p As _externalScraperModuleClass) As Boolean
            Return (p.AssemblyName = EmberNativeScraperModule._AssemblyName)
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__14(ByVal p As _externalScraperModuleClass) As Boolean
            Return (p.AssemblyName = EmberNativeScraperModule._AssemblyName)
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__16(ByVal p As _externalScraperModuleClass) As Boolean
            Return (p.AssemblyName = EmberNativeScraperModule._AssemblyName)
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__17(ByVal p As _externalScraperModuleClass) As Boolean
            Return (p.AssemblyName = EmberNativeScraperModule._AssemblyName)
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__18(ByVal y As _externalScraperModuleClass) As Boolean
            Return y.ProcessorModule.IsPostScraper
        End Function

        Private Sub btnDown_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim e$__ As New _Closure$__3 With { _
                .$VB$Local_order = ModulesManager.Instance.externalScrapersModules.FirstOrDefault(Of _externalScraperModuleClass)(New Func(Of _externalScraperModuleClass, Boolean)(AddressOf frmMediaSettingsHolder._Lambda$__10)).PostScraperOrder _
            }
            If (e$__.$VB$Local_order < (ModulesManager.Instance.externalScrapersModules.Where(Of _externalScraperModuleClass)(New Func(Of _externalScraperModuleClass, Boolean)(AddressOf frmMediaSettingsHolder._Lambda$__11)).Count(Of _externalScraperModuleClass)() - 1)) Then
                ModulesManager.Instance.externalScrapersModules.FirstOrDefault(Of _externalScraperModuleClass)(New Func(Of _externalScraperModuleClass, Boolean)(AddressOf e$__._Lambda$__12)).PostScraperOrder = e$__.$VB$Local_order
                ModulesManager.Instance.externalScrapersModules.FirstOrDefault(Of _externalScraperModuleClass)(New Func(Of _externalScraperModuleClass, Boolean)(AddressOf frmMediaSettingsHolder._Lambda$__13)).PostScraperOrder = (e$__.$VB$Local_order + 1)
                Dim setupPostScraperChangedEvent As SetupPostScraperChangedEventHandler = Me.SetupPostScraperChangedEvent
                If (Not setupPostScraperChangedEvent Is Nothing) Then
                    setupPostScraperChangedEvent.Invoke(Me.cbEnabled.Checked, 1)
                End If
                Me.orderChanged
            End If
        End Sub

        Private Sub btnUp_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim e$__ As New _Closure$__4 With { _
                .$VB$Local_order = ModulesManager.Instance.externalScrapersModules.FirstOrDefault(Of _externalScraperModuleClass)(New Func(Of _externalScraperModuleClass, Boolean)(AddressOf frmMediaSettingsHolder._Lambda$__14)).PostScraperOrder _
            }
            If (e$__.$VB$Local_order > 0) Then
                ModulesManager.Instance.externalScrapersModules.FirstOrDefault(Of _externalScraperModuleClass)(New Func(Of _externalScraperModuleClass, Boolean)(AddressOf e$__._Lambda$__15)).PostScraperOrder = e$__.$VB$Local_order
                ModulesManager.Instance.externalScrapersModules.FirstOrDefault(Of _externalScraperModuleClass)(New Func(Of _externalScraperModuleClass, Boolean)(AddressOf frmMediaSettingsHolder._Lambda$__16)).PostScraperOrder = (e$__.$VB$Local_order - 1)
                Dim setupPostScraperChangedEvent As SetupPostScraperChangedEventHandler = Me.SetupPostScraperChangedEvent
                If (Not setupPostScraperChangedEvent Is Nothing) Then
                    setupPostScraperChangedEvent.Invoke(Me.cbEnabled.Checked, -1)
                End If
                Me.orderChanged
            End If
        End Sub

        Private Sub cbEnabled_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim setupPostScraperChangedEvent As SetupPostScraperChangedEventHandler = Me.SetupPostScraperChangedEvent
            If (Not setupPostScraperChangedEvent Is Nothing) Then
                setupPostScraperChangedEvent.Invoke(Me.cbEnabled.Checked, 0)
            End If
        End Sub

        Private Sub cbManualETSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
        End Sub

        Private Sub cbTrailerTMDBPref_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
        End Sub

        Public Sub CheckTrailer()
            Me.txtTimeout.Enabled = Me.chkDownloadTrailer.Checked
            Me.chkTrailerIMDB.Enabled = Me.chkDownloadTrailer.Checked
            Me.chkTrailerTMDB.Enabled = Me.chkDownloadTrailer.Checked
            Me.chkTrailerTMDBXBMC.Enabled = Me.chkDownloadTrailer.Checked
            If Not Me.chkDownloadTrailer.Checked Then
                Me.txtTimeout.Text = "2"
                Me.chkTrailerTMDB.Checked = False
                Me.chkTrailerIMDB.Checked = False
                Me.chkTrailerTMDBXBMC.Checked = False
            End If
        End Sub

        Private Sub chkDownloadTrailer_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
            Me.CheckTrailer
        End Sub

        Private Sub chkScrapeFanart_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
            Me.grpSaveFanart.Enabled = Me.chkScrapeFanart.Checked
        End Sub

        Private Sub chkScrapePoster_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
        End Sub

        Private Sub chkTrailerIMDB_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
        End Sub

        Private Sub chkTrailerTMDB_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Me.chkTrailerTMDBXBMC.Enabled = Me.chkTrailerTMDB.Checked
            Me.cbTrailerTMDBPref.Enabled = Me.chkTrailerTMDB.Checked
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
        End Sub

        Private Sub chkTrailerTMDBXBMC_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
        End Sub

        Private Sub chkUseIMPA_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
        End Sub

        Private Sub chkUseMPDB_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
        End Sub

        Private Sub chkUseTMDB_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Me.cbManualETSize.Enabled = Me.chkUseTMDB.Checked
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
            Dim manager As New ComponentResourceManager(GetType(frmMediaSettingsHolder))
            Me.pnlSettings = New Panel
            Me.Label1 = New Label
            Me.PictureBox1 = New PictureBox
            Me.GroupBox3 = New GroupBox
            Me.GroupBox4 = New GroupBox
            Me.cbManualETSize = New ComboBox
            Me.grpSaveFanart = New GroupBox
            Me.optFanartFolderExtraFanart = New RadioButton
            Me.optFanartFolderExtraThumbs = New RadioButton
            Me.GroupBox9 = New GroupBox
            Me.chkUseMPDB = New CheckBox
            Me.chkUseTMDB = New CheckBox
            Me.chkUseIMPA = New CheckBox
            Me.chkScrapePoster = New CheckBox
            Me.chkScrapeFanart = New CheckBox
            Me.GroupBox1 = New GroupBox
            Me.GroupBox5 = New GroupBox
            Me.cbTrailerTMDBPref = New ComboBox
            Me.Label2 = New Label
            Me.chkDownloadTrailer = New CheckBox
            Me.Label23 = New Label
            Me.txtTimeout = New TextBox
            Me.GroupBox2 = New GroupBox
            Me.chkTrailerTMDBXBMC = New CheckBox
            Me.chkTrailerIMDB = New CheckBox
            Me.chkTrailerTMDB = New CheckBox
            Me.Panel2 = New Panel
            Me.Label3 = New Label
            Me.btnDown = New Button
            Me.btnUp = New Button
            Me.cbEnabled = New CheckBox
            Me.pnlSettings.SuspendLayout
            DirectCast(Me.PictureBox1, ISupportInitialize).BeginInit
            Me.GroupBox3.SuspendLayout
            Me.GroupBox4.SuspendLayout
            Me.grpSaveFanart.SuspendLayout
            Me.GroupBox9.SuspendLayout
            Me.GroupBox1.SuspendLayout
            Me.GroupBox5.SuspendLayout
            Me.GroupBox2.SuspendLayout
            Me.Panel2.SuspendLayout
            Me.SuspendLayout
            Me.pnlSettings.Controls.Add(Me.Label1)
            Me.pnlSettings.Controls.Add(Me.PictureBox1)
            Me.pnlSettings.Controls.Add(Me.GroupBox3)
            Me.pnlSettings.Controls.Add(Me.GroupBox1)
            Me.pnlSettings.Controls.Add(Me.Panel2)
            Dim point2 As New Point(12, 4)
            Me.pnlSettings.Location = point2
            Me.pnlSettings.Name = "pnlSettings"
            Dim size2 As New Size(&H269, &H171)
            Me.pnlSettings.Size = size2
            Me.pnlSettings.TabIndex = &H53
            Me.Label1.Anchor = (AnchorStyles.Left Or AnchorStyles.Bottom)
            Me.Label1.Font = New Font("Segoe UI", 6.75!, FontStyle.Regular, GraphicsUnit.Point, &HEE)
            Me.Label1.ForeColor = Color.Blue
            point2 = New Point(&H25, &H151)
            Me.Label1.Location = point2
            Me.Label1.Name = "Label1"
            size2 = New Size(&HE1, &H1F)
            Me.Label1.Size = size2
            Me.Label1.TabIndex = &H5F
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
            Me.PictureBox1.TabIndex = &H5E
            Me.PictureBox1.TabStop = False
            Me.GroupBox3.Controls.Add(Me.GroupBox4)
            Me.GroupBox3.Controls.Add(Me.grpSaveFanart)
            Me.GroupBox3.Controls.Add(Me.GroupBox9)
            Me.GroupBox3.Controls.Add(Me.chkScrapePoster)
            Me.GroupBox3.Controls.Add(Me.chkScrapeFanart)
            Me.GroupBox3.Font = New Font("Segoe UI", 8.25!, FontStyle.Bold, GraphicsUnit.Point, 0)
            point2 = New Point(15, &H1F)
            Me.GroupBox3.Location = point2
            Me.GroupBox3.Name = "GroupBox3"
            size2 = New Size(&H24B, &H83)
            Me.GroupBox3.Size = size2
            Me.GroupBox3.TabIndex = &H5D
            Me.GroupBox3.TabStop = False
            Me.GroupBox3.Text = "Images"
            Me.GroupBox4.Controls.Add(Me.cbManualETSize)
            point2 = New Point(&H176, 11)
            Me.GroupBox4.Location = point2
            Me.GroupBox4.Name = "GroupBox4"
            size2 = New Size(160, 80)
            Me.GroupBox4.Size = size2
            Me.GroupBox4.TabIndex = &H59
            Me.GroupBox4.TabStop = False
            Me.GroupBox4.Text = "TMDB Extrathumbs Size:"
            Me.cbManualETSize.DropDownStyle = ComboBoxStyle.DropDownList
            Me.cbManualETSize.Enabled = False
            Me.cbManualETSize.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            Me.cbManualETSize.FormattingEnabled = True
            Me.cbManualETSize.Items.AddRange(New Object() { "original", "w1280", "poster", "thumb" })
            point2 = New Point(&H15, &H23)
            Me.cbManualETSize.Location = point2
            Me.cbManualETSize.Name = "cbManualETSize"
            size2 = New Size(&H79, &H15)
            Me.cbManualETSize.Size = size2
            Me.cbManualETSize.TabIndex = 0
            Me.grpSaveFanart.Controls.Add(Me.optFanartFolderExtraFanart)
            Me.grpSaveFanart.Controls.Add(Me.optFanartFolderExtraThumbs)
            Me.grpSaveFanart.Enabled = False
            point2 = New Point(&H18, &H36)
            Me.grpSaveFanart.Location = point2
            Me.grpSaveFanart.Name = "grpSaveFanart"
            size2 = New Size(&H7B, &H42)
            Me.grpSaveFanart.Size = size2
            Me.grpSaveFanart.TabIndex = &H58
            Me.grpSaveFanart.TabStop = False
            Me.grpSaveFanart.Text = "Save Fanart In:"
            Me.optFanartFolderExtraFanart.AutoSize = True
            Me.optFanartFolderExtraFanart.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(&H16, &H26)
            Me.optFanartFolderExtraFanart.Location = point2
            Me.optFanartFolderExtraFanart.Name = "optFanartFolderExtraFanart"
            size2 = New Size(&H55, &H11)
            Me.optFanartFolderExtraFanart.Size = size2
            Me.optFanartFolderExtraFanart.TabIndex = &H54
            Me.optFanartFolderExtraFanart.TabStop = True
            Me.optFanartFolderExtraFanart.Text = "\extrafanart"
            Me.optFanartFolderExtraFanart.UseVisualStyleBackColor = True
            Me.optFanartFolderExtraThumbs.AutoSize = True
            Me.optFanartFolderExtraThumbs.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(&H16, &H13)
            Me.optFanartFolderExtraThumbs.Location = point2
            Me.optFanartFolderExtraThumbs.Name = "optFanartFolderExtraThumbs"
            size2 = New Size(&H5D, &H11)
            Me.optFanartFolderExtraThumbs.Size = size2
            Me.optFanartFolderExtraThumbs.TabIndex = &H54
            Me.optFanartFolderExtraThumbs.TabStop = True
            Me.optFanartFolderExtraThumbs.Text = "\extrathumbs"
            Me.optFanartFolderExtraThumbs.UseVisualStyleBackColor = True
            Me.GroupBox9.Controls.Add(Me.chkUseMPDB)
            Me.GroupBox9.Controls.Add(Me.chkUseTMDB)
            Me.GroupBox9.Controls.Add(Me.chkUseIMPA)
            Me.GroupBox9.Font = New Font("Segoe UI", 8.25!, FontStyle.Bold, GraphicsUnit.Point, 0)
            point2 = New Point(&HA5, 11)
            Me.GroupBox9.Location = point2
            Me.GroupBox9.Name = "GroupBox9"
            size2 = New Size(160, 80)
            Me.GroupBox9.Size = size2
            Me.GroupBox9.TabIndex = &H52
            Me.GroupBox9.TabStop = False
            Me.GroupBox9.Text = "Get Images From:"
            Me.chkUseMPDB.CheckAlign = ContentAlignment.TopLeft
            Me.chkUseMPDB.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(6, &H38)
            Me.chkUseMPDB.Location = point2
            Me.chkUseMPDB.Name = "chkUseMPDB"
            size2 = New Size(150, &H16)
            Me.chkUseMPDB.Size = size2
            Me.chkUseMPDB.TabIndex = 2
            Me.chkUseMPDB.Text = "MoviePosterDB.com"
            Me.chkUseMPDB.TextAlign = ContentAlignment.TopLeft
            Me.chkUseMPDB.UseVisualStyleBackColor = True
            Me.chkUseTMDB.CheckAlign = ContentAlignment.TopLeft
            Me.chkUseTMDB.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(6, &H12)
            Me.chkUseTMDB.Location = point2
            Me.chkUseTMDB.Name = "chkUseTMDB"
            size2 = New Size(&H95, &H13)
            Me.chkUseTMDB.Size = size2
            Me.chkUseTMDB.TabIndex = 0
            Me.chkUseTMDB.Text = "themoviedb.org"
            Me.chkUseTMDB.TextAlign = ContentAlignment.TopLeft
            Me.chkUseTMDB.UseVisualStyleBackColor = True
            Me.chkUseIMPA.CheckAlign = ContentAlignment.TopLeft
            Me.chkUseIMPA.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(6, &H25)
            Me.chkUseIMPA.Location = point2
            Me.chkUseIMPA.Name = "chkUseIMPA"
            size2 = New Size(&H95, 20)
            Me.chkUseIMPA.Size = size2
            Me.chkUseIMPA.TabIndex = 1
            Me.chkUseIMPA.Text = "IMPAwards.com"
            Me.chkUseIMPA.TextAlign = ContentAlignment.TopLeft
            Me.chkUseIMPA.UseVisualStyleBackColor = True
            Me.chkScrapePoster.CheckAlign = ContentAlignment.TopLeft
            Me.chkScrapePoster.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(6, &H13)
            Me.chkScrapePoster.Location = point2
            Me.chkScrapePoster.Name = "chkScrapePoster"
            size2 = New Size(&H72, 15)
            Me.chkScrapePoster.Size = size2
            Me.chkScrapePoster.TabIndex = &H55
            Me.chkScrapePoster.Text = "Get Posters"
            Me.chkScrapePoster.TextAlign = ContentAlignment.TopLeft
            Me.chkScrapePoster.UseVisualStyleBackColor = True
            Me.chkScrapeFanart.CheckAlign = ContentAlignment.TopLeft
            Me.chkScrapeFanart.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(6, &H25)
            Me.chkScrapeFanart.Location = point2
            Me.chkScrapeFanart.Name = "chkScrapeFanart"
            size2 = New Size(&H54, &H10)
            Me.chkScrapeFanart.Size = size2
            Me.chkScrapeFanart.TabIndex = &H57
            Me.chkScrapeFanart.Text = "Get Fanart"
            Me.chkScrapeFanart.TextAlign = ContentAlignment.TopLeft
            Me.chkScrapeFanart.UseVisualStyleBackColor = True
            Me.GroupBox1.Controls.Add(Me.GroupBox5)
            Me.GroupBox1.Controls.Add(Me.chkDownloadTrailer)
            Me.GroupBox1.Controls.Add(Me.Label23)
            Me.GroupBox1.Controls.Add(Me.txtTimeout)
            Me.GroupBox1.Controls.Add(Me.GroupBox2)
            Me.GroupBox1.Font = New Font("Microsoft Sans Serif", 8.25!, FontStyle.Bold, GraphicsUnit.Point, 0)
            point2 = New Point(15, &HA8)
            Me.GroupBox1.Location = point2
            Me.GroupBox1.Name = "GroupBox1"
            size2 = New Size(&H24B, &H70)
            Me.GroupBox1.Size = size2
            Me.GroupBox1.TabIndex = &H5C
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Trailers"
            Me.GroupBox1.UseCompatibleTextRendering = True
            Me.GroupBox5.Controls.Add(Me.cbTrailerTMDBPref)
            Me.GroupBox5.Controls.Add(Me.Label2)
            Me.GroupBox5.Font = New Font("Segoe UI", 8.25!, FontStyle.Bold)
            point2 = New Point(&H176, 12)
            Me.GroupBox5.Location = point2
            Me.GroupBox5.Name = "GroupBox5"
            size2 = New Size(&HA1, &H5E)
            Me.GroupBox5.Size = size2
            Me.GroupBox5.TabIndex = &H5C
            Me.GroupBox5.TabStop = False
            Me.GroupBox5.Text = "Youtube/TMDB Trailer:"
            Me.cbTrailerTMDBPref.DropDownStyle = ComboBoxStyle.DropDownList
            Me.cbTrailerTMDBPref.Enabled = False
            Me.cbTrailerTMDBPref.Font = New Font("Segoe UI", 8.25!)
            Me.cbTrailerTMDBPref.FormattingEnabled = True
            Me.cbTrailerTMDBPref.Items.AddRange(New Object() { "bg", "cs", "da", "de", "el", "en", "es", "fi", "fr", "he", "hu", "it", "nb", "nl", "no", "pl", "pt", "ru", "sk", "sv", "ta", "tr", "uk", "vi", "xx", "zh" })
            point2 = New Point(&H15, &H33)
            Me.cbTrailerTMDBPref.Location = point2
            Me.cbTrailerTMDBPref.Name = "cbTrailerTMDBPref"
            size2 = New Size(&H79, &H15)
            Me.cbTrailerTMDBPref.Size = size2
            Me.cbTrailerTMDBPref.TabIndex = 1
            Me.Label2.AutoSize = True
            Me.Label2.Font = New Font("Segoe UI", 8.25!)
            point2 = New Point(&H1A, &H1A)
            Me.Label2.Location = point2
            Me.Label2.Name = "Label2"
            size2 = New Size(&H6F, 13)
            Me.Label2.Size = size2
            Me.Label2.TabIndex = 0
            Me.Label2.Text = "Preferred Language:"
            Me.chkDownloadTrailer.AutoSize = True
            Me.chkDownloadTrailer.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(6, &H13)
            Me.chkDownloadTrailer.Location = point2
            Me.chkDownloadTrailer.Name = "chkDownloadTrailer"
            size2 = New Size(140, &H11)
            Me.chkDownloadTrailer.Size = size2
            Me.chkDownloadTrailer.TabIndex = &H54
            Me.chkDownloadTrailer.Text = "Enable Trailer Support"
            Me.chkDownloadTrailer.UseVisualStyleBackColor = True
            Me.Label23.AutoSize = True
            Me.Label23.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(&H15, &H2B)
            Me.Label23.Location = point2
            Me.Label23.Name = "Label23"
            size2 = New Size(&H33, 13)
            Me.Label23.Size = size2
            Me.Label23.TabIndex = &H5B
            Me.Label23.Text = "Timeout:"
            Me.txtTimeout.Enabled = False
            Me.txtTimeout.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(&H52, &H27)
            Me.txtTimeout.Location = point2
            Me.txtTimeout.Name = "txtTimeout"
            size2 = New Size(50, &H16)
            Me.txtTimeout.Size = size2
            Me.txtTimeout.TabIndex = 90
            Me.GroupBox2.Controls.Add(Me.chkTrailerTMDBXBMC)
            Me.GroupBox2.Controls.Add(Me.chkTrailerIMDB)
            Me.GroupBox2.Controls.Add(Me.chkTrailerTMDB)
            Me.GroupBox2.Font = New Font("Segoe UI", 8.25!, FontStyle.Bold, GraphicsUnit.Point, 0)
            point2 = New Point(&HA5, 12)
            Me.GroupBox2.Location = point2
            Me.GroupBox2.Name = "GroupBox2"
            size2 = New Size(&HA1, &H5E)
            Me.GroupBox2.Size = size2
            Me.GroupBox2.TabIndex = &H53
            Me.GroupBox2.TabStop = False
            Me.GroupBox2.Text = "Supported Sites:"
            Me.chkTrailerTMDBXBMC.AutoSize = True
            Me.chkTrailerTMDBXBMC.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(&H1A, &H2C)
            Me.chkTrailerTMDBXBMC.Location = point2
            Me.chkTrailerTMDBXBMC.Name = "chkTrailerTMDBXBMC"
            size2 = New Size(&H5F, &H11)
            Me.chkTrailerTMDBXBMC.Size = size2
            Me.chkTrailerTMDBXBMC.TabIndex = 12
            Me.chkTrailerTMDBXBMC.Text = "XBMC Format"
            Me.chkTrailerTMDBXBMC.UseVisualStyleBackColor = True
            Me.chkTrailerIMDB.AutoSize = True
            Me.chkTrailerIMDB.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(6, &H43)
            Me.chkTrailerIMDB.Location = point2
            Me.chkTrailerIMDB.Name = "chkTrailerIMDB"
            size2 = New Size(&H36, &H11)
            Me.chkTrailerIMDB.Size = size2
            Me.chkTrailerIMDB.TabIndex = 11
            Me.chkTrailerIMDB.Text = "IMDB"
            Me.chkTrailerIMDB.UseVisualStyleBackColor = True
            Me.chkTrailerTMDB.AutoSize = True
            Me.chkTrailerTMDB.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(6, &H15)
            Me.chkTrailerTMDB.Location = point2
            Me.chkTrailerTMDB.Name = "chkTrailerTMDB"
            size2 = New Size(&H67, &H11)
            Me.chkTrailerTMDB.Size = size2
            Me.chkTrailerTMDB.TabIndex = 10
            Me.chkTrailerTMDB.Text = "Youtube/TMDB"
            Me.chkTrailerTMDB.UseVisualStyleBackColor = True
            Me.Panel2.Anchor = (AnchorStyles.Right Or (AnchorStyles.Left Or AnchorStyles.Top))
            Me.Panel2.BackColor = Color.WhiteSmoke
            Me.Panel2.Controls.Add(Me.Label3)
            Me.Panel2.Controls.Add(Me.btnDown)
            Me.Panel2.Controls.Add(Me.btnUp)
            Me.Panel2.Controls.Add(Me.cbEnabled)
            point2 = New Point(0, 0)
            Me.Panel2.Location = point2
            Me.Panel2.Name = "Panel2"
            size2 = New Size(&H465, &H19)
            Me.Panel2.Size = size2
            Me.Panel2.TabIndex = &H51
            Me.Label3.AutoSize = True
            Me.Label3.Font = New Font("Segoe UI", 6.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
            point2 = New Point(500, 7)
            Me.Label3.Location = point2
            Me.Label3.Name = "Label3"
            size2 = New Size(&H3A, 12)
            Me.Label3.Size = size2
            Me.Label3.TabIndex = &H57
            Me.Label3.Text = "Scraper order"
            Me.btnDown.Anchor = (AnchorStyles.Left Or AnchorStyles.Bottom)
            Me.btnDown.Image = DirectCast(manager.GetObject("btnDown.Image"), Image)
            point2 = New Point(&H24F, 1)
            Me.btnDown.Location = point2
            Me.btnDown.Name = "btnDown"
            size2 = New Size(&H17, &H17)
            Me.btnDown.Size = size2
            Me.btnDown.TabIndex = &H56
            Me.btnDown.UseVisualStyleBackColor = True
            Me.btnUp.Anchor = (AnchorStyles.Left Or AnchorStyles.Bottom)
            Me.btnUp.Image = DirectCast(manager.GetObject("btnUp.Image"), Image)
            point2 = New Point(&H236, 1)
            Me.btnUp.Location = point2
            Me.btnUp.Name = "btnUp"
            size2 = New Size(&H17, &H17)
            Me.btnUp.Size = size2
            Me.btnUp.TabIndex = &H55
            Me.btnUp.UseVisualStyleBackColor = True
            Me.cbEnabled.AutoSize = True
            point2 = New Point(10, 5)
            Me.cbEnabled.Location = point2
            Me.cbEnabled.Name = "cbEnabled"
            size2 = New Size(&H44, &H11)
            Me.cbEnabled.Size = size2
            Me.cbEnabled.TabIndex = &H52
            Me.cbEnabled.Text = "Enabled"
            Me.cbEnabled.UseVisualStyleBackColor = True
            Dim ef2 As New SizeF(96!, 96!)
            Me.AutoScaleDimensions = ef2
            Me.AutoScaleMode = AutoScaleMode.Dpi
            Me.BackColor = Color.White
            size2 = New Size(&H28C, &H184)
            Me.ClientSize = size2
            Me.Controls.Add(Me.pnlSettings)
            Me.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            Me.FormBorderStyle = FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "frmMediaSettingsHolder"
            Me.ShowInTaskbar = False
            Me.StartPosition = FormStartPosition.CenterParent
            Me.Text = "Scraper Setup"
            Me.pnlSettings.ResumeLayout(False)
            DirectCast(Me.PictureBox1, ISupportInitialize).EndInit
            Me.GroupBox3.ResumeLayout(False)
            Me.GroupBox4.ResumeLayout(False)
            Me.grpSaveFanart.ResumeLayout(False)
            Me.grpSaveFanart.PerformLayout
            Me.GroupBox9.ResumeLayout(False)
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout
            Me.GroupBox5.ResumeLayout(False)
            Me.GroupBox5.PerformLayout
            Me.GroupBox2.ResumeLayout(False)
            Me.GroupBox2.PerformLayout
            Me.Panel2.ResumeLayout(False)
            Me.Panel2.PerformLayout
            Me.ResumeLayout(False)
        End Sub

        Private Sub optFanartFolderExtraFanart_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
        End Sub

        Private Sub optFanartFolderExtraThumbs_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim moduleSettingsChangedEvent As ModuleSettingsChangedEventHandler = Me.ModuleSettingsChangedEvent
            If (Not moduleSettingsChangedEvent Is Nothing) Then
                moduleSettingsChangedEvent.Invoke
            End If
        End Sub

        Public Sub orderChanged()
            Dim postScraperOrder As Integer = ModulesManager.Instance.externalScrapersModules.FirstOrDefault(Of _externalScraperModuleClass)(New Func(Of _externalScraperModuleClass, Boolean)(AddressOf frmMediaSettingsHolder._Lambda$__17)).PostScraperOrder
            Me.btnDown.Enabled = (postScraperOrder < (ModulesManager.Instance.externalScrapersModules.Where(Of _externalScraperModuleClass)(New Func(Of _externalScraperModuleClass, Boolean)(AddressOf frmMediaSettingsHolder._Lambda$__18)).Count(Of _externalScraperModuleClass)() - 1))
            Me.btnUp.Enabled = (postScraperOrder > 1)
        End Sub

        Public Sub SetUp()
            Me.txtTimeout.Text = Master.eSettings.TrailerTimeout.ToString
            Me.Label23.Text = Master.eLang.GetString(7, "Timeout:", False)
            Me.GroupBox2.Text = Master.eLang.GetString(8, "Supported Sites:", False)
            Me.GroupBox9.Text = Master.eLang.GetString(9, "Get Images From:", False)
            Me.grpSaveFanart.Text = Master.eLang.GetString(&H1F41, "Save Fanart In:", False)
            Me.chkDownloadTrailer.Text = Master.eLang.GetString(&H211, "Enable Trailer Support", True)
            Me.Label3.Text = Master.eLang.GetString(&HA8, "Scrape Order", True)
            Me.cbEnabled.Text = Master.eLang.GetString(&H306, "Enabled", True)
            Me.chkScrapePoster.Text = Master.eLang.GetString(&H65, "Get Posters", False)
            Me.chkScrapeFanart.Text = Master.eLang.GetString(&H66, "Get Fanart", False)
            Me.Label1.Text = String.Format(Master.eLang.GetString(&H67, "These settings are specific to this module.{0}Please refer to the global settings for more options.", False), ChrW(13) & ChrW(10))
        End Sub

        Private Sub txtTimeout_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
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

        Friend Overridable Property cbManualETSize As ComboBox
            <DebuggerNonUserCode> _
            Get
                Return Me._cbManualETSize
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As ComboBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.cbManualETSize_SelectedIndexChanged)
                If (Not Me._cbManualETSize Is Nothing) Then
                    RemoveHandler Me._cbManualETSize.SelectedIndexChanged, handler
                End If
                Me._cbManualETSize = WithEventsValue
                If (Not Me._cbManualETSize Is Nothing) Then
                    AddHandler Me._cbManualETSize.SelectedIndexChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property cbTrailerTMDBPref As ComboBox
            <DebuggerNonUserCode> _
            Get
                Return Me._cbTrailerTMDBPref
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As ComboBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.cbTrailerTMDBPref_SelectedIndexChanged)
                If (Not Me._cbTrailerTMDBPref Is Nothing) Then
                    RemoveHandler Me._cbTrailerTMDBPref.SelectedIndexChanged, handler
                End If
                Me._cbTrailerTMDBPref = WithEventsValue
                If (Not Me._cbTrailerTMDBPref Is Nothing) Then
                    AddHandler Me._cbTrailerTMDBPref.SelectedIndexChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property chkDownloadTrailer As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._chkDownloadTrailer
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.chkDownloadTrailer_CheckedChanged)
                If (Not Me._chkDownloadTrailer Is Nothing) Then
                    RemoveHandler Me._chkDownloadTrailer.CheckedChanged, handler
                End If
                Me._chkDownloadTrailer = WithEventsValue
                If (Not Me._chkDownloadTrailer Is Nothing) Then
                    AddHandler Me._chkDownloadTrailer.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property chkScrapeFanart As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._chkScrapeFanart
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.chkScrapeFanart_CheckedChanged)
                If (Not Me._chkScrapeFanart Is Nothing) Then
                    RemoveHandler Me._chkScrapeFanart.CheckedChanged, handler
                End If
                Me._chkScrapeFanart = WithEventsValue
                If (Not Me._chkScrapeFanart Is Nothing) Then
                    AddHandler Me._chkScrapeFanart.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property chkScrapePoster As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._chkScrapePoster
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.chkScrapePoster_CheckedChanged)
                If (Not Me._chkScrapePoster Is Nothing) Then
                    RemoveHandler Me._chkScrapePoster.CheckedChanged, handler
                End If
                Me._chkScrapePoster = WithEventsValue
                If (Not Me._chkScrapePoster Is Nothing) Then
                    AddHandler Me._chkScrapePoster.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property chkTrailerIMDB As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._chkTrailerIMDB
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.chkTrailerIMDB_CheckedChanged)
                If (Not Me._chkTrailerIMDB Is Nothing) Then
                    RemoveHandler Me._chkTrailerIMDB.CheckedChanged, handler
                End If
                Me._chkTrailerIMDB = WithEventsValue
                If (Not Me._chkTrailerIMDB Is Nothing) Then
                    AddHandler Me._chkTrailerIMDB.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property chkTrailerTMDB As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._chkTrailerTMDB
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.chkTrailerTMDB_CheckedChanged)
                If (Not Me._chkTrailerTMDB Is Nothing) Then
                    RemoveHandler Me._chkTrailerTMDB.CheckedChanged, handler
                End If
                Me._chkTrailerTMDB = WithEventsValue
                If (Not Me._chkTrailerTMDB Is Nothing) Then
                    AddHandler Me._chkTrailerTMDB.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property chkTrailerTMDBXBMC As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._chkTrailerTMDBXBMC
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.chkTrailerTMDBXBMC_CheckedChanged)
                If (Not Me._chkTrailerTMDBXBMC Is Nothing) Then
                    RemoveHandler Me._chkTrailerTMDBXBMC.CheckedChanged, handler
                End If
                Me._chkTrailerTMDBXBMC = WithEventsValue
                If (Not Me._chkTrailerTMDBXBMC Is Nothing) Then
                    AddHandler Me._chkTrailerTMDBXBMC.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property chkUseIMPA As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._chkUseIMPA
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.chkUseIMPA_CheckedChanged)
                If (Not Me._chkUseIMPA Is Nothing) Then
                    RemoveHandler Me._chkUseIMPA.CheckedChanged, handler
                End If
                Me._chkUseIMPA = WithEventsValue
                If (Not Me._chkUseIMPA Is Nothing) Then
                    AddHandler Me._chkUseIMPA.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property chkUseMPDB As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._chkUseMPDB
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.chkUseMPDB_CheckedChanged)
                If (Not Me._chkUseMPDB Is Nothing) Then
                    RemoveHandler Me._chkUseMPDB.CheckedChanged, handler
                End If
                Me._chkUseMPDB = WithEventsValue
                If (Not Me._chkUseMPDB Is Nothing) Then
                    AddHandler Me._chkUseMPDB.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property chkUseTMDB As CheckBox
            <DebuggerNonUserCode> _
            Get
                Return Me._chkUseTMDB
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As CheckBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.chkUseTMDB_CheckedChanged)
                If (Not Me._chkUseTMDB Is Nothing) Then
                    RemoveHandler Me._chkUseTMDB.CheckedChanged, handler
                End If
                Me._chkUseTMDB = WithEventsValue
                If (Not Me._chkUseTMDB Is Nothing) Then
                    AddHandler Me._chkUseTMDB.CheckedChanged, handler
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

        Friend Overridable Property GroupBox3 As GroupBox
            <DebuggerNonUserCode> _
            Get
                Return Me._GroupBox3
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As GroupBox)
                Me._GroupBox3 = WithEventsValue
            End Set
        End Property

        Friend Overridable Property GroupBox4 As GroupBox
            <DebuggerNonUserCode> _
            Get
                Return Me._GroupBox4
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As GroupBox)
                Me._GroupBox4 = WithEventsValue
            End Set
        End Property

        Friend Overridable Property GroupBox5 As GroupBox
            <DebuggerNonUserCode> _
            Get
                Return Me._GroupBox5
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As GroupBox)
                Me._GroupBox5 = WithEventsValue
            End Set
        End Property

        Friend Overridable Property GroupBox9 As GroupBox
            <DebuggerNonUserCode> _
            Get
                Return Me._GroupBox9
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As GroupBox)
                Me._GroupBox9 = WithEventsValue
            End Set
        End Property

        Friend Overridable Property grpSaveFanart As GroupBox
            <DebuggerNonUserCode> _
            Get
                Return Me._grpSaveFanart
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As GroupBox)
                Me._grpSaveFanart = WithEventsValue
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

        Friend Overridable Property Label23 As Label
            <DebuggerNonUserCode> _
            Get
                Return Me._Label23
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Label)
                Me._Label23 = WithEventsValue
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

        Friend Overridable Property optFanartFolderExtraFanart As RadioButton
            <DebuggerNonUserCode> _
            Get
                Return Me._optFanartFolderExtraFanart
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As RadioButton)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.optFanartFolderExtraFanart_CheckedChanged)
                If (Not Me._optFanartFolderExtraFanart Is Nothing) Then
                    RemoveHandler Me._optFanartFolderExtraFanart.CheckedChanged, handler
                End If
                Me._optFanartFolderExtraFanart = WithEventsValue
                If (Not Me._optFanartFolderExtraFanart Is Nothing) Then
                    AddHandler Me._optFanartFolderExtraFanart.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property optFanartFolderExtraThumbs As RadioButton
            <DebuggerNonUserCode> _
            Get
                Return Me._optFanartFolderExtraThumbs
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As RadioButton)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.optFanartFolderExtraThumbs_CheckedChanged)
                If (Not Me._optFanartFolderExtraThumbs Is Nothing) Then
                    RemoveHandler Me._optFanartFolderExtraThumbs.CheckedChanged, handler
                End If
                Me._optFanartFolderExtraThumbs = WithEventsValue
                If (Not Me._optFanartFolderExtraThumbs Is Nothing) Then
                    AddHandler Me._optFanartFolderExtraThumbs.CheckedChanged, handler
                End If
            End Set
        End Property

        Friend Overridable Property Panel2 As Panel
            <DebuggerNonUserCode> _
            Get
                Return Me._Panel2
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As Panel)
                Me._Panel2 = WithEventsValue
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

        Friend Overridable Property txtTimeout As TextBox
            <DebuggerNonUserCode> _
            Get
                Return Me._txtTimeout
            End Get
            <MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode> _
            Set(ByVal WithEventsValue As TextBox)
                Dim handler As EventHandler = New EventHandler(AddressOf Me.txtTimeout_TextChanged)
                If (Not Me._txtTimeout Is Nothing) Then
                    RemoveHandler Me._txtTimeout.TextChanged, handler
                End If
                Me._txtTimeout = WithEventsValue
                If (Not Me._txtTimeout Is Nothing) Then
                    AddHandler Me._txtTimeout.TextChanged, handler
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
        <AccessedThroughProperty("cbManualETSize")> _
        Private _cbManualETSize As ComboBox
        <AccessedThroughProperty("cbTrailerTMDBPref")> _
        Private _cbTrailerTMDBPref As ComboBox
        <AccessedThroughProperty("chkDownloadTrailer")> _
        Private _chkDownloadTrailer As CheckBox
        <AccessedThroughProperty("chkScrapeFanart")> _
        Private _chkScrapeFanart As CheckBox
        <AccessedThroughProperty("chkScrapePoster")> _
        Private _chkScrapePoster As CheckBox
        <AccessedThroughProperty("chkTrailerIMDB")> _
        Private _chkTrailerIMDB As CheckBox
        <AccessedThroughProperty("chkTrailerTMDB")> _
        Private _chkTrailerTMDB As CheckBox
        <AccessedThroughProperty("chkTrailerTMDBXBMC")> _
        Private _chkTrailerTMDBXBMC As CheckBox
        <AccessedThroughProperty("chkUseIMPA")> _
        Private _chkUseIMPA As CheckBox
        <AccessedThroughProperty("chkUseMPDB")> _
        Private _chkUseMPDB As CheckBox
        <AccessedThroughProperty("chkUseTMDB")> _
        Private _chkUseTMDB As CheckBox
        <AccessedThroughProperty("GroupBox1")> _
        Private _GroupBox1 As GroupBox
        <AccessedThroughProperty("GroupBox2")> _
        Private _GroupBox2 As GroupBox
        <AccessedThroughProperty("GroupBox3")> _
        Private _GroupBox3 As GroupBox
        <AccessedThroughProperty("GroupBox4")> _
        Private _GroupBox4 As GroupBox
        <AccessedThroughProperty("GroupBox5")> _
        Private _GroupBox5 As GroupBox
        <AccessedThroughProperty("GroupBox9")> _
        Private _GroupBox9 As GroupBox
        <AccessedThroughProperty("grpSaveFanart")> _
        Private _grpSaveFanart As GroupBox
        <AccessedThroughProperty("Label1")> _
        Private _Label1 As Label
        <AccessedThroughProperty("Label2")> _
        Private _Label2 As Label
        <AccessedThroughProperty("Label23")> _
        Private _Label23 As Label
        <AccessedThroughProperty("Label3")> _
        Private _Label3 As Label
        <AccessedThroughProperty("optFanartFolderExtraFanart")> _
        Private _optFanartFolderExtraFanart As RadioButton
        <AccessedThroughProperty("optFanartFolderExtraThumbs")> _
        Private _optFanartFolderExtraThumbs As RadioButton
        <AccessedThroughProperty("Panel2")> _
        Private _Panel2 As Panel
        <AccessedThroughProperty("PictureBox1")> _
        Private _PictureBox1 As PictureBox
        <AccessedThroughProperty("pnlSettings")> _
        Private _pnlSettings As Panel
        <AccessedThroughProperty("txtTimeout")> _
        Private _txtTimeout As TextBox
        Private components As IContainer

        ' Nested Types
        <CompilerGenerated> _
        Friend Class _Closure$__3
            ' Methods
            <DebuggerNonUserCode> _
            Public Sub New()
            End Sub

            <DebuggerNonUserCode> _
            Public Sub New(ByVal other As _Closure$__3)
                If (Not other Is Nothing) Then
                    Me.$VB$Local_order = other.$VB$Local_order
                End If
            End Sub

            <CompilerGenerated> _
            Public Function _Lambda$__12(ByVal p As _externalScraperModuleClass) As Boolean
                Return (p.PostScraperOrder = (Me.$VB$Local_order + 1))
            End Function


            ' Fields
            Public $VB$Local_order As Integer
        End Class

        <CompilerGenerated> _
        Friend Class _Closure$__4
            ' Methods
            <DebuggerNonUserCode> _
            Public Sub New()
            End Sub

            <DebuggerNonUserCode> _
            Public Sub New(ByVal other As _Closure$__4)
                If (Not other Is Nothing) Then
                    Me.$VB$Local_order = other.$VB$Local_order
                End If
            End Sub

            <CompilerGenerated> _
            Public Function _Lambda$__15(ByVal p As _externalScraperModuleClass) As Boolean
                Return (p.PostScraperOrder = (Me.$VB$Local_order - 1))
            End Function


            ' Fields
            Public $VB$Local_order As Integer
        End Class

        Public Delegate Sub ModuleSettingsChangedEventHandler()

        Public Delegate Sub SetupPostScraperChangedEventHandler(ByVal state As Boolean, ByVal difforder As Integer)
    End Class
End Namespace

