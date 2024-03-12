using System;
using System.IO;
using DarkestDungeonSaveManager.Interfaces.Models;
using DarkestDungeonSaveManager.Interfaces.Serialization;
using Newtonsoft.Json;

namespace DarkestDungeonSaveManager.Serialization;

/// <summary>
/// Serializes app settings.
/// </summary>
public class SettingsSerializer : ISettingsSerializer
{
    /// <summary>
    /// Path to settings file.
    /// </summary>
    private readonly string _settingFilePath;

    /// <summary>
    /// Initializes a new instance of <see cref="SettingsSerializer"/>.
    /// </summary>
    public SettingsSerializer()
    {
        _settingFilePath = Path.Combine(Environment.CurrentDirectory, "Settings.json");
    }

    /// <summary>
    /// Loads app settings.
    /// </summary>
    /// <param name="settings">Settings.</param>
    public void Load(IAppSettings settings)
    {
        if (!File.Exists(_settingFilePath))
        {
            return;
        }
        var serialized = File.ReadAllText(_settingFilePath);
        var serializable = JsonConvert.DeserializeObject<SerializableSettings>(serialized);

        settings.SaveGameFolderPath.Value = serializable.SaveGameFolderPath;
        settings.BackupFolderPath.Value = serializable.BackupFolderPath;
    }

    /// <summary>
    /// Saves app settings.
    /// </summary>
    /// <param name="settings">Settings.</param>
    public void Save(IAppSettings settings)
    {
        var serializable = Serialize(settings);
        var serialized = JsonConvert.SerializeObject(serializable, Formatting.Indented);
        File.WriteAllText(_settingFilePath, serialized);
    }

    /// <summary>
    /// Serializes settings.
    /// </summary>
    /// <param name="settings">Settings to serialize.</param>
    /// <returns>Serialized settings.</returns>
    private SerializableSettings Serialize(IAppSettings settings)
    {
        return new SerializableSettings
        {
            SaveGameFolderPath = settings.SaveGameFolderPath.Value,
            BackupFolderPath = settings.BackupFolderPath.Value
        };
    }
}