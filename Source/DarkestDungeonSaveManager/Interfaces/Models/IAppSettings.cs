using Barrent.Common.Interfaces.Models;

namespace DarkestDungeonSaveManager.Interfaces.Models;

/// <summary>
/// Application settings.
/// </summary>
public interface IAppSettings
{
    /// <summary>
    /// Folder where the game stores game profiles.
    /// For instance, \Program Files (x86)\Steam\userdata\112191091\262060\remote
    /// </summary>
    IParameter<string> SaveGameFolderPath { get; }

    /// <summary>
    /// Folder to store save game backups
    /// For instance, \Documents\My Games\Darkest Dungeon
    /// </summary>
    IParameter<string> BackupFolderPath { get; }
}