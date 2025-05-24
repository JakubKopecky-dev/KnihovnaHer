using System.Diagnostics;
using KnihovnaHer.Maui.Services;
using KnihovnaHer.Maui.Views;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Hosting;

namespace KnihovnaHer.Maui
{
    public partial class App : Application
    {
        // Služba, která bude poskytnuta celou aplikací (prostřednictvím Dependency Injection)
        public static IServiceProvider Services { get; private set; } = null!;
        private readonly ITokenStorageService tokenStorageService;

        // Konstruktor pro aplikaci
        public App(IServiceProvider services)
        {
            InitializeComponent();
            Services = services; // DI se postará o správné naplnění Services
            tokenStorageService = Services.GetRequiredService<ITokenStorageService>();

        }



        protected override Window CreateWindow(IActivationState? activationState)
        {
            var shell = Services.GetRequiredService<AppShell>();
            var tokenService = Services.GetRequiredService<ITokenStorageService>();
            var dispatcher = Application.Current?.Dispatcher;

            if (dispatcher is not null)
            {
                dispatcher.Dispatch(async () =>
                {
                    var token = await tokenService.GetTokenAsync();
                    Debug.WriteLine("Token při startu aplikace: " + token);

                    bool isTokenValid = await tokenStorageService.IsTokenValidAsync();

                    if (!isTokenValid)
                    {
                        await Shell.Current.GoToAsync(nameof(LoginPage));
                    }
                    else
                    {
                       Debug.WriteLine($"[App] Automatické přihlášení s tokenem: {token}");
                        await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
                    }
                });
            }

            return new Window(shell);



        }












    }



}