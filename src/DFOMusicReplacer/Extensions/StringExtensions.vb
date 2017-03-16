Imports System.Runtime.CompilerServices
Imports System.Threading

Friend Module StringExtensions
    ''' <summary>
    ''' Get a lock on this string.
    ''' </summary>
    ''' <param name="s">The string to get a lock on.</param>
    <Extension()>
    Public Sub GetLock(ByRef s As String)
        While Not Monitor.TryEnter(s)
            Thread.Sleep(10)
        End While
    End Sub

    ''' <summary>
    ''' Is the string (s) equal to one of the strings within items.
    ''' Similar to SQL: s IN ('a', 'b', 'c')
    ''' </summary>
    ''' <param name="s">The initial string to test against.</param>
    ''' <param name="items">The array of strings to check if s is one of.</param>
    ''' <returns>True if items contains a string equal to s.</returns>
    <Extension()>
    Public Function IsOneOf(ByRef s As String, ParamArray items As String())
        IsOneOf = False
        For Each item In items
            If item = s Then
                IsOneOf = True
                Exit Function
            End If
        Next
    End Function

    ''' <summary>
    ''' Return if the current string is null or whitespace.
    ''' </summary>
    ''' <param name="s">The string to test.</param>
    ''' <returns>True/False</returns>
    <Extension()>
    Public Function IsNullOrWhiteSpace(ByRef s As String) As Boolean
        IsNullOrWhiteSpace = String.IsNullOrWhiteSpace(s)
    End Function

    ''' <summary>
    ''' Release the lock on this string.
    ''' </summary>
    ''' <param name="s">The string to have its lock released.</param>
    <Extension()>
    Public Sub ReleaseLock(ByRef s As String)
        Monitor.Exit(s)
    End Sub
End Module