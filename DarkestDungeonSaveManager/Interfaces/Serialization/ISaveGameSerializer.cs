using DarkestDungeonSaveManager.Serialization.CampaignLog;
using DarkestDungeonSaveManager.Serialization.Estate;
using DarkestDungeonSaveManager.Serialization.Game;

namespace DarkestDungeonSaveManager.Interfaces.Serialization;

public interface ISaveGameSerializer
{
    PersistGame? ReadPersistGame(string folderPath);
    PersistEstate? ReadPersistEstate(string folderPath);
    PersistCampaignLog? ReadPersistCampaignLog(string folderPath);
}