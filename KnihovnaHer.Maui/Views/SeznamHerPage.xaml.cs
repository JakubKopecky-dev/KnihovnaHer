using KnihovnaHer.Dto;
using KnihovnaHer.Maui.ViewModels;

namespace KnihovnaHer.Maui.Views;

public partial class SeznamHerPage : ContentPage
{
    public SeznamHerPage()
    {
        InitializeComponent();
        BindingContext = App.Services.GetService<SeznamHerViewModel>();
    }

    // tato metoda je vždy volaná když se zobrazí tato stránka
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is SeznamHerViewModel viewModel)
        {
            await viewModel.LoadHryAsync();
        }
    }


    // TLAÈÍTKO pøidat hry
    private async void OnAddGameClicked(object sender,EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(PridatHruPage));
    }


    // TLAÈÍTKO žánry a vydavatelé
    private async void OnZanrVydavatelClicked(object sender,EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ZanrVydavatelPage));
    }



    // TLAÈÍTKO EDITOVAT na seznamu her
    private async void OnEditHraClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var hraDto = button?.BindingContext as HraDto;

        if(hraDto is not null)
        {
            await Shell.Current.GoToAsync($"{nameof(PridatHruPage)}?HraId={hraDto.HraId}");
        }
    }





    // TLAÈÍTKO ODSTRANIT na seznamu her
    private async void OnDeleteHraClicked(object sender,EventArgs e)
    {
        var button = sender as Button;
        var hraDto = button?.BindingContext as HraDto;

        if (hraDto is not null)
        {
            bool confirmed = await DisplayAlert("Odstranit hru", $"Opravdu chcete odstranit hru: {hraDto.Nazev}?", "Ano", "Ne");

            if (confirmed)
            {
                if(BindingContext is SeznamHerViewModel viewModel) 
                    await viewModel.DeleteHraAsync(hraDto);
            }

        }

       

    }


}
