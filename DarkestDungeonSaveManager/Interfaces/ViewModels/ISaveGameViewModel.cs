using System;
using System.Collections.ObjectModel;
using Barrent.Common.WPF.Interfaces.ViewModels;
using Barrent.Common.WPF.Interfaces.ViewModels.Parameters;

namespace DarkestDungeonSaveManager.Interfaces.ViewModels;

public interface ISaveGameViewModel : ISelectableViewModel
{
    IResourceViewModel<int> Blueprints { get; }
    IResourceViewModel<int> Busts { get; }
    IResourceViewModel<int> Crests { get; }
    IResourceViewModel<int> Deeds { get; }
    IResourceViewModel<int> Gold { get; }
    IParameterViewModel<string> Name { get; }
    ObservableCollection<IParameterViewModel> Parameters { get; }
    IParameterViewModel<string> Path { get; }
    IResourceViewModel<int> Portraits { get; }
    ObservableCollection<IResourceViewModel<int>> Resources { get; }
    IResourceViewModel<int> Shards { get; }
    IParameterViewModel<int> Week { get; }

    IParameterViewModel<DateTime> Date { get; }
}