using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace DarkestDungeonSaveManager.Serialization.Game;

public class BaseRoot
{
    [JsonProperty(PropertyName = "totalelapsed")]
    public int TotalElapsed { get; set; }

    [JsonProperty(PropertyName = "inraid")]
    public bool IsInRaid { get; set; }

    [JsonProperty(PropertyName = "estatename")]
    public string EstateName { get; set; }

    [JsonProperty(PropertyName = "game_mode")]
    public string GameMode { get; set; }

    [JsonProperty(PropertyName = "date_time")]
    public DateTime DateTime { get; set; }
}