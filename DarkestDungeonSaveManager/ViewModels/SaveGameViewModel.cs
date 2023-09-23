using System.Collections.ObjectModel;
using Barrent.Common.WPF.Interfaces.ViewModels;
using Barrent.Common.WPF.ViewModels.Parameters;
using DarkestDungeonSaveManager.Interfaces.Models;
using DarkestDungeonSaveManager.Interfaces.ViewModels;
using DarkestDungeonSaveManager.Serialization.Estate;

namespace DarkestDungeonSaveManager.ViewModels;

public class SaveGameViewModel : ISaveGameViewModel
{
    public SaveGameViewModel(ISaveGame saveGame)
    {
        EstateName = new ParameterViewModel<string>(saveGame.EstateName);
        Days = new ParameterViewModel<int>(saveGame.Days);

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
    public IParameterViewModel<string> EstateName { get; }
    public IParameterViewModel<int> Days { get; }

    public ObservableCollection<IResourceViewModel<int>> Resources { get; }
}