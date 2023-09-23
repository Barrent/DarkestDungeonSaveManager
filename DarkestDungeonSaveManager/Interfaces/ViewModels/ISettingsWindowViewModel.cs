using System.Collections.ObjectModel;
using System.Windows.Input;
using Barrent.Common.WPF.Interfaces.ViewModels.Parameters;

namespace DarkestDungeonSaveManager.Interfaces.ViewModels;

public interface ISettingsWindowViewModel
{
    ICommand ApplyCommand { get; }
    ICommand CancelCommand { get; }
    ObservableCollection<IParameterViewModel> Parameters { get; }
}