using System;
using System.Collections.ObjectModel;
using System.Linq;
using Barrent.Common.WPF.ViewModels;
using DarkestDungeonSaveManager.Interfaces.Models;
using DarkestDungeonSaveManager.Interfaces.ViewModels;

namespace DarkestDungeonSaveManager.ViewModels;

/// <summary>
/// View model of the main window.
/// </summary>
public class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
{
    /// <summary>
    /// Player profile manager.
    /// </summary>
    private readonly IProfileManager _profileManager;

    /// <summary>
    /// Active player profile.
    /// </summary>
    private IProfileViewModel? _activeProfile;

    /// <summary>
    /// Initializes a new instance of <see cref="MainWindowViewModel"/>.
    /// </summary>
    /// <param name="menuViewModel">Menu view model.</param>
    /// <param name="profileManager">Player profiles manager.</param>
    public MainWindowViewModel(IMainMenuViewModel menuViewModel, IProfileManager profileManager)
    {
        _profileManager = profileManager;
        _profileManager.ProfilesChanged += OnProfilesChanged;
        MenuViewModel = menuViewModel;
        Profiles = new ObservableCollection<IProfileViewModel>();

        InitProfiles();
    }

    /// <summary>
    /// Selected player profile.
    /// </summary>
    public IProfileViewModel? ActiveProfile
    {
        get { return _activeProfile; }
        set { SetValue(ref _activeProfile, value); }
    }

    /// <summary>
    /// Main menu.
    /// </summary>
    public IMainMenuViewModel MenuViewModel { get; }

    /// <summary>
    /// Player profiles.
    /// </summary>
    public ObservableCollection<IProfileViewModel> Profiles { get; }

    /// <summary>
    /// Creates player profile view model.
    /// </summary>
    /// <param name="profile">Player profile.</param>
    /// <returns>Player profile view model.</returns>
    private IProfileViewModel CreateProfile(IProfile profile)
    {
        return new ProfileViewModel(profile);
    }

    /// <summary>
    /// Initializes player profiles.
    /// </summary>
    private void InitProfiles()
    {
        Profiles.Clear();
        Profiles.AddRange(_profileManager.Profiles.Select(CreateProfile));
    }

    /// <summary>
    /// Handles change of player profiles.
    /// </summary>
    /// <param name="sender">Event sender.</param>
    /// <param name="args">Event args.</param>
    private void OnProfilesChanged(IProfileManager sender, EventArgs args)
    {
        InitProfiles();
    }
}