using System.Collections.ObjectModel;
using System.Windows.Input;
using Barrent.Common.WPF.Interfaces.ViewModels;

namespace DarkestDungeonSaveManager.ViewModels;

/// <summary>
/// TODO: replace with Barrent.WPF implementation
/// </summary>
public class MenuItemViewModel : IMenuItemViewModel
{
    public ICommand Command { get; set; }

    public string Header { get; set; }

    public ObservableCollection<IMenuItemViewModel> Items { get; set; }
}