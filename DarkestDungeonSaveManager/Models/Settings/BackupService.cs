namespace DarkestDungeonSaveManager.Models.Settings;

public class BackupService
{
    private readonly AppSettings _settings;

    public BackupService(AppSettings settings)
    {
        _settings = settings;
    }

    public void Save(Profile profile)
    {
        // TODO: copy files from profile folder to backup folder
    }

    public void Load(Profile profile)
    {
        // TODO: copy files from backup folder to profile folder
    }
}