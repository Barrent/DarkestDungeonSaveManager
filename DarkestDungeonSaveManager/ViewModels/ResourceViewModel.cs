using System;
using Barrent.Common.WPF.Interfaces.Models;
using Barrent.Common.WPF.ViewModels.Parameters;
using DarkestDungeonSaveManager.Interfaces.ViewModels;
using DarkestDungeonSaveManager.Serialization.Estate;

namespace DarkestDungeonSaveManager.ViewModels;

public class ResourceViewModel<T> : ParameterViewModel<T>, IResourceViewModel<T> where T : IComparable
{
    public ResourceViewModel(IParameter<T> parameter, ResourceType type) : base(parameter)
    {
        Type = type;
    }

    public ResourceType Type { get; }
}