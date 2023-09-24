using System.Windows;
using Barrent.Common.WPF.Interfaces.Services;
using Barrent.Common.WPF.Services;
using Barrent.Common.WPF.ViewModels;
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
        private readonly IHost appHost;

        public App()
        {
            appHost = Host.CreateDefaultBuilder().ConfigureServices(Register).Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await appHost.StartAsync();

            var window = appHost.Services.GetKeyedService<Window>(ServiceKey.Main);
            var viewModel = appHost.Services.GetRequiredService<IMainWindowViewModel>();
            window.DataContext = viewModel;
            window.Show();

            var settings = appHost.Services.GetRequiredService<ISettingsService>();
            settings.LoadSettings();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await appHost.StopAsync();
            base.OnExit(e);
        }

        private void Register(HostBuilderContext context, IServiceCollection services)
        {
            services.AddSingleton<IDialogService, DialogService>();
            services.AddSingleton<IBackupService, BackupService>();

            services.AddKeyedSingleton<Window, MainWindow>(ServiceKey.Main);
            services.AddSingleton<IMainWindowController, MainWindowController>();

            services.AddSingleton<ISaveGameSerializer, SaveGameSerializer>();
            services.AddSingleton<ISettingsSerializer, SettingsSerializer>();
            services.AddSingleton<IProfileSerializer, ProfileSerializer>();
            services.AddSingleton<IAppSettings, AppSettings>();
            services.AddSingleton<IProfileManager, ProfileManager>();
            services.AddSingleton<ISettingsService, SettingsService>();

            services.AddSingleton<IMainWindowViewModel, MainWindowViewModel>();
            services.AddSingleton<IMainMenuViewModel, MainMenuViewModel>();

        }
    }
}
