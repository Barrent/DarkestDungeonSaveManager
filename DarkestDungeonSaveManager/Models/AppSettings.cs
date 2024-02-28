using Barrent.Common.Interfaces.Models;
using Barrent.Common.Models;
using DarkestDungeonSaveManager.Interfaces.Models;
using DarkestDungeonSaveManager.Resources;

namespace DarkestDungeonSaveManager.Models;

public class AppSettings : IAppSettings
{
    public AppSettings()
    {
        SaveGameFolderPath = new Parameter<string>(null) { Name = Strings.SavegameFolder };
        BackupFolderPath = new Parameter<string>(null) { Name = Strings.BackupFolder };
    }

    /// <summary>
    /// Folder where the game stores game profiles.
    /// D:\Program Files (x86)\Steam\userdata\112191091\262060\remote
    /// </summary>
    public IParameter<string> SaveGameFolderPath { get; }

    /// <summary>
    /// Folder to store save game backups
    /// </summary>
    public IParameter<string> BackupFolderPath { get; }
}