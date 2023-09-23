using Barrent.Common.WPF.Interfaces.ViewModels.Parameters;
using DarkestDungeonSaveManager.Serialization.Estate;

namespace DarkestDungeonSaveManager.Interfaces.ViewModels;

public interface IResourceViewModel<T> : IParameterViewModel<T>
{
    ResourceType Type { get; }
}