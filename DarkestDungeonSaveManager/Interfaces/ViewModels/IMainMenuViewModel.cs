using System.Collections.ObjectModel;
using Barrent.Common.WPF.Interfaces.ViewModels;

namespace DarkestDungeonSaveManager.Interfaces.ViewModels;

public interface IMainMenuViewModel
{
    ObservableCollection<IMenuItemViewModel> Items { get; }
}