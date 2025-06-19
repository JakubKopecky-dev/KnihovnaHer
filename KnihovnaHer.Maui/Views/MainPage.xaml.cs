using System.Diagnostics;
using System.Threading.Tasks;
using KnihovnaHer.Dto;
using KnihovnaHer.Maui.Services;
using KnihovnaHer.Maui.ViewModels;
using KnihovnaHer.Maui.Views;
using Microsoft.Extensions.DependencyInjection;

namespace KnihovnaHer.Maui.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext =App.Services.GetService<MainSeznamHerViewModel>();
        }

        
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var tokenService = App.Services.GetRequiredService<ITokenStorageService>();
            var token = await tokenService.GetTokenAsync();

            if (string.IsNullOrWhiteSpace(token))
            {
                // Přesměruj na login
                await Shell.Current.GoToAsync(nameof(LoginPage));
                return;
            }

            Debug.WriteLine("Můj token před načtením statusů: " + token);

            if (BindingContext is MainSeznamHerViewModel viewModel)
            {
                await viewModel.LoadAdminStatus();
                await viewModel.LoadStatusHer();
            }
        }

        


      



      


        private async void OnOdhlasitClicled(object sender, EventArgs e)
        {

            bool alert = await DisplayAlert("Odhlášení", "Chcete se opravdu odhlásit?", "ANO", "NE");

            if (alert)
            {
                var tokenService = App.Services.GetRequiredService<ITokenStorageService>();
                tokenService.DeleteToken();

                await Shell.Current.GoToAsync(nameof(LoginPage)); 
                
            }

        }


        private async void OnSeznamHerClicked(object sender, EventArgs e)
        {

            await Shell.Current.GoToAsync(nameof(SeznamHerObecnyUzivatelPage));
        }



       
        // detail statusy
        /*
        private async void OnDetailStatusHryClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(SeznamHerObecnyUzivatelPage));

        }
        */


        private async void OnDeleteStatusHryClicked(Object sender, EventArgs e)
        {
            var button = sender as Button;
            var statusHryDto = button?.BindingContext as StatusHryViewDto;

            if(statusHryDto is not null)
            {
                bool confirmend = await DisplayAlert("Odebrat hru", $"Opravdu chcete odebrat hru: {statusHryDto.Hra!.Nazev}", "ANO", "NE");

                if (confirmend)
                {
                    if(BindingContext is MainSeznamHerViewModel viewmodel)
                    await viewmodel.DeleteStatusHer(statusHryDto);

                }

            }

        }



      
        private async void OnAdminClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(AdminPrehledPage));
        }


      


    }
}
