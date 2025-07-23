using ainbox.Services;
using ainbox.Shared.Services;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ainbox
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Add device-specific services used by the ainbox.Shared project
            builder.Services.AddSingleton<IFormFactor, FormFactor>();
            builder.Services.AddSingleton<ISettingsService, SettingsService>();

            builder.Services.AddMauiBlazorWebView();

            // Configure logging for MAUI - log to file using Serilog
            var logFilePath = Path.Combine(FileSystem.AppDataDirectory, "logs", "ainbox-.log");
            Directory.CreateDirectory(Path.GetDirectoryName(logFilePath)!);

            var loggerConfig = new LoggerConfiguration()
                .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day);

#if DEBUG
            // In debug mode, also log to debug output for development
            loggerConfig.WriteTo.Debug();
#endif

            Log.Logger = loggerConfig.CreateLogger();

            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(Log.Logger);

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
#endif

            return builder.Build();
        }
    }
}
