using System.Collections.ObjectModel;
using Barrent.Common.WPF.Interfaces.ViewModels;

namespace DarkestDungeonSaveManager.Interfaces.ViewModels;

/// <summary>
/// Main menu.
/// </summary>
public interface IMainMenuViewModel
{
    /// <summary>
    /// Menu items.
    /// </summary>
    ObservableCollection<IMenuItemViewModel> Items { get; }
}