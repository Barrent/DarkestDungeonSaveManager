using System.IO;
using Barrent.Common.WPF.Interfaces.Models;
using Barrent.Common.WPF.Models;
using DarkestDungeonSaveManager.Interfaces.Models;
using DarkestDungeonSaveManager.Interfaces.Serialization;

namespace DarkestDungeonSaveManager.Models;

public class Profile : IProfile
{
    private readonly ISaveGameSerializer _serializer;

    public Profile(string path, ISaveGameSerializer serializer)
    {
        _serializer = serializer;
        var directory = new DirectoryInfo(path);

        FolderName = new Parameter<string>(directory.Name);
        FolderPath = new Parameter<string>(path);
        DisplayName = new Parameter<string>(directory.Name);
        ActiveSave = new SaveGame();

        LoadData();
    }

    private void LoadData()
    {
        var persistGame = _serializer.ReadPersistGame(FolderPath.Value);
        ActiveSave.Days.Value = persistGame?.BaseRoot?.TotalElapsed ?? 0;
        ActiveSave.EstateName.Value = persistGame?.BaseRoot?.EstateName;
    }

    public IParameter<string> FolderName { get; }

    public IParameter<string> FolderPath { get; }

    public IParameter<string> DisplayName { get; }

    public ISaveGame ActiveSave { get; }
}