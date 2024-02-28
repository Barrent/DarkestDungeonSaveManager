using System;
using Barrent.Common.Interfaces.Models;

namespace DarkestDungeonSaveManager.Interfaces.Models;

public interface ISaveGame
{
    IParameter<string> EstateName { get; }
    IParameter<int> Week { get; }
    IParameter<int> Gold { get; }
    IParameter<int> Busts { get; }
    IParameter<int> Portraits { get; }
    IParameter<int> Deeds { get; }
    IParameter<int> Crests { get; }
    IParameter<int> Blueprints { get; }
    IParameter<int> Shards { get; }
    IParameter<int> Memories { get; }
    IParameter<string> Difficulty { get; set; }
    IParameter<bool> IsInRaid { get; set; }
    IParameter<DateTime> Date { get; set; }

    string Path { get; set; }
}