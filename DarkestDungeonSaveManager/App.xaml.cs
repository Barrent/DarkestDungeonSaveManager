using System.Windows;
using Barrent.Common.WPF;
using Barrent.Common.WPF.Interfaces.Services;
using Barrent.Common.WPF.Services;
using DarkestDungeonSaveManager.Interfaces.Models;
using DarkestDungeonSaveManager.Interfaces.Serialization;
using DarkestDungeonSaveManager.Interfaces.Services;
using DarkestDungeonSaveManager.Interfaces.ViewModels;
using DarkestDungeonSaveManager.Models;
using DarkestDungeonSaveManager.Serialization;
using DarkestDungeonSaveManager.Services;
using DarkestDungeonSaveManager.ViewModels;
using DarkestDungeonSaveManager.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DarkestDungeonSaveManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : BaseApp
    {
        /// <summary>
        /// Raises the <see cref="Application.Startup" /> event.
        /// </summary>
        /// <param name="e">A <see cref="StartupEventArgs" /> that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var settings = Container!.Services.GetRequiredService<ISettingsService>();
            settings.LoadSettings();
        }

        /// <summary>
        /// Registers classes in the DI container.
        /// </summary>
        /// <param name="context">Build context.</param>
        /// <param name="services">Services.</param>
        protected override void RegisterDependencies(HostBuilderContext context, IServiceCollection services)
        {
            services.AddSingleton<IDialogService, DialogService>();
            services.AddSingleton<IBackupService, BackupService>();

            services.AddKeyedSingleton<Window, MainWindow>(ServiceKey.Main);
            services.AddKeyedSingleton<IWindowController, MainWindowController>(ServiceKey.Main);

            services.AddSingleton<ISaveGameParser, SaveGameParser>();
            services.AddSingleton<ISettingsSerializer, SettingsSerializer>();
            services.AddSingleton<IAppSettings, AppSettings>();
            services.AddSingleton<IProfileManager, ProfileManager>();
            services.AddSingleton<ISettingsService, SettingsService>();

            services.AddSingleton<IMainWindowViewModel, MainWindowViewModel>();
            services.AddSingleton<IMainMenuViewModel, MainMenuViewModel>();
        }

        /// <summary>
        /// Creates instance of the main window.
        /// </summary>
        /// <returns>Main window.</returns>
        protected override Window ResolveMainWindow()
        {
            return Container!.Services.GetKeyedService<Window>(ServiceKey.Main)!;
        }

        /// <summary>
        /// Creates data context of the main window.
        /// </summary>
        /// <returns>View model of thr main window.</returns>
        protected override object ResolveMainWindowDataContext()
        {
            return Container!.Services.GetRequiredService<IMainWindowViewModel>();
        }
    }
}
