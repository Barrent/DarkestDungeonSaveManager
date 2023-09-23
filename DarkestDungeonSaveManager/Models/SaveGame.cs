using System;
using System.Windows.Forms;
using Barrent.Common.WPF.Interfaces.Models;
using Barrent.Common.WPF.Models;
using DarkestDungeonSaveManager.Interfaces.Models;

namespace DarkestDungeonSaveManager.Models;

public class SaveGame : ISaveGame
{
    public SaveGame()
    {
        Days = new Parameter<int>(0);
        EstateName = new Parameter<string>(String.Empty);
    }

    public IParameter<string> EstateName { get; }
    public IParameter<int> Days { get; }
}