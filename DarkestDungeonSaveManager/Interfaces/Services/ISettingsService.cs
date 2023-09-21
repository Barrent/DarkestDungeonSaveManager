using DarkestDungeonSaveManager.Interfaces.Models.Settings;

namespace DarkestDungeonSaveManager.Interfaces.Services;

public interface ISettingsService
{
    IAppSettings Settings { get; }
    void LoadSettings();
    void ShowEditor();
    void SaveSettings();
}