using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using Serilog;
using Serilog.Hosting;
using PatientProgress.Data;
using Microsoft.EntityFrameworkCore;
using PatientProgress.Services;
using PatientProgress.ViewModels;
using PatientProgress.Views;

namespace PatientProgress;

public partial class App : Application
{
    public static IHost AppHost { get; private set; }

    public App()
    {
        AppHost = Host.CreateDefaultBuilder()
            .UseSerilog((context, services, config) => {
                config.MinimumLevel.Information()
                      .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day);
            })
            .ConfigureServices((context, services) =>
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlite("Data Source=medicalapp.db"));

                services.AddSingleton<ImageProcessingService>();

                services.AddScoped<PatientImageService>();

                services.AddDbContext<ApplicationDbContext>();

                services.AddTransient<MainWindowViewModel>();

                services.AddSingleton<MainWindow>();

            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost.StartAsync();

        var mainWindow = AppHost.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();

        Log.Information("Приложение запущено.");

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        Log.Information("Приложение завершает работу.");
        await AppHost.StopAsync();
        AppHost.Dispose();
        base.OnExit(e);
    }
}
