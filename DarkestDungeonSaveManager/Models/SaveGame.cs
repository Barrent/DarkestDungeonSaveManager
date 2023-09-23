using System;
using Barrent.Common.WPF.Interfaces.Models;
using Barrent.Common.WPF.Models;
using DarkestDungeonSaveManager.Interfaces.Models;
using DarkestDungeonSaveManager.Resources;

namespace DarkestDungeonSaveManager.Models;

public class SaveGame : ISaveGame
{
    public SaveGame()
    {
        Week = new Parameter<int>(0) { Name = Strings.WeekParameterName };
        EstateName = new Parameter<string>(string.Empty) { Name = Strings.EstateParameterName };
        Difficulty = new Parameter<string>(string.Empty) { Name = Strings.DifficultyParameterName };

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
    public IParameter<int> Week { get; }

    public IParameter<int> Gold { get; }

    public IParameter<int> Busts { get; }

    public IParameter<int> Portraits { get; }

    public IParameter<int> Deeds { get; }

    public IParameter<int> Crests { get; }

    public IParameter<int> Blueprints { get; }

    public IParameter<int> Shards { get; }

    public IParameter<int> Memories { get; }
    public IParameter<string> Difficulty { get; set; }
}