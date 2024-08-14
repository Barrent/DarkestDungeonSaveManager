using System;
using Barrent.Common.Interfaces.Models;
using Barrent.Common.Models;
using DarkestDungeonSaveManager.Interfaces.Models;
using DarkestDungeonSaveManager.Resources;

namespace DarkestDungeonSaveManager.Models;

/// <summary>
/// Save game data.
/// </summary>
public class SaveGame : ISaveGame
{
    /// <summary>
    /// Initializes a new instance of <see cref="SaveGame"/>.
    /// </summary>
    public SaveGame(string path)
    {
        Path = path;

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
        IsInRaid = new Parameter<bool>(false);
        Date = new Parameter<DateTime>(DateTime.MinValue);
    }

    /// <summary>
    /// Amount of blueprints.
    /// </summary>
    public IParameter<int> Blueprints { get; }

    /// <summary>
    /// Amount of busts.
    /// </summary>
    public IParameter<int> Busts { get; }

    /// <summary>
    /// Amount of crests.
    /// </summary>
    public IParameter<int> Crests { get; }

    /// <summary>
    /// Save game date.
    /// </summary>
    public IParameter<DateTime> Date { get; set; }

    /// <summary>
    /// Amount of deeds.
    /// </summary>
    public IParameter<int> Deeds { get; }

    /// <summary>
    /// Difficulty level.
    /// </summary>
    public IParameter<string> Difficulty { get; set; }

    /// <summary>
    /// Profile name displayed in the game.
    /// </summary>
    public IParameter<string> EstateName { get; }

    /// <summary>
    /// Amount of gold.
    /// </summary>
    public IParameter<int> Gold { get; }

    /// <summary>
    /// Flag indicating if there is a raid in process.
    /// </summary>
    public IParameter<bool> IsInRaid { get; set; }

    /// <summary>
    /// Amount of memories.
    /// </summary>
    public IParameter<int> Memories { get; }

    /// <summary>
    /// Path to the save game.
    /// </summary>
    public string Path { get; }

    /// <summary>
    /// Amount of portraits.
    /// </summary>
    public IParameter<int> Portraits { get; }

    /// <summary>
    /// Amount of shards.
    /// </summary>
    public IParameter<int> Shards { get; }

    /// <summary>
    /// Number of weeks passed.
    /// </summary>
    public IParameter<int> Week { get; }
}