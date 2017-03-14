Imports System.IO

Module FileHelper
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
End Module
