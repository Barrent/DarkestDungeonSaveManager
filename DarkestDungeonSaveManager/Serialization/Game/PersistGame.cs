﻿using Newtonsoft.Json;

namespace DarkestDungeonSaveManager.Serialization.Game;

public class PersistGame
{
    [JsonProperty(PropertyName = "base_root")]
    public BaseRoot BaseRoot { get; set; }

}