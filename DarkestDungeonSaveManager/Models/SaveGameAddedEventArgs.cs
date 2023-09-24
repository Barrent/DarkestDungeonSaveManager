using System;
using DarkestDungeonSaveManager.Interfaces.Models;

namespace DarkestDungeonSaveManager.Models;

public class SaveGameAddedEventArgs : EventArgs
{
    public SaveGameAddedEventArgs(ISaveGame saveGame)
    {
        SaveGame = saveGame;
    }

    public ISaveGame SaveGame { get; }
}