Imports System.IO
Imports System.Runtime.CompilerServices

Friend Module ListViewExtensions
    ''' <summary>
    ''' Default extension support is m4a, ogg, and mp3.
    ''' </summary>
    ''' <param name="item">the current file to check against.</param>
    ''' <returns>Whether or not this file is supported</returns>
    Private Function ExtensionSupported(ByVal item As String) As Boolean
        If item.EndsWith(".m4a", StringComparison.InvariantCultureIgnoreCase) Or
           item.EndsWith(".mp3", StringComparison.InvariantCultureIgnoreCase) Or
           item.EndsWith(".ogg", StringComparison.InvariantCultureIgnoreCase) Then
            ExtensionSupported = True
        Else
            ExtensionSupported = False
        End If
    End Function

    ''' <summary>
    ''' ListView.Items = List of files from the specified path.
    ''' Your function (extensionSupport) will be used as a filter.
    ''' </summary>
    ''' <param name="lv">The listview to be loaded into.</param>
    ''' <param name="path">The path to the desired files to be loaded into the listview.</param>
    ''' <param name="extensionSupport">The function used to filter what files are loaded into the listview.</param>
    <Extension()>
    Public Sub LoadItemsFromDirectory(ByRef lv As ListView, ByVal path As String, Optional ByVal extensionSupport As Func(Of String, Boolean) = Nothing)
        If (extensionSupport Is Nothing) Then
            extensionSupport = AddressOf ExtensionSupported
        End If

        With lv
            .Items.Clear()
            .Columns.Clear()
            .Columns.Add(New ColumnHeader() With {.Text = "FileName", .Name = "fileName", .Width = lv.Width - 21})
            ' 21 width removes the bottom scrollbar due to the 20px vert scroll width

            For Each file In Directory.GetFiles(path).Where(Function(t) extensionSupport(t)).OrderBy(Function(t) t)
                .Items.Add(file.Substring(file.LastIndexOf("\") + 1))
            Next
        End With
    End Sub
End Module
