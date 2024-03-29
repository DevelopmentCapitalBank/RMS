﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RMS.DATA;
using RMS.DocumentProcessing.Reader;
using RMS.DocumentProcessing.Verification;
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

                services.AddTransient<IDialogService, DialogService>();
                services.AddTransient<ICurrencyRateParser, CurrencyRateParser>();
                services.AddTransient<IExcelReader, ExcelReader>(); 
                services.AddTransient<IDocumentVerification, DocumentVerification>();
                services.AddTransient<IVisListHandler, VisListHandler>();
                services.AddTransient<IUploadingHandler, UploadingHandler>();
                services.AddTransient<ITransformData, TransformData>();
                services.AddSingleton(new DbConfig { Name = @"Data Source=\\WSRV1\POLE\ukoJ\RMS.db" });
                //services.AddSingleton(new DbConfig { Name = @"Data Source=D:\RMS.db" });
                services.AddSingleton<DbContext>();

                services.AddTransient<HomeViewModel>();
                services.AddTransient<GroupViewModel>();
                services.AddTransient<CompanyViewModel>();
                services.AddTransient<AccountViewModel>();
                services.AddTransient<MaskViewModel>();
                services.AddTransient<ImportViewModel>();
                services.AddTransient<ExportViewModel>();
                services.AddTransient<ExchangeRatesViewModel>();
                services.AddTransient<SqlViewModel>();
                services.AddTransient<SettingsViewModel>();
            }).Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();

            var dbContext = AppHost.Services.GetRequiredService<DbContext>();
            dbContext.Setup();

            var startupForm = AppHost.Services.GetRequiredService<MainWindow>();

            var mainViewModel = new MainWindowViewModel();
            mainViewModel.PageViewModels[0] = AppHost.Services.GetRequiredService<HomeViewModel>();
            mainViewModel.PageViewModels[1] = AppHost.Services.GetRequiredService<GroupViewModel>();
            mainViewModel.PageViewModels[2] = AppHost.Services.GetRequiredService<CompanyViewModel>();
            mainViewModel.PageViewModels[3] = AppHost.Services.GetRequiredService<AccountViewModel>();
            mainViewModel.PageViewModels[4] = AppHost.Services.GetRequiredService<MaskViewModel>();
            mainViewModel.PageViewModels[5] = AppHost.Services.GetRequiredService<ImportViewModel>();
            mainViewModel.PageViewModels[6] = AppHost.Services.GetRequiredService<ExportViewModel>();
            mainViewModel.PageViewModels[7] = AppHost.Services.GetRequiredService<ExchangeRatesViewModel>();
            mainViewModel.PageViewModels[8] = AppHost.Services.GetRequiredService<SqlViewModel>();
            mainViewModel.PageViewModels[9] = AppHost.Services.GetRequiredService<SettingsViewModel>();
            mainViewModel.CurrentPageViewModel = mainViewModel.PageViewModels[0];

            startupForm!.DataContext = mainViewModel;
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
