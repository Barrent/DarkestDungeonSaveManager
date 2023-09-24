using Newtonsoft.Json;

namespace DarkestDungeonSaveManager.Serialization.CampaignLog;

public class PersistCampaignLog
{
    [JsonProperty(PropertyName = "base_root")]
    public BaseRoot BaseRoot { get; set; }
}