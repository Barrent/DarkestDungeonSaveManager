using Newtonsoft.Json;

namespace DarkestDungeonSaveManager.Serialization.Estate;

/// <summary>
/// Content of persis.estate.json save game file.
/// </summary>
public class PersistEstate
{
    /// <summary>
    /// base_root element
    /// </summary>
    [JsonProperty(PropertyName = "base_root")]
    public BaseRoot? BaseRoot { get; set; }
}