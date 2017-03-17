Imports System.IO

Friend Module AudioHelper
    ''' <summary>
    ''' Converts the file to OGG and returns the filename, if exists.
    ''' </summary>
    ''' <param name="fileName">The file to convert.</param>
    ''' <returns>Filepath to Ogg output file.</returns>
    Public Function ConvertToOgg(ByVal fileName As String) As String
        ConvertToOgg = String.Empty

        If (fileName.ToLower().EndsWith(".ogg")) Then
            ' Welp.. our job here is done
            ConvertToOgg = fileName
            Exit Function
        End If

        Dim outputFile As String = fileName.Substring(0, fileName.LastIndexOf(".")) & ".ogg"
        Dim args As String = $"/c ffmpeg.exe -y -i ""{fileName}"" -vn -c:a libvorbis -q:a 10 ""{outputFile}"""

        Dim ffmpeg As Process = New Process()
        Dim currentTime = DateTime.Now
        ffmpeg.StartInfo.FileName = "cmd.exe"
        ffmpeg.StartInfo.Arguments = args
        ffmpeg.StartInfo.CreateNoWindow = True
        ffmpeg.StartInfo.RedirectStandardError = True
        ffmpeg.StartInfo.RedirectStandardOutput = True
        ffmpeg.StartInfo.UseShellExecute = False
        ffmpeg.Start()

        Dim output =
            $"OUTPUT FOR TIME {currentTime.ToLocalTime()}" & vbCrLf &
            "============================================" & vbCrLf &
            ffmpeg.StandardOutput.ReadToEnd() &
            "============================================" & vbCrLf &
            $"STD ERR" & vbCrLf &
            "============================================" & vbCrLf &
            ffmpeg.StandardError.ReadToEnd()

        ffmpeg.WaitForExit()
        output.WriteTextToFile($"Logs\{currentTime.Ticks.ToString()}.txt", True)

        If (File.Exists(outputFile)) Then
            ConvertToOgg = outputFile
        End If
    End Function
End Module
