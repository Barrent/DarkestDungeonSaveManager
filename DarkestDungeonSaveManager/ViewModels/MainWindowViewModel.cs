using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Barrent.Common.WPF.Commands;
using Barrent.Common.WPF.ViewModels;
using DarkestDungeonSaveManager.Interfaces.Models;
using DarkestDungeonSaveManager.Interfaces.ViewModels;

namespace DarkestDungeonSaveManager.ViewModels;

public class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
{
    private readonly IProfileManager _profileManager;

    private IProfileViewModel? _activeProfile;

    public MainWindowViewModel(IMainMenuViewModel menuViewModel, IProfileManager profileManager)
    {
        _profileManager = profileManager;
        _profileManager.ProfilesChanged += OnProfilesChanged;
        MenuViewModel = menuViewModel;
        Profiles = new ObservableCollection<IProfileViewModel>();

        InitProfiles();
    }

    public IProfileViewModel? ActiveProfile
    {
        get { return _activeProfile; }
        set { SetValue(ref _activeProfile, value); }
    }

    public IMainMenuViewModel MenuViewModel { get; }

    public ObservableCollection<IProfileViewModel> Profiles { get; }

   

    private IProfileViewModel CreateProfile(IProfile profile)
    {
        return new ProfileViewModel(profile);
    }

    private void InitProfiles()
    {
        Profiles.Clear();
        Profiles.AddRange(_profileManager.Profiles.Select(CreateProfile));
    }

    private void OnProfilesChanged(IProfileManager sender, EventArgs args)
    {
        InitProfiles();
    }


}