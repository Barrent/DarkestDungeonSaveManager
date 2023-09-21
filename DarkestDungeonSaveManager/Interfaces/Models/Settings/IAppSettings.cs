using Barrent.Common.WPF.Interfaces.Models;

namespace DarkestDungeonSaveManager.Interfaces.Models.Settings;

public interface IAppSettings
{
    /// <summary>
    /// Folder where the game stores game profiles.
    /// D:\Program Files (x86)\Steam\userdata\112191091\262060\remote
    /// </summary>
    IParameter<string> SaveGameFolderPath { get; }

    /// <summary>
    /// Folder to store save game backups
    /// </summary>
    IParameter<string> BackupFolderPath { get; }
}