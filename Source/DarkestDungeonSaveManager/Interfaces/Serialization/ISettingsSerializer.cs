using DarkestDungeonSaveManager.Interfaces.Models;

namespace DarkestDungeonSaveManager.Interfaces.Serialization;

/// <summary>
/// Serializes app settings.
/// </summary>
public interface ISettingsSerializer
{
    /// <summary>
    /// Loads app settings.
    /// </summary>
    /// <param name="settings">Settings.</param>
    void Load(IAppSettings settings);

    /// <summary>
    /// Saves app settings.
    /// </summary>
    /// <param name="settings">Settings.</param>
    void Save(IAppSettings settings);
}