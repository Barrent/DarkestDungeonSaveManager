namespace DarkestDungeonSaveManager.Serialization;

/// <summary>
/// Settings to serialize.
/// </summary>
public class SerializableSettings
{
    /// <summary>
    /// Path to game profile folder.
    /// \Program Files (x86)\Steam\userdata\112191091\262060\remote
    /// </summary>
    public string? SaveGameFolderPath { get; set; }

    /// <summary>
    /// Path to backup folder.
    /// \Documents\My Games\Darkest Dungeon
    /// </summary>
    public string? BackupFolderPath { get; set; }
}