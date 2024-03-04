using System.Collections.Generic;
using System.IO;
using System.Linq;
using Barrent.Common.Events;
using Barrent.Common.Interfaces.Models;
using Barrent.Common.Models;
using DarkestDungeonSaveManager.Interfaces.Models;
using DarkestDungeonSaveManager.Interfaces.Serialization;
using DarkestDungeonSaveManager.Interfaces.Services;
using DarkestDungeonSaveManager.Serialization.Estate;

namespace DarkestDungeonSaveManager.Models;

/// <summary>
/// Game profile.
/// </summary>
public class Profile : IProfile
{
    /// <summary>
    /// Service to move save games to a backup folder.
    /// </summary>
    private readonly IBackupService _backupService;

    /// <summary>
    /// Reads parameters from a save game.
    /// </summary>
    private readonly ISaveGameParser _parser;

    /// <summary>
    /// Existing save games in a backup folder.
    /// </summary>
    private readonly List<ISaveGame> _saves;
    /// <summary>
    /// Initializes a new instance of <see cref="Profile"/>.
    /// </summary>
    /// <param name="path">Full path to game profile. Program Files (x86)\Steam\userdata\112191091\262060\remote\profile_0</param>
    /// <param name="parser">Save game data parser.</param>
    /// <param name="backupService">Service to move save games to a backup folder.</param>
    public Profile(string path, ISaveGameParser parser, IBackupService backupService)
    {
        _parser = parser;
        _backupService = backupService;
        var directory = new DirectoryInfo(path);

        FolderName = new Parameter<string>(directory.Name);
        FolderPath = new Parameter<string>(path);
        DisplayName = new Parameter<string>(directory.Name);
        ActiveSave = new SaveGame(FolderPath.Value!);
        LoadSaveGameData(ActiveSave);

        _saves = new List<ISaveGame>();
        LoadBackups();
    }

    /// <summary>
    /// Raised on update of <see cref="IProfile.Saves"/>.
    /// </summary>
    public event EventHandler<IProfile, SaveGameAddedEventArgs>? SaveGameAdded;

    /// <summary>
    /// Raised on update of <see cref="IProfile.Saves"/>.
    /// </summary>
    public event EventHandler<IProfile, SaveGameDeletedEventArgs>? SaveGameDeleted;

    /// <summary>
    /// Active save game you are playing.
    /// </summary>
    public ISaveGame ActiveSave { get; }

    /// <summary>
    /// Name of profile to display in the UI.
    /// </summary>
    public IParameter<string> DisplayName { get; }

    /// <summary>
    /// Folder name of game profile. (profile_0)
    /// Save game files will be save in a folder with the same number under backup folder specified in settings.
    /// For instance, \\Documents\\My Games\\Darkest Dungeon\\profile_0
    /// </summary>
    public IParameter<string> FolderName { get; }

    /// <summary>
    /// Full path to game profile.
    /// For instance, Program Files (x86)\Steam\userdata\112191091\262060\remote\profile_0
    /// </summary>
    public IParameter<string> FolderPath { get; }

    /// <summary>
    /// List of saves in the backup folder.
    /// </summary>
    public IReadOnlyList<ISaveGame> Saves
    {
        get { return _saves; }
    }

    /// <summary>
    /// Deletes save game from the backup folder.
    /// </summary>
    /// <param name="saveGame">Save game to delete.</param>
    public void Delete(ISaveGame saveGame)
    {
        if (!_saves.Remove(saveGame))
        {
            return;
        };

        _backupService.Delete(saveGame);

        SaveGameDeleted?.Invoke(this, new SaveGameDeletedEventArgs(saveGame));
    }

    /// <summary>
    /// Loads save game from backup folder into <see cref="ActiveSave"/>.
    /// Copies files to the game profile folder.
    /// </summary>
    /// <param name="saveGame">Save game to load.</param>
    public void Load(ISaveGame saveGame)
    {
        _backupService.Load(this, saveGame);
        LoadSaveGameData(ActiveSave);
    }

    /// <summary>
    /// Loads existing save games from backup folder into <see cref="Saves"/>.
    /// </summary>
    public void LoadBackups()
    {
        _saves.Clear();
        foreach (var saveGamePath in _backupService.GetSaveGamePaths(this))
        {
            var saveGame = new SaveGame(saveGamePath);
            LoadSaveGameData(saveGame);
            _saves.Add(saveGame);
            SaveGameAdded?.Invoke(this, new SaveGameAddedEventArgs(saveGame));
        }
    }

    /// <summary>
    /// Saves <see cref="ActiveSave"/> into backup folder.
    /// </summary>
    /// <returns>Backed up save game.</returns>
    public ISaveGame Save()
    {
        var path = _backupService.Save(this, ActiveSave);
        var saveGame = new SaveGame(path);
        LoadSaveGameData(saveGame);
        _saves.Add(saveGame);
        SaveGameAdded?.Invoke(this, new SaveGameAddedEventArgs(saveGame));

        return saveGame;
    }

    /// <summary>
    /// Reads parameters from save game files.
    /// </summary>
    /// <param name="saveGame">Save game to load.</param>
    private void LoadSaveGameData(ISaveGame saveGame)
    {
        var campaignLog = _parser.ReadPersistCampaignLog(saveGame.Path);
        if(campaignLog != null)
        {
            // in game date, week # is zero-based
            saveGame.Week.Value = campaignLog.BaseRoot.TotalWeeks - 1;
        }

        var persistGame = _parser.ReadPersistGame(saveGame.Path);
        if (persistGame != null)
        {
            saveGame.EstateName.Value = persistGame.BaseRoot.EstateName;
            saveGame.Difficulty.Value = persistGame.BaseRoot.GameMode;
            saveGame.IsInRaid.Value = persistGame.BaseRoot.IsInRaid;
            saveGame.Date.Value = persistGame.BaseRoot.DateTime;
        }

        var persistEstate = _parser.ReadPersistEstate(saveGame.Path);

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