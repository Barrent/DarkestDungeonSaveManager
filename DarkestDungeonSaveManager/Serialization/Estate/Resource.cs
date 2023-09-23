using Newtonsoft.Json;

namespace DarkestDungeonSaveManager.Serialization.Estate;

public class Resource
{
    [JsonProperty(PropertyName = "amount")]
    public int Amount { get; set; }

    [JsonProperty(PropertyName = "type")]
    [JsonConverter(typeof(StringToResourceTypeConverter))]
    public ResourceType Type { get; set; }
}