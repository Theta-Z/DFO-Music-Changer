Imports System.ComponentModel
Imports System.IO

Public Class MainForm
    Private _AudioInit As Boolean
    Private _AudioPlayer As NAudio.Wave.WaveOut
    Private _AudioReader As NAudio.Vorbis.VorbisWaveReader
    Private _DFOPath As String
    Private _LastDFO As String = "Please Select a DFO Track"
    Private _LastRep As String = "Please Select a Replacement Track"
    Private _RepPath As String

#Region "Override Subs"
    ''' <summary>
    ''' We have to close the audio items before exiting.
    ''' </summary>
    Protected Overrides Sub OnClosing(e As CancelEventArgs)
        If (_AudioInit) Then
            _AudioPlayer.Stop()
            _AudioPlayer.Dispose()
            _AudioReader.Dispose()
        End If
        MyBase.OnClosing(e)
    End Sub

    ''' <summary>
    ''' There is no purpose loading the interface, if we have no dfo path.
    ''' If there is a DFO path, add music to the list view.
    ''' </summary>
    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _DFOPath = DFOHelper.GetFullPathDFO()

        If (_DFOPath = Nothing) Then
            Close()
        End If

        _RepPath = FileHelper.GetMostRecentPath()
        lvOriginalMusic.LoadItemsFromDirectory(_DFOPath)

        If Not _RepPath.IsNullOrWhiteSpace Then
            lvRepMusic.LoadItemsFromDirectory(_RepPath)
        End If
    End Sub
#End Region

    ' Todo: Break Audio manipulation into a helper class
#Region "Shared Audio Player Subs"
    Private Sub btnPause_Click(sender As Object, e As EventArgs) Handles btnPause.Click
        ' Pause, or unpause, music.
        If (_AudioPlayer.PlaybackState = NAudio.Wave.PlaybackState.Playing) Then
            _AudioPlayer.Pause()
            btnPause.Text = "Resume Music"
        ElseIf (_AudioPlayer.PlaybackState = NAudio.Wave.PlaybackState.Paused) Then
            _AudioPlayer.Resume()
            btnPause.Text = "Pause Music"
        End If
    End Sub

    Private Sub PlayMusic(ByVal path As String, ByVal lv As ListView)
        If (_AudioInit) Then
            _AudioPlayer.Stop()
            _AudioPlayer.Dispose()
            _AudioReader.Dispose()
        End If

        _AudioInit = True
        _AudioReader = New NAudio.Vorbis.VorbisWaveReader(path & lv.SelectedItems(0).Text)
        _AudioPlayer = New NAudio.Wave.WaveOut()
        _AudioPlayer.Init(_AudioReader)
        _AudioPlayer.Volume = tbVolume.Value / 10.0
        _AudioPlayer.Play()
        btnPause.Text = "Pause Music"
        tmrMusicUpdate.Start()
    End Sub

    Private Sub tbVolume_Scroll(sender As Object, e As EventArgs) Handles tbVolume.Scroll
        ' Update volume for the audio player.
        txtVolumeControl.Text = "Volume: " & tbVolume.Value & "0%"
        _AudioPlayer.Volume = tbVolume.Value / 10.0
    End Sub

    Private Sub tmrMusicUpdate_Tick(sender As Object, e As EventArgs) Handles tmrMusicUpdate.Tick
        txtMusicProgress.Text = $"{_AudioReader.CurrentTime.ToString("mm\:ss")} / {_AudioReader.TotalTime.ToString("mm\:ss")}"

        If (_AudioReader.CurrentTime = _AudioReader.TotalTime) And (chkRepeatMusic.Checked) Then
            _AudioReader.Seek(0, SeekOrigin.Begin)
            _AudioPlayer.Play()
        End If
    End Sub
#End Region

    Private Sub btnOrigPlay_Click(sender As Object, e As EventArgs) Handles btnOrigPlay.Click
        If Not (lvOriginalMusic.SelectedItems.Count = 1) Then
            MessageBox.Show("Please select one, and only one, file to play.")
            Exit Sub
        End If
        PlayMusic(_DFOPath, lvOriginalMusic)
    End Sub

    Private Sub lvOriginalMusic_DoubleClick(sender As Object, e As EventArgs) Handles lvOriginalMusic.DoubleClick
        btnOrigPlay.PerformClick()
    End Sub

    Private Sub btnRepPlay_Click(sender As Object, e As EventArgs) Handles btnRepPlay.Click
        If Not (lvRepMusic.SelectedItems.Count = 1) Then
            MessageBox.Show("Please select one, and only one, file to play.")
            Exit Sub
        End If

        Dim file As String = lvRepMusic.SelectedItems(0).Text

        If Not (file.EndsWith(".ogg")) Then
            pnlConverting.Visible = True
            pbLoad.Refresh()
            Dim ac As AudioHelper = New AudioHelper()
            Dim outFile As String = ac.ConvertToOgg(_RepPath & file)
            pnlConverting.Visible = False

            If (outFile.IsNullOrWhiteSpace) Then
                MessageBox.Show("Error converting to Ogg format.")
                Exit Sub
            Else
                lvRepMusic.LoadItemsFromDirectory(_RepPath)
                lvRepMusic.FindItemWithText($"{file.Substring(0, file.LastIndexOf("."))}.ogg").Selected = True
            End If
        End If

        PlayMusic(_RepPath, lvRepMusic)
    End Sub

    Private Sub lvRepMusic_DoubleClick(sender As Object, e As EventArgs) Handles lvRepMusic.DoubleClick
        btnRepPlay.PerformClick()
    End Sub

    Private Sub btnLoadRep_Click(sender As Object, e As EventArgs) Handles btnLoadRep.Click
        Dim ofd As OpenFileDialog = New OpenFileDialog
        _RepPath = ofd.GetFilePath("Supported Music|*.mp3;*.m4a;*.ogg|MP3|*.mp3|M4A|*.m4a|Ogg Vorbis|*.ogg", "mp3", "Select a music file...") & "\"
        If Not (_RepPath.Length < 2) Then
            lvRepMusic.LoadItemsFromDirectory(_RepPath)
        End If
    End Sub

    Private Sub lvRepMusic_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvRepMusic.SelectedIndexChanged
        If (lvRepMusic.SelectedItems.Count = 1) Then
            _LastRep = lvRepMusic.SelectedItems(0).Text
            If (_LastDFO.ToLower().EndsWith(".ogg")) Then
                btnReplace.Enabled = True
            End If

            txtReplacementMsg.Text = $"{_LastDFO}{vbCrLf}Will be Replaced By{vbCrLf}{_LastRep}"
        End If
    End Sub

    Private Sub lvOriginalMusic_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvOriginalMusic.SelectedIndexChanged
        If (lvOriginalMusic.SelectedItems.Count = 1) Then
            _LastDFO = lvOriginalMusic.SelectedItems(0).Text
            If (_LastRep.ToLower().EndsWith(".mp3") Or _LastRep.ToLower().EndsWith(".ogg")) Then
                btnReplace.Enabled = True
            End If

            txtReplacementMsg.Text = $"{_LastDFO}{vbCrLf}Will be Replaced By{vbCrLf}{_LastRep}"
        End If
    End Sub

    Private Sub btnReplace_Click(sender As Object, e As EventArgs) Handles btnReplace.Click
        Dim confirm As DialogResult = MessageBox.Show($"Really replace ""{_LastDFO}""{vbCrLf}With ""{_LastRep}""?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information)

        If (confirm = DialogResult.Yes) Then
            If Not (_LastRep.ToLower().EndsWith(".ogg")) Then
                pnlConverting.Visible = True
                pbLoad.Refresh()
                Dim ac As AudioHelper = New AudioHelper
                Dim temp As String = ac.ConvertToOgg(_RepPath & _LastRep)
                pnlConverting.Visible = False

                If (temp.IsNullOrWhiteSpace) Then
                    MessageBox.Show("Error converting file to ogg.")
                    Exit Sub
                End If
                _LastRep = temp.Substring(temp.LastIndexOf("\"))
            End If

            'before we replace, we must stop any music playing in the current app
            If (_AudioInit) Then
                tmrMusicUpdate.Stop()
                _AudioPlayer.Stop()
                _AudioPlayer.Dispose()
                _AudioReader.Dispose()
                _AudioInit = False
            End If

            'now we have an ogg file, time to replace
            If (chkBackup.Checked) Then
                FileHelper.BackupFile(_DFOPath & _LastDFO)
            End If

            File.Copy(_RepPath & _LastRep, _DFOPath & _LastDFO, True)
            lvOriginalMusic.LoadItemsFromDirectory(_DFOPath)
        End If
    End Sub

    Private Sub btnUndoAll_Click(sender As Object, e As EventArgs) Handles btnUndoAll.Click
        Dim confirm = MessageBox.Show("Really undo all music replacements with originals?" & vbCrLf & "This only works if you've been saving backups.", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Hand)

        If (confirm = DialogResult.Yes) Then
            If (_AudioInit) Then
                tmrMusicUpdate.Stop()
                _AudioInit = False
                _AudioPlayer.Stop()
                _AudioPlayer.Dispose()
                _AudioReader.Dispose()
            End If

            For Each f In Directory.GetFiles(_DFOPath).Where(Function(t) t.EndsWith("____0.ogg", StringComparison.InvariantCultureIgnoreCase))
                f = f.Substring(f.LastIndexOf("\"))
                File.Copy(_DFOPath & f, _DFOPath & f.Replace("____0.ogg", ".ogg"), True)
            Next

            lvOriginalMusic.LoadItemsFromDirectory(_DFOPath)

            MessageBox.Show("We've restored your old DFO music!")
        End If
    End Sub

    Private Sub btnHelp_Click(sender As Object, e As EventArgs) Handles btnHelp.Click
        MessageBox.Show("You thought it was a help page, but it was actually just me! Taz!" & vbCrLf & "This software uses libraries from the FFmpeg project under the LGPLv2.1")

        If (File.Exists("__README__.txt")) Then
            Process.Start("__README__.txt")
        End If
    End Sub
End Class
