using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Barrent.Common.WPF.Events;
using Barrent.Common.WPF.Interfaces.Models;
using DarkestDungeonSaveManager.Interfaces.Models;
using DarkestDungeonSaveManager.Interfaces.Serialization;
using DarkestDungeonSaveManager.Interfaces.Services;

namespace DarkestDungeonSaveManager.Models;

public class ProfileManager : IProfileManager
{
    private readonly List<IProfile> _profiles;

    private readonly IAppSettings _settings;
    private ISaveGameSerializer _serializer;
    private readonly IBackupService _backupService;

    public ProfileManager(IAppSettings settings, ISaveGameSerializer serializer, IBackupService backupService)
    {
        _settings = settings;
        _serializer = serializer;
        _backupService = backupService;
        _profiles = new List<IProfile>();
        _settings.SaveGameFolderPath.ValueChanged += OnSaveGameFolderChanged;
        _settings.BackupFolderPath.ValueChanged += OnBackupFolderChanged;
        LoadProfiles();
    }

    public event EventHandler<IProfileManager, EventArgs>? ProfilesChanged;

    public IProfile? ActiveProfile { get; set; }

    public IReadOnlyList<IProfile> Profiles => _profiles;

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

    private Profile CreateProfile(string path)
    {
        var profile = new Profile(path, _serializer, _backupService);
       

        return profile;
    }

    private void OnBackupFolderChanged(IParameter<string> sender, ParameterValueChangedEventArgs<string> args)
    {
        foreach (var profile in Profiles)
        {
            profile.LoadBackups();
        }
    }

    private void OnSaveGameFolderChanged(IParameter<string> sender, ParameterValueChangedEventArgs<string> args)
    {
        LoadProfiles();
    }
}