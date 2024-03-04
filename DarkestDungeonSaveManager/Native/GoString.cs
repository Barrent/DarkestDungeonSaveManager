namespace DarkestDungeonSaveManager.Native;

/// <summary>
/// Go-compatible string.
/// https://stackoverflow.com/questions/75534603/cgo-c-sharp-string-array-to-go-slice
/// </summary>
public struct GoString
{
    /// <summary>
    /// String content
    /// </summary>
    public string msg;

    /// <summary>
    /// String length.
    /// </summary>
    public long len;

    /// <summary>
    /// Initializes a new instance of <see cref="GoString"/>
    /// </summary>
    /// <param name="msg">String content</param>
    public GoString(string msg)
    {
        this.msg = msg;
        this.len = msg.Length;
    }
}