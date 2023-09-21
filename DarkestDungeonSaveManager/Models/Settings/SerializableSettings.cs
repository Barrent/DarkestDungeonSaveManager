namespace DarkestDungeonSaveManager.Models.Settings;

public class SerializableSettings
{
    public SerializableProfile[] Profiles { get; set; }

    public SerializableGameSettings GameSettings { get; set; }

    public string? SelectedProfile { get; set; }
}