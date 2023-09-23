using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace DarkestDungeonSaveManager.Interfaces.ViewModels;

public interface IMainWindowViewModel : INotifyPropertyChanged, IDisposable
{
    IMainMenuViewModel MenuViewModel { get; }
    ObservableCollection<IProfileViewModel> Profiles { get; }
    IProfileViewModel? ActiveProfile { get; set; }
    public ICommand SaveCommand { get; }
}