using System.IO;

namespace DarkestDungeonSaveManager.Models.Settings;

public class Profile
{
    public Profile(string path)
    {
        var directory = new DirectoryInfo(path);
        FolderName = directory.Name;
        FolderPath = path;
        DisplayName = FolderName;
    }

    public string FolderName { get; }

    public string FolderPath { get; }

    public string DisplayName { get; set; }
}