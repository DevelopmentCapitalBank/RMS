using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RMS.DATA;
using RMS.UI.DialogBoxes;
using RMS.UI.Services;
using RMS.UI.ViewModels;
using RMS.UI.Views;
using System.Windows;

namespace RMS.UI
{
    public partial class App : Application
    {
        public static IHost? AppHost { get; private set; }
        public App()
        {
            AppHost = Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<MainWindow>();
                services.AddTransient<IDataModel, DataModel>();
                services.AddTransient<IDialogService, DialogService>();
                services.AddSingleton(new DbConfig { Name = @"Data Source=\\WSRV1\POLE\kd\RMS.db" });
                services.AddSingleton<DbContext>();
            }).Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();
            var startupForm = AppHost.Services.GetRequiredService<MainWindow>();

            var context = new DbContext(new DbConfig { Name = @"Data Source=\\WSRV1\POLE\kd\RMS.db" });
            await context.Setup();

            startupForm!.DataContext = new MainWindowViewModel(context, new DialogService());
            startupForm!.Show();
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();
            base.OnExit(e);
        }
    }
}
