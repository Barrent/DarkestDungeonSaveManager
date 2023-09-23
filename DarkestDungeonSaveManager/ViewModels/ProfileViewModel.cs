using System.Collections.ObjectModel;
using Barrent.Common.WPF.Interfaces.ViewModels.Parameters;
using Barrent.Common.WPF.ViewModels.Parameters;
using DarkestDungeonSaveManager.Interfaces.Models;
using DarkestDungeonSaveManager.Interfaces.ViewModels;

namespace DarkestDungeonSaveManager.ViewModels;

public class ProfileViewModel : IProfileViewModel
{
    private readonly IProfile _profile;

    public ProfileViewModel(IProfile profile)
    {
        _profile = profile;

        Name = new ParameterViewModel<string>(profile.DisplayName);

        ActiveSaveGame = new SaveGameViewModel(profile.ActiveSave);
    }

    public IParameterViewModel<string> Name { get; }

    public ISaveGameViewModel ActiveSaveGame { get; }
}