using System.IO;
using Barrent.Common.WPF.Interfaces.Services;
using Barrent.Common.WPF.Services;
using DarkestDungeonSaveManager.Interfaces.Models.Settings;
using DarkestDungeonSaveManager.Interfaces.Services;
using DarkestDungeonSaveManager.Models.Settings;
using DarkestDungeonSaveManager.ViewModels;
using DarkestDungeonSaveManager.Views;

namespace DarkestDungeonSaveManager.Services;

public class SettingsService : ISettingsService
{
    private readonly ISettingsSerializer _serializer;
    private readonly IDialogService _dialogService;

    public SettingsService(ISettingsSerializer serializer, IDialogService dialogService)
    {
        _serializer = serializer;
        _dialogService = dialogService;
        Settings = new AppSettings();
    }

    public IAppSettings Settings { get; }

    public void LoadSettings()
    {
        _serializer.Load(Settings);

        if (!Directory.Exists(Settings.BackupFolderPath.Value) || !Directory.Exists(Settings.SaveGameFolderPath.Value))
        {
            ShowEditor();
        }
    }

    public void SaveSettings()
    {
        _serializer.Save(Settings);
    }

    public void ShowEditor()
    {
        var window = new SettingsWindow();
        var windowController = new WindowController(window);
        var viewModel = new SettingsWindowViewModel(windowController, _dialogService, Settings);
        window.DataContext = viewModel;
        if (_dialogService.ShowDialog(window) == true)
        {
            SaveSettings();
        };
    }
}