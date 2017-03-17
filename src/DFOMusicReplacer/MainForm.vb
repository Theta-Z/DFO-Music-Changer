Imports System.ComponentModel
Imports System.IO
Imports System.Threading

Public Class MainForm
    Private _AudioService As AudioService
    Private _CurrentMusicLength As String
    Private _Messages As String() = New String() {
            "Oh man.. We're workin' hard today!",
            "That's gonna be 1.21 jiggawaits!",
            "Conversions? Water you doing!",
            "Preparing to unload the toad.",
            "Thank you, based, FFmpeg <3!",
            "I'll take an apple daiquiri, minus the daiquiri, please!",
            "Rammus can't taunt this!",
            "Loading ACT & Prepping Balance...",
            ">mfw you read this",
            "One conversion, extra pickles! That'll be $0.00 at the first window."
        }
    Private _Random As Random

#Region "Override Subs"
    ''' <summary>
    ''' There is no purpose loading the interface, if we have no dfo path.
    ''' If there is a DFO path, add music to the list view.
    ''' </summary>
    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _AudioService = New AudioService()
        _AudioService.DFOPath = DFOHelper.GetFullPathDFO()
        _AudioService.AudioTimeTick = AddressOf TrackAudio
        _Random = New Random()

        If _AudioService.DFOPath = Nothing Then
            MessageBox.Show("Could not detect your DFO path... Is DFO installed on this computer?")
            Close()
        End If

        _AudioService.RepPath = FileHelper.GetMostRecentPath()
        lvOriginalMusic.LoadItemsFromDirectory(_AudioService.DFOPath)

        If Not _AudioService.RepPath.IsNullOrWhiteSpace Then
            lvRepMusic.LoadItemsFromDirectory(_AudioService.RepPath)
        End If

        If Not Directory.Exists("Logs") Then
            Directory.CreateDirectory("Logs")
        End If
    End Sub

    ''' <summary>
    ''' We have to close the audio items before exiting.
    ''' </summary>
    Protected Overrides Sub OnClosing(e As CancelEventArgs)
        _AudioService.ExitPlayer()
        MyBase.OnClosing(e)
    End Sub
#End Region

#Region "Form Event Handlers"
    ''' <summary>
    ''' Display a messagebox with "help", notify the use of FFMPEG, and then show the __README__.txt
    ''' </summary>
    Private Sub btnHelp_Click(sender As Object, e As EventArgs) Handles btnHelp.Click
        MessageBox.Show("You thought it was a help page, but it was actually just me! Taz!" & vbCrLf & "This software uses libraries from the FFmpeg project under the LGPLv2.1")

        If (File.Exists("__README__.txt")) Then
            Process.Start("__README__.txt")
        End If
    End Sub

    ''' <summary>
    ''' Show an OpenFileDialog and get the path to supported music files. Then load the supported files within lvRepMusic.
    ''' </summary>
    Private Sub btnLoadRep_Click(sender As Object, e As EventArgs) Handles btnLoadRep.Click
        Dim ofd As OpenFileDialog = New OpenFileDialog
        Dim path = ofd.GetFilePath("Supported Music|*.mp3;*.m4a;*.ogg|MP3|*.mp3|M4A|*.m4a|Ogg Vorbis|*.ogg", "mp3", "Select a music file...") & "\"
        If path.IsNullOrWhiteSpace Then
            Exit Sub
        End If

        _AudioService.RepPath = path
        _AudioService.LastRep = My.Resources.ResData.DefaultReplacementRep
        txtReplacementMsg.Text = $"{_AudioService.LastDFO}{vbCrLf}Will be Replaced By{vbCrLf}{_AudioService.LastRep}"
        btnReplace.Enabled = False

        If Not (_AudioService.RepPath.Length < 2) Then
            lvRepMusic.LoadItemsFromDirectory(_AudioService.RepPath)
        End If
    End Sub

    ''' <summary>
    ''' Play the music selected within lvOriginalMusic.
    ''' </summary>
    Private Sub btnOrigPlay_Click(sender As Object, e As EventArgs) Handles btnOrigPlay.Click
        If Not (lvOriginalMusic.SelectedItems.Count = 1) Then
            MessageBox.Show("Please select one, and only one, file to play.")
            Exit Sub
        End If

        _AudioService.PlayMusic(_AudioService.DFOPath & _AudioService.LastDFO)
        GetCurrentMusicLength()
        btnPause.Text = "Pause"
    End Sub

    ''' <summary>
    ''' Pause or unpause the music.
    ''' </summary>
    Private Sub btnPause_Click(sender As Object, e As EventArgs) Handles btnPause.Click
        _AudioService.PauseUnpauseAudio()
        btnPause.Text = IIf(_AudioService.CurrentlyPlaying, "Pause", "Resume") & " Music"
    End Sub

    ''' <summary>
    ''' Replace the selected item in lvOriginalMusic with the selected item in lvRepMusic.
    ''' </summary>
    Private Sub btnReplace_Click(sender As Object, e As EventArgs) Handles btnReplace.Click
        Dim confirm As DialogResult = MessageBox.Show($"Really replace ""{_AudioService.LastDFO}""{vbCrLf}With ""{_AudioService.LastRep}""?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information)

        If (confirm = DialogResult.Yes) Then
            If Not _AudioService.LastRep.ToLower().EndsWith(".ogg") Then
                gbAvailableMusic.Enabled = False
                pnlBottomArea.Enabled = False
                txtConversion.Text = _Messages(_Random.Next(_Messages.Length))
                pnlConverting.Visible = True
            End If

            Dim tStart = New ParameterizedThreadStart(AddressOf _AudioService.ConvertToOGG)
            Dim conversionThread = New Thread(tStart)
            conversionThread.Start(New Tuple(Of String, AudioService.PostConversion)(_AudioService.RepPath & _AudioService.LastRep, AddressOf ReplaceConversionFinished))
        End If
    End Sub

    ''' <summary>
    ''' Play the music selected within lvRepMusic.
    ''' </summary>
    Private Sub btnRepPlay_Click(sender As Object, e As EventArgs) Handles btnRepPlay.Click
        If Not (lvRepMusic.SelectedItems.Count = 1) Then
            MessageBox.Show("Please select one, and only one, file to play.")
            Exit Sub
        End If

        If Not _AudioService.LastRep.ToLower().EndsWith(".ogg") Then
            gbAvailableMusic.Enabled = False
            pnlBottomArea.Enabled = False
            txtConversion.Text = _Messages(_Random.Next(_Messages.Length))
            pnlConverting.Visible = True
        End If

        Dim tStart = New ParameterizedThreadStart(AddressOf _AudioService.ConvertToOGG)
        Dim conversionThread = New Thread(tStart)
        conversionThread.Start(New Tuple(Of String, AudioService.PostConversion)(_AudioService.RepPath & _AudioService.LastRep, AddressOf ConvertAudioFinish))
    End Sub

    ''' <summary>
    ''' Undo music replacements within their DFO music. This will only work if they've been doing backups.
    ''' The [..]____0.ogg file is the original.
    ''' </summary>
    Private Sub btnUndoAll_Click(sender As Object, e As EventArgs) Handles btnUndoAll.Click
        Dim confirm = MessageBox.Show("Really undo all music replacements with originals?" & vbCrLf & "This only works if you've been saving backups.", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Hand)

        If (confirm = DialogResult.Yes) Then
            Dim needStopAudio = _AudioService.LastPlayed.Contains(_AudioService.DFOPath)

            If needStopAudio Then
                _AudioService.ClosePlayer()
                _AudioService.LastPlayed = String.Empty
                txtMusicProgress.Text = ":"
            End If

            For Each f In Directory.GetFiles(_AudioService.DFOPath).Where(Function(t) t.EndsWith("____0.ogg", StringComparison.InvariantCultureIgnoreCase))
                f = f.Substring(f.LastIndexOf("\"))
                File.Copy(_AudioService.DFOPath & f, _AudioService.DFOPath & f.Replace("____0.ogg", ".ogg"), True)
            Next

            lvOriginalMusic.LoadItemsFromDirectory(_AudioService.DFOPath)
            MessageBox.Show("We've restored your old DFO music!")
        End If
    End Sub

    ''' <summary>
    ''' Update _AudioService with information about if it's to repeat music or not.
    ''' </summary>
    Private Sub chkRepeatMusic_CheckedChanged(sender As Object, e As EventArgs) Handles chkRepeatMusic.CheckedChanged
        _AudioService.RepeatAudio = chkRepeatMusic.Checked
    End Sub

    ''' <summary>
    ''' Play the selected music within lvOriginalMusic.
    ''' </summary>
    Private Sub lvOriginalMusic_DoubleClick(sender As Object, e As EventArgs) Handles lvOriginalMusic.DoubleClick
        btnOrigPlay.PerformClick()
    End Sub

    ''' <summary>
    ''' When a new item is selected within lvOriginalMusic we need to:
    '''     A - Update AudioService with the new LastDFO selected.
    '''     B - Update the replace button to be enabled.
    '''     C - Update UI so user knows what will happen if they replace.
    ''' </summary>
    Private Sub lvOriginalMusic_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvOriginalMusic.SelectedIndexChanged
        If (lvOriginalMusic.SelectedItems.Count = 1) Then
            _AudioService.LastDFO = lvOriginalMusic.SelectedItems(0).Text
            If (_AudioService.LastRep.ToLower().EndsWith(".mp3") Or _AudioService.LastRep.ToLower().EndsWith(".ogg")) Then
                btnReplace.Enabled = True
            End If

            txtReplacementMsg.Text = $"{_AudioService.LastDFO}{vbCrLf}Will be Replaced By{vbCrLf}{_AudioService.LastRep}"
        End If
    End Sub

    ''' <summary>
    ''' Play the selected music within lvRepMusic.
    ''' </summary>
    Private Sub lvRepMusic_DoubleClick(sender As Object, e As EventArgs) Handles lvRepMusic.DoubleClick
        btnRepPlay.PerformClick()
    End Sub

    ''' <summary>
    ''' When a new item is selected within lvRepMusic we need to:
    '''     A - Update AudioService with the new LastRep selected.
    '''     B - Update the replace button to be enabled.
    '''     C - Update UI so user knows what will happen if they replace.
    ''' </summary>
    Private Sub lvRepMusic_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvRepMusic.SelectedIndexChanged
        If (lvRepMusic.SelectedItems.Count = 1) Then
            _AudioService.LastRep = lvRepMusic.SelectedItems(0).Text
            If (_AudioService.LastDFO.ToLower().EndsWith(".ogg")) Then
                btnReplace.Enabled = True
            End If

            txtReplacementMsg.Text = $"{_AudioService.LastDFO}{vbCrLf}Will be Replaced By{vbCrLf}{_AudioService.LastRep}"
        End If
    End Sub

    ''' <summary>
    ''' Update the volume of the music player.
    ''' </summary>
    Private Sub tbVolume_Scroll(sender As Object, e As EventArgs) Handles tbVolume.Scroll
        txtVolumeControl.Text = "Volume: " & tbVolume.Value & "0%"
        _AudioService.Volume = tbVolume.Value / 10.0
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' After conversion, we need to either:
    '''     A - Inform the user of a failed conversion attempt.
    '''     B - Select the new ogg file in the listview and play it.
    ''' </summary>
    ''' <param name="fileName">The new ogg filename. Empty string will be taken as failed conversion.</param>
    Private Sub ConvertAudioFinish(ByVal fileName As String)
        If lvRepMusic.InvokeRequired Then
            Dim delInvoke As AudioService.PostConversion = AddressOf ConvertAudioFinish
            lvRepMusic.Invoke(delInvoke, fileName)
            Exit Sub
        End If

        If (fileName.IsNullOrWhiteSpace) Then
            MessageBox.Show("Error converting to Ogg format.")
        Else
            lvRepMusic.LoadItemsFromDirectory(_AudioService.RepPath)
            Thread.Sleep(100)

            Dim musicNameStart = fileName.LastIndexOf("\") + 1
            Dim musicName = $"{fileName.Substring(musicNameStart, fileName.LastIndexOf(".") - musicNameStart)}.ogg"

            lvRepMusic.FindItemWithText(musicName).Selected = True
            lvRepMusic.FindItemWithText(musicName).EnsureVisible()
        End If

        pnlConverting.Visible = False
        gbAvailableMusic.Enabled = True
        pnlBottomArea.Enabled = True

        _AudioService.PlayMusic(_AudioService.RepPath & _AudioService.LastRep)
        GetCurrentMusicLength()
        btnPause.Text = "Pause"
    End Sub

    ''' <summary>
    ''' Get the length of the current playing music.
    ''' </summary>
    Private Sub GetCurrentMusicLength()
        Dim audioLen = _AudioService.GetAudioLength()
        _CurrentMusicLength = $"{audioLen.Item1}:{audioLen.Item2:00}"
    End Sub

    ''' <summary>
    ''' After conversion, we need to either:
    '''     A - Inform the user of a failed conversion attempt.
    '''     B - Select the new ogg file in the listview and play it.
    ''' </summary>
    ''' <param name="fileName">The new ogg filename. Empty string will be taken as failed conversion.</param>
    Private Sub ReplaceConversionFinished(ByVal fileName As String)
        If lvRepMusic.InvokeRequired Then
            Dim delInvoke As AudioService.PostConversion = AddressOf ReplaceConversionFinished
            lvRepMusic.Invoke(delInvoke, fileName)
            Exit Sub
        End If

        If fileName.IsNullOrWhiteSpace Then
            MessageBox.Show("Error converting to ogg.")
            Enabled = True
            Exit Sub
        End If

        Dim dfoMusic = _AudioService.DFOPath & _AudioService.LastDFO
        Dim needsMusicStop = _AudioService.LastPlayed.IsOneOf(dfoMusic)

        'If we're listening to the current song or replacing the current song, stop playback
        'to prevent interfering with file operations.
        If needsMusicStop Then
            _AudioService.ClosePlayer()
        End If

        If (chkBackup.Checked) Then
            FileHelper.BackupFile(dfoMusic)
        End If

        File.Copy(fileName, dfoMusic, True)
        lvOriginalMusic.LoadItemsFromDirectory(_AudioService.DFOPath)
        lvRepMusic.LoadItemsFromDirectory(_AudioService.RepPath)

        Dim item = lvOriginalMusic.FindItemWithText(_AudioService.LastDFO)
        lvOriginalMusic.Items(item.Index).EnsureVisible()

        item = lvRepMusic.FindItemWithText(_AudioService.LastRep)
        lvRepMusic.Items(item.Index).EnsureVisible()

        'If we stopped the music, turn it back on.
        If needsMusicStop Then
            _AudioService.PlayMusic(_AudioService.LastPlayed)
            GetCurrentMusicLength()
        End If

        pnlConverting.Visible = False
        gbAvailableMusic.Enabled = True
        pnlBottomArea.Enabled = True
    End Sub

    ''' <summary>
    ''' Method passed into _AudioService that will run every thread loop.
    ''' Keeps track of current audio time for display to the user.
    ''' </summary>
    Private Sub TrackAudio()
        If txtMusicProgress.InvokeRequired Then
            txtMusicProgress.Invoke(_AudioService.AudioTimeTick)
            Exit Sub
        End If

        Dim currentTime = _AudioService.GetAudioTime()
        txtMusicProgress.Text = $"{currentTime.Item1}:{currentTime.Item2:00} / {_CurrentMusicLength}"
    End Sub
#End Region
End Class
