using DarkestDungeonSaveManager.Interfaces.Models;

namespace DarkestDungeonSaveManager.Interfaces.Serialization;

public interface IProfileSerializer
{
    void Load(IProfileManager profileManager);
    void Save(IProfileManager profileManager);
}