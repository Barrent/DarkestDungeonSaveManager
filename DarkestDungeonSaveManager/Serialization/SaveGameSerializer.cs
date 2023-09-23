using System.IO;
using DarkestDungeonSaveManager.Interfaces.Serialization;
using DarkestDungeonSaveManager.Native;
using Newtonsoft.Json;

namespace DarkestDungeonSaveManager.Serialization;

public class SaveGameSerializer : ISaveGameSerializer
{
    private const string PersistGameFileName = "persist.game.json";
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
}