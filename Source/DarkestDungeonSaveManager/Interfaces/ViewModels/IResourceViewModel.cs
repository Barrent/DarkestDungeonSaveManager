using Barrent.Common.WPF.Interfaces.ViewModels.Parameters;
using DarkestDungeonSaveManager.Serialization.Estate;

namespace DarkestDungeonSaveManager.Interfaces.ViewModels;

public interface IResourceViewModel<T> : IParameterViewModel<T>
{
    /// <summary>
    /// View model of resource.
    /// </summary>
    ResourceType Type { get; }
}