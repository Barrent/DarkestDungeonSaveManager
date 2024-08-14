using System.IO;
using Barrent.Common.WPF.Interfaces.Services;
using Barrent.Common.WPF.Services;
using DarkestDungeonSaveManager.Interfaces.Models;
using DarkestDungeonSaveManager.Interfaces.Serialization;
using DarkestDungeonSaveManager.Interfaces.Services;
using DarkestDungeonSaveManager.ViewModels;
using DarkestDungeonSaveManager.Views;

namespace DarkestDungeonSaveManager.Services;

/// <summary>
/// Loads/Edits app settings.
/// </summary>
public class SettingsService : ISettingsService
{
    /// <summary>
    /// Dialog service.
    /// </summary>
    private readonly IDialogService _dialogService;

    /// <summary>
    /// App settings.
    /// </summary>
    private readonly IAppSettings _settings;

    /// <summary>
    /// (De)Serializes settings.
    /// </summary>
    private readonly ISettingsSerializer _settingsSerializer;

    /// <summary>
    /// Initializes a new instance of <see cref="SettingsService"/>.
    /// </summary>
    /// <param name="dialogService">Dialog service.</param>
    /// <param name="settingsSerializer">Settings serializer.</param>
    /// <param name="settings">App settings.</param>
    public SettingsService(IDialogService dialogService, 
        ISettingsSerializer settingsSerializer,
        IAppSettings settings)
    {
        _settingsSerializer = settingsSerializer;
        _dialogService = dialogService;
        _settings = settings;
    }

    /// <summary>
    /// Reloads settings from file.
    /// </summary>
    public void LoadSettings()
    {
        _settingsSerializer.Load(_settings);

        if (!Directory.Exists(_settings.BackupFolderPath.Value)
            || !Directory.Exists(_settings.SaveGameFolderPath.Value))
        {
            ShowEditor();
        }
    }


    /// <summary>
    /// Shows settings editor.
    /// </summary>
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

    /// <summary>
    /// Saves updated settings.
    /// </summary>
    private void SaveSettings()
    {
        _settingsSerializer.Save(_settings);
    }
}