using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Barrent.Common.WPF.Interfaces.ViewModels.Parameters;

namespace DarkestDungeonSaveManager.Interfaces.ViewModels;

public interface IProfileViewModel
{
    IParameterViewModel<string> Name { get; }
    ISaveGameViewModel ActiveSaveGame { get; }
    ObservableCollection<ISaveGameViewModel> Saves { get; }

    ICommand SaveCommand { get; }

    ICommand DeleteCommand { get; }

    ICommand DeleteAllCommand { get; }

    ICommand LoadCommand { get; }
    public ICommand RefreshCommand { get; }
}