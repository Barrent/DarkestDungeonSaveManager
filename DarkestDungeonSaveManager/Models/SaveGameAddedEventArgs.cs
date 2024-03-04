using System;
using DarkestDungeonSaveManager.Interfaces.Models;

namespace DarkestDungeonSaveManager.Models;

/// <summary>
/// Args of event publish on save game creation.
/// </summary>
public class SaveGameAddedEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of <see cref="SaveGameAddedEventArgs"/>.
    /// </summary>
    /// <param name="saveGame">Created save game.</param>
    public SaveGameAddedEventArgs(ISaveGame saveGame)
    {
        SaveGame = saveGame;
    }

    /// <summary>
    /// Created save game.
    /// </summary>
    public ISaveGame SaveGame { get; }
}