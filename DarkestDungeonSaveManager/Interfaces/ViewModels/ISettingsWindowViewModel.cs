using System.Collections.ObjectModel;
using System.Windows.Input;
using Barrent.Common.WPF.Interfaces.ViewModels.Parameters;

namespace DarkestDungeonSaveManager.Interfaces.ViewModels;

/// <summary>
/// View model of the settings window.
/// </summary>
public interface ISettingsWindowViewModel
{
    /// <summary>
    /// Applies changes.
    /// </summary>
    ICommand ApplyCommand { get; }

    /// <summary>
    /// Discards changes.
    /// </summary>
    ICommand CancelCommand { get; }

    /// <summary>
    /// Parameters to display.
    /// </summary>
    ObservableCollection<IParameterViewModel> Parameters { get; }
}