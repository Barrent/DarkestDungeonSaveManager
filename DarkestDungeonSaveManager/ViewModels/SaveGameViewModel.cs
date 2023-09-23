using Barrent.Common.WPF.Interfaces.ViewModels;
using Barrent.Common.WPF.ViewModels.Parameters;
using DarkestDungeonSaveManager.Interfaces.Models;
using DarkestDungeonSaveManager.Interfaces.ViewModels;

namespace DarkestDungeonSaveManager.ViewModels;

public class SaveGameViewModel : ISaveGameViewModel
{
    public SaveGameViewModel(ISaveGame saveGame)
    {
        EstateName = new ParameterViewModel<string>(saveGame.EstateName);
        Days = new ParameterViewModel<int>(saveGame.Days);
    }
    public IParameterViewModel<string> EstateName { get; }
    public IParameterViewModel<int> Days { get; }
}