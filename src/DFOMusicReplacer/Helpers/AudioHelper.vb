Imports System.IO
Imports System.Threading

Public Class AudioHelper
    ''' <summary>
    ''' Converts the file to OGG and returns the filename, if exists.
    ''' </summary>
    ''' <param name="fileName">The file to convert.</param>
    ''' <returns>Filepath to Ogg output file.</returns>
    Public Function ConvertToOgg(ByVal fileName As String) As String
        ConvertToOgg = String.Empty

        If (fileName.EndsWith(".ogg")) Then
            ' Welp.. our job here is done
            ConvertToOgg = fileName
            Exit Function
        End If

        Dim outputFile As String = fileName.Substring(0, fileName.LastIndexOf(".")) & ".ogg"
        Dim args As String = $"-y -i ""{fileName}"" -vn -c:a libvorbis -q:a 10 ""{outputFile}"""

        Dim ffmpegThread As Thread

        Dim ffmpeg As Process = New Process()
        ffmpeg.StartInfo.FileName = "ffmpeg.exe"
        ffmpeg.StartInfo.Arguments = args
        ffmpeg.StartInfo.WindowStyle = ProcessWindowStyle.Hidden

        'ffmpeg.Start()
        ffmpegThread = New Thread(New ParameterizedThreadStart(AddressOf ExecuteFFMPEG))
        ffmpegThread.Start(ffmpeg)

        While ffmpegThread.IsAlive
            Thread.Sleep(100)
        End While

        If (File.Exists(outputFile)) Then
            ConvertToOgg = outputFile
        End If
    End Function

    Private Sub ExecuteFFMPEG(ByVal p As Process)
        p.Start()
        p.WaitForExit()
    End Sub
End Class
