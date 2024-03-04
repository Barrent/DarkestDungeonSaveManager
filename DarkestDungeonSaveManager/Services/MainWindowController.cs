using System.Windows;
using Barrent.Common.WPF.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DarkestDungeonSaveManager.Services;

/// <summary>
/// Controls the main window.
/// </summary>
public class MainWindowController : WindowController
{
    /// <summary>
    /// Initializes a new instance of <see cref="MainWindowController"/>.
    /// </summary>
    /// <param name="mainWindow">Main window of the app.</param>
    public MainWindowController([FromKeyedServices(ServiceKey.Main)] Window mainWindow)
        : base(mainWindow)
    {
    }
}