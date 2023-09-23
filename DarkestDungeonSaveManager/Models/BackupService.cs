using DarkestDungeonSaveManager.Interfaces.Models;

namespace DarkestDungeonSaveManager.Models;

public class BackupService
{
    private readonly AppSettings _settings;

    public BackupService(AppSettings settings)
    {
        _settings = settings;
    }

    public void Save(IProfile profile)
    {
        // TODO: copy files from profile folder to backup folder
    }

    public void Load(IProfile profile)
    {
        // TODO: copy files from backup folder to profile folder
    }
}