﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBoxee
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
		Me.pnlSettings = New System.Windows.Forms.Panel()
		Me.GroupBox1 = New System.Windows.Forms.GroupBox()
		Me.chkBoxeeId = New System.Windows.Forms.CheckBox()
		Me.Panel1 = New System.Windows.Forms.Panel()
		Me.chkEnabled = New System.Windows.Forms.CheckBox()
		Me.pnlSettings.SuspendLayout()
		Me.GroupBox1.SuspendLayout()
		Me.Panel1.SuspendLayout()
		Me.SuspendLayout()
		'
		'pnlSettings
		'
		Me.pnlSettings.BackColor = System.Drawing.Color.White
		Me.pnlSettings.Controls.Add(Me.GroupBox1)
		Me.pnlSettings.Controls.Add(Me.Panel1)
		Me.pnlSettings.Location = New System.Drawing.Point(12, 12)
		Me.pnlSettings.Name = "pnlSettings"
		Me.pnlSettings.Size = New System.Drawing.Size(617, 327)
		Me.pnlSettings.TabIndex = 85
		'
		'GroupBox1
		'
		Me.GroupBox1.Controls.Add(Me.chkBoxeeId)
		Me.GroupBox1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.GroupBox1.Location = New System.Drawing.Point(10, 31)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(604, 49)
		Me.GroupBox1.TabIndex = 1
		Me.GroupBox1.TabStop = False
		Me.GroupBox1.Text = "TV Show Options"
		'
		'chkBoxeeId
		'
		Me.chkBoxeeId.CheckAlign = System.Drawing.ContentAlignment.TopLeft
		Me.chkBoxeeId.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.chkBoxeeId.Location = New System.Drawing.Point(6, 21)
		Me.chkBoxeeId.Name = "chkBoxeeId"
		Me.chkBoxeeId.Size = New System.Drawing.Size(584, 17)
		Me.chkBoxeeId.TabIndex = 0
		Me.chkBoxeeId.Text = "Replace ID field with Boxee Id Field"
		Me.chkBoxeeId.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.chkBoxeeId.UseVisualStyleBackColor = True
		'
		'Panel1
		'
		Me.Panel1.BackColor = System.Drawing.Color.WhiteSmoke
		Me.Panel1.Controls.Add(Me.chkEnabled)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
		Me.Panel1.Location = New System.Drawing.Point(0, 0)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(617, 25)
		Me.Panel1.TabIndex = 0
		'
		'chkEnabled
		'
		Me.chkEnabled.AutoSize = True
		Me.chkEnabled.Location = New System.Drawing.Point(10, 5)
		Me.chkEnabled.Name = "chkEnabled"
		Me.chkEnabled.Size = New System.Drawing.Size(65, 17)
		Me.chkEnabled.TabIndex = 0
		Me.chkEnabled.Text = "Enabled"
		Me.chkEnabled.UseVisualStyleBackColor = True
		'
		'frmBoxee
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(643, 356)
		Me.Controls.Add(Me.pnlSettings)
		Me.Name = "frmBoxee"
		Me.Text = "frmBoxee"
		Me.pnlSettings.ResumeLayout(False)
		Me.GroupBox1.ResumeLayout(False)
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.ResumeLayout(False)

	End Sub
    Friend WithEvents pnlSettings As System.Windows.Forms.Panel
    Friend WithEvents chkBoxeeId As System.Windows.Forms.CheckBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
End Class
