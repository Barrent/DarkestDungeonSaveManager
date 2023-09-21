using System;
using System.IO;
using DarkestDungeonSaveManager.Interfaces.Models.Settings;
using Newtonsoft.Json;

namespace DarkestDungeonSaveManager.Models.Settings;

public class SettingsSerializer : ISettingsSerializer
{
    private readonly string _settingFilePath;

    public SettingsSerializer()
    {
        _settingFilePath = Path.Combine(Environment.CurrentDirectory, "Settings.json");
    }

    public void Load(IAppSettings settings)
    {
        if (!File.Exists(_settingFilePath))
        {
            Save(settings);
        }
        else
        {
            var serialized = File.ReadAllText(_settingFilePath);
            var serializable = JsonConvert.DeserializeObject<SerializableSettings>(serialized);
            Load(settings, serializable);
        }
    }

    private void Load(IAppSettings settings, SerializableSettings serializable)
    {
        settings.SaveGameFolderPath.Value = serializable.GameSettings.SaveGameFolderPath;
        settings.BackupFolderPath.Value = serializable.GameSettings.BackupFolderPath;
    }
        
    public void Save(IAppSettings settings)
    {
        var serializable = Serialize(settings);
        var serialized = JsonConvert.SerializeObject(serializable, Formatting.Indented);
        File.WriteAllText(_settingFilePath, serialized);
    }


    private SerializableSettings Serialize(IAppSettings settings)
    {
        return new SerializableSettings
        { 
            GameSettings = new SerializableGameSettings
            {
                SaveGameFolderPath = settings.SaveGameFolderPath.Value,
                BackupFolderPath = settings.BackupFolderPath.Value,
            }
        };
    }
}