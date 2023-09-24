using System;
using DarkestDungeonSaveManager.Interfaces.Models;

namespace DarkestDungeonSaveManager.Models;

public class SaveGameDeletedEventArgs : EventArgs
{
    public SaveGameDeletedEventArgs(ISaveGame saveGame)
    {
        SaveGame = saveGame;
    }

    public ISaveGame SaveGame { get; }
}