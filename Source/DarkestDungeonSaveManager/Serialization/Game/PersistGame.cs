using Newtonsoft.Json;

namespace DarkestDungeonSaveManager.Serialization.Game;

/// <summary>
/// Content of persist.game.json save game file.
/// </summary>
public class PersistGame
{
    /// <summary>
    /// base_root child element
    /// </summary>
    [JsonProperty(PropertyName = "base_root")]
    public BaseRoot BaseRoot { get; set; }

}