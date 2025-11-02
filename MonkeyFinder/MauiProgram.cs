using Microsoft.Extensions.Logging;
using MonkeyFinder.Services;

namespace MonkeyFinder
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<IMonkeyService, MonkeyService>();
            builder.Services.AddSingleton<IMonkeysViewModel, MonkeysViewModel>();
            builder.Services.AddTransient<IMonkeyDetailsViewModel, MonkeyDetailsViewModel>();
            builder.Services.AddSingleton<View.MainPage>();
            builder.Services.AddTransient<View.DetailsPage>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
