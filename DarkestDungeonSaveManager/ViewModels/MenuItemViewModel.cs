using System.Collections.ObjectModel;
using System.Windows.Input;
using Barrent.Common.WPF.Interfaces.ViewModels;
using DarkestDungeonSaveManager.Interfaces.ViewModels;

namespace DarkestDungeonSaveManager.ViewModels;

public class MenuItemViewModel : IMenuItemViewModel
{
    public ICommand Command { get; set; }

    public string Header { get; set; }

    public ObservableCollection<IMenuItemViewModel> Items { get; set; }
}