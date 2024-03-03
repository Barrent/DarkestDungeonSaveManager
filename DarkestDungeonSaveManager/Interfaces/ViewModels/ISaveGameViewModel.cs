using System;
using System.Collections.ObjectModel;
using Barrent.Common.WPF.Interfaces.ViewModels;
using Barrent.Common.WPF.Interfaces.ViewModels.Parameters;

namespace DarkestDungeonSaveManager.Interfaces.ViewModels;

public interface ISaveGameViewModel : ISelectableViewModel
{
    /// <summary>
    /// Amount of blueprints.
    /// </summary>
    IResourceViewModel<int> Blueprints { get; }

    /// <summary>
    /// Amount of busts.
    /// </summary>
    IResourceViewModel<int> Busts { get; }

    /// <summary>
    /// Amount of crests.
    /// </summary>
    IResourceViewModel<int> Crests { get; }

    /// <summary>
    /// Save game date.
    /// </summary>
    IParameterViewModel<DateTime> Date { get; }

    /// <summary>
    /// Amount of deeds.
    /// </summary>
    IResourceViewModel<int> Deeds { get; }

    /// <summary>
    /// Amount of gold.
    /// </summary>
    IResourceViewModel<int> Gold { get; }

    /// <summary>
    /// Save game name.
    /// </summary>
    IParameterViewModel<string> Name { get; }

    /// <summary>
    /// Aggregates all the parameters to ease displaying in the UI.
    /// </summary>
    ObservableCollection<IParameterViewModel> Parameters { get; }

    /// <summary>
    /// Path to a save game.
    /// </summary>
    IParameterViewModel<string> Path { get; }

    /// <summary>
    /// Amount of portraits.
    /// </summary>
    IResourceViewModel<int> Portraits { get; }

    /// <summary>
    /// Aggregates all the resources to ease displaying in the UI.
    /// </summary>
    ObservableCollection<IResourceViewModel<int>> Resources { get; }

    /// <summary>
    /// Amount of shards.
    /// </summary>
    IResourceViewModel<int> Shards { get; }

    /// <summary>
    /// Number of in-game weeks passed.
    /// </summary>
    IParameterViewModel<int> Week { get; }
}