using Newtonsoft.Json;

namespace DarkestDungeonSaveManager.Serialization.Estate;

public class PersistEstate
{
    [JsonProperty(PropertyName = "base_root")]
    public BaseRoot BaseRoot { get; set; }
}