using DarkestDungeonSaveManager.Models.Settings;

namespace DarkestDungeonSaveManager.Interfaces.Models.Settings;

public interface ISettingsSerializer
{
    void Load(IAppSettings settings);

    void Save(IAppSettings settings);
}