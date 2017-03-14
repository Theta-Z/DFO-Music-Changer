Imports Microsoft.Win32

Public Module DFOHelper
    Private Const _ChooseFile As String =
        "No path detected." & vbCrLf &
        "Please select a music file from within your DFO folder." & vbCrLf &
        "Press OK to try agian or press Cancel to cancel."
    Private Const _REGDFO As String = "\Software\Neople_DFO"
    Private Const _REGDFONAME As String = "Path"
    Private Const _REGROOT As String = "HKEY_CURRENT_USER"

    ''' <summary>
    ''' Get the path to DFO's music folder.
    ''' Null means user did not select the folder.
    ''' </summary>
    ''' <returns>Path to DFO's music folder. If null is returned, the user chose to not select it.</returns>
    Public Function GetFullPathDFO() As String
        Dim dfoPath As String = Registry.GetValue(_REGROOT & _REGDFO, _REGDFONAME, String.Empty)

        If (dfoPath.IsNullOrWhiteSpace) Then
            MessageBox.Show("Unable to find registry value for DFO path." & vbCrLf & "Please select the path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Dim ofd As OpenFileDialog = New OpenFileDialog()
            While (dfoPath.IsNullOrWhiteSpace)
                dfoPath = ofd.GetFilePath("DFO Music File (Ogg Vorbis)|*.ogg", "ogg", "Select a music file from DFO...")
                If (dfoPath.IsNullOrWhiteSpace) Then
                    If (DialogResult.Cancel = MessageBox.Show(_ChooseFile, "Whoops?", MessageBoxButtons.OKCancel)) Then
                        GetFullPathDFO = Nothing
                        Exit Function
                    End If
                End If
            End While

            GetFullPathDFO = dfoPath & "\"
        Else
            GetFullPathDFO = dfoPath & "Music\"
        End If
    End Function
End Module
