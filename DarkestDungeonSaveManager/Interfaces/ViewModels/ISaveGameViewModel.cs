using System.Collections.ObjectModel;
using Barrent.Common.WPF.Interfaces.ViewModels;

namespace DarkestDungeonSaveManager.Interfaces.ViewModels;

public interface ISaveGameViewModel
{
    IParameterViewModel<string> EstateName { get; }

    IParameterViewModel<int> Days { get; }
    ObservableCollection<IResourceViewModel<int>> Resources { get; }
}