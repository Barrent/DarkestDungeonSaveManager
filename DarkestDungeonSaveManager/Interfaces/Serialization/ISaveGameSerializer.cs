using DarkestDungeonSaveManager.Serialization;

namespace DarkestDungeonSaveManager.Interfaces.Serialization;

public interface ISaveGameSerializer
{
    PersistGame? ReadPersistGame(string folderPath);
}