using System.Collections.ObjectModel;
using Barrent.Common.WPF.Commands;
using Barrent.Common.WPF.Interfaces.Services;
using Barrent.Common.WPF.Interfaces.ViewModels;
using Barrent.Common.WPF.Services;
using Barrent.Common.WPF.ViewModels;
using DarkestDungeonSaveManager.Interfaces.Services;
using DarkestDungeonSaveManager.Interfaces.ViewModels;
using DarkestDungeonSaveManager.Resources;
using Microsoft.Extensions.DependencyInjection;

namespace DarkestDungeonSaveManager.ViewModels;

/// <summary>
/// Main menu.
/// </summary>
public class MainMenuViewModel : IMainMenuViewModel
{
    /// <summary>
    /// Controls the main window of the app.
    /// </summary>
    private readonly IWindowController _mainWindow;

    /// <summary>
    /// App settings.
    /// </summary>
    private readonly ISettingsService _settingsService;

    /// <summary>
    /// Initializes a new instance of <see cref="MainMenuViewModel"/>.
    /// </summary>
    /// <param name="settingsService">App settings.</param>
    /// <param name="mainWindow">Controls the main window of the app.</param>
    public MainMenuViewModel(ISettingsService settingsService, 
        [FromKeyedServices(ServiceKey.Main)] IWindowController mainWindow)
    {
        _settingsService = settingsService;
        _mainWindow = mainWindow;
        Items = new ObservableCollection<IMenuItemViewModel> { CreateFileMenuItem() };
    }

    /// <summary>
    /// Menu items.
    /// </summary>
    public ObservableCollection<IMenuItemViewModel> Items { get; }

    /// <summary>
    /// Menu item to exit the app.
    /// </summary>
    /// <returns> Menu item. </returns>
    private IMenuItemViewModel CreateExitMenuItem()
    {
        return new MenuItemViewModel()
        {
            Header = MenuResources.ExitHeader,
            Command = new RelayCommand(Exit)
        };
    }

    /// <summary>
    /// Creates File menu and its child items.
    /// </summary>
    /// <returns>File menu.</returns>
    private IMenuItemViewModel CreateFileMenuItem()
    {
        return new MenuItemViewModel()
        {
            Header = MenuResources.FileHeader,
            Items = new ObservableCollection<IMenuItemViewModel>()
            {
                CreateSettingsMenuItem(),
                CreateExitMenuItem()
            }
        };
    }

    /// <summary>
    /// Menu item to display settings editor.
    /// </summary>
    /// <returns>Settings menu item.</returns>
    private IMenuItemViewModel CreateSettingsMenuItem()
    {
        return new MenuItemViewModel()
        {
            Header = MenuResources.SettingsHeader,
            Command = new RelayCommand(ShowSettings)
        };
    }

    /// <summary>
    /// Exits the app.
    /// </summary>
    /// <param name="parameters">Command parameter.</param>
    private void Exit(object? parameters)
    {
        _mainWindow.Close();
    }

    /// <summary>
    /// Displays settings editor.
    /// </summary>
    /// <param name="parameters">Command parameter.</param>
    private void ShowSettings(object? parameters)
    {
        _settingsService.ShowEditor();
    }
}