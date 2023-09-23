using System.IO;
using System.Linq;
using Barrent.Common.WPF.Interfaces.Models;
using Barrent.Common.WPF.Models;
using DarkestDungeonSaveManager.Interfaces.Models;
using DarkestDungeonSaveManager.Interfaces.Serialization;
using DarkestDungeonSaveManager.Serialization.Estate;

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
        if (persistGame != null)
        {
            ActiveSave.Days.Value = persistGame.BaseRoot.TotalElapsed;
            ActiveSave.EstateName.Value = persistGame.BaseRoot.EstateName;
        }

        var persistEstate = _serializer.ReadPersistEstate(FolderPath.Value);

        if (persistEstate != null)
        {
            var wallet = persistEstate.BaseRoot.Wallet;
            ActiveSave.Gold.Value = wallet.Values.First(v => v.Type == ResourceType.Gold).Amount;
            ActiveSave.Busts.Value = wallet.Values.First(v => v.Type == ResourceType.Bust).Amount;
            ActiveSave.Portraits.Value = wallet.Values.First(v => v.Type == ResourceType.Portrait).Amount;
            ActiveSave.Deeds.Value = wallet.Values.First(v => v.Type == ResourceType.Deed).Amount;
            ActiveSave.Crests.Value = wallet.Values.First(v => v.Type == ResourceType.Crest).Amount;
            ActiveSave.Blueprints.Value = wallet.Values.First(v => v.Type == ResourceType.Blueprint).Amount;
            ActiveSave.Shards.Value = wallet.Values.First(v => v.Type == ResourceType.Shard).Amount;
            ActiveSave.Memories.Value = wallet.Values.First(v => v.Type == ResourceType.Memory).Amount;
        }
    }

    public IParameter<string> FolderName { get; }

    public IParameter<string> FolderPath { get; }

    public IParameter<string> DisplayName { get; }

    public ISaveGame ActiveSave { get; }
}