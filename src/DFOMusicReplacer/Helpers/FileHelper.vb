Imports System.IO

Module FileHelper
    Private Const _FilePath As String = "recent.x"

    ''' <summary>
    ''' Save the current file, without overwriting any previous versions.
    ''' </summary>
    ''' <param name="path">The file path to save.</param>
    Public Sub BackupFile(ByVal path As String)
        BackupFile(path.Substring(0, path.LastIndexOf(".")), path.Substring(path.LastIndexOf(".")), 0)
    End Sub

    ''' <summary>
    ''' Recursive function to find the most recent file we can save as.
    ''' </summary>
    ''' <param name="path">The path of the file to save.</param>
    ''' <param name="ext">The extension of the file.</param>
    ''' <param name="num">The current number we're on. This ultimately makes it not overwrite previous versions.</param>
    Private Sub BackupFile(ByVal path As String, ByVal ext As String, ByVal num As Integer)
        If (File.Exists(path & "____" & num & ext)) Then
            BackupFile(path, ext, num + 1)
        Else
            File.Move(path & ext, path & "____" & num & ext)
        End If
    End Sub

    ''' <summary>
    ''' Get the most recently used filepath for replacement audio.
    ''' </summary>
    ''' <returns>The most recently used filepath for replacement audio. Returns Nothing if no file exists.</returns>
    Public Function GetMostRecentPath() As String
        GetMostRecentPath = Nothing

        If File.Exists(_FilePath) Then
            Using sr = New StreamReader(_FilePath)
                Dim testPath = sr.ReadLine()

                If Directory.Exists(testPath) Then
                    GetMostRecentPath = testPath
                End If
            End Using
        End If
    End Function

    ''' <summary>
    ''' Write the most recently used filepath, for replacement audio, to file.
    ''' </summary>
    ''' <param name="path">The path to the recently used filepath, for replacement audio.</param>
    Public Sub SetMostRecentPath(ByVal path As String)
        If File.Exists(_FilePath) Then
            File.Delete(_FilePath)
        End If

        Using sw As New StreamWriter(_FilePath)
            sw.Write(path)
            sw.Close()
        End Using
    End Sub
End Module
