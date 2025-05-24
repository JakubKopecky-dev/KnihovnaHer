using KnihovnaHer.Maui.Services;
using KnihovnaHer.Maui.ViewModels;
using KnihovnaHer.Maui.Views;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Http;
using System.Net.Http.Headers;



namespace KnihovnaHer.Maui
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


            builder.Services.AddSingleton<ITokenStorageService, TokenStorageService>();
            builder.Services.AddTransient<AuthorizationHandler>();


            builder.Services.AddHttpClient<IApiService, ApiService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7189/api/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            })
                .AddHttpMessageHandler<AuthorizationHandler>();




            builder.Services.AddSingleton<AppShell>();


            // transient = dělá se nový viewmodel pro každou page, dočasný stav => nová instance při každém otevření stránky
            // singleton = jeden viewmodel pro celou appku

            builder.Services.AddSingleton<SeznamHerViewModel>();
            builder.Services.AddTransient<VytvoritHruViewModel>();
            builder.Services.AddSingleton<ZanrVydavatelViewModel>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddSingleton<MainSeznamHerViewModel>();
            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<PridatHruPage>();
            builder.Services.AddSingleton<SeznamUzivateluViewModel>();
            builder.Services.AddTransient<PridatUzivateleViewModel>();
            builder.Services.AddTransient<PridatUzivatelePage>();
            



#if DEBUG
    		builder.Logging.AddDebug();
#endif
           


            return builder.Build();
        }
    }
}
