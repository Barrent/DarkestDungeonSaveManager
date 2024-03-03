using System.Collections.ObjectModel;
using System.Windows.Input;
using Barrent.Common.WPF.Interfaces.ViewModels.Parameters;

namespace DarkestDungeonSaveManager.Interfaces.ViewModels;

public interface IProfileViewModel
{
    /// <summary>
    /// Active save game.
    /// </summary>
    ISaveGameViewModel ActiveSaveGame { get; }

    /// <summary>
    /// Clears up the backup storage.
    /// </summary>
    ICommand DeleteAllCommand { get; }

    /// <summary>
    /// Command to delete selected in the UI save games from the backup storage.
    /// </summary>
    ICommand DeleteCommand { get; }

    /// <summary>
    /// Loads selected save game from the backup storage.
    /// </summary>
    ICommand LoadCommand { get; }

    /// <summary>
    /// Profile name.
    /// </summary>
    IParameterViewModel<string> Name { get; }
    /// <summary>
    /// Updates list of available save games.
    /// </summary>
    public ICommand RefreshCommand { get; }

    /// <summary>
    /// Command to save <see cref="ActiveSaveGame"/> to the backup storage.
    /// </summary>
    ICommand SaveCommand { get; }

    /// <summary>
    /// Backed up save games.
    /// </summary>
    ObservableCollection<ISaveGameViewModel> Saves { get; }
}