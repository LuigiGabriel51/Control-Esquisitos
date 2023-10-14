using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace Control_Esquisitos;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {

        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("fa-brands-400.ttf", "fa-brand");
                fonts.AddFont("fa-solid-900.ttf", "fa-solid");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}
