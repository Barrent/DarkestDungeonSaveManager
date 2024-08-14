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

/// <summary>
/// View model of the settings window.
/// </summary>
public class SettingsWindowViewModel : ViewModelBase, ISettingsWindowViewModel
{
    /// <summary>
    /// Dialog service.
    /// </summary>
    private readonly IDialogService _dialogService;

    /// <summary>
    /// All the parameters.
    /// </summary>
    private readonly ICommitDecorator<string>[] _parameters;

    /// <summary>
    /// Controls Settings window.
    /// </summary>
    private readonly IWindowController _windowController;

    /// <summary>
    /// Initializes a new instance of <see cref="SettingsWindowViewModel"/>.
    /// </summary>
    /// <param name="windowController">Controls Settings window.</param>
    /// <param name="dialogService">Service to display dialog windows.</param>
    /// <param name="settings">App settings.</param>
    public SettingsWindowViewModel(IWindowController windowController,
        IDialogService dialogService, IAppSettings settings)
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

    /// <summary>
    /// Applies changes.
    /// </summary>
    public ICommand ApplyCommand { get; }

    /// <summary>
    /// Discards changes.
    /// </summary>
    public ICommand CancelCommand { get; }

    /// <summary>
    /// Parameters to display.
    /// </summary>
    public ObservableCollection<IParameterViewModel> Parameters { get; }

    /// <summary>
    /// Applies pending changes.
    /// </summary>
    private void Apply()
    {
        foreach (var parameter in _parameters)
        {
            parameter.CommitChanges();
        }

        _windowController.Close();
    }

    /// <summary>
    /// Discards pending changes.
    /// </summary>
    private void Cancel()
    {
        _windowController.Close();
    }

    /// <summary>
    /// Wraps parameter with decorator that support delayed parameter value update.
    /// </summary>
    /// <typeparam name="T">Type of parameter value.</typeparam>
    /// <param name="parameter"></param>
    /// <returns>Wrapper parameter.</returns>
    private ICommitDecorator<T> CreateDecorator<T>(IParameter<T> parameter) where T : IComparable
    {
        return new CommitDecorator<T>(parameter);
    }

    /// <summary>
    /// Creates parameter view model.
    /// </summary>
    /// <param name="parameter">Parameter.</param>
    /// <returns>View model.</returns>
    private IParameterViewModel<string> CreateFolderViewModel(IParameter<string> parameter)
    {
        return new FolderPathParameterViewModel(_dialogService, parameter);
    }
}