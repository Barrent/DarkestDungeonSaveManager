using DarkestDungeonSaveManager.Interfaces.Models;

namespace DarkestDungeonSaveManager.Interfaces.Services;

public interface ISettingsService
{
    void LoadSettings();
    void ShowEditor();
}