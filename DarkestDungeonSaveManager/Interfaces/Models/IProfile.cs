using System.Collections.Generic;
using Barrent.Common.Events;
using Barrent.Common.Interfaces.Models;
using DarkestDungeonSaveManager.Models;

namespace DarkestDungeonSaveManager.Interfaces.Models;

public interface IProfile
{
    IParameter<string> FolderName { get; }
    IParameter<string> FolderPath { get; }
    IParameter<string> DisplayName { get; }
    ISaveGame ActiveSave { get; }
    IReadOnlyList<ISaveGame> Saves { get; }

    ISaveGame Save();

    void Delete(ISaveGame saveGame);

    void LoadBackups();
    event EventHandler<IProfile, SaveGameAddedEventArgs> SaveGameAdded;
    event EventHandler<IProfile, SaveGameDeletedEventArgs> SaveGameDeleted;
    void Load(ISaveGame saveGame);
}