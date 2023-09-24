using System.Collections.Generic;
using DarkestDungeonSaveManager.Interfaces.Models;

namespace DarkestDungeonSaveManager.Interfaces.Services;

public interface IBackupService
{
    /// <summary>
    /// Copies active save game to the backup folder.
    /// </summary>
    /// <param name="profile"></param>
    string Save(string profileFolderName, ISaveGame saveGame);

    /// <summary>
    /// Reads all the available save games from backup folder for the selected profile.
    /// </summary>
    /// <returns></returns>
    IReadOnlyList<string> GetSaveGamePaths(string profileFolderName);

    /// <summary>
    /// Copies savegame from the backup folder to the profile folder.
    /// </summary>
    /// <param name="profile"></param>
    /// <param name="saveGame"></param>
    void Load(string profileFolderName, ISaveGame saveGame);

    void Delete(ISaveGame saveGame);
}