using System.Collections.Generic;
using Newtonsoft.Json;

namespace DarkestDungeonSaveManager.Serialization.Estate;

/// <summary>
/// base_root element of persis.estate.json save game file.
/// </summary>
public class BaseRoot
{
    /// <summary>
    /// wallet element
    /// </summary>
    [JsonProperty(PropertyName = "wallet")]
    public Dictionary<string, Resource> Wallet { get; set; }
}