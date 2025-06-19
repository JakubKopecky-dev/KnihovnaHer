using KnihovnaHer.Maui.Views;

namespace KnihovnaHer.Maui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();


            Routing.RegisterRoute(nameof(SeznamHerPage),typeof(SeznamHerPage));
            Routing.RegisterRoute(nameof(PridatHruPage), typeof(PridatHruPage));
            Routing.RegisterRoute(nameof(ZanrVydavatelPage),typeof(ZanrVydavatelPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(SeznamHerObecnyUzivatelPage),typeof(SeznamHerObecnyUzivatelPage));
            Routing.RegisterRoute(nameof(SeznamUzivateluPage),typeof(SeznamUzivateluPage));
            Routing.RegisterRoute(nameof(PridatUzivatelePage),typeof(PridatUzivatelePage));
            Routing.RegisterRoute(nameof(AdminPrehledPage), typeof(AdminPrehledPage));





        }
    }
}
