using System;
using System.Runtime.InteropServices;

namespace DarkestDungeonSaveManager.Native;

public static class DarkestSavior
{
    const string libName = "darkest-savior.so";

    [DllImport(libName, CharSet = CharSet.Unicode)]
    public static extern IntPtr Convert(GoString path);

    public static string Convert(string path)
    {
        var goPath = new GoString(path);
        var ptr = Convert(goPath);
        return Marshal.PtrToStringAnsi(ptr);
    }
}