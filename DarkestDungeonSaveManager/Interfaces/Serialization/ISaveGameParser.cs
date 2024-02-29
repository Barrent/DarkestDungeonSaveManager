using DarkestDungeonSaveManager.Serialization.CampaignLog;
using DarkestDungeonSaveManager.Serialization.Estate;
using DarkestDungeonSaveManager.Serialization.Game;

namespace DarkestDungeonSaveManager.Interfaces.Serialization;

/// <summary>
/// Reads data from save game files.
/// </summary>
public interface ISaveGameParser
{
    /// <summary>
    /// Reads child elements of PersistGame element in json game file.
    /// </summary>
    /// <param name="folderPath">Path to save game folder.</param>
    /// <returns>Parsed data.</returns>
    PersistGame? ReadPersistGame(string folderPath);

    /// <summary>
    /// Reads child elements of PersistEstate element in json game file.
    /// </summary>
    /// <param name="folderPath">Path to save game folder.</param>
    /// <returns>Parsed data.</returns>
    PersistEstate? ReadPersistEstate(string folderPath);

    /// <summary>
    /// Reads child elements of PersistCampaignLog element in json game file.
    /// </summary>
    /// <param name="folderPath">Path to save game folder.</param>
    /// <returns>Parsed data.</returns>
    PersistCampaignLog? ReadPersistCampaignLog(string folderPath);
}