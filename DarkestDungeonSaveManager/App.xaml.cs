using System.Windows;
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
    public partial class App : Application
    {
        /// <summary>
        /// DI container.
        /// </summary>
        private readonly IHost appHost;

        /// <summary>
        /// Initializes a new instance of <see cref="App"/>.
        /// </summary>
        public App()
        {
            appHost = Host.CreateDefaultBuilder().ConfigureServices(Register).Build();
        }

        /// <summary>
        /// Raises the <see cref="Application.Startup" /> event.
        /// </summary>
        /// <param name="e">A <see cref="StartupEventArgs" /> that contains the event data.</param>
        protected override async void OnStartup(StartupEventArgs e)
        {
            await appHost.StartAsync();

            var window = appHost.Services.GetKeyedService<Window>(ServiceKey.Main);
            var viewModel = appHost.Services.GetRequiredService<IMainWindowViewModel>();
            window!.DataContext = viewModel;
            window.Show();

            var settings = appHost.Services.GetRequiredService<ISettingsService>();
            settings.LoadSettings();

            base.OnStartup(e);
        }

        /// <summary>
        /// Raises the <see cref="Application.Exit" /> event.
        /// </summary>
        /// <param name="e">An <see cref="ExitEventArgs" /> that contains the event data.</param>
        protected override async void OnExit(ExitEventArgs e)
        {
            await appHost.StopAsync();
            base.OnExit(e);
        }

        /// <summary>
        /// Registers classes in the DI container.
        /// </summary>
        /// <param name="context">Build context.</param>
        /// <param name="services">Services.</param>
        private void Register(HostBuilderContext context, IServiceCollection services)
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
    }
}
