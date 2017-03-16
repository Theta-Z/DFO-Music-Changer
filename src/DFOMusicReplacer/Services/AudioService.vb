Imports System.IO
Imports System.Threading

Friend Class AudioService
    Private _AudioInit As Boolean
    Private _AudioPlayer As NAudio.Wave.WaveOut
    Private _AudioReader As NAudio.Vorbis.VorbisWaveReader
    Private _AudioThread As Thread = New Thread(AddressOf AudioTick)
    Private _AudioVolume As Single = 1

    Public Delegate Sub MultiThreadSub()
    Public Delegate Sub PostConversion(ByVal fileName As String)

#Region "Properties"
    ''' <summary>
    ''' Everytime the audio thread goes through another loop, this will tick.
    ''' </summary>
    Public Property AudioTimeTick As MultiThreadSub

    ''' <summary>
    ''' The token to request locks for when performing operations with the service.
    ''' </summary>
    Public Property AudioToken As String = "Token"

    ''' <summary>
    ''' Gets whether or not the audio player is currently playing or paused.
    ''' </summary>
    Public Property CurrentlyPlaying As Boolean

    ''' <summary>
    ''' The path of the DFO installation.
    ''' </summary>
    Public Property DFOPath As String

    ''' <summary>
    ''' The last music played within the DFO area.
    ''' </summary>
    Public Property LastDFO As String = My.Resources.ResData.DefaultReplacementDFO

    ''' <summary>
    ''' The last played music.
    ''' </summary>
    ''' <returns></returns>
    Public Property LastPlayed As String

    ''' <summary>
    ''' The last music played within the replacement folder.
    ''' </summary>
    Public Property LastRep As String = My.Resources.ResData.DefaultReplacementRep

    ''' <summary>
    ''' When the audio finishes, will we repeat it.
    ''' </summary>
    Public Property RepeatAudio As Boolean

    ''' <summary>
    ''' The path of the replacement music.
    ''' </summary>
    Public Property RepPath As String

    ''' <summary>
    ''' The volume of the audio player.
    ''' </summary>
    Public Property Volume As Single
        Get
            Return If(_AudioPlayer?.Volume, _AudioVolume)
        End Get
        Set(value As Single)
            If _AudioInit Then
                _AudioPlayer.Volume = value
            End If
            _AudioVolume = value
        End Set
    End Property
#End Region 'Properties

    ''' <summary>
    ''' On construction, we need to start the audio thread.
    ''' </summary>
    Public Sub New()
        _AudioThread.Start()
    End Sub

    ''' <summary>
    ''' Essentially a timer, every 50 ms will execute AudioTimeTick & check for repeats.
    ''' </summary>
    Private Sub AudioTick()
        While True
            If _AudioInit Then
                AudioToken.GetLock()

                If RepeatAudio And (_AudioReader.CurrentTime = _AudioReader.TotalTime) Then
                    _AudioReader.Seek(0, SeekOrigin.Begin)
                    _AudioPlayer.Play()
                End If

                AudioToken.ReleaseLock()
                AudioTimeTick.Invoke()
            End If

            Thread.Sleep(50)
        End While
    End Sub

    ''' <summary>
    ''' Close the audio player.
    ''' </summary>
    Public Sub ClosePlayer()
        If (_AudioInit) Then
            AudioToken.GetLock()
            _AudioInit = False
            _AudioPlayer.Stop()
            _AudioPlayer.Dispose()
            _AudioReader.Dispose()
            AudioToken.ReleaseLock()
        End If
    End Sub

    ''' <summary>
    ''' Convert the input file (params.item1) to ogg music type.
    ''' </summary>
    ''' <param name="params">item1 = input file, item2 = sub to run post conversion.</param>
    Public Sub ConvertToOGG(ByVal params As Tuple(Of String, PostConversion))
        Dim outFile As String = AudioHelper.ConvertToOgg(params.Item1)
        params.Item2.Invoke(outFile)
    End Sub

    ''' <summary>
    ''' Use to exit the audio service.
    ''' </summary>
    Public Sub ExitPlayer()
        AudioTimeTick = Nothing
        _AudioInit = False
        _AudioThread.Abort()
    End Sub

    ''' <summary>
    ''' Get the length of the playing music.
    ''' </summary>
    ''' <returns>Tuple(Of Integer, Integer)(minutes, seconds)</returns>
    Public Function GetAudioLength() As Tuple(Of Integer, Integer)
        GetAudioLength = New Tuple(Of Integer, Integer)(
                _AudioReader.TotalTime.Minutes,
                _AudioReader.TotalTime.Seconds
            )
    End Function

    ''' <summary>
    ''' Get the current time of the audio.
    ''' </summary>
    ''' <returns>Tuple(Of Integer, Integer)(minutes, seconds)</returns>
    Public Function GetAudioTime() As Tuple(Of Integer, Integer)
        GetAudioTime = New Tuple(Of Integer, Integer)(
                _AudioReader.CurrentTime.Minutes,
                _AudioReader.CurrentTime.Seconds
            )
    End Function

    ''' <summary>
    ''' Pause or unpause the audio player.
    ''' </summary>
    Public Sub PauseUnpauseAudio()
        If _AudioInit <> True Then
            Exit Sub
        End If

        If (_AudioPlayer.PlaybackState = NAudio.Wave.PlaybackState.Playing) Then
            _AudioPlayer.Pause()
            CurrentlyPlaying = False

        ElseIf (_AudioPlayer.PlaybackState = NAudio.Wave.PlaybackState.Paused) Then
            _AudioPlayer.Resume()
            CurrentlyPlaying = True
        End If
    End Sub

    ''' <summary>
    ''' Play the specified music.
    ''' </summary>
    ''' <param name="path">Absolute path to the music to be played.</param>
    Public Sub PlayMusic(ByVal path As String)
        If path.IsNullOrWhiteSpace Then
            Exit Sub
        End If

        Dim locked As Boolean = False
        LastPlayed = path

        If _AudioInit Then
            AudioToken.GetLock()
            locked = True
        End If

        Dim tmpVolume = Volume
        ClosePlayer()
        _AudioReader = New NAudio.Vorbis.VorbisWaveReader(path)
        _AudioPlayer = New NAudio.Wave.WaveOut()
        _AudioPlayer.Init(_AudioReader)
        _AudioPlayer.Volume = tmpVolume
        _AudioPlayer.Play()
        _AudioInit = True

        If locked Then
            AudioToken.ReleaseLock()
        End If
    End Sub
End Class
