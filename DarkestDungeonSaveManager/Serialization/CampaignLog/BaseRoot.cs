using Newtonsoft.Json;

namespace DarkestDungeonSaveManager.Serialization.CampaignLog;

public class BaseRoot
{
    [JsonProperty(PropertyName = "total_weeks")]
    public int TotalWeeks { get; set; }
}