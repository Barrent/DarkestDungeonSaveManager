namespace DarkestDungeonSaveManager.Native;

public struct GoString
{
    public string msg;
    public long len;
    public GoString(string msg)
    {
        this.msg = msg;
        this.len = msg.Length;
    }
}