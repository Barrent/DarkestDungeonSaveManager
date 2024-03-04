namespace DarkestDungeonSaveManager.Interfaces.Services;

/// <summary>
/// Loads/Edits app settings.
/// </summary>
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