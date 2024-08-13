using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Barrent.Common.Events;
using Barrent.Common.Interfaces.Models;
using DarkestDungeonSaveManager.Interfaces.Models;
using DarkestDungeonSaveManager.Interfaces.Serialization;
using DarkestDungeonSaveManager.Interfaces.Services;

namespace DarkestDungeonSaveManager.Models;

/// <summary>
/// Stores game profiles.
/// </summary>
public class ProfileManager : IProfileManager
{
    /// <summary>
    /// Copies save games from the game folder to the storage and back.
    /// </summary>
    private readonly IBackupService _backupService;

    /// <summary>
    /// Parses save game data.
    /// </summary>
    private readonly ISaveGameParser _parser;

    /// <summary>
    /// Existing game profiles.
    /// </summary>
    private readonly List<IProfile> _profiles;

    /// <summary>
    /// App settings.
    /// </summary>
    private readonly IAppSettings _settings;

    /// <summary>
    /// Initializes a new instance of <see cref="ProfileManager"/>.
    /// </summary>
    /// <param name="settings">App settings.</param>
    /// <param name="parser">Parses save game data.</param>
    /// <param name="backupService">Copies save games from the game folder to the storage and back.</param>
    public ProfileManager(IAppSettings settings, ISaveGameParser parser, IBackupService backupService)
    {
        _settings = settings;
        _parser = parser;
        _backupService = backupService;
        _profiles = new List<IProfile>();
        _settings.SaveGameFolderPath.ValueChanged += OnSaveGameFolderChanged;
        _settings.BackupFolderPath.ValueChanged += OnBackupFolderChanged;
        LoadProfiles();
    }

    /// <summary>
    /// Raised on change of <see cref="IProfileManager.Profiles"/>.
    /// </summary>
    public event EventHandler<IProfileManager, EventArgs>? ProfilesChanged;

    /// <summary>
    /// Currently active game profile.
    /// </summary>
    public IProfile? ActiveProfile { get; set; }

    /// <summary>
    /// Exiting game profiles.
    /// </summary>
    public IReadOnlyList<IProfile> Profiles => _profiles;

    /// <summary>
    /// Loads game profiles from the game folder.
    /// </summary>
    public void LoadProfiles()
    {
        _profiles.Clear();

        if (!Directory.Exists(_settings.SaveGameFolderPath.Value))
        {
            ProfilesChanged?.Invoke(this, EventArgs.Empty);
            return;
        }

        var folders = Directory.EnumerateDirectories(_settings.SaveGameFolderPath.Value);
        _profiles.AddRange(folders.Select(CreateProfile));

        ProfilesChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Creates a game profile based on a path to a profile folder under the game folder.
    /// </summary>
    /// <param name="path">Path to a profile under the game folder.</param>
    /// <returns>Game profile.</returns>
    private Profile CreateProfile(string path)
    {
        var profile = new Profile(path, _parser, _backupService);
       

        return profile;
    }

    /// <summary>
    /// Handles change of backup folder.
    /// </summary>
    /// <param name="sender">Event sender.</param>
    /// <param name="args">Event args.</param>
    private void OnBackupFolderChanged(IParameter<string> sender, ParameterValueChangedEventArgs<string?> args)
    {
        foreach (var profile in Profiles)
        {
            profile.LoadBackups();
        }
    }

    /// <summary>
    /// Handles change of the game folder.
    /// </summary>
    /// <param name="sender">Event sender.</param>
    /// <param name="args">Event args.</param>
    private void OnSaveGameFolderChanged(IParameter<string> sender, ParameterValueChangedEventArgs<string?> args)
    {
        LoadProfiles();
    }
}