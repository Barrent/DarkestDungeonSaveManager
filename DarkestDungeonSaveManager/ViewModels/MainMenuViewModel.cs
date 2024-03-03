using DarkestDungeonSaveManager.Interfaces.ViewModels;
using System.Collections.ObjectModel;
using Barrent.Common.WPF.Commands;
using Barrent.Common.WPF.Interfaces.Services;
using Barrent.Common.WPF.Interfaces.ViewModels;
using DarkestDungeonSaveManager.Interfaces.Services;
using DarkestDungeonSaveManager.Resources;
using Microsoft.Extensions.DependencyInjection;
using Barrent.Common.WPF.Services;

namespace DarkestDungeonSaveManager.ViewModels;

public class MainMenuViewModel : IMainMenuViewModel
{
    private readonly ISettingsService _settingsService;

    private readonly IWindowController _mainWindow;

    public MainMenuViewModel(ISettingsService settingsService, [FromKeyedServices(ServiceKey.Main)] IWindowController mainWindow)
    {
        _settingsService = settingsService;
        _mainWindow = mainWindow;
        Items = new ObservableCollection<IMenuItemViewModel>() { CreateFileMenuItem() };
    }

    public ObservableCollection<IMenuItemViewModel> Items { get; }

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

    private IMenuItemViewModel CreateSettingsMenuItem()
    {
        return new MenuItemViewModel()
        {
            Header = MenuResources.SettingsHeader,
            Command = new RelayCommand(ShowSettings)
        };
    }

    private IMenuItemViewModel CreateExitMenuItem()
    {
        return new MenuItemViewModel()
        {
            Header = MenuResources.ExitHeader,
            Command = new RelayCommand(Exit)
        };
    }

    private void ShowSettings(object parameters)
    {
        _settingsService.ShowEditor();
    }

    private void Exit(object parameters)
    {
        _mainWindow.Close();
    }
}