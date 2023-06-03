using Animalib.Pages;
using Animalib.ViewModels;
using CommunityToolkit.Maui;

namespace Animalib;

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
			});

		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddTransient<AnimalsPage>();
		builder.Services.AddTransient<AnimalsPageViewModel>();
        builder.Services.AddTransient<AnimalDetailPage>();
		builder.Services.AddTransient<AnimalDetailPageViewModel>();
		builder.Services.AddTransient<SettingsPage>();
        builder.Services.AddTransient<SettingsPageViewModel>();
        builder.Services.AddTransient<SpeciesPage>();
        builder.Services.AddTransient<SpeciesPageViewModel>();
        builder.Services.AddTransient<RegionsPage>();
        builder.Services.AddTransient<RegionsPageViewModel>();

		Routing.RegisterRoute(nameof(AnimalDetailPage), typeof(AnimalDetailPage));               

        return builder.Build();
	}
}
