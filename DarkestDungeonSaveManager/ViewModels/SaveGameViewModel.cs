using System;
using System.Collections.ObjectModel;
using System.IO;
using Barrent.Common.Models;
using Barrent.Common.WPF.Converters;
using Barrent.Common.WPF.Interfaces.ViewModels.Parameters;
using Barrent.Common.WPF.ViewModels;
using Barrent.Common.WPF.ViewModels.Parameters;
using DarkestDungeonSaveManager.Interfaces.Models;
using DarkestDungeonSaveManager.Interfaces.ViewModels;
using DarkestDungeonSaveManager.Serialization.Estate;

namespace DarkestDungeonSaveManager.ViewModels;

public class SaveGameViewModel : ViewModelBase, ISaveGameViewModel
{
    private readonly ISaveGame _saveGame;
    private bool _isSelected;

    public SaveGameViewModel(ISaveGame saveGame)
    {
        _saveGame = saveGame;

        Week = new ParameterViewModel<int>(saveGame.Week, true);

        var folder = new DirectoryInfo(saveGame.Path);
        Name = new ParameterViewModel<string>(new Parameter<string>(folder.Name), true);
        Path = new ParameterViewModel<string>(new Parameter<string>(saveGame.Path), true);
        Date = new ParameterViewModel<DateTime>(saveGame.Date);

        Parameters = new ObservableCollection<IParameterViewModel>()
        {
            new ParameterViewModel<string>(saveGame.EstateName, true),
            Week,
            new ParameterViewModel<string>(saveGame.Difficulty, true)
        };

        Gold = new ResourceViewModel<int>(saveGame.Gold, ResourceType.Gold);
        Busts = new ResourceViewModel<int>(saveGame.Busts, ResourceType.Bust);
        Portraits = new ResourceViewModel<int>(saveGame.Portraits, ResourceType.Portrait);
        Deeds = new ResourceViewModel<int>(saveGame.Deeds, ResourceType.Deed);
        Crests = new ResourceViewModel<int>(saveGame.Crests, ResourceType.Crest);
        Shards = new ResourceViewModel<int>(saveGame.Shards, ResourceType.Shard);
        Blueprints = new ResourceViewModel<int>(saveGame.Blueprints, ResourceType.Blueprint);

        Resources = new ObservableCollection<IResourceViewModel<int>>
        {
            Gold, Busts, Portraits, Deeds, Crests, Shards, Blueprints
        };
    }

    public IResourceViewModel<int> Blueprints { get; }
    public IResourceViewModel<int> Busts { get; }
    public IResourceViewModel<int> Crests { get; }
    public IResourceViewModel<int> Deeds { get; }
    public IResourceViewModel<int> Gold { get; }
    public bool IsSelected
    {
        get { return _isSelected; }
        set { SetValue(ref _isSelected, value); }
    }

    public IParameterViewModel<string> Name { get; }
    public ObservableCollection<IParameterViewModel> Parameters { get; }
    public IParameterViewModel<string> Path { get; }
    public IResourceViewModel<int> Portraits { get; }
    public ObservableCollection<IResourceViewModel<int>> Resources { get; }
    public IResourceViewModel<int> Shards { get; }
    public IParameterViewModel<int> Week { get; }
    public IParameterViewModel<DateTime> Date { get; }
}