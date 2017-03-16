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
        Dim args As String = $"-y -i ""{fileName}"" -vn -c:a libvorbis -q:a 10 ""{outputFile}"""

        Dim ffmpeg As Process = New Process()
        ffmpeg.StartInfo.FileName = "ffmpeg.exe"
        ffmpeg.StartInfo.Arguments = args
        ffmpeg.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        ffmpeg.Start()
        ffmpeg.WaitForExit()

        If (File.Exists(outputFile)) Then
            ConvertToOgg = outputFile
        End If
    End Function
End Module
