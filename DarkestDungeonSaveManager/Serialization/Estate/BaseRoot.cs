using System.Collections.Generic;
using Newtonsoft.Json;

namespace DarkestDungeonSaveManager.Serialization.Estate;

public class BaseRoot
{
    [JsonProperty(PropertyName = "wallet")]
    public Dictionary<string, Resource> Wallet { get; set; }
}