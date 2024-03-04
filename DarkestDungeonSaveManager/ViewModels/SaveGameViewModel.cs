using System;
using System.Collections.ObjectModel;
using System.IO;
using Barrent.Common.Models;
using Barrent.Common.WPF.Interfaces.ViewModels.Parameters;
using Barrent.Common.WPF.ViewModels;
using Barrent.Common.WPF.ViewModels.Parameters;
using DarkestDungeonSaveManager.Interfaces.Models;
using DarkestDungeonSaveManager.Interfaces.ViewModels;
using DarkestDungeonSaveManager.Serialization.Estate;

namespace DarkestDungeonSaveManager.ViewModels;

/// <summary>
/// View model of a save game.
/// </summary>
public class SaveGameViewModel : ViewModelBase, ISaveGameViewModel
{
    /// <summary>
    /// Indicates if item is selected.
    /// </summary>
    private bool _isSelected;

    /// <summary>
    /// Initializes a new instance of <see cref="SaveGameViewModel"/>.
    /// </summary>
    /// <param name="saveGame">Save game.</param>
    public SaveGameViewModel(ISaveGame saveGame)
    {
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

    /// <summary>
    /// Amount of blueprints.
    /// </summary>
    public IResourceViewModel<int> Blueprints { get; }

    /// <summary>
    /// Amount of busts.
    /// </summary>
    public IResourceViewModel<int> Busts { get; }

    /// <summary>
    /// Amount of crests.
    /// </summary>
    public IResourceViewModel<int> Crests { get; }

    /// <summary>
    /// Save game date.
    /// </summary>
    public IParameterViewModel<DateTime> Date { get; }

    /// <summary>
    /// Amount of deeds.
    /// </summary>
    public IResourceViewModel<int> Deeds { get; }

    /// <summary>
    /// Amount of gold.
    /// </summary>
    public IResourceViewModel<int> Gold { get; }

    /// <summary>
    /// Indicates if item is selected.
    /// </summary>
    public bool IsSelected
    {
        get { return _isSelected; }
        set { SetValue(ref _isSelected, value); }
    }

    /// <summary>
    /// Save game name.
    /// </summary>
    public IParameterViewModel<string> Name { get; }

    /// <summary>
    /// Aggregates all the parameters to ease displaying in the UI.
    /// </summary>
    public ObservableCollection<IParameterViewModel> Parameters { get; }

    /// <summary>
    /// Path to a save game.
    /// </summary>
    public IParameterViewModel<string> Path { get; }

    /// <summary>
    /// Amount of portraits.
    /// </summary>
    public IResourceViewModel<int> Portraits { get; }

    /// <summary>
    /// Aggregates all the resources to ease displaying in the UI.
    /// </summary>
    public ObservableCollection<IResourceViewModel<int>> Resources { get; }

    /// <summary>
    /// Amount of shards.
    /// </summary>
    public IResourceViewModel<int> Shards { get; }

    /// <summary>
    /// Number of in-game weeks passed.
    /// </summary>
    public IParameterViewModel<int> Week { get; }
}