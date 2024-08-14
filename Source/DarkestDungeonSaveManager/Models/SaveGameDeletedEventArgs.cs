using System;
using DarkestDungeonSaveManager.Interfaces.Models;

namespace DarkestDungeonSaveManager.Models;

/// <summary>
/// Args of event publish on save game deletion.
/// </summary>
public class SaveGameDeletedEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of <see cref="SaveGameDeletedEventArgs"/>.
    /// </summary>
    /// <param name="saveGame">Deleted save game.</param>
    public SaveGameDeletedEventArgs(ISaveGame saveGame)
    {
        SaveGame = saveGame;
    }

    /// <summary>
    /// Deleted save game.
    /// </summary>
    public ISaveGame SaveGame { get; }
}