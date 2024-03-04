using System.Collections.Generic;
using DarkestDungeonSaveManager.Interfaces.Models;

namespace DarkestDungeonSaveManager.Interfaces.Services;

/// <summary>
/// Copies save games from the game folder to the storage and back.
/// </summary>
public interface IBackupService
{
    /// <summary>
    /// Deletes save game from backup storage.
    /// </summary>
    /// <param name="saveGame">Save game to delete.</param>
    void Delete(ISaveGame saveGame);

    /// <summary>
    /// Reads all the available save games from backup folder for the specified profile.
    /// </summary>
    /// <param name="profile">Game profile.</param>
    /// <returns>Full paths to backed uo save games.</returns>
    IReadOnlyList<string> GetSaveGamePaths(IProfile profile);

    /// <summary>
    /// Copies save game from the backup folder to the profile folder.
    /// </summary>
    /// <param name="profile">Game profile.</param>
    /// <param name="saveGame">Save game to load.</param>
    void Load(IProfile profile, ISaveGame saveGame);

    /// <summary>
    /// Copies active save game to the backup folder.
    /// </summary>
    /// <param name="profile">Game profile.</param>
    /// <param name="saveGame">Save game to backup.</param>
    string Save(IProfile profile, ISaveGame saveGame);
}