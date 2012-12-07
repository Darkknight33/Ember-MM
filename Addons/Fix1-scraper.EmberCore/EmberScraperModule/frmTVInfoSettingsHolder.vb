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
    Public Class frmTVInfoSettingsHolder
        Inherits Form
        ' Events
        Public Custom Event ModuleSettingsChanged As ModuleSettingsChangedEventHandler
        Public Custom Event SetupScraperChanged As SetupScraperChangedEventHandler

        ' Methods
        Public Sub New()
            frmTVInfoSettingsHolder.__ENCAddToList(Me)
            Me.InitializeComponent
            Me.SetUp
            Me.orderChanged
        End Sub

        <DebuggerNonUserCode> _
        Private Shared Sub __ENCAddToList(ByVal value As Object)
            Dim list As List(Of WeakReference) = frmTVInfoSettingsHolder.__ENCList
            SyncLock list
                If (frmTVInfoSettingsHolder.__ENCList.Count = frmTVInfoSettingsHolder.__ENCList.Capacity) Then
                    Dim index As Integer = 0
                    Dim num3 As Integer = (frmTVInfoSettingsHolder.__ENCList.Count - 1)
                    Dim i As Integer = 0
                    Do While (i <= num3)
                        Dim reference As WeakReference = frmTVInfoSettingsHolder.__ENCList.Item(i)
                        If reference.IsAlive Then
                            If (i <> index) Then
                                frmTVInfoSettingsHolder.__ENCList.Item(index) = frmTVInfoSettingsHolder.__ENCList.Item(i)
                            End If
                            index += 1
                        End If
                        i += 1
                    Loop
                    frmTVInfoSettingsHolder.__ENCList.RemoveRange(index, (frmTVInfoSettingsHolder.__ENCList.Count - index))
                    frmTVInfoSettingsHolder.__ENCList.Capacity = frmTVInfoSettingsHolder.__ENCList.Count
                End If
                frmTVInfoSettingsHolder.__ENCList.Add(New WeakReference(RuntimeHelpers.GetObjectValue(value)))
            End SyncLock
        End Sub

        <CompilerGenerated> _
        Private Shared Function _Lambda$__19(ByVal p As _externalScraperModuleClass) As Boolean
            Return (p.AssemblyName = EmberNativeScraperModule._AssemblyName)
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__20(ByVal y As _externalScraperModuleClass) As Boolean
            Return y.ProcessorModule.IsScraper
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__22(ByVal p As _externalScraperModuleClass) As Boolean
            Return (p.AssemblyName = EmberNativeScraperModule._AssemblyName)
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__23(ByVal p As _externalScraperModuleClass) As Boolean
            Return (p.AssemblyName = EmberNativeScraperModule._AssemblyName)
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__25(ByVal p As _externalScraperModuleClass) As Boolean
            Return (p.AssemblyName = EmberNativeScraperModule._AssemblyName)
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__26(ByVal p As _externalScraperModuleClass) As Boolean
            Return (p.AssemblyName = EmberNativeScraperModule._AssemblyName)
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__27(ByVal y As _externalScraperModuleClass) As Boolean
            Return y.ProcessorModule.IsScraper
        End Function

        Private Sub btnDown_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim e$__ As New _Closure$__5 With { _
                .$VB$Local_order = ModulesManager.Instance.externalScrapersModules.FirstOrDefault(Of _externalScraperModuleClass)(New Func(Of _externalScraperModuleClass, Boolean)(AddressOf frmTVInfoSettingsHolder._Lambda$__19)).ScraperOrder _
            }
            If (e$__.$VB$Local_order < (ModulesManager.Instance.externalScrapersModules.Where(Of _externalScraperModuleClass)(New Func(Of _externalScraperModuleClass, Boolean)(AddressOf frmTVInfoSettingsHolder._Lambda$__20)).Count(Of _externalScraperModuleClass)() - 1)) Then
                ModulesManager.Instance.externalScrapersModules.FirstOrDefault(Of _externalScraperModuleClass)(New Func(Of _externalScraperModuleClass, Boolean)(AddressOf e$__._Lambda$__21)).ScraperOrder = e$__.$VB$Local_order
                ModulesManager.Instance.externalScrapersModules.FirstOrDefault(Of _externalScraperModuleClass)(New Func(Of _externalScraperModuleClass, Boolean)(AddressOf frmTVInfoSettingsHolder._Lambda$__22)).ScraperOrder = (e$__.$VB$Local_order + 1)
                Dim setupScraperChangedEvent As SetupScraperChangedEventHandler = Me.SetupScraperChangedEvent
                If (Not setupScraperChangedEvent Is Nothing) Then
                    setupScraperChangedEvent.Invoke(Me.cbEnabled.Checked, 1)
                End If
                Me.orderChanged
            End If
        End Sub

        Private Sub btnUp_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim e$__ As New _Closure$__6 With { _
                .$VB$Local_order = ModulesManager.Instance.externalScrapersModules.FirstOrDefault(Of _externalScraperModuleClass)(New Func(Of _externalScraperModuleClass, Boolean)(AddressOf frmTVInfoSettingsHolder._Lambda$__23)).ScraperOrder _
            }
            If (e$__.$VB$Local_order > 0) Then
                ModulesManager.Instance.externalScrapersModules.FirstOrDefault(Of _externalScraperModuleClass)(New Func(Of _externalScraperModuleClass, Boolean)(AddressOf e$__._Lambda$__24)).ScraperOrder = e$__.$VB$Local_order
                ModulesManager.Instance.externalScrapersModules.FirstOrDefault(Of _externalScraperModuleClass)(New Func(Of _externalScraperModuleClass, Boolean)(AddressOf frmTVInfoSettingsHolder._Lambda$__25)).ScraperOrder = (e$__.$VB$Local_order - 1)
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
            Dim manager As New ComponentResourceManager(GetType(frmTVInfoSettingsHolder))
            Me.lblVersion = New Label
            Me.cbEnabled = New CheckBox
            Me.Panel1 = New Panel
            Me.Label2 = New Label
            Me.btnDown = New Button
            Me.btnUp = New Button
            Me.pnlSettings = New Panel
            Me.Label1 = New Label
            Me.PictureBox1 = New PictureBox
            Me.Panel1.SuspendLayout
            Me.pnlSettings.SuspendLayout
            DirectCast(Me.PictureBox1, ISupportInitialize).BeginInit
            Me.SuspendLayout
            Dim point2 As New Point(&H11E, &H189)
            Me.lblVersion.Location = point2
            Me.lblVersion.Name = "lblVersion"
            Dim size2 As New Size(90, &H10)
            Me.lblVersion.Size = size2
            Me.lblVersion.TabIndex = &H4A
            Me.lblVersion.Text = "Version:"
            Me.cbEnabled.AutoSize = True
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
            Me.Name = "frmTVInfoSettingsHolder"
            Me.ShowInTaskbar = False
            Me.StartPosition = FormStartPosition.CenterParent
            Me.Text = "Scraper Setup"
            Me.Panel1.ResumeLayout(False)
            Me.Panel1.PerformLayout
            Me.pnlSettings.ResumeLayout(False)
            DirectCast(Me.PictureBox1, ISupportInitialize).EndInit
            Me.ResumeLayout(False)
        End Sub

        Public Sub orderChanged()
            Dim scraperOrder As Integer = ModulesManager.Instance.externalScrapersModules.FirstOrDefault(Of _externalScraperModuleClass)(New Func(Of _externalScraperModuleClass, Boolean)(AddressOf frmTVInfoSettingsHolder._Lambda$__26)).ScraperOrder
            Me.btnDown.Enabled = (scraperOrder < (ModulesManager.Instance.externalScrapersModules.Where(Of _externalScraperModuleClass)(New Func(Of _externalScraperModuleClass, Boolean)(AddressOf frmTVInfoSettingsHolder._Lambda$__27)).Count(Of _externalScraperModuleClass)() - 1))
            Me.btnUp.Enabled = (scraperOrder > 1)
        End Sub

        Private Sub SetUp()
            Me.Label2.Text = Master.eLang.GetString(&HA8, "Scrape Order", True)
            Me.cbEnabled.Text = Master.eLang.GetString(&H306, "Enabled", True)
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


        ' Fields
        Private Shared __ENCList As List(Of WeakReference) = New List(Of WeakReference)
        <AccessedThroughProperty("btnDown")> _
        Private _btnDown As Button
        <AccessedThroughProperty("btnUp")> _
        Private _btnUp As Button
        <AccessedThroughProperty("cbEnabled")> _
        Private _cbEnabled As CheckBox
        <AccessedThroughProperty("Label1")> _
        Private _Label1 As Label
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
        Private components As IContainer

        ' Nested Types
        <CompilerGenerated> _
        Friend Class _Closure$__5
            ' Methods
            <DebuggerNonUserCode> _
            Public Sub New()
            End Sub

            <DebuggerNonUserCode> _
            Public Sub New(ByVal other As _Closure$__5)
                If (Not other Is Nothing) Then
                    Me.$VB$Local_order = other.$VB$Local_order
                End If
            End Sub

            <CompilerGenerated> _
            Public Function _Lambda$__21(ByVal p As _externalScraperModuleClass) As Boolean
                Return (p.ScraperOrder = (Me.$VB$Local_order + 1))
            End Function


            ' Fields
            Public $VB$Local_order As Integer
        End Class

        <CompilerGenerated> _
        Friend Class _Closure$__6
            ' Methods
            <DebuggerNonUserCode> _
            Public Sub New()
            End Sub

            <DebuggerNonUserCode> _
            Public Sub New(ByVal other As _Closure$__6)
                If (Not other Is Nothing) Then
                    Me.$VB$Local_order = other.$VB$Local_order
                End If
            End Sub

            <CompilerGenerated> _
            Public Function _Lambda$__24(ByVal p As _externalScraperModuleClass) As Boolean
                Return (p.ScraperOrder = (Me.$VB$Local_order - 1))
            End Function


            ' Fields
            Public $VB$Local_order As Integer
        End Class

        Public Delegate Sub ModuleSettingsChangedEventHandler()

        Public Delegate Sub SetupScraperChangedEventHandler(ByVal state As Boolean, ByVal difforder As Integer)
    End Class
End Namespace

