using Newtonsoft.Json;
using System;

namespace DarkestDungeonSaveManager.Serialization.Game;

/// <summary>
/// base_root element of persist.game.json save game file.
/// </summary>
public class BaseRoot
{
    /// <summary>
    /// totalelapsed child element
    /// </summary>
    [JsonProperty(PropertyName = "totalelapsed")]
    public int TotalElapsed { get; set; }

    /// <summary>
    /// inraid child element
    /// </summary>
    [JsonProperty(PropertyName = "inraid")]
    public bool IsInRaid { get; set; }

    /// <summary>
    /// estatename child element
    /// </summary>
    [JsonProperty(PropertyName = "estatename")]
    public string EstateName { get; set; }

    /// <summary>
    /// game_mode child element
    /// </summary>
    [JsonProperty(PropertyName = "game_mode")]
    public string GameMode { get; set; }

    /// <summary>
    /// date_time child element
    /// </summary>
    [JsonProperty(PropertyName = "date_time")]
    public DateTime DateTime { get; set; }
}