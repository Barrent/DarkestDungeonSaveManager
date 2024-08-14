using Newtonsoft.Json;

namespace DarkestDungeonSaveManager.Serialization.CampaignLog;

/// <summary>
/// Content of persist.campaign_log.json save game file.
/// </summary>
public class PersistCampaignLog
{
    /// <summary>
    /// base_root element
    /// </summary>
    [JsonProperty(PropertyName = "base_root")]
    public BaseRoot? BaseRoot { get; set; }
}