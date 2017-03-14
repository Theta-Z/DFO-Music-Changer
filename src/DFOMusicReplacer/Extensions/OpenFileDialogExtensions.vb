﻿Imports System.IO
Imports System.Runtime.CompilerServices

Module OpenFileDialogExtensions
    ''' <summary>
    ''' Get the filepath of the file chosen by the OpenFileDialog.
    ''' This method will show the dialog & return the file path, if they close dialog without choosing an item,
    ''' an empty string is returned.
    ''' </summary>
    ''' <param name="ofd">The open file dialog showed to the user.</param>
    ''' <param name="allowedExt">The allowed extensions for the user to pick.</param>
    ''' <param name="defExt">The default extension for the open file dialog.</param>
    ''' <param name="title">The title of the dialog.</param>
    ''' <returns>Empty string, if closed or no item selected, file path of the item selected otherwise.</returns>
    <Extension()>
    Public Function GetFilePath(ByRef ofd As OpenFileDialog, ByVal allowedExt As String, ByVal defExt As String, ByVal title As String) As String
        With ofd
            .DefaultExt = defExt
            .Filter = allowedExt
            .InitialDirectory = Directory.GetCurrentDirectory().Substring(0, 3) ' [Drive]:\
            .Title = title

            Dim result As DialogResult = .ShowDialog()
            If (Not (result = DialogResult.Cancel) And .CheckFileExists) Then
                GetFilePath = .FileName.Substring(0, .FileName.LastIndexOf("\"))
            Else ' They probably closed the dialog
                GetFilePath = String.Empty
            End If
        End With
    End Function
End Module