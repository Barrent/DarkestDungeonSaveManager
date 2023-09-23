using Barrent.Common.WPF.Interfaces.Models;

namespace DarkestDungeonSaveManager.Interfaces.Models;

public interface ISaveGame
{
    IParameter<string> EstateName { get; }

    IParameter<int> Days { get; }
}