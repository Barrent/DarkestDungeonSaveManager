using Barrent.Common.WPF.Interfaces.Models;

namespace DarkestDungeonSaveManager.Interfaces.Models;

public interface IProfile
{
    IParameter<string> FolderName { get; }
    IParameter<string> FolderPath { get; }
    IParameter<string> DisplayName { get; }
    ISaveGame ActiveSave { get; }
}