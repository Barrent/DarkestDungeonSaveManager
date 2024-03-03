namespace DarkestDungeonSaveManager.Interfaces.Services;

public interface ISettingsService
{
    /// <summary>
    /// Reloads settings from file.
    /// </summary>
    void LoadSettings();

    /// <summary>
    /// Shows settings editor.
    /// </summary>
    void ShowEditor();
}