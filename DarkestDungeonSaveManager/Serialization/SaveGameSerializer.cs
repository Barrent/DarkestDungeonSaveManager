using System.IO;
using DarkestDungeonSaveManager.Interfaces.Serialization;
using DarkestDungeonSaveManager.Native;
using DarkestDungeonSaveManager.Serialization.Estate;
using DarkestDungeonSaveManager.Serialization.Game;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DarkestDungeonSaveManager.Serialization;

public class SaveGameSerializer : ISaveGameSerializer
{
    private const string PersistGameFileName = "persist.game.json";

    private const string PersistEstateFileName = "persist.estate.json";
    public PersistGame? ReadPersistGame(string folderPath)
    {
        var filePath = Path.Combine(folderPath, PersistGameFileName);
        if (!File.Exists(filePath))
        {
            return null;
        }
        var content = DarkestSavior.Convert(filePath);
        return JsonConvert.DeserializeObject<PersistGame>(content);
    }

    public PersistEstate? ReadPersistEstate(string folderPath)
    {
        var filePath = Path.Combine(folderPath, PersistEstateFileName);
        if (!File.Exists(filePath))
        {
            return null;
        }

        var settings = new JsonSerializerSettings();
        settings.Converters.Add(new StringEnumConverter());
        var content = DarkestSavior.Convert(filePath);
        return JsonConvert.DeserializeObject<PersistEstate>(content, settings);
    }
}