using System;
using System.Runtime.InteropServices;

namespace DarkestDungeonSaveManager.Native;

/// <summary>
/// Calls methods from darkest-savior. See https://github.com/thanhnguyen2187/darkest-savior
/// Library is manually built with C-shared mode, that makes it possible to call it from C#
/// 
/// Added a method to main.go
/// import "C"  
/// 
/// //export Convert
/// func Convert(path string) *C.char {
///     // go build -o darkest-savior.so -buildmode=c-shared main.go
///     byteArray := cli.Convert(path)
///     str := string(byteArray[:])
///     return C.CString(str)
/// }
/// </summary>
public static class DarkestSavior
{
    /// <summary>
    /// Library name.
    /// </summary>
    const string libName = "darkest-savior.so";

    /// <summary>
    /// Deserializes json in proprietary format into plain json.
    /// </summary>
    /// <param name="path"> Path to save game json file. </param>
    /// <returns>Plain json.</returns>
    [DllImport(libName, CharSet = CharSet.Unicode)]
    public static extern IntPtr Convert(GoString path);

    /// <summary>
    /// Deserializes json in proprietary format into plain json.
    /// </summary>
    /// <param name="path"> Path to save game json file. </param>
    /// <returns>Plain json.</returns>
    public static string Convert(string path)
    {
        var goPath = new GoString(path);
        var ptr = Convert(goPath);
        return Marshal.PtrToStringAnsi(ptr)!;
    }
}