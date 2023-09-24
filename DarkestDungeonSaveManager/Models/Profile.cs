using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Barrent.Common.WPF.Events;
using Barrent.Common.WPF.Interfaces.Models;
using Barrent.Common.WPF.Models;
using DarkestDungeonSaveManager.Interfaces.Models;
using DarkestDungeonSaveManager.Interfaces.Serialization;
using DarkestDungeonSaveManager.Interfaces.Services;
using DarkestDungeonSaveManager.Serialization.Estate;

namespace DarkestDungeonSaveManager.Models;

public class Profile : IProfile
{

    private readonly IBackupService _backupService;
    private readonly List<ISaveGame> _saves;
    private readonly ISaveGameSerializer _serializer;
    public Profile(string path, ISaveGameSerializer serializer, IBackupService backupService)
    {
        _serializer = serializer;
        _backupService = backupService;
        var directory = new DirectoryInfo(path);

        FolderName = new Parameter<string>(directory.Name);
        FolderPath = new Parameter<string>(path);
        DisplayName = new Parameter<string>(directory.Name);
        ActiveSave = new SaveGame() { Path = FolderPath.Value };
        LoadSaveGameData(ActiveSave);

        _saves = new List<ISaveGame>();
        LoadBackups();
    }

    public event EventHandler<IProfile, SaveGameAddedEventArgs> SaveGameAdded;

    public event EventHandler<IProfile, SaveGameDeletedEventArgs> SaveGameDeleted;
    public void Load(ISaveGame saveGame)
    {
        _backupService.Load(FolderPath.Value, saveGame);
        LoadSaveGameData(ActiveSave);
    }

    public ISaveGame ActiveSave { get; }

    public IParameter<string> DisplayName { get; }

    public IParameter<string> FolderName { get; }

    public IParameter<string> FolderPath { get; }

    public IReadOnlyList<ISaveGame> Saves
    {
        get { return _saves; }
    }

    public void Delete(ISaveGame saveGame)
    {
        if (!_saves.Remove(saveGame))
        {
            return;
        };

        _backupService.Delete(saveGame);

        SaveGameDeleted?.Invoke(this, new SaveGameDeletedEventArgs(saveGame));
    }

    public void LoadBackups()
    {
        _saves.Clear();
        foreach (var saveGamePath in _backupService.GetSaveGamePaths(FolderName.Value))
        {
            var saveGame = new SaveGame() { Path = saveGamePath };
            LoadSaveGameData(saveGame);
            _saves.Add(saveGame);
            SaveGameAdded?.Invoke(this, new SaveGameAddedEventArgs(saveGame));
        }
    }

    public ISaveGame Save()
    {
        var path = _backupService.Save(FolderName.Value, ActiveSave);
        var saveGame = new SaveGame() { Path = path };
        LoadSaveGameData(saveGame);
        _saves.Add(saveGame);
        SaveGameAdded?.Invoke(this, new SaveGameAddedEventArgs(saveGame));

        return saveGame;
    }

    private void LoadSaveGameData(ISaveGame saveGame)
    {
        var campaignLog = _serializer.ReadPersistCampaignLog(saveGame.Path);
        if(campaignLog != null)
        {
            // in game, week # is zero-based
            saveGame.Week.Value = campaignLog.BaseRoot.TotalWeeks - 1;
        }

        var persistGame = _serializer.ReadPersistGame(saveGame.Path);
        if (persistGame != null)
        {
            saveGame.EstateName.Value = persistGame.BaseRoot.EstateName;
            saveGame.Difficulty.Value = persistGame.BaseRoot.GameMode;
            saveGame.IsInRaid.Value = persistGame.BaseRoot.IsInRaid;
            saveGame.Date.Value = persistGame.BaseRoot.DateTime;
        }

        var persistEstate = _serializer.ReadPersistEstate(saveGame.Path);

        if (persistEstate != null)
        {
            var wallet = persistEstate.BaseRoot.Wallet;
            saveGame.Gold.Value = wallet.Values.First(v => v.Type == ResourceType.Gold).Amount;
            saveGame.Busts.Value = wallet.Values.First(v => v.Type == ResourceType.Bust).Amount;
            saveGame.Portraits.Value = wallet.Values.First(v => v.Type == ResourceType.Portrait).Amount;
            saveGame.Deeds.Value = wallet.Values.First(v => v.Type == ResourceType.Deed).Amount;
            saveGame.Crests.Value = wallet.Values.First(v => v.Type == ResourceType.Crest).Amount;
            saveGame.Blueprints.Value = wallet.Values.First(v => v.Type == ResourceType.Blueprint).Amount;
            saveGame.Shards.Value = wallet.Values.First(v => v.Type == ResourceType.Shard).Amount;
            saveGame.Memories.Value = wallet.Values.First(v => v.Type == ResourceType.Memory).Amount;
        }
    }
}