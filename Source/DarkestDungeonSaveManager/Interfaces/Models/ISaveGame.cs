using System;
using Barrent.Common.Interfaces.Models;

namespace DarkestDungeonSaveManager.Interfaces.Models;

/// <summary>
/// Save game data.
/// </summary>
public interface ISaveGame
{
    /// <summary>
    /// Amount of blueprints.
    /// </summary>
    IParameter<int> Blueprints { get; }

    /// <summary>
    /// Amount of busts.
    /// </summary>
    IParameter<int> Busts { get; }

    /// <summary>
    /// Amount of crests.
    /// </summary>
    IParameter<int> Crests { get; }

    /// <summary>
    /// Save game date.
    /// </summary>
    IParameter<DateTime> Date { get; set; }

    /// <summary>
    /// Amount of deeds.
    /// </summary>
    IParameter<int> Deeds { get; }

    /// <summary>
    /// Difficulty level.
    /// </summary>
    IParameter<string> Difficulty { get; set; }

    /// <summary>
    /// Profile name displayed in the game.
    /// </summary>
    IParameter<string> EstateName { get; }

    /// <summary>
    /// Amount of gold.
    /// </summary>
    IParameter<int> Gold { get; }

    /// <summary>
    /// Flag indicating if there is a raid in process.
    /// </summary>
    IParameter<bool> IsInRaid { get; set; }

    /// <summary>
    /// Amount of memories.
    /// </summary>
    IParameter<int> Memories { get; }

    /// <summary>
    /// Path to the save game.
    /// </summary>
    string Path { get; }

    /// <summary>
    /// Amount of portraits.
    /// </summary>
    IParameter<int> Portraits { get; }

    /// <summary>
    /// Amount of shards.
    /// </summary>
    IParameter<int> Shards { get; }

    /// <summary>
    /// Number of weeks passed.
    /// </summary>
    IParameter<int> Week { get; }
}