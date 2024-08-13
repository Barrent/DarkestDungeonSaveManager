using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DarkestDungeonSaveManager.Interfaces.ViewModels;

/// <summary>
/// View model of the main window.
/// </summary>
public interface IMainWindowViewModel : INotifyPropertyChanged, IDisposable
{
    /// <summary>
    /// Selected player profile.
    /// </summary>
    IProfileViewModel? ActiveProfile { get; set; }

    /// <summary>
    /// Main menu.
    /// </summary>
    IMainMenuViewModel MenuViewModel { get; }

    /// <summary>
    /// Player profiles.
    /// </summary>
    ObservableCollection<IProfileViewModel> Profiles { get; }
}