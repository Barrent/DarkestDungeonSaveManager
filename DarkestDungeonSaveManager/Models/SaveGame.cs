using System;
using Barrent.Common.WPF.Interfaces.Models;
using Barrent.Common.WPF.Models;
using DarkestDungeonSaveManager.Interfaces.Models;

namespace DarkestDungeonSaveManager.Models;

public class SaveGame : ISaveGame
{
    public SaveGame()
    {
        Days = new Parameter<int>(0);
        EstateName = new Parameter<string>(String.Empty);
        Gold = new Parameter<int>(0);
        Busts = new Parameter<int>(0);
        Portraits = new Parameter<int>(0);
        Deeds = new Parameter<int>(0);
        Crests = new Parameter<int>(0);
        Blueprints = new Parameter<int>(0);
        Shards = new Parameter<int>(0);
        Memories = new Parameter<int>(0);
    }

    public IParameter<string> EstateName { get; }
    public IParameter<int> Days { get; }

    public IParameter<int> Gold { get; }

    public IParameter<int> Busts { get; }

    public IParameter<int> Portraits { get; }

    public IParameter<int> Deeds { get; }

    public IParameter<int> Crests { get; }

    public IParameter<int> Blueprints { get; }

    public IParameter<int> Shards { get; }

    public IParameter<int> Memories { get; }
}