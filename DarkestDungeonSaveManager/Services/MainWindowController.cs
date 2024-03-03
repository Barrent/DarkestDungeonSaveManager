using System.Windows;
using Barrent.Common.WPF.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DarkestDungeonSaveManager.Services;

public class MainWindowController : WindowController
{
    public MainWindowController([FromKeyedServices(ServiceKey.Main)] Window mainWindow)
        : base(mainWindow)
    {
    }
}