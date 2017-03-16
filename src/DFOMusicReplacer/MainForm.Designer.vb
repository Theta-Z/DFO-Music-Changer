<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.gbAvailableMusic = New System.Windows.Forms.GroupBox()
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.btnUndoAll = New System.Windows.Forms.Button()
        Me.btnLoadRep = New System.Windows.Forms.Button()
        Me.btnRepPlay = New System.Windows.Forms.Button()
        Me.btnOrigPlay = New System.Windows.Forms.Button()
        Me.txtReplacementMusic = New System.Windows.Forms.TextBox()
        Me.txtDfoMusic = New System.Windows.Forms.TextBox()
        Me.lvRepMusic = New System.Windows.Forms.ListView()
        Me.lvOriginalMusic = New System.Windows.Forms.ListView()
        Me.pnlConverting = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pbLoad = New System.Windows.Forms.PictureBox()
        Me.tbVolume = New System.Windows.Forms.TrackBar()
        Me.txtVolumeControl = New System.Windows.Forms.TextBox()
        Me.btnPause = New System.Windows.Forms.Button()
        Me.txtMusicProgress = New System.Windows.Forms.TextBox()
        Me.chkRepeatMusic = New System.Windows.Forms.CheckBox()
        Me.btnReplace = New System.Windows.Forms.Button()
        Me.txtReplacementMsg = New System.Windows.Forms.TextBox()
        Me.chkBackup = New System.Windows.Forms.CheckBox()
        Me.pnlBottomArea = New System.Windows.Forms.Panel()
        Me.gbAvailableMusic.SuspendLayout()
        Me.pnlConverting.SuspendLayout()
        CType(Me.pbLoad, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbVolume, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlBottomArea.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbAvailableMusic
        '
        Me.gbAvailableMusic.Controls.Add(Me.btnHelp)
        Me.gbAvailableMusic.Controls.Add(Me.btnUndoAll)
        Me.gbAvailableMusic.Controls.Add(Me.btnLoadRep)
        Me.gbAvailableMusic.Controls.Add(Me.btnRepPlay)
        Me.gbAvailableMusic.Controls.Add(Me.btnOrigPlay)
        Me.gbAvailableMusic.Controls.Add(Me.txtReplacementMusic)
        Me.gbAvailableMusic.Controls.Add(Me.txtDfoMusic)
        Me.gbAvailableMusic.Controls.Add(Me.lvRepMusic)
        Me.gbAvailableMusic.Controls.Add(Me.lvOriginalMusic)
        Me.gbAvailableMusic.Location = New System.Drawing.Point(12, 11)
        Me.gbAvailableMusic.Name = "gbAvailableMusic"
        Me.gbAvailableMusic.Size = New System.Drawing.Size(733, 407)
        Me.gbAvailableMusic.TabIndex = 0
        Me.gbAvailableMusic.TabStop = False
        Me.gbAvailableMusic.Text = "Available Music"
        '
        'btnHelp
        '
        Me.btnHelp.Location = New System.Drawing.Point(87, 380)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(27, 23)
        Me.btnHelp.TabIndex = 13
        Me.btnHelp.Text = "?"
        Me.btnHelp.UseVisualStyleBackColor = True
        '
        'btnUndoAll
        '
        Me.btnUndoAll.Location = New System.Drawing.Point(407, 378)
        Me.btnUndoAll.Name = "btnUndoAll"
        Me.btnUndoAll.Size = New System.Drawing.Size(75, 23)
        Me.btnUndoAll.TabIndex = 7
        Me.btnUndoAll.Text = "Restore"
        Me.btnUndoAll.UseVisualStyleBackColor = True
        '
        'btnLoadRep
        '
        Me.btnLoadRep.Location = New System.Drawing.Point(251, 378)
        Me.btnLoadRep.Name = "btnLoadRep"
        Me.btnLoadRep.Size = New System.Drawing.Size(75, 23)
        Me.btnLoadRep.TabIndex = 6
        Me.btnLoadRep.Text = "Load Path"
        Me.btnLoadRep.UseVisualStyleBackColor = True
        '
        'btnRepPlay
        '
        Me.btnRepPlay.Location = New System.Drawing.Point(6, 380)
        Me.btnRepPlay.Name = "btnRepPlay"
        Me.btnRepPlay.Size = New System.Drawing.Size(75, 23)
        Me.btnRepPlay.TabIndex = 5
        Me.btnRepPlay.Text = "Play Music"
        Me.btnRepPlay.UseVisualStyleBackColor = True
        '
        'btnOrigPlay
        '
        Me.btnOrigPlay.Location = New System.Drawing.Point(652, 380)
        Me.btnOrigPlay.Name = "btnOrigPlay"
        Me.btnOrigPlay.Size = New System.Drawing.Size(75, 23)
        Me.btnOrigPlay.TabIndex = 4
        Me.btnOrigPlay.Text = "Play Music"
        Me.btnOrigPlay.UseVisualStyleBackColor = True
        '
        'txtReplacementMusic
        '
        Me.txtReplacementMusic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReplacementMusic.Location = New System.Drawing.Point(6, 27)
        Me.txtReplacementMusic.Name = "txtReplacementMusic"
        Me.txtReplacementMusic.ReadOnly = True
        Me.txtReplacementMusic.Size = New System.Drawing.Size(320, 20)
        Me.txtReplacementMusic.TabIndex = 3
        Me.txtReplacementMusic.Text = "Replacement Music"
        Me.txtReplacementMusic.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtDfoMusic
        '
        Me.txtDfoMusic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDfoMusic.Location = New System.Drawing.Point(407, 27)
        Me.txtDfoMusic.Name = "txtDfoMusic"
        Me.txtDfoMusic.ReadOnly = True
        Me.txtDfoMusic.Size = New System.Drawing.Size(320, 20)
        Me.txtDfoMusic.TabIndex = 2
        Me.txtDfoMusic.Text = "DFO Music"
        Me.txtDfoMusic.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lvRepMusic
        '
        Me.lvRepMusic.Location = New System.Drawing.Point(6, 46)
        Me.lvRepMusic.MultiSelect = False
        Me.lvRepMusic.Name = "lvRepMusic"
        Me.lvRepMusic.Size = New System.Drawing.Size(320, 328)
        Me.lvRepMusic.TabIndex = 1
        Me.lvRepMusic.UseCompatibleStateImageBehavior = False
        Me.lvRepMusic.View = System.Windows.Forms.View.Details
        '
        'lvOriginalMusic
        '
        Me.lvOriginalMusic.Location = New System.Drawing.Point(407, 46)
        Me.lvOriginalMusic.MultiSelect = False
        Me.lvOriginalMusic.Name = "lvOriginalMusic"
        Me.lvOriginalMusic.Size = New System.Drawing.Size(320, 328)
        Me.lvOriginalMusic.TabIndex = 0
        Me.lvOriginalMusic.UseCompatibleStateImageBehavior = False
        Me.lvOriginalMusic.View = System.Windows.Forms.View.Details
        '
        'pnlConverting
        '
        Me.pnlConverting.BackColor = System.Drawing.SystemColors.Control
        Me.pnlConverting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlConverting.Controls.Add(Me.Label1)
        Me.pnlConverting.Controls.Add(Me.pbLoad)
        Me.pnlConverting.Location = New System.Drawing.Point(164, 163)
        Me.pnlConverting.Name = "pnlConverting"
        Me.pnlConverting.Size = New System.Drawing.Size(405, 88)
        Me.pnlConverting.TabIndex = 14
        Me.pnlConverting.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(115, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(177, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Oh man.. We're working hard today!"
        '
        'pbLoad
        '
        Me.pbLoad.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pbLoad.Image = CType(resources.GetObject("pbLoad.Image"), System.Drawing.Image)
        Me.pbLoad.Location = New System.Drawing.Point(126, 34)
        Me.pbLoad.Name = "pbLoad"
        Me.pbLoad.Size = New System.Drawing.Size(152, 39)
        Me.pbLoad.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbLoad.TabIndex = 0
        Me.pbLoad.TabStop = False
        '
        'tbVolume
        '
        Me.tbVolume.Location = New System.Drawing.Point(500, 16)
        Me.tbVolume.Minimum = 1
        Me.tbVolume.Name = "tbVolume"
        Me.tbVolume.Size = New System.Drawing.Size(170, 45)
        Me.tbVolume.TabIndex = 1
        Me.tbVolume.Value = 10
        '
        'txtVolumeControl
        '
        Me.txtVolumeControl.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtVolumeControl.Location = New System.Drawing.Point(500, 5)
        Me.txtVolumeControl.Name = "txtVolumeControl"
        Me.txtVolumeControl.ReadOnly = True
        Me.txtVolumeControl.Size = New System.Drawing.Size(170, 13)
        Me.txtVolumeControl.TabIndex = 6
        Me.txtVolumeControl.Text = "Volume: 100%"
        Me.txtVolumeControl.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnPause
        '
        Me.btnPause.Location = New System.Drawing.Point(500, 47)
        Me.btnPause.Name = "btnPause"
        Me.btnPause.Size = New System.Drawing.Size(170, 25)
        Me.btnPause.TabIndex = 7
        Me.btnPause.Text = "Pause Music"
        Me.btnPause.UseVisualStyleBackColor = True
        '
        'txtMusicProgress
        '
        Me.txtMusicProgress.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtMusicProgress.Location = New System.Drawing.Point(500, 72)
        Me.txtMusicProgress.Name = "txtMusicProgress"
        Me.txtMusicProgress.ReadOnly = True
        Me.txtMusicProgress.Size = New System.Drawing.Size(170, 13)
        Me.txtMusicProgress.TabIndex = 8
        Me.txtMusicProgress.Text = ":"
        Me.txtMusicProgress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'chkRepeatMusic
        '
        Me.chkRepeatMusic.AutoSize = True
        Me.chkRepeatMusic.Location = New System.Drawing.Point(537, 88)
        Me.chkRepeatMusic.Name = "chkRepeatMusic"
        Me.chkRepeatMusic.Size = New System.Drawing.Size(92, 17)
        Me.chkRepeatMusic.TabIndex = 9
        Me.chkRepeatMusic.Text = "Repeat Music"
        Me.chkRepeatMusic.UseVisualStyleBackColor = True
        '
        'btnReplace
        '
        Me.btnReplace.Enabled = False
        Me.btnReplace.Location = New System.Drawing.Point(139, 62)
        Me.btnReplace.Name = "btnReplace"
        Me.btnReplace.Size = New System.Drawing.Size(75, 23)
        Me.btnReplace.TabIndex = 7
        Me.btnReplace.Text = "Do It!"
        Me.btnReplace.UseVisualStyleBackColor = True
        '
        'txtReplacementMsg
        '
        Me.txtReplacementMsg.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtReplacementMsg.Location = New System.Drawing.Point(15, 15)
        Me.txtReplacementMsg.Multiline = True
        Me.txtReplacementMsg.Name = "txtReplacementMsg"
        Me.txtReplacementMsg.ReadOnly = True
        Me.txtReplacementMsg.Size = New System.Drawing.Size(326, 39)
        Me.txtReplacementMsg.TabIndex = 10
        Me.txtReplacementMsg.Text = "Please Select a DFO Track" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Please Select a Replacement Track"
        Me.txtReplacementMsg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'chkBackup
        '
        Me.chkBackup.AutoSize = True
        Me.chkBackup.Checked = True
        Me.chkBackup.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkBackup.Location = New System.Drawing.Point(91, 85)
        Me.chkBackup.Name = "chkBackup"
        Me.chkBackup.Size = New System.Drawing.Size(174, 17)
        Me.chkBackup.TabIndex = 11
        Me.chkBackup.Text = "Keep a Copy of Old DFO Music"
        Me.chkBackup.UseVisualStyleBackColor = True
        '
        'pnlBottomArea
        '
        Me.pnlBottomArea.Controls.Add(Me.btnPause)
        Me.pnlBottomArea.Controls.Add(Me.txtReplacementMsg)
        Me.pnlBottomArea.Controls.Add(Me.chkBackup)
        Me.pnlBottomArea.Controls.Add(Me.tbVolume)
        Me.pnlBottomArea.Controls.Add(Me.txtVolumeControl)
        Me.pnlBottomArea.Controls.Add(Me.btnReplace)
        Me.pnlBottomArea.Controls.Add(Me.chkRepeatMusic)
        Me.pnlBottomArea.Controls.Add(Me.txtMusicProgress)
        Me.pnlBottomArea.Location = New System.Drawing.Point(-3, 418)
        Me.pnlBottomArea.Name = "pnlBottomArea"
        Me.pnlBottomArea.Size = New System.Drawing.Size(759, 107)
        Me.pnlBottomArea.TabIndex = 15
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(757, 525)
        Me.Controls.Add(Me.pnlBottomArea)
        Me.Controls.Add(Me.pnlConverting)
        Me.Controls.Add(Me.gbAvailableMusic)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(773, 563)
        Me.MinimumSize = New System.Drawing.Size(773, 563)
        Me.Name = "MainForm"
        Me.Text = "DFO Music Replacer"
        Me.gbAvailableMusic.ResumeLayout(False)
        Me.gbAvailableMusic.PerformLayout()
        Me.pnlConverting.ResumeLayout(False)
        Me.pnlConverting.PerformLayout()
        CType(Me.pbLoad, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbVolume, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlBottomArea.ResumeLayout(False)
        Me.pnlBottomArea.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents gbAvailableMusic As GroupBox
    Friend WithEvents lvRepMusic As ListView
    Friend WithEvents lvOriginalMusic As ListView
    Friend WithEvents btnOrigPlay As Button
    Friend WithEvents txtReplacementMusic As TextBox
    Friend WithEvents txtDfoMusic As TextBox
    Friend WithEvents btnRepPlay As Button
    Friend WithEvents tbVolume As TrackBar
    Friend WithEvents txtVolumeControl As TextBox
    Friend WithEvents btnPause As Button
    Friend WithEvents txtMusicProgress As TextBox
    Friend WithEvents chkRepeatMusic As CheckBox
    Friend WithEvents btnLoadRep As Button
    Friend WithEvents btnReplace As Button
    Friend WithEvents txtReplacementMsg As TextBox
    Friend WithEvents chkBackup As CheckBox
    Friend WithEvents btnUndoAll As Button
    Friend WithEvents btnHelp As Button
    Friend WithEvents pnlConverting As Panel
    Friend WithEvents pbLoad As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents pnlBottomArea As Panel
End Class
