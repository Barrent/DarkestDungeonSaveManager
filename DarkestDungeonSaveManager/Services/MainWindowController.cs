using System.Windows;
using Barrent.Common.WPF.Services;
using DarkestDungeonSaveManager.Interfaces.Services;
using DarkestDungeonSaveManager.Views;
using Microsoft.Extensions.DependencyInjection;

namespace DarkestDungeonSaveManager.Services;

public class MainWindowController : WindowController, IMainWindowController
{
    public MainWindowController([FromKeyedServices("main")] Window mainWindow) : base(mainWindow)
    {
        
    }
}