using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DarkestDungeonSaveManager.Interfaces.ViewModels;

public interface IMainWindowViewModel : INotifyPropertyChanged, IDisposable
{
    IMainMenuViewModel MenuViewModel { get; }
    ObservableCollection<IProfileViewModel> Profiles { get; }
    IProfileViewModel? ActiveProfile { get; set; }
}