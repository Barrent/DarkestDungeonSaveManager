using System.Collections.ObjectModel;
using Barrent.Common.WPF.Interfaces.ViewModels.Parameters;

namespace DarkestDungeonSaveManager.Interfaces.ViewModels;

public interface ISaveGameViewModel
{
    ObservableCollection<IResourceViewModel<int>> Resources { get; }
    ObservableCollection<IParameterViewModel> Parameters { get; }
}