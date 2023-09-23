using DarkestDungeonSaveManager.Interfaces.ViewModels;
using System.Collections.ObjectModel;
using Barrent.Common.WPF.Commands;
using Barrent.Common.WPF.Interfaces.ViewModels;
using DarkestDungeonSaveManager.Interfaces.Services;
using DarkestDungeonSaveManager.Resources;

namespace DarkestDungeonSaveManager.ViewModels;

public class MainMenuViewModel : IMainMenuViewModel
{
    private readonly ISettingsService _settingsService;

    private readonly IMainWindowController _mainWindow;

    public MainMenuViewModel(ISettingsService settingsService, IMainWindowController mainWindow)
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