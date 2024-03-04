using System;
using Barrent.Common.Interfaces.Models;
using Barrent.Common.WPF.ViewModels.Parameters;
using DarkestDungeonSaveManager.Interfaces.ViewModels;
using DarkestDungeonSaveManager.Serialization.Estate;

namespace DarkestDungeonSaveManager.ViewModels;

/// <summary>
/// View model of in-game resource.
/// </summary>
/// <typeparam name="T">Resource value type.</typeparam>
public class ResourceViewModel<T> : ParameterViewModel<T>, IResourceViewModel<T> where T : IComparable
{
    /// <summary>
    /// Initializes a new instance of <see cref="ResourceViewModel{T}"/>.
    /// </summary>
    /// <param name="parameter">Resource.</param>
    /// <param name="type">Resource type.</param>
    public ResourceViewModel(IParameter<T> parameter, ResourceType type) : base(parameter)
    {
        Type = type;
    }

    /// <summary>
    /// View model of resource.
    /// </summary>
    public ResourceType Type { get; }
}