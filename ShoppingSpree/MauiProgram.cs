using Microsoft.Extensions.Logging;

namespace ShoppingSpree;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
            var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("Poppins-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("Poppins-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
