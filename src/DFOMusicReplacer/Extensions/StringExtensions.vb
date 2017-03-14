Imports System.Runtime.CompilerServices

Public Module StringExtensions
    ''' <summary>
    ''' Return if the current string is null or whitespace.
    ''' </summary>
    ''' <param name="s">The string to test.</param>
    ''' <returns>True/False</returns>
    <Extension()>
    Public Function IsNullOrWhiteSpace(ByVal s As String) As Boolean
        IsNullOrWhiteSpace = String.IsNullOrWhiteSpace(s)
    End Function
End Module