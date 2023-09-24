using System;
using System.IO;
using System.Linq;
using DarkestDungeonSaveManager.Interfaces.Serialization;
using DarkestDungeonSaveManager.Native;
using DarkestDungeonSaveManager.Serialization.CampaignLog;
using DarkestDungeonSaveManager.Serialization.Estate;
using DarkestDungeonSaveManager.Serialization.Game;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DarkestDungeonSaveManager.Serialization;

public class SaveGameSerializer : ISaveGameSerializer
{
    private const string PersistCampaignLogFileName = "persist.campaign_log.json";
    private const string PersistEstateFileName = "persist.estate.json";
    private const string PersistGameFileName = "persist.game.json";
    public PersistCampaignLog? ReadPersistCampaignLog(string folderPath)
    {
        var filePath = Path.Combine(folderPath, PersistCampaignLogFileName);
        return ReadFile<PersistCampaignLog>(filePath) ?? HackCampaignLog(filePath);
    }

    public PersistEstate? ReadPersistEstate(string folderPath)
    {
        return ReadFile<PersistEstate>(folderPath, PersistEstateFileName);
    }

    public PersistGame? ReadPersistGame(string folderPath)
    {
        return ReadFile<PersistGame>(folderPath, PersistGameFileName);

    }

    private PersistCampaignLog HackCampaignLog(string path)
    {
        if (!File.Exists(path))
        {
            return null;
        }

        var bytes = File.ReadAllBytes(path);
        var weekBytes = new[]
        {
            bytes[^4],
            bytes[^5],
            bytes[^6],
            bytes[^7]
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

    private T? ReadFile<T>(string folderPath, string fileName)
        where T : class
    {
        var filePath = Path.Combine(folderPath, fileName);
        return ReadFile<T>(filePath);
    }
}
