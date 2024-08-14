using System;
using System.IO;
using DarkestDungeonSaveManager.Interfaces.Serialization;
using DarkestDungeonSaveManager.Native;
using DarkestDungeonSaveManager.Serialization.CampaignLog;
using DarkestDungeonSaveManager.Serialization.Estate;
using DarkestDungeonSaveManager.Serialization.Game;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DarkestDungeonSaveManager.Serialization;

/// <summary>
/// Reads data from save game files.
/// </summary>
public class SaveGameParser : ISaveGameParser
{
    /// <summary>
    /// Campaign data file.
    /// </summary>
    private const string PersistCampaignLogFileName = "persist.campaign_log.json";

    /// <summary>
    /// Estate data file.
    /// </summary>
    private const string PersistEstateFileName = "persist.estate.json";

    /// <summary>
    /// Game data file.
    /// </summary>
    private const string PersistGameFileName = "persist.game.json";

    /// <summary>
    /// Reads child elements of PersistCampaignLog element in json game file.
    /// </summary>
    /// <param name="folderPath">Path to save game folder.</param>
    /// <returns>Parsed data.</returns>
    public PersistCampaignLog? ReadPersistCampaignLog(string folderPath)
    {
        var filePath = Path.Combine(folderPath, PersistCampaignLogFileName);
        return ReadFile<PersistCampaignLog>(filePath) ?? HackCampaignLog(filePath);
    }

    /// <summary>
    /// Reads child elements of PersistEstate element in json game file.
    /// </summary>
    /// <param name="folderPath">Path to save game folder.</param>
    /// <returns>Parsed data.</returns>
    public PersistEstate? ReadPersistEstate(string folderPath)
    {
        return ReadFile<PersistEstate>(folderPath, PersistEstateFileName);
    }

    /// <summary>
    /// Reads child elements of PersistGame element in json game file.
    /// </summary>
    /// <param name="folderPath">Path to save game folder.</param>
    /// <returns>Parsed data.</returns>
    public PersistGame? ReadPersistGame(string folderPath)
    {
        return ReadFile<PersistGame>(folderPath, PersistGameFileName);

    }

    /// <summary>
    /// Reads last bytes from the game file where total weeks value is expected to be.
    /// </summary>
    /// <param name="path">Path to file to parse.</param>
    /// <returns>Campaign data.</returns>
    private PersistCampaignLog? HackCampaignLog(string path)
    {
        if (!File.Exists(path))
        {
            return null;
        }

        var bytes = File.ReadAllBytes(path);
        var weekBytes = new byte[]
        {
            bytes[^4],
            bytes[^5],
            0,
            0
        };
        var totalWeeks = BitConverter.ToInt32(weekBytes);
        return new PersistCampaignLog
        {
            BaseRoot = new CampaignLog.BaseRoot
            {
                TotalWeeks = totalWeeks
            }
        };
    }

    /// <summary>
    /// Parses a file. Uses go lib to convert game files to plain json.
    /// https://github.com/thanhnguyen2187/darkest-savior/tree/master
    /// </summary>
    /// <typeparam name="T">Type of serialized data.</typeparam>
    /// <param name="filePath">Path to a file to parse.</param>
    /// <returns>Parsed data.</returns>
    private T? ReadFile<T>(string filePath)
                    where T : class
    {
        if (!File.Exists(filePath))
        {
            return null;
        }

        var content = DarkestSavior.Convert(filePath);

        var settings = new JsonSerializerSettings();
        settings.Converters.Add(new StringEnumConverter());
        return JsonConvert.DeserializeObject<T>(content, settings);
    }

    /// <summary>
    /// Parses a file. Uses go lib to convert game files to plain json.
    /// https://github.com/thanhnguyen2187/darkest-savior/tree/master
    /// </summary>
    /// <typeparam name="T">Type of serialized data.</typeparam>
    /// <param name="folderPath">Folder containing a file to parse.</param>
    /// <param name="fileName">Name of a file to parse.</param>
    /// <returns>Parsed data.</returns>
    private T? ReadFile<T>(string folderPath, string fileName)
        where T : class
    {
        var filePath = Path.Combine(folderPath, fileName);
        return ReadFile<T>(filePath);
    }
}
