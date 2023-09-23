using System.IO;
using Barrent.Common.WPF.Interfaces.Services;
using Barrent.Common.WPF.Services;
using DarkestDungeonSaveManager.Interfaces.Models;
using DarkestDungeonSaveManager.Interfaces.Serialization;
using DarkestDungeonSaveManager.Interfaces.Services;
using DarkestDungeonSaveManager.Serialization;
using DarkestDungeonSaveManager.ViewModels;
using DarkestDungeonSaveManager.Views;

namespace DarkestDungeonSaveManager.Services;

public class SettingsService : ISettingsService
{
    private readonly ISettingsSerializer _settingsSerializer;
    private readonly IProfileSerializer _profileSerializer;
    private readonly IDialogService _dialogService;
    private readonly IAppSettings _settings;
    private readonly IProfileManager _profileManager;

    public SettingsService(IDialogService dialogService, 
        ISettingsSerializer settingsSerializer,
        IProfileSerializer profileSerializer,
        IAppSettings settings,
        IProfileManager profileManager)
    {
        _settingsSerializer = settingsSerializer;
        _profileSerializer = profileSerializer;
        _dialogService = dialogService;
        _settings = settings;
        _profileManager = profileManager;
    }

    public void LoadSettings()
    {
        _settingsSerializer.Load(_settings);

        if (!Directory.Exists(_settings.BackupFolderPath.Value)
            || !Directory.Exists(_settings.SaveGameFolderPath.Value))
        {
            ShowEditor();
        }

        _profileSerializer.Load(_profileManager);
    }


    public void SaveSettings()
    {
        _settingsSerializer.Save(_settings);
    }

    public void ShowEditor()
    {
        var window = new SettingsWindow();
        var windowController = new WindowController(window);
        var viewModel = new SettingsWindowViewModel(windowController, _dialogService, _settings);
        window.DataContext = viewModel;
        if (_dialogService.ShowDialog(window) == true)
        {
            SaveSettings();
        };
    }
}