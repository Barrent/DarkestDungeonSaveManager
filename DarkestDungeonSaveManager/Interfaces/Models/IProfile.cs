using System.Collections.Generic;
using Barrent.Common.Events;
using Barrent.Common.Interfaces.Models;
using DarkestDungeonSaveManager.Models;

namespace DarkestDungeonSaveManager.Interfaces.Models;

/// <summary>
/// Game profile.
/// </summary>
public interface IProfile
{
    /// <summary>
    /// Raised on update of <see cref="Saves"/>.
    /// </summary>
    event EventHandler<IProfile, SaveGameAddedEventArgs>? SaveGameAdded;

    /// <summary>
    /// Raised on update of <see cref="Saves"/>.
    /// </summary>
    event EventHandler<IProfile, SaveGameDeletedEventArgs>? SaveGameDeleted;

    /// <summary>
    /// Active save game you are playing.
    /// </summary>
    ISaveGame ActiveSave { get; }

    /// <summary>
    /// Profile name to display in the UI.
    /// </summary>
    IParameter<string> DisplayName { get; }

    /// <summary>
    /// Folder name of game profile. (profile_0)
    /// Save game files will be save in a folder with the same number under backup folder specified in settings.
    /// For instance, \\Documents\\My Games\\Darkest Dungeon\\profile_0
    /// </summary>
    IParameter<string> FolderName { get; }

    /// <summary>
    /// Full path to game profile.
    /// For instance, Program Files (x86)\Steam\userdata\112191091\262060\remote\profile_0
    /// </summary>
    IParameter<string> FolderPath { get; }

    /// <summary>
    /// List of saves in the backup folder.
    /// </summary>
    IReadOnlyList<ISaveGame> Saves { get; }

    /// <summary>
    /// Deletes save game from the backup folder.
    /// </summary>
    /// <param name="saveGame">Save game to delete.</param>
    void Delete(ISaveGame saveGame);

    /// <summary>
    /// Loads save game from backup folder into <see cref="ActiveSave"/>.
    /// Copies files to the game profile folder.
    /// </summary>
    /// <param name="saveGame">Save game to load.</param>
    void Load(ISaveGame saveGame);


    /// <summary>
    /// Loads existing save games from backup folder into <see cref="Saves"/>.
    /// </summary>
    void LoadBackups();

    /// <summary>
    /// Saves <see cref="ActiveSave"/> into backup folder.
    /// </summary>
    /// <returns>Backed up save game.</returns>
    ISaveGame Save();
}