using System.Windows;
using Barrent.Common.WPF.Interfaces.Services;
using Barrent.Common.WPF.ViewModels;
using DarkestDungeonSaveManager.Interfaces.Models;
using DarkestDungeonSaveManager.Interfaces.Models.Settings;
using DarkestDungeonSaveManager.Interfaces.Services;
using DarkestDungeonSaveManager.Models;
using DarkestDungeonSaveManager.Models.Settings;
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

            var window = appHost.Services.GetRequiredService<MainWindow>();
            var viewModel = appHost.Services.GetRequiredService<MainWindowViewModel>();
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
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<ISettingsSerializer, SettingsSerializer>();
            services.AddSingleton<ISettingsService, SettingsService>();
            services.AddSingleton<IDialogService, DialogService>(s => new DialogService(s.GetRequiredService<MainWindow>()));
        }
    }
}
