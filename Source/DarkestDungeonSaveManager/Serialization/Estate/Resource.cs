using Newtonsoft.Json;

namespace DarkestDungeonSaveManager.Serialization.Estate;

/// <summary>
/// Resource element of persis.estate.json save game file.
/// Instead of resource name, int is used in the json.
/// </summary>
public class Resource
{
    /// <summary>
    /// amount element
    /// </summary>
    [JsonProperty(PropertyName = "amount")]
    public int Amount { get; set; }

    /// <summary>
    /// type element
    /// </summary>
    [JsonProperty(PropertyName = "type")]
    [JsonConverter(typeof(StringToResourceTypeConverter))]
    public ResourceType Type { get; set; }
}