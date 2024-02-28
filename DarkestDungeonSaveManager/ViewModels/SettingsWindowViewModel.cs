using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Barrent.Common.Interfaces.Models;
using Barrent.Common.Models;
using Barrent.Common.WPF.Interfaces.Services;
using Barrent.Common.WPF.Interfaces.ViewModels.Parameters;
using Barrent.Common.WPF.ViewModels;
using Barrent.Common.WPF.ViewModels.Parameters;
using DarkestDungeonSaveManager.Interfaces.Models;
using DarkestDungeonSaveManager.Interfaces.ViewModels;
using Prism.Commands;

namespace DarkestDungeonSaveManager.ViewModels;

public class SettingsWindowViewModel : ViewModelBase, ISettingsWindowViewModel
{
    private readonly IDialogService _dialogService;
    private readonly ICommitDecorator<string>[] _parameters;
    private readonly IWindowController _windowController;
    public SettingsWindowViewModel(IWindowController windowController, IDialogService dialogService, IAppSettings settings)
    {
        _windowController = windowController;
        _dialogService = dialogService;
        ApplyCommand = new DelegateCommand(Apply);
        CancelCommand = new DelegateCommand(Cancel);

        _parameters = new[]
        {
            CreateDecorator(settings.SaveGameFolderPath),
            CreateDecorator(settings.BackupFolderPath)
        };

        Parameters = new ObservableCollection<IParameterViewModel>()
        {
            CreateFolderViewModel(_parameters[0]),
            CreateFolderViewModel(_parameters[1])
        };
    }

    public ICommand ApplyCommand { get; }

    public ICommand CancelCommand { get; }

    public ObservableCollection<IParameterViewModel> Parameters { get; }

    private void Apply()
    {
        foreach (var parameter in _parameters)
        {
            parameter.CommitChanges();
        }

        _windowController.Close();
    }

    private void Cancel()
    {
        _windowController.Close();
    }

    private ICommitDecorator<T> CreateDecorator<T>(IParameter<T> parameter) where T : IComparable
    {
        return new CommitDecorator<T>(parameter);
    }
    private IParameterViewModel<string> CreateFolderViewModel(IParameter<string> parameter)
    {
        return new FolderPathParameterViewModel(_dialogService, parameter);
    }
}