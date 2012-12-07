Imports EmberAPI
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms

Namespace EmberScraperModule
    <DesignerGenerated> _
    Public Class dlgTVEpisodePoster
        Inherits Form
        ' Methods
        <DebuggerNonUserCode> _
        Public Sub New()
            dlgTVEpisodePoster.__ENCAddToList(Me)
            Me.InitializeComponent
        End Sub

        <DebuggerNonUserCode> _
        Private Shared Sub __ENCAddToList(ByVal value As Object)
            Dim list As List(Of WeakReference) = dlgTVEpisodePoster.__ENCList
            SyncLock list
                If (dlgTVEpisodePoster.__ENCList.Count = dlgTVEpisodePoster.__ENCList.Capacity) Then
                    Dim index As Integer = 0
                    Dim num3 As Integer = (dlgTVEpisodePoster.__ENCList.Count - 1)
                    Dim i As Integer = 0
                    Do While (i <= num3)
                        Dim reference As WeakReference = dlgTVEpisodePoster.__ENCList.Item(i)
                        If reference.IsAlive Then
                            If (i <> index) Then
                                dlgTVEpisodePoster.__ENCList.Item(index) = dlgTVEpisodePoster.__ENCList.Item(i)
                            End If
                            index += 1
                        End If
                        i += 1
                    Loop
                    dlgTVEpisodePoster.__ENCList.RemoveRange(index, (dlgTVEpisodePoster.__ENCList.Count - index))
                    dlgTVEpisodePoster.__ENCList.Capacity = dlgTVEpisodePoster.__ENCList.Count
                End If
                dlgTVEpisodePoster.__ENCList.Add(New WeakReference(RuntimeHelpers.GetObjectValue(value)))
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

        <DebuggerStepThrough> _
        Private Sub InitializeComponent()
            Me.OK_Button = New Button
            Me.Cancel_Button = New Button
            Me.pbPoster = New PictureBox
            DirectCast(Me.pbPoster, ISupportInitialize).BeginInit
            Me.SuspendLayout
            Dim point2 As New Point(&H124, &H11F)
            Me.OK_Button.Location = point2
            Me.OK_Button.Name = "OK_Button"
            Dim size2 As New Size(&H43, &H17)
            Me.OK_Button.Size = size2
            Me.OK_Button.TabIndex = 0
            Me.OK_Button.Text = "OK"
            Me.Cancel_Button.DialogResult = DialogResult.Cancel
            point2 = New Point(&H16A, &H11F)
            Me.Cancel_Button.Location = point2
            Me.Cancel_Button.Name = "Cancel_Button"
            size2 = New Size(&H43, &H17)
            Me.Cancel_Button.Size = size2
            Me.Cancel_Button.TabIndex = 1
            Me.Cancel_Button.Text = "Cancel"
            Me.pbPoster.BackColor = Color.DimGray
            Me.pbPoster.BorderStyle = BorderStyle.FixedSingle
            point2 = New Point(6, 6)
            Me.pbPoster.Location = point2
            Me.pbPoster.Name = "pbPoster"
            size2 = New Size(&H1A7, &H114)
            Me.pbPoster.Size = size2
            Me.pbPoster.SizeMode = PictureBoxSizeMode.Zoom
            Me.pbPoster.TabIndex = 2
            Me.pbPoster.TabStop = False
            Me.AcceptButton = Me.OK_Button
            Dim ef2 As New SizeF(96!, 96!)
            Me.AutoScaleDimensions = ef2
            Me.AutoScaleMode = AutoScaleMode.Dpi
            Me.CancelButton = Me.Cancel_Button
            size2 = New Size(&H1B3, &H13B)
            Me.ClientSize = size2
            Me.ControlBox = False
            Me.Controls.Add(Me.pbPoster)
            Me.Controls.Add(Me.OK_Button)
            Me.Controls.Add(Me.Cancel_Button)
            Me.Font = New Font("Segoe UI", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            Me.FormBorderStyle = FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "dlgTVEpisodePoster"
            Me.ShowInTaskbar = False
            Me.StartPosition = FormStartPosition.CenterParent
            Me.Text = "Confirm Episode Poster"
            DirectCast(Me.pbPoster, ISupportInitialize).EndInit
            Me.ResumeLayout(False)
        End Sub

        Private Sub OK_Button_Click(ByVal sender As Object, ByVal e As EventArgs)
            Me.DialogResult = DialogResult.OK
            Me.Close
        End Sub

        Private Sub pbPoster_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim pbPoster As PictureBox = Me.pbPoster
            Dim image As Image = pbPoster.Image
            ModulesManager.Instance.RuntimeObjects.InvokeOpenImageViewer(image)
            pbPoster.Image = image
        End Sub

        Public Function ShowDialog(ByVal Poster As Image) As DialogResult
            Me.pbPoster.Image = Poster
            Return MyBase.ShowDialog
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
                Dim handler As EventHandler = New EventHandler(AddressOf Me.pbPoster_Click)
                If (Not Me._pbPoster Is Nothing) Then
                    RemoveHandler Me._pbPoster.Click, handler
                End If
                Me._pbPoster = WithEventsValue
                If (Not Me._pbPoster Is Nothing) Then
                    AddHandler Me._pbPoster.Click, handler
                End If
            End Set
        End Property


        ' Fields
        Private Shared __ENCList As List(Of WeakReference) = New List(Of WeakReference)
        <AccessedThroughProperty("Cancel_Button")> _
        Private _Cancel_Button As Button
        <AccessedThroughProperty("OK_Button")> _
        Private _OK_Button As Button
        <AccessedThroughProperty("pbPoster")> _
        Private _pbPoster As PictureBox
        Private components As IContainer
    End Class
End Namespace

