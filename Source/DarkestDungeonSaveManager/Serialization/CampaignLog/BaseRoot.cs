using Newtonsoft.Json;

namespace DarkestDungeonSaveManager.Serialization.CampaignLog;

/// <summary>
/// base_root element of persist.campaign_log.json save game file.
/// </summary>
public class BaseRoot
{
    /// <summary>
    /// total_weeks element
    /// </summary>
    [JsonProperty(PropertyName = "total_weeks")]
    public int TotalWeeks { get; set; }
}