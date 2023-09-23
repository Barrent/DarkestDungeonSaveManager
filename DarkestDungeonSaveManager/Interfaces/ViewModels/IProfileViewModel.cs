using Barrent.Common.WPF.Interfaces.ViewModels;

namespace DarkestDungeonSaveManager.Interfaces.ViewModels;

public interface IProfileViewModel
{
    IParameterViewModel<string> Name { get; }
    ISaveGameViewModel ActiveSaveGame { get; }
}