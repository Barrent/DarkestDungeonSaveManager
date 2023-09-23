using System.Collections.ObjectModel;
using Barrent.Common.WPF.Interfaces.ViewModels.Parameters;
using Barrent.Common.WPF.ViewModels.Parameters;
using DarkestDungeonSaveManager.Interfaces.Models;
using DarkestDungeonSaveManager.Interfaces.ViewModels;
using DarkestDungeonSaveManager.Serialization.Estate;

namespace DarkestDungeonSaveManager.ViewModels;

public class SaveGameViewModel : ISaveGameViewModel
{
    public SaveGameViewModel(ISaveGame saveGame)
    {
        Parameters = new ObservableCollection<IParameterViewModel>()
        {
            new ParameterViewModel<string>(saveGame.EstateName, true),
            new ParameterViewModel<int>(saveGame.Week, true),
            new ParameterViewModel<string>(saveGame.Difficulty, true)
        };

        Resources = new ObservableCollection<IResourceViewModel<int>>
        {
            new ResourceViewModel<int>(saveGame.Gold, ResourceType.Gold),
            new ResourceViewModel<int>(saveGame.Busts, ResourceType.Bust),
            new ResourceViewModel<int>(saveGame.Portraits, ResourceType.Portrait),
            new ResourceViewModel<int>(saveGame.Deeds, ResourceType.Deed),
            new ResourceViewModel<int>(saveGame.Crests, ResourceType.Crest),
            new ResourceViewModel<int>(saveGame.Shards, ResourceType.Shard),
            new ResourceViewModel<int>(saveGame.Blueprints, ResourceType.Blueprint)
        };
    }

    public ObservableCollection<IResourceViewModel<int>> Resources { get; }

    public ObservableCollection<IParameterViewModel> Parameters { get; }
}