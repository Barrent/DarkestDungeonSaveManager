using Barrent.Common.WPF.Interfaces.Models;

namespace DarkestDungeonSaveManager.Interfaces.Models;

public interface ISaveGame
{
    IParameter<string> EstateName { get; }

    IParameter<int> Days { get; }
    IParameter<int> Gold { get; }
    IParameter<int> Busts { get; }
    IParameter<int> Portraits { get; }
    IParameter<int> Deeds { get; }
    IParameter<int> Crests { get; }
    IParameter<int> Blueprints { get; }
    IParameter<int> Shards { get; }
    IParameter<int> Memories { get; }
}