using Barrent.Common.WPF.Interfaces.ViewModels.Parameters;

namespace DarkestDungeonSaveManager.Interfaces.ViewModels;

public interface IProfileViewModel
{
    IParameterViewModel<string> Name { get; }
    ISaveGameViewModel ActiveSaveGame { get; }
}