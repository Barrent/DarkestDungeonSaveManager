using DarkestDungeonSaveManager.Interfaces.Models;
using DarkestDungeonSaveManager.Models;

namespace DarkestDungeonSaveManager.Interfaces.Serialization;

public interface ISettingsSerializer
{
    void Load(IAppSettings settings);

    void Save(IAppSettings settings);
}