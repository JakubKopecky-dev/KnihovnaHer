using KnihovnaHer.Dto;
using KnihovnaHer.Maui.ViewModels;

namespace KnihovnaHer.Maui.Views;

public partial class SeznamHerObecnyUzivatelPage : ContentPage
{
	public SeznamHerObecnyUzivatelPage()
	{
        InitializeComponent();
        BindingContext = App.Services.GetService<SeznamHerViewModel>();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is SeznamHerViewModel viewModel)
        {
            await viewModel.LoadHryAsync();
            
        }
    }



    public async void OnPridatStatusHryClicked(object sender, EventArgs e)
    {

        if (BindingContext is SeznamHerViewModel viewModel)
        {
            var button = sender as Button;
            var hraDto = button?.BindingContext as HraDto;

            if (hraDto is not null)
            {
                bool result = await viewModel.PridatStatusHryAsync(hraDto);

                if (result)
                    await DisplayAlert("Pøidáni hry", "Úspìšnì byla pøidána hra do Vašeho seznamu", "OK");
                else
                    await DisplayAlert("Chyba", "Nastala chyba pøi pøidání hry", "OK");

            }


        }

    }

}